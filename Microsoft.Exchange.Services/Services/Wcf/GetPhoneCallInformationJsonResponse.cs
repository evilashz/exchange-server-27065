using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BE2 RID: 3042
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetPhoneCallInformationJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F39 RID: 12089
		[DataMember(IsRequired = true, Order = 0)]
		public GetPhoneCallInformationResponseMessage Body;
	}
}
