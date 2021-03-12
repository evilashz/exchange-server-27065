using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Calendar
{
	// Token: 0x020000B6 RID: 182
	internal sealed class CalendarAssistantType : IEventBasedAssistantType, IAssistantType
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x000367F6 File Offset: 0x000349F6
		public LocalizedString Name
		{
			get
			{
				return Strings.calName;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x000367FD File Offset: 0x000349FD
		public string NonLocalizedName
		{
			get
			{
				return "CalendarAssistant";
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x00036804 File Offset: 0x00034A04
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return (MapiEventTypeFlags)(-1);
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00036807 File Offset: 0x00034A07
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x0003680A File Offset: 0x00034A0A
		public bool NeedsMailboxSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0003680D File Offset: 0x00034A0D
		public bool ProcessesPublicDatabases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060007A7 RID: 1959 RVA: 0x00036810 File Offset: 0x00034A10
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return CalendarAssistantType.preloadItemProperties;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00036817 File Offset: 0x00034A17
		public Guid Identity
		{
			get
			{
				return AssistantIdentity.CalendarAssistant;
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0003681E File Offset: 0x00034A1E
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new CalendarAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x04000591 RID: 1425
		internal const string AssistantName = "CalendarAssistant";

		// Token: 0x04000592 RID: 1426
		private static readonly PropertyDefinition[] preloadItemProperties = new PropertyDefinition[]
		{
			MeetingMessageSchema.CalendarProcessed,
			CalendarItemBaseSchema.AppointmentSequenceNumber,
			MessageItemSchema.MapiHasAttachment,
			MessageItemSchema.ReceivedRepresentingEmailAddress,
			ItemSchema.InternetMessageId
		};
	}
}
