using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001B2 RID: 434
	internal class ThrottlingPolicyEwsSettings : ThrottlingPolicyBaseSettingsWithCommonAttributes
	{
		// Token: 0x06000F09 RID: 3849 RVA: 0x0002EA83 File Offset: 0x0002CC83
		public ThrottlingPolicyEwsSettings()
		{
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0002EA8C File Offset: 0x0002CC8C
		private ThrottlingPolicyEwsSettings(string value) : base(value)
		{
			Unlimited<uint>? maxSubscriptions = this.MaxSubscriptions;
			Unlimited<uint>? outlookServiceMaxConcurrency = this.OutlookServiceMaxConcurrency;
			Unlimited<uint>? outlookServiceMaxBurst = this.OutlookServiceMaxBurst;
			Unlimited<uint>? outlookServiceRechargeRate = this.OutlookServiceRechargeRate;
			Unlimited<uint>? outlookServiceCutoffBalance = this.OutlookServiceCutoffBalance;
			Unlimited<uint>? outlookServiceMaxSubscriptions = this.OutlookServiceMaxSubscriptions;
			Unlimited<uint>? outlookServiceMaxSocketConnectionsPerDevice = this.OutlookServiceMaxSocketConnectionsPerDevice;
			Unlimited<uint>? outlookServiceMaxSocketConnectionsPerUser = this.OutlookServiceMaxSocketConnectionsPerUser;
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0002EAD8 File Offset: 0x0002CCD8
		public static ThrottlingPolicyEwsSettings Parse(string stateToParse)
		{
			return new ThrottlingPolicyEwsSettings(stateToParse);
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x0002EAE0 File Offset: 0x0002CCE0
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x0002EAED File Offset: 0x0002CCED
		internal Unlimited<uint>? MaxSubscriptions
		{
			get
			{
				return base.GetValueFromPropertyBag("MaxSub");
			}
			set
			{
				base.SetValueInPropertyBag("MaxSub", value);
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x0002EAFB File Offset: 0x0002CCFB
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x0002EB08 File Offset: 0x0002CD08
		internal Unlimited<uint>? OutlookServiceMaxConcurrency
		{
			get
			{
				return base.GetValueFromPropertyBag("OutlookServiceMaxConcur");
			}
			set
			{
				base.SetValueInPropertyBag("OutlookServiceMaxConcur", value);
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x0002EB16 File Offset: 0x0002CD16
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x0002EB23 File Offset: 0x0002CD23
		internal Unlimited<uint>? OutlookServiceMaxBurst
		{
			get
			{
				return base.GetValueFromPropertyBag("OutlookServiceMaxBurst");
			}
			set
			{
				base.SetValueInPropertyBag("OutlookServiceMaxBurst", value);
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0002EB31 File Offset: 0x0002CD31
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x0002EB3E File Offset: 0x0002CD3E
		internal Unlimited<uint>? OutlookServiceRechargeRate
		{
			get
			{
				return base.GetValueFromPropertyBag("OutlookServiceRechargeRate");
			}
			set
			{
				base.SetValueInPropertyBag("OutlookServiceRechargeRate", value);
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x0002EB4C File Offset: 0x0002CD4C
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x0002EB59 File Offset: 0x0002CD59
		internal Unlimited<uint>? OutlookServiceCutoffBalance
		{
			get
			{
				return base.GetValueFromPropertyBag("OutlookServiceCutoff");
			}
			set
			{
				base.SetValueInPropertyBag("OutlookServiceCutoff", value);
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x0002EB67 File Offset: 0x0002CD67
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x0002EB74 File Offset: 0x0002CD74
		internal Unlimited<uint>? OutlookServiceMaxSubscriptions
		{
			get
			{
				return base.GetValueFromPropertyBag("OutlookServiceMaxSub");
			}
			set
			{
				base.SetValueInPropertyBag("OutlookServiceMaxSub", value);
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x0002EB82 File Offset: 0x0002CD82
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x0002EB8F File Offset: 0x0002CD8F
		internal Unlimited<uint>? OutlookServiceMaxSocketConnectionsPerDevice
		{
			get
			{
				return base.GetValueFromPropertyBag("OutlookServiceMaxSocketConDevice");
			}
			set
			{
				base.SetValueInPropertyBag("OutlookServiceMaxSocketConDevice", value);
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x0002EB9D File Offset: 0x0002CD9D
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x0002EBAA File Offset: 0x0002CDAA
		internal Unlimited<uint>? OutlookServiceMaxSocketConnectionsPerUser
		{
			get
			{
				return base.GetValueFromPropertyBag("OutlookServiceMaxSocketConUser");
			}
			set
			{
				base.SetValueInPropertyBag("OutlookServiceMaxSocketConUser", value);
			}
		}

		// Token: 0x0400090A RID: 2314
		private const string MaxSubscriptionsPrefix = "MaxSub";

		// Token: 0x0400090B RID: 2315
		private const string OutlookServiceMaxConcurrencyPrefix = "OutlookServiceMaxConcur";

		// Token: 0x0400090C RID: 2316
		private const string OutlookServiceMaxBurstPrefix = "OutlookServiceMaxBurst";

		// Token: 0x0400090D RID: 2317
		private const string OutlookServiceRechargeRatePrefix = "OutlookServiceRechargeRate";

		// Token: 0x0400090E RID: 2318
		private const string OutlookServiceCutoffBalancePrefix = "OutlookServiceCutoff";

		// Token: 0x0400090F RID: 2319
		private const string OutlookServiceMaxSubscriptionsPrefix = "OutlookServiceMaxSub";

		// Token: 0x04000910 RID: 2320
		private const string OutlookServiceMaxSocketConnectionsPerDevicePrefix = "OutlookServiceMaxSocketConDevice";

		// Token: 0x04000911 RID: 2321
		private const string OutlookServiceMaxSocketConnectionsPerUserPrefix = "OutlookServiceMaxSocketConUser";
	}
}
