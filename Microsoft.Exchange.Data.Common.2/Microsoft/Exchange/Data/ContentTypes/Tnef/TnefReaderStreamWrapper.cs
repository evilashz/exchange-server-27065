using System;
using System.IO;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.ContentTypes.Tnef
{
	// Token: 0x020000E2 RID: 226
	internal class TnefReaderStreamWrapper : Stream
	{
		// Token: 0x0600092A RID: 2346 RVA: 0x000311A6 File Offset: 0x0002F3A6
		public TnefReaderStreamWrapper(TnefReader reader)
		{
			this.Reader = reader;
			this.Reader.Child = this;
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x000311C1 File Offset: 0x0002F3C1
		public override bool CanRead
		{
			get
			{
				return this.Reader != null;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x000311CF File Offset: 0x0002F3CF
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x0600092D RID: 2349 RVA: 0x000311D2 File Offset: 0x0002F3D2
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x000311D5 File Offset: 0x0002F3D5
		public override long Length
		{
			get
			{
				throw new NotSupportedException(TnefStrings.StreamDoesNotSupportSeek);
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x0600092F RID: 2351 RVA: 0x000311E1 File Offset: 0x0002F3E1
		// (set) Token: 0x06000930 RID: 2352 RVA: 0x000311ED File Offset: 0x0002F3ED
		public override long Position
		{
			get
			{
				throw new NotSupportedException(TnefStrings.StreamDoesNotSupportSeek);
			}
			set
			{
				throw new NotSupportedException(TnefStrings.StreamDoesNotSupportSeek);
			}
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x000311F9 File Offset: 0x0002F3F9
		public override void Flush()
		{
			throw new NotSupportedException(TnefStrings.StreamDoesNotSupportWrite);
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x00031205 File Offset: 0x0002F405
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.Reader == null)
			{
				throw new ObjectDisposedException("TnefReaderStreamWrapper");
			}
			return this.Reader.ReadPropertyRawValue(buffer, offset, count, true);
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x00031229 File Offset: 0x0002F429
		public override void SetLength(long value)
		{
			throw new NotSupportedException(TnefStrings.StreamDoesNotSupportSeek);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x00031235 File Offset: 0x0002F435
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException(TnefStrings.StreamDoesNotSupportWrite);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x00031241 File Offset: 0x0002F441
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException(TnefStrings.StreamDoesNotSupportSeek);
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0003124D File Offset: 0x0002F44D
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.Reader != null)
			{
				this.Reader.Child = null;
			}
			this.Reader = null;
			base.Dispose(disposing);
		}

		// Token: 0x040007CD RID: 1997
		internal TnefReader Reader;
	}
}
