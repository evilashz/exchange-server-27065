using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components.Network.Probes
{
	// Token: 0x0200022C RID: 556
	public class DnsServiceProbe : ProbeWorkItem
	{
		// Token: 0x06000F9C RID: 3996 RVA: 0x00067BC8 File Offset: 0x00065DC8
		protected override void DoWork(CancellationToken cancellationToken)
		{
			using (ServiceController serviceController = new ServiceController("DNS"))
			{
				if (serviceController.Status == ServiceControllerStatus.StartPending)
				{
					serviceController.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(15.0));
					serviceController.Refresh();
				}
				if (serviceController.Status != ServiceControllerStatus.Running)
				{
					this.LogMessageAndThrow("The '{0}' service is not running; its current status is '{1}'.", new object[]
					{
						"DNS",
						serviceController.Status
					});
				}
			}
			this.LogMessage("The '{0}' service is running.", new object[]
			{
				"DNS"
			});
			cancellationToken.ThrowIfCancellationRequested();
			string text = DnsServiceProbe.AttemptDnsQuery();
			if (text != null)
			{
				this.LogMessageAndThrow("The '{0}' service is running but it did not respond successfully to a trivial nslookup query: {1}", new object[]
				{
					"DNS",
					text
				});
			}
			this.LogMessage("The '{0}' service successfully responded to a trivial nslookup query.", new object[]
			{
				"DNS"
			});
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x00067CC0 File Offset: 0x00065EC0
		private static string AttemptDnsQuery()
		{
			string text = null;
			try
			{
				using (Process process = Process.Start(new ProcessStartInfo
				{
					FileName = Environment.SystemDirectory + "\\nslookup.exe",
					Arguments = "127.0.0.1 127.0.0.1",
					UseShellExecute = false,
					RedirectStandardError = true,
					RedirectStandardOutput = true
				}))
				{
					if (!process.WaitForExit(1000))
					{
						process.Kill();
						text = "nslookup did not exit within 1 second.";
					}
					text = process.StandardError.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				text = ex.Message;
			}
			if (!string.IsNullOrEmpty(text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x00067D78 File Offset: 0x00065F78
		private void LogMessage(string message, params object[] formatArgs)
		{
			if (formatArgs != null && formatArgs.Length > 0)
			{
				message = string.Format(message, formatArgs);
			}
			NetworkUtils.LogWorkItemMessage(base.TraceContext, base.Result, message, new object[0]);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00067DA4 File Offset: 0x00065FA4
		private void LogMessageAndThrow(string message, params object[] formatArgs)
		{
			if (formatArgs != null && formatArgs.Length > 0)
			{
				message = string.Format(message, formatArgs);
			}
			NetworkUtils.LogWorkItemMessage(base.TraceContext, base.Result, message, new object[0]);
			throw new Exception(message);
		}

		// Token: 0x04000BC5 RID: 3013
		public const string DnsServiceName = "DNS";
	}
}
