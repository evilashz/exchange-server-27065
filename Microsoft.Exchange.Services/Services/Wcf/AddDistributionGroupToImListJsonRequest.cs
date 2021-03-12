using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C27 RID: 3111
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AddDistributionGroupToImListJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F7E RID: 12158
		[DataMember(IsRequired = true, Order = 0)]
		public AddDistributionGroupToImListRequest Body;
	}
}
