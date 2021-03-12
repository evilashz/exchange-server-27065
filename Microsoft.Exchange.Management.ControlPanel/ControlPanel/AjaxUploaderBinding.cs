using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200059A RID: 1434
	public class AjaxUploaderBinding : ComponentBinding
	{
		// Token: 0x060041C5 RID: 16837 RVA: 0x000C80FE File Offset: 0x000C62FE
		public AjaxUploaderBinding(Control control, string clientPropertyName) : base(control, clientPropertyName)
		{
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x000C8108 File Offset: 0x000C6308
		protected override string ToJavaScriptWhenVisible(IControlResolver resolver)
		{
			return string.Format("new AjaxUploaderBinding('{0}','{1}')", base.Control.ClientID, base.ClientPropertyName);
		}
	}
}
