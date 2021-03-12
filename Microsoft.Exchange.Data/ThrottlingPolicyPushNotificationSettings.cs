using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001B8 RID: 440
	internal class ThrottlingPolicyPushNotificationSettings : ThrottlingPolicyBaseSettingsWithCommonAttributes
	{
		// Token: 0x06000F6D RID: 3949 RVA: 0x0002F0B0 File Offset: 0x0002D2B0
		public ThrottlingPolicyPushNotificationSettings()
		{
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0002F0B8 File Offset: 0x0002D2B8
		private ThrottlingPolicyPushNotificationSettings(string value) : base(value)
		{
			Unlimited<uint>? pushNotificationMaxBurstPerDevice = this.PushNotificationMaxBurstPerDevice;
			Unlimited<uint>? pushNotificationRechargeRatePerDevice = this.PushNotificationRechargeRatePerDevice;
			Unlimited<uint>? pushNotificationSamplingPeriodPerDevice = this.PushNotificationSamplingPeriodPerDevice;
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0002F0D6 File Offset: 0x0002D2D6
		public static ThrottlingPolicyPushNotificationSettings Parse(string stateToParse)
		{
			return new ThrottlingPolicyPushNotificationSettings(stateToParse);
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0002F0DE File Offset: 0x0002D2DE
		// (set) Token: 0x06000F71 RID: 3953 RVA: 0x0002F0EB File Offset: 0x0002D2EB
		internal Unlimited<uint>? PushNotificationMaxBurstPerDevice
		{
			get
			{
				return base.GetValueFromPropertyBag("PushNotificationMaximumLimitPerDevice");
			}
			set
			{
				base.SetValueInPropertyBag("PushNotificationMaximumLimitPerDevice", value);
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0002F0F9 File Offset: 0x0002D2F9
		// (set) Token: 0x06000F73 RID: 3955 RVA: 0x0002F106 File Offset: 0x0002D306
		internal Unlimited<uint>? PushNotificationRechargeRatePerDevice
		{
			get
			{
				return base.GetValueFromPropertyBag("PushNotificationRechargeRatePerDevice");
			}
			set
			{
				base.SetValueInPropertyBag("PushNotificationRechargeRatePerDevice", value);
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0002F114 File Offset: 0x0002D314
		// (set) Token: 0x06000F75 RID: 3957 RVA: 0x0002F121 File Offset: 0x0002D321
		internal Unlimited<uint>? PushNotificationSamplingPeriodPerDevice
		{
			get
			{
				return base.GetValueFromPropertyBag("PushNotificationSamplingPeriodPerDevice");
			}
			set
			{
				base.SetValueInPropertyBag("PushNotificationSamplingPeriodPerDevice", value);
			}
		}

		// Token: 0x04000933 RID: 2355
		internal const string PushNotificationMaxBurstPerDevicePrefix = "PushNotificationMaximumLimitPerDevice";

		// Token: 0x04000934 RID: 2356
		internal const string PushNotificationRechargeRatePerDevicePrefix = "PushNotificationRechargeRatePerDevice";

		// Token: 0x04000935 RID: 2357
		internal const string PushNotificationSamplingPeriodPerDevicePrefix = "PushNotificationSamplingPeriodPerDevice";
	}
}
