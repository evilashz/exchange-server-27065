using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000303 RID: 771
	[DataContract]
	public class DeviceAccessRuleRow : BaseRow
	{
		// Token: 0x06002E31 RID: 11825 RVA: 0x0008C710 File Offset: 0x0008A910
		public DeviceAccessRuleRow(ActiveSyncDeviceAccessRule rule) : base(rule)
		{
			this.ActiveSyncDeviceAccessRule = rule;
		}

		// Token: 0x17001E94 RID: 7828
		// (get) Token: 0x06002E32 RID: 11826 RVA: 0x0008C720 File Offset: 0x0008A920
		// (set) Token: 0x06002E33 RID: 11827 RVA: 0x0008C728 File Offset: 0x0008A928
		public ActiveSyncDeviceAccessRule ActiveSyncDeviceAccessRule { get; set; }

		// Token: 0x17001E95 RID: 7829
		// (get) Token: 0x06002E34 RID: 11828 RVA: 0x0008C731 File Offset: 0x0008A931
		// (set) Token: 0x06002E35 RID: 11829 RVA: 0x0008C73E File Offset: 0x0008A93E
		[DataMember]
		public string Name
		{
			get
			{
				return this.ActiveSyncDeviceAccessRule.Name;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E96 RID: 7830
		// (get) Token: 0x06002E36 RID: 11830 RVA: 0x0008C745 File Offset: 0x0008A945
		// (set) Token: 0x06002E37 RID: 11831 RVA: 0x0008C752 File Offset: 0x0008A952
		[DataMember]
		public string QueryString
		{
			get
			{
				return this.ActiveSyncDeviceAccessRule.QueryString;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E97 RID: 7831
		// (get) Token: 0x06002E38 RID: 11832 RVA: 0x0008C75C File Offset: 0x0008A95C
		// (set) Token: 0x06002E39 RID: 11833 RVA: 0x0008C7C8 File Offset: 0x0008A9C8
		[DataMember]
		public string CharacteristicDescription
		{
			get
			{
				switch (this.ActiveSyncDeviceAccessRule.Characteristic)
				{
				case DeviceAccessCharacteristic.DeviceType:
					return Strings.DeviceAccessRulesDeviceType;
				case DeviceAccessCharacteristic.DeviceModel:
					return Strings.DeviceAccessRulesDeviceModel;
				case DeviceAccessCharacteristic.DeviceOS:
					return Strings.DeviceAccessRulesDeviceOS;
				case DeviceAccessCharacteristic.UserAgent:
					return Strings.DeviceAccessRulesUserAgent;
				default:
					throw new FaultException(Strings.InvalidDeviceAccessCharacteristic);
				}
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001E98 RID: 7832
		// (get) Token: 0x06002E3A RID: 11834 RVA: 0x0008C7D0 File Offset: 0x0008A9D0
		// (set) Token: 0x06002E3B RID: 11835 RVA: 0x0008C82D File Offset: 0x0008AA2D
		[DataMember]
		public string AccessLevelDescription
		{
			get
			{
				switch (this.ActiveSyncDeviceAccessRule.AccessLevel)
				{
				case DeviceAccessLevel.Allow:
					return Strings.DeviceAccessRulesAllow;
				case DeviceAccessLevel.Block:
					return Strings.DeviceAccessRulesBlock;
				case DeviceAccessLevel.Quarantine:
					return Strings.DeviceAccessRulesQuarantine;
				default:
					throw new FaultException(Strings.InvalidAccessLevel);
				}
			}
			private set
			{
				throw new NotSupportedException();
			}
		}
	}
}
