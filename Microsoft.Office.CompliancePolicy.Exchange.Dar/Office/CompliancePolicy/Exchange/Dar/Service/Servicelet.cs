using System;
using Microsoft.Exchange.ServiceHost;
using Microsoft.Office.CompliancePolicy.Exchange.Dar.Diagnostics;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Office.CompliancePolicy.Exchange.Dar.Service
{
	// Token: 0x02000011 RID: 17
	internal class Servicelet : Servicelet
	{
		// Token: 0x06000096 RID: 150 RVA: 0x000042F4 File Offset: 0x000024F4
		public override void Work()
		{
			bool isStarted = false;
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						LogItem.Publish("Runtime", "StartingRuntime");
						if (!HostRpcServer.Start())
						{
							LogItem.Publish("Runtime", "RuntimeStartupFailed", "Failed to start DAR Runtime", ResultSeverityLevel.Critical);
						}
						else
						{
							isStarted = true;
							LogItem.Publish("Runtime", "StartedRuntime");
							this.StopEvent.WaitOne();
						}
					}
					finally
					{
						if (isStarted)
						{
							LogItem.Publish("Runtime", "StoppingRuntime");
							HostRpcServer.Stop();
						}
					}
				});
			}
			catch (GrayException ex)
			{
				LogItem.Publish("Runtime", "RuntimeStartupFailed", "Critical error starting runtime: " + ex.ToString(), ResultSeverityLevel.Critical);
			}
		}
	}
}
