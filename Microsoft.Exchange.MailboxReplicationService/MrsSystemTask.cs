using System;
using Microsoft.Exchange.Data.ConfigurationSettings;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000A1 RID: 161
	internal class MrsSystemTask : SystemTaskBase
	{
		// Token: 0x0600081A RID: 2074 RVA: 0x00037A8A File Offset: 0x00035C8A
		public MrsSystemTask(Job job, Action callback, SystemWorkloadBase systemWorkload, ResourceReservation reservation, bool ignoreTaskSuccessfulExecutionTime = false) : base(systemWorkload)
		{
			this.Job = job;
			base.ResourceReservation = reservation;
			this.Callback = callback;
			this.IgnoreTaskSuccessfulExecutionTime = ignoreTaskSuccessfulExecutionTime;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00037AB1 File Offset: 0x00035CB1
		// (set) Token: 0x0600081C RID: 2076 RVA: 0x00037AB9 File Offset: 0x00035CB9
		public Action Callback { get; private set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x00037AC2 File Offset: 0x00035CC2
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x00037ACA File Offset: 0x00035CCA
		public Job Job { get; private set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x00037AD3 File Offset: 0x00035CD3
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x00037ADB File Offset: 0x00035CDB
		public bool IgnoreTaskSuccessfulExecutionTime { get; private set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00037AE4 File Offset: 0x00035CE4
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x00037AEC File Offset: 0x00035CEC
		public Exception Failure { get; private set; }

		// Token: 0x06000823 RID: 2083 RVA: 0x00037AF8 File Offset: 0x00035CF8
		protected override TaskStepResult Execute()
		{
			TaskStepResult result;
			using (SettingsContextBase.ActivateContext(this.Job as ISettingsContextProvider))
			{
				this.Job.GetCurrentActivityScope();
				try
				{
					this.Run();
				}
				catch (Exception exception)
				{
					this.Job.PerformCrashingFailureActions(exception);
					throw;
				}
				result = TaskStepResult.Complete;
			}
			return result;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00037B7A File Offset: 0x00035D7A
		public void Run()
		{
			CommonUtils.CatchKnownExceptions(delegate
			{
				this.Callback();
			}, delegate(Exception failure)
			{
				this.Failure = failure;
			});
		}
	}
}
