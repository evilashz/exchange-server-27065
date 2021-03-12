using System;
using System.Web;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x0200001D RID: 29
	internal static class RPSPerfCounterHelper
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000523C File Offset: 0x0000343C
		public static RemotePowershellPerformanceCountersInstance CurrentPerfCounter
		{
			get
			{
				if (RPSPerfCounterHelper.current == null)
				{
					string text = null;
					try
					{
						text = HttpRuntime.AppDomainAppPath;
					}
					catch (ArgumentNullException)
					{
					}
					string instanceName;
					if (text != null && text.IndexOf("powershell-liveid-proxy", StringComparison.OrdinalIgnoreCase) != -1)
					{
						instanceName = "RemotePS-LiveID";
					}
					else if (text != null && text.IndexOf("powershell-proxy", StringComparison.OrdinalIgnoreCase) != -1)
					{
						instanceName = "RemotePS";
					}
					else
					{
						instanceName = "RemotePS-NoPro";
					}
					RPSPerfCounterHelper.current = RemotePowershellPerformanceCounters.GetInstance(instanceName);
				}
				return RPSPerfCounterHelper.current;
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000052BC File Offset: 0x000034BC
		public static RemotePowershellPerformanceCountersInstance GetPerfCounterForAuthZ(string vdirName, int port)
		{
			string instanceName = "RemotePS-NoPro";
			if (vdirName != null && vdirName.Equals("/powershell", StringComparison.InvariantCultureIgnoreCase) && port == 444)
			{
				instanceName = "RemotePS";
			}
			else if (vdirName != null && vdirName.Equals("/powershell-liveid", StringComparison.InvariantCultureIgnoreCase))
			{
				instanceName = "RemotePS-LiveID";
			}
			return RemotePowershellPerformanceCounters.GetInstance(instanceName);
		}

		// Token: 0x0400007E RID: 126
		private const string PSPerfCounterInstanceName = "RemotePS";

		// Token: 0x0400007F RID: 127
		private const string PSLiveIdPerfCounterInstanceName = "RemotePS-LiveID";

		// Token: 0x04000080 RID: 128
		private const string PSNoProxyPerfCounterInstanceName = "RemotePS-NoPro";

		// Token: 0x04000081 RID: 129
		private static RemotePowershellPerformanceCountersInstance current;
	}
}
