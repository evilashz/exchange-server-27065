using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BE1 RID: 3041
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetPhoneCallInformationJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F38 RID: 12088
		[DataMember(IsRequired = true, Order = 0)]
		public GetPhoneCallInformationRequest Body;
	}
}
