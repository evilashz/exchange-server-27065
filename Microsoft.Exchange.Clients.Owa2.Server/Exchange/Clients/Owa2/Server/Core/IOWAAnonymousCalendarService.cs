using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200014F RID: 335
	[ServiceContract]
	public interface IOWAAnonymousCalendarService
	{
		// Token: 0x06000BDB RID: 3035
		[OfflineClient(Queued = false)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		FindItemJsonResponse FindItem(FindItemJsonRequest request);

		// Token: 0x06000BDC RID: 3036
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OfflineClient(Queued = false)]
		[OperationContract]
		[WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
		GetItemJsonResponse GetItem(GetItemJsonRequest request);
	}
}
