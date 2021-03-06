using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C60 RID: 3168
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class LogPushNotificationDataJsonRequest : BaseJsonRequest
	{
		// Token: 0x04002FB4 RID: 12212
		[DataMember(IsRequired = true, Order = 0)]
		public LogPushNotificationDataRequest Body;
	}
}
