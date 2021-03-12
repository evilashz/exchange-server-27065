using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Monitoring;
using Microsoft.Mapi;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200041D RID: 1053
	internal class MailboxAssistantsProcessingEvents : AssistantTroubleshooterBase
	{
		// Token: 0x0600249E RID: 9374 RVA: 0x00091F59 File Offset: 0x00090159
		public MailboxAssistantsProcessingEvents(PropertyBag fields) : base(fields)
		{
		}

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x0600249F RID: 9375 RVA: 0x00091F62 File Offset: 0x00090162
		private uint MaxWaitTime
		{
			get
			{
				return (uint)(this.fields["MaxProcessingTimeInMinutes"] ?? 15U) * 60U;
			}
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x00091F88 File Offset: 0x00090188
		public override MonitoringData InternalRunCheck()
		{
			MonitoringData monitoringData = new MonitoringData();
			if (base.ExchangeServer.AdminDisplayVersion.Major < MailboxAssistantsProcessingEvents.minExpectedServerVersion.Major || (base.ExchangeServer.AdminDisplayVersion.Major == MailboxAssistantsProcessingEvents.minExpectedServerVersion.Major && base.ExchangeServer.AdminDisplayVersion.Minor < MailboxAssistantsProcessingEvents.minExpectedServerVersion.Minor))
			{
				monitoringData.Events.Add(new MonitoringEvent(AssistantTroubleshooterBase.EventSource, 5101, EventTypeEnumeration.Warning, Strings.TSMinServerVersion(MailboxAssistantsProcessingEvents.minExpectedServerVersion.ToString())));
			}
			else
			{
				MonitoringPerformanceCounter mdbstatusUpdateCounterValue = this.GetMDBStatusUpdateCounterValue();
				if (mdbstatusUpdateCounterValue != null)
				{
					monitoringData.PerformanceCounters.Add(mdbstatusUpdateCounterValue);
					if (mdbstatusUpdateCounterValue.Value > this.MaxWaitTime)
					{
						monitoringData.Events.Add(new MonitoringEvent(AssistantTroubleshooterBase.EventSource, 5206, EventTypeEnumeration.Error, Strings.AIDatabaseStatusPollThreadHung(base.ExchangeServer.Name, mdbstatusUpdateCounterValue.Value)));
					}
				}
				else
				{
					monitoringData.Events.Add(this.TSMDBperformanceCounterNotLoaded(base.ExchangeServer.Name, "Elapsed Time since Last Database Status Update Attempt"));
				}
				List<MdbStatus> onlineMDBList = base.GetOnlineMDBList();
				int maximumEventQueueSize = this.GetMaximumEventQueueSize(base.ExchangeServer.Fqdn);
				foreach (MdbStatus mdbStatus in onlineMDBList)
				{
					string mdbName = mdbStatus.MdbName;
					MonitoringPerformanceCounter mdblastEventPollingAttemptCounterValue = this.GetMDBLastEventPollingAttemptCounterValue(mdbName);
					if (mdblastEventPollingAttemptCounterValue == null)
					{
						monitoringData.Events.Add(this.TSMDBperformanceCounterNotLoaded(base.ExchangeServer.Name, "Elapsed Time since Last Event Polling Attempt"));
					}
					else
					{
						monitoringData.PerformanceCounters.Add(mdblastEventPollingAttemptCounterValue);
						if (mdblastEventPollingAttemptCounterValue.Value > this.MaxWaitTime)
						{
							monitoringData.Events.Add(new MonitoringEvent(AssistantTroubleshooterBase.EventSource, 5205, EventTypeEnumeration.Error, Strings.AIMDBLastEventPollingThreadHung(base.ExchangeServer.Name, mdbName, mdblastEventPollingAttemptCounterValue.Value)));
						}
					}
					MonitoringPerformanceCounter mdblastEventPolledCounterValue = this.GetMDBLastEventPolledCounterValue(mdbName);
					if (mdblastEventPolledCounterValue == null)
					{
						monitoringData.Events.Add(this.TSMDBperformanceCounterNotLoaded(base.ExchangeServer.Name, "Elapsed Time Since Last Event Polled"));
					}
					MonitoringPerformanceCounter mdbeventsInQueueCounterValue = this.GetMDBEventsInQueueCounterValue(mdbName);
					if (mdbeventsInQueueCounterValue == null)
					{
						monitoringData.Events.Add(this.TSMDBperformanceCounterNotLoaded(base.ExchangeServer.Name, "Events in queue"));
					}
					if (mdblastEventPolledCounterValue != null && mdbeventsInQueueCounterValue != null)
					{
						monitoringData.PerformanceCounters.Add(mdblastEventPolledCounterValue);
						monitoringData.PerformanceCounters.Add(mdbeventsInQueueCounterValue);
						if (mdbeventsInQueueCounterValue.Value >= (double)maximumEventQueueSize && mdblastEventPolledCounterValue.Value > this.MaxWaitTime)
						{
							monitoringData.Events.Add(new MonitoringEvent(AssistantTroubleshooterBase.EventSource, 5205, EventTypeEnumeration.Error, Strings.AIMDBLastEventPollingThreadHung(base.ExchangeServer.Name, mdbName, mdblastEventPolledCounterValue.Value)));
						}
					}
				}
			}
			return monitoringData;
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x00092270 File Offset: 0x00090470
		private MonitoringPerformanceCounter GetMDBStatusUpdateCounterValue()
		{
			return this.GetMonitoringPerformanceCounter(base.ExchangeServer.Fqdn, "MsExchange Assistants - Per Database", "Elapsed Time since Last Database Status Update Attempt", "msexchangemailboxassistants-total");
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x00092292 File Offset: 0x00090492
		private MonitoringPerformanceCounter GetMDBLastEventPollingAttemptCounterValue(string mdbName)
		{
			return this.GetMonitoringPerformanceCounter(base.ExchangeServer.Fqdn, "MsExchange Assistants - Per Database", "Elapsed Time since Last Event Polling Attempt", "msexchangemailboxassistants-" + mdbName);
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x000922BA File Offset: 0x000904BA
		private MonitoringPerformanceCounter GetMDBLastEventPolledCounterValue(string mdbName)
		{
			return this.GetMonitoringPerformanceCounter(base.ExchangeServer.Fqdn, "MsExchange Assistants - Per Database", "Elapsed Time Since Last Event Polled", "msexchangemailboxassistants-" + mdbName);
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x000922E2 File Offset: 0x000904E2
		private MonitoringPerformanceCounter GetMDBEventsInQueueCounterValue(string mdbName)
		{
			return this.GetMonitoringPerformanceCounter(base.ExchangeServer.Fqdn, "MsExchange Assistants - Per Database", "Events in queue", "msexchangemailboxassistants-" + mdbName);
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x0009230C File Offset: 0x0009050C
		private int GetMaximumEventQueueSize(string machineName)
		{
			int result = 500;
			try
			{
				using (RegistryKey registryKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, machineName))
				{
					if (registryKey != null)
					{
						using (RegistryKey registryKey2 = registryKey.OpenSubKey("System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters"))
						{
							if (registryKey2 != null)
							{
								result = (int)registryKey2.GetValue("MaximumEventQueueSize", 500);
							}
						}
					}
				}
			}
			catch (IOException)
			{
			}
			catch (SecurityException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			return result;
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x000923BC File Offset: 0x000905BC
		private MonitoringPerformanceCounter GetMonitoringPerformanceCounter(string machineName, string categoryName, string counterName, string instanceName)
		{
			MonitoringPerformanceCounter result = null;
			try
			{
				using (PerformanceCounter localizedPerformanceCounter = base.GetLocalizedPerformanceCounter(categoryName, counterName, instanceName, machineName))
				{
					float num = localizedPerformanceCounter.NextValue();
					if (num == 0f)
					{
						Thread.Sleep(1000);
						num = localizedPerformanceCounter.NextValue();
					}
					localizedPerformanceCounter.Close();
					result = new MonitoringPerformanceCounter(categoryName, counterName, instanceName, (double)num);
				}
			}
			catch (Win32Exception)
			{
			}
			catch (InvalidOperationException)
			{
			}
			return result;
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x00092448 File Offset: 0x00090648
		private MonitoringEvent TSMDBperformanceCounterNotLoaded(string serverName, string counterName)
		{
			return new MonitoringEvent(AssistantTroubleshooterBase.EventSource, 5100, EventTypeEnumeration.Warning, Strings.TSMDBperformanceCounterNotLoaded(serverName, counterName));
		}

		// Token: 0x04001CFC RID: 7420
		public const string MaxProcessingTimeInMinutesPropertyName = "MaxProcessingTimeInMinutes";

		// Token: 0x04001CFD RID: 7421
		private const string MsExchangeAssistantPerDatabaseCategory = "MsExchange Assistants - Per Database";

		// Token: 0x04001CFE RID: 7422
		private const string MdbLastEventPollingAttemptCounter = "Elapsed Time since Last Event Polling Attempt";

		// Token: 0x04001CFF RID: 7423
		private const string MdbLastEventPolledCounter = "Elapsed Time Since Last Event Polled";

		// Token: 0x04001D00 RID: 7424
		private const string MdbEventsInQueueCounter = "Events in queue";

		// Token: 0x04001D01 RID: 7425
		private const string MdbStatusUpdateCounter = "Elapsed Time since Last Database Status Update Attempt";

		// Token: 0x04001D02 RID: 7426
		private const int DefaultMaximumEventQueueSize = 500;

		// Token: 0x04001D03 RID: 7427
		private const uint DefaultWaitTimeInMinutes = 15U;

		// Token: 0x04001D04 RID: 7428
		private const string AssistantParameterRegistryPath = "System\\CurrentControlSet\\Services\\MSExchangeMailboxAssistants\\Parameters";

		// Token: 0x04001D05 RID: 7429
		private const string MaximumEventQueueSizeRegistryName = "MaximumEventQueueSize";

		// Token: 0x04001D06 RID: 7430
		private static ServerVersion minExpectedServerVersion = new ServerVersion(14, 1, 0, 0);
	}
}
