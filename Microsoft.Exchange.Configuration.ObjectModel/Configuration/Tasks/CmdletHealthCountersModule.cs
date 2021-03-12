using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Configuration.Core;
using Microsoft.Exchange.Configuration.TenantMonitoring;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ProvisioningMonitoring;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000274 RID: 628
	internal class CmdletHealthCountersModule : ITaskModule, ICriticalFeature
	{
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x0005079E File Offset: 0x0004E99E
		// (set) Token: 0x060015A5 RID: 5541 RVA: 0x000507A6 File Offset: 0x0004E9A6
		private protected TaskContext CurrentTaskContext { protected get; private set; }

		// Token: 0x060015A6 RID: 5542 RVA: 0x000507AF File Offset: 0x0004E9AF
		public CmdletHealthCountersModule(TaskContext context)
		{
			this.CurrentTaskContext = context;
		}

		// Token: 0x060015A7 RID: 5543 RVA: 0x000507BE File Offset: 0x0004E9BE
		bool ICriticalFeature.IsCriticalException(Exception ex)
		{
			return false;
		}

		// Token: 0x060015A8 RID: 5544 RVA: 0x000507C1 File Offset: 0x0004E9C1
		public void Init(ITaskEvent task)
		{
			task.IterateCompleted += this.Task_IterateCompleted;
			task.Error += this.Task_Error;
			task.Release += this.Task_Release;
		}

		// Token: 0x060015A9 RID: 5545 RVA: 0x000507F9 File Offset: 0x0004E9F9
		public void Dispose()
		{
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x000507FB File Offset: 0x0004E9FB
		protected virtual CounterType CounterTypeForAttempts
		{
			get
			{
				return CounterType.CmdletAttempts;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x060015AB RID: 5547 RVA: 0x000507FF File Offset: 0x0004E9FF
		protected virtual CounterType CounterTypeForSuccesses
		{
			get
			{
				return CounterType.CmdletSuccesses;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x060015AC RID: 5548 RVA: 0x00050803 File Offset: 0x0004EA03
		protected virtual CounterType CounterTypeForIterationAttempts
		{
			get
			{
				return CounterType.CmdletIterationAttempts;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x060015AD RID: 5549 RVA: 0x00050807 File Offset: 0x0004EA07
		protected virtual CounterType CounterTypeForIterationSuccesses
		{
			get
			{
				return CounterType.CmdletIterationSuccesses;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x060015AE RID: 5550 RVA: 0x0005080C File Offset: 0x0004EA0C
		protected virtual string TenantNameForMonitoringCounters
		{
			get
			{
				if (this.CurrentTaskContext.UserInfo == null)
				{
					return string.Empty;
				}
				OrganizationId currentOrganizationId = this.CurrentTaskContext.UserInfo.CurrentOrganizationId;
				if (!(currentOrganizationId == null))
				{
					return currentOrganizationId.GetFriendlyName();
				}
				return string.Empty;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060015AF RID: 5551 RVA: 0x00050854 File Offset: 0x0004EA54
		private CmdletHealthCounters CmdletHealthCounters
		{
			get
			{
				if (this.cmdletHealthCounters == null)
				{
					if (ProvisioningMonitoringConfig.IsCmdletMonitoringEnabled && this.CurrentTaskContext.InvocationInfo != null && this.CurrentTaskContext.ExchangeRunspaceConfig != null && ProvisioningMonitoringConfig.IsCmdletMonitored(this.CurrentTaskContext.InvocationInfo.CommandName) && ProvisioningMonitoringConfig.IsHostMonitored(this.HostApplicationForMonitoring) && (this.CurrentTaskContext.ExchangeRunspaceConfig == null || this.CurrentTaskContext.ExchangeRunspaceConfig.ConfigurationSettings == null || ProvisioningMonitoringConfig.IsClientApplicationMonitored(this.CurrentTaskContext.ExchangeRunspaceConfig.ConfigurationSettings.ClientApplication)))
					{
						this.cmdletHealthCounters = this.GetCmdletHealthCounters();
					}
					else
					{
						this.cmdletHealthCounters = ProvisioningMonitoringConfig.NullCmdletHealthCounters;
					}
				}
				return this.cmdletHealthCounters;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x00050910 File Offset: 0x0004EB10
		private string HostApplicationForMonitoring
		{
			get
			{
				if (this.CurrentTaskContext.ExchangeRunspaceConfig != null && this.CurrentTaskContext.ExchangeRunspaceConfig.ConfigurationSettings.ClientApplication == ExchangeRunspaceConfigurationSettings.ExchangeApplication.SimpleDataMigration)
				{
					return this.CurrentTaskContext.ExchangeRunspaceConfig.ConfigurationSettings.ClientApplication.ToString();
				}
				string result = "";
				if (this.CurrentTaskContext.InvocationInfo != null)
				{
					result = this.CurrentTaskContext.InvocationInfo.ShellHostName;
				}
				return result;
			}
		}

		// Token: 0x060015B1 RID: 5553 RVA: 0x00050988 File Offset: 0x0004EB88
		private void Task_Release(object sender, EventArgs e)
		{
			this.IncrementSuccessCount(null);
			this.IncrementInvocationCount();
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00050998 File Offset: 0x0004EB98
		private void Task_Error(object sender, GenericEventArg<TaskErrorEventArg> e)
		{
			if (e.Data.ExceptionHandled)
			{
				return;
			}
			ErrorRecord errorRecord = Task.CreateErrorRecord(this.CurrentTaskContext.ErrorInfo);
			if (this.CurrentTaskContext.Stage == TaskStage.ProcessRecord)
			{
				this.CmdletHealthCounters.UpdateIterationSuccessCount(errorRecord);
				return;
			}
			this.IncrementSuccessCount(errorRecord);
			this.IncrementInvocationCount();
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x000509EC File Offset: 0x0004EBEC
		private void Task_IterateCompleted(object sender, EventArgs e)
		{
			this.CmdletHealthCounters.IncrementIterationInvocationCount();
			if (this.CurrentTaskContext.ErrorInfo != null && !this.CurrentTaskContext.ErrorInfo.HasErrors)
			{
				this.CmdletHealthCounters.UpdateIterationSuccessCount(null);
			}
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x00050A24 File Offset: 0x0004EC24
		private CmdletHealthCounters GetCmdletHealthCounters()
		{
			return new PerTenantCmdletHealthCounters(this.CurrentTaskContext.InvocationInfo.CommandName, this.TenantNameForMonitoringCounters, this.HostApplicationForMonitoring, this.CounterTypeForAttempts, this.CounterTypeForSuccesses, this.CounterTypeForIterationAttempts, this.CounterTypeForIterationSuccesses);
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00050A5F File Offset: 0x0004EC5F
		private void IncrementInvocationCount()
		{
			if (!this.invocationCountIncremented)
			{
				this.CmdletHealthCounters.IncrementInvocationCount();
				this.invocationCountIncremented = true;
			}
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00050A7B File Offset: 0x0004EC7B
		private void IncrementSuccessCount(ErrorRecord errorRecord)
		{
			if (!this.successCountIncremented)
			{
				this.CmdletHealthCounters.UpdateSuccessCount(errorRecord);
				this.successCountIncremented = true;
			}
		}

		// Token: 0x0400069E RID: 1694
		private CmdletHealthCounters cmdletHealthCounters;

		// Token: 0x0400069F RID: 1695
		private bool invocationCountIncremented;

		// Token: 0x040006A0 RID: 1696
		private bool successCountIncremented;
	}
}
