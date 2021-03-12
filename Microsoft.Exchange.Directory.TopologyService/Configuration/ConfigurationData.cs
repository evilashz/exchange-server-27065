using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Directory.TopologyService;
using Microsoft.Exchange.Directory.TopologyService.EventLog;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Win32;

namespace Microsoft.Exchange.Directory.TopologyService.Configuration
{
	// Token: 0x0200002B RID: 43
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ConfigurationData
	{
		// Token: 0x06000162 RID: 354 RVA: 0x0000AC8C File Offset: 0x00008E8C
		protected ConfigurationData()
		{
			this.MinUserDC = ConfigurationData.MinUserDCDefault;
			this.DisableNetLogonCheck = ConfigurationData.DisableNetlogonCheckDefault;
			this.AllowPreW2KSP3DC = ConfigurationData.AllowPreW2KSP3DCDefault;
			this.ConfigDCAffinityInMinutes = ConfigurationData.CDCAffinityInMinutesDefault;
			this.ConfigurationDC = null;
			this.ExcludedDC = new string[0];
			this.DomainControllers = new List<string>(0);
			this.GlobalCatalogs = new List<string>(0);
			this.LoadConfigurationFromRegistry();
			this.registryWatcher = new RegistryWatcher("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess", true);
			this.registryTimer = new Timer(new TimerCallback(this.RegistryCheck), null, this.CheckRegistryEveryNMilliseconds, -1);
			this.tsc = (((ServiceConfigurationSection)ConfigurationManager.GetSection("Topology")) ?? new ServiceConfigurationSection());
			Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			string directoryName = Path.GetDirectoryName(configuration.FilePath);
			string fileName = Path.GetFileName(configuration.FilePath);
			this.watcher = new FileSystemWatcher
			{
				Path = directoryName,
				Filter = fileName,
				EnableRaisingEvents = true,
				NotifyFilter = NotifyFilters.LastWrite
			};
			this.watcher.Changed += this.ReloadFileConfiguration;
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000163 RID: 355 RVA: 0x0000ADE8 File Offset: 0x00008FE8
		// (remove) Token: 0x06000164 RID: 356 RVA: 0x0000AE20 File Offset: 0x00009020
		public event Action ConfigurationChanged = delegate()
		{
		};

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000AE55 File Offset: 0x00009055
		public static ConfigurationData Instance
		{
			get
			{
				return ConfigurationData.hookableInstance.Value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000AE61 File Offset: 0x00009061
		public int MinimumPrefixMatch
		{
			get
			{
				return this.tsc.MinimumPrefixMatch;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000AE6E File Offset: 0x0000906E
		public TimeSpan FullTopologyDiscoveryTimeout
		{
			get
			{
				return this.tsc.FullTopologyDiscoveryTimeout;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000AE7B File Offset: 0x0000907B
		public TimeSpan UrgentOrInitialTopologyTimeout
		{
			get
			{
				return this.tsc.UrgentOrInitialTopologyTimeout;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000AE88 File Offset: 0x00009088
		public TimeSpan DiscoveryFrequency
		{
			get
			{
				if (ConfigurationData.TopoRecheckSecondsDefault != this.topoRecheckInSeconds)
				{
					return TimeSpan.FromSeconds((double)this.topoRecheckInSeconds);
				}
				return this.tsc.DiscoveryFrequency;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000AEAF File Offset: 0x000090AF
		public TimeSpan DiscoveryFrequencyOnFailure
		{
			get
			{
				return this.tsc.DiscoveryFrequencyOnFailure;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000AEBC File Offset: 0x000090BC
		public TimeSpan DiscoveryFrequencyOnNoTopology
		{
			get
			{
				return this.tsc.DiscoveryFrequencyOnNoTopology;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600016C RID: 364 RVA: 0x0000AEC9 File Offset: 0x000090C9
		public TimeSpan DiscoveryFrequencyOnMinPercentageDC
		{
			get
			{
				return this.tsc.DiscoveryFrequencyOnMinPercentageDC;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000AED6 File Offset: 0x000090D6
		public TimeSpan WaitTimeBetweenInitialAndFullDiscovery
		{
			get
			{
				return this.tsc.WaitTimeBetweenInitialAndFullDiscovery;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600016E RID: 366 RVA: 0x0000AEE3 File Offset: 0x000090E3
		public TimeSpan ForestScanFrequency
		{
			get
			{
				return this.tsc.ForestScanFrequency;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000AEF0 File Offset: 0x000090F0
		public TimeSpan ForestScanFrequencyOnFailure
		{
			get
			{
				return this.tsc.ForestScanFrequencyOnFailure;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000170 RID: 368 RVA: 0x0000AEFD File Offset: 0x000090FD
		public TimeSpan ForestScanTimeout
		{
			get
			{
				return this.tsc.ForestScanTimeout;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000AF0A File Offset: 0x0000910A
		public TimeSpan SiteMonitorFrequency
		{
			get
			{
				return this.tsc.SiteMonitorFrequency;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000AF17 File Offset: 0x00009117
		public TimeSpan SiteMonitorFrequencyOnFailure
		{
			get
			{
				return this.tsc.SiteMonitorFrequencyOnFailure;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000AF24 File Offset: 0x00009124
		public TimeSpan SiteMonitorTimeout
		{
			get
			{
				return this.tsc.SiteMonitorTimeout;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000AF31 File Offset: 0x00009131
		public int MaxRemoteForestDiscoveryErrorsPerHour
		{
			get
			{
				return this.tsc.MaxRemoteForestDiscoveryErrorsPerHour;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000AF3E File Offset: 0x0000913E
		public TimeSpan RemoteDomainSingleServerDiscoveryTimeout
		{
			get
			{
				return this.tsc.RemoteDomainSingleServerDiscoveryTimeout;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000AF4B File Offset: 0x0000914B
		public TimeSpan ThrottleOnEmptyQueue
		{
			get
			{
				return this.tsc.ThrottleOnEmptyQueue;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000AF58 File Offset: 0x00009158
		public TimeSpan ThrottleOnFullQueue
		{
			get
			{
				return this.tsc.ThrottleOnFullQueue;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000178 RID: 376 RVA: 0x0000AF65 File Offset: 0x00009165
		public int MaxRunningTasks
		{
			get
			{
				return this.tsc.MaxRunningTasks;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000AF72 File Offset: 0x00009172
		public TimeSpan WaitAmountBeforeRestartRequest
		{
			get
			{
				return this.tsc.WaitAmountBeforeRestartRequest;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600017A RID: 378 RVA: 0x0000AF7F File Offset: 0x0000917F
		public TimeSpan LocalIPAddressesCacheRefreshInterval
		{
			get
			{
				return this.tsc.LocalIPAddressesCacheRefreshInterval;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000AF8C File Offset: 0x0000918C
		public int MaxAttemptsResetDCCache
		{
			get
			{
				return this.tsc.MaxAttemptsResetDCCache;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000AF99 File Offset: 0x00009199
		public TimeSpan MaintenanceModeDiscoveryTimeout
		{
			get
			{
				return this.tsc.MaintenanceModeDiscoveryTimeout;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000AFA6 File Offset: 0x000091A6
		public TimeSpan MaintenanceModeDiscoveryFrequency
		{
			get
			{
				return this.tsc.MaintenanceModeDiscoveryFrequency;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000AFB3 File Offset: 0x000091B3
		public string ExchangeTopologyCacheLifetime
		{
			get
			{
				return this.tsc.ExchangeTopologyCacheLifetime;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000AFC0 File Offset: 0x000091C0
		public string ExchangeTopologyCacheFrequency
		{
			get
			{
				return this.tsc.ExchangeTopologyCacheFrequency;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000AFCD File Offset: 0x000091CD
		public int MinPercentageOfHealthyDC
		{
			get
			{
				return this.tsc.MinPercentageOfHealthyDC;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000AFDA File Offset: 0x000091DA
		public int MinSuitableServer
		{
			get
			{
				if (!Globals.IsMicrosoftHostedOnly)
				{
					return 1;
				}
				return this.tsc.MinSuitableServer;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000AFF0 File Offset: 0x000091F0
		public bool EnableWholeForestDiscovery
		{
			get
			{
				return this.tsc.EnableWholeForestDiscovery || ExEnvironment.IsTest;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000B006 File Offset: 0x00009206
		public bool ForestWideAffinityRequested
		{
			get
			{
				return this.tsc.ForestWideAffinityRequested;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0000B013 File Offset: 0x00009213
		// (set) Token: 0x06000185 RID: 389 RVA: 0x0000B01B File Offset: 0x0000921B
		public int MinUserDC { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000B024 File Offset: 0x00009224
		// (set) Token: 0x06000187 RID: 391 RVA: 0x0000B02C File Offset: 0x0000922C
		public bool DisableNetLogonCheck { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000B035 File Offset: 0x00009235
		// (set) Token: 0x06000189 RID: 393 RVA: 0x0000B03D File Offset: 0x0000923D
		public string[] ExcludedDC { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000B046 File Offset: 0x00009246
		public bool IsPDCCheckEnabled
		{
			get
			{
				return ConfigurationData.MinUserDCDefault != this.MinUserDC;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000B058 File Offset: 0x00009258
		// (set) Token: 0x0600018C RID: 396 RVA: 0x0000B060 File Offset: 0x00009260
		public IList<string> DomainControllers { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000B069 File Offset: 0x00009269
		// (set) Token: 0x0600018E RID: 398 RVA: 0x0000B071 File Offset: 0x00009271
		public IList<string> GlobalCatalogs { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000B07A File Offset: 0x0000927A
		// (set) Token: 0x06000190 RID: 400 RVA: 0x0000B082 File Offset: 0x00009282
		public string ConfigurationDC { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000B08B File Offset: 0x0000928B
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0000B093 File Offset: 0x00009293
		public bool AllowPreW2KSP3DC { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000B09C File Offset: 0x0000929C
		// (set) Token: 0x06000194 RID: 404 RVA: 0x0000B0A4 File Offset: 0x000092A4
		public int ConfigDCAffinityInMinutes { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000B0AD File Offset: 0x000092AD
		protected virtual int NMillisecondsRegistryWaitTimeOnChange
		{
			get
			{
				return 60000;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000B0B4 File Offset: 0x000092B4
		protected virtual int CheckRegistryEveryNMilliseconds
		{
			get
			{
				return 30000;
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000B0BC File Offset: 0x000092BC
		public static void LogEvent(ExEventLog.EventTuple tuple, string periodicKey, params object[] messageArgs)
		{
			object[] array = new object[messageArgs.Length + 2];
			array[0] = Globals.ProcessName;
			array[1] = Globals.ProcessId;
			messageArgs.CopyTo(array, 2);
			ConfigurationData.logger.LogEvent(tuple, periodicKey, array);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000B100 File Offset: 0x00009300
		internal static void LogServiceStartingEvent(string message)
		{
			if (ConfigurationData.Instance.tsc.ServiceStartupLoggingEnabled)
			{
				ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_ServiceStarting, null, new object[]
				{
					message
				});
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000B138 File Offset: 0x00009338
		internal static void LogSecureChannelDCForDomain(string domainFqdn)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("domainFqdn", domainFqdn);
			string secureChannelDCForDomain = NativeHelpers.GetSecureChannelDCForDomain(domainFqdn, false);
			if (string.IsNullOrEmpty(secureChannelDCForDomain))
			{
				return;
			}
			ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_DSC_EVENT_SECURE_CHANNEL_DC, null, new object[]
			{
				domainFqdn,
				secureChannelDCForDomain
			});
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000B17C File Offset: 0x0000937C
		internal static IDisposable SetTestHook(ConfigurationData wrapper)
		{
			return ConfigurationData.hookableInstance.SetTestHook(wrapper);
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000B18C File Offset: 0x0000938C
		protected virtual bool LoadConfigurationFromRegistry()
		{
			Exception ex = null;
			bool result = false;
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess"))
				{
					if (registryKey != null)
					{
						int cdcaffinityInMinutesDefault = ConfigurationData.CDCAffinityInMinutesDefault;
						object value = registryKey.GetValue(ConfigurationData.CDCAffinityInMinutesKeyName);
						if (value != null)
						{
							if (int.TryParse(value.ToString(), out cdcaffinityInMinutesDefault))
							{
								if (cdcaffinityInMinutesDefault < 1)
								{
									Globals.LogBadNumber(ConfigurationData.CDCAffinityInMinutesKeyName, 1, 32767, cdcaffinityInMinutesDefault, ConfigurationData.CDCAffinityInMinutesDefault);
									cdcaffinityInMinutesDefault = ConfigurationData.CDCAffinityInMinutesDefault;
								}
							}
							else
							{
								cdcaffinityInMinutesDefault = ConfigurationData.CDCAffinityInMinutesDefault;
							}
						}
						if (cdcaffinityInMinutesDefault != this.ConfigDCAffinityInMinutes)
						{
							result = true;
						}
						this.ConfigDCAffinityInMinutes = cdcaffinityInMinutesDefault;
						ExTraceGlobals.ServiceTracer.TraceInformation<int>(this.GetHashCode(), (long)this.GetHashCode(), "Registry. ConfigDCAffinityMins {0}", this.ConfigDCAffinityInMinutes);
						int topoRecheckSecondsDefault = ConfigurationData.TopoRecheckSecondsDefault;
						value = registryKey.GetValue(ConfigurationData.TopoRecheckSecondsKeyName);
						if (value != null)
						{
							if (int.TryParse(value.ToString(), out topoRecheckSecondsDefault))
							{
								if (topoRecheckSecondsDefault < 1)
								{
									Globals.LogBadNumber(ConfigurationData.TopoRecheckSecondsKeyName, 1, 32767, topoRecheckSecondsDefault, ConfigurationData.TopoRecheckSecondsDefault);
									topoRecheckSecondsDefault = ConfigurationData.TopoRecheckSecondsDefault;
								}
							}
							else
							{
								topoRecheckSecondsDefault = ConfigurationData.TopoRecheckSecondsDefault;
							}
						}
						if (topoRecheckSecondsDefault != this.topoRecheckInSeconds)
						{
							result = true;
						}
						this.topoRecheckInSeconds = topoRecheckSecondsDefault;
						ExTraceGlobals.ServiceTracer.TraceInformation<int>(this.GetHashCode(), (long)this.GetHashCode(), "Registry. TopoRecheckSeconds {0}", this.topoRecheckInSeconds);
					}
				}
				using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey(ConfigurationData.ProfilesDefaultKeyName, false))
				{
					if (registryKey2 != null)
					{
						int minUserDCDefault = ConfigurationData.MinUserDCDefault;
						object value2 = registryKey2.GetValue(ConfigurationData.MinUserDCKeyName);
						if (value2 != null)
						{
							if (int.TryParse(value2.ToString(), out minUserDCDefault))
							{
								if (minUserDCDefault <= 0)
								{
									Globals.LogBadNumber(ConfigurationData.MinUserDCKeyName, 1, 32767, minUserDCDefault, ConfigurationData.MinUserDCDefault);
									minUserDCDefault = ConfigurationData.MinUserDCDefault;
								}
							}
							else
							{
								minUserDCDefault = ConfigurationData.MinUserDCDefault;
							}
						}
						if (minUserDCDefault != this.MinUserDC)
						{
							result = true;
						}
						this.MinUserDC = minUserDCDefault;
						ExTraceGlobals.ServiceTracer.TraceInformation<int>(this.GetHashCode(), (long)this.GetHashCode(), "Registry. MinUserDC {0}", minUserDCDefault);
						value2 = registryKey2.GetValue(ConfigurationData.DisableNetlogonCheckKeyName, ConfigurationData.DisableNetlogonCheckDefault);
						int num;
						if (int.TryParse(value2.ToString(), out num))
						{
							bool flag = 0 != num;
							if (flag != this.DisableNetLogonCheck)
							{
								result = true;
							}
							this.DisableNetLogonCheck = flag;
						}
						ExTraceGlobals.ServiceTracer.TraceInformation<bool>(this.GetHashCode(), (long)this.GetHashCode(), "Registry. DisableNetLogonCheck {0}", this.DisableNetLogonCheck);
						value2 = registryKey2.GetValue(ConfigurationData.AllowPreW2KSP3DCKeyName, ConfigurationData.AllowPreW2KSP3DCDefault);
						int num2;
						if (int.TryParse(value2.ToString(), out num2))
						{
							bool flag2 = 0 != num2;
							if (flag2 != this.AllowPreW2KSP3DC)
							{
								result = true;
							}
							this.AllowPreW2KSP3DC = flag2;
						}
						ExTraceGlobals.ServiceTracer.TraceInformation<bool>(this.GetHashCode(), (long)this.GetHashCode(), "Registry. AllowPreW2KSP3DC {0}", this.AllowPreW2KSP3DC);
						string[] array = registryKey2.GetValue(ConfigurationData.ExcludedDCsKeyName, Array<string>.Empty) as string[];
						if (!ConfigurationData.ContainSameElements(array, this.ExcludedDC))
						{
							result = true;
						}
						if (array == null)
						{
							this.ExcludedDC = Array<string>.Empty;
						}
						else
						{
							this.ExcludedDC = array;
						}
						if (ExTraceGlobals.ServiceTracer.IsTraceEnabled(TraceType.InfoTrace))
						{
							ExTraceGlobals.ServiceTracer.TraceInformation<string>(this.GetHashCode(), (long)this.GetHashCode(), "Registry. Excluded DCs {0}", string.Join(",", this.ExcludedDC));
						}
						string[] subKeyNames = registryKey2.GetSubKeyNames();
						foreach (string name in subKeyNames)
						{
							using (RegistryKey registryKey3 = registryKey2.OpenSubKey(name))
							{
								if (registryKey3 != null)
								{
									string text = registryKey3.GetValue(ConfigurationData.HostNameKeyName, string.Empty) as string;
									if (!string.IsNullOrEmpty(text))
									{
										if ((int)registryKey3.GetValue(ConfigurationData.IsGCKeyName, 0) == 0)
										{
											list.Add(text);
										}
										else
										{
											list2.Add(text);
										}
									}
								}
							}
						}
					}
				}
				if (ExTraceGlobals.ServiceTracer.IsTraceEnabled(TraceType.InfoTrace))
				{
					ExTraceGlobals.ServiceTracer.TraceInformation<string>(this.GetHashCode(), (long)this.GetHashCode(), "Registry. DCs {0}", string.Join(",", this.DomainControllers));
					ExTraceGlobals.ServiceTracer.TraceInformation<string>(this.GetHashCode(), (long)this.GetHashCode(), "Registry. GCs {0}", string.Join(",", this.GlobalCatalogs));
				}
				using (RegistryKey registryKey4 = Registry.LocalMachine.OpenSubKey(ConfigurationData.StaticConfigDCKeyName, false))
				{
					if (registryKey4 != null)
					{
						string text2 = registryKey4.GetValue(ConfigurationData.ConfigDCHostNameKeyName, string.Empty) as string;
						if (string.Compare(text2, this.ConfigurationDC) != 0)
						{
							result = true;
						}
						if (!string.IsNullOrEmpty(text2))
						{
							if (!list.Contains(text2, StringComparer.OrdinalIgnoreCase))
							{
								list.Add(text2);
							}
							this.ConfigurationDC = text2;
						}
						else
						{
							this.ConfigurationDC = null;
						}
					}
					else
					{
						this.ConfigurationDC = null;
					}
				}
				ExTraceGlobals.ServiceTracer.TraceInformation<string>(this.GetHashCode(), (long)this.GetHashCode(), "Registry. CDC {0}", (this.ConfigurationDC != null) ? this.ConfigurationDC : "<NULL>");
			}
			catch (SecurityException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			catch (UnauthorizedAccessException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				ExTraceGlobals.ServiceTracer.TraceError<Exception>((long)this.GetHashCode(), "Exception while reading registry configuration. Error {0}", ex);
				ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_ConfigurationDataLoadingException, null, new object[]
				{
					ex.Message
				});
			}
			if (!ConfigurationData.ContainSameElements(list, this.DomainControllers) || !ConfigurationData.ContainSameElements(list2, this.GlobalCatalogs))
			{
				result = true;
			}
			this.DomainControllers = list;
			this.GlobalCatalogs = list2;
			return result;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000B7C8 File Offset: 0x000099C8
		private static bool ContainSameElements(IEnumerable<string> first, IEnumerable<string> second)
		{
			if (first == null && second == null)
			{
				return true;
			}
			if (first != null && second != null)
			{
				return (from s in first
				orderby s
				select s).SequenceEqual(from s in second
				orderby s
				select s, StringComparer.OrdinalIgnoreCase);
			}
			return false;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000B890 File Offset: 0x00009A90
		private void EnqueueRefresh()
		{
			if (1 == Interlocked.Increment(ref this.registryRefreshScheduled))
			{
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Registry change, configuration to be reloaded in 1 minute.");
				ThreadPool.RegisterWaitForSingleObject(this.refreshTimerEvent, delegate(object x, bool y)
				{
					ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Starting registry reloading.");
					try
					{
						bool flag = this.LoadConfigurationFromRegistry();
						if (flag)
						{
							this.ConfigurationChanged();
						}
					}
					finally
					{
						Interlocked.Decrement(ref this.registryRefreshScheduled);
					}
				}, null, this.NMillisecondsRegistryWaitTimeOnChange, true);
				return;
			}
			ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Registry changed, configuration reload already enqueued.");
			Interlocked.Decrement(ref this.registryRefreshScheduled);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000B90C File Offset: 0x00009B0C
		private void ReloadFileConfiguration(object source, FileSystemEventArgs e)
		{
			try
			{
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Refreshing configuration");
				ConfigurationManager.RefreshSection("Topology");
			}
			catch (ConfigurationErrorsException ex)
			{
				ExTraceGlobals.ServiceTracer.TraceError<string>((long)this.GetHashCode(), "Error Refreshing configuration {0}", ex.ToString());
				ConfigurationData.LogEvent(MSExchangeADTopologyEventLogConstants.Tuple_CurruptedServiceConfiguration, null, new object[]
				{
					ex.Message
				});
			}
			this.tsc = (((ServiceConfigurationSection)ConfigurationManager.GetSection("Topology")) ?? new ServiceConfigurationSection());
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000B9A4 File Offset: 0x00009BA4
		private void RegistryCheck(object stateNotUsed)
		{
			if (this.registryWatcher.IsChanged())
			{
				ExTraceGlobals.ServiceTracer.TraceDebug((long)this.GetHashCode(), "Registry changed. Enqueue configuration refresh.");
				this.EnqueueRefresh();
			}
			this.registryTimer.Change(this.CheckRegistryEveryNMilliseconds, -1);
		}

		// Token: 0x040000AE RID: 174
		private const string SectionName = "Topology";

		// Token: 0x040000AF RID: 175
		private const int NMillisecondsWaitTime = 60000;

		// Token: 0x040000B0 RID: 176
		private const int NMillisecondRegistryCheck = 30000;

		// Token: 0x040000B1 RID: 177
		internal static readonly int MinUserDCDefault = -1;

		// Token: 0x040000B2 RID: 178
		internal static readonly string MinUserDCKeyName = "MinUserDC";

		// Token: 0x040000B3 RID: 179
		internal static readonly string DisableNetlogonCheckKeyName = "DisableNetlogonCheck";

		// Token: 0x040000B4 RID: 180
		internal static readonly string ExcludedDCsKeyName = "ExcludedDCs";

		// Token: 0x040000B5 RID: 181
		internal static readonly string IsGCKeyName = "IsGC";

		// Token: 0x040000B6 RID: 182
		internal static readonly string HostNameKeyName = "HostName";

		// Token: 0x040000B7 RID: 183
		internal static readonly string AllowPreW2KSP3DCKeyName = "AllowPreW2KSP3DC";

		// Token: 0x040000B8 RID: 184
		internal static readonly bool AllowPreW2KSP3DCDefault = false;

		// Token: 0x040000B9 RID: 185
		internal static readonly bool DisableNetlogonCheckDefault = Globals.IsMicrosoftHostedOnly;

		// Token: 0x040000BA RID: 186
		internal static readonly string ConfigDCHostNameKeyName = "ConfigDCHostName";

		// Token: 0x040000BB RID: 187
		internal static readonly string CDCAffinityInMinutesKeyName = "ConfigDCAffinityMins";

		// Token: 0x040000BC RID: 188
		internal static readonly int CDCAffinityInMinutesDefault = 480;

		// Token: 0x040000BD RID: 189
		internal static readonly string TopoRecheckSecondsKeyName = "TopoRecheckSecs";

		// Token: 0x040000BE RID: 190
		internal static readonly int TopoRecheckSecondsDefault = 900;

		// Token: 0x040000BF RID: 191
		internal static readonly string ProfilesDefaultKeyName = "SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Profiles\\Default";

		// Token: 0x040000C0 RID: 192
		internal static readonly string StaticConfigDCKeyName = "SYSTEM\\CurrentControlSet\\Services\\MSExchange ADAccess\\Instance0";

		// Token: 0x040000C1 RID: 193
		private static Hookable<ConfigurationData> hookableInstance = Hookable<ConfigurationData>.Create(true, new ConfigurationData());

		// Token: 0x040000C2 RID: 194
		private static ExEventLog logger = new ExEventLog(ExTraceGlobals.ServiceTracer.Category, "MSExchangeADTopology");

		// Token: 0x040000C3 RID: 195
		private readonly AutoResetEvent refreshTimerEvent = new AutoResetEvent(false);

		// Token: 0x040000C4 RID: 196
		private RegistryWatcher registryWatcher;

		// Token: 0x040000C5 RID: 197
		private Timer registryTimer;

		// Token: 0x040000C6 RID: 198
		private FileSystemWatcher watcher;

		// Token: 0x040000C7 RID: 199
		private ServiceConfigurationSection tsc;

		// Token: 0x040000C8 RID: 200
		private int topoRecheckInSeconds = ConfigurationData.TopoRecheckSecondsDefault;

		// Token: 0x040000C9 RID: 201
		private int registryRefreshScheduled;
	}
}
