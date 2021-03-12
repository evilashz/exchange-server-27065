using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004D5 RID: 1237
	internal class SmtpAvailabilityPerfCountersWrapper : IDisposable, ISmtpAvailabilityPerfCounters
	{
		// Token: 0x0600391A RID: 14618 RVA: 0x000E8BD0 File Offset: 0x000E6DD0
		public SmtpAvailabilityPerfCountersWrapper(ProcessTransportRole transportRole, string instanceName, int smtpAvailabilityMinConnectionsToMonitor)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("instanceName", instanceName);
			this.connectorInstance = instanceName;
			this.smtpAvailabilityMinConnectionsToMonitor = smtpAvailabilityMinConnectionsToMonitor;
			this.lastEventReportTime = DateTime.MinValue;
			this.inLowAvailabilityRedState = false;
			this.availabilityPercentage = new SlidingPercentageCounter(SmtpAvailabilityPerfCountersWrapper.SlidingCounterInterval, SmtpAvailabilityPerfCountersWrapper.SlidingCounterPrecision);
			this.failuresDueToMaxInboundConnectionLimit = new SlidingPercentageCounter(SmtpAvailabilityPerfCountersWrapper.SlidingCounterInterval, SmtpAvailabilityPerfCountersWrapper.SlidingCounterPrecision);
			this.failuresDueToWLIDDown = new SlidingPercentageCounter(SmtpAvailabilityPerfCountersWrapper.SlidingCounterInterval, SmtpAvailabilityPerfCountersWrapper.SlidingCounterPrecision);
			this.failuresDueToADDown = new SlidingPercentageCounter(SmtpAvailabilityPerfCountersWrapper.SlidingCounterInterval, SmtpAvailabilityPerfCountersWrapper.SlidingCounterPrecision);
			this.failuresDueToBackPressure = new SlidingPercentageCounter(SmtpAvailabilityPerfCountersWrapper.SlidingCounterInterval, SmtpAvailabilityPerfCountersWrapper.SlidingCounterPrecision);
			this.failuresDueToIOExceptions = new SlidingPercentageCounter(SmtpAvailabilityPerfCountersWrapper.SlidingCounterInterval, SmtpAvailabilityPerfCountersWrapper.SlidingCounterPrecision);
			this.failuresDueToTLSErrors = new SlidingPercentageCounter(SmtpAvailabilityPerfCountersWrapper.SlidingCounterInterval, SmtpAvailabilityPerfCountersWrapper.SlidingCounterPrecision);
			this.failuresDueToMaxLocalLoopCount = new SlidingTotalCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(1.0), () => DateTime.UtcNow);
			this.totalConnections = new SlidingPercentageCounter(SmtpAvailabilityPerfCountersWrapper.SlidingCounterInterval, SmtpAvailabilityPerfCountersWrapper.SlidingCounterPrecision);
			this.loopingMessagesLastHour = new SlidingTotalCounter(TimeSpan.FromHours(1.0), TimeSpan.FromMinutes(1.0), () => DateTime.UtcNow);
			this.refreshTimer = new GuardedTimer(delegate(object state)
			{
				this.RefreshMessageLoopsInLastHourCounter();
			}, null, TimeSpan.FromMinutes(5.0));
			try
			{
				SmtpAvailabilityPerfCounters.SetCategoryName(SmtpAvailabilityPerfCountersWrapper.perfCounterCategoryMap[transportRole]);
				this.perfCountersInstance = SmtpAvailabilityPerfCounters.GetInstance(instanceName);
			}
			catch (InvalidOperationException ex)
			{
				ExTraceGlobals.SmtpReceiveTracer.TraceError<string, InvalidOperationException>((long)this.GetHashCode(), "Failed to initialize performance counters instance '{0}': {1}", instanceName, ex);
				SmtpAvailabilityPerfCountersWrapper.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveAvailabilityCounterFailure, null, new object[]
				{
					instanceName,
					ex
				});
				this.perfCountersInstance = null;
			}
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x000E8DF4 File Offset: 0x000E6FF4
		protected SmtpAvailabilityPerfCountersWrapper()
		{
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x000E8E12 File Offset: 0x000E7012
		public void Dispose()
		{
			if (!this.disposed)
			{
				if (this.refreshTimer != null)
				{
					this.refreshTimer.Change(-1, -1);
					this.refreshTimer.Dispose(true);
				}
				this.disposed = true;
			}
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x000E8E48 File Offset: 0x000E7048
		public virtual void UpdatePerformanceCounters(LegitimateSmtpAvailabilityCategory category)
		{
			if (this.perfCountersInstance == null)
			{
				return;
			}
			lock (this.syncObject)
			{
				this.totalConnections.AddNumerator(1L);
				this.availabilityPercentage.AddDenominator(1L);
				this.failuresDueToMaxInboundConnectionLimit.AddDenominator(1L);
				this.failuresDueToWLIDDown.AddDenominator(1L);
				this.failuresDueToADDown.AddDenominator(1L);
				this.failuresDueToBackPressure.AddDenominator(1L);
				this.failuresDueToIOExceptions.AddDenominator(1L);
				this.failuresDueToTLSErrors.AddDenominator(1L);
				switch (category)
				{
				case LegitimateSmtpAvailabilityCategory.SuccessfulSubmission:
					this.availabilityPercentage.AddNumerator(1L);
					break;
				case LegitimateSmtpAvailabilityCategory.RejectDueToMaxInboundConnectionLimit:
					this.failuresDueToMaxInboundConnectionLimit.AddNumerator(1L);
					break;
				case LegitimateSmtpAvailabilityCategory.RejectDueToWLIDDown:
					this.failuresDueToWLIDDown.AddNumerator(1L);
					break;
				case LegitimateSmtpAvailabilityCategory.RejectDueToADDown:
					this.failuresDueToADDown.AddNumerator(1L);
					break;
				case LegitimateSmtpAvailabilityCategory.RejectDueToBackPressure:
					this.failuresDueToBackPressure.AddNumerator(1L);
					break;
				case LegitimateSmtpAvailabilityCategory.RejectDueToIOException:
					this.failuresDueToIOExceptions.AddNumerator(1L);
					break;
				case LegitimateSmtpAvailabilityCategory.RejectDueToTLSError:
					this.failuresDueToTLSErrors.AddNumerator(1L);
					break;
				case LegitimateSmtpAvailabilityCategory.RejectDueToMaxLocalLoopCount:
					this.failuresDueToMaxLocalLoopCount.AddValue(1L);
					break;
				}
				this.perfCountersInstance.TotalConnections.RawValue = this.totalConnections.Numerator;
				this.perfCountersInstance.AvailabilityPercentage.RawValue = (long)this.availabilityPercentage.GetSlidingPercentage();
				if (this.perfCountersInstance.TotalConnections.RawValue > (long)this.smtpAvailabilityMinConnectionsToMonitor)
				{
					this.perfCountersInstance.ActivityPercentage.RawValue = (long)this.availabilityPercentage.GetSlidingPercentage();
				}
				else
				{
					this.perfCountersInstance.ActivityPercentage.RawValue = 100L;
				}
				if (this.perfCountersInstance.ActivityPercentage.RawValue < 99L)
				{
					if (DateTime.UtcNow - this.lastEventReportTime > SmtpAvailabilityPerfCountersWrapper.MinReportInterval)
					{
						this.lastEventReportTime = DateTime.UtcNow;
						this.inLowAvailabilityRedState = true;
						SmtpAvailabilityPerfCountersWrapper.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveConnectorAvailabilityLow, null, new object[]
						{
							this.connectorInstance,
							this.perfCountersInstance.ActivityPercentage.RawValue
						});
						string notificationReason = string.Format("The SMTP availability of receive connector {0} was low ({1} percent) in the last 15 minutes.", this.connectorInstance, this.perfCountersInstance.ActivityPercentage.RawValue);
						EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "SmtpReceiveConnectorAvailabilityLow", null, notificationReason, ResultSeverityLevel.Error, false);
					}
				}
				else if (this.perfCountersInstance.ActivityPercentage.RawValue == 100L && this.inLowAvailabilityRedState)
				{
					this.inLowAvailabilityRedState = false;
					SmtpAvailabilityPerfCountersWrapper.eventLogger.LogEvent(TransportEventLogConstants.Tuple_SmtpReceiveConnectorAvailabilityNormal, null, new object[]
					{
						this.connectorInstance,
						this.perfCountersInstance.ActivityPercentage.RawValue
					});
					string notificationReason2 = string.Format("The SMTP availability of receive connector {0} was normal ({1} percent) in the last 15 minutes.", this.connectorInstance, this.perfCountersInstance.ActivityPercentage.RawValue);
					EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "SmtpReceiveConnectorAvailabilityLow", null, notificationReason2, ResultSeverityLevel.Informational, false);
				}
				this.perfCountersInstance.FailuresDueToMaxInboundConnectionLimit.RawValue = (long)this.failuresDueToMaxInboundConnectionLimit.GetSlidingPercentage();
				this.perfCountersInstance.FailuresDueToWLIDDown.RawValue = (long)this.failuresDueToWLIDDown.GetSlidingPercentage();
				this.perfCountersInstance.FailuresDueToADDown.RawValue = (long)this.failuresDueToADDown.GetSlidingPercentage();
				this.perfCountersInstance.FailuresDueToBackPressure.RawValue = (long)this.failuresDueToBackPressure.GetSlidingPercentage();
				this.perfCountersInstance.FailuresDueToIOExceptions.RawValue = (long)this.failuresDueToIOExceptions.GetSlidingPercentage();
				this.perfCountersInstance.FailuresDueToTLSErrors.RawValue = (long)this.failuresDueToTLSErrors.GetSlidingPercentage();
				this.perfCountersInstance.FailuresDueToMaxLocalLoopCount.RawValue = this.failuresDueToMaxLocalLoopCount.Sum;
			}
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x000E9268 File Offset: 0x000E7468
		public void IncrementMessageLoopsInLastHourCounter(long incrementValue)
		{
			if (this.perfCountersInstance == null)
			{
				return;
			}
			lock (this.syncLoopingMessagesObject)
			{
				this.loopingMessagesLastHour.AddValue(incrementValue);
				this.perfCountersInstance.LoopingMessagesLastHour.RawValue = this.loopingMessagesLastHour.Sum;
			}
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x000E92D4 File Offset: 0x000E74D4
		private void RefreshMessageLoopsInLastHourCounter()
		{
			if (this.perfCountersInstance == null)
			{
				return;
			}
			lock (this.syncLoopingMessagesObject)
			{
				this.perfCountersInstance.LoopingMessagesLastHour.RawValue = this.loopingMessagesLastHour.Sum;
			}
		}

		// Token: 0x04001D0A RID: 7434
		private static readonly IDictionary<ProcessTransportRole, string> perfCounterCategoryMap = new Dictionary<ProcessTransportRole, string>
		{
			{
				ProcessTransportRole.Edge,
				"MSExchangeTransport SmtpAvailability"
			},
			{
				ProcessTransportRole.Hub,
				"MSExchangeTransport SmtpAvailability"
			},
			{
				ProcessTransportRole.FrontEnd,
				"MSExchangeFrontEndTransport SmtpAvailability"
			},
			{
				ProcessTransportRole.MailboxDelivery,
				"MSExchange Delivery SmtpAvailability"
			}
		};

		// Token: 0x04001D0B RID: 7435
		private static readonly TimeSpan SlidingCounterInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x04001D0C RID: 7436
		private static readonly TimeSpan SlidingCounterPrecision = TimeSpan.FromSeconds(1.0);

		// Token: 0x04001D0D RID: 7437
		private static readonly TimeSpan MinReportInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x04001D0E RID: 7438
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.SmtpReceiveTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x04001D0F RID: 7439
		private readonly int smtpAvailabilityMinConnectionsToMonitor;

		// Token: 0x04001D10 RID: 7440
		private SlidingPercentageCounter availabilityPercentage;

		// Token: 0x04001D11 RID: 7441
		private SlidingPercentageCounter failuresDueToMaxInboundConnectionLimit;

		// Token: 0x04001D12 RID: 7442
		private SlidingPercentageCounter failuresDueToWLIDDown;

		// Token: 0x04001D13 RID: 7443
		private SlidingPercentageCounter failuresDueToADDown;

		// Token: 0x04001D14 RID: 7444
		private SlidingPercentageCounter failuresDueToBackPressure;

		// Token: 0x04001D15 RID: 7445
		private SlidingPercentageCounter failuresDueToIOExceptions;

		// Token: 0x04001D16 RID: 7446
		private SlidingPercentageCounter failuresDueToTLSErrors;

		// Token: 0x04001D17 RID: 7447
		private SlidingTotalCounter failuresDueToMaxLocalLoopCount;

		// Token: 0x04001D18 RID: 7448
		private SlidingPercentageCounter totalConnections;

		// Token: 0x04001D19 RID: 7449
		private SmtpAvailabilityPerfCountersInstance perfCountersInstance;

		// Token: 0x04001D1A RID: 7450
		private string connectorInstance;

		// Token: 0x04001D1B RID: 7451
		private DateTime lastEventReportTime;

		// Token: 0x04001D1C RID: 7452
		private bool inLowAvailabilityRedState;

		// Token: 0x04001D1D RID: 7453
		private object syncObject = new object();

		// Token: 0x04001D1E RID: 7454
		private SlidingTotalCounter loopingMessagesLastHour;

		// Token: 0x04001D1F RID: 7455
		private object syncLoopingMessagesObject = new object();

		// Token: 0x04001D20 RID: 7456
		private GuardedTimer refreshTimer;

		// Token: 0x04001D21 RID: 7457
		private bool disposed;
	}
}
