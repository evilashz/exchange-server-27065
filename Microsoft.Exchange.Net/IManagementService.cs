using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using Microsoft.Exchange.HostedServices.AdminCenter.UI.Services;

// Token: 0x02000867 RID: 2151
[GeneratedCode("System.ServiceModel", "3.0.0.0")]
[ServiceContract(ConfigurationName = "IManagementService")]
internal interface IManagementService
{
	// Token: 0x06002DEA RID: 11754
	[OperationContract(Action = "http://tempuri.org/IManagementService/PingManagementService", ReplyAction = "http://tempuri.org/IManagementService/PingManagementServiceResponse")]
	bool PingManagementService();

	// Token: 0x06002DEB RID: 11755
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/GetCompanyByNameServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[OperationContract(Action = "http://tempuri.org/IManagementService/GetCompanyByName", ReplyAction = "http://tempuri.org/IManagementService/GetCompanyByNameResponse")]
	Company GetCompanyByName(string name);

	// Token: 0x06002DEC RID: 11756
	[OperationContract(Action = "http://tempuri.org/IManagementService/GetCompanyById", ReplyAction = "http://tempuri.org/IManagementService/GetCompanyByIdResponse")]
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/GetCompanyByIdServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	Company GetCompanyById(int companyId);

	// Token: 0x06002DED RID: 11757
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/GetCompanyByGuidServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[OperationContract(Action = "http://tempuri.org/IManagementService/GetCompanyByGuid", ReplyAction = "http://tempuri.org/IManagementService/GetCompanyByGuidResponse")]
	Company GetCompanyByGuid(Guid companyGuid);

	// Token: 0x06002DEE RID: 11758
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/GetDomainServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[OperationContract(Action = "http://tempuri.org/IManagementService/GetDomain", ReplyAction = "http://tempuri.org/IManagementService/GetDomainResponse")]
	Domain GetDomain(string domainName);

	// Token: 0x06002DEF RID: 11759
	[OperationContract(Action = "http://tempuri.org/IManagementService/GetAllCompanies", ReplyAction = "http://tempuri.org/IManagementService/GetAllCompaniesResponse")]
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/GetAllCompaniesServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	Company[] GetAllCompanies(int parentCompanyId, int pageSize, int pageIndex);

	// Token: 0x06002DF0 RID: 11760
	[OperationContract(Action = "http://tempuri.org/IManagementService/GetAllDomains", ReplyAction = "http://tempuri.org/IManagementService/GetAllDomainsResponse")]
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/GetAllDomainsServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	Domain[] GetAllDomains(int companyId);

	// Token: 0x06002DF1 RID: 11761
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/GetAllDomainsUnderResellerServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[OperationContract(Action = "http://tempuri.org/IManagementService/GetAllDomainsUnderReseller", ReplyAction = "http://tempuri.org/IManagementService/GetAllDomainsUnderResellerResponse")]
	Domain[] GetAllDomainsUnderReseller(int resellerCompanyId, int pageSize, int pageIndex);

	// Token: 0x06002DF2 RID: 11762
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/GetDataCenterIPsServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[OperationContract(Action = "http://tempuri.org/IManagementService/GetDataCenterIPs", ReplyAction = "http://tempuri.org/IManagementService/GetDataCenterIPsResponse")]
	string[] GetDataCenterIPs();

	// Token: 0x06002DF3 RID: 11763
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/GetCompanyInboundIPListServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[OperationContract(Action = "http://tempuri.org/IManagementService/GetCompanyInboundIPList", ReplyAction = "http://tempuri.org/IManagementService/GetCompanyInboundIPListResponse")]
	SmtpProfile[] GetCompanyInboundIPList(int companyId);

	// Token: 0x06002DF4 RID: 11764
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/GetDomainInboundIPListServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[OperationContract(Action = "http://tempuri.org/IManagementService/GetDomainInboundIPList", ReplyAction = "http://tempuri.org/IManagementService/GetDomainInboundIPListResponse")]
	SmtpProfile GetDomainInboundIPList(string domainName);

	// Token: 0x06002DF5 RID: 11765
	[OperationContract(Action = "http://tempuri.org/IManagementService/GetCompanyIPList", ReplyAction = "http://tempuri.org/IManagementService/GetCompanyIPListResponse")]
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/GetCompanyIPListServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	string[] GetCompanyIPList(int companyId, IPAddressType ipType);

	// Token: 0x06002DF6 RID: 11766
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/GetDomainIPListServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[OperationContract(Action = "http://tempuri.org/IManagementService/GetDomainIPList", ReplyAction = "http://tempuri.org/IManagementService/GetDomainIPListResponse")]
	string[] GetDomainIPList(string domainName, IPAddressType ipType);

	// Token: 0x06002DF7 RID: 11767
	[OperationContract(Action = "http://tempuri.org/IManagementService/EnableCompanies", ReplyAction = "http://tempuri.org/IManagementService/EnableCompaniesResponse")]
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/EnableCompaniesServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	CompanyResponseInfoSet EnableCompanies(int[] companyIdList);

	// Token: 0x06002DF8 RID: 11768
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/DisableCompaniesServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[OperationContract(Action = "http://tempuri.org/IManagementService/DisableCompanies", ReplyAction = "http://tempuri.org/IManagementService/DisableCompaniesResponse")]
	CompanyResponseInfoSet DisableCompanies(int[] companyIdList);

	// Token: 0x06002DF9 RID: 11769
	[OperationContract(Action = "http://tempuri.org/IManagementService/EnableDomains", ReplyAction = "http://tempuri.org/IManagementService/EnableDomainsResponse")]
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/EnableDomainsServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	DomainResponseInfoSet EnableDomains(Domain[] domainList);

	// Token: 0x06002DFA RID: 11770
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/DisableDomainsServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[OperationContract(Action = "http://tempuri.org/IManagementService/DisableDomains", ReplyAction = "http://tempuri.org/IManagementService/DisableDomainsResponse")]
	DomainResponseInfoSet DisableDomains(Domain[] domainList);

	// Token: 0x06002DFB RID: 11771
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/UpdateCompanySettingsServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[OperationContract(Action = "http://tempuri.org/IManagementService/UpdateCompanySettings", ReplyAction = "http://tempuri.org/IManagementService/UpdateCompanySettingsResponse")]
	CompanyResponseInfoSet UpdateCompanySettings(CompanyConfigurationSettings[] settings);

	// Token: 0x06002DFC RID: 11772
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/UpdateDomainSettingsServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[OperationContract(Action = "http://tempuri.org/IManagementService/UpdateDomainSettings", ReplyAction = "http://tempuri.org/IManagementService/UpdateDomainSettingsResponse")]
	DomainResponseInfoSet UpdateDomainSettings(DomainConfigurationSettings[] settings);

	// Token: 0x06002DFD RID: 11773
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/SetDefaultOutboundDomainsServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[OperationContract(Action = "http://tempuri.org/IManagementService/SetDefaultOutboundDomains", ReplyAction = "http://tempuri.org/IManagementService/SetDefaultOutboundDomainsResponse")]
	DomainResponseInfoSet SetDefaultOutboundDomains(Domain[] defaultDomains);

	// Token: 0x06002DFE RID: 11774
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IManagementService/UpdateDomainSettingsByGuidsServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	[OperationContract(Action = "http://tempuri.org/IManagementService/UpdateDomainSettingsByGuids", ReplyAction = "http://tempuri.org/IManagementService/UpdateDomainSettingsByGuidsResponse")]
	DomainResponseInfoSet UpdateDomainSettingsByGuids(DomainConfigurationSettings[] domainConfigs);
}
