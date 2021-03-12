using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AE6 RID: 2790
	public abstract class AddressListPagingTaskBase : SystemConfigurationObjectActionTask<OrganizationIdParameter, ExchangeConfigurationUnit>
	{
		// Token: 0x17001E0D RID: 7693
		// (get) Token: 0x06006314 RID: 25364 RVA: 0x0019E149 File Offset: 0x0019C349
		// (set) Token: 0x06006315 RID: 25365 RVA: 0x0019E160 File Offset: 0x0019C360
		[Parameter(Mandatory = false, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override OrganizationIdParameter Identity
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x06006316 RID: 25366 RVA: 0x0019E173 File Offset: 0x0019C373
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (this.Identity != null)
			{
				base.InternalValidate();
			}
			TaskLogger.LogExit();
		}
	}
}
