using System;
using System.Collections.Generic;

namespace Microsoft.Office.Datacenter.ActiveMonitoring.Local
{
	// Token: 0x0200007D RID: 125
	internal class KeyComparer<TKey> : EqualityComparer<TKey>
	{
		// Token: 0x060006D5 RID: 1749 RVA: 0x0001C900 File Offset: 0x0001AB00
		public override bool Equals(TKey key1, TKey key2)
		{
			if (typeof(TKey) == typeof(string))
			{
				return string.Equals(key1 as string, key2 as string, StringComparison.OrdinalIgnoreCase);
			}
			return EqualityComparer<TKey>.Default.Equals(key1, key2);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001C951 File Offset: 0x0001AB51
		public override int GetHashCode(TKey key)
		{
			if (typeof(TKey) == typeof(string))
			{
				return (key as string).ToLower().GetHashCode();
			}
			return key.GetHashCode();
		}
	}
}
