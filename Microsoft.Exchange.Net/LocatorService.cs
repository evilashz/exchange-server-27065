using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data;

// Token: 0x02000C4C RID: 3148
[ServiceContract(Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService", ConfigurationName = "LocatorService")]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public interface LocatorService
{
	// Token: 0x0600450A RID: 17674
	[OperationContract(Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindTenant", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindTenantResponse")]
	[FaultContract(typeof(LocatorServiceFault), Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindTenantLocatorServiceFaultFault", Name = "LocatorServiceFault", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	FindTenantResponse FindTenant(RequestIdentity identity, FindTenantRequest findTenantRequest);

	// Token: 0x0600450B RID: 17675
	[OperationContract(AsyncPattern = true, Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindTenant", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindTenantResponse")]
	IAsyncResult BeginFindTenant(RequestIdentity identity, FindTenantRequest findTenantRequest, AsyncCallback callback, object asyncState);

	// Token: 0x0600450C RID: 17676
	FindTenantResponse EndFindTenant(IAsyncResult result);

	// Token: 0x0600450D RID: 17677
	[OperationContract(Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindDomain", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindDomainResponse")]
	[FaultContract(typeof(LocatorServiceFault), Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindDomainLocatorServiceFaultFault", Name = "LocatorServiceFault", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	FindDomainResponse FindDomain(RequestIdentity identity, FindDomainRequest findDomainRequest);

	// Token: 0x0600450E RID: 17678
	[OperationContract(AsyncPattern = true, Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindDomain", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindDomainResponse")]
	IAsyncResult BeginFindDomain(RequestIdentity identity, FindDomainRequest findDomainRequest, AsyncCallback callback, object asyncState);

	// Token: 0x0600450F RID: 17679
	FindDomainResponse EndFindDomain(IAsyncResult result);

	// Token: 0x06004510 RID: 17680
	[OperationContract(Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/SaveTenant", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/SaveTenantResponse")]
	[FaultContract(typeof(LocatorServiceFault), Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/SaveTenantLocatorServiceFaultFault", Name = "LocatorServiceFault", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	SaveTenantResponse SaveTenant(RequestIdentity identity, SaveTenantRequest saveTenantRequest);

	// Token: 0x06004511 RID: 17681
	[OperationContract(AsyncPattern = true, Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/SaveTenant", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/SaveTenantResponse")]
	IAsyncResult BeginSaveTenant(RequestIdentity identity, SaveTenantRequest saveTenantRequest, AsyncCallback callback, object asyncState);

	// Token: 0x06004512 RID: 17682
	SaveTenantResponse EndSaveTenant(IAsyncResult result);

	// Token: 0x06004513 RID: 17683
	[FaultContract(typeof(LocatorServiceFault), Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/SaveDomainLocatorServiceFaultFault", Name = "LocatorServiceFault", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[OperationContract(Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/SaveDomain", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/SaveDomainResponse")]
	SaveDomainResponse SaveDomain(RequestIdentity identity, SaveDomainRequest saveDomainRequest);

	// Token: 0x06004514 RID: 17684
	[OperationContract(AsyncPattern = true, Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/SaveDomain", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/SaveDomainResponse")]
	IAsyncResult BeginSaveDomain(RequestIdentity identity, SaveDomainRequest saveDomainRequest, AsyncCallback callback, object asyncState);

	// Token: 0x06004515 RID: 17685
	SaveDomainResponse EndSaveDomain(IAsyncResult result);

	// Token: 0x06004516 RID: 17686
	[OperationContract(Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/DeleteTenant", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/DeleteTenantResponse")]
	[FaultContract(typeof(LocatorServiceFault), Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/DeleteTenantLocatorServiceFaultFault", Name = "LocatorServiceFault", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	DeleteTenantResponse DeleteTenant(RequestIdentity identity, DeleteTenantRequest deleteTenantRequest);

	// Token: 0x06004517 RID: 17687
	[OperationContract(AsyncPattern = true, Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/DeleteTenant", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/DeleteTenantResponse")]
	IAsyncResult BeginDeleteTenant(RequestIdentity identity, DeleteTenantRequest deleteTenantRequest, AsyncCallback callback, object asyncState);

	// Token: 0x06004518 RID: 17688
	DeleteTenantResponse EndDeleteTenant(IAsyncResult result);

	// Token: 0x06004519 RID: 17689
	[FaultContract(typeof(LocatorServiceFault), Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/DeleteDomainLocatorServiceFaultFault", Name = "LocatorServiceFault", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[OperationContract(Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/DeleteDomain", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/DeleteDomainResponse")]
	DeleteDomainResponse DeleteDomain(RequestIdentity identity, DeleteDomainRequest deleteDomainRequest);

	// Token: 0x0600451A RID: 17690
	[OperationContract(AsyncPattern = true, Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/DeleteDomain", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/DeleteDomainResponse")]
	IAsyncResult BeginDeleteDomain(RequestIdentity identity, DeleteDomainRequest deleteDomainRequest, AsyncCallback callback, object asyncState);

	// Token: 0x0600451B RID: 17691
	DeleteDomainResponse EndDeleteDomain(IAsyncResult result);

	// Token: 0x0600451C RID: 17692
	[OperationContract(Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindDomains", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindDomainsResponse")]
	[FaultContract(typeof(LocatorServiceFault), Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindDomainsLocatorServiceFaultFault", Name = "LocatorServiceFault", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	FindDomainsResponse FindDomains(RequestIdentity identity, FindDomainsRequest findDomainsRequest);

	// Token: 0x0600451D RID: 17693
	[OperationContract(AsyncPattern = true, Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindDomains", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindDomainsResponse")]
	IAsyncResult BeginFindDomains(RequestIdentity identity, FindDomainsRequest findDomainsRequest, AsyncCallback callback, object asyncState);

	// Token: 0x0600451E RID: 17694
	FindDomainsResponse EndFindDomains(IAsyncResult result);

	// Token: 0x0600451F RID: 17695
	[OperationContract(Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/SaveUser", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/SaveUserResponse")]
	[FaultContract(typeof(LocatorServiceFault), Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/SaveUserLocatorServiceFaultFault", Name = "LocatorServiceFault", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	SaveUserResponse SaveUser(RequestIdentity identity, SaveUserRequest saveUserRequest);

	// Token: 0x06004520 RID: 17696
	[OperationContract(AsyncPattern = true, Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/SaveUser", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/SaveUserResponse")]
	IAsyncResult BeginSaveUser(RequestIdentity identity, SaveUserRequest saveUserRequest, AsyncCallback callback, object asyncState);

	// Token: 0x06004521 RID: 17697
	SaveUserResponse EndSaveUser(IAsyncResult result);

	// Token: 0x06004522 RID: 17698
	[FaultContract(typeof(LocatorServiceFault), Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindUserLocatorServiceFaultFault", Name = "LocatorServiceFault", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[OperationContract(Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindUser", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindUserResponse")]
	FindUserResponse FindUser(RequestIdentity identity, FindUserRequest findUserRequest);

	// Token: 0x06004523 RID: 17699
	[OperationContract(AsyncPattern = true, Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindUser", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindUserResponse")]
	IAsyncResult BeginFindUser(RequestIdentity identity, FindUserRequest findUserRequest, AsyncCallback callback, object asyncState);

	// Token: 0x06004524 RID: 17700
	FindUserResponse EndFindUser(IAsyncResult result);

	// Token: 0x06004525 RID: 17701
	[OperationContract(Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/DeleteUser", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/DeleteUserResponse")]
	[FaultContract(typeof(LocatorServiceFault), Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/DeleteUserLocatorServiceFaultFault", Name = "LocatorServiceFault", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	DeleteUserResponse DeleteUser(RequestIdentity identity, DeleteUserRequest deleteUserRequest);

	// Token: 0x06004526 RID: 17702
	[OperationContract(AsyncPattern = true, Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/DeleteUser", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/DeleteUserResponse")]
	IAsyncResult BeginDeleteUser(RequestIdentity identity, DeleteUserRequest deleteUserRequest, AsyncCallback callback, object asyncState);

	// Token: 0x06004527 RID: 17703
	DeleteUserResponse EndDeleteUser(IAsyncResult result);

	// Token: 0x06004528 RID: 17704
	[FaultContract(typeof(LocatorServiceFault), Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindTenantFromPrimaryLocatorServiceFaultFault", Name = "LocatorServiceFault", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[OperationContract(Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindTenantFromPrimary", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindTenantFromPrimaryResponse")]
	FindTenantResponse[] FindTenantFromPrimary(RequestIdentity identity, FindTenantRequest findTenantRequest, int? copiesToRead);

	// Token: 0x06004529 RID: 17705
	[OperationContract(AsyncPattern = true, Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindTenantFromPrimary", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/FindTenantFromPrimaryResponse")]
	IAsyncResult BeginFindTenantFromPrimary(RequestIdentity identity, FindTenantRequest findTenantRequest, int? copiesToRead, AsyncCallback callback, object asyncState);

	// Token: 0x0600452A RID: 17706
	FindTenantResponse[] EndFindTenantFromPrimary(IAsyncResult result);

	// Token: 0x0600452B RID: 17707
	[OperationContract(Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/GetGlsMachineStatus", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/GetGlsMachineStatusResponse")]
	string GetGlsMachineStatus();

	// Token: 0x0600452C RID: 17708
	[OperationContract(AsyncPattern = true, Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/GetGlsMachineStatus", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/LocatorService/GetGlsMachineStatusResponse")]
	IAsyncResult BeginGetGlsMachineStatus(AsyncCallback callback, object asyncState);

	// Token: 0x0600452D RID: 17709
	string EndGetGlsMachineStatus(IAsyncResult result);
}
