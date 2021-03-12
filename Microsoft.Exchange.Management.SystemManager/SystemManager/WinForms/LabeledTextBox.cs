using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001B3 RID: 435
	public class LabeledTextBox : CaptionedTextBox
	{
		// Token: 0x06001198 RID: 4504 RVA: 0x000458F2 File Offset: 0x00043AF2
		public LabeledTextBox()
		{
			this.InitializeComponent();
			base.Name = "LabeledTextBox";
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0004590C File Offset: 0x00043B0C
		private void InitializeComponent()
		{
			this.labelCaption = new Label();
			this.tableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel.Controls.Add(this.labelCaption, 0, 0);
			this.labelCaption.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.labelCaption.AutoSize = true;
			this.labelCaption.Location = new Point(0, 0);
			this.labelCaption.Margin = new Padding(0, 3, 0, 4);
			this.labelCaption.Name = "labelCaption";
			this.labelCaption.TabIndex = 0;
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x000459CC File Offset: 0x00043BCC
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x000459D9 File Offset: 0x00043BD9
		[DefaultValue("")]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string Caption
		{
			get
			{
				return this.labelCaption.Text;
			}
			set
			{
				this.labelCaption.Text = value;
			}
		}

		// Token: 0x040006BC RID: 1724
		private Label labelCaption;
	}
}
