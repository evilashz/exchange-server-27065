using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x020000A0 RID: 160
	internal class EncodedStream : Stream, IDisposeTrackable, IDisposable
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x0000DC54 File Offset: 0x0000BE54
		public EncodedStream(Stream unicodeStream, Encoding encoding, IResourceTracker resourceTracker)
		{
			Util.ThrowOnNullArgument(unicodeStream, "unicodeStream");
			Util.ThrowOnNullArgument(encoding, "encoding");
			Util.ThrowOnNullArgument(resourceTracker, "resourceTracker");
			this.unicodeStream = unicodeStream;
			this.encoding = encoding;
			this.resourceTracker = resourceTracker;
			this.disposeTracker = this.GetDisposeTracker();
			using (DisposeGuard disposeGuard = this.Guard())
			{
				byte[] array = new byte[this.unicodeStream.Length];
				unicodeStream.Read(array, 0, array.Length);
				char[] chars = Encoding.Unicode.GetChars(array);
				int byteCount = encoding.GetByteCount(chars);
				this.VerifyCanChangeStreamSize(byteCount);
				byte[] bytes = encoding.GetBytes(chars);
				this.conversionStream = new MemoryStream(bytes.Length);
				this.conversionStream.Write(bytes, 0, bytes.Length);
				this.conversionStream.Seek(0L, SeekOrigin.Begin);
				disposeGuard.Success();
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		public override bool CanRead
		{
			get
			{
				return this.unicodeStream.CanRead;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000DD59 File Offset: 0x0000BF59
		public override bool CanSeek
		{
			get
			{
				return this.unicodeStream.CanSeek;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000DD66 File Offset: 0x0000BF66
		public override bool CanWrite
		{
			get
			{
				return this.unicodeStream.CanWrite;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000DD73 File Offset: 0x0000BF73
		public override long Length
		{
			get
			{
				return this.conversionStream.Length;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000DD80 File Offset: 0x0000BF80
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000DD8D File Offset: 0x0000BF8D
		public override long Position
		{
			get
			{
				return this.conversionStream.Position;
			}
			set
			{
				this.conversionStream.Position = value;
			}
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0000DD9C File Offset: 0x0000BF9C
		public override void Flush()
		{
			char[] encodedCharacters = EncodedStream.GetEncodedCharacters(this.conversionStream, this.encoding);
			byte[] bytes = Encoding.Unicode.GetBytes(encodedCharacters);
			this.unicodeStream.Position = 0L;
			this.unicodeStream.Write(bytes, 0, bytes.Length);
			this.unicodeStream.SetLength((long)bytes.Length);
			this.unicodeStream.Flush();
			this.conversionStream.Flush();
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000DE09 File Offset: 0x0000C009
		public override int Read(byte[] buffer, int offset, int count)
		{
			return this.conversionStream.Read(buffer, offset, count);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000DE19 File Offset: 0x0000C019
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.conversionStream.Seek(offset, origin);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000DE28 File Offset: 0x0000C028
		public override void SetLength(long value)
		{
			if (value > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("value", string.Format("Attempted to set the stream size to {0}.", value));
			}
			this.VerifyCanChangeStreamSize((int)value);
			this.conversionStream.SetLength(value);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000DE64 File Offset: 0x0000C064
		public override void Write(byte[] buffer, int offset, int count)
		{
			long num = this.conversionStream.Position + (long)count;
			if (num > (long)this.reservedMemory)
			{
				this.VerifyCanChangeStreamSize((int)num);
			}
			this.conversionStream.Write(buffer, offset, count);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000DEA0 File Offset: 0x0000C0A0
		protected override void Dispose(bool isDisposing)
		{
			base.Dispose(isDisposing);
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				if (isDisposing)
				{
					this.resourceTracker.TryReserveMemory(-this.reservedMemory);
					Util.DisposeIfPresent(this.conversionStream);
					Util.DisposeIfPresent(this.unicodeStream);
					Util.DisposeIfPresent(this.disposeTracker);
				}
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000DEFA File Offset: 0x0000C0FA
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<EncodedStream>(this);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000DF02 File Offset: 0x0000C102
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000DF18 File Offset: 0x0000C118
		private void VerifyCanChangeStreamSize(int newStreamSize)
		{
			if (newStreamSize < 0)
			{
				throw new ArgumentOutOfRangeException("newStreamSize", string.Format("Stream size cannot be negative. Requested size = {0}.", newStreamSize));
			}
			if (newStreamSize > this.resourceTracker.StreamSizeLimit)
			{
				throw new RopExecutionException(string.Format("Attempted to allocate a stream that is larger than the allowed size. StreamSizeLimit = {0}. Requested stream size = {1}.", this.resourceTracker.StreamSizeLimit, newStreamSize), ErrorCode.MaxSubmissionExceeded);
			}
			if (!this.resourceTracker.TryReserveMemory(newStreamSize - this.reservedMemory))
			{
				throw new RopExecutionException(string.Format("Attempted to use more memory than allowed. Reserve memory request = {0}.", newStreamSize - this.reservedMemory), ErrorCode.MaxSubmissionExceeded);
			}
			this.reservedMemory = newStreamSize;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000DFBB File Offset: 0x0000C1BB
		private static char[] GetEncodedCharacters(MemoryStream conversionStream, Encoding encoding)
		{
			return encoding.GetChars(conversionStream.GetBuffer(), 0, (int)conversionStream.Length);
		}

		// Token: 0x04000267 RID: 615
		private readonly Stream unicodeStream;

		// Token: 0x04000268 RID: 616
		private readonly MemoryStream conversionStream;

		// Token: 0x04000269 RID: 617
		private readonly Encoding encoding;

		// Token: 0x0400026A RID: 618
		private readonly DisposeTracker disposeTracker;

		// Token: 0x0400026B RID: 619
		private readonly IResourceTracker resourceTracker;

		// Token: 0x0400026C RID: 620
		private bool isDisposed;

		// Token: 0x0400026D RID: 621
		private int reservedMemory;
	}
}
