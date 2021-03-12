using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x0200009D RID: 157
	internal class CachingStream : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600057C RID: 1404 RVA: 0x00016DA8 File Offset: 0x00014FA8
		public CachingStream(Stream parentStream, int maximum)
		{
			this.wrappedStream = parentStream;
			this.maximum = maximum;
			this.SetMemoryStreamModeIfPossible(parentStream.Length);
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x00016DD6 File Offset: 0x00014FD6
		public override bool CanRead
		{
			get
			{
				this.ThrowIfDisposed();
				return true;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00016DDF File Offset: 0x00014FDF
		public override bool CanWrite
		{
			get
			{
				this.ThrowIfDisposed();
				return this.wrappedStream != null && this.wrappedStream.CanWrite;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x00016DFC File Offset: 0x00014FFC
		public override bool CanSeek
		{
			get
			{
				this.ThrowIfDisposed();
				return true;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00016E05 File Offset: 0x00015005
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.memoryStream != null)
				{
					return this.memoryStream.Length;
				}
				return this.wrappedStream.Length;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x00016E2C File Offset: 0x0001502C
		public bool InMemory
		{
			get
			{
				this.ThrowIfDisposed();
				return this.memoryStream != null;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00016E40 File Offset: 0x00015040
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x00016E67 File Offset: 0x00015067
		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.memoryStream != null)
				{
					return this.memoryStream.Position;
				}
				return this.wrappedStream.Position;
			}
			set
			{
				this.ThrowIfDisposed();
				if (this.wrappedStream != null && value == this.wrappedStream.Position)
				{
					return;
				}
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00016E8F File Offset: 0x0001508F
		public DisposeTracker GetDisposeTracker()
		{
			this.ThrowIfDisposed();
			return DisposeTracker.Get<CachingStream>(this);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00016E9D File Offset: 0x0001509D
		public void SuppressDisposeTracker()
		{
			this.ThrowIfDisposed();
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00016EB8 File Offset: 0x000150B8
		public override void Flush()
		{
			this.ThrowIfDisposed();
			this.wrappedStream.Flush();
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00016ECB File Offset: 0x000150CB
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			if (this.memoryStream != null)
			{
				return this.memoryStream.Read(buffer, offset, count);
			}
			return this.wrappedStream.Read(buffer, offset, count);
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00016EF8 File Offset: 0x000150F8
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
			long result = -1L;
			if (this.wrappedStream != null)
			{
				result = this.wrappedStream.Seek(offset, origin);
			}
			if (this.memoryStream != null)
			{
				result = this.memoryStream.Seek(offset, origin);
			}
			return result;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00016F3C File Offset: 0x0001513C
		public override void SetLength(long value)
		{
			this.ThrowIfDisposed();
			if (this.Length != value)
			{
				this.SetMemoryStreamModeIfPossible(value);
				if (this.memoryStream != null)
				{
					if (value > (long)this.maximum)
					{
						this.memoryStream.Close();
						this.memoryStream = null;
					}
					else
					{
						this.memoryStream.SetLength(value);
					}
				}
				this.wrappedStream.SetLength(value);
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00016FA0 File Offset: 0x000151A0
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
			if (this.memoryStream != null)
			{
				if (this.memoryStream.Position + (long)count > (long)this.maximum)
				{
					this.memoryStream.Close();
					this.memoryStream = null;
				}
				else
				{
					this.memoryStream.Write(buffer, offset, count);
				}
			}
			if (this.wrappedStream != null)
			{
				this.wrappedStream.Write(buffer, offset, count);
				return;
			}
			throw new NotSupportedException("The stream does not support writing. ");
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00017015 File Offset: 0x00015215
		internal void ReleaseDatabase()
		{
			this.ThrowIfDisposed();
			if (this.wrappedStream != null)
			{
				this.wrappedStream.Close();
				this.wrappedStream = null;
			}
		}

		// Token: 0x0600058C RID: 1420 RVA: 0x00017038 File Offset: 0x00015238
		internal void ReOpenForRead()
		{
			this.ThrowIfDisposed();
			using (PooledBufferedStream pooledBufferedStream = this.wrappedStream as PooledBufferedStream)
			{
				if (pooledBufferedStream == null)
				{
					throw new InvalidOperationException(Strings.NotBufferedStream);
				}
				using (DataStreamImmediateWriter dataStreamImmediateWriter = pooledBufferedStream.FlushAndTakeWrappedStream() as DataStreamImmediateWriter)
				{
					if (dataStreamImmediateWriter == null)
					{
						throw new InvalidOperationException(Strings.NotOpenForWrite);
					}
					this.wrappedStream = new PooledBufferedStream(new DataStreamLazyReader(dataStreamImmediateWriter), CachingStream.pool);
				}
			}
		}

		// Token: 0x0600058D RID: 1421 RVA: 0x000170D4 File Offset: 0x000152D4
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.disposed = true;
					if (this.disposeTracker != null)
					{
						this.disposeTracker.Dispose();
						this.disposeTracker = null;
					}
					if (this.wrappedStream != null)
					{
						this.wrappedStream.Close();
						this.wrappedStream = null;
					}
					if (this.memoryStream != null)
					{
						this.memoryStream.Close();
						this.memoryStream = null;
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00017154 File Offset: 0x00015354
		private void SetMemoryStreamModeIfPossible(long length)
		{
			if (length == 0L && this.maximum > 0 && this.memoryStream == null)
			{
				this.memoryStream = new PooledMemoryStream(8192);
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0001717C File Offset: 0x0001537C
		private void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("CachingStream");
			}
		}

		// Token: 0x040002CF RID: 719
		private static BufferPool pool = new BufferPool(DataStream.BufferedStreamSize, true);

		// Token: 0x040002D0 RID: 720
		private Stream wrappedStream;

		// Token: 0x040002D1 RID: 721
		private Stream memoryStream;

		// Token: 0x040002D2 RID: 722
		private int maximum;

		// Token: 0x040002D3 RID: 723
		private DisposeTracker disposeTracker;

		// Token: 0x040002D4 RID: 724
		private bool disposed;
	}
}
