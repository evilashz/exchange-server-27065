using System;
using System.Security;

namespace System.Collections.Generic
{
	// Token: 0x0200049C RID: 1180
	internal sealed class RandomizedStringEqualityComparer : IEqualityComparer<string>, IEqualityComparer, IWellKnownStringEqualityComparer
	{
		// Token: 0x0600398A RID: 14730 RVA: 0x000DACFC File Offset: 0x000D8EFC
		public RandomizedStringEqualityComparer()
		{
			this._entropy = HashHelpers.GetEntropy();
		}

		// Token: 0x0600398B RID: 14731 RVA: 0x000DAD0F File Offset: 0x000D8F0F
		public bool Equals(object x, object y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x is string && y is string)
			{
				return this.Equals((string)x, (string)y);
			}
			ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArgumentForComparison);
			return false;
		}

		// Token: 0x0600398C RID: 14732 RVA: 0x000DAD49 File Offset: 0x000D8F49
		public bool Equals(string x, string y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x0600398D RID: 14733 RVA: 0x000DAD61 File Offset: 0x000D8F61
		[SecuritySafeCritical]
		public int GetHashCode(string obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return string.InternalMarvin32HashString(obj, obj.Length, this._entropy);
		}

		// Token: 0x0600398E RID: 14734 RVA: 0x000DAD7C File Offset: 0x000D8F7C
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

		// Token: 0x0600398F RID: 14735 RVA: 0x000DADB4 File Offset: 0x000D8FB4
		public override bool Equals(object obj)
		{
			RandomizedStringEqualityComparer randomizedStringEqualityComparer = obj as RandomizedStringEqualityComparer;
			return randomizedStringEqualityComparer != null && this._entropy == randomizedStringEqualityComparer._entropy;
		}

		// Token: 0x06003990 RID: 14736 RVA: 0x000DADDB File Offset: 0x000D8FDB
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode() ^ (int)(this._entropy & 2147483647L);
		}

		// Token: 0x06003991 RID: 14737 RVA: 0x000DADFC File Offset: 0x000D8FFC
		IEqualityComparer IWellKnownStringEqualityComparer.GetRandomizedEqualityComparer()
		{
			return new RandomizedStringEqualityComparer();
		}

		// Token: 0x06003992 RID: 14738 RVA: 0x000DAE03 File Offset: 0x000D9003
		IEqualityComparer IWellKnownStringEqualityComparer.GetEqualityComparerForSerialization()
		{
			return EqualityComparer<string>.Default;
		}

		// Token: 0x04001896 RID: 6294
		private long _entropy;
	}
}
