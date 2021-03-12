using System;

namespace Microsoft.Exchange.Data.Globalization
{
	// Token: 0x020000FB RID: 251
	internal struct SimpleCodepageDetector
	{
		// Token: 0x06000A7C RID: 2684 RVA: 0x0003E8EA File Offset: 0x0003CAEA
		public void AddData(char ch)
		{
			this.mask |= ~CodePageDetectData.codePageMask[(int)CodePageDetectData.index[(int)ch]];
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x0003E907 File Offset: 0x0003CB07
		public void AddData(char[] buffer, int offset, int count)
		{
			count++;
			while (--count != 0)
			{
				this.mask |= ~CodePageDetectData.codePageMask[(int)CodePageDetectData.index[(int)buffer[offset]]];
				offset++;
			}
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0003E93A File Offset: 0x0003CB3A
		public void AddData(string buffer, int offset, int count)
		{
			count++;
			while (--count != 0)
			{
				this.mask |= ~CodePageDetectData.codePageMask[(int)CodePageDetectData.index[(int)buffer[offset]]];
				offset++;
			}
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0003E974 File Offset: 0x0003CB74
		public int GetCodePage(int[] codePagePriorityList, bool onlyValidCodePages)
		{
			uint num = ~this.mask;
			if (onlyValidCodePages)
			{
				num &= CodePageDetect.ValidCodePagesMask;
			}
			return CodePageDetect.GetCodePage(ref num, codePagePriorityList);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x0003E99C File Offset: 0x0003CB9C
		public int GetWindowsCodePage(int[] codePagePriorityList, bool onlyValidCodePages)
		{
			uint num = ~this.mask;
			num &= 260038656U;
			if (onlyValidCodePages)
			{
				num &= CodePageDetect.ValidCodePagesMask;
			}
			if (num == 0U)
			{
				return 1252;
			}
			return CodePageDetect.GetCodePage(ref num, codePagePriorityList);
		}

		// Token: 0x04000D8A RID: 3466
		private uint mask;
	}
}
