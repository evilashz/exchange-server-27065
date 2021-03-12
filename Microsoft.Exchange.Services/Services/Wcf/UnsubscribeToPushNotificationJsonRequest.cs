using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C5E RID: 3166
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UnsubscribeToPushNotificationJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FB2 RID: 12210
		[DataMember(IsRequired = true, Order = 0)]
		public UnsubscribeToPushNotificationRequest Body;
	}
}
