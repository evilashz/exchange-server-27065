using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D9C RID: 3484
	[MessageContract(IsWrapped = false)]
	public class GetClutterStateSoapRequest : BaseSoapRequest
	{
		// Token: 0x0400312E RID: 12590
		[MessageBodyMember(Name = "GetClutterState", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public GetClutterStateRequest Body;
	}
}
