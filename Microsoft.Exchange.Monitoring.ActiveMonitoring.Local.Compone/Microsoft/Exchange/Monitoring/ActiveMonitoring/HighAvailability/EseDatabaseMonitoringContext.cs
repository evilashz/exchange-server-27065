using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability
{
	// Token: 0x020001A0 RID: 416
	internal sealed class EseDatabaseMonitoringContext : MonitoringContextBase
	{
		// Token: 0x06000BFC RID: 3068 RVA: 0x0004CFF2 File Offset: 0x0004B1F2
		public EseDatabaseMonitoringContext(IMaintenanceWorkBroker broker, LocalEndpointManager endpointManager, TracingContext traceContext) : base(broker, endpointManager, traceContext)
		{
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0004D064 File Offset: 0x0004B264
		public override void CreateContext()
		{
			bool isDataCenter = LocalEndpointManager.IsDataCenter;
			using (IEnumerator<MailboxDatabaseInfo> enumerator = base.EndpointManager.MailboxDatabaseEndpoint.UnverifiedMailboxDatabaseInfoCollectionForBackendLiveIdAuthenticationProbe.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					MailboxDatabaseInfo dbInfo = enumerator.Current;
					base.InvokeCatchAndLog(delegate
					{
						this.CreateDbErrorMonitor(dbInfo);
					});
					base.InvokeCatchAndLog(delegate
					{
						this.CreateInconsistentDataMonitor(dbInfo);
					});
					base.InvokeCatchAndLog(delegate
					{
						this.CreateLostFlushMonitor(dbInfo);
					});
					if (isDataCenter)
					{
						base.InvokeCatchAndLog(delegate
						{
							this.CreateSinglePageLogicalCorruptionMonitor(dbInfo);
						});
						base.InvokeCatchAndLog(delegate
						{
							this.CreateDbDivergenceMonitor(dbInfo);
						});
					}
				}
			}
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0004D168 File Offset: 0x0004B368
		private static bool EseDbTimeTooNewEventMatcher(EventLogNotification.EventRecordInternal eventRecord)
		{
			if (eventRecord.Id == 516)
			{
				IList<EventProperty> properties = eventRecord.EventRecord.Properties;
				return properties != null && properties.Count >= 8 && properties[7].Value != null && properties[7].Value.ToString().Equals(-567.ToString());
			}
			return true;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0004D1D4 File Offset: 0x0004B3D4
		private static bool EseDbTimeTooOldEventMatcher(EventLogNotification.EventRecordInternal eventRecord)
		{
			if (eventRecord.Id == 516)
			{
				IList<EventProperty> properties = eventRecord.EventRecord.Properties;
				return properties != null && properties.Count >= 8 && properties[7].Value != null && properties[7].Value.ToString().Equals(-566.ToString());
			}
			return true;
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0004D240 File Offset: 0x0004B440
		private static void EseEventNotificationProcessor(EventLogNotification.EventRecordInternal eventRecord, ref EventLogNotification.EventNotificationMetadata eventNotification)
		{
			IList<EventProperty> properties = eventRecord.EventRecord.Properties;
			if (properties != null && properties.Count >= 3)
			{
				string text = (properties[2].Value == null) ? "NULL" : properties[2].Value.ToString().Split(new char[]
				{
					':'
				})[0];
				eventNotification.StateAttribute3 = text;
				if (properties[2].Value != null)
				{
					eventNotification.TagName = text;
				}
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x0004D2C0 File Offset: 0x0004B4C0
		private void CreateLostFlushMonitor(MailboxDatabaseInfo dbInfo)
		{
			string name = "EseLostFlushEventProbe";
			string name2 = "EseLostFlushMonitor";
			string name3 = "EseLostFlushEscalate";
			EventLogSubscription eventLogSubscription = new EventLogSubscription(name, TimeSpan.FromSeconds(600.0), new EventMatchingRule("Application", "ESE", new int[]
			{
				530
			}, 2, false, false, null, new EventMatchingRule.CustomNotification(EseDatabaseMonitoringContext.EseEventNotificationProcessor)), null, null, null);
			EventLogNotification.Instance.AddSubscription(eventLogSubscription);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(name2, EventLogNotification.ConstructResultMask(eventLogSubscription.Name, dbInfo.MailboxDatabaseName), HighAvailabilityConstants.ServiceName, ExchangeComponent.DataProtection, 1, true, 600);
			monitorDefinition.TargetResource = dbInfo.MailboxDatabaseName;
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = "Validate HA health is not impacted by ESE issues";
			base.AddChainedResponders(ref monitorDefinition, new MonitorStateResponderTuple[]
			{
				new MonitorStateResponderTuple
				{
					MonitorState = new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
					Responder = EscalateResponder.CreateDefinition(name3, HighAvailabilityConstants.ServiceName, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), monitorDefinition.TargetResource, ServiceHealthStatus.Unhealthy, "ESE", Strings.EseLostFlushDetectedEscalationSubject(HighAvailabilityConstants.ServiceName, Environment.MachineName, dbInfo.MailboxDatabaseName), Strings.EseLostFlushDetectedEscalationMessage(Environment.MachineName, dbInfo.MailboxDatabaseName), true, NotificationServiceClass.Scheduled, 18000, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false)
				}
			});
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0004D420 File Offset: 0x0004B620
		private void CreateDbErrorMonitor(MailboxDatabaseInfo dbInfo)
		{
			string name = "EseDbTimeTooNewEventProbe";
			string name2 = "EseDbTimeTooOldEventProbe";
			string text = "EseDbTimeTooNewMonitor";
			string name3 = "EseDbTimeTooOldMonitor";
			string name4 = "EseDbTimeTooNewEscalate";
			string name5 = "EseDbTimeTooOldEscalate";
			string responderName = "EseDbTimeTooNewCollectAndMerge";
			EventLogSubscription eventLogSubscription = new EventLogSubscription(name, TimeSpan.FromSeconds(600.0), new EventMatchingRule("Application", "ESE", new int[]
			{
				516,
				538
			}, 2, true, true, new EventMatchingRule.CustomMatching(EseDatabaseMonitoringContext.EseDbTimeTooNewEventMatcher), new EventMatchingRule.CustomNotification(EseDatabaseMonitoringContext.EseEventNotificationProcessor)), null, null, null);
			EventLogNotification.Instance.AddSubscription(eventLogSubscription);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(text, EventLogNotification.ConstructResultMask(eventLogSubscription.Name, dbInfo.MailboxDatabaseName), HighAvailabilityConstants.ServiceName, ExchangeComponent.DataProtection, 1, true, 600);
			monitorDefinition.TargetResource = dbInfo.MailboxDatabaseName;
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = "Validate HA health is not impacted by ESE issues";
			List<MonitorStateResponderTuple> list = new List<MonitorStateResponderTuple>
			{
				new MonitorStateResponderTuple
				{
					MonitorState = new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
					Responder = EscalateResponder.CreateDefinition(name4, HighAvailabilityConstants.ServiceName, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), monitorDefinition.TargetResource, ServiceHealthStatus.Unhealthy, "ESE", Strings.EseDbTimeAdvanceEscalationSubject(HighAvailabilityConstants.ServiceName, Environment.MachineName, dbInfo.MailboxDatabaseName), Strings.EseDbTimeAdvanceEscalationMessage(Environment.MachineName, dbInfo.MailboxDatabaseName), true, NotificationServiceClass.Urgent, 18000, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false)
				}
			};
			if (LocalEndpointManager.IsDataCenter)
			{
				list.Add(new MonitorStateResponderTuple
				{
					MonitorState = new MonitorStateTransition(ServiceHealthStatus.Unhealthy, list[list.Count - 1].MonitorState.TransitionTimeout.Add(TimeSpan.FromMinutes(1.0))),
					Responder = CollectAndMergeResponder.CreateDefinition(responderName, text, ServiceHealthStatus.Unrecoverable2, dbInfo.MailboxDatabaseName, "Exchange", true)
				});
			}
			base.AddChainedResponders(ref monitorDefinition, list.ToArray());
			EventLogSubscription eventLogSubscription2 = new EventLogSubscription(name2, TimeSpan.FromSeconds(600.0), new EventMatchingRule("Application", "ESE", new int[]
			{
				516,
				539
			}, 2, true, true, new EventMatchingRule.CustomMatching(EseDatabaseMonitoringContext.EseDbTimeTooOldEventMatcher), new EventMatchingRule.CustomNotification(EseDatabaseMonitoringContext.EseEventNotificationProcessor)), null, null, null);
			EventLogNotification.Instance.AddSubscription(eventLogSubscription2);
			MonitorDefinition monitorDefinition2 = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(name3, EventLogNotification.ConstructResultMask(eventLogSubscription2.Name, dbInfo.MailboxDatabaseName), HighAvailabilityConstants.ServiceName, ExchangeComponent.DataProtection, 1, true, 600);
			monitorDefinition2.TargetResource = dbInfo.MailboxDatabaseName;
			monitorDefinition2.ServicePriority = 0;
			monitorDefinition2.ScenarioDescription = "Validate HA health is not impacted by ESE issues";
			base.AddChainedResponders(ref monitorDefinition2, new MonitorStateResponderTuple[]
			{
				new MonitorStateResponderTuple
				{
					MonitorState = new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
					Responder = EscalateResponder.CreateDefinition(name5, HighAvailabilityConstants.ServiceName, monitorDefinition2.Name, monitorDefinition2.ConstructWorkItemResultName(), monitorDefinition2.TargetResource, ServiceHealthStatus.Unhealthy, "ESE", Strings.EseDbTimeSmallerEscalationSubject(HighAvailabilityConstants.ServiceName, Environment.MachineName, dbInfo.MailboxDatabaseName), Strings.EseDbTimeSmallerEscalationMessage(Environment.MachineName, dbInfo.MailboxDatabaseName), true, NotificationServiceClass.Scheduled, 18000, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false)
				}
			});
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0004D780 File Offset: 0x0004B980
		private void CreateInconsistentDataMonitor(MailboxDatabaseInfo dbInfo)
		{
			string name = "EseInconsistentDataEventProbe";
			string name2 = "EseInconsistentDataMonitor";
			string name3 = "EseInconsistentDataEscalate";
			EventLogSubscription eventLogSubscription = new EventLogSubscription(name, TimeSpan.FromSeconds(600.0), new EventMatchingRule("Application", "ESE", new int[]
			{
				447,
				448
			}, 2, false, false, null, new EventMatchingRule.CustomNotification(EseDatabaseMonitoringContext.EseEventNotificationProcessor)), null, null, null);
			EventLogNotification.Instance.AddSubscription(eventLogSubscription);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(name2, EventLogNotification.ConstructResultMask(eventLogSubscription.Name, dbInfo.MailboxDatabaseName), HighAvailabilityConstants.ServiceName, ExchangeComponent.DataProtection, 1, true, 600);
			monitorDefinition.TargetResource = dbInfo.MailboxDatabaseName;
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = "Validate HA health is not impacted by ESE issues";
			base.AddChainedResponders(ref monitorDefinition, new MonitorStateResponderTuple[]
			{
				new MonitorStateResponderTuple
				{
					MonitorState = new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
					Responder = EscalateResponder.CreateDefinition(name3, HighAvailabilityConstants.ServiceName, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), monitorDefinition.TargetResource, ServiceHealthStatus.Unhealthy, "ESE", Strings.EseInconsistentDataDetectedEscalationSubject(HighAvailabilityConstants.ServiceName, Environment.MachineName, dbInfo.MailboxDatabaseName), Strings.EseInconsistentDataDetectedEscalationMessage(Environment.MachineName, dbInfo.MailboxDatabaseName), true, NotificationServiceClass.Scheduled, 18000, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false)
				}
			});
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0004D900 File Offset: 0x0004BB00
		private void CreateSinglePageLogicalCorruptionMonitor(MailboxDatabaseInfo dbInfo)
		{
			string name = "EseSinglePageLogicalCorruptionEventProbe";
			string name2 = "EseSinglePageLogicalCorruptionMonitor";
			string name3 = "EseSinglePageLogicalCorruptionEscalate";
			EventLogSubscription eventLogSubscription = new EventLogSubscription(name, TimeSpan.FromSeconds(600.0), new EventMatchingRule("Application", "ESE", new int[]
			{
				475,
				476,
				497,
				517,
				537,
				542
			}, 2, false, false, null, new EventMatchingRule.CustomNotification(EseDatabaseMonitoringContext.EseEventNotificationProcessor)), null, null, null);
			EventLogNotification.Instance.AddSubscription(eventLogSubscription);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(name2, EventLogNotification.ConstructResultMask(eventLogSubscription.Name, dbInfo.MailboxDatabaseName), HighAvailabilityConstants.ServiceName, ExchangeComponent.DataProtection, 1, true, 600);
			monitorDefinition.TargetResource = dbInfo.MailboxDatabaseName;
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = "Validate HA health is not impacted by ESE issues";
			base.AddChainedResponders(ref monitorDefinition, new MonitorStateResponderTuple[]
			{
				new MonitorStateResponderTuple
				{
					MonitorState = new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
					Responder = EscalateResponder.CreateDefinition(name3, HighAvailabilityConstants.ServiceName, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), monitorDefinition.TargetResource, ServiceHealthStatus.Unhealthy, "ESE", Strings.EseSinglePageLogicalCorruptionDetectedSubject(HighAvailabilityConstants.ServiceName, Environment.MachineName, dbInfo.MailboxDatabaseName), Strings.EseSinglePageLogicalCorruptionDetectedEscalationMessage(Environment.MachineName, dbInfo.MailboxDatabaseName), true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false)
				}
			});
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0004DA60 File Offset: 0x0004BC60
		private void CreateDbDivergenceMonitor(MailboxDatabaseInfo dbInfo)
		{
			string name = "EseDbDivergenceEventProbe";
			string name2 = "EseDbDivergenceMonitor";
			string name3 = "EseDbDivergenceEscalate";
			EventLogSubscription eventLogSubscription = new EventLogSubscription(name, TimeSpan.FromSeconds(600.0), new EventMatchingRule("Application", "ESE", new int[]
			{
				540,
				541
			}, 2, false, false, null, new EventMatchingRule.CustomNotification(EseDatabaseMonitoringContext.EseEventNotificationProcessor)), null, null, null);
			EventLogNotification.Instance.AddSubscription(eventLogSubscription);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(name2, EventLogNotification.ConstructResultMask(eventLogSubscription.Name, dbInfo.MailboxDatabaseName), HighAvailabilityConstants.ServiceName, ExchangeComponent.DataProtection, 1, true, 600);
			monitorDefinition.TargetResource = dbInfo.MailboxDatabaseName;
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = "Validate HA health is not impacted by ESE issues";
			base.AddChainedResponders(ref monitorDefinition, new MonitorStateResponderTuple[]
			{
				new MonitorStateResponderTuple
				{
					MonitorState = new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
					Responder = EscalateResponder.CreateDefinition(name3, HighAvailabilityConstants.ServiceName, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), monitorDefinition.TargetResource, ServiceHealthStatus.Unhealthy, "ESE", Strings.EseDbDivergenceDetectedSubject(HighAvailabilityConstants.ServiceName, Environment.MachineName, dbInfo.MailboxDatabaseName), Strings.EseDbDivergenceDetectedEscalationMessage(Environment.MachineName, dbInfo.MailboxDatabaseName), true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false)
				}
			});
		}

		// Token: 0x04000910 RID: 2320
		private const int DbTimeMismatchEventId = 516;

		// Token: 0x04000911 RID: 2321
		private const int DbtimeCheckActiveBehindEventId = 538;

		// Token: 0x04000912 RID: 2322
		private const int DbtimeCheckPassiveBehindEventId = 539;

		// Token: 0x04000913 RID: 2323
		private const string EseEventLogName = "Application";

		// Token: 0x04000914 RID: 2324
		private const string EseEventProviderName = "ESE";

		// Token: 0x020001A1 RID: 417
		public enum DbErrorEventType
		{
			// Token: 0x04000916 RID: 2326
			DbTimeAdvance = -567,
			// Token: 0x04000917 RID: 2327
			DbTimeSmaller
		}
	}
}
