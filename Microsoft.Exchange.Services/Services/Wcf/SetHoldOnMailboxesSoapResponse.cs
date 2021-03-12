using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D3D RID: 3389
	[MessageContract(IsWrapped = false)]
	public class SetHoldOnMailboxesSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030CF RID: 12495
		[MessageBodyMember(Name = "SetHoldOnMailboxesResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SetHoldOnMailboxesResponse Body;
	}
}
