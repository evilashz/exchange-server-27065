using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000011 RID: 17
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal sealed class KeyedPair<T, U> : Pair<T, U>
	{
		// Token: 0x06000052 RID: 82 RVA: 0x000025AC File Offset: 0x000007AC
		public KeyedPair(T first, U second) : base(first, second)
		{
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000025B8 File Offset: 0x000007B8
		public override int GetHashCode()
		{
			T first = base.First;
			return first.GetHashCode();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000025DC File Offset: 0x000007DC
		public override bool Equals(object obj)
		{
			KeyedPair<T, U> keyedPair = obj as KeyedPair<T, U>;
			return keyedPair != null && object.Equals(keyedPair.First, base.First);
		}
	}
}
