﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveSync.Probes
{
	// Token: 0x0200000A RID: 10
	public class ActiveSyncCustomerTouchPointProbe : ActiveSyncProbeBase
	{
		// Token: 0x06000023 RID: 35 RVA: 0x00003BF8 File Offset: 0x00001DF8
		public static ProbeDefinition CreateDefinition(string assemblyPath, string endpoint, int recurrence)
		{
			return new ProbeDefinition
			{
				AssemblyPath = assemblyPath,
				TypeName = typeof(ActiveSyncCustomerTouchPointProbe).FullName,
				Name = "ActiveSyncCTPProbe",
				ServiceName = ExchangeComponent.ActiveSync.Name,
				RecurrenceIntervalSeconds = recurrence,
				TimeoutSeconds = Math.Min(recurrence, 90) - 2,
				MaxRetryAttempts = 3,
				Endpoint = endpoint,
				TargetResource = "MSExchangeSyncAppPool"
			};
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003C74 File Offset: 0x00001E74
		public override void PopulateDefinition<Definition>(Definition definition, Dictionary<string, string> propertyBag)
		{
			ProbeDefinition probeDefinition = definition as ProbeDefinition;
			if (probeDefinition == null)
			{
				throw new ArgumentException("definition must be a ProbeDefinition");
			}
			MailboxDatabaseInfo monitoringAccount = this.GetMonitoringAccount();
			if (monitoringAccount == null)
			{
				throw new ApplicationException("No monitoring account could be found for this server.");
			}
			probeDefinition.Account = monitoringAccount.MonitoringAccount + "@" + monitoringAccount.MonitoringAccountDomain;
			probeDefinition.AccountPassword = monitoringAccount.MonitoringAccountPassword;
			probeDefinition.Endpoint = "https://localhost/Microsoft-Server-ActiveSync";
			if (propertyBag.ContainsKey("Account"))
			{
				probeDefinition.Account = propertyBag["Account"];
			}
			if (propertyBag.ContainsKey("Password"))
			{
				probeDefinition.AccountPassword = propertyBag["Password"];
			}
			if (propertyBag.ContainsKey("Endpoint"))
			{
				probeDefinition.Endpoint = propertyBag["Endpoint"];
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003D40 File Offset: 0x00001F40
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return new List<PropertyInformation>();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003D54 File Offset: 0x00001F54
		protected override void DoWork(CancellationToken cancellationToken)
		{
			base.Result.StateAttribute21 = "PDWS;";
			this.latencyMeasurementStart = DateTime.UtcNow;
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
			HttpWebRequest request = ActiveSyncProbeUtil.CreateSettingsCommand(base.Definition.Endpoint, false, false, base.Definition.Account, base.Definition.AccountPassword, "<?xml version=\"1.0\" encoding=\"utf-8\"?><Settings xmlns=\"Settings:\"><UserInformation><Get/></UserInformation></Settings>", "14.1", string.Empty, 85);
			this.probeTrackingObject = new ActiveSyncProbeStateObject(request, base.Result, ProbeState.Settings1);
			this.probeTrackingObject.Result.StateAttribute22 = base.Definition.Endpoint;
			this.probeTrackingObject.TimeoutLimit = DateTime.UtcNow.AddMilliseconds(90000.0);
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

		// Token: 0x06000027 RID: 39 RVA: 0x00003F50 File Offset: 0x00002150
		protected override void ParseResponseSetNextState(ActiveSyncProbeStateObject probeStateObject)
		{
			ProbeResult result = probeStateObject.Result;
			result.StateAttribute21 += "PSMS;";
			ActiveSyncProbeBase.StateResult stateResult = ActiveSyncProbeBase.StateResult.Success;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ActiveSyncTracer, base.TraceContext, "Parsing Settings response.", null, "ParseResponseSetNextState", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ActiveSync\\ActiveSyncCustomerTouchPointProbe.cs", 185);
			string diagnosticsValue = probeStateObject.WebResponses[probeStateObject.LastResponseIndex].DiagnosticsValue;
			if (!base.AcceptableError(diagnosticsValue))
			{
				if (probeStateObject.WebResponses[probeStateObject.LastResponseIndex].HttpStatus == 503)
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
					probeStateObject.WebRequest = ActiveSyncProbeUtil.CreateSettingsCommand(base.Definition.Endpoint, false, false, base.Definition.Account, base.Definition.AccountPassword, "<?xml version=\"1.0\" encoding=\"utf-8\"?><Settings xmlns=\"Settings:\"><UserInformation><Get/></UserInformation></Settings>", "14.1", string.Empty, ActiveSyncProbeUtil.GetCafeBackEndTimeout(this.probeTrackingObject.TimeoutLimit));
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

		// Token: 0x06000028 RID: 40 RVA: 0x000041BC File Offset: 0x000023BC
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
			ICollection<MailboxDatabaseInfo> mailboxDatabaseInfoCollectionForCafe = LocalEndpointManager.Instance.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForCafe;
			Random random = new Random();
			int index = random.Next(mailboxDatabaseInfoCollectionForCafe.Count);
			return mailboxDatabaseInfoCollectionForCafe.ElementAt(index);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00004211 File Offset: 0x00002411
		protected override void HandleSocketError(ActiveSyncProbeStateObject probeStateObject)
		{
			WTFDiagnostics.TraceError(ExTraceGlobals.ActiveSyncTracer, base.TraceContext, "Socket exception considered a failure, should never have SocketException on local machines.", null, "HandleSocketError", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ActiveSync\\ActiveSyncCustomerTouchPointProbe.cs", 311);
			probeStateObject.State = ProbeState.Failure;
		}

		// Token: 0x0400002B RID: 43
		protected const string ProtocolVersion = "14.1";

		// Token: 0x0400002C RID: 44
		protected const string Endpoint = "https://localhost/Microsoft-Server-ActiveSync";

		// Token: 0x0400002D RID: 45
		private const int Timeout = 90000;
	}
}
