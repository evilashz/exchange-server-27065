using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.CommonHandlers
{
	// Token: 0x020000C2 RID: 194
	public sealed class ProcessDiagnosticsInfoManager : ExchangeDiagnosableWrapper<ProcessInfo>
	{
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600083C RID: 2108 RVA: 0x00021A89 File Offset: 0x0001FC89
		protected override string UsageText
		{
			get
			{
				return "This is a generic handler to retrieve information about current process. A sample usage is as shown below:";
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00021A90 File Offset: 0x0001FC90
		protected override string UsageSample
		{
			get
			{
				return "Get-ExchangeDiagnosticsInfo -Process <ProcessName> -Component ProcessInfo";
			}
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00021A98 File Offset: 0x0001FC98
		public static ProcessDiagnosticsInfoManager GetInstance()
		{
			if (ProcessDiagnosticsInfoManager.instance == null)
			{
				lock (ProcessDiagnosticsInfoManager.lockObject)
				{
					if (ProcessDiagnosticsInfoManager.instance == null)
					{
						ProcessDiagnosticsInfoManager.instance = new ProcessDiagnosticsInfoManager();
					}
				}
			}
			return ProcessDiagnosticsInfoManager.instance;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x00021AF0 File Offset: 0x0001FCF0
		private ProcessDiagnosticsInfoManager()
		{
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x00021AF8 File Offset: 0x0001FCF8
		protected override string ComponentName
		{
			get
			{
				return "ProcessInfo";
			}
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x00021B00 File Offset: 0x0001FD00
		internal override ProcessInfo GetExchangeDiagnosticsInfoData(DiagnosableParameters argument)
		{
			ProcessInfo result;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				result = new ProcessInfo
				{
					ServerName = currentProcess.MachineName,
					ProcessID = currentProcess.Id,
					ThreadCount = currentProcess.Threads.Count,
					MemorySize = currentProcess.VirtualMemorySize64,
					ProcessUpTime = currentProcess.TotalProcessorTime.TotalHours,
					Version = "15.00.1497.010"
				};
			}
			return result;
		}

		// Token: 0x040003AE RID: 942
		private static ProcessDiagnosticsInfoManager instance;

		// Token: 0x040003AF RID: 943
		private static object lockObject = new object();
	}
}
