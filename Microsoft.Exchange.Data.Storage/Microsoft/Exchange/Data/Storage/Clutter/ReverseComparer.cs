using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Clutter
{
	// Token: 0x02000442 RID: 1090
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ReverseComparer<T> : IComparer<T> where T : struct, IComparable<T>
	{
		// Token: 0x060030AE RID: 12462 RVA: 0x000C7D76 File Offset: 0x000C5F76
		private ReverseComparer()
		{
		}

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x060030AF RID: 12463 RVA: 0x000C7D7E File Offset: 0x000C5F7E
		public static ReverseComparer<T> Instance
		{
			get
			{
				return ReverseComparer<T>.defaultInstance;
			}
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000C7D85 File Offset: 0x000C5F85
		public int Compare(T object1, T object2)
		{
			return -((IComparable)((object)object1)).CompareTo(object2);
		}

		// Token: 0x04001A76 RID: 6774
		private static readonly ReverseComparer<T> defaultInstance = new ReverseComparer<T>();
	}
}
