using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000E3 RID: 227
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ApplicationName
	{
		// Token: 0x0600066F RID: 1647 RVA: 0x0001A4E4 File Offset: 0x000186E4
		private ApplicationName(string name, string uniqueId, int processId)
		{
			this.name = name;
			this.uniqueId = uniqueId;
			this.processId = processId;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001A501 File Offset: 0x00018701
		public static ApplicationName Current
		{
			get
			{
				if (ApplicationName.current == null)
				{
					ApplicationName.current = ApplicationName.GetCurrentApplicationName();
				}
				return ApplicationName.current;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0001A519 File Offset: 0x00018719
		public string UniqueId
		{
			get
			{
				return this.uniqueId;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0001A521 File Offset: 0x00018721
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0001A529 File Offset: 0x00018729
		public int ProcessId
		{
			get
			{
				return this.processId;
			}
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001A534 File Offset: 0x00018734
		private static ApplicationName GetCurrentApplicationName()
		{
			ApplicationName result;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				if (StringComparer.OrdinalIgnoreCase.Equals(currentProcess.ProcessName, "w3wp") && currentProcess.StartInfo != null && currentProcess.StartInfo.EnvironmentVariables != null && currentProcess.StartInfo.EnvironmentVariables.ContainsKey("APP_POOL_ID"))
				{
					string text = currentProcess.StartInfo.EnvironmentVariables["APP_POOL_ID"];
					if (!string.IsNullOrEmpty(text))
					{
						return new ApplicationName(text, string.Concat(new object[]
						{
							"w3wp_",
							text,
							"_",
							currentProcess.Id
						}), currentProcess.Id);
					}
				}
				result = new ApplicationName(currentProcess.ProcessName, currentProcess.ProcessName + "_" + currentProcess.Id, currentProcess.Id);
			}
			return result;
		}

		// Token: 0x0400045E RID: 1118
		private const string IISWorkerProcessName = "w3wp";

		// Token: 0x0400045F RID: 1119
		private const string AppPoolId = "APP_POOL_ID";

		// Token: 0x04000460 RID: 1120
		private static ApplicationName current;

		// Token: 0x04000461 RID: 1121
		private readonly string uniqueId;

		// Token: 0x04000462 RID: 1122
		private readonly string name;

		// Token: 0x04000463 RID: 1123
		private readonly int processId;
	}
}
