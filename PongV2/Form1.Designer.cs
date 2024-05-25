namespace PongV2
{
    partial class Pong
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
            components = new System.ComponentModel.Container();
            playerScoreLabel = new Label();
            cpuScoreLabel = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // playerScoreLabel
            // 
            playerScoreLabel.AutoSize = true;
            playerScoreLabel.BackColor = Color.Transparent;
            playerScoreLabel.Font = new Font("Cascadia Mono", 28F, FontStyle.Regular, GraphicsUnit.Point, 0);
            playerScoreLabel.ForeColor = Color.DeepSkyBlue;
            playerScoreLabel.Location = new Point(269, 77);
            playerScoreLabel.Name = "playerScoreLabel";
            playerScoreLabel.Size = new Size(65, 74);
            playerScoreLabel.TabIndex = 0;
            playerScoreLabel.Text = "0";
            // 
            // cpuScoreLabel
            // 
            cpuScoreLabel.AutoSize = true;
            cpuScoreLabel.BackColor = Color.Transparent;
            cpuScoreLabel.Font = new Font("Cascadia Mono", 28F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cpuScoreLabel.ForeColor = Color.Red;
            cpuScoreLabel.Location = new Point(1251, 77);
            cpuScoreLabel.Name = "cpuScoreLabel";
            cpuScoreLabel.Size = new Size(65, 74);
            cpuScoreLabel.TabIndex = 1;
            cpuScoreLabel.Text = "0";
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 10;
            timer1.Tick += timer1_Tick;
            // 
            // Pong
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(224, 224, 224);
            ClientSize = new Size(1670, 943);
            Controls.Add(cpuScoreLabel);
            Controls.Add(playerScoreLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Pong";
            Text = "Pong";
            Load += Pong_Load;
            KeyDown += Pong_KeyDown;
            KeyUp += Pong_KeyUp;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label playerScoreLabel;
        private Label cpuScoreLabel;
        private System.Windows.Forms.Timer timer1;
    }
}
