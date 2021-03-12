using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement.Implementation
{
	// Token: 0x020001FF RID: 511
	internal static class ProcessThreadHelper
	{
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x0003D3A8 File Offset: 0x0003B5A8
		internal static ProcessThread Current
		{
			get
			{
				ProcessThread processThread = null;
				Dictionary<int, ProcessThread> refreshedMap = ProcessThreadHelper.threadsMap;
				int currentThreadId = DiagnosticsNativeMethods.GetCurrentThreadId();
				refreshedMap.TryGetValue(currentThreadId, out processThread);
				if (processThread == null || processThread.Id != currentThreadId)
				{
					refreshedMap = ProcessThreadHelper.GetRefreshedMap();
					processThread = refreshedMap[currentThreadId];
					ProcessThreadHelper.threadsMap = refreshedMap;
				}
				return processThread;
			}
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0003D3F0 File Offset: 0x0003B5F0
		private static Dictionary<int, ProcessThread> GetRefreshedMap()
		{
			Dictionary<int, ProcessThread> dictionary = new Dictionary<int, ProcessThread>();
			Process currentProcess = Process.GetCurrentProcess();
			foreach (object obj in currentProcess.Threads)
			{
				ProcessThread processThread = (ProcessThread)obj;
				dictionary.Add(processThread.Id, processThread);
			}
			return dictionary;
		}

		// Token: 0x04000AAE RID: 2734
		private static Dictionary<int, ProcessThread> threadsMap = new Dictionary<int, ProcessThread>();
	}
}
