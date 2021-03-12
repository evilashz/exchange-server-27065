using System;
using System.Collections.Generic;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarNotification
{
	// Token: 0x020000E0 RID: 224
	internal sealed class CalendarNotificationAssistantType : IEventBasedAssistantType, IAssistantType
	{
		// Token: 0x0600096A RID: 2410 RVA: 0x0003F690 File Offset: 0x0003D890
		static CalendarNotificationAssistantType()
		{
			List<PropertyDefinition> list = new List<PropertyDefinition>(CalendarInfo.InterestedProperties);
			list.AddRange(new PropertyDefinition[]
			{
				ItemSchema.InternetMessageId,
				ItemSchema.InternetMessageIdHash,
				MessageItemSchema.TextMessageDeliveryStatus
			});
			CalendarNotificationAssistantType.preloadItemProperties = list.ToArray();
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x0003F6E1 File Offset: 0x0003D8E1
		public LocalizedString Name
		{
			get
			{
				return Strings.calnotifName;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600096D RID: 2413 RVA: 0x0003F6E8 File Offset: 0x0003D8E8
		public string NonLocalizedName
		{
			get
			{
				return "CalendarNotificationAssistant";
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600096E RID: 2414 RVA: 0x0003F6EF File Offset: 0x0003D8EF
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return (MapiEventTypeFlags)(-1);
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x0003F6F2 File Offset: 0x0003D8F2
		public bool NeedsMailboxSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000970 RID: 2416 RVA: 0x0003F6F5 File Offset: 0x0003D8F5
		public bool ProcessesPublicDatabases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x0003F6F8 File Offset: 0x0003D8F8
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return CalendarNotificationAssistantType.preloadItemProperties;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x0003F6FF File Offset: 0x0003D8FF
		public Guid Identity
		{
			get
			{
				return AssistantIdentity.CalendarNotificationAssistant;
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x0003F706 File Offset: 0x0003D906
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new CalendarNotificationAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x04000657 RID: 1623
		internal const string AssistantName = "CalendarNotificationAssistant";

		// Token: 0x04000658 RID: 1624
		private static readonly PropertyDefinition[] preloadItemProperties;
	}
}
