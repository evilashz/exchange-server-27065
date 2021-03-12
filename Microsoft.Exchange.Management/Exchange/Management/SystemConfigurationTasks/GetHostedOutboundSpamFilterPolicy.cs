using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A53 RID: 2643
	[Cmdlet("Get", "HostedOutboundSpamFilterPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetHostedOutboundSpamFilterPolicy : GetMultitenancySystemConfigurationObjectTask<HostedOutboundSpamFilterPolicyIdParameter, HostedOutboundSpamFilterPolicy>
	{
		// Token: 0x17001C8A RID: 7306
		// (get) Token: 0x06005EBB RID: 24251 RVA: 0x0018CC2E File Offset: 0x0018AE2E
		// (set) Token: 0x06005EBC RID: 24252 RVA: 0x0018CC36 File Offset: 0x0018AE36
		[Parameter]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17001C8B RID: 7307
		// (get) Token: 0x06005EBD RID: 24253 RVA: 0x0018CC3F File Offset: 0x0018AE3F
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001C8C RID: 7308
		// (get) Token: 0x06005EBE RID: 24254 RVA: 0x0018CC42 File Offset: 0x0018AE42
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
