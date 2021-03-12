using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Metering.ResourceMonitoring;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring.Exchange;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200004B RID: 75
	internal class ResourceManagerEventLogger
	{
		// Token: 0x060001D5 RID: 469 RVA: 0x00008F10 File Offset: 0x00007110
		public virtual void LogResourcePressureChangedEvent(ResourceUses previousResourceUses, ResourceUses currentResourceUses, string currentState)
		{
			ResourceManagerEventLogger.eventLogger.LogEvent((currentResourceUses > previousResourceUses) ? TransportEventLogConstants.Tuple_ResourceUtilizationUp : TransportEventLogConstants.Tuple_ResourceUtilizationDown, null, new object[]
			{
				ResourceManager.MapToLocalizedString(previousResourceUses),
				ResourceManager.MapToLocalizedString(currentResourceUses),
				currentState
			});
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00008F58 File Offset: 0x00007158
		public virtual void LogResourcePressureChangedEvent(ResourceUse aggregateResourceUse, string currentState)
		{
			if (aggregateResourceUse.CurrentUseLevel != aggregateResourceUse.PreviousUseLevel)
			{
				ResourceManagerEventLogger.eventLogger.LogEvent((aggregateResourceUse.CurrentUseLevel > aggregateResourceUse.PreviousUseLevel) ? TransportEventLogConstants.Tuple_ResourceUtilizationUp : TransportEventLogConstants.Tuple_ResourceUtilizationDown, null, new object[]
				{
					this.localizedUseLevel[aggregateResourceUse.PreviousUseLevel],
					this.localizedUseLevel[aggregateResourceUse.CurrentUseLevel],
					currentState
				});
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00008FD0 File Offset: 0x000071D0
		public virtual void LogLowOnDiskSpaceEvent(ResourceUses resourceUses, string currentState)
		{
			ResourceManagerEventLogger.eventLogger.LogEvent(TransportEventLogConstants.Tuple_DiskSpaceLow, resourceUses.ToString(), new object[]
			{
				currentState
			});
			EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "DiskSpaceLow", null, "Microsoft Exchange Transport is rejecting message submissions because the available disk space has dropped below the configured threshold.", ResultSeverityLevel.Warning, false);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00009020 File Offset: 0x00007220
		public virtual void LogLowOnDiskSpaceEvent(UseLevel resourceUse, string currentState)
		{
			ResourceManagerEventLogger.eventLogger.LogEvent(TransportEventLogConstants.Tuple_DiskSpaceLow, resourceUse.ToString(), new object[]
			{
				currentState
			});
			EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "DiskSpaceLow", null, "Microsoft Exchange Transport is rejecting message submissions because the available disk space has dropped below the configured threshold.", ResultSeverityLevel.Warning, false);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00009070 File Offset: 0x00007270
		public virtual void LogPrivateBytesHighEvent(string currentState)
		{
			ResourceManagerEventLogger.eventLogger.LogEvent(TransportEventLogConstants.Tuple_PrivateBytesHigh, string.Empty, new object[]
			{
				currentState
			});
			this.RaiseActiveMonitoringErrorNotification(ResourceManagerEventLogger.transportRejectingMessageSubmissionsNotification, currentState);
		}

		// Token: 0x060001DA RID: 474 RVA: 0x000090AC File Offset: 0x000072AC
		private void RaiseActiveMonitoringErrorNotification(string notificationEvent, string message)
		{
			EventNotificationItem eventNotificationItem = new EventNotificationItem(ResourceManagerEventLogger.transportNotificationServiceName, notificationEvent, message, ResultSeverityLevel.Error);
			eventNotificationItem.Publish(false);
		}

		// Token: 0x0400011C RID: 284
		private static readonly string transportNotificationServiceName = ExchangeComponent.Transport.Name;

		// Token: 0x0400011D RID: 285
		private static readonly string transportRejectingMessageSubmissionsNotification = TransportNotificationEvent.TransportRejectingMessageSubmissions.ToString();

		// Token: 0x0400011E RID: 286
		private static ExEventLog eventLogger = new ExEventLog(ExTraceGlobals.ResourceManagerTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x0400011F RID: 287
		private readonly Dictionary<UseLevel, string> localizedUseLevel = new Dictionary<UseLevel, string>
		{
			{
				UseLevel.Low,
				Strings.LowResourceUses
			},
			{
				UseLevel.Medium,
				Strings.MediumResourceUses
			},
			{
				UseLevel.High,
				Strings.HighResourceUses
			}
		};
	}
}
