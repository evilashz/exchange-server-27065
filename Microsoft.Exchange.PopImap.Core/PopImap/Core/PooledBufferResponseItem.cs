using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000024 RID: 36
	internal class PooledBufferResponseItem : IResponseItem, IDisposeTrackable, IDisposable
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x0000724E File Offset: 0x0000544E
		public PooledBufferResponseItem(int bufferSize)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.pooledMemoryStream = new PooledMemoryStream(bufferSize);
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000726E File Offset: 0x0000546E
		public BaseSession.SendCompleteDelegate SendCompleteDelegate
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00007271 File Offset: 0x00005471
		public int Size
		{
			get
			{
				return (int)this.pooledMemoryStream.Position;
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00007280 File Offset: 0x00005480
		public virtual int GetNextChunk(BaseSession session, out byte[] buffer, out int offset)
		{
			buffer = this.pooledMemoryStream.GetBuffer();
			offset = 0;
			int result = (int)this.pooledMemoryStream.Position;
			this.pooledMemoryStream.Position = 0L;
			return result;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000072B8 File Offset: 0x000054B8
		public void Write(byte[] buffer, int offset, int count)
		{
			this.pooledMemoryStream.Write(buffer, offset, count);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000072C8 File Offset: 0x000054C8
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<PooledBufferResponseItem>(this);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000072D0 File Offset: 0x000054D0
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
				this.disposeTracker = null;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000072EC File Offset: 0x000054EC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000072FB File Offset: 0x000054FB
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			if (this.pooledMemoryStream != null)
			{
				this.pooledMemoryStream.Dispose();
				this.pooledMemoryStream = null;
			}
		}

		// Token: 0x04000113 RID: 275
		private PooledMemoryStream pooledMemoryStream;

		// Token: 0x04000114 RID: 276
		private DisposeTracker disposeTracker;
	}
}
