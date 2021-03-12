using System;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000097 RID: 151
	internal class LogTransactionInformationForTestPurposes : ILogTransactionInformation
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x0001E37A File Offset: 0x0001C57A
		public LogTransactionInformationForTestPurposes()
		{
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001E384 File Offset: 0x0001C584
		public LogTransactionInformationForTestPurposes(byte bufferLength)
		{
			this.buffer = new byte[(int)bufferLength];
			for (byte b = 0; b < bufferLength; b += 1)
			{
				this.buffer[(int)b] = b;
			}
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001E3BC File Offset: 0x0001C5BC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Test block:\nbuffer: ");
			foreach (byte value in this.buffer)
			{
				stringBuilder.Append(value);
			}
			stringBuilder.Append('\n');
			return stringBuilder.ToString();
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0001E40B File Offset: 0x0001C60B
		public byte Type()
		{
			return 1;
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0001E410 File Offset: 0x0001C610
		public int Serialize(byte[] buffer, int offset)
		{
			int num = offset;
			if (buffer != null)
			{
				buffer[offset] = this.Type();
			}
			offset++;
			if (buffer != null)
			{
				buffer[offset] = (byte)this.buffer.Length;
			}
			offset++;
			if (buffer != null)
			{
				this.buffer.CopyTo(buffer, offset);
			}
			offset += this.buffer.Length;
			return offset - num;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001E464 File Offset: 0x0001C664
		public void Parse(byte[] buffer, ref int offset)
		{
			byte b = buffer[offset++];
			int num = (int)buffer[offset++];
			this.buffer = new byte[num];
			Array.Copy(buffer, offset, this.buffer, 0, num);
			offset += num;
		}

		// Token: 0x040003E9 RID: 1001
		private byte[] buffer;
	}
}
