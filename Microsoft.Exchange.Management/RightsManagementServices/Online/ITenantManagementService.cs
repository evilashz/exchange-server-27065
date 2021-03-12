using System;
using System.CodeDom.Compiler;
using System.ServiceModel;

namespace Microsoft.RightsManagementServices.Online
{
	// Token: 0x0200073F RID: 1855
	[GeneratedCode("System.ServiceModel", "4.0.0.0")]
	[ServiceContract(Namespace = "http://microsoft.com/RightsManagementServiceOnline/2011/04", ConfigurationName = "Microsoft.RightsManagementServices.Online.ITenantManagementService")]
	public interface ITenantManagementService
	{
		// Token: 0x060041C3 RID: 16835
		[OperationContract(Action = "http://microsoft.com/RightsManagementServiceOnline/2011/04/ITenantManagementService/EnrollTenant", ReplyAction = "http://microsoft.com/RightsManagementServiceOnline/2011/04/ITenantManagementService/EnrollTenantResponse")]
		[FaultContract(typeof(ArgumentException), Action = "http://microsoft.com/RightsManagementServiceOnline/2011/04/ITenantManagementService/EnrollTenantArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		TenantInfo EnrollTenant(TenantEnrollmentInfo enrollmentInfo);

		// Token: 0x060041C4 RID: 16836
		[FaultContract(typeof(CommonFault), Action = "http://microsoft.com/RightsManagementServiceOnline/2011/04/ITenantManagementService/CreateOrUpdateTenantCommonFaultFault", Name = "CommonFault")]
		[OperationContract(Action = "http://microsoft.com/RightsManagementServiceOnline/2011/04/ITenantManagementService/CreateOrUpdateTenant", ReplyAction = "http://microsoft.com/RightsManagementServiceOnline/2011/04/ITenantManagementService/CreateOrUpdateTenantResponse")]
		TenantPublishingMessage CreateOrUpdateTenant(TenantProvisioningMessage provisioningMsg);

		// Token: 0x060041C5 RID: 16837
		[OperationContract(Action = "http://microsoft.com/RightsManagementServiceOnline/2011/04/ITenantManagementService/GetTenantInfoByCookie", ReplyAction = "http://microsoft.com/RightsManagementServiceOnline/2011/04/ITenantManagementService/GetTenantInfoByCookieResponse")]
		[FaultContract(typeof(ArgumentException), Action = "http://microsoft.com/RightsManagementServiceOnline/2011/04/ITenantManagementService/GetTenantInfoByCookieArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		TenantInfo[] GetTenantInfoByCookie(out string nextCookie, string cookie, int maxTenants);

		// Token: 0x060041C6 RID: 16838
		[FaultContract(typeof(ArgumentException), Action = "http://microsoft.com/RightsManagementServiceOnline/2011/04/ITenantManagementService/GetTenantInfoArgumentExceptionFault", Name = "ArgumentException", Namespace = "http://schemas.datacontract.org/2004/07/System")]
		[OperationContract(Action = "http://microsoft.com/RightsManagementServiceOnline/2011/04/ITenantManagementService/GetTenantInfo", ReplyAction = "http://microsoft.com/RightsManagementServiceOnline/2011/04/ITenantManagementService/GetTenantInfoResponse")]
		TenantInfo[] GetTenantInfo(string[] tenantIds);
	}
}
