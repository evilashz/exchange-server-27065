using System;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000594 RID: 1428
	public class ClientControlBinding : ControlBinding
	{
		// Token: 0x060041B5 RID: 16821 RVA: 0x000C7FC3 File Offset: 0x000C61C3
		public ClientControlBinding(Control control, string clientPropertyName) : base(control)
		{
			this.ClientPropertyName = clientPropertyName;
		}

		// Token: 0x17002576 RID: 9590
		// (get) Token: 0x060041B6 RID: 16822 RVA: 0x000C7FD3 File Offset: 0x000C61D3
		// (set) Token: 0x060041B7 RID: 16823 RVA: 0x000C7FDB File Offset: 0x000C61DB
		private protected string ClientPropertyName { protected get; private set; }

		// Token: 0x060041B8 RID: 16824 RVA: 0x000C7FE4 File Offset: 0x000C61E4
		protected override string ToJavaScriptWhenVisible(IControlResolver resolver)
		{
			return string.Format("new ClientControlBinding('{0}','{1}')", this.ClientID, this.ClientPropertyName);
		}
	}
}
