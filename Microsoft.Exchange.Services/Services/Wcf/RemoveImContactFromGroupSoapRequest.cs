using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D60 RID: 3424
	[MessageContract(IsWrapped = false)]
	public class RemoveImContactFromGroupSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030F2 RID: 12530
		[MessageBodyMember(Name = "RemoveImContactFromGroup", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public RemoveImContactFromGroupRequest Body;
	}
}
