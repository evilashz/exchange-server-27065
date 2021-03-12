using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D6E RID: 3438
	[MessageContract(IsWrapped = false)]
	public class RemoveDistributionGroupFromImListSoapRequest : BaseSoapRequest
	{
		// Token: 0x04003100 RID: 12544
		[MessageBodyMember(Name = "RemoveDistributionGroupFromImList", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public RemoveDistributionGroupFromImListRequest Body;
	}
}
