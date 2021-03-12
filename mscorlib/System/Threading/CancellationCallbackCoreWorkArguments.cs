using System;

namespace System.Threading
{
	// Token: 0x02000519 RID: 1305
	internal struct CancellationCallbackCoreWorkArguments
	{
		// Token: 0x06003E49 RID: 15945 RVA: 0x000E76A4 File Offset: 0x000E58A4
		public CancellationCallbackCoreWorkArguments(SparselyPopulatedArrayFragment<CancellationCallbackInfo> currArrayFragment, int currArrayIndex)
		{
			this.m_currArrayFragment = currArrayFragment;
			this.m_currArrayIndex = currArrayIndex;
		}

		// Token: 0x040019F1 RID: 6641
		internal SparselyPopulatedArrayFragment<CancellationCallbackInfo> m_currArrayFragment;

		// Token: 0x040019F2 RID: 6642
		internal int m_currArrayIndex;
	}
}
