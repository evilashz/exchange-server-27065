using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.TenantMonitoring;

namespace Microsoft.Exchange.ProvisioningMonitoring
{
	// Token: 0x02000209 RID: 521
	internal sealed class PerTenantCmdletHealthCounters : CmdletHealthCounters
	{
		// Token: 0x06001226 RID: 4646 RVA: 0x000390DA File Offset: 0x000372DA
		internal PerTenantCmdletHealthCounters(string name, string orgName, string hostName, CounterType attempts, CounterType successes, CounterType iterationAttempts, CounterType iterationSuccesses)
		{
			this.cmdletName = name;
			this.instanceName = ProvisioningMonitoringConfig.GetInstanceName(hostName, orgName);
			this.counterForAttempts = attempts;
			this.counterForSuccesses = successes;
			this.counterForIterationAttempts = iterationAttempts;
			this.counterForIterationSuccesses = iterationSuccesses;
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00039116 File Offset: 0x00037316
		internal override void IncrementInvocationCount()
		{
			TenantMonitor.LogActivity(this.counterForAttempts, this.instanceName);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00039129 File Offset: 0x00037329
		internal override void UpdateSuccessCount(ErrorRecord errorRecord)
		{
			if (errorRecord == null || ProvisioningMonitoringConfig.IsExceptionWhiteListedForCmdlet(errorRecord, this.cmdletName))
			{
				TenantMonitor.LogActivity(this.counterForSuccesses, this.instanceName);
			}
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x0003914D File Offset: 0x0003734D
		internal override void IncrementIterationInvocationCount()
		{
			TenantMonitor.LogActivity(this.counterForIterationAttempts, this.instanceName);
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00039160 File Offset: 0x00037360
		internal override void UpdateIterationSuccessCount(ErrorRecord errorRecord)
		{
			if (errorRecord == null || ProvisioningMonitoringConfig.IsExceptionWhiteListedForCmdlet(errorRecord, this.cmdletName))
			{
				TenantMonitor.LogActivity(this.counterForIterationSuccesses, this.instanceName);
			}
		}

		// Token: 0x04000455 RID: 1109
		private string cmdletName;

		// Token: 0x04000456 RID: 1110
		private string instanceName;

		// Token: 0x04000457 RID: 1111
		private CounterType counterForAttempts;

		// Token: 0x04000458 RID: 1112
		private CounterType counterForSuccesses;

		// Token: 0x04000459 RID: 1113
		private CounterType counterForIterationAttempts;

		// Token: 0x0400045A RID: 1114
		private CounterType counterForIterationSuccesses;
	}
}
