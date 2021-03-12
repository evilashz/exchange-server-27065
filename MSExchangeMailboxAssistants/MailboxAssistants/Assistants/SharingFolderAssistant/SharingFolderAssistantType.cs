using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.SharingFolderAssistant
{
	// Token: 0x020000CF RID: 207
	internal sealed class SharingFolderAssistantType : IEventBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x0003C325 File Offset: 0x0003A525
		public LocalizedString Name
		{
			get
			{
				return Strings.descSharingFolderAssistantName;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0003C32C File Offset: 0x0003A52C
		public string NonLocalizedName
		{
			get
			{
				return "SharingFolderAssistant";
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x0003C333 File Offset: 0x0003A533
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return SharingFolderAssistantType.PossibleInterestingEvents;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x0003C33A File Offset: 0x0003A53A
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.User;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060008D1 RID: 2257 RVA: 0x0003C33D File Offset: 0x0003A53D
		public bool NeedsMailboxSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x0003C340 File Offset: 0x0003A540
		public bool ProcessesPublicDatabases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x0003C343 File Offset: 0x0003A543
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return SharingFolderAssistantType.PreloadProperties;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0003C34A File Offset: 0x0003A54A
		public Guid Identity
		{
			get
			{
				return AssistantIdentity.SharingFolderAssistant;
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0003C351 File Offset: 0x0003A551
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new SharingFolderAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x04000614 RID: 1556
		internal const string AssistantName = "SharingFolderAssistant";

		// Token: 0x04000615 RID: 1557
		private static readonly PropertyDefinition[] PreloadProperties = new PropertyDefinition[]
		{
			FolderSchema.ExtendedFolderFlags
		};

		// Token: 0x04000616 RID: 1558
		public static MapiEventTypeFlags PossibleInterestingEvents = MapiEventTypeFlags.ObjectCreated | MapiEventTypeFlags.ObjectDeleted | MapiEventTypeFlags.ObjectMoved;
	}
}
