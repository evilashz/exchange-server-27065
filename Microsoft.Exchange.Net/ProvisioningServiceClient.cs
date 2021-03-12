using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.HostedServices.AdminCenter.UI.Services;

// Token: 0x02000866 RID: 2150
[GeneratedCode("System.ServiceModel", "3.0.0.0")]
[DebuggerStepThrough]
internal class ProvisioningServiceClient : ClientBase<IProvisioningService>, IProvisioningService
{
	// Token: 0x06002DE0 RID: 11744 RVA: 0x00066138 File Offset: 0x00064338
	public ProvisioningServiceClient()
	{
	}

	// Token: 0x06002DE1 RID: 11745 RVA: 0x00066140 File Offset: 0x00064340
	public ProvisioningServiceClient(string endpointConfigurationName) : base(endpointConfigurationName)
	{
	}

	// Token: 0x06002DE2 RID: 11746 RVA: 0x00066149 File Offset: 0x00064349
	public ProvisioningServiceClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x06002DE3 RID: 11747 RVA: 0x00066153 File Offset: 0x00064353
	public ProvisioningServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x06002DE4 RID: 11748 RVA: 0x0006615D File Offset: 0x0006435D
	public ProvisioningServiceClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
	{
	}

	// Token: 0x06002DE5 RID: 11749 RVA: 0x00066167 File Offset: 0x00064367
	public bool PingProvisioningService()
	{
		return base.Channel.PingProvisioningService();
	}

	// Token: 0x06002DE6 RID: 11750 RVA: 0x00066174 File Offset: 0x00064374
	public CompanyResponseInfoSet CreateCompanies(Company[] companyList)
	{
		return base.Channel.CreateCompanies(companyList);
	}

	// Token: 0x06002DE7 RID: 11751 RVA: 0x00066182 File Offset: 0x00064382
	public CompanyResponseInfoSet DeleteCompanies(int[] companyIdList)
	{
		return base.Channel.DeleteCompanies(companyIdList);
	}

	// Token: 0x06002DE8 RID: 11752 RVA: 0x00066190 File Offset: 0x00064390
	public DomainResponseInfoSet AddDomains(Domain[] domainList)
	{
		return base.Channel.AddDomains(domainList);
	}

	// Token: 0x06002DE9 RID: 11753 RVA: 0x0006619E File Offset: 0x0006439E
	public DomainResponseInfoSet DeleteDomains(Domain[] domainList)
	{
		return base.Channel.DeleteDomains(domainList);
	}
}
