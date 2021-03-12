﻿using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.Online.Provisioning.CompanyManagement;

// Token: 0x020002A6 RID: 678
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
[ServiceContract(Namespace = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09", ConfigurationName = "ICompanyManagerFederatedServiceTest")]
public interface ICompanyManagerFederatedServiceTest
{
	// Token: 0x06001332 RID: 4914
	[FaultContract(typeof(ServiceUnavailableFault), Action = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09/ICompanyManagerFederatedServiceTest/FederatedServiceAddAuthorizedServiceInstanceToCompanyServiceUnavailableFaultFault", Name = "ServiceUnavailableFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Provisioning.CompanyManagement")]
	[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09/ICompanyManagerFederatedServiceTest/FederatedServiceAddAuthorizedServiceInstanceToCompanyBindingRedirectionFaultFault", Name = "BindingRedirectionFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Provisioning.CompanyManagement")]
	[FaultContract(typeof(DifferentServiceInstanceAlreadyExistsFault), Action = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09/ICompanyManagerFederatedServiceTest/FederatedServiceAddAuthorizedServiceInstanceToCompanyDifferentServiceInstanceAlreadyExistsFaultFault", Name = "DifferentServiceInstanceAlreadyExistsFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Provisioning.CompanyManagement")]
	[FaultContract(typeof(InvalidServiceInstanceFault), Action = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09/ICompanyManagerFederatedServiceTest/FederatedServiceAddAuthorizedServiceInstanceToCompanyInvalidServiceInstanceFaultFault", Name = "InvalidServiceInstanceFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Provisioning.CompanyManagement")]
	[FaultContract(typeof(CompanyNotFoundFault), Action = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09/ICompanyManagerFederatedServiceTest/FederatedServiceAddAuthorizedServiceInstanceToCompanyCompanyNotFoundFaultFault", Name = "CompanyNotFoundFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Provisioning.CompanyManagement")]
	[FaultContract(typeof(CompanyManagementFault), Action = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09/ICompanyManagerFederatedServiceTest/FederatedServiceAddAuthorizedServiceInstanceToCompanyCompanyManagementFaultFault", Name = "CompanyManagementFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Provisioning.CompanyManagement")]
	[OperationContract(Action = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09/ICompanyManagerFederatedServiceTest/FederatedServiceAddAuthorizedServiceInstanceToCompany", ReplyAction = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09/ICompanyManagerFederatedServiceTest/FederatedServiceAddAuthorizedServiceInstanceToCompanyResponse")]
	void FederatedServiceAddAuthorizedServiceInstanceToCompany(Guid contextId, string serviceInstance);

	// Token: 0x06001333 RID: 4915
	[OperationContract(Action = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09/ICompanyManagerFederatedServiceTest/FederatedServiceAddAuthorizedServiceInstanceToCompany", ReplyAction = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09/ICompanyManagerFederatedServiceTest/FederatedServiceAddAuthorizedServiceInstanceToCompanyResponse")]
	Task FederatedServiceAddAuthorizedServiceInstanceToCompanyAsync(Guid contextId, string serviceInstance);

	// Token: 0x06001334 RID: 4916
	[FaultContract(typeof(BindingRedirectionFault), Action = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09/ICompanyManagerFederatedServiceTest/FederatedServiceCreateUpdateDeleteSubscriptionBindingRedirectionFaultFault", Name = "BindingRedirectionFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Provisioning.CompanyManagement")]
	[OperationContract(Action = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09/ICompanyManagerFederatedServiceTest/FederatedServiceCreateUpdateDeleteSubscription", ReplyAction = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09/ICompanyManagerFederatedServiceTest/FederatedServiceCreateUpdateDeleteSubscriptionResponse")]
	[FaultContract(typeof(CompanyManagementFault), Action = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09/ICompanyManagerFederatedServiceTest/FederatedServiceCreateUpdateDeleteSubscriptionCompanyManagementFaultFault", Name = "CompanyManagementFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Provisioning.CompanyManagement")]
	[FaultContract(typeof(CompanyNotFoundFault), Action = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09/ICompanyManagerFederatedServiceTest/FederatedServiceCreateUpdateDeleteSubscriptionCompanyNotFoundFaultFault", Name = "CompanyNotFoundFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Provisioning.CompanyManagement")]
	[FaultContract(typeof(ServiceUnavailableFault), Action = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09/ICompanyManagerFederatedServiceTest/FederatedServiceCreateUpdateDeleteSubscriptionServiceUnavailableFaultFault", Name = "ServiceUnavailableFault", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Provisioning.CompanyManagement")]
	void FederatedServiceCreateUpdateDeleteSubscription(Subscription subscription);

	// Token: 0x06001335 RID: 4917
	[OperationContract(Action = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09/ICompanyManagerFederatedServiceTest/FederatedServiceCreateUpdateDeleteSubscription", ReplyAction = "http://schemas.microsoftonline.com/online/companymanager/federatedservicetest/2010/09/ICompanyManagerFederatedServiceTest/FederatedServiceCreateUpdateDeleteSubscriptionResponse")]
	Task FederatedServiceCreateUpdateDeleteSubscriptionAsync(Subscription subscription);
}
