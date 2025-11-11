namespace BradleyLeach_BooseApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            RunButtom = new Button();
            InputBox = new TextBox();
            DisplayBox = new PictureBox();
            splitContainer1 = new SplitContainer();
            ((System.ComponentModel.ISupportInitialize)DisplayBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // RunButtom
            // 
            RunButtom.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            RunButtom.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            RunButtom.Cursor = Cursors.Hand;
            RunButtom.Location = new Point(5, 637);
            RunButtom.Name = "RunButtom";
            RunButtom.Size = new Size(578, 29);
            RunButtom.TabIndex = 0;
            RunButtom.Text = "Run";
            RunButtom.UseVisualStyleBackColor = true;
            RunButtom.Click += RunButtom_Click;
            // 
            // InputBox
            // 
            InputBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            InputBox.Location = new Point(5, 3);
            InputBox.Multiline = true;
            InputBox.Name = "InputBox";
            InputBox.ScrollBars = ScrollBars.Vertical;
            InputBox.Size = new Size(578, 628);
            InputBox.TabIndex = 1;
            InputBox.Text = "<Enter Code Here>";
            InputBox.WordWrap = false;
            // 
            // DisplayBox
            // 
            DisplayBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DisplayBox.BackColor = SystemColors.Window;
            DisplayBox.BorderStyle = BorderStyle.Fixed3D;
            DisplayBox.Location = new Point(3, 3);
            DisplayBox.Name = "DisplayBox";
            DisplayBox.Size = new Size(458, 663);
            DisplayBox.TabIndex = 2;
            DisplayBox.TabStop = false;
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = BorderStyle.Fixed3D;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(RunButtom);
            splitContainer1.Panel1.Controls.Add(InputBox);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(DisplayBox);
            splitContainer1.Size = new Size(1062, 673);
            splitContainer1.SplitterDistance = 590;
            splitContainer1.TabIndex = 3;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1062, 673);
            Controls.Add(splitContainer1);
            Name = "Form1";
            Text = "Form1";
            Paint += Form1_Paint;
            ((System.ComponentModel.ISupportInitialize)DisplayBox).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button RunButtom;
        private TextBox InputBox;
        private PictureBox DisplayBox;
        private SplitContainer splitContainer1;
    }
}
