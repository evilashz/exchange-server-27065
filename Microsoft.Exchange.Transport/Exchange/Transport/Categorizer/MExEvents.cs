using System;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000207 RID: 519
	internal class MExEvents
	{
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x0005D5FE File Offset: 0x0005B7FE
		public static ExEventLog EventLogger
		{
			get
			{
				return MExEvents.eventLogger;
			}
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0005D605 File Offset: 0x0005B805
		public static IMExSession GetExecutionContext(TransportMailItem currentMailItem, AcceptedDomainCollection acceptedDomains, Action asyncStartAgentCallback, Action asyncCompleteAgentCallback, Func<bool> resumeAgentCallback)
		{
			if (MExEvents.mexRuntime == null)
			{
				throw new InvalidOperationException("Initialize() must be called before GetExecutionContext()");
			}
			return MExEvents.mexRuntime.CreateSession(CatServer.GetInstance(currentMailItem, acceptedDomains), "CAT", asyncStartAgentCallback, asyncCompleteAgentCallback, resumeAgentCallback);
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0005D633 File Offset: 0x0005B833
		public static void FreeExecutionContext(IMExSession context)
		{
			if (MExEvents.mexRuntime == null)
			{
				throw new InvalidOperationException("Initialize() must be called before FreeExecutionContext()");
			}
			context.Close();
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x0005D64D File Offset: 0x0005B84D
		public static IMExSession CloneExecutionContext(IMExSession mexSession)
		{
			return (IMExSession)mexSession.Clone();
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0005D65C File Offset: 0x0005B85C
		public static IAsyncResult RaiseEvent(IMExSession mexSession, string eventTopic, AsyncCallback callback, object state, params object[] contexts)
		{
			if (MExEvents.mexRuntime == null)
			{
				throw new InvalidOperationException("Initialize() must be called before RaiseEvent()");
			}
			IAsyncResult result;
			try
			{
				result = mexSession.BeginInvoke(eventTopic, contexts[0], contexts[1], callback, state);
			}
			catch (LocalizedException e)
			{
				MExEvents.HandleAgentExchangeExceptions(mexSession, e);
				throw;
			}
			return result;
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0005D6AC File Offset: 0x0005B8AC
		public static void EndEvent(IMExSession mexSession, IAsyncResult ar)
		{
			try
			{
				mexSession.EndInvoke(ar);
			}
			catch (LocalizedException e)
			{
				MExEvents.HandleAgentExchangeExceptions(mexSession, e);
				throw;
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0005D6FC File Offset: 0x0005B8FC
		public static void Initialize(string configFilePath)
		{
			if (MExEvents.mexRuntime != null)
			{
				throw new InvalidOperationException("Cannot Initialize() again without calling Shutdown() first");
			}
			MExEvents.mexRuntime = new MExRuntime();
			MExEvents.mexRuntime.Initialize(configFilePath, "Microsoft.Exchange.Data.Transport.Routing.RoutingAgent", Components.Configuration.ProcessTransportRole, ConfigurationContext.Setup.InstallPath, delegate(AgentFactory agentFactory)
			{
				IDiagnosable diagnosable = agentFactory as IDiagnosable;
				if (diagnosable != null)
				{
					ProcessAccessManager.RegisterComponent(diagnosable);
				}
			});
			AgentLatencyTracker.RegisterMExRuntime(LatencyAgentGroup.Categorizer, MExEvents.mexRuntime);
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x0005D76C File Offset: 0x0005B96C
		public static void Shutdown()
		{
			if (MExEvents.mexRuntime != null)
			{
				MExEvents.mexRuntime.Shutdown();
				MExEvents.mexRuntime = null;
			}
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x0005D788 File Offset: 0x0005B988
		public static IAsyncResult RaiseOnSubmittedMessage(TaskContext context, AsyncCallback callback, MailItem mailItem)
		{
			InternalSubmittedMessageSource internalSubmittedMessageSource = new InternalSubmittedMessageSource();
			AgentSubmittedMessageSource agentSubmittedMessageSource = new AgentSubmittedMessageSource();
			internalSubmittedMessageSource.Initialize(context.MexSession, context);
			agentSubmittedMessageSource.Initialize(internalSubmittedMessageSource);
			AgentQueuedMessageEventArgs agentQueuedMessageEventArgs = new AgentQueuedMessageEventArgs(mailItem);
			return MExEvents.RaiseEvent(context.MexSession, "OnSubmittedMessage", callback, context, new object[]
			{
				agentSubmittedMessageSource,
				agentQueuedMessageEventArgs
			});
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x0005D7E4 File Offset: 0x0005B9E4
		public static IAsyncResult RaiseOnResolvedMessage(TaskContext context, AsyncCallback callback, MailItem mailItem)
		{
			InternalResolvedMessageSource internalResolvedMessageSource = new InternalResolvedMessageSource();
			AgentResolvedMessageSource agentResolvedMessageSource = new AgentResolvedMessageSource();
			internalResolvedMessageSource.Initialize(context.MexSession, context);
			agentResolvedMessageSource.Initialize(internalResolvedMessageSource);
			AgentQueuedMessageEventArgs agentQueuedMessageEventArgs = new AgentQueuedMessageEventArgs(mailItem);
			return MExEvents.RaiseEvent(context.MexSession, "OnResolvedMessage", callback, context, new object[]
			{
				agentResolvedMessageSource,
				agentQueuedMessageEventArgs
			});
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0005D840 File Offset: 0x0005BA40
		public static IAsyncResult RaiseOnRoutedMessage(TaskContext context, AsyncCallback callback, MailItem mailItem)
		{
			InternalRoutedMessageSource internalRoutedMessageSource = new InternalRoutedMessageSource();
			AgentRoutedMessageSource agentRoutedMessageSource = new AgentRoutedMessageSource();
			internalRoutedMessageSource.Initialize(context.MexSession, context);
			agentRoutedMessageSource.Initialize(internalRoutedMessageSource);
			AgentQueuedMessageEventArgs agentQueuedMessageEventArgs = new AgentQueuedMessageEventArgs(mailItem);
			return MExEvents.RaiseEvent(context.MexSession, "OnRoutedMessage", callback, context, new object[]
			{
				agentRoutedMessageSource,
				agentQueuedMessageEventArgs
			});
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0005D89C File Offset: 0x0005BA9C
		public static IAsyncResult RaiseOnCategorizedMessage(TaskContext context, AsyncCallback callback, MailItem mailItem)
		{
			InternalCategorizedMessageSource internalCategorizedMessageSource = new InternalCategorizedMessageSource();
			AgentCategorizedMessageSource agentCategorizedMessageSource = new AgentCategorizedMessageSource();
			internalCategorizedMessageSource.Initialize(context.MexSession, context);
			agentCategorizedMessageSource.Initialize(internalCategorizedMessageSource);
			AgentQueuedMessageEventArgs agentQueuedMessageEventArgs = new AgentQueuedMessageEventArgs(mailItem);
			return MExEvents.RaiseEvent(context.MexSession, "OnCategorizedMessage", callback, context, new object[]
			{
				agentCategorizedMessageSource,
				agentQueuedMessageEventArgs
			});
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0005D8F8 File Offset: 0x0005BAF8
		public static void HandleAgentExchangeExceptions(IMExSession mexSession, LocalizedException e)
		{
			ExTraceGlobals.ExtensibilityTracer.TraceError<string, string, LocalizedException>(0L, "Agent {0} running in context {1} hit Unhandled Exception {2}", (mexSession.CurrentAgent == null) ? null : mexSession.CurrentAgent.Name, mexSession.EventTopic, e);
			ExEventLog.EventTuple tuple;
			if (string.Equals(mexSession.EventTopic, "OnSubmittedMessage", StringComparison.OrdinalIgnoreCase))
			{
				tuple = TransportEventLogConstants.Tuple_OnSubmittedMessageAgentException;
			}
			else if (string.Equals(mexSession.EventTopic, "OnRoutedMessage", StringComparison.OrdinalIgnoreCase))
			{
				tuple = TransportEventLogConstants.Tuple_OnRoutedMessageAgentException;
			}
			else if (string.Equals(mexSession.EventTopic, "OnResolvedMessage", StringComparison.OrdinalIgnoreCase))
			{
				tuple = TransportEventLogConstants.Tuple_OnResolvedMessageAgentException;
			}
			else
			{
				if (!string.Equals(mexSession.EventTopic, "OnCategorizedMessage", StringComparison.OrdinalIgnoreCase))
				{
					throw new InvalidOperationException("Unknown agent type");
				}
				tuple = TransportEventLogConstants.Tuple_OnCategorizedMessageAgentException;
			}
			MExEvents.eventLogger.LogEvent(tuple, null, new object[]
			{
				(mexSession.CurrentAgent == null) ? null : mexSession.CurrentAgent.Name,
				e
			});
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0005D9DC File Offset: 0x0005BBDC
		public static XElement[] GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			if (MExEvents.mexRuntime != null)
			{
				return MExEvents.mexRuntime.GetDiagnosticInfo(parameters, "RoutingAgent");
			}
			return null;
		}

		// Token: 0x04000B69 RID: 2921
		private static MExRuntime mexRuntime;

		// Token: 0x04000B6A RID: 2922
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.ExtensibilityTracer.Category, TransportEventLog.GetEventSource());
	}
}
