using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Hygiene.Data.DataProvider;
using Microsoft.Exchange.Hygiene.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Sync
{
	// Token: 0x02000227 RID: 551
	internal class SyncSession
	{
		// Token: 0x06001657 RID: 5719 RVA: 0x000452CF File Offset: 0x000434CF
		public SyncSession()
		{
			this.DataProvider = ConfigDataProviderFactory.Default.Create(DatabaseType.Directory);
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x000452E8 File Offset: 0x000434E8
		public SyncSession(Guid caller) : this()
		{
			this.Caller = caller;
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x000452F7 File Offset: 0x000434F7
		// (set) Token: 0x0600165A RID: 5722 RVA: 0x000452FF File Offset: 0x000434FF
		public Guid Caller { get; private set; }

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x00045308 File Offset: 0x00043508
		// (set) Token: 0x0600165C RID: 5724 RVA: 0x00045310 File Offset: 0x00043510
		internal IConfigDataProvider DataProvider { get; set; }

		// Token: 0x0600165D RID: 5725 RVA: 0x0004531C File Offset: 0x0004351C
		public bool AcquireServiceCookie(ServiceCookieFilter filter, out ServiceCookie cookie)
		{
			if (filter == null)
			{
				throw new ArgumentException("filter");
			}
			if (this.Caller == Guid.Empty)
			{
				throw new IllegalOperationNoCallerSpecifiedException();
			}
			IConfigurable[] source = this.DataProvider.Find<ServiceCookie>(SyncSession.BuildFindCookieQuery(this.Caller, filter, true), null, true, null);
			cookie = (source.FirstOrDefault<IConfigurable>() as ServiceCookie);
			return cookie != null;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00045380 File Offset: 0x00043580
		public bool AcquireTenantCookie(TenantCookieFilter filter, out TenantCookie cookie)
		{
			if (filter == null)
			{
				throw new ArgumentException("filter");
			}
			if (this.Caller == Guid.Empty)
			{
				throw new IllegalOperationNoCallerSpecifiedException();
			}
			IConfigurable[] source = this.DataProvider.Find<TenantCookie>(SyncSession.BuildFindCookieQuery(this.Caller, filter, true), null, true, null);
			cookie = (source.FirstOrDefault<IConfigurable>() as TenantCookie);
			return cookie != null;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x000453E4 File Offset: 0x000435E4
		public T[] FindCookies<T>(BaseCookieFilter filter) where T : IConfigurable, new()
		{
			if (filter == null)
			{
				throw new ArgumentException("filter");
			}
			if (this.Caller == Guid.Empty)
			{
				throw new IllegalOperationNoCallerSpecifiedException();
			}
			return this.DataProvider.Find<T>(SyncSession.BuildFindCookieQuery(this.Caller, filter, false), null, true, null).Cast<T>().ToArray<T>();
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0004543C File Offset: 0x0004363C
		public IEnumerable<UnsyncedObject> FindUnsyncedObjects(string serviceInstance, ADObjectId tenantId = null, TimeSpan? cooldown = null)
		{
			if (string.IsNullOrEmpty(serviceInstance))
			{
				throw new ArgumentNullException("serviceInstance");
			}
			return this.DataProvider.Find<UnsyncedObject>(SyncSession.BuildFindUnsyncedObjectQuery(serviceInstance, tenantId, cooldown ?? SyncSession.DefaultCooldown), null, true, null).Cast<UnsyncedObject>();
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0004548F File Offset: 0x0004368F
		public IEnumerable<AssignedPlan> FindUnpublishedPlans(string serviceInstance)
		{
			if (string.IsNullOrEmpty(serviceInstance))
			{
				throw new ArgumentNullException("serviceInstance");
			}
			return this.DataProvider.Find<AssignedPlan>(SyncSession.BuildFindUnpublishedObjectQuery(serviceInstance), null, true, null).Cast<AssignedPlan>();
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x000454BD File Offset: 0x000436BD
		public IEnumerable<UnpublishedObject> FindUnpublishedObjects(string serviceInstance)
		{
			if (string.IsNullOrEmpty(serviceInstance))
			{
				throw new ArgumentNullException("serviceInstance");
			}
			return this.DataProvider.Find<UnpublishedObject>(SyncSession.BuildFindUnpublishedObjectQuery(serviceInstance), null, true, null).Cast<UnpublishedObject>();
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x000454EC File Offset: 0x000436EC
		public IEnumerable<AcceptedDomain> FindUnprovisionedDomains(string serviceInstance)
		{
			if (string.IsNullOrEmpty(serviceInstance))
			{
				throw new ArgumentNullException("serviceInstance");
			}
			QueryFilter filter = new AndFilter(new ComparisonFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, CommonSyncProperties.PublishedProp, false),
				new ComparisonFilter(ComparisonOperator.Equal, CommonSyncProperties.ServiceInstanceProp, serviceInstance)
			});
			IEnumerable<AcceptedDomain> enumerable = this.DataProvider.Find<AcceptedDomain>(filter, null, true, null).Cast<AcceptedDomain>();
			foreach (AcceptedDomain acceptedDomain in enumerable)
			{
				acceptedDomain[CommonSyncProperties.PublishedProp] = true;
				if ((bool)acceptedDomain[SyncSession.IsDeletedProp])
				{
					acceptedDomain[ADObjectSchema.ObjectState] = ObjectState.Deleted;
				}
			}
			return enumerable;
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x000455CB File Offset: 0x000437CB
		public bool SyncStreamShouldRefreshProperties(string serviceInstance)
		{
			return this.FindSyncPropertyRefresh(serviceInstance).Any((SyncPropertyRefresh r) => r.Status == SyncPropertyRefreshStatus.Requested);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x000455F8 File Offset: 0x000437F8
		internal IEnumerable<SyncPropertyRefresh> FindSyncPropertyRefresh(string serviceInstance)
		{
			if (string.IsNullOrEmpty(serviceInstance))
			{
				throw new ArgumentNullException("serviceInstance");
			}
			QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, serviceInstance);
			return this.DataProvider.Find<SyncPropertyRefresh>(filter, null, false, null).Cast<SyncPropertyRefresh>();
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00045644 File Offset: 0x00043844
		public void CompletePropertyRefresh(string serviceInstance)
		{
			foreach (SyncPropertyRefresh syncPropertyRefresh in from r in this.FindSyncPropertyRefresh(serviceInstance)
			where r.Status == SyncPropertyRefreshStatus.InProgress
			select r)
			{
				syncPropertyRefresh.Status = SyncPropertyRefreshStatus.Completed;
				this.Save(syncPropertyRefresh);
			}
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x000456C8 File Offset: 0x000438C8
		public void SetSyncPropertyRefreshInProgress(string serviceInstance)
		{
			IEnumerable<SyncPropertyRefresh> enumerable = from r in this.FindSyncPropertyRefresh(serviceInstance)
			where r.Status == SyncPropertyRefreshStatus.Requested
			select r;
			if (!enumerable.Any<SyncPropertyRefresh>())
			{
				throw new InvalidOperationException(string.Format("Refresh not requested for service instance {0}", serviceInstance));
			}
			foreach (SyncPropertyRefresh syncPropertyRefresh in enumerable)
			{
				syncPropertyRefresh.Status = SyncPropertyRefreshStatus.InProgress;
				this.Save(syncPropertyRefresh);
			}
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0004575C File Offset: 0x0004395C
		public void Save(IConfigurable configurable)
		{
			if (configurable == null)
			{
				throw new ArgumentNullException("configurable");
			}
			BaseCookie baseCookie = configurable as BaseCookie;
			if (baseCookie != null)
			{
				if (this.Caller == Guid.Empty)
				{
					throw new IllegalOperationNoCallerSpecifiedException();
				}
				baseCookie.ActiveMachine = Environment.MachineName;
				baseCookie[BaseCookieSchema.CallerProp] = this.Caller;
				baseCookie[BaseCookieSchema.AllowNullCookieProp] = false;
			}
			this.DataProvider.Save(configurable);
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x000457D8 File Offset: 0x000439D8
		public void ResetCookie(BaseCookie cookie)
		{
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			if (this.Caller == Guid.Empty)
			{
				throw new IllegalOperationNoCallerSpecifiedException();
			}
			cookie[BaseCookieSchema.CallerProp] = this.Caller;
			cookie[BaseCookieSchema.AllowNullCookieProp] = true;
			this.DataProvider.Save(cookie);
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x00045840 File Offset: 0x00043A40
		internal static QueryFilter BuildFindCookieQuery(Guid caller, BaseCookieFilter cookieFilter, bool acquireCookie)
		{
			ComparisonFilter item = new ComparisonFilter(ComparisonOperator.Equal, BaseCookieSchema.CallerProp, caller);
			ComparisonFilter item2 = new ComparisonFilter(ComparisonOperator.Equal, BaseCookieSchema.AcquireCookieLockProp, acquireCookie);
			List<QueryFilter> queryFilters = cookieFilter.GetQueryFilters();
			queryFilters.Add(item);
			queryFilters.Add(item2);
			return new AndFilter(queryFilters.ToArray());
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00045894 File Offset: 0x00043A94
		internal static QueryFilter BuildFindUnsyncedObjectQuery(string serviceInstance, ADObjectId tenantId, TimeSpan cooldown)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			list.Add(new ComparisonFilter(ComparisonOperator.Equal, UnpublishedObjectSchema.ServiceInstanceProp, serviceInstance));
			if (tenantId != null)
			{
				list.Add(new ComparisonFilter(ComparisonOperator.Equal, UnpublishedObjectSchema.TenantIdProp, tenantId));
			}
			list.Add(new ComparisonFilter(ComparisonOperator.Equal, SyncSession.CooldownProp, cooldown.Minutes));
			return new AndFilter(list.ToArray());
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x000458F6 File Offset: 0x00043AF6
		internal static QueryFilter BuildFindUnpublishedObjectQuery(string serviceInstance)
		{
			return new ComparisonFilter(ComparisonOperator.Equal, UnpublishedObjectSchema.ServiceInstanceProp, serviceInstance);
		}

		// Token: 0x04000B52 RID: 2898
		public static readonly HygienePropertyDefinition IsDeletedProp = new HygienePropertyDefinition("isDeleted", typeof(bool), false, ExchangeObjectVersion.Exchange2007, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000B53 RID: 2899
		public static readonly HygienePropertyDefinition CooldownProp = new HygienePropertyDefinition("Cooldown", typeof(int), -1, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000B54 RID: 2900
		private static readonly TimeSpan DefaultCooldown = TimeSpan.FromSeconds(0.0);
	}
}
