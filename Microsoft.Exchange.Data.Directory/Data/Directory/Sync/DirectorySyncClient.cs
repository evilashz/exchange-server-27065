using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000981 RID: 2433
	[DebuggerStepThrough]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public class DirectorySyncClient : ClientBase<IDirectorySync>, IDirectorySync
	{
		// Token: 0x060070FE RID: 28926 RVA: 0x00177A5F File Offset: 0x00175C5F
		public DirectorySyncClient()
		{
		}

		// Token: 0x060070FF RID: 28927 RVA: 0x00177A67 File Offset: 0x00175C67
		public DirectorySyncClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x06007100 RID: 28928 RVA: 0x00177A70 File Offset: 0x00175C70
		public DirectorySyncClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06007101 RID: 28929 RVA: 0x00177A7A File Offset: 0x00175C7A
		public DirectorySyncClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x06007102 RID: 28930 RVA: 0x00177A84 File Offset: 0x00175C84
		public DirectorySyncClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x06007103 RID: 28931 RVA: 0x00177A8E File Offset: 0x00175C8E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		NewCookieResponse IDirectorySync.NewCookie(NewCookieRequest request)
		{
			return base.Channel.NewCookie(request);
		}

		// Token: 0x06007104 RID: 28932 RVA: 0x00177A9C File Offset: 0x00175C9C
		public byte[] NewCookie(string serviceInstance, SyncOptions options, string[] alwaysReturnProperties)
		{
			NewCookieResponse newCookieResponse = ((IDirectorySync)this).NewCookie(new NewCookieRequest
			{
				serviceInstance = serviceInstance,
				options = options,
				alwaysReturnProperties = alwaysReturnProperties
			});
			return newCookieResponse.NewCookieResult;
		}

		// Token: 0x06007105 RID: 28933 RVA: 0x00177AD2 File Offset: 0x00175CD2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<NewCookieResponse> IDirectorySync.NewCookieAsync(NewCookieRequest request)
		{
			return base.Channel.NewCookieAsync(request);
		}

		// Token: 0x06007106 RID: 28934 RVA: 0x00177AE0 File Offset: 0x00175CE0
		public Task<NewCookieResponse> NewCookieAsync(string serviceInstance, SyncOptions options, string[] alwaysReturnProperties)
		{
			return ((IDirectorySync)this).NewCookieAsync(new NewCookieRequest
			{
				serviceInstance = serviceInstance,
				options = options,
				alwaysReturnProperties = alwaysReturnProperties
			});
		}

		// Token: 0x06007107 RID: 28935 RVA: 0x00177B0F File Offset: 0x00175D0F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		NewCookie2Response IDirectorySync.NewCookie2(NewCookie2Request request)
		{
			return base.Channel.NewCookie2(request);
		}

		// Token: 0x06007108 RID: 28936 RVA: 0x00177B20 File Offset: 0x00175D20
		public byte[] NewCookie2(int schemaRevision, string serviceInstance, SyncOptions options, string[] objectClassesOfInterest, string[] propertiesOfInterest, string[] linkClassesOfInterest, string[] alwaysReturnProperties)
		{
			NewCookie2Response newCookie2Response = ((IDirectorySync)this).NewCookie2(new NewCookie2Request
			{
				schemaRevision = schemaRevision,
				serviceInstance = serviceInstance,
				options = options,
				objectClassesOfInterest = objectClassesOfInterest,
				propertiesOfInterest = propertiesOfInterest,
				linkClassesOfInterest = linkClassesOfInterest,
				alwaysReturnProperties = alwaysReturnProperties
			});
			return newCookie2Response.NewCookie2Result;
		}

		// Token: 0x06007109 RID: 28937 RVA: 0x00177B76 File Offset: 0x00175D76
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<NewCookie2Response> IDirectorySync.NewCookie2Async(NewCookie2Request request)
		{
			return base.Channel.NewCookie2Async(request);
		}

		// Token: 0x0600710A RID: 28938 RVA: 0x00177B84 File Offset: 0x00175D84
		public Task<NewCookie2Response> NewCookie2Async(int schemaRevision, string serviceInstance, SyncOptions options, string[] objectClassesOfInterest, string[] propertiesOfInterest, string[] linkClassesOfInterest, string[] alwaysReturnProperties)
		{
			return ((IDirectorySync)this).NewCookie2Async(new NewCookie2Request
			{
				schemaRevision = schemaRevision,
				serviceInstance = serviceInstance,
				options = options,
				objectClassesOfInterest = objectClassesOfInterest,
				propertiesOfInterest = propertiesOfInterest,
				linkClassesOfInterest = linkClassesOfInterest,
				alwaysReturnProperties = alwaysReturnProperties
			});
		}

		// Token: 0x0600710B RID: 28939 RVA: 0x00177BD3 File Offset: 0x00175DD3
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		GetChangesResponse IDirectorySync.GetChanges(GetChangesRequest request)
		{
			return base.Channel.GetChanges(request);
		}

		// Token: 0x0600710C RID: 28940 RVA: 0x00177BE4 File Offset: 0x00175DE4
		public DirectoryChanges GetChanges(byte[] lastCookie)
		{
			GetChangesResponse changes = ((IDirectorySync)this).GetChanges(new GetChangesRequest
			{
				lastCookie = lastCookie
			});
			return changes.GetChangesResult;
		}

		// Token: 0x0600710D RID: 28941 RVA: 0x00177C0C File Offset: 0x00175E0C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<GetChangesResponse> IDirectorySync.GetChangesAsync(GetChangesRequest request)
		{
			return base.Channel.GetChangesAsync(request);
		}

		// Token: 0x0600710E RID: 28942 RVA: 0x00177C1C File Offset: 0x00175E1C
		public Task<GetChangesResponse> GetChangesAsync(byte[] lastCookie)
		{
			return ((IDirectorySync)this).GetChangesAsync(new GetChangesRequest
			{
				lastCookie = lastCookie
			});
		}

		// Token: 0x0600710F RID: 28943 RVA: 0x00177C3D File Offset: 0x00175E3D
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		PublishResponse IDirectorySync.Publish(PublishRequest request)
		{
			return base.Channel.Publish(request);
		}

		// Token: 0x06007110 RID: 28944 RVA: 0x00177C4C File Offset: 0x00175E4C
		public UpdateResultCode[] Publish(ServicePublication[] publications)
		{
			PublishResponse publishResponse = ((IDirectorySync)this).Publish(new PublishRequest
			{
				publications = publications
			});
			return publishResponse.PublishResult;
		}

		// Token: 0x06007111 RID: 28945 RVA: 0x00177C74 File Offset: 0x00175E74
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<PublishResponse> IDirectorySync.PublishAsync(PublishRequest request)
		{
			return base.Channel.PublishAsync(request);
		}

		// Token: 0x06007112 RID: 28946 RVA: 0x00177C84 File Offset: 0x00175E84
		public Task<PublishResponse> PublishAsync(ServicePublication[] publications)
		{
			return ((IDirectorySync)this).PublishAsync(new PublishRequest
			{
				publications = publications
			});
		}

		// Token: 0x06007113 RID: 28947 RVA: 0x00177CA5 File Offset: 0x00175EA5
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		GetContextResponse IDirectorySync.GetContext(GetContextRequest request)
		{
			return base.Channel.GetContext(request);
		}

		// Token: 0x06007114 RID: 28948 RVA: 0x00177CB4 File Offset: 0x00175EB4
		public DirectoryObjectsAndLinks GetContext(byte[] lastCookie, string contextId, byte[] lastPageToken)
		{
			GetContextResponse context = ((IDirectorySync)this).GetContext(new GetContextRequest
			{
				lastCookie = lastCookie,
				contextId = contextId,
				lastPageToken = lastPageToken
			});
			return context.GetContextResult;
		}

		// Token: 0x06007115 RID: 28949 RVA: 0x00177CEA File Offset: 0x00175EEA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<GetContextResponse> IDirectorySync.GetContextAsync(GetContextRequest request)
		{
			return base.Channel.GetContextAsync(request);
		}

		// Token: 0x06007116 RID: 28950 RVA: 0x00177CF8 File Offset: 0x00175EF8
		public Task<GetContextResponse> GetContextAsync(byte[] lastCookie, string contextId, byte[] lastPageToken)
		{
			return ((IDirectorySync)this).GetContextAsync(new GetContextRequest
			{
				lastCookie = lastCookie,
				contextId = contextId,
				lastPageToken = lastPageToken
			});
		}

		// Token: 0x06007117 RID: 28951 RVA: 0x00177D27 File Offset: 0x00175F27
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		GetDirectoryObjectsResponse IDirectorySync.GetDirectoryObjects(GetDirectoryObjectsRequest request)
		{
			return base.Channel.GetDirectoryObjects(request);
		}

		// Token: 0x06007118 RID: 28952 RVA: 0x00177D38 File Offset: 0x00175F38
		public DirectoryObjectsAndLinks GetDirectoryObjects(byte[] lastGetChangesCookieOrGetContextPageToken, DirectoryObjectIdentity[] objectIdentities, GetDirectoryObjectsOptions? options, byte[] lastPageToken)
		{
			GetDirectoryObjectsResponse directoryObjects = ((IDirectorySync)this).GetDirectoryObjects(new GetDirectoryObjectsRequest
			{
				lastGetChangesCookieOrGetContextPageToken = lastGetChangesCookieOrGetContextPageToken,
				objectIdentities = objectIdentities,
				options = options,
				lastPageToken = lastPageToken
			});
			return directoryObjects.GetDirectoryObjectsResult;
		}

		// Token: 0x06007119 RID: 28953 RVA: 0x00177D76 File Offset: 0x00175F76
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<GetDirectoryObjectsResponse> IDirectorySync.GetDirectoryObjectsAsync(GetDirectoryObjectsRequest request)
		{
			return base.Channel.GetDirectoryObjectsAsync(request);
		}

		// Token: 0x0600711A RID: 28954 RVA: 0x00177D84 File Offset: 0x00175F84
		public Task<GetDirectoryObjectsResponse> GetDirectoryObjectsAsync(byte[] lastGetChangesCookieOrGetContextPageToken, DirectoryObjectIdentity[] objectIdentities, GetDirectoryObjectsOptions? options, byte[] lastPageToken)
		{
			return ((IDirectorySync)this).GetDirectoryObjectsAsync(new GetDirectoryObjectsRequest
			{
				lastGetChangesCookieOrGetContextPageToken = lastGetChangesCookieOrGetContextPageToken,
				objectIdentities = objectIdentities,
				options = options,
				lastPageToken = lastPageToken
			});
		}

		// Token: 0x0600711B RID: 28955 RVA: 0x00177DBB File Offset: 0x00175FBB
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		MergeCookiesResponse IDirectorySync.MergeCookies(MergeCookiesRequest request)
		{
			return base.Channel.MergeCookies(request);
		}

		// Token: 0x0600711C RID: 28956 RVA: 0x00177DCC File Offset: 0x00175FCC
		public DirectoryChanges MergeCookies(byte[] lastGetChangesCookie, byte[] lastGetContextPageToken, byte[] lastMergeCookie)
		{
			MergeCookiesResponse mergeCookiesResponse = ((IDirectorySync)this).MergeCookies(new MergeCookiesRequest
			{
				lastGetChangesCookie = lastGetChangesCookie,
				lastGetContextPageToken = lastGetContextPageToken,
				lastMergeCookie = lastMergeCookie
			});
			return mergeCookiesResponse.MergeCookiesResult;
		}

		// Token: 0x0600711D RID: 28957 RVA: 0x00177E02 File Offset: 0x00176002
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<MergeCookiesResponse> IDirectorySync.MergeCookiesAsync(MergeCookiesRequest request)
		{
			return base.Channel.MergeCookiesAsync(request);
		}

		// Token: 0x0600711E RID: 28958 RVA: 0x00177E10 File Offset: 0x00176010
		public Task<MergeCookiesResponse> MergeCookiesAsync(byte[] lastGetChangesCookie, byte[] lastGetContextPageToken, byte[] lastMergeCookie)
		{
			return ((IDirectorySync)this).MergeCookiesAsync(new MergeCookiesRequest
			{
				lastGetChangesCookie = lastGetChangesCookie,
				lastGetContextPageToken = lastGetContextPageToken,
				lastMergeCookie = lastMergeCookie
			});
		}

		// Token: 0x0600711F RID: 28959 RVA: 0x00177E3F File Offset: 0x0017603F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		GetDirSyncDrainageStatusResponse IDirectorySync.GetDirSyncDrainageStatus(GetDirSyncDrainageStatusRequest request)
		{
			return base.Channel.GetDirSyncDrainageStatus(request);
		}

		// Token: 0x06007120 RID: 28960 RVA: 0x00177E50 File Offset: 0x00176050
		public DirSyncDrainageCode[] GetDirSyncDrainageStatus(ContextDirSyncStatus[] contextDirSyncStatusList, byte[] getChangesCookie)
		{
			GetDirSyncDrainageStatusResponse dirSyncDrainageStatus = ((IDirectorySync)this).GetDirSyncDrainageStatus(new GetDirSyncDrainageStatusRequest
			{
				contextDirSyncStatusList = contextDirSyncStatusList,
				getChangesCookie = getChangesCookie
			});
			return dirSyncDrainageStatus.GetDirSyncDrainageStatusResult;
		}

		// Token: 0x06007121 RID: 28961 RVA: 0x00177E7F File Offset: 0x0017607F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<GetDirSyncDrainageStatusResponse> IDirectorySync.GetDirSyncDrainageStatusAsync(GetDirSyncDrainageStatusRequest request)
		{
			return base.Channel.GetDirSyncDrainageStatusAsync(request);
		}

		// Token: 0x06007122 RID: 28962 RVA: 0x00177E90 File Offset: 0x00176090
		public Task<GetDirSyncDrainageStatusResponse> GetDirSyncDrainageStatusAsync(ContextDirSyncStatus[] contextDirSyncStatusList, byte[] getChangesCookie)
		{
			return ((IDirectorySync)this).GetDirSyncDrainageStatusAsync(new GetDirSyncDrainageStatusRequest
			{
				contextDirSyncStatusList = contextDirSyncStatusList,
				getChangesCookie = getChangesCookie
			});
		}

		// Token: 0x06007123 RID: 28963 RVA: 0x00177EB8 File Offset: 0x001760B8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		UpdateCookieResponse IDirectorySync.UpdateCookie(UpdateCookieRequest request)
		{
			return base.Channel.UpdateCookie(request);
		}

		// Token: 0x06007124 RID: 28964 RVA: 0x00177EC8 File Offset: 0x001760C8
		public byte[] UpdateCookie(byte[] getChangesCookie, int? schemaRevision, SyncOptions? options, string[] objectClassesOfInterest, string[] propertiesOfInterest, string[] linkClassesOfInterest, string[] alwaysReturnProperties)
		{
			UpdateCookieResponse updateCookieResponse = ((IDirectorySync)this).UpdateCookie(new UpdateCookieRequest
			{
				getChangesCookie = getChangesCookie,
				schemaRevision = schemaRevision,
				options = options,
				objectClassesOfInterest = objectClassesOfInterest,
				propertiesOfInterest = propertiesOfInterest,
				linkClassesOfInterest = linkClassesOfInterest,
				alwaysReturnProperties = alwaysReturnProperties
			});
			return updateCookieResponse.UpdateCookieResult;
		}

		// Token: 0x06007125 RID: 28965 RVA: 0x00177F1E File Offset: 0x0017611E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<UpdateCookieResponse> IDirectorySync.UpdateCookieAsync(UpdateCookieRequest request)
		{
			return base.Channel.UpdateCookieAsync(request);
		}

		// Token: 0x06007126 RID: 28966 RVA: 0x00177F2C File Offset: 0x0017612C
		public Task<UpdateCookieResponse> UpdateCookieAsync(byte[] getChangesCookie, int? schemaRevision, SyncOptions? options, string[] objectClassesOfInterest, string[] propertiesOfInterest, string[] linkClassesOfInterest, string[] alwaysReturnProperties)
		{
			return ((IDirectorySync)this).UpdateCookieAsync(new UpdateCookieRequest
			{
				getChangesCookie = getChangesCookie,
				schemaRevision = schemaRevision,
				options = options,
				objectClassesOfInterest = objectClassesOfInterest,
				propertiesOfInterest = propertiesOfInterest,
				linkClassesOfInterest = linkClassesOfInterest,
				alwaysReturnProperties = alwaysReturnProperties
			});
		}

		// Token: 0x06007127 RID: 28967 RVA: 0x00177F7B File Offset: 0x0017617B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		GetCookieUpdateStatusResponse IDirectorySync.GetCookieUpdateStatus(GetCookieUpdateStatusRequest request)
		{
			return base.Channel.GetCookieUpdateStatus(request);
		}

		// Token: 0x06007128 RID: 28968 RVA: 0x00177F8C File Offset: 0x0017618C
		public CookieUpdateStatus GetCookieUpdateStatus(byte[] getChangesCookie)
		{
			GetCookieUpdateStatusResponse cookieUpdateStatus = ((IDirectorySync)this).GetCookieUpdateStatus(new GetCookieUpdateStatusRequest
			{
				getChangesCookie = getChangesCookie
			});
			return cookieUpdateStatus.GetCookieUpdateStatusResult;
		}

		// Token: 0x06007129 RID: 28969 RVA: 0x00177FB4 File Offset: 0x001761B4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<GetCookieUpdateStatusResponse> IDirectorySync.GetCookieUpdateStatusAsync(GetCookieUpdateStatusRequest request)
		{
			return base.Channel.GetCookieUpdateStatusAsync(request);
		}

		// Token: 0x0600712A RID: 28970 RVA: 0x00177FC4 File Offset: 0x001761C4
		public Task<GetCookieUpdateStatusResponse> GetCookieUpdateStatusAsync(byte[] getChangesCookie)
		{
			return ((IDirectorySync)this).GetCookieUpdateStatusAsync(new GetCookieUpdateStatusRequest
			{
				getChangesCookie = getChangesCookie
			});
		}

		// Token: 0x0600712B RID: 28971 RVA: 0x00177FE5 File Offset: 0x001761E5
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		FilterAndGetContextRecoveryInfoResponse IDirectorySync.FilterAndGetContextRecoveryInfo(FilterAndGetContextRecoveryInfoRequest request)
		{
			return base.Channel.FilterAndGetContextRecoveryInfo(request);
		}

		// Token: 0x0600712C RID: 28972 RVA: 0x00177FF4 File Offset: 0x001761F4
		public ContextRecoveryInfo FilterAndGetContextRecoveryInfo(byte[] getChangesCookie, string contextId)
		{
			FilterAndGetContextRecoveryInfoResponse filterAndGetContextRecoveryInfoResponse = ((IDirectorySync)this).FilterAndGetContextRecoveryInfo(new FilterAndGetContextRecoveryInfoRequest
			{
				getChangesCookie = getChangesCookie,
				contextId = contextId
			});
			return filterAndGetContextRecoveryInfoResponse.FilterAndGetContextRecoveryInfoResult;
		}

		// Token: 0x0600712D RID: 28973 RVA: 0x00178023 File Offset: 0x00176223
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<FilterAndGetContextRecoveryInfoResponse> IDirectorySync.FilterAndGetContextRecoveryInfoAsync(FilterAndGetContextRecoveryInfoRequest request)
		{
			return base.Channel.FilterAndGetContextRecoveryInfoAsync(request);
		}

		// Token: 0x0600712E RID: 28974 RVA: 0x00178034 File Offset: 0x00176234
		public Task<FilterAndGetContextRecoveryInfoResponse> FilterAndGetContextRecoveryInfoAsync(byte[] getChangesCookie, string contextId)
		{
			return ((IDirectorySync)this).FilterAndGetContextRecoveryInfoAsync(new FilterAndGetContextRecoveryInfoRequest
			{
				getChangesCookie = getChangesCookie,
				contextId = contextId
			});
		}

		// Token: 0x0600712F RID: 28975 RVA: 0x0017805C File Offset: 0x0017625C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		EstimateBacklogResponse IDirectorySync.EstimateBacklog(EstimateBacklogRequest request)
		{
			return base.Channel.EstimateBacklog(request);
		}

		// Token: 0x06007130 RID: 28976 RVA: 0x0017806C File Offset: 0x0017626C
		public BacklogEstimateBatch EstimateBacklog(byte[] latestGetChangesCookie, byte[] lastPageToken)
		{
			EstimateBacklogResponse estimateBacklogResponse = ((IDirectorySync)this).EstimateBacklog(new EstimateBacklogRequest
			{
				latestGetChangesCookie = latestGetChangesCookie,
				lastPageToken = lastPageToken
			});
			return estimateBacklogResponse.EstimateBacklogResult;
		}

		// Token: 0x06007131 RID: 28977 RVA: 0x0017809B File Offset: 0x0017629B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		Task<EstimateBacklogResponse> IDirectorySync.EstimateBacklogAsync(EstimateBacklogRequest request)
		{
			return base.Channel.EstimateBacklogAsync(request);
		}

		// Token: 0x06007132 RID: 28978 RVA: 0x001780AC File Offset: 0x001762AC
		public Task<EstimateBacklogResponse> EstimateBacklogAsync(byte[] latestGetChangesCookie, byte[] lastPageToken)
		{
			return ((IDirectorySync)this).EstimateBacklogAsync(new EstimateBacklogRequest
			{
				latestGetChangesCookie = latestGetChangesCookie,
				lastPageToken = lastPageToken
			});
		}
	}
}
