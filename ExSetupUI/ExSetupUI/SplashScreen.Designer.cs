namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000028 RID: 40
	internal partial class SplashScreen : global::System.Windows.Forms.Form
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x0000AEEC File Offset: 0x000090EC
		private void InitializeComponent()
		{
			this.statusLabel = new global::System.Windows.Forms.Label();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.statusLabel.AutoSize = true;
			this.statusLabel.Font = new global::System.Drawing.Font("Segoe UI", 14f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Pixel, 0);
			this.statusLabel.Location = new global::System.Drawing.Point(3, 21);
			this.statusLabel.Name = "statusLabel";
			this.statusLabel.Size = new global::System.Drawing.Size(129, 19);
			this.statusLabel.TabIndex = 0;
			this.statusLabel.Text = "[Splash Screen Text]";
			this.statusLabel.TextAlign = global::System.Drawing.ContentAlignment.MiddleLeft;
			this.panel1.AutoSize = true;
			this.panel1.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.statusLabel);
			this.panel1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new global::System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(215, 67);
			this.panel1.TabIndex = 1;
			this.AutoSize = true;
			this.BackColor = global::System.Drawing.Color.White;
			base.ClientSize = new global::System.Drawing.Size(215, 67);
			base.Controls.Add(this.panel1);
			this.Font = new global::System.Drawing.Font("Segoe UI", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.None;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "SplashScreen";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000108 RID: 264
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x04000109 RID: 265
		private global::System.Windows.Forms.Label statusLabel;
	}
}
