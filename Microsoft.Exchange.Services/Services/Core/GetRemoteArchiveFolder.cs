using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.SoapWebClient.EWS;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000323 RID: 803
	internal sealed class GetRemoteArchiveFolder : SingleStepServiceCommand<GetFolderRequest, GetFolderResponse>
	{
		// Token: 0x060016B6 RID: 5814 RVA: 0x00078380 File Offset: 0x00076580
		public GetRemoteArchiveFolder(CallContext callContext, GetFolderRequest request) : base(callContext, request)
		{
			ServiceCommandBase.ThrowIfNull(request, "remoteArchiveRequest", "GetRemoteArchiveFolder::GetRemoteArchiveFolder");
			this.folderIds = request.Ids;
			this.responseShape = Global.ResponseShapeResolver.GetResponseShape<FolderResponseShape>(request.ShapeName, request.FolderShape, null);
			this.archiveService = ((IRemoteArchiveRequest)request).ArchiveService;
			ServiceCommandBase.ThrowIfNullOrEmpty<BaseFolderId>(this.folderIds, "this.folderIds", "GetRemoteArchiveFolder::GetRemoteArchiveFolder");
			ServiceCommandBase.ThrowIfNull(this.responseShape, "this.responseShape", "GetRemoteArchiveFolder::GetRemoteArchiveFolder");
			ServiceCommandBase.ThrowIfNull(this.archiveService, "this.archiveService", "GetRemoteArchiveFolder::GetRemoteArchiveFolder");
			this.archiveService.Timeout = EwsClientHelper.RemoteEwsClientTimeout;
			this.GetFolderFunc = new Func<GetFolderType, GetFolderResponseType>(this.archiveService.GetFolder);
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060016B7 RID: 5815 RVA: 0x00078442 File Offset: 0x00076642
		// (set) Token: 0x060016B8 RID: 5816 RVA: 0x0007844A File Offset: 0x0007664A
		internal Func<GetFolderType, GetFolderResponseType> GetFolderFunc { get; set; }

		// Token: 0x060016B9 RID: 5817 RVA: 0x0007847C File Offset: 0x0007667C
		internal override ServiceResult<GetFolderResponse> Execute()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			GetFolderRequest getFolderRequest = new GetFolderRequest();
			getFolderRequest.Ids = this.folderIds;
			getFolderRequest.FolderShape = this.responseShape;
			GetFolderResponse getFolderResponse;
			try
			{
				GetFolderType getFolderType = EwsClientHelper.Convert<GetFolderRequest, GetFolderType>(getFolderRequest);
				Exception ex = null;
				GetFolderResponseType getFolderResponseType = null;
				bool flag = EwsClientHelper.ExecuteEwsCall(delegate
				{
					getFolderResponseType = this.GetFolderFunc(getFolderType);
				}, out ex);
				if (flag)
				{
					getFolderResponse = EwsClientHelper.Convert<GetFolderResponseType, GetFolderResponse>(getFolderResponseType);
				}
				else
				{
					ServiceError error = new ServiceError(CoreResources.IDs.ErrorGetRemoteArchiveFolderFailed, Microsoft.Exchange.Services.Core.Types.ResponseCodeType.ErrorInternalServerError, 0, ExchangeVersion.Exchange2012);
					ExTraceGlobals.SearchTracer.TraceError<string, string>((long)this.GetHashCode(), "[GetRemoteArchiveFolder::ExecuteRemoteArchiveGetFolder] Get folder against URL {0} failed with error: {1}.", this.archiveService.Url, ex.Message);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericError(RequestDetailsLogger.Current, "FailedGetRemoteArchiveFolder", ex.Message);
					getFolderResponse = new GetFolderResponse();
					getFolderResponse.AddResponse(getFolderResponse.CreateResponseMessage<Microsoft.Exchange.Services.Core.Types.BaseFolderType>(ServiceResultCode.Error, error, null));
				}
			}
			finally
			{
				stopwatch.Stop();
				RequestDetailsLogger.Current.AppendGenericInfo("GetRemoteArchiveFolderProcessingTime", stopwatch.ElapsedMilliseconds);
			}
			return new ServiceResult<GetFolderResponse>(getFolderResponse);
		}

		// Token: 0x060016BA RID: 5818 RVA: 0x000785A4 File Offset: 0x000767A4
		internal override IExchangeWebMethodResponse GetResponse()
		{
			return base.Result.Value;
		}

		// Token: 0x04000F50 RID: 3920
		private const string ProcessingTimeLogField = "GetRemoteArchiveFolderProcessingTime";

		// Token: 0x04000F51 RID: 3921
		private const string FailureMessageLogField = "FailedGetRemoteArchiveFolder";

		// Token: 0x04000F52 RID: 3922
		private BaseFolderId[] folderIds;

		// Token: 0x04000F53 RID: 3923
		private FolderResponseShape responseShape;

		// Token: 0x04000F54 RID: 3924
		private ExchangeServiceBinding archiveService;
	}
}
