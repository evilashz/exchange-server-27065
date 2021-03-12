using System;
using System.ServiceModel;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000D5C RID: 3420
	[MessageContract(IsWrapped = false)]
	public class AddDistributionGroupToImListSoapRequest : BaseSoapRequest
	{
		// Token: 0x040030EE RID: 12526
		[MessageBodyMember(Name = "AddDistributionGroupToImList", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages", Order = 0)]
		public AddDistributionGroupToImListRequest Body;
	}
}
