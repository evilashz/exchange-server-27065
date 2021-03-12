using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Net
{
	// Token: 0x0200000A RID: 10
	internal abstract class StreamBase : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000241E File Offset: 0x0000061E
		protected StreamBase(StreamBase.Capabilities capabilities)
		{
			EnumValidator.ThrowIfInvalid<StreamBase.Capabilities>(capabilities, "capabilities");
			this.capabilities = capabilities;
			this.disposeTracker = this.GetDisposeTracker();
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002444 File Offset: 0x00000644
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
				this.isClosed = true;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002488 File Offset: 0x00000688
		public override bool CanRead
		{
			get
			{
				this.CheckDisposed("CanRead:get");
				return (this.capabilities & StreamBase.Capabilities.Readable) == StreamBase.Capabilities.Readable;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000024A0 File Offset: 0x000006A0
		public override bool CanWrite
		{
			get
			{
				this.CheckDisposed("CanWrite:get");
				return (this.capabilities & StreamBase.Capabilities.Writable) == StreamBase.Capabilities.Writable;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000024B8 File Offset: 0x000006B8
		public override bool CanSeek
		{
			get
			{
				this.CheckDisposed("CanSeek:get");
				return (this.capabilities & StreamBase.Capabilities.Seekable) == StreamBase.Capabilities.Seekable;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000024D0 File Offset: 0x000006D0
		public override long Length
		{
			get
			{
				this.CheckDisposed("Length:Get");
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000024E2 File Offset: 0x000006E2
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000024F4 File Offset: 0x000006F4
		public override long Position
		{
			get
			{
				this.CheckDisposed("Position:get");
				throw new NotSupportedException();
			}
			set
			{
				this.CheckDisposed("Position:set");
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002506 File Offset: 0x00000706
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed("Write");
			throw new NotSupportedException();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002518 File Offset: 0x00000718
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed("Read");
			throw new NotSupportedException();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000252A File Offset: 0x0000072A
		public override void SetLength(long value)
		{
			this.CheckDisposed("SetLength");
			throw new NotSupportedException();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000253C File Offset: 0x0000073C
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckDisposed("Seek");
			throw new NotSupportedException();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000254E File Offset: 0x0000074E
		public override void Flush()
		{
			this.CheckDisposed("Flush");
			throw new NotSupportedException();
		}

		// Token: 0x06000023 RID: 35
		public abstract DisposeTracker GetDisposeTracker();

		// Token: 0x06000024 RID: 36 RVA: 0x00002560 File Offset: 0x00000760
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002575 File Offset: 0x00000775
		protected void CheckDisposed(string methodName)
		{
			if (this.isClosed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002590 File Offset: 0x00000790
		protected bool IsClosed
		{
			get
			{
				return this.isClosed;
			}
		}

		// Token: 0x0400000D RID: 13
		private StreamBase.Capabilities capabilities;

		// Token: 0x0400000E RID: 14
		private bool isClosed;

		// Token: 0x0400000F RID: 15
		private readonly DisposeTracker disposeTracker;

		// Token: 0x0200000B RID: 11
		[Flags]
		internal enum Capabilities
		{
			// Token: 0x04000011 RID: 17
			None = 0,
			// Token: 0x04000012 RID: 18
			Readable = 1,
			// Token: 0x04000013 RID: 19
			Writable = 2,
			// Token: 0x04000014 RID: 20
			Seekable = 4
		}
	}
}
