using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C07 RID: 3079
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetInboxRulesJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F5E RID: 12126
		[DataMember(IsRequired = true, Order = 0)]
		public GetInboxRulesRequest Body;
	}
}
