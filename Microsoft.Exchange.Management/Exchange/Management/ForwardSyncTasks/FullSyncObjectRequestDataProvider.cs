using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000347 RID: 839
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FullSyncObjectRequestDataProvider : IConfigDataProvider
	{
		// Token: 0x06001CF7 RID: 7415 RVA: 0x0007FE9C File Offset: 0x0007E09C
		public FullSyncObjectRequestDataProvider(bool readOnly, string serviceInstanceName)
		{
			this.lastRidMasterRefreshTime = DateTime.MinValue;
			this.serviceInstanceName = serviceInstanceName;
			this.configurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(null, readOnly, ConsistencyMode.PartiallyConsistent, ADSessionSettings.FromRootOrgScopeSet(), 55, ".ctor", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\ForwardSync\\FullSyncObjectRequestDataProvider.cs");
			this.configurationSession.UseConfigNC = false;
			this.RefreshRidMasterInformation();
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x0007FEF8 File Offset: 0x0007E0F8
		private void RefreshRidMasterInformation()
		{
			if (DateTime.UtcNow - this.lastRidMasterRefreshTime > FullSyncObjectRequestDataProvider.RidMasterRefreshInterval)
			{
				RidMasterInfo ridMasterInfo = SyncDaemonArbitrationConfigHelper.GetRidMasterInfo(this.configurationSession);
				this.configurationSession.DomainController = ridMasterInfo.RidMasterServer;
				this.lastRidMasterRefreshTime = DateTime.UtcNow;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x0007FF49 File Offset: 0x0007E149
		public string Source
		{
			get
			{
				return this.configurationSession.Source;
			}
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x0007FF58 File Offset: 0x0007E158
		public IConfigurable Read<T>(ObjectId identity) where T : IConfigurable, new()
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			if (this.serviceInstanceName == null)
			{
				throw new InvalidOperationException("Read can be performed only for specific service instance.");
			}
			SyncObjectId syncObjectId = identity as SyncObjectId;
			if (syncObjectId == null)
			{
				throw new NotSupportedException(identity.GetType().FullName);
			}
			this.RefreshRidMasterInformation();
			MsoMainStreamCookieContainer msoMainStreamCookieContainer = this.configurationSession.GetMsoMainStreamCookieContainer(this.serviceInstanceName);
			MultiValuedProperty<FullSyncObjectRequest> msoForwardSyncObjectFullSyncRequests = msoMainStreamCookieContainer.MsoForwardSyncObjectFullSyncRequests;
			foreach (FullSyncObjectRequest fullSyncObjectRequest in msoForwardSyncObjectFullSyncRequests)
			{
				if (syncObjectId.Equals(fullSyncObjectRequest.Identity))
				{
					fullSyncObjectRequest.ResetChangeTracking(true);
					return fullSyncObjectRequest;
				}
			}
			return null;
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x00080020 File Offset: 0x0007E220
		public IConfigurable[] Find<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy) where T : IConfigurable, new()
		{
			return this.FindPaged<T>(filter, rootId, deepSearch, sortBy, 0).Cast<IConfigurable>().ToArray<IConfigurable>();
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x00080040 File Offset: 0x0007E240
		public IEnumerable<T> FindPaged<T>(QueryFilter filter, ObjectId rootId, bool deepSearch, SortBy sortBy, int pageSize) where T : IConfigurable, new()
		{
			Func<SyncServiceInstance, string> func = null;
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (filter != null && comparisonFilter == null)
			{
				throw new NotSupportedException(filter.GetType().FullName);
			}
			if (comparisonFilter != null && comparisonFilter.ComparisonOperator != ComparisonOperator.Equal)
			{
				throw new NotSupportedException(comparisonFilter.ComparisonOperator.ToString());
			}
			this.RefreshRidMasterInformation();
			List<string> list = new List<string>();
			if (this.serviceInstanceName != null)
			{
				list.Add(this.serviceInstanceName);
			}
			else
			{
				List<string> list2 = list;
				IEnumerable<SyncServiceInstance> allServiceInstances = this.GetAllServiceInstances();
				if (func == null)
				{
					func = ((SyncServiceInstance i) => i.Name);
				}
				list2.AddRange(allServiceInstances.Select(func));
			}
			List<T> list3 = new List<T>();
			foreach (string text in list)
			{
				list3.AddRange(this.FindInServiceInstance<T>(comparisonFilter, text));
			}
			return list3;
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x00080128 File Offset: 0x0007E328
		private IEnumerable<SyncServiceInstance> GetAllServiceInstances()
		{
			return this.configurationSession.FindPaged<SyncServiceInstance>(SyncServiceInstance.GetMsoSyncRootContainer(), QueryScope.OneLevel, null, null, 0);
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x00080170 File Offset: 0x0007E370
		private IEnumerable<T> FindInServiceInstance<T>(ComparisonFilter comparisonFilter, string serviceInstanceName)
		{
			MsoMainStreamCookieContainer msoMainStreamCookieContainer = this.configurationSession.GetMsoMainStreamCookieContainer(serviceInstanceName);
			MultiValuedProperty<FullSyncObjectRequest> msoForwardSyncObjectFullSyncRequests = msoMainStreamCookieContainer.MsoForwardSyncObjectFullSyncRequests;
			if (comparisonFilter == null)
			{
				return (IEnumerable<T>)msoForwardSyncObjectFullSyncRequests;
			}
			return (IEnumerable<T>)(from request in msoForwardSyncObjectFullSyncRequests
			where StringComparer.OrdinalIgnoreCase.Equals(request[comparisonFilter.Property], comparisonFilter.PropertyValue)
			select request);
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x00080218 File Offset: 0x0007E418
		public void Save(IConfigurable instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (this.configurationSession.ReadOnly)
			{
				throw new InvalidOperationException("read only");
			}
			if (this.serviceInstanceName == null)
			{
				throw new InvalidOperationException("Save can be performed only for specific service instance.");
			}
			FullSyncObjectRequest request = instance as FullSyncObjectRequest;
			if (request == null)
			{
				throw new NotSupportedException(instance.GetType().FullName);
			}
			this.RefreshRidMasterInformation();
			MsoMainStreamCookieContainer msoMainStreamCookieContainer = this.configurationSession.GetMsoMainStreamCookieContainer(this.serviceInstanceName);
			MultiValuedProperty<FullSyncObjectRequest> msoForwardSyncObjectFullSyncRequests = msoMainStreamCookieContainer.MsoForwardSyncObjectFullSyncRequests;
			FullSyncObjectRequest fullSyncObjectRequest = msoForwardSyncObjectFullSyncRequests.Find((FullSyncObjectRequest r) => request.Identity.Equals(r.Identity) && request.ServiceInstanceId == r.ServiceInstanceId);
			if (fullSyncObjectRequest != null)
			{
				if (request.ObjectState == ObjectState.New)
				{
					throw new ADObjectAlreadyExistsException(DirectoryStrings.ExceptionADOperationFailedAlreadyExist(this.configurationSession.DomainController, request.ToString()));
				}
				if (request.ObjectState == ObjectState.Changed)
				{
					msoForwardSyncObjectFullSyncRequests.Remove(fullSyncObjectRequest);
				}
			}
			else
			{
				IEnumerable<FullSyncObjectRequest> source = from r in msoForwardSyncObjectFullSyncRequests
				where request.ServiceInstanceId == r.ServiceInstanceId
				select r;
				int maxObjectFullSyncRequestsPerServiceInstance = ProvisioningTasksConfigImpl.MaxObjectFullSyncRequestsPerServiceInstance;
				if (source.Count<FullSyncObjectRequest>() >= maxObjectFullSyncRequestsPerServiceInstance)
				{
					throw new DataSourceOperationException(Strings.OperationExceedsPerServiceInstanceFullSyncObjectRequestLimit(maxObjectFullSyncRequestsPerServiceInstance, request.ServiceInstanceId));
				}
			}
			msoForwardSyncObjectFullSyncRequests.Add(request);
			this.configurationSession.Save(msoMainStreamCookieContainer);
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x00080370 File Offset: 0x0007E570
		public void Delete(IConfigurable instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (this.configurationSession.ReadOnly)
			{
				throw new InvalidOperationException("read only");
			}
			if (this.serviceInstanceName == null)
			{
				throw new InvalidOperationException("Delete can be performed only for specific service instance.");
			}
			FullSyncObjectRequest fullSyncObjectRequest = instance as FullSyncObjectRequest;
			if (fullSyncObjectRequest == null)
			{
				throw new NotSupportedException(instance.GetType().FullName);
			}
			this.RefreshRidMasterInformation();
			MsoMainStreamCookieContainer msoMainStreamCookieContainer = this.configurationSession.GetMsoMainStreamCookieContainer(this.serviceInstanceName);
			MultiValuedProperty<FullSyncObjectRequest> msoForwardSyncObjectFullSyncRequests = msoMainStreamCookieContainer.MsoForwardSyncObjectFullSyncRequests;
			if (msoForwardSyncObjectFullSyncRequests.Contains(fullSyncObjectRequest))
			{
				msoForwardSyncObjectFullSyncRequests.Remove(fullSyncObjectRequest);
				this.configurationSession.Save(msoMainStreamCookieContainer);
				return;
			}
			throw new ADNoSuchObjectException(DirectoryStrings.ExceptionADOperationFailedNoSuchObject(this.configurationSession.DomainController, fullSyncObjectRequest.ToString()));
		}

		// Token: 0x0400186E RID: 6254
		private static readonly TimeSpan RidMasterRefreshInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400186F RID: 6255
		private readonly ITopologyConfigurationSession configurationSession;

		// Token: 0x04001870 RID: 6256
		private DateTime lastRidMasterRefreshTime;

		// Token: 0x04001871 RID: 6257
		private readonly string serviceInstanceName;
	}
}
