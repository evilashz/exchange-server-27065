using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x0200001A RID: 26
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class Pair<T, U>
	{
		// Token: 0x06000065 RID: 101 RVA: 0x000036F7 File Offset: 0x000018F7
		internal Pair(T first, U second)
		{
			this.First = first;
			this.Second = second;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00003710 File Offset: 0x00001910
		public override int GetHashCode()
		{
			T first = this.First;
			int hashCode = first.GetHashCode();
			U second = this.Second;
			return hashCode ^ second.GetHashCode();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003748 File Offset: 0x00001948
		public override bool Equals(object obj)
		{
			if (obj is Pair<T, U>)
			{
				Pair<T, U> pair = (Pair<T, U>)obj;
				return object.Equals(pair.First, this.First) && object.Equals(pair.Second, this.Second);
			}
			return false;
		}

		// Token: 0x04000033 RID: 51
		internal readonly T First;

		// Token: 0x04000034 RID: 52
		internal readonly U Second;
	}
}
