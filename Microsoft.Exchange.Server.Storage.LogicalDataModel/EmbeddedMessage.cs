using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200003D RID: 61
	public class EmbeddedMessage : Message, ISubobject
	{
		// Token: 0x06000605 RID: 1541 RVA: 0x000383B8 File Offset: 0x000365B8
		private EmbeddedMessage(Context context, AttachmentTable attachmentTable, Mailbox mailbox, object inid) : base(context, attachmentTable.Table, attachmentTable.Size, mailbox, false, inid == null, inid != null, !ConfigurationSchema.AttachmentMessageSaveChunking.Value, (inid == null) ? new ColumnValue[]
		{
			new ColumnValue(attachmentTable.MailboxPartitionNumber, mailbox.MailboxPartitionNumber)
		} : new ColumnValue[]
		{
			new ColumnValue(attachmentTable.MailboxPartitionNumber, mailbox.MailboxPartitionNumber),
			new ColumnValue(attachmentTable.Inid, inid)
		})
		{
			this.attachmentTable = attachmentTable;
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00038470 File Offset: 0x00036670
		private EmbeddedMessage(Context context, Attachment parent, long? inid, long? copyOriginalInid) : this(context, DatabaseSchema.AttachmentTable(parent.Mailbox.Database), parent.Mailbox, inid)
		{
			this.parentAttachment = parent;
			if (inid != null)
			{
				this.currentInid = inid;
				this.originalInid = ((copyOriginalInid != null) ? copyOriginalInid : inid);
				base.SubobjectReferenceState.Addref(this.currentInid.Value);
				if (copyOriginalInid != null)
				{
					SubobjectCleanup.AddTombstone(context, parent, this.currentInid.Value, 0L);
				}
			}
			else
			{
				base.SetColumn(context, this.attachmentTable.IsEmbeddedMessage, true);
				PropertySchemaPopulation.InitializeEmbeddedMessage(context, this);
				if (parent.Mailbox.SharedState.UnifiedState != null)
				{
					int num = (int)parent.GetPropertyValue(context, PropTag.Attachment.MailboxNum);
					if (num != parent.Mailbox.MailboxNumber)
					{
						throw new StoreException((LID)64220U, ErrorCodeValue.NotSupported);
					}
					if (num != parent.Mailbox.MailboxPartitionNumber)
					{
						base.SetColumn(context, this.attachmentTable.MailboxNumber, num);
					}
				}
			}
			if (context.PerfInstance != null)
			{
				context.PerfInstance.SubobjectsOpenedRate.Increment();
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x000385AC File Offset: 0x000367AC
		public AttachmentTable AttachmentTable
		{
			get
			{
				return this.attachmentTable;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x000385B4 File Offset: 0x000367B4
		public Attachment ParentAttachment
		{
			get
			{
				return this.parentAttachment;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x000385BC File Offset: 0x000367BC
		public override bool IsEmbedded
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x000385BF File Offset: 0x000367BF
		public long? CurrentInid
		{
			get
			{
				return this.currentInid;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x000385C7 File Offset: 0x000367C7
		public long? OriginalInid
		{
			get
			{
				return this.originalInid;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x000385CF File Offset: 0x000367CF
		int ISubobject.ChildNumber
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000385D4 File Offset: 0x000367D4
		internal static EmbeddedMessage CopyEmbeddedMessage(Context context, Attachment parent, long sourceInid)
		{
			long value = ObjectCopyOnWrite.CopyAttachment(context, parent.Mailbox, sourceInid);
			EmbeddedMessage embeddedMessage = new EmbeddedMessage(context, parent, new long?(value), new long?(sourceInid));
			embeddedMessage.DeepCopySubobjects(context);
			return embeddedMessage;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0003860C File Offset: 0x0003680C
		internal static EmbeddedMessage CreateEmbeddedMessage(Context context, Attachment parent)
		{
			return new EmbeddedMessage(context, parent, null, null);
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00038634 File Offset: 0x00036834
		internal static EmbeddedMessage OpenEmbeddedMessage(Context context, Attachment parent, long inid)
		{
			return new EmbeddedMessage(context, parent, new long?(inid), null);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00038657 File Offset: 0x00036857
		public long? GetInid(Context context)
		{
			return (long?)base.GetColumnValue(context, this.attachmentTable.Inid);
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00038670 File Offset: 0x00036870
		public override bool SaveChanges(Context context)
		{
			if (this.IsDirty || this.originalInid == null || this.originalInid != this.currentInid)
			{
				if (!this.DataRow.ColumnDirty(this.attachmentTable.LastModificationTime) || base.GetColumnValue(context, this.attachmentTable.LastModificationTime) == null)
				{
					base.SetColumn(context, this.attachmentTable.LastModificationTime, base.Mailbox.UtcNow);
				}
				base.SaveChanges(context);
				this.parentAttachment.SaveChild(context, this);
				this.originalInid = this.currentInid;
				if (base.Subobjects != null)
				{
					base.Subobjects.ClearDeleted(context);
				}
			}
			return true;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00038750 File Offset: 0x00036950
		public override void Delete(Context context)
		{
			if (base.IsDead)
			{
				return;
			}
			if (this.originalInid != null)
			{
				this.parentAttachment.DeleteChild(context, this);
			}
			base.ReleaseDescendants(context, false);
			if (this.currentInid != null)
			{
				base.SubobjectReferenceState.Release(context, this.currentInid.Value, base.Mailbox);
				this.currentInid = null;
			}
			base.MarkAsDeleted(context);
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x000387C5 File Offset: 0x000369C5
		public override void Scrub(Context context)
		{
			base.Scrub(context);
			base.SetColumn(context, this.attachmentTable.Content, null);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x000387E4 File Offset: 0x000369E4
		protected override IReadOnlyDictionary<Column, Column> GetColumnRenames(Context context)
		{
			MessageTable messageTable = DatabaseSchema.MessageTable(context.Database);
			Dictionary<Column, Column> dictionary = new Dictionary<Column, Column>(1);
			dictionary[messageTable.VirtualIsRead] = messageTable.IsRead;
			return dictionary;
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00038818 File Offset: 0x00036A18
		protected override void CopyOnWrite(Context context)
		{
			if (this.originalInid != null && this.originalInid == this.currentInid)
			{
				this.currentInid = new long?(ObjectCopyOnWrite.CopyAttachment(context, base.Mailbox, this.originalInid.Value));
				base.SubobjectReferenceState.Addref(this.currentInid.Value);
				base.SubobjectReferenceState.Release(context, this.originalInid.Value, base.Mailbox);
				SubobjectCleanup.AddTombstone(context, this, this.currentInid.Value, 0L);
				base.SetPrimaryKey(new ColumnValue[]
				{
					new ColumnValue(this.attachmentTable.MailboxPartitionNumber, base.Mailbox.MailboxPartitionNumber),
					new ColumnValue(this.attachmentTable.Inid, this.currentInid)
				});
				if (context.PerfInstance != null)
				{
					context.PerfInstance.SubobjectsCreatedRate.Increment();
				}
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x00038950 File Offset: 0x00036B50
		public override void Flush(Context context, bool flushLargeDirtyStreams)
		{
			base.Flush(context, flushLargeDirtyStreams);
			if (this.currentInid == null)
			{
				this.currentInid = this.GetInid(context);
				base.SubobjectReferenceState.Addref(this.currentInid.Value);
				SubobjectCleanup.AddTombstone(context, this, this.currentInid.Value, base.CurrentSizeEstimateWithoutChildren);
				if (context.PerfInstance != null)
				{
					context.PerfInstance.SubobjectsCreatedRate.Increment();
					return;
				}
			}
			else if (base.SubobjectReferenceState.IsInTombstone(this.currentInid.Value))
			{
				SubobjectCleanup.UpdateTombstonedSize(context, this, this.currentInid.Value, base.CurrentSizeEstimateWithoutChildren);
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000389F7 File Offset: 0x00036BF7
		protected override ObjectType GetObjectType()
		{
			return ObjectType.EmbeddedMessage;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x000389FA File Offset: 0x00036BFA
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<EmbeddedMessage>(this);
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x00038A04 File Offset: 0x00036C04
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (this.currentInid != null)
			{
				base.SubobjectReferenceState.Release(calledFromDispose ? base.Mailbox.CurrentOperationContext : null, this.currentInid.Value, calledFromDispose ? base.Mailbox : null);
				this.currentInid = null;
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x04000331 RID: 817
		private Attachment parentAttachment;

		// Token: 0x04000332 RID: 818
		private long? currentInid;

		// Token: 0x04000333 RID: 819
		private long? originalInid;

		// Token: 0x04000334 RID: 820
		private AttachmentTable attachmentTable;
	}
}
