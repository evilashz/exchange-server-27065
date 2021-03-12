using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BDE RID: 3038
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetMailTipsJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F35 RID: 12085
		[DataMember(IsRequired = true, Order = 0)]
		public GetMailTipsResponseMessage Body;
	}
}
