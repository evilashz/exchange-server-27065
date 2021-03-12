using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D9E RID: 3486
	[MessageContract(IsWrapped = false)]
	public class SetClutterStateSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003130 RID: 12592
		[MessageBodyMember(Name = "SetClutterState", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SetClutterStateRequest Body;
	}
}
