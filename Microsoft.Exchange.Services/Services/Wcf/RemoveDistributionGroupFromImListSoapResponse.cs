using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D6F RID: 3439
	[MessageContract(IsWrapped = false)]
	public class RemoveDistributionGroupFromImListSoapResponse : BaseSoapResponse
	{
		// Token: 0x04003101 RID: 12545
		[MessageBodyMember(Name = "RemoveDistributionGroupFromImListResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public RemoveDistributionGroupFromImListResponseMessage Body;
	}
}
