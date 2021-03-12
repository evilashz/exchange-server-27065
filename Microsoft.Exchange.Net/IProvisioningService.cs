﻿using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using Microsoft.Exchange.HostedServices.AdminCenter.UI.Services;

// Token: 0x02000864 RID: 2148
[GeneratedCode("System.ServiceModel", "3.0.0.0")]
[ServiceContract(ConfigurationName = "IProvisioningService")]
internal interface IProvisioningService
{
	// Token: 0x06002DDB RID: 11739
	[OperationContract(Action = "http://tempuri.org/IProvisioningService/PingProvisioningService", ReplyAction = "http://tempuri.org/IProvisioningService/PingProvisioningServiceResponse")]
	bool PingProvisioningService();

	// Token: 0x06002DDC RID: 11740
	[OperationContract(Action = "http://tempuri.org/IProvisioningService/CreateCompanies", ReplyAction = "http://tempuri.org/IProvisioningService/CreateCompaniesResponse")]
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IProvisioningService/CreateCompaniesServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	CompanyResponseInfoSet CreateCompanies(Company[] companyList);

	// Token: 0x06002DDD RID: 11741
	[OperationContract(Action = "http://tempuri.org/IProvisioningService/DeleteCompanies", ReplyAction = "http://tempuri.org/IProvisioningService/DeleteCompaniesResponse")]
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IProvisioningService/DeleteCompaniesServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	CompanyResponseInfoSet DeleteCompanies(int[] companyIdList);

	// Token: 0x06002DDE RID: 11742
	[OperationContract(Action = "http://tempuri.org/IProvisioningService/AddDomains", ReplyAction = "http://tempuri.org/IProvisioningService/AddDomainsResponse")]
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IProvisioningService/AddDomainsServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	DomainResponseInfoSet AddDomains(Domain[] domainList);

	// Token: 0x06002DDF RID: 11743
	[OperationContract(Action = "http://tempuri.org/IProvisioningService/DeleteDomains", ReplyAction = "http://tempuri.org/IProvisioningService/DeleteDomainsResponse")]
	[FaultContract(typeof(ServiceFault), Action = "http://tempuri.org/IProvisioningService/DeleteDomainsServiceFaultFault", Name = "ServiceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Exchange.HostedServices.AdminCenter.UI.Services")]
	DomainResponseInfoSet DeleteDomains(Domain[] domainList);
}
