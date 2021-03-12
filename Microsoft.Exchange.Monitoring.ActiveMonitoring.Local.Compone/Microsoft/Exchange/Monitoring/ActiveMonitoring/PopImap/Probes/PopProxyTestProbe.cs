using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PopImap.Probes
{
	// Token: 0x0200028E RID: 654
	public class PopProxyTestProbe : PopImapProbeBase
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x0007FA0D File Offset: 0x0007DC0D
		protected override string ProbeComponent
		{
			get
			{
				return "POP3";
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x0007FA14 File Offset: 0x0007DC14
		protected override bool ProbeMbxScope
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x0007FA18 File Offset: 0x0007DC18
		public static ProbeDefinition CreateDefinition(string assemblyPath)
		{
			return new ProbeDefinition
			{
				AssemblyPath = assemblyPath,
				TypeName = typeof(PopProxyTestProbe).FullName,
				Name = "PopProxyTestProbe",
				ServiceName = ExchangeComponent.PopProxy.Name,
				TargetResource = "MSExchangePop3",
				RecurrenceIntervalSeconds = 60,
				TimeoutSeconds = 55,
				MaxRetryAttempts = 3,
				Endpoint = IPAddress.Loopback.ToString(),
				SecondaryEndpoint = "995"
			};
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x0007FAA0 File Offset: 0x0007DCA0
		public override void PopulateDefinition<Definition>(Definition definition, Dictionary<string, string> propertyBag)
		{
			ProbeDefinition probeDefinition = definition as ProbeDefinition;
			if (probeDefinition == null)
			{
				throw new ArgumentException("definition must be a ProbeDefinition");
			}
			probeDefinition.Endpoint = IPAddress.Loopback.ToString();
			probeDefinition.SecondaryEndpoint = "995";
			probeDefinition.Attributes["InvokeNowExecution"] = true.ToString();
			if (propertyBag.ContainsKey("Endpoint"))
			{
				probeDefinition.Endpoint = propertyBag["Endpoint"];
			}
			if (propertyBag.ContainsKey("SecondaryEndpoint"))
			{
				probeDefinition.SecondaryEndpoint = propertyBag["SecondaryEndpoint"];
			}
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x0007FB38 File Offset: 0x0007DD38
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return new List<PropertyInformation>();
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x0007FB4C File Offset: 0x0007DD4C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			PopImapProbeStateObject popImapProbeStateObject = null;
			try
			{
				base.Result.StateAttribute21 = "PDWS;";
				WTFDiagnostics.TraceDebug(ExTraceGlobals.POP3Tracer, base.TraceContext, "Entering PopProxyTestProbe DoWork().", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopProxyTestProbe.cs", 145);
				this.latencyMeasurementStart = DateTime.UtcNow;
				IPAddress ipaddress;
				IPAddress.TryParse(base.Definition.Endpoint, out ipaddress);
				int port;
				int.TryParse(base.Definition.SecondaryEndpoint, out port);
				IPEndPoint targetAddress = new IPEndPoint(ipaddress, port);
				popImapProbeStateObject = PopImapProbeUtil.CreatePopSSLStateObject(targetAddress, base.Result);
				base.HandleConnectionExceptions(popImapProbeStateObject);
				PopImapProbeUtil.CreatePopCapabilities(popImapProbeStateObject);
				popImapProbeStateObject.Result.StateAttribute22 = ipaddress.ToString();
				this.isInvokeNowExecution = false;
				popImapProbeStateObject.TimeoutLimit = DateTime.UtcNow.AddSeconds(55.0);
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

		// Token: 0x06001285 RID: 4741 RVA: 0x0007FCF0 File Offset: 0x0007DEF0
		protected override void ParseResponseSetNextState(PopImapProbeStateObject probeTrackingObject)
		{
			ProbeResult result = probeTrackingObject.Result;
			result.StateAttribute21 += "PSMS;";
			PopImapProbeBase.StateResult stateResult = PopImapProbeBase.StateResult.Success;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.POP3Tracer, base.TraceContext, "Parsing response.", null, "ParseResponseSetNextState", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopProxyTestProbe.cs", 197);
			if (probeTrackingObject.TcpResponses[probeTrackingObject.LastResponseIndex].ResponseType != ResponseType.success && !base.AcceptableError(probeTrackingObject))
			{
				stateResult = PopImapProbeBase.StateResult.Fail;
			}
			ProbeState state = probeTrackingObject.State;
			if (state == ProbeState.Capability1)
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
				ProbeResult result2 = probeTrackingObject.Result;
				result2.StateAttribute13 += "C1";
			}
			else
			{
				probeTrackingObject.State = ProbeState.Failure;
				ProbeResult result3 = probeTrackingObject.Result;
				result3.StateAttribute13 += "F";
			}
			ProbeResult result4 = probeTrackingObject.Result;
			object stateAttribute = result4.StateAttribute21;
			result4.StateAttribute21 = string.Concat(new object[]
			{
				stateAttribute,
				"PSME:",
				DateTime.UtcNow.TimeOfDay,
				";"
			});
		}

		// Token: 0x06001286 RID: 4742 RVA: 0x0007FE14 File Offset: 0x0007E014
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

		// Token: 0x04000DEB RID: 3563
		protected const string Port = "995";

		// Token: 0x04000DEC RID: 3564
		private const int Timeout = 55;

		// Token: 0x04000DED RID: 3565
		private const int Recurrence = 60;
	}
}
