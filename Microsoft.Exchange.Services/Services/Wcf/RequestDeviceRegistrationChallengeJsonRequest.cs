using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C5A RID: 3162
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RequestDeviceRegistrationChallengeJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FAE RID: 12206
		[DataMember(IsRequired = true, Order = 0)]
		public DeviceRegistrationChallengeRequest Body;
	}
}
