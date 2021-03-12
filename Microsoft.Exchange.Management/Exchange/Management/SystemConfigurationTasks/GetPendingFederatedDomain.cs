using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009E7 RID: 2535
	[Cmdlet("Get", "PendingFederatedDomain")]
	public sealed class GetPendingFederatedDomain : GetTaskBase<PendingFederatedDomain>
	{
		// Token: 0x06005A84 RID: 23172 RVA: 0x0017B148 File Offset: 0x00179348
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			ADPagedReader<AcceptedDomain> adpagedReader = this.ConfigurationSession.FindPaged<AcceptedDomain>(null, QueryScope.SubTree, null, null, 0);
			SmtpDomain pendingAccountNamespace = null;
			List<SmtpDomain> list = new List<SmtpDomain>();
			foreach (AcceptedDomain acceptedDomain in adpagedReader)
			{
				if (acceptedDomain.PendingFederatedAccountNamespace)
				{
					pendingAccountNamespace = acceptedDomain.DomainName.SmtpDomain;
				}
				else if (acceptedDomain.PendingFederatedDomain)
				{
					list.Add(acceptedDomain.DomainName.SmtpDomain);
				}
			}
			this.WriteResult(new PendingFederatedDomain(pendingAccountNamespace, list));
			TaskLogger.LogExit();
		}

		// Token: 0x06005A85 RID: 23173 RVA: 0x0017B1F0 File Offset: 0x001793F0
		protected override IConfigDataProvider CreateSession()
		{
			return null;
		}
	}
}
