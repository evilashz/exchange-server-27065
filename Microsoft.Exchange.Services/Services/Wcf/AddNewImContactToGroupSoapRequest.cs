using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D64 RID: 3428
	[MessageContract(IsWrapped = false)]
	public class AddNewImContactToGroupSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030F6 RID: 12534
		[MessageBodyMember(Name = "AddNewImContactToGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public AddNewImContactToGroupRequest Body;
	}
}
