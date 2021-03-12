using System;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200000E RID: 14
	internal sealed class GsmDefaultCoder : CoderBase
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000380A File Offset: 0x00001A0A
		public override CodingScheme CodingScheme
		{
			get
			{
				return CodingScheme.GsmDefault;
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x0000380D File Offset: 0x00001A0D
		public override int GetCodedRadixCount(char ch)
		{
			return UnicodeToGsmMap.GetUnicodeToGsmRadixCount(ch, true);
		}
	}
}
