using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D73 RID: 3443
	[MessageContract(IsWrapped = false)]
	public class SetImGroupSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003105 RID: 12549
		[MessageBodyMember(Name = "SetImGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SetImGroupResponseMessage Body;
	}
}
