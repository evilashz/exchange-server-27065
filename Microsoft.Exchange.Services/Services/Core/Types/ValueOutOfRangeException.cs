using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020008BF RID: 2239
	internal class ValueOutOfRangeException : ServicePermanentException
	{
		// Token: 0x06003F6D RID: 16237 RVA: 0x000DB63E File Offset: 0x000D983E
		public ValueOutOfRangeException() : base(CoreResources.IDs.ErrorValueOutOfRange)
		{
		}

		// Token: 0x06003F6E RID: 16238 RVA: 0x000DB650 File Offset: 0x000D9850
		public ValueOutOfRangeException(Exception innerException) : base(CoreResources.IDs.ErrorValueOutOfRange, innerException)
		{
		}

		// Token: 0x06003F6F RID: 16239 RVA: 0x000DB664 File Offset: 0x000D9864
		public ValueOutOfRangeException(Exception innerException, string[] keys, string[] values) : base(CoreResources.IDs.ErrorValueOutOfRange, innerException)
		{
			if (keys != null && values != null)
			{
				int num = 0;
				while (num < keys.Length && num < values.Length)
				{
					if (!string.IsNullOrEmpty(keys[num]) && !base.ConstantValues.ContainsKey(keys[num]))
					{
						base.ConstantValues.Add(keys[num], values[num]);
					}
					num++;
				}
			}
		}

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06003F70 RID: 16240 RVA: 0x000DB6C7 File Offset: 0x000D98C7
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}
	}
}
