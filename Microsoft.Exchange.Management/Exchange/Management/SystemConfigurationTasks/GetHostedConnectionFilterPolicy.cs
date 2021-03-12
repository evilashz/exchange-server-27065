using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A3C RID: 2620
	[Cmdlet("Get", "HostedConnectionFilterPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetHostedConnectionFilterPolicy : GetMultitenancySystemConfigurationObjectTask<HostedConnectionFilterPolicyIdParameter, HostedConnectionFilterPolicy>
	{
		// Token: 0x17001C10 RID: 7184
		// (get) Token: 0x06005D86 RID: 23942 RVA: 0x00189C8F File Offset: 0x00187E8F
		// (set) Token: 0x06005D87 RID: 23943 RVA: 0x00189C97 File Offset: 0x00187E97
		[Parameter]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17001C11 RID: 7185
		// (get) Token: 0x06005D88 RID: 23944 RVA: 0x00189CA0 File Offset: 0x00187EA0
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C12 RID: 7186
		// (get) Token: 0x06005D89 RID: 23945 RVA: 0x00189CA3 File Offset: 0x00187EA3
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Dehydrateable;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}
	}
}
