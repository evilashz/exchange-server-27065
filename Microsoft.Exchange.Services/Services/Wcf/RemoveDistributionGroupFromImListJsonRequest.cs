using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C39 RID: 3129
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RemoveDistributionGroupFromImListJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F90 RID: 12176
		[DataMember(IsRequired = true, Order = 0)]
		public RemoveDistributionGroupFromImListRequest Body;
	}
}
