using System;
using System.IO;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x0200013A RID: 314
	internal class ReadableWritableDataStorageOnStream : ReadableWritableDataStorage
	{
		// Token: 0x06000C30 RID: 3120 RVA: 0x0006BD67 File Offset: 0x00069F67
		public ReadableWritableDataStorageOnStream(Stream stream, bool ownsStream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.stream = stream;
			this.ownsStream = ownsStream;
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0006BD8B File Offset: 0x00069F8B
		public override long Length
		{
			get
			{
				base.ThrowIfDisposed();
				return this.stream.Length;
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0006BDA0 File Offset: 0x00069FA0
		public override int Read(long position, byte[] buffer, int offset, int count)
		{
			base.ThrowIfDisposed();
			int result = 0;
			if (this.isReadOnly)
			{
				this.readOnlySemaphore.Wait();
				try
				{
					return this.InternalRead(position, buffer, offset, count);
				}
				finally
				{
					this.readOnlySemaphore.Release();
				}
			}
			result = this.InternalRead(position, buffer, offset, count);
			return result;
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x0006BE00 File Offset: 0x0006A000
		public override void Write(long position, byte[] buffer, int offset, int count)
		{
			base.ThrowIfDisposed();
			if (this.isReadOnly)
			{
				throw new InvalidOperationException("Write to read-only DataStorage");
			}
			this.stream.Position = position;
			this.stream.Write(buffer, offset, count);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0006BE36 File Offset: 0x0006A036
		public override void SetLength(long length)
		{
			base.ThrowIfDisposed();
			if (this.isReadOnly)
			{
				throw new InvalidOperationException("Write to read-only DataStorage");
			}
			this.stream.SetLength(length);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x0006BE5D File Offset: 0x0006A05D
		protected override void Dispose(bool disposing)
		{
			if (!base.IsDisposed)
			{
				if (disposing && this.ownsStream)
				{
					this.stream.Dispose();
				}
				this.stream = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x0006BE8B File Offset: 0x0006A08B
		private int InternalRead(long position, byte[] buffer, int offset, int count)
		{
			this.stream.Position = position;
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x04000EF4 RID: 3828
		protected Stream stream;

		// Token: 0x04000EF5 RID: 3829
		protected bool ownsStream;
	}
}
