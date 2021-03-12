using System;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000010 RID: 16
	internal sealed class UsAsciiCoder : CoderBase
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000382C File Offset: 0x00001A2C
		public override CodingScheme CodingScheme
		{
			get
			{
				return CodingScheme.UsAscii;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003830 File Offset: 0x00001A30
		public override int GetCodedRadixCount(char ch)
		{
			int num = Convert.ToInt32(ch);
			if (128 <= num)
			{
				return 0;
			}
			return 1;
		}
	}
}
