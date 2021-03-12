using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200040F RID: 1039
	internal class SmtpInBdatParser : SmtpInParser
	{
		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x000BEB42 File Offset: 0x000BCD42
		// (set) Token: 0x06002FB2 RID: 12210 RVA: 0x000BEB4A File Offset: 0x000BCD4A
		public long ChunkSize
		{
			get
			{
				return this.chunkSize;
			}
			set
			{
				this.chunkSize = value;
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06002FB3 RID: 12211 RVA: 0x000BEB53 File Offset: 0x000BCD53
		public override bool IsEodSeen
		{
			get
			{
				return base.TotalBytesRead >= this.ChunkSize;
			}
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000BEB66 File Offset: 0x000BCD66
		public override void Reset()
		{
			base.Reset();
			this.chunkSize = 0L;
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000BEB78 File Offset: 0x000BCD78
		public override bool Write(byte[] data, int offset, int numBytes, out int numBytesConsumed)
		{
			if (numBytes < 0)
			{
				throw new LocalizedException(Strings.SmtpReceiveParserNegativeBytes);
			}
			long num = this.chunkSize - base.TotalBytesRead;
			if (num <= 0L)
			{
				numBytesConsumed = 0;
				return true;
			}
			int num2 = (int)Math.Min(num, (long)numBytes);
			base.TotalBytesRead += (long)num2;
			if (!base.IsDiscardingData && num2 > 0)
			{
				base.Write(data, offset, num2);
			}
			numBytesConsumed = num2;
			return base.TotalBytesRead >= this.chunkSize;
		}

		// Token: 0x0400177B RID: 6011
		private long chunkSize;
	}
}
