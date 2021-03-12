using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using Microsoft.Online.Provisioning.CompanyManagement;

// Token: 0x020002A8 RID: 680
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[DebuggerStepThrough]
public class CompanyManagerFederatedServiceTestClient : ClientBase<ICompanyManagerFederatedServiceTest>, ICompanyManagerFederatedServiceTest
{
	// Token: 0x06001336 RID: 4918 RVA: 0x00085A19 File Offset: 0x00083C19
	public CompanyManagerFederatedServiceTestClient()
	{
	}

	// Token: 0x06001337 RID: 4919 RVA: 0x00085A21 File Offset: 0x00083C21
	public CompanyManagerFederatedServiceTestClient(string endpointConfigurationName) : base(endpointConfigurationName)
	{
	}

	// Token: 0x06001338 RID: 4920 RVA: 0x00085A2A File Offset: 0x00083C2A
	public CompanyManagerFederatedServiceTestClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x06001339 RID: 4921 RVA: 0x00085A34 File Offset: 0x00083C34
	public CompanyManagerFederatedServiceTestClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x0600133A RID: 4922 RVA: 0x00085A3E File Offset: 0x00083C3E
	public CompanyManagerFederatedServiceTestClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
	{
	}

	// Token: 0x0600133B RID: 4923 RVA: 0x00085A48 File Offset: 0x00083C48
	public void FederatedServiceAddAuthorizedServiceInstanceToCompany(Guid contextId, string serviceInstance)
	{
		base.Channel.FederatedServiceAddAuthorizedServiceInstanceToCompany(contextId, serviceInstance);
	}

	// Token: 0x0600133C RID: 4924 RVA: 0x00085A57 File Offset: 0x00083C57
	public Task FederatedServiceAddAuthorizedServiceInstanceToCompanyAsync(Guid contextId, string serviceInstance)
	{
		return base.Channel.FederatedServiceAddAuthorizedServiceInstanceToCompanyAsync(contextId, serviceInstance);
	}

	// Token: 0x0600133D RID: 4925 RVA: 0x00085A66 File Offset: 0x00083C66
	public void FederatedServiceCreateUpdateDeleteSubscription(Subscription subscription)
	{
		base.Channel.FederatedServiceCreateUpdateDeleteSubscription(subscription);
	}

	// Token: 0x0600133E RID: 4926 RVA: 0x00085A74 File Offset: 0x00083C74
	public Task FederatedServiceCreateUpdateDeleteSubscriptionAsync(Subscription subscription)
	{
		return base.Channel.FederatedServiceCreateUpdateDeleteSubscriptionAsync(subscription);
	}
}
