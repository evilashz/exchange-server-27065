using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000F7 RID: 247
	internal class TextNotificationSettings
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x00043699 File Offset: 0x00041899
		// (set) Token: 0x06000A2F RID: 2607 RVA: 0x000436A1 File Offset: 0x000418A1
		internal MailboxRegionalConfiguration RegionalConfiguration { get; private set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x000436AA File Offset: 0x000418AA
		// (set) Token: 0x06000A31 RID: 2609 RVA: 0x000436B2 File Offset: 0x000418B2
		internal CalendarNotification TextNotification { get; private set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x000436BB File Offset: 0x000418BB
		// (set) Token: 0x06000A33 RID: 2611 RVA: 0x000436C3 File Offset: 0x000418C3
		internal StorageWorkingHours WorkingHours { get; private set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000A34 RID: 2612 RVA: 0x000436CC File Offset: 0x000418CC
		internal bool Enabled
		{
			get
			{
				return Utils.TextNotificationIsEnabled(this.TextNotification);
			}
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x000436D9 File Offset: 0x000418D9
		internal TextNotificationSettings(MailboxRegionalConfiguration regionalConfiguration, CalendarNotification textNotification, StorageWorkingHours workingHours)
		{
			this.RegionalConfiguration = regionalConfiguration;
			this.TextNotification = textNotification;
			this.WorkingHours = workingHours;
		}
	}
}
