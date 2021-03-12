using System;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration
{
	// Token: 0x0200014D RID: 333
	internal abstract class PerTenantConfigurationLoader<T>
	{
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x00027430 File Offset: 0x00025630
		protected virtual bool RefreshOnChange
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x00027440 File Offset: 0x00025640
		public PerTenantConfigurationLoader(OrganizationId organizationId)
		{
			this.organizationId = organizationId;
		}

		// Token: 0x06000924 RID: 2340 RVA: 0x0002749C File Offset: 0x0002569C
		public PerTenantConfigurationLoader(OrganizationId organizationId, TimeSpan timeoutInterval)
		{
			this.organizationId = organizationId;
			this.timeoutInterval = timeoutInterval;
		}

		// Token: 0x06000925 RID: 2341
		public abstract void Initialize();

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x000274F4 File Offset: 0x000256F4
		protected T Data
		{
			get
			{
				if (this.HasExpired())
				{
					IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), this.organizationId, null, false), 107, "Data", "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\OrganizationConfiguration\\PerTenantConfigurationLoader.cs");
					this.InternalRead(tenantOrTopologyConfigurationSession, true);
					this.startTime = ExDateTime.Now;
				}
				return this.data;
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0002754C File Offset: 0x0002574C
		protected void Initialize(object notificationLock)
		{
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), this.organizationId, null, false), 132, "Initialize", "f:\\15.00.1497\\sources\\dev\\infoworker\\src\\common\\OrganizationConfiguration\\PerTenantConfigurationLoader.cs");
			if (this.data == null || this.notificationCookie == null)
			{
				this.InternalRead(tenantOrTopologyConfigurationSession, false);
				this.startTime = ExDateTime.Now;
			}
			if (this.organizationId != OrganizationId.ForestWideOrgId || VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Global.MultiTenancy.Enabled)
			{
				return;
			}
			if (this.notificationCookie == null)
			{
				lock (notificationLock)
				{
					if (this.notificationCookie == null)
					{
						this.RegisterChangeNotification(tenantOrTopologyConfigurationSession);
					}
				}
			}
		}

		// Token: 0x06000928 RID: 2344
		protected abstract T Read(IConfigurationSession session);

		// Token: 0x06000929 RID: 2345
		protected abstract ADNotificationRequestCookie Register(IConfigurationSession session);

		// Token: 0x0600092A RID: 2346 RVA: 0x00027684 File Offset: 0x00025884
		protected void ChangeCallback(ADNotificationEventArgs args)
		{
			IConfigurationSession session = (IConfigurationSession)args.Context;
			this.adChangeCoalescer.Value.OnEvent(delegate
			{
				try
				{
					this.InternalRead(session, this.RefreshOnChange);
				}
				catch (DataSourceTransientException)
				{
				}
				catch (DataSourceOperationException)
				{
				}
				catch (DataValidationException)
				{
				}
			});
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000276F4 File Offset: 0x000258F4
		private void RegisterChangeNotification(IConfigurationSession session)
		{
			int retryCount = 3;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				this.notificationCookie = this.Register(session);
			}, retryCount);
			if (adoperationResult.Exception != null)
			{
				CachedOrganizationConfiguration.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToRegisterChangeNotification, adoperationResult.Exception.GetType().FullName, new object[]
				{
					typeof(T).Name,
					adoperationResult.Exception.GetType().FullName,
					adoperationResult.Exception.Message
				});
				if (!(adoperationResult.Exception is DataSourceTransientException))
				{
					throw adoperationResult.Exception;
				}
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x000277C4 File Offset: 0x000259C4
		protected void InternalRead(IConfigurationSession session, bool force = false)
		{
			T newData = default(T);
			OrganizationProperties organizationProperties;
			if (!OrganizationPropertyCache.TryGetOrganizationProperties(this.organizationId, out organizationProperties) || force || !organizationProperties.TryGetValue<T>(out newData))
			{
				int retryCount = 3;
				ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					newData = this.Read(session);
				}, retryCount);
				if (adoperationResult.Exception != null)
				{
					CachedOrganizationConfiguration.Logger.LogEvent(InfoWorkerEventLogConstants.Tuple_UnableToReadConfigurationFromAD, adoperationResult.Exception.GetType().FullName, new object[]
					{
						typeof(T).Name,
						adoperationResult.Exception.GetType().FullName,
						adoperationResult.Exception.Message
					});
					throw adoperationResult.Exception;
				}
				if (adoperationResult.Succeeded && organizationProperties != null)
				{
					organizationProperties.SetValue<T>(newData);
				}
			}
			this.data = newData;
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x000278CA File Offset: 0x00025ACA
		private bool HasExpired()
		{
			return this.timeoutInterval != TimeSpan.Zero && ExDateTime.Now - this.startTime > this.timeoutInterval;
		}

		// Token: 0x0400071F RID: 1823
		private ADNotificationRequestCookie notificationCookie;

		// Token: 0x04000720 RID: 1824
		private readonly TimeSpan timeoutInterval = TimeSpan.Zero;

		// Token: 0x04000721 RID: 1825
		private ExDateTime startTime;

		// Token: 0x04000722 RID: 1826
		protected OrganizationId organizationId;

		// Token: 0x04000723 RID: 1827
		protected T data;

		// Token: 0x04000724 RID: 1828
		private Lazy<PerTenantConfigurationLoader<T>.Coalescer> adChangeCoalescer = new Lazy<PerTenantConfigurationLoader<T>.Coalescer>(() => new PerTenantConfigurationLoader<T>.Coalescer(PerTenantConfigurationLoader<T>.adChangeCoalescingTime), LazyThreadSafetyMode.PublicationOnly);

		// Token: 0x04000725 RID: 1829
		private static readonly TimeSpan adChangeCoalescingTime = new TimeSpan(0, 0, 5);

		// Token: 0x0200014E RID: 334
		private class Coalescer
		{
			// Token: 0x06000931 RID: 2353 RVA: 0x0002790A File Offset: 0x00025B0A
			public Coalescer(TimeSpan delay)
			{
				this.delay = delay;
			}

			// Token: 0x06000932 RID: 2354 RVA: 0x0002791C File Offset: 0x00025B1C
			private void TimerCallback(Action callback)
			{
				lock (this)
				{
					this.timer.Dispose();
					this.timer = null;
				}
				callback();
			}

			// Token: 0x06000933 RID: 2355 RVA: 0x00027988 File Offset: 0x00025B88
			public void OnEvent(Action callback)
			{
				lock (this)
				{
					if (this.timer == null)
					{
						this.timer = new Timer(delegate(object o)
						{
							this.TimerCallback(callback);
						}, null, this.delay, new TimeSpan(-1L));
					}
					else
					{
						this.timer.Change(this.delay, new TimeSpan(-1L));
					}
				}
			}

			// Token: 0x04000728 RID: 1832
			private Timer timer;

			// Token: 0x04000729 RID: 1833
			private readonly TimeSpan delay;
		}
	}
}
