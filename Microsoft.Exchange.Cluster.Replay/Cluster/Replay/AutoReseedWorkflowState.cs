using System;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Data.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000284 RID: 644
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AutoReseedWorkflowState
	{
		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x00067244 File Offset: 0x00065444
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.AutoReseedTracer;
			}
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0006724B File Offset: 0x0006544B
		public AutoReseedWorkflowState(Guid dbGuid, AutoReseedWorkflowType workflowType)
		{
			this.m_dbGuid = dbGuid;
			this.m_workflowType = workflowType;
			this.m_workflowName = workflowType.ToString();
			this.m_fields = new AutoReseedWorkflowStateValues(dbGuid, workflowType);
		}

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06001902 RID: 6402 RVA: 0x0006727F File Offset: 0x0006547F
		public Guid DatabaseGuid
		{
			get
			{
				return this.m_dbGuid;
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x00067287 File Offset: 0x00065487
		public AutoReseedWorkflowType WorkflowType
		{
			get
			{
				return this.m_workflowType;
			}
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x0006728F File Offset: 0x0006548F
		public string WorkflowName
		{
			get
			{
				return this.m_workflowName;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x00067297 File Offset: 0x00065497
		// (set) Token: 0x06001906 RID: 6406 RVA: 0x000672A9 File Offset: 0x000654A9
		public bool WorkflowInfoIsValid
		{
			get
			{
				return this.m_fields.GetValue<bool>("WorkflowInfoIsValid");
			}
			set
			{
				this.m_fields.SetValue<bool>("WorkflowInfoIsValid", value);
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x06001907 RID: 6407 RVA: 0x000672BD File Offset: 0x000654BD
		// (set) Token: 0x06001908 RID: 6408 RVA: 0x000672CF File Offset: 0x000654CF
		public AutoReseedWorkflowExecutionResult WorkflowExecutionResult
		{
			get
			{
				return this.m_fields.GetValue<AutoReseedWorkflowExecutionResult>("WorkflowExecutionResult2");
			}
			set
			{
				this.m_fields.SetValue<AutoReseedWorkflowExecutionResult>("WorkflowExecutionResult2", value);
			}
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001909 RID: 6409 RVA: 0x000672E3 File Offset: 0x000654E3
		// (set) Token: 0x0600190A RID: 6410 RVA: 0x000672F5 File Offset: 0x000654F5
		public string WorkflowExecutionError
		{
			get
			{
				return this.m_fields.GetValue<string>("WorkflowExecutionError");
			}
			set
			{
				this.m_fields.SetValue<string>("WorkflowExecutionError", value);
			}
		}

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x0600190B RID: 6411 RVA: 0x00067309 File Offset: 0x00065509
		// (set) Token: 0x0600190C RID: 6412 RVA: 0x0006731B File Offset: 0x0006551B
		public DateTime WorkflowExecutionTime
		{
			get
			{
				return this.m_fields.GetValue<DateTime>("WorkflowExecutionTime");
			}
			set
			{
				this.m_fields.SetValue<DateTime>("WorkflowExecutionTime", value);
			}
		}

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x0006732F File Offset: 0x0006552F
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x00067341 File Offset: 0x00065541
		public DateTime WorkflowNextExecutionTime
		{
			get
			{
				return this.m_fields.GetValue<DateTime>("WorkflowNextExecutionTime");
			}
			set
			{
				this.m_fields.SetValue<DateTime>("WorkflowNextExecutionTime", value);
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x00067355 File Offset: 0x00065555
		// (set) Token: 0x06001910 RID: 6416 RVA: 0x00067367 File Offset: 0x00065567
		public string AssignedVolumeName
		{
			get
			{
				return this.m_fields.GetValue<string>("AssignedVolumeName");
			}
			set
			{
				this.m_fields.SetValue<string>("AssignedVolumeName", value);
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x0006737B File Offset: 0x0006557B
		// (set) Token: 0x06001912 RID: 6418 RVA: 0x0006738D File Offset: 0x0006558D
		public ReseedState LastReseedRecoveryAction
		{
			get
			{
				return this.m_fields.GetValue<ReseedState>("LastReseedRecoveryAction");
			}
			set
			{
				this.m_fields.SetValue<ReseedState>("LastReseedRecoveryAction", value);
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x000673A1 File Offset: 0x000655A1
		// (set) Token: 0x06001914 RID: 6420 RVA: 0x000673B3 File Offset: 0x000655B3
		public int ReseedRecoveryActionRetryCount
		{
			get
			{
				return this.m_fields.GetValue<int>("ReseedRecoveryActionRetryCount");
			}
			set
			{
				this.m_fields.SetValue<int>("ReseedRecoveryActionRetryCount", value);
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x000673C7 File Offset: 0x000655C7
		// (set) Token: 0x06001916 RID: 6422 RVA: 0x000673D9 File Offset: 0x000655D9
		public bool IgnoreInPlaceOverwriteDelay
		{
			get
			{
				return this.m_fields.GetValue<bool>("IgnoreInPlaceOverwriteDelay");
			}
			set
			{
				this.m_fields.SetValue<bool>("IgnoreInPlaceOverwriteDelay", value);
			}
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x000673ED File Offset: 0x000655ED
		public bool IsLastReseedRecoveryActionPending()
		{
			return this.LastReseedRecoveryAction != ReseedState.Unknown && this.LastReseedRecoveryAction != ReseedState.Completed;
		}

		// Token: 0x06001918 RID: 6424 RVA: 0x00067408 File Offset: 0x00065608
		public void WriteWorkflowExecutionState(Exception exception)
		{
			this.WorkflowInfoIsValid = false;
			this.WorkflowExecutionTime = DateTime.UtcNow;
			if (exception != null)
			{
				this.WorkflowExecutionResult = AutoReseedWorkflowExecutionResult.Failed;
				this.WorkflowExecutionError = exception.Message;
			}
			else
			{
				this.WorkflowExecutionError = string.Empty;
				this.WorkflowExecutionResult = AutoReseedWorkflowExecutionResult.Success;
			}
			this.WorkflowInfoIsValid = true;
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x00067458 File Offset: 0x00065658
		public void WriteWorkflowNextExecutionDueTime(TimeSpan nextDueAfter)
		{
			DateTime workflowNextExecutionTime = DateTime.MinValue;
			if (nextDueAfter == InvokeWithTimeout.InfiniteTimeSpan)
			{
				workflowNextExecutionTime = DateTime.MaxValue;
			}
			else if (nextDueAfter != TimeSpan.Zero)
			{
				workflowNextExecutionTime = DateTime.UtcNow.Add(nextDueAfter);
			}
			this.WorkflowNextExecutionTime = workflowNextExecutionTime;
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x000674A4 File Offset: 0x000656A4
		public void UpdateReseedRecoveryAction(ReseedState action)
		{
			this.WorkflowInfoIsValid = false;
			if (action != this.LastReseedRecoveryAction)
			{
				AutoReseedWorkflowState.Tracer.TraceDebug((long)this.GetHashCode(), "AutoReseed workflow '{0}' for database '{1}': State is moving from '{2}' to '{3}'. ReseedRecoveryActionRetryCount is being reset to 0.", new object[]
				{
					this.WorkflowName,
					this.DatabaseGuid,
					this.LastReseedRecoveryAction,
					action
				});
				this.ResetReseedRecoveryActionInternal(action);
			}
			else
			{
				this.ReseedRecoveryActionRetryCount++;
				AutoReseedWorkflowState.Tracer.TraceDebug((long)this.GetHashCode(), "AutoReseed workflow '{0}' for database '{1}': Retry count of state '{2}' is being incremented to: ReseedRecoveryActionRetryCount = {3}.", new object[]
				{
					this.WorkflowName,
					this.DatabaseGuid,
					this.LastReseedRecoveryAction,
					this.ReseedRecoveryActionRetryCount
				});
			}
			this.WorkflowInfoIsValid = true;
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x0006757C File Offset: 0x0006577C
		public void ResetReseedRecoveryAction()
		{
			this.WorkflowInfoIsValid = false;
			this.ResetReseedRecoveryActionInternal(ReseedState.Unknown);
			this.IgnoreInPlaceOverwriteDelay = false;
			this.WorkflowInfoIsValid = true;
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x0006759C File Offset: 0x0006579C
		public static Exception WriteManualWorkflowExecutionState(Guid dbGuid, AutoReseedWorkflowType manualAction)
		{
			Exception result = null;
			try
			{
				AutoReseedWorkflowState autoReseedWorkflowState = new AutoReseedWorkflowState(dbGuid, manualAction);
				if (manualAction == AutoReseedWorkflowType.ManualResume)
				{
					autoReseedWorkflowState.LastReseedRecoveryAction = ReseedState.Resume;
				}
				else
				{
					autoReseedWorkflowState.LastReseedRecoveryAction = ReseedState.InPlaceReseed;
				}
				autoReseedWorkflowState.WriteWorkflowExecutionState(null);
			}
			catch (RegistryParameterException ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x000675E8 File Offset: 0x000657E8
		public static Exception TriggerInPlaceReseed(Guid dbGuid, string dbName)
		{
			Exception result = null;
			try
			{
				AutoReseedWorkflowState autoReseedWorkflowState = new AutoReseedWorkflowState(dbGuid, AutoReseedWorkflowType.FailedSuspendedCopyAutoReseed);
				autoReseedWorkflowState.ResetReseedRecoveryAction();
				autoReseedWorkflowState.IgnoreInPlaceOverwriteDelay = true;
				autoReseedWorkflowState.UpdateReseedRecoveryAction(ReseedState.InPlaceReseed);
				ReplayCrimsonEvents.AutoReseedTriggerInPlaceReseed.LogPeriodic<string, Guid>(dbGuid, DiagCore.DefaultEventSuppressionInterval, dbName, dbGuid);
			}
			catch (RegistryParameterException ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x00067644 File Offset: 0x00065844
		private void ResetReseedRecoveryActionInternal(ReseedState action)
		{
			this.ReseedRecoveryActionRetryCount = 0;
			this.LastReseedRecoveryAction = action;
			this.WorkflowExecutionResult = AutoReseedWorkflowExecutionResult.Unknown;
			this.WorkflowExecutionError = string.Empty;
			this.WorkflowExecutionTime = DateTime.MinValue;
			this.WorkflowNextExecutionTime = DateTime.MinValue;
		}

		// Token: 0x04000A04 RID: 2564
		private readonly Guid m_dbGuid;

		// Token: 0x04000A05 RID: 2565
		private readonly string m_workflowName;

		// Token: 0x04000A06 RID: 2566
		private readonly AutoReseedWorkflowType m_workflowType;

		// Token: 0x04000A07 RID: 2567
		private readonly AutoReseedWorkflowStateValues m_fields;
	}
}
