using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BE3 RID: 3043
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DisconnectPhoneCallJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002F3A RID: 12090
		[DataMember(IsRequired = true, Order = 0)]
		public DisconnectPhoneCallRequest Body;
	}
}
