using System;
using System.ServiceModel;
using System.Xml;

namespace Microsoft.Exchange.Management.RightsManagement
{
	// Token: 0x02000742 RID: 1858
	internal static class RmsOnlineConstants
	{
		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x060041D0 RID: 16848 RVA: 0x0010CA9F File Offset: 0x0010AC9F
		public static TimeSpan SendTimeout
		{
			get
			{
				return TimeSpan.FromMinutes(1.0);
			}
		}

		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x060041D1 RID: 16849 RVA: 0x0010CAAF File Offset: 0x0010ACAF
		public static TimeSpan ReceiveTimeout
		{
			get
			{
				return TimeSpan.FromMinutes(1.0);
			}
		}

		// Token: 0x17001408 RID: 5128
		// (get) Token: 0x060041D2 RID: 16850 RVA: 0x0010CABF File Offset: 0x0010ACBF
		public static long MaxReceivedMessageSize
		{
			get
			{
				return 2147483647L;
			}
		}

		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x060041D3 RID: 16851 RVA: 0x0010CAC7 File Offset: 0x0010ACC7
		public static string BindingName
		{
			get
			{
				return "Microsoft.RightsManagementServices.Online.TenantManagementService";
			}
		}

		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x060041D4 RID: 16852 RVA: 0x0010CAD0 File Offset: 0x0010ACD0
		public static XmlDictionaryReaderQuotas ReaderQuotas
		{
			get
			{
				return new XmlDictionaryReaderQuotas
				{
					MaxStringContentLength = 70254592
				};
			}
		}

		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x060041D5 RID: 16853 RVA: 0x0010CAF0 File Offset: 0x0010ACF0
		public static WSHttpSecurity Security
		{
			get
			{
				return new WSHttpSecurity
				{
					Mode = SecurityMode.Transport,
					Transport = 
					{
						ClientCredentialType = HttpClientCredentialType.Certificate
					}
				};
			}
		}

		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x060041D6 RID: 16854 RVA: 0x0010CB17 File Offset: 0x0010AD17
		public static string AuthenticationCertificateSubjectDistinguishedName
		{
			get
			{
				return "CN=auth.outlook.com, OU=Exchange, O=Microsoft Corporation, L=Redmond, S=Washington, C=US";
			}
		}
	}
}
