using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C5B RID: 3163
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RequestDeviceRegistrationChallengeJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FAF RID: 12207
		[DataMember(IsRequired = true, Order = 0)]
		public RequestDeviceRegistrationChallengeResponseMessage Body;
	}
}
