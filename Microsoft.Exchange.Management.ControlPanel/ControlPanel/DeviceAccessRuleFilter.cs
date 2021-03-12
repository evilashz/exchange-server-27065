using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000308 RID: 776
	[DataContract]
	public class DeviceAccessRuleFilter : WebServiceParameters
	{
		// Token: 0x17001EA6 RID: 7846
		// (get) Token: 0x06002E56 RID: 11862 RVA: 0x0008CA92 File Offset: 0x0008AC92
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-ActiveSyncDeviceAccessRule";
			}
		}

		// Token: 0x17001EA7 RID: 7847
		// (get) Token: 0x06002E57 RID: 11863 RVA: 0x0008CA99 File Offset: 0x0008AC99
		public override string RbacScope
		{
			get
			{
				return "@C:OrganizationConfig";
			}
		}
	}
}
