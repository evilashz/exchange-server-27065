using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000596 RID: 1430
	public class ComboBoxBinding : ClientControlBinding
	{
		// Token: 0x060041BB RID: 16827 RVA: 0x000C8037 File Offset: 0x000C6237
		public ComboBoxBinding(Control control, string clientPropertyName) : base(control, clientPropertyName)
		{
		}

		// Token: 0x060041BC RID: 16828 RVA: 0x000C8041 File Offset: 0x000C6241
		protected override string ToJavaScriptWhenVisible(IControlResolver resolver)
		{
			return string.Format("new ComboBoxBinding('{0}','{1}')", this.ClientID, base.ClientPropertyName);
		}
	}
}
