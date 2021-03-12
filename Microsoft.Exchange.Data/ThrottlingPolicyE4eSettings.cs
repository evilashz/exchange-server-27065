using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001B0 RID: 432
	internal class ThrottlingPolicyE4eSettings : ThrottlingPolicyBaseSettings
	{
		// Token: 0x06000EED RID: 3821 RVA: 0x0002E8CD File Offset: 0x0002CACD
		public ThrottlingPolicyE4eSettings()
		{
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0002E8D8 File Offset: 0x0002CAD8
		private ThrottlingPolicyE4eSettings(string value) : base(value)
		{
			Unlimited<uint>? encryptionSenderMaxConcurrency = this.EncryptionSenderMaxConcurrency;
			Unlimited<uint>? encryptionSenderMaxBurst = this.EncryptionSenderMaxBurst;
			Unlimited<uint>? encryptionSenderRechargeRate = this.EncryptionSenderRechargeRate;
			Unlimited<uint>? encryptionSenderCutoffBalance = this.EncryptionSenderCutoffBalance;
			Unlimited<uint>? encryptionRecipientMaxConcurrency = this.EncryptionRecipientMaxConcurrency;
			Unlimited<uint>? encryptionRecipientMaxBurst = this.EncryptionRecipientMaxBurst;
			Unlimited<uint>? encryptionRecipientRechargeRate = this.EncryptionRecipientRechargeRate;
			Unlimited<uint>? encryptionRecipientCutoffBalance = this.EncryptionRecipientCutoffBalance;
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0002E924 File Offset: 0x0002CB24
		public static ThrottlingPolicyE4eSettings Parse(string stateToParse)
		{
			return new ThrottlingPolicyE4eSettings(stateToParse);
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0002E92C File Offset: 0x0002CB2C
		// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x0002E939 File Offset: 0x0002CB39
		internal Unlimited<uint>? EncryptionSenderMaxConcurrency
		{
			get
			{
				return base.GetValueFromPropertyBag("SenderMaxConcur");
			}
			set
			{
				base.SetValueInPropertyBag("SenderMaxConcur", value);
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x0002E947 File Offset: 0x0002CB47
		// (set) Token: 0x06000EF3 RID: 3827 RVA: 0x0002E954 File Offset: 0x0002CB54
		internal Unlimited<uint>? EncryptionSenderMaxBurst
		{
			get
			{
				return base.GetValueFromPropertyBag("SenderMaxBurst");
			}
			set
			{
				base.SetValueInPropertyBag("SenderMaxBurst", value);
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x0002E962 File Offset: 0x0002CB62
		// (set) Token: 0x06000EF5 RID: 3829 RVA: 0x0002E96F File Offset: 0x0002CB6F
		internal Unlimited<uint>? EncryptionSenderRechargeRate
		{
			get
			{
				return base.GetValueFromPropertyBag("SenderRechargeRate");
			}
			set
			{
				base.SetValueInPropertyBag("SenderRechargeRate", value);
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x0002E97D File Offset: 0x0002CB7D
		// (set) Token: 0x06000EF7 RID: 3831 RVA: 0x0002E98A File Offset: 0x0002CB8A
		internal Unlimited<uint>? EncryptionSenderCutoffBalance
		{
			get
			{
				return base.GetValueFromPropertyBag("SenderCutoff");
			}
			set
			{
				base.SetValueInPropertyBag("SenderCutoff", value);
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x0002E998 File Offset: 0x0002CB98
		// (set) Token: 0x06000EF9 RID: 3833 RVA: 0x0002E9A5 File Offset: 0x0002CBA5
		internal Unlimited<uint>? EncryptionRecipientMaxConcurrency
		{
			get
			{
				return base.GetValueFromPropertyBag("RecipientMaxConcur");
			}
			set
			{
				base.SetValueInPropertyBag("RecipientMaxConcur", value);
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x0002E9B3 File Offset: 0x0002CBB3
		// (set) Token: 0x06000EFB RID: 3835 RVA: 0x0002E9C0 File Offset: 0x0002CBC0
		internal Unlimited<uint>? EncryptionRecipientMaxBurst
		{
			get
			{
				return base.GetValueFromPropertyBag("RecipientMaxBurst");
			}
			set
			{
				base.SetValueInPropertyBag("RecipientMaxBurst", value);
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0002E9CE File Offset: 0x0002CBCE
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x0002E9DB File Offset: 0x0002CBDB
		internal Unlimited<uint>? EncryptionRecipientRechargeRate
		{
			get
			{
				return base.GetValueFromPropertyBag("RecipientRechargeRate");
			}
			set
			{
				base.SetValueInPropertyBag("RecipientRechargeRate", value);
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x0002E9E9 File Offset: 0x0002CBE9
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x0002E9F6 File Offset: 0x0002CBF6
		internal Unlimited<uint>? EncryptionRecipientCutoffBalance
		{
			get
			{
				return base.GetValueFromPropertyBag("RecipientCutoff");
			}
			set
			{
				base.SetValueInPropertyBag("RecipientCutoff", value);
			}
		}

		// Token: 0x040008FF RID: 2303
		internal const string SenderConcurrencyPrefix = "SenderMaxConcur";

		// Token: 0x04000900 RID: 2304
		internal const string SenderMaxBurstPrefix = "SenderMaxBurst";

		// Token: 0x04000901 RID: 2305
		internal const string SenderRechargeRatePrefix = "SenderRechargeRate";

		// Token: 0x04000902 RID: 2306
		internal const string SenderCutoffBalancePrefix = "SenderCutoff";

		// Token: 0x04000903 RID: 2307
		internal const string RecipientConcurrencyPrefix = "RecipientMaxConcur";

		// Token: 0x04000904 RID: 2308
		internal const string RecipientMaxBurstPrefix = "RecipientMaxBurst";

		// Token: 0x04000905 RID: 2309
		internal const string RecipientRechargeRatePrefix = "RecipientRechargeRate";

		// Token: 0x04000906 RID: 2310
		internal const string RecipientCutoffBalancePrefix = "RecipientCutoff";
	}
}
