using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Inference.Mdb
{
	// Token: 0x02000020 RID: 32
	internal class XsoStreamWrapper : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00004DAB File Offset: 0x00002FAB
		public XsoStreamWrapper(Stream wrappedStream) : this(null, wrappedStream)
		{
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004DB5 File Offset: 0x00002FB5
		public XsoStreamWrapper(Item item, Stream wrappedStream)
		{
			this.item = item;
			this.internalStream = wrappedStream;
			this.canDisposeInternalStream = true;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004DDE File Offset: 0x00002FDE
		public override bool CanRead
		{
			get
			{
				this.CheckDisposed();
				return this.internalStream != null && this.internalStream.CanRead;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00004DFB File Offset: 0x00002FFB
		public override bool CanWrite
		{
			get
			{
				this.CheckDisposed();
				return this.internalStream != null && this.internalStream.CanWrite;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004E18 File Offset: 0x00003018
		public override bool CanSeek
		{
			get
			{
				this.CheckDisposed();
				return this.internalStream != null && this.internalStream.CanSeek;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004E42 File Offset: 0x00003042
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

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00004E76 File Offset: 0x00003076
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00004ECC File Offset: 0x000030CC
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00004F1F File Offset: 0x0000311F
		internal Stream InternalStream
		{
			get
			{
				this.CheckDisposed();
				return this.internalStream;
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004F30 File Offset: 0x00003130
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

		// Token: 0x060000C1 RID: 193 RVA: 0x00004F94 File Offset: 0x00003194
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<XsoStreamWrapper>(this);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004F9C File Offset: 0x0000319C
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004FE0 File Offset: 0x000031E0
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

		// Token: 0x060000C4 RID: 196 RVA: 0x00005070 File Offset: 0x00003270
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed();
			if (this.internalStream == null || !this.internalStream.CanRead)
			{
				throw new NotSupportedException();
			}
			return XsoStreamWrapper.MapXsoExceptions<int>(() => this.internalStream.Read(buffer, offset, count));
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x000050F4 File Offset: 0x000032F4
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

		// Token: 0x060000C6 RID: 198 RVA: 0x0000517C File Offset: 0x0000337C
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckDisposed();
			if (this.internalStream == null || !this.internalStream.CanSeek)
			{
				throw new NotSupportedException();
			}
			return XsoStreamWrapper.MapXsoExceptions<long>(() => this.internalStream.Seek(offset, origin));
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000051D6 File Offset: 0x000033D6
		public override void Flush()
		{
			this.CheckDisposed();
			if (this.internalStream == null)
			{
				throw new NotSupportedException();
			}
			XsoStreamWrapper.MapXsoExceptions(new Action(this.internalStream.Flush));
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005204 File Offset: 0x00003404
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

		// Token: 0x060000C9 RID: 201 RVA: 0x0000526C File Offset: 0x0000346C
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

		// Token: 0x060000CA RID: 202 RVA: 0x000052B4 File Offset: 0x000034B4
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

		// Token: 0x060000CB RID: 203 RVA: 0x000052FC File Offset: 0x000034FC
		private void CheckDisposed()
		{
			if (this.isClosed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x0400004F RID: 79
		private readonly bool canDisposeInternalStream;

		// Token: 0x04000050 RID: 80
		private Stream internalStream;

		// Token: 0x04000051 RID: 81
		private Item item;

		// Token: 0x04000052 RID: 82
		private bool isClosed;

		// Token: 0x04000053 RID: 83
		private DisposeTracker disposeTracker;
	}
}
