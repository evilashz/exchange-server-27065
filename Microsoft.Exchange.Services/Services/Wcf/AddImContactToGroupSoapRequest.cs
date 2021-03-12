using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D5E RID: 3422
	[MessageContract(IsWrapped = false)]
	public class AddImContactToGroupSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030F0 RID: 12528
		[MessageBodyMember(Name = "AddImContactToGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public AddImContactToGroupRequest Body;
	}
}
