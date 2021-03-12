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
	// Token: 0x0200028D RID: 653
	public class PopDeepTestProbe : PopImapProbeBase
	{
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001275 RID: 4725 RVA: 0x0007F349 File Offset: 0x0007D549
		protected override string ProbeComponent
		{
			get
			{
				return "POP3";
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x0007F350 File Offset: 0x0007D550
		protected override bool ProbeMbxScope
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0007F354 File Offset: 0x0007D554
		public static ProbeDefinition CreateDefinition(string assemblyPath)
		{
			return new ProbeDefinition
			{
				AssemblyPath = assemblyPath,
				TypeName = typeof(PopDeepTestProbe).FullName,
				Name = "PopDeepTestProbe",
				ServiceName = ExchangeComponent.PopProtocol.Name,
				TargetResource = "MSExchangePop3BE",
				RecurrenceIntervalSeconds = 180,
				TimeoutSeconds = 110,
				MaxRetryAttempts = 3,
				Endpoint = IPAddress.Loopback.ToString(),
				SecondaryEndpoint = "1995"
			};
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0007F3E0 File Offset: 0x0007D5E0
		public override void PopulateDefinition<Definition>(Definition definition, Dictionary<string, string> propertyBag)
		{
			ProbeDefinition probeDefinition = definition as ProbeDefinition;
			if (probeDefinition == null)
			{
				throw new ArgumentException("definition must be a ProbeDefinition");
			}
			probeDefinition.Endpoint = IPAddress.Loopback.ToString();
			probeDefinition.SecondaryEndpoint = "1995";
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

		// Token: 0x06001279 RID: 4729 RVA: 0x0007F4F0 File Offset: 0x0007D6F0
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return new List<PropertyInformation>();
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x0007F504 File Offset: 0x0007D704
		protected override void DoWork(CancellationToken cancellationToken)
		{
			PopImapProbeStateObject popImapProbeStateObject = null;
			try
			{
				base.Result.StateAttribute21 = "PDWS;";
				WTFDiagnostics.TraceDebug(ExTraceGlobals.POP3Tracer, base.TraceContext, "Entering PopDeepTestProbe DoWork().", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDeepTestProbe.cs", 168);
				this.latencyMeasurementStart = DateTime.UtcNow;
				IPAddress ipaddress;
				IPAddress.TryParse(base.Definition.Endpoint, out ipaddress);
				int port;
				int.TryParse(base.Definition.SecondaryEndpoint, out port);
				IPEndPoint targetAddress = new IPEndPoint(ipaddress, port);
				popImapProbeStateObject = PopImapProbeUtil.CreatePopSSLStateObject(targetAddress, base.Result);
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
				PopImapProbeUtil.CreatePopCapabilities(popImapProbeStateObject);
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

		// Token: 0x0600127B RID: 4731 RVA: 0x0007F710 File Offset: 0x0007D910
		protected override void ParseResponseSetNextState(PopImapProbeStateObject probeTrackingObject)
		{
			ProbeResult result = probeTrackingObject.Result;
			result.StateAttribute21 += "PSMS;";
			PopImapProbeBase.StateResult stateResult = PopImapProbeBase.StateResult.Success;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.POP3Tracer, base.TraceContext, "Parsing response.", null, "ParseResponseSetNextState", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopDeepTestProbe.cs", 238);
			if (probeTrackingObject.TcpResponses[probeTrackingObject.LastResponseIndex].ResponseType != ResponseType.success && !base.AcceptableError(probeTrackingObject))
			{
				stateResult = PopImapProbeBase.StateResult.Fail;
			}
			ProbeState state = probeTrackingObject.State;
			if (state != ProbeState.Capability1)
			{
				switch (state)
				{
				case ProbeState.User1:
				{
					PopImapProbeBase.StateResult stateResult2 = stateResult;
					if (stateResult2 == PopImapProbeBase.StateResult.Success)
					{
						probeTrackingObject.State = ProbeState.Pass1;
						PopImapProbeUtil.CreatePopPassCommand(probeTrackingObject);
					}
					else
					{
						probeTrackingObject.State = ProbeState.Failure;
					}
					ProbeResult result2 = probeTrackingObject.Result;
					result2.StateAttribute13 += "U1";
					goto IL_15A;
				}
				case ProbeState.Pass1:
				{
					PopImapProbeBase.StateResult stateResult3 = stateResult;
					if (stateResult3 == PopImapProbeBase.StateResult.Success)
					{
						probeTrackingObject.State = ProbeState.Finish;
					}
					else
					{
						probeTrackingObject.State = ProbeState.Failure;
					}
					ProbeResult result3 = probeTrackingObject.Result;
					result3.StateAttribute13 += "P1";
					goto IL_15A;
				}
				}
				probeTrackingObject.State = ProbeState.Failure;
				ProbeResult result4 = probeTrackingObject.Result;
				result4.StateAttribute13 += "F";
			}
			else
			{
				PopImapProbeBase.StateResult stateResult4 = stateResult;
				if (stateResult4 == PopImapProbeBase.StateResult.Success)
				{
					probeTrackingObject.State = ProbeState.User1;
					PopImapProbeUtil.CreatePopUserCommand(probeTrackingObject);
				}
				else
				{
					probeTrackingObject.State = ProbeState.Failure;
				}
				ProbeResult result5 = probeTrackingObject.Result;
				result5.StateAttribute13 += "C1";
			}
			IL_15A:
			ProbeResult result6 = probeTrackingObject.Result;
			object stateAttribute = result6.StateAttribute21;
			result6.StateAttribute21 = string.Concat(new object[]
			{
				stateAttribute,
				"PSME:",
				DateTime.UtcNow.TimeOfDay,
				";"
			});
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x0007F8C8 File Offset: 0x0007DAC8
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

		// Token: 0x0600127D RID: 4733 RVA: 0x0007F99C File Offset: 0x0007DB9C
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

		// Token: 0x04000DE8 RID: 3560
		protected const string Port = "1995";

		// Token: 0x04000DE9 RID: 3561
		private const int Timeout = 110;

		// Token: 0x04000DEA RID: 3562
		private const int Recurrence = 180;
	}
}
