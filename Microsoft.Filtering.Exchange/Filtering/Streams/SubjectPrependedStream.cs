using System;
using System.IO;
using System.Text;

namespace Microsoft.Filtering.Streams
{
	// Token: 0x0200001A RID: 26
	internal class SubjectPrependedStream : Stream
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00003730 File Offset: 0x00001930
		public SubjectPrependedStream(string subject, Stream stream)
		{
			this.stream = stream;
			this.subjectBytes = ((!string.IsNullOrEmpty(subject)) ? Encoding.Unicode.GetBytes(subject) : new byte[0]);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003760 File Offset: 0x00001960
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003763 File Offset: 0x00001963
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003766 File Offset: 0x00001966
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003769 File Offset: 0x00001969
		public override long Length
		{
			get
			{
				return (long)this.subjectBytes.Length + this.stream.Length;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003780 File Offset: 0x00001980
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00003788 File Offset: 0x00001988
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

		// Token: 0x0600007B RID: 123 RVA: 0x00003791 File Offset: 0x00001991
		public override void Flush()
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003794 File Offset: 0x00001994
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.position >= this.Length)
			{
				return 0;
			}
			int num = this.subjectBytes.Length;
			int num2 = 0;
			int num3 = 0;
			if (this.position < (long)num)
			{
				num2 = Math.Min(count, num - (int)this.position);
				Buffer.BlockCopy(this.subjectBytes, (int)this.position, buffer, offset, num2);
				if (this.position + (long)count >= (long)num)
				{
					this.position = (long)num;
					num3 = num2;
				}
			}
			if (this.position >= (long)num)
			{
				this.stream.Position = this.position - (long)num;
				num2 += this.stream.Read(buffer, offset + num2, count - num2);
			}
			this.Seek((long)(num2 - num3), SeekOrigin.Current);
			return num2;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003844 File Offset: 0x00001A44
		public override long Seek(long offset, SeekOrigin origin)
		{
			switch (origin)
			{
			case SeekOrigin.Begin:
				this.Position = offset;
				break;
			case SeekOrigin.Current:
				this.Position += offset;
				break;
			case SeekOrigin.End:
				this.Position = this.Length - offset;
				break;
			default:
				throw new ArgumentException("Invalid seek origin");
			}
			return this.Position;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000038A1 File Offset: 0x00001AA1
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000038A8 File Offset: 0x00001AA8
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000045 RID: 69
		private byte[] subjectBytes;

		// Token: 0x04000046 RID: 70
		private Stream stream;

		// Token: 0x04000047 RID: 71
		private long position;
	}
}
