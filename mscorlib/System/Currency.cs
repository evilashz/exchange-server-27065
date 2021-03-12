using System;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
	// Token: 0x020000CF RID: 207
	[Serializable]
	internal struct Currency
	{
		// Token: 0x06000CE6 RID: 3302 RVA: 0x00027586 File Offset: 0x00025786
		public Currency(decimal value)
		{
			this.m_value = decimal.ToCurrency(value).m_value;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00027599 File Offset: 0x00025799
		internal Currency(long value, int ignored)
		{
			this.m_value = value;
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x000275A2 File Offset: 0x000257A2
		public static Currency FromOACurrency(long cy)
		{
			return new Currency(cy, 0);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x000275AB File Offset: 0x000257AB
		public long ToOACurrency()
		{
			return this.m_value;
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x000275B4 File Offset: 0x000257B4
		[SecuritySafeCritical]
		public static decimal ToDecimal(Currency c)
		{
			decimal result = 0m;
			Currency.FCallToDecimal(ref result, c);
			return result;
		}

		// Token: 0x06000CEB RID: 3307
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void FCallToDecimal(ref decimal result, Currency c);

		// Token: 0x04000539 RID: 1337
		internal long m_value;
	}
}
