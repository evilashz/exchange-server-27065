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
	// Token: 0x02000286 RID: 646
	public class ImapProxyTestProbe : PopImapProbeBase
	{
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600123D RID: 4669 RVA: 0x0007C8ED File Offset: 0x0007AAED
		protected override string ProbeComponent
		{
			get
			{
				return "IMAP4";
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600123E RID: 4670 RVA: 0x0007C8F4 File Offset: 0x0007AAF4
		protected override bool ProbeMbxScope
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0007C8F8 File Offset: 0x0007AAF8
		public static ProbeDefinition CreateDefinition(string assemblyPath)
		{
			return new ProbeDefinition
			{
				AssemblyPath = assemblyPath,
				TypeName = typeof(ImapProxyTestProbe).FullName,
				Name = "ImapProxyTestProbe",
				ServiceName = ExchangeComponent.ImapProxy.Name,
				TargetResource = "MSExchangeImap4",
				RecurrenceIntervalSeconds = 60,
				TimeoutSeconds = 55,
				MaxRetryAttempts = 3,
				Endpoint = IPAddress.Loopback.ToString(),
				SecondaryEndpoint = "993"
			};
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x0007C980 File Offset: 0x0007AB80
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
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0007CA18 File Offset: 0x0007AC18
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return new List<PropertyInformation>();
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0007CA2C File Offset: 0x0007AC2C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			PopImapProbeStateObject popImapProbeStateObject = null;
			try
			{
				base.Result.StateAttribute21 = "PDWS;";
				WTFDiagnostics.TraceDebug(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "Entering ImapProxyTestProbe DoWork().", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapProxyTestProbe.cs", 145);
				this.latencyMeasurementStart = DateTime.UtcNow;
				IPAddress ipaddress;
				IPAddress.TryParse(base.Definition.Endpoint, out ipaddress);
				int port;
				int.TryParse(base.Definition.SecondaryEndpoint, out port);
				IPEndPoint targetAddress = new IPEndPoint(ipaddress, port);
				popImapProbeStateObject = PopImapProbeUtil.CreateImapSSLStateObject(targetAddress, base.Result);
				base.HandleConnectionExceptions(popImapProbeStateObject);
				PopImapProbeUtil.CreateImapCapabilities(popImapProbeStateObject);
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

		// Token: 0x06001243 RID: 4675 RVA: 0x0007CBD0 File Offset: 0x0007ADD0
		protected override void ParseResponseSetNextState(PopImapProbeStateObject probeTrackingObject)
		{
			ProbeResult result = probeTrackingObject.Result;
			result.StateAttribute21 += "PSMS;";
			PopImapProbeBase.StateResult stateResult = PopImapProbeBase.StateResult.Success;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.IMAP4Tracer, base.TraceContext, "Parsing response.", null, "ParseResponseSetNextState", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\PopImap\\Imap4\\ImapProxyTestProbe.cs", 197);
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

		// Token: 0x06001244 RID: 4676 RVA: 0x0007CCF4 File Offset: 0x0007AEF4
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

		// Token: 0x04000DC8 RID: 3528
		protected const string Port = "993";

		// Token: 0x04000DC9 RID: 3529
		private const int Timeout = 55;

		// Token: 0x04000DCA RID: 3530
		private const int Recurrence = 60;
	}
}
