using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001B5 RID: 437
	internal class ThrottlingPolicyOwaSettings : ThrottlingPolicyBaseSettingsWithCommonAttributes
	{
		// Token: 0x06000F40 RID: 3904 RVA: 0x0002EDF3 File Offset: 0x0002CFF3
		public ThrottlingPolicyOwaSettings()
		{
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0002EDFB File Offset: 0x0002CFFB
		private ThrottlingPolicyOwaSettings(string value) : base(value)
		{
			Unlimited<uint>? voiceMaxConcurrency = this.VoiceMaxConcurrency;
			Unlimited<uint>? voiceMaxBurst = this.VoiceMaxBurst;
			Unlimited<uint>? voiceRechargeRate = this.VoiceRechargeRate;
			Unlimited<uint>? voiceCutoffBalance = this.VoiceCutoffBalance;
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x0002EE20 File Offset: 0x0002D020
		public static ThrottlingPolicyOwaSettings Parse(string stateToParse)
		{
			return new ThrottlingPolicyOwaSettings(stateToParse);
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x0002EE28 File Offset: 0x0002D028
		// (set) Token: 0x06000F44 RID: 3908 RVA: 0x0002EE35 File Offset: 0x0002D035
		internal Unlimited<uint>? VoiceMaxConcurrency
		{
			get
			{
				return base.GetValueFromPropertyBag("VoiceMaxConcur");
			}
			set
			{
				base.SetValueInPropertyBag("VoiceMaxConcur", value);
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x0002EE43 File Offset: 0x0002D043
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x0002EE50 File Offset: 0x0002D050
		internal Unlimited<uint>? VoiceMaxBurst
		{
			get
			{
				return base.GetValueFromPropertyBag("VoiceMaxBurst");
			}
			set
			{
				base.SetValueInPropertyBag("VoiceMaxBurst", value);
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x0002EE5E File Offset: 0x0002D05E
		// (set) Token: 0x06000F48 RID: 3912 RVA: 0x0002EE6B File Offset: 0x0002D06B
		internal Unlimited<uint>? VoiceRechargeRate
		{
			get
			{
				return base.GetValueFromPropertyBag("VoiceRechargeRate");
			}
			set
			{
				base.SetValueInPropertyBag("VoiceRechargeRate", value);
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x0002EE79 File Offset: 0x0002D079
		// (set) Token: 0x06000F4A RID: 3914 RVA: 0x0002EE86 File Offset: 0x0002D086
		internal Unlimited<uint>? VoiceCutoffBalance
		{
			get
			{
				return base.GetValueFromPropertyBag("VoiceCutoff");
			}
			set
			{
				base.SetValueInPropertyBag("VoiceCutoff", value);
			}
		}

		// Token: 0x04000921 RID: 2337
		private const string VoiceMaxConcurrencyPrefix = "VoiceMaxConcur";

		// Token: 0x04000922 RID: 2338
		private const string VoiceMaxBurstPrefix = "VoiceMaxBurst";

		// Token: 0x04000923 RID: 2339
		private const string VoiceRechargeRatePrefix = "VoiceRechargeRate";

		// Token: 0x04000924 RID: 2340
		private const string VoiceCutoffBalancePrefix = "VoiceCutoff";
	}
}
