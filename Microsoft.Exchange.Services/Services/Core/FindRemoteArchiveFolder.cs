using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002F4 RID: 756
	internal sealed class FindRemoteArchiveFolder : SingleStepServiceCommand<FindFolderRequest, FindFolderResponse>
	{
		// Token: 0x06001556 RID: 5462 RVA: 0x0006D3A4 File Offset: 0x0006B5A4
		public FindRemoteArchiveFolder(CallContext callContext, FindFolderRequest request) : base(callContext, request)
		{
			ServiceCommandBase.ThrowIfNull(request, "remoteArchiveRequest", "FindRemoteArchiveFolder::FindRemoteArchiveFolder");
			this.responseShape = Global.ResponseShapeResolver.GetResponseShape<FolderResponseShape>(request.ShapeName, request.FolderShape, null);
			this.paging = request.Paging;
			this.restriction = request.Restriction;
			this.parentFolderIds = request.ParentFolderIds;
			this.folderQueryTraversal = request.Traversal;
			this.returnParentFolder = request.ReturnParentFolder;
			this.archiveService = ((IRemoteArchiveRequest)request).ArchiveService;
			ServiceCommandBase.ThrowIfNullOrEmpty<BaseFolderId>(this.parentFolderIds, "this.parentFolderIds", "FindRemoteArchiveFolder::FindRemoteArchiveFolder");
			ServiceCommandBase.ThrowIfNull(this.responseShape, "this.responseShape", "FindRemoteArchiveFolder::FindRemoteArchiveFolder");
			ServiceCommandBase.ThrowIfNull(this.archiveService, "this.archiveService", "FindRemoteArchiveFolder::FindRemoteArchiveFolder");
			this.archiveService.Timeout = EwsClientHelper.RemoteEwsClientTimeout;
			this.FindFolderFunc = new Func<FindFolderType, FindFolderResponseType>(this.archiveService.FindFolder);
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x0006D496 File Offset: 0x0006B696
		// (set) Token: 0x06001558 RID: 5464 RVA: 0x0006D49E File Offset: 0x0006B69E
		internal Func<FindFolderType, FindFolderResponseType> FindFolderFunc { get; set; }

		// Token: 0x06001559 RID: 5465 RVA: 0x0006D4D0 File Offset: 0x0006B6D0
		internal override ServiceResult<FindFolderResponse> Execute()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			FindFolderRequest findFolderRequest = new FindFolderRequest();
			findFolderRequest.FolderShape = this.responseShape;
			findFolderRequest.ParentFolderIds = this.parentFolderIds;
			findFolderRequest.Traversal = this.folderQueryTraversal;
			findFolderRequest.Paging = this.paging;
			findFolderRequest.Restriction = this.restriction;
			findFolderRequest.ReturnParentFolder = this.returnParentFolder;
			FindFolderResponse findFolderResponse;
			try
			{
				FindFolderType findFolderType = EwsClientHelper.Convert<FindFolderRequest, FindFolderType>(findFolderRequest);
				Exception ex = null;
				FindFolderResponseType findFolderResponseType = null;
				bool flag = EwsClientHelper.ExecuteEwsCall(delegate
				{
					findFolderResponseType = this.FindFolderFunc(findFolderType);
				}, out ex);
				if (flag)
				{
					findFolderResponse = EwsClientHelper.Convert<FindFolderResponseType, FindFolderResponse>(findFolderResponseType);
				}
				else
				{
					ServiceError error = new ServiceError((CoreResources.IDs)4160418372U, Microsoft.Exchange.Services.Core.Types.ResponseCodeType.ErrorInternalServerError, 0, ExchangeVersion.Exchange2012);
					ExTraceGlobals.SearchTracer.TraceError<string, string>((long)this.GetHashCode(), "[FindRemoteArchiveFolder::ExecuteRemoteArchiveFindFolder] Find folder against URL {0} failed with error: {1}.", this.archiveService.Url, ex.Message);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericError(RequestDetailsLogger.Current, "FailedFindRemoteArchiveFolder", ex.Message);
					findFolderResponse = new FindFolderResponse();
					findFolderResponse.AddResponse(new FindFolderResponseMessage(ServiceResultCode.Error, error, null));
				}
			}
			finally
			{
				stopwatch.Stop();
				CallContext.Current.ProtocolLog.AppendGenericInfo("FindRemoteArchiveFolderProcessingTime", stopwatch.ElapsedMilliseconds);
			}
			return new ServiceResult<FindFolderResponse>(findFolderResponse);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0006D62C File Offset: 0x0006B82C
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return base.Result.Value;
		}

		// Token: 0x04000E7A RID: 3706
		private const string ProcessingTimeLogField = "FindRemoteArchiveFolderProcessingTime";

		// Token: 0x04000E7B RID: 3707
		private const string FailureMessageLogField = "FailedFindRemoteArchiveFolder";

		// Token: 0x04000E7C RID: 3708
		private FolderResponseShape responseShape;

		// Token: 0x04000E7D RID: 3709
		private Microsoft.Exchange.Services.Core.Search.BasePagingType paging;

		// Token: 0x04000E7E RID: 3710
		private Microsoft.Exchange.Services.Core.Types.RestrictionType restriction;

		// Token: 0x04000E7F RID: 3711
		private BaseFolderId[] parentFolderIds;

		// Token: 0x04000E80 RID: 3712
		private FolderQueryTraversal folderQueryTraversal;

		// Token: 0x04000E81 RID: 3713
		private readonly bool returnParentFolder;

		// Token: 0x04000E82 RID: 3714
		private ExchangeServiceBinding archiveService;
	}
}
