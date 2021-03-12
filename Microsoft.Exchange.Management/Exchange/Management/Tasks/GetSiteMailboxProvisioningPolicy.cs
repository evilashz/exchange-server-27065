using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000457 RID: 1111
	[Cmdlet("Get", "SiteMailboxProvisioningPolicy", DefaultParameterSetName = "Identity")]
	public sealed class GetSiteMailboxProvisioningPolicy : GetMailboxPolicyBase<TeamMailboxProvisioningPolicy>
	{
		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x06002758 RID: 10072 RVA: 0x0009B886 File Offset: 0x00099A86
		// (set) Token: 0x06002759 RID: 10073 RVA: 0x0009B88E File Offset: 0x00099A8E
		[Parameter(Mandatory = false)]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x0600275A RID: 10074 RVA: 0x0009B897 File Offset: 0x00099A97
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Static;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}
	}
}
