using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Monitoring.ServiceContextProvider;
using Microsoft.Forefront.RecoveryActionArbiter.Contract;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders
{
	// Token: 0x020000D1 RID: 209
	public class RecoveryModeResponder : ResponderWorkItem
	{
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x000287D4 File Offset: 0x000269D4
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x000287DC File Offset: 0x000269DC
		internal string RecoveryID { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x000287E5 File Offset: 0x000269E5
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x000287ED File Offset: 0x000269ED
		internal ArbitrationScope ArbitrationScope { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x000287F6 File Offset: 0x000269F6
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x000287FE File Offset: 0x000269FE
		internal ArbitrationSource ArbitrationSource { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00028807 File Offset: 0x00026A07
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x0002880F File Offset: 0x00026A0F
		internal RequestedAction RequestedAction { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x00028818 File Offset: 0x00026A18
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x00028820 File Offset: 0x00026A20
		internal RecoveryFlags RecoveryFlags { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00028829 File Offset: 0x00026A29
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x00028831 File Offset: 0x00026A31
		internal string FailureReason { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0002883A File Offset: 0x00026A3A
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x00028842 File Offset: 0x00026A42
		internal ServiceHealthStatus EnterRecoveryModeTargetHealthState { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0002884B File Offset: 0x00026A4B
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x00028853 File Offset: 0x00026A53
		internal ServiceHealthStatus ExitRecoveryModeTargetHealthState { get; set; }

		// Token: 0x060006DE RID: 1758 RVA: 0x0002885C File Offset: 0x00026A5C
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string targetResource, string alertMask, string alertTypeId, string recoveryID, ServiceHealthStatus enterRecoveryModeTargetHealthState, ServiceHealthStatus exitRecoveryModeTargetHealthState, ArbitrationScope? arbitrationScope, ArbitrationSource? arbitrationSource, RequestedAction? requestedAction, RecoveryFlags? recoveryFlags, string failureReason)
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = typeof(RecoveryModeResponder).Assembly.Location;
			responderDefinition.TypeName = typeof(RecoveryModeResponder).FullName;
			responderDefinition.Name = name;
			responderDefinition.ServiceName = serviceName;
			responderDefinition.TargetResource = targetResource;
			responderDefinition.AlertMask = alertMask;
			responderDefinition.AlertTypeId = alertTypeId;
			responderDefinition.TargetHealthState = ServiceHealthStatus.None;
			responderDefinition.Attributes["RecoveryID"] = recoveryID;
			responderDefinition.Attributes["EnterRecoveryModeTargetHealthState"] = enterRecoveryModeTargetHealthState.ToString();
			responderDefinition.Attributes["ExitRecoveryModeTargetHealthState"] = exitRecoveryModeTargetHealthState.ToString();
			if (arbitrationScope != null)
			{
				responderDefinition.Attributes["ArbitrationScope"] = arbitrationScope.ToString();
			}
			if (arbitrationSource != null)
			{
				responderDefinition.Attributes["ArbitrationSource"] = arbitrationSource.ToString();
			}
			if (requestedAction != null)
			{
				responderDefinition.Attributes["RequestedAction"] = requestedAction.ToString();
			}
			if (recoveryFlags != null)
			{
				responderDefinition.Attributes["RecoveryFlags"] = recoveryFlags.ToString();
			}
			responderDefinition.Attributes["FailureReason"] = failureReason;
			return responderDefinition;
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x000289C0 File Offset: 0x00026BC0
		protected virtual void InitializeAttributes(AttributeHelper attributeHelper = null)
		{
			if (attributeHelper == null)
			{
				attributeHelper = new AttributeHelper(base.Definition);
			}
			this.EnterRecoveryModeTargetHealthState = attributeHelper.GetEnum<ServiceHealthStatus>("EnterRecoveryModeTargetHealthState", false, ServiceHealthStatus.None);
			this.ExitRecoveryModeTargetHealthState = attributeHelper.GetEnum<ServiceHealthStatus>("ExitRecoveryModeTargetHealthState", false, ServiceHealthStatus.None);
			if (this.EnterRecoveryModeTargetHealthState == ServiceHealthStatus.None && this.ExitRecoveryModeTargetHealthState == ServiceHealthStatus.None)
			{
				throw new ArgumentException("EnterRecoveryModeTargetHealthState and ExitRecoveryModeTargetHealthState cannot both be set to \"None.\"");
			}
			bool flag = this.EnterRecoveryModeTargetHealthState != ServiceHealthStatus.None;
			this.RecoveryID = attributeHelper.GetString("RecoveryID", true, "");
			if (this.RecoveryID.Equals(string.Empty))
			{
				throw new ArgumentException("RecoveryID is a mandatory attribute and cannot be empty.");
			}
			this.ArbitrationScope = attributeHelper.GetEnum<ArbitrationScope>("ArbitrationScope", flag, 0);
			this.ArbitrationSource = attributeHelper.GetEnum<ArbitrationSource>("ArbitrationSource", flag, 0);
			this.RequestedAction = attributeHelper.GetEnum<RequestedAction>("RequestedAction", flag, 2);
			this.RecoveryFlags = attributeHelper.GetEnum<RecoveryFlags>("RecoveryFlags", flag, 0);
			this.FailureReason = attributeHelper.GetString("FailureReason", flag, "");
			if (flag && this.FailureReason.Equals(string.Empty))
			{
				throw new ArgumentException("FailureReason is a mandatory attribute and cannot be empty.");
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00028B6C File Offset: 0x00026D6C
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			this.InitializeAttributes(null);
			Task<MonitorResult> lastSuccessfulMonitorResult = base.GetLastSuccessfulMonitorResult(cancellationToken);
			if (lastSuccessfulMonitorResult != null)
			{
				lastSuccessfulMonitorResult.Continue(delegate(MonitorResult lastMonitorResult)
				{
					bool flag = ServiceContextProvider.RecoveryRequestExists(this.RecoveryID);
					if (lastMonitorResult != null)
					{
						if (lastMonitorResult.HealthState == this.EnterRecoveryModeTargetHealthState)
						{
							ServiceContextProvider.Instance.RequestApprovalForRecovery(this.RecoveryID, this.ArbitrationScope, this.ArbitrationSource, this.RequestedAction, this.RecoveryFlags, this.FailureReason, "");
							return;
						}
						if (lastMonitorResult.HealthState == this.ExitRecoveryModeTargetHealthState && flag)
						{
							ServiceContextProvider.Instance.NotifyRecoveryCompletion(this.RecoveryID, true, "");
						}
					}
				}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
			}
		}

		// Token: 0x020000D2 RID: 210
		internal static class AttributeNames
		{
			// Token: 0x0400047A RID: 1146
			internal const string RecoveryID = "RecoveryID";

			// Token: 0x0400047B RID: 1147
			internal const string ArbitrationScope = "ArbitrationScope";

			// Token: 0x0400047C RID: 1148
			internal const string ArbitrationSource = "ArbitrationSource";

			// Token: 0x0400047D RID: 1149
			internal const string RequestedAction = "RequestedAction";

			// Token: 0x0400047E RID: 1150
			internal const string RecoveryFlags = "RecoveryFlags";

			// Token: 0x0400047F RID: 1151
			internal const string FailureReason = "FailureReason";

			// Token: 0x04000480 RID: 1152
			internal const string EnterRecoveryModeTargetHealthState = "EnterRecoveryModeTargetHealthState";

			// Token: 0x04000481 RID: 1153
			internal const string ExitRecoveryModeTargetHealthState = "ExitRecoveryModeTargetHealthState";
		}

		// Token: 0x020000D3 RID: 211
		internal static class DefaultValues
		{
			// Token: 0x04000482 RID: 1154
			internal const string RecoveryID = "";

			// Token: 0x04000483 RID: 1155
			internal const ArbitrationScope ArbitrationScope = 0;

			// Token: 0x04000484 RID: 1156
			internal const ArbitrationSource ArbitrationSource = 0;

			// Token: 0x04000485 RID: 1157
			internal const RequestedAction RequestedAction = 2;

			// Token: 0x04000486 RID: 1158
			internal const RecoveryFlags RecoveryFlags = 0;

			// Token: 0x04000487 RID: 1159
			internal const string FailureReason = "";

			// Token: 0x04000488 RID: 1160
			internal const ServiceHealthStatus EnterRecoveryModeTargetHealthState = ServiceHealthStatus.None;

			// Token: 0x04000489 RID: 1161
			internal const ServiceHealthStatus ExitRecoveryModeTargetHealthState = ServiceHealthStatus.None;
		}
	}
}
