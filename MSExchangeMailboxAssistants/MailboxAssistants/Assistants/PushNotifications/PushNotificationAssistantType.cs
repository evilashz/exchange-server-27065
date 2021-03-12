using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PushNotifications
{
	// Token: 0x02000207 RID: 519
	internal sealed class PushNotificationAssistantType : IEventBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x060013EE RID: 5102 RVA: 0x00073BCC File Offset: 0x00071DCC
		static PushNotificationAssistantType()
		{
			PushNotificationsAssistantPerfCounters.PublishingRequestErrors.Reset();
			PushNotificationsAssistantPerfCounters.TotalNewSubscriptionsCreated.Reset();
			PushNotificationsAssistantPerfCounters.TotalSubscriptionsUpdated.Reset();
			PushNotificationsAssistantPerfCounters.TotalSubscriptionsExpiredCleanup.Reset();
			PushNotificationsAssistantPerfCounters.CurrentActiveUserSubscriptions.Reset();
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x00073C0B File Offset: 0x00071E0B
		public LocalizedString Name
		{
			get
			{
				return Strings.pushNotificationAssistantName;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060013F0 RID: 5104 RVA: 0x00073C12 File Offset: 0x00071E12
		public string NonLocalizedName
		{
			get
			{
				return "PushNotificationAssistant";
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060013F1 RID: 5105 RVA: 0x00073C19 File Offset: 0x00071E19
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return MapiEventTypeFlags.NewMail | MapiEventTypeFlags.ObjectCreated | MapiEventTypeFlags.ObjectModified;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060013F2 RID: 5106 RVA: 0x00073C1D File Offset: 0x00071E1D
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060013F3 RID: 5107 RVA: 0x00073C20 File Offset: 0x00071E20
		public bool NeedsMailboxSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060013F4 RID: 5108 RVA: 0x00073C23 File Offset: 0x00071E23
		public bool ProcessesPublicDatabases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060013F5 RID: 5109 RVA: 0x00073C26 File Offset: 0x00071E26
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return PushNotificationAssistantType.preloadItemProperties;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060013F6 RID: 5110 RVA: 0x00073C2D File Offset: 0x00071E2D
		public Guid Identity
		{
			get
			{
				return AssistantIdentity.PushNotitificationEventBasedAssistant;
			}
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00073C34 File Offset: 0x00071E34
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new PushNotificationAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x04000C1C RID: 3100
		internal const string AssistantName = "PushNotificationAssistant";

		// Token: 0x04000C1D RID: 3101
		private static readonly PropertyDefinition[] preloadItemProperties = new PropertyDefinition[0];
	}
}
