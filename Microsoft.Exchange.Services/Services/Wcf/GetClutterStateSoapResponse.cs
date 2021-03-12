using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D9D RID: 3485
	[MessageContract(IsWrapped = false)]
	public class GetClutterStateSoapResponse : BaseSoapResponse
	{
		// Token: 0x0400312F RID: 12591
		[MessageBodyMember(Name = "GetClutterStateResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetClutterStateResponse Body;
	}
}
