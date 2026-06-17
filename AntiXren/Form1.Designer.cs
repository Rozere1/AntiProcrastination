namespace AntiXren
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button2 = new Button();
            SuspendLayout();
            // 
            // button2
            // 
            resources.ApplyResources(button2, "button2");
            button2.AutoEllipsis = true;
            button2.Name = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.UseWaitCursor = true;
            button2.Click += button1_Click;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            Name = "Form1";
            Activated += Form1_Load;
            Load += Form1_Load;
            ResumeLayout(false);

        }

        #endregion
        private Button button2;
    }
}
