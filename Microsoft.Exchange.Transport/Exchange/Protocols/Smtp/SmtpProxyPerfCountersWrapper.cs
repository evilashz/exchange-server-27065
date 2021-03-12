using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004FC RID: 1276
	internal class SmtpProxyPerfCountersWrapper
	{
		// Token: 0x06003AC2 RID: 15042 RVA: 0x000F470C File Offset: 0x000F290C
		public SmtpProxyPerfCountersWrapper(string instanceName)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("instanceName", instanceName);
			this.connectorInstance = instanceName;
			this.proxySetupFailurePercentage = new SlidingPercentageCounter(SmtpProxyPerfCountersWrapper.SlidingCounterInterval, SmtpProxyPerfCountersWrapper.SlidingCounterPrecision);
			this.proxyAttempts = new SlidingTotalCounter(SmtpProxyPerfCountersWrapper.SlidingCounterInterval, SmtpProxyPerfCountersWrapper.SlidingCounterPrecision);
			this.successfulProxyAttempts = new SlidingTotalCounter(SmtpProxyPerfCountersWrapper.SlidingCounterInterval, SmtpProxyPerfCountersWrapper.SlidingCounterPrecision);
			this.userLookupFailures = new SlidingTotalCounter(SmtpProxyPerfCountersWrapper.SlidingCounterInterval, SmtpProxyPerfCountersWrapper.SlidingCounterPrecision);
			this.backEndLocatorFailures = new SlidingTotalCounter(SmtpProxyPerfCountersWrapper.SlidingCounterInterval, SmtpProxyPerfCountersWrapper.SlidingCounterPrecision);
			this.dnsLookupFailures = new SlidingTotalCounter(SmtpProxyPerfCountersWrapper.SlidingCounterInterval, SmtpProxyPerfCountersWrapper.SlidingCounterPrecision);
			this.connectionFailures = new SlidingTotalCounter(SmtpProxyPerfCountersWrapper.SlidingCounterInterval, SmtpProxyPerfCountersWrapper.SlidingCounterPrecision);
			this.protocolErrors = new SlidingTotalCounter(SmtpProxyPerfCountersWrapper.SlidingCounterInterval, SmtpProxyPerfCountersWrapper.SlidingCounterPrecision);
			this.socketErrors = new SlidingTotalCounter(SmtpProxyPerfCountersWrapper.SlidingCounterInterval, SmtpProxyPerfCountersWrapper.SlidingCounterPrecision);
			try
			{
				this.perfCountersInstance = SmtpProxyPerfCounters.GetInstance(instanceName);
			}
			catch (InvalidOperationException ex)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceError<string, InvalidOperationException>((long)this.GetHashCode(), "Failed to initialize performance counters instance '{0}': {1}", instanceName, ex);
				SmtpProxyPerfCountersWrapper.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveProxyCounterFailure, null, new object[]
				{
					instanceName,
					ex
				});
				this.perfCountersInstance = null;
			}
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x000F485C File Offset: 0x000F2A5C
		public void UpdateOnProxyStepFailure(SessionSetupFailureReason failureReason)
		{
			if (this.perfCountersInstance == null)
			{
				return;
			}
			lock (this.syncObject)
			{
				switch (failureReason)
				{
				case SessionSetupFailureReason.None:
				case SessionSetupFailureReason.Shutdown:
					break;
				case SessionSetupFailureReason.UserLookupFailure:
					this.userLookupFailures.AddValue(1L);
					break;
				case SessionSetupFailureReason.DnsLookupFailure:
					this.dnsLookupFailures.AddValue(1L);
					break;
				case SessionSetupFailureReason.ConnectionFailure:
					this.connectionFailures.AddValue(1L);
					break;
				case SessionSetupFailureReason.ProtocolError:
					this.protocolErrors.AddValue(1L);
					break;
				case SessionSetupFailureReason.SocketError:
					this.socketErrors.AddValue(1L);
					break;
				case SessionSetupFailureReason.BackEndLocatorFailure:
					this.backEndLocatorFailures.AddValue(1L);
					break;
				default:
					throw new InvalidOperationException("Invalid proxy failure reason");
				}
				this.UpdateAllCounters();
			}
		}

		// Token: 0x06003AC4 RID: 15044 RVA: 0x000F493C File Offset: 0x000F2B3C
		public void UpdateOnProxyFailure()
		{
			if (this.perfCountersInstance == null)
			{
				return;
			}
			lock (this.syncObject)
			{
				this.proxyAttempts.AddValue(1L);
				this.proxySetupFailurePercentage.AddDenominator(1L);
				this.proxySetupFailurePercentage.AddNumerator(1L);
			}
			if (this.proxySetupFailurePercentage.GetSlidingPercentage() >= 15.0 && this.proxyAttempts.Sum >= 100L)
			{
				SmtpProxyPerfCountersWrapper.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveTooManyProxySessionFailures, this.connectorInstance, new object[0]);
			}
			this.UpdateAllCounters();
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x000F49F4 File Offset: 0x000F2BF4
		public void UpdateOnProxySuccess()
		{
			if (this.perfCountersInstance == null)
			{
				return;
			}
			lock (this.syncObject)
			{
				this.proxyAttempts.AddValue(1L);
				this.proxySetupFailurePercentage.AddDenominator(1L);
				this.successfulProxyAttempts.AddValue(1L);
				this.UpdateAllCounters();
			}
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x000F4A68 File Offset: 0x000F2C68
		public void UpdateBytesProxied(int bytesProxied)
		{
			if (this.perfCountersInstance == null)
			{
				return;
			}
			this.perfCountersInstance.TotalBytesProxied.IncrementBy((long)bytesProxied);
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x000F4A86 File Offset: 0x000F2C86
		public void IncrementOutboundConnectionsCurrent()
		{
			if (this.perfCountersInstance == null)
			{
				return;
			}
			this.perfCountersInstance.OutboundConnectionsCurrent.Increment();
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x000F4AA2 File Offset: 0x000F2CA2
		public void DecrementOutboundConnectionsCurrent()
		{
			if (this.perfCountersInstance == null)
			{
				return;
			}
			this.perfCountersInstance.OutboundConnectionsCurrent.Decrement();
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x000F4ABE File Offset: 0x000F2CBE
		public void DecrementInboundConnectionsCurrent()
		{
			if (this.perfCountersInstance == null)
			{
				return;
			}
			this.perfCountersInstance.InboundConnectionsCurrent.Decrement();
		}

		// Token: 0x06003ACA RID: 15050 RVA: 0x000F4ADA File Offset: 0x000F2CDA
		public void IncrementInboundConnectionsCurrent()
		{
			if (this.perfCountersInstance == null)
			{
				return;
			}
			this.perfCountersInstance.InboundConnectionsCurrent.Increment();
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x000F4AF6 File Offset: 0x000F2CF6
		public void IncrementOutboundConnectionsTotal()
		{
			if (this.perfCountersInstance == null)
			{
				return;
			}
			this.perfCountersInstance.OutboundConnectionsTotal.Increment();
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x000F4B12 File Offset: 0x000F2D12
		public void IncrementMessagesProxiedTotalBy(int count)
		{
			if (this.perfCountersInstance == null)
			{
				return;
			}
			this.perfCountersInstance.MessagesProxiedTotal.IncrementBy((long)count);
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x000F4B30 File Offset: 0x000F2D30
		private void UpdateAllCounters()
		{
			this.perfCountersInstance.TotalProxyAttempts.RawValue = this.proxyAttempts.Sum;
			this.perfCountersInstance.PercentageProxySetupFailures.RawValue = (long)this.proxySetupFailurePercentage.GetSlidingPercentage();
			this.perfCountersInstance.TotalProxyUserLookupFailures.RawValue = this.userLookupFailures.Sum;
			this.perfCountersInstance.TotalProxyBackEndLocatorFailures.RawValue = this.backEndLocatorFailures.Sum;
			this.perfCountersInstance.TotalProxyDnsLookupFailures.RawValue = this.dnsLookupFailures.Sum;
			this.perfCountersInstance.TotalProxyConnectionFailures.RawValue = this.connectionFailures.Sum;
			this.perfCountersInstance.TotalProxyProtocolErrors.RawValue = this.protocolErrors.Sum;
			this.perfCountersInstance.TotalProxySocketErrors.RawValue = this.socketErrors.Sum;
			this.perfCountersInstance.TotalSuccessfulProxySessions.RawValue = this.successfulProxyAttempts.Sum;
		}

		// Token: 0x04001D93 RID: 7571
		private static readonly TimeSpan SlidingCounterInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x04001D94 RID: 7572
		private static readonly TimeSpan SlidingCounterPrecision = TimeSpan.FromSeconds(1.0);

		// Token: 0x04001D95 RID: 7573
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.SmtpReceiveTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x04001D96 RID: 7574
		private readonly string connectorInstance;

		// Token: 0x04001D97 RID: 7575
		private SlidingPercentageCounter proxySetupFailurePercentage;

		// Token: 0x04001D98 RID: 7576
		private SlidingTotalCounter proxyAttempts;

		// Token: 0x04001D99 RID: 7577
		private SlidingTotalCounter successfulProxyAttempts;

		// Token: 0x04001D9A RID: 7578
		private SlidingTotalCounter userLookupFailures;

		// Token: 0x04001D9B RID: 7579
		private SlidingTotalCounter backEndLocatorFailures;

		// Token: 0x04001D9C RID: 7580
		private SlidingTotalCounter dnsLookupFailures;

		// Token: 0x04001D9D RID: 7581
		private SlidingTotalCounter connectionFailures;

		// Token: 0x04001D9E RID: 7582
		private SlidingTotalCounter protocolErrors;

		// Token: 0x04001D9F RID: 7583
		private SlidingTotalCounter socketErrors;

		// Token: 0x04001DA0 RID: 7584
		private SmtpProxyPerfCountersInstance perfCountersInstance;

		// Token: 0x04001DA1 RID: 7585
		private object syncObject = new object();
	}
}
