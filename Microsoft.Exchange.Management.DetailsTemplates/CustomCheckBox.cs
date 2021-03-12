using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x02000004 RID: 4
	[Designer(typeof(CustomControlDesigner))]
	internal sealed class CustomCheckBox : CheckBox, IDetailsTemplateControlBound
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000024F2 File Offset: 0x000006F2
		// (set) Token: 0x06000016 RID: 22 RVA: 0x000024FF File Offset: 0x000006FF
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000251A File Offset: 0x0000071A
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002527 File Offset: 0x00000727
		[TypeConverter(typeof(MAPITypeConverter))]
		public string AttributeName
		{
			get
			{
				return this.detailsTemplateControl.AttributeName;
			}
			set
			{
				this.detailsTemplateControl.AttributeName = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002535 File Offset: 0x00000735
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000253D File Offset: 0x0000073D
		[Browsable(false)]
		public DetailsTemplateControl DetailsTemplateControl
		{
			get
			{
				return this.detailsTemplateControl;
			}
			set
			{
				this.detailsTemplateControl = (value as CheckboxControl);
				base.Text = this.detailsTemplateControl.Text;
			}
		}

		// Token: 0x04000002 RID: 2
		private CheckboxControl detailsTemplateControl = new CheckboxControl();
	}
}
