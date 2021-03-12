using System;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000B8 RID: 184
	internal class ServiceKillConfig
	{
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x00024A83 File Offset: 0x00022C83
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x00024A8B File Offset: 0x00022C8B
		internal string ServiceName { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x00024A94 File Offset: 0x00022C94
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x00024A9C File Offset: 0x00022C9C
		internal DateTime LastKillTime { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00024AA5 File Offset: 0x00022CA5
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x00024AAD File Offset: 0x00022CAD
		internal bool IsDisabled { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x00024AB6 File Offset: 0x00022CB6
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x00024ABE File Offset: 0x00022CBE
		internal TimeSpan DurationBetweenKill { get; set; }

		// Token: 0x06000798 RID: 1944 RVA: 0x00024AC7 File Offset: 0x00022CC7
		internal ServiceKillConfig(string serviceName)
		{
			this.ServiceName = serviceName;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00024AD8 File Offset: 0x00022CD8
		internal static void WithKey(string serviceName, bool isWritable, Action<RegistryKey> operation)
		{
			string text = string.Format("{0}\\{1}", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters\\ServiceKill", serviceName);
			using (RegistryKey registryKey = isWritable ? Registry.LocalMachine.CreateSubKey(text) : Registry.LocalMachine.OpenSubKey(text))
			{
				if (registryKey != null)
				{
					operation(registryKey);
				}
			}
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00024B38 File Offset: 0x00022D38
		internal static T GetValue<T>(RegistryKey key, string propertyName, T defaultValue)
		{
			T result = defaultValue;
			if (key != null)
			{
				result = (T)((object)key.GetValue(propertyName, defaultValue));
			}
			return result;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00024BD4 File Offset: 0x00022DD4
		internal static ServiceKillConfig Read(string serviceName)
		{
			ServiceKillConfig skc = new ServiceKillConfig(serviceName);
			ServiceKillConfig.WithKey(serviceName, false, delegate(RegistryKey key)
			{
				string value = ServiceKillConfig.GetValue<string>(key, "LastKillTime", null);
				if (value != null)
				{
					skc.LastKillTime = DateTime.Parse(value);
				}
				skc.IsDisabled = (ServiceKillConfig.GetValue<int>(key, "IsDisabled", 0) > 0);
				skc.DurationBetweenKill = TimeSpan.FromSeconds((double)ServiceKillConfig.GetValue<int>(key, "DurationBetweenKillInSec", 14400));
			});
			return skc;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00024C40 File Offset: 0x00022E40
		internal void UpdateKillTime(DateTime killTime)
		{
			this.LastKillTime = killTime;
			ServiceKillConfig.WithKey(this.ServiceName, true, delegate(RegistryKey key)
			{
				string value = killTime.ToString("o");
				key.SetValue("LastKillTime", value);
			});
		}

		// Token: 0x04000354 RID: 852
		internal const string ConfigKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters\\ServiceKill";

		// Token: 0x04000355 RID: 853
		internal const string LastKillTimeProperty = "LastKillTime";

		// Token: 0x04000356 RID: 854
		internal const string IsDisabledProperty = "IsDisabled";

		// Token: 0x04000357 RID: 855
		internal const string DurationBetweenKillProperty = "DurationBetweenKillInSec";

		// Token: 0x04000358 RID: 856
		internal const int DefaultMinimumDurationBetweenKillInSec = 14400;
	}
}
