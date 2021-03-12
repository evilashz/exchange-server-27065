using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x020008FE RID: 2302
	[ServiceContract]
	public interface IJsonStreamingServiceContract
	{
		// Token: 0x0600411E RID: 16670
		[WebGet]
		[OperationContract(AsyncPattern = true)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[JsonResponseOptions(IsCacheable = true)]
		IAsyncResult BeginGetUserPhoto(string email, UserPhotoSize size, bool isPreview, bool fallbackToClearImage, AsyncCallback callback, object state);

		// Token: 0x0600411F RID: 16671
		Stream EndGetUserPhoto(IAsyncResult result);

		// Token: 0x06004120 RID: 16672
		[OperationContract(AsyncPattern = true)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		[JsonResponseOptions(IsCacheable = true)]
		[WebGet]
		IAsyncResult BeginGetPeopleICommunicateWith(AsyncCallback callback, object state);

		// Token: 0x06004121 RID: 16673
		Stream EndGetPeopleICommunicateWith(IAsyncResult result);
	}
}
