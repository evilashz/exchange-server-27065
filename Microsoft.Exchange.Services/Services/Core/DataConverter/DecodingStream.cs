using System;
using System.IO;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000155 RID: 341
	internal abstract class DecodingStream : Stream
	{
		// Token: 0x06000962 RID: 2402 RVA: 0x0002DD4A File Offset: 0x0002BF4A
		public DecodingStream(TextWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000963 RID: 2403 RVA: 0x0002DD59 File Offset: 0x0002BF59
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000964 RID: 2404 RVA: 0x0002DD5C File Offset: 0x0002BF5C
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000965 RID: 2405 RVA: 0x0002DD5F File Offset: 0x0002BF5F
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0002DD62 File Offset: 0x0002BF62
		public override long Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0002DD69 File Offset: 0x0002BF69
		// (set) Token: 0x06000968 RID: 2408 RVA: 0x0002DD70 File Offset: 0x0002BF70
		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0002DD77 File Offset: 0x0002BF77
		public override void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0002DD84 File Offset: 0x0002BF84
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0002DD8B File Offset: 0x0002BF8B
		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0002DD92 File Offset: 0x0002BF92
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000780 RID: 1920
		protected TextWriter writer;
	}
}
