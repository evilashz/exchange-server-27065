using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveSync.Probes
{
	// Token: 0x0200000B RID: 11
	public class ActiveSyncMailboxDeepProbe : ActiveSyncProbeBase
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00004248 File Offset: 0x00002448
		public static ProbeDefinition CreateDefinition(string assemblyPath, string endpoint, int recurrence)
		{
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = assemblyPath;
			probeDefinition.TypeName = typeof(ActiveSyncMailboxDeepProbe).FullName;
			probeDefinition.Name = "ActiveSyncDeepTestProbe";
			probeDefinition.ServiceName = ExchangeComponent.ActiveSyncProtocol.Name;
			probeDefinition.RecurrenceIntervalSeconds = recurrence;
			probeDefinition.TimeoutSeconds = Math.Min(recurrence, 90) - 2;
			probeDefinition.MaxRetryAttempts = 3;
			probeDefinition.Endpoint = endpoint;
			probeDefinition.TargetResource = "MSExchangeSyncAppPool";
			if (VariantConfiguration.InvariantNoFlightingSnapshot.ActiveMonitoring.ActiveSyncDiscovery.Enabled)
			{
				probeDefinition.Attributes["DCProbe"] = true.ToString();
			}
			return probeDefinition;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000042F8 File Offset: 0x000024F8
		public override void PopulateDefinition<Definition>(Definition definition, Dictionary<string, string> propertyBag)
		{
			ProbeDefinition probeDefinition = definition as ProbeDefinition;
			if (probeDefinition == null)
			{
				throw new ArgumentException("definition must be a ProbeDefinition");
			}
			probeDefinition.Endpoint = "https://localhost:444/Microsoft-Server-ActiveSync/Proxy";
			if (VariantConfiguration.InvariantNoFlightingSnapshot.ActiveMonitoring.ActiveSyncDiscovery.Enabled)
			{
				probeDefinition.Attributes["DCProbe"] = true.ToString();
			}
			probeDefinition.Attributes["InvokeNowExecution"] = true.ToString();
			if (propertyBag.ContainsKey("Account"))
			{
				probeDefinition.Account = propertyBag["Account"];
			}
			if (propertyBag.ContainsKey("Endpoint"))
			{
				probeDefinition.Endpoint = propertyBag["Endpoint"];
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000043B4 File Offset: 0x000025B4
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return new List<PropertyInformation>();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000043C8 File Offset: 0x000025C8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			base.Result.StateAttribute21 = "PDWS;";
			this.latencyMeasurementStart = DateTime.UtcNow;
			this.isDcProbe = false;
			if (base.Definition.Attributes.ContainsKey("DCProbe"))
			{
				bool.TryParse(base.Definition.Attributes["DCProbe"], out this.isDcProbe);
			}
			if (base.Definition.Attributes.ContainsKey("KnownFailure"))
			{
				this.acceptableErrors.AddRange(base.Definition.Attributes["KnownFailure"].ToLowerInvariant().Split(new string[]
				{
					";"
				}, StringSplitOptions.None));
			}
			base.TrustAllCerts();
			if (string.IsNullOrEmpty(base.Definition.Account))
			{
				MailboxDatabaseInfo monitoringAccount = this.GetMonitoringAccount();
				base.Definition.Account = monitoringAccount.MonitoringAccount + "@" + monitoringAccount.MonitoringAccountDomain;
				base.Definition.AccountPassword = monitoringAccount.MonitoringAccountPassword;
			}
			HttpWebRequest httpWebRequest = null;
			this.probeTrackingObject = new ActiveSyncProbeStateObject(httpWebRequest, base.Result, ProbeState.Settings1);
			try
			{
				httpWebRequest = ActiveSyncProbeUtil.CreateSettingsCommand(base.Definition.Endpoint, true, this.isDcProbe, base.Definition.Account, base.Definition.AccountPassword, "<?xml version=\"1.0\" encoding=\"utf-8\"?><Settings xmlns=\"Settings:\"><UserInformation><Get/></UserInformation></Settings>", "14.1", string.Empty, 0);
				this.probeTrackingObject.Result.StateAttribute22 = base.Definition.Endpoint;
				this.probeTrackingObject.WebRequest = httpWebRequest;
			}
			catch (CommonAccessTokenException ex)
			{
				this.probeTrackingObject.ProbeErrorResponse = "CommonAccessToken Creation Failure";
				this.probeTrackingObject.Result.FailureContext = string.Format("Error encountered when creating CommonAccessToken from windows identity {0} : {1}}", base.Definition.Account, ex.Message);
				this.probeTrackingObject.State = ProbeState.Failure;
			}
			if (this.probeTrackingObject.State == ProbeState.Failure)
			{
				base.ReportFailure(false);
				return;
			}
			this.isInvokeNowExecution = false;
			if (base.Definition.Attributes.ContainsKey("InvokeNowExecution"))
			{
				bool.TryParse(base.Definition.Attributes["InvokeNowExecution"], out this.isInvokeNowExecution);
			}
			this.probeTrackingObject.TimeoutLimit = DateTime.UtcNow.AddMilliseconds(50000.0);
			base.Result.StateAttribute21 = string.Concat(new object[]
			{
				"MaxTimeout:",
				this.probeTrackingObject.TimeoutLimit,
				";",
				base.Result.StateAttribute21
			});
			ProbeResult result = base.Result;
			object stateAttribute = result.StateAttribute21;
			result.StateAttribute21 = string.Concat(new object[]
			{
				stateAttribute,
				"PDWE:",
				DateTime.UtcNow.TimeOfDay,
				";"
			});
			base.DoWork(cancellationToken);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000046C8 File Offset: 0x000028C8
		protected override void ParseResponseSetNextState(ActiveSyncProbeStateObject probeStateObject)
		{
			ProbeResult result = probeStateObject.Result;
			result.StateAttribute21 += "PSMS;";
			ActiveSyncProbeBase.StateResult stateResult = ActiveSyncProbeBase.StateResult.Success;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ActiveSyncTracer, base.TraceContext, "Parsing Options response.", null, "ParseResponseSetNextState", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ActiveSync\\ActiveSyncMailboxDeepProbe.cs", 218);
			if (!base.AcceptableError(probeStateObject.WebResponses[probeStateObject.LastResponseIndex].DiagnosticsValue))
			{
				if (probeStateObject.WebResponses[probeStateObject.LastResponseIndex].HttpStatus == 401)
				{
					stateResult = ActiveSyncProbeBase.StateResult.Fail;
					this.probeTrackingObject.ProbeErrorResponse = "CommonAccessToken Validation Failure";
				}
				else if (probeStateObject.WebResponses[probeStateObject.LastResponseIndex].HttpStatus == 503)
				{
					stateResult = ActiveSyncProbeBase.StateResult.Retry;
				}
				else if (probeStateObject.WebResponses[probeStateObject.LastResponseIndex].HttpStatus != 200)
				{
					stateResult = ActiveSyncProbeBase.StateResult.Fail;
				}
				else if (probeStateObject.WebResponses[probeStateObject.LastResponseIndex].ActiveSyncStatus[0] == 111)
				{
					stateResult = ActiveSyncProbeBase.StateResult.Retry;
				}
				else if (probeStateObject.WebResponses[probeStateObject.LastResponseIndex].ActiveSyncStatus.Length != 2 || probeStateObject.WebResponses[probeStateObject.LastResponseIndex].ActiveSyncStatus[0] != 1)
				{
					stateResult = ActiveSyncProbeBase.StateResult.Fail;
				}
			}
			switch (probeStateObject.State)
			{
			case ProbeState.Settings1:
			{
				switch (stateResult)
				{
				case ActiveSyncProbeBase.StateResult.Success:
					probeStateObject.State = ProbeState.Finish;
					break;
				case ActiveSyncProbeBase.StateResult.Retry:
					probeStateObject.WebRequest = ActiveSyncProbeUtil.CreateSettingsCommand(base.Definition.Endpoint, true, this.isDcProbe, base.Definition.Account, base.Definition.AccountPassword, "<?xml version=\"1.0\" encoding=\"utf-8\"?><Settings xmlns=\"Settings:\"><UserInformation><Get/></UserInformation></Settings>", "14.1", string.Empty, 0);
					probeStateObject.State = ProbeState.Settings2;
					break;
				default:
					probeStateObject.State = ProbeState.Failure;
					break;
				}
				ProbeResult result2 = probeStateObject.Result;
				result2.StateAttribute13 += "S1";
				break;
			}
			case ProbeState.Settings2:
			{
				ActiveSyncProbeBase.StateResult stateResult2 = stateResult;
				if (stateResult2 == ActiveSyncProbeBase.StateResult.Success)
				{
					probeStateObject.State = ProbeState.Finish;
				}
				else
				{
					probeStateObject.State = ProbeState.Failure;
				}
				ProbeResult result3 = probeStateObject.Result;
				result3.StateAttribute13 += "S2";
				break;
			}
			default:
			{
				probeStateObject.State = ProbeState.Failure;
				ProbeResult result4 = probeStateObject.Result;
				result4.StateAttribute13 += "F";
				break;
			}
			}
			ProbeResult result5 = probeStateObject.Result;
			object stateAttribute = result5.StateAttribute21;
			result5.StateAttribute21 = string.Concat(new object[]
			{
				stateAttribute,
				"PSME:",
				DateTime.UtcNow.TimeOfDay,
				";"
			});
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00004958 File Offset: 0x00002B58
		protected override void HandleSocketError(ActiveSyncProbeStateObject probeStateObject)
		{
			WTFDiagnostics.TraceError(ExTraceGlobals.ActiveSyncTracer, base.TraceContext, "Socket exception considered a failure, should never have SocketException on local machines.", null, "HandleSocketError", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ActiveSync\\ActiveSyncMailboxDeepProbe.cs", 315);
			probeStateObject.State = ProbeState.Failure;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00004988 File Offset: 0x00002B88
		protected MailboxDatabaseInfo GetMonitoringAccount()
		{
			if (LocalEndpointManager.Instance.ExchangeServerRoleEndpoint == null)
			{
				return null;
			}
			if (LocalEndpointManager.Instance.MailboxDatabaseEndpoint == null)
			{
				return null;
			}
			ICollection<MailboxDatabaseInfo> mailboxDatabaseInfoCollectionForBackend = LocalEndpointManager.Instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend;
			Random random = new Random();
			int index = random.Next(mailboxDatabaseInfoCollectionForBackend.Count);
			return mailboxDatabaseInfoCollectionForBackend.ElementAt(index);
		}

		// Token: 0x0400002E RID: 46
		protected const string ProtocolVersion = "14.1";

		// Token: 0x0400002F RID: 47
		protected const string Endpoint = "https://localhost:444/Microsoft-Server-ActiveSync/Proxy";

		// Token: 0x04000030 RID: 48
		private const int Timeout = 50000;
	}
}
