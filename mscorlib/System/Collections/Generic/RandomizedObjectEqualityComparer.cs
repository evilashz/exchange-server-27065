using System;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x0200049D RID: 1181
	internal sealed class RandomizedObjectEqualityComparer : IEqualityComparer, IWellKnownStringEqualityComparer
	{
		// Token: 0x06003993 RID: 14739 RVA: 0x000DAE0A File Offset: 0x000D900A
		public RandomizedObjectEqualityComparer()
		{
			this._entropy = HashHelpers.GetEntropy();
		}

		// Token: 0x06003994 RID: 14740 RVA: 0x000DAE1D File Offset: 0x000D901D
		public bool Equals(object x, object y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x000DAE38 File Offset: 0x000D9038
		[SecuritySafeCritical]
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			string text = obj as string;
			if (text != null)
			{
				return string.InternalMarvin32HashString(text, text.Length, this._entropy);
			}
			return obj.GetHashCode();
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x000DAE70 File Offset: 0x000D9070
		public override bool Equals(object obj)
		{
			RandomizedObjectEqualityComparer randomizedObjectEqualityComparer = obj as RandomizedObjectEqualityComparer;
			return randomizedObjectEqualityComparer != null && this._entropy == randomizedObjectEqualityComparer._entropy;
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x000DAE97 File Offset: 0x000D9097
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode() ^ (int)(this._entropy & 2147483647L);
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x000DAEB8 File Offset: 0x000D90B8
		IEqualityComparer IWellKnownStringEqualityComparer.GetRandomizedEqualityComparer()
		{
			return new RandomizedObjectEqualityComparer();
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x000DAEBF File Offset: 0x000D90BF
		IEqualityComparer IWellKnownStringEqualityComparer.GetEqualityComparerForSerialization()
		{
			return null;
		}

		// Token: 0x04001897 RID: 6295
		private long _entropy;
	}
}
