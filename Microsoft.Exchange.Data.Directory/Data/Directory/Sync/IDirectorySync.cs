using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020008A2 RID: 2210
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[ServiceContract(Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11", ConfigurationName = "Microsoft.Exchange.Data.Directory.Sync.IDirectorySync")]
	public interface IDirectorySync
	{
		// Token: 0x06006DEC RID: 28140
		[XmlSerializerFormat]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/NewCookieArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/NewCookieBindingRedirectionFaultFault", Name = "BindingRedirectionFault")]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/NewCookie", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/NewCookieResponse")]
		NewCookieResponse NewCookie(NewCookieRequest request);

		// Token: 0x06006DED RID: 28141
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/NewCookie", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/NewCookieResponse")]
		Task<NewCookieResponse> NewCookieAsync(NewCookieRequest request);

		// Token: 0x06006DEE RID: 28142
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/NewCookie2BindingRedirectionFaultFault", Name = "BindingRedirectionFault")]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/NewCookie2", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/NewCookie2Response")]
		[XmlSerializerFormat]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/NewCookie2ArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		NewCookie2Response NewCookie2(NewCookie2Request request);

		// Token: 0x06006DEF RID: 28143
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/NewCookie2", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/NewCookie2Response")]
		Task<NewCookie2Response> NewCookie2Async(NewCookie2Request request);

		// Token: 0x06006DF0 RID: 28144
		[XmlSerializerFormat]
		[ServiceKnownType(typeof(DirectoryProperty))]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetChangesArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		[ServiceKnownType(typeof(DirectoryReference))]
		[FaultContract(typeof(SecretEncryptionFailureFault), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetChangesSecretEncryptionFailureFaultFault", Name = "SecretEncryptionFailureFault")]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetChanges", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetChangesResponse")]
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetChangesBindingRedirectionFaultFault", Name = "BindingRedirectionFault")]
		GetChangesResponse GetChanges(GetChangesRequest request);

		// Token: 0x06006DF1 RID: 28145
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetChanges", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetChangesResponse")]
		Task<GetChangesResponse> GetChangesAsync(GetChangesRequest request);

		// Token: 0x06006DF2 RID: 28146
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/PublishBindingRedirectionFaultFault", Name = "BindingRedirectionFault")]
		[ServiceKnownType(typeof(DirectoryProperty))]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/Publish", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/PublishResponse")]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/PublishArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[XmlSerializerFormat]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		[ServiceKnownType(typeof(DirectoryReference))]
		PublishResponse Publish(PublishRequest request);

		// Token: 0x06006DF3 RID: 28147
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/Publish", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/PublishResponse")]
		Task<PublishResponse> PublishAsync(PublishRequest request);

		// Token: 0x06006DF4 RID: 28148
		[XmlSerializerFormat]
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetContextBindingRedirectionFaultFault", Name = "BindingRedirectionFault")]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetContext", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetContextResponse")]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetContextArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		[ServiceKnownType(typeof(DirectoryProperty))]
		[FaultContract(typeof(SecretEncryptionFailureFault), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetContextSecretEncryptionFailureFaultFault", Name = "SecretEncryptionFailureFault")]
		[ServiceKnownType(typeof(DirectoryReference))]
		GetContextResponse GetContext(GetContextRequest request);

		// Token: 0x06006DF5 RID: 28149
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetContext", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetContextResponse")]
		Task<GetContextResponse> GetContextAsync(GetContextRequest request);

		// Token: 0x06006DF6 RID: 28150
		[ServiceKnownType(typeof(CompanyDomainValue))]
		[ServiceKnownType(typeof(DirectoryProperty))]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetDirectoryObjects", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetDirectoryObjectsResponse")]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetDirectoryObjectsArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetDirectoryObjectsBindingRedirectionFaultFault", Name = "BindingRedirectionFault")]
		[FaultContract(typeof(SecretEncryptionFailureFault), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetDirectoryObjectsSecretEncryptionFailureFaultFault", Name = "SecretEncryptionFailureFault")]
		[XmlSerializerFormat]
		[ServiceKnownType(typeof(DirectoryReference))]
		GetDirectoryObjectsResponse GetDirectoryObjects(GetDirectoryObjectsRequest request);

		// Token: 0x06006DF7 RID: 28151
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetDirectoryObjects", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetDirectoryObjectsResponse")]
		Task<GetDirectoryObjectsResponse> GetDirectoryObjectsAsync(GetDirectoryObjectsRequest request);

		// Token: 0x06006DF8 RID: 28152
		[FaultContract(typeof(SecretEncryptionFailureFault), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/MergeCookiesSecretEncryptionFailureFaultFault", Name = "SecretEncryptionFailureFault")]
		[XmlSerializerFormat]
		[ServiceKnownType(typeof(DirectoryReference))]
		[ServiceKnownType(typeof(DirectoryProperty))]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/MergeCookies", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/MergeCookiesResponse")]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/MergeCookiesArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/MergeCookiesBindingRedirectionFaultFault", Name = "BindingRedirectionFault")]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		MergeCookiesResponse MergeCookies(MergeCookiesRequest request);

		// Token: 0x06006DF9 RID: 28153
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/MergeCookies", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/MergeCookiesResponse")]
		Task<MergeCookiesResponse> MergeCookiesAsync(MergeCookiesRequest request);

		// Token: 0x06006DFA RID: 28154
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetDirSyncDrainageStatusBindingRedirectionFaultFault", Name = "BindingRedirectionFault")]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetDirSyncDrainageStatus", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetDirSyncDrainageStatusResponse")]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetDirSyncDrainageStatusArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		[ServiceKnownType(typeof(DirectoryProperty))]
		[XmlSerializerFormat]
		[ServiceKnownType(typeof(DirectoryReference))]
		GetDirSyncDrainageStatusResponse GetDirSyncDrainageStatus(GetDirSyncDrainageStatusRequest request);

		// Token: 0x06006DFB RID: 28155
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetDirSyncDrainageStatus", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetDirSyncDrainageStatusResponse")]
		Task<GetDirSyncDrainageStatusResponse> GetDirSyncDrainageStatusAsync(GetDirSyncDrainageStatusRequest request);

		// Token: 0x06006DFC RID: 28156
		[ServiceKnownType(typeof(DirectoryProperty))]
		[ServiceKnownType(typeof(DirectoryReference))]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/UpdateCookieArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/UpdateCookie", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/UpdateCookieResponse")]
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/UpdateCookieBindingRedirectionFaultFault", Name = "BindingRedirectionFault")]
		[XmlSerializerFormat]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		UpdateCookieResponse UpdateCookie(UpdateCookieRequest request);

		// Token: 0x06006DFD RID: 28157
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/UpdateCookie", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/UpdateCookieResponse")]
		Task<UpdateCookieResponse> UpdateCookieAsync(UpdateCookieRequest request);

		// Token: 0x06006DFE RID: 28158
		[ServiceKnownType(typeof(CompanyDomainValue))]
		[XmlSerializerFormat]
		[ServiceKnownType(typeof(DirectoryReference))]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetCookieUpdateStatus", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetCookieUpdateStatusResponse")]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetCookieUpdateStatusArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[ServiceKnownType(typeof(DirectoryProperty))]
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetCookieUpdateStatusBindingRedirectionFaultFault", Name = "BindingRedirectionFault")]
		GetCookieUpdateStatusResponse GetCookieUpdateStatus(GetCookieUpdateStatusRequest request);

		// Token: 0x06006DFF RID: 28159
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetCookieUpdateStatus", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/GetCookieUpdateStatusResponse")]
		Task<GetCookieUpdateStatusResponse> GetCookieUpdateStatusAsync(GetCookieUpdateStatusRequest request);

		// Token: 0x06006E00 RID: 28160
		[ServiceKnownType(typeof(DirectoryReference))]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		[ServiceKnownType(typeof(DirectoryProperty))]
		[XmlSerializerFormat]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/FilterAndGetContextRecoveryInfo", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/FilterAndGetContextRecoveryInfoResponse")]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/FilterAndGetContextRecoveryInfoArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/FilterAndGetContextRecoveryInfoBindingRedirectionFaultFault", Name = "BindingRedirectionFault")]
		FilterAndGetContextRecoveryInfoResponse FilterAndGetContextRecoveryInfo(FilterAndGetContextRecoveryInfoRequest request);

		// Token: 0x06006E01 RID: 28161
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/FilterAndGetContextRecoveryInfo", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/FilterAndGetContextRecoveryInfoResponse")]
		Task<FilterAndGetContextRecoveryInfoResponse> FilterAndGetContextRecoveryInfoAsync(FilterAndGetContextRecoveryInfoRequest request);

		// Token: 0x06006E02 RID: 28162
		[XmlSerializerFormat]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/EstimateBacklogArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/EstimateBacklogBindingRedirectionFaultFault", Name = "BindingRedirectionFault")]
		[ServiceKnownType(typeof(DirectoryReference))]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/EstimateBacklog", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/EstimateBacklogResponse")]
		[ServiceKnownType(typeof(DirectoryProperty))]
		EstimateBacklogResponse EstimateBacklog(EstimateBacklogRequest request);

		// Token: 0x06006E03 RID: 28163
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/EstimateBacklog", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11/IDirectorySync/EstimateBacklogResponse")]
		Task<EstimateBacklogResponse> EstimateBacklogAsync(EstimateBacklogRequest request);
	}
}
