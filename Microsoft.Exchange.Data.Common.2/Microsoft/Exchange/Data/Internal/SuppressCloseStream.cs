using System;
using System.IO;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000135 RID: 309
	internal sealed class SuppressCloseStream : Stream, ICloneableStream
	{
		// Token: 0x06000C00 RID: 3072 RVA: 0x0006B711 File Offset: 0x00069911
		public SuppressCloseStream(Stream sourceStream)
		{
			if (sourceStream == null)
			{
				throw new ArgumentNullException("sourceStream");
			}
			this.sourceStream = sourceStream;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0006B72E File Offset: 0x0006992E
		public override bool CanRead
		{
			get
			{
				return this.sourceStream != null && this.sourceStream.CanRead;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0006B745 File Offset: 0x00069945
		public override bool CanWrite
		{
			get
			{
				return this.sourceStream != null && this.sourceStream.CanWrite;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x0006B75C File Offset: 0x0006995C
		public override bool CanSeek
		{
			get
			{
				return this.sourceStream != null && this.sourceStream.CanSeek;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x0006B773 File Offset: 0x00069973
		public override long Length
		{
			get
			{
				this.AssertOpen();
				return this.sourceStream.Length;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0006B786 File Offset: 0x00069986
		// (set) Token: 0x06000C06 RID: 3078 RVA: 0x0006B799 File Offset: 0x00069999
		public override long Position
		{
			get
			{
				this.AssertOpen();
				return this.sourceStream.Position;
			}
			set
			{
				this.AssertOpen();
				this.sourceStream.Position = value;
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0006B7AD File Offset: 0x000699AD
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.AssertOpen();
			return this.sourceStream.Read(buffer, offset, count);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0006B7C3 File Offset: 0x000699C3
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.AssertOpen();
			this.sourceStream.Write(buffer, offset, count);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0006B7D9 File Offset: 0x000699D9
		public override void Flush()
		{
			this.AssertOpen();
			this.sourceStream.Flush();
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0006B7EC File Offset: 0x000699EC
		public override void SetLength(long value)
		{
			this.AssertOpen();
			this.sourceStream.SetLength(value);
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0006B800 File Offset: 0x00069A00
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.AssertOpen();
			return this.sourceStream.Seek(offset, origin);
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0006B818 File Offset: 0x00069A18
		public Stream Clone()
		{
			this.AssertOpen();
			if (this.CanWrite)
			{
				throw new NotSupportedException();
			}
			ICloneableStream cloneableStream = this.sourceStream as ICloneableStream;
			if (cloneableStream == null)
			{
				if (!this.sourceStream.CanSeek)
				{
					throw new NotSupportedException();
				}
				this.sourceStream = new AutoPositionReadOnlyStream(this.sourceStream, false);
				cloneableStream = (this.sourceStream as ICloneableStream);
			}
			return new SuppressCloseStream(cloneableStream.Clone());
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x0006B884 File Offset: 0x00069A84
		private void AssertOpen()
		{
			if (this.sourceStream == null)
			{
				throw new ObjectDisposedException("SuppressCloseStream");
			}
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x0006B899 File Offset: 0x00069A99
		protected override void Dispose(bool disposing)
		{
			this.sourceStream = null;
			base.Dispose(disposing);
		}

		// Token: 0x04000EEC RID: 3820
		private Stream sourceStream;
	}
}
