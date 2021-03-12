using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.BirthdayAssistant
{
	// Token: 0x02000021 RID: 33
	internal sealed class BirthdayAssistantType : IEventBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00005B66 File Offset: 0x00003D66
		public LocalizedString Name
		{
			get
			{
				return Strings.birthdayName;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00005B6D File Offset: 0x00003D6D
		public string NonLocalizedName
		{
			get
			{
				return "BirthdayAssistant";
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00005B74 File Offset: 0x00003D74
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00005B77 File Offset: 0x00003D77
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return MapiEventTypeFlags.ObjectCreated | MapiEventTypeFlags.ObjectDeleted | MapiEventTypeFlags.ObjectModified | MapiEventTypeFlags.ObjectMoved;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00005B7B File Offset: 0x00003D7B
		public bool NeedsMailboxSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00005B7E File Offset: 0x00003D7E
		public bool ProcessesPublicDatabases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005B84 File Offset: 0x00003D84
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return new PropertyDefinition[]
				{
					ContactSchema.BirthdayLocal,
					ContactSchema.NotInBirthdayCalendar,
					ContactSchema.PartnerNetworkId
				};
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005BB1 File Offset: 0x00003DB1
		public Guid Identity
		{
			get
			{
				return AssistantIdentity.BirthdayAssistant;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005BB8 File Offset: 0x00003DB8
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new BirthdayAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x0400010C RID: 268
		public const string AssistantName = "BirthdayAssistant";
	}
}
