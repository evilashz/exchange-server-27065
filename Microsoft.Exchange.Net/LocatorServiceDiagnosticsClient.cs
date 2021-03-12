using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data;

// Token: 0x02000C4B RID: 3147
[DebuggerStepThrough]
[GeneratedCode("System.ServiceModel", "4.0.0.0")]
public class LocatorServiceDiagnosticsClient : ClientBase<ILocatorServiceDiagnostics>, ILocatorServiceDiagnostics
{
	// Token: 0x060044FF RID: 17663 RVA: 0x000B7155 File Offset: 0x000B5355
	public LocatorServiceDiagnosticsClient()
	{
	}

	// Token: 0x06004500 RID: 17664 RVA: 0x000B715D File Offset: 0x000B535D
	public LocatorServiceDiagnosticsClient(string endpointConfigurationName) : base(endpointConfigurationName)
	{
	}

	// Token: 0x06004501 RID: 17665 RVA: 0x000B7166 File Offset: 0x000B5366
	public LocatorServiceDiagnosticsClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x06004502 RID: 17666 RVA: 0x000B7170 File Offset: 0x000B5370
	public LocatorServiceDiagnosticsClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
	{
	}

	// Token: 0x06004503 RID: 17667 RVA: 0x000B717A File Offset: 0x000B537A
	public LocatorServiceDiagnosticsClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
	{
	}

	// Token: 0x06004504 RID: 17668 RVA: 0x000B7184 File Offset: 0x000B5384
	public DIFindTenantResponse DIFindTenant(RequestIdentity identity, DIFindTenantRequest dIFindTenantRequest)
	{
		return base.Channel.DIFindTenant(identity, dIFindTenantRequest);
	}

	// Token: 0x06004505 RID: 17669 RVA: 0x000B7193 File Offset: 0x000B5393
	public IAsyncResult BeginDIFindTenant(RequestIdentity identity, DIFindTenantRequest dIFindTenantRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginDIFindTenant(identity, dIFindTenantRequest, callback, asyncState);
	}

	// Token: 0x06004506 RID: 17670 RVA: 0x000B71A5 File Offset: 0x000B53A5
	public DIFindTenantResponse EndDIFindTenant(IAsyncResult result)
	{
		return base.Channel.EndDIFindTenant(result);
	}

	// Token: 0x06004507 RID: 17671 RVA: 0x000B71B3 File Offset: 0x000B53B3
	public DIFindDomainsResponse DIFindDomains(RequestIdentity identity, DIFindDomainsRequest dIFindDomainsRequest)
	{
		return base.Channel.DIFindDomains(identity, dIFindDomainsRequest);
	}

	// Token: 0x06004508 RID: 17672 RVA: 0x000B71C2 File Offset: 0x000B53C2
	public IAsyncResult BeginDIFindDomains(RequestIdentity identity, DIFindDomainsRequest dIFindDomainsRequest, AsyncCallback callback, object asyncState)
	{
		return base.Channel.BeginDIFindDomains(identity, dIFindDomainsRequest, callback, asyncState);
	}

	// Token: 0x06004509 RID: 17673 RVA: 0x000B71D4 File Offset: 0x000B53D4
	public DIFindDomainsResponse EndDIFindDomains(IAsyncResult result)
	{
		return base.Channel.EndDIFindDomains(result);
	}
}
