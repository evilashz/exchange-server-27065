using System;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000004 RID: 4
	internal sealed class UserWorkloadManagerHandler : ExchangeDiagnosableWrapper<UserWorkloadManagerResult>
	{
		// Token: 0x06000008 RID: 8 RVA: 0x00002173 File Offset: 0x00000373
		private UserWorkloadManagerHandler()
		{
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000217B File Offset: 0x0000037B
		protected override string ComponentName
		{
			get
			{
				return "UserWorkloadManager";
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002182 File Offset: 0x00000382
		protected override string UsageText
		{
			get
			{
				return "This diagnostics handler returns detailed information of the inner workings of the WLM. Below are examples for using this diagnostics handler: ";
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002189 File Offset: 0x00000389
		protected override string UsageSample
		{
			get
			{
				return "Example 1: For a “no cache dump” invocation:\r\n                        Get-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component UserWorkloadManager\r\n                        \r\n                        Example 2:For a “cache dump” invocation:\r\n                        Get-ExchangeDiagnosticInfo -Process MSExchangeSyncAppPool -Component UserWorkloadManager -Argument dumpcache";
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002190 File Offset: 0x00000390
		public static UserWorkloadManagerHandler GetInstance()
		{
			if (UserWorkloadManagerHandler.instance == null)
			{
				lock (UserWorkloadManagerHandler.lockObject)
				{
					if (UserWorkloadManagerHandler.instance == null)
					{
						UserWorkloadManagerHandler.instance = new UserWorkloadManagerHandler();
					}
				}
			}
			return UserWorkloadManagerHandler.instance;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021E8 File Offset: 0x000003E8
		internal override UserWorkloadManagerResult GetExchangeDiagnosticsInfoData(DiagnosableParameters argument)
		{
			bool dumpCaches = !string.IsNullOrEmpty(argument.Argument) && argument.Argument.ToLower() == "dumpcache";
			return UserWorkloadManager.Singleton.GetDiagnosticSnapshot(dumpCaches);
		}

		// Token: 0x04000007 RID: 7
		private const string DumpCacheArgument = "dumpcache";

		// Token: 0x04000008 RID: 8
		private static UserWorkloadManagerHandler instance;

		// Token: 0x04000009 RID: 9
		private static object lockObject = new object();
	}
}
