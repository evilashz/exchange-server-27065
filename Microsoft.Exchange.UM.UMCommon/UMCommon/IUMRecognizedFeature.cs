using System;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000B6 RID: 182
	internal interface IUMRecognizedFeature
	{
		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000667 RID: 1639
		string Name { get; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000668 RID: 1640
		string Value { get; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000669 RID: 1641
		int FirstWordIndex { get; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600066A RID: 1642
		int CountOfWords { get; }
	}
}
