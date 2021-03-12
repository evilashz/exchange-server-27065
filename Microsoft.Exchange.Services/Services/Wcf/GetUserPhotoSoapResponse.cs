using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D85 RID: 3461
	[MessageContract(IsWrapped = false)]
	public class GetUserPhotoSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003117 RID: 12567
		[MessageBodyMember(Name = "GetUserPhotoResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUserPhotoResponseMessage Body;
	}
}
