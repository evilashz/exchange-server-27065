using System;
using System.IO;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000130 RID: 304
	internal sealed class AutoPositionReadOnlyStream : Stream, ICloneableStream
	{
		// Token: 0x06000BE6 RID: 3046 RVA: 0x0006B4C9 File Offset: 0x000696C9
		public AutoPositionReadOnlyStream(Stream wrapped, bool ownsStream)
		{
			this.storage = new ReadableDataStorageOnStream(wrapped, ownsStream);
			this.position = wrapped.Position;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0006B4EA File Offset: 0x000696EA
		private AutoPositionReadOnlyStream(AutoPositionReadOnlyStream original)
		{
			original.storage.AddRef();
			this.storage = original.storage;
			this.position = original.position;
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0006B515 File Offset: 0x00069715
		public override bool CanRead
		{
			get
			{
				return this.storage != null;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0006B523 File Offset: 0x00069723
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0006B526 File Offset: 0x00069726
		public override bool CanSeek
		{
			get
			{
				return this.storage != null;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0006B534 File Offset: 0x00069734
		public override long Length
		{
			get
			{
				if (this.storage == null)
				{
					throw new ObjectDisposedException("AutoPositionReadOnlyStream");
				}
				return this.storage.Length;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0006B554 File Offset: 0x00069754
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x0006B56F File Offset: 0x0006976F
		public override long Position
		{
			get
			{
				if (this.storage == null)
				{
					throw new ObjectDisposedException("AutoPositionReadOnlyStream");
				}
				return this.position;
			}
			set
			{
				if (this.storage == null)
				{
					throw new ObjectDisposedException("AutoPositionReadOnlyStream");
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", SharedStrings.CannotSeekBeforeBeginning);
				}
				this.position = value;
			}
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x0006B5A0 File Offset: 0x000697A0
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this.storage == null)
			{
				throw new ObjectDisposedException("AutoPositionReadOnlyStream");
			}
			int num = this.storage.Read(this.position, buffer, offset, count);
			this.position += (long)num;
			return num;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0006B5E5 File Offset: 0x000697E5
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0006B5EC File Offset: 0x000697EC
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0006B5F3 File Offset: 0x000697F3
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0006B5FC File Offset: 0x000697FC
		public override long Seek(long offset, SeekOrigin origin)
		{
			if (this.storage == null)
			{
				throw new ObjectDisposedException("AutoPositionReadOnlyStream");
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
				throw new ArgumentOutOfRangeException("offset", SharedStrings.CannotSeekBeforeBeginning);
			}
			this.position = offset;
			return this.position;
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0006B677 File Offset: 0x00069877
		public Stream Clone()
		{
			if (this.storage == null)
			{
				throw new ObjectDisposedException("AutoPositionReadOnlyStream");
			}
			return new AutoPositionReadOnlyStream(this);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0006B692 File Offset: 0x00069892
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.storage != null)
			{
				this.storage.Release();
				this.storage = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000EA4 RID: 3748
		private ReadableDataStorage storage;

		// Token: 0x04000EA5 RID: 3749
		private long position;
	}
}
