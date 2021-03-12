using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C9D RID: 3229
	[MessageContract(IsWrapped = false)]
	public class UploadItemsSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400302D RID: 12333
		[MessageBodyMember(Name = "UploadItemsResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public UploadItemsResponse Body;
	}
}
