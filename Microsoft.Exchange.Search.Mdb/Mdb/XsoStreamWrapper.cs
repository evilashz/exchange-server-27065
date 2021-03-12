using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000031 RID: 49
	internal class XsoStreamWrapper : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600018D RID: 397 RVA: 0x0000C15F File Offset: 0x0000A35F
		public XsoStreamWrapper(Stream wrappedStream) : this(null, wrappedStream)
		{
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000C169 File Offset: 0x0000A369
		public XsoStreamWrapper(Item item, Stream wrappedStream)
		{
			this.item = item;
			this.internalStream = wrappedStream;
			this.canDisposeInternalStream = true;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000C192 File Offset: 0x0000A392
		public override bool CanRead
		{
			get
			{
				this.CheckDisposed();
				return this.internalStream != null && this.internalStream.CanRead;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000C1AF File Offset: 0x0000A3AF
		public override bool CanWrite
		{
			get
			{
				this.CheckDisposed();
				return this.internalStream != null && this.internalStream.CanWrite;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000C1CC File Offset: 0x0000A3CC
		public override bool CanSeek
		{
			get
			{
				this.CheckDisposed();
				return this.internalStream != null && this.internalStream.CanSeek;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000C1F6 File Offset: 0x0000A3F6
		public override long Length
		{
			get
			{
				this.CheckDisposed();
				if (this.internalStream == null)
				{
					throw new NotSupportedException();
				}
				return XsoStreamWrapper.MapXsoExceptions<long>(() => this.internalStream.Length);
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000C22A File Offset: 0x0000A42A
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000C280 File Offset: 0x0000A480
		public override long Position
		{
			get
			{
				this.CheckDisposed();
				if (this.internalStream == null || !this.internalStream.CanSeek)
				{
					throw new NotSupportedException();
				}
				return XsoStreamWrapper.MapXsoExceptions<long>(() => this.internalStream.Position);
			}
			set
			{
				this.CheckDisposed();
				if (this.internalStream == null || !this.internalStream.CanSeek)
				{
					throw new NotSupportedException();
				}
				XsoStreamWrapper.MapXsoExceptions(delegate()
				{
					this.internalStream.Position = value;
				});
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000C2D3 File Offset: 0x0000A4D3
		internal Stream InternalStream
		{
			get
			{
				this.CheckDisposed();
				return this.internalStream;
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		public override void Close()
		{
			try
			{
				if (!this.isClosed && this.canDisposeInternalStream && this.internalStream != null)
				{
					XsoStreamWrapper.MapXsoExceptions(new Action(this.internalStream.Close));
					this.internalStream = null;
				}
				this.isClosed = true;
			}
			finally
			{
				base.Close();
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000C348 File Offset: 0x0000A548
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<XsoStreamWrapper>(this);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000C350 File Offset: 0x0000A550
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000C394 File Offset: 0x0000A594
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			if (this.internalStream == null || !this.internalStream.CanWrite)
			{
				throw new NotSupportedException();
			}
			XsoStreamWrapper.MapXsoExceptions(delegate()
			{
				this.internalStream.Write(buffer, offset, count);
			});
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000C424 File Offset: 0x0000A624
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			if (this.internalStream == null || !this.internalStream.CanRead)
			{
				throw new NotSupportedException();
			}
			return XsoStreamWrapper.MapXsoExceptions<int>(() => this.internalStream.Read(buffer, offset, count));
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000C4A8 File Offset: 0x0000A6A8
		public override void SetLength(long value)
		{
			this.CheckDisposed();
			if (this.internalStream == null || !this.internalStream.CanWrite || this.internalStream.CanSeek)
			{
				throw new NotSupportedException();
			}
			XsoStreamWrapper.MapXsoExceptions(delegate()
			{
				this.internalStream.SetLength(value);
			});
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000C530 File Offset: 0x0000A730
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckDisposed();
			if (this.internalStream == null || !this.internalStream.CanSeek)
			{
				throw new NotSupportedException();
			}
			return XsoStreamWrapper.MapXsoExceptions<long>(() => this.internalStream.Seek(offset, origin));
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000C58A File Offset: 0x0000A78A
		public override void Flush()
		{
			this.CheckDisposed();
			if (this.internalStream == null)
			{
				throw new NotSupportedException();
			}
			XsoStreamWrapper.MapXsoExceptions(new Action(this.internalStream.Flush));
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000C5B8 File Offset: 0x0000A7B8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.item != null)
				{
					if (this.item.Session != null)
					{
						this.item.Session.Dispose();
					}
					this.item.Dispose();
					this.item = null;
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
					this.disposeTracker = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000C620 File Offset: 0x0000A820
		private static TResult MapXsoExceptions<TResult>(Func<TResult> call)
		{
			Exception innerException = null;
			try
			{
				return call();
			}
			catch (StorageTransientException ex)
			{
				innerException = ex;
			}
			catch (StoragePermanentException ex2)
			{
				innerException = ex2;
			}
			throw new OperationFailedException(innerException);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000C668 File Offset: 0x0000A868
		private static void MapXsoExceptions(Action call)
		{
			Exception ex = null;
			try
			{
				call();
			}
			catch (StorageTransientException ex2)
			{
				ex = ex2;
			}
			catch (StoragePermanentException ex3)
			{
				ex = ex3;
			}
			if (ex != null)
			{
				throw new OperationFailedException(ex);
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000C6B0 File Offset: 0x0000A8B0
		private void CheckDisposed()
		{
			if (this.isClosed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x0400010B RID: 267
		private readonly bool canDisposeInternalStream;

		// Token: 0x0400010C RID: 268
		private Stream internalStream;

		// Token: 0x0400010D RID: 269
		private Item item;

		// Token: 0x0400010E RID: 270
		private bool isClosed;

		// Token: 0x0400010F RID: 271
		private DisposeTracker disposeTracker;
	}
}
