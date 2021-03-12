using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x02000009 RID: 9
	[Designer(typeof(CustomControlDesigner))]
	internal sealed class CustomMultiValuedListBox : ListBox, IDetailsTemplateControlBound
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000274E File Offset: 0x0000094E
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000275B File Offset: 0x0000095B
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

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002769 File Offset: 0x00000969
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002771 File Offset: 0x00000971
		[Browsable(false)]
		public DetailsTemplateControl DetailsTemplateControl
		{
			get
			{
				return this.detailsTemplateControl;
			}
			set
			{
				this.detailsTemplateControl = (value as MultiValuedListboxControl);
			}
		}

		// Token: 0x04000007 RID: 7
		private MultiValuedListboxControl detailsTemplateControl = new MultiValuedListboxControl();
	}
}
