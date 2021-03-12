using System;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Delivery;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Delivery
{
	// Token: 0x020003C3 RID: 963
	internal class DeliveryAgentMExEvents
	{
		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06002C29 RID: 11305 RVA: 0x000B0859 File Offset: 0x000AEA59
		public ExEventLog EventLogger
		{
			get
			{
				return this.eventLogger;
			}
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000B0861 File Offset: 0x000AEA61
		public virtual DeliveryAgentMExEvents.DeliveryAgentMExSession GetExecutionContext(string deliveryProtocol, SmtpServer smtpServer, Action startAsyncAgentCallback, Action completedAsyncAgentCallback, Func<bool> resumeAgentCallback)
		{
			if (this.mexRuntime == null)
			{
				throw new InvalidOperationException("Initialize() must be called before GetExecutionContext()");
			}
			return new DeliveryAgentMExEvents.DeliveryAgentMExSession(this.mexRuntime.CreateSession(smtpServer, deliveryProtocol, startAsyncAgentCallback, completedAsyncAgentCallback, resumeAgentCallback));
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000B088D File Offset: 0x000AEA8D
		public virtual void FreeExecutionContext(DeliveryAgentMExEvents.DeliveryAgentMExSession context)
		{
			if (this.mexRuntime == null)
			{
				throw new InvalidOperationException("Initialize() must be called before FreeExecutionContext()");
			}
			context.Close();
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000B08A8 File Offset: 0x000AEAA8
		public virtual IAsyncResult RaiseEvent(DeliveryAgentMExEvents.DeliveryAgentMExSession mexSession, string eventTopic, AsyncCallback callback, object state, params object[] contexts)
		{
			if (this.mexRuntime == null)
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
				this.HandleAgentExchangeExceptions(mexSession, e);
				throw;
			}
			return result;
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000B08FC File Offset: 0x000AEAFC
		public virtual void EndEvent(DeliveryAgentMExEvents.DeliveryAgentMExSession mexSession, IAsyncResult ar)
		{
			try
			{
				mexSession.EndInvoke(ar);
			}
			catch (LocalizedException e)
			{
				this.HandleAgentExchangeExceptions(mexSession, e);
				throw;
			}
		}

		// Token: 0x06002C2E RID: 11310 RVA: 0x000B0930 File Offset: 0x000AEB30
		public virtual void Initialize(string configFilePath)
		{
			if (this.mexRuntime != null)
			{
				throw new InvalidOperationException("Cannot Initialize() again without calling Shutdown() first");
			}
			this.mexRuntime = new MExRuntime();
			this.mexRuntime.Initialize(configFilePath, typeof(DeliveryAgent).ToString(), Components.Configuration.ProcessTransportRole, ConfigurationContext.Setup.InstallPath, new FactoryInitializer(ProcessAccessManager.RegisterAgentFactory));
			AgentLatencyTracker.RegisterMExRuntime(LatencyAgentGroup.Delivery, this.mexRuntime);
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x000B099D File Offset: 0x000AEB9D
		public virtual void Shutdown()
		{
			if (this.mexRuntime != null)
			{
				this.mexRuntime.Shutdown();
				this.mexRuntime = null;
			}
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x000B09BC File Offset: 0x000AEBBC
		private void HandleAgentExchangeExceptions(DeliveryAgentMExEvents.DeliveryAgentMExSession mexSession, LocalizedException e)
		{
			ExTraceGlobals.ExtensibilityTracer.TraceError<string, string, LocalizedException>(0L, "Agent {0} running in context {1} hit Unhandled Exception {2}", mexSession.CurrentAgentName, mexSession.EventTopic, e);
			ExEventLog.EventTuple tuple;
			if (string.Equals(mexSession.EventTopic, "OnOpenConnection", StringComparison.OrdinalIgnoreCase))
			{
				tuple = TransportEventLogConstants.Tuple_OnOpenConnectionAgentException;
			}
			else if (string.Equals(mexSession.EventTopic, "OnDeliverMailItem", StringComparison.OrdinalIgnoreCase))
			{
				tuple = TransportEventLogConstants.Tuple_OnDeliverMailItemAgentException;
			}
			else
			{
				if (!string.Equals(mexSession.EventTopic, "OnCloseConnection", StringComparison.OrdinalIgnoreCase))
				{
					throw new InvalidOperationException("Unknown agent type");
				}
				tuple = TransportEventLogConstants.Tuple_OnCloseConnectionAgentException;
			}
			this.EventLogger.LogEvent(tuple, null, new object[]
			{
				mexSession.CurrentAgentName,
				e
			});
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000B0A66 File Offset: 0x000AEC66
		public XElement[] GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			if (this.mexRuntime != null)
			{
				return this.mexRuntime.GetDiagnosticInfo(parameters, "DeliveryAgent");
			}
			return null;
		}

		// Token: 0x0400162E RID: 5678
		private MExRuntime mexRuntime;

		// Token: 0x0400162F RID: 5679
		private ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.ExtensibilityTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x020003C4 RID: 964
		public class DeliveryAgentMExSession
		{
			// Token: 0x06002C33 RID: 11315 RVA: 0x000B0AA5 File Offset: 0x000AECA5
			public DeliveryAgentMExSession(IMExSession mexSession)
			{
				this.mexSession = mexSession;
				this.agentLatencyTracker = new AgentLatencyTracker(this.mexSession);
			}

			// Token: 0x06002C34 RID: 11316 RVA: 0x000B0AC5 File Offset: 0x000AECC5
			protected DeliveryAgentMExSession()
			{
			}

			// Token: 0x17000D65 RID: 3429
			// (get) Token: 0x06002C35 RID: 11317 RVA: 0x000B0ACD File Offset: 0x000AECCD
			public virtual string CurrentAgentName
			{
				get
				{
					if (this.mexSession.CurrentAgent != null)
					{
						return this.mexSession.CurrentAgent.Name;
					}
					return string.Empty;
				}
			}

			// Token: 0x17000D66 RID: 3430
			// (get) Token: 0x06002C36 RID: 11318 RVA: 0x000B0AF2 File Offset: 0x000AECF2
			public virtual string EventTopic
			{
				get
				{
					return this.mexSession.EventTopic;
				}
			}

			// Token: 0x17000D67 RID: 3431
			// (get) Token: 0x06002C37 RID: 11319 RVA: 0x000B0AFF File Offset: 0x000AECFF
			public virtual AgentLatencyTracker AgentLatencyTracker
			{
				get
				{
					return this.agentLatencyTracker;
				}
			}

			// Token: 0x06002C38 RID: 11320 RVA: 0x000B0B07 File Offset: 0x000AED07
			public virtual IAsyncResult BeginInvoke(string topic, object source, object e, AsyncCallback callback, object callbackState)
			{
				return this.mexSession.BeginInvoke(topic, source, e, callback, callbackState);
			}

			// Token: 0x06002C39 RID: 11321 RVA: 0x000B0B1B File Offset: 0x000AED1B
			public virtual void EndInvoke(IAsyncResult ar)
			{
				this.mexSession.EndInvoke(ar);
			}

			// Token: 0x06002C3A RID: 11322 RVA: 0x000B0B29 File Offset: 0x000AED29
			public virtual void Close()
			{
				this.agentLatencyTracker.Dispose();
				this.agentLatencyTracker = null;
				this.mexSession.Close();
				this.mexSession = null;
			}

			// Token: 0x04001630 RID: 5680
			private IMExSession mexSession;

			// Token: 0x04001631 RID: 5681
			private AgentLatencyTracker agentLatencyTracker;
		}
	}
}
