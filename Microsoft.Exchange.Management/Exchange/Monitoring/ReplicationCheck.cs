using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000535 RID: 1333
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ReplicationCheck : DisposeTrackableBase, IReplicationCheck
	{
		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06002FC8 RID: 12232 RVA: 0x000C1294 File Offset: 0x000BF494
		// (set) Token: 0x06002FC9 RID: 12233 RVA: 0x000C12B6 File Offset: 0x000BF4B6
		protected virtual string ErrorKey
		{
			get
			{
				return this.m_ErrorKey = this.GetDefaultErrorKey(base.GetType());
			}
			set
			{
				this.m_ErrorKey = value;
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06002FCA RID: 12234 RVA: 0x000C12C0 File Offset: 0x000BF4C0
		// (set) Token: 0x06002FCB RID: 12235 RVA: 0x000C12F0 File Offset: 0x000BF4F0
		protected string InstanceIdentity
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_instanceIdentity))
				{
					return this.m_instanceIdentity = this.m_ServerName;
				}
				return this.m_instanceIdentity;
			}
			set
			{
				this.m_instanceIdentity = value;
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06002FCC RID: 12236 RVA: 0x000C12F9 File Offset: 0x000BF4F9
		public string Title
		{
			get
			{
				return this.m_Title;
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06002FCD RID: 12237 RVA: 0x000C1301 File Offset: 0x000BF501
		public CheckId CheckId
		{
			get
			{
				return this.m_checkId;
			}
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06002FCE RID: 12238 RVA: 0x000C1309 File Offset: 0x000BF509
		public LocalizedString Description
		{
			get
			{
				return this.m_Description;
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06002FCF RID: 12239 RVA: 0x000C1311 File Offset: 0x000BF511
		public CheckCategory Category
		{
			get
			{
				return this.m_Category;
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06002FD0 RID: 12240 RVA: 0x000C1319 File Offset: 0x000BF519
		public string ServerName
		{
			get
			{
				return this.m_ServerName;
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06002FD1 RID: 12241 RVA: 0x000C1321 File Offset: 0x000BF521
		public IEventManager EventManager
		{
			get
			{
				return this.m_EventManager;
			}
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06002FD2 RID: 12242 RVA: 0x000C1329 File Offset: 0x000BF529
		public uint? IgnoreTransientErrorsThreshold
		{
			get
			{
				return this.m_ignoreTransientErrorsThreshold;
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06002FD3 RID: 12243 RVA: 0x000C1331 File Offset: 0x000BF531
		public bool HasRun
		{
			get
			{
				return this.m_checkResultInfo.HasRun;
			}
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x000C133E File Offset: 0x000BF53E
		public bool HasError
		{
			get
			{
				return this.m_checkResultInfo.HasFailures || this.m_checkResultInfo.HasWarnings || this.m_checkResultInfo.HasTransientError;
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x000C1367 File Offset: 0x000BF567
		public bool HasPassed
		{
			get
			{
				return this.m_checkResultInfo.HasPassed;
			}
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06002FD6 RID: 12246 RVA: 0x000C1374 File Offset: 0x000BF574
		public ReplicationCheckOutcome Outcome
		{
			get
			{
				return this.GetCheckOutcome();
			}
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x06002FD7 RID: 12247 RVA: 0x000C137C File Offset: 0x000BF57C
		public List<ReplicationCheckOutputObject> OutputObjects
		{
			get
			{
				return this.GetCheckOutputObjects();
			}
		}

		// Token: 0x06002FD8 RID: 12248 RVA: 0x000C1384 File Offset: 0x000BF584
		public ReplicationCheck(string title, CheckId checkId, LocalizedString description, CheckCategory category, IEventManager eventManager, string momEventSource, string serverName) : this(title, checkId, description, category, eventManager, momEventSource, serverName, null)
		{
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x000C13AC File Offset: 0x000BF5AC
		public ReplicationCheck(string title, CheckId checkId, LocalizedString description, CheckCategory category, IEventManager eventManager, string momEventSource, string serverName, uint? ignoreTransientErrorsThreshold)
		{
			this.m_Title = title;
			this.m_checkId = checkId;
			this.m_Description = description;
			this.m_Category = category;
			this.m_ServerName = serverName;
			this.m_EventManager = eventManager;
			this.m_checkResultInfo = new ReplicationCheckResultInfo(this);
			uint? num = ignoreTransientErrorsThreshold;
			this.m_ignoreTransientErrorsThreshold = ((num != null) ? new uint?(num.GetValueOrDefault()) : new uint?(0U));
		}

		// Token: 0x06002FDA RID: 12250 RVA: 0x000C1420 File Offset: 0x000BF620
		public virtual void Run()
		{
			try
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)this.GetHashCode(), "Run(): Check {0} has started running.", this.Title);
				this.InternalRun();
				if (this.HasError)
				{
					if (this.m_checkResultInfo.HasFailures)
					{
						this.FailInternal();
					}
					else
					{
						if (this.m_checkResultInfo.HasWarnings)
						{
							throw new ReplicationCheckWarningException(this.m_Title, this.BuildErrorMessageForOutcome());
						}
						if (this.m_checkResultInfo.HasTransientError)
						{
							this.m_checkResultInfo.SetPassed();
						}
					}
				}
				else
				{
					this.MarkCheckAsPassed();
				}
			}
			catch (ReplicationCheckSkippedException)
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)this.GetHashCode(), "Skip(): Check '{0}' is being skipped.", this.Title);
			}
			catch (DataSourceTransientException ex)
			{
				this.HandleKnownException(ex);
			}
			catch (ClusterException ex2)
			{
				this.HandleKnownException(ex2);
			}
			catch (DataSourceOperationException ex3)
			{
				this.HandleKnownException(ex3);
			}
			catch (AmServerException ex4)
			{
				this.HandleKnownException(ex4);
			}
			catch (AmServerTransientException ex5)
			{
				this.HandleKnownException(ex5);
			}
			finally
			{
				if (!this.m_hasSkipped)
				{
					this.m_checkResultInfo.HasRun = true;
				}
			}
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x000C1570 File Offset: 0x000BF770
		private void HandleKnownException(Exception ex)
		{
			this.m_checkResultInfo.AddFailure(this.InstanceIdentity, ex.Message, true);
			string errorMessage = this.BuildErrorMessageForOutcome();
			if (this.m_Category == CheckCategory.SystemHighPriority)
			{
				throw new ReplicationCheckHighPriorityFailedException(this.m_Title, errorMessage);
			}
			throw new ReplicationCheckFailedException(this.m_Title, errorMessage);
		}

		// Token: 0x06002FDC RID: 12252 RVA: 0x000C15BD File Offset: 0x000BF7BD
		public void Skip()
		{
			this.m_hasSkipped = true;
			if (!IgnoreTransientErrors.HasPassed(this.GetDefaultErrorKey(base.GetType())))
			{
				this.FailInternal();
			}
			throw new ReplicationCheckSkippedException();
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x000C15E4 File Offset: 0x000BF7E4
		private string BuildErrorMessageForOutcome()
		{
			return this.m_checkResultInfo.BuildErrorMessageForOutcome();
		}

		// Token: 0x06002FDE RID: 12254
		protected abstract void InternalRun();

		// Token: 0x06002FDF RID: 12255 RVA: 0x000C15F1 File Offset: 0x000BF7F1
		public void LogEvents()
		{
			this.m_checkResultInfo.LogEvents(this.m_EventManager);
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x000C1604 File Offset: 0x000BF804
		public ReplicationCheckOutcome GetCheckOutcome()
		{
			return this.m_checkResultInfo.GetCheckOutcome();
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x000C1611 File Offset: 0x000BF811
		public List<ReplicationCheckOutputObject> GetCheckOutputObjects()
		{
			return this.m_checkResultInfo.GetCheckOutputObjects();
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x000C1620 File Offset: 0x000BF820
		private void FailInternal()
		{
			string text = this.BuildErrorMessageForOutcome();
			if (this.m_Category == CheckCategory.SystemHighPriority)
			{
				ExTraceGlobals.HealthChecksTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Critical check {0} has failed!. Error: {1}", this.m_Title, text);
				throw new ReplicationCheckHighPriorityFailedException(this.m_Title, text);
			}
			ExTraceGlobals.HealthChecksTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Check {0} has failed. Error: {1}", this.m_Title, text);
			throw new ReplicationCheckFailedException(this.m_Title, text);
		}

		// Token: 0x06002FE3 RID: 12259 RVA: 0x000C1690 File Offset: 0x000BF890
		protected void Fail(LocalizedString error)
		{
			this.m_checkResultInfo.HasTransientError = true;
			bool isTransitioningState;
			if (!IgnoreTransientErrors.IgnoreTransientError(this.ErrorKey, this.IgnoreTransientErrorsThreshold.Value, ErrorType.Failure, out isTransitioningState))
			{
				this.m_checkResultInfo.AddFailure(this.InstanceIdentity, error, isTransitioningState);
				this.FailInternal();
				return;
			}
			this.m_checkResultInfo.AddSuccess(this.InstanceIdentity, isTransitioningState);
			ExTraceGlobals.HealthChecksTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Check '{0}' is logging treating the failure for instance '{1}' as transient and reporting a success record instead.", this.Title, this.InstanceIdentity);
		}

		// Token: 0x06002FE4 RID: 12260 RVA: 0x000C171A File Offset: 0x000BF91A
		protected void FailContinue(LocalizedString error)
		{
			this.FailContinue(error, 0U);
		}

		// Token: 0x06002FE5 RID: 12261 RVA: 0x000C1724 File Offset: 0x000BF924
		protected void FailContinue(LocalizedString error, uint dbFailureEventId)
		{
			this.m_checkResultInfo.HasTransientError = true;
			bool isTransitioningState;
			if (!IgnoreTransientErrors.IgnoreTransientError(this.ErrorKey, this.IgnoreTransientErrorsThreshold.Value, ErrorType.Failure, out isTransitioningState))
			{
				this.m_checkResultInfo.AddFailure(this.InstanceIdentity, error, false, (dbFailureEventId != 0U) ? new uint?(dbFailureEventId) : null, isTransitioningState);
				return;
			}
			this.m_checkResultInfo.AddSuccess(this.InstanceIdentity, isTransitioningState);
			ExTraceGlobals.HealthChecksTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Check '{0}' is logging treating the failure for instance '{1}' as transient and reporting a success record instead.", this.Title, this.InstanceIdentity);
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x000C17C0 File Offset: 0x000BF9C0
		protected void FailFatal(LocalizedString error)
		{
			this.m_checkResultInfo.HasTransientError = true;
			bool isTransitioningState;
			if (!IgnoreTransientErrors.IgnoreTransientError(this.ErrorKey, this.IgnoreTransientErrorsThreshold.Value, ErrorType.Failure, out isTransitioningState))
			{
				this.m_checkResultInfo.AddFailure(this.InstanceIdentity, Strings.ReplicationCheckFatalError(this.m_Title, error), isTransitioningState);
				throw new ReplicationCheckFatalException(this.m_Title, this.BuildErrorMessageForOutcome());
			}
			this.m_checkResultInfo.AddSuccess(this.InstanceIdentity, isTransitioningState);
			ExTraceGlobals.HealthChecksTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Check '{0}' is logging treating the failure for instance '{1}' as transient and reporting a success record instead.", this.Title, this.InstanceIdentity);
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x000C1868 File Offset: 0x000BFA68
		protected void WriteWarning(LocalizedString warning)
		{
			this.m_checkResultInfo.HasTransientError = true;
			bool isTransitioningState;
			if (!IgnoreTransientErrors.IgnoreTransientError(this.ErrorKey, this.IgnoreTransientErrorsThreshold.Value, ErrorType.Warning, out isTransitioningState))
			{
				this.m_checkResultInfo.AddWarning(this.InstanceIdentity, warning, isTransitioningState);
				string text = this.BuildErrorMessageForOutcome();
				ExTraceGlobals.HealthChecksTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Check {0} has issued a warning. Error: {1}", this.m_Title, text);
				throw new ReplicationCheckWarningException(this.m_Title, text);
			}
			this.m_checkResultInfo.AddSuccess(this.InstanceIdentity, isTransitioningState);
			ExTraceGlobals.HealthChecksTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Check '{0}' is logging treating the warning for instance '{1}' as transient and reporting a success record instead.", this.Title, this.InstanceIdentity);
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x000C191C File Offset: 0x000BFB1C
		protected void WriteWarningContinue(LocalizedString warning)
		{
			this.m_checkResultInfo.HasTransientError = true;
			bool isTransitioningState;
			if (!IgnoreTransientErrors.IgnoreTransientError(this.ErrorKey, this.IgnoreTransientErrorsThreshold.Value, ErrorType.Warning, out isTransitioningState))
			{
				this.m_checkResultInfo.AddWarning(this.InstanceIdentity, warning, isTransitioningState);
				return;
			}
			this.m_checkResultInfo.AddSuccess(this.InstanceIdentity, isTransitioningState);
			ExTraceGlobals.HealthChecksTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Check '{0}' is logging treating the warning for instance '{1}' as transient and reporting a success record instead.", this.Title, this.InstanceIdentity);
		}

		// Token: 0x06002FE9 RID: 12265 RVA: 0x000C19A0 File Offset: 0x000BFBA0
		protected void ReportPassedInstance()
		{
			bool isTransitioningState = IgnoreTransientErrors.ResetError(this.ErrorKey);
			this.m_checkResultInfo.AddSuccess(this.InstanceIdentity, isTransitioningState);
			ExTraceGlobals.HealthChecksTracer.TraceDebug<string, string>((long)this.GetHashCode(), "Check {0} is recording a pass entry for instance identity '{1}'.", this.Title, this.InstanceIdentity);
		}

		// Token: 0x06002FEA RID: 12266 RVA: 0x000C19F0 File Offset: 0x000BFBF0
		protected void MarkCheckAsPassed()
		{
			bool isTransitioningState = IgnoreTransientErrors.ResetError(this.ErrorKey);
			this.m_checkResultInfo.SetPassed(this.InstanceIdentity, isTransitioningState);
			ExTraceGlobals.HealthChecksTracer.TraceDebug<string>((long)this.GetHashCode(), "Check {0} has passed.", this.Title);
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000C1A38 File Offset: 0x000BFC38
		protected string GetDefaultErrorKey(Type type)
		{
			return string.Concat(new string[]
			{
				this.ServerName,
				"|",
				type.Name,
				"|",
				this.InstanceIdentity
			});
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000C1A7D File Offset: 0x000BFC7D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ReplicationCheck>(this);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000C1A85 File Offset: 0x000BFC85
		protected override void InternalDispose(bool calledFromDispose)
		{
		}

		// Token: 0x0400221D RID: 8733
		private readonly string m_Title;

		// Token: 0x0400221E RID: 8734
		private readonly CheckId m_checkId;

		// Token: 0x0400221F RID: 8735
		private readonly LocalizedString m_Description;

		// Token: 0x04002220 RID: 8736
		private readonly CheckCategory m_Category;

		// Token: 0x04002221 RID: 8737
		private readonly string m_ServerName;

		// Token: 0x04002222 RID: 8738
		private readonly IEventManager m_EventManager;

		// Token: 0x04002223 RID: 8739
		private readonly uint? m_ignoreTransientErrorsThreshold;

		// Token: 0x04002224 RID: 8740
		private bool m_hasSkipped;

		// Token: 0x04002225 RID: 8741
		private ReplicationCheckResultInfo m_checkResultInfo;

		// Token: 0x04002226 RID: 8742
		protected string m_ErrorKey;

		// Token: 0x04002227 RID: 8743
		protected string m_instanceIdentity;
	}
}
