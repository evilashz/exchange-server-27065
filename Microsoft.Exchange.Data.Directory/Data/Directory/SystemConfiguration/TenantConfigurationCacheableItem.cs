using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200062F RID: 1583
	internal abstract class TenantConfigurationCacheableItem<TConfig> : TenantConfigurationCacheableItemBase where TConfig : ADConfigurationObject, new()
	{
		// Token: 0x06004B03 RID: 19203 RVA: 0x00114CAB File Offset: 0x00112EAB
		protected TenantConfigurationCacheableItem()
		{
		}

		// Token: 0x06004B04 RID: 19204 RVA: 0x00114CB3 File Offset: 0x00112EB3
		protected TenantConfigurationCacheableItem(bool initialized)
		{
			this.initialized = initialized;
		}

		// Token: 0x170018CC RID: 6348
		// (get) Token: 0x06004B05 RID: 19205 RVA: 0x00114CC2 File Offset: 0x00112EC2
		public OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x06004B06 RID: 19206 RVA: 0x00114CCC File Offset: 0x00112ECC
		internal static void HandleChangeNotification(ADNotificationEventArgs args)
		{
			CacheNotificationArgs cacheNotificationArgs = (CacheNotificationArgs)args.Context;
			cacheNotificationArgs.CacheNotificationHandler(cacheNotificationArgs.OrganizationId);
		}

		// Token: 0x06004B07 RID: 19207
		public abstract void ReadData(IConfigurationSession session);

		// Token: 0x06004B08 RID: 19208 RVA: 0x00114CF6 File Offset: 0x00112EF6
		public virtual void ReadData(IConfigurationSession session, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004B09 RID: 19209 RVA: 0x00114D00 File Offset: 0x00112F00
		public override bool InitializeWithoutRegistration(IConfigurationSession adSession, bool allowExceptions)
		{
			this.organizationId = adSession.SessionSettings.CurrentOrganizationId;
			return this.initialized = this.InternalRead(adSession, allowExceptions, null);
		}

		// Token: 0x06004B0A RID: 19210 RVA: 0x00114D30 File Offset: 0x00112F30
		public override bool TryInitialize(OrganizationId organizationId, CacheNotificationHandler cacheNotificationHandler, object state)
		{
			IConfigurationSession configurationSession = this.Initialize(organizationId, false);
			if (configurationSession == null)
			{
				return false;
			}
			if (this.InternalRead(configurationSession, false, state) && this.RegisterChangeNotification(configurationSession, cacheNotificationHandler, false))
			{
				this.initialized = true;
				return true;
			}
			return false;
		}

		// Token: 0x06004B0B RID: 19211 RVA: 0x00114D6C File Offset: 0x00112F6C
		public override bool Initialize(OrganizationId organizationId, CacheNotificationHandler cacheNotificationHandler, object state)
		{
			IConfigurationSession configurationSession = this.Initialize(organizationId, true);
			if (configurationSession == null)
			{
				return false;
			}
			if (this.InternalRead(configurationSession, true, state) && this.RegisterChangeNotification(configurationSession, cacheNotificationHandler, true))
			{
				this.initialized = true;
				return true;
			}
			return false;
		}

		// Token: 0x06004B0C RID: 19212 RVA: 0x00114DB4 File Offset: 0x00112FB4
		public override void UnregisterChangeNotification()
		{
			if (this.notificationCookie != null)
			{
				this.TryRunADOperation(delegate
				{
					ADNotificationAdapter.UnregisterChangeNotification(this.notificationCookie);
				}, false);
			}
		}

		// Token: 0x06004B0D RID: 19213 RVA: 0x00114DE4 File Offset: 0x00112FE4
		protected void ThrowIfNotInitialized(object source)
		{
			if (!this.initialized)
			{
				throw new InvalidOperationException(source.ToString() + " is not initialized and cannot be used");
			}
		}

		// Token: 0x06004B0E RID: 19214 RVA: 0x00114E64 File Offset: 0x00113064
		protected virtual IConfigurationSession CreateSession(bool throwExceptions)
		{
			TConfig tconfig = Activator.CreateInstance<TConfig>();
			if (tconfig.IsShareable)
			{
				IConfigurationSession sharedConfigSession = null;
				this.TryRunADOperation(delegate
				{
					sharedConfigSession = SharedConfiguration.CreateScopedToSharedConfigADSession(this.organizationId);
				}, throwExceptions);
				return sharedConfigSession;
			}
			IConfigurationSession session = null;
			this.TryRunADOperation(delegate
			{
				session = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.organizationId), 214, "CreateSession", "f:\\15.00.1497\\sources\\dev\\data\\src\\directory\\SystemConfiguration\\ConfigurationCache\\TenantConfigurationCacheableItem.cs");
			}, throwExceptions);
			return session;
		}

		// Token: 0x06004B0F RID: 19215 RVA: 0x00114EE3 File Offset: 0x001130E3
		private IConfigurationSession Initialize(OrganizationId organizationId, bool throwExceptions)
		{
			this.organizationId = organizationId;
			return this.CreateSession(throwExceptions);
		}

		// Token: 0x06004B10 RID: 19216 RVA: 0x00114EF4 File Offset: 0x001130F4
		private bool RegisterChangeNotification(IConfigurationSession session, CacheNotificationHandler cacheNotificationHandler, bool throwExceptions)
		{
			if (!OrganizationId.ForestWideOrgId.Equals(this.organizationId))
			{
				return true;
			}
			try
			{
				this.notificationCookie = ADNotificationAdapter.RegisterChangeNotification<TConfig>(this.organizationId.ConfigurationUnit ?? session.GetOrgContainerId(), new ADNotificationCallback(TenantConfigurationCacheableItem<TConfig>.HandleChangeNotification), new CacheNotificationArgs(cacheNotificationHandler, this.organizationId));
			}
			catch (TransientException)
			{
				if (!throwExceptions)
				{
					return false;
				}
				throw;
			}
			catch (DataSourceOperationException)
			{
				if (!throwExceptions)
				{
					return false;
				}
				throw;
			}
			return true;
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x00114FB8 File Offset: 0x001131B8
		private bool InternalRead(IConfigurationSession session, bool throwExceptions, object state)
		{
			if (state != null)
			{
				return this.TryRunADOperation(delegate
				{
					this.ReadData(session, state);
				}, throwExceptions);
			}
			return this.TryRunADOperation(delegate
			{
				this.ReadData(session);
			}, throwExceptions);
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x00115034 File Offset: 0x00113234
		private bool TryRunADOperation(ADOperation operation, bool throwExceptions)
		{
			int retryCount = 3;
			ADOperationResult adoperationResult = ADNotificationAdapter.TryRunADOperation(delegate()
			{
				operation();
			}, retryCount);
			if (adoperationResult.Exception == null)
			{
				return adoperationResult.Succeeded;
			}
			if (!throwExceptions)
			{
				ExTraceGlobals.ExceptionTracer.TraceError<Exception>((long)this.GetHashCode(), "Failed to run AD operation: {0}", adoperationResult.Exception);
				return false;
			}
			throw adoperationResult.Exception;
		}

		// Token: 0x0400339B RID: 13211
		private OrganizationId organizationId;

		// Token: 0x0400339C RID: 13212
		private ADNotificationRequestCookie notificationCookie;

		// Token: 0x0400339D RID: 13213
		private bool initialized;
	}
}
