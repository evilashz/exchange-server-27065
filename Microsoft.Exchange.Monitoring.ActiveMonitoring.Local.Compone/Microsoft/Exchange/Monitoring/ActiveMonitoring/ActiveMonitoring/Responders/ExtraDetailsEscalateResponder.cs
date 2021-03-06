using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders
{
	// Token: 0x0200004A RID: 74
	internal class ExtraDetailsEscalateResponder : EscalateResponder
	{
		// Token: 0x0600026A RID: 618 RVA: 0x0001149C File Offset: 0x0000F69C
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, string escalationService, string escalationTeam, string escalationSubjectUnhealthy, string escalationMessageUnhealthy, int probeIntervalSeconds, string logFolderRelativePath, string appPoolName, Type probeMonitorResultParserType, bool enabled = true, NotificationServiceClass notificationServiceClass = NotificationServiceClass.Urgent, int minimumSecondsBetweenEscalates = 14400, string dailySchedulePattern = "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59")
		{
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(name, serviceName, alertTypeId, alertMask, targetResource, targetHealthState, escalationService, escalationTeam, escalationSubjectUnhealthy, escalationMessageUnhealthy, enabled, notificationServiceClass, minimumSecondsBetweenEscalates, string.Empty, false);
			responderDefinition.AssemblyPath = ExtraDetailsEscalateResponder.AssemblyPath;
			responderDefinition.TypeName = ExtraDetailsEscalateResponder.TypeName;
			responderDefinition.Attributes[ExtraDetailsEscalateResponder.ProbeIntervalSecondsKey] = probeIntervalSeconds.ToString();
			responderDefinition.Attributes[ExtraDetailsEscalateResponder.LogFolderKey] = logFolderRelativePath;
			responderDefinition.Attributes[ExtraDetailsEscalateResponder.AppPoolNameKey] = appPoolName;
			responderDefinition.Attributes[ExtraDetailsEscalateResponder.ProbeMonitorResultParserTypeKey] = probeMonitorResultParserType.ToString();
			return responderDefinition;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00011584 File Offset: 0x0000F784
		internal override void GetEscalationSubjectAndMessage(MonitorResult monitorResult, out string escalationSubject, out string escalationMessage, bool rethrow = false, Action<ResponseMessageReader> textGeneratorModifier = null)
		{
			Action<ResponseMessageReader> action = null;
			bool flag = false;
			string escalationMessage2 = base.Definition.EscalationMessage;
			if (escalationMessage2.Contains(ExtraDetailsEscalateResponder.OncallHelpHtmlKeyword))
			{
				base.Definition.EscalationMessage = escalationMessage2.Replace(ExtraDetailsEscalateResponder.OncallHelpHtmlKeyword, ExtraDetailsEscalateResponder.HtmlTemplate);
				flag = true;
			}
			if (flag && monitorResult.LastFailedProbeResultId > 0)
			{
				ProbeResult result = base.Broker.GetProbeResult(monitorResult.LastFailedProbeId, monitorResult.LastFailedProbeResultId).ExecuteAsync(base.LocalCancellationToken, base.TraceContext).Result;
				if (result != null)
				{
					try
					{
						ObjectHandle objectHandle = Activator.CreateInstance(null, base.Definition.Attributes[ExtraDetailsEscalateResponder.ProbeMonitorResultParserTypeKey]);
						ExtraDetailsEscalateResponder.IProbeMonitorResultParser parser = (ExtraDetailsEscalateResponder.IProbeMonitorResultParser)objectHandle.Unwrap();
						ExtraDetailsEscalateResponder.ExtraStrings extra = this.BuildExtraStrings(result, monitorResult, parser);
						action = delegate(ResponseMessageReader reader)
						{
							if (textGeneratorModifier != null)
							{
								textGeneratorModifier(reader);
							}
							reader.AddObjectResolver<ExtraDetailsEscalateResponder.ExtraStrings>("Extra", () => extra);
						};
					}
					catch (Exception ex)
					{
						base.Result.StateAttribute5 = ex.ToString();
						WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "[ExtraDetailsEscalateResponder.GetEscalationSubjectAndMessage] Exception hit while generating alert contents: {0}", ex.ToString(), null, "GetEscalationSubjectAndMessage", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\ExtraDetailsEscalateResponder.cs", 212);
					}
				}
			}
			base.GetEscalationSubjectAndMessage(monitorResult, out escalationSubject, out escalationMessage, rethrow, action ?? textGeneratorModifier);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x000116E8 File Offset: 0x0000F8E8
		private ExtraDetailsEscalateResponder.ExtraStrings BuildExtraStrings(ProbeResult probeResult, MonitorResult monitorResult, ExtraDetailsEscalateResponder.IProbeMonitorResultParser parser)
		{
			string impactedServerFqdn = parser.GetImpactedServerFqdn(probeResult, monitorResult);
			TimeSpan t = TimeSpan.FromSeconds(double.Parse(base.Definition.Attributes[ExtraDetailsEscalateResponder.ProbeIntervalSecondsKey]) * 5.0);
			DateTime dateTime;
			if (monitorResult.FirstAlertObservedTime != null)
			{
				dateTime = monitorResult.FirstAlertObservedTime.Value - t;
			}
			else
			{
				dateTime = probeResult.ExecutionStartTime - TimeSpan.FromMinutes(2.0);
			}
			string text = parser.GetHelpUrlForError(probeResult.Error.Trim());
			if (!string.IsNullOrEmpty(text))
			{
				text = HtmlHelper.CreateLink(ExtraDetailsEscalateResponder.ErrorHelpLinkText, text);
			}
			string ospurl;
			string debugUrl;
			if (impactedServerFqdn.ToLower().Contains("namsdf01"))
			{
				ospurl = ExtraDetailsEscalateResponder.OSPUrlSDF;
				debugUrl = ExtraDetailsEscalateResponder.DebugUrlSDF;
			}
			else
			{
				ospurl = ExtraDetailsEscalateResponder.OSPUrlProd;
				debugUrl = ExtraDetailsEscalateResponder.DebugUrlProd;
			}
			return new ExtraDetailsEscalateResponder.ExtraStrings
			{
				OneNoteLink = text,
				ServerName = ExtraDetailsEscalateResponder.GetServerNameFromFqdn(impactedServerFqdn),
				ServerFQDN = impactedServerFqdn,
				Username = parser.GetUsername(probeResult, monitorResult),
				Password = parser.GetPassword(probeResult, monitorResult),
				LogFolder = base.Definition.Attributes[ExtraDetailsEscalateResponder.LogFolderKey],
				LogStartTime = dateTime.ToString("s"),
				LogEndTime = probeResult.ExecutionEndTime.ToString("s"),
				RequestId = parser.GetRequestId(probeResult, monitorResult),
				AppPoolName = base.Definition.Attributes[ExtraDetailsEscalateResponder.AppPoolNameKey],
				OSPUrl = ospurl,
				DebugUrl = debugUrl
			};
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0001188C File Offset: 0x0000FA8C
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

		// Token: 0x040001B9 RID: 441
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040001BA RID: 442
		private static readonly string TypeName = typeof(ExtraDetailsEscalateResponder).FullName;

		// Token: 0x040001BB RID: 443
		internal static readonly string ProbeIntervalSecondsKey = "ProbeIntervalSecondsKey";

		// Token: 0x040001BC RID: 444
		internal static readonly string LogFolderKey = "LogFolderKey";

		// Token: 0x040001BD RID: 445
		internal static readonly string AppPoolNameKey = "AppPoolNameKey";

		// Token: 0x040001BE RID: 446
		internal static readonly string ProbeMonitorResultParserTypeKey = "ProbeMonitorResultParserTypeKey";

		// Token: 0x040001BF RID: 447
		private static readonly string ErrorHelpLinkText = "(Onenote for specific troubleshooting)";

		// Token: 0x040001C0 RID: 448
		private static readonly string OncallHelpHtmlKeyword = "{OnCallHelpHtml}";

		// Token: 0x040001C1 RID: 449
		private static readonly string OSPUrlProd = "https://osp.outlook.com";

		// Token: 0x040001C2 RID: 450
		private static readonly string DebugUrlProd = "https://osp.outlook.com/ecp/osp/change/remote.rdp?server=BL2PR03LG202.namprd03.prod.outlook.com&gateway=exordg.swe.prd.msft.net";

		// Token: 0x040001C3 RID: 451
		private static readonly string OSPUrlSDF = "https://ospbeta.outlook.com";

		// Token: 0x040001C4 RID: 452
		private static readonly string DebugUrlSDF = "https://ospbeta.outlook.com/ecp/osp/change/remote.rdp?server=CH1SDF0110LG001.namsdf01.sdf.exchangelabs.com&gateway=exordg.swe.prd.msft.net";

		// Token: 0x040001C5 RID: 453
		private static readonly string HtmlTemplate = "<font face=\"Segoe UI\" size=\"3\">investigation steps</font><br> You can visit the <a href=\"https://msft.spoppe.com/teams/EXOos/_layouts/OneNote.aspx?id=%2fteams%2fEXOos%2fShared%20Documents%2fCafe%20Alert%20Troubleshooting&wd=target%28Monitoring%20Overview.one%7c3877724C-1245-4737-898D-FF183B8E69F7%2f%29\">Monitoring Overview</a> for information on our probes.  You can use the following tools once connected to a debug server to diagnose further.<br><br>  <table cellpadding=\"0\" cellspacing=\"0\">     <tr style=\"\">         <td style=\"background: #1D96E0;text-align: center; height: 40px; width: 160px; font-family: Calibri; font-size: 15px;\">         <a href=\"{Extra.OSPUrl}\" target=\"_blank\" style=\"text-decoration: none; color: white; padding: 10px 15px;\">         Office Service Pulse         </a></td>                 <td style=\"width: 10px;\">&nbsp;</td>                 <td style=\"background: #455E66; text-align: center; height: 40px; width: 210px; font-family: Calibri; font-size: 15px;\">         <a href=\"{Extra.DebugUrl}\" target=\"_blank\" style=\"text-decoration: none; color: white; padding: 10px 18px;\">         Connect to a Debug Server         </a></td>     </tr> </table>   <ul> <li style=\"margin-bottom:20px\">     <p><b>Check for unhealthy components</b><br>     Is this the only protocol that's affected?  Has the server recovered since the alert?  Dependent components that are unhealthy may be the cause of this failure. The following must be run in the server's capacity forest.</p>      <table cellpadding=\"0\" cellspacing=\"0\" style=\"margin: 10px; width: 90%;\">     <tr><td style=\"padding: 6px; font-size:13px;font-family:consolas;background: #D6EDFF;\"><p style=\"margin: 0; padding: 0;\">     Get-ServerHealth -Server {Extra.ServerName}  | ?{$_.AlertValue -Eq \"Unhealthy\" }     </p></td></tr>     </table>           <p>You can also visit the health check page from a debug machine (click through the SSL certificate warning).  If the response shows 200 OK then the server is taking traffic from the load balancer.</p>         <table cellpadding=\"0\" cellspacing=\"0\" style=\"margin: 10px; width: 90%;\">     <tr><td style=\"padding: 6px; font-size:13px;font-family:consolas;background: #e7e7e7;\"><p style=\"margin: 0; padding: 0;\">     https://{Extra.ServerFQDN}/ews/HealthCheck.htm     </td></tr>     </table>     </li>  <li style=\"margin-bottom:20px\">     <p><b>Try manually reproduce the problem</b><br>     You can use the following credentials to manually make a request to the server.  </p>         <table cellpadding=\"0\" cellspacing=\"0\" style=\"margin: 10px; width: 90%;\">     <tr><td style=\"padding: 6px; font-size:13px;font-family:consolas;background: #e7e7e7;\"><p style=\"margin: 0; padding: 0;\">     <b>Credentials:</b> {Extra.Username} {Extra.Password}     </td></tr>     </table> </li>  <li style=\"margin-bottom:20px\">     <b>View protocol logs for the probes</b><br>         <ol>         <li>Copy this folder to the desktop on a debug machine: \\\\redmond\\exchange\\build\\e15\\latest\\sources\\sources\\test\\tools\\src\\LogMiningScripts</li>         <li>Open a capacity forest shell to the server's forest, change to the directory just copied and execute this command. <br>                     <table cellpadding=\"0\" cellspacing=\"0\" style=\"margin: 10px; width: 90%;\">         <tr><td style=\"padding: 6px; font-size:13px;font-family:consolas;background: #D6EDFF;\"><p style=\"margin: 0; padding: 0;\">         .\\GetProtocolLogs.ps1 -Component \"{Extra.LogFolder}\" -DebugScript $true -Start \"{Extra.LogStartTime}\" -End \"{Extra.LogEndTime}\" -OutputFolder \".\\out\" -Servers (Get-ClientAccessServer {Extra.ServerName}) -User \"{Extra.Username}\"         </p></td></tr>         </table>         <li>Results will be in in the .\\out folder.</li>     </ol>         <p>You can use the following to correlate this alert to the logs:</p>         <table cellpadding=\"0\" cellspacing=\"0\" style=\"margin: 10px; width: 90%;\">     <tr><td style=\"padding: 6px; font-size:13px;font-family:consolas;background: #e7e7e7;\"><p style=\"margin: 0; padding: 0;\">     <b>ExecutionStartTime:</b> {Probe.ExecutionStartTime} <br>     <b>ExecutionEndTime:</b> {Probe.ExecutionEndTime} <br>     <b>RequestID:</b> {Extra.RequestId}    </td></tr>     </table> </li>  <li style=\"margin-bottom:20px\">     <p><b>Recycle the application pool</b><br>     Run the following command to recycle this app pool. Fill in the reason appropriately.</p>         <table cellpadding=\"0\" cellspacing=\"0\" style=\"margin: 10px; width: 90%;\">     <tr><td style=\"padding: 6px; font-size:13px;font-family:consolas;background: #D6EDFF;\"><p style=\"margin: 0; padding: 0;\">     Request-RecycleAppPool.ps1 -AppPoolName {Extra.AppPoolName} -Target {Extra.ServerName} -Reason \"\"     </p></td></tr>     </table> </li>  <li style=\"margin-bottom:20px\">     <p><b>Collect a dump of the process</b><br>     Run this command to request a full memory dump of the process.  The process will be killed.  You must fill in your alias and the reason. </p>         <table cellpadding=\"0\" cellspacing=\"0\" style=\"margin: 10px; width: 90%;\">     <tr><td style=\"padding: 6px; font-size:13px;font-family:consolas;background: #D6EDFF;\"><p style=\"margin: 0; padding: 0;\">     $alias = \"YOUR_ALIAS_HERE\";<br>$reason = \"\";<br>$counters = Get-Counter -ComputerName {Extra.ServerFQDN} -Counter \"\\WAS_W3WP(*)\\Health Ping Reply Latency\";$instanceName = $counters.CounterSamples | ?{$_.InstanceName -like '*{Extra.AppPoolName}*'} | Select-Object InstanceName;$id = $instanceName.InstanceName.Substring(0, $instanceName.InstanceName.IndexOf('_')); Request-DumpProcess.ps1 -processName w3wp -Target {Extra.ServerName} -full -uniquePid $id -Alias ($alias + \"@microsoft.com\") -Reason $reason     </p></td></tr>     </table> </li>  <li style=\"margin-bottom:20px\">     <p><b>Check the event log for more information</b><br>     The application event log may contain errors with details of exceptions or crashes.  You can filter the log by errors to narrow down the results.</p> </li> </ul>";

		// Token: 0x0200004B RID: 75
		internal class ExtraStrings
		{
			// Token: 0x040001C6 RID: 454
			public string OneNoteLink;

			// Token: 0x040001C7 RID: 455
			public string ServerName;

			// Token: 0x040001C8 RID: 456
			public string ServerFQDN;

			// Token: 0x040001C9 RID: 457
			public string Username;

			// Token: 0x040001CA RID: 458
			public string Password;

			// Token: 0x040001CB RID: 459
			public string LogFolder;

			// Token: 0x040001CC RID: 460
			public string LogStartTime;

			// Token: 0x040001CD RID: 461
			public string LogEndTime;

			// Token: 0x040001CE RID: 462
			public string RequestId;

			// Token: 0x040001CF RID: 463
			public string AppPoolName;

			// Token: 0x040001D0 RID: 464
			public string OSPUrl;

			// Token: 0x040001D1 RID: 465
			public string DebugUrl;
		}

		// Token: 0x0200004C RID: 76
		internal interface IProbeMonitorResultParser
		{
			// Token: 0x06000271 RID: 625
			string GetHelpUrlForError(string errorStr);

			// Token: 0x06000272 RID: 626
			string GetImpactedServerFqdn(ProbeResult probeResult, MonitorResult monitorResult);

			// Token: 0x06000273 RID: 627
			string GetUsername(ProbeResult probeResult, MonitorResult monitorResult);

			// Token: 0x06000274 RID: 628
			string GetPassword(ProbeResult probeResult, MonitorResult monitorResult);

			// Token: 0x06000275 RID: 629
			string GetRequestId(ProbeResult probeResult, MonitorResult monitorResult);
		}
	}
}
