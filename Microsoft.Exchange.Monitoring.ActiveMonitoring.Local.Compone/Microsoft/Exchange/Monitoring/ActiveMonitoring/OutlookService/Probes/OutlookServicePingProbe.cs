using System;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Office.Outlook.V1.Mail;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OutlookService.Probes
{
	// Token: 0x0200025F RID: 607
	public class OutlookServicePingProbe : OutlookServiceSocketProbeBase
	{
		// Token: 0x0600111A RID: 4378 RVA: 0x00071FAE File Offset: 0x000701AE
		public OutlookServicePingProbe()
		{
			base.Type = 0;
			base.Timeout = TimeSpan.FromSeconds(50.0);
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x00071FD4 File Offset: 0x000701D4
		protected override void ExecuteRequest(SocketClient client)
		{
			client.Ping(new PingRequest(), base.Timeout);
			base.Result.StateAttribute13 = client.ExtraInfo;
			if (client.ExecutionSuccess)
			{
				base.Result.ResultType = ResultType.Succeeded;
				return;
			}
			base.Result.ResultType = ResultType.Failed;
			throw new Exception(string.Format("PingProbe execution failed, Details : {0}", client.ExtraInfo));
		}
	}
}
