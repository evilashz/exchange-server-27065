using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveSync.Probes
{
	// Token: 0x02000009 RID: 9
	public class ActiveSyncAppPoolPingProbe : ActiveSyncProbeBase
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000037B0 File Offset: 0x000019B0
		public static ProbeDefinition CreateDefinition(string assemblyPath)
		{
			return new ProbeDefinition
			{
				AssemblyPath = assemblyPath,
				TypeName = typeof(ActiveSyncAppPoolPingProbe).FullName,
				Name = "ActiveSyncSelfTestProbe",
				ServiceName = ExchangeComponent.ActiveSyncProtocol.Name,
				TargetResource = "MSExchangeSyncAppPool",
				RecurrenceIntervalSeconds = 60,
				TimeoutSeconds = 58,
				MaxRetryAttempts = 3,
				Endpoint = "https://localhost:444/Microsoft-Server-ActiveSync/exhealth.check"
			};
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003828 File Offset: 0x00001A28
		public override void PopulateDefinition<Definition>(Definition definition, Dictionary<string, string> propertyBag)
		{
			ProbeDefinition probeDefinition = definition as ProbeDefinition;
			if (probeDefinition == null)
			{
				throw new ArgumentException("definition must be a ProbeDefinition");
			}
			probeDefinition.Endpoint = "https://localhost:444/Microsoft-Server-ActiveSync/exhealth.check";
			probeDefinition.Attributes["InvokeNowExecution"] = true.ToString();
			if (propertyBag.ContainsKey("Endpoint"))
			{
				probeDefinition.Endpoint = propertyBag["Endpoint"];
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003894 File Offset: 0x00001A94
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return new List<PropertyInformation>();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000038A8 File Offset: 0x00001AA8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			base.Result.StateAttribute21 = "PDWS;";
			WTFDiagnostics.TraceDebug(ExTraceGlobals.ActiveSyncTracer, base.TraceContext, "Entering ActiveSyncSelfTestProbe DoWork().", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ActiveSync\\ActiveSyncAppPoolPingProbe.cs", 107);
			this.latencyMeasurementStart = DateTime.UtcNow;
			base.TrustAllCerts();
			HttpWebRequest request = ActiveSyncProbeUtil.CreateEmptyGetCommand(base.Definition.Endpoint);
			this.probeTrackingObject = new ActiveSyncProbeStateObject(request, base.Result, ProbeState.Get1);
			this.probeTrackingObject.Result.StateAttribute22 = base.Definition.Endpoint;
			this.isInvokeNowExecution = false;
			if (base.Definition.Attributes.ContainsKey("InvokeNowExecution"))
			{
				bool.TryParse(base.Definition.Attributes["InvokeNowExecution"], out this.isInvokeNowExecution);
			}
			this.probeTrackingObject.TimeoutLimit = DateTime.UtcNow.AddMilliseconds(55000.0);
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

		// Token: 0x06000020 RID: 32 RVA: 0x00003A38 File Offset: 0x00001C38
		protected override void ParseResponseSetNextState(ActiveSyncProbeStateObject probeStateObject)
		{
			ProbeResult result = probeStateObject.Result;
			result.StateAttribute21 += "PSMS;";
			probeStateObject.State = ProbeState.Finish;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.ActiveSyncTracer, base.TraceContext, "Parsing Get response.", null, "ParseResponseSetNextState", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ActiveSync\\ActiveSyncAppPoolPingProbe.cs", 150);
			if (probeStateObject.WebResponses[probeStateObject.LastResponseIndex].HttpStatus != 200)
			{
				probeStateObject.State = ProbeState.Failure;
				ProbeResult result2 = probeStateObject.Result;
				object stateAttribute = result2.StateAttribute21;
				result2.StateAttribute21 = string.Concat(new object[]
				{
					stateAttribute,
					"PSME:",
					DateTime.UtcNow.TimeOfDay,
					";"
				});
				return;
			}
			ProbeResult result3 = probeStateObject.Result;
			object stateAttribute2 = result3.StateAttribute21;
			result3.StateAttribute21 = string.Concat(new object[]
			{
				stateAttribute2,
				"PSME:",
				DateTime.UtcNow.TimeOfDay,
				";"
			});
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003B4C File Offset: 0x00001D4C
		protected override void HandleSocketError(ActiveSyncProbeStateObject probeStateObject)
		{
			DateTime utcNow = DateTime.UtcNow;
			ProbeResult result = probeStateObject.Result;
			result.StateAttribute21 += "PVIPS;";
			WTFDiagnostics.TraceError(ExTraceGlobals.ActiveSyncTracer, base.TraceContext, "Socket exception considered a failure, should never have SocketException on local machines.", null, "HandleSocketError", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ActiveSync\\ActiveSyncAppPoolPingProbe.cs", 174);
			probeStateObject.State = ProbeState.Failure;
			ProbeResult result2 = probeStateObject.Result;
			object stateAttribute = result2.StateAttribute21;
			result2.StateAttribute21 = string.Concat(new object[]
			{
				stateAttribute,
				"PVIPE:",
				DateTime.UtcNow.TimeOfDay,
				";"
			});
		}

		// Token: 0x04000029 RID: 41
		protected const string Endpoint = "https://localhost:444/Microsoft-Server-ActiveSync/exhealth.check";

		// Token: 0x0400002A RID: 42
		private const int Timeout = 55000;
	}
}
