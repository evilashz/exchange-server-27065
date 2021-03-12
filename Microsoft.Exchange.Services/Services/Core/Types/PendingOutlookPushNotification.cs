using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000F73 RID: 3955
	internal class PendingOutlookPushNotification
	{
		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x06006429 RID: 25641 RVA: 0x00138C8E File Offset: 0x00136E8E
		// (set) Token: 0x0600642A RID: 25642 RVA: 0x00138C96 File Offset: 0x00136E96
		internal bool CalendarChanged { get; set; }

		// Token: 0x170016B1 RID: 5809
		// (get) Token: 0x0600642B RID: 25643 RVA: 0x00138C9F File Offset: 0x00136E9F
		// (set) Token: 0x0600642C RID: 25644 RVA: 0x00138CA7 File Offset: 0x00136EA7
		internal bool ConnectionLost { get; set; }

		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x0600642D RID: 25645 RVA: 0x00138CB0 File Offset: 0x00136EB0
		// (set) Token: 0x0600642E RID: 25646 RVA: 0x00138CB8 File Offset: 0x00136EB8
		internal uint NotificationWaterMark { get; set; }

		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x0600642F RID: 25647 RVA: 0x00138CC1 File Offset: 0x00136EC1
		// (set) Token: 0x06006430 RID: 25648 RVA: 0x00138CC9 File Offset: 0x00136EC9
		internal Guid MailboxGuid { get; set; }

		// Token: 0x06006431 RID: 25649 RVA: 0x00138CD4 File Offset: 0x00136ED4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"PendingOutlookPushNotification(CalendarChanged=",
				this.CalendarChanged,
				"; NotificationWaterMark=",
				this.NotificationWaterMark,
				"; MailboxGuid=",
				this.MailboxGuid,
				")"
			});
		}

		// Token: 0x06006432 RID: 25650 RVA: 0x00138D38 File Offset: 0x00136F38
		internal void Merge(PendingOutlookPushNotification other)
		{
			this.CalendarChanged |= other.CalendarChanged;
			this.ConnectionLost |= other.ConnectionLost;
			if (this.NotificationWaterMark < other.NotificationWaterMark)
			{
				this.NotificationWaterMark = other.NotificationWaterMark;
			}
		}
	}
}
