using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.OAB
{
	// Token: 0x02000141 RID: 321
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class BaseStream : Stream
	{
		// Token: 0x06000D2A RID: 3370 RVA: 0x00037A1D File Offset: 0x00035C1D
		public BaseStream(Stream stream)
		{
			this.stream = stream;
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x00037A2C File Offset: 0x00035C2C
		public Stream InnerStream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00037A34 File Offset: 0x00035C34
		public override bool CanRead
		{
			get
			{
				return this.stream.CanRead;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x00037A41 File Offset: 0x00035C41
		public override bool CanWrite
		{
			get
			{
				return this.stream.CanWrite;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x00037A4E File Offset: 0x00035C4E
		public override bool CanSeek
		{
			get
			{
				return this.stream.CanSeek;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x00037A5B File Offset: 0x00035C5B
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x00037A68 File Offset: 0x00035C68
		// (set) Token: 0x06000D31 RID: 3377 RVA: 0x00037A75 File Offset: 0x00035C75
		public override long Position
		{
			get
			{
				return this.stream.Position;
			}
			set
			{
				this.stream.Position = value;
			}
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x00037A83 File Offset: 0x00035C83
		public override void Close()
		{
			this.stream.Close();
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x00037A90 File Offset: 0x00035C90
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x00037AA0 File Offset: 0x00035CA0
		public override void Flush()
		{
			this.stream.Flush();
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x00037AAD File Offset: 0x00035CAD
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00037ABD File Offset: 0x00035CBD
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.stream.Seek(offset, origin);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x00037ACC File Offset: 0x00035CCC
		public override void SetLength(long length)
		{
			this.stream.SetLength(length);
		}

		// Token: 0x0400070B RID: 1803
		private readonly Stream stream;
	}
}
