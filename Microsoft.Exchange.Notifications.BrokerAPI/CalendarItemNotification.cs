using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000014 RID: 20
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	public sealed class CalendarItemNotification : RowNotification
	{
		// Token: 0x0600007C RID: 124 RVA: 0x0000330E File Offset: 0x0000150E
		public CalendarItemNotification() : base(NotificationType.CalendarItem)
		{
		}
	}
}
