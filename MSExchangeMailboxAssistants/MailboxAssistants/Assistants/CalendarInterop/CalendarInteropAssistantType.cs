using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.CalendarInterop
{
	// Token: 0x02000261 RID: 609
	internal sealed class CalendarInteropAssistantType : IEventBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x00080276 File Offset: 0x0007E476
		public LocalizedString Name
		{
			get
			{
				return Strings.CalendarInteropAssistantName;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x0008027D File Offset: 0x0007E47D
		public string NonLocalizedName
		{
			get
			{
				return "CalendarInteropAssistant";
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00080284 File Offset: 0x0007E484
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return MapiEventTypeFlags.ObjectModified;
			}
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x00080288 File Offset: 0x0007E488
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x0008028B File Offset: 0x0007E48B
		public bool NeedsMailboxSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0008028E File Offset: 0x0007E48E
		public bool ProcessesPublicDatabases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x00080291 File Offset: 0x0007E491
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return CalendarInteropAssistantType.preloadItemProperties;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x00080298 File Offset: 0x0007E498
		public Guid Identity
		{
			get
			{
				return AssistantIdentity.CalendarInteropAssistant;
			}
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0008029F File Offset: 0x0007E49F
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new CalendarInteropAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x04000D41 RID: 3393
		internal const string AssistantName = "CalendarInteropAssistant";

		// Token: 0x04000D42 RID: 3394
		private static readonly PropertyDefinition[] preloadItemProperties = new PropertyDefinition[0];
	}
}
