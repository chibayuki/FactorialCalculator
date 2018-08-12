namespace WinFormApp
{
    partial class Form_Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.Panel_Main = new System.Windows.Forms.Panel();
            this.Panel_Client = new System.Windows.Forms.Panel();
            this.Panel_Factorial = new System.Windows.Forms.Panel();
            this.Label_AppName = new System.Windows.Forms.Label();
            this.Label_Note = new System.Windows.Forms.Label();
            this.Label_ReturnToZero = new System.Windows.Forms.Label();
            this.Panel_Input = new System.Windows.Forms.Panel();
            this.Label_Factorial = new System.Windows.Forms.Label();
            this.Label_Input = new System.Windows.Forms.Label();
            this.ContextMenuStrip_Input = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_Input_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_Input_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.Panel_Output = new System.Windows.Forms.Panel();
            this.ContextMenuStrip_Output = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItem_Output_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.Label_Equal = new System.Windows.Forms.Label();
            this.Label_Val = new System.Windows.Forms.Label();
            this.Label_Exp = new System.Windows.Forms.Label();
            this.Label_ExpExp = new System.Windows.Forms.Label();
            this.Label_Time = new System.Windows.Forms.Label();
            this.Panel_Main.SuspendLayout();
            this.Panel_Client.SuspendLayout();
            this.Panel_Factorial.SuspendLayout();
            this.Panel_Input.SuspendLayout();
            this.ContextMenuStrip_Input.SuspendLayout();
            this.Panel_Output.SuspendLayout();
            this.ContextMenuStrip_Output.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel_Main
            // 
            this.Panel_Main.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Main.Controls.Add(this.Panel_Client);
            this.Panel_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Main.Location = new System.Drawing.Point(0, 0);
            this.Panel_Main.Name = "Panel_Main";
            this.Panel_Main.Size = new System.Drawing.Size(450, 230);
            this.Panel_Main.TabIndex = 0;
            // 
            // Panel_Client
            // 
            this.Panel_Client.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Client.Controls.Add(this.Panel_Factorial);
            this.Panel_Client.Location = new System.Drawing.Point(0, 0);
            this.Panel_Client.Name = "Panel_Client";
            this.Panel_Client.Size = new System.Drawing.Size(450, 230);
            this.Panel_Client.TabIndex = 0;
            // 
            // Panel_Factorial
            // 
            this.Panel_Factorial.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Factorial.Controls.Add(this.Label_AppName);
            this.Panel_Factorial.Controls.Add(this.Label_Note);
            this.Panel_Factorial.Controls.Add(this.Label_ReturnToZero);
            this.Panel_Factorial.Controls.Add(this.Panel_Input);
            this.Panel_Factorial.Controls.Add(this.Panel_Output);
            this.Panel_Factorial.Controls.Add(this.Label_Time);
            this.Panel_Factorial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_Factorial.Location = new System.Drawing.Point(0, 0);
            this.Panel_Factorial.Name = "Panel_Factorial";
            this.Panel_Factorial.Size = new System.Drawing.Size(450, 230);
            this.Panel_Factorial.TabIndex = 0;
            this.Panel_Factorial.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel_Factorial_Paint);
            // 
            // Label_AppName
            // 
            this.Label_AppName.AutoSize = true;
            this.Label_AppName.BackColor = System.Drawing.Color.Transparent;
            this.Label_AppName.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_AppName.ForeColor = System.Drawing.Color.White;
            this.Label_AppName.Location = new System.Drawing.Point(20, 25);
            this.Label_AppName.Name = "Label_AppName";
            this.Label_AppName.Size = new System.Drawing.Size(117, 28);
            this.Label_AppName.TabIndex = 0;
            this.Label_AppName.Text = "阶乘计算器";
            this.Label_AppName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_Note
            // 
            this.Label_Note.AutoSize = true;
            this.Label_Note.BackColor = System.Drawing.Color.Transparent;
            this.Label_Note.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label_Note.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Note.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.Label_Note.Location = new System.Drawing.Point(22, 80);
            this.Label_Note.Name = "Label_Note";
            this.Label_Note.Size = new System.Drawing.Size(280, 17);
            this.Label_Note.TabIndex = 0;
            this.Label_Note.Text = "输入介于 ±1E9223372036854775808 之间的实数";
            this.Label_Note.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label_ReturnToZero
            // 
            this.Label_ReturnToZero.BackColor = System.Drawing.Color.Transparent;
            this.Label_ReturnToZero.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_ReturnToZero.ForeColor = System.Drawing.Color.White;
            this.Label_ReturnToZero.Location = new System.Drawing.Point(395, 80);
            this.Label_ReturnToZero.Name = "Label_ReturnToZero";
            this.Label_ReturnToZero.Size = new System.Drawing.Size(30, 30);
            this.Label_ReturnToZero.TabIndex = 0;
            this.Label_ReturnToZero.Text = "C";
            this.Label_ReturnToZero.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_ReturnToZero.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Label_ReturnToZero_MouseClick);
            this.Label_ReturnToZero.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_ReturnToZero_MouseDown);
            this.Label_ReturnToZero.MouseEnter += new System.EventHandler(this.Label_ReturnToZero_MouseEnter);
            this.Label_ReturnToZero.MouseLeave += new System.EventHandler(this.Label_ReturnToZero_MouseLeave);
            this.Label_ReturnToZero.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Label_ReturnToZero_MouseUp);
            // 
            // Panel_Input
            // 
            this.Panel_Input.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Input.Controls.Add(this.Label_Factorial);
            this.Panel_Input.Controls.Add(this.Label_Input);
            this.Panel_Input.Location = new System.Drawing.Point(25, 120);
            this.Panel_Input.Name = "Panel_Input";
            this.Panel_Input.Size = new System.Drawing.Size(200, 49);
            this.Panel_Input.TabIndex = 0;
            this.Panel_Input.LocationChanged += new System.EventHandler(this.Panel_Input_LocationChanged);
            this.Panel_Input.SizeChanged += new System.EventHandler(this.Panel_Input_SizeChanged);
            this.Panel_Input.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_Input_MouseDown);
            this.Panel_Input.MouseLeave += new System.EventHandler(this.Panel_Input_MouseLeave);
            this.Panel_Input.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_Input_MouseMove);
            this.Panel_Input.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_Input_MouseUp);
            // 
            // Label_Factorial
            // 
            this.Label_Factorial.AutoSize = true;
            this.Label_Factorial.BackColor = System.Drawing.Color.Transparent;
            this.Label_Factorial.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.Label_Factorial.ForeColor = System.Drawing.Color.White;
            this.Label_Factorial.Location = new System.Drawing.Point(181, 22);
            this.Label_Factorial.Name = "Label_Factorial";
            this.Label_Factorial.Size = new System.Drawing.Size(18, 27);
            this.Label_Factorial.TabIndex = 0;
            this.Label_Factorial.Text = "!";
            // 
            // Label_Input
            // 
            this.Label_Input.AutoSize = true;
            this.Label_Input.BackColor = System.Drawing.Color.Transparent;
            this.Label_Input.ContextMenuStrip = this.ContextMenuStrip_Input;
            this.Label_Input.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Input.ForeColor = System.Drawing.Color.White;
            this.Label_Input.Location = new System.Drawing.Point(0, 22);
            this.Label_Input.Name = "Label_Input";
            this.Label_Input.Size = new System.Drawing.Size(62, 27);
            this.Label_Input.TabIndex = 0;
            this.Label_Input.Text = "Input";
            this.Label_Input.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.Label_Input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Label_Input_KeyDown);
            this.Label_Input.LocationChanged += new System.EventHandler(this.Label_Input_LocationChanged);
            this.Label_Input.SizeChanged += new System.EventHandler(this.Label_Input_SizeChanged);
            this.Label_Input.TextChanged += new System.EventHandler(this.Label_Input_TextChanged);
            this.Label_Input.GotFocus += new System.EventHandler(this.Label_Input_GotFocus);
            this.Label_Input.LostFocus += new System.EventHandler(this.Label_Input_LostFocus);
            this.Label_Input.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label_Input_MouseDown);
            this.Label_Input.MouseLeave += new System.EventHandler(this.Label_Input_MouseLeave);
            this.Label_Input.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Label_Input_MouseMove);
            // 
            // ContextMenuStrip_Input
            // 
            this.ContextMenuStrip_Input.BackColor = System.Drawing.Color.White;
            this.ContextMenuStrip_Input.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Input_Copy,
            this.ToolStripMenuItem_Input_Paste});
            this.ContextMenuStrip_Input.Name = "ContextMenuStrip_ID";
            this.ContextMenuStrip_Input.Size = new System.Drawing.Size(117, 48);
            // 
            // ToolStripMenuItem_Input_Copy
            // 
            this.ToolStripMenuItem_Input_Copy.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItem_Input_Copy.ForeColor = System.Drawing.Color.Black;
            this.ToolStripMenuItem_Input_Copy.Name = "ToolStripMenuItem_Input_Copy";
            this.ToolStripMenuItem_Input_Copy.Size = new System.Drawing.Size(116, 22);
            this.ToolStripMenuItem_Input_Copy.Text = "复制(C)";
            this.ToolStripMenuItem_Input_Copy.Click += new System.EventHandler(this.ToolStripMenuItem_Input_Copy_Click);
            // 
            // ToolStripMenuItem_Input_Paste
            // 
            this.ToolStripMenuItem_Input_Paste.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItem_Input_Paste.ForeColor = System.Drawing.Color.Black;
            this.ToolStripMenuItem_Input_Paste.Name = "ToolStripMenuItem_Input_Paste";
            this.ToolStripMenuItem_Input_Paste.Size = new System.Drawing.Size(116, 22);
            this.ToolStripMenuItem_Input_Paste.Text = "粘贴(P)";
            this.ToolStripMenuItem_Input_Paste.Click += new System.EventHandler(this.ToolStripMenuItem_Input_Paste_Click);
            // 
            // Panel_Output
            // 
            this.Panel_Output.BackColor = System.Drawing.Color.Transparent;
            this.Panel_Output.ContextMenuStrip = this.ContextMenuStrip_Output;
            this.Panel_Output.Controls.Add(this.Label_Equal);
            this.Panel_Output.Controls.Add(this.Label_Val);
            this.Panel_Output.Controls.Add(this.Label_Exp);
            this.Panel_Output.Controls.Add(this.Label_ExpExp);
            this.Panel_Output.Location = new System.Drawing.Point(225, 120);
            this.Panel_Output.Name = "Panel_Output";
            this.Panel_Output.Size = new System.Drawing.Size(200, 49);
            this.Panel_Output.TabIndex = 0;
            this.Panel_Output.LocationChanged += new System.EventHandler(this.Panel_Output_LocationChanged);
            this.Panel_Output.SizeChanged += new System.EventHandler(this.Panel_Output_SizeChanged);
            // 
            // ContextMenuStrip_Output
            // 
            this.ContextMenuStrip_Output.BackColor = System.Drawing.Color.White;
            this.ContextMenuStrip_Output.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_Output_Copy});
            this.ContextMenuStrip_Output.Name = "ContextMenuStrip_ID";
            this.ContextMenuStrip_Output.Size = new System.Drawing.Size(117, 26);
            // 
            // ToolStripMenuItem_Output_Copy
            // 
            this.ToolStripMenuItem_Output_Copy.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripMenuItem_Output_Copy.ForeColor = System.Drawing.Color.Black;
            this.ToolStripMenuItem_Output_Copy.Name = "ToolStripMenuItem_Output_Copy";
            this.ToolStripMenuItem_Output_Copy.Size = new System.Drawing.Size(116, 22);
            this.ToolStripMenuItem_Output_Copy.Text = "复制(C)";
            this.ToolStripMenuItem_Output_Copy.Click += new System.EventHandler(this.ToolStripMenuItem_Output_Copy_Click);
            // 
            // Label_Equal
            // 
            this.Label_Equal.AutoSize = true;
            this.Label_Equal.BackColor = System.Drawing.Color.Transparent;
            this.Label_Equal.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.Label_Equal.ForeColor = System.Drawing.Color.White;
            this.Label_Equal.Location = new System.Drawing.Point(0, 22);
            this.Label_Equal.Name = "Label_Equal";
            this.Label_Equal.Size = new System.Drawing.Size(27, 27);
            this.Label_Equal.TabIndex = 0;
            this.Label_Equal.Text = "=";
            // 
            // Label_Val
            // 
            this.Label_Val.AutoSize = true;
            this.Label_Val.BackColor = System.Drawing.Color.Transparent;
            this.Label_Val.Font = new System.Drawing.Font("微软雅黑", 15F);
            this.Label_Val.ForeColor = System.Drawing.Color.White;
            this.Label_Val.Location = new System.Drawing.Point(27, 22);
            this.Label_Val.Name = "Label_Val";
            this.Label_Val.Size = new System.Drawing.Size(42, 27);
            this.Label_Val.TabIndex = 0;
            this.Label_Val.Text = "Val";
            this.Label_Val.LocationChanged += new System.EventHandler(this.Label_Val_LocationChanged);
            this.Label_Val.SizeChanged += new System.EventHandler(this.Label_Val_SizeChanged);
            // 
            // Label_Exp
            // 
            this.Label_Exp.AutoSize = true;
            this.Label_Exp.BackColor = System.Drawing.Color.Transparent;
            this.Label_Exp.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Label_Exp.ForeColor = System.Drawing.Color.White;
            this.Label_Exp.Location = new System.Drawing.Point(69, 11);
            this.Label_Exp.Name = "Label_Exp";
            this.Label_Exp.Size = new System.Drawing.Size(37, 21);
            this.Label_Exp.TabIndex = 0;
            this.Label_Exp.Text = "Exp";
            this.Label_Exp.LocationChanged += new System.EventHandler(this.Label_Exp_LocationChanged);
            this.Label_Exp.SizeChanged += new System.EventHandler(this.Label_Exp_SizeChanged);
            // 
            // Label_ExpExp
            // 
            this.Label_ExpExp.AutoSize = true;
            this.Label_ExpExp.BackColor = System.Drawing.Color.Transparent;
            this.Label_ExpExp.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.Label_ExpExp.ForeColor = System.Drawing.Color.White;
            this.Label_ExpExp.Location = new System.Drawing.Point(106, 0);
            this.Label_ExpExp.Name = "Label_ExpExp";
            this.Label_ExpExp.Size = new System.Drawing.Size(64, 21);
            this.Label_ExpExp.TabIndex = 0;
            this.Label_ExpExp.Text = "ExpExp";
            this.Label_ExpExp.LocationChanged += new System.EventHandler(this.Label_ExpExp_LocationChanged);
            this.Label_ExpExp.SizeChanged += new System.EventHandler(this.Label_ExpExp_SizeChanged);
            // 
            // Label_Time
            // 
            this.Label_Time.BackColor = System.Drawing.Color.Transparent;
            this.Label_Time.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Label_Time.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Label_Time.ForeColor = System.Drawing.Color.White;
            this.Label_Time.Location = new System.Drawing.Point(0, 210);
            this.Label_Time.Name = "Label_Time";
            this.Label_Time.Size = new System.Drawing.Size(450, 20);
            this.Label_Time.TabIndex = 0;
            this.Label_Time.Text = "用时";
            this.Label_Time.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(450, 230);
            this.Controls.Add(this.Panel_Main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Panel_Main.ResumeLayout(false);
            this.Panel_Client.ResumeLayout(false);
            this.Panel_Factorial.ResumeLayout(false);
            this.Panel_Factorial.PerformLayout();
            this.Panel_Input.ResumeLayout(false);
            this.Panel_Input.PerformLayout();
            this.ContextMenuStrip_Input.ResumeLayout(false);
            this.Panel_Output.ResumeLayout(false);
            this.Panel_Output.PerformLayout();
            this.ContextMenuStrip_Output.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel Panel_Main;
        private System.Windows.Forms.Panel Panel_Client;
        private System.Windows.Forms.Panel Panel_Input;
        private System.Windows.Forms.Label Label_Input;
        private System.Windows.Forms.Panel Panel_Output;
        private System.Windows.Forms.Label Label_ExpExp;
        private System.Windows.Forms.Label Label_Exp;
        private System.Windows.Forms.Label Label_Val;
        private System.Windows.Forms.Label Label_Note;
        private System.Windows.Forms.Label Label_AppName;
        private System.Windows.Forms.Label Label_Time;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip_Output;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Output_Copy;
        private System.Windows.Forms.ContextMenuStrip ContextMenuStrip_Input;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Input_Copy;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_Input_Paste;
        private System.Windows.Forms.Label Label_Equal;
        private System.Windows.Forms.Label Label_Factorial;
        private System.Windows.Forms.Panel Panel_Factorial;
        private System.Windows.Forms.Label Label_ReturnToZero;
    }
}