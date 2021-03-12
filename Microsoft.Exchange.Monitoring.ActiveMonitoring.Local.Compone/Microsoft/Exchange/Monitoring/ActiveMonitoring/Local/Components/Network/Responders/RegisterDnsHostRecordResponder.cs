using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components.Network.Responders
{
	// Token: 0x02000235 RID: 565
	public class RegisterDnsHostRecordResponder : ResponderWorkItem
	{
		// Token: 0x06000FCA RID: 4042 RVA: 0x00069B30 File Offset: 0x00067D30
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				FileName = Environment.SystemDirectory + "\\ipconfig.exe",
				Arguments = "/registerdns"
			};
			using (Process process = Process.Start(startInfo))
			{
				process.WaitForExit(30000);
			}
		}
	}
}
