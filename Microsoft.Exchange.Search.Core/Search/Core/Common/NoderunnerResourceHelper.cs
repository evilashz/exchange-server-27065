using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000077 RID: 119
	public class NoderunnerResourceHelper
	{
		// Token: 0x060002FF RID: 767 RVA: 0x00009E67 File Offset: 0x00008067
		public NoderunnerResourceHelper()
		{
			this.ProcessDictionary = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
			this.PopulateProcessDictionary();
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00009E85 File Offset: 0x00008085
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00009E8D File Offset: 0x0000808D
		public Dictionary<string, int> ProcessDictionary { get; private set; }

		// Token: 0x06000302 RID: 770 RVA: 0x00009E98 File Offset: 0x00008098
		public bool IsIndexNodeMemoryUsageExceeded(long memoryMaxUsage)
		{
			int pid = this.GetPid("IndexNode1");
			return pid != 0 && this.GetMemoryUsage(pid) > memoryMaxUsage;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00009EC0 File Offset: 0x000080C0
		private void PopulateProcessDictionary()
		{
			string queryString = "SELECT ProcessId, CommandLine from Win32_Process WHERE Name LIKE \"%NodeRunner%\"";
			using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(queryString))
			{
				using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
				{
					foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						try
						{
							int value = 0;
							if (managementObject["CommandLine"] != null && managementObject["ProcessId"] != null && int.TryParse(managementObject["ProcessId"].ToString(), out value))
							{
								foreach (string text in NoderunnerResourceHelper.NodeRunnerInstanceNames)
								{
									if (managementObject["CommandLine"].ToString().IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
									{
										this.ProcessDictionary[text] = value;
										break;
									}
								}
							}
						}
						finally
						{
							managementObject.Dispose();
						}
					}
				}
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00009FF0 File Offset: 0x000081F0
		private int GetPid(string fastNodeName)
		{
			int result;
			if (!this.ProcessDictionary.TryGetValue(fastNodeName, out result))
			{
				return 0;
			}
			return result;
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000A010 File Offset: 0x00008210
		private long GetMemoryUsage(int pid)
		{
			long result = 0L;
			try
			{
				using (Process processById = Process.GetProcessById(pid))
				{
					result = processById.WorkingSet64;
				}
			}
			catch (ArgumentException)
			{
				return 0L;
			}
			return result;
		}

		// Token: 0x04000148 RID: 328
		private const string AdminNode1 = "AdminNode1";

		// Token: 0x04000149 RID: 329
		private const string ContentEngineNode1 = "ContentEngineNode1";

		// Token: 0x0400014A RID: 330
		private const string InteractionEngineNode1 = "InteractionEngineNode1";

		// Token: 0x0400014B RID: 331
		private const string IndexNode1 = "IndexNode1";

		// Token: 0x0400014C RID: 332
		private static readonly string[] NodeRunnerInstanceNames = new string[]
		{
			"AdminNode1",
			"ContentEngineNode1",
			"IndexNode1",
			"InteractionEngineNode1"
		};
	}
}
