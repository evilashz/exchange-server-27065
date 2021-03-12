using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D5F RID: 3423
	[MessageContract(IsWrapped = false)]
	public class AddImContactToGroupSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030F1 RID: 12529
		[MessageBodyMember(Name = "AddImContactToGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public AddImContactToGroupResponseMessage Body;
	}
}
