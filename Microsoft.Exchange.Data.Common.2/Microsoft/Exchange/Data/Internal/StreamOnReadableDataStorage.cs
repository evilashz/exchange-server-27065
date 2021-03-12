using System;
using System.IO;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000138 RID: 312
	internal class StreamOnReadableDataStorage : StreamOnDataStorage, ICloneableStream
	{
		// Token: 0x06000C18 RID: 3096 RVA: 0x0006B980 File Offset: 0x00069B80
		public StreamOnReadableDataStorage(ReadableDataStorage baseStorage, long start, long end)
		{
			if (baseStorage != null)
			{
				baseStorage.AddRef();
				this.baseStorage = baseStorage;
			}
			this.start = start;
			this.end = end;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x0006B9A6 File Offset: 0x00069BA6
		private StreamOnReadableDataStorage(ReadableDataStorage baseStorage, long start, long end, long position)
		{
			if (baseStorage != null)
			{
				baseStorage.AddRef();
				this.baseStorage = baseStorage;
			}
			this.start = start;
			this.end = end;
			this.position = position;
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0006B9D4 File Offset: 0x00069BD4
		public override DataStorage Storage
		{
			get
			{
				this.ThrowIfDisposed();
				return this.baseStorage;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x0006B9E2 File Offset: 0x00069BE2
		public override long Start
		{
			get
			{
				this.ThrowIfDisposed();
				return this.start;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0006B9F0 File Offset: 0x00069BF0
		public override long End
		{
			get
			{
				this.ThrowIfDisposed();
				return this.end;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x0006B9FE File Offset: 0x00069BFE
		public override bool CanRead
		{
			get
			{
				return !this.disposed;
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x0006BA09 File Offset: 0x00069C09
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0006BA0C File Offset: 0x00069C0C
		public override bool CanSeek
		{
			get
			{
				return !this.disposed;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0006BA17 File Offset: 0x00069C17
		public override long Length
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.end != 9223372036854775807L)
				{
					return this.end - this.start;
				}
				return this.baseStorage.Length - this.start;
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0006BA50 File Offset: 0x00069C50
		// (set) Token: 0x06000C22 RID: 3106 RVA: 0x0006BA5E File Offset: 0x00069C5E
		public override long Position
		{
			get
			{
				this.ThrowIfDisposed();
				return this.position;
			}
			set
			{
				this.ThrowIfDisposed();
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value", SharedStrings.CannotSeekBeforeBeginning);
				}
				this.position = value;
			}
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0006BA84 File Offset: 0x00069C84
		public override int Read(byte[] buffer, int offset, int count)
		{
			this.ThrowIfDisposed();
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
			int num = 0;
			if ((this.end == 9223372036854775807L || this.position < this.end - this.start) && count != 0)
			{
				if (this.end != 9223372036854775807L && (long)count > this.end - this.start - this.position)
				{
					count = (int)(this.end - this.start - this.position);
				}
				int num2;
				do
				{
					num2 = this.baseStorage.Read(this.start + this.position, buffer, offset, count);
					count -= num2;
					offset += num2;
					this.position += (long)num2;
					num += num2;
				}
				while (count != 0 && num2 != 0);
			}
			return num;
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0006BB99 File Offset: 0x00069D99
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0006BBA0 File Offset: 0x00069DA0
		public override void Flush()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x0006BBA7 File Offset: 0x00069DA7
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x0006BBB0 File Offset: 0x00069DB0
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.ThrowIfDisposed();
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
				throw new ArgumentException("Invalid Origin enumeration value", "origin");
			}
			if (offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset", SharedStrings.CannotSeekBeforeBeginning);
			}
			this.position = offset;
			return this.position;
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0006BC23 File Offset: 0x00069E23
		public Stream Clone()
		{
			this.ThrowIfDisposed();
			return new StreamOnReadableDataStorage(this.baseStorage, this.start, this.end, this.position);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0006BC48 File Offset: 0x00069E48
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.baseStorage != null)
			{
				this.baseStorage.Release();
				this.baseStorage = null;
			}
			this.disposed = true;
			base.Dispose(disposing);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x0006BC75 File Offset: 0x00069E75
		private void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException("StreamOnReadableDataStorage");
			}
		}

		// Token: 0x04000EED RID: 3821
		private ReadableDataStorage baseStorage;

		// Token: 0x04000EEE RID: 3822
		private long start;

		// Token: 0x04000EEF RID: 3823
		private long end;

		// Token: 0x04000EF0 RID: 3824
		private long position;

		// Token: 0x04000EF1 RID: 3825
		private bool disposed;
	}
}
