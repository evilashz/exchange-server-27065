using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200001B RID: 27
	public class Attachment : Item, ISubobject
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x0002C060 File Offset: 0x0002A260
		private Attachment(Context context, AttachmentTable attachmentTable, Message parent, object inid) : base(context, attachmentTable.Table, attachmentTable.Size, parent.Mailbox, false, inid == null, inid != null, !ConfigurationSchema.AttachmentMessageSaveChunking.Value, (inid == null) ? new ColumnValue[]
		{
			new ColumnValue(attachmentTable.MailboxPartitionNumber, parent.Mailbox.MailboxPartitionNumber)
		} : new ColumnValue[]
		{
			new ColumnValue(attachmentTable.MailboxPartitionNumber, parent.Mailbox.MailboxPartitionNumber),
			new ColumnValue(attachmentTable.Inid, inid)
		})
		{
			this.attachmentTable = attachmentTable;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0002C128 File Offset: 0x0002A328
		private Attachment(Context context, Message parent, int attachNum, long? inid, long? copyOriginalInid) : this(context, DatabaseSchema.AttachmentTable(parent.Mailbox.Database), parent, inid)
		{
			this.parentMessage = parent;
			this.attachmentNumber = attachNum;
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
				PropertySchemaPopulation.InitializeAttachment(context, this);
				base.SetColumn(context, this.attachmentTable.MailboxPartitionNumber, parent.Mailbox.MailboxPartitionNumber);
				if (parent.Mailbox.SharedState.UnifiedState != null)
				{
					int num = (int)parent.GetPropertyValue(context, PropTag.Message.MailboxNum);
					if (num != parent.Mailbox.MailboxNumber)
					{
						throw new StoreException((LID)59932U, ErrorCodeValue.NotSupported);
					}
					if (num != parent.Mailbox.MailboxPartitionNumber)
					{
						base.SetColumn(context, this.attachmentTable.MailboxNumber, num);
					}
				}
				DateTime utcNow = parent.Mailbox.UtcNow;
				base.SetColumn(context, this.attachmentTable.CreationTime, utcNow);
				base.SetColumn(context, this.attachmentTable.LastModificationTime, utcNow);
				base.SetColumn(context, this.attachmentTable.RenderingPosition, -1);
				base.SetColumn(context, this.attachmentTable.AttachmentMethod, 0);
				base.SetColumn(context, this.attachmentTable.MailFlags, 0);
			}
			if (context.PerfInstance != null)
			{
				context.PerfInstance.SubobjectsOpenedRate.Increment();
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0002C2F7 File Offset: 0x0002A4F7
		public Message ParentMessage
		{
			get
			{
				return this.parentMessage;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0002C2FF File Offset: 0x0002A4FF
		public long? CurrentInid
		{
			get
			{
				return this.currentInid;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0002C307 File Offset: 0x0002A507
		public long? OriginalInid
		{
			get
			{
				return this.originalInid;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0002C30F File Offset: 0x0002A50F
		public int AttachmentNumber
		{
			get
			{
				return this.attachmentNumber;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0002C317 File Offset: 0x0002A517
		int ISubobject.ChildNumber
		{
			get
			{
				return this.attachmentNumber;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0002C31F File Offset: 0x0002A51F
		protected override StorePropTag ItemSubobjectsBinPropTag
		{
			get
			{
				return PropTag.Attachment.ItemSubobjectsBin;
			}
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0002C328 File Offset: 0x0002A528
		internal static Attachment CopyAttachment(Context context, Message parent, int attachNum, long sourceInid)
		{
			long value = ObjectCopyOnWrite.CopyAttachment(context, parent.Mailbox, sourceInid);
			Attachment attachment = new Attachment(context, parent, attachNum, new long?(value), new long?(sourceInid));
			attachment.DeepCopySubobjects(context);
			return attachment;
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0002C360 File Offset: 0x0002A560
		internal static Attachment CreateAttachment(Context context, Message parent, int attachNum)
		{
			return new Attachment(context, parent, attachNum, null, null);
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0002C388 File Offset: 0x0002A588
		internal static Attachment OpenAttachment(Context context, Message parent, int attachNum, long inid)
		{
			return new Attachment(context, parent, attachNum, new long?(inid), null);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0002C3AC File Offset: 0x0002A5AC
		private long? GetInid(Context context)
		{
			return (long?)base.GetColumnValue(context, this.attachmentTable.Inid);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0002C3C8 File Offset: 0x0002A5C8
		public EmbeddedMessage OpenEmbeddedMessage(Context context)
		{
			if (base.Subobjects == null || !base.Subobjects.ContainsChild(0))
			{
				return null;
			}
			return (EmbeddedMessage)this.OpenChild(context, 0, base.Subobjects.GetChildInid(0).Value);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0002C40E File Offset: 0x0002A60E
		public EmbeddedMessage CreateEmbeddedMessage(Context context)
		{
			if (base.Subobjects == null)
			{
				base.ReserveChildNumber();
			}
			return EmbeddedMessage.CreateEmbeddedMessage(context, this);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0002C428 File Offset: 0x0002A628
		protected override Item OpenChild(Context context, int childNumber, long inid)
		{
			Item result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				EmbeddedMessage embeddedMessage = disposeGuard.Add<EmbeddedMessage>(EmbeddedMessage.OpenEmbeddedMessage(context, this, inid));
				if (base.Subobjects.GetChildSize(0) == -1L)
				{
					base.Subobjects.SetChildSize(0, embeddedMessage.OriginalSize);
				}
				disposeGuard.Success();
				result = embeddedMessage;
			}
			return result;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0002C49C File Offset: 0x0002A69C
		protected override Item CopyChild(Context context, int childNumber, long inid)
		{
			EmbeddedMessage embeddedMessage = EmbeddedMessage.CopyEmbeddedMessage(context, this, inid);
			if (base.Subobjects.GetChildSize(childNumber) == -1L)
			{
				base.Subobjects.SetChildSize(childNumber, embeddedMessage.OriginalSize);
			}
			return embeddedMessage;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0002C4D8 File Offset: 0x0002A6D8
		public override bool SaveChanges(Context context)
		{
			if (this.IsDirty || this.originalInid == null || this.originalInid != this.currentInid)
			{
				if (!this.DataRow.ColumnDirty(this.attachmentTable.LastModificationTime) || base.GetColumnValue(context, this.attachmentTable.LastModificationTime) == null)
				{
					base.SetColumn(context, this.attachmentTable.LastModificationTime, base.Mailbox.UtcNow);
				}
				if (this.originalInid == null)
				{
					ExchangeId nextObjectId = base.Mailbox.GetNextObjectId(context);
					base.SetColumn(context, this.attachmentTable.AttachmentId, nextObjectId.To26ByteArray());
				}
				base.SaveChanges(context);
				this.parentMessage.SaveChild(context, this);
				this.originalInid = this.currentInid;
				if (base.Subobjects != null)
				{
					base.Subobjects.ClearDeleted(context);
				}
			}
			return true;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0002C5EC File Offset: 0x0002A7EC
		public override void Delete(Context context)
		{
			if (base.IsDead)
			{
				return;
			}
			if (this.originalInid != null)
			{
				this.parentMessage.DeleteChild(context, this);
			}
			base.ReleaseDescendants(context, false);
			if (this.currentInid != null)
			{
				base.SubobjectReferenceState.Release(context, this.currentInid.Value, base.Mailbox);
				this.currentInid = null;
			}
			base.MarkAsDeleted(context);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0002C661 File Offset: 0x0002A861
		public bool IsEmbeddedMessage(Context context)
		{
			return base.Subobjects != null && base.Subobjects.ContainsChild(0);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0002C67C File Offset: 0x0002A87C
		protected override void CopyOnWrite(Context context)
		{
			if (this.originalInid != null && this.originalInid == this.currentInid)
			{
				this.currentInid = new long?(ObjectCopyOnWrite.CopyAttachment(context, base.Mailbox, this.originalInid.Value));
				base.SubobjectReferenceState.Addref(this.currentInid.Value);
				base.SubobjectReferenceState.Release(context, this.originalInid.Value, base.Mailbox);
				SubobjectCleanup.AddTombstone(context, this, this.currentInid.Value, base.CurrentSizeEstimateWithoutChildren);
				base.SetPrimaryKey(new ColumnValue[]
				{
					new ColumnValue(this.attachmentTable.MailboxPartitionNumber, base.Mailbox.MailboxPartitionNumber),
					new ColumnValue(this.attachmentTable.Inid, this.currentInid.Value)
				});
				if (context.PerfInstance != null)
				{
					context.PerfInstance.SubobjectsCreatedRate.Increment();
				}
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0002C7BC File Offset: 0x0002A9BC
		public override void Flush(Context context, bool flushLargeDirtyStreams)
		{
			base.Flush(context, flushLargeDirtyStreams);
			if (this.currentInid == null)
			{
				this.currentInid = this.GetInid(context);
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail((this.currentInid.Value & 140737488355328L) == 0L, "Negative Inid - unexpected. This assumtion comes from SubobjectCleanup logic");
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

		// Token: 0x06000480 RID: 1152 RVA: 0x0002C886 File Offset: 0x0002AA86
		public ExchangeId GetId(Context context)
		{
			return ExchangeId.CreateFrom26ByteArray(context, base.Mailbox.ReplidGuidMap, (byte[])base.GetColumnValue(context, this.attachmentTable.AttachmentId));
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0002C8B0 File Offset: 0x0002AAB0
		public DateTime GetCreationTime(Context context)
		{
			return (DateTime)this.GetPropertyValue(context, PropTag.Attachment.CreationTime);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0002C8C3 File Offset: 0x0002AAC3
		public DateTime GetLastModificationTime(Context context)
		{
			return (DateTime)this.GetPropertyValue(context, PropTag.Attachment.LastModificationTime);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0002C8D6 File Offset: 0x0002AAD6
		internal void SetLastModificationTime(Context context, DateTime value)
		{
			this.SetProperty(context, PropTag.Attachment.LastModificationTime, value);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0002C8EB File Offset: 0x0002AAEB
		public object GetContent(Context context)
		{
			return this.GetPropertyValue(context, PropTag.Attachment.Content);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0002C8F9 File Offset: 0x0002AAF9
		public void SetContent(Context context, byte[] value)
		{
			this.SetProperty(context, PropTag.Attachment.Content, value);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0002C909 File Offset: 0x0002AB09
		protected override ObjectType GetObjectType()
		{
			return ObjectType.Attachment;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0002C90C File Offset: 0x0002AB0C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<Attachment>(this);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0002C914 File Offset: 0x0002AB14
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (this.currentInid != null)
			{
				base.SubobjectReferenceState.Release(calledFromDispose ? base.Mailbox.CurrentOperationContext : null, this.currentInid.Value, calledFromDispose ? base.Mailbox : null);
				this.currentInid = null;
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x0400021E RID: 542
		private Message parentMessage;

		// Token: 0x0400021F RID: 543
		private int attachmentNumber;

		// Token: 0x04000220 RID: 544
		private long? currentInid;

		// Token: 0x04000221 RID: 545
		private long? originalInid;

		// Token: 0x04000222 RID: 546
		private AttachmentTable attachmentTable;
	}
}
