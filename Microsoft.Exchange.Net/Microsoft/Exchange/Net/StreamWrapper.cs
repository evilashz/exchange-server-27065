using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Net
{
	// Token: 0x0200000C RID: 12
	internal class StreamWrapper : StreamBase
	{
		// Token: 0x06000027 RID: 39 RVA: 0x00002598 File Offset: 0x00000798
		internal static StreamBase.Capabilities GetStreamCapabilities(Stream stream)
		{
			if (stream == null)
			{
				return StreamBase.Capabilities.None;
			}
			return (stream.CanRead ? StreamBase.Capabilities.Readable : StreamBase.Capabilities.None) | (stream.CanWrite ? StreamBase.Capabilities.Writable : StreamBase.Capabilities.None) | (stream.CanSeek ? StreamBase.Capabilities.Seekable : StreamBase.Capabilities.None);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025C5 File Offset: 0x000007C5
		public StreamWrapper(Stream wrappedStream) : base(StreamWrapper.GetStreamCapabilities(wrappedStream))
		{
			this.internalStream = wrappedStream;
			this.canDisposeInternalStream = true;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025E1 File Offset: 0x000007E1
		public StreamWrapper(Stream wrappedStream, bool canDispose) : base(StreamWrapper.GetStreamCapabilities(wrappedStream))
		{
			this.internalStream = wrappedStream;
			this.canDisposeInternalStream = canDispose;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000025FD File Offset: 0x000007FD
		public StreamWrapper(Stream wrappedStream, bool canDispose, StreamBase.Capabilities capabilities) : base(capabilities)
		{
			this.internalStream = wrappedStream;
			this.canDisposeInternalStream = canDispose;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002614 File Offset: 0x00000814
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && !base.IsClosed && this.canDisposeInternalStream && this.internalStream != null)
				{
					this.internalStream.Dispose();
					this.internalStream = null;
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002668 File Offset: 0x00000868
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<StreamWrapper>(this);
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002670 File Offset: 0x00000870
		public Stream InternalStream
		{
			get
			{
				base.CheckDisposed("InternalStream:get");
				return this.internalStream;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002683 File Offset: 0x00000883
		public override bool CanRead
		{
			get
			{
				base.CheckDisposed("CanRead");
				return base.CanRead && (this.internalStream == null || this.internalStream.CanRead);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000026AF File Offset: 0x000008AF
		public override bool CanWrite
		{
			get
			{
				base.CheckDisposed("CanWrite");
				return base.CanWrite && (this.internalStream == null || this.internalStream.CanWrite);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000026DB File Offset: 0x000008DB
		public override bool CanSeek
		{
			get
			{
				base.CheckDisposed("CanSeek");
				return base.CanSeek && (this.internalStream == null || this.internalStream.CanSeek);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002707 File Offset: 0x00000907
		public override long Length
		{
			get
			{
				base.CheckDisposed("Length:get");
				if (this.internalStream == null)
				{
					throw new NotSupportedException();
				}
				return this.internalStream.Length;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000272D File Offset: 0x0000092D
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000275B File Offset: 0x0000095B
		public override long Position
		{
			get
			{
				base.CheckDisposed("Position:get");
				if (!base.CanSeek || this.internalStream == null)
				{
					throw new NotSupportedException();
				}
				return this.internalStream.Position;
			}
			set
			{
				base.CheckDisposed("Position:set");
				if (!base.CanSeek || this.internalStream == null)
				{
					throw new NotSupportedException();
				}
				this.internalStream.Position = value;
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000278A File Offset: 0x0000098A
		public override void Write(byte[] buffer, int offset, int count)
		{
			base.CheckDisposed("Write");
			if (!base.CanWrite || this.internalStream == null)
			{
				throw new NotSupportedException();
			}
			this.internalStream.Write(buffer, offset, count);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027BB File Offset: 0x000009BB
		public override int Read(byte[] buffer, int offset, int count)
		{
			base.CheckDisposed("Read");
			if (!base.CanRead || this.internalStream == null)
			{
				throw new NotSupportedException();
			}
			return this.internalStream.Read(buffer, offset, count);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000027EC File Offset: 0x000009EC
		public override void SetLength(long value)
		{
			base.CheckDisposed("SetLength");
			if (!base.CanWrite || !base.CanSeek || this.internalStream == null)
			{
				throw new NotSupportedException();
			}
			this.internalStream.SetLength(value);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002823 File Offset: 0x00000A23
		public override long Seek(long offset, SeekOrigin origin)
		{
			base.CheckDisposed("Seek");
			if (!base.CanSeek || this.internalStream == null)
			{
				throw new NotSupportedException();
			}
			EnumValidator.ThrowIfInvalid<SeekOrigin>(origin, "origin");
			return this.internalStream.Seek(offset, origin);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000285E File Offset: 0x00000A5E
		public override void Flush()
		{
			base.CheckDisposed("Flush");
			if (this.internalStream == null)
			{
				throw new NotSupportedException();
			}
			this.internalStream.Flush();
		}

		// Token: 0x04000015 RID: 21
		private readonly bool canDisposeInternalStream;

		// Token: 0x04000016 RID: 22
		private Stream internalStream;
	}
}
