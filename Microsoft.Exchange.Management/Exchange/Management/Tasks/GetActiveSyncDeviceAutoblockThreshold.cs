using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200006F RID: 111
	[Cmdlet("Get", "ActiveSyncDeviceAutoblockThreshold", DefaultParameterSetName = "Identity")]
	public sealed class GetActiveSyncDeviceAutoblockThreshold : GetMultitenancySystemConfigurationObjectTask<ActiveSyncDeviceAutoblockThresholdIdParameter, ActiveSyncDeviceAutoblockThreshold>
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000E7A0 File Offset: 0x0000C9A0
		// (set) Token: 0x06000373 RID: 883 RVA: 0x0000E7A3 File Offset: 0x0000C9A3
		public override OrganizationIdParameter Organization
		{
			get
			{
				return null;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000E7AA File Offset: 0x0000C9AA
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
