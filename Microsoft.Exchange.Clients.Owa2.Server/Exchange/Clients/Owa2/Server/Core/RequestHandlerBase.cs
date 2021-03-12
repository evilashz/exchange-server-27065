using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200008C RID: 140
	internal abstract class RequestHandlerBase
	{
		// Token: 0x0600052E RID: 1326
		internal abstract void OnPostAuthorizeRequest(object sender, EventArgs e);

		// Token: 0x0600052F RID: 1327
		internal abstract void OnPreRequestHandlerExecute(object sender, EventArgs e);

		// Token: 0x06000530 RID: 1328
		internal abstract void OnEndRequest(object sender, EventArgs e);

		// Token: 0x06000531 RID: 1329
		internal abstract void OnPreSendRequestHeaders(object sender, EventArgs e);
	}
}
