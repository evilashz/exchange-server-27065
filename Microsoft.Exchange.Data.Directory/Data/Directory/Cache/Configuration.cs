using System;
using System.Diagnostics;
using System.Runtime.Caching;
using Microsoft.Exchange.Data.Directory.EventLog;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.Cache
{
	// Token: 0x0200009A RID: 154
	internal class Configuration
	{
		// Token: 0x060008C6 RID: 2246 RVA: 0x000273B4 File Offset: 0x000255B4
		internal static bool IsCacheEnabled(string processNameOrProcessAppName)
		{
			bool result;
			try
			{
				result = Configuration.hookableInstance.Value.IsCacheEnabled(processNameOrProcessAppName);
			}
			catch (Exception ex)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_ReadADCacheConfigurationFailed, processNameOrProcessAppName, new object[]
				{
					ex.ToString()
				});
				result = false;
			}
			return result;
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00027408 File Offset: 0x00025608
		internal static CacheMode GetCacheMode(string processNameOrProcessAppName)
		{
			CacheMode result;
			try
			{
				result = Configuration.hookableInstance.Value.GetCacheMode(processNameOrProcessAppName);
			}
			catch (Exception ex)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_ReadADCacheConfigurationFailed, processNameOrProcessAppName, new object[]
				{
					ex.ToString()
				});
				result = CacheMode.Disabled;
			}
			return result;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x0002745C File Offset: 0x0002565C
		internal static CacheMode GetCacheModeForCurrentProcess()
		{
			CacheMode result;
			try
			{
				result = Configuration.hookableInstance.Value.GetCacheModeForCurrentProcess();
			}
			catch (Exception ex)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_ReadADCacheConfigurationFailed, Globals.ProcessName, new object[]
				{
					ex.ToString()
				});
				result = CacheMode.Disabled;
			}
			return result;
		}

		// Token: 0x060008C9 RID: 2249 RVA: 0x000274B4 File Offset: 0x000256B4
		internal static bool IsCacheEnableForCurrentProcess()
		{
			bool result;
			try
			{
				result = Configuration.hookableInstance.Value.IsCacheEnableForCurrentProcess();
			}
			catch (Exception ex)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_ReadADCacheConfigurationFailed, Globals.ProcessName, new object[]
				{
					ex.ToString()
				});
				result = false;
			}
			return result;
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x0002750C File Offset: 0x0002570C
		internal static bool IsCacheEnabled(Type type)
		{
			bool result;
			try
			{
				result = Configuration.hookableInstance.Value.IsCacheEnabled(type);
			}
			catch (Exception ex)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_ReadADCacheConfigurationFailed, Globals.ProcessName, new object[]
				{
					ex.ToString()
				});
				result = false;
			}
			return result;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x00027564 File Offset: 0x00025764
		internal static bool IsCacheEnabledForInsertOnSave(ADRawEntry rawEntry)
		{
			bool result;
			try
			{
				result = Configuration.hookableInstance.Value.IsCacheEnabledForInsertOnSave(rawEntry);
			}
			catch (Exception ex)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_ReadADCacheConfigurationFailed, Globals.ProcessName, new object[]
				{
					ex.ToString()
				});
				result = false;
			}
			return result;
		}

		// Token: 0x060008CC RID: 2252 RVA: 0x000275BC File Offset: 0x000257BC
		internal static int GetCacheExpirationForObject(ADRawEntry rawEntry)
		{
			int result;
			try
			{
				result = Configuration.hookableInstance.Value.GetCacheExpirationForObject(rawEntry);
			}
			catch (Exception ex)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_ReadADCacheConfigurationFailed, Globals.ProcessName, new object[]
				{
					ex.ToString()
				});
				result = 2147483646;
			}
			return result;
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00027618 File Offset: 0x00025818
		internal static CacheItemPriority GetCachePriorityForObject(ADRawEntry rawEntry)
		{
			CacheItemPriority result;
			try
			{
				result = Configuration.hookableInstance.Value.GetCachePriorityForObject(rawEntry);
			}
			catch (Exception ex)
			{
				Globals.LogEvent(DirectoryEventLogConstants.Tuple_ReadADCacheConfigurationFailed, Globals.ProcessName, new object[]
				{
					ex.ToString()
				});
				result = CacheItemPriority.Default;
			}
			return result;
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00027670 File Offset: 0x00025870
		internal static IDisposable SetTestHook(ICacheConfiguration wrapper)
		{
			return Configuration.hookableInstance.SetTestHook(wrapper);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00027680 File Offset: 0x00025880
		internal static void Refresh()
		{
			ConfigurationADImpl configurationADImpl = (ConfigurationADImpl)Configuration.hookableInstance.Value;
			Configuration.hookableInstance = Hookable<ICacheConfiguration>.Create(true, new ConfigurationADImpl());
			configurationADImpl.Dispose();
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000276B3 File Offset: 0x000258B3
		[Conditional("DEBUG")]
		internal static void FriendlyWatson(Exception exception)
		{
			if (Debugger.IsAttached)
			{
				Debugger.Break();
			}
			ExWatson.SendReport(exception, ReportOptions.DeepStackTraceHash, null);
		}

		// Token: 0x040002C1 RID: 705
		private static Hookable<ICacheConfiguration> hookableInstance = Hookable<ICacheConfiguration>.Create(true, new ConfigurationADImpl());
	}
}
