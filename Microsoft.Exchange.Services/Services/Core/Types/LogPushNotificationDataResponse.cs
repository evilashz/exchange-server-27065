using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200052E RID: 1326
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class LogPushNotificationDataResponse : BaseResponseMessage
	{
		// Token: 0x060025E4 RID: 9700 RVA: 0x000A60B2 File Offset: 0x000A42B2
		public LogPushNotificationDataResponse() : base(ResponseType.LogPushNotificationDataResponseMessage)
		{
		}
	}
}
