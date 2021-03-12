using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000599 RID: 1433
	public class ComponentBinding : ClientControlBinding
	{
		// Token: 0x060041C3 RID: 16835 RVA: 0x000C80D7 File Offset: 0x000C62D7
		public ComponentBinding(Control control, string clientPropertyName) : base(control, clientPropertyName)
		{
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x000C80E1 File Offset: 0x000C62E1
		protected override string ToJavaScriptWhenVisible(IControlResolver resolver)
		{
			return string.Format("new ComponentBinding('{0}','{1}')", base.Control.ClientID, base.ClientPropertyName);
		}
	}
}
