using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OutlookService
{
	// Token: 0x0200025B RID: 603
	internal class ExtraDetailsAlertResponder : EscalateResponder
	{
		// Token: 0x060010E9 RID: 4329 RVA: 0x0007108C File Offset: 0x0006F28C
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, string escalationTeam, string escalationSubjectUnhealthy, string escalationMessageUnhealthy, int probeIntervalSeconds, string logFolderRelativePath, string appPoolName, bool enabled = true, NotificationServiceClass notificationServiceClass = NotificationServiceClass.Urgent, int minimumSecondsBetweenEscalates = 14400, string dailySchedulePattern = "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59")
		{
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(name, serviceName, alertTypeId, alertMask, targetResource, targetHealthState, escalationTeam, escalationSubjectUnhealthy, escalationMessageUnhealthy, enabled, notificationServiceClass, minimumSecondsBetweenEscalates, string.Empty, false);
			responderDefinition.AssemblyPath = ExtraDetailsAlertResponder.AssemblyPath;
			responderDefinition.TypeName = ExtraDetailsAlertResponder.TypeName;
			responderDefinition.Attributes[ExtraDetailsAlertResponder.ProbeIntervalSecondsKey] = probeIntervalSeconds.ToString();
			responderDefinition.Attributes[ExtraDetailsAlertResponder.LogFolderKey] = logFolderRelativePath;
			responderDefinition.Attributes[ExtraDetailsAlertResponder.AppPoolNameKey] = appPoolName;
			return responderDefinition;
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0007115C File Offset: 0x0006F35C
		internal override void GetEscalationSubjectAndMessage(MonitorResult monitorResult, out string escalationSubject, out string escalationMessage, bool rethrow = false, Action<ResponseMessageReader> textGeneratorModifier = null)
		{
			Action<ResponseMessageReader> action = null;
			bool flag = false;
			string escalationMessage2 = base.Definition.EscalationMessage;
			if (escalationMessage2.Contains(ExtraDetailsAlertResponder.OncallHelpHtmlKeyword))
			{
				base.Definition.EscalationMessage = escalationMessage2.Replace(ExtraDetailsAlertResponder.OncallHelpHtmlKeyword, this.GetEscalationMessageHtmlTemplate());
				flag = true;
			}
			if (flag && monitorResult.LastFailedProbeResultId > 0)
			{
				ProbeResult result = base.Broker.GetProbeResult(monitorResult.LastFailedProbeId, monitorResult.LastFailedProbeResultId).ExecuteAsync(base.LocalCancellationToken, base.TraceContext).Result;
				if (result != null)
				{
					try
					{
						ExtraDetailsAlertResponder.ExtraStrings extra = this.BuildExtraStrings(result, monitorResult);
						action = delegate(ResponseMessageReader reader)
						{
							if (textGeneratorModifier != null)
							{
								textGeneratorModifier(reader);
							}
							reader.AddObjectResolver<ExtraDetailsAlertResponder.ExtraStrings>("Extra", () => extra);
						};
					}
					catch (Exception ex)
					{
						base.Result.StateAttribute5 = ex.ToString();
						WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "[ExtraDetailsAlertResponder.GetEscalationSubjectAndMessage] Exception hit while generating alert contents: {0}", ex.ToString(), null, "GetEscalationSubjectAndMessage", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\Responders\\ExtraDetailsAlertResponder.cs", 200);
					}
				}
			}
			base.GetEscalationSubjectAndMessage(monitorResult, out escalationSubject, out escalationMessage, rethrow, action ?? textGeneratorModifier);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x00071290 File Offset: 0x0006F490
		private ExtraDetailsAlertResponder.ExtraStrings BuildExtraStrings(ProbeResult probeResult, MonitorResult monitorResult)
		{
			string impactedServerFqdn = this.GetImpactedServerFqdn(probeResult, monitorResult);
			TimeSpan t = TimeSpan.FromSeconds(double.Parse(base.Definition.Attributes[ExtraDetailsAlertResponder.ProbeIntervalSecondsKey]) * 5.0);
			DateTime dateTime;
			if (monitorResult.FirstAlertObservedTime != null)
			{
				dateTime = monitorResult.FirstAlertObservedTime.Value - t;
			}
			else
			{
				dateTime = probeResult.ExecutionStartTime - TimeSpan.FromMinutes(2.0);
			}
			string ospurl;
			string debugUrl;
			if (impactedServerFqdn.ToLower().Contains("namsdf01"))
			{
				ospurl = ExtraDetailsAlertResponder.OSPUrlSDF;
				debugUrl = ExtraDetailsAlertResponder.DebugUrlSDF;
			}
			else
			{
				ospurl = ExtraDetailsAlertResponder.OSPUrlProd;
				debugUrl = ExtraDetailsAlertResponder.DebugUrlProd;
			}
			return new ExtraDetailsAlertResponder.ExtraStrings
			{
				ServerName = ExtraDetailsAlertResponder.GetServerNameFromFqdn(impactedServerFqdn),
				ServerFQDN = impactedServerFqdn,
				Username = this.GetUsername(probeResult, monitorResult),
				Password = this.GetPassword(probeResult, monitorResult),
				LogFolder = base.Definition.Attributes[ExtraDetailsAlertResponder.LogFolderKey],
				LogStartTime = dateTime.ToString("s"),
				LogEndTime = probeResult.ExecutionEndTime.ToString("s"),
				RequestId = this.GetRequestId(probeResult, monitorResult),
				AppPoolName = base.Definition.Attributes[ExtraDetailsAlertResponder.AppPoolNameKey],
				OSPUrl = ospurl,
				DebugUrl = debugUrl,
				ProbeFullName = this.GetFullProbeName(probeResult)
			};
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x00071414 File Offset: 0x0006F614
		private static string GetServerNameFromFqdn(string fqdn)
		{
			if (string.IsNullOrEmpty(fqdn))
			{
				throw new ArgumentNullException("fqdn");
			}
			if (fqdn.Contains('.'))
			{
				return fqdn.Split(new char[]
				{
					'.'
				})[0];
			}
			return string.Empty;
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x00071459 File Offset: 0x0006F659
		private string GetImpactedServerFqdn(ProbeResult probeResult, MonitorResult monitorResult)
		{
			return DirectoryGeneralUtils.GetLocalFQDN();
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x00071460 File Offset: 0x0006F660
		private string GetUsername(ProbeResult probeResult, MonitorResult monitorResult)
		{
			if (probeResult.StateAttribute23 == null)
			{
				return "No Account available";
			}
			return probeResult.StateAttribute23;
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x00071476 File Offset: 0x0006F676
		private string GetPassword(ProbeResult probeResult, MonitorResult monitorResult)
		{
			if (probeResult.StateAttribute22 == null)
			{
				return "No Password available";
			}
			return probeResult.StateAttribute22;
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0007148C File Offset: 0x0006F68C
		private string GetRequestId(ProbeResult probeResult, MonitorResult monitorResult)
		{
			return probeResult.StateAttribute1;
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x00071494 File Offset: 0x0006F694
		private string GetFullProbeName(ProbeResult proberesult)
		{
			return proberesult.HealthSetName + "\\" + proberesult.ResultName;
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x000714AC File Offset: 0x0006F6AC
		private string GetEscalationMessageHtmlTemplate()
		{
			string result = "";
			try
			{
				using (Stream manifestResourceStream = typeof(ExtraDetailsAlertResponder).Assembly.GetManifestResourceStream(ExtraDetailsAlertResponder.OutlookServiceProbeEscalationHtml))
				{
					if (manifestResourceStream != null)
					{
						using (StreamReader streamReader = new StreamReader(manifestResourceStream))
						{
							result = streamReader.ReadToEnd();
							goto IL_87;
						}
					}
					base.Result.StateAttribute5 = string.Format("Problem reading resource file {0}", ExtraDetailsAlertResponder.OutlookServiceProbeEscalationHtml);
					WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "[ExtraDetailsAlertResponder.GetEscalationSubjectAndMessage] {0}", base.Result.StateAttribute5, null, "GetEscalationMessageHtmlTemplate", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\Responders\\ExtraDetailsAlertResponder.cs", 387);
					IL_87:;
				}
			}
			catch (Exception ex)
			{
				base.Result.StateAttribute5 = ex.ToString();
				WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "[ExtraDetailsAlertResponder.GetEscalationSubjectAndMessage] Exception hit while getting EscalationMessageHtmlTemplate: {0}", ex.ToString(), null, "GetEscalationMessageHtmlTemplate", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OutlookService\\Responders\\ExtraDetailsAlertResponder.cs", 400);
			}
			return result;
		}

		// Token: 0x04000CB8 RID: 3256
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000CB9 RID: 3257
		private static readonly string TypeName = typeof(ExtraDetailsAlertResponder).FullName;

		// Token: 0x04000CBA RID: 3258
		internal static readonly string ProbeIntervalSecondsKey = "ProbeIntervalSecondsKey";

		// Token: 0x04000CBB RID: 3259
		internal static readonly string LogFolderKey = "LogFolderKey";

		// Token: 0x04000CBC RID: 3260
		internal static readonly string AppPoolNameKey = "AppPoolNameKey";

		// Token: 0x04000CBD RID: 3261
		internal static readonly string ProbeMonitorResultParserTypeKey = "ProbeMonitorResultParserTypeKey";

		// Token: 0x04000CBE RID: 3262
		private static readonly string OncallHelpHtmlKeyword = "{OnCallHelpHtml}";

		// Token: 0x04000CBF RID: 3263
		private static readonly string OutlookServiceProbeEscalationHtml = "EscalationMessage.OutlookServiceProbe.html";

		// Token: 0x04000CC0 RID: 3264
		private static readonly string OSPUrlProd = "https://osp.outlook.com";

		// Token: 0x04000CC1 RID: 3265
		private static readonly string DebugUrlProd = "https://osp.outlook.com/ecp/osp/Change/remote.rdp?machine=BL2PR03LG202&forest=namprd03.prod.outlook.com&service=Exchange&loc=BL2";

		// Token: 0x04000CC2 RID: 3266
		private static readonly string OSPUrlSDF = "https://ospbeta.outlook.com";

		// Token: 0x04000CC3 RID: 3267
		private static readonly string DebugUrlSDF = "https://ospbeta.outlook.com/ecp/osp/Change/remote.rdp?machine=SN2SDF0110LG001&forest=namsdf01.sdf.exchangelabs.com&service=Exchange&loc=SN2";

		// Token: 0x0200025C RID: 604
		internal class ExtraStrings
		{
			// Token: 0x04000CC4 RID: 3268
			public string ServerName;

			// Token: 0x04000CC5 RID: 3269
			public string ServerFQDN;

			// Token: 0x04000CC6 RID: 3270
			public string Username;

			// Token: 0x04000CC7 RID: 3271
			public string Password;

			// Token: 0x04000CC8 RID: 3272
			public string LogFolder;

			// Token: 0x04000CC9 RID: 3273
			public string LogStartTime;

			// Token: 0x04000CCA RID: 3274
			public string LogEndTime;

			// Token: 0x04000CCB RID: 3275
			public string RequestId;

			// Token: 0x04000CCC RID: 3276
			public string AppPoolName;

			// Token: 0x04000CCD RID: 3277
			public string OSPUrl;

			// Token: 0x04000CCE RID: 3278
			public string DebugUrl;

			// Token: 0x04000CCF RID: 3279
			public string ProbeFullName;
		}
	}
}
