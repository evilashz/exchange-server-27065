using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D97 RID: 3479
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ImportCalendarStream : MemoryStream
	{
		// Token: 0x060077BC RID: 30652 RVA: 0x00210AD2 File Offset: 0x0020ECD2
		public ImportCalendarStream()
		{
			this.Capacity = 16384;
		}

		// Token: 0x060077BD RID: 30653 RVA: 0x00210AF4 File Offset: 0x0020ECF4
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.CheckDisposed("Write");
			if (this.isContentCopied)
			{
				throw new NotSupportedException("Can not write after content copied to this stream.");
			}
			int num = offset;
			while (num + offset < count)
			{
				this.WriteByte(buffer[num]);
				num++;
			}
		}

		// Token: 0x060077BE RID: 30654 RVA: 0x00210B38 File Offset: 0x0020ED38
		public override void WriteByte(byte value)
		{
			this.CheckDisposed("WriteByte");
			if (this.isContentCopied)
			{
				throw new NotSupportedException("Can not write after content copied to this stream.");
			}
			if (this.lastByte != 13 && value == 10)
			{
				base.WriteByte(13);
			}
			base.WriteByte(value);
			this.lastByte = new byte?(value);
		}

		// Token: 0x060077BF RID: 30655 RVA: 0x00210BA4 File Offset: 0x0020EDA4
		public int CopyFrom(Stream stream)
		{
			this.CheckDisposed("CopyFrom");
			this.SetLength(0L);
			if (stream.CanSeek)
			{
				this.Capacity = (int)stream.Length;
				if (this.Capacity > StorageLimits.Instance.CalendarMaxNumberBytesForICalImport)
				{
					return -1;
				}
			}
			byte[] buffer = new byte[4096];
			int i = 0;
			while (i <= StorageLimits.Instance.CalendarMaxNumberBytesForICalImport)
			{
				int num = stream.Read(buffer, 0, 4096);
				this.Write(buffer, 0, num);
				i += num;
				if (num <= 0)
				{
					this.Seek(0L, SeekOrigin.Begin);
					this.isContentCopied = true;
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060077C0 RID: 30656 RVA: 0x00210C3C File Offset: 0x0020EE3C
		public override void Close()
		{
			this.isClosed = true;
			base.Close();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060077C1 RID: 30657 RVA: 0x00210C51 File Offset: 0x0020EE51
		private void CheckDisposed(string methodName)
		{
			if (this.isClosed)
			{
				StorageGlobals.TraceFailedCheckDisposed(this, methodName);
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x040052D7 RID: 21207
		private const byte LineFeed = 10;

		// Token: 0x040052D8 RID: 21208
		private const byte CarriageReturn = 13;

		// Token: 0x040052D9 RID: 21209
		private const int CopyBufferSize = 4096;

		// Token: 0x040052DA RID: 21210
		private const int DefaultCapacity = 16384;

		// Token: 0x040052DB RID: 21211
		private byte? lastByte = null;

		// Token: 0x040052DC RID: 21212
		private bool isContentCopied;

		// Token: 0x040052DD RID: 21213
		private bool isClosed;
	}
}
