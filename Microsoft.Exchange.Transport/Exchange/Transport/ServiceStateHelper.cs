using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000053 RID: 83
	internal class ServiceStateHelper
	{
		// Token: 0x0600022B RID: 555 RVA: 0x0000AE5B File Offset: 0x0000905B
		public ServiceStateHelper(ITransportConfiguration configuration, string hostName)
		{
			this.configuration = configuration;
			this.hostName = hostName;
			this.stateChangeEventTuple = this.GetEventLogStateChangeTuple(hostName);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000AE7E File Offset: 0x0000907E
		public static ServiceState GetServiceState(ITransportConfiguration configuration, string hostName)
		{
			return ServerComponentStates.ReadEffectiveComponentState(null, configuration.LocalServer.TransportServer.ComponentStates, hostName, ServiceStateHelper.GetDefaultServiceState());
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000AE9C File Offset: 0x0000909C
		public static ServiceState GetDefaultServiceState()
		{
			if (!VariantConfiguration.InvariantNoFlightingSnapshot.Transport.DefaultTransportServiceStateToInactive.Enabled)
			{
				return ServiceState.Active;
			}
			return ServiceState.Inactive;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000AEC8 File Offset: 0x000090C8
		public bool CheckState(ServiceState? initialState)
		{
			if (!this.configuration.AppConfig.StateManagement.StateChangeDetectionEnabled || initialState == null)
			{
				return false;
			}
			if (Components.ShuttingDown)
			{
				ExTraceGlobals.ServiceTracer.TraceDebug(0L, "Service shutting down. State checking skipped.");
				return false;
			}
			ServiceState serviceState = ServiceStateHelper.GetServiceState(this.configuration, this.hostName);
			if (initialState.Value != serviceState)
			{
				if (initialState.Value == ServiceState.Active && Components.IsPaused && serviceState == ServiceState.Draining)
				{
					ExTraceGlobals.ServiceTracer.TraceDebug(0L, "Initial service state is Active, but service is paused and target state is draining.");
				}
				else
				{
					ServiceStateHelper.eventLogger.LogEvent(this.stateChangeEventTuple, null, new object[]
					{
						initialState.Value,
						serviceState
					});
					EventNotificationItem.Publish(this.hostName, "ServiceStateChange", null, string.Format("Inconsistent state: current = {0}, target = {1}", initialState.Value, serviceState), ResultSeverityLevel.Warning, false);
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000AFBC File Offset: 0x000091BC
		private ExEventLog.EventTuple GetEventLogStateChangeTuple(string hostname)
		{
			if (this.hostName == ServerComponentStates.GetComponentId(ServerComponentEnum.HubTransport))
			{
				return TransportEventLogConstants.Tuple_HubTransportServiceStateChanged;
			}
			if (this.hostName == ServerComponentStates.GetComponentId(ServerComponentEnum.EdgeTransport))
			{
				return TransportEventLogConstants.Tuple_EdgeTransportServiceStateChanged;
			}
			if (this.hostName == ServerComponentStates.GetComponentId(ServerComponentEnum.FrontendTransport))
			{
				return TransportEventLogConstants.Tuple_FrontendTransportServiceStateChanged;
			}
			throw new ArgumentException("hostname");
		}

		// Token: 0x0400015D RID: 349
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.ServiceTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x0400015E RID: 350
		private readonly string hostName;

		// Token: 0x0400015F RID: 351
		private readonly ExEventLog.EventTuple stateChangeEventTuple;

		// Token: 0x04000160 RID: 352
		private ITransportConfiguration configuration;
	}
}
