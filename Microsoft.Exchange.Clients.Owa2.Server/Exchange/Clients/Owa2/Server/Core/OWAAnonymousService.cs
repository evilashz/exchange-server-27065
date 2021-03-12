using System;
using System.ServiceModel.Activation;
using Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands.Anonymous;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001FC RID: 508
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
	public class OWAAnonymousService : IOWAAnonymousCalendarService
	{
		// Token: 0x060011E1 RID: 4577 RVA: 0x000450DD File Offset: 0x000432DD
		public FindItemJsonResponse FindItem(FindItemJsonRequest request)
		{
			return new FindItemAnonymous(request).Execute();
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x000450EA File Offset: 0x000432EA
		public GetItemJsonResponse GetItem(GetItemJsonRequest request)
		{
			return new GetItemAnonymous(request).Execute();
		}
	}
}
