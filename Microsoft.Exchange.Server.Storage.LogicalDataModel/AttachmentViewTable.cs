using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200001C RID: 28
	public sealed class AttachmentViewTable : ViewTable
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x0002C974 File Offset: 0x0002AB74
		public AttachmentViewTable(Context context, Mailbox mailbox, Message parentMessage) : base(mailbox, DatabaseSchema.AttachmentTable(mailbox.Database).Table)
		{
			this.parentMessage = parentMessage;
			AttachmentTable attachmentTable = DatabaseSchema.AttachmentTable(mailbox.Database);
			AttachmentTableFunctionTableFunction attachmentTableFunctionTableFunction = DatabaseSchema.AttachmentTableFunctionTableFunction(mailbox.Database);
			this.UpdatePseudoIndexIfNeeded(context);
			SearchCriteria implicitCriteria = Factory.CreateSearchCriteriaCompare(attachmentTable.MailboxPartitionNumber, SearchCriteriaCompare.SearchRelOp.Equal, Factory.CreateConstantColumn(mailbox.MailboxPartitionNumber, attachmentTable.MailboxPartitionNumber));
			base.SetImplicitCriteria(implicitCriteria);
			this.SortTable(attachmentTableFunctionTableFunction.TableFunction.PrimaryKeyIndex.SortOrder);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0002CA11 File Offset: 0x0002AC11
		public AttachmentViewTable(Context context, Mailbox mailbox, Message parentMessage, List<Column> columns, SortOrder sortOrder, SearchCriteria criteria) : this(context, mailbox, parentMessage)
		{
			base.SetColumns(context, columns);
			this.SortTable(sortOrder);
			this.Restrict(context, criteria);
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0002CA36 File Offset: 0x0002AC36
		protected override Index LogicalKeyIndex
		{
			get
			{
				return this.inScopePseudoIndexes[0].IndexTable.PrimaryKeyIndex;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0002CA4E File Offset: 0x0002AC4E
		protected override bool MustUseLazyIndex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0002CA51 File Offset: 0x0002AC51
		protected internal override IList<IIndex> GetInScopePseudoIndexes(Context context, SearchCriteria findRowCriteria, out IList<IIndex> masterIndexes)
		{
			masterIndexes = null;
			this.UpdatePseudoIndexIfNeeded(context);
			return this.inScopePseudoIndexes;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0002CA63 File Offset: 0x0002AC63
		protected override void BringIndexesToCurrent(Context context, IList<IIndex> indexList, DataAccessOperator queryPlan)
		{
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0002CA68 File Offset: 0x0002AC68
		private void UpdatePseudoIndexIfNeeded(Context context)
		{
			if (this.parentMessage.IsDead)
			{
				return;
			}
			int subobjectsChangeCookie = this.parentMessage.SubobjectsChangeCookie;
			if (this.currentSubobjectsChangeCookie == subobjectsChangeCookie)
			{
				return;
			}
			AttachmentTable attachmentTable = DatabaseSchema.AttachmentTable(context.Database);
			AttachmentTableFunctionTableFunction attachmentTableFunctionTableFunction = DatabaseSchema.AttachmentTableFunctionTableFunction(context.Database);
			byte[] attachmentsBlob = this.parentMessage.GetAttachmentsBlob();
			Dictionary<Column, Column> dictionary = new Dictionary<Column, Column>(3);
			dictionary.Add(attachmentTable.MailboxPartitionNumber, Factory.CreateConstantColumn(this.parentMessage.Mailbox.MailboxPartitionNumber, attachmentTable.MailboxPartitionNumber));
			dictionary.Add(attachmentTable.Inid, attachmentTableFunctionTableFunction.Inid);
			if (UnifiedMailbox.IsReady(context, context.Database))
			{
				dictionary.Add(attachmentTable.MailboxNumber, Factory.CreateConstantColumn(this.parentMessage.Mailbox.MailboxNumber, attachmentTable.MailboxNumber));
			}
			SimplePseudoIndex item = new SimplePseudoIndex(attachmentTable.Table, attachmentTableFunctionTableFunction.TableFunction, new object[]
			{
				attachmentsBlob
			}, attachmentTableFunctionTableFunction.TableFunction.PrimaryKeyIndex.SortOrder, dictionary, null, true);
			this.inScopePseudoIndexes.Clear();
			this.inScopePseudoIndexes.Add(item);
			this.currentSubobjectsChangeCookie = subobjectsChangeCookie;
		}

		// Token: 0x04000223 RID: 547
		private readonly List<IIndex> inScopePseudoIndexes = new List<IIndex>(1);

		// Token: 0x04000224 RID: 548
		private readonly Message parentMessage;

		// Token: 0x04000225 RID: 549
		private int currentSubobjectsChangeCookie = -1;
	}
}
