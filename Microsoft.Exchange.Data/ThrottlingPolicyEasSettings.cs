using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001B1 RID: 433
	internal class ThrottlingPolicyEasSettings : ThrottlingPolicyBaseSettingsWithCommonAttributes
	{
		// Token: 0x06000F00 RID: 3840 RVA: 0x0002EA04 File Offset: 0x0002CC04
		public ThrottlingPolicyEasSettings()
		{
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0002EA0C File Offset: 0x0002CC0C
		private ThrottlingPolicyEasSettings(string value) : base(value)
		{
			Unlimited<uint>? maxDevices = this.MaxDevices;
			Unlimited<uint>? maxDeviceDeletesPerMonth = this.MaxDeviceDeletesPerMonth;
			Unlimited<uint>? maxInactivityForDeviceCleanup = this.MaxInactivityForDeviceCleanup;
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0002EA2A File Offset: 0x0002CC2A
		public static ThrottlingPolicyEasSettings Parse(string stateToParse)
		{
			return new ThrottlingPolicyEasSettings(stateToParse);
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000F03 RID: 3843 RVA: 0x0002EA32 File Offset: 0x0002CC32
		// (set) Token: 0x06000F04 RID: 3844 RVA: 0x0002EA3F File Offset: 0x0002CC3F
		internal Unlimited<uint>? MaxDevices
		{
			get
			{
				return base.GetValueFromPropertyBag("MaxDevices");
			}
			set
			{
				base.SetValueInPropertyBag("MaxDevices", value);
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x0002EA4D File Offset: 0x0002CC4D
		// (set) Token: 0x06000F06 RID: 3846 RVA: 0x0002EA5A File Offset: 0x0002CC5A
		internal Unlimited<uint>? MaxDeviceDeletesPerMonth
		{
			get
			{
				return base.GetValueFromPropertyBag("MaxDeviceDeletesPerMonth");
			}
			set
			{
				base.SetValueInPropertyBag("MaxDeviceDeletesPerMonth", value);
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x0002EA68 File Offset: 0x0002CC68
		// (set) Token: 0x06000F08 RID: 3848 RVA: 0x0002EA75 File Offset: 0x0002CC75
		internal Unlimited<uint>? MaxInactivityForDeviceCleanup
		{
			get
			{
				return base.GetValueFromPropertyBag("MaxInactivityForDeviceCleanup");
			}
			set
			{
				base.SetValueInPropertyBag("MaxInactivityForDeviceCleanup", value);
			}
		}

		// Token: 0x04000907 RID: 2311
		private const string MaxDevicesPrefix = "MaxDevices";

		// Token: 0x04000908 RID: 2312
		private const string MaxDeviceDeletesPerMonthPrefix = "MaxDeviceDeletesPerMonth";

		// Token: 0x04000909 RID: 2313
		private const string MaxInactivityForDeviceCleanupPrefix = "MaxInactivityForDeviceCleanup";
	}
}
