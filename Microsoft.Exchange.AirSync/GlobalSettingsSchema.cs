using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Win32;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000C1 RID: 193
	internal sealed class GlobalSettingsSchema
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x0003BFC4 File Offset: 0x0003A1C4
		private static Dictionary<string, GlobalSettingsPropertyDefinition> PropertyMapping
		{
			get
			{
				if (GlobalSettingsSchema.propMap == null)
				{
					lock (GlobalSettingsSchema.staticLock)
					{
						if (GlobalSettingsSchema.propMap == null)
						{
							GlobalSettingsSchema.propMap = new Dictionary<string, GlobalSettingsPropertyDefinition>();
							Type typeFromHandle = typeof(GlobalSettingsSchema);
							foreach (FieldInfo fieldInfo in from x in typeFromHandle.GetTypeInfo().DeclaredFields
							where x.IsStatic && x.IsPublic && x.FieldType == typeof(GlobalSettingsPropertyDefinition)
							select x)
							{
								GlobalSettingsPropertyDefinition globalSettingsPropertyDefinition = (GlobalSettingsPropertyDefinition)fieldInfo.GetValue(null);
								GlobalSettingsSchema.propMap[globalSettingsPropertyDefinition.Name] = globalSettingsPropertyDefinition;
							}
						}
					}
				}
				return GlobalSettingsSchema.propMap;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x0003C0AC File Offset: 0x0003A2AC
		public static IList<GlobalSettingsPropertyDefinition> AllProperties
		{
			get
			{
				return GlobalSettingsSchema.PropertyMapping.Values.ToList<GlobalSettingsPropertyDefinition>();
			}
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0003C0C0 File Offset: 0x0003A2C0
		public static GlobalSettingsPropertyDefinition GetProperty(string name)
		{
			GlobalSettingsPropertyDefinition result = null;
			if (GlobalSettingsSchema.PropertyMapping.TryGetValue(name, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0003C0E1 File Offset: 0x0003A2E1
		internal static string GetAppSetting(GlobalSettingsPropertyDefinition propDef)
		{
			if (TestHooks.GlobalSettings_GetAppSetting != null)
			{
				return TestHooks.GlobalSettings_GetAppSetting(propDef);
			}
			return ConfigurationManager.AppSettings[propDef.Name];
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0003C106 File Offset: 0x0003A306
		internal static object GetRegistrySetting(GlobalSettingsPropertyDefinition propDef, string registryPath)
		{
			if (TestHooks.GlobalSettings_GetRegistrySetting != null)
			{
				return TestHooks.GlobalSettings_GetRegistrySetting(propDef, registryPath);
			}
			return GlobalSettingsSchema.ReadRegistryValue(propDef.Name, registryPath);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0003C128 File Offset: 0x0003A328
		internal static bool GetFlightingSetting(GlobalSettingsPropertyDefinition propDef, IFeature feature)
		{
			if (TestHooks.GlobalSettings_GetFlightingSetting != null)
			{
				return (bool)TestHooks.GlobalSettings_GetFlightingSetting(propDef);
			}
			return feature.Enabled;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0003C148 File Offset: 0x0003A348
		internal static bool GccGetStoredSecretKeysValid()
		{
			if (TestHooks.GccUtils_AreStoredSecurityKeysValid != null)
			{
				return TestHooks.GccUtils_AreStoredSecurityKeysValid();
			}
			return GccUtils.AreStoredSecretKeysValid();
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0003C164 File Offset: 0x0003A364
		private static GlobalSettingsPropertyDefinition CreateMaxWorkerThreadsPerProc()
		{
			int num;
			int num2;
			ThreadPool.GetMaxThreads(out num, out num2);
			num /= Environment.ProcessorCount;
			return GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MaxWorkerThreadsPerProc", num, 1, num);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0003C18F File Offset: 0x0003A38F
		private static GlobalSettingsPropertyDefinition CreateConstrainedPropertyDefinition<T>(string name, T defaultValue, T minValue, T maxValue) where T : struct, IComparable
		{
			return GlobalSettingsSchema.CreateConstrainedPropertyDefinition<T>(name, defaultValue, minValue, maxValue, false, null);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0003C19C File Offset: 0x0003A39C
		private static GlobalSettingsPropertyDefinition CreateConstrainedPropertyDefinition<T>(string name, T defaultValue, T minValue, T maxValue, bool logMissingEntries, Func<GlobalSettingsPropertyDefinition, object> getter) where T : struct, IComparable
		{
			return new GlobalSettingsPropertyDefinition(name, typeof(T), defaultValue, new RangedValueConstraint<T>(minValue, maxValue), logMissingEntries, getter);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0003C1BF File Offset: 0x0003A3BF
		private static GlobalSettingsPropertyDefinition CreatePropertyDefinition(string name, Type type, object defaultValue)
		{
			return GlobalSettingsSchema.CreatePropertyDefinition(name, type, defaultValue, false, null);
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0003C1CB File Offset: 0x0003A3CB
		private static GlobalSettingsPropertyDefinition CreatePropertyDefinition(string name, Type type, object defaultValue, bool logMissingEntries, Func<GlobalSettingsPropertyDefinition, object> getter)
		{
			return new GlobalSettingsPropertyDefinition(name, type, defaultValue, null, logMissingEntries, getter);
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0003C228 File Offset: 0x0003A428
		private static GlobalSettingsPropertyDefinition CreateVDirPropertyDefinition<T>(string name, T defaultValue, Func<ADMobileVirtualDirectory, T> getter)
		{
			return GlobalSettingsSchema.CreatePropertyDefinition(name, typeof(T), defaultValue, false, delegate(GlobalSettingsPropertyDefinition propDef2)
			{
				if (TestHooks.GlobalSettings_GetVDirSetting != null)
				{
					return TestHooks.GlobalSettings_GetVDirSetting(propDef2);
				}
				ADMobileVirtualDirectory admobileVirtualDirectory = ADNotificationManager.ADMobileVirtualDirectory;
				return (admobileVirtualDirectory == null) ? defaultValue : getter(admobileVirtualDirectory);
			});
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0003C274 File Offset: 0x0003A474
		private static object LoadRegistryBool(GlobalSettingsPropertyDefinition propDef, string registryPath)
		{
			object registrySetting = GlobalSettingsSchema.GetRegistrySetting(propDef, registryPath);
			if (registrySetting != null && registrySetting.GetType() == typeof(int))
			{
				return (int)registrySetting != 0;
			}
			return (bool)propDef.DefaultValue;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0003C2C8 File Offset: 0x0003A4C8
		private static int LoadRegistryInt(GlobalSettingsPropertyDefinition propDef, string registryPath)
		{
			object registrySetting = GlobalSettingsSchema.GetRegistrySetting(propDef, registryPath);
			if (registrySetting != null && registrySetting.GetType() == typeof(int))
			{
				return (int)registrySetting;
			}
			return (int)propDef.DefaultValue;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0003C30C File Offset: 0x0003A50C
		private static object ReadRegistryValue(string keyName, string registryPath)
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(registryPath))
				{
					if (registryKey != null)
					{
						return registryKey.GetValue(keyName);
					}
					AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_GlobalValueRegistryValueMissing, new string[]
					{
						keyName,
						registryPath
					});
				}
			}
			catch (SecurityException ex)
			{
				AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_GlobalValueRegistryReadFailure, new string[]
				{
					keyName,
					registryPath,
					ex.ToString()
				});
			}
			catch (UnauthorizedAccessException ex2)
			{
				AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_GlobalValueRegistryReadFailure, new string[]
				{
					keyName,
					registryPath,
					ex2.ToString()
				});
			}
			return null;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0003C3E4 File Offset: 0x0003A5E4
		private static object ConvertSecondsToTimeSpan(GlobalSettingsPropertyDefinition propDef)
		{
			int num = (int)GlobalSettingsPropertyDefinition.ConvertValueFromString(GlobalSettingsSchema.GetAppSetting(propDef), typeof(int), propDef.Name, (int)((TimeSpan)propDef.DefaultValue).TotalSeconds);
			return TimeSpan.FromSeconds((double)num);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0003C437 File Offset: 0x0003A637
		private static object GetPartnerHostedOnly(GlobalSettingsPropertyDefinition propDef)
		{
			return DatacenterRegistry.IsPartnerHostedOnly();
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0003C444 File Offset: 0x0003A644
		private static object ConvertMinutesToTimeSpan(GlobalSettingsPropertyDefinition propDef)
		{
			int num = (int)GlobalSettingsPropertyDefinition.ConvertValueFromString(GlobalSettingsSchema.GetAppSetting(propDef), typeof(int), propDef.Name, (int)((TimeSpan)propDef.DefaultValue).TotalMinutes);
			return TimeSpan.FromMinutes((double)num);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0003C498 File Offset: 0x0003A698
		private static object DeviceTypeParse(GlobalSettingsPropertyDefinition propDef)
		{
			string appSetting = GlobalSettingsSchema.GetAppSetting(propDef);
			if (string.IsNullOrWhiteSpace(appSetting))
			{
				return (string[])propDef.DefaultValue;
			}
			string[] array = (string[])propDef.DefaultValue;
			string[] array2 = appSetting.Split(new string[]
			{
				","
			}, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length > 0)
			{
				string[] array3 = new string[array2.Length + array.Length];
				Array.Copy(array2, array3, array2.Length);
				Array.Copy(array, 0, array3, array2.Length, array.Length);
				return array3;
			}
			return array2;
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0003C518 File Offset: 0x0003A718
		private static List<string> ParseCommaSeparatedValuesIntoList(GlobalSettingsPropertyDefinition propDef)
		{
			string appSetting = GlobalSettingsSchema.GetAppSetting(propDef);
			if (string.IsNullOrEmpty(appSetting))
			{
				return propDef.DefaultValue as List<string>;
			}
			return new List<string>(appSetting.ToLower().Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries));
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0003C560 File Offset: 0x0003A760
		private static List<string> ParseSupportedIPMTypes(GlobalSettingsPropertyDefinition propDef)
		{
			string appSetting = GlobalSettingsSchema.GetAppSetting(propDef);
			if (string.IsNullOrEmpty(appSetting))
			{
				if (propDef.LogMissingEntries)
				{
					AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_GlobalValueHasBeenDefaulted, new string[]
					{
						propDef.Name,
						propDef.DefaultValue.ToString()
					});
				}
				return (List<string>)propDef.DefaultValue;
			}
			char[] separator = new char[]
			{
				';'
			};
			string[] array = appSetting.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			List<string> list = new List<string>(array.Length);
			foreach (string text in array)
			{
				bool flag = true;
				foreach (char c in text)
				{
					if (!char.IsLetterOrDigit(c) && c != '.' && c != '-')
					{
						AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_GlobalValueHasInvalidCharacters, new string[]
						{
							propDef.Name,
							text
						});
						flag = false;
						break;
					}
				}
				if (flag)
				{
					string text3 = text.ToUpperInvariant();
					if (text3.StartsWith("IPM.", StringComparison.Ordinal))
					{
						list.Add(text3);
					}
					else
					{
						AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_CustomTypeDoesNotStartWithIPM, new string[]
						{
							propDef.Name,
							text
						});
					}
				}
			}
			if (list.Count == 0)
			{
				if (propDef.LogMissingEntries)
				{
					AirSyncDiagnostics.LogEvent(AirSyncEventLogConstants.Tuple_GlobalValueHasBeenDefaulted, new string[]
					{
						propDef.Name,
						propDef.DefaultValue.ToString()
					});
				}
				return (List<string>)propDef.DefaultValue;
			}
			return list;
		}

		// Token: 0x04000650 RID: 1616
		private const char CommaSeparator = ',';

		// Token: 0x04000651 RID: 1617
		private static Dictionary<string, GlobalSettingsPropertyDefinition> propMap = null;

		// Token: 0x04000652 RID: 1618
		private static object staticLock = new object();

		// Token: 0x04000653 RID: 1619
		public static GlobalSettingsPropertyDefinition UseTestBudget = GlobalSettingsSchema.CreatePropertyDefinition("UseTestBudget", typeof(bool), false);

		// Token: 0x04000654 RID: 1620
		public static GlobalSettingsPropertyDefinition WriteProtocolLogDiagnostics = GlobalSettingsSchema.CreatePropertyDefinition("WriteProtocolLogDiagnostics", typeof(bool), false);

		// Token: 0x04000655 RID: 1621
		public static GlobalSettingsPropertyDefinition HangingSyncHintCacheSize = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("HangingSyncHintCacheSize", 10000, 1, int.MaxValue);

		// Token: 0x04000656 RID: 1622
		public static GlobalSettingsPropertyDefinition HangingSyncHintCacheTimeout = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("HangingSyncHintCacheTimeout", TimeSpan.FromMinutes(15.0), TimeSpan.Zero, TimeSpan.MaxValue, false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ConvertMinutesToTimeSpan));

		// Token: 0x04000657 RID: 1623
		public static GlobalSettingsPropertyDefinition MaxNumOfFolders = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MaxNumOfFolders", 100, 30, 5000);

		// Token: 0x04000658 RID: 1624
		public static GlobalSettingsPropertyDefinition NumOfQueuedMailboxLogEntries = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("NumOfQueuedMailboxLogEntries", 15, 1, 200);

		// Token: 0x04000659 RID: 1625
		public static GlobalSettingsPropertyDefinition MaxSizeOfMailboxLog = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MaxSizeOfMailboxLog", 8000, 1000, 1000000);

		// Token: 0x0400065A RID: 1626
		public static GlobalSettingsPropertyDefinition MaxNoOfPartnershipToAutoClean = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MaxNoOfPartnershipToAutoClean", 10, 0, int.MaxValue);

		// Token: 0x0400065B RID: 1627
		public static GlobalSettingsPropertyDefinition ADCacheRefreshInterval = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("ADCacheRefreshInterval", TimeSpan.FromSeconds(60.0), TimeSpan.FromSeconds(60.0), TimeSpan.MaxValue, false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ConvertSecondsToTimeSpan));

		// Token: 0x0400065C RID: 1628
		public static GlobalSettingsPropertyDefinition MaxCleanUpExecutionTime = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("MaxCleanUpExecutionTime", TimeSpan.FromSeconds(45.0), TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(60.0), false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ConvertSecondsToTimeSpan));

		// Token: 0x0400065D RID: 1629
		public static GlobalSettingsPropertyDefinition EventQueuePollingInterval = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("EventQueuePollingInterval", TimeSpan.FromSeconds(2.0), TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(600.0), false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ConvertSecondsToTimeSpan));

		// Token: 0x0400065E RID: 1630
		public static GlobalSettingsPropertyDefinition MaxRetrievedItems = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MaxRetrievedItems", 100, 1, int.MaxValue);

		// Token: 0x0400065F RID: 1631
		public static GlobalSettingsPropertyDefinition MaxNoOfItemsMove = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MaxNoOfItemsMove", 1000, 1, int.MaxValue);

		// Token: 0x04000660 RID: 1632
		public static GlobalSettingsPropertyDefinition MaxWindowSize = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MaxWindowSize", 100, 1, int.MaxValue);

		// Token: 0x04000661 RID: 1633
		public static GlobalSettingsPropertyDefinition BudgetBackOffMinThreshold = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("BudgetBackOffMinThreshold", TimeSpan.FromSeconds(10.0), TimeSpan.FromSeconds(0.0), TimeSpan.MaxValue);

		// Token: 0x04000662 RID: 1634
		public static GlobalSettingsPropertyDefinition AutoblockBackOffMediumThreshold = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<double>("AutoblockBackOffMediumThreshold", 0.5, 0.0, 1.0);

		// Token: 0x04000663 RID: 1635
		public static GlobalSettingsPropertyDefinition AutoblockBackOffHighThreshold = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<double>("AutoblockBackOffHighThreshold", 0.75, 0.0, 1.0);

		// Token: 0x04000664 RID: 1636
		public static GlobalSettingsPropertyDefinition MaxNumberOfClientOperations = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MaxNumberOfClientOperations", 200, 1, int.MaxValue);

		// Token: 0x04000665 RID: 1637
		public static GlobalSettingsPropertyDefinition MinRedirectProtocolVersion = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MinRedirectProtocolVersion", 140, 120, 140);

		// Token: 0x04000666 RID: 1638
		public static GlobalSettingsPropertyDefinition DeviceClassCacheMaxStartDelay = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("DeviceClassCacheMaxStartDelay", TimeSpan.FromSeconds(21600.0), TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(86400.0), false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ConvertSecondsToTimeSpan));

		// Token: 0x04000667 RID: 1639
		public static GlobalSettingsPropertyDefinition DeviceClassCacheMaxADUploadCount = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("DeviceClassCacheMaxADUploadCount", 300, 1, 1000);

		// Token: 0x04000668 RID: 1640
		public static GlobalSettingsPropertyDefinition DeviceClassCachePerOrgRefreshInterval = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("DeviceClassCachePerOrgRefreshInterval", 10800, 0, 86400);

		// Token: 0x04000669 RID: 1641
		public static GlobalSettingsPropertyDefinition RequestCacheTimeInterval = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("RequestCacheTimeInterval", 10, 0, 1440);

		// Token: 0x0400066A RID: 1642
		public static GlobalSettingsPropertyDefinition RequestCacheMaxCount = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("RequestCacheMaxCount", 5000, 0, int.MaxValue);

		// Token: 0x0400066B RID: 1643
		public static GlobalSettingsPropertyDefinition DeviceClassPerOrgMaxADCount = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("DeviceClassPerOrgMaxADCount", 1000, 1, 10000);

		// Token: 0x0400066C RID: 1644
		public static GlobalSettingsPropertyDefinition DeviceClassCacheADCleanupInterval = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("DeviceClassCacheADCleanupInterval", 90, 1, 730);

		// Token: 0x0400066D RID: 1645
		public static GlobalSettingsPropertyDefinition DisableCaching = GlobalSettingsSchema.CreatePropertyDefinition("DisableCaching", typeof(bool), false);

		// Token: 0x0400066E RID: 1646
		public static GlobalSettingsPropertyDefinition SyncLogEnabled = GlobalSettingsSchema.CreatePropertyDefinition("SyncLogEnabled", typeof(bool), true);

		// Token: 0x0400066F RID: 1647
		public static GlobalSettingsPropertyDefinition SyncLogDirectory = GlobalSettingsSchema.CreatePropertyDefinition("SyncLogDirectory", typeof(string), string.Empty);

		// Token: 0x04000670 RID: 1648
		public static GlobalSettingsPropertyDefinition EnableCredentialRequest = GlobalSettingsSchema.CreatePropertyDefinition("EnableCredentialRequest", typeof(bool), true);

		// Token: 0x04000671 RID: 1649
		public static GlobalSettingsPropertyDefinition EnableMailboxLoggingVerboseMode = GlobalSettingsSchema.CreatePropertyDefinition("EnableMailboxLoggingVerboseMode", typeof(bool), false, false, delegate(GlobalSettingsPropertyDefinition propDef)
		{
			bool flightingSetting = GlobalSettingsSchema.GetFlightingSetting(propDef, VariantConfiguration.InvariantNoFlightingSnapshot.ActiveSync.MailboxLoggingVerboseMode);
			if (flightingSetting)
			{
				string appSetting = GlobalSettingsSchema.GetAppSetting(propDef);
				bool flag;
				return bool.TryParse(appSetting, out flag) ? flag : ((bool)propDef.DefaultValue);
			}
			return (bool)propDef.DefaultValue;
		});

		// Token: 0x04000672 RID: 1650
		public static GlobalSettingsPropertyDefinition SchemaDirectory = GlobalSettingsSchema.CreatePropertyDefinition("SchemaDirectory", typeof(string), string.Empty, true, null);

		// Token: 0x04000673 RID: 1651
		public static GlobalSettingsPropertyDefinition SchemaValidate = GlobalSettingsSchema.CreatePropertyDefinition("SchemaValidate", typeof(bool), true, true, (GlobalSettingsPropertyDefinition prop) => !string.Equals(GlobalSettingsSchema.GetAppSetting(prop), "off", StringComparison.OrdinalIgnoreCase));

		// Token: 0x04000674 RID: 1652
		public static GlobalSettingsPropertyDefinition BlockNewMailboxes = GlobalSettingsSchema.CreatePropertyDefinition("BlockNewMailboxes", typeof(bool), false);

		// Token: 0x04000675 RID: 1653
		public static GlobalSettingsPropertyDefinition BlockLegacyMailboxes = GlobalSettingsSchema.CreatePropertyDefinition("BlockLegacyMailboxes", typeof(bool), false);

		// Token: 0x04000676 RID: 1654
		public static GlobalSettingsPropertyDefinition SkipAzureADCall = GlobalSettingsSchema.CreatePropertyDefinition("SkipAzureADCall", typeof(bool), false);

		// Token: 0x04000677 RID: 1655
		public static GlobalSettingsPropertyDefinition ProxyVirtualDirectory = GlobalSettingsSchema.CreatePropertyDefinition("ProxyVirtualDirectory", typeof(string), "/Microsoft-Server-ActiveSync");

		// Token: 0x04000678 RID: 1656
		public static GlobalSettingsPropertyDefinition BackOffThreshold = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("BackOffThreshold", 3, 1, 99999999);

		// Token: 0x04000679 RID: 1657
		public static GlobalSettingsPropertyDefinition BackOffTimeOut = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("BackOffTimeOut", 60, 1, 600);

		// Token: 0x0400067A RID: 1658
		public static GlobalSettingsPropertyDefinition BackOffErrorWindow = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("BackOffErrorWindow", 60, 1, 600);

		// Token: 0x0400067B RID: 1659
		public static GlobalSettingsPropertyDefinition ProxyHandlerLongTimeout = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("ProxyHandlerLongTimeout", TimeSpan.FromSeconds(3600.0), TimeSpan.FromSeconds(60.0), TimeSpan.FromSeconds(9999.0), false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ConvertSecondsToTimeSpan));

		// Token: 0x0400067C RID: 1660
		public static GlobalSettingsPropertyDefinition ProxyHandlerShortTimeout = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("ProxyHandlerShortTimeout", TimeSpan.FromSeconds(120.0), TimeSpan.FromSeconds(12.0), TimeSpan.FromSeconds(600.0), false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ConvertSecondsToTimeSpan));

		// Token: 0x0400067D RID: 1661
		public static GlobalSettingsPropertyDefinition EarlyCompletionTolerance = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("EarlyCompletionTolerance", 500, 0, int.MaxValue);

		// Token: 0x0400067E RID: 1662
		public static GlobalSettingsPropertyDefinition EarlyWakeupBufferTime = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("EarlyWakeupBufferTime", 10, 0, 59);

		// Token: 0x0400067F RID: 1663
		public static GlobalSettingsPropertyDefinition ErrorResponseDelay = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("ErrorResponseDelay", 20, 0, 59);

		// Token: 0x04000680 RID: 1664
		public static GlobalSettingsPropertyDefinition MaxThrottlingDelay = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MaxThrottlingDelay", 70, 0, 300);

		// Token: 0x04000681 RID: 1665
		public static GlobalSettingsPropertyDefinition ProxyConnectionPoolConnectionLimit = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("ProxyConnectionPoolConnectionLimit", 50000, 48, 200000);

		// Token: 0x04000682 RID: 1666
		public static GlobalSettingsPropertyDefinition MaxCollectionsToLog = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MaxCollectionsToLog", 100, 0, 1000);

		// Token: 0x04000683 RID: 1667
		public static GlobalSettingsPropertyDefinition ProxyHeaders = GlobalSettingsSchema.CreatePropertyDefinition("ProxyHeaders", typeof(string[]), "PUBLIC,ALLOW,MS-SERVER-ACTIVESYNC,MS-ASPROTOCOLVERSIONS,MS-ASPROTOCOLCOMMANDS,CONTENT-TYPE,CONTENT-LENGTH,CONTENT-ENCODING".Split(new char[]
		{
			','
		}), false, delegate(GlobalSettingsPropertyDefinition propDef)
		{
			string appSetting = GlobalSettingsSchema.GetAppSetting(propDef);
			if (string.IsNullOrEmpty(appSetting))
			{
				return (string[])propDef.DefaultValue;
			}
			return appSetting.Split(new char[]
			{
				','
			});
		});

		// Token: 0x04000684 RID: 1668
		public static GlobalSettingsPropertyDefinition DeviceTypesSupportingRedirect = GlobalSettingsSchema.CreatePropertyDefinition("DeviceTypesSupportingRedirect", typeof(string[]), "iPod,iPad,iPhone,WindowsPhone,WP,WP8,WindowsMail,EASProbeDeviceType,BlackBerry".Split(new char[]
		{
			','
		}), false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.DeviceTypeParse));

		// Token: 0x04000685 RID: 1669
		public static GlobalSettingsPropertyDefinition DeviceTypesWithBasicMDMNotification = GlobalSettingsSchema.CreatePropertyDefinition("DeviceTypesWithBasicMDMNotification", typeof(List<string>), new List<string>
		{
			"ipod",
			"ipad",
			"iphone"
		}, false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ParseCommaSeparatedValuesIntoList));

		// Token: 0x04000686 RID: 1670
		public static GlobalSettingsPropertyDefinition DeviceTypesToParseOSVersion = GlobalSettingsSchema.CreatePropertyDefinition("DeviceTypesToParseOSVersion", typeof(List<string>), new List<string>
		{
			"samsung",
			"android"
		}, false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ParseCommaSeparatedValuesIntoList));

		// Token: 0x04000687 RID: 1671
		public static GlobalSettingsPropertyDefinition NegativeDeviceStatusCacheExpirationInterval = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("NegativeDeviceStatusCacheExpirationInterval", TimeSpan.FromSeconds(30.0), TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(86400.0), false, delegate(GlobalSettingsPropertyDefinition propDef)
		{
			if (TestHooks.GlobalSettings_GetFlightingSetting != null)
			{
				return TestHooks.GlobalSettings_GetFlightingSetting(propDef);
			}
			return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveSync.MdmNotification.NegativeDeviceStatusCacheExpirationInterval;
		});

		// Token: 0x04000688 RID: 1672
		public static GlobalSettingsPropertyDefinition DeviceStatusCacheExpirationInterval = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("DeviceStatusCacheExpirationInterval", TimeSpan.FromSeconds(900.0), TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(86400.0), false, delegate(GlobalSettingsPropertyDefinition propDef)
		{
			if (TestHooks.GlobalSettings_GetFlightingSetting != null)
			{
				return TestHooks.GlobalSettings_GetFlightingSetting(propDef);
			}
			return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveSync.MdmNotification.DeviceStatusCacheExpirationInternal;
		});

		// Token: 0x04000689 RID: 1673
		public static GlobalSettingsPropertyDefinition ValidSingleNamespaceUrls = GlobalSettingsSchema.CreatePropertyDefinition("ValidSingleNamespaceUrls", typeof(List<string>), new List<string>
		{
			"sdfpilot.outlook.com",
			"outlook.office365.com",
			"partner.outlook.cn"
		}, false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ParseCommaSeparatedValuesIntoList));

		// Token: 0x0400068A RID: 1674
		public static GlobalSettingsPropertyDefinition MdmEnrollmentUrl = GlobalSettingsSchema.CreatePropertyDefinition("MDMEnrollmentUrl", typeof(Uri), new Uri("http://go.microsoft.com/fwlink/?LinkId=396941"), false, delegate(GlobalSettingsPropertyDefinition propDef)
		{
			if (TestHooks.GlobalSettings_GetFlightingSetting != null)
			{
				return TestHooks.GlobalSettings_GetFlightingSetting(propDef);
			}
			return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveSync.MdmNotification.EnrollmentUrl;
		});

		// Token: 0x0400068B RID: 1675
		public static GlobalSettingsPropertyDefinition MdmActivationUrl = GlobalSettingsSchema.CreatePropertyDefinition("MDMActivationUrl", typeof(string), "https://{0}/manage/{1}/eas/activation?easid={2}&traceid={3}");

		// Token: 0x0400068C RID: 1676
		public static GlobalSettingsPropertyDefinition MdmComplianceStatusUrl = GlobalSettingsSchema.CreatePropertyDefinition("MDMComplianceStatusUrl", typeof(Uri), new Uri("http://go.microsoft.com/fwlink/?LinkId=397185"), false, delegate(GlobalSettingsPropertyDefinition propDef)
		{
			if (TestHooks.GlobalSettings_GetFlightingSetting != null)
			{
				return TestHooks.GlobalSettings_GetFlightingSetting(propDef);
			}
			return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveSync.MdmNotification.ComplianceStatusUrl;
		});

		// Token: 0x0400068D RID: 1677
		public static GlobalSettingsPropertyDefinition ADRegistrationServiceUrl = GlobalSettingsSchema.CreatePropertyDefinition("ADRegistrationServiceUrl", typeof(string), "enterpriseregistration.windows.net", false, delegate(GlobalSettingsPropertyDefinition propDef)
		{
			if (TestHooks.GlobalSettings_GetFlightingSetting != null)
			{
				return TestHooks.GlobalSettings_GetFlightingSetting(propDef);
			}
			return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveSync.MdmNotification.ADRegistrationServiceHost;
		});

		// Token: 0x0400068E RID: 1678
		public static GlobalSettingsPropertyDefinition MdmEnrollmentUrlWithBasicSteps = GlobalSettingsSchema.CreatePropertyDefinition("MdmEnrollmentUrlWithBasicSteps", typeof(Uri), new Uri("http://aka.ms/deviceenroll?easID={0}"), false, delegate(GlobalSettingsPropertyDefinition propDef)
		{
			if (TestHooks.GlobalSettings_GetFlightingSetting != null)
			{
				return TestHooks.GlobalSettings_GetFlightingSetting(propDef);
			}
			return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveSync.MdmNotification.EnrollmentUrlWithBasicSteps;
		});

		// Token: 0x0400068F RID: 1679
		public static GlobalSettingsPropertyDefinition DisableDeviceHealthStatusCache = GlobalSettingsSchema.CreatePropertyDefinition("DisableDeviceHealthStatusCache", typeof(bool), false);

		// Token: 0x04000690 RID: 1680
		public static GlobalSettingsPropertyDefinition DisableAadClientCache = GlobalSettingsSchema.CreatePropertyDefinition("DisableAadClientCache", typeof(bool), false);

		// Token: 0x04000691 RID: 1681
		public static GlobalSettingsPropertyDefinition MdmActivationUrlWithBasicSteps = GlobalSettingsSchema.CreatePropertyDefinition("MdmActivationUrlWithBasicSteps", typeof(string), "companyportal://enrollment/mapping?easID={0}", false, delegate(GlobalSettingsPropertyDefinition propDef)
		{
			if (TestHooks.GlobalSettings_GetFlightingSetting != null)
			{
				return TestHooks.GlobalSettings_GetFlightingSetting(propDef);
			}
			return VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveSync.MdmNotification.ActivationUrlWithBasicSteps;
		});

		// Token: 0x04000692 RID: 1682
		public static GlobalSettingsPropertyDefinition MaxWorkerThreadsPerProc = GlobalSettingsSchema.CreateMaxWorkerThreadsPerProc();

		// Token: 0x04000693 RID: 1683
		public static GlobalSettingsPropertyDefinition MaxRequestsQueued = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MaxRequestsQueued", 500, 1, 10000);

		// Token: 0x04000694 RID: 1684
		public static GlobalSettingsPropertyDefinition MaxMailboxSearchResults = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MaxMailboxSearchResults", 100, 10, 100000);

		// Token: 0x04000695 RID: 1685
		public static GlobalSettingsPropertyDefinition MailboxSearchTimeout = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("MailboxSearchTimeout", TimeSpan.FromSeconds(30.0), TimeSpan.FromSeconds(2.0), TimeSpan.FromSeconds(150.0), false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ConvertSecondsToTimeSpan));

		// Token: 0x04000696 RID: 1686
		public static GlobalSettingsPropertyDefinition MailboxSessionCacheInitialSize = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MailboxSessionCacheInitialSize", 100, 0, 32000);

		// Token: 0x04000697 RID: 1687
		public static GlobalSettingsPropertyDefinition MailboxSessionCacheMaxSize = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MailboxSessionCacheMaxSize", 1000, 1, 32000);

		// Token: 0x04000698 RID: 1688
		public static GlobalSettingsPropertyDefinition MailboxSessionCacheTimeout = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("MailboxSessionCacheTimeout", TimeSpan.FromMinutes(15.0), TimeSpan.Zero, TimeSpan.FromMinutes(1440.0), false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ConvertMinutesToTimeSpan));

		// Token: 0x04000699 RID: 1689
		public static GlobalSettingsPropertyDefinition MailboxSearchTimeoutNoContentIndexing = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("MailboxSearchTimeoutNoContentIndexing", TimeSpan.FromSeconds(90.0), TimeSpan.FromSeconds(3.0), TimeSpan.FromSeconds(300.0), false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ConvertSecondsToTimeSpan));

		// Token: 0x0400069A RID: 1690
		public static GlobalSettingsPropertyDefinition MaxClientSentBadItems = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MaxClientSentBadItems", 4, 1, 10000);

		// Token: 0x0400069B RID: 1691
		public static GlobalSettingsPropertyDefinition BadItemIncludeStackTrace = GlobalSettingsSchema.CreatePropertyDefinition("BadItemIncludeStackTrace", typeof(bool), false);

		// Token: 0x0400069C RID: 1692
		public static GlobalSettingsPropertyDefinition BadItemIncludeEmailToText = GlobalSettingsSchema.CreatePropertyDefinition("BadItemIncludeEmailToText", typeof(bool), false);

		// Token: 0x0400069D RID: 1693
		public static GlobalSettingsPropertyDefinition BadItemEmailToText = GlobalSettingsSchema.CreatePropertyDefinition("BadItemEmailToText", typeof(string), string.Empty, false, (GlobalSettingsPropertyDefinition propDef) => GlobalSettingsSchema.GetAppSetting(propDef) ?? string.Empty);

		// Token: 0x0400069E RID: 1694
		public static GlobalSettingsPropertyDefinition SupportedIPMTypes = GlobalSettingsSchema.CreatePropertyDefinition("SupportedIPMTypes", typeof(List<string>), new List<string>(), true, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ParseSupportedIPMTypes));

		// Token: 0x0400069F RID: 1695
		public static GlobalSettingsPropertyDefinition MaxGALSearchResults = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MaxGALSearchResults", 100, 10, 1000);

		// Token: 0x040006A0 RID: 1696
		public static GlobalSettingsPropertyDefinition MaxDocumentLibrarySearchResults = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MaxDocumentLibrarySearchResults", 1000, 10, 10000);

		// Token: 0x040006A1 RID: 1697
		public static GlobalSettingsPropertyDefinition MaxDocumentDataSize = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MaxDocumentDataSize", 10240000, 10240, int.MaxValue);

		// Token: 0x040006A2 RID: 1698
		public static GlobalSettingsPropertyDefinition MaxSMimeADDistributionListExpansion = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MaxSMimeADDistributionListExpansion", 2000, 5, 65535);

		// Token: 0x040006A3 RID: 1699
		public static GlobalSettingsPropertyDefinition IrmEnabled = GlobalSettingsSchema.CreatePropertyDefinition("IrmEnabled", typeof(bool), true);

		// Token: 0x040006A4 RID: 1700
		public static GlobalSettingsPropertyDefinition MaxRmsTemplates = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MaxRmsTemplates", 20, 0, 50);

		// Token: 0x040006A5 RID: 1701
		public static GlobalSettingsPropertyDefinition NegativeRmsTemplateCacheExpirationInterval = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("NegativeRmsTemplateCacheExpirationInterval", TimeSpan.FromSeconds(300.0), TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(86400.0), false, delegate(GlobalSettingsPropertyDefinition propDef)
		{
			string appSetting = GlobalSettingsSchema.GetAppSetting(propDef);
			int num;
			return int.TryParse(appSetting, out num) ? TimeSpan.FromSeconds((double)num) : ((TimeSpan)propDef.DefaultValue);
		});

		// Token: 0x040006A6 RID: 1702
		public static GlobalSettingsPropertyDefinition HeartBeatInterval = GlobalSettingsSchema.CreatePropertyDefinition("HeartbeatInterval", typeof(HeartBeatInterval), Microsoft.Exchange.AirSync.HeartBeatInterval.Default, false, (GlobalSettingsPropertyDefinition propDef) => Microsoft.Exchange.AirSync.HeartBeatInterval.Read());

		// Token: 0x040006A7 RID: 1703
		public static GlobalSettingsPropertyDefinition HeartbeatSampleSize = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("HeartbeatSampleSize", 200, 0, 10000);

		// Token: 0x040006A8 RID: 1704
		public static GlobalSettingsPropertyDefinition HeartbeatAlertThreshold = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("HeartbeatAlertThreshold", 540, 0, 3540);

		// Token: 0x040006A9 RID: 1705
		public static GlobalSettingsPropertyDefinition ADCacheExpirationTimeout = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("ADCacheExpirationTimeout", TimeSpan.FromSeconds(300.0), TimeSpan.FromSeconds(1.0), TimeSpan.MaxValue, false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ConvertSecondsToTimeSpan));

		// Token: 0x040006AA RID: 1706
		public static GlobalSettingsPropertyDefinition ADCacheMaxOrgCount = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("ADCacheMaxOrgCount", 50000, 1, int.MaxValue);

		// Token: 0x040006AB RID: 1707
		public static GlobalSettingsPropertyDefinition FullServerVersion = GlobalSettingsSchema.CreatePropertyDefinition("FullServerVersion", typeof(bool), false);

		// Token: 0x040006AC RID: 1708
		public static GlobalSettingsPropertyDefinition VdirCacheTimeoutSeconds = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("VdirCacheTimeoutSeconds", TimeSpan.FromSeconds(900.0), TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(2147483647.0), false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ConvertSecondsToTimeSpan));

		// Token: 0x040006AD RID: 1709
		public static GlobalSettingsPropertyDefinition AllowProxyingWithoutSsl = GlobalSettingsSchema.CreatePropertyDefinition("AllowProxyingWithoutSsl", typeof(bool), false, false, (GlobalSettingsPropertyDefinition propDef) => GlobalSettingsSchema.LoadRegistryBool(propDef, "SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA"));

		// Token: 0x040006AE RID: 1710
		public static GlobalSettingsPropertyDefinition HDPhotoCacheMaxSize = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("HDPhotoCacheMaxSize", 5000, 1, int.MaxValue);

		// Token: 0x040006AF RID: 1711
		public static GlobalSettingsPropertyDefinition HDPhotoCacheExpirationTimeOut = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("HDPhotoCacheExpirationTimeOut", TimeSpan.FromSeconds(900.0), TimeSpan.FromSeconds(60.0), TimeSpan.FromSeconds(2147483647.0), false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ConvertSecondsToTimeSpan));

		// Token: 0x040006B0 RID: 1712
		public static GlobalSettingsPropertyDefinition MaxRequestExecutionTime = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("MaxRequestExecutionTime", TimeSpan.FromSeconds(90.0), TimeSpan.FromSeconds(0.0), TimeSpan.FromSeconds(120.0), false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ConvertSecondsToTimeSpan));

		// Token: 0x040006B1 RID: 1713
		public static GlobalSettingsPropertyDefinition HDPhotoDefaultSupportedResolution = GlobalSettingsSchema.CreatePropertyDefinition("HDPhotoDefaultSupportedResolution", typeof(UserPhotoResolution), UserPhotoResolution.HR96x96);

		// Token: 0x040006B2 RID: 1714
		public static GlobalSettingsPropertyDefinition HDPhotoMaxNumberOfRequestsToProcess = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("HDPhotoMaxNumberOfRequestsToProcess", 100, 0, int.MaxValue);

		// Token: 0x040006B3 RID: 1715
		public static GlobalSettingsPropertyDefinition AllowInternalUntrustedCerts = GlobalSettingsSchema.CreatePropertyDefinition("AllowInternalUntrustedCerts", typeof(bool), true, false, (GlobalSettingsPropertyDefinition propDef) => GlobalSettingsSchema.LoadRegistryBool(propDef, "SYSTEM\\CurrentControlSet\\Services\\MSExchange OWA"));

		// Token: 0x040006B4 RID: 1716
		public static GlobalSettingsPropertyDefinition AllowDirectPush = GlobalSettingsSchema.CreatePropertyDefinition("AllowDirectPush", typeof(GlobalSettings.DirectPushEnabled), GlobalSettings.DirectPushEnabled.On, false, delegate(GlobalSettingsPropertyDefinition propDef)
		{
			GlobalSettings.DirectPushEnabled directPushEnabled = (GlobalSettings.DirectPushEnabled)GlobalSettingsSchema.LoadRegistryInt(propDef, "SYSTEM\\CurrentControlSet\\Services\\MSExchange ActiveSync");
			if (directPushEnabled >= GlobalSettings.DirectPushEnabled.Off && directPushEnabled <= GlobalSettings.DirectPushEnabled.OnWithAddressCheck)
			{
				return directPushEnabled;
			}
			return propDef.DefaultValue;
		});

		// Token: 0x040006B5 RID: 1717
		public static GlobalSettingsPropertyDefinition IsMultiTenancyEnabled = GlobalSettingsSchema.CreatePropertyDefinition("IsMultiTenancyEnabled", typeof(bool), false, false, (GlobalSettingsPropertyDefinition propDef) => GlobalSettingsSchema.GetFlightingSetting(propDef, VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy));

		// Token: 0x040006B6 RID: 1718
		public static GlobalSettingsPropertyDefinition IsWindowsLiveIDEnabled = GlobalSettingsSchema.CreatePropertyDefinition("IsWindowsLiveIDEnabled", typeof(bool), false, false, (GlobalSettingsPropertyDefinition propDef) => GlobalSettingsSchema.GetFlightingSetting(propDef, VariantConfiguration.InvariantNoFlightingSnapshot.Global.WindowsLiveID));

		// Token: 0x040006B7 RID: 1719
		public static GlobalSettingsPropertyDefinition IsGCCEnabled = GlobalSettingsSchema.CreatePropertyDefinition("IsGCCEnabled", typeof(bool), false, false, (GlobalSettingsPropertyDefinition propDef) => GlobalSettingsSchema.GetFlightingSetting(propDef, VariantConfiguration.InvariantNoFlightingSnapshot.ActiveSync.GlobalCriminalCompliance));

		// Token: 0x040006B8 RID: 1720
		public static GlobalSettingsPropertyDefinition AreGccStoredSecretKeysValid = GlobalSettingsSchema.CreatePropertyDefinition("AreGccStoredSecretKeysValid", typeof(bool), false, false, (GlobalSettingsPropertyDefinition propDef) => GlobalSettingsSchema.GetFlightingSetting(propDef, VariantConfiguration.InvariantNoFlightingSnapshot.ActiveSync.GlobalCriminalCompliance) && GlobalSettingsSchema.GccGetStoredSecretKeysValid());

		// Token: 0x040006B9 RID: 1721
		public static GlobalSettingsPropertyDefinition BootstrapCABForWM61HostingURL = GlobalSettingsSchema.CreatePropertyDefinition("BootstrapCABForWM61HostingURL", typeof(string), "http://go.microsoft.com/fwlink/?LinkId=150061");

		// Token: 0x040006BA RID: 1722
		public static GlobalSettingsPropertyDefinition MobileUpdateInformationURL = GlobalSettingsSchema.CreatePropertyDefinition("MobileUpdateInformationURL", typeof(string), "http://go.microsoft.com/fwlink/?LinkId=143155");

		// Token: 0x040006BB RID: 1723
		public static GlobalSettingsPropertyDefinition AutoBlockWriteToAd = GlobalSettingsSchema.CreatePropertyDefinition("AutoBlockWriteToAd", typeof(bool), true);

		// Token: 0x040006BC RID: 1724
		public static GlobalSettingsPropertyDefinition AutoBlockADWriteDelay = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("AutoBlockADWriteDelay", 3, 0, 24);

		// Token: 0x040006BD RID: 1725
		public static GlobalSettingsPropertyDefinition ADDataSyncInterval = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("ADDataSyncInterval", 24, 0, 168);

		// Token: 0x040006BE RID: 1726
		public static GlobalSettingsPropertyDefinition DeviceBehaviorCacheInitialSize = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("DeviceBehaviorCacheInitialSize", 100, 0, 32000);

		// Token: 0x040006BF RID: 1727
		public static GlobalSettingsPropertyDefinition DeviceBehaviorCacheMaxSize = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("DeviceBehaviorCacheMaxSize", 1000, 1, 32000);

		// Token: 0x040006C0 RID: 1728
		public static GlobalSettingsPropertyDefinition DeviceBehaviorCacheTimeout = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("DeviceBehaviorCacheTimeout", 15, 0, 1440);

		// Token: 0x040006C1 RID: 1729
		public static GlobalSettingsPropertyDefinition BootstrapMailDeliveryDelay = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("BootstrapMailDeliveryDelay", 259200, 0, int.MaxValue);

		// Token: 0x040006C2 RID: 1730
		public static GlobalSettingsPropertyDefinition UpgradeGracePeriod = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("UpgradeGracePeriod", 10080, 0, int.MaxValue);

		// Token: 0x040006C3 RID: 1731
		public static GlobalSettingsPropertyDefinition DeviceDiscoveryPeriod = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("DeviceDiscoveryPeriod", 14, 0, int.MaxValue);

		// Token: 0x040006C4 RID: 1732
		public static GlobalSettingsPropertyDefinition OnlyOrganizersCanSendMeetingChanges = GlobalSettingsSchema.CreatePropertyDefinition("OnlyOrganizersCanSendMeetingChanges", typeof(bool), true);

		// Token: 0x040006C5 RID: 1733
		public static GlobalSettingsPropertyDefinition IsPartnerHostedOnly = GlobalSettingsSchema.CreatePropertyDefinition("IsPartnerHostedOnly", typeof(bool), false, false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.GetPartnerHostedOnly));

		// Token: 0x040006C6 RID: 1734
		public static GlobalSettingsPropertyDefinition ExternalProxy = GlobalSettingsSchema.CreatePropertyDefinition("ExternalProxy", typeof(string), string.Empty);

		// Token: 0x040006C7 RID: 1735
		public static GlobalSettingsPropertyDefinition WriteActivityContextDiagnostics = GlobalSettingsSchema.CreatePropertyDefinition("WriteActivityContextDiagnostics", typeof(bool), false);

		// Token: 0x040006C8 RID: 1736
		public static GlobalSettingsPropertyDefinition WriteBudgetDiagnostics = GlobalSettingsSchema.CreatePropertyDefinition("WriteBudgetDiagnostics", typeof(bool), false);

		// Token: 0x040006C9 RID: 1737
		public static GlobalSettingsPropertyDefinition WriteExceptionDiagnostics = GlobalSettingsSchema.CreatePropertyDefinition("WriteExceptionDiagnostics", typeof(bool), false);

		// Token: 0x040006CA RID: 1738
		public static GlobalSettingsPropertyDefinition LogCompressedExceptionDetails = GlobalSettingsSchema.CreatePropertyDefinition("LogCompressedExceptionDetails", typeof(bool), true);

		// Token: 0x040006CB RID: 1739
		public static GlobalSettingsPropertyDefinition IncludeRequestInWatson = GlobalSettingsSchema.CreatePropertyDefinition("IncludeRequestInWatson", typeof(bool), false);

		// Token: 0x040006CC RID: 1740
		public static GlobalSettingsPropertyDefinition SendWatsonReport = GlobalSettingsSchema.CreateVDirPropertyDefinition<bool>("SendWatsonReport", true, (ADMobileVirtualDirectory vdir) => vdir.SendWatsonReport);

		// Token: 0x040006CD RID: 1741
		public static GlobalSettingsPropertyDefinition MeetingOrganizerCleanupTime = GlobalSettingsSchema.CreatePropertyDefinition("MeetingOrganizerCleanupTime", typeof(TimeSpan), TimeSpan.FromDays(1.0));

		// Token: 0x040006CE RID: 1742
		public static GlobalSettingsPropertyDefinition MeetingOrganizerEntryLiveTime = GlobalSettingsSchema.CreatePropertyDefinition("MeetingOrganizerEntryLiveTime", typeof(TimeSpan), TimeSpan.FromDays(7.0));

		// Token: 0x040006CF RID: 1743
		public static GlobalSettingsPropertyDefinition TimeTrackingEnabled = GlobalSettingsSchema.CreatePropertyDefinition("TimeTrackingEnabled", typeof(bool), true);

		// Token: 0x040006D0 RID: 1744
		public static GlobalSettingsPropertyDefinition MinGALSearchLength = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<int>("MinGALSearchLength", 4, 1, 256);

		// Token: 0x040006D1 RID: 1745
		public static GlobalSettingsPropertyDefinition RemoteDocumentsAllowedServers = GlobalSettingsSchema.CreateVDirPropertyDefinition<MultiValuedProperty<string>>("RemoteDocumentsAllowedServers", null, (ADMobileVirtualDirectory vdir) => vdir.RemoteDocumentsAllowedServers);

		// Token: 0x040006D2 RID: 1746
		public static GlobalSettingsPropertyDefinition RemoteDocumentsActionForUnknownServers = GlobalSettingsSchema.CreateVDirPropertyDefinition<RemoteDocumentsActions?>("RemoteDocumentsActionForUnknownServers", null, (ADMobileVirtualDirectory vdir) => vdir.RemoteDocumentsActionForUnknownServers);

		// Token: 0x040006D3 RID: 1747
		public static GlobalSettingsPropertyDefinition RemoteDocumentsBlockedServers = GlobalSettingsSchema.CreateVDirPropertyDefinition<MultiValuedProperty<string>>("RemoteDocumentsBlockedServers", null, (ADMobileVirtualDirectory vdir) => vdir.RemoteDocumentsBlockedServers);

		// Token: 0x040006D4 RID: 1748
		public static GlobalSettingsPropertyDefinition RemoteDocumentsInternalDomainSuffixList = GlobalSettingsSchema.CreateVDirPropertyDefinition<MultiValuedProperty<string>>("RemoteDocumentsInternalDomainSuffixList", null, (ADMobileVirtualDirectory vdir) => vdir.RemoteDocumentsInternalDomainSuffixList);

		// Token: 0x040006D5 RID: 1749
		public static GlobalSettingsPropertyDefinition EnableV160 = GlobalSettingsSchema.CreatePropertyDefinition("EnableV160", typeof(bool), false);

		// Token: 0x040006D6 RID: 1750
		public static GlobalSettingsPropertyDefinition MaxBackoffDuration = GlobalSettingsSchema.CreateConstrainedPropertyDefinition<TimeSpan>("MaxBackOffDuration", TimeSpan.FromSeconds(3600.0), TimeSpan.Zero, TimeSpan.FromSeconds(86400.0), false, new Func<GlobalSettingsPropertyDefinition, object>(GlobalSettingsSchema.ConvertSecondsToTimeSpan));

		// Token: 0x040006D7 RID: 1751
		public static GlobalSettingsPropertyDefinition AddBackOffReasonHeader = GlobalSettingsSchema.CreatePropertyDefinition("AddBackOffReasonHeader", typeof(bool), false);

		// Token: 0x040006D8 RID: 1752
		public static GlobalSettingsPropertyDefinition AllowFlightingOverrides = GlobalSettingsSchema.CreatePropertyDefinition("AllowFlightingOverrides", typeof(bool), false);

		// Token: 0x040006D9 RID: 1753
		public static GlobalSettingsPropertyDefinition DisableCharsetDetectionInCopyMessageContents = GlobalSettingsSchema.CreatePropertyDefinition("DisableCharsetDetectionInCopyMessageContents", typeof(bool), VariantConfiguration.InvariantNoFlightingSnapshot.ActiveSync.DisableCharsetDetectionInCopyMessageContents.Enabled, false, (GlobalSettingsPropertyDefinition propDef) => GlobalSettingsSchema.GetFlightingSetting(propDef, VariantConfiguration.InvariantNoFlightingSnapshot.ActiveSync.DisableCharsetDetectionInCopyMessageContents));

		// Token: 0x040006DA RID: 1754
		public static GlobalSettingsPropertyDefinition UseOAuthMasterSidForSecurityContext = GlobalSettingsSchema.CreatePropertyDefinition("UseOAuthMasterSidForSecurityContext", typeof(bool), VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveSync.UseOAuthMasterSidForSecurityContext.Enabled, false, (GlobalSettingsPropertyDefinition propDef) => GlobalSettingsSchema.GetFlightingSetting(propDef, VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveSync.UseOAuthMasterSidForSecurityContext));

		// Token: 0x040006DB RID: 1755
		public static GlobalSettingsPropertyDefinition GetGoidFromCalendarItemForMeetingResponse = GlobalSettingsSchema.CreatePropertyDefinition("GetGoidFromCalendarItemForMeetingResponse", typeof(bool), VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveSync.GetGoidFromCalendarItemForMeetingResponse.Enabled, false, (GlobalSettingsPropertyDefinition propDef) => GlobalSettingsSchema.GetFlightingSetting(propDef, VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).ActiveSync.GetGoidFromCalendarItemForMeetingResponse));

		// Token: 0x040006DC RID: 1756
		internal static GlobalSettingsPropertyDefinition Supporting_MinHeartbeatInterval = GlobalSettingsSchema.CreatePropertyDefinition("MinHeartbeatInterval", typeof(int), 60);

		// Token: 0x040006DD RID: 1757
		internal static GlobalSettingsPropertyDefinition Supporting_MaxHeartbeatInterval = GlobalSettingsSchema.CreatePropertyDefinition("MaxHeartbeatInterval", typeof(int), 3540);

		// Token: 0x040006DE RID: 1758
		internal static GlobalSettingsPropertyDefinition ClientAccessRulesLogPeriodicEvent = GlobalSettingsSchema.CreatePropertyDefinition("ClientAccessRulesLogPeriodicEvent", typeof(bool), false);

		// Token: 0x040006DF RID: 1759
		internal static GlobalSettingsPropertyDefinition ClientAccessRulesLatencyThreshold = GlobalSettingsSchema.CreatePropertyDefinition("ClientAccessRulesLatencyThreshold", typeof(double), 50.0);
	}
}
