using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000C61 RID: 3169
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class LogPushNotificationDataJsonResponse : BaseJsonResponse
	{
		// Token: 0x04002FB5 RID: 12213
		[DataMember(IsRequired = true, Order = 0)]
		public LogPushNotificationDataResponse Body;
	}
}
