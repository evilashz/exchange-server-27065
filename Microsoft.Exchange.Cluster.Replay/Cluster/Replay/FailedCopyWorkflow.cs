using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.HA.FailureItem;
using Microsoft.Exchange.Rpc.Cluster;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200028A RID: 650
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FailedCopyWorkflow : AutoReseedWorkflow
	{
		// Token: 0x06001938 RID: 6456 RVA: 0x0006841B File Offset: 0x0006661B
		public FailedCopyWorkflow(AutoReseedContext context, string workflowLaunchReason) : base(AutoReseedWorkflowType.FailedCopy, workflowLaunchReason, context)
		{
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x00068426 File Offset: 0x00066626
		protected override bool IsDisabled
		{
			get
			{
				return RegistryParameters.AutoReseedDbFailedWorkflowDisabled;
			}
		}

		// Token: 0x0600193A RID: 6458 RVA: 0x0006842D File Offset: 0x0006662D
		protected override TimeSpan GetThrottlingInterval(AutoReseedWorkflowState state)
		{
			return TimeSpan.Zero;
		}

		// Token: 0x0600193B RID: 6459 RVA: 0x00068434 File Offset: 0x00066634
		protected override LocalizedString RunPrereqs(AutoReseedWorkflowState state)
		{
			LocalizedString result = base.RunPrereqs(state);
			if (!result.IsEmpty)
			{
				return result;
			}
			result = FailedSuspendedCopyAutoReseedWorkflow.CheckExchangeVolumesPresent(base.Context);
			if (!result.IsEmpty)
			{
				return result;
			}
			return FailedSuspendedCopyAutoReseedWorkflow.CheckDatabaseLogPaths(base.Context);
		}

		// Token: 0x0600193C RID: 6460 RVA: 0x00068478 File Offset: 0x00066678
		protected override Exception ExecuteInternal(AutoReseedWorkflowState state)
		{
			RpcDatabaseCopyStatus2 copyStatus = base.Context.TargetCopyStatus.CopyStatus;
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)RegistryParameters.AutoReseedDbFailedPeriodicIntervalInSecs);
			base.TraceDebug("Calling SuspendAndFailLocalDatabaseCopy() ...", new object[0]);
			return DatabaseTasks.SuspendAndFailLocalDatabaseCopy(base.Context.Database, ReplayStrings.AutoReseedFailedCopyWorkflowSuspendedCopy(timeSpan.ToString()), copyStatus.ErrorMessage, copyStatus.ErrorEventId, copyStatus.ResumeBlocked, copyStatus.ReseedBlocked, copyStatus.InPlaceReseedBlocked);
		}
	}
}
