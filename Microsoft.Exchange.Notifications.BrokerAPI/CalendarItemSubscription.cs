using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000024 RID: 36
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	public class CalendarItemSubscription : RowSubscription
	{
		// Token: 0x060000D8 RID: 216 RVA: 0x000038A6 File Offset: 0x00001AA6
		public CalendarItemSubscription() : base(NotificationType.CalendarItem)
		{
		}
	}
}
