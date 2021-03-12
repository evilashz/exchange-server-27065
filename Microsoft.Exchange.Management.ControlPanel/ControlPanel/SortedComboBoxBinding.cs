using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000597 RID: 1431
	public class SortedComboBoxBinding : ClientControlBinding
	{
		// Token: 0x060041BD RID: 16829 RVA: 0x000C8059 File Offset: 0x000C6259
		public SortedComboBoxBinding(Control control, string clientPropertyName, SortDirection sortedDirection) : base(control, clientPropertyName)
		{
			this.SortedDirection = sortedDirection;
		}

		// Token: 0x17002577 RID: 9591
		// (get) Token: 0x060041BE RID: 16830 RVA: 0x000C806A File Offset: 0x000C626A
		// (set) Token: 0x060041BF RID: 16831 RVA: 0x000C8072 File Offset: 0x000C6272
		public SortDirection SortedDirection { get; set; }

		// Token: 0x060041C0 RID: 16832 RVA: 0x000C807C File Offset: 0x000C627C
		protected override string ToJavaScriptWhenVisible(IControlResolver resolver)
		{
			return string.Format("new SortedComboBoxBinding('{0}','{1}',{2})", this.ClientID, base.ClientPropertyName, (this.SortedDirection == SortDirection.Ascending).ToString().ToLowerInvariant());
		}
	}
}
