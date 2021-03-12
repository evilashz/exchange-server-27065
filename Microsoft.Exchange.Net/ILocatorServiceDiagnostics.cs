using System;
using System.CodeDom.Compiler;
using System.ServiceModel;
using schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data;

// Token: 0x02000C49 RID: 3145
[ServiceContract(Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService", ConfigurationName = "ILocatorServiceDiagnostics")]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public interface ILocatorServiceDiagnostics
{
	// Token: 0x060044F9 RID: 17657
	[OperationContract(Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/ILocatorServiceDiagnostics/DIFindTenant", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/ILocatorServiceDiagnostics/DIFindTenantResponse")]
	[FaultContract(typeof(LocatorServiceFault), Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/ILocatorServiceDiagnostics/DIFindTenantLocatorServiceFaultFault", Name = "LocatorServiceFault", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	DIFindTenantResponse DIFindTenant(RequestIdentity identity, DIFindTenantRequest dIFindTenantRequest);

	// Token: 0x060044FA RID: 17658
	[OperationContract(AsyncPattern = true, Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/ILocatorServiceDiagnostics/DIFindTenant", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/ILocatorServiceDiagnostics/DIFindTenantResponse")]
	IAsyncResult BeginDIFindTenant(RequestIdentity identity, DIFindTenantRequest dIFindTenantRequest, AsyncCallback callback, object asyncState);

	// Token: 0x060044FB RID: 17659
	DIFindTenantResponse EndDIFindTenant(IAsyncResult result);

	// Token: 0x060044FC RID: 17660
	[FaultContract(typeof(LocatorServiceFault), Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/ILocatorServiceDiagnostics/DIFindDomainsLocatorServiceFaultFault", Name = "LocatorServiceFault", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[OperationContract(Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/ILocatorServiceDiagnostics/DIFindDomains", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/ILocatorServiceDiagnostics/DIFindDomainsResponse")]
	DIFindDomainsResponse DIFindDomains(RequestIdentity identity, DIFindDomainsRequest dIFindDomainsRequest);

	// Token: 0x060044FD RID: 17661
	[OperationContract(AsyncPattern = true, Action = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/ILocatorServiceDiagnostics/DIFindDomains", ReplyAction = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/ILocatorServiceDiagnostics/DIFindDomainsResponse")]
	IAsyncResult BeginDIFindDomains(RequestIdentity identity, DIFindDomainsRequest dIFindDomainsRequest, AsyncCallback callback, object asyncState);

	// Token: 0x060044FE RID: 17662
	DIFindDomainsResponse EndDIFindDomains(IAsyncResult result);
}
