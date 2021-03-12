using System;
using System.IO;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000066 RID: 102
	internal class AppendStreamOnDataStorage : StreamOnDataStorage
	{
		// Token: 0x060003BA RID: 954 RVA: 0x00015D20 File Offset: 0x00013F20
		public AppendStreamOnDataStorage(ReadableWritableDataStorage storage)
		{
			storage.AddRef();
			this.storage = storage;
			this.position = storage.Length;
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060003BB RID: 955 RVA: 0x00015D41 File Offset: 0x00013F41
		public override DataStorage Storage
		{
			get
			{
				return this.storage;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00015D49 File Offset: 0x00013F49
		public override long Start
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00015D4D File Offset: 0x00013F4D
		public override long End
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00015D55 File Offset: 0x00013F55
		public ReadableWritableDataStorage ReadableWritableStorage
		{
			get
			{
				return this.storage;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00015D5D File Offset: 0x00013F5D
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00015D60 File Offset: 0x00013F60
		public override bool CanWrite
		{
			get
			{
				return this.storage != null;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00015D6E File Offset: 0x00013F6E
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00015D71 File Offset: 0x00013F71
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00015D78 File Offset: 0x00013F78
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x00015D7F File Offset: 0x00013F7F
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

		// Token: 0x060003C5 RID: 965 RVA: 0x00015D86 File Offset: 0x00013F86
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00015D90 File Offset: 0x00013F90
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.storage == null)
			{
				throw new ObjectDisposedException("AppendStreamOnDataStorage");
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

		// Token: 0x060003C7 RID: 967 RVA: 0x00015E2D File Offset: 0x0001402D
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00015E34 File Offset: 0x00014034
		public override void Flush()
		{
			if (this.storage == null)
			{
				throw new ObjectDisposedException("AppendStreamOnDataStorage");
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00015E49 File Offset: 0x00014049
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00015E50 File Offset: 0x00014050
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.storage != null)
			{
				this.storage.Release();
				this.storage = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040002E1 RID: 737
		private ReadableWritableDataStorage storage;

		// Token: 0x040002E2 RID: 738
		private long position;
	}
}
