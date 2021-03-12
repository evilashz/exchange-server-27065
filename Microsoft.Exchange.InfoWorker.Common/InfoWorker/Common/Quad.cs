using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x0200001C RID: 28
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class Quad<T, U, V, W>
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00003881 File Offset: 0x00001A81
		internal Quad(T first, U second, V third, W fourth)
		{
			this.First = first;
			this.Second = second;
			this.Third = third;
			this.Fourth = fourth;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000038A8 File Offset: 0x00001AA8
		public override int GetHashCode()
		{
			T first = this.First;
			int hashCode = first.GetHashCode();
			U second = this.Second;
			int num = hashCode ^ second.GetHashCode();
			V third = this.Third;
			int num2 = num ^ third.GetHashCode();
			W fourth = this.Fourth;
			return num2 ^ fourth.GetHashCode();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003908 File Offset: 0x00001B08
		public override bool Equals(object obj)
		{
			if (obj is Quad<T, U, V, W>)
			{
				Quad<T, U, V, W> quad = (Quad<T, U, V, W>)obj;
				return object.Equals(quad.First, this.First) && object.Equals(quad.Second, this.Second) && object.Equals(quad.Third, this.Third) && object.Equals(quad.Fourth, this.Fourth);
			}
			return false;
		}

		// Token: 0x04000038 RID: 56
		internal readonly T First;

		// Token: 0x04000039 RID: 57
		internal readonly U Second;

		// Token: 0x0400003A RID: 58
		internal readonly V Third;

		// Token: 0x0400003B RID: 59
		internal readonly W Fourth;
	}
}
