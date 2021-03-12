using System;
using System.IO;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000106 RID: 262
	internal class SynchronizedStream : Stream
	{
		// Token: 0x06000B31 RID: 2865 RVA: 0x00026D5E File Offset: 0x00024F5E
		public SynchronizedStream(Stream wrapped)
		{
			this.stream = wrapped;
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000B32 RID: 2866 RVA: 0x00026D6D File Offset: 0x00024F6D
		public override bool CanRead
		{
			get
			{
				return this.stream != null && this.stream.CanRead;
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00026D84 File Offset: 0x00024F84
		public override bool CanWrite
		{
			get
			{
				return this.stream != null && this.stream.CanWrite;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x00026D9B File Offset: 0x00024F9B
		public override bool CanSeek
		{
			get
			{
				return this.stream != null && this.stream.CanSeek;
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x00026DB4 File Offset: 0x00024FB4
		public override long Length
		{
			get
			{
				if (this.stream == null)
				{
					throw new ObjectDisposedException("AutoPositionStream");
				}
				long length;
				lock (this.stream)
				{
					length = this.stream.Length;
				}
				return length;
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x00026E10 File Offset: 0x00025010
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x00026E2B File Offset: 0x0002502B
		public override long Position
		{
			get
			{
				if (this.stream == null)
				{
					throw new ObjectDisposedException("AutoPositionStream");
				}
				return this.position;
			}
			set
			{
				if (this.stream == null)
				{
					throw new ObjectDisposedException("AutoPositionStream");
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", "Cannot Seek before the beginning");
				}
				this.position = value;
			}
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00026E5C File Offset: 0x0002505C
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.stream == null)
			{
				throw new ObjectDisposedException("AutoPositionStream");
			}
			int num = 0;
			lock (this.stream)
			{
				this.stream.Position = this.position;
				num = this.stream.Read(buffer, offset, count);
			}
			this.position += (long)num;
			return num;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00026EDC File Offset: 0x000250DC
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.stream == null)
			{
				throw new ObjectDisposedException("AutoPositionStream");
			}
			lock (this.stream)
			{
				this.stream.Position = this.position;
				this.stream.Write(buffer, offset, count);
			}
			this.position += (long)count;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00026F58 File Offset: 0x00025158
		public override void SetLength(long value)
		{
			if (this.stream == null)
			{
				throw new ObjectDisposedException("AutoPositionStream");
			}
			lock (this.stream)
			{
				this.stream.SetLength(value);
			}
			if (this.position > value)
			{
				this.position = value;
			}
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00026FC4 File Offset: 0x000251C4
		public override void Flush()
		{
			if (this.stream == null)
			{
				throw new ObjectDisposedException("AutoPositionStream");
			}
			lock (this.stream)
			{
				this.stream.Flush();
			}
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x0002701C File Offset: 0x0002521C
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (this.stream == null)
			{
				throw new ObjectDisposedException("AutoPositionStream");
			}
			switch (origin)
			{
			case SeekOrigin.Begin:
				break;
			case SeekOrigin.Current:
				offset += this.position;
				break;
			case SeekOrigin.End:
				offset += this.Length;
				break;
			default:
				throw new ArgumentException("origin");
			}
			if (0L > offset)
			{
				throw new ArgumentOutOfRangeException("offset", "Cannot Seek before the beginning");
			}
			this.position = offset;
			return this.position;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00027097 File Offset: 0x00025297
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.stream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040004D3 RID: 1235
		private long position;

		// Token: 0x040004D4 RID: 1236
		private Stream stream;
	}
}
