using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C08 RID: 3080
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetInboxRulesJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F5F RID: 12127
		[DataMember(IsRequired = true, Order = 0)]
		public GetInboxRulesResponse Body;
	}
}
