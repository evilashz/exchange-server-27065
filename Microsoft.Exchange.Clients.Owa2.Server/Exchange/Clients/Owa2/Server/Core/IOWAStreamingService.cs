using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000151 RID: 337
	[ServiceContract]
	public interface IOWAStreamingService : IJsonStreamingServiceContract
	{
		// Token: 0x06000C56 RID: 3158
		[JsonRequestFormat(Format = JsonRequestFormat.QueryString)]
		[WebGet]
		[JsonResponseOptions(IsCacheable = true)]
		[OperationContract]
		Stream GetFileAttachment(string id, bool isImagePreview, bool asDataUri);

		// Token: 0x06000C57 RID: 3159
		[WebGet]
		[OperationContract]
		[JsonRequestFormat(Format = JsonRequestFormat.QueryString)]
		[JsonResponseOptions(IsCacheable = true)]
		Stream GetAllAttachmentsAsZip(string id);

		// Token: 0x06000C58 RID: 3160
		[OperationContract]
		[WebGet]
		[JsonResponseOptions(IsCacheable = true)]
		[JsonRequestFormat(Format = JsonRequestFormat.Custom)]
		Stream GetPersonaPhoto(string personId, string adObjectId, string email, string singleSourceId, UserPhotoSize size);
	}
}
