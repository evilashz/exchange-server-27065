using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200007C RID: 124
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class Pair<T, U>
	{
		// Token: 0x06000326 RID: 806 RVA: 0x00013F68 File Offset: 0x00012168
		public Pair(T first, U second)
		{
			this.first = first;
			this.second = second;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000327 RID: 807 RVA: 0x00013F7E File Offset: 0x0001217E
		public T First
		{
			get
			{
				return this.first;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00013F86 File Offset: 0x00012186
		public U Second
		{
			get
			{
				return this.second;
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00013F90 File Offset: 0x00012190
		public override int GetHashCode()
		{
			T t = this.first;
			int hashCode = t.GetHashCode();
			U u = this.second;
			return hashCode ^ u.GetHashCode();
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00013FC8 File Offset: 0x000121C8
		public override bool Equals(object obj)
		{
			if (obj is Pair<T, U>)
			{
				Pair<T, U> pair = (Pair<T, U>)obj;
				return object.Equals(pair.First, this.First) && object.Equals(pair.Second, this.Second);
			}
			return false;
		}

		// Token: 0x040001A8 RID: 424
		private readonly T first;

		// Token: 0x040001A9 RID: 425
		private readonly U second;
	}
}
