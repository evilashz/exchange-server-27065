using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C09 RID: 3081
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateInboxRulesJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F60 RID: 12128
		[DataMember(IsRequired = true, Order = 0)]
		public UpdateInboxRulesRequest Body;
	}
}
