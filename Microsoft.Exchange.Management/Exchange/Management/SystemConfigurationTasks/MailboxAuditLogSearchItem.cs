using System;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200008D RID: 141
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MailboxAuditLogSearchItem : AuditLogSearchItemBase
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x00013486 File Offset: 0x00011686
		public MailboxAuditLogSearchItem(MailboxSession session, Folder folder) : base(session, folder)
		{
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00013490 File Offset: 0x00011690
		public MailboxAuditLogSearchItem(MailboxSession session, VersionedId messageId) : base(session, messageId)
		{
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0001349A File Offset: 0x0001169A
		protected override string ItemClass
		{
			get
			{
				return "IPM.AuditLogSearch.Mailbox";
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x000134A1 File Offset: 0x000116A1
		protected override PropertyDefinition[] PropertiesToLoad
		{
			get
			{
				return MailboxAuditLogSearchItem.propertiesToLoad;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x000134A8 File Offset: 0x000116A8
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x00013518 File Offset: 0x00011718
		public MultiValuedProperty<ADObjectId> MailboxIds
		{
			get
			{
				MultiValuedProperty<byte[]> propertiesPossiblyNotFound = base.GetPropertiesPossiblyNotFound<byte[]>(MailboxAuditLogSearchItemSchema.MailboxIds);
				MultiValuedProperty<ADObjectId> multiValuedProperty = new MultiValuedProperty<ADObjectId>();
				foreach (byte[] bytes in propertiesPossiblyNotFound)
				{
					multiValuedProperty.Add(new ADObjectId(bytes));
				}
				return multiValuedProperty;
			}
			set
			{
				base.Message[MailboxAuditLogSearchItemSchema.MailboxIds] = (from adObjectId in value
				select adObjectId.GetBytes()).ToArray<byte[]>();
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00013552 File Offset: 0x00011752
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x0001355F File Offset: 0x0001175F
		public MultiValuedProperty<string> LogonTypeStrings
		{
			get
			{
				return base.GetPropertiesPossiblyNotFound<string>(MailboxAuditLogSearchItemSchema.LogonTypeStrings);
			}
			set
			{
				base.Message[MailboxAuditLogSearchItemSchema.LogonTypeStrings] = value.ToArray();
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00013577 File Offset: 0x00011777
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x00013584 File Offset: 0x00011784
		public MultiValuedProperty<string> Operations
		{
			get
			{
				return base.GetPropertiesPossiblyNotFound<string>(MailboxAuditLogSearchItemSchema.Operations);
			}
			set
			{
				base.Message[MailboxAuditLogSearchItemSchema.Operations] = value.ToArray();
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0001359C File Offset: 0x0001179C
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x000135B3 File Offset: 0x000117B3
		public bool ShowDetails
		{
			get
			{
				return (bool)base.Message[MailboxAuditLogSearchItemSchema.ShowDetails];
			}
			set
			{
				base.Message[MailboxAuditLogSearchItemSchema.ShowDetails] = value;
			}
		}

		// Token: 0x0400023E RID: 574
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
			MailboxAuditLogSearchItemSchema.MailboxIds,
			MailboxAuditLogSearchItemSchema.LogonTypeStrings,
			MailboxAuditLogSearchItemSchema.Operations,
			MailboxAuditLogSearchItemSchema.ShowDetails
		};
	}
}
