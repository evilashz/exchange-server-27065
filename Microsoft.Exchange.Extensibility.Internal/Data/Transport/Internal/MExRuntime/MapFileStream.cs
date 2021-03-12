using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x02000086 RID: 134
	internal sealed class MapFileStream : Stream
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x0001352F File Offset: 0x0001172F
		public MapFileStream(SafeViewOfFileHandle viewHandle, int length, bool writable)
		{
			this.viewHandle = viewHandle;
			this.length = (long)length;
			this.writable = writable;
			this.position = 0L;
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x00013555 File Offset: 0x00011755
		public override bool CanRead
		{
			get
			{
				return this.viewHandle != null && !this.viewHandle.IsInvalid;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0001356F File Offset: 0x0001176F
		public override bool CanSeek
		{
			get
			{
				return this.viewHandle != null && !this.viewHandle.IsInvalid;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00013589 File Offset: 0x00011789
		public override bool CanWrite
		{
			get
			{
				return this.viewHandle != null && !this.viewHandle.IsInvalid && this.writable;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x000135A8 File Offset: 0x000117A8
		public override long Length
		{
			get
			{
				if (this.viewHandle == null || this.viewHandle.IsInvalid)
				{
					return 0L;
				}
				return this.length;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x000135C8 File Offset: 0x000117C8
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x000135D0 File Offset: 0x000117D0
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				this.ThrowIfInvalidHandle();
				if (value < 0L || value > this.length)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.position = value;
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000135F8 File Offset: 0x000117F8
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfInvalidHandle();
			long num;
			switch (origin)
			{
			case SeekOrigin.Begin:
				num = offset;
				break;
			case SeekOrigin.Current:
				num = this.position + offset;
				break;
			case SeekOrigin.End:
				num = this.length + offset;
				break;
			default:
				throw new ArgumentException("origin");
			}
			if (num < 0L || num > this.length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			this.position = num;
			return this.position;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001366C File Offset: 0x0001186C
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfInvalidHandle();
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("buffer", "insufficient buffer size");
			}
			int num = (int)Math.Min(this.length - this.position, (long)count);
			Marshal.Copy((IntPtr)((long)this.viewHandle.DangerousGetHandle() + this.position), buffer, offset, num);
			this.position += (long)num;
			return num;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000136E4 File Offset: 0x000118E4
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.ThrowIfInvalidHandle();
			if (!this.writable)
			{
				throw new InvalidOperationException("stream not writable");
			}
			if (buffer.Length - offset < count)
			{
				throw new ArgumentException("buffer", "insufficient buffer size");
			}
			int num = (int)Math.Min(this.length - this.position, (long)count);
			Marshal.Copy(buffer, offset, (IntPtr)((long)this.viewHandle.DangerousGetHandle() + this.position), num);
			this.position += (long)num;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001376C File Offset: 0x0001196C
		public override void Flush()
		{
			this.ThrowIfInvalidHandle();
			bool flag = NativeMethods.FlushViewOfFile(this.viewHandle, (UIntPtr)((ulong)this.length));
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag)
			{
				throw new IOException("FlushViewOfFile failed", (lastWin32Error == 0) ? null : new Win32Exception(lastWin32Error));
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000137B6 File Offset: 0x000119B6
		public override void Close()
		{
			if (this.viewHandle != null)
			{
				this.viewHandle.Dispose();
				base.Close();
				this.viewHandle = null;
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000137D8 File Offset: 0x000119D8
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000137DF File Offset: 0x000119DF
		private void ThrowIfInvalidHandle()
		{
			if (this.viewHandle == null)
			{
				throw new ObjectDisposedException("MapFileStream");
			}
			if (this.viewHandle.IsInvalid)
			{
				throw new InvalidOperationException("Invalid handle");
			}
		}

		// Token: 0x04000490 RID: 1168
		private readonly long length;

		// Token: 0x04000491 RID: 1169
		private readonly bool writable;

		// Token: 0x04000492 RID: 1170
		private SafeViewOfFileHandle viewHandle;

		// Token: 0x04000493 RID: 1171
		private long position;
	}
}
