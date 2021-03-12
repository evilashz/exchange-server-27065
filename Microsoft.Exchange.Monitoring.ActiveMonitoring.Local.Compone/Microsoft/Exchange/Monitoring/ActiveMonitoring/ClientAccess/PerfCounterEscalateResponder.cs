using System;
using System.Reflection;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ClientAccess
{
	// Token: 0x02000050 RID: 80
	internal class PerfCounterEscalateResponder : EscalateResponder
	{
		// Token: 0x06000288 RID: 648 RVA: 0x00011CE4 File Offset: 0x0000FEE4
		static PerfCounterEscalateResponder()
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 55, ".cctor", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Cafe\\Responders\\PerfCounterEscalateResponder.cs");
			PerfCounterEscalateResponder.LocalSite = topologyConfigurationSession.GetLocalSite().Name;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00011D4C File Offset: 0x0000FF4C
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, string escalationTeam, string escalationSubjectUnhealthy, string escalationMessageUnhealthy, string perfCounterValueUnits, bool enabled = true, NotificationServiceClass notificationServiceClass = NotificationServiceClass.Urgent, int minimumSecondsBetweenEscalates = 14400, string dailySchedulePattern = "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59")
		{
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(name, serviceName, alertTypeId, alertMask, targetResource, targetHealthState, escalationTeam, escalationSubjectUnhealthy, escalationMessageUnhealthy, enabled, notificationServiceClass, minimumSecondsBetweenEscalates, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.AssemblyPath = PerfCounterEscalateResponder.AssemblyPath;
			responderDefinition.TypeName = PerfCounterEscalateResponder.TypeName;
			responderDefinition.Attributes[PerfCounterEscalateResponder.ValueUnitsKey] = perfCounterValueUnits;
			return responderDefinition;
		}

		// Token: 0x0600028A RID: 650 RVA: 0x00011DCC File Offset: 0x0000FFCC
		internal override void GetEscalationSubjectAndMessage(MonitorResult monitorResult, out string escalationSubject, out string escalationMessage, bool rethrow = false, Action<ResponseMessageReader> textGeneratorModifier = null)
		{
			if (monitorResult.LastFailedProbeResultId > 0)
			{
				ProbeResult result = base.Broker.GetProbeResult(monitorResult.LastFailedProbeId, monitorResult.LastFailedProbeResultId).ExecuteAsync(base.LocalCancellationToken, base.TraceContext).Result;
				if (result != null)
				{
					string[] array = result.Error.Split(new char[]
					{
						'$'
					});
					if (array.Length == 3)
					{
						array = array[1].Split(new char[]
						{
							'|'
						});
						if (array.Length == 4)
						{
							string text = this.ReadAttribute(PerfCounterEscalateResponder.ValueUnitsKey, string.Empty);
							PerfCounterEscalateResponder.PassiveAlertData data = new PerfCounterEscalateResponder.PassiveAlertData
							{
								CounterName = array[0],
								Protocol = array[1],
								DestSite = array[2].ToUpper(),
								CounterValue = array[3] + text,
								LocalSite = PerfCounterEscalateResponder.LocalSite,
								FullText = result.Error
							};
							try
							{
								string diffProtcolsToDestSiteHtml;
								string sameProtocolToDiffSitesHtml;
								PerfCounterHelper.GetPerSiteCountersHtml(data.CounterName, data.Protocol, data.DestSite, text, out diffProtcolsToDestSiteHtml, out sameProtocolToDiffSitesHtml);
								data.DiffProtcolsToDestSiteHtml = diffProtcolsToDestSiteHtml;
								data.SameProtocolToDiffSitesHtml = sameProtocolToDiffSitesHtml;
								data.AdditionalCountersHtml = PerfCounterHelper.GetInterestingCountersHtml(data.Protocol);
							}
							catch (Exception ex)
							{
								PerfCounterEscalateResponder.PassiveAlertData data2 = data;
								data2.FullText = data2.FullText + "\n" + ex.ToString();
							}
							textGeneratorModifier = delegate(ResponseMessageReader reader)
							{
								reader.AddObjectResolver<PerfCounterEscalateResponder.PassiveAlertData>("PassiveAlertData", () => data);
							};
						}
					}
				}
			}
			base.GetEscalationSubjectAndMessage(monitorResult, out escalationSubject, out escalationMessage, rethrow, textGeneratorModifier);
		}

		// Token: 0x040001D6 RID: 470
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040001D7 RID: 471
		private static readonly string TypeName = typeof(PerfCounterEscalateResponder).FullName;

		// Token: 0x040001D8 RID: 472
		private static readonly string ValueUnitsKey = "ValueUnitsKey";

		// Token: 0x040001D9 RID: 473
		private static readonly string LocalSite;

		// Token: 0x02000051 RID: 81
		internal class PassiveAlertData
		{
			// Token: 0x040001DA RID: 474
			public string CounterName = PerfCounterEscalateResponder.PassiveAlertData.NoData;

			// Token: 0x040001DB RID: 475
			public string Protocol = PerfCounterEscalateResponder.PassiveAlertData.NoData;

			// Token: 0x040001DC RID: 476
			public string DestSite = PerfCounterEscalateResponder.PassiveAlertData.NoData;

			// Token: 0x040001DD RID: 477
			public string CounterValue = PerfCounterEscalateResponder.PassiveAlertData.NoData;

			// Token: 0x040001DE RID: 478
			public string DiffProtcolsToDestSiteHtml = PerfCounterEscalateResponder.PassiveAlertData.NoData;

			// Token: 0x040001DF RID: 479
			public string SameProtocolToDiffSitesHtml = PerfCounterEscalateResponder.PassiveAlertData.NoData;

			// Token: 0x040001E0 RID: 480
			public string AdditionalCountersHtml = PerfCounterEscalateResponder.PassiveAlertData.NoData;

			// Token: 0x040001E1 RID: 481
			public string ProtocolLogHtml = PerfCounterEscalateResponder.PassiveAlertData.NoData;

			// Token: 0x040001E2 RID: 482
			public string LocalSite = PerfCounterEscalateResponder.PassiveAlertData.NoData;

			// Token: 0x040001E3 RID: 483
			public string FullText = PerfCounterEscalateResponder.PassiveAlertData.NoData;

			// Token: 0x040001E4 RID: 484
			private static readonly string NoData = "No data.";
		}
	}
}
