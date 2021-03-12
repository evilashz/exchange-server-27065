using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000322 RID: 802
	[DataContract]
	[KnownType(typeof(DeviceClassPickerObject))]
	public class DeviceClassPickerObject : BaseRow
	{
		// Token: 0x06002EC3 RID: 11971 RVA: 0x0008EE5C File Offset: 0x0008D05C
		public DeviceClassPickerObject(ActiveSyncDeviceClass deviceClass) : base(deviceClass)
		{
			this.ActiveSyncDeviceClass = deviceClass;
			this.deviceTypeQueryString = new DeviceAccessRuleQueryString
			{
				QueryString = deviceClass.DeviceType
			};
			this.deviceModelQueryString = new DeviceAccessRuleQueryString
			{
				QueryString = deviceClass.DeviceModel
			};
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x0008EEA9 File Offset: 0x0008D0A9
		public DeviceClassPickerObject(DeviceAccessRuleQueryString deviceType, DeviceAccessRuleQueryString deviceModel)
		{
			this.deviceTypeQueryString = deviceType;
			this.deviceModelQueryString = deviceModel;
		}

		// Token: 0x17001EBA RID: 7866
		// (get) Token: 0x06002EC5 RID: 11973 RVA: 0x0008EEBF File Offset: 0x0008D0BF
		// (set) Token: 0x06002EC6 RID: 11974 RVA: 0x0008EEC7 File Offset: 0x0008D0C7
		private ActiveSyncDeviceClass ActiveSyncDeviceClass { get; set; }

		// Token: 0x17001EBB RID: 7867
		// (get) Token: 0x06002EC7 RID: 11975 RVA: 0x0008EED0 File Offset: 0x0008D0D0
		// (set) Token: 0x06002EC8 RID: 11976 RVA: 0x0008EED8 File Offset: 0x0008D0D8
		[DataMember]
		public DeviceAccessRuleQueryString DeviceType
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

		// Token: 0x17001EBC RID: 7868
		// (get) Token: 0x06002EC9 RID: 11977 RVA: 0x0008EEDF File Offset: 0x0008D0DF
		// (set) Token: 0x06002ECA RID: 11978 RVA: 0x0008EEE7 File Offset: 0x0008D0E7
		[DataMember]
		public DeviceAccessRuleQueryString DeviceModel
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

		// Token: 0x17001EBD RID: 7869
		// (get) Token: 0x06002ECB RID: 11979 RVA: 0x0008EEEE File Offset: 0x0008D0EE
		// (set) Token: 0x06002ECC RID: 11980 RVA: 0x0008EEFB File Offset: 0x0008D0FB
		[DataMember]
		public string DeviceTypeString
		{
			get
			{
				return this.deviceTypeQueryString.QueryString;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001EBE RID: 7870
		// (get) Token: 0x06002ECD RID: 11981 RVA: 0x0008EF02 File Offset: 0x0008D102
		// (set) Token: 0x06002ECE RID: 11982 RVA: 0x0008EF0F File Offset: 0x0008D10F
		[DataMember]
		public string DeviceModelString
		{
			get
			{
				return this.deviceModelQueryString.QueryString;
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x040022D6 RID: 8918
		public static DeviceAccessRuleQueryString AllDeviceTypeQueryString = new DeviceAccessRuleQueryString
		{
			IsWildcard = true,
			QueryString = Strings.DeviceTypePickerAll
		};

		// Token: 0x040022D7 RID: 8919
		public static DeviceAccessRuleQueryString AllDeviceModelQueryString = new DeviceAccessRuleQueryString
		{
			IsWildcard = true,
			QueryString = Strings.DeviceModelPickerAll
		};

		// Token: 0x040022D8 RID: 8920
		private readonly DeviceAccessRuleQueryString deviceTypeQueryString;

		// Token: 0x040022D9 RID: 8921
		private readonly DeviceAccessRuleQueryString deviceModelQueryString;
	}
}
