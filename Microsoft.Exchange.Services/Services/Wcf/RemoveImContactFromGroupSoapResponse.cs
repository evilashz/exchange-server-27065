using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D61 RID: 3425
	[MessageContract(IsWrapped = false)]
	public class RemoveImContactFromGroupSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030F3 RID: 12531
		[MessageBodyMember(Name = "RemoveImContactFromGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public RemoveImContactFromGroupResponseMessage Body;
	}
}
