using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200005D RID: 93
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class Pair<T, U>
	{
		// Token: 0x06000712 RID: 1810 RVA: 0x0003841F File Offset: 0x0003661F
		internal Pair(T first, U second)
		{
			this.First = first;
			this.Second = second;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00038438 File Offset: 0x00036638
		public override int GetHashCode()
		{
			T first = this.First;
			int hashCode = first.GetHashCode();
			U second = this.Second;
			return hashCode ^ second.GetHashCode();
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x00038470 File Offset: 0x00036670
		public override bool Equals(object obj)
		{
			if (obj is Pair<T, U>)
			{
				Pair<T, U> pair = (Pair<T, U>)obj;
				return object.Equals(pair.First, this.First) && object.Equals(pair.Second, this.Second);
			}
			return false;
		}

		// Token: 0x040001E2 RID: 482
		internal readonly T First;

		// Token: 0x040001E3 RID: 483
		internal readonly U Second;
	}
}
