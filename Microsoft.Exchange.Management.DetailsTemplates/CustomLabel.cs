using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x02000007 RID: 7
	[Designer(typeof(CustomControlDesigner))]
	internal sealed class CustomLabel : Label, IDetailsTemplateControlBound
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000260F File Offset: 0x0000080F
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000261C File Offset: 0x0000081C
		public override string Text
		{
			get
			{
				return this.detailsTemplateControl.Text;
			}
			set
			{
				base.Text = value;
				this.detailsTemplateControl.Text = value;
				this.Refresh();
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002637 File Offset: 0x00000837
		// (set) Token: 0x06000029 RID: 41 RVA: 0x0000263F File Offset: 0x0000083F
		[Browsable(false)]
		public DetailsTemplateControl DetailsTemplateControl
		{
			get
			{
				return this.detailsTemplateControl;
			}
			set
			{
				this.detailsTemplateControl = (value as LabelControl);
				base.Text = this.detailsTemplateControl.Text;
			}
		}

		// Token: 0x04000005 RID: 5
		private LabelControl detailsTemplateControl = new LabelControl();
	}
}
