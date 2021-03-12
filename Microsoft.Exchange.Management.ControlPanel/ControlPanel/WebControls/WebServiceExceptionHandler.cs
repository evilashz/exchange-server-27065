using System;
using System.Security.Principal;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x0200065A RID: 1626
	[ClientScriptResource("WebServiceExceptionHandler", "Microsoft.Exchange.Management.ControlPanel.Client.Common.js")]
	public abstract class WebServiceExceptionHandler : ScriptComponent
	{
		// Token: 0x060046BE RID: 18110 RVA: 0x000D612E File Offset: 0x000D432E
		public WebServiceExceptionHandler()
		{
		}

		// Token: 0x1700273D RID: 10045
		// (get) Token: 0x060046BF RID: 18111 RVA: 0x000D6136 File Offset: 0x000D4336
		// (set) Token: 0x060046C0 RID: 18112 RVA: 0x000D613E File Offset: 0x000D433E
		public string ExceptionName { get; set; }

		// Token: 0x060046C1 RID: 18113 RVA: 0x000D6147 File Offset: 0x000D4347
		public virtual bool ApplyRbacRolesAndAddControls(WebControl parentControl, IPrincipal currentUser)
		{
			return true;
		}

		// Token: 0x1700273E RID: 10046
		// (get) Token: 0x060046C2 RID: 18114 RVA: 0x000D614A File Offset: 0x000D434A
		// (set) Token: 0x060046C3 RID: 18115 RVA: 0x000D6152 File Offset: 0x000D4352
		public ExceptionHandlerType ExceptionHandlerType { get; set; }

		// Token: 0x1700273F RID: 10047
		// (get) Token: 0x060046C4 RID: 18116 RVA: 0x000D615B File Offset: 0x000D435B
		// (set) Token: 0x060046C5 RID: 18117 RVA: 0x000D6163 File Offset: 0x000D4363
		public ErrorCoExistingActionType ErrorCoExistingAction { get; set; }

		// Token: 0x060046C6 RID: 18118 RVA: 0x000D616C File Offset: 0x000D436C
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("ExceptionName", this.ExceptionName);
			descriptor.AddProperty("ExceptionHandlerType", this.ExceptionHandlerType);
			descriptor.AddProperty("ErrorCoExistingAction", this.ErrorCoExistingAction);
		}
	}
}
