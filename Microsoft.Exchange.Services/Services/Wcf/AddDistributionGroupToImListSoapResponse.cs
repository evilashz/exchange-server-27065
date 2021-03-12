using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D5D RID: 3421
	[MessageContract(IsWrapped = false)]
	public class AddDistributionGroupToImListSoapResponse : BaseSoapResponse
	{
		// Token: 0x040030EF RID: 12527
		[MessageBodyMember(Name = "AddDistributionGroupToImListResponse", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public AddDistributionGroupToImListResponseMessage Body;
	}
}
