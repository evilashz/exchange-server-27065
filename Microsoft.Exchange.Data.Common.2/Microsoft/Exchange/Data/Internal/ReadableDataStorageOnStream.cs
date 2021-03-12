using System;
using System.IO;

namespace Microsoft.Exchange.Data.Internal
{
	// Token: 0x02000139 RID: 313
	internal class ReadableDataStorageOnStream : ReadableDataStorage
	{
		// Token: 0x06000C2B RID: 3115 RVA: 0x0006BC8A File Offset: 0x00069E8A
		public ReadableDataStorageOnStream(Stream stream, bool ownsStream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.stream = stream;
			this.ownsStream = ownsStream;
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x0006BCAE File Offset: 0x00069EAE
		public override long Length
		{
			get
			{
				return this.stream.Length;
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x0006BCBC File Offset: 0x00069EBC
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

		// Token: 0x06000C2E RID: 3118 RVA: 0x0006BD1C File Offset: 0x00069F1C
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

		// Token: 0x06000C2F RID: 3119 RVA: 0x0006BD4A File Offset: 0x00069F4A
		private int InternalRead(long position, byte[] buffer, int offset, int count)
		{
			this.stream.Position = position;
			return this.stream.Read(buffer, offset, count);
		}

		// Token: 0x04000EF2 RID: 3826
		private Stream stream;

		// Token: 0x04000EF3 RID: 3827
		private bool ownsStream;
	}
}
