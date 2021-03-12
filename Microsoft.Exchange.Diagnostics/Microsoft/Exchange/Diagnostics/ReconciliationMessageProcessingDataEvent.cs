using System;
using Microsoft.Exchange.Conversion;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000A5 RID: 165
	public class ReconciliationMessageProcessingDataEvent : IPerfEventData
	{
		// Token: 0x06000416 RID: 1046 RVA: 0x0000EE60 File Offset: 0x0000D060
		public ReconciliationMessageProcessingDataEvent()
		{
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000EE68 File Offset: 0x0000D068
		public ReconciliationMessageProcessingDataEvent(string mailbox, DateTime sentTime, string messageId)
		{
			this.mailbox = mailbox;
			this.sentTime = sentTime.ToFileTime();
			this.messageId = messageId;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000EE8C File Offset: 0x0000D08C
		public void FromBytes(byte[] data)
		{
			int num = 0;
			this.sentTime = BitConverter.ToInt64(data, num);
			num += 8;
			this.messageId = ExBitConverter.ReadAsciiString(data, num);
			num += this.messageId.Length + 1;
			this.mailbox = ExBitConverter.ReadAsciiString(data, num);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000EED8 File Offset: 0x0000D0D8
		public byte[] ToBytes()
		{
			byte[] array = new byte[this.mailbox.Length + this.messageId.Length + 10];
			int num = 0;
			num += ExBitConverter.Write(this.sentTime, array, num);
			num += ExBitConverter.Write(this.messageId, false, array, num);
			num += ExBitConverter.Write(this.mailbox, false, array, num);
			return array;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000EF3C File Offset: 0x0000D13C
		public string[] ToCsvRecord()
		{
			return new string[]
			{
				DateTime.FromFileTimeUtc(this.sentTime).ToString(),
				this.messageId.ToString(),
				this.mailbox.ToString()
			};
		}

		// Token: 0x04000339 RID: 825
		private long sentTime;

		// Token: 0x0400033A RID: 826
		private string mailbox;

		// Token: 0x0400033B RID: 827
		private string messageId;
	}
}
