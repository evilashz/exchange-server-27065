using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000598 RID: 1432
	public class RadioButtonBinding : ClientControlBinding
	{
		// Token: 0x060041C1 RID: 16833 RVA: 0x000C80B5 File Offset: 0x000C62B5
		public RadioButtonBinding(Control control, string clientPropertyName) : base(control, clientPropertyName)
		{
		}

		// Token: 0x060041C2 RID: 16834 RVA: 0x000C80BF File Offset: 0x000C62BF
		protected override string ToJavaScriptWhenVisible(IControlResolver resolver)
		{
			return string.Format("new RadioButtonBinding('{0}','{1}')", this.ClientID, base.ClientPropertyName);
		}
	}
}
