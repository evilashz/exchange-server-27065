using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000010 RID: 16
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class Pair<T, U>
	{
		// Token: 0x0600004D RID: 77 RVA: 0x000024F4 File Offset: 0x000006F4
		public Pair(T first, U second)
		{
			this.first = first;
			this.second = second;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000250A File Offset: 0x0000070A
		public T First
		{
			get
			{
				return this.first;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002512 File Offset: 0x00000712
		public U Second
		{
			get
			{
				return this.second;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000251C File Offset: 0x0000071C
		public override int GetHashCode()
		{
			T t = this.first;
			int hashCode = t.GetHashCode();
			U u = this.second;
			return hashCode ^ u.GetHashCode();
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002554 File Offset: 0x00000754
		public override bool Equals(object obj)
		{
			if (obj is Pair<T, U>)
			{
				Pair<T, U> pair = (Pair<T, U>)obj;
				return object.Equals(pair.First, this.First) && object.Equals(pair.Second, this.Second);
			}
			return false;
		}

		// Token: 0x04000039 RID: 57
		private readonly T first;

		// Token: 0x0400003A RID: 58
		private readonly U second;
	}
}
