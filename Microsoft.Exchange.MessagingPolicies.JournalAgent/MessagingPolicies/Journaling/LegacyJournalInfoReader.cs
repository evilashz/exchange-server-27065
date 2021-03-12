using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
{
	// Token: 0x02000028 RID: 40
	internal class LegacyJournalInfoReader
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000C9F7 File Offset: 0x0000ABF7
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x0000C9FF File Offset: 0x0000ABFF
		public LegacyJournalInfoReader(byte[] data, int start, int length)
		{
			this.data = data;
			this.currentIndex = start;
			this.endIndex = start + length;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000CA1E File Offset: 0x0000AC1E
		private bool ReadNextValue()
		{
			if (this.currentIndex >= this.endIndex)
			{
				return false;
			}
			this.value = null;
			this.value = this.ReadUTF8String();
			return true;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000CA44 File Offset: 0x0000AC44
		private int ReadInt32()
		{
			if (this.currentIndex > this.endIndex - 4)
			{
				throw new TransportPropertyException("TruncatedData");
			}
			int result = BitConverter.ToInt32(this.data, this.currentIndex);
			this.currentIndex += 4;
			return result;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000CA90 File Offset: 0x0000AC90
		private string ReadUTF8String()
		{
			int num = this.ReadInt32();
			string result = null;
			if (num < 1 || num > this.endIndex - this.currentIndex)
			{
				throw new TransportPropertyException("invalid string length prefix");
			}
			int num2 = Array.IndexOf<byte>(this.data, 0, this.currentIndex, num);
			if (num2 == -1 || num2 != this.currentIndex + num - 1)
			{
				throw new TransportPropertyException("string is not null-terminated");
			}
			try
			{
				result = LegacyJournalInfoReader.CheckedUTF8.GetString(this.data, this.currentIndex, num - 1);
			}
			catch (DecoderFallbackException innerException)
			{
				throw new TransportPropertyException("invalid encoding", innerException);
			}
			this.currentIndex += num;
			return result;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000CB40 File Offset: 0x0000AD40
		public List<string> GetStringProperties()
		{
			List<string> list = new List<string>();
			while (this.ReadNextValue())
			{
				list.Add(this.Value);
			}
			return list;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000CB6C File Offset: 0x0000AD6C
		public List<ProxyAddress> GetProxyAddressProperties()
		{
			List<ProxyAddress> list = new List<ProxyAddress>();
			while (this.ReadNextValue())
			{
				ProxyAddress proxyAddress = ProxyAddress.Parse(this.Value);
				if (proxyAddress is InvalidProxyAddress)
				{
					throw new TransportPropertyException("invalid proxy address");
				}
				list.Add(proxyAddress);
				this.ReadNextValue();
			}
			return list;
		}

		// Token: 0x040000D5 RID: 213
		private static readonly Encoding CheckedUTF8 = new UTF8Encoding(false, true);

		// Token: 0x040000D6 RID: 214
		private byte[] data;

		// Token: 0x040000D7 RID: 215
		private int currentIndex;

		// Token: 0x040000D8 RID: 216
		private int endIndex;

		// Token: 0x040000D9 RID: 217
		private string value;
	}
}
