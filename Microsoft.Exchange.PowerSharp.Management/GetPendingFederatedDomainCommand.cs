using System;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000685 RID: 1669
	public class GetPendingFederatedDomainCommand : SyntheticCommandWithPipelineInput<PendingFederatedDomain, PendingFederatedDomain>
	{
		// Token: 0x060058D5 RID: 22741 RVA: 0x0008B0D7 File Offset: 0x000892D7
		private GetPendingFederatedDomainCommand() : base("Get-PendingFederatedDomain")
		{
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x0008B0E4 File Offset: 0x000892E4
		public GetPendingFederatedDomainCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}
	}
}
