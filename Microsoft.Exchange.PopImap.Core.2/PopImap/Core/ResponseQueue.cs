using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000036 RID: 54
	internal class ResponseQueue : IDisposeTrackable, IDisposable
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x00010B3C File Offset: 0x0000ED3C
		public ResponseQueue() : this(100)
		{
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00010B46 File Offset: 0x0000ED46
		public ResponseQueue(int capacity) : this(capacity, true)
		{
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00010B50 File Offset: 0x0000ED50
		public ResponseQueue(int capacity, bool shouldBufferResponse)
		{
			this.items = new Queue<IResponseItem>((capacity > 0) ? capacity : 100);
			this.disposeTracker = this.GetDisposeTracker();
			this.disposed = false;
			this.shouldBufferResponse = shouldBufferResponse;
			this.isSending = false;
			this.inBufferMode = true;
			if (this.shouldBufferResponse)
			{
				this.pooledBufferResponseItem = new PooledBufferResponseItem(ResponseQueue.responseBufferCapacity);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00010BC2 File Offset: 0x0000EDC2
		public object SyncRoot
		{
			get
			{
				return this.syncRoot;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00010BCA File Offset: 0x0000EDCA
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x00010BD7 File Offset: 0x0000EDD7
		public bool IsSending
		{
			get
			{
				this.CheckDisposed();
				return this.isSending;
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00010BE5 File Offset: 0x0000EDE5
		public void Enqueue(IResponseItem item)
		{
			this.CheckDisposed();
			this.items.Enqueue(item);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00010BF9 File Offset: 0x0000EDF9
		public void DequeueForSend()
		{
			this.CheckDisposed();
			this.itemBeingSent = this.items.Dequeue();
			this.isSending = true;
			this.inBufferMode = true;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00010C20 File Offset: 0x0000EE20
		public void Clear()
		{
			this.CheckDisposed();
			this.items.Clear();
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00010C34 File Offset: 0x0000EE34
		public int GetNextChunk(BaseSession session, out byte[] buffer, out int offset)
		{
			this.CheckDisposed();
			buffer = null;
			offset = 0;
			int num = 0;
			bool flag = false;
			try
			{
				if (this.shouldBufferResponse)
				{
					num = this.InternalGetNextChunk(session, out buffer, out offset);
					while (num > 0 && this.pooledBufferResponseItem.Size - num < ResponseQueue.responseBufferCapacity)
					{
						this.pooledBufferResponseItem.Write(buffer, offset, num);
						num = this.InternalGetNextChunk(session, out buffer, out offset);
					}
					if (session.Disposed)
					{
						flag = true;
						return 0;
					}
					if (num > 0)
					{
						this.pooledBufferResponseItem.Write(buffer, offset, num);
					}
					if (this.pooledBufferResponseItem.Size > 0)
					{
						num = this.pooledBufferResponseItem.GetNextChunk(session, out buffer, out offset);
						flag = true;
						return num;
					}
				}
				num = this.InternalGetNextChunk(session, out buffer, out offset);
				if (num == 0)
				{
					if (!this.inBufferMode)
					{
						num = this.InternalGetNextChunk(session, out buffer, out offset);
						if (num != 0)
						{
							flag = true;
							return num;
						}
					}
					this.isSending = false;
				}
			}
			finally
			{
				if (!flag)
				{
					this.isSending = false;
				}
			}
			return num;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00010D30 File Offset: 0x0000EF30
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ResponseQueue>(this);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00010D38 File Offset: 0x0000EF38
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x00010D4D File Offset: 0x0000EF4D
		public void Dispose()
		{
			this.Dispose(true);
			this.disposed = true;
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00010D64 File Offset: 0x0000EF64
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			this.DisposeItemsQueued();
			this.DisposeItemBeingProcessed();
			if (this.pooledBufferResponseItem != null)
			{
				this.pooledBufferResponseItem.Dispose();
				this.pooledBufferResponseItem = null;
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00010DB4 File Offset: 0x0000EFB4
		private int InternalGetNextChunk(BaseSession session, out byte[] buffer, out int offset)
		{
			buffer = null;
			offset = 0;
			if (this.itemBeingSent == null)
			{
				if (this.items.Count <= 0)
				{
					return 0;
				}
				this.DequeueForSend();
			}
			if (this.inBufferMode && (this.itemBeingSent is EndResponseItem || this.itemBeingSent.SendCompleteDelegate != null))
			{
				this.inBufferMode = false;
				return 0;
			}
			int nextChunk = this.itemBeingSent.GetNextChunk(session, out buffer, out offset);
			if (session.Disposed)
			{
				return 0;
			}
			while (nextChunk == 0)
			{
				if (this.itemBeingSent.SendCompleteDelegate != null)
				{
					this.itemBeingSent.SendCompleteDelegate();
				}
				if (session.Disposed)
				{
					return 0;
				}
				this.DisposeItemBeingProcessed();
				if (this.items.Count == 0)
				{
					this.itemBeingSent = null;
					return 0;
				}
				this.DequeueForSend();
				if (this.itemBeingSent == null)
				{
					return 0;
				}
				nextChunk = this.itemBeingSent.GetNextChunk(session, out buffer, out offset);
			}
			return nextChunk;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00010E91 File Offset: 0x0000F091
		private void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00010EAC File Offset: 0x0000F0AC
		private void DisposeItemsQueued()
		{
			foreach (object obj in this.items)
			{
				IDisposable disposable = obj as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			this.items.Clear();
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x00010F14 File Offset: 0x0000F114
		private void DisposeItemBeingProcessed()
		{
			if (this.itemBeingSent != null)
			{
				IDisposable disposable = this.itemBeingSent as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				this.itemBeingSent = null;
			}
		}

		// Token: 0x040001EC RID: 492
		private const int DefaultCapacity = 100;

		// Token: 0x040001ED RID: 493
		private static int responseBufferCapacity = 8192;

		// Token: 0x040001EE RID: 494
		private object syncRoot = new object();

		// Token: 0x040001EF RID: 495
		private Queue<IResponseItem> items;

		// Token: 0x040001F0 RID: 496
		private IResponseItem itemBeingSent;

		// Token: 0x040001F1 RID: 497
		private DisposeTracker disposeTracker;

		// Token: 0x040001F2 RID: 498
		private bool disposed;

		// Token: 0x040001F3 RID: 499
		private bool shouldBufferResponse;

		// Token: 0x040001F4 RID: 500
		private PooledBufferResponseItem pooledBufferResponseItem;

		// Token: 0x040001F5 RID: 501
		private bool isSending;

		// Token: 0x040001F6 RID: 502
		private bool inBufferMode;
	}
}
