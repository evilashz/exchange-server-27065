using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PopImap.Probes
{
	// Token: 0x02000285 RID: 645
	public class ImapDeepTestProbe : PopImapProbeBase
	{
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0007C279 File Offset: 0x0007A479
		protected override string ProbeComponent
		{
			get
			{
				return "IMAP4";
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x0007C280 File Offset: 0x0007A480
		protected override bool ProbeMbxScope
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0007C284 File Offset: 0x0007A484
		public static ProbeDefinition CreateDefinition(string assemblyPath)
		{
			return new ProbeDefinition
			{
				AssemblyPath = assemblyPath,
				TypeName = typeof(ImapDeepTestProbe).FullName,
				Name = "ImapDeepTestProbe",
				ServiceName = ExchangeComponent.ImapProtocol.Name,
				TargetResource = "MSExchangeImap4BE",
				RecurrenceIntervalSeconds = 180,
				TimeoutSeconds = 110,
				MaxRetryAttempts = 3,
				Endpoint = IPAddress.Loopback.ToString(),
				SecondaryEndpoint = "1993"
			};
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0007C310 File Offset: 0x0007A510
		public override void PopulateDefinition<Definition>(Definition definition, Dictionary<string, string> propertyBag)
		{
			ProbeDefinition probeDefinition = definition as ProbeDefinition;
			if (probeDefinition == null)
			{
				throw new ArgumentException("definition must be a ProbeDefinition");
			}
			probeDefinition.Endpoint = IPAddress.Loopback.ToString();
			probeDefinition.SecondaryEndpoint = "1993";
			probeDefinition.Attributes["InvokeNowExecution"] = true.ToString();
			if (propertyBag.ContainsKey("Endpoint"))
			{
				probeDefinition.Endpoint = propertyBag["Endpoint"];
			}
			if (propertyBag.ContainsKey("SecondaryEndpoint"))
			{
				probeDefinition.SecondaryEndpoint = propertyBag["SecondaryEndpoint"];
			}
			MailboxDatabaseInfo monitoringAccount = this.GetMonitoringAccount();
			if (monitoringAccount == null)
			{
				throw new ApplicationException("No monitoring account could be found for this server.");
			}
			probeDefinition.Account = monitoringAccount.MonitoringAccount + "@" + monitoringAccount.MonitoringAccountDomain;
			probeDefinition.AccountPassword = monitoringAccount.MonitoringAccountPassword;
			if (propertyBag.ContainsKey("Account"))
			{
				probeDefinition.Account = propertyBag["Account"];
			}
			if (propertyBag.ContainsKey("Password"))
			{
				probeDefinition.AccountPassword = propertyBag["Password"];
			}
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x0007C420 File Offset: 0x0007A620
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return new List<PropertyInformation>();
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0007C434 File Offset: 0x0007A634
		protected override void DoWork(CancellationToken cancellationToken)
		{
			PopImapProbeStateObject popImapProbeStateObject = null;
			try
			{
				base.Result.StateAttribute21 = "PDWS;";
				WTFDiagnostics.TraceDebug(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "Entering ImapDeepTestProbe DoWork().", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDeepTestProbe.cs", 168);
				this.latencyMeasurementStart = DateTime.UtcNow;
				IPAddress ipaddress;
				IPAddress.TryParse(base.Definition.Endpoint, out ipaddress);
				int port;
				int.TryParse(base.Definition.SecondaryEndpoint, out port);
				IPEndPoint targetAddress = new IPEndPoint(ipaddress, port);
				popImapProbeStateObject = PopImapProbeUtil.CreateImapSSLStateObject(targetAddress, base.Result);
				base.HandleConnectionExceptions(popImapProbeStateObject);
				if (string.IsNullOrEmpty(base.Definition.Account))
				{
					MailboxDatabaseInfo monitoringAccount = this.GetMonitoringAccount();
					popImapProbeStateObject.UserAccount = monitoringAccount.MonitoringAccount + "@" + monitoringAccount.MonitoringAccountDomain;
					popImapProbeStateObject.UserPassword = monitoringAccount.MonitoringAccountPassword;
				}
				else
				{
					popImapProbeStateObject.UserAccount = base.Definition.Account;
					popImapProbeStateObject.UserPassword = base.Definition.AccountPassword;
				}
				PopImapProbeUtil.CreateImapCapabilities(popImapProbeStateObject);
				popImapProbeStateObject.Result.StateAttribute22 = ipaddress.ToString();
				this.isInvokeNowExecution = false;
				popImapProbeStateObject.TimeoutLimit = DateTime.UtcNow.AddSeconds(110.0);
				base.Result.StateAttribute21 = string.Concat(new object[]
				{
					"MaxTimeout:",
					popImapProbeStateObject.TimeoutLimit,
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
				base.DoWork(popImapProbeStateObject, cancellationToken);
			}
			finally
			{
				if (popImapProbeStateObject.Connection != null)
				{
					popImapProbeStateObject.Connection.Dispose();
				}
			}
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0007C640 File Offset: 0x0007A840
		protected override void ParseResponseSetNextState(PopImapProbeStateObject probeTrackingObject)
		{
			ProbeResult result = probeTrackingObject.Result;
			result.StateAttribute21 += "PSMS;";
			PopImapProbeBase.StateResult stateResult = PopImapProbeBase.StateResult.Success;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "Parsing response.", null, "ParseResponseSetNextState", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapDeepTestProbe.cs", 235);
			if (probeTrackingObject.TcpResponses[probeTrackingObject.LastResponseIndex].ResponseType != ResponseType.success && !base.AcceptableError(probeTrackingObject))
			{
				stateResult = PopImapProbeBase.StateResult.Fail;
			}
			ProbeState state = probeTrackingObject.State;
			if (state != ProbeState.Capability1)
			{
				if (state != ProbeState.Login1)
				{
					probeTrackingObject.State = ProbeState.Failure;
					ProbeResult result2 = probeTrackingObject.Result;
					result2.StateAttribute13 += "F";
				}
				else
				{
					PopImapProbeBase.StateResult stateResult2 = stateResult;
					if (stateResult2 == PopImapProbeBase.StateResult.Success)
					{
						probeTrackingObject.State = ProbeState.Finish;
					}
					else
					{
						probeTrackingObject.State = ProbeState.Failure;
					}
					ProbeResult result3 = probeTrackingObject.Result;
					result3.StateAttribute13 += "L1";
				}
			}
			else
			{
				PopImapProbeBase.StateResult stateResult3 = stateResult;
				if (stateResult3 == PopImapProbeBase.StateResult.Success)
				{
					probeTrackingObject.State = ProbeState.Login1;
					PopImapProbeUtil.CreateImapLogin(probeTrackingObject);
				}
				else
				{
					probeTrackingObject.State = ProbeState.Failure;
				}
				ProbeResult result4 = probeTrackingObject.Result;
				result4.StateAttribute13 += "C1";
			}
			ProbeResult result5 = probeTrackingObject.Result;
			object stateAttribute = result5.StateAttribute21;
			result5.StateAttribute21 = string.Concat(new object[]
			{
				stateAttribute,
				"PSME:",
				DateTime.UtcNow.TimeOfDay,
				";"
			});
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x0007C7A8 File Offset: 0x0007A9A8
		protected override void HandleSocketError(Exception e)
		{
			DateTime utcNow = DateTime.UtcNow;
			ProbeResult result = base.Result;
			result.StateAttribute21 += "PHSS;";
			base.Result.FailureCategory = 9;
			base.Result.StateAttribute1 = this.ProbeComponent;
			base.Result.StateAttribute2 = "Infrastructure Failure";
			ProbeResult result2 = base.Result;
			result2.StateAttribute13 += ":FAIL";
			ProbeResult result3 = base.Result;
			object stateAttribute = result3.StateAttribute21;
			result3.StateAttribute21 = string.Concat(new object[]
			{
				stateAttribute,
				"PHSE:",
				(int)(DateTime.UtcNow - utcNow).TotalMilliseconds,
				";"
			});
			throw new InvalidOperationException("Unable to initialise TCP Network connection", e);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0007C87C File Offset: 0x0007AA7C
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
			if (mailboxDatabaseInfoCollectionForBackend.Count <= 0)
			{
				throw new InvalidOperationException("No mailboxes were found to use in the DeepTest probe.");
			}
			Random random = new Random();
			int index = random.Next(mailboxDatabaseInfoCollectionForBackend.Count);
			return mailboxDatabaseInfoCollectionForBackend.ElementAt(index);
		}

		// Token: 0x04000DC5 RID: 3525
		protected const string Port = "1993";

		// Token: 0x04000DC6 RID: 3526
		private const int Timeout = 110;

		// Token: 0x04000DC7 RID: 3527
		private const int Recurrence = 180;
	}
}
