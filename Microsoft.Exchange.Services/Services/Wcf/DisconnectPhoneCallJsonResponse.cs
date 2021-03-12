using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000BE4 RID: 3044
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DisconnectPhoneCallJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002F3B RID: 12091
		[DataMember(IsRequired = true, Order = 0)]
		public DisconnectPhoneCallResponseMessage Body;
	}
}
