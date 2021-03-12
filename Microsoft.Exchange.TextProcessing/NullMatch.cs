using System;

namespace Microsoft.Exchange.TextProcessing
{
	// Token: 0x0200003E RID: 62
	internal class NullMatch : IMatch
	{
		// Token: 0x06000209 RID: 521 RVA: 0x0000E53A File Offset: 0x0000C73A
		public bool IsMatch(TextScanContext data)
		{
			return false;
		}
	}
}
