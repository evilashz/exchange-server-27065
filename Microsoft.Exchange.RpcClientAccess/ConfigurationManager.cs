using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ConfigurationManager : BaseObject
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000022E4 File Offset: 0x000004E4
		public ConfigurationManager()
		{
			ConfigurationManager.SyncInstance[] array = new ConfigurationManager.SyncInstance[2];
			array[0] = new ConfigurationManager.SyncInstance(this, ConfigurationSchema<ServiceConfiguration.Schema>.Instance, (ConfigurationPropertyBag propertyBag) => Configuration.ServiceConfiguration = new ServiceConfiguration(propertyBag));
			array[1] = new ConfigurationManager.SyncInstance(this, ConfigurationSchema<ProtocolLogConfiguration.Schema>.Instance, (ConfigurationPropertyBag propertyBag) => Configuration.ProtocolLogConfiguration = new ProtocolLogConfiguration(propertyBag));
			this.allInstances = array;
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600001D RID: 29 RVA: 0x00002368 File Offset: 0x00000568
		// (remove) Token: 0x0600001E RID: 30 RVA: 0x000023A0 File Offset: 0x000005A0
		public event Action<Exception> ConfigurationLoadFailed;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600001F RID: 31 RVA: 0x000023D8 File Offset: 0x000005D8
		// (remove) Token: 0x06000020 RID: 32 RVA: 0x00002410 File Offset: 0x00000610
		public event Action<Exception> AsyncUnhandledException;

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002448 File Offset: 0x00000648
		public bool HasConfigurationsThatFailToUpdate
		{
			get
			{
				foreach (ConfigurationManager.SyncInstance syncInstance in this.allInstances)
				{
					if (!syncInstance.LastUpdateSucceeded)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002488 File Offset: 0x00000688
		internal static ConfigurationManager CreateForTest(out Dictionary<ConfigurationSchema, ConfigurationManager.ITestHook> testHooks)
		{
			ConfigurationManager configurationManager = new ConfigurationManager();
			testHooks = configurationManager.allInstances.ToDictionary((ConfigurationManager.SyncInstance syncInstance) => syncInstance.Schema, (ConfigurationManager.SyncInstance syncInstance) => syncInstance);
			return configurationManager;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024EC File Offset: 0x000006EC
		public void LoadAndRegisterForNotifications()
		{
			if (!this.isInitialized)
			{
				this.isInitialized = true;
				this.ForEach(delegate(ConfigurationManager.SyncInstance x)
				{
					x.RegisterForNotifications();
				});
				this.ForceReload();
				return;
			}
			throw new InvalidOperationException("ConfigurationManager.LoadAndRegisterForNotifications() can only be called once");
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002546 File Offset: 0x00000746
		internal void ForceReload()
		{
			this.ForEach(delegate(ConfigurationManager.SyncInstance x)
			{
				x.Load();
			});
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000256C File Offset: 0x0000076C
		protected override void InternalDispose()
		{
			foreach (Action action in this.cleanupActions)
			{
				action();
			}
			this.cleanupActions.Clear();
			base.InternalDispose();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025D0 File Offset: 0x000007D0
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ConfigurationManager>(this);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025D8 File Offset: 0x000007D8
		private void ForEach(Action<ConfigurationManager.SyncInstance> action)
		{
			foreach (ConfigurationManager.SyncInstance obj in this.allInstances)
			{
				action(obj);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002608 File Offset: 0x00000808
		private void OnAsyncUnhandledException(Exception ex)
		{
			Action<Exception> asyncUnhandledException = this.AsyncUnhandledException;
			if (asyncUnhandledException != null)
			{
				asyncUnhandledException(ex);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002628 File Offset: 0x00000828
		private bool TryOnConfigurationLoadFailed(Exception ex)
		{
			Action<Exception> configurationLoadFailed = this.ConfigurationLoadFailed;
			if (configurationLoadFailed != null)
			{
				configurationLoadFailed(ex);
				return true;
			}
			return false;
		}

		// Token: 0x0400000D RID: 13
		private readonly ConfigurationManager.SyncInstance[] allInstances;

		// Token: 0x0400000E RID: 14
		private readonly List<Action> cleanupActions = new List<Action>();

		// Token: 0x0400000F RID: 15
		private bool isInitialized;

		// Token: 0x02000005 RID: 5
		internal interface ITestHook
		{
			// Token: 0x06000030 RID: 48
			Action SetOverride<TValue>(ConfigurationSchema.Property<TValue> property, TValue valueOverride);
		}

		// Token: 0x02000006 RID: 6
		private sealed class SyncInstance : ConfigurationManager.ITestHook
		{
			// Token: 0x06000031 RID: 49 RVA: 0x00002649 File Offset: 0x00000849
			public SyncInstance(ConfigurationManager configurationManager, ConfigurationSchema schema, Func<ConfigurationPropertyBag, object> setter)
			{
				this.configurationManager = configurationManager;
				this.Schema = schema;
				this.setter = setter;
			}

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x06000032 RID: 50 RVA: 0x0000267C File Offset: 0x0000087C
			// (set) Token: 0x06000033 RID: 51 RVA: 0x00002684 File Offset: 0x00000884
			public bool LastUpdateSucceeded { get; private set; }

			// Token: 0x06000034 RID: 52 RVA: 0x0000268D File Offset: 0x0000088D
			public void Load()
			{
				this.Load(this.Schema.DataSources);
			}

			// Token: 0x06000035 RID: 53 RVA: 0x000026B9 File Offset: 0x000008B9
			public void Load(IEnumerable<ConfigurationSchema.DataSource> dataSources)
			{
				this.ExecuteDataSourceOperation(dataSources, delegate(ConfigurationSchema.DataSource dataSource)
				{
					dataSource.Load(new ConfigurationSchema.ConfigurationUpdater(this.UpdateConfiguration), Configuration.EventLogger);
				});
			}

			// Token: 0x06000036 RID: 54 RVA: 0x000026FF File Offset: 0x000008FF
			public void RegisterForNotifications()
			{
				this.ExecuteDataSourceOperation(this.Schema.DataSources, delegate(ConfigurationSchema.DataSource dataSource)
				{
					Action action = dataSource.RegisterNotifications(this.GetNotificationCallback(dataSource));
					if (action != null)
					{
						this.configurationManager.cleanupActions.Add(action);
					}
				});
			}

			// Token: 0x06000037 RID: 55 RVA: 0x000027B0 File Offset: 0x000009B0
			Action ConfigurationManager.ITestHook.SetOverride<TValue>(ConfigurationSchema.Property<TValue> property, TValue valueOverride)
			{
				object previousValueOverrideObject;
				bool wasOldOverrideReplaced;
				lock (this.updateLock)
				{
					wasOldOverrideReplaced = this.overrides.TryGetValue(property, out previousValueOverrideObject);
					this.overrides[property] = valueOverride;
				}
				this.Load();
				return delegate()
				{
					lock (this.updateLock)
					{
						if (wasOldOverrideReplaced)
						{
							this.overrides[property] = previousValueOverrideObject;
						}
						else
						{
							this.overrides.Remove(property);
						}
					}
					this.Load();
				};
			}

			// Token: 0x06000038 RID: 56 RVA: 0x00002844 File Offset: 0x00000A44
			private void ExecuteDataSourceOperation(IEnumerable<ConfigurationSchema.DataSource> dataSources, Action<ConfigurationSchema.DataSource> operationDelegate)
			{
				foreach (ConfigurationSchema.DataSource obj in dataSources)
				{
					try
					{
						operationDelegate(obj);
					}
					catch (ConfigurationSchema.LoadException ex)
					{
						if (!this.configurationManager.TryOnConfigurationLoadFailed(ex.InnerException))
						{
							throw;
						}
					}
				}
			}

			// Token: 0x06000039 RID: 57 RVA: 0x00002920 File Offset: 0x00000B20
			private Action GetNotificationCallback(ConfigurationSchema.DataSource dataSource)
			{
				return delegate()
				{
					ExWatson.SendReportOnUnhandledException(delegate()
					{
						this.Load(new ConfigurationSchema.DataSource[]
						{
							dataSource
						});
					}, delegate(object ex)
					{
						this.configurationManager.OnAsyncUnhandledException(ex as Exception);
						return true;
					});
				};
			}

			// Token: 0x0600003A RID: 58 RVA: 0x00002950 File Offset: 0x00000B50
			private void UpdateConfiguration(Action<ConfigurationPropertyBag> loadActionDelegate)
			{
				object obj = null;
				this.LastUpdateSucceeded = false;
				lock (this.updateLock)
				{
					ConfigurationPropertyBag configurationPropertyBag = new ConfigurationPropertyBag(this.propertyBag, this.overrides);
					loadActionDelegate(configurationPropertyBag);
					if (configurationPropertyBag.IsValid)
					{
						this.propertyBag = configurationPropertyBag;
						obj = this.setter(configurationPropertyBag);
					}
				}
				if (obj != null)
				{
					this.LastUpdateSucceeded = true;
					Configuration.InternalFireOnChanged(obj);
					return;
				}
				this.configurationManager.TryOnConfigurationLoadFailed(null);
			}

			// Token: 0x04000018 RID: 24
			public readonly ConfigurationSchema Schema;

			// Token: 0x04000019 RID: 25
			private readonly ConfigurationManager configurationManager;

			// Token: 0x0400001A RID: 26
			private readonly Func<ConfigurationPropertyBag, object> setter;

			// Token: 0x0400001B RID: 27
			private readonly Dictionary<ConfigurationSchema.Property, object> overrides = new Dictionary<ConfigurationSchema.Property, object>();

			// Token: 0x0400001C RID: 28
			private readonly object updateLock = new object();

			// Token: 0x0400001D RID: 29
			private ConfigurationPropertyBag propertyBag;
		}
	}
}
