using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x020001FE RID: 510
	public sealed class RpsDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000F89 RID: 3977 RVA: 0x00028B88 File Offset: 0x00026D88
		protected override void DoWork(CancellationToken cancellationToken)
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			try
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.RPSTracer, base.TraceContext, "[FFO RPS.DoWork]: Discovery Started.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\RPS\\RpsDiscovery.cs", 41);
				GenericWorkItemHelper.CreateAllDefinitions(new List<string>
				{
					"FFORpsDiscovery.xml"
				}, base.Broker, base.TraceContext, base.Result);
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceError<string>(ExTraceGlobals.RPSTracer, base.TraceContext, "[FFO RPS.DoWork]: Exception occurred during discovery. {0}", ex.ToString(), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\RPS\\RpsDiscovery.cs", 57);
				throw;
			}
		}

		// Token: 0x04000769 RID: 1897
		private const string ServiceName = "RPS";
	}
}
