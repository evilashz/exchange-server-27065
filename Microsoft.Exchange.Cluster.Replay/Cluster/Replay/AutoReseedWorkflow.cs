using System;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000281 RID: 641
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AutoReseedWorkflow
	{
		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060018E2 RID: 6370 RVA: 0x00065E52 File Offset: 0x00064052
		protected static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.AutoReseedTracer;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x00065E59 File Offset: 0x00064059
		internal AutoReseedContext Context
		{
			get
			{
				return this.m_context;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x00065E61 File Offset: 0x00064061
		protected AutoReseedWorkflowType WorkflowType
		{
			get
			{
				return this.m_workflowType;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x00065E69 File Offset: 0x00064069
		protected string WorkflowName
		{
			get
			{
				return this.m_workflowName;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060018E6 RID: 6374 RVA: 0x00065E71 File Offset: 0x00064071
		protected string WorkflowLaunchReason
		{
			get
			{
				return this.m_workflowLaunchReason;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060018E7 RID: 6375
		protected abstract bool IsDisabled { get; }

		// Token: 0x060018E8 RID: 6376
		protected abstract TimeSpan GetThrottlingInterval(AutoReseedWorkflowState state);

		// Token: 0x060018E9 RID: 6377 RVA: 0x00065E79 File Offset: 0x00064079
		protected AutoReseedWorkflow(AutoReseedWorkflowType workflowType, string workflowLaunchReason, AutoReseedContext context)
		{
			this.m_workflowType = workflowType;
			this.m_workflowName = workflowType.ToString();
			this.m_workflowLaunchReason = workflowLaunchReason;
			this.m_context = context;
		}

		// Token: 0x060018EA RID: 6378 RVA: 0x00065EA8 File Offset: 0x000640A8
		protected void TraceDebug(string formatString, params object[] args)
		{
			if (AutoReseedWorkflow.Tracer.IsTraceEnabled(TraceType.DebugTrace) || AutoReseedWorkflow.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				AutoReseedWorkflow.Tracer.TraceDebug((long)this.GetHashCode(), "AutoReseed workflow '{0}' for database '{1}' [{2}]: {3}", new object[]
				{
					this.WorkflowName,
					this.Context.Database.Name,
					this.Context.Database.Guid,
					string.Format(formatString, args)
				});
			}
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x00065F2C File Offset: 0x0006412C
		protected void TraceError(string formatString, params object[] args)
		{
			if (AutoReseedWorkflow.Tracer.IsTraceEnabled(TraceType.ErrorTrace) || AutoReseedWorkflow.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				AutoReseedWorkflow.Tracer.TraceError((long)this.GetHashCode(), "AutoReseed workflow '{0}' for database '{1}' [{2}]: {3}", new object[]
				{
					this.WorkflowName,
					this.Context.Database.Name,
					this.Context.Database.Guid,
					string.Format(formatString, args)
				});
			}
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x00065FB0 File Offset: 0x000641B0
		public Exception Execute()
		{
			Exception ex = null;
			if (this.IsDisabled)
			{
				AutoReseedWorkflow.Tracer.TraceDebug<string, string, Guid>((long)this.GetHashCode(), "AutoReseed workflow '{0}' for database '{1}' [{2}] will not be executed because it has been disabled via regkey.", this.WorkflowName, this.Context.Database.Name, this.Context.Database.Guid);
				return null;
			}
			try
			{
				AutoReseedWorkflowState autoReseedWorkflowState = new AutoReseedWorkflowState(this.Context.Database.Guid, this.WorkflowType);
				if (this.ShouldSkipWorkflowExecution(autoReseedWorkflowState))
				{
					return null;
				}
				this.LogWorkflowStarted();
				if (this.ArePrereqsSatisfied(autoReseedWorkflowState, out ex) && !this.ShouldThrottleExecution(autoReseedWorkflowState, out ex))
				{
					bool flag = true;
					try
					{
						ex = this.ExecuteInternal(autoReseedWorkflowState);
						flag = false;
					}
					catch (RegistryParameterException ex2)
					{
						flag = false;
						ex = new AutoReseedException(ex2.Message, ex2);
					}
					finally
					{
						if (flag)
						{
							ex = new AutoReseedUnhandledException(this.Context.Database.Name, this.Context.TargetServerName.NetbiosName);
						}
						autoReseedWorkflowState.WriteWorkflowExecutionState(ex);
					}
				}
			}
			catch (RegistryParameterException ex3)
			{
				ex = new AutoReseedException(ex3.Message, ex3);
			}
			this.LogWorkflowEnded(ex);
			return ex;
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x000660EC File Offset: 0x000642EC
		private bool ShouldSkipWorkflowExecution(AutoReseedWorkflowState state)
		{
			bool result = false;
			DateTime workflowNextExecutionTime = state.WorkflowNextExecutionTime;
			if (workflowNextExecutionTime != DateTime.MinValue && DateTime.UtcNow < workflowNextExecutionTime)
			{
				AutoReseedWorkflow.Tracer.TraceDebug((long)this.GetHashCode(), "AutoReseed workflow '{0}' for database '{1}' [{2}]: Skipping execution because WorkflowNextExecutionTime is set to '{3}'.", new object[]
				{
					this.WorkflowName,
					this.Context.Database.Name,
					this.Context.Database.Guid,
					workflowNextExecutionTime
				});
				result = true;
			}
			return result;
		}

		// Token: 0x060018EE RID: 6382
		protected abstract Exception ExecuteInternal(AutoReseedWorkflowState state);

		// Token: 0x060018EF RID: 6383 RVA: 0x0006617A File Offset: 0x0006437A
		protected virtual LocalizedString RunPrereqs(AutoReseedWorkflowState state)
		{
			return this.CheckThirdPartyReplicationEnabled();
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00066182 File Offset: 0x00064382
		protected LocalizedString CheckThirdPartyReplicationEnabled()
		{
			if (this.Context.Dag.ThirdPartyReplication == ThirdPartyReplicationMode.Enabled)
			{
				return ReplayStrings.AutoReseedWorkflowNotSupportedOnTPR(this.Context.Dag.Name);
			}
			return LocalizedString.Empty;
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x000661B4 File Offset: 0x000643B4
		protected bool GetWorkflowElapsedExecutionTime(AutoReseedWorkflowState state, out TimeSpan elapsedExecutionTime)
		{
			elapsedExecutionTime = TimeSpan.Zero;
			if (state.WorkflowExecutionTime.Equals(DateTime.MinValue))
			{
				this.TraceDebug("GetWorkflowElapsedExecutionTime(): Returning 'false' since WorkflowExecutionTime is DateTime.MinValue.", new object[0]);
				return false;
			}
			elapsedExecutionTime = DateTime.UtcNow.Subtract(state.WorkflowExecutionTime);
			this.TraceDebug("GetWorkflowElapsedExecutionTime(): Returning elapsedExecutionTime = '{0}'", new object[]
			{
				elapsedExecutionTime
			});
			return true;
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00066230 File Offset: 0x00064430
		private bool ArePrereqsSatisfied(AutoReseedWorkflowState state, out Exception exception)
		{
			exception = null;
			LocalizedString value = this.RunPrereqs(state);
			if (!value.IsEmpty)
			{
				exception = new AutoReseedPrereqFailedException(this.Context.Database.Name, this.Context.TargetServerName.NetbiosName, value);
				return false;
			}
			return true;
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x00066284 File Offset: 0x00064484
		private bool ShouldThrottleExecution(AutoReseedWorkflowState state, out Exception exception)
		{
			bool flag = false;
			exception = null;
			TimeSpan t;
			if (this.GetWorkflowElapsedExecutionTime(state, out t))
			{
				TimeSpan throttlingInterval = this.GetThrottlingInterval(state);
				if (t < throttlingInterval)
				{
					flag = true;
					exception = new AutoReseedThrottledException(this.Context.Database.Name, this.Context.TargetServerName.NetbiosName, throttlingInterval.ToString());
				}
				AutoReseedWorkflow.Tracer.TraceDebug((long)this.GetHashCode(), "AutoReseed workflow '{0}' for database '{1}' [{2}]: Throttling interval ({3}). Last execution time ({4}). Throttle = {5}.", new object[]
				{
					this.WorkflowName,
					this.Context.Database.Name,
					this.Context.Database.Guid,
					throttlingInterval,
					state.WorkflowExecutionTime,
					flag
				});
			}
			else
			{
				AutoReseedWorkflow.Tracer.TraceDebug<string, string, Guid>((long)this.GetHashCode(), "AutoReseed workflow '{0}' for database '{1}' [{2}] will not be throttled because this is the first time it is being executed.", this.WorkflowName, this.Context.Database.Name, this.Context.Database.Guid);
			}
			return flag;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0006639C File Offset: 0x0006459C
		private void LogWorkflowStarted()
		{
			AutoReseedWorkflow.Tracer.TraceDebug((long)this.GetHashCode(), "Starting AutoReseed workflow '{0}' for database '{1}' [{2}]. WorkflowLaunchReason: {3}", new object[]
			{
				this.WorkflowName,
				this.Context.Database.Name,
				this.Context.Database.Guid,
				this.m_workflowLaunchReason
			});
			ReplayCrimsonEvents.AutoReseedManagerBeginWorkflow.Log<string, Guid, string, string>(this.Context.Database.Name, this.Context.Database.Guid, this.WorkflowName, this.m_workflowLaunchReason);
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0006643C File Offset: 0x0006463C
		private void LogWorkflowEnded(Exception exception)
		{
			if (exception == null)
			{
				AutoReseedWorkflow.Tracer.TraceDebug<string, string, Guid>((long)this.GetHashCode(), "AutoReseed workflow '{0}' for database '{1}' [{2}] completed successfully.", this.WorkflowName, this.Context.Database.Name, this.Context.Database.Guid);
				ReplayCrimsonEvents.AutoReseedManagerWorkflowCompletedSuccess.Log<string, Guid, string, string>(this.Context.Database.Name, this.Context.Database.Guid, this.WorkflowName, this.m_workflowLaunchReason);
				return;
			}
			if (exception is AutoReseedThrottledException)
			{
				AutoReseedWorkflow.Tracer.TraceError((long)this.GetHashCode(), "AutoReseed workflow '{0}' for database '{1}' [{2}] got throttled! Error: {3}", new object[]
				{
					this.WorkflowName,
					this.Context.Database.Name,
					this.Context.Database.Guid,
					AmExceptionHelper.GetExceptionMessageOrNoneString(exception)
				});
				ReplayCrimsonEvents.AutoReseedManagerWorkflowThrottled.LogPeriodic<string, Guid, string, string, string>(this.Context.Database.Guid, DiagCore.DefaultEventSuppressionInterval, this.Context.Database.Name, this.Context.Database.Guid, this.WorkflowName, this.m_workflowLaunchReason, AmExceptionHelper.GetExceptionMessageOrNoneString(exception));
				return;
			}
			AutoReseedWorkflow.Tracer.TraceError((long)this.GetHashCode(), "AutoReseed workflow '{0}' for database '{1}' [{2}] failed! Error: {3}", new object[]
			{
				this.WorkflowName,
				this.Context.Database.Name,
				this.Context.Database.Guid,
				AmExceptionHelper.GetExceptionMessageOrNoneString(exception)
			});
			ReplayCrimsonEvents.AutoReseedManagerWorkflowFailed.Log<string, Guid, string, string, string>(this.Context.Database.Name, this.Context.Database.Guid, this.WorkflowName, this.m_workflowLaunchReason, AmExceptionHelper.GetExceptionMessageOrNoneString(exception));
		}

		// Token: 0x040009F6 RID: 2550
		private readonly AutoReseedContext m_context;

		// Token: 0x040009F7 RID: 2551
		private readonly AutoReseedWorkflowType m_workflowType;

		// Token: 0x040009F8 RID: 2552
		private readonly string m_workflowName;

		// Token: 0x040009F9 RID: 2553
		private readonly string m_workflowLaunchReason;
	}
}
