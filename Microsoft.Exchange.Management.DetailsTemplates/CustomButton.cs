using System;
using System.ComponentModel;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x02000003 RID: 3
	[Designer(typeof(CustomControlDesigner))]
	internal sealed class CustomButton : ExchangeButton, IDetailsTemplateControlBound
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002490 File Offset: 0x00000690
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000249D File Offset: 0x0000069D
		public override string Text
		{
			get
			{
				return this.detailsTemplateControl.Text;
			}
			set
			{
				this.detailsTemplateControl.Text = value;
				base.Text = value;
				this.Refresh();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000024B8 File Offset: 0x000006B8
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000024C0 File Offset: 0x000006C0
		[Browsable(false)]
		public DetailsTemplateControl DetailsTemplateControl
		{
			get
			{
				return this.detailsTemplateControl;
			}
			set
			{
				this.detailsTemplateControl = (value as ButtonControl);
				this.Text = this.detailsTemplateControl.Text;
			}
		}

		// Token: 0x04000001 RID: 1
		private ButtonControl detailsTemplateControl = new ButtonControl();
	}
}
