using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001A9 RID: 425
	[DataContract]
	internal class ReminderNotificationPayload : NotificationPayloadBase
	{
		// Token: 0x06000F33 RID: 3891 RVA: 0x0003B067 File Offset: 0x00039267
		public ReminderNotificationPayload()
		{
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x0003B06F File Offset: 0x0003926F
		public ReminderNotificationPayload(bool shouldGetReminders)
		{
			this.shouldGetReminders = shouldGetReminders;
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x0003B07E File Offset: 0x0003927E
		// (set) Token: 0x06000F36 RID: 3894 RVA: 0x0003B086 File Offset: 0x00039286
		public bool ShouldGetReminders
		{
			get
			{
				return this.shouldGetReminders;
			}
			set
			{
				this.shouldGetReminders = value;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x0003B08F File Offset: 0x0003928F
		// (set) Token: 0x06000F38 RID: 3896 RVA: 0x0003B097 File Offset: 0x00039297
		public bool Reload
		{
			get
			{
				return this.reload;
			}
			set
			{
				this.reload = value;
			}
		}

		// Token: 0x04000931 RID: 2353
		[DataMember]
		private bool shouldGetReminders;

		// Token: 0x04000932 RID: 2354
		[DataMember(EmitDefaultValue = false)]
		private bool reload;
	}
}
