using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000342 RID: 834
	internal sealed class CustomFormRedirectPreFormAction : IPreFormAction
	{
		// Token: 0x06001F93 RID: 8083 RVA: 0x000B606B File Offset: 0x000B426B
		public PreFormActionResponse Execute(OwaContext owaContext, out ApplicationElement applicationElement, out string type, out string state, out string action)
		{
			if (owaContext == null)
			{
				throw new ArgumentNullException("owaContext");
			}
			applicationElement = ApplicationElement.Item;
			type = owaContext.FormsRegistryContext.Type;
			state = owaContext.FormsRegistryContext.State;
			action = owaContext.FormsRegistryContext.Action;
			return null;
		}
	}
}
