using System;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders
{
	// Token: 0x020001CE RID: 462
	public class HeartbeatResponder : ResponderWorkItem
	{
		// Token: 0x06000D14 RID: 3348 RVA: 0x000552DC File Offset: 0x000534DC
		public static ResponderDefinition CreateDefinition(string responderName, string monitorName)
		{
			return new ResponderDefinition
			{
				AssemblyPath = HeartbeatResponder.AssemblyPath,
				TypeName = HeartbeatResponder.TypeName,
				ServiceName = ExchangeComponent.Monitoring.Name,
				RecurrenceIntervalSeconds = 0,
				TimeoutSeconds = 30,
				WaitIntervalSeconds = 1,
				Enabled = true,
				Name = responderName,
				AlertMask = monitorName,
				AlertTypeId = monitorName
			};
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00055348 File Offset: 0x00053548
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.HeartbeatTracer, base.TraceContext, "Heartbeat responder successfully executed.", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\HeartbeatResponder.cs", 70);
		}

		// Token: 0x040009A4 RID: 2468
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040009A5 RID: 2469
		private static readonly string TypeName = typeof(HeartbeatResponder).FullName;
	}
}
