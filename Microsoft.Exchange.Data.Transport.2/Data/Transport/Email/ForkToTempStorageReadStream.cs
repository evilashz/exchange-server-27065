using System;
using System.IO;
using Microsoft.Exchange.Data.Internal;

namespace Microsoft.Exchange.Data.Transport.Email
{
	// Token: 0x020000F1 RID: 241
	internal class ForkToTempStorageReadStream : Stream
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x00010C86 File Offset: 0x0000EE86
		public ForkToTempStorageReadStream(Stream sourceStream)
		{
			this.sourceStream = sourceStream;
			this.storage = new TemporaryDataStorage();
			this.forkStream = this.storage.OpenWriteStream(true);
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x00010CB2 File Offset: 0x0000EEB2
		public DataStorage Storage
		{
			get
			{
				return this.storage;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00010CBA File Offset: 0x0000EEBA
		public override bool CanRead
		{
			get
			{
				return this.sourceStream != null;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x00010CC8 File Offset: 0x0000EEC8
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00010CCB File Offset: 0x0000EECB
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x00010CCE File Offset: 0x0000EECE
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x00010CD5 File Offset: 0x0000EED5
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x00010CDC File Offset: 0x0000EEDC
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00010CE4 File Offset: 0x0000EEE4
		public override int Read(byte[] buffer, int offset, int count)
		{
			int num = this.sourceStream.Read(buffer, offset, count);
			this.forkStream.Write(buffer, offset, num);
			return num;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00010D0F File Offset: 0x0000EF0F
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00010D16 File Offset: 0x0000EF16
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00010D1D File Offset: 0x0000EF1D
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00010D24 File Offset: 0x0000EF24
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x00010D2C File Offset: 0x0000EF2C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.sourceStream != null)
			{
				this.sourceStream.Dispose();
				this.sourceStream = null;
				this.forkStream.Dispose();
				this.forkStream = null;
				this.storage.Release();
				this.storage = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040003BF RID: 959
		private Stream sourceStream;

		// Token: 0x040003C0 RID: 960
		private Stream forkStream;

		// Token: 0x040003C1 RID: 961
		private TemporaryDataStorage storage;
	}
}
