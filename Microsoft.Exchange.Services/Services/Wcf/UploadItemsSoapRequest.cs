using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C9C RID: 3228
	[MessageContract(IsWrapped = false)]
	public class UploadItemsSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400302C RID: 12332
		[MessageBodyMember(Name = "UploadItems", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UploadItemsRequest Body;
	}
}
