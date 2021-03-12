using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000982 RID: 2434
	[ServiceContract(Namespace = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11", ConfigurationName = "Microsoft.Exchange.Data.Directory.Sync.IFederatedServiceOnboarding")]
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	public interface IFederatedServiceOnboarding
	{
		// Token: 0x06007133 RID: 28979
		[ServiceKnownType(typeof(DirectoryReference))]
		[XmlSerializerFormat]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/GetServiceInstanceMapArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[ServiceKnownType(typeof(DirectoryProperty))]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/GetServiceInstanceMap", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/GetServiceInstanceMapResponse")]
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/GetServiceInstanceMapBindingRedirectionFaultFault", Name = "BindingRedirectionFault", Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
		GetServiceInstanceMapResponse GetServiceInstanceMap(GetServiceInstanceMapRequest request);

		// Token: 0x06007134 RID: 28980
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/GetServiceInstanceMap", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/GetServiceInstanceMapResponse")]
		Task<GetServiceInstanceMapResponse> GetServiceInstanceMapAsync(GetServiceInstanceMapRequest request);

		// Token: 0x06007135 RID: 28981
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/SetServiceInstanceMapArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[XmlSerializerFormat]
		[ServiceKnownType(typeof(DirectoryReference))]
		[ServiceKnownType(typeof(DirectoryProperty))]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/SetServiceInstanceMap", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/SetServiceInstanceMapResponse")]
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/SetServiceInstanceMapBindingRedirectionFaultFault", Name = "BindingRedirectionFault", Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
		SetServiceInstanceMapResponse SetServiceInstanceMap(SetServiceInstanceMapRequest request);

		// Token: 0x06007136 RID: 28982
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/SetServiceInstanceMap", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/SetServiceInstanceMapResponse")]
		Task<SetServiceInstanceMapResponse> SetServiceInstanceMapAsync(SetServiceInstanceMapRequest request);

		// Token: 0x06007137 RID: 28983
		[ServiceKnownType(typeof(DirectoryProperty))]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		[ServiceKnownType(typeof(DirectoryReference))]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/GetServiceInstanceInfo", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/GetServiceInstanceInfoResponse")]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/GetServiceInstanceInfoArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/GetServiceInstanceInfoBindingRedirectionFaultFault", Name = "BindingRedirectionFault", Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
		[XmlSerializerFormat]
		GetServiceInstanceInfoResponse GetServiceInstanceInfo(GetServiceInstanceInfoRequest request);

		// Token: 0x06007138 RID: 28984
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/GetServiceInstanceInfo", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/GetServiceInstanceInfoResponse")]
		Task<GetServiceInstanceInfoResponse> GetServiceInstanceInfoAsync(GetServiceInstanceInfoRequest request);

		// Token: 0x06007139 RID: 28985
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/SetServiceInstanceInfoBindingRedirectionFaultFault", Name = "BindingRedirectionFault", Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		[ServiceKnownType(typeof(DirectoryReference))]
		[ServiceKnownType(typeof(DirectoryProperty))]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/SetServiceInstanceInfoArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/SetServiceInstanceInfo", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/SetServiceInstanceInfoResponse")]
		[XmlSerializerFormat]
		SetServiceInstanceInfoResponse SetServiceInstanceInfo(SetServiceInstanceInfoRequest request);

		// Token: 0x0600713A RID: 28986
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/SetServiceInstanceInfo", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/SetServiceInstanceInfoResponse")]
		Task<SetServiceInstanceInfoResponse> SetServiceInstanceInfoAsync(SetServiceInstanceInfoRequest request);

		// Token: 0x0600713B RID: 28987
		[XmlSerializerFormat]
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/QueueEduFaultinBindingRedirectionFaultFault", Name = "BindingRedirectionFault", Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
		[ServiceKnownType(typeof(DirectoryReference))]
		[ServiceKnownType(typeof(DirectoryProperty))]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/QueueEduFaultin", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/QueueEduFaultinResponse")]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/QueueEduFaultinArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		QueueEduFaultinResponse QueueEduFaultin(QueueEduFaultinRequest request);

		// Token: 0x0600713C RID: 28988
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/QueueEduFaultin", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/QueueEduFaultinResponse")]
		Task<QueueEduFaultinResponse> QueueEduFaultinAsync(QueueEduFaultinRequest request);

		// Token: 0x0600713D RID: 28989
		[ServiceKnownType(typeof(DirectoryReference))]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/GetEduFaultinStatus", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/GetEduFaultinStatusResponse")]
		[ServiceKnownType(typeof(DirectoryProperty))]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/GetEduFaultinStatusArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/GetEduFaultinStatusBindingRedirectionFaultFault", Name = "BindingRedirectionFault", Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
		[XmlSerializerFormat]
		GetEduFaultinStatusResponse GetEduFaultinStatus(GetEduFaultinStatusRequest request);

		// Token: 0x0600713E RID: 28990
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/GetEduFaultinStatus", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/federatedserviceonboarding/2008/11/IFederatedServiceOnboarding/GetEduFaultinStatusResponse")]
		Task<GetEduFaultinStatusResponse> GetEduFaultinStatusAsync(GetEduFaultinStatusRequest request);
	}
}
