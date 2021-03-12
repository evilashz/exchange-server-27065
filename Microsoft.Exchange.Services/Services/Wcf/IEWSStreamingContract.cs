using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000B78 RID: 2936
	[ServiceContract]
	public interface IEWSStreamingContract
	{
		// Token: 0x0600542B RID: 21547
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract(AsyncPattern = true)]
		[WebGet]
		IAsyncResult BeginGetUserPhoto(string email, UserPhotoSize size, AsyncCallback callback, object state);

		// Token: 0x0600542C RID: 21548
		Stream EndGetUserPhoto(IAsyncResult result);

		// Token: 0x0600542D RID: 21549
		[WebGet]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[OperationContract(AsyncPattern = true)]
		IAsyncResult BeginGetPeopleICommunicateWith(AsyncCallback callback, object state);

		// Token: 0x0600542E RID: 21550
		Stream EndGetPeopleICommunicateWith(IAsyncResult result);
	}
}
