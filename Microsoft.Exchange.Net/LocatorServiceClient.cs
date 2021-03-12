using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data;

// Token: 0x02000C4E RID: 3150
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[DebuggerStepThrough]
public class LocatorServiceClient : ClientBase<LocatorService>, LocatorService
{
	// Token: 0x0600452E RID: 17710 RVA: 0x000B71E2 File Offset: 0x000B53E2
	public LocatorServiceClient()
	{
	}

	// Token: 0x0600452F RID: 17711 RVA: 0x000B71EA File Offset: 0x000B53EA
	public LocatorServiceClient(string endpointConfigurationName) : base(endpointConfigurationName)
	{
	}

	// Token: 0x06004530 RID: 17712 RVA: 0x000B71F3 File Offset: 0x000B53F3
	public LocatorServiceClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x06004531 RID: 17713 RVA: 0x000B71FD File Offset: 0x000B53FD
	public LocatorServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x06004532 RID: 17714 RVA: 0x000B7207 File Offset: 0x000B5407
	public LocatorServiceClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
	{
	}

	// Token: 0x06004533 RID: 17715 RVA: 0x000B7211 File Offset: 0x000B5411
	public FindTenantResponse FindTenant(RequestIdentity identity, FindTenantRequest findTenantRequest)
	{
		return base.Channel.FindTenant(identity, findTenantRequest);
	}

	// Token: 0x06004534 RID: 17716 RVA: 0x000B7220 File Offset: 0x000B5420
	public IAsyncResult BeginFindTenant(RequestIdentity identity, FindTenantRequest findTenantRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginFindTenant(identity, findTenantRequest, callback, asyncState);
	}

	// Token: 0x06004535 RID: 17717 RVA: 0x000B7232 File Offset: 0x000B5432
	public FindTenantResponse EndFindTenant(IAsyncResult result)
	{
		return base.Channel.EndFindTenant(result);
	}

	// Token: 0x06004536 RID: 17718 RVA: 0x000B7240 File Offset: 0x000B5440
	public FindDomainResponse FindDomain(RequestIdentity identity, FindDomainRequest findDomainRequest)
	{
		return base.Channel.FindDomain(identity, findDomainRequest);
	}

	// Token: 0x06004537 RID: 17719 RVA: 0x000B724F File Offset: 0x000B544F
	public IAsyncResult BeginFindDomain(RequestIdentity identity, FindDomainRequest findDomainRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginFindDomain(identity, findDomainRequest, callback, asyncState);
	}

	// Token: 0x06004538 RID: 17720 RVA: 0x000B7261 File Offset: 0x000B5461
	public FindDomainResponse EndFindDomain(IAsyncResult result)
	{
		return base.Channel.EndFindDomain(result);
	}

	// Token: 0x06004539 RID: 17721 RVA: 0x000B726F File Offset: 0x000B546F
	public SaveTenantResponse SaveTenant(RequestIdentity identity, SaveTenantRequest saveTenantRequest)
	{
		return base.Channel.SaveTenant(identity, saveTenantRequest);
	}

	// Token: 0x0600453A RID: 17722 RVA: 0x000B727E File Offset: 0x000B547E
	public IAsyncResult BeginSaveTenant(RequestIdentity identity, SaveTenantRequest saveTenantRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginSaveTenant(identity, saveTenantRequest, callback, asyncState);
	}

	// Token: 0x0600453B RID: 17723 RVA: 0x000B7290 File Offset: 0x000B5490
	public SaveTenantResponse EndSaveTenant(IAsyncResult result)
	{
		return base.Channel.EndSaveTenant(result);
	}

	// Token: 0x0600453C RID: 17724 RVA: 0x000B729E File Offset: 0x000B549E
	public SaveDomainResponse SaveDomain(RequestIdentity identity, SaveDomainRequest saveDomainRequest)
	{
		return base.Channel.SaveDomain(identity, saveDomainRequest);
	}

	// Token: 0x0600453D RID: 17725 RVA: 0x000B72AD File Offset: 0x000B54AD
	public IAsyncResult BeginSaveDomain(RequestIdentity identity, SaveDomainRequest saveDomainRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginSaveDomain(identity, saveDomainRequest, callback, asyncState);
	}

	// Token: 0x0600453E RID: 17726 RVA: 0x000B72BF File Offset: 0x000B54BF
	public SaveDomainResponse EndSaveDomain(IAsyncResult result)
	{
		return base.Channel.EndSaveDomain(result);
	}

	// Token: 0x0600453F RID: 17727 RVA: 0x000B72CD File Offset: 0x000B54CD
	public DeleteTenantResponse DeleteTenant(RequestIdentity identity, DeleteTenantRequest deleteTenantRequest)
	{
		return base.Channel.DeleteTenant(identity, deleteTenantRequest);
	}

	// Token: 0x06004540 RID: 17728 RVA: 0x000B72DC File Offset: 0x000B54DC
	public IAsyncResult BeginDeleteTenant(RequestIdentity identity, DeleteTenantRequest deleteTenantRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginDeleteTenant(identity, deleteTenantRequest, callback, asyncState);
	}

	// Token: 0x06004541 RID: 17729 RVA: 0x000B72EE File Offset: 0x000B54EE
	public DeleteTenantResponse EndDeleteTenant(IAsyncResult result)
	{
		return base.Channel.EndDeleteTenant(result);
	}

	// Token: 0x06004542 RID: 17730 RVA: 0x000B72FC File Offset: 0x000B54FC
	public DeleteDomainResponse DeleteDomain(RequestIdentity identity, DeleteDomainRequest deleteDomainRequest)
	{
		return base.Channel.DeleteDomain(identity, deleteDomainRequest);
	}

	// Token: 0x06004543 RID: 17731 RVA: 0x000B730B File Offset: 0x000B550B
	public IAsyncResult BeginDeleteDomain(RequestIdentity identity, DeleteDomainRequest deleteDomainRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginDeleteDomain(identity, deleteDomainRequest, callback, asyncState);
	}

	// Token: 0x06004544 RID: 17732 RVA: 0x000B731D File Offset: 0x000B551D
	public DeleteDomainResponse EndDeleteDomain(IAsyncResult result)
	{
		return base.Channel.EndDeleteDomain(result);
	}

	// Token: 0x06004545 RID: 17733 RVA: 0x000B732B File Offset: 0x000B552B
	public FindDomainsResponse FindDomains(RequestIdentity identity, FindDomainsRequest findDomainsRequest)
	{
		return base.Channel.FindDomains(identity, findDomainsRequest);
	}

	// Token: 0x06004546 RID: 17734 RVA: 0x000B733A File Offset: 0x000B553A
	public IAsyncResult BeginFindDomains(RequestIdentity identity, FindDomainsRequest findDomainsRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginFindDomains(identity, findDomainsRequest, callback, asyncState);
	}

	// Token: 0x06004547 RID: 17735 RVA: 0x000B734C File Offset: 0x000B554C
	public FindDomainsResponse EndFindDomains(IAsyncResult result)
	{
		return base.Channel.EndFindDomains(result);
	}

	// Token: 0x06004548 RID: 17736 RVA: 0x000B735A File Offset: 0x000B555A
	public SaveUserResponse SaveUser(RequestIdentity identity, SaveUserRequest saveUserRequest)
	{
		return base.Channel.SaveUser(identity, saveUserRequest);
	}

	// Token: 0x06004549 RID: 17737 RVA: 0x000B7369 File Offset: 0x000B5569
	public IAsyncResult BeginSaveUser(RequestIdentity identity, SaveUserRequest saveUserRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginSaveUser(identity, saveUserRequest, callback, asyncState);
	}

	// Token: 0x0600454A RID: 17738 RVA: 0x000B737B File Offset: 0x000B557B
	public SaveUserResponse EndSaveUser(IAsyncResult result)
	{
		return base.Channel.EndSaveUser(result);
	}

	// Token: 0x0600454B RID: 17739 RVA: 0x000B7389 File Offset: 0x000B5589
	public FindUserResponse FindUser(RequestIdentity identity, FindUserRequest findUserRequest)
	{
		return base.Channel.FindUser(identity, findUserRequest);
	}

	// Token: 0x0600454C RID: 17740 RVA: 0x000B7398 File Offset: 0x000B5598
	public IAsyncResult BeginFindUser(RequestIdentity identity, FindUserRequest findUserRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginFindUser(identity, findUserRequest, callback, asyncState);
	}

	// Token: 0x0600454D RID: 17741 RVA: 0x000B73AA File Offset: 0x000B55AA
	public FindUserResponse EndFindUser(IAsyncResult result)
	{
		return base.Channel.EndFindUser(result);
	}

	// Token: 0x0600454E RID: 17742 RVA: 0x000B73B8 File Offset: 0x000B55B8
	public DeleteUserResponse DeleteUser(RequestIdentity identity, DeleteUserRequest deleteUserRequest)
	{
		return base.Channel.DeleteUser(identity, deleteUserRequest);
	}

	// Token: 0x0600454F RID: 17743 RVA: 0x000B73C7 File Offset: 0x000B55C7
	public IAsyncResult BeginDeleteUser(RequestIdentity identity, DeleteUserRequest deleteUserRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginDeleteUser(identity, deleteUserRequest, callback, asyncState);
	}

	// Token: 0x06004550 RID: 17744 RVA: 0x000B73D9 File Offset: 0x000B55D9
	public DeleteUserResponse EndDeleteUser(IAsyncResult result)
	{
		return base.Channel.EndDeleteUser(result);
	}

	// Token: 0x06004551 RID: 17745 RVA: 0x000B73E7 File Offset: 0x000B55E7
	public FindTenantResponse[] FindTenantFromPrimary(RequestIdentity identity, FindTenantRequest findTenantRequest, int? copiesToRead)
	{
		return base.Channel.FindTenantFromPrimary(identity, findTenantRequest, copiesToRead);
	}

	// Token: 0x06004552 RID: 17746 RVA: 0x000B73F7 File Offset: 0x000B55F7
	public IAsyncResult BeginFindTenantFromPrimary(RequestIdentity identity, FindTenantRequest findTenantRequest, int? copiesToRead, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginFindTenantFromPrimary(identity, findTenantRequest, copiesToRead, callback, asyncState);
	}

	// Token: 0x06004553 RID: 17747 RVA: 0x000B740B File Offset: 0x000B560B
	public FindTenantResponse[] EndFindTenantFromPrimary(IAsyncResult result)
	{
		return base.Channel.EndFindTenantFromPrimary(result);
	}

	// Token: 0x06004554 RID: 17748 RVA: 0x000B7419 File Offset: 0x000B5619
	public string GetGlsMachineStatus()
	{
		return base.Channel.GetGlsMachineStatus();
	}

	// Token: 0x06004555 RID: 17749 RVA: 0x000B7426 File Offset: 0x000B5626
	public IAsyncResult BeginGetGlsMachineStatus(AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginGetGlsMachineStatus(callback, asyncState);
	}

	// Token: 0x06004556 RID: 17750 RVA: 0x000B7435 File Offset: 0x000B5635
	public string EndGetGlsMachineStatus(IAsyncResult result)
	{
		return base.Channel.EndGetGlsMachineStatus(result);
	}
}
