namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x0200000E RID: 14
	public partial class CustomMessageBox : global::System.Windows.Forms.Form
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00004EBB File Offset: 0x000030BB
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000050A0 File Offset: 0x000032A0
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			new global::System.ComponentModel.ComponentResourceManager(typeof(global::Microsoft.Exchange.Setup.ExSetupUI.CustomMessageBox));
			this.btnTwo = new global::System.Windows.Forms.Button();
			this.btnOne = new global::System.Windows.Forms.Button();
			this.displayPanel = new global::System.Windows.Forms.Panel();
			this.messageLabel = new global::Microsoft.Exchange.Setup.ExSetupUI.FontScalingLabel();
			this.captureLabel = new global::System.Windows.Forms.Label();
			this.messageImageBox = new global::System.Windows.Forms.PictureBox();
			this.customMessageBoxImageList = new global::System.Windows.Forms.ImageList(this.components);
			this.displayPanel.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.messageImageBox).BeginInit();
			base.SuspendLayout();
			this.btnTwo.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			this.btnTwo.FlatAppearance.BorderColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.btnTwo.FlatAppearance.BorderSize = 2;
			this.btnTwo.FlatAppearance.MouseDownBackColor = global::System.Drawing.Color.FromArgb(0, 114, 198);
			this.btnTwo.FlatAppearance.MouseOverBackColor = global::System.Drawing.Color.Transparent;
			this.btnTwo.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.btnTwo.ForeColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.btnTwo.Location = new global::System.Drawing.Point(160, 130);
			this.btnTwo.Name = "btnTwo";
			this.btnTwo.Size = new global::System.Drawing.Size(90, 30);
			this.btnTwo.TabIndex = 5;
			this.btnTwo.Text = "[btnTwo]";
			this.btnTwo.UseVisualStyleBackColor = true;
			this.btnOne.BackgroundImageLayout = global::System.Windows.Forms.ImageLayout.Stretch;
			this.btnOne.FlatAppearance.BorderColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.btnOne.FlatAppearance.BorderSize = 2;
			this.btnOne.FlatAppearance.MouseDownBackColor = global::System.Drawing.Color.FromArgb(0, 114, 198);
			this.btnOne.FlatAppearance.MouseOverBackColor = global::System.Drawing.Color.Transparent;
			this.btnOne.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.btnOne.ForeColor = global::System.Drawing.Color.FromArgb(102, 102, 102);
			this.btnOne.Location = new global::System.Drawing.Point(270, 130);
			this.btnOne.Name = "btnOne";
			this.btnOne.Size = new global::System.Drawing.Size(90, 30);
			this.btnOne.TabIndex = 4;
			this.btnOne.Text = "[btnOne]";
			this.btnOne.UseVisualStyleBackColor = true;
			this.displayPanel.BackColor = global::System.Drawing.Color.Transparent;
			this.displayPanel.Controls.Add(this.messageLabel);
			this.displayPanel.Controls.Add(this.captureLabel);
			this.displayPanel.Controls.Add(this.messageImageBox);
			this.displayPanel.Controls.Add(this.btnTwo);
			this.displayPanel.Controls.Add(this.btnOne);
			this.displayPanel.Location = new global::System.Drawing.Point(20, 20);
			this.displayPanel.Margin = new global::System.Windows.Forms.Padding(0);
			this.displayPanel.Name = "displayPanel";
			this.displayPanel.Size = new global::System.Drawing.Size(360, 160);
			this.displayPanel.TabIndex = 6;
			this.messageLabel.AutoSize = true;
			this.messageLabel.BackColor = global::System.Drawing.Color.Transparent;
			this.messageLabel.Font = new global::System.Drawing.Font("Segoe UI", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Pixel);
			this.messageLabel.Location = new global::System.Drawing.Point(0, 50);
			this.messageLabel.Margin = new global::System.Windows.Forms.Padding(2, 2, 2, 2);
			this.messageLabel.Name = "messageLabel";
			this.messageLabel.PreferredFontSize = 12f;
			this.messageLabel.Size = new global::System.Drawing.Size(360, 70);
			this.messageLabel.TabIndex = 9;
			this.messageLabel.Text = "[messageLabel]";
			this.messageLabel.TextAlign = global::System.Drawing.ContentAlignment.TopCenter;
			this.captureLabel.AutoSize = true;
			this.captureLabel.Font = new global::System.Drawing.Font("Segoe UI", 22.2f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Pixel, 0);
			this.captureLabel.Location = new global::System.Drawing.Point(62, 0);
			this.captureLabel.Name = "captureLabel";
			this.captureLabel.Size = new global::System.Drawing.Size(161, 31);
			this.captureLabel.TabIndex = 7;
			this.captureLabel.Text = "[captureLabel]";
			this.messageImageBox.Location = new global::System.Drawing.Point(0, 0);
			this.messageImageBox.Name = "messageImageBox";
			this.messageImageBox.Size = new global::System.Drawing.Size(32, 32);
			this.messageImageBox.TabIndex = 6;
			this.messageImageBox.TabStop = false;
			this.customMessageBoxImageList.ImageSize = new global::System.Drawing.Size(32, 32);
			this.customMessageBoxImageList.TransparentColor = global::System.Drawing.Color.Transparent;
			this.customMessageBoxImageList.Images.Add(global::Microsoft.Exchange.Setup.ExSetupUI.SetupFormBase.Images[4]);
			this.customMessageBoxImageList.Images.Add(global::Microsoft.Exchange.Setup.ExSetupUI.SetupFormBase.Images[5]);
			this.customMessageBoxImageList.Images.SetKeyName(0, "SetupError.png");
			this.customMessageBoxImageList.Images.SetKeyName(1, "SetupWarning.png");
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(7f, 15f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.SystemColors.Window;
			base.ClientSize = new global::System.Drawing.Size(398, 198);
			base.ControlBox = false;
			base.Controls.Add(this.displayPanel);
			this.Font = new global::System.Drawing.Font("Segoe UI", 12f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Pixel, 0);
			this.ForeColor = global::System.Drawing.Color.FromArgb(51, 51, 51);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.KeyPreview = true;
			base.Name = "CustomMessageBox";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.displayPanel.ResumeLayout(false);
			this.displayPanel.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.messageImageBox).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x04000051 RID: 81
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000052 RID: 82
		private global::System.Windows.Forms.Button btnTwo;

		// Token: 0x04000053 RID: 83
		private global::System.Windows.Forms.Button btnOne;

		// Token: 0x04000054 RID: 84
		private global::System.Windows.Forms.Panel displayPanel;

		// Token: 0x04000055 RID: 85
		private global::System.Windows.Forms.PictureBox messageImageBox;

		// Token: 0x04000056 RID: 86
		private global::System.Windows.Forms.Label captureLabel;

		// Token: 0x04000057 RID: 87
		private global::System.Windows.Forms.ImageList customMessageBoxImageList;

		// Token: 0x04000058 RID: 88
		private global::Microsoft.Exchange.Setup.ExSetupUI.FontScalingLabel messageLabel;
	}
}
