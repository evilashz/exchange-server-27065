using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x02000005 RID: 5
	[Designer(typeof(CustomControlDesigner))]
	internal sealed class CustomComboBox : ComboBox, IDetailsTemplateControlBound
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x0000256F File Offset: 0x0000076F
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000257C File Offset: 0x0000077C
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

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000258A File Offset: 0x0000078A
		// (set) Token: 0x0600001F RID: 31 RVA: 0x00002592 File Offset: 0x00000792
		[Browsable(false)]
		public DetailsTemplateControl DetailsTemplateControl
		{
			get
			{
				return this.detailsTemplateControl;
			}
			set
			{
				this.detailsTemplateControl = (value as MultiValuedDropdownControl);
			}
		}

		// Token: 0x04000003 RID: 3
		private MultiValuedDropdownControl detailsTemplateControl = new MultiValuedDropdownControl();
	}
}
