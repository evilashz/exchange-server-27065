using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.Common.Extensions;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability
{
	// Token: 0x02000190 RID: 400
	internal class CachedRegistryReader : IRegistryReader
	{
		// Token: 0x06000B95 RID: 2965 RVA: 0x00049D3C File Offset: 0x00047F3C
		private CachedRegistryReader()
		{
			if (CachedRegistryReader.baseKeyMap == null)
			{
				CachedRegistryReader.baseKeyMap = new Dictionary<string, RegistryKey>();
				CachedRegistryReader.baseKeyMap.Add(Registry.ClassesRoot.Name, Registry.ClassesRoot);
				CachedRegistryReader.baseKeyMap.Add(Registry.CurrentConfig.Name, Registry.CurrentConfig);
				CachedRegistryReader.baseKeyMap.Add(Registry.CurrentUser.Name, Registry.CurrentUser);
				CachedRegistryReader.baseKeyMap.Add(Registry.LocalMachine.Name, Registry.LocalMachine);
				CachedRegistryReader.baseKeyMap.Add(Registry.PerformanceData.Name, Registry.PerformanceData);
				CachedRegistryReader.baseKeyMap.Add(Registry.Users.Name, Registry.Users);
			}
			int value = RegistryReader.Instance.GetValue<int>(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\HighAvailability\\Parameters", "RegCacheExpirationInSeconds", 300);
			this.cachedList = new CachedList<object, string>(delegate(string str)
			{
				string[] array = CachedRegistryReader.SeperateKey(str);
				return RegistryReader.Instance.GetValue<object>(CachedRegistryReader.baseKeyMap[array[0]], array[1], array[2], null);
			}, TimeSpan.FromSeconds((double)value));
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x00049E48 File Offset: 0x00048048
		public static IRegistryReader Instance
		{
			get
			{
				if (CachedRegistryReader.cachedRegReaderInstance == null)
				{
					lock (CachedRegistryReader.instanceCreationLock)
					{
						CachedRegistryReader.cachedRegReaderInstance = new CachedRegistryReader();
					}
				}
				return CachedRegistryReader.cachedRegReaderInstance;
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00049E98 File Offset: 0x00048098
		public string[] GetSubKeyNames(RegistryKey baseKey, string subkeyName)
		{
			return RegistryReader.Instance.GetSubKeyNames(baseKey, subkeyName);
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00049EA8 File Offset: 0x000480A8
		public T GetValue<T>(RegistryKey baseKey, string subkeyName, string valueName, T defaultValue)
		{
			object value = this.cachedList.GetValue(CachedRegistryReader.MakeKey(baseKey, subkeyName, valueName));
			if (value != null)
			{
				return (T)((object)value);
			}
			return defaultValue;
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00049ED8 File Offset: 0x000480D8
		private static string MakeKey(RegistryKey baseKey, string subkeyName, string valueName)
		{
			return string.Join("`", new string[]
			{
				baseKey.Name,
				subkeyName,
				valueName
			});
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00049F08 File Offset: 0x00048108
		private static string[] SeperateKey(string key)
		{
			return key.Split(new char[]
			{
				'`'
			});
		}

		// Token: 0x040008CB RID: 2251
		private static IRegistryReader cachedRegReaderInstance = null;

		// Token: 0x040008CC RID: 2252
		private static object instanceCreationLock = new object();

		// Token: 0x040008CD RID: 2253
		private static Dictionary<string, RegistryKey> baseKeyMap = null;

		// Token: 0x040008CE RID: 2254
		private CachedList<object, string> cachedList;
	}
}
