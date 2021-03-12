using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004F5 RID: 1269
	internal class SmtpInBdatStreamBuilder : SmtpInStreamBuilderBase
	{
		// Token: 0x06003A90 RID: 14992 RVA: 0x000F35C5 File Offset: 0x000F17C5
		public SmtpInBdatStreamBuilder()
		{
			base.EohPos = -1L;
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x06003A91 RID: 14993 RVA: 0x000F35D5 File Offset: 0x000F17D5
		// (set) Token: 0x06003A92 RID: 14994 RVA: 0x000F35DD File Offset: 0x000F17DD
		public long ChunkSize { get; set; }

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x06003A93 RID: 14995 RVA: 0x000F35E6 File Offset: 0x000F17E6
		public override bool IsEodSeen
		{
			get
			{
				return base.TotalBytesRead >= this.ChunkSize;
			}
		}

		// Token: 0x06003A94 RID: 14996 RVA: 0x000F35F9 File Offset: 0x000F17F9
		public override void Reset()
		{
			base.Reset();
			this.ChunkSize = 0L;
		}

		// Token: 0x06003A95 RID: 14997 RVA: 0x000F360C File Offset: 0x000F180C
		public override bool Write(byte[] data, int offset, int numBytes, out int numBytesConsumed)
		{
			if (numBytes < 0)
			{
				throw new LocalizedException(Strings.SmtpReceiveParserNegativeBytes);
			}
			long num = this.ChunkSize - base.TotalBytesRead;
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
			return base.TotalBytesRead >= this.ChunkSize;
		}
	}
}
