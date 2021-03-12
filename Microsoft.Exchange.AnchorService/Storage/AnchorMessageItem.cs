using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService.Storage
{
	// Token: 0x02000036 RID: 54
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AnchorMessageItem : AnchorStoreObject, IAnchorMessageItem, IAnchorStoreObject, IDisposable, IPropertyBag, IReadOnlyPropertyBag, IAnchorAttachmentMessage
	{
		// Token: 0x0600024E RID: 590 RVA: 0x00008DDC File Offset: 0x00006FDC
		internal AnchorMessageItem(AnchorContext context, MessageItem message)
		{
			base.AnchorContext = context;
			this.Message = message;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00008DF2 File Offset: 0x00006FF2
		internal AnchorMessageItem(AnchorContext context, MailboxSession mailboxSession, StoreObjectId id)
		{
			base.AnchorContext = context;
			this.Initialize(mailboxSession, id, AnchorStoreObject.IdPropertyDefinition);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008E0E File Offset: 0x0000700E
		internal AnchorMessageItem(MailboxSession mailboxSession, StoreObjectId id, PropertyDefinition[] propertyDefinitions)
		{
			this.Initialize(mailboxSession, id, propertyDefinitions);
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00008E1F File Offset: 0x0000701F
		public override string Name
		{
			get
			{
				return this.Message.ClassName;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000252 RID: 594 RVA: 0x00008E2C File Offset: 0x0000702C
		protected override StoreObject StoreObject
		{
			get
			{
				return this.Message;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00008E34 File Offset: 0x00007034
		// (set) Token: 0x06000254 RID: 596 RVA: 0x00008E3C File Offset: 0x0000703C
		private MessageItem Message { get; set; }

		// Token: 0x06000255 RID: 597 RVA: 0x00008E45 File Offset: 0x00007045
		public override void OpenAsReadWrite()
		{
			this.Message.OpenAsReadWrite();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00008E52 File Offset: 0x00007052
		public override void Save(SaveMode saveMode)
		{
			this.Message.Save(saveMode);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00008E61 File Offset: 0x00007061
		public AnchorAttachment CreateAttachment(string name)
		{
			base.CheckDisposed();
			return AnchorMessageHelper.CreateAttachment(base.AnchorContext, this.Message, name);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00008E7B File Offset: 0x0000707B
		public AnchorAttachment GetAttachment(string name, PropertyOpenMode openMode)
		{
			base.CheckDisposed();
			return AnchorMessageHelper.GetAttachment(base.AnchorContext, this.Message, name, openMode);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00008E96 File Offset: 0x00007096
		public void DeleteAttachment(string name)
		{
			base.CheckDisposed();
			AnchorMessageHelper.DeleteAttachment(this.Message, name);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00008EAA File Offset: 0x000070AA
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.Message != null)
			{
				this.Message.Dispose();
				this.Message = null;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00008EC9 File Offset: 0x000070C9
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AnchorMessageItem>(this);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00008ED4 File Offset: 0x000070D4
		private void Initialize(MailboxSession mailboxSession, StoreObjectId id, PropertyDefinition[] properties)
		{
			bool flag = false;
			try
			{
				AnchorUtil.ThrowOnNullArgument(mailboxSession, "dataProvider");
				AnchorUtil.ThrowOnNullArgument(id, "id");
				AnchorUtil.ThrowOnNullArgument(properties, "properties");
				this.Message = MessageItem.Bind(mailboxSession, id, properties);
				flag = true;
			}
			catch (ArgumentException ex)
			{
				base.AnchorContext.Logger.Log(MigrationEventType.Error, ex, "Encountered an argument exception when trying to find message with id={0}", new object[]
				{
					id.ToString()
				});
				throw new ObjectNotFoundException(ServerStrings.ExItemNotFound, ex);
			}
			finally
			{
				if (!flag)
				{
					this.Dispose();
				}
			}
		}
	}
}
