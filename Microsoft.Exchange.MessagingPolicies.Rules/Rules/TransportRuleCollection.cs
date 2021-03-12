using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Transport.Configuration;
using Microsoft.Exchange.Transport.Extensibility;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000033 RID: 51
	internal sealed class TransportRuleCollection : RuleCollection
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x000095E5 File Offset: 0x000077E5
		public TransportRuleCollection(string name) : base(name)
		{
			this.supportsBifurcation = false;
			this.ResetInternalState();
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000095FB File Offset: 0x000077FB
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x00009603 File Offset: 0x00007803
		public bool SupportsBifurcation
		{
			get
			{
				return this.supportsBifurcation;
			}
			set
			{
				this.supportsBifurcation = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x0000960C File Offset: 0x0000780C
		public RulesCountersInstance TotalPerformanceCounterInstance
		{
			set
			{
				this.totalCounter = value;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00009618 File Offset: 0x00007818
		public ExecutionStatus Run(SmtpServer server, MailItem mailItem, ReceiveMessageEventSource endOfDataSource, SmtpSession session, TransportRulesCostMonitor ruleSetExecutionMonitor = null)
		{
			TransportRulesEvaluationContext context = new TransportRulesEvaluationContext(this, mailItem, server, endOfDataSource, session, ruleSetExecutionMonitor);
			return this.Run(context);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000963C File Offset: 0x0000783C
		public ExecutionStatus Run(SmtpServer server, MailItem mailItem, QueuedMessageEventSource eventSource, bool shouldAudit = false, TransportRulesTracer tracer = null, TestMessageConfig testMessageConfig = null, TransportRulesCostMonitor ruleSetExecutionMonitor = null, TenantConfigurationCache<TransportRulesPerTenantSettings> transportRulesCache = null)
		{
			return this.Run(new TransportRulesEvaluationContext(this, mailItem, server, eventSource, ruleSetExecutionMonitor, shouldAudit, transportRulesCache, tracer)
			{
				TestMessageConfig = testMessageConfig
			});
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000966C File Offset: 0x0000786C
		public void CreatePerformanceCounters()
		{
			foreach (Rule rule in this)
			{
				TransportRule transportRule = (TransportRule)rule;
				if (transportRule.Enabled == RuleState.Enabled)
				{
					transportRule.CreatePerformanceCounter(base.Name);
				}
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000096C8 File Offset: 0x000078C8
		internal ExecutionStatus Run(TransportRulesEvaluationContext context)
		{
			context.Tracer.TraceDebug("A rule collection is executing on a message.");
			ExecutionStatus result;
			try
			{
				TransportRulesEvaluator transportRulesEvaluator = new TransportRulesEvaluator(context);
				transportRulesEvaluator.Run();
				result = context.ExecutionStatus;
			}
			catch (Exception exception)
			{
				OrganizationId organizationID = TransportUtils.GetOrganizationID(context.MailItem);
				TransportRulesErrorHandler.LogRuleEvaluationFailureEvent(context, exception, (organizationID == null) ? "No org Id available" : organizationID.ToString(), TransportUtils.GetMessageID(context.MailItem));
				this.IncrementMessagesDeferredDueToErrors();
				IErrorHandlingAction errorHandlingAction = TransportRulesErrorHandler.GetErrorHandlingAction(exception, context.MailItem);
				if (errorHandlingAction == null)
				{
					throw;
				}
				if (errorHandlingAction is AgentErrorHandlingDeferAction && context.RuleSetExecutionMonitor != null)
				{
					context.RuleSetExecutionMonitor.StopAndSetReporter(null);
				}
				errorHandlingAction.TakeAction(context.OnResolvedSource, context.MailItem);
				result = TransportRulesErrorHandler.ErrorActionToExecutionStatus(errorHandlingAction.ActionType);
			}
			return result;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000979C File Offset: 0x0000799C
		public void IncrementMessagesEvaluated()
		{
			if (!this.hasMessageEvaluatedCounted && this.totalCounter != null)
			{
				this.totalCounter.MessagesEvaluated.Increment();
				this.hasMessageEvaluatedCounted = true;
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000097C6 File Offset: 0x000079C6
		public void IncrementMessagesProcessed()
		{
			if (!this.hasMessageProcessedCounted && this.totalCounter != null)
			{
				this.totalCounter.MessagesProcessed.Increment();
				this.hasMessageProcessedCounted = true;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000097F0 File Offset: 0x000079F0
		public void IncrementMessagesSkipped()
		{
			if (!this.hasMessageSkippedCounted && this.totalCounter != null)
			{
				this.totalCounter.MessagesSkipped.Increment();
				this.hasMessageSkippedCounted = true;
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000981A File Offset: 0x00007A1A
		public void IncrementMessagesDeferredDueToErrors()
		{
			if (!this.hasMessageDeferredDueToErrorsCounted && this.totalCounter != null)
			{
				this.totalCounter.MessagesDeferredDueToRuleErrors.Increment();
				this.hasMessageDeferredDueToErrorsCounted = true;
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00009844 File Offset: 0x00007A44
		public void ResetInternalState()
		{
			this.hasMessageEvaluatedCounted = false;
			this.hasMessageProcessedCounted = false;
			this.hasMessageSkippedCounted = false;
			this.hasMessageDeferredDueToErrorsCounted = false;
		}

		// Token: 0x04000163 RID: 355
		private bool supportsBifurcation;

		// Token: 0x04000164 RID: 356
		private RulesCountersInstance totalCounter;

		// Token: 0x04000165 RID: 357
		private bool hasMessageEvaluatedCounted;

		// Token: 0x04000166 RID: 358
		private bool hasMessageProcessedCounted;

		// Token: 0x04000167 RID: 359
		private bool hasMessageSkippedCounted;

		// Token: 0x04000168 RID: 360
		private bool hasMessageDeferredDueToErrorsCounted;
	}
}
