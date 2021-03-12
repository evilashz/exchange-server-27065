using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D63 RID: 3427
	[MessageContract(IsWrapped = false)]
	public class AddImGroupSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030F5 RID: 12533
		[MessageBodyMember(Name = "AddImGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public AddImGroupResponseMessage Body;
	}
}
