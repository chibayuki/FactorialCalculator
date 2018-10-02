/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
Copyright © 2018 chibayuki@foxmail.com

阶乘计算器 (FactorialCalculator)
Version 18.10.2.1600

This file is part of "阶乘计算器" (FactorialCalculator)

"阶乘计算器" (FactorialCalculator) is released under the GPLv3 license
* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using System.Text.RegularExpressions;

namespace WinFormApp
{
    public partial class Form_Main : Form
    {
        #region 版本信息

        private static readonly string ApplicationName = Application.ProductName; // 程序名。
        private static readonly string ApplicationEdition = "18"; // 程序版本。

        private static readonly Int32 MajorVersion = new Version(Application.ProductVersion).Major; // 主版本。
        private static readonly Int32 MinorVersion = new Version(Application.ProductVersion).Minor; // 副版本。
        private static readonly Int32 BuildNumber = new Version(Application.ProductVersion).Build; // 版本号。
        private static readonly Int32 BuildRevision = new Version(Application.ProductVersion).Revision; // 修订版本。
        private static readonly string LabString = "REL"; // 分支名。
        private static readonly string BuildTime = "181002-1600"; // 编译时间。

        #endregion

        #region 程序功能变量

        private struct SciN // 科学记数法。
        {
            public double Val; // 有效数字。
            public Int64 Exp; // 数量级。
        }

        private SciN SN_Input = new SciN(); // 输入的数值。

        private Color FocusedColor; // 指定日期标签获得焦点时的背景色。
        private Color PointedColor; // 指定日期标签被指向但未获得焦点时的背景色。
        private Color UnfocusedColor; // 指定日期标签失去焦点时的背景色。

        #endregion

        #region 窗体构造

        private Com.WinForm.FormManager Me;

        public Com.WinForm.FormManager FormManager
        {
            get
            {
                return Me;
            }
        }

        private void _Ctor(Com.WinForm.FormManager owner)
        {
            InitializeComponent();

            //

            if (owner != null)
            {
                Me = new Com.WinForm.FormManager(this, owner);
            }
            else
            {
                Me = new Com.WinForm.FormManager(this);
            }

            //

            FormDefine();
        }

        public Form_Main()
        {
            _Ctor(null);
        }

        public Form_Main(Com.WinForm.FormManager owner)
        {
            _Ctor(owner);
        }

        private void FormDefine()
        {
            Me.Caption = Application.ProductName;
            Me.FormStyle = Com.WinForm.FormStyle.Fixed;
            Me.EnableMaximize = false;
            Me.EnableFullScreen = false;
            Me.ClientSize = new Size(450, 230);
            Me.Theme = Com.WinForm.Theme.Colorful;
            Me.ThemeColor = Com.ColorManipulation.GetRandomColorX();

            Me.Loaded += LoadedEvents;
            Me.Closed += ClosedEvents;
            Me.SizeChanged += SizeChangedEvents;
            Me.ThemeChanged += ThemeColorChangedEvents;
            Me.ThemeColorChanged += ThemeColorChangedEvents;
        }

        #endregion

        #region 窗体事件

        private void LoadedEvents(object sender, EventArgs e)
        {
            //
            // 在窗体加载后发生。
            //

            Me.OnThemeChanged();

            //

            ReturnToZero();
        }

        private void ClosedEvents(object sender, EventArgs e)
        {
            //
            // 在窗体关闭后发生。
            //

            Calc_Stop();
        }

        private void SizeChangedEvents(object sender, EventArgs e)
        {
            //
            // 在窗体的大小更改时发生。
            //

            Panel_Client.Size = Panel_Main.Size;

            //

            Panel_Factorial.Refresh();
        }

        private void ThemeColorChangedEvents(object sender, EventArgs e)
        {
            //
            // 在窗体的主题色更改时发生。
            //

            FocusedColor = Me.RecommendColors.Background_INC.ToColor();
            PointedColor = Me.RecommendColors.Background.ToColor();
            UnfocusedColor = Color.Transparent;

            //

            Label_AppName.ForeColor = Me.RecommendColors.Text.ToColor();
            Label_Note.ForeColor = Me.RecommendColors.Text_DEC.ToColor();

            //

            Label_ReturnToZero.ForeColor = Me.RecommendColors.Text.ToColor();
            Label_ReturnToZero.BackColor = Me.RecommendColors.Background_DEC.ToColor();

            Panel_Input.BackColor = Panel_Output.BackColor = Me.RecommendColors.Background_DEC.ToColor();

            Label_Input.ForeColor = Label_Factorial.ForeColor = Me.RecommendColors.Text.ToColor();
            Label_Equal.ForeColor = Label_Val.ForeColor = Label_Exp.ForeColor = Label_ExpExp.ForeColor = Me.RecommendColors.Text.ToColor();

            Label_Time.ForeColor = (Me.RecommendColors.Main.Lightness_LAB < 70 ? Color.White : Color.Black);
            Label_Time.BackColor = Me.RecommendColors.Main.ToColor();

            ContextMenuStrip_Input.BackColor = Me.RecommendColors.MenuItemBackground.ToColor();
            ToolStripMenuItem_Input_Copy.ForeColor = ToolStripMenuItem_Input_Paste.ForeColor = Me.RecommendColors.MenuItemText.ToColor();

            ContextMenuStrip_Output.BackColor = Me.RecommendColors.MenuItemBackground.ToColor();
            ToolStripMenuItem_Output_Copy.ForeColor = Me.RecommendColors.MenuItemText.ToColor();

            //

            Com.WinForm.ControlSubstitution.LabelAsButton(Label_ReturnToZero, Label_ReturnToZero_Click, Me.RecommendColors.Background_DEC.ToColor(), PointedColor, FocusedColor);
        }

        #endregion

        #region 背景绘图

        private void Panel_Factorial_Paint(object sender, PaintEventArgs e)
        {
            //
            // Panel_Factorial 绘图。
            //

            Pen BorderLine = new Pen(Me.RecommendColors.Border_DEC.ToColor(), 1);
            Pen BorderLine_Shadow = new Pen(Me.RecommendColors.Border_DEC.AtAlpha(96).ToColor(), 1);

            e.Graphics.DrawRectangle(BorderLine_Shadow, Com.Geometry.GetMinimumBoundingRectangleOfControls(new Control[] { Label_ReturnToZero }, 3));
            e.Graphics.DrawRectangle(BorderLine, Com.Geometry.GetMinimumBoundingRectangleOfControls(new Control[] { Label_ReturnToZero }, 2));

            e.Graphics.DrawRectangle(BorderLine_Shadow, Com.Geometry.GetMinimumBoundingRectangleOfControls(new Control[] { Panel_Input, Panel_Output }, 3));
            e.Graphics.DrawRectangle(BorderLine, Com.Geometry.GetMinimumBoundingRectangleOfControls(new Control[] { Panel_Input, Panel_Output }, 2));
        }

        #endregion

        #region 阶乘函数

        private SciN RecursionForGamma(double V)
        {
            //
            // 伽玛函数的递归函数。对于 0 或正实数 V，计算 V * (V - 1) * (V - 2) * ... * (V - Math.Floor(V)) 的值。
            //

            if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.CancellationPending)
            {
                return new SciN();
            }

            //

            SciN SN = new SciN();

            double Vx = V - Math.Floor(V);

            if (V < 1)
            {
                SN.Val = V;
                SN.Exp = 0;
            }
            else if (V < 2)
            {
                SN.Val = V * Vx;
                SN.Exp = 0;
            }
            else
            {
                Int64 Vm = (Int64)Math.Pow(10, Math.Floor(Math.Log10(V)));

                if (Vm == V - Vx)
                {
                    Vm /= 10;
                }

                SciN RGx = RecursionForGamma(Vm + Vx);

                SN.Val = RGx.Val;
                SN.Exp = RGx.Exp + (Int64)(Math.Log10(Vm) * (V - Vx - Vm));

                for (double i = Vm + Vx + 1; i <= V; i++)
                {
                    if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.CancellationPending)
                    {
                        return new SciN();
                    }

                    if ((Int64)i % 1048576 == 0 && LastReportProgressToNow.TotalMilliseconds >= 1000)
                    {
                        ReportRemainingTime(i);
                    }

                    //

                    SN.Val *= (i / Vm);

                    if (SN.Val >= 10)
                    {
                        SN.Val /= 10;
                        SN.Exp++;
                    }
                }
            }

            return SN;
        }

        private static readonly double[] Coeff = new double[] { 0.99999999999980993, 676.5203681218851, -1259.1392167224028, 771.32342877765313, -176.61502916214059, 12.507343278686905, -0.13857109526572012, 9.9843695780195716E-6, 1.5056327351493116E-7 }; // 切比雪夫多项式系数。

        private SciN Gamma(double V)
        {
            //
            // 伽玛函数。计算 -1E14 + 0.1 至 1E14 - 0.1 之间的小数的阶乘。
            //

            if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.CancellationPending)
            {
                return new SciN();
            }

            //

            SciN SN = new SciN();

            if (V <= 0) // 欧拉反射公式。计算小于 0 的小数的伽玛值。
            {
                SciN R = Gamma(1 - V);

                if (R.Val == 0)
                {
                    SN.Val = 0;
                    SN.Exp = 0;
                }
                else
                {
                    SN.Val = Math.PI / (Math.Sin(Math.PI * V) * R.Val);
                    SN.Exp = -R.Exp;

                    while (SN.Val <= -10 || SN.Val >= 10)
                    {
                        SN.Val /= 10;
                        SN.Exp++;
                    }

                    if (SN.Val == 0)
                    {
                        SN.Exp = 0;
                    }
                    else
                    {
                        while (SN.Val > -1 && SN.Val < 1)
                        {
                            SN.Val *= 10;
                            SN.Exp--;
                        }
                    }
                }
            }
            else if (V > 0 && V <= 1) // 切比雪夫算法。计算 0 到 1 之间的小数的伽玛值。
            {
                double Sum = Coeff[0];

                V--;

                for (int i = 1; i < Coeff.Count(); i++)
                {
                    if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.CancellationPending)
                    {
                        return new SciN();
                    }

                    Sum += Coeff[i] / (V + i);
                }

                double Base = V + 0.5 + (Coeff.Count() - 2);

                SN.Val = Math.Sqrt(2 * Math.PI) * Math.Pow(Base, V + 0.5) * Math.Exp(-Base) * Sum;
                SN.Exp = 0;

                while (SN.Val >= 10)
                {
                    SN.Val /= 10;
                    SN.Exp++;
                }
            }
            else // 递推公式。计算大于 1 的小数的伽玛值。
            {
                SciN RG = RecursionForGamma(V - 1);

                SN.Val = RG.Val;
                SN.Exp = RG.Exp;

                double Vx = V - Math.Truncate(V);

                SciN R = Gamma(Vx);

                SN.Val *= R.Val;
                SN.Exp += R.Exp;

                while (SN.Val >= 10)
                {
                    SN.Val /= 10;
                    SN.Exp++;
                }
            }

            return SN;
        }

        private struct FactRslt // 阶乘函数的计算结果。
        {
            public string ValStr; // 底数的字符串。
            public string ExpStr; // 指数的字符串。
            public string ExpExpStr; // 指数的指数的字符串。
            public bool IsExactValue; // 此计算结果是（true）否为准确值。
        }

        private FactRslt Stirling(SciN SN)
        {
            //
            // 斯特林公式。计算 0 至 1E9223372036854775808 - 1 之间的实数的阶乘的近似值。
            //

            if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.CancellationPending)
            {
                return new FactRslt();
            }

            //

            FactRslt FR = new FactRslt();
            FR.IsExactValue = false;

            if (SN.Val >= 0) // 计算 0 与正数的阶乘。
            {
                if (SN.Exp <= 14) // 斯特林公式。计算 0 至 1E15 - 1 的阶乘。
                {
                    double V = SN.Val * Math.Pow(10, SN.Exp);

                    if (V == 0 || V == 1) // 计算 0 或 1 的阶乘。
                    {
                        FR.ValStr = "1";
                    } // 计算 0 或 1 的阶乘。
                    else
                    {
                        double FR_Val_F = 0; // 阶乘结果的底数。
                        decimal FR_Exp_M = 0; // 阶乘结果的指数（十进制数）。

                        double XdivE_Val = SN.Val / Math.E;
                        decimal XdivE_Exp = SN.Exp;

                        if (XdivE_Val == 0)
                        {
                            XdivE_Exp = 0;
                        }
                        else
                        {
                            if (XdivE_Val < 1)
                            {
                                XdivE_Val *= 10;
                                XdivE_Exp--;
                            }
                        }

                        FR_Val_F = Math.Pow(XdivE_Val, SN.Val);
                        FR_Exp_M = XdivE_Exp * (decimal)SN.Val * (decimal)Math.Pow(10, SN.Exp);

                        decimal ExpTmp = 0;

                        for (long i = 1; i <= SN.Exp; i++)
                        {
                            if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.CancellationPending)
                            {
                                return new FactRslt();
                            }

                            ExpTmp *= 10;
                            FR_Val_F = Math.Pow(FR_Val_F, 10);
                            ExpTmp += (decimal)Math.Floor(Math.Log10(FR_Val_F));
                            FR_Val_F /= Math.Pow(10, Math.Floor(Math.Log10(FR_Val_F)));
                        }

                        FR_Exp_M += ExpTmp;
                        FR_Exp_M += (decimal)(0.5 * SN.Exp);

                        double Sq2PiX = Math.Sqrt(2 * Math.PI * SN.Val);

                        FR_Val_F *= Sq2PiX * Math.Pow(10, (double)(FR_Exp_M - Math.Floor(FR_Exp_M)));
                        FR_Exp_M = Math.Floor(FR_Exp_M);

                        while (FR_Val_F >= 10)
                        {
                            FR_Val_F /= 10;
                            FR_Exp_M++;
                        }

                        if (FR_Exp_M >= 15 || FR_Exp_M <= -5)
                        {
                            FR.ValStr = FR_Val_F + " × 10";
                            FR.ExpStr = ((Int64)Math.Round(FR_Exp_M)).ToString();
                        }
                        else
                        {
                            FR.ValStr = (FR_Val_F * Math.Pow(10, (double)FR_Exp_M)).ToString();
                        }
                    }
                } // 斯特林公式。计算 0 至 1E15 - 1 的阶乘。
                else if (SN.Exp <= 304) // 斯特林公式。计算 0 至 1E305 - 1 的阶乘。
                {
                    double FR_Val_F = 0; // 阶乘结果的底数。
                    double FR_Exp_F = 0; // 阶乘结果的指数。
                    UInt64 FR_ExpExp_UL = 0; // 阶乘结果的指数的指数（64 位无符号整数）。

                    double XdivE_Val = SN.Val / Math.E;
                    double XdivE_Exp = SN.Exp;

                    if (XdivE_Val == 0)
                    {
                        XdivE_Exp = 0;
                    }
                    else
                    {
                        if (XdivE_Val < 1)
                        {
                            XdivE_Val *= 10;
                            XdivE_Exp--;
                        }
                    }

                    FR_Val_F = Math.Pow(XdivE_Val, SN.Val);
                    FR_Exp_F = XdivE_Exp * SN.Val * Math.Pow(10, SN.Exp);

                    double ExpTmp = 0;

                    for (long i = 1; i <= SN.Exp; i++)
                    {
                        if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.CancellationPending)
                        {
                            return new FactRslt();
                        }

                        ExpTmp *= 10;
                        FR_Val_F = Math.Pow(FR_Val_F, 10);
                        ExpTmp += Math.Floor(Math.Log10(FR_Val_F));
                        FR_Val_F /= Math.Pow(10, Math.Floor(Math.Log10(FR_Val_F)));
                    }

                    FR_Exp_F += ExpTmp;
                    FR_Exp_F += 0.5 * SN.Exp;

                    double Sq2PiX = Math.Sqrt(2 * Math.PI * SN.Val);

                    FR_Val_F *= Sq2PiX * Math.Pow(10, FR_Exp_F - Math.Floor(FR_Exp_F));
                    FR_Exp_F = Math.Floor(FR_Exp_F);

                    while (FR_Val_F >= 10)
                    {
                        FR_Val_F /= 10;
                        FR_Exp_F++;
                    }

                    Int32 ExpIndex = FR_Exp_F.ToString().IndexOf("E");

                    if (ExpIndex > 0)
                    {
                        FR_ExpExp_UL = Convert.ToUInt64(FR_Exp_F.ToString().Substring(ExpIndex + 2));
                        FR_Exp_F = Convert.ToDouble(FR_Exp_F.ToString().Substring(0, ExpIndex));

                        FR.ValStr = FR_Val_F + " × 10";
                        FR.ExpStr = FR_Exp_F + " × 10";
                        FR.ExpExpStr = FR_ExpExp_UL.ToString();
                    }
                    else
                    {
                        if (FR_Exp_F >= 15 || FR_Exp_F <= -5)
                        {
                            FR.ValStr = FR_Val_F + " × 10";
                            FR.ExpStr = FR_Exp_F.ToString();
                        }
                        else
                        {
                            FR.ValStr = (FR_Val_F * Math.Pow(10, FR_Exp_F)).ToString();
                        }
                    }
                } // 斯特林公式。计算 1E15 至 1E305 - 1 的阶乘。
                else  // 斯特林公式。计算 1E305 至 1E9223372036854775808 - 1 的阶乘。
                {
                    double FR_Val_F = 0; // 阶乘结果的底数。
                    double FR_Exp_F = 0; // 阶乘结果的指数。
                    UInt64 FR_ExpExp_UL = 0; // 阶乘结果的指数的指数（64 位无符号整数）。

                    double XdivE_Val = SN.Val / Math.E;
                    double XdivE_Exp = SN.Exp;

                    if (XdivE_Val == 0)
                    {
                        XdivE_Exp = 0;
                    }
                    else
                    {
                        if (XdivE_Val < 1)
                        {
                            XdivE_Val *= 10;
                            XdivE_Exp--;
                        }
                    }

                    FR_Val_F = Math.Pow(XdivE_Val, SN.Val);
                    FR_Exp_F = XdivE_Exp * SN.Val;
                    FR_ExpExp_UL = (UInt64)SN.Exp;

                    if (FR_Exp_F >= 10)
                    {
                        FR_Exp_F /= 10;
                        FR_ExpExp_UL++;
                    }

                    double ExpTmp = 0;

                    double SqExp = Math.Sqrt(SN.Exp);

                    for (long i = 1; i <= SqExp; i++)
                    {
                        if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.CancellationPending)
                        {
                            return new FactRslt();
                        }

                        if (i % 1048576 == 0 && LastReportProgressToNow.TotalMilliseconds >= 1000)
                        {
                            ReportRemainingTime(i);
                        }

                        //

                        FR_Val_F = Math.Pow(FR_Val_F, 10);
                        ExpTmp += Math.Pow(10, (-1 - i)) * Math.Floor(Math.Log10(FR_Val_F));
                        FR_Val_F /= Math.Pow(10, Math.Floor(Math.Log10(FR_Val_F)));
                    }

                    FR_Exp_F += ExpTmp;

                    double Sq2PiX = Math.Sqrt(2 * Math.PI * SN.Val);

                    FR_Val_F *= Sq2PiX;

                    while (FR_Val_F >= 10)
                    {
                        FR_Val_F /= 10;
                    }

                    while (FR_Exp_F >= 10)
                    {
                        FR_Exp_F /= 10;
                        FR_ExpExp_UL++;
                    }

                    FR.ValStr = FR_Val_F + " × 10";
                    FR.ExpStr = FR_Exp_F + " × 10";
                    FR.ExpExpStr = FR_ExpExp_UL.ToString();
                } // 斯特林公式。计算 1E305 至 1E9223372036854775808 - 1 的阶乘。
            }
            else
            {
                FR.ValStr = "未定义";
            }

            return FR;
        }

        private SciN RecursionForFactorial(Int64 L)
        {
            //
            // 阶乘函数的递归函数。计算 1 至 1E15 - 1 之间的整数的阶乘。
            //

            if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.CancellationPending)
            {
                return new SciN();
            }

            //

            SciN SN = new SciN();

            if (L == 1)
            {
                SN.Val = 1;
                SN.Exp = 0;
            }
            else
            {
                Int64 Lv = (Int64)Math.Pow(10, Math.Floor(Math.Log10(L)));

                if (Lv == L)
                {
                    Lv /= 10;
                }

                SciN RFx = RecursionForFactorial(Lv);

                SN.Val = RFx.Val;
                SN.Exp = RFx.Exp + (Int64)Math.Log10(Lv) * (L - Lv);

                for (double i = Lv + 1; i <= L; i++)
                {
                    if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.CancellationPending)
                    {
                        return new SciN();
                    }

                    if ((Int64)i % 1048576 == 0 && LastReportProgressToNow.TotalMilliseconds >= 1000)
                    {
                        ReportRemainingTime(i);
                    }

                    //

                    SN.Val *= (i / Lv);

                    if (SN.Val >= 10)
                    {
                        SN.Val /= 10;
                        SN.Exp++;
                    }
                }
            }

            return SN;
        }

        private FactRslt Factorial(SciN SN)
        {
            //
            // 阶乘函数。
            //

            if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.CancellationPending)
            {
                return new FactRslt();
            }

            //

            FactRslt FR = new FactRslt();
            FR.IsExactValue = true;

            if (SN.Exp >= (new Regex(@"[^\d]").Replace(SN.Val.ToString(), string.Empty)).Length - 1) // 计算整数的阶乘。
            {
                if (SN.Val >= 0) // 计算 0 与正整数的阶乘。
                {
                    if (SN.Exp <= 14) // 计算 0 至 1E15 - 1 的阶乘。
                    {
                        Int64 V_L = (Int64)Math.Round(SN.Val * Math.Pow(10, SN.Exp));

                        if (V_L == 0 || V_L == 1) // 计算 0 或 1 的阶乘。
                        {
                            FR.ValStr = "1";
                        } // 计算 0 或 1 的阶乘。
                        else if (V_L <= 20) // 计算 2 至 20 的阶乘。
                        {
                            Int64 FR_Val_L = 1;

                            for (long i = 1; i <= V_L; i++)
                            {
                                if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.CancellationPending)
                                {
                                    return new FactRslt();
                                }

                                FR_Val_L *= i;
                            }

                            FR.ValStr = FR_Val_L.ToString();
                        } // 计算 2 至 20 的阶乘。
                        else // 调用 RecursionForFactorial 函数。计算 21 至 1E15 - 1 的阶乘。
                        {
                            SciN RF = RecursionForFactorial(V_L);

                            FR.ValStr = RF.Val + " × 10";
                            FR.ExpStr = RF.Exp.ToString();
                        } // 调用 RecursionForFactorial 函数。计算 21 至 1E15 - 1 的阶乘。
                    } // 计算 0 至 1E15 - 1 的阶乘。
                    else// 斯特林公式。计算 1E15 至 1E9223372036854775808 - 1 的阶乘。
                    {
                        FR = Stirling(SN);
                    }
                } // 计算正整数的阶乘。
                else // 计算负整数的阶乘。
                {
                    FR.ValStr = "无穷大";
                } // 计算负整数的阶乘。
            } // 计算整数的阶乘。
            else // 计算小数的阶乘。
            {
                if (SN.Exp <= 14) // 调用 Gamma 函数。计算 -1E14 + 0.1 至 1E14 - 0.1 之间的小数的阶乘。
                {
                    double V = SN.Val * Math.Pow(10, SN.Exp);

                    SciN G = Gamma(V + 1);

                    if (G.Exp >= -4 && G.Exp <= 14)
                    {
                        FR.ValStr = (G.Val * Math.Pow(10, G.Exp)).ToString();
                    }
                    else
                    {
                        FR.ValStr = G.Val + " × 10";
                        FR.ExpStr = G.Exp.ToString();
                    }
                } // 调用 Gamma 函数。计算 -1E14 + 0.1 至 1E14 - 0.1 之间的小数的阶乘。
                else
                {
                    FR.ValStr = "无穷大";
                }
            } // 计算小数的阶乘。

            return FR;
        }

        #endregion

        #region 后台计算

        private string Result_Equal, Result_Val, Result_Exp, Result_ExpExp, Result_Time, Result_Time_BefStr; // 计算结果字符串。

        private void RefreshResult()
        {
            //
            // 刷新计算结果。
            //

            Label_Equal.Text = Result_Equal;
            Label_Val.Text = Result_Val;
            Label_Exp.Text = Result_Exp;
            Label_ExpExp.Text = Result_ExpExp;
            Label_Time.Text = Result_Time;
        }

        // 后台计算异步线程。

        private BackgroundWorker BackgroundWorker_Calc = new BackgroundWorker(); // 后台计算异步线程。

        private DateTime LastWorkAsync = DateTime.Now; // 上次开始异步工作的时刻。
        private TimeSpan LastWorkAsyncToNow => DateTime.Now - LastWorkAsync; // 上次开始异步工作到现在的时间间隔。

        private void BackgroundWorker_Calc_DoWork(object sender, DoWorkEventArgs e)
        {
            //
            // 后台计算执行异步工作。
            //

            LastWorkAsync = DateTime.Now;

            //

            Calc_WorkAsync();
        }

        private DateTime LastReportProgress = DateTime.Now; // 上次报告异步工作进度的时刻。
        private TimeSpan LastReportProgressToNow => DateTime.Now - LastReportProgress; // 上次报告异步工作进度到现在的时间间隔。

        private void BackgroundWorker_Calc_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //
            // 后台计算异步工作进度改变。
            //

            LastReportProgress = DateTime.Now;

            //

            Calc_ReportProgress();
        }

        private void BackgroundWorker_Calc_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //
            // 后台计算异步工作完成。
            //

            if (!e.Cancelled)
            {
                Calc_WorkDone();
            }
        }

        // 计算步骤周期与时间。

        private const Int32 CycSteps = 1; // 最大计算步骤数。

        private double[] CycCount = new double[CycSteps]; // 所有计算步骤分别需要的循环或递归的周期数。

        private Int32 _CycDone = 0; // 已完成的计算步骤数。
        private Int32 CycDone
        {
            get
            {
                return _CycDone;
            }

            set
            {
                _CycDone = Math.Max(0, Math.Min(value, CycSteps));
            }
        }

        private double CycCount_Total // 所有计算步骤需要的循环或递归的总周期数。
        {
            get
            {
                double _CycCount_Total = 0;

                for (int i = 0; i <= CycSteps - 1; i++)
                {
                    double V = (double.IsNaN(CycCount[i]) || double.IsInfinity(CycCount[i]) || CycCount[i] <= 0 ? 0 : CycCount[i]);

                    _CycCount_Total += V;
                }

                return Math.Max(1, _CycCount_Total);
            }
        }
        private double CycCount_Done // 已完成的计算步骤数包含的循环或递归的周期数。
        {
            get
            {
                double _CycCount_Done = 0;

                for (int i = 0; i <= CycSteps - 1; i++)
                {
                    if (CycDone >= i + 1)
                    {
                        double V = (double.IsNaN(CycCount[i]) || double.IsInfinity(CycCount[i]) || CycCount[i] <= 0 ? 0 : CycCount[i]);

                        _CycCount_Done += V;
                    }
                    else
                    {
                        break;
                    }
                }

                return Math.Max(1, _CycCount_Done);
            }
        }

        private void CycReset()
        {
            //
            // 将 CycCount 的所有元素和 CycDone 重置为 0。
            //

            for (int i = 0; i <= CycSteps - 1; i++)
            {
                CycCount[i] = 0;
            }

            CycDone = 0;
        }

        private void ReportRemainingTime(double Cyc)
        {
            //
            // 向后台计算异步线程报告剩余时间。Cyc：当前计算步骤已完成的循环或递归的周期数。
            //

            if (BackgroundWorker_Calc != null && BackgroundWorker_Calc.WorkerReportsProgress && BackgroundWorker_Calc.IsBusy)
            {
                double _Cyc = CycCount_Done + Cyc;

                TimeSpan TS = TimeSpan.FromMilliseconds(LastWorkAsyncToNow.TotalMilliseconds / _Cyc * (CycCount_Total - _Cyc));

                Result_Time = Result_Time_BefStr + (TS.TotalMilliseconds >= 1000 ? "还需大约 " + GetTimeStringFromTimeSpan(TS) : "即将完成");

                BackgroundWorker_Calc.ReportProgress(0);
            }
        }

        // 后台计算异步工作控制。

        private void Calc_Start()
        {
            //
            // 后台计算开始。
            //

            CycReset();

            Result_Equal = Result_Val = Result_Exp = Result_ExpExp = Result_Time = Result_Time_BefStr = string.Empty;

            //

            BackgroundWorker_Calc = new BackgroundWorker();

            BackgroundWorker_Calc.WorkerReportsProgress = true;
            BackgroundWorker_Calc.WorkerSupportsCancellation = true;
            BackgroundWorker_Calc.DoWork += BackgroundWorker_Calc_DoWork;
            BackgroundWorker_Calc.ProgressChanged += BackgroundWorker_Calc_ProgressChanged;
            BackgroundWorker_Calc.RunWorkerCompleted += BackgroundWorker_Calc_RunWorkerCompleted;

            if (!BackgroundWorker_Calc.IsBusy)
            {
                BackgroundWorker_Calc.RunWorkerAsync();
            }
        }

        private void Calc_Stop()
        {
            //
            // 后台计算停止。
            //

            if (BackgroundWorker_Calc != null)
            {
                BackgroundWorker_Calc.DoWork -= BackgroundWorker_Calc_DoWork;
                BackgroundWorker_Calc.ProgressChanged -= BackgroundWorker_Calc_ProgressChanged;
                BackgroundWorker_Calc.RunWorkerCompleted -= BackgroundWorker_Calc_RunWorkerCompleted;

                if (BackgroundWorker_Calc.IsBusy)
                {
                    BackgroundWorker_Calc.CancelAsync();
                }

                BackgroundWorker_Calc.Dispose();
            }
        }

        private void Calc_Restart()
        {
            //
            // 后台计算停止并重新开始。
            //

            Calc_Stop();
            Calc_Start();
        }

        private void Calc_WorkAsync()
        {
            //
            // 后台计算异步工作内容。
            //

            if (Validity_Input)
            {
                if (SN_Input.Val >= 0)
                {
                    if (SN_Input.Exp <= 14)
                    {
                        double V = SN_Input.Val * Math.Pow(10, SN_Input.Exp);

                        FactRslt FR_Stirling = Stirling(SN_Input);

                        CycCount[0] = Math.Abs(SN_Input.Val) * Math.Pow(10, SN_Input.Exp);

                        Result_Equal = "≈";
                        Result_Val = FR_Stirling.ValStr;
                        Result_Exp = FR_Stirling.ExpStr;
                        Result_ExpExp = FR_Stirling.ExpExpStr;
                        Result_Time = "正在计算准确值…";
                        Result_Time_BefStr = "正在计算准确值，";
                    }
                    else
                    {
                        CycCount[0] = Math.Sqrt(SN_Input.Exp);

                        Result_Equal = "≈";
                        Result_Val = "正在计算…";
                        Result_Time = "正在计算…";
                        Result_Time_BefStr = "正在计算，";
                    }
                }
                else
                {
                    CycCount[0] = Math.Abs(SN_Input.Val) * Math.Pow(10, SN_Input.Exp);

                    Result_Equal = "=";
                    Result_Val = "正在计算…";
                    Result_Time = "正在计算…";
                    Result_Time_BefStr = "正在计算，";
                }

                BackgroundWorker_Calc.ReportProgress(0);

                //

                Stopwatch Sw = new Stopwatch();
                Sw.Restart();

                FactRslt FR = Factorial(SN_Input);

                Sw.Stop();

                Result_Equal = (FR.IsExactValue ? "=" : "≈");
                Result_Val = FR.ValStr;
                Result_Exp = FR.ExpStr;
                Result_ExpExp = FR.ExpExpStr;
                Result_Time = "用时 " + GetTimeStringFromTimeSpan(Sw.Elapsed);
                Result_Time_BefStr = string.Empty;
            }
            else
            {
                Result_Equal = "=";
                Result_Val = "无效输入";
                Result_Exp = Result_ExpExp = string.Empty;
                Result_Time = Result_Time_BefStr = string.Empty;
            }
        }

        private void Calc_ReportProgress()
        {
            //
            // 后台计算报告异步工作进度。
            //

            RefreshResult();
        }

        private void Calc_WorkDone()
        {
            //
            // 后台计算异步工作完成。
            //

            RefreshResult();
        }

        #endregion

        #region 输入输出

        // 归零。

        private void ReturnToZero()
        {
            //
            // 归零。
            //

            Calc_Stop();

            //

            SN_Input = new SciN();
            Label_Input.TextChanged -= Label_Input_TextChanged;
            Label_Input.Text = "0";
            Label_Input.TextChanged += Label_Input_TextChanged;
            Validity_Input = true;

            //

            Label_Equal.Text = "=";
            Label_Val.Text = "1";
            Label_Exp.Text = Label_ExpExp.Text = string.Empty;
            Label_Time.Text = string.Empty;

            //

            Label_Input.Focus();
        }

        private void Label_ReturnToZero_Click(object sender, EventArgs e)
        {
            //
            // 单击 Label_ReturnToZero。
            //

            ReturnToZero();
        }

        // 输入合法性。

        private bool Validity_Input = false; // 输入的数值的合法性。

        // 输入区域容器。

        private void Panel_Input_MouseMove(object sender, MouseEventArgs e)
        {
            //
            // 鼠标经过 Panel_Input。
            //

            Point PTC = Panel_Input.PointToClient(Cursor.Position);

            if (PTC.X <= Label_Factorial.Left)
            {
                if (!Label_Input.Focused)
                {
                    Label_Input.BackColor = PointedColor;
                }
            }
            else
            {
                if (!Label_Input.Focused)
                {
                    Label_Input.BackColor = UnfocusedColor;
                }
            }
        }

        private void Panel_Input_MouseLeave(object sender, EventArgs e)
        {
            //
            // 鼠标离开 Panel_Input。
            //

            if (!Label_Input.Focused)
            {
                Label_Input.BackColor = UnfocusedColor;
            }
        }

        private void Panel_Input_MouseDown(object sender, MouseEventArgs e)
        {
            //
            // 鼠标按下 Panel_Input。
            //

            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                Point PTC = Panel_Input.PointToClient(Cursor.Position);

                if (PTC.X <= Label_Factorial.Left)
                {
                    Label_Input.Focus();
                }
            }
        }

        private void Panel_Input_MouseUp(object sender, MouseEventArgs e)
        {
            //
            // 鼠标释放 Panel_Input。
            //

            if (e.Button == MouseButtons.Right)
            {
                Point PTC = Panel_Input.PointToClient(Cursor.Position);

                if (PTC.X <= Label_Factorial.Left)
                {
                    ContextMenuStrip_Input.Show(Cursor.Position);
                }
            }
        }

        private void Panel_Input_LocationChanged(object sender, EventArgs e)
        {
            //
            // Panel_Input 位置改变。
            //

            Panel_Output.Left = Panel_Input.Right;
        }

        private void Panel_Input_SizeChanged(object sender, EventArgs e)
        {
            //
            // Panel_Input 大小改变。
            //

            Panel_Output.Left = Panel_Input.Right;
        }

        // 输入值。

        private void Label_Input_MouseMove(object sender, MouseEventArgs e)
        {
            //
            // 鼠标经过 Label_Input。
            //

            if (!Label_Input.Focused)
            {
                Label_Input.BackColor = PointedColor;
            }
        }

        private void Label_Input_MouseLeave(object sender, EventArgs e)
        {
            //
            // 鼠标离开 Label_Input。
            //

            if (!Label_Input.Focused)
            {
                Label_Input.BackColor = UnfocusedColor;
            }
        }

        private void Label_Input_MouseDown(object sender, MouseEventArgs e)
        {
            //
            // 鼠标按下 Label_Input。
            //

            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                Label_Input.Focus();
            }
        }

        private void Label_Input_GotFocus(object sender, EventArgs e)
        {
            //
            // Label_Input 接收焦点。
            //

            Label_Input.BackColor = FocusedColor;
        }

        private void Label_Input_LostFocus(object sender, EventArgs e)
        {
            //
            // Label_Input 失去焦点。
            //

            Label_Input.BackColor = UnfocusedColor;
        }

        private void Label_Input_KeyDown(object sender, KeyEventArgs e)
        {
            //
            // 在 Label_Input 按下键。
            //

            if (Label_Input.Visible)
            {
                switch (e.KeyCode)
                {
                    case Keys.D0:
                    case Keys.NumPad0:
                        Label_Input.Text += "0";
                        break;

                    case Keys.D1:
                    case Keys.NumPad1:
                        Label_Input.Text += "1";
                        break;

                    case Keys.D2:
                    case Keys.NumPad2:
                        Label_Input.Text += "2";
                        break;

                    case Keys.D3:
                    case Keys.NumPad3:
                        Label_Input.Text += "3";
                        break;

                    case Keys.D4:
                    case Keys.NumPad4:
                        Label_Input.Text += "4";
                        break;

                    case Keys.D5:
                    case Keys.NumPad5:
                        Label_Input.Text += "5";
                        break;

                    case Keys.D6:
                    case Keys.NumPad6:
                        Label_Input.Text += "6";
                        break;

                    case Keys.D7:
                    case Keys.NumPad7:
                        Label_Input.Text += "7";
                        break;

                    case Keys.D8:
                    case Keys.NumPad8:
                        Label_Input.Text += "8";
                        break;

                    case Keys.D9:
                    case Keys.NumPad9:
                        Label_Input.Text += "9";
                        break;

                    case Keys.OemMinus:
                    case Keys.Subtract:
                        Label_Input.Text += "-";
                        break;

                    case Keys.OemPeriod:
                    case Keys.Decimal:
                        Label_Input.Text += ".";
                        break;

                    case Keys.E:
                    case Keys.X:
                        Label_Input.Text += "E";
                        break;

                    case Keys.Back:
                        Label_Input.Text = Label_Input.Text.Substring(0, Math.Max(0, Label_Input.Text.Length - 1));
                        break;

                    case Keys.Delete:
                        Label_Input.Text = "0";
                        break;

                    case Keys.Escape:
                        ReturnToZero();
                        break;
                }
            }
        }

        private void Label_Input_TextChanged(object sender, EventArgs e)
        {
            //
            // Label_Input 文本改变。
            //

            SciN SN;

            string Str = Label_Input.Text;

            REJUDGE:

            SN = new SciN();

            Str = new Regex(@"[^\d\.\-Ee]").Replace(Str, string.Empty).ToUpper();

            Int32 MCount = Regex.Matches(Str, @"-").Count; // "-" 出现在文本的次数。
            Int32 EIndex = Str.IndexOf("E"); // "E" 第一次出现在文本的位置。
            Int32 DIndex = Str.IndexOf("."); // "." 第一次出现在文本的位置。

            if (MCount % 2 == 0) // "-" 出现偶数次。
            {
                if (EIndex == -1) // "E" 未出现。
                {
                    if (DIndex == -1) // "." 未出现。
                    {
                        string Text = new Regex(@"[^\d]").Replace(Str, string.Empty).Substring(0, Math.Min(15, new Regex(@"[^\d]").Replace(Str, string.Empty).Length));

                        if (Text.Length > 0)
                        {
                            string _Str = Convert.ToDouble(Text).ToString();

                            SN.Val = Convert.ToDouble(_Str.Substring(0, 1) + "." + _Str.Substring(1));
                            SN.Exp = _Str.Length - 1;

                            Str = _Str;
                        }
                        else
                        {
                            SN.Val = 0;
                            SN.Exp = 0;

                            Str = "0";
                        }

                        Validity_Input = true;
                    } // "." 未出现。
                    else // "." 出现。
                    {
                        string BeforeD = new Regex(@"[^\d]").Replace(Str.Substring(0, DIndex), string.Empty);
                        string AfterD = new Regex(@"[^\d]").Replace(Str.Substring(DIndex + 1), string.Empty);

                        string Part1 = BeforeD.Substring(0, Math.Min(15, BeforeD.Length));
                        string Part2 = AfterD.Substring(0, Math.Min(15 - Part1.Length, AfterD.Length));

                        if (Part1.Length > 0)
                        {
                            if (Part2.Length > 0)
                            {
                                string _Str = Convert.ToDouble(Part1 + Part2).ToString();

                                SN.Val = Convert.ToDouble(_Str.Substring(0, 1) + "." + _Str.Substring(1));

                                if (Convert.ToDouble(Part1) == 0)
                                {
                                    if (Convert.ToDouble(Part2) == 0)
                                    {
                                        SN.Exp = 0;
                                    }
                                    else
                                    {
                                        SN.Exp = (Int64)Math.Floor(Math.Log10(Convert.ToDouble("0." + Part2)));
                                    }
                                }
                                else
                                {
                                    SN.Exp = Part1.Length - 1;
                                }

                                Str = Convert.ToDouble(Part1) + "." + Part2;

                                Validity_Input = true;
                            }
                            else if (Part1.Length < 15)
                            {
                                Str = Convert.ToDouble(Part1) + ".";

                                Validity_Input = false;
                            }
                            else
                            {
                                Str = Convert.ToDouble(Part1).ToString();

                                goto REJUDGE;
                            }
                        }
                        else
                        {
                            Str = "0";

                            goto REJUDGE;
                        }
                    } // "." 出现。
                } // "E" 未出现。
                else if (EIndex == 0) // "E" 出现在 Index = 0。
                {
                    Str = new Regex(@"[^\d\.]").Replace(Str, string.Empty);

                    goto REJUDGE;
                } // "E" 出现在 Index = 0。
                else // "E" 出现在 Index > 0。
                {
                    string BeforeE = new Regex(@"[^\d]").Replace(Str.Substring(0, EIndex), string.Empty);
                    string AfterE = new Regex(@"[^\d]").Replace(Str.Substring(EIndex + 1), string.Empty);

                    string Part1 = BeforeE.Substring(0, Math.Min(15, BeforeE.Length));
                    string Part2 = AfterE.Substring(0, Math.Min(20, AfterE.Length));

                    if (Part2.Length > 0)
                    {
                        if (Convert.ToDouble(Part2) > Int64.MaxValue)
                        {
                            Part2 = Int64.MaxValue.ToString();
                        }
                        else
                        {
                            Part2 = Convert.ToInt64(Part2).ToString();
                        }

                        SN.Exp = Convert.ToInt64(Part2);
                    }
                    else
                    {
                        SN.Exp = 0;
                    }

                    if (Part1.Length == 0)
                    {
                        Str = Part2;

                        goto REJUDGE;
                    }
                    else if (Convert.ToDouble(Part1) == 0)
                    {
                        Str = "0";

                        goto REJUDGE;
                    }
                    else if (Part1.Length == 1)
                    {
                        SN.Val = Convert.ToDouble(Part1);

                        Str = Part1 + "E" + Part2;

                        if (Part2.Length > 0)
                        {
                            Validity_Input = true;
                        }
                        else
                        {
                            Validity_Input = false;
                        }
                    }
                    else
                    {
                        string _Str = Convert.ToDouble(Part1).ToString();

                        SN.Val = Convert.ToDouble(_Str.Substring(0, 1) + "." + _Str.Substring(1));

                        Str = SN.Val + "E" + Part2;

                        if (Part2.Length > 0)
                        {
                            Validity_Input = true;
                        }
                        else
                        {
                            Validity_Input = false;
                        }
                    }
                } // "E" 出现在 Index > 0。
            } // "-" 出现偶数次。
            else // "-" 出现奇数次。
            {
                if (EIndex == -1) // "E" 未出现。
                {
                    if (DIndex == -1) // "." 未出现。
                    {
                        string Text = new Regex(@"[^\d]").Replace(Str, string.Empty).Substring(0, Math.Min(15, new Regex(@"[^\d]").Replace(Str, string.Empty).Length));

                        if (Text.Length > 0)
                        {
                            string _Str = Convert.ToDouble(Text).ToString();

                            SN.Val = Convert.ToDouble("-" + _Str.Substring(0, 1) + "." + _Str.Substring(1));
                            SN.Exp = Text.Length - 1;

                            Str = "-" + _Str;

                            Validity_Input = true;
                        }
                        else
                        {
                            SN.Val = 0;
                            SN.Exp = 0;

                            Str = "-";

                            Validity_Input = false;
                        }
                    } // "." 未出现。
                    else // "." 出现。
                    {
                        string BeforeD = new Regex(@"[^\d]").Replace(Str.Substring(0, DIndex), string.Empty);
                        string AfterD = new Regex(@"[^\d]").Replace(Str.Substring(DIndex + 1), string.Empty);

                        string Part1 = BeforeD.Substring(0, Math.Min(15, BeforeD.Length));
                        string Part2 = AfterD.Substring(0, Math.Min(15 - Part1.Length, AfterD.Length));

                        if (Part1.Length > 0)
                        {
                            if (Part2.Length > 0)
                            {
                                string _Str = Convert.ToDouble(Part1 + Part2).ToString();

                                SN.Val = Convert.ToDouble("-" + _Str.Substring(0, 1) + "." + _Str.Substring(1));

                                if (Convert.ToDouble(Part1) == 0)
                                {
                                    if (Convert.ToDouble(Part2) == 0)
                                    {
                                        SN.Exp = 0;
                                    }
                                    else
                                    {
                                        SN.Exp = (Int64)Math.Floor(Math.Log10(Convert.ToDouble("0." + Part2)));
                                    }
                                }
                                else
                                {
                                    SN.Exp = Part1.Length - 1;
                                }

                                Str = "-" + Convert.ToDouble(Part1) + "." + Part2;

                                Validity_Input = true;
                            }
                            else if (Part1.Length < 15)
                            {
                                Str = "-" + Convert.ToDouble(Part1) + ".";

                                Validity_Input = false;
                            }
                            else
                            {
                                Str = "-" + Convert.ToDouble(Part1);

                                goto REJUDGE;
                            }
                        }
                        else
                        {
                            Str = "-";

                            goto REJUDGE;
                        }
                    } // "." 出现。
                } // "E" 未出现。
                else if (EIndex == 0) // "E" 出现在 Index = 0。
                {
                    Str = new Regex(@"[^\d\.]").Replace(Str, string.Empty);

                    goto REJUDGE;
                } // "E" 出现在 Index = 0。
                else // "E" 出现在 Index > 0。
                {
                    string BeforeE = new Regex(@"[^\d]").Replace(Str.Substring(0, EIndex), string.Empty);
                    string AfterE = new Regex(@"[^\d]").Replace(Str.Substring(EIndex + 1), string.Empty);

                    string Part1 = BeforeE.Substring(0, Math.Min(15, BeforeE.Length));
                    string Part2 = AfterE.Substring(0, Math.Min(20, AfterE.Length));

                    if (Part2.Length > 0)
                    {
                        if (Convert.ToDouble(Part2) > Int64.MaxValue)
                        {
                            Part2 = Int64.MaxValue.ToString();
                        }
                        else
                        {
                            Part2 = Convert.ToInt64(Part2).ToString();
                        }

                        SN.Exp = Convert.ToInt64(Part2);
                    }
                    else
                    {
                        SN.Exp = 0;
                    }

                    if (Part1.Length == 0)
                    {
                        Str = "-" + Part2;

                        goto REJUDGE;
                    }
                    else if (Convert.ToDouble(Part1) == 0)
                    {
                        Str = "-" + "0";

                        goto REJUDGE;
                    }
                    else if (Part1.Length == 1)
                    {
                        SN.Val = Convert.ToDouble("-" + Part1);

                        Str = "-" + Part1 + "E" + Part2;

                        if (Part2.Length > 0)
                        {
                            Validity_Input = true;
                        }
                        else
                        {
                            Validity_Input = false;
                        }
                    }
                    else
                    {
                        string _Str = Convert.ToDouble(Part1).ToString();

                        SN.Val = Convert.ToDouble("-" + _Str.Substring(0, 1) + "." + _Str.Substring(1));

                        Str = SN.Val + "E" + Part2;

                        if (Part2.Length > 0)
                        {
                            Validity_Input = true;
                        }
                        else
                        {
                            Validity_Input = false;
                        }
                    }
                } // "E" 出现在 Index > 0。
            } // "-" 出现奇数次。

            Label_Input.TextChanged -= Label_Input_TextChanged;
            Label_Input.Text = Str;
            Label_Input.TextChanged += Label_Input_TextChanged;

            //

            if (Validity_Input)
            {
                SN_Input = SN;
            }

            //

            Calc_Restart();
        }

        private void Label_Input_LocationChanged(object sender, EventArgs e)
        {
            //
            // Label_Input 位置改变。
            //

            Panel_Input.Width = Math.Max(200, Label_Input.Right + Label_Factorial.Width);

            Label_Factorial.Left = Panel_Input.Width - Label_Factorial.Width;
        }

        private void Label_Input_SizeChanged(object sender, EventArgs e)
        {
            //
            // Label_Input 大小改变。
            //

            Panel_Input.Width = Math.Max(200, Label_Input.Right + Label_Factorial.Width);

            Label_Factorial.Left = Panel_Input.Width - Label_Factorial.Width;
        }

        // 输出区域容器。

        private void ResizeForm()
        {
            //
            // 重置窗体大小。
            //

            Me.Width = Panel_Output.Right + Panel_Input.Left;
            Me.X = Math.Max(0, Math.Min(Screen.PrimaryScreen.WorkingArea.X + Screen.PrimaryScreen.WorkingArea.Width - Me.Width, Me.X));
        }

        private void Panel_Output_LocationChanged(object sender, EventArgs e)
        {
            //
            // Panel_Output 位置改变。
            //

            Label_ReturnToZero.Left = Panel_Output.Right - Label_ReturnToZero.Width;

            //

            ResizeForm();
        }

        private void Panel_Output_SizeChanged(object sender, EventArgs e)
        {
            //
            // Panel_Output 大小改变。
            //

            Label_ReturnToZero.Left = Panel_Output.Right - Label_ReturnToZero.Width;

            //

            ResizeForm();
        }

        // 输出值标签。

        private void Label_Val_LocationChanged(object sender, EventArgs e)
        {
            //
            // Label_Val 位置改变。
            //

            Label_Exp.Left = Label_Val.Right;
        }

        private void Label_Val_SizeChanged(object sender, EventArgs e)
        {
            //
            // Label_Val 大小改变。
            //

            Label_Exp.Left = Label_Val.Right;
        }

        private void Label_Exp_LocationChanged(object sender, EventArgs e)
        {
            //
            // Label_Exp 位置改变。
            //

            Label_ExpExp.Left = Label_Exp.Right;
        }

        private void Label_Exp_SizeChanged(object sender, EventArgs e)
        {
            //
            // Label_Exp 大小改变。
            //

            Label_ExpExp.Left = Label_Exp.Right;
        }

        private void Label_ExpExp_LocationChanged(object sender, EventArgs e)
        {
            //
            // Label_ExpExp 位置改变。
            //

            Panel_Output.Width = Math.Max(200, Label_ExpExp.Right);
        }

        private void Label_ExpExp_SizeChanged(object sender, EventArgs e)
        {
            //
            // Label_ExpExp 大小改变。
            //

            Panel_Output.Width = Math.Max(200, Label_ExpExp.Right);
        }

        #endregion

        #region 菜单项

        // 输入。

        private void ToolStripMenuItem_Input_Copy_Click(object sender, EventArgs e)
        {
            //
            // 单击 ToolStripMenuItem_Input_Copy。
            //

            if (Label_Input.Text != string.Empty)
            {
                Clipboard.SetDataObject(Label_Input.Text);
            }
        }

        private void ToolStripMenuItem_Input_Paste_Click(object sender, EventArgs e)
        {
            //
            // 单击 ToolStripMenuItem_Input_Paste。
            //

            IDataObject Data = Clipboard.GetDataObject();

            if (Data.GetDataPresent(DataFormats.Text))
            {
                Label_Input.Text = (string)Data.GetData(DataFormats.Text);
            }
        }

        // 输出。

        private void ToolStripMenuItem_Output_Copy_Click(object sender, EventArgs e)
        {
            //
            // 单击 ToolStripMenuItem_Output_Copy。
            //

            if (Label_Val.Text != string.Empty)
            {
                if (Label_Exp.Text == string.Empty)
                {
                    Clipboard.SetDataObject(Label_Val.Text);
                }
                else
                {
                    if (Label_ExpExp.Text == string.Empty)
                    {
                        Clipboard.SetDataObject(Label_Val.Text + " ^ " + Label_Exp.Text);
                    }
                    else
                    {
                        Clipboard.SetDataObject(Label_Val.Text + " ^ (" + Label_Exp.Text + " ^ " + Label_ExpExp.Text + ")");
                    }
                }
            }
        }

        #endregion

        #region 公用函数与方法

        private string GetTimeStringFromTimeSpan(TimeSpan TS)
        {
            //
            // 获取时间间隔的字符串。TS：时间间隔。
            //

            try
            {
                return (TS.TotalHours >= 1 ? Math.Floor(TS.TotalHours) + " 小时 " + TS.Minutes + " 分 " + TS.Seconds + " 秒" : (TS.TotalMinutes >= 1 ? TS.Minutes + " 分 " + TS.Seconds + " 秒" : (TS.TotalSeconds >= 1 ? TS.Seconds + "." + TS.Milliseconds.ToString("D3").Substring(0, TS.Seconds >= 10 ? 1 : 2) + " 秒" : (TS.TotalMilliseconds >= 1 ? Math.Truncate(TS.TotalMilliseconds) + (TS.TotalMilliseconds < 100 ? "." + ((Int32)((TS.TotalMilliseconds - Math.Truncate(TS.TotalMilliseconds)) * 1000)).ToString("D3").Substring(0, TS.TotalMilliseconds >= 10 ? 1 : 2) : string.Empty) + " 毫秒" : (TS.TotalMilliseconds * 1000 >= 0.1 ? Math.Truncate(TS.TotalMilliseconds * 1000) + (TS.TotalMilliseconds * 1000 < 100 ? "." + ((Int32)((TS.TotalMilliseconds * 1000 - Math.Truncate(TS.TotalMilliseconds * 1000)) * 1000)).ToString("D3").Substring(0, 1) : string.Empty) + " 微秒" : "小于 0.1 微秒")))));
            }
            catch
            {
                return string.Empty;
            }
        }

        #endregion

    }
}