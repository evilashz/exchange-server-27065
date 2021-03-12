using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001AD RID: 429
	internal abstract class ThrottlingPolicyBaseSettingsWithCommonAttributes : ThrottlingPolicyBaseSettings
	{
		// Token: 0x06000E0C RID: 3596 RVA: 0x0002D948 File Offset: 0x0002BB48
		protected ThrottlingPolicyBaseSettingsWithCommonAttributes()
		{
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x0002D950 File Offset: 0x0002BB50
		protected ThrottlingPolicyBaseSettingsWithCommonAttributes(string value) : base(value)
		{
			Unlimited<uint>? maxConcurrency = this.MaxConcurrency;
			Unlimited<uint>? maxBurst = this.MaxBurst;
			Unlimited<uint>? rechargeRate = this.RechargeRate;
			Unlimited<uint>? cutoffBalance = this.CutoffBalance;
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0002D975 File Offset: 0x0002BB75
		// (set) Token: 0x06000E0F RID: 3599 RVA: 0x0002D982 File Offset: 0x0002BB82
		internal Unlimited<uint>? MaxConcurrency
		{
			get
			{
				return base.GetValueFromPropertyBag("MaxConcur");
			}
			set
			{
				base.SetValueInPropertyBag("MaxConcur", value);
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0002D990 File Offset: 0x0002BB90
		// (set) Token: 0x06000E11 RID: 3601 RVA: 0x0002D99D File Offset: 0x0002BB9D
		internal Unlimited<uint>? MaxBurst
		{
			get
			{
				return base.GetValueFromPropertyBag("MaxBurst");
			}
			set
			{
				base.SetValueInPropertyBag("MaxBurst", value);
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0002D9AB File Offset: 0x0002BBAB
		// (set) Token: 0x06000E13 RID: 3603 RVA: 0x0002D9B8 File Offset: 0x0002BBB8
		internal Unlimited<uint>? RechargeRate
		{
			get
			{
				return base.GetValueFromPropertyBag("RechargeRate");
			}
			set
			{
				base.SetValueInPropertyBag("RechargeRate", value);
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0002D9C6 File Offset: 0x0002BBC6
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x0002D9D3 File Offset: 0x0002BBD3
		internal Unlimited<uint>? CutoffBalance
		{
			get
			{
				return base.GetValueFromPropertyBag("Cutoff");
			}
			set
			{
				base.SetValueInPropertyBag("Cutoff", value);
			}
		}

		// Token: 0x0400089C RID: 2204
		internal const string ConcurrencyPrefix = "MaxConcur";

		// Token: 0x0400089D RID: 2205
		internal const string MaxBurstPrefix = "MaxBurst";

		// Token: 0x0400089E RID: 2206
		internal const string RechargeRatePrefix = "RechargeRate";

		// Token: 0x0400089F RID: 2207
		internal const string CutoffBalancePrefix = "Cutoff";
	}
}
