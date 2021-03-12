using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D65 RID: 3429
	[MessageContract(IsWrapped = false)]
	public class AddNewImContactToGroupSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030F7 RID: 12535
		[MessageBodyMember(Name = "AddNewImContactToGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public AddNewImContactToGroupResponseMessage Body;
	}
}
