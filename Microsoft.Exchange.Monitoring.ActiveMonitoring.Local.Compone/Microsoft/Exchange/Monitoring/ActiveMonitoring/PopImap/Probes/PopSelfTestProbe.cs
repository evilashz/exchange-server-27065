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
	// Token: 0x0200028F RID: 655
	public class PopSelfTestProbe : PopImapProbeBase
	{
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x0007FEED File Offset: 0x0007E0ED
		protected override string ProbeComponent
		{
			get
			{
				return "POP3";
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06001289 RID: 4745 RVA: 0x0007FEF4 File Offset: 0x0007E0F4
		protected override bool ProbeMbxScope
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x0007FEF8 File Offset: 0x0007E0F8
		public static ProbeDefinition CreateDefinition(string assemblyPath)
		{
			return new ProbeDefinition
			{
				AssemblyPath = assemblyPath,
				TypeName = typeof(PopSelfTestProbe).FullName,
				Name = "PopSelfTestProbe",
				ServiceName = ExchangeComponent.PopProtocol.Name,
				TargetResource = "MSExchangePop3BE",
				RecurrenceIntervalSeconds = 60,
				TimeoutSeconds = 55,
				MaxRetryAttempts = 3,
				Endpoint = IPAddress.Loopback.ToString(),
				SecondaryEndpoint = "1995"
			};
		}

		// Token: 0x0600128B RID: 4747 RVA: 0x0007FF80 File Offset: 0x0007E180
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
		}

		// Token: 0x0600128C RID: 4748 RVA: 0x00080018 File Offset: 0x0007E218
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return new List<PropertyInformation>();
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x0008002C File Offset: 0x0007E22C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			PopImapProbeStateObject popImapProbeStateObject = null;
			try
			{
				base.Result.StateAttribute21 = "PDWS;";
				WTFDiagnostics.TraceDebug(ExTraceGlobals.POP3Tracer, base.TraceContext, "Entering PopSelfTestProbe DoWork().", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopSelfTestProbe.cs", 145);
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

		// Token: 0x0600128E RID: 4750 RVA: 0x000801D0 File Offset: 0x0007E3D0
		protected override void ParseResponseSetNextState(PopImapProbeStateObject probeTrackingObject)
		{
			ProbeResult result = probeTrackingObject.Result;
			result.StateAttribute21 += "PSMS;";
			PopImapProbeBase.StateResult stateResult = PopImapProbeBase.StateResult.Success;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.POP3Tracer, base.TraceContext, "Parsing response.", null, "ParseResponseSetNextState", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Pop3\\PopSelfTestProbe.cs", 197);
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

		// Token: 0x0600128F RID: 4751 RVA: 0x000802F4 File Offset: 0x0007E4F4
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

		// Token: 0x04000DEE RID: 3566
		protected const string Port = "1995";

		// Token: 0x04000DEF RID: 3567
		private const int Timeout = 55;

		// Token: 0x04000DF0 RID: 3568
		private const int Recurrence = 60;
	}
}
