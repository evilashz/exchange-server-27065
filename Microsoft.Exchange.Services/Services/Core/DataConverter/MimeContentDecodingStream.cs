using System;
using System.IO;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000165 RID: 357
	internal sealed class MimeContentDecodingStream : DecodingStream
	{
		// Token: 0x06000A19 RID: 2585 RVA: 0x00030ED7 File Offset: 0x0002F0D7
		public MimeContentDecodingStream(TextWriter writer) : base(writer)
		{
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00030EE0 File Offset: 0x0002F0E0
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this.leftOverBytesCount > 0)
			{
				int num = this.leftOverBytesCount;
				while (num < 3 && count > 0)
				{
					this.leftOverBytes[num++] = buffer[offset++];
					count--;
				}
				if (count == 0 && num < 3)
				{
					this.leftOverBytesCount = num;
					return;
				}
				this.writer.Write(Convert.ToBase64String(this.leftOverBytes));
			}
			this.leftOverBytesCount = count % 3;
			if (this.leftOverBytesCount > 0)
			{
				count -= this.leftOverBytesCount;
				if (this.leftOverBytes == null)
				{
					this.leftOverBytes = new byte[3];
				}
				for (int i = 0; i < this.leftOverBytesCount; i++)
				{
					this.leftOverBytes[i] = buffer[offset + count + i];
				}
			}
			this.writer.Write(Convert.ToBase64String(buffer, offset, count));
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00030FA8 File Offset: 0x0002F1A8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.leftOverBytesCount > 0)
			{
				this.writer.Write(Convert.ToBase64String(this.leftOverBytes, 0, this.leftOverBytesCount));
			}
			base.Dispose(disposing);
		}

		// Token: 0x040007AF RID: 1967
		private int leftOverBytesCount;

		// Token: 0x040007B0 RID: 1968
		private byte[] leftOverBytes;
	}
}
