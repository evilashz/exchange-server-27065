using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Compliance.DAR;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.Dar
{
	// Token: 0x02000264 RID: 612
	internal sealed class TaskStoreEventBasedAssistantType : IEventBasedAssistantType, IAssistantType, IMailboxFilter
	{
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x00080460 File Offset: 0x0007E660
		public LocalizedString Name
		{
			get
			{
				return Strings.DarTaskStoreEventBasedAssistant;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x00080467 File Offset: 0x0007E667
		public string NonLocalizedName
		{
			get
			{
				return "DarTaskStoreEventBasedAssistant";
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x0008046E File Offset: 0x0007E66E
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return MapiEventTypeFlags.ObjectCreated;
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060016B2 RID: 5810 RVA: 0x00080471 File Offset: 0x0007E671
		public bool NeedsMailboxSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x00080474 File Offset: 0x0007E674
		public bool ProcessesPublicDatabases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060016B4 RID: 5812 RVA: 0x00080477 File Offset: 0x0007E677
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return TaskStoreEventBasedAssistantType.InternalPreloadItemProperties;
			}
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x0008047E File Offset: 0x0007E67E
		public Guid Identity
		{
			get
			{
				return AssistantIdentity.DarTaskStoreEventBasedAssistant;
			}
		}

		// Token: 0x060016B6 RID: 5814 RVA: 0x00080485 File Offset: 0x0007E685
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new TaskStoreEventBasedAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x00080499 File Offset: 0x0007E699
		public MailboxType MailboxType
		{
			get
			{
				return MailboxType.Arbitration;
			}
		}

		// Token: 0x04000D43 RID: 3395
		internal const string AssistantName = "DarTaskStoreEventBasedAssistant";

		// Token: 0x04000D44 RID: 3396
		internal static readonly PropertyDefinition[] InternalPreloadItemProperties = new PropertyDefinition[]
		{
			TaskStoreObjectSchema.CreateStorePropertyDefinition(EwsStoreObjectSchema.AlternativeId)
		};
	}
}
