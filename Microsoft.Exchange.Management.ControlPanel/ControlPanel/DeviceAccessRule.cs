using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000304 RID: 772
	[DataContract]
	[KnownType(typeof(DeviceAccessRule))]
	public class DeviceAccessRule : DeviceAccessRuleRow
	{
		// Token: 0x06002E3C RID: 11836 RVA: 0x0008C834 File Offset: 0x0008AA34
		public DeviceAccessRule(ActiveSyncDeviceAccessRule rule) : base(rule)
		{
			switch (base.ActiveSyncDeviceAccessRule.Characteristic)
			{
			case DeviceAccessCharacteristic.DeviceType:
				this.deviceTypeQueryString = new DeviceAccessRuleQueryString
				{
					QueryString = base.ActiveSyncDeviceAccessRule.QueryString
				};
				this.deviceModelQueryString = new DeviceAccessRuleQueryString
				{
					IsWildcard = true,
					QueryString = Strings.DeviceModelPickerAll
				};
				return;
			case DeviceAccessCharacteristic.DeviceModel:
				this.deviceTypeQueryString = new DeviceAccessRuleQueryString
				{
					IsWildcard = true,
					QueryString = Strings.DeviceTypePickerAll
				};
				this.deviceModelQueryString = new DeviceAccessRuleQueryString
				{
					QueryString = base.ActiveSyncDeviceAccessRule.QueryString
				};
				return;
			case DeviceAccessCharacteristic.DeviceOS:
			case DeviceAccessCharacteristic.UserAgent:
				this.deviceTypeQueryString = new DeviceAccessRuleQueryString
				{
					IsWildcard = true,
					QueryString = Strings.DeviceTypePickerAll
				};
				this.deviceModelQueryString = new DeviceAccessRuleQueryString
				{
					IsWildcard = true,
					QueryString = Strings.DeviceTypePickerAll
				};
				return;
			default:
				throw new FaultException(Strings.InvalidDeviceAccessCharacteristic);
			}
		}

		// Token: 0x17001E99 RID: 7833
		// (get) Token: 0x06002E3D RID: 11837 RVA: 0x0008C957 File Offset: 0x0008AB57
		// (set) Token: 0x06002E3E RID: 11838 RVA: 0x0008C95F File Offset: 0x0008AB5F
		[DataMember]
		public DeviceAccessRuleQueryString DeviceTypeQueryString
		{
			get
			{
				return this.deviceTypeQueryString;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E9A RID: 7834
		// (get) Token: 0x06002E3F RID: 11839 RVA: 0x0008C966 File Offset: 0x0008AB66
		// (set) Token: 0x06002E40 RID: 11840 RVA: 0x0008C96E File Offset: 0x0008AB6E
		[DataMember]
		public DeviceAccessRuleQueryString DeviceModelQueryString
		{
			get
			{
				return this.deviceModelQueryString;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E9B RID: 7835
		// (get) Token: 0x06002E41 RID: 11841 RVA: 0x0008C975 File Offset: 0x0008AB75
		// (set) Token: 0x06002E42 RID: 11842 RVA: 0x0008C98C File Offset: 0x0008AB8C
		[DataMember]
		public string AccessLevel
		{
			get
			{
				return base.ActiveSyncDeviceAccessRule.AccessLevel.ToString();
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04002283 RID: 8835
		private DeviceAccessRuleQueryString deviceTypeQueryString;

		// Token: 0x04002284 RID: 8836
		private DeviceAccessRuleQueryString deviceModelQueryString;
	}
}
