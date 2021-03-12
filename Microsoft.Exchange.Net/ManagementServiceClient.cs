using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Exchange.HostedServices.AdminCenter.UI.Services;

// Token: 0x02000869 RID: 2153
[GeneratedCode("System.ServiceModel", "3.0.0.0")]
[DebuggerStepThrough]
internal class ManagementServiceClient : ClientBase<IManagementService>, IManagementService
{
	// Token: 0x06002DFF RID: 11775 RVA: 0x000661AC File Offset: 0x000643AC
	public ManagementServiceClient()
	{
	}

	// Token: 0x06002E00 RID: 11776 RVA: 0x000661B4 File Offset: 0x000643B4
	public ManagementServiceClient(string endpointConfigurationName) : base(endpointConfigurationName)
	{
	}

	// Token: 0x06002E01 RID: 11777 RVA: 0x000661BD File Offset: 0x000643BD
	public ManagementServiceClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x06002E02 RID: 11778 RVA: 0x000661C7 File Offset: 0x000643C7
	public ManagementServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x06002E03 RID: 11779 RVA: 0x000661D1 File Offset: 0x000643D1
	public ManagementServiceClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
	{
	}

	// Token: 0x06002E04 RID: 11780 RVA: 0x000661DB File Offset: 0x000643DB
	public bool PingManagementService()
	{
		return base.Channel.PingManagementService();
	}

	// Token: 0x06002E05 RID: 11781 RVA: 0x000661E8 File Offset: 0x000643E8
	public Company GetCompanyByName(string name)
	{
		return base.Channel.GetCompanyByName(name);
	}

	// Token: 0x06002E06 RID: 11782 RVA: 0x000661F6 File Offset: 0x000643F6
	public Company GetCompanyById(int companyId)
	{
		return base.Channel.GetCompanyById(companyId);
	}

	// Token: 0x06002E07 RID: 11783 RVA: 0x00066204 File Offset: 0x00064404
	public Company GetCompanyByGuid(Guid companyGuid)
	{
		return base.Channel.GetCompanyByGuid(companyGuid);
	}

	// Token: 0x06002E08 RID: 11784 RVA: 0x00066212 File Offset: 0x00064412
	public Domain GetDomain(string domainName)
	{
		return base.Channel.GetDomain(domainName);
	}

	// Token: 0x06002E09 RID: 11785 RVA: 0x00066220 File Offset: 0x00064420
	public Company[] GetAllCompanies(int parentCompanyId, int pageSize, int pageIndex)
	{
		return base.Channel.GetAllCompanies(parentCompanyId, pageSize, pageIndex);
	}

	// Token: 0x06002E0A RID: 11786 RVA: 0x00066230 File Offset: 0x00064430
	public Domain[] GetAllDomains(int companyId)
	{
		return base.Channel.GetAllDomains(companyId);
	}

	// Token: 0x06002E0B RID: 11787 RVA: 0x0006623E File Offset: 0x0006443E
	public Domain[] GetAllDomainsUnderReseller(int resellerCompanyId, int pageSize, int pageIndex)
	{
		return base.Channel.GetAllDomainsUnderReseller(resellerCompanyId, pageSize, pageIndex);
	}

	// Token: 0x06002E0C RID: 11788 RVA: 0x0006624E File Offset: 0x0006444E
	public string[] GetDataCenterIPs()
	{
		return base.Channel.GetDataCenterIPs();
	}

	// Token: 0x06002E0D RID: 11789 RVA: 0x0006625B File Offset: 0x0006445B
	public SmtpProfile[] GetCompanyInboundIPList(int companyId)
	{
		return base.Channel.GetCompanyInboundIPList(companyId);
	}

	// Token: 0x06002E0E RID: 11790 RVA: 0x00066269 File Offset: 0x00064469
	public SmtpProfile GetDomainInboundIPList(string domainName)
	{
		return base.Channel.GetDomainInboundIPList(domainName);
	}

	// Token: 0x06002E0F RID: 11791 RVA: 0x00066277 File Offset: 0x00064477
	public string[] GetCompanyIPList(int companyId, IPAddressType ipType)
	{
		return base.Channel.GetCompanyIPList(companyId, ipType);
	}

	// Token: 0x06002E10 RID: 11792 RVA: 0x00066286 File Offset: 0x00064486
	public string[] GetDomainIPList(string domainName, IPAddressType ipType)
	{
		return base.Channel.GetDomainIPList(domainName, ipType);
	}

	// Token: 0x06002E11 RID: 11793 RVA: 0x00066295 File Offset: 0x00064495
	public CompanyResponseInfoSet EnableCompanies(int[] companyIdList)
	{
		return base.Channel.EnableCompanies(companyIdList);
	}

	// Token: 0x06002E12 RID: 11794 RVA: 0x000662A3 File Offset: 0x000644A3
	public CompanyResponseInfoSet DisableCompanies(int[] companyIdList)
	{
		return base.Channel.DisableCompanies(companyIdList);
	}

	// Token: 0x06002E13 RID: 11795 RVA: 0x000662B1 File Offset: 0x000644B1
	public DomainResponseInfoSet EnableDomains(Domain[] domainList)
	{
		return base.Channel.EnableDomains(domainList);
	}

	// Token: 0x06002E14 RID: 11796 RVA: 0x000662BF File Offset: 0x000644BF
	public DomainResponseInfoSet DisableDomains(Domain[] domainList)
	{
		return base.Channel.DisableDomains(domainList);
	}

	// Token: 0x06002E15 RID: 11797 RVA: 0x000662CD File Offset: 0x000644CD
	public CompanyResponseInfoSet UpdateCompanySettings(CompanyConfigurationSettings[] settings)
	{
		return base.Channel.UpdateCompanySettings(settings);
	}

	// Token: 0x06002E16 RID: 11798 RVA: 0x000662DB File Offset: 0x000644DB
	public DomainResponseInfoSet UpdateDomainSettings(DomainConfigurationSettings[] settings)
	{
		return base.Channel.UpdateDomainSettings(settings);
	}

	// Token: 0x06002E17 RID: 11799 RVA: 0x000662E9 File Offset: 0x000644E9
	public DomainResponseInfoSet SetDefaultOutboundDomains(Domain[] defaultDomains)
	{
		return base.Channel.SetDefaultOutboundDomains(defaultDomains);
	}

	// Token: 0x06002E18 RID: 11800 RVA: 0x000662F7 File Offset: 0x000644F7
	public DomainResponseInfoSet UpdateDomainSettingsByGuids(DomainConfigurationSettings[] domainConfigs)
	{
		return base.Channel.UpdateDomainSettingsByGuids(domainConfigs);
	}
}
