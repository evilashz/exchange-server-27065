using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Microsoft.RightsManagementServices.Online
{
	// Token: 0x02000741 RID: 1857
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[DebuggerStepThrough]
	public class TenantManagementServiceClient : ClientBase<ITenantManagementService>, ITenantManagementService
	{
		// Token: 0x060041C7 RID: 16839 RVA: 0x0010CA36 File Offset: 0x0010AC36
		public TenantManagementServiceClient()
		{
		}

		// Token: 0x060041C8 RID: 16840 RVA: 0x0010CA3E File Offset: 0x0010AC3E
		public TenantManagementServiceClient(string endpointConfigurationName) : base(endpointConfigurationName)
		{
		}

		// Token: 0x060041C9 RID: 16841 RVA: 0x0010CA47 File Offset: 0x0010AC47
		public TenantManagementServiceClient(string endpointConfigurationName, string remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x0010CA51 File Offset: 0x0010AC51
		public TenantManagementServiceClient(string endpointConfigurationName, EndpointAddress remoteAddress) : base(endpointConfigurationName, remoteAddress)
		{
		}

		// Token: 0x060041CB RID: 16843 RVA: 0x0010CA5B File Offset: 0x0010AC5B
		public TenantManagementServiceClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
		{
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x0010CA65 File Offset: 0x0010AC65
		public TenantInfo EnrollTenant(TenantEnrollmentInfo enrollmentInfo)
		{
			return base.Channel.EnrollTenant(enrollmentInfo);
		}

		// Token: 0x060041CD RID: 16845 RVA: 0x0010CA73 File Offset: 0x0010AC73
		public TenantPublishingMessage CreateOrUpdateTenant(TenantProvisioningMessage provisioningMsg)
		{
			return base.Channel.CreateOrUpdateTenant(provisioningMsg);
		}

		// Token: 0x060041CE RID: 16846 RVA: 0x0010CA81 File Offset: 0x0010AC81
		public TenantInfo[] GetTenantInfoByCookie(out string nextCookie, string cookie, int maxTenants)
		{
			return base.Channel.GetTenantInfoByCookie(out nextCookie, cookie, maxTenants);
		}

		// Token: 0x060041CF RID: 16847 RVA: 0x0010CA91 File Offset: 0x0010AC91
		public TenantInfo[] GetTenantInfo(string[] tenantIds)
		{
			return base.Channel.GetTenantInfo(tenantIds);
		}
	}
}
