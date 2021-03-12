using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000307 RID: 775
	[DataContract]
	public class NewDeviceAccessRuleData : BaseRow
	{
		// Token: 0x06002E51 RID: 11857 RVA: 0x0008CA21 File Offset: 0x0008AC21
		public NewDeviceAccessRuleData(MobileDevice device) : base(device)
		{
			this.device = device;
		}

		// Token: 0x17001EA4 RID: 7844
		// (get) Token: 0x06002E52 RID: 11858 RVA: 0x0008CA34 File Offset: 0x0008AC34
		// (set) Token: 0x06002E53 RID: 11859 RVA: 0x0008CA59 File Offset: 0x0008AC59
		[DataMember]
		public DeviceAccessRuleQueryString DeviceTypeQueryString
		{
			get
			{
				return new DeviceAccessRuleQueryString
				{
					QueryString = this.device.DeviceType
				};
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17001EA5 RID: 7845
		// (get) Token: 0x06002E54 RID: 11860 RVA: 0x0008CA60 File Offset: 0x0008AC60
		// (set) Token: 0x06002E55 RID: 11861 RVA: 0x0008CA8B File Offset: 0x0008AC8B
		[DataMember]
		public DeviceAccessRuleQueryString DeviceModelQueryString
		{
			get
			{
				return new DeviceAccessRuleQueryString
				{
					IsWildcard = true,
					QueryString = Strings.DeviceModelPickerAll
				};
			}
			private set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x04002287 RID: 8839
		private readonly MobileDevice device;
	}
}
