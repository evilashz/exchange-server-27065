using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Extensibility;
using Microsoft.Exchange.Transport.LoggingCommon;
using Microsoft.Filtering;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x0200003B RID: 59
	internal sealed class TransportRulesErrorHandler
	{
		// Token: 0x0600020A RID: 522 RVA: 0x0000B9A8 File Offset: 0x00009BA8
		private static List<Type> InitializeKnownRuleEvaluationExceptions()
		{
			List<Type> list = new List<Type>
			{
				typeof(TransportRuleTimeoutException),
				typeof(ADTransientException),
				typeof(TransportRuleTransientException),
				typeof(RegexMatchTimeoutException),
				typeof(TransportRulePermanentException),
				typeof(RuleInvalidOperationException),
				typeof(InvalidTransportRuleEventSourceTypeException),
				typeof(OutboundConnectorNotFoundException)
			};
			list.AddRange(TransportRulesErrorHandler.filteringServiceExceptions);
			return list;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000BA4C File Offset: 0x00009C4C
		internal static IErrorHandlingAction GetErrorHandlingAction(Exception exception, MailItem mailItem)
		{
			IErrorHandlingAction errorHandlingAction = TransportRulesErrorHandler.AgentErrorHandler.GetErrorHandlingAction(TrackAgentInfoAgentName.TRA.ToString("G"), exception, mailItem);
			if (errorHandlingAction != null)
			{
				AgentErrorHandlingDeferAction agentErrorHandlingDeferAction = errorHandlingAction as AgentErrorHandlingDeferAction;
				if (agentErrorHandlingDeferAction != null)
				{
					string unicodeString = string.Format("Message deferred. {0}, tenant - {1}", exception.Message, TransportUtils.GetOrganizationID(mailItem).ToString());
					SmtpResponse value = new SmtpResponse("421", "4.7.11", "Message deferred by Transport Rules Agent", true, new string[]
					{
						TransportRulesErrorHandler.EncodeStringToUtf7(unicodeString)
					});
					agentErrorHandlingDeferAction.SmtpResponse = new SmtpResponse?(value);
				}
			}
			return errorHandlingAction;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		internal static bool IsKnownFipsException(Exception ex)
		{
			return TransportRulesErrorHandler.filteringServiceExceptions.Any((Type fipsException) => fipsException.IsInstanceOfType(ex));
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000BB38 File Offset: 0x00009D38
		internal static bool IsTimeoutException(Exception ex)
		{
			return TransportRulesErrorHandler.timeoutExceptions.Any((Type timeoutException) => timeoutException.IsInstanceOfType(ex));
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000BB68 File Offset: 0x00009D68
		internal static ExecutionStatus ErrorActionToExecutionStatus(ErrorHandlingActionType errorActionType)
		{
			switch (errorActionType)
			{
			case ErrorHandlingActionType.NDR:
			case ErrorHandlingActionType.Drop:
				return ExecutionStatus.PermanentError;
			case ErrorHandlingActionType.Defer:
				return ExecutionStatus.TransientError;
			case ErrorHandlingActionType.Allow:
				return ExecutionStatus.Success;
			default:
				return ExecutionStatus.PermanentError;
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0000BB98 File Offset: 0x00009D98
		internal static void LogFailureEvent(MailItem mailItem, ExEventLog.EventTuple eventLog, string errorMessage)
		{
			string text = string.Format("{0} Message-Id:{1}", errorMessage, TransportUtils.GetMessageID(mailItem));
			ExTraceGlobals.TransportRulesEngineTracer.TraceError(0L, errorMessage);
			TransportAction.Logger.LogEvent(eventLog, null, new object[]
			{
				text
			});
			EventNotificationItem.Publish(ExchangeComponent.Transport.Name, string.Format("{0}.Event-0x{1:X}", TransportRulesEvaluator.ActiveMonitoringComponentName, eventLog.EventId), null, text, ResultSeverityLevel.Error, false);
			SystemProbeHelper.EtrTracer.TraceFail(mailItem, 0L, "Error processing rules. Details: {0}", text);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000BC1E File Offset: 0x00009E1E
		internal static bool IsDeferredOrDeleted(ExecutionStatus ruleExecutionStatus)
		{
			return ruleExecutionStatus == ExecutionStatus.SuccessMailItemDeleted || ruleExecutionStatus == ExecutionStatus.SuccessMailItemDeferred || ruleExecutionStatus == ExecutionStatus.PermanentError || ruleExecutionStatus == ExecutionStatus.TransientError;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000BC34 File Offset: 0x00009E34
		internal static void LogRuleEvaluationFailureEvent(TransportRulesEvaluationContext context, Exception exception, string tenantId, string messageId)
		{
			Type type = exception.GetType();
			if (TransportRulesErrorHandler.IsKnownFipsException(exception))
			{
				TransportRulesErrorHandler.LogRuleEvaluationFailureEvent(context, MessagingPoliciesEventLogConstants.Tuple_RuleEvaluationFilteringServiceFailure, type.Name, exception.ToString(), tenantId, messageId);
				return;
			}
			TransportRulesErrorHandler.LogRuleEvaluationFailureEvent(context, MessagingPoliciesEventLogConstants.Tuple_RuleEvaluationFailure, type.Name, exception.ToString(), tenantId, messageId);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000BC84 File Offset: 0x00009E84
		internal static void LogRuleEvaluationIgnoredFailureEvent(TransportRulesEvaluationContext context, Exception exception, string tenantId, string messageId)
		{
			Type type = exception.GetType();
			if (TransportRulesErrorHandler.IsKnownFipsException(exception))
			{
				TransportRulesErrorHandler.LogRuleEvaluationFailureEvent(context, MessagingPoliciesEventLogConstants.Tuple_RuleEvaluationIgnoredFilteringServiceFailure, type.Name, exception.ToString(), tenantId, messageId);
				return;
			}
			TransportRulesErrorHandler.LogRuleEvaluationFailureEvent(context, MessagingPoliciesEventLogConstants.Tuple_RuleEvaluationIgnoredFailure, type.Name, exception.ToString(), tenantId, messageId);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000BCD3 File Offset: 0x00009ED3
		internal static string EncodeStringToUtf7(string unicodeString)
		{
			return Encoding.ASCII.GetString(Encoding.UTF7.GetBytes(unicodeString));
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000BD08 File Offset: 0x00009F08
		internal static bool IsIgnorableException(Exception ex, TransportRulesEvaluationContext context)
		{
			if (context != null && context.CurrentRule != null && context.CurrentRule.ErrorAction == RuleErrorAction.Ignore && TransportRulesErrorHandler.knownRuleEvaluationExceptions.Any((Type knownExceptionType) => knownExceptionType == ex.GetType()))
			{
				return true;
			}
			IErrorHandlingAction errorHandlingAction = TransportRulesErrorHandler.GetErrorHandlingAction(ex, context.MailItem);
			return errorHandlingAction != null && errorHandlingAction.ActionType == ErrorHandlingActionType.Allow;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000BD78 File Offset: 0x00009F78
		private static void LogRuleEvaluationFailureEvent(TransportRulesEvaluationContext context, ExEventLog.EventTuple eventLog, string exceptionName, string exceptionMessage, string tenantId, string messageId)
		{
			string errorMessage = string.Format("Organization: '{0}' Message ID '{1}' Rule ID '{2}' Predicate '{3}' Action '{4}'. {5} Error: {6}.", new object[]
			{
				tenantId,
				messageId,
				TransportUtils.GetCurrentRuleId(context),
				TransportUtils.GetCurrentPredicateName(context),
				TransportUtils.GetCurrentActionName(context),
				exceptionName,
				exceptionMessage
			});
			TransportRulesErrorHandler.LogFailureEvent(context.MailItem, eventLog, errorMessage);
		}

		// Token: 0x04000180 RID: 384
		private static readonly List<Type> filteringServiceExceptions = new List<Type>
		{
			typeof(FilteringServiceTimeoutException),
			typeof(FilteringServiceFailureException),
			typeof(ScannerCrashException),
			typeof(ScanQueueTimeoutException),
			typeof(ScanTimeoutException),
			typeof(ResultsValidationException),
			typeof(ClassificationEngineInvalidOobConfigurationException),
			typeof(ClassificationEngineInvalidCustomConfigurationException),
			typeof(BiasException),
			typeof(QueueFullException),
			typeof(ConfigurationException),
			typeof(ServiceUnavailableException),
			typeof(ScanAbortedException),
			typeof(FilteringException)
		};

		// Token: 0x04000181 RID: 385
		private static readonly List<Type> knownRuleEvaluationExceptions = TransportRulesErrorHandler.InitializeKnownRuleEvaluationExceptions();

		// Token: 0x04000182 RID: 386
		private static readonly List<Type> timeoutExceptions = new List<Type>
		{
			typeof(FilteringServiceTimeoutException),
			typeof(ScanQueueTimeoutException),
			typeof(ScanTimeoutException),
			typeof(RegexMatchTimeoutException)
		};

		// Token: 0x04000183 RID: 387
		private static readonly List<Type> exceptionsToNdr = new List<Type>
		{
			typeof(ParserException),
			typeof(DataSourceOperationException)
		};

		// Token: 0x04000184 RID: 388
		private static readonly int DeferCount = 5;

		// Token: 0x04000185 RID: 389
		private static readonly TimeSpan DeferInteval = TimeSpan.FromMinutes(10.0);

		// Token: 0x04000186 RID: 390
		private static readonly IErrorHandlingAction DeferActionProgressive = new AgentErrorHandlingDeferAction(TransportRulesErrorHandler.DeferInteval, true);

		// Token: 0x04000187 RID: 391
		private static readonly List<AgentErrorHandlingDefinition> TransportRulesAgentErrorHandlingMap = new List<AgentErrorHandlingDefinition>
		{
			new AgentErrorHandlingDefinition("Transport Rule - NDR when defer count reaches 5", new AgentErrorHandlingCondition(TrackAgentInfoAgentName.TRA.ToString("G"), TransportRulesErrorHandler.knownRuleEvaluationExceptions, TransportRulesErrorHandler.DeferCount, null), AgentErrorHandlingMap.NdrActionBadContent),
			new AgentErrorHandlingDefinition("Transport Rule - Defer", new AgentErrorHandlingCondition(TrackAgentInfoAgentName.TRA.ToString("G"), TransportRulesErrorHandler.knownRuleEvaluationExceptions, 0, null), TransportRulesErrorHandler.DeferActionProgressive),
			new AgentErrorHandlingDefinition("Transport Rule - NDR Immediately", new AgentErrorHandlingCondition(TrackAgentInfoAgentName.TRA.ToString("G"), TransportRulesErrorHandler.exceptionsToNdr, 0, null), AgentErrorHandlingMap.NdrActionBadContent)
		};

		// Token: 0x04000188 RID: 392
		private static readonly AgentErrorHandling AgentErrorHandler = new AgentErrorHandling(TransportRulesErrorHandler.TransportRulesAgentErrorHandlingMap);
	}
}
