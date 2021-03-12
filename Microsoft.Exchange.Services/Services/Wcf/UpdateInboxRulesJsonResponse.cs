using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C0A RID: 3082
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateInboxRulesJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F61 RID: 12129
		[DataMember(IsRequired = true, Order = 0)]
		public UpdateInboxRulesResponse Body;
	}
}
