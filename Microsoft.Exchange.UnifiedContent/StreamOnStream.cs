using System;
using System.IO;

namespace Microsoft.Exchange.UnifiedContent
{
	// Token: 0x0200000D RID: 13
	public class StreamOnStream : Stream
	{
		// Token: 0x06000082 RID: 130 RVA: 0x000039B0 File Offset: 0x00001BB0
		public StreamOnStream(Stream stream, long start, long length)
		{
			this.baseStream = stream;
			this.start = Math.Min(start, stream.Length);
			this.length = Math.Min(length, stream.Length - this.start);
			this.position = 0L;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000039FD File Offset: 0x00001BFD
		public override bool CanRead
		{
			get
			{
				return this.baseStream.CanRead;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003A0A File Offset: 0x00001C0A
		public override bool CanSeek
		{
			get
			{
				return this.baseStream.CanSeek;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003A17 File Offset: 0x00001C17
		public override bool CanWrite
		{
			get
			{
				return this.baseStream.CanWrite;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003A24 File Offset: 0x00001C24
		public override long Length
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003A2C File Offset: 0x00001C2C
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003A34 File Offset: 0x00001C34
		public override long Position
		{
			get
			{
				return this.position;
			}
			set
			{
				this.position = value;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003A3D File Offset: 0x00001C3D
		public override void Flush()
		{
			this.baseStream.Flush();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003A4C File Offset: 0x00001C4C
		public override int Read(byte[] buffer, int offset, int count)
		{
			count = Math.Min(count, (int)(this.length - this.position));
			long num = this.baseStream.Position;
			this.baseStream.Position = this.position + this.start;
			int result = this.baseStream.Read(buffer, offset, count);
			this.position = this.baseStream.Position - this.start;
			this.baseStream.Position = num;
			return result;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
			case SeekOrigin.Begin:
				this.position = offset;
				break;
			case SeekOrigin.Current:
				this.position += offset;
				break;
			case SeekOrigin.End:
				this.position = this.length - offset;
				break;
			default:
				throw new ArgumentException("Invalid seek origin");
			}
			if (this.position < 0L)
			{
				throw new IOException("Cannot seek before beginning of stream.");
			}
			return this.position;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003B3A File Offset: 0x00001D3A
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003B44 File Offset: 0x00001D44
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (count > (int)(this.length - this.position))
			{
				throw new NotSupportedException();
			}
			long num = this.baseStream.Position;
			this.baseStream.Position = this.position + this.start;
			this.baseStream.Write(buffer, offset, count);
			this.position = this.baseStream.Position - this.start;
			this.baseStream.Position = num;
		}

		// Token: 0x0400004D RID: 77
		private readonly long start;

		// Token: 0x0400004E RID: 78
		private readonly long length;

		// Token: 0x0400004F RID: 79
		private readonly Stream baseStream;

		// Token: 0x04000050 RID: 80
		private long position;
	}
}
