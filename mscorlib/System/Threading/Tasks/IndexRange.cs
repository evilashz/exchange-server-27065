using System;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000525 RID: 1317
	[StructLayout(LayoutKind.Auto)]
	internal struct IndexRange
	{
		// Token: 0x04001A15 RID: 6677
		internal long m_nFromInclusive;

		// Token: 0x04001A16 RID: 6678
		internal long m_nToExclusive;

		// Token: 0x04001A17 RID: 6679
		internal volatile Shared<long> m_nSharedCurrentIndexOffset;

		// Token: 0x04001A18 RID: 6680
		internal int m_bRangeFinished;
	}
}
