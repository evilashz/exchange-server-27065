using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x02000006 RID: 6
	[Designer(typeof(CustomControlDesigner))]
	internal sealed class CustomGroupBox : GroupBox, IDetailsTemplateControlBound
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000025B3 File Offset: 0x000007B3
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000025C0 File Offset: 0x000007C0
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
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000025D5 File Offset: 0x000007D5
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000025DD File Offset: 0x000007DD
		[Browsable(false)]
		public DetailsTemplateControl DetailsTemplateControl
		{
			get
			{
				return this.detailsTemplateControl;
			}
			set
			{
				this.detailsTemplateControl = (value as GroupboxControl);
				base.Text = this.detailsTemplateControl.Text;
			}
		}

		// Token: 0x04000004 RID: 4
		private GroupboxControl detailsTemplateControl = new GroupboxControl();
	}
}
