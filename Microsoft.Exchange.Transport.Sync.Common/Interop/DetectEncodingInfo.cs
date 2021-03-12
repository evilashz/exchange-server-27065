using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common.Interop
{
	// Token: 0x0200007B RID: 123
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct DetectEncodingInfo
	{
		// Token: 0x040001A4 RID: 420
		public uint LangID;

		// Token: 0x040001A5 RID: 421
		public uint CodePage;

		// Token: 0x040001A6 RID: 422
		public int DocPercent;

		// Token: 0x040001A7 RID: 423
		public int Confidence;
	}
}
