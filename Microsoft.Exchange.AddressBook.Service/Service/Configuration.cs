using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using Microsoft.Exchange.AddressBook.EventLog;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AddressBook.Service;
using Microsoft.Win32;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x02000004 RID: 4
	internal static class Configuration
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002787 File Offset: 0x00000987
		// (set) Token: 0x06000019 RID: 25 RVA: 0x0000278E File Offset: 0x0000098E
		internal static string NspiHttpPort { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002796 File Offset: 0x00000996
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000279D File Offset: 0x0000099D
		internal static string RfrHttpPort { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000027A5 File Offset: 0x000009A5
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000027AC File Offset: 0x000009AC
		internal static bool EnablePhoneticSort { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000027B4 File Offset: 0x000009B4
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000027BB File Offset: 0x000009BB
		internal static bool ProtocolLoggingEnabled { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000027C3 File Offset: 0x000009C3
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000027CA File Offset: 0x000009CA
		internal static bool ApplyHourPrecision { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000027D2 File Offset: 0x000009D2
		// (set) Token: 0x06000023 RID: 35 RVA: 0x000027D9 File Offset: 0x000009D9
		internal static ByteQuantifiedSize MaxDirectorySize { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000027E1 File Offset: 0x000009E1
		// (set) Token: 0x06000025 RID: 37 RVA: 0x000027E8 File Offset: 0x000009E8
		internal static string LogFilePath { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000027F0 File Offset: 0x000009F0
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000027F7 File Offset: 0x000009F7
		internal static int MaxRetentionPeriod { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000028 RID: 40 RVA: 0x000027FF File Offset: 0x000009FF
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002806 File Offset: 0x00000A06
		internal static ByteQuantifiedSize PerFileMaxSize { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000280E File Offset: 0x00000A0E
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002815 File Offset: 0x00000A15
		internal static bool CrashOnUnhandledException { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002C RID: 44 RVA: 0x0000281D File Offset: 0x00000A1D
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002824 File Offset: 0x00000A24
		internal static int ModCacheExpiryTimeInSeconds { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000282C File Offset: 0x00000A2C
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002833 File Offset: 0x00000A33
		internal static string NspiTestServer { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000283B File Offset: 0x00000A3B
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002842 File Offset: 0x00000A42
		internal static bool SuppressNspiEndpointRegistration { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000284A File Offset: 0x00000A4A
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002851 File Offset: 0x00000A51
		internal static int AverageLatencySamples { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002859 File Offset: 0x00000A59
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002860 File Offset: 0x00000A60
		internal static bool EncryptionRequired { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002868 File Offset: 0x00000A68
		// (set) Token: 0x06000037 RID: 55 RVA: 0x0000286F File Offset: 0x00000A6F
		internal static bool ServiceEnabled { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002877 File Offset: 0x00000A77
		// (set) Token: 0x06000039 RID: 57 RVA: 0x0000287E File Offset: 0x00000A7E
		internal static ADObjectId MicrosoftExchangeConfigurationRoot { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002886 File Offset: 0x00000A86
		// (set) Token: 0x0600003B RID: 59 RVA: 0x0000288D File Offset: 0x00000A8D
		internal static ADObjectId ConfigNamingContext { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002895 File Offset: 0x00000A95
		// (set) Token: 0x0600003D RID: 61 RVA: 0x0000289C File Offset: 0x00000A9C
		internal static TimeSpan? ADTimeout { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000028A4 File Offset: 0x00000AA4
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000028AB File Offset: 0x00000AAB
		internal static TimeSpan MaxExecutionTime { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000028B3 File Offset: 0x00000AB3
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000028BA File Offset: 0x00000ABA
		internal static bool UseDefaultAppConfig
		{
			get
			{
				return Configuration.useDefaultAppConfig;
			}
			set
			{
				Configuration.useDefaultAppConfig = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000028C2 File Offset: 0x00000AC2
		// (set) Token: 0x06000043 RID: 67 RVA: 0x000028C9 File Offset: 0x00000AC9
		internal static bool IsDatacenter { get; private set; }

		// Token: 0x06000044 RID: 68 RVA: 0x000028D4 File Offset: 0x00000AD4
		internal static void Initialize(ExEventLog eventLog, Action stopService)
		{
			Configuration.GeneralTracer.TraceDebug(0L, "Configuration.Initialize");
			Configuration.ServiceEnabled = true;
			Configuration.stopService = stopService;
			Configuration.eventLog = eventLog;
			Configuration.EncryptionRequired = true;
			Configuration.LoadAppConfig();
			Configuration.LoadRegistryConfig();
			if (Configuration.LoadADConfiguration())
			{
				Configuration.ExchangeRpcClientAccessChangeNotification(null);
			}
			Configuration.IsDatacenter = Datacenter.IsMicrosoftHostedOnly(true);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000292C File Offset: 0x00000B2C
		internal static void Terminate()
		{
			if (Configuration.exchangeRCANotificationCookie != null)
			{
				ADNotificationAdapter.UnregisterChangeNotification(Configuration.exchangeRCANotificationCookie);
				Configuration.exchangeRCANotificationCookie = null;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000029D8 File Offset: 0x00000BD8
		private static bool LoadADConfiguration()
		{
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 196, "LoadADConfiguration", "f:\\15.00.1497\\sources\\dev\\DoMT\\src\\Service\\Configuration.cs");
				tenantOrTopologyConfigurationSession.ServerTimeout = Configuration.ADTimeout;
				ConfigurationContainer configurationContainer = tenantOrTopologyConfigurationSession.Read<ConfigurationContainer>(tenantOrTopologyConfigurationSession.ConfigurationNamingContext);
				if (configurationContainer != null)
				{
					Guid objectGuid = (Guid)configurationContainer[ADObjectSchema.Guid];
					Configuration.ConfigNamingContext = new ADObjectId(tenantOrTopologyConfigurationSession.ConfigurationNamingContext.DistinguishedName, objectGuid);
				}
				Configuration.MicrosoftExchangeConfigurationRoot = Configuration.ConfigNamingContext.GetDescendantId("Services", "Microsoft Exchange", new string[0]);
			});
			if (!adoperationResult.Succeeded)
			{
				Configuration.ServiceEnabled = false;
			}
			return Configuration.ServiceEnabled;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002A1C File Offset: 0x00000C1C
		private static void LoadAppConfig()
		{
			Configuration.settingsAppConfig = null;
			Configuration.settingsDefaultConfig = null;
			Configuration.ProtocolLoggingEnabled = Configuration.GetConfigBool("ProtocolLoggingEnabled", false);
			Configuration.ApplyHourPrecision = Configuration.GetConfigBool("ApplyHourPrecision", true);
			Configuration.MaxDirectorySize = Configuration.GetConfigByteQuantifiedSize("MaxDirectorySize", ByteQuantifiedSize.Parse("1GB"));
			Configuration.LogFilePath = Configuration.GetConfigString("LogFilePath", ProtocolLog.DefaultLogFilePath);
			Configuration.MaxRetentionPeriod = Configuration.GetConfigInt("MaxRetentionPeriod", 720);
			Configuration.PerFileMaxSize = Configuration.GetConfigByteQuantifiedSize("PerFileMaxSize", ByteQuantifiedSize.Parse("10MB"));
			Configuration.CrashOnUnhandledException = Configuration.GetConfigBool("CrashOnUnhandledException", false);
			Configuration.ModCacheExpiryTimeInSeconds = Configuration.GetConfigInt("ModCacheExpiryTimeInSeconds", 7200);
			Configuration.AverageLatencySamples = Configuration.GetConfigInt("AverageLatencySamples", 1024);
			Configuration.NspiTestServer = Configuration.GetConfigString("NspiTestServer", string.Empty);
			Configuration.SuppressNspiEndpointRegistration = Configuration.GetConfigBool("SuppressNspiEndpointRegistration", false);
			Configuration.ADTimeout = new TimeSpan?(TimeSpan.FromSeconds((double)Configuration.GetConfigInt("ADTimeout", 30)));
			Configuration.MaxExecutionTime = TimeSpan.FromSeconds((double)Configuration.GetConfigInt("MaxExecutionTime", 15));
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B40 File Offset: 0x00000D40
		private static void LoadRegistryConfig()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchangeAB\\Parameters"))
			{
				Configuration.NspiHttpPort = Configuration.GetRegistryString(registryKey, "NspiHttpPort", "6004");
				Configuration.RfrHttpPort = Configuration.GetRegistryString(registryKey, "RfrHttpPort", "6002");
				Configuration.EnablePhoneticSort = Configuration.GetRegistryBoolean(registryKey, "EnablePhoneticSort", false);
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002CF8 File Offset: 0x00000EF8
		private static void ExchangeRpcClientAccessChangeNotification(ADNotificationEventArgs args)
		{
			Configuration.GeneralTracer.TraceDebug(0L, "ExchangeRpcClientAccessChangeNotification called");
			ADNotificationAdapter.TryRunADOperation(delegate()
			{
				ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 280, "ExchangeRpcClientAccessChangeNotification", "f:\\15.00.1497\\sources\\dev\\DoMT\\src\\Service\\Configuration.cs");
				topologyConfigurationSession.ServerTimeout = Configuration.ADTimeout;
				Server server = topologyConfigurationSession.ReadLocalServer();
				if (server == null)
				{
					Configuration.GeneralTracer.TraceError(0L, "Failed to find local server in AD");
					Configuration.eventLog.LogEvent(AddressBookEventLogConstants.Tuple_FailedToFindLocalServerInAD, string.Empty, new object[0]);
					if (args == null)
					{
						Configuration.ServiceEnabled = false;
						return;
					}
					Configuration.stopService();
					return;
				}
				else
				{
					ExchangeRpcClientAccess exchangeRpcClientAccess = topologyConfigurationSession.Read<ExchangeRpcClientAccess>(ExchangeRpcClientAccess.FromServerId(server.Id));
					if (exchangeRpcClientAccess != null)
					{
						if (Configuration.exchangeRCANotificationCookie == null)
						{
							Configuration.exchangeRCANotificationCookie = ADNotificationAdapter.RegisterChangeNotification<ExchangeRpcClientAccess>(exchangeRpcClientAccess.Id, new ADNotificationCallback(Configuration.ExchangeRpcClientAccessChangeNotification));
						}
						if (Configuration.EncryptionRequired != exchangeRpcClientAccess.EncryptionRequired)
						{
							Configuration.GeneralTracer.TraceDebug<bool, bool>(0L, "Changing EncryptionRequired from {0} to {1}", Configuration.EncryptionRequired, exchangeRpcClientAccess.EncryptionRequired);
							Configuration.EncryptionRequired = exchangeRpcClientAccess.EncryptionRequired;
						}
						return;
					}
					Configuration.GeneralTracer.TraceDebug(0L, "ExchangeRpcClientAccess disabled");
					Configuration.eventLog.LogEvent(AddressBookEventLogConstants.Tuple_ExchangeRpcClientAccessDisabled, string.Empty, new object[0]);
					if (args == null)
					{
						Configuration.ServiceEnabled = false;
						return;
					}
					Configuration.stopService();
					return;
				}
			});
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D38 File Offset: 0x00000F38
		private static bool GetConfigBool(string key, bool defaultValue)
		{
			string configurationValue = Configuration.GetConfigurationValue(key);
			if (string.IsNullOrEmpty(configurationValue))
			{
				Configuration.GeneralTracer.TraceDebug<string, bool>(0L, "Config[{0}]: {1} (default)", key, defaultValue);
				return defaultValue;
			}
			if (configurationValue.Equals("true", StringComparison.OrdinalIgnoreCase))
			{
				Configuration.GeneralTracer.TraceDebug<string>(0L, "Config[{0}]: true", key);
				return true;
			}
			if (configurationValue.Equals("false", StringComparison.OrdinalIgnoreCase))
			{
				Configuration.GeneralTracer.TraceDebug<string>(0L, "Config[{0}]: false", key);
				return false;
			}
			Configuration.GeneralTracer.TraceError<string, string, bool>(0L, "Config[{0}]: {1} (Invalid: defaulting to {2})", key, configurationValue, defaultValue);
			return defaultValue;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002DC4 File Offset: 0x00000FC4
		private static int GetConfigInt(string key, int defaultValue)
		{
			string configurationValue = Configuration.GetConfigurationValue(key);
			int num;
			if (string.IsNullOrEmpty(configurationValue) || !int.TryParse(configurationValue, out num))
			{
				Configuration.GeneralTracer.TraceDebug<string, int>(0L, "Config[{0}]: {1} (default)", key, defaultValue);
				return defaultValue;
			}
			Configuration.GeneralTracer.TraceDebug<string, int>(0L, "Config[{0}]: {1}", key, num);
			return num;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E14 File Offset: 0x00001014
		private static string GetConfigString(string key, string defaultValue)
		{
			string configurationValue = Configuration.GetConfigurationValue(key);
			if (configurationValue == null)
			{
				Configuration.GeneralTracer.TraceDebug<string, string>(0L, "Config[{0}]: {1} (default)", key, defaultValue);
				return defaultValue;
			}
			Configuration.GeneralTracer.TraceDebug<string, string>(0L, "Config[{0}]: {1}", key, configurationValue);
			return configurationValue;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E54 File Offset: 0x00001054
		private static string GetRegistryString(RegistryKey regkey, string key, string defaultValue)
		{
			string text = null;
			if (regkey != null)
			{
				text = (regkey.GetValue(key) as string);
			}
			if (text == null)
			{
				Configuration.GeneralTracer.TraceDebug<string, string>(0L, "Registry[{0}]: {1} (default)", key, defaultValue);
				return defaultValue;
			}
			Configuration.GeneralTracer.TraceDebug<string, string>(0L, "Registry[{0}]: {1}", key, text);
			return text;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002EA0 File Offset: 0x000010A0
		private static bool GetRegistryBoolean(RegistryKey regkey, string key, bool defaultValue)
		{
			if (regkey == null)
			{
				Configuration.GeneralTracer.TraceDebug<string, bool>(0L, "Registry[{0}]: {1} (default)", key, defaultValue);
				return defaultValue;
			}
			bool result;
			try
			{
				result = Convert.ToBoolean(regkey.GetValue(key, defaultValue));
			}
			catch (Exception ex)
			{
				Configuration.GeneralTracer.TraceDebug<string>(0L, "Exception raised. Using default value. [Exception Message] ", ex.ToString());
				Configuration.GeneralTracer.TraceDebug<string, bool>(0L, "Registry[{0}]: {1} (default)", key, defaultValue);
				result = defaultValue;
			}
			return result;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002F1C File Offset: 0x0000111C
		private static ByteQuantifiedSize GetConfigByteQuantifiedSize(string key, ByteQuantifiedSize defaultValue)
		{
			string configurationValue = Configuration.GetConfigurationValue(key);
			ByteQuantifiedSize byteQuantifiedSize;
			if (string.IsNullOrEmpty(configurationValue) || !ByteQuantifiedSize.TryParse(configurationValue, out byteQuantifiedSize))
			{
				Configuration.GeneralTracer.TraceDebug<string, ByteQuantifiedSize>(0L, "Config[{0}]: {1} (default)", key, defaultValue);
				return defaultValue;
			}
			Configuration.GeneralTracer.TraceDebug<string, ByteQuantifiedSize>(0L, "Config[{0}]: {1}", key, byteQuantifiedSize);
			return byteQuantifiedSize;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002F6C File Offset: 0x0000116C
		private static string GetConfigurationValue(string key)
		{
			object settings = Configuration.Settings;
			if (settings != null)
			{
				KeyValueConfigurationCollection keyValueConfigurationCollection = settings as KeyValueConfigurationCollection;
				if (keyValueConfigurationCollection != null)
				{
					KeyValueConfigurationElement keyValueConfigurationElement = keyValueConfigurationCollection[key];
					if (keyValueConfigurationElement == null)
					{
						return null;
					}
					return keyValueConfigurationElement.Value;
				}
				else
				{
					NameValueCollection nameValueCollection = settings as NameValueCollection;
					if (nameValueCollection != null)
					{
						return nameValueCollection[key];
					}
				}
			}
			return null;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002FB4 File Offset: 0x000011B4
		private static object Settings
		{
			get
			{
				if (Configuration.UseDefaultAppConfig)
				{
					if (Configuration.settingsDefaultConfig == null)
					{
						ConfigurationManager.RefreshSection("appSettings");
						Configuration.settingsDefaultConfig = ConfigurationManager.AppSettings;
					}
					return Configuration.settingsDefaultConfig;
				}
				if (Configuration.settingsAppConfig == null)
				{
					string location = Assembly.GetExecutingAssembly().Location;
					Configuration.GeneralTracer.TraceDebug<string>(0L, "Loading configuration file: {0}", location);
					Configuration configuration = ConfigurationManager.OpenExeConfiguration(location);
					Configuration.settingsAppConfig = configuration.AppSettings.Settings;
				}
				return Configuration.settingsAppConfig;
			}
		}

		// Token: 0x0400000E RID: 14
		private const string RegistryParameters = "SYSTEM\\CurrentControlSet\\Services\\MSExchangeAB\\Parameters";

		// Token: 0x0400000F RID: 15
		private const string SectionName = "appSettings";

		// Token: 0x04000010 RID: 16
		internal static readonly Trace GeneralTracer = ExTraceGlobals.GeneralTracer;

		// Token: 0x04000011 RID: 17
		private static Action stopService;

		// Token: 0x04000012 RID: 18
		private static ExEventLog eventLog;

		// Token: 0x04000013 RID: 19
		private static ADNotificationRequestCookie exchangeRCANotificationCookie;

		// Token: 0x04000014 RID: 20
		private static bool useDefaultAppConfig = false;

		// Token: 0x04000015 RID: 21
		private static KeyValueConfigurationCollection settingsAppConfig = null;

		// Token: 0x04000016 RID: 22
		private static NameValueCollection settingsDefaultConfig = null;
	}
}
