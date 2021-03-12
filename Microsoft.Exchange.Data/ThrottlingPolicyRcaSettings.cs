using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001B9 RID: 441
	internal class ThrottlingPolicyRcaSettings : ThrottlingPolicyBaseSettingsWithCommonAttributes
	{
		// Token: 0x06000F76 RID: 3958 RVA: 0x0002F12F File Offset: 0x0002D32F
		public ThrottlingPolicyRcaSettings()
		{
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0002F137 File Offset: 0x0002D337
		private ThrottlingPolicyRcaSettings(string value) : base(value)
		{
			Unlimited<uint>? cpaMaxConcurrency = this.CpaMaxConcurrency;
			Unlimited<uint>? cpaMaxBurst = this.CpaMaxBurst;
			Unlimited<uint>? cpaRechargeRate = this.CpaRechargeRate;
			Unlimited<uint>? cpaCutoffBalance = this.CpaCutoffBalance;
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0002F15C File Offset: 0x0002D35C
		public static ThrottlingPolicyRcaSettings Parse(string stateToParse)
		{
			return new ThrottlingPolicyRcaSettings(stateToParse);
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0002F164 File Offset: 0x0002D364
		// (set) Token: 0x06000F7A RID: 3962 RVA: 0x0002F171 File Offset: 0x0002D371
		internal Unlimited<uint>? CpaMaxConcurrency
		{
			get
			{
				return base.GetValueFromPropertyBag("CpaMaxConcur");
			}
			set
			{
				base.SetValueInPropertyBag("CpaMaxConcur", value);
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0002F17F File Offset: 0x0002D37F
		// (set) Token: 0x06000F7C RID: 3964 RVA: 0x0002F18C File Offset: 0x0002D38C
		internal Unlimited<uint>? CpaMaxBurst
		{
			get
			{
				return base.GetValueFromPropertyBag("CpaMaxBurst");
			}
			set
			{
				base.SetValueInPropertyBag("CpaMaxBurst", value);
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x0002F19A File Offset: 0x0002D39A
		// (set) Token: 0x06000F7E RID: 3966 RVA: 0x0002F1A7 File Offset: 0x0002D3A7
		internal Unlimited<uint>? CpaRechargeRate
		{
			get
			{
				return base.GetValueFromPropertyBag("CpaRecharge");
			}
			set
			{
				base.SetValueInPropertyBag("CpaRecharge", value);
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0002F1B5 File Offset: 0x0002D3B5
		// (set) Token: 0x06000F80 RID: 3968 RVA: 0x0002F1C2 File Offset: 0x0002D3C2
		internal Unlimited<uint>? CpaCutoffBalance
		{
			get
			{
				return base.GetValueFromPropertyBag("CpaCutoff");
			}
			set
			{
				base.SetValueInPropertyBag("CpaCutoff", value);
			}
		}

		// Token: 0x04000936 RID: 2358
		internal const string CpaMaxConcurrencyPrefix = "CpaMaxConcur";

		// Token: 0x04000937 RID: 2359
		internal const string CpaMaxBurstPrefix = "CpaMaxBurst";

		// Token: 0x04000938 RID: 2360
		internal const string CpaRechargeRatePrefix = "CpaRecharge";

		// Token: 0x04000939 RID: 2361
		internal const string CpaCutoffBalancePrefix = "CpaCutoff";
	}
}
