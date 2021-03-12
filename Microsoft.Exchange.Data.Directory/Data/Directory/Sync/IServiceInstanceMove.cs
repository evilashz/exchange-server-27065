using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000992 RID: 2450
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[ServiceContract(Namespace = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11", ConfigurationName = "Microsoft.Exchange.Data.Directory.Sync.IServiceInstanceMove")]
	public interface IServiceInstanceMove
	{
		// Token: 0x06007174 RID: 29044
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/StartServiceInstanceMoveTaskArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/StartServiceInstanceMoveTask", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/StartServiceInstanceMoveTaskResponse")]
		[ServiceKnownType(typeof(DirectoryReference))]
		[ServiceKnownType(typeof(DirectoryProperty))]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/StartServiceInstanceMoveTaskBindingRedirectionFaultFault", Name = "BindingRedirectionFault", Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
		[XmlSerializerFormat]
		StartServiceInstanceMoveTaskResponse StartServiceInstanceMoveTask(StartServiceInstanceMoveTaskRequest request);

		// Token: 0x06007175 RID: 29045
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/StartServiceInstanceMoveTask", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/StartServiceInstanceMoveTaskResponse")]
		Task<StartServiceInstanceMoveTaskResponse> StartServiceInstanceMoveTaskAsync(StartServiceInstanceMoveTaskRequest request);

		// Token: 0x06007176 RID: 29046
		[ServiceKnownType(typeof(DirectoryProperty))]
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/GetServiceInstanceMoveTaskStatusBindingRedirectionFaultFault", Name = "BindingRedirectionFault", Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
		[ServiceKnownType(typeof(DirectoryReference))]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/GetServiceInstanceMoveTaskStatusArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/GetServiceInstanceMoveTaskStatus", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/GetServiceInstanceMoveTaskStatusResponse")]
		[XmlSerializerFormat]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		GetServiceInstanceMoveTaskStatusResponse GetServiceInstanceMoveTaskStatus(GetServiceInstanceMoveTaskStatusRequest request);

		// Token: 0x06007177 RID: 29047
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/GetServiceInstanceMoveTaskStatus", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/GetServiceInstanceMoveTaskStatusResponse")]
		Task<GetServiceInstanceMoveTaskStatusResponse> GetServiceInstanceMoveTaskStatusAsync(GetServiceInstanceMoveTaskStatusRequest request);

		// Token: 0x06007178 RID: 29048
		[XmlSerializerFormat]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/FinalizeServiceInstanceMoveTask", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/FinalizeServiceInstanceMoveTaskResponse")]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/FinalizeServiceInstanceMoveTaskArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/FinalizeServiceInstanceMoveTaskBindingRedirectionFaultFault", Name = "BindingRedirectionFault", Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		[ServiceKnownType(typeof(DirectoryReference))]
		[ServiceKnownType(typeof(DirectoryProperty))]
		FinalizeServiceInstanceMoveTaskResponse FinalizeServiceInstanceMoveTask(FinalizeServiceInstanceMoveTaskRequest request);

		// Token: 0x06007179 RID: 29049
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/FinalizeServiceInstanceMoveTask", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/FinalizeServiceInstanceMoveTaskResponse")]
		Task<FinalizeServiceInstanceMoveTaskResponse> FinalizeServiceInstanceMoveTaskAsync(FinalizeServiceInstanceMoveTaskRequest request);

		// Token: 0x0600717A RID: 29050
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/DeleteServiceInstanceMoveTaskBindingRedirectionFaultFault", Name = "BindingRedirectionFault", Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/DeleteServiceInstanceMoveTask", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/DeleteServiceInstanceMoveTaskResponse")]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/DeleteServiceInstanceMoveTaskArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		[ServiceKnownType(typeof(DirectoryProperty))]
		[XmlSerializerFormat]
		[ServiceKnownType(typeof(DirectoryReference))]
		DeleteServiceInstanceMoveTaskResponse DeleteServiceInstanceMoveTask(DeleteServiceInstanceMoveTaskRequest request);

		// Token: 0x0600717B RID: 29051
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/DeleteServiceInstanceMoveTask", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/DeleteServiceInstanceMoveTaskResponse")]
		Task<DeleteServiceInstanceMoveTaskResponse> DeleteServiceInstanceMoveTaskAsync(DeleteServiceInstanceMoveTaskRequest request);

		// Token: 0x0600717C RID: 29052
		[XmlSerializerFormat]
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/RecoverServiceInstanceMoveTask", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/RecoverServiceInstanceMoveTaskResponse")]
		[FaultContract(typeof(ArgumentException), Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/RecoverServiceInstanceMoveTaskArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/RecoverServiceInstanceMoveTaskBindingRedirectionFaultFault", Name = "BindingRedirectionFault", Namespace = "http://schemas.microsoft.com/online/directoryservices/sync/2008/11")]
		[ServiceKnownType(typeof(CompanyDomainValue))]
		[ServiceKnownType(typeof(DirectoryReference))]
		[ServiceKnownType(typeof(DirectoryProperty))]
		RecoverServiceInstanceMoveTaskResponse RecoverServiceInstanceMoveTask(RecoverServiceInstanceMoveTaskRequest request);

		// Token: 0x0600717D RID: 29053
		[OperationContract(Action = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/RecoverServiceInstanceMoveTask", ReplyAction = "http://schemas.microsoft.com/online/directoryservices/serviceinstancemove/2008/11/IServiceInstanceMove/RecoverServiceInstanceMoveTaskResponse")]
		Task<RecoverServiceInstanceMoveTaskResponse> RecoverServiceInstanceMoveTaskAsync(RecoverServiceInstanceMoveTaskRequest request);
	}
}
