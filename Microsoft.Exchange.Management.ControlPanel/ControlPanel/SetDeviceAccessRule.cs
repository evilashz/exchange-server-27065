using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000305 RID: 773
	[DataContract]
	public class SetDeviceAccessRule : SetObjectProperties
	{
		// Token: 0x17001E9C RID: 7836
		// (get) Token: 0x06002E43 RID: 11843 RVA: 0x0008C993 File Offset: 0x0008AB93
		public sealed override string AssociatedCmdlet
		{
			get
			{
				return "Set-ActiveSyncDeviceAccessRule";
			}
		}

		// Token: 0x17001E9D RID: 7837
		// (get) Token: 0x06002E44 RID: 11844 RVA: 0x0008C99A File Offset: 0x0008AB9A
		public override string RbacScope
		{
			get
			{
				return "@C:OrganizationConfig";
			}
		}

		// Token: 0x17001E9E RID: 7838
		// (get) Token: 0x06002E45 RID: 11845 RVA: 0x0008C9A1 File Offset: 0x0008ABA1
		// (set) Token: 0x06002E46 RID: 11846 RVA: 0x0008C9B3 File Offset: 0x0008ABB3
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
