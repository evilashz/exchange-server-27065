using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000306 RID: 774
	[DataContract]
	public class NewDeviceAccessRule : SetObjectProperties
	{
		// Token: 0x17001E9F RID: 7839
		// (get) Token: 0x06002E48 RID: 11848 RVA: 0x0008C9C9 File Offset: 0x0008ABC9
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "New-ActiveSyncDeviceAccessRule";
			}
		}

		// Token: 0x17001EA0 RID: 7840
		// (get) Token: 0x06002E49 RID: 11849 RVA: 0x0008C9D0 File Offset: 0x0008ABD0
		public override string RbacScope
		{
			get
			{
				return "@C:OrganizationConfig";
			}
		}

		// Token: 0x17001EA1 RID: 7841
		// (get) Token: 0x06002E4A RID: 11850 RVA: 0x0008C9D7 File Offset: 0x0008ABD7
		// (set) Token: 0x06002E4B RID: 11851 RVA: 0x0008C9DF File Offset: 0x0008ABDF
		[DataMember]
		public DeviceAccessRuleQueryString DeviceTypeQueryString { get; set; }

		// Token: 0x17001EA2 RID: 7842
		// (get) Token: 0x06002E4C RID: 11852 RVA: 0x0008C9E8 File Offset: 0x0008ABE8
		// (set) Token: 0x06002E4D RID: 11853 RVA: 0x0008C9F0 File Offset: 0x0008ABF0
		[DataMember]
		public DeviceAccessRuleQueryString DeviceModelQueryString { get; set; }

		// Token: 0x17001EA3 RID: 7843
		// (get) Token: 0x06002E4E RID: 11854 RVA: 0x0008C9F9 File Offset: 0x0008ABF9
		// (set) Token: 0x06002E4F RID: 11855 RVA: 0x0008CA0B File Offset: 0x0008AC0B
		[DataMember]
		public string AccessLevel
		{
			get
			{
				return (string)base[ActiveSyncDeviceAccessRuleSchema.AccessLevel];
			}
			set
			{
				base[ActiveSyncDeviceAccessRuleSchema.AccessLevel] = value;
			}
		}
	}
}
