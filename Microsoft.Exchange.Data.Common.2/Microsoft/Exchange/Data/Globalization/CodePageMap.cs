using System;
using System.Text;

namespace Microsoft.Exchange.Data.Globalization
{
	// Token: 0x02000101 RID: 257
	internal class CodePageMap : CodePageMapData
	{
		// Token: 0x06000A88 RID: 2696 RVA: 0x0005EB90 File Offset: 0x0005CD90
		public bool ChoseCodePage(int codePage)
		{
			if (codePage == this.codePage)
			{
				return true;
			}
			this.codePage = codePage;
			this.ranges = null;
			if (codePage == 1200)
			{
				return true;
			}
			for (int i = CodePageMapData.codePages.Length - 1; i >= 0; i--)
			{
				if ((int)CodePageMapData.codePages[i].cpid == codePage)
				{
					this.ranges = CodePageMapData.codePages[i].ranges;
					this.lastRangeIndex = this.ranges.Length / 2;
					this.lastRange = this.ranges[this.lastRangeIndex];
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0005EC2C File Offset: 0x0005CE2C
		public bool ChoseCodePage(Encoding encoding)
		{
			return this.ChoseCodePage(CodePageMap.GetCodePage(encoding));
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0005EC3C File Offset: 0x0005CE3C
		public bool IsUnsafeExtendedCharacter(char ch)
		{
			if (this.ranges == null)
			{
				return false;
			}
			if (ch <= (char)this.lastRange.last)
			{
				if (ch >= (char)this.lastRange.first)
				{
					return this.lastRange.offset != ushort.MaxValue && (CodePageMapData.bitmap[(int)(this.lastRange.offset + (ushort)(ch - (char)this.lastRange.first))] & this.lastRange.mask) == 0;
				}
				int num = this.lastRangeIndex;
				while (--num >= 0)
				{
					if (ch >= (char)this.ranges[num].first)
					{
						if (ch > (char)this.ranges[num].last)
						{
							break;
						}
						if (ch == (char)this.ranges[num].first)
						{
							return false;
						}
						this.lastRangeIndex = num;
						this.lastRange = this.ranges[num];
						return this.lastRange.offset != ushort.MaxValue && (CodePageMapData.bitmap[(int)(this.lastRange.offset + (ushort)(ch - (char)this.lastRange.first))] & this.lastRange.mask) == 0;
					}
				}
			}
			else
			{
				int num2 = this.lastRangeIndex;
				while (++num2 < this.ranges.Length)
				{
					if (ch <= (char)this.ranges[num2].last)
					{
						if (ch < (char)this.ranges[num2].first)
						{
							break;
						}
						if (ch == (char)this.ranges[num2].first)
						{
							return false;
						}
						this.lastRangeIndex = num2;
						this.lastRange = this.ranges[num2];
						return this.lastRange.offset != ushort.MaxValue && (CodePageMapData.bitmap[(int)(this.lastRange.offset + (ushort)(ch - (char)this.lastRange.first))] & this.lastRange.mask) == 0;
					}
				}
			}
			return true;
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0005EE3A File Offset: 0x0005D03A
		public static Encoding GetEncoding(int codePage)
		{
			return Encoding.GetEncoding(codePage);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0005EE42 File Offset: 0x0005D042
		public static int GetCodePage(Encoding encoding)
		{
			return encoding.CodePage;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0005EE4A File Offset: 0x0005D04A
		public static int GetWindowsCodePage(Encoding encoding)
		{
			return encoding.WindowsCodePage;
		}

		// Token: 0x04000DA4 RID: 3492
		private int codePage;

		// Token: 0x04000DA5 RID: 3493
		private CodePageMapData.CodePageRange[] ranges;

		// Token: 0x04000DA6 RID: 3494
		private int lastRangeIndex;

		// Token: 0x04000DA7 RID: 3495
		private CodePageMapData.CodePageRange lastRange;
	}
}
