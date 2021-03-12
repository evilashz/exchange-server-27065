using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PushNotifications.Server.Services
{
	// Token: 0x02000025 RID: 37
	internal class TokenBucketFactory : ITokenBucketFactory
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x0000431C File Offset: 0x0000251C
		public ITokenBucket Create(Unlimited<uint> maxBurst, Unlimited<uint> rechargeRate, Unlimited<uint> rechargeInterval)
		{
			return this.Create(null, maxBurst, rechargeRate, rechargeInterval);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004328 File Offset: 0x00002528
		public ITokenBucket Create(ITokenBucket template, Unlimited<uint> maxBurst, Unlimited<uint> rechargeRate, Unlimited<uint> rechargeInterval)
		{
			if (maxBurst.IsUnlimited || rechargeInterval.IsUnlimited || rechargeRate.IsUnlimited)
			{
				return TokenBucketBoundary.Unlimited;
			}
			if (maxBurst.Value == 0U || rechargeRate.Value == 0U || rechargeInterval == 0U)
			{
				return TokenBucketBoundary.Empty;
			}
			uint num = maxBurst.Value;
			if (template != null)
			{
				num = Math.Min(num, template.CurrentBalance);
				new ExDateTime?(template.NextRecharge);
			}
			return new TokenBucket(maxBurst.Value, rechargeRate.Value, rechargeInterval.Value, num, null);
		}

		// Token: 0x04000060 RID: 96
		internal static readonly TokenBucketFactory Default = new TokenBucketFactory();
	}
}
