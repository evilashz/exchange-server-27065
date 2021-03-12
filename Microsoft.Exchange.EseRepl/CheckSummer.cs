using System;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x02000028 RID: 40
	internal class CheckSummer
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00004BF8 File Offset: 0x00002DF8
		public void Sum(byte[] buf, int off, int len)
		{
			int num = off;
			int i = len;
			while (i >= 4)
			{
				uint num2 = BitConverter.ToUInt32(buf, num);
				num += 4;
				i -= 4;
				this.m_csum ^= num2;
				this.m_totalBytes += 4UL;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004C3C File Offset: 0x00002E3C
		public uint GetSum()
		{
			return this.m_csum;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004C44 File Offset: 0x00002E44
		public ulong GetTotalBytes()
		{
			return this.m_totalBytes;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004C4C File Offset: 0x00002E4C
		public void Reset()
		{
			this.m_csum = 0U;
			this.m_totalBytes = 0UL;
		}

		// Token: 0x040000BA RID: 186
		private uint m_csum;

		// Token: 0x040000BB RID: 187
		private ulong m_totalBytes;
	}
}
