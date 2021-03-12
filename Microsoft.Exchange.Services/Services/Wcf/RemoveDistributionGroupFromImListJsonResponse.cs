using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C3A RID: 3130
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveDistributionGroupFromImListJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F91 RID: 12177
		[DataMember(IsRequired = true, Order = 0)]
		public RemoveDistributionGroupFromImListResponseMessage Body;
	}
}
