using System;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200000F RID: 15
	internal sealed class UnicodeCoder : CoderBase
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000381E File Offset: 0x00001A1E
		public override CodingScheme CodingScheme
		{
			get
			{
				return CodingScheme.Unicode;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003821 File Offset: 0x00001A21
		public override int GetCodedRadixCount(char ch)
		{
			return 1;
		}
	}
}
