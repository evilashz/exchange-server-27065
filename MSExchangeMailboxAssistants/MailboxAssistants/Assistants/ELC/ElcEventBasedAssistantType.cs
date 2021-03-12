using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.ELC
{
	// Token: 0x02000082 RID: 130
	internal sealed class ElcEventBasedAssistantType : IEventBasedAssistantType, IAssistantType
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00024DA7 File Offset: 0x00022FA7
		public LocalizedString Name
		{
			get
			{
				return Strings.elcEventName;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00024DAE File Offset: 0x00022FAE
		public string NonLocalizedName
		{
			get
			{
				return "ElcEventBasedAssistant";
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00024DB5 File Offset: 0x00022FB5
		public MapiEventTypeFlags EventMask
		{
			get
			{
				return (MapiEventTypeFlags)(-1);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00024DB8 File Offset: 0x00022FB8
		public bool NeedsMailboxSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00024DBB File Offset: 0x00022FBB
		public bool ProcessesPublicDatabases
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00024DBE File Offset: 0x00022FBE
		public PropertyDefinition[] PreloadItemProperties
		{
			get
			{
				return ElcEventBasedAssistantType.InternalPreloadItemProperties;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00024DC5 File Offset: 0x00022FC5
		public Guid Identity
		{
			get
			{
				return AssistantIdentity.ElcEventBasedAssistant;
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00024DCC File Offset: 0x00022FCC
		public IEventBasedAssistant CreateInstance(DatabaseInfo databaseInfo)
		{
			return new ElcEventBasedAssistant(databaseInfo, this.Name, this.NonLocalizedName);
		}

		// Token: 0x040003AB RID: 939
		internal const string AssistantName = "ElcEventBasedAssistant";

		// Token: 0x040003AC RID: 940
		internal static readonly PropertyDefinition[] InternalPreloadItemProperties = new PropertyDefinition[]
		{
			ItemSchema.RetentionDate,
			ItemSchema.EHAMigrationExpiryDate,
			StoreObjectSchema.PolicyTag,
			StoreObjectSchema.RetentionPeriod,
			ItemSchema.StartDateEtc,
			StoreObjectSchema.PolicyTag,
			StoreObjectSchema.RetentionPeriod,
			StoreObjectSchema.RetentionFlags,
			StoreObjectSchema.RetentionFlags,
			StoreObjectSchema.ItemClass,
			CalendarItemBaseSchema.CalendarItemType,
			CalendarItemInstanceSchema.EndTime,
			TaskSchema.IsTaskRecurring,
			ItemSchema.Subject,
			ItemSchema.SentRepresentingEmailAddress,
			MessageItemSchema.SenderEmailAddress,
			ItemSchema.ElcAutoCopyTag,
			ItemSchema.ParentDisplayName,
			StoreObjectSchema.ArchiveTag,
			StoreObjectSchema.ArchivePeriod,
			StoreObjectSchema.ArchiveTag,
			ItemSchema.ArchiveDate,
			MessageItemSchema.IsDraft,
			StoreObjectSchema.ExplicitPolicyTag,
			StoreObjectSchema.ExplicitArchiveTag,
			StoreObjectSchema.ExplicitPolicyTag,
			StoreObjectSchema.ExplicitArchiveTag
		};
	}
}
