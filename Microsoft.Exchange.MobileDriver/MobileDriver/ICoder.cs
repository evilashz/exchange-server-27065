using System;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200000C RID: 12
	internal interface ICoder
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004B RID: 75
		CodingScheme CodingScheme { get; }

		// Token: 0x0600004C RID: 76
		CodedText Code(string str);

		// Token: 0x0600004D RID: 77
		int GetCodedRadixCount(char ch);
	}
}
