using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x0200001B RID: 27
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class Triplet<T, U, V>
	{
		// Token: 0x06000068 RID: 104 RVA: 0x000037A0 File Offset: 0x000019A0
		internal Triplet(T first, U second, V third)
		{
			this.First = first;
			this.Second = second;
			this.Third = third;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000037C0 File Offset: 0x000019C0
		public override int GetHashCode()
		{
			T first = this.First;
			int hashCode = first.GetHashCode();
			U second = this.Second;
			int num = hashCode ^ second.GetHashCode();
			V third = this.Third;
			return num ^ third.GetHashCode();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000380C File Offset: 0x00001A0C
		public override bool Equals(object obj)
		{
			if (obj is Triplet<T, U, V>)
			{
				Triplet<T, U, V> triplet = (Triplet<T, U, V>)obj;
				return object.Equals(triplet.First, this.First) && object.Equals(triplet.Second, this.Second) && object.Equals(triplet.Third, this.Third);
			}
			return false;
		}

		// Token: 0x04000035 RID: 53
		internal readonly T First;

		// Token: 0x04000036 RID: 54
		internal readonly U Second;

		// Token: 0x04000037 RID: 55
		internal readonly V Third;
	}
}
