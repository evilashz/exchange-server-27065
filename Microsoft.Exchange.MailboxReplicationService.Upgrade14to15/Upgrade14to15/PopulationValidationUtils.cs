using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.MailboxReplicationService.Upgrade14to15.TestTenantManagement;

namespace Microsoft.Exchange.MailboxReplicationService.Upgrade14to15
{
	// Token: 0x02000097 RID: 151
	public static class PopulationValidationUtils
	{
		// Token: 0x060003B6 RID: 950 RVA: 0x00005544 File Offset: 0x00003744
		public static List<Guid> GetTenantsBasedOnPopulationStatus(PopulationStatus status, string testTenantManagementUrl = "https://tws.365upgrade.microsoftonline.com/TestTenantManagementService.svc", string serviceInstancePrefix = "Exchange/namprd03")
		{
			List<Guid> list = new List<Guid>();
			using (ProxyWrapper<TestTenantManagementClient, ITestTenantManagement> proxyWrapper = new ProxyWrapper<TestTenantManagementClient, ITestTenantManagement>(new Uri(testTenantManagementUrl), PopulationValidationUtils.TestTenantManagementCert))
			{
				Tenant[] array = proxyWrapper.Proxy.QueryTenantsToPopulate(status);
				foreach (Tenant tenant in array)
				{
					if (string.IsNullOrEmpty(serviceInstancePrefix) || tenant.ExchangeServiceInstance.StartsWith(serviceInstancePrefix, true, CultureInfo.InvariantCulture))
					{
						list.Add(tenant.TenantId);
					}
				}
			}
			return list;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x000055D8 File Offset: 0x000037D8
		public static void SetPopulationStatus(Guid tenantId, PopulationStatus status, string testTenantManagementUrl = "https://tws.365upgrade.microsoftonline.com/TestTenantManagementService.svc")
		{
			using (ProxyWrapper<TestTenantManagementClient, ITestTenantManagement> proxyWrapper = new ProxyWrapper<TestTenantManagementClient, ITestTenantManagement>(new Uri(testTenantManagementUrl), PopulationValidationUtils.TestTenantManagementCert))
			{
				proxyWrapper.Proxy.UpdateTenantPopulationStatus(tenantId, status);
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00005620 File Offset: 0x00003820
		public static List<Guid> GetTenantsBasedOnValidationStatus(ValidationStatus status, string testTenantManagementUrl = "https://tws.365upgrade.microsoftonline.com/TestTenantManagementService.svc", string serviceInstancePrefix = "Exchange/namprd03")
		{
			List<Guid> list = new List<Guid>();
			using (ProxyWrapper<TestTenantManagementClient, ITestTenantManagement> proxyWrapper = new ProxyWrapper<TestTenantManagementClient, ITestTenantManagement>(new Uri(testTenantManagementUrl), PopulationValidationUtils.TestTenantManagementCert))
			{
				Tenant[] array = proxyWrapper.Proxy.QueryTenantsToValidate(status);
				foreach (Tenant tenant in array)
				{
					if (string.IsNullOrEmpty(serviceInstancePrefix) || tenant.ExchangeServiceInstance.StartsWith(serviceInstancePrefix, true, CultureInfo.InvariantCulture))
					{
						list.Add(tenant.TenantId);
					}
				}
			}
			return list;
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x000056B4 File Offset: 0x000038B4
		public static void SetValidationStatus(Guid tenantId, ValidationStatus status, int? bugsId, string testTenantManagementUrl = "https://tws.365upgrade.microsoftonline.com/TestTenantManagementService.svc")
		{
			using (ProxyWrapper<TestTenantManagementClient, ITestTenantManagement> proxyWrapper = new ProxyWrapper<TestTenantManagementClient, ITestTenantManagement>(new Uri(testTenantManagementUrl), PopulationValidationUtils.TestTenantManagementCert))
			{
				proxyWrapper.Proxy.UpdateTenantValidationStatus(tenantId, status, bugsId);
			}
		}

		// Token: 0x040001AC RID: 428
		private const string TestTenantCertificateSubject = "CN=auth.outlook.com, OU=Exchange, O=Microsoft Corporation, L=Redmond, S=Washington, C=US";

		// Token: 0x040001AD RID: 429
		private static readonly X509Certificate2 TestTenantManagementCert = CertificateHelper.GetExchangeCertificate("CN=auth.outlook.com, OU=Exchange, O=Microsoft Corporation, L=Redmond, S=Washington, C=US");
	}
}
