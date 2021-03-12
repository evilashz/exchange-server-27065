using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000053 RID: 83
	public class TestActiveMonitoringProbe : ProbeWorkItem
	{
		// Token: 0x06000567 RID: 1383 RVA: 0x00016274 File Offset: 0x00014474
		public static ProbeDefinition CreateDefinition(string probeName)
		{
			return new ProbeDefinition
			{
				AssemblyPath = TestActiveMonitoringProbe.AssemblyPath,
				TypeName = TestActiveMonitoringProbe.TypeName,
				ServiceName = ExchangeComponent.Monitoring.Name,
				RecurrenceIntervalSeconds = 300,
				TimeoutSeconds = 30,
				MaxRetryAttempts = 0,
				Enabled = true,
				Name = probeName
			};
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x000162D6 File Offset: 0x000144D6
		public override void PopulateDefinition<ProbeDefinition>(ProbeDefinition definition, Dictionary<string, string> propertyBag)
		{
			definition.TargetResource = propertyBag["TargetResource"];
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x000162F0 File Offset: 0x000144F0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.HeartbeatTracer, base.TraceContext, "TestActiveMonitoringProbe.DoWork: TestActiveMonitoring Probe is logging a message", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\ActiveMonitoring\\WorkItems\\Probes\\TestActiveMonitoringProbe.cs", 79);
			if (!Settings.IsCortex)
			{
				throw new Exception("Failed by design.");
			}
			WebClient webClient = new WebClient();
			webClient.DownloadData("http://ipv6.google.com");
		}

		// Token: 0x040003B3 RID: 947
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040003B4 RID: 948
		private static readonly string TypeName = typeof(TestActiveMonitoringProbe).FullName;
	}
}
