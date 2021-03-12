using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.RpcClientAccess.Messages;
using Microsoft.Win32;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class ConfigurationSchema
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002B84 File Offset: 0x00000D84
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002B8B File Offset: 0x00000D8B
		public static TimeSpan RegistryNotificationPollPeriod
		{
			get
			{
				return ConfigurationSchema.registryNotificationPollPeriod;
			}
			internal set
			{
				ConfigurationSchema.registryNotificationPollPeriod = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000047 RID: 71
		public abstract IEnumerable<ConfigurationSchema.DataSource> DataSources { get; }

		// Token: 0x06000048 RID: 72
		public abstract void LoadAll(ConfigurationSchema.ConfigurationUpdater configurationUpdater, ConfigurationSchema.EventLogger eventLogger);

		// Token: 0x04000023 RID: 35
		private static TimeSpan registryNotificationPollPeriod = TimeSpan.FromSeconds(3.0);

		// Token: 0x02000009 RID: 9
		// (Invoke) Token: 0x0600004C RID: 76
		internal delegate void ConfigurationUpdater(Action<ConfigurationPropertyBag> refreshDelegate);

		// Token: 0x0200000A RID: 10
		// (Invoke) Token: 0x06000050 RID: 80
		internal delegate bool TryConvert<TInput, TOutput>(TInput input, out TOutput output);

		// Token: 0x0200000B RID: 11
		// (Invoke) Token: 0x06000054 RID: 84
		internal delegate void EventLogger(ExEventLog.EventTuple tuple, params object[] args);

		// Token: 0x0200000C RID: 12
		public abstract class DataSource
		{
			// Token: 0x06000057 RID: 87 RVA: 0x00002BB0 File Offset: 0x00000DB0
			protected DataSource(ICollection<ConfigurationSchema.DataSource> dataSourceRegistry)
			{
				dataSourceRegistry.Add(this);
			}

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000058 RID: 88 RVA: 0x00002BCA File Offset: 0x00000DCA
			protected IEnumerable<ConfigurationSchema.Property> Properties
			{
				get
				{
					return this.dataSourceProperties;
				}
			}

			// Token: 0x06000059 RID: 89 RVA: 0x00002BD2 File Offset: 0x00000DD2
			internal void AddProperty(ConfigurationSchema.Property property)
			{
				this.dataSourceProperties.Add(property);
			}

			// Token: 0x0600005A RID: 90 RVA: 0x00002BE0 File Offset: 0x00000DE0
			internal bool CanQueryData(object context)
			{
				return context != null;
			}

			// Token: 0x0600005B RID: 91
			internal abstract void Load(ConfigurationSchema.ConfigurationUpdater configurationUpdater, ConfigurationSchema.EventLogger eventLogger);

			// Token: 0x0600005C RID: 92 RVA: 0x00002BE9 File Offset: 0x00000DE9
			internal virtual Action RegisterNotifications(Action notificationDelegate)
			{
				return null;
			}

			// Token: 0x04000024 RID: 36
			private readonly List<ConfigurationSchema.Property> dataSourceProperties = new List<ConfigurationSchema.Property>();
		}

		// Token: 0x0200000D RID: 13
		public abstract class Property
		{
			// Token: 0x0600005D RID: 93 RVA: 0x00002BEC File Offset: 0x00000DEC
			public static bool TryCast<TInput, TOutput>(TInput input, out TOutput output)
			{
				if (input is TOutput)
				{
					output = (TOutput)((object)input);
					return true;
				}
				output = default(TOutput);
				return false;
			}

			// Token: 0x0600005E RID: 94 RVA: 0x00002C4C File Offset: 0x00000E4C
			public static ConfigurationSchema.TryConvert<TInput, TOutput> TryConvertIf<TInput, TOutput>(Predicate<TInput> validationDelegate, Converter<TInput, TOutput> conversionDelegate)
			{
				return delegate(TInput input, out TOutput output)
				{
					if (validationDelegate(input))
					{
						output = conversionDelegate(input);
						return true;
					}
					output = default(TOutput);
					return false;
				};
			}

			// Token: 0x0600005F RID: 95 RVA: 0x00002CA4 File Offset: 0x00000EA4
			public static ConfigurationSchema.TryConvert<TInput, TOutput> TryCastIf<TInput, TOutput>(Predicate<TInput> validationDelegate)
			{
				return delegate(TInput input, out TOutput output)
				{
					if (validationDelegate(input))
					{
						return ConfigurationSchema.Property.TryCast<TInput, TOutput>(input, out output);
					}
					output = default(TOutput);
					return false;
				};
			}

			// Token: 0x06000060 RID: 96 RVA: 0x00002CCA File Offset: 0x00000ECA
			public static bool Identical<T>(T input, out T output)
			{
				output = input;
				return true;
			}

			// Token: 0x06000061 RID: 97
			internal abstract void Load(ConfigurationPropertyBag configurationPropertyBag, object context, ConfigurationSchema.EventLogger eventLogger);
		}

		// Token: 0x0200000E RID: 14
		public abstract class Property<TValue> : ConfigurationSchema.Property
		{
			// Token: 0x06000063 RID: 99 RVA: 0x00002CDC File Offset: 0x00000EDC
			protected Property(ConfigurationSchema.DataSource dataSource, TValue defaultValue)
			{
				this.defaultValue = defaultValue;
				dataSource.AddProperty(this);
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000064 RID: 100 RVA: 0x00002CF2 File Offset: 0x00000EF2
			internal TValue DefaultValue
			{
				get
				{
					return this.defaultValue;
				}
			}

			// Token: 0x06000065 RID: 101 RVA: 0x00002D10 File Offset: 0x00000F10
			internal static ConfigurationSchema.Property<TValue> Declare(ConfigurationSchema.DataSource constantDataSource, Func<TValue> valueDelegate)
			{
				if (!(constantDataSource is ConfigurationSchema.ConstantDataSource))
				{
					throw new ArgumentException("Should be Schema.ConstantDataSource", "constantDataSource");
				}
				return new ConfigurationSchema.DelegateProperty<TValue, ConfigurationSchema.ConstantDataSource>((ConfigurationSchema.ConstantDataSource)constantDataSource, (ConfigurationSchema.ConstantDataSource dataSource, object context) => valueDelegate(), valueDelegate());
			}

			// Token: 0x06000066 RID: 102 RVA: 0x00002D64 File Offset: 0x00000F64
			internal static ConfigurationSchema.Property<TValue> Declare<TDataSource>(TDataSource dataSource, Func<TDataSource, object, TValue> valueDelegate, TValue defaultValue) where TDataSource : ConfigurationSchema.DataSource
			{
				return new ConfigurationSchema.DelegateProperty<TValue, TDataSource>(dataSource, valueDelegate, defaultValue);
			}

			// Token: 0x06000067 RID: 103 RVA: 0x00002D6E File Offset: 0x00000F6E
			internal static ConfigurationSchema.Property<TValue> Declare<TKey, TRawData>(ConfigurationSchema.DataSource<TKey, TRawData> dataSource, TKey dataKey, TValue defaultValue)
			{
				return ConfigurationSchema.Property<TValue>.Declare<TKey, TRawData, TValue>(dataSource, dataKey, new ConfigurationSchema.TryConvert<TRawData, TValue>(ConfigurationSchema.Property.TryCast<TRawData, TValue>), new ConfigurationSchema.TryConvert<TValue, TValue>(ConfigurationSchema.Property.Identical<TValue>), defaultValue);
			}

			// Token: 0x06000068 RID: 104 RVA: 0x00002D90 File Offset: 0x00000F90
			internal static ConfigurationSchema.Property<TValue> Declare<TKey, TRawData, TIntermediate>(ConfigurationSchema.DataSource<TKey, TRawData> dataSource, TKey dataKey, ConfigurationSchema.TryConvert<TIntermediate, TValue> tryCovertDelegate, TValue defaultValue)
			{
				return ConfigurationSchema.Property<TValue>.Declare<TKey, TRawData, TIntermediate>(dataSource, dataKey, new ConfigurationSchema.TryConvert<TRawData, TIntermediate>(ConfigurationSchema.Property.TryCast<TRawData, TIntermediate>), tryCovertDelegate, defaultValue);
			}

			// Token: 0x06000069 RID: 105 RVA: 0x00002DA7 File Offset: 0x00000FA7
			internal static ConfigurationSchema.Property<TValue> Declare<TKey, TRawData, TIntermediate>(ConfigurationSchema.DataSource<TKey, TRawData> dataSource, TKey dataKey, Predicate<TIntermediate> validationDelegate, Converter<TIntermediate, TValue> conversionDelegate, TValue defaultValue)
			{
				return ConfigurationSchema.Property<TValue>.Declare<TKey, TRawData, TIntermediate>(dataSource, dataKey, new ConfigurationSchema.TryConvert<TRawData, TIntermediate>(ConfigurationSchema.Property.TryCast<TRawData, TIntermediate>), ConfigurationSchema.Property.TryConvertIf<TIntermediate, TValue>(validationDelegate, conversionDelegate), defaultValue);
			}

			// Token: 0x0600006A RID: 106 RVA: 0x00002DC5 File Offset: 0x00000FC5
			internal static ConfigurationSchema.Property<TValue> Declare<TKey, TRawData, TIntermediate>(ConfigurationSchema.DataSource<TKey, TRawData> dataSource, TKey dataKey, ConfigurationSchema.TryConvert<TRawData, TIntermediate> tryParseDelegate, ConfigurationSchema.TryConvert<TIntermediate, TValue> tryCovertDelegate, TValue defaultValue)
			{
				Util.ThrowOnNullArgument(dataSource, "dataSource");
				return new ConfigurationSchema.DataSourceProperty<TValue, TKey, TRawData, TIntermediate>(dataSource, dataKey, tryParseDelegate, tryCovertDelegate, defaultValue);
			}

			// Token: 0x04000025 RID: 37
			private readonly TValue defaultValue;
		}

		// Token: 0x0200000F RID: 15
		public class LoadException : Exception
		{
			// Token: 0x0600006B RID: 107 RVA: 0x00002DDD File Offset: 0x00000FDD
			public LoadException(Exception innerException) : base("Could not load configuration information.", innerException)
			{
			}

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x0600006C RID: 108 RVA: 0x00002DEB File Offset: 0x00000FEB
			public override string Message
			{
				get
				{
					return base.InnerException.Message;
				}
			}
		}

		// Token: 0x02000010 RID: 16
		internal abstract class DataSource<TKey, TRawData> : ConfigurationSchema.DataSource
		{
			// Token: 0x0600006D RID: 109 RVA: 0x00002DF8 File Offset: 0x00000FF8
			protected DataSource(ICollection<ConfigurationSchema.DataSource> dataSourceRegistry) : base(dataSourceRegistry)
			{
			}

			// Token: 0x0600006E RID: 110
			internal abstract string GetDatumAdminDisplayName(TKey key);

			// Token: 0x0600006F RID: 111 RVA: 0x00002E01 File Offset: 0x00001001
			internal virtual string GetTypeDisplayName(Type rawValueType)
			{
				return rawValueType.Name;
			}

			// Token: 0x06000070 RID: 112
			internal abstract bool TryGetRawData(TKey key, object context, out TRawData rawData);

			// Token: 0x06000071 RID: 113 RVA: 0x00002E84 File Offset: 0x00001084
			protected void Load(ConfigurationSchema.ConfigurationUpdater configurationUpdater, object context, ConfigurationSchema.EventLogger eventLogger)
			{
				configurationUpdater(delegate(ConfigurationPropertyBag newConfiguration)
				{
					newConfiguration.Delete(this.Properties);
					foreach (ConfigurationSchema.Property property in this.Properties)
					{
						property.Load(newConfiguration, context, eventLogger);
					}
				});
			}
		}

		// Token: 0x02000011 RID: 17
		protected sealed class RegistryDataSource : ConfigurationSchema.DataSource<string, object>
		{
			// Token: 0x06000072 RID: 114 RVA: 0x00002EBE File Offset: 0x000010BE
			internal RegistryDataSource(ICollection<ConfigurationSchema.DataSource> dataSourceRegistry, string registryKeyName) : base(dataSourceRegistry)
			{
				this.registryKeyName = registryKeyName;
			}

			// Token: 0x06000073 RID: 115 RVA: 0x00002ECE File Offset: 0x000010CE
			internal override string GetDatumAdminDisplayName(string key)
			{
				return string.Format("{0}\\{1}\\@{2}", Registry.LocalMachine.Name, this.registryKeyName, key);
			}

			// Token: 0x06000074 RID: 116 RVA: 0x00002F0C File Offset: 0x0000110C
			internal override bool TryGetRawData(string key, object context, out object rawData)
			{
				rawData = this.ExecuteRegistryOperation<object>(() => ((RegistryKey)context).GetValue(key, null));
				return rawData != null;
			}

			// Token: 0x06000075 RID: 117 RVA: 0x00002F49 File Offset: 0x00001149
			internal override string GetTypeDisplayName(Type rawValueType)
			{
				return ConfigurationSchema.RegistryDataSource.typeToRegistryType[rawValueType];
			}

			// Token: 0x06000076 RID: 118 RVA: 0x00002F6C File Offset: 0x0000116C
			internal override void Load(ConfigurationSchema.ConfigurationUpdater configurationUpdater, ConfigurationSchema.EventLogger eventLogger)
			{
				using (RegistryKey registryKey = this.ExecuteRegistryOperation<RegistryKey>(() => Registry.LocalMachine.OpenSubKey(this.registryKeyName, false)))
				{
					base.Load(configurationUpdater, registryKey, eventLogger);
				}
			}

			// Token: 0x06000077 RID: 119 RVA: 0x00002FD0 File Offset: 0x000011D0
			internal override Action RegisterNotifications(Action notificationDelegate)
			{
				RegistryWatcher watcher = new RegistryWatcher(this.registryKeyName, false);
				lock (ConfigurationSchema.RegistryDataSource.watchers)
				{
					ConfigurationSchema.RegistryDataSource.watchers.Add(watcher, notificationDelegate);
				}
				ConfigurationSchema.RegistryDataSource.EnableDisableNotificationTimer();
				return delegate()
				{
					ConfigurationSchema.RegistryDataSource.RemoveWatcher(watcher);
				};
			}

			// Token: 0x06000078 RID: 120 RVA: 0x00003044 File Offset: 0x00001244
			private static void EnableDisableNotificationTimer()
			{
				lock (ConfigurationSchema.RegistryDataSource.watchers)
				{
					ConfigurationSchema.RegistryDataSource.notificationTimer.Change((ConfigurationSchema.RegistryDataSource.watchers.Count > 0) ? ((long)ConfigurationSchema.RegistryNotificationPollPeriod.TotalMilliseconds) : -1L, -1L);
				}
			}

			// Token: 0x06000079 RID: 121 RVA: 0x000030AC File Offset: 0x000012AC
			private static void NotificationTimerCallback(object state)
			{
				try
				{
					KeyValuePair<RegistryWatcher, Action>[] array;
					lock (ConfigurationSchema.RegistryDataSource.watchers)
					{
						array = ConfigurationSchema.RegistryDataSource.watchers.ToArray<KeyValuePair<RegistryWatcher, Action>>();
					}
					foreach (KeyValuePair<RegistryWatcher, Action> keyValuePair in array)
					{
						if (keyValuePair.Key.IsChanged())
						{
							keyValuePair.Value();
						}
					}
				}
				finally
				{
					ConfigurationSchema.RegistryDataSource.EnableDisableNotificationTimer();
				}
			}

			// Token: 0x0600007A RID: 122 RVA: 0x00003144 File Offset: 0x00001344
			private static void RemoveWatcher(RegistryWatcher watcher)
			{
				lock (ConfigurationSchema.RegistryDataSource.watchers)
				{
					ConfigurationSchema.RegistryDataSource.watchers.Remove(watcher);
				}
				ConfigurationSchema.RegistryDataSource.EnableDisableNotificationTimer();
			}

			// Token: 0x0600007B RID: 123 RVA: 0x00003190 File Offset: 0x00001390
			private TResult ExecuteRegistryOperation<TResult>(Func<TResult> registryOperation)
			{
				TResult result;
				try
				{
					result = registryOperation();
				}
				catch (SecurityException innerException)
				{
					throw new ConfigurationSchema.LoadException(innerException);
				}
				catch (IOException innerException2)
				{
					throw new ConfigurationSchema.LoadException(innerException2);
				}
				catch (UnauthorizedAccessException innerException3)
				{
					throw new ConfigurationSchema.LoadException(innerException3);
				}
				return result;
			}

			// Token: 0x04000026 RID: 38
			private static readonly Dictionary<Type, string> typeToRegistryType = new Dictionary<Type, string>().AddPair(typeof(int), "REG_DWORD").AddPair(typeof(long), "REG_QWORD").AddPair(typeof(string), "REG_SZ").AddPair(typeof(string[]), "REG_MULTI_SZ").AddPair(typeof(byte[]), "REG_BINARY");

			// Token: 0x04000027 RID: 39
			private static readonly Dictionary<RegistryWatcher, Action> watchers = new Dictionary<RegistryWatcher, Action>();

			// Token: 0x04000028 RID: 40
			private static readonly Timer notificationTimer = new Timer(new TimerCallback(ConfigurationSchema.RegistryDataSource.NotificationTimerCallback), null, -1, -1);

			// Token: 0x04000029 RID: 41
			private readonly string registryKeyName;
		}

		// Token: 0x02000012 RID: 18
		protected sealed class DirectoryDataSource<TDirectoryObject> : ConfigurationSchema.DataSource<ADPropertyDefinition, object> where TDirectoryObject : ADConfigurationObject, new()
		{
			// Token: 0x0600007E RID: 126 RVA: 0x00003286 File Offset: 0x00001486
			internal DirectoryDataSource(ICollection<ConfigurationSchema.DataSource> dataSourceRegistry, Func<ITopologyConfigurationSession, TDirectoryObject> adObjectReadDelegate, Func<ITopologyConfigurationSession, ADObjectId> notificationScopeDelegate) : base(dataSourceRegistry)
			{
				this.adObjectReadDelegate = adObjectReadDelegate;
				this.notificationScopeDelegate = notificationScopeDelegate;
			}

			// Token: 0x0600007F RID: 127 RVA: 0x000032B3 File Offset: 0x000014B3
			internal ConfigurationSchema.Property<TValue> DeclareProperty<TValue>(ADPropertyDefinition dataKey)
			{
				return ConfigurationSchema.Property<TValue>.Declare<ADPropertyDefinition, object>(this, dataKey, (TValue)((object)dataKey.DefaultValue));
			}

			// Token: 0x06000080 RID: 128 RVA: 0x000032C8 File Offset: 0x000014C8
			internal override string GetDatumAdminDisplayName(ADPropertyDefinition key)
			{
				if (this.adObjectId != null)
				{
					return string.Format("({0}), {1}", this.adObjectId.DistinguishedName, key.Name);
				}
				string format = "@(...)[objectCategory={0}], {1}";
				TDirectoryObject tdirectoryObject = Activator.CreateInstance<TDirectoryObject>();
				return string.Format(format, tdirectoryObject.MostDerivedObjectClass, key.Name);
			}

			// Token: 0x06000081 RID: 129 RVA: 0x0000331C File Offset: 0x0000151C
			internal override bool TryGetRawData(ADPropertyDefinition key, object context, out object rawData)
			{
				TDirectoryObject tdirectoryObject = (TDirectoryObject)((object)context);
				rawData = tdirectoryObject[key];
				return true;
			}

			// Token: 0x06000082 RID: 130 RVA: 0x0000335C File Offset: 0x0000155C
			internal override Action RegisterNotifications(Action notificationDelegate)
			{
				this.notificationDelegate = notificationDelegate;
				if (this.notificationScope == null)
				{
					this.notificationScope = this.GetNotificationScope();
				}
				if (this.notificationScope == null)
				{
					this.notificationScopeRetry = new Timer(delegate(object unused)
					{
						this.RetryNotificationScope();
					}, null, 0, ConfigurationSchema.DirectoryDataSource<TDirectoryObject>.NotificationScopeRetryPeriod.Milliseconds);
				}
				else
				{
					this.notificationCookie = ADNotificationAdapter.RegisterChangeNotification<TDirectoryObject>(this.notificationScope, delegate(ADNotificationEventArgs unusedNotificationInfo)
					{
						this.InternalOnNotify();
					});
				}
				return delegate()
				{
					this.UnregisterChangeNotificationInternal();
				};
			}

			// Token: 0x06000083 RID: 131 RVA: 0x000033EC File Offset: 0x000015EC
			private void InternalOnNotify()
			{
				try
				{
					if (Monitor.TryEnter(this.changeNotificationLock))
					{
						this.notificationDelegate();
					}
				}
				finally
				{
					if (Monitor.IsEntered(this.changeNotificationLock))
					{
						Monitor.Exit(this.changeNotificationLock);
					}
				}
			}

			// Token: 0x06000084 RID: 132 RVA: 0x00003444 File Offset: 0x00001644
			private void RetryNotificationScope()
			{
				try
				{
					if (Monitor.TryEnter(this.notificationScopeLock))
					{
						if (this.notificationScopeRetry != null)
						{
							this.notificationScope = this.GetNotificationScope();
							if (this.notificationScope != null)
							{
								this.notificationCookie = ADNotificationAdapter.RegisterChangeNotification<TDirectoryObject>(this.notificationScope, delegate(ADNotificationEventArgs unusedNotificationInfo)
								{
									this.InternalOnNotify();
								});
								this.InternalOnNotify();
								this.notificationScopeRetry.Dispose();
								this.notificationScopeRetry = null;
							}
						}
					}
				}
				catch (ADTransientException)
				{
				}
				finally
				{
					if (Monitor.IsEntered(this.notificationScopeLock))
					{
						Monitor.Exit(this.notificationScopeLock);
					}
				}
			}

			// Token: 0x06000085 RID: 133 RVA: 0x000034F4 File Offset: 0x000016F4
			private void UnregisterChangeNotificationInternal()
			{
				lock (this.notificationScopeLock)
				{
					if (this.notificationScopeRetry != null)
					{
						this.notificationScopeRetry.Dispose();
						this.notificationScopeRetry = null;
					}
					if (this.notificationCookie != null)
					{
						ADNotificationAdapter.UnregisterChangeNotification(this.notificationCookie);
					}
				}
			}

			// Token: 0x06000086 RID: 134 RVA: 0x0000355C File Offset: 0x0000175C
			private ADObjectId GetNotificationScope()
			{
				ADObjectId adobjectId = this.ExecuteADOperation<ADObjectId>(this.notificationScopeDelegate);
				if (adobjectId != null && adobjectId.ObjectGuid != Guid.Empty)
				{
					return adobjectId;
				}
				return null;
			}

			// Token: 0x06000087 RID: 135 RVA: 0x00003590 File Offset: 0x00001790
			internal override void Load(ConfigurationSchema.ConfigurationUpdater configurationUpdater, ConfigurationSchema.EventLogger eventLogger)
			{
				base.Load(configurationUpdater, this.LoadDirectoryObject(), eventLogger);
			}

			// Token: 0x06000088 RID: 136 RVA: 0x000035A8 File Offset: 0x000017A8
			private TDirectoryObject LoadDirectoryObject()
			{
				TDirectoryObject tdirectoryObject = this.ExecuteADOperation<TDirectoryObject>(this.adObjectReadDelegate);
				if (tdirectoryObject != null)
				{
					this.adObjectId = tdirectoryObject.Id;
				}
				return tdirectoryObject;
			}

			// Token: 0x06000089 RID: 137 RVA: 0x00003628 File Offset: 0x00001828
			private TResult ExecuteADOperation<TResult>(Func<ITopologyConfigurationSession, TResult> adOperationDelegate)
			{
				TResult result = default(TResult);
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					ITopologyConfigurationSession arg = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.FullyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 679, "ExecuteADOperation", "f:\\15.00.1497\\sources\\dev\\mapimt\\src\\Common\\ConfigurationSchema.cs");
					result = adOperationDelegate(arg);
				}, 1);
				if (adoperationResult != ADOperationResult.Success)
				{
					throw new ConfigurationSchema.LoadException(adoperationResult.Exception);
				}
				return result;
			}

			// Token: 0x0400002A RID: 42
			private static readonly TimeSpan NotificationScopeRetryPeriod = TimeSpan.FromMinutes(1.0);

			// Token: 0x0400002B RID: 43
			private readonly Func<ITopologyConfigurationSession, TDirectoryObject> adObjectReadDelegate;

			// Token: 0x0400002C RID: 44
			private readonly Func<ITopologyConfigurationSession, ADObjectId> notificationScopeDelegate;

			// Token: 0x0400002D RID: 45
			private ADObjectId adObjectId;

			// Token: 0x0400002E RID: 46
			private ADObjectId notificationScope;

			// Token: 0x0400002F RID: 47
			private Action notificationDelegate;

			// Token: 0x04000030 RID: 48
			private Timer notificationScopeRetry;

			// Token: 0x04000031 RID: 49
			private ADNotificationRequestCookie notificationCookie;

			// Token: 0x04000032 RID: 50
			private object notificationScopeLock = new object();

			// Token: 0x04000033 RID: 51
			private object changeNotificationLock = new object();
		}

		// Token: 0x02000013 RID: 19
		protected sealed class AppSettingsDataSource : ConfigurationSchema.DataSource<string, string>
		{
			// Token: 0x0600008F RID: 143 RVA: 0x00003690 File Offset: 0x00001890
			public AppSettingsDataSource(ICollection<ConfigurationSchema.DataSource> dataSourceRegistry) : base(dataSourceRegistry)
			{
			}

			// Token: 0x06000090 RID: 144 RVA: 0x00003699 File Offset: 0x00001899
			internal override bool TryGetRawData(string key, object context, out string rawData)
			{
				rawData = ((NameValueCollection)context)[key];
				return rawData != null;
			}

			// Token: 0x06000091 RID: 145 RVA: 0x000036B1 File Offset: 0x000018B1
			internal override string GetDatumAdminDisplayName(string key)
			{
				return string.Format("app.config://configuration/{0}/add[@key='{1}']/@value", "appSettings", key);
			}

			// Token: 0x06000092 RID: 146 RVA: 0x000036D8 File Offset: 0x000018D8
			internal override Action RegisterNotifications(Action notificationDelegate)
			{
				string appConfigFileName = Configuration.AppConfigFileName;
				FileSystemWatcherTimer watcher = new FileSystemWatcherTimer(appConfigFileName, notificationDelegate);
				return delegate()
				{
					watcher.Dispose();
				};
			}

			// Token: 0x06000093 RID: 147 RVA: 0x0000370C File Offset: 0x0000190C
			internal override void Load(ConfigurationSchema.ConfigurationUpdater configurationUpdater, ConfigurationSchema.EventLogger eventLogger)
			{
				NameValueCollection appSettings;
				try
				{
					ConfigurationManager.RefreshSection("appSettings");
					appSettings = ConfigurationManager.AppSettings;
				}
				catch (ConfigurationErrorsException innerException)
				{
					throw new ConfigurationSchema.LoadException(innerException);
				}
				base.Load(configurationUpdater, appSettings, eventLogger);
			}

			// Token: 0x04000034 RID: 52
			private const string SectionName = "appSettings";
		}

		// Token: 0x02000014 RID: 20
		protected sealed class ConstantDataSource : ConfigurationSchema.DataSource<ConfigurationSchema.ConstantDataSource, ConfigurationSchema.ConstantDataSource>
		{
			// Token: 0x06000094 RID: 148 RVA: 0x0000374C File Offset: 0x0000194C
			internal ConstantDataSource(ICollection<ConfigurationSchema.DataSource> dataSourceRegistry) : base(dataSourceRegistry)
			{
			}

			// Token: 0x06000095 RID: 149 RVA: 0x00003755 File Offset: 0x00001955
			internal ConfigurationSchema.Property<TValue> Declare<TValue>(Func<TValue> valueDelegate)
			{
				return ConfigurationSchema.Property<TValue>.Declare(this, valueDelegate);
			}

			// Token: 0x06000096 RID: 150 RVA: 0x0000375E File Offset: 0x0000195E
			internal override string GetDatumAdminDisplayName(ConfigurationSchema.ConstantDataSource key)
			{
				return string.Format("constant:{0}", key);
			}

			// Token: 0x06000097 RID: 151 RVA: 0x0000376B File Offset: 0x0000196B
			internal override void Load(ConfigurationSchema.ConfigurationUpdater configurationUpdater, ConfigurationSchema.EventLogger eventLogger)
			{
				base.Load(configurationUpdater, null, eventLogger);
			}

			// Token: 0x06000098 RID: 152 RVA: 0x00003776 File Offset: 0x00001976
			internal override bool TryGetRawData(ConfigurationSchema.ConstantDataSource key, object context, out ConfigurationSchema.ConstantDataSource rawData)
			{
				rawData = null;
				return true;
			}
		}

		// Token: 0x02000015 RID: 21
		protected class DelegateProperty<TValue, TDataSource> : ConfigurationSchema.Property<TValue> where TDataSource : ConfigurationSchema.DataSource
		{
			// Token: 0x06000099 RID: 153 RVA: 0x0000377C File Offset: 0x0000197C
			internal DelegateProperty(TDataSource dataSource, Func<TDataSource, object, TValue> valueDelegate, TValue defaultValue) : base(dataSource, defaultValue)
			{
				this.dataSource = dataSource;
				this.valueDelegate = valueDelegate;
			}

			// Token: 0x0600009A RID: 154 RVA: 0x00003799 File Offset: 0x00001999
			internal override void Load(ConfigurationPropertyBag configurationPropertyBag, object context, ConfigurationSchema.EventLogger eventLogger)
			{
				configurationPropertyBag.Set<TValue>(this, this.valueDelegate(this.dataSource, context));
			}

			// Token: 0x04000035 RID: 53
			private readonly TDataSource dataSource;

			// Token: 0x04000036 RID: 54
			private readonly Func<TDataSource, object, TValue> valueDelegate;
		}

		// Token: 0x02000016 RID: 22
		protected class DataSourceProperty<TValue, TKey, TRawData, TIntermediate> : ConfigurationSchema.Property<TValue>
		{
			// Token: 0x0600009B RID: 155 RVA: 0x000037B4 File Offset: 0x000019B4
			internal DataSourceProperty(ConfigurationSchema.DataSource<TKey, TRawData> dataSource, TKey dataKey, ConfigurationSchema.TryConvert<TRawData, TIntermediate> tryParseDelegate, ConfigurationSchema.TryConvert<TIntermediate, TValue> tryConvertDelegate, TValue defaultValue) : base(dataSource, defaultValue)
			{
				this.dataKey = dataKey;
				this.dataSource = dataSource;
				this.tryParseDelegate = tryParseDelegate;
				this.tryCovertDelegate = tryConvertDelegate;
			}

			// Token: 0x0600009C RID: 156 RVA: 0x000037DC File Offset: 0x000019DC
			internal override void Load(ConfigurationPropertyBag configurationPropertyBag, object context, ConfigurationSchema.EventLogger eventLogger)
			{
				TRawData trawData;
				if (!this.dataSource.CanQueryData(context) || !this.dataSource.TryGetRawData(this.dataKey, context, out trawData))
				{
					return;
				}
				TIntermediate input;
				if (!this.tryParseDelegate(trawData, out input))
				{
					eventLogger(RpcClientAccessServiceEventLogConstants.Tuple_ConfigurationInvalidValueType, new object[]
					{
						this.dataSource.GetDatumAdminDisplayName(this.dataKey),
						trawData,
						this.dataSource.GetTypeDisplayName(typeof(TIntermediate))
					});
					configurationPropertyBag.MarkInvalid();
					return;
				}
				TValue value;
				if (this.tryCovertDelegate(input, out value))
				{
					configurationPropertyBag.Set<TValue>(this, value);
					return;
				}
				eventLogger(RpcClientAccessServiceEventLogConstants.Tuple_ConfigurationInvalidValue, new object[]
				{
					this.dataSource.GetDatumAdminDisplayName(this.dataKey),
					trawData
				});
				configurationPropertyBag.MarkInvalid();
			}

			// Token: 0x0600009D RID: 157 RVA: 0x000038C1 File Offset: 0x00001AC1
			[Conditional("DEBUG")]
			private void DebugCheckConversions(TKey key)
			{
				this.dataSource.GetDatumAdminDisplayName(key);
			}

			// Token: 0x0600009E RID: 158 RVA: 0x000038D0 File Offset: 0x00001AD0
			[Conditional("DEBUG")]
			private void DebugCheckConversions(Type rawDataType)
			{
				this.dataSource.GetTypeDisplayName(rawDataType);
			}

			// Token: 0x04000037 RID: 55
			private readonly TKey dataKey;

			// Token: 0x04000038 RID: 56
			private readonly ConfigurationSchema.DataSource<TKey, TRawData> dataSource;

			// Token: 0x04000039 RID: 57
			private readonly ConfigurationSchema.TryConvert<TRawData, TIntermediate> tryParseDelegate;

			// Token: 0x0400003A RID: 58
			private readonly ConfigurationSchema.TryConvert<TIntermediate, TValue> tryCovertDelegate;
		}
	}
}
