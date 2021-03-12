using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200014F RID: 335
	internal sealed class ItemDisabledPreFormAction : IPreFormAction
	{
		// Token: 0x06000B84 RID: 2948 RVA: 0x00050A94 File Offset: 0x0004EC94
		public PreFormActionResponse Execute(OwaContext owaContext, out ApplicationElement applicationElement, out string type, out string state, out string action)
		{
			applicationElement = ApplicationElement.Item;
			type = "Disabled";
			action = owaContext.FormsRegistryContext.Action;
			state = string.Empty;
			return null;
		}
	}
}
