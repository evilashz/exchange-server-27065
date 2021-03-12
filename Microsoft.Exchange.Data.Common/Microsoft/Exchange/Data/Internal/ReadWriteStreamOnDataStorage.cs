using System;
using System.IO;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x0200013D RID: 317
	internal class ReadWriteStreamOnDataStorage : StreamOnDataStorage, ICloneableStream
	{
		// Token: 0x06000C51 RID: 3153 RVA: 0x0006CBD2 File Offset: 0x0006ADD2
		internal ReadWriteStreamOnDataStorage(ReadableWritableDataStorage storage)
		{
			storage.AddRef();
			this.storage = storage;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0006CBE7 File Offset: 0x0006ADE7
		private ReadWriteStreamOnDataStorage(ReadableWritableDataStorage storage, long position)
		{
			storage.AddRef();
			this.storage = storage;
			this.position = position;
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0006CC03 File Offset: 0x0006AE03
		public override DataStorage Storage
		{
			get
			{
				if (this.storage == null)
				{
					throw new ObjectDisposedException("ReadWriteStreamOnDataStorage");
				}
				return this.storage;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x0006CC1E File Offset: 0x0006AE1E
		public override long Start
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x0006CC22 File Offset: 0x0006AE22
		public override long End
		{
			get
			{
				return long.MaxValue;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x0006CC2D File Offset: 0x0006AE2D
		public override bool CanRead
		{
			get
			{
				return this.storage != null;
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0006CC3A File Offset: 0x0006AE3A
		public override bool CanWrite
		{
			get
			{
				return this.storage != null;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x0006CC47 File Offset: 0x0006AE47
		public override bool CanSeek
		{
			get
			{
				return this.storage != null;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0006CC54 File Offset: 0x0006AE54
		public override long Length
		{
			get
			{
				if (this.storage == null)
				{
					throw new ObjectDisposedException("ReadWriteStreamOnDataStorage");
				}
				return this.storage.Length;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000C5A RID: 3162 RVA: 0x0006CC74 File Offset: 0x0006AE74
		// (set) Token: 0x06000C5B RID: 3163 RVA: 0x0006CC8F File Offset: 0x0006AE8F
		public override long Position
		{
			get
			{
				if (this.storage == null)
				{
					throw new ObjectDisposedException("ReadWriteStreamOnDataStorage");
				}
				return this.position;
			}
			set
			{
				if (this.storage == null)
				{
					throw new ObjectDisposedException("ReadWriteStreamOnDataStorage");
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", SharedStrings.CannotSeekBeforeBeginning);
				}
				this.position = value;
			}
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0006CCC0 File Offset: 0x0006AEC0
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.storage == null)
			{
				throw new ObjectDisposedException("ReadWriteStreamOnDataStorage");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset > buffer.Length || offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SharedStrings.OffsetOutOfRange);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SharedStrings.CountOutOfRange);
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count", SharedStrings.CountTooLarge);
			}
			int num = this.storage.Read(this.position, buffer, offset, count);
			this.position += (long)num;
			return num;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0006CD5C File Offset: 0x0006AF5C
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.storage == null)
			{
				throw new ObjectDisposedException("ReadWriteStreamOnDataStorage");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset > buffer.Length || offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SharedStrings.OffsetOutOfRange);
			}
			if (count > buffer.Length || count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SharedStrings.CountOutOfRange);
			}
			if (count + offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count", SharedStrings.CountTooLarge);
			}
			this.storage.Write(this.position, buffer, offset, count);
			this.position += (long)count;
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0006CDF9 File Offset: 0x0006AFF9
		public override void SetLength(long value)
		{
			if (this.storage == null)
			{
				throw new ObjectDisposedException("ReadWriteStreamOnDataStorage");
			}
			if (value < 0L)
			{
				throw new ArgumentOutOfRangeException("value", SharedStrings.CannotSetNegativelength);
			}
			this.storage.SetLength(value);
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0006CE2F File Offset: 0x0006B02F
		public override void Flush()
		{
			if (this.storage == null)
			{
				throw new ObjectDisposedException("ReadWriteStreamOnDataStorage");
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0006CE44 File Offset: 0x0006B044
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (this.storage == null)
			{
				throw new ObjectDisposedException("ReadWriteStreamOnDataStorage");
			}
			switch (origin)
			{
			case SeekOrigin.Begin:
				this.position = offset;
				break;
			case SeekOrigin.Current:
				offset = this.position + offset;
				break;
			case SeekOrigin.End:
				offset = this.storage.Length + offset;
				break;
			default:
				throw new ArgumentException("Invalid Origin enumeration value", "origin");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset", SharedStrings.CannotSeekBeforeBeginning);
			}
			this.position = offset;
			return this.position;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0006CED2 File Offset: 0x0006B0D2
		Stream ICloneableStream.Clone()
		{
			if (this.storage == null)
			{
				throw new ObjectDisposedException("ReadWriteStreamOnDataStorage");
			}
			return new ReadWriteStreamOnDataStorage(this.storage, this.position);
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0006CEF8 File Offset: 0x0006B0F8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.storage != null)
			{
				this.storage.Release();
				this.storage = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000F09 RID: 3849
		private ReadableWritableDataStorage storage;

		// Token: 0x04000F0A RID: 3850
		private long position;
	}
}
