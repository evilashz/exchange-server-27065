using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D71 RID: 3441
	[MessageContract(IsWrapped = false)]
	public class RemoveImGroupSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003103 RID: 12547
		[MessageBodyMember(Name = "RemoveImGroupResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public RemoveImGroupResponseMessage Body;
	}
}
