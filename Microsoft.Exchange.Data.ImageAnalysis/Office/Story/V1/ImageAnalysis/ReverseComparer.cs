using System;
using System.Collections.Generic;

namespace Microsoft.Office.Story.V1.ImageAnalysis
{
	// Token: 0x0200000F RID: 15
	internal class ReverseComparer<T> : IComparer<T>
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00002E39 File Offset: 0x00001039
		public ReverseComparer(IComparer<T> comparer)
		{
			this.comparer = (comparer ?? Comparer<T>.Default);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002E51 File Offset: 0x00001051
		public int Compare(T first, T second)
		{
			return this.comparer.Compare(second, first);
		}

		// Token: 0x04000034 RID: 52
		private readonly IComparer<T> comparer;
	}
}
