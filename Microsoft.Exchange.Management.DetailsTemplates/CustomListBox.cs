using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x02000008 RID: 8
	[Designer(typeof(CustomControlDesigner))]
	internal sealed class CustomListBox : ListBox, IDetailsTemplateControlBound
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002671 File Offset: 0x00000871
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000267E File Offset: 0x0000087E
		public ScrollBars ScrollBars
		{
			get
			{
				return this.detailsTemplateControl.ScrollBars;
			}
			set
			{
				this.detailsTemplateControl.ScrollBars = value;
				this.UpdateScrollBars();
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002692 File Offset: 0x00000892
		// (set) Token: 0x0600002E RID: 46 RVA: 0x0000269F File Offset: 0x0000089F
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

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000026AD File Offset: 0x000008AD
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000026B5 File Offset: 0x000008B5
		[Browsable(false)]
		public DetailsTemplateControl DetailsTemplateControl
		{
			get
			{
				return this.detailsTemplateControl;
			}
			set
			{
				this.detailsTemplateControl = (value as ListboxControl);
				this.UpdateScrollBars();
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026CC File Offset: 0x000008CC
		private void UpdateScrollBars()
		{
			base.MultiColumn = false;
			base.HorizontalScrollbar = false;
			base.ScrollAlwaysVisible = false;
			switch (this.detailsTemplateControl.ScrollBars)
			{
			case ScrollBars.Horizontal:
				base.MultiColumn = true;
				base.HorizontalScrollbar = true;
				base.ScrollAlwaysVisible = true;
				return;
			case ScrollBars.Vertical:
				base.ScrollAlwaysVisible = true;
				return;
			case ScrollBars.Both:
				base.HorizontalScrollbar = true;
				base.ScrollAlwaysVisible = true;
				return;
			default:
				return;
			}
		}

		// Token: 0x04000006 RID: 6
		private ListboxControl detailsTemplateControl = new ListboxControl();
	}
}
