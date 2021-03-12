using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PushNotifications
{
	// Token: 0x0200041E RID: 1054
	public class NotificationDeliveryProbe : ProbeWorkItem
	{
		// Token: 0x06001ADB RID: 6875 RVA: 0x00094044 File Offset: 0x00092244
		public override void PopulateDefinition<Definition>(Definition definition, Dictionary<string, string> propertyBag)
		{
			ProbeDefinition probeDefinition = definition as ProbeDefinition;
			if (probeDefinition == null)
			{
				throw new ArgumentException("definition must be a ProbeDefinition");
			}
			if (propertyBag.ContainsKey("TargetAppId"))
			{
				probeDefinition.Attributes["TargetAppId"] = propertyBag["TargetAppId"].ToString();
			}
			if (propertyBag.ContainsKey("IsAzureApp"))
			{
				probeDefinition.Attributes["IsAzureApp"] = propertyBag["IsAzureApp"].ToString();
			}
			if (propertyBag.ContainsKey("IsDeviceRegistrationChannelEnabled"))
			{
				probeDefinition.Attributes["IsDeviceRegistrationChannelEnabled"] = propertyBag["IsDeviceRegistrationChannelEnabled"].ToString();
			}
			if (propertyBag.ContainsKey("IsChallengeRequestChannelEnabled"))
			{
				probeDefinition.Attributes["IsChallengeRequestChannelEnabled"] = propertyBag["IsChallengeRequestChannelEnabled"].ToString();
			}
			if (propertyBag.ContainsKey("SkipInstanceTag"))
			{
				probeDefinition.Attributes["SkipInstanceTag"] = propertyBag["SkipInstanceTag"].ToString();
			}
		}

		// Token: 0x06001ADC RID: 6876 RVA: 0x0009414C File Offset: 0x0009234C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (base.Definition.Attributes.ContainsKey("TargetAppId"))
			{
				this.targetAppId = base.Definition.Attributes["TargetAppId"].ToString();
			}
			if (base.Definition.Attributes.ContainsKey("IsAzureApp"))
			{
				this.isAzureApp = bool.Parse(base.Definition.Attributes["IsAzureApp"]);
			}
			if (base.Definition.Attributes.ContainsKey("IsDeviceRegistrationChannelEnabled"))
			{
				this.isDeviceRegistrationChannelEnabled = bool.Parse(base.Definition.Attributes["IsDeviceRegistrationChannelEnabled"]);
			}
			if (base.Definition.Attributes.ContainsKey("IsChallengeRequestChannelEnabled"))
			{
				this.isChallengeRequestChannelEnabled = bool.Parse(base.Definition.Attributes["IsChallengeRequestChannelEnabled"]);
			}
			if (base.Definition.Attributes.ContainsKey("SkipInstanceTag"))
			{
				bool.TryParse(base.Definition.Attributes["SkipInstanceTag"], out this.skipInstance);
			}
			WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.PushNotificationTracer, base.TraceContext, "NotificationDeliveryProbe.DoWork: Checking presence of notification processed events for - {0}", this.targetAppId, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PushNotifications\\Probes\\NotificationDeliveryProbe.cs", 175);
			this.notificationDeliveryInterval = base.Definition.RecurrenceIntervalSeconds;
			DateTime executionStartTime = base.Result.ExecutionStartTime;
			DateTime startTime = executionStartTime - TimeSpan.FromSeconds((double)this.notificationDeliveryInterval);
			DateTime endTime = executionStartTime;
			if (this.skipInstance && base.Definition.CreatedTime.AddSeconds((double)this.notificationDeliveryInterval) > executionStartTime)
			{
				ProbeResult result = base.Result;
				result.StateAttribute12 += string.Format("Skipping this probe instance: No notification is expected yet as probe instance was created less than {0} minutes ago. \n", this.notificationDeliveryInterval / 60);
				return;
			}
			if (string.IsNullOrEmpty(this.targetAppId))
			{
				this.targetPublisherProbeName = "PushNotificationsDatacenterOnPremBackendEndpointProbe";
				this.notificationProcessedEvent = NotificationItem.GenerateResultName(ExchangeComponent.PushNotificationsProtocol.Name, "EnterpriseNotificationProcessed", null);
			}
			else
			{
				this.targetPublisherProbeName = "PushNotificationsPublisherProbe";
				this.notificationProcessedEvent = NotificationItem.GenerateResultName(ExchangeComponent.PushNotificationsProtocol.Name, "NotificationProcessed", this.targetAppId);
			}
			IDataAccessQuery<ProbeResult> probeResults = base.Broker.GetProbeResults(this.targetPublisherProbeName, startTime, endTime);
			if (probeResults.Count<ProbeResult>() < 1)
			{
				ProbeResult result2 = base.Result;
				result2.StateAttribute12 += "Publisher Probe not generating results. \n";
				throw new InvalidOperationException(string.Format("No Publisher Probe result found for last {0} minutes", this.notificationDeliveryInterval / 60));
			}
			ProbeResult probeResult = probeResults.First<ProbeResult>();
			if (probeResult.ResultType != ResultType.Succeeded)
			{
				ProbeResult result3 = base.Result;
				result3.StateAttribute12 += string.Format("Publisher Probe {0} was '{1}' at {2} UTC. \n", probeResult.ResultName, probeResult.ResultType, probeResult.ExecutionEndTime);
				ProbeResult result4 = base.Result;
				result4.StateAttribute13 += string.Format("Publish Error: {0}. ", probeResult.Error);
				throw new InvalidOperationException("The latest attempt to publish notifications failed");
			}
			ProbeResult result5 = base.Result;
			result5.StateAttribute12 += string.Format("Latest Notification {0} was published at {1} UTC. \n", probeResult.ResultName, probeResult.ExecutionEndTime);
			ProbeResult result6 = base.Result;
			result6.StateAttribute13 += string.Format("Publish Result: {0}. ", probeResult.StateAttribute2);
			IDataAccessQuery<ProbeResult> probeResults2 = base.Broker.GetProbeResults(this.notificationProcessedEvent, startTime, endTime);
			if (probeResults2.Count<ProbeResult>() > 0)
			{
				ProbeResult probeResult2 = probeResults2.First<ProbeResult>();
				ProbeResult result7 = base.Result;
				result7.StateAttribute12 += string.Format("Latest Notification {0} was processed successfully at {1} UTC. \n", probeResult2.ResultName, probeResult2.ExecutionEndTime);
				string sampleMask = NotificationItem.GenerateResultName(ExchangeComponent.PushNotificationsProtocol.Name, "SendPublishNotification", null);
				IDataAccessQuery<ProbeResult> probeResults3 = base.Broker.GetProbeResults(sampleMask, startTime, endTime);
				if (probeResults3.Count<ProbeResult>() > 0)
				{
					ProbeResult probeResult3 = probeResults3.First<ProbeResult>();
					if (probeResult3.ResultType != ResultType.Succeeded)
					{
						ProbeResult result8 = base.Result;
						result8.StateAttribute12 += string.Format("Assistant's SendPublishNotification event {0} was '{1}' at {2} UTC. \n", probeResult3.ResultName, probeResult3.ResultType, probeResult3.ExecutionEndTime);
						ProbeResult result9 = base.Result;
						result9.StateAttribute13 += string.Format("Assistant Error: {0}. ", probeResult3.StateAttribute2);
						throw new InvalidOperationException("Latest attempt by assistant to publish event failed");
					}
				}
				if (this.isAzureApp)
				{
					if (this.skipInstance && base.Definition.CreatedTime.AddSeconds((double)(this.notificationDeliveryInterval * 2)) > executionStartTime)
					{
						ProbeResult result10 = base.Result;
						result10.StateAttribute12 += string.Format("Skipping this probe instance: No Hub Creation notification is expected yet as probe instance was created less than {0} minutes ago. \n", this.notificationDeliveryInterval * 2 / 60);
						return;
					}
					string sampleMask2 = NotificationItem.GenerateResultName(ExchangeComponent.PushNotificationsProtocol.Name, "HubCreationProcessed", this.targetAppId);
					IDataAccessQuery<ProbeResult> probeResults4 = base.Broker.GetProbeResults(sampleMask2, startTime, endTime);
					if (probeResults4.Count<ProbeResult>() <= 0)
					{
						this.AppendOperationalChannelEventsToOutput("Azure hub creation processing", startTime, endTime);
						throw new InvalidOperationException(string.Format("Azure hub creation processing failed for {0} in last {1} minutes", this.targetAppId, this.notificationDeliveryInterval / 60));
					}
					ProbeResult probeResult4 = probeResults4.First<ProbeResult>();
					ProbeResult result11 = base.Result;
					result11.StateAttribute12 += string.Format("Last successful attempt at creating Azure Notification Hub {0} was at {1} UTC. \n", probeResult4.ResultName, probeResult4.ExecutionEndTime);
					if (this.isDeviceRegistrationChannelEnabled)
					{
						string sampleMask3 = NotificationItem.GenerateResultName(ExchangeComponent.PushNotificationsProtocol.Name, "DeviceRegistrationProcessed", this.targetAppId);
						IDataAccessQuery<ProbeResult> probeResults5 = base.Broker.GetProbeResults(sampleMask3, startTime, endTime);
						if (probeResults5.Count<ProbeResult>() <= 0)
						{
							this.AppendOperationalChannelEventsToOutput("Azure device registration processing", startTime, endTime);
							throw new InvalidOperationException(string.Format("Azure device registration processing failed for {0} in last {1} minutes", this.targetAppId, this.notificationDeliveryInterval / 60));
						}
						ProbeResult probeResult5 = probeResults5.First<ProbeResult>();
						ProbeResult result12 = base.Result;
						result12.StateAttribute12 += string.Format("Last successful attempt at device registration {0} was at {1} UTC. \n", probeResult5.ResultName, probeResult5.ExecutionEndTime);
					}
					if (this.isChallengeRequestChannelEnabled)
					{
						string sampleMask4 = NotificationItem.GenerateResultName(ExchangeComponent.PushNotificationsProtocol.Name, "ChallengeRequestProcessed", this.targetAppId);
						IDataAccessQuery<ProbeResult> probeResults6 = base.Broker.GetProbeResults(sampleMask4, startTime, endTime);
						if (probeResults6.Count<ProbeResult>() > 0)
						{
							ProbeResult probeResult6 = probeResults6.First<ProbeResult>();
							ProbeResult result13 = base.Result;
							result13.StateAttribute12 += string.Format("Last successful attempt at challenge request {0} was at {1} UTC. \n", probeResult6.ResultName, probeResult6.ExecutionEndTime);
							return;
						}
						this.AppendOperationalChannelEventsToOutput("Azure challenge request processing", startTime, endTime);
						throw new InvalidOperationException(string.Format("Azure challenge request processing failed for {0} in last {1} minutes", this.targetAppId, this.notificationDeliveryInterval / 60));
					}
				}
				return;
			}
			this.AppendOperationalChannelEventsToOutput("Notification processing", startTime, endTime);
			throw new InvalidOperationException(string.Format("Notification processing failed for {0} in last {1} minutes", this.targetAppId, this.notificationDeliveryInterval / 60));
		}

		// Token: 0x06001ADD RID: 6877 RVA: 0x00094870 File Offset: 0x00092A70
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return new List<PropertyInformation>();
		}

		// Token: 0x06001ADE RID: 6878 RVA: 0x00094884 File Offset: 0x00092A84
		private void AppendOperationalChannelEventsToOutput(string errorStringPrefix, DateTime startTime, DateTime endTime)
		{
			List<EventRecord> operationalChannelEvents = PushNotificationsProbeUtil.GetOperationalChannelEvents(startTime, endTime, 3, null);
			if (operationalChannelEvents.Count == 0)
			{
				ProbeResult result = base.Result;
				result.StateAttribute12 += string.Format("{0} failed but no errors found in 'Operational Channel'. \n", errorStringPrefix);
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0} failed. Last 3 Error records found in last {1} minutes<br><br>", errorStringPrefix, this.notificationDeliveryInterval / 60);
			foreach (EventRecord eventRecord in operationalChannelEvents)
			{
				stringBuilder.AppendFormat("<li>{0}&nbsp|&nbsp{1}&nbsp|&nbsp{2}", eventRecord.TimeCreated, eventRecord.TaskDisplayName, eventRecord.FormatDescription());
			}
			ProbeResult result2 = base.Result;
			result2.StateAttribute12 += stringBuilder.ToString();
		}

		// Token: 0x0400125E RID: 4702
		public const string TargetAppIdProperty = "TargetAppId";

		// Token: 0x0400125F RID: 4703
		public const string IsAzureAppProperty = "IsAzureApp";

		// Token: 0x04001260 RID: 4704
		public const string IsDeviceRegistrationChannelEnabledProperty = "IsDeviceRegistrationChannelEnabled";

		// Token: 0x04001261 RID: 4705
		public const string IsChallengeRequestChannelEnabledProperty = "IsChallengeRequestChannelEnabled";

		// Token: 0x04001262 RID: 4706
		protected const int NumberOfErrorsToRead = 3;

		// Token: 0x04001263 RID: 4707
		protected string targetAppId;

		// Token: 0x04001264 RID: 4708
		protected string targetPublisherProbeName;

		// Token: 0x04001265 RID: 4709
		protected string notificationProcessedEvent;

		// Token: 0x04001266 RID: 4710
		protected bool isAzureApp;

		// Token: 0x04001267 RID: 4711
		protected bool isDeviceRegistrationChannelEnabled;

		// Token: 0x04001268 RID: 4712
		protected bool isChallengeRequestChannelEnabled;

		// Token: 0x04001269 RID: 4713
		protected bool skipInstance;

		// Token: 0x0400126A RID: 4714
		protected int notificationDeliveryInterval;
	}
}
