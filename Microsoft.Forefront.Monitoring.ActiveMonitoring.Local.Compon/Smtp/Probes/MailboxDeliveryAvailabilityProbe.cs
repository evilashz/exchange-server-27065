using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Transport;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x02000227 RID: 551
	public class MailboxDeliveryAvailabilityProbe : ProbeWorkItem
	{
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x000349ED File Offset: 0x00032BED
		// (set) Token: 0x06001218 RID: 4632 RVA: 0x000349F5 File Offset: 0x00032BF5
		internal List<ProbeResult> AllInstanceProbeResults
		{
			get
			{
				return this.allActiveInstanceProbeResults;
			}
			set
			{
				this.allActiveInstanceProbeResults = value;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x000349FE File Offset: 0x00032BFE
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x00034A06 File Offset: 0x00032C06
		internal Dictionary<string, List<ProbeResult>> DatabaseInstanceProbeResults
		{
			get
			{
				return this.activeDatabaseInstanceProbeResults;
			}
			set
			{
				this.activeDatabaseInstanceProbeResults = value;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x00034A0F File Offset: 0x00032C0F
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x00034A17 File Offset: 0x00032C17
		internal int MailboxesAtDiscovery { get; set; }

		// Token: 0x0600121D RID: 4637 RVA: 0x00034A20 File Offset: 0x00032C20
		public static void SetCustomErrorResponderMessage(ProbeResult result, string message)
		{
			result.StateAttribute1 = message;
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00034A29 File Offset: 0x00032C29
		public static string GetCustomErrorResponderMessage(ProbeResult result)
		{
			return result.StateAttribute1;
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00034A34 File Offset: 0x00032C34
		public static ProbeDefinition CreateMailboxDeliveryAvailabilityProbe(int mailboxCount)
		{
			return new ProbeDefinition
			{
				AssemblyPath = typeof(MailboxDeliveryAvailabilityProbe).Assembly.Location,
				TypeName = typeof(MailboxDeliveryAvailabilityProbe).FullName,
				Name = "MailboxDeliveryAvailabilityAggregationProbe",
				ServiceName = ExchangeComponent.MailboxTransport.Name,
				RecurrenceIntervalSeconds = 120,
				TimeoutSeconds = 90,
				MaxRetryAttempts = 3,
				TargetGroup = mailboxCount.ToString()
			};
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00034AB8 File Offset: 0x00032CB8
		public static MonitorDefinition CreateMailboxDeliveryAvailabilityMonitor(ProbeDefinition probe)
		{
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition("MailboxDeliveryAvailabilityAggregationMonitor", probe.ConstructWorkItemResultName(), ExchangeComponent.MailboxTransport.Name, ExchangeComponent.MailboxTransport, 1, true, 120);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, TimeSpan.FromSeconds(0.0)),
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, TimeSpan.FromSeconds(600.0))
			};
			return monitorDefinition;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00034B28 File Offset: 0x00032D28
		public static ResponderDefinition CreateMailboxDeliveryAvailabilityRestartResponder(ProbeDefinition probe, MonitorDefinition monitor)
		{
			string responderName = "MailboxDeliveryAvailabilityAggregationRestartResponder";
			string monitorName = monitor.ConstructWorkItemResultName();
			string windowsServiceName = "MSExchangeDelivery";
			ServiceHealthStatus responderTargetState = ServiceHealthStatus.Degraded;
			int serviceStopTimeoutInSeconds = 300;
			int serviceStartTimeoutInSeconds = 300;
			int serviceStartDelayInSeconds = 2;
			bool enabled = true;
			return RestartServiceResponder.CreateDefinition(responderName, monitorName, windowsServiceName, responderTargetState, serviceStopTimeoutInSeconds, serviceStartTimeoutInSeconds, serviceStartDelayInSeconds, false, DumpMode.FullDump, null, 15.0, 0, ExchangeComponent.MailboxTransport.Name, null, true, enabled, null, false);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00034B74 File Offset: 0x00032D74
		public static ResponderDefinition CreateMailboxDeliveryAvailabilityEscalateResponder(ProbeDefinition probe, MonitorDefinition monitor)
		{
			return EscalateResponder.CreateDefinition("MailboxDeliveryAvailabilityAggregationEscalateResponder", ExchangeComponent.MailboxTransport.Name, monitor.Name, monitor.ConstructWorkItemResultName(), Environment.MachineName, ServiceHealthStatus.Unhealthy, ExchangeComponent.MailboxTransport.EscalationTeam, "MSExchangeDelivery service is not working.", "The MSExchangeDelivery service is failing due to this exception: \r\n{Probe.Exception}\r\n\r\nAdditional details:\r\n{Probe.StateAttribute1}", true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00034BC8 File Offset: 0x00032DC8
		internal static int GetMailboxesAtDiscovery(ProbeDefinition definition)
		{
			int result;
			if (int.TryParse(definition.TargetGroup, out result))
			{
				return result;
			}
			return -1;
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00034BE7 File Offset: 0x00032DE7
		internal static bool ExactExceptionFinder(ProbeResult result, string exception)
		{
			return result.ResultType == ResultType.Failed && result.Exception.IndexOf(exception, StringComparison.OrdinalIgnoreCase) > -1;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00034C04 File Offset: 0x00032E04
		internal static bool AnyExceptionFinder(ProbeResult result, string exception)
		{
			return result.ResultType == ResultType.Failed && !string.IsNullOrEmpty(result.Exception);
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00034C20 File Offset: 0x00032E20
		internal static bool RegexExceptionFinder(ProbeResult result, string exception)
		{
			Regex regex = new Regex(exception, RegexOptions.IgnoreCase);
			return result.ResultType == ResultType.Failed && regex.IsMatch(result.Exception);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00034C4C File Offset: 0x00032E4C
		internal static void DetectHangOrStoppedProcess(ProbeWorkItem workitem)
		{
			using (ServiceController serviceController = new ServiceController("MSExchangeDelivery"))
			{
				if (serviceController.Status == ServiceControllerStatus.Running)
				{
					MailboxDeliveryAvailabilityProbe.SetCustomErrorResponderMessage(workitem.Result, "The Delivery service appears to be hung. The service is listed as running in the service controller. As part of the restart responder a dump was taken.");
				}
				else if (serviceController.Status == ServiceControllerStatus.Stopped)
				{
					MailboxDeliveryAvailabilityProbe.SetCustomErrorResponderMessage(workitem.Result, "The Delivery service is stopped. The restart responder has failed or the service stopped since the restart responder performed its recovery.");
				}
				else
				{
					MailboxDeliveryAvailabilityProbe.SetCustomErrorResponderMessage(workitem.Result, string.Format("The Delivery service is hung during {0}. As part of the restart responder a dump was taken.", serviceController.Status.ToString()));
				}
			}
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x00034CDC File Offset: 0x00032EDC
		internal void CheckKnownExceptions()
		{
			foreach (MailboxDeliveryAvailabilityProbe.KnownException ex in MailboxDeliveryAvailabilityProbe.KnownExceptionList)
			{
				string text;
				if (this.FindCommonException(ex.Exception, ex.MonitoringThreshold, ex.FailurePercent, new MailboxDeliveryAvailabilityProbe.ExceptionFinder(MailboxDeliveryAvailabilityProbe.ExactExceptionFinder), out text))
				{
					if (ex.Followups != null && ex.Followups.Count > 0)
					{
						foreach (MailboxDeliveryAvailabilityProbe.Followup followup in ex.Followups)
						{
							followup(this);
						}
					}
					throw new MailboxDeliveryAvailabilityProbe.MailDeliveryAvailabilityException(string.Format("'{2}' caused {0}% failure over {1} samples.", ex.FailurePercent, ex.MonitoringThreshold, ex.Exception));
				}
			}
			foreach (MailboxDeliveryAvailabilityProbe.KnownException ex2 in this.ParseExtensionAttributes())
			{
				string text2;
				if (this.FindCommonException(ex2.Exception, ex2.MonitoringThreshold, ex2.FailurePercent, new MailboxDeliveryAvailabilityProbe.ExceptionFinder(MailboxDeliveryAvailabilityProbe.ExactExceptionFinder), out text2))
				{
					throw new MailboxDeliveryAvailabilityProbe.MailDeliveryAvailabilityException(string.Format("'{2}' caused {0}% failure over {1} samples.", ex2.FailurePercent, ex2.MonitoringThreshold, ex2.Exception));
				}
			}
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00034EB0 File Offset: 0x000330B0
		internal void CheckAllInstancesForSameFailures()
		{
			List<ProbeResult> source;
			if (!this.FindAnyException(5, 100, out source))
			{
				return;
			}
			List<MailboxDeliveryAvailabilityProbe.ExceptionHitBucket> list = new List<MailboxDeliveryAvailabilityProbe.ExceptionHitBucket>((from g in source
			group g by g.Exception into s
			select new MailboxDeliveryAvailabilityProbe.ExceptionHitBucket
			{
				Exception = s.Key,
				Count = s.Count<ProbeResult>()
			}).ToArray<MailboxDeliveryAvailabilityProbe.ExceptionHitBucket>());
			if (list.Count != 1)
			{
				return;
			}
			MailboxDeliveryAvailabilityProbe.SetCustomErrorResponderMessage(base.Result, string.Format("{0} - {1}", list[0].Count, list[0].Exception));
			throw new MailboxDeliveryAvailabilityProbe.MailDeliveryAvailabilityProbeException("Same Unknown Exception");
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00034FE0 File Offset: 0x000331E0
		internal void CheckAllInstancesForDifferentFailures()
		{
			List<ProbeResult> source;
			if (this.FindAnyException(10, 100, out source))
			{
				List<MailboxDeliveryAvailabilityProbe.ExceptionHitBucket> list = new List<MailboxDeliveryAvailabilityProbe.ExceptionHitBucket>((from g in source
				group g by string.Format("{1}: {0}", g.Exception, g.StateAttribute21) into s
				select new MailboxDeliveryAvailabilityProbe.ExceptionHitBucket
				{
					Exception = s.Key,
					Count = s.Count<ProbeResult>()
				}).ToArray<MailboxDeliveryAvailabilityProbe.ExceptionHitBucket>());
				list.Sort((MailboxDeliveryAvailabilityProbe.ExceptionHitBucket x, MailboxDeliveryAvailabilityProbe.ExceptionHitBucket y) => x.Count.CompareTo(y.Count));
				MailboxDeliveryAvailabilityProbe.SetCustomErrorResponderMessage(base.Result, string.Join(",", from x in list
				select string.Format("{0} - {1}, ", x.Count, x.Exception)));
				throw new MailboxDeliveryAvailabilityProbe.MailDeliveryAvailabilityProbeException("Multiple different exceptions");
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00035370 File Offset: 0x00033570
		internal IEnumerable<MailboxDeliveryAvailabilityProbe.KnownException> ParseExtensionAttributes()
		{
			string knownExceptionsValue;
			if (base.Definition.Attributes.TryGetValue("KnownExceptionTypes", out knownExceptionsValue) && !string.IsNullOrWhiteSpace(knownExceptionsValue))
			{
				string[] array = knownExceptionsValue.Split(new char[]
				{
					';'
				});
				int i = 0;
				while (i < array.Length)
				{
					string token = array[i];
					MailboxDeliveryAvailabilityProbe.KnownException exception;
					try
					{
						string[] array2 = token.Split(new char[]
						{
							'|'
						});
						if (string.IsNullOrWhiteSpace(array2[0]) || string.IsNullOrWhiteSpace(array2[1]) || string.IsNullOrWhiteSpace(array2[2]))
						{
							ProbeResult result = base.Result;
							result.FailureContext += string.Format("Found known exception with missing values: {0}. ", token);
							goto IL_189;
						}
						exception = new MailboxDeliveryAvailabilityProbe.KnownException
						{
							Exception = array2[0],
							FailurePercent = int.Parse(array2[1]),
							MonitoringThreshold = int.Parse(array2[2])
						};
					}
					catch
					{
						ProbeResult result2 = base.Result;
						result2.FailureContext += string.Format("Failed to parse this token: {0}. ", token);
						goto IL_189;
					}
					goto IL_16B;
					IL_189:
					i++;
					continue;
					IL_16B:
					yield return exception;
					goto IL_189;
				}
			}
			yield break;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x000353B0 File Offset: 0x000335B0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			this.MailboxesAtDiscovery = MailboxDeliveryAvailabilityProbe.GetMailboxesAtDiscovery(base.Definition);
			ProbeDefinition[] array = (from w in LocalDataAccess.GetAllDefinitions<ProbeDefinition>()
			where w.Name.Equals("MailboxDeliveryInstanceAvailabilityProbe", StringComparison.OrdinalIgnoreCase)
			select w).ToArray<ProbeDefinition>();
			if (this.MailboxesAtDiscovery > 0 && array.Length != this.MailboxesAtDiscovery)
			{
				ProbeResult result = base.Result;
				result.ExecutionContext += string.Format("Found {0} instance probe definitions. Expected count is {1}. ", array.Length, this.MailboxesAtDiscovery);
			}
			LocalDataAccess localDataAccess = new LocalDataAccess();
			DateTime minExecutionEndTime = DateTime.UtcNow.ToLocalTime().Subtract(TimeSpan.FromMinutes(60.0));
			this.allActiveInstanceProbeResults = new List<ProbeResult>();
			this.activeDatabaseInstanceProbeResults = new Dictionary<string, List<ProbeResult>>();
			List<ProbeResult> list = new List<ProbeResult>();
			foreach (ProbeDefinition probeDefinition in array)
			{
				List<ProbeResult> list2 = new List<ProbeResult>(from o in localDataAccess.GetTable<ProbeResult, string>(WorkItemResultIndex<ProbeResult>.ResultNameAndExecutionEndTime(probeDefinition.ConstructWorkItemResultName(), minExecutionEndTime))
				orderby o.ExecutionEndTime descending
				select o);
				if (list2.Any<ProbeResult>())
				{
					list.AddRange(list2);
					if (MailboxDeliveryInstanceAvailabilityProbe.GetActiveMDBStatus(list2.First<ProbeResult>()))
					{
						List<ProbeResult> list3 = new List<ProbeResult>(from w in list2
						where MailboxDeliveryInstanceAvailabilityProbe.GetActiveMDBStatus(w)
						select w);
						if (list3.Any<ProbeResult>())
						{
							this.allActiveInstanceProbeResults.AddRange(list3);
							this.activeDatabaseInstanceProbeResults.Add(probeDefinition.TargetResource, list3);
						}
					}
				}
			}
			if (!this.allActiveInstanceProbeResults.Any<ProbeResult>() && list.Any<ProbeResult>())
			{
				ProbeResult result2 = base.Result;
				result2.ExecutionContext += "No active instance probe results found, only passive databases. Proceeding as success. ";
				return;
			}
			if (!list.Any<ProbeResult>() && this.MailboxesAtDiscovery > 0)
			{
				ProbeResult result3 = base.Result;
				result3.ExecutionContext += "Discovery indicates mailboxes, but no probe results for any mailboxes. Proceeding as success. ";
				return;
			}
			this.CheckKnownExceptions();
			this.CheckAllInstancesForSameFailures();
			this.CheckAllInstancesForDifferentFailures();
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x000355CD File Offset: 0x000337CD
		private bool FindAnyException(int monitoringThreshold, int failurePercentage, out List<ProbeResult> allResults)
		{
			return this.FindException(monitoringThreshold, failurePercentage, string.Empty, new MailboxDeliveryAvailabilityProbe.ExceptionFinder(MailboxDeliveryAvailabilityProbe.AnyExceptionFinder), out allResults);
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x000355EC File Offset: 0x000337EC
		private bool FindCommonException(string exception, int monitoringThreshold, int failurePercentage, MailboxDeliveryAvailabilityProbe.ExceptionFinder searchQuery, out string exceptionInfo)
		{
			List<ProbeResult> list;
			bool flag = this.FindException(monitoringThreshold, failurePercentage, exception, searchQuery, out list);
			if (flag && list.Count > 0)
			{
				exceptionInfo = list.First<ProbeResult>().Exception;
			}
			else
			{
				exceptionInfo = string.Empty;
			}
			return flag;
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00035638 File Offset: 0x00033838
		private bool FindException(int monitoringThreshold, int failurePercentage, string exception, MailboxDeliveryAvailabilityProbe.ExceptionFinder searchQuery, out List<ProbeResult> allResults)
		{
			Dictionary<string, bool> dictionary = this.BuildTrackingDictionary();
			double num = (double)(failurePercentage / 100);
			int num2 = (int)num * monitoringThreshold;
			string empty = string.Empty;
			List<ProbeResult> list = new List<ProbeResult>();
			foreach (KeyValuePair<string, List<ProbeResult>> keyValuePair in this.activeDatabaseInstanceProbeResults)
			{
				List<ProbeResult> list2 = this.SearchForMatches(keyValuePair.Value, exception, monitoringThreshold, searchQuery);
				if (list2.Count >= num2)
				{
					dictionary[keyValuePair.Key] = true;
					list.AddRange(list2);
				}
			}
			bool flag = (from w in dictionary
			where !w.Value
			select w).Count<KeyValuePair<string, bool>>() == 0;
			allResults = new List<ProbeResult>();
			if (flag)
			{
				allResults = list;
			}
			return flag;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00035734 File Offset: 0x00033934
		private List<ProbeResult> SearchForMatches(List<ProbeResult> instanceProbeResults, string exception, int monitoringThreshold, MailboxDeliveryAvailabilityProbe.ExceptionFinder findException)
		{
			List<ProbeResult> range = instanceProbeResults.GetRange(0, (instanceProbeResults.Count > monitoringThreshold) ? monitoringThreshold : instanceProbeResults.Count);
			return new List<ProbeResult>((from w in range
			where findException(w, exception)
			select w).ToArray<ProbeResult>());
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0003578C File Offset: 0x0003398C
		private Dictionary<string, bool> BuildTrackingDictionary()
		{
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
			foreach (KeyValuePair<string, List<ProbeResult>> keyValuePair in this.activeDatabaseInstanceProbeResults)
			{
				dictionary.Add(keyValuePair.Key, false);
			}
			return dictionary;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x000357F8 File Offset: 0x000339F8
		// Note: this type is marked as 'beforefieldinit'.
		static MailboxDeliveryAvailabilityProbe()
		{
			List<MailboxDeliveryAvailabilityProbe.KnownException> list = new List<MailboxDeliveryAvailabilityProbe.KnownException>();
			List<MailboxDeliveryAvailabilityProbe.KnownException> list2 = list;
			MailboxDeliveryAvailabilityProbe.KnownException item2 = default(MailboxDeliveryAvailabilityProbe.KnownException);
			item2.Exception = "System.Net.Sockets.SocketException";
			item2.MonitoringThreshold = 2;
			item2.FailurePercent = 100;
			List<MailboxDeliveryAvailabilityProbe.Followup> list3 = new List<MailboxDeliveryAvailabilityProbe.Followup>();
			list3.Add(delegate(ProbeWorkItem item)
			{
				MailboxDeliveryAvailabilityProbe.DetectHangOrStoppedProcess(item);
			});
			item2.Followups = list3;
			list2.Add(item2);
			MailboxDeliveryAvailabilityProbe.KnownExceptionList = list;
		}

		// Token: 0x04000845 RID: 2117
		internal const string MailboxDeliveryAvailabilityProbeName = "MailboxDeliveryAvailabilityAggregationProbe";

		// Token: 0x04000846 RID: 2118
		internal const string MailboxDeliveryAvailabilityMonitorName = "MailboxDeliveryAvailabilityAggregationMonitor";

		// Token: 0x04000847 RID: 2119
		internal const string MailboxDeliveryAvailabilityRestartResponderName = "MailboxDeliveryAvailabilityAggregationRestartResponder";

		// Token: 0x04000848 RID: 2120
		internal const string MailboxDeliveryAvailabilityEscalateResponderName = "MailboxDeliveryAvailabilityAggregationEscalateResponder";

		// Token: 0x04000849 RID: 2121
		internal const string RestartResponderServiceToRestart = "MSExchangeDelivery";

		// Token: 0x0400084A RID: 2122
		internal const string EscalateResponderEmailSubject = "MSExchangeDelivery service is not working.";

		// Token: 0x0400084B RID: 2123
		internal const string EscalateResponderEmailMessage = "The MSExchangeDelivery service is failing due to this exception: \r\n{Probe.Exception}\r\n\r\nAdditional details:\r\n{Probe.StateAttribute1}";

		// Token: 0x0400084C RID: 2124
		internal const int RandomExceptionMonitoringThreshold = 10;

		// Token: 0x0400084D RID: 2125
		internal const int RandomExceptionFailurePercentThreshold = 100;

		// Token: 0x0400084E RID: 2126
		internal const int UnknownSameExceptionMonitoringThreshold = 5;

		// Token: 0x0400084F RID: 2127
		internal const int UnknownSameExceptionFailurePercentThreshold = 100;

		// Token: 0x04000850 RID: 2128
		internal const string ExtensionAttributesKnownExceptionTypes = "KnownExceptionTypes";

		// Token: 0x04000851 RID: 2129
		internal const int ProbeRecurrenceIntervalSeconds = 120;

		// Token: 0x04000852 RID: 2130
		internal const int ProbeTimeoutSeconds = 90;

		// Token: 0x04000853 RID: 2131
		internal const int ProbeMaxRetryAttempts = 3;

		// Token: 0x04000854 RID: 2132
		internal const int MonitorFailureCount = 1;

		// Token: 0x04000855 RID: 2133
		internal const int MonitorMonitoringInterval = 120;

		// Token: 0x04000856 RID: 2134
		internal const bool MonitorEnabled = true;

		// Token: 0x04000857 RID: 2135
		internal const int MonitorTransitionToRestartInSeconds = 0;

		// Token: 0x04000858 RID: 2136
		internal const int MonitorTransitionToEscalateInSeconds = 600;

		// Token: 0x04000859 RID: 2137
		internal const int RestartResponderServiceStopTimeoutSeconds = 300;

		// Token: 0x0400085A RID: 2138
		internal const int RestartResponderServiceStartTimeoutSeconds = 300;

		// Token: 0x0400085B RID: 2139
		internal const int RestartResponderServiceStartDelayInSeconds = 2;

		// Token: 0x0400085C RID: 2140
		internal const bool RestartResponderEnabled = true;

		// Token: 0x0400085D RID: 2141
		internal const bool EscalateResponderEnabled = true;

		// Token: 0x0400085E RID: 2142
		internal const NotificationServiceClass EscalateLevel = NotificationServiceClass.Urgent;

		// Token: 0x0400085F RID: 2143
		internal static readonly List<string> TransientSmtpResponseList = new List<string>
		{
			AckReason.MessageDelayedDeleteByAdmin.ToString(),
			AckReason.MessageDeletedByAdmin.ToString(),
			AckReason.MessageDeletedByTransportAgent.ToString(),
			AckReason.PoisonMessageDeletedByAdmin.ToString(),
			AckReason.MessageDelayedDeleteByAdmin.ToString(),
			AckReason.MessageDeletedByAdmin.ToString(),
			AckReason.MessageDeletedByTransportAgent.ToString(),
			AckReason.PoisonMessageDeletedByAdmin.ToString(),
			AckReason.MessageDelayedDeleteByAdmin.ToString(),
			AckReason.MessageDeletedByAdmin.ToString(),
			AckReason.MessageDeletedByTransportAgent.ToString(),
			AckReason.PoisonMessageDeletedByAdmin.ToString(),
			AckReason.MailboxServerOffline.ToString(),
			AckReason.MDBOffline.ToString(),
			AckReason.MapiNoAccessFailure.ToString(),
			AckReason.MailboxServerTooBusy.ToString(),
			AckReason.MailboxMapiSessionLimit.ToString(),
			AckReason.MailboxServerMaxThreadsPerMdbExceeded.ToString(),
			AckReason.MapiExceptionMaxThreadsPerSCTExceeded.ToString(),
			AckReason.MailboxDatabaseThreadLimitExceeded.ToString(),
			AckReason.RecipientThreadLimitExceeded.ToString(),
			AckReason.DeliverySourceThreadLimitExceeded.ToString(),
			AckReason.DynamicMailboxDatabaseThrottlingLimitExceeded.ToString(),
			AckReason.MailboxIOError.ToString(),
			AckReason.MailboxServerNotEnoughMemory.ToString(),
			AckReason.MissingMdbProperties.ToString(),
			AckReason.RecipientMailboxQuarantined.ToString()
		};

		// Token: 0x04000860 RID: 2144
		internal static readonly List<string> WildCardTransientResponseList = new List<string>
		{
			"MailboxOfflineException",
			"MailboxUnavailableException.MapiExceptionUnknownUser",
			"StorageTransientException.MapiExceptionTimeout",
			"501 5.1.3 Invalid address"
		};

		// Token: 0x04000861 RID: 2145
		internal static readonly List<MailboxDeliveryAvailabilityProbe.KnownException> KnownExceptionList;

		// Token: 0x04000862 RID: 2146
		private List<ProbeResult> allActiveInstanceProbeResults;

		// Token: 0x04000863 RID: 2147
		private Dictionary<string, List<ProbeResult>> activeDatabaseInstanceProbeResults;

		// Token: 0x02000228 RID: 552
		// (Invoke) Token: 0x06001240 RID: 4672
		internal delegate void Followup(ProbeWorkItem workitem);

		// Token: 0x02000229 RID: 553
		// (Invoke) Token: 0x06001244 RID: 4676
		private delegate bool ExceptionFinder(ProbeResult result, string exception);

		// Token: 0x0200022A RID: 554
		internal struct KnownException
		{
			// Token: 0x04000870 RID: 2160
			public string Exception;

			// Token: 0x04000871 RID: 2161
			public int MonitoringThreshold;

			// Token: 0x04000872 RID: 2162
			public int FailurePercent;

			// Token: 0x04000873 RID: 2163
			public List<MailboxDeliveryAvailabilityProbe.Followup> Followups;
		}

		// Token: 0x0200022B RID: 555
		internal struct ExceptionHitBucket
		{
			// Token: 0x04000874 RID: 2164
			public string Exception;

			// Token: 0x04000875 RID: 2165
			public int Count;
		}

		// Token: 0x0200022C RID: 556
		internal class MailDeliveryAvailabilityProbeException : Exception
		{
			// Token: 0x06001247 RID: 4679 RVA: 0x00035B7B File Offset: 0x00033D7B
			public MailDeliveryAvailabilityProbeException(string message) : base(message)
			{
			}
		}

		// Token: 0x0200022D RID: 557
		internal class MailDeliveryAvailabilityException : Exception
		{
			// Token: 0x06001248 RID: 4680 RVA: 0x00035B84 File Offset: 0x00033D84
			public MailDeliveryAvailabilityException(string message) : base(message)
			{
			}
		}
	}
}
