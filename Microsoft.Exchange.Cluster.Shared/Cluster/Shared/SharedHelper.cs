using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Win32;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000013 RID: 19
	internal static class SharedHelper
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003644 File Offset: 0x00001844
		internal static ExDateTime ExDateTimeMinValueUtc
		{
			get
			{
				return ExDateTime.MinValue.ToUtc();
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003660 File Offset: 0x00001860
		internal static DateTime DateTimeMinValueUtc
		{
			get
			{
				return DateTime.MinValue.ToUniversalTime();
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000367C File Offset: 0x0000187C
		public static bool RunADOperation(EventHandler ev)
		{
			Exception ex = SharedHelper.RunADOperationEx(ev);
			if (ex != null)
			{
				AmTrace.Error("RunADOperation(): ADException occurred : {0}", new object[]
				{
					ex
				});
				return false;
			}
			return true;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000036AC File Offset: 0x000018AC
		public static Exception RunADOperationEx(EventHandler ev)
		{
			Exception result = null;
			try
			{
				ev(null, null);
			}
			catch (ADTopologyUnexpectedException ex)
			{
				result = ex;
			}
			catch (ADTopologyPermanentException ex2)
			{
				result = ex2;
			}
			catch (ADOperationException ex3)
			{
				result = ex3;
			}
			catch (ADTransientException ex4)
			{
				result = ex4;
			}
			catch (DataValidationException ex5)
			{
				result = ex5;
			}
			return result;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003724 File Offset: 0x00001924
		public static Exception RunClusterOperation(Action codeToRun)
		{
			Exception result = null;
			try
			{
				codeToRun();
			}
			catch (ClusterException ex)
			{
				result = ex;
			}
			return result;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003754 File Offset: 0x00001954
		internal static string GetFqdnNameFromNode(string nodeName)
		{
			if (string.IsNullOrEmpty(nodeName) || SharedHelper.StringIEquals(nodeName, "localhost"))
			{
				nodeName = Environment.MachineName;
			}
			if (!nodeName.Contains("."))
			{
				return AmServerNameCache.Instance.GetFqdn(nodeName);
			}
			return nodeName;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000378C File Offset: 0x0000198C
		internal static bool IsLooksLikeFqdn(string serverName)
		{
			return MachineName.IsLikeFqdn(serverName);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003794 File Offset: 0x00001994
		internal static string GetNodeNameFromFqdn(string nodeName)
		{
			return MachineName.GetNodeNameFromFqdn(nodeName);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000379C File Offset: 0x0000199C
		internal static bool StringIEquals(string str1, string str2)
		{
			return StringUtil.IsEqualIgnoreCase(str1, str2);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000037A5 File Offset: 0x000019A5
		internal static int GetStringIHashCode(string str)
		{
			return StringUtil.GetStringIHashCode(str);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000037B0 File Offset: 0x000019B0
		internal static void DisposeObjectList<T>(IEnumerable<T> objList) where T : IDisposable
		{
			if (objList != null)
			{
				foreach (T t in objList)
				{
					if (t != null)
					{
						t.Dispose();
					}
				}
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000380C File Offset: 0x00001A0C
		public static void SetRegistryProperty(string keyName, string propertyName, object propertyValue)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(keyName, true))
			{
				if (registryKey != null)
				{
					registryKey.SetValue(propertyName, propertyValue);
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003850 File Offset: 0x00001A50
		public static T GetRegistryProperty<T>(string keyName, string propertyName, T defaultValue)
		{
			T result = defaultValue;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(keyName, true))
			{
				if (registryKey != null)
				{
					result = (T)((object)registryKey.GetValue(propertyName, defaultValue));
				}
			}
			return result;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000038A0 File Offset: 0x00001AA0
		public static void Log(string format, params object[] args)
		{
			ReplayCrimsonEvents.GenericMessage.Log<string>(string.Format(format, args));
		}

		// Token: 0x04000021 RID: 33
		internal static string ExchangeKeyRoot = "SOFTWARE\\Microsoft\\ExchangeServer\\v15";

		// Token: 0x04000022 RID: 34
		internal static string AmRegKeyRoot = SharedHelper.ExchangeKeyRoot + "\\ActiveManager";
	}
}
