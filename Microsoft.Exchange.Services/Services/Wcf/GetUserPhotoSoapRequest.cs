using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D84 RID: 3460
	[MessageContract(IsWrapped = false)]
	public class GetUserPhotoSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003116 RID: 12566
		[MessageBodyMember(Name = "GetUserPhoto", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetUserPhotoRequest Body;
	}
}
