using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x0200028C RID: 652
	internal class EasDeviceThrottlingPolicy : SingleComponentThrottlingPolicy
	{
		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x0008D301 File Offset: 0x0008B501
		// (set) Token: 0x060017F5 RID: 6133 RVA: 0x0008D309 File Offset: 0x0008B509
		public float Percentage { get; private set; }

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060017F6 RID: 6134 RVA: 0x0008D312 File Offset: 0x0008B512
		// (set) Token: 0x060017F7 RID: 6135 RVA: 0x0008D31A File Offset: 0x0008B51A
		public string DeviceId { get; private set; }

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060017F8 RID: 6136 RVA: 0x0008D323 File Offset: 0x0008B523
		// (set) Token: 0x060017F9 RID: 6137 RVA: 0x0008D32B File Offset: 0x0008B52B
		public string DeviceType { get; private set; }

		// Token: 0x060017FA RID: 6138 RVA: 0x0008D334 File Offset: 0x0008B534
		public EasDeviceThrottlingPolicy(IThrottlingPolicy innerPolicy, string deviceId, string deviceType, float percentage) : base(BudgetType.Eas, innerPolicy)
		{
			ArgumentValidator.ThrowIfNull("innerPolicy", innerPolicy);
			ArgumentValidator.ThrowIfNullOrEmpty("deviceId", deviceId);
			ArgumentValidator.ThrowIfNullOrEmpty("deviceType", deviceType);
			if (percentage <= 0f || percentage > 1f)
			{
				throw new ArgumentOutOfRangeException("percentage", percentage, "Percentage must be > 0 and <= 1");
			}
			this.DeviceId = deviceId;
			this.DeviceType = deviceType;
			this.Percentage = percentage;
			this.cachedIdentity = string.Format("{0}-{1}-{2}-{3}-{4}", new object[]
			{
				innerPolicy.GetShortIdentityString(),
				this.DeviceId,
				this.DeviceType,
				this.Percentage,
				TimeProvider.UtcNow
			});
			this.cutoffBalance = this.GetFactoredValue(innerPolicy.EasCutoffBalance);
			this.maxBurst = this.GetFactoredValue(innerPolicy.EasMaxBurst);
			this.rechargeRate = innerPolicy.EasRechargeRate;
			this.maxConcurrency = innerPolicy.EasMaxConcurrency;
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x0008D433 File Offset: 0x0008B633
		public override Unlimited<uint> CutoffBalance
		{
			get
			{
				return this.cutoffBalance;
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x0008D43B File Offset: 0x0008B63B
		public override Unlimited<uint> MaxBurst
		{
			get
			{
				return this.maxBurst;
			}
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x0008D443 File Offset: 0x0008B643
		public override Unlimited<uint> MaxConcurrency
		{
			get
			{
				return this.maxConcurrency;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x0008D44B File Offset: 0x0008B64B
		public override Unlimited<uint> RechargeRate
		{
			get
			{
				return this.rechargeRate;
			}
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x0008D453 File Offset: 0x0008B653
		private Unlimited<uint> GetFactoredValue(Unlimited<uint> fullValue)
		{
			if (fullValue.IsUnlimited)
			{
				return fullValue;
			}
			return (uint)Math.Ceiling((double)(fullValue.Value * this.Percentage));
		}

		// Token: 0x04000EA9 RID: 3753
		private const string IdentityFormat = "{0}-{1}-{2}-{3}-{4}";

		// Token: 0x04000EAA RID: 3754
		private readonly string cachedIdentity;

		// Token: 0x04000EAB RID: 3755
		private readonly Unlimited<uint> cutoffBalance;

		// Token: 0x04000EAC RID: 3756
		private readonly Unlimited<uint> rechargeRate;

		// Token: 0x04000EAD RID: 3757
		private readonly Unlimited<uint> maxBurst;

		// Token: 0x04000EAE RID: 3758
		private readonly Unlimited<uint> maxConcurrency;
	}
}
