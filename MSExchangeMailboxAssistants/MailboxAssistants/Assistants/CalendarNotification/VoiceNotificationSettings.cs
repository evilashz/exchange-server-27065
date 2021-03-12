using System;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000FE RID: 254
	internal class VoiceNotificationSettings
	{
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00045253 File Offset: 0x00043453
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x0004525B File Offset: 0x0004345B
		public bool Enabled { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x00045264 File Offset: 0x00043464
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x0004526C File Offset: 0x0004346C
		public ExTimeZoneValue TimeZone { get; set; }

		// Token: 0x06000A8B RID: 2699 RVA: 0x00045275 File Offset: 0x00043475
		internal VoiceNotificationSettings(bool voiceNotificationEnabled, ExTimeZoneValue timeZone)
		{
			this.Enabled = voiceNotificationEnabled;
			this.TimeZone = timeZone;
		}
	}
}
