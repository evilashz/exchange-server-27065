using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D72 RID: 3442
	[MessageContract(IsWrapped = false)]
	public class SetImGroupSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003104 RID: 12548
		[MessageBodyMember(Name = "SetImGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public SetImGroupRequest Body;
	}
}
