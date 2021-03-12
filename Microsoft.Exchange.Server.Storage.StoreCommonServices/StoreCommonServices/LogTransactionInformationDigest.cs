using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000096 RID: 150
	internal class LogTransactionInformationDigest : ILogTransactionInformation
	{
		// Token: 0x06000533 RID: 1331 RVA: 0x0001E1B0 File Offset: 0x0001C3B0
		public LogTransactionInformationDigest()
		{
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0001E1B8 File Offset: 0x0001C3B8
		public LogTransactionInformationDigest(Dictionary<byte, LogTransactionInformationCollector.Counter> perLogTransactionInformationBlockTypeCounter)
		{
			this.perLogTransactionInformationBlockTypeCounter = perLogTransactionInformationBlockTypeCounter;
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0001E1C8 File Offset: 0x0001C3C8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Digest:\n");
			foreach (KeyValuePair<byte, LogTransactionInformationCollector.Counter> keyValuePair in this.perLogTransactionInformationBlockTypeCounter)
			{
				stringBuilder.Append(string.Format("Block Type: {0}: counter {1}\n", (LogTransactionInformationBlockType)keyValuePair.Key, keyValuePair.Value.Value));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001E25C File Offset: 0x0001C45C
		public byte Type()
		{
			return 6;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001E260 File Offset: 0x0001C460
		public int Serialize(byte[] buffer, int offset)
		{
			int num = offset;
			if (buffer != null)
			{
				buffer[offset] = this.Type();
			}
			offset++;
			offset += SerializedValue.SerializeInt32(this.perLogTransactionInformationBlockTypeCounter.Count, buffer, offset);
			foreach (KeyValuePair<byte, LogTransactionInformationCollector.Counter> keyValuePair in this.perLogTransactionInformationBlockTypeCounter)
			{
				if (buffer != null)
				{
					buffer[offset] = keyValuePair.Key;
				}
				offset++;
				offset += SerializedValue.SerializeInt32(keyValuePair.Value.Value, buffer, offset);
			}
			return offset - num;
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001E304 File Offset: 0x0001C504
		public void Parse(byte[] buffer, ref int offset)
		{
			byte b = buffer[offset++];
			int num = SerializedValue.ParseInt32(buffer, ref offset);
			this.perLogTransactionInformationBlockTypeCounter = new Dictionary<byte, LogTransactionInformationCollector.Counter>((num > 0 && num < 10) ? num : 10);
			for (int i = 0; i < num; i++)
			{
				byte key = buffer[offset++];
				LogTransactionInformationCollector.Counter counter = new LogTransactionInformationCollector.Counter();
				counter.Value = SerializedValue.ParseInt32(buffer, ref offset);
				this.perLogTransactionInformationBlockTypeCounter.Add(key, counter);
			}
		}

		// Token: 0x040003E8 RID: 1000
		private Dictionary<byte, LogTransactionInformationCollector.Counter> perLogTransactionInformationBlockTypeCounter;
	}
}
