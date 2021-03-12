using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200007D RID: 125
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class KeyedPair<T, U> : Pair<T, U>
	{
		// Token: 0x0600032B RID: 811 RVA: 0x00014020 File Offset: 0x00012220
		public KeyedPair(T first, U second) : base(first, second)
		{
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0001402C File Offset: 0x0001222C
		public override int GetHashCode()
		{
			T first = base.First;
			return first.GetHashCode();
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00014050 File Offset: 0x00012250
		public override bool Equals(object obj)
		{
			KeyedPair<T, U> keyedPair = obj as KeyedPair<T, U>;
			return keyedPair != null && object.Equals(keyedPair.First, base.First);
		}
	}
}
