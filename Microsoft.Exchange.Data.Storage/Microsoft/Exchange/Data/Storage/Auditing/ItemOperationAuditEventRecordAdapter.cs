using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.Compliance.Audit.Schema.Mailbox;

namespace Microsoft.Exchange.Data.Storage.Auditing
{
	// Token: 0x02000F2A RID: 3882
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ItemOperationAuditEventRecordAdapter : AuditEventRecordAdapter
	{
		// Token: 0x06008589 RID: 34185 RVA: 0x00247F64 File Offset: 0x00246164
		public ItemOperationAuditEventRecordAdapter(ExchangeMailboxAuditRecord record, string displayOrganizationId) : base(record, displayOrganizationId)
		{
			this.record = record;
		}

		// Token: 0x0600858A RID: 34186 RVA: 0x00248310 File Offset: 0x00246510
		protected override IEnumerable<KeyValuePair<string, string>> InternalGetEventDetails()
		{
			foreach (KeyValuePair<string, string> detail in base.InternalGetEventDetails())
			{
				yield return detail;
			}
			if (this.record.Item != null && this.record.Item.ParentFolder != null)
			{
				yield return base.MakePair("FolderId", this.record.Item.ParentFolder.Id);
				yield return base.MakePair("FolderPathName", this.record.Item.ParentFolder.PathName);
			}
			if (MailboxAuditOperations.FolderBind != base.AuditOperation)
			{
				if (this.record.Item != null)
				{
					KeyValuePair<string, string> p;
					if (base.TryMakePair("ItemId", this.record.Item.Id, out p))
					{
						yield return p;
					}
					if (base.TryMakePair("ItemSubject", this.record.Item.Subject, out p))
					{
						yield return p;
					}
				}
				if (MailboxAuditOperations.Update == base.AuditOperation)
				{
					List<string> dirtyProperties = this.record.ModifiedProperties ?? new List<string>(0);
					yield return base.MakePair("DirtyProperties", string.Join(", ", dirtyProperties.ToArray()));
				}
			}
			yield break;
		}

		// Token: 0x04005967 RID: 22887
		private readonly ExchangeMailboxAuditRecord record;
	}
}
