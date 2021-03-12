using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200008E RID: 142
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AdminAuditLogSearchItem : AuditLogSearchItemBase
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x0001365C File Offset: 0x0001185C
		public AdminAuditLogSearchItem(MailboxSession session, Folder folder) : base(session, folder)
		{
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00013666 File Offset: 0x00011866
		public AdminAuditLogSearchItem(MailboxSession session, VersionedId messageId) : base(session, messageId)
		{
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x00013670 File Offset: 0x00011870
		protected override string ItemClass
		{
			get
			{
				return "IPM.AuditLogSearch.Admin";
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00013677 File Offset: 0x00011877
		protected override PropertyDefinition[] PropertiesToLoad
		{
			get
			{
				return AdminAuditLogSearchItem.propertiesToLoad;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0001367E File Offset: 0x0001187E
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x0001368B File Offset: 0x0001188B
		public MultiValuedProperty<string> Cmdlets
		{
			get
			{
				return base.GetPropertiesPossiblyNotFound<string>(AdminAuditLogSearchItemSchema.Cmdlets);
			}
			set
			{
				base.Message[AdminAuditLogSearchItemSchema.Cmdlets] = value.ToArray();
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x000136A3 File Offset: 0x000118A3
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x000136B0 File Offset: 0x000118B0
		public MultiValuedProperty<string> Parameters
		{
			get
			{
				return base.GetPropertiesPossiblyNotFound<string>(AdminAuditLogSearchItemSchema.Parameters);
			}
			set
			{
				base.Message[AdminAuditLogSearchItemSchema.Parameters] = value.ToArray();
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x000136C8 File Offset: 0x000118C8
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x000136D5 File Offset: 0x000118D5
		public MultiValuedProperty<string> ObjectIds
		{
			get
			{
				return base.GetPropertiesPossiblyNotFound<string>(AdminAuditLogSearchItemSchema.ObjectIds);
			}
			set
			{
				base.Message[AdminAuditLogSearchItemSchema.ObjectIds] = value.ToArray();
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x000136ED File Offset: 0x000118ED
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x000136FA File Offset: 0x000118FA
		public MultiValuedProperty<string> RawUserIds
		{
			get
			{
				return base.GetPropertiesPossiblyNotFound<string>(AdminAuditLogSearchItemSchema.RawUserIds);
			}
			set
			{
				base.Message[AdminAuditLogSearchItemSchema.RawUserIds] = value.ToArray();
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00013712 File Offset: 0x00011912
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0001371F File Offset: 0x0001191F
		public MultiValuedProperty<string> ResolvedUsers
		{
			get
			{
				return base.GetPropertiesPossiblyNotFound<string>(AdminAuditLogSearchItemSchema.ResolvedUsers);
			}
			set
			{
				base.Message[AdminAuditLogSearchItemSchema.ResolvedUsers] = value.ToArray();
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00013737 File Offset: 0x00011937
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x00013749 File Offset: 0x00011949
		public bool RedactDatacenterAdmins
		{
			get
			{
				return base.Message.GetValueOrDefault<bool>(AdminAuditLogSearchItemSchema.RedactDatacenterAdmins);
			}
			set
			{
				base.Message[AdminAuditLogSearchItemSchema.RedactDatacenterAdmins] = value;
			}
		}

		// Token: 0x04000240 RID: 576
		private static PropertyDefinition[] propertiesToLoad = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.CreationTime,
			AuditLogSearchItemSchema.Identity,
			AuditLogSearchItemSchema.Name,
			AuditLogSearchItemSchema.StartDate,
			AuditLogSearchItemSchema.EndDate,
			AuditLogSearchItemSchema.StatusMailRecipients,
			AuditLogSearchItemSchema.CreatedBy,
			AuditLogSearchItemSchema.CreatedByEx,
			AuditLogSearchItemSchema.ExternalAccess,
			AdminAuditLogSearchItemSchema.Cmdlets,
			AdminAuditLogSearchItemSchema.Parameters,
			AdminAuditLogSearchItemSchema.ObjectIds,
			AdminAuditLogSearchItemSchema.RawUserIds,
			AdminAuditLogSearchItemSchema.ResolvedUsers,
			AdminAuditLogSearchItemSchema.RedactDatacenterAdmins
		};
	}
}
