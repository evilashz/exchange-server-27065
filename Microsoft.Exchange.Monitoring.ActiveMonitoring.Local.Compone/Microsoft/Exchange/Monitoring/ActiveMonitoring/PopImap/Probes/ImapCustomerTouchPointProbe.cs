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
	// Token: 0x02000284 RID: 644
	public class ImapCustomerTouchPointProbe : PopImapProbeBase
	{
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x0007BC04 File Offset: 0x00079E04
		protected override string ProbeComponent
		{
			get
			{
				return "IMAP4";
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x0007BC0B File Offset: 0x00079E0B
		protected override bool ProbeMbxScope
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0007BC10 File Offset: 0x00079E10
		public static ProbeDefinition CreateDefinition(string assemblyPath)
		{
			return new ProbeDefinition
			{
				AssemblyPath = assemblyPath,
				TypeName = typeof(ImapCustomerTouchPointProbe).FullName,
				Name = "ImapCTPProbe",
				ServiceName = ExchangeComponent.Imap.Name,
				TargetResource = "MSExchangeImap4",
				RecurrenceIntervalSeconds = 240,
				TimeoutSeconds = 110,
				MaxRetryAttempts = 3,
				Endpoint = IPAddress.Loopback.ToString(),
				SecondaryEndpoint = "993"
			};
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0007BC9C File Offset: 0x00079E9C
		public override void PopulateDefinition<Definition>(Definition definition, Dictionary<string, string> propertyBag)
		{
			ProbeDefinition probeDefinition = definition as ProbeDefinition;
			if (probeDefinition == null)
			{
				throw new ArgumentException("definition must be a ProbeDefinition");
			}
			probeDefinition.Endpoint = IPAddress.Loopback.ToString();
			probeDefinition.SecondaryEndpoint = "993";
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

		// Token: 0x0600122D RID: 4653 RVA: 0x0007BDAC File Offset: 0x00079FAC
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return new List<PropertyInformation>();
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x0007BDC0 File Offset: 0x00079FC0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			PopImapProbeStateObject popImapProbeStateObject = null;
			try
			{
				base.Result.StateAttribute21 = "PDWS;";
				WTFDiagnostics.TraceDebug(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "Entering ImapCustomerTouchPointProbe DoWork().", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapCustomerTouchPointProbe.cs", 168);
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

		// Token: 0x0600122F RID: 4655 RVA: 0x0007BFCC File Offset: 0x0007A1CC
		protected override void ParseResponseSetNextState(PopImapProbeStateObject probeTrackingObject)
		{
			ProbeResult result = probeTrackingObject.Result;
			result.StateAttribute21 += "PSMS;";
			PopImapProbeBase.StateResult stateResult = PopImapProbeBase.StateResult.Success;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "Parsing response.", null, "ParseResponseSetNextState", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapCustomerTouchPointProbe.cs", 235);
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

		// Token: 0x06001230 RID: 4656 RVA: 0x0007C134 File Offset: 0x0007A334
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

		// Token: 0x06001231 RID: 4657 RVA: 0x0007C208 File Offset: 0x0007A408
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
			if (mailboxDatabaseInfoCollectionForCafe.Count <= 0)
			{
				throw new InvalidOperationException("No mailboxes were found to use in the CTP probe.");
			}
			Random random = new Random();
			int index = random.Next(mailboxDatabaseInfoCollectionForCafe.Count);
			return mailboxDatabaseInfoCollectionForCafe.ElementAt(index);
		}

		// Token: 0x04000DC2 RID: 3522
		protected const string Port = "993";

		// Token: 0x04000DC3 RID: 3523
		private const int Timeout = 110;

		// Token: 0x04000DC4 RID: 3524
		private const int Recurrence = 240;
	}
}
