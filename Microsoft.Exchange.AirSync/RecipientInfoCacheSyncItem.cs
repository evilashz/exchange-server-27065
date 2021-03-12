using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000268 RID: 616
	internal class RecipientInfoCacheSyncItem : ISyncItem, IDisposeTrackable, IDisposable
	{
		// Token: 0x060016D9 RID: 5849 RVA: 0x0008993B File Offset: 0x00087B3B
		private RecipientInfoCacheSyncItem(RecipientInfoCacheEntry item)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.item = item;
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x060016DA RID: 5850 RVA: 0x00089956 File Offset: 0x00087B56
		public ISyncItemId Id
		{
			get
			{
				this.CheckDisposed("get_Id");
				if (this.id == null)
				{
					this.id = new RecipientInfoCacheSyncItemId(this.item.CacheEntryId);
				}
				return this.id;
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x060016DB RID: 5851 RVA: 0x00089987 File Offset: 0x00087B87
		public object NativeItem
		{
			get
			{
				this.CheckDisposed("get_NativeItem");
				return this.item;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x060016DC RID: 5852 RVA: 0x0008999C File Offset: 0x00087B9C
		public ISyncWatermark Watermark
		{
			get
			{
				this.CheckDisposed("get_Watermark");
				if (this.watermark == null)
				{
					Dictionary<RecipientInfoCacheSyncItemId, long> dictionary = new Dictionary<RecipientInfoCacheSyncItemId, long>(1);
					using (RecipientInfoCacheSyncItem recipientInfoCacheSyncItem = RecipientInfoCacheSyncItem.Bind(this.item))
					{
						dictionary[(RecipientInfoCacheSyncItemId)recipientInfoCacheSyncItem.Id] = this.item.DateTimeTicks;
						this.watermark = RecipientInfoCacheSyncWatermark.Create(dictionary, (ExDateTime)new DateTime(this.item.DateTimeTicks));
					}
				}
				return this.watermark;
			}
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x00089A30 File Offset: 0x00087C30
		public static RecipientInfoCacheSyncItem Bind(RecipientInfoCacheEntry item)
		{
			return new RecipientInfoCacheSyncItem(item);
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x00089A38 File Offset: 0x00087C38
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<RecipientInfoCacheSyncItem>(this);
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x00089A40 File Offset: 0x00087C40
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x00089A55 File Offset: 0x00087C55
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x00089A64 File Offset: 0x00087C64
		public void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.InternalDispose(disposing);
			}
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00089A7C File Offset: 0x00087C7C
		public bool IsItemInFilter(QueryFilter filter)
		{
			this.CheckDisposed("IsItemInFilter");
			return true;
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00089A8A File Offset: 0x00087C8A
		public void Load()
		{
			this.CheckDisposed("Load");
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00089A97 File Offset: 0x00087C97
		public void Save()
		{
			this.CheckDisposed("Save");
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00089AA4 File Offset: 0x00087CA4
		protected void InternalDispose(bool disposing)
		{
			if (disposing && this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.item = null;
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00089AC3 File Offset: 0x00087CC3
		private void CheckDisposed(string methodName)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x04000E15 RID: 3605
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000E16 RID: 3606
		private RecipientInfoCacheSyncItemId id;

		// Token: 0x04000E17 RID: 3607
		private RecipientInfoCacheEntry item;

		// Token: 0x04000E18 RID: 3608
		private bool disposed;

		// Token: 0x04000E19 RID: 3609
		private RecipientInfoCacheSyncWatermark watermark;
	}
}
