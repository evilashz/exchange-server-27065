using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x0200083A RID: 2106
	public class GetProvisioningReconciliationConfigCommand : SyntheticCommandWithPipelineInput<ProvisioningReconciliationConfig, ProvisioningReconciliationConfig>
	{
		// Token: 0x060068EC RID: 26860 RVA: 0x0009FA2E File Offset: 0x0009DC2E
		private GetProvisioningReconciliationConfigCommand() : base("Get-ProvisioningReconciliationConfig")
		{
		}

		// Token: 0x060068ED RID: 26861 RVA: 0x0009FA3B File Offset: 0x0009DC3B
		public GetProvisioningReconciliationConfigCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}
	}
}
