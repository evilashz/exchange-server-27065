using System;
using System.Web;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000250 RID: 592
	public interface IOwaServiceMessageInspector
	{
		// Token: 0x0600164A RID: 5706
		void AfterReceiveRequest(HttpRequest httpRequest, string methodName, object request);

		// Token: 0x0600164B RID: 5707
		void BeforeSendReply(HttpResponse httpResponse, string methodName, object response);
	}
}
