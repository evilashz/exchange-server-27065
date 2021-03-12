using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000356 RID: 854
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ItemAttachment : Attachment, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag
	{
		// Token: 0x06002605 RID: 9733 RVA: 0x00098448 File Offset: 0x00096648
		internal ItemAttachment(CoreAttachment coreAttachment) : base(coreAttachment)
		{
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x00098451 File Offset: 0x00096651
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ItemAttachment>(this);
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x00098459 File Offset: 0x00096659
		public Item GetItem()
		{
			base.CheckDisposed("GetItem");
			return this.GetItem(base.CalculateOpenMode(), false, null);
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x00098474 File Offset: 0x00096674
		public Item GetItem(ICollection<PropertyDefinition> propsToLoad)
		{
			base.CheckDisposed("GetItem");
			return this.GetItem(base.CalculateOpenMode(), false, propsToLoad);
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x0009848F File Offset: 0x0009668F
		public MessageItem GetItemAsMessage()
		{
			return this.GetItemAsMessage(null);
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x00098498 File Offset: 0x00096698
		public MessageItem GetItemAsMessage(params PropertyDefinition[] propsToLoad)
		{
			return this.GetItemAsMessage((ICollection<PropertyDefinition>)propsToLoad);
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x000984A6 File Offset: 0x000966A6
		public MessageItem GetItemAsMessage(ICollection<PropertyDefinition> propsToLoad)
		{
			base.CheckDisposed("GetItemAsMessage");
			return (MessageItem)this.GetItem(base.CalculateOpenMode(), true, propsToLoad);
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x000984C6 File Offset: 0x000966C6
		public Item GetItemAsReadOnly(ICollection<PropertyDefinition> propsToLoad)
		{
			return this.GetItem(PropertyOpenMode.ReadOnly, false, propsToLoad);
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x000984D4 File Offset: 0x000966D4
		private Item GetItem(PropertyOpenMode openMode, bool bindAsMessage, ICollection<PropertyDefinition> propsToLoad)
		{
			base.CheckDisposed("GetItem");
			if (this.IsItemOpen)
			{
				throw new InvalidOperationException("Embedded item is already open, close/dispose it before calling GetItem");
			}
			ICoreItem coreItem = null;
			Item item = null;
			bool flag = false;
			try
			{
				bool noMessageDecoding = base.CoreAttachment.ParentCollection.ContainerItem.CharsetDetector.NoMessageDecoding;
				coreItem = base.CoreAttachment.PropertyBag.OpenAttachedItem(openMode, propsToLoad, noMessageDecoding);
				coreItem.TopLevelItem = (base.CoreAttachment.ParentCollection.ContainerItem.TopLevelItem ?? base.CoreAttachment.ParentCollection.ContainerItem);
				if (bindAsMessage)
				{
					item = new MessageItem(coreItem, false);
				}
				else
				{
					item = Microsoft.Exchange.Data.Storage.Item.InternalBindCoreItem(coreItem);
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					if (item != null)
					{
						item.Dispose();
					}
					if (coreItem != null)
					{
						coreItem.Dispose();
					}
				}
			}
			this.item = item;
			return item;
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x000985AC File Offset: 0x000967AC
		internal bool IsItemOpen
		{
			get
			{
				return this.item != null && !this.item.IsDisposed;
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x000985C6 File Offset: 0x000967C6
		protected override Schema Schema
		{
			get
			{
				return ItemAttachmentSchema.Instance;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06002610 RID: 9744 RVA: 0x000985CD File Offset: 0x000967CD
		public override AttachmentType AttachmentType
		{
			get
			{
				base.CheckDisposed("AttachmentType::get");
				return AttachmentType.EmbeddedMessage;
			}
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x000985DB File Offset: 0x000967DB
		protected override void OnBeforeSave()
		{
			base.OnBeforeSave();
			base.PropertyBag[InternalSchema.AttachMethod] = 5;
		}

		// Token: 0x040016DC RID: 5852
		internal const int AttachMethod = 5;

		// Token: 0x040016DD RID: 5853
		private Item item;
	}
}
