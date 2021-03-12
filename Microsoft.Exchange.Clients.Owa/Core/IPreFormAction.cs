using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x0200000D RID: 13
	internal interface IPreFormAction
	{
		// Token: 0x06000073 RID: 115
		PreFormActionResponse Execute(OwaContext owaContext, out ApplicationElement applicationElement, out string type, out string state, out string action);
	}
}
