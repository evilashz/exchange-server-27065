using System;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.MailboxProcessor.MailboxProcessorDefinitions
{
	// Token: 0x02000237 RID: 567
	internal class MailboxProcessorMailboxData : AdminRpcMailboxData
	{
		// Token: 0x06001561 RID: 5473 RVA: 0x00079BAC File Offset: 0x00077DAC
		public MailboxProcessorMailboxData(DatabaseInfo databaseInfo, Guid mailboxGuid, int mailboxNumber, PropValue[] row) : base(mailboxGuid, mailboxNumber, databaseInfo.Guid)
		{
			MailboxProcessorAssistantType.TraceInformation(0L, "Retrieving mailbox information for database {0}", new object[]
			{
				databaseInfo.Guid
			});
			this.DatabaseName = databaseInfo.DatabaseName;
			PropValue mailboxProperty = MailboxTableQuery.GetMailboxProperty(row, PropTag.MailboxMiscFlags);
			if (mailboxProperty.PropTag == PropTag.MailboxMiscFlags)
			{
				MailboxMiscFlags @int = (MailboxMiscFlags)mailboxProperty.GetInt();
				this.IsMoveDestination = ((@int & MailboxMiscFlags.CreatedByMove) == MailboxMiscFlags.CreatedByMove);
				this.IsArchive = ((@int & MailboxMiscFlags.ArchiveMailbox) == MailboxMiscFlags.ArchiveMailbox);
			}
			else
			{
				MailboxProcessorAssistantType.TraceInformation(0L, "Cannot retrieve property MailboxMiscFlags for Mailbox with GUID {0}", new object[]
				{
					mailboxGuid
				});
				this.IsMoveDestination = false;
				this.IsArchive = false;
			}
			PropValue mailboxProperty2 = MailboxTableQuery.GetMailboxProperty(row, PropTag.PersistableTenantPartitionHint);
			this.TenantPartitionHint = null;
			if (mailboxProperty2.PropTag == PropTag.PersistableTenantPartitionHint)
			{
				byte[] bytes = mailboxProperty2.GetBytes();
				if (bytes != null && bytes.Length != 0)
				{
					this.TenantPartitionHint = TenantPartitionHint.FromPersistablePartitionHint(bytes);
				}
			}
			else
			{
				MailboxProcessorAssistantType.TraceInformation(0L, "Cannot retrieve property PersistableTenantPartitionHint for Mailbox with GUID {0}", new object[]
				{
					mailboxGuid
				});
			}
			MailboxProcessorAssistantType.TraceInformation(0L, "Found mailbox with GUID {0}. Is archive: {1}", new object[]
			{
				mailboxGuid,
				this.IsArchive
			});
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001562 RID: 5474 RVA: 0x00079CF6 File Offset: 0x00077EF6
		// (set) Token: 0x06001563 RID: 5475 RVA: 0x00079CFE File Offset: 0x00077EFE
		public string DatabaseName { get; private set; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x00079D07 File Offset: 0x00077F07
		// (set) Token: 0x06001565 RID: 5477 RVA: 0x00079D0F File Offset: 0x00077F0F
		public TenantPartitionHint TenantPartitionHint { get; private set; }

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x00079D18 File Offset: 0x00077F18
		// (set) Token: 0x06001567 RID: 5479 RVA: 0x00079D20 File Offset: 0x00077F20
		public bool IsArchive { get; private set; }

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x00079D29 File Offset: 0x00077F29
		// (set) Token: 0x06001569 RID: 5481 RVA: 0x00079D31 File Offset: 0x00077F31
		public bool IsMoveDestination { get; private set; }

		// Token: 0x0600156A RID: 5482 RVA: 0x00079D3C File Offset: 0x00077F3C
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			MailboxProcessorMailboxData mailboxProcessorMailboxData = other as MailboxProcessorMailboxData;
			return mailboxProcessorMailboxData != null && base.MailboxGuid == mailboxProcessorMailboxData.MailboxGuid && base.Equals(other);
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00079D78 File Offset: 0x00077F78
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ base.MailboxGuid.GetHashCode();
		}
	}
}
