using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.ServiceProcess;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200000C RID: 12
	internal sealed class MachinePingTask : ActiveDirectoryConnectivityBase
	{
		// Token: 0x06000077 RID: 119 RVA: 0x0000348C File Offset: 0x0000168C
		internal MachinePingTask(ActiveDirectoryConnectivityContext context) : base(context)
		{
			base.CanContinue = !context.Instance.SkipRemainingTests;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003584 File Offset: 0x00001784
		protected override IEnumerable<AsyncResult<ActiveDirectoryConnectivityOutcome>> BuildTransactions()
		{
			yield return ActiveDirectoryConnectivityBase.SingleCommandTransactionSync(new ActiveDirectoryConnectivityBase.ActiveDirectoryConnectivityTask(this.MachinePing));
			yield break;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000035A1 File Offset: 0x000017A1
		protected override void InternalDispose(bool isDisposing)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000035A8 File Offset: 0x000017A8
		private ActiveDirectoryConnectivityOutcome IsNTDSRunning()
		{
			ActiveDirectoryConnectivityOutcome activeDirectoryConnectivityOutcome = base.CreateOutcome(TestActiveDirectoryConnectivityTask.ScenarioId.IsNTDSRunning, Strings.IsNTDSRunningScenario, Strings.IsNTDSRunningScenario, base.Context.CurrentDomainController);
			if (!base.CanContinue)
			{
				base.WriteVerbose(Strings.CannotContinue(Strings.IsNTDSRunningScenario));
				activeDirectoryConnectivityOutcome.Update(CasTransactionResultEnum.Skipped);
				return activeDirectoryConnectivityOutcome;
			}
			return this.IsServiceRunning("NTDS", activeDirectoryConnectivityOutcome, TestActiveDirectoryConnectivityTask.ScenarioId.NTDSNotRunning);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003614 File Offset: 0x00001814
		private ActiveDirectoryConnectivityOutcome IsNetlogonRunning()
		{
			ActiveDirectoryConnectivityOutcome activeDirectoryConnectivityOutcome = base.CreateOutcome(TestActiveDirectoryConnectivityTask.ScenarioId.IsNetlogonRunning, Strings.IsNetlogonRunningScenario, Strings.IsNetlogonRunningScenario, base.Context.CurrentDomainController);
			if (!base.CanContinue)
			{
				base.WriteVerbose(Strings.CannotContinue(Strings.IsNetlogonRunningScenario));
				activeDirectoryConnectivityOutcome.Update(CasTransactionResultEnum.Skipped);
				return activeDirectoryConnectivityOutcome;
			}
			return this.IsServiceRunning("Netlogon", activeDirectoryConnectivityOutcome, TestActiveDirectoryConnectivityTask.ScenarioId.NetLogonNotRunning);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003680 File Offset: 0x00001880
		private ActiveDirectoryConnectivityOutcome IsServiceRunning(string serviceName, ActiveDirectoryConnectivityOutcome outcome, TestActiveDirectoryConnectivityTask.ScenarioId failureId)
		{
			try
			{
				using (ServiceController serviceController = new ServiceController(serviceName, base.Context.CurrentDomainController))
				{
					if (serviceController.Status != ServiceControllerStatus.Running)
					{
						outcome.Update(CasTransactionResultEnum.Failure, TimeSpan.Zero, Strings.ServiceNotRunning(serviceName));
						base.AddMonitoringEvent(failureId, EventTypeEnumeration.Error, base.GenerateErrorMessage(outcome.Id, 0, Strings.ServiceNotRunning(serviceName), null));
						return outcome;
					}
				}
			}
			catch (InvalidOperationException ex)
			{
				outcome.Update(CasTransactionResultEnum.Failure, TimeSpan.Zero, ex.ToString());
				base.AddMonitoringEvent(failureId, EventTypeEnumeration.Error, base.GenerateErrorMessage(outcome.Id, 0, ex.ToString(), null));
				return outcome;
			}
			outcome.Update(CasTransactionResultEnum.Success);
			return outcome;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000374C File Offset: 0x0000194C
		private ActiveDirectoryConnectivityOutcome MachinePing()
		{
			ActiveDirectoryConnectivityOutcome activeDirectoryConnectivityOutcome = base.CreateOutcome(TestActiveDirectoryConnectivityTask.ScenarioId.MachinePing, Strings.MachinePingScenario, Strings.MachinePingScenario, base.Context.CurrentDomainController);
			if (!base.CanContinue)
			{
				base.WriteVerbose(Strings.CannotContinue(Strings.MachinePingScenario));
				activeDirectoryConnectivityOutcome.Update(CasTransactionResultEnum.Skipped);
				return activeDirectoryConnectivityOutcome;
			}
			try
			{
				Dns.GetHostEntry(base.Context.CurrentDomainController);
				activeDirectoryConnectivityOutcome.Update(CasTransactionResultEnum.Success);
			}
			catch (SocketException ex)
			{
				base.Context.Instance.SkipRemainingTests = true;
				activeDirectoryConnectivityOutcome.Update(CasTransactionResultEnum.Failure);
				base.AddMonitoringEvent(TestActiveDirectoryConnectivityTask.ScenarioId.MachinePingFailed, EventTypeEnumeration.Error, base.GenerateErrorMessage(TestActiveDirectoryConnectivityTask.ScenarioId.MachinePing, 0, ex.Message, null));
			}
			return activeDirectoryConnectivityOutcome;
		}
	}
}
