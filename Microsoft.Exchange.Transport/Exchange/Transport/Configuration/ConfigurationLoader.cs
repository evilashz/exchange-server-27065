using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x0200028A RID: 650
	internal class ConfigurationLoader<TCache, TBuilder> : ITransportComponent where TCache : class where TBuilder : ConfigurationLoader<TCache, TBuilder>.Builder, new()
	{
		// Token: 0x06001BE2 RID: 7138 RVA: 0x000726C1 File Offset: 0x000708C1
		public ConfigurationLoader(TimeSpan reloadInterval) : this(null, reloadInterval)
		{
		}

		// Token: 0x06001BE3 RID: 7139 RVA: 0x000726CC File Offset: 0x000708CC
		public ConfigurationLoader(ConfigurationLoader<TCache, TBuilder>.ExternalConfigurationSetter externalConfigurationSetter, TimeSpan reloadInterval)
		{
			this.session = this.CreateSession();
			this.externalConfigurationSetter = externalConfigurationSetter;
			this.cookies = new List<ADNotificationRequestCookie>();
			if (reloadInterval > TimeSpan.Zero)
			{
				this.reloadInterval = reloadInterval;
				this.reloadTimer = new GuardedTimer(new TimerCallback(this.TimedReload), null, -1, this.reloadInterval.Milliseconds);
			}
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x0007274B File Offset: 0x0007094B
		protected virtual ITopologyConfigurationSession CreateSession()
		{
			return DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 152, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\Configuration\\ConfigurationLoader.cs");
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06001BE5 RID: 7141 RVA: 0x0007276C File Offset: 0x0007096C
		// (remove) Token: 0x06001BE6 RID: 7142 RVA: 0x000727A4 File Offset: 0x000709A4
		public event ConfigurationUpdateHandler<TCache> Changed;

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x000727D9 File Offset: 0x000709D9
		public bool IsInitialized
		{
			get
			{
				return this.cache != null && this.cookies.Count > 0;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x06001BE8 RID: 7144 RVA: 0x000727F8 File Offset: 0x000709F8
		public TCache Cache
		{
			get
			{
				return this.cache;
			}
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x00072800 File Offset: 0x00070A00
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			if (this.cache is IDiagnosable)
			{
				return ((IDiagnosable)((object)this.cache)).GetDiagnosticInfo(parameters);
			}
			return null;
		}

		// Token: 0x06001BEA RID: 7146 RVA: 0x0007282C File Offset: 0x00070A2C
		public void Load()
		{
			lock (this)
			{
				if (!this.IsInitialized)
				{
					TBuilder builder = Activator.CreateInstance<TBuilder>();
					if (this.externalConfigurationSetter != null)
					{
						this.externalConfigurationSetter(builder);
					}
					builder.SetLoader(this);
					builder.Register();
					this.Reload(null);
					if (this.reloadTimer != null)
					{
						this.reloadTimer.Change(this.reloadInterval, this.reloadInterval);
					}
				}
				if (!this.IsInitialized)
				{
					throw new TransportComponentLoadFailedException(Strings.ConfigurationLoaderFailed(typeof(TCache).Name), null);
				}
			}
		}

		// Token: 0x06001BEB RID: 7147 RVA: 0x000728F0 File Offset: 0x00070AF0
		public void Unload()
		{
			foreach (ADNotificationRequestCookie requestCookie in this.cookies)
			{
				ADNotificationAdapter.UnregisterChangeNotification(requestCookie, true);
			}
			if (this.reloadTimer != null)
			{
				this.reloadTimer.Dispose(true);
				this.reloadTimer = null;
			}
			this.cookies.Clear();
			this.cache = default(TCache);
		}

		// Token: 0x06001BEC RID: 7148 RVA: 0x00072978 File Offset: 0x00070B78
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x0007297B File Offset: 0x00070B7B
		public void Reload(object sender, EventArgs e)
		{
			this.Reload(null);
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x00072984 File Offset: 0x00070B84
		private void TimedReload(object state)
		{
			this.Reload(true);
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x0007298D File Offset: 0x00070B8D
		protected void Reload(ADNotificationEventArgs args)
		{
			this.Reload(false);
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x00072998 File Offset: 0x00070B98
		private void Reload(bool timerReload)
		{
			lock (this)
			{
				if (!timerReload || !(DateTime.UtcNow.Subtract(this.lastNotificationUpdate) < this.reloadInterval))
				{
					TBuilder tbuilder = this.LoadFromAD();
					if (tbuilder != null)
					{
						TCache tcache = tbuilder.BuildCache();
						if (tcache != null)
						{
							this.cache = tcache;
							ConfigurationUpdateHandler<TCache> changed = this.Changed;
							if (changed != null)
							{
								changed(tcache);
							}
							if (timerReload)
							{
								ConfigurationLoader<TCache, TBuilder>.Log.LogEvent(TransportEventLogConstants.Tuple_ConfigurationLoaderSuccessfulForcedUpdate, null, new object[]
								{
									typeof(TCache),
									this.lastDescription,
									tbuilder.Description
								});
							}
							else
							{
								this.lastNotificationUpdate = DateTime.UtcNow;
								this.lastDescription = tbuilder.Description;
								ConfigurationLoader<TCache, TBuilder>.Log.LogEvent(TransportEventLogConstants.Tuple_ConfigurationLoaderSuccessfulUpdate, null, new object[]
								{
									typeof(TCache)
								});
							}
						}
						else
						{
							ConfigurationLoader<TCache, TBuilder>.Log.LogEvent(TransportEventLogConstants.Tuple_ConfigurationLoaderExternalError, this.ToString(), new object[]
							{
								typeof(TCache),
								tbuilder.FailureMessage
							});
						}
					}
				}
			}
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x00072B48 File Offset: 0x00070D48
		private TBuilder LoadFromAD()
		{
			TBuilder builder = Activator.CreateInstance<TBuilder>();
			if (this.externalConfigurationSetter != null)
			{
				this.externalConfigurationSetter(builder);
			}
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				builder.LoadData(this.session, QueryScope.SubTree);
			});
			if (!adoperationResult.Succeeded)
			{
				ConfigurationLoader<TCache, TBuilder>.Log.LogEvent(TransportEventLogConstants.Tuple_ConfigurationLoaderException, this.ToString(), new object[]
				{
					typeof(TCache),
					adoperationResult.Exception
				});
				return default(TBuilder);
			}
			return builder;
		}

		// Token: 0x04000D2B RID: 3371
		public const int DefaultRegisterNotificationRetryCount = 3;

		// Token: 0x04000D2C RID: 3372
		private static readonly ExEventLog Log = new ExEventLog(ExTraceGlobals.ConfigurationTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x04000D2D RID: 3373
		private readonly ITopologyConfigurationSession session;

		// Token: 0x04000D2E RID: 3374
		private TCache cache;

		// Token: 0x04000D2F RID: 3375
		private List<ADNotificationRequestCookie> cookies;

		// Token: 0x04000D30 RID: 3376
		private ConfigurationLoader<TCache, TBuilder>.ExternalConfigurationSetter externalConfigurationSetter;

		// Token: 0x04000D31 RID: 3377
		private GuardedTimer reloadTimer;

		// Token: 0x04000D32 RID: 3378
		private TimeSpan reloadInterval;

		// Token: 0x04000D33 RID: 3379
		private DateTime lastNotificationUpdate = DateTime.UtcNow;

		// Token: 0x04000D34 RID: 3380
		private string lastDescription = string.Empty;

		// Token: 0x0200028B RID: 651
		// (Invoke) Token: 0x06001BF4 RID: 7156
		public delegate void ExternalConfigurationSetter(TBuilder builder);

		// Token: 0x0200028C RID: 652
		public abstract class Builder
		{
			// Token: 0x17000752 RID: 1874
			// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x00072BFF File Offset: 0x00070DFF
			// (set) Token: 0x06001BF8 RID: 7160 RVA: 0x00072C07 File Offset: 0x00070E07
			public string Description
			{
				get
				{
					return this.description;
				}
				set
				{
					this.description = value;
				}
			}

			// Token: 0x17000753 RID: 1875
			// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x00072C10 File Offset: 0x00070E10
			// (set) Token: 0x06001BFA RID: 7162 RVA: 0x00072C18 File Offset: 0x00070E18
			public LocalizedString FailureMessage
			{
				get
				{
					return this.failureMessage;
				}
				set
				{
					this.failureMessage = value;
				}
			}

			// Token: 0x06001BFB RID: 7163 RVA: 0x00072C21 File Offset: 0x00070E21
			public void SetLoader(ConfigurationLoader<TCache, TBuilder> loader)
			{
				this.loader = loader;
			}

			// Token: 0x06001BFC RID: 7164
			public abstract void Register();

			// Token: 0x06001BFD RID: 7165
			public abstract void LoadData(ITopologyConfigurationSession session, QueryScope scope);

			// Token: 0x06001BFE RID: 7166
			public abstract TCache BuildCache();

			// Token: 0x06001BFF RID: 7167 RVA: 0x00072C2C File Offset: 0x00070E2C
			protected static ADObjectId GetFirstOrgContainerId()
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 457, "GetFirstOrgContainerId", "f:\\15.00.1497\\sources\\dev\\Transport\\src\\Configuration\\ConfigurationLoader.cs");
				return tenantOrTopologyConfigurationSession.GetOrgContainerId();
			}

			// Token: 0x06001C00 RID: 7168 RVA: 0x00072C5F File Offset: 0x00070E5F
			protected void Register<T>() where T : ADConfigurationObject, new()
			{
				this.Register<T>(null);
			}

			// Token: 0x06001C01 RID: 7169 RVA: 0x00072C68 File Offset: 0x00070E68
			protected void Register<T>(Func<ADObjectId> rootIdGetter) where T : ADConfigurationObject, new()
			{
				this.RegisterWithAD<T>(rootIdGetter);
			}

			// Token: 0x06001C02 RID: 7170 RVA: 0x00072C74 File Offset: 0x00070E74
			private void RegisterWithAD<T>(Func<ADObjectId> rootIdGetter) where T : ADConfigurationObject, new()
			{
				ADNotificationRequestCookie item;
				ADOperationResult adoperationResult = this.TryRegisterChangeNotification<T>(rootIdGetter, out item);
				if (adoperationResult != ADOperationResult.Success)
				{
					ConfigurationLoader<TCache, TBuilder>.Log.LogEvent(TransportEventLogConstants.Tuple_ConfigurationLoaderException, this.ToString(), new object[]
					{
						typeof(TCache),
						adoperationResult.Exception
					});
					throw new TransportComponentLoadFailedException(Strings.ConfigurationLoaderFailed(typeof(TCache).Name), null);
				}
				this.loader.cookies.Add(item);
			}

			// Token: 0x06001C03 RID: 7171 RVA: 0x00072CF8 File Offset: 0x00070EF8
			protected virtual ADOperationResult TryRegisterChangeNotification<TConfigObject>(Func<ADObjectId> rootIdGetter, out ADNotificationRequestCookie cookie) where TConfigObject : ADConfigurationObject, new()
			{
				cookie = null;
				return ADNotificationAdapter.TryRegisterChangeNotification<TConfigObject>(rootIdGetter, new ADNotificationCallback(this.Reload), null, 3, out cookie);
			}

			// Token: 0x06001C04 RID: 7172 RVA: 0x00072D1F File Offset: 0x00070F1F
			protected void Reload(ADNotificationEventArgs args)
			{
				this.loader.Reload(false);
			}

			// Token: 0x04000D36 RID: 3382
			private ConfigurationLoader<TCache, TBuilder> loader;

			// Token: 0x04000D37 RID: 3383
			private LocalizedString failureMessage = LocalizedString.Empty;

			// Token: 0x04000D38 RID: 3384
			private string description = string.Empty;
		}

		// Token: 0x0200028D RID: 653
		public abstract class SimpleBuilder<TConfigObject> : ConfigurationLoader<TCache, TBuilder>.Builder where TConfigObject : ADConfigurationObject, new()
		{
			// Token: 0x06001C06 RID: 7174 RVA: 0x00072D53 File Offset: 0x00070F53
			public override void Register()
			{
				base.Register<TConfigObject>(() => this.RootId);
			}

			// Token: 0x06001C07 RID: 7175 RVA: 0x00072D67 File Offset: 0x00070F67
			public override void LoadData(ITopologyConfigurationSession session, QueryScope scope = QueryScope.SubTree)
			{
				this.configObjects = new List<TConfigObject>(session.FindPaged<TConfigObject>(this.RootId, scope, null, null, 0));
			}

			// Token: 0x06001C08 RID: 7176 RVA: 0x00072D84 File Offset: 0x00070F84
			public override TCache BuildCache()
			{
				return this.BuildCache(this.configObjects);
			}

			// Token: 0x17000754 RID: 1876
			// (get) Token: 0x06001C09 RID: 7177 RVA: 0x00072D92 File Offset: 0x00070F92
			// (set) Token: 0x06001C0A RID: 7178 RVA: 0x00072D9A File Offset: 0x00070F9A
			protected ADObjectId RootId
			{
				get
				{
					return this.rootId;
				}
				set
				{
					this.rootId = value;
				}
			}

			// Token: 0x06001C0B RID: 7179
			protected abstract TCache BuildCache(List<TConfigObject> configObjects);

			// Token: 0x04000D39 RID: 3385
			private List<TConfigObject> configObjects;

			// Token: 0x04000D3A RID: 3386
			private ADObjectId rootId;
		}
	}
}
