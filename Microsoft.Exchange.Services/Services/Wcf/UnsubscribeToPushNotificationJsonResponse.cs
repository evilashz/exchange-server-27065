using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C5F RID: 3167
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UnsubscribeToPushNotificationJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FB3 RID: 12211
		[DataMember(IsRequired = true, Order = 0)]
		public UnsubscribeToPushNotificationResponse Body;
	}
}
