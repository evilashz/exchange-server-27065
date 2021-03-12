using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport;
using Microsoft.Exchange.Transport.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200042F RID: 1071
	internal class InboundProxyDestinationTracker : DisposeTrackableBase, IInboundProxyDestinationTracker
	{
		// Token: 0x0600314B RID: 12619 RVA: 0x000C4AD4 File Offset: 0x000C2CD4
		public InboundProxyDestinationTracker(InboundProxyTrackerType trackerType, bool trackingEnabled, bool rejectBasedOnInboundProxyDestinationTrackingEnabled, int perDestinationConnectionPercentageThreshold, InboundProxyDestinationTracker.TryGetDestinationConnectionThresholdDelegate tryGetDestinationConnectionThresholdDelegate, ConnectionsTracker.GetExPerfCounterDelegate getConnectionsCurrent, ConnectionsTracker.GetExPerfCounterDelegate getConnectionsTotal, IExEventLog eventLog, TimeSpan trackerLogInterval, IEnumerable<ReceiveConnector> receiveConnectors)
		{
			ArgumentValidator.ThrowIfNull("trackerType", trackerType);
			ArgumentValidator.ThrowIfNull("tryGetDestinationConnectionThresholdDelegate", tryGetDestinationConnectionThresholdDelegate);
			ArgumentValidator.ThrowIfNull("getConnectionsCurrent", getConnectionsCurrent);
			ArgumentValidator.ThrowIfNull("eventLog", eventLog);
			ArgumentValidator.ThrowIfNull("trackerLogInterval", trackerLogInterval);
			ArgumentValidator.ThrowIfNull("receiveConnectors", receiveConnectors);
			this.smtpProxyTracker = new ConnectionsTracker(getConnectionsCurrent, getConnectionsTotal);
			if (!trackingEnabled)
			{
				return;
			}
			this.trackingEnabled = true;
			this.trackerType = trackerType;
			this.rejectBasedOnInboundProxyDestinationTrackingEnabled = rejectBasedOnInboundProxyDestinationTrackingEnabled;
			this.perDestinationConnectionPercentageThreshold = perDestinationConnectionPercentageThreshold;
			this.tryGetDestinationConnectionThresholdDelegate = tryGetDestinationConnectionThresholdDelegate;
			this.perDestinationConnectionThreshold = this.GetThresholdFromConnectors(receiveConnectors);
			this.eventLog = eventLog;
			this.logTimer = new GuardedTimer(new TimerCallback(this.LogData), null, trackerLogInterval);
		}

		// Token: 0x0600314C RID: 12620 RVA: 0x000C4BA0 File Offset: 0x000C2DA0
		public void IncrementProxyCount(string destination)
		{
			if (!this.trackingEnabled)
			{
				return;
			}
			this.smtpProxyTracker.IncrementProxyCount(destination);
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x000C4BB7 File Offset: 0x000C2DB7
		public void DecrementProxyCount(string destination)
		{
			if (!this.trackingEnabled)
			{
				return;
			}
			this.smtpProxyTracker.DecrementProxyCount(destination);
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x000C4BD0 File Offset: 0x000C2DD0
		public bool ShouldRejectMessage(string destination, out SmtpResponse rejectResponse)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("destination", destination);
			if (!this.trackingEnabled)
			{
				rejectResponse = SmtpResponse.Empty;
				return false;
			}
			int usage = this.smtpProxyTracker.GetUsage(destination);
			int threhsoldValue;
			if (this.tryGetDestinationConnectionThresholdDelegate(destination, out threhsoldValue) && this.IsOverThreshold(usage, threhsoldValue, destination))
			{
				if (this.rejectBasedOnInboundProxyDestinationTrackingEnabled)
				{
					rejectResponse = SmtpResponse.InboundProxyDestinationTrackerReject;
					return true;
				}
			}
			else if (this.IsOverThreshold(usage, this.perDestinationConnectionThreshold, destination) && this.rejectBasedOnInboundProxyDestinationTrackingEnabled)
			{
				rejectResponse = SmtpResponse.InboundProxyDestinationTrackerReject;
				return true;
			}
			rejectResponse = SmtpResponse.Empty;
			return false;
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x000C4C6C File Offset: 0x000C2E6C
		public bool TryGetDiagnosticInfo(DiagnosableParameters parameters, out XElement diagnosticInfo)
		{
			if (this.trackingEnabled && (parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1 || parameters.Argument.Equals(this.trackerType.ToString(), StringComparison.InvariantCultureIgnoreCase)))
			{
				diagnosticInfo = this.GetDiagnosticInfo();
				return true;
			}
			diagnosticInfo = null;
			return false;
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x000C4CC3 File Offset: 0x000C2EC3
		public void UpdateReceiveConnectors(IEnumerable<ReceiveConnector> receiveConnectors)
		{
			ArgumentValidator.ThrowIfNull("receiveConnectors", receiveConnectors);
			if (!this.trackingEnabled)
			{
				return;
			}
			this.perDestinationConnectionThreshold = this.GetThresholdFromConnectors(receiveConnectors);
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x000C4CE6 File Offset: 0x000C2EE6
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<InboundProxyDestinationTracker>(this);
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x000C4CEE File Offset: 0x000C2EEE
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.logTimer != null)
			{
				this.logTimer.Dispose(true);
				this.logTimer = null;
			}
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000C4D10 File Offset: 0x000C2F10
		private bool IsOverThreshold(int currentValue, int threhsoldValue, string destination)
		{
			if (currentValue >= threhsoldValue)
			{
				this.eventLog.LogEvent((this.trackerType == InboundProxyTrackerType.InboundProxyDestinationTracker) ? TransportEventLogConstants.Tuple_InboundProxyDestinationsTrackerReject : TransportEventLogConstants.Tuple_InboundProxyAccountForestsTrackerReject, destination, new object[]
				{
					destination,
					currentValue,
					threhsoldValue
				});
				EventNotificationItem.PublishPeriodic(ExchangeComponent.FrontendTransport.Name, "InboundProxyDestinationsTrackerReject", null, string.Format("{0} will reject connections for destination {1} as current usage {2} is greater than or equal to threshold {3}", new object[]
				{
					this.trackerType,
					destination,
					currentValue,
					threhsoldValue
				}), destination, TimeSpan.FromMinutes(15.0), ResultSeverityLevel.Error, false);
				return true;
			}
			if ((double)currentValue >= 0.8 * (double)threhsoldValue)
			{
				this.eventLog.LogEvent((this.trackerType == InboundProxyTrackerType.InboundProxyDestinationTracker) ? TransportEventLogConstants.Tuple_InboundProxyDestinationsTrackerNearThreshold : TransportEventLogConstants.Tuple_InboundProxyAccountForestsTrackerNearThreshold, destination, new object[]
				{
					destination,
					currentValue,
					threhsoldValue
				});
				EventNotificationItem.PublishPeriodic(ExchangeComponent.FrontendTransport.Name, "InboundProxyDestinationsTrackerNearThreshold", null, string.Format("{0} shows that connections for next hop {1} is currently {2} and close to threshold {3}.", new object[]
				{
					this.trackerType,
					destination,
					currentValue,
					threhsoldValue
				}), destination, TimeSpan.FromMinutes(15.0), ResultSeverityLevel.Error, false);
			}
			return false;
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000C4E70 File Offset: 0x000C3070
		private XElement GetDiagnosticInfo()
		{
			XElement xelement = new XElement(this.trackerType.ToString());
			xelement.SetAttributeValue("PerDestinationThreshold", this.perDestinationConnectionThreshold);
			xelement.Add(this.smtpProxyTracker.GetDiagnosticInfo("Destination"));
			return xelement;
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x000C4ECC File Offset: 0x000C30CC
		private void LogData(object state)
		{
			this.eventLog.LogEvent(TransportEventLogConstants.Tuple_InboundProxyDestinationsTrackerDiagnosticInfo, null, new object[]
			{
				this.trackerType.ToString(),
				this.GetDiagnosticInfo()
			});
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x000C4F10 File Offset: 0x000C3110
		private int GetThresholdFromConnectors(IEnumerable<ReceiveConnector> connectors)
		{
			int num = 0;
			bool flag = false;
			foreach (ReceiveConnector receiveConnector in connectors)
			{
				if (receiveConnector.Enabled)
				{
					flag = true;
					num = Math.Max(num, receiveConnector.MaxInboundConnection.Value);
				}
			}
			if (flag)
			{
				return num * this.perDestinationConnectionPercentageThreshold / 100;
			}
			return 1000;
		}

		// Token: 0x0400180E RID: 6158
		private const int DefaultPerDestinationMaxConnectionsThreshold = 1000;

		// Token: 0x0400180F RID: 6159
		private readonly InboundProxyTrackerType trackerType;

		// Token: 0x04001810 RID: 6160
		private readonly bool trackingEnabled;

		// Token: 0x04001811 RID: 6161
		public bool rejectBasedOnInboundProxyDestinationTrackingEnabled;

		// Token: 0x04001812 RID: 6162
		private readonly int perDestinationConnectionPercentageThreshold;

		// Token: 0x04001813 RID: 6163
		private readonly InboundProxyDestinationTracker.TryGetDestinationConnectionThresholdDelegate tryGetDestinationConnectionThresholdDelegate;

		// Token: 0x04001814 RID: 6164
		private int perDestinationConnectionThreshold;

		// Token: 0x04001815 RID: 6165
		private readonly IExEventLog eventLog;

		// Token: 0x04001816 RID: 6166
		private GuardedTimer logTimer;

		// Token: 0x04001817 RID: 6167
		private readonly ConnectionsTracker smtpProxyTracker;

		// Token: 0x02000430 RID: 1072
		// (Invoke) Token: 0x06003158 RID: 12632
		public delegate bool TryGetDestinationConnectionThresholdDelegate(string destination, out int threshold);
	}
}
