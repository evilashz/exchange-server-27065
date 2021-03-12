using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E61 RID: 3681
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxSyncItem : ISyncItem, IDisposeTrackable, IDisposable
	{
		// Token: 0x06007F7F RID: 32639 RVA: 0x0022E72C File Offset: 0x0022C92C
		protected MailboxSyncItem(Item item)
		{
			StorageGlobals.TraceConstructIDisposable(this);
			this.disposeTracker = this.GetDisposeTracker();
			this.item = item;
		}

		// Token: 0x170021FF RID: 8703
		// (get) Token: 0x06007F80 RID: 32640 RVA: 0x0022E74D File Offset: 0x0022C94D
		public virtual ISyncItemId Id
		{
			get
			{
				this.CheckDisposed("get_Id");
				if (this.id == null)
				{
					this.id = MailboxSyncItemId.CreateForNewItem(this.item.Id.ObjectId);
				}
				return this.id;
			}
		}

		// Token: 0x17002200 RID: 8704
		// (get) Token: 0x06007F81 RID: 32641 RVA: 0x0022E783 File Offset: 0x0022C983
		// (set) Token: 0x06007F82 RID: 32642 RVA: 0x0022E796 File Offset: 0x0022C996
		public object NativeItem
		{
			get
			{
				this.CheckDisposed("get_NativeItem");
				return this.item;
			}
			protected set
			{
				this.CheckDisposed("set_NativeItem");
				if (!object.ReferenceEquals(this.item, value))
				{
					if (this.item != null)
					{
						this.item.Dispose();
					}
					this.item = (Item)value;
				}
			}
		}

		// Token: 0x17002201 RID: 8705
		// (get) Token: 0x06007F83 RID: 32643 RVA: 0x0022E7D0 File Offset: 0x0022C9D0
		public ISyncWatermark Watermark
		{
			get
			{
				this.CheckDisposed("get_Watermark");
				if (this.watermark == null)
				{
					this.watermark = MailboxSyncWatermark.CreateForSingleItem();
				}
				this.watermark.UpdateWithChangeNumber((int)this.item.TryGetProperty(InternalSchema.ArticleId), (bool)this.item.TryGetProperty(InternalSchema.IsRead));
				return this.watermark;
			}
		}

		// Token: 0x06007F84 RID: 32644 RVA: 0x0022E836 File Offset: 0x0022CA36
		public static MailboxSyncItem Bind(Item item)
		{
			return new MailboxSyncItem(item);
		}

		// Token: 0x06007F85 RID: 32645 RVA: 0x0022E83E File Offset: 0x0022CA3E
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxSyncItem>(this);
		}

		// Token: 0x06007F86 RID: 32646 RVA: 0x0022E846 File Offset: 0x0022CA46
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06007F87 RID: 32647 RVA: 0x0022E85B File Offset: 0x0022CA5B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06007F88 RID: 32648 RVA: 0x0022E86A File Offset: 0x0022CA6A
		public void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.disposed, disposing);
			if (!this.disposed)
			{
				this.disposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x06007F89 RID: 32649 RVA: 0x0022E88F File Offset: 0x0022CA8F
		public bool IsItemInFilter(QueryFilter filter)
		{
			this.CheckDisposed("IsItemInFilter");
			return EvaluatableFilter.Evaluate(filter, this.item);
		}

		// Token: 0x06007F8A RID: 32650 RVA: 0x0022E8A8 File Offset: 0x0022CAA8
		public void Load()
		{
			this.CheckDisposed("Load");
			this.item.Load(new PropertyDefinition[]
			{
				InternalSchema.ArticleId
			});
		}

		// Token: 0x06007F8B RID: 32651 RVA: 0x0022E8DB File Offset: 0x0022CADB
		public void Save()
		{
			this.CheckDisposed("Save");
			this.item.Save(SaveMode.NoConflictResolution);
		}

		// Token: 0x06007F8C RID: 32652 RVA: 0x0022E8F5 File Offset: 0x0022CAF5
		protected virtual void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.item != null)
				{
					this.item.Dispose();
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
			this.item = null;
		}

		// Token: 0x06007F8D RID: 32653 RVA: 0x0022E927 File Offset: 0x0022CB27
		protected void CheckDisposed(string methodName)
		{
			if (this.disposed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x04005645 RID: 22085
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04005646 RID: 22086
		private MailboxSyncItemId id;

		// Token: 0x04005647 RID: 22087
		private bool disposed;

		// Token: 0x04005648 RID: 22088
		private Item item;

		// Token: 0x04005649 RID: 22089
		private MailboxSyncWatermark watermark;
	}
}
