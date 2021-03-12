using System;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.Web.Configuration;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000061 RID: 97
	internal class AutodiscoverWebConfiguration
	{
		// Token: 0x060002BB RID: 699 RVA: 0x000125D0 File Offset: 0x000107D0
		internal AutodiscoverWebConfiguration()
		{
			Configuration config = WebConfigurationManager.OpenWebConfiguration("~/web.config");
			ServiceElementCollection services = ServiceModelSectionGroup.GetSectionGroup(config).Services.Services;
			foreach (object obj in services)
			{
				ServiceElement serviceElement = (ServiceElement)obj;
				foreach (object obj2 in serviceElement.Endpoints)
				{
					ServiceEndpointElement serviceEndpointElement = (ServiceEndpointElement)obj2;
					if (!string.IsNullOrEmpty(serviceEndpointElement.BindingConfiguration) && serviceEndpointElement.Contract.Equals(Common.EndpointContract, StringComparison.OrdinalIgnoreCase))
					{
						if (serviceEndpointElement.Address.OriginalString.Equals("wssecurity/symmetrickey", StringComparison.OrdinalIgnoreCase))
						{
							this.wsSecuritySymmetricKeyEndpointEnabled = true;
						}
						else if (serviceEndpointElement.Address.OriginalString.Equals("wssecurity/x509cert", StringComparison.OrdinalIgnoreCase))
						{
							this.wsSecurityX509CertEndpointEnabled = true;
						}
						else if (serviceEndpointElement.Address.OriginalString.Equals("wssecurity", StringComparison.OrdinalIgnoreCase))
						{
							this.wsSecurityEndpointEnabled = true;
						}
						else if (serviceEndpointElement.Address.OriginalString.Equals(AutodiscoverWebConfiguration.soapAddress, StringComparison.OrdinalIgnoreCase))
						{
							this.soapEndpointEnabled = true;
						}
					}
				}
			}
			this.oAuthEndpointEnabled = OAuthHttpModule.IsModuleLoaded.Value;
			this.mySiteServiceUrlTemplate = WebConfigurationManager.AppSettings["mySiteServiceUrlTemplate"];
			this.mySiteLocationUrlTemplate = WebConfigurationManager.AppSettings["mySiteLocationUrlTemplate"];
			this.projectSiteServiceUrl = WebConfigurationManager.AppSettings["projectSiteServiceUrl"];
			this.projectSiteLocationUrl = WebConfigurationManager.AppSettings["projectSiteLocationUrl"];
			this.documentTypesSupportedForSharing = WebConfigurationManager.AppSettings["documentTypesSupportedForSharing"];
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002BC RID: 700 RVA: 0x000127D4 File Offset: 0x000109D4
		public string MySiteServiceUrlTemplate
		{
			get
			{
				return this.mySiteServiceUrlTemplate;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000127DC File Offset: 0x000109DC
		public string MySiteLocationUrlTemplate
		{
			get
			{
				return this.mySiteLocationUrlTemplate;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002BE RID: 702 RVA: 0x000127E4 File Offset: 0x000109E4
		public string ProjectSiteServiceUrl
		{
			get
			{
				return this.projectSiteServiceUrl;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002BF RID: 703 RVA: 0x000127EC File Offset: 0x000109EC
		public string ProjectSiteLocationUrl
		{
			get
			{
				return this.projectSiteLocationUrl;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x000127F4 File Offset: 0x000109F4
		public string DocumentTypesSupportedForSharing
		{
			get
			{
				return this.documentTypesSupportedForSharing;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x000127FC File Offset: 0x000109FC
		internal bool SoapEndpointEnabled
		{
			get
			{
				return this.soapEndpointEnabled;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00012804 File Offset: 0x00010A04
		internal bool WsSecurityEndpointEnabled
		{
			get
			{
				return this.wsSecurityEndpointEnabled;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0001280C File Offset: 0x00010A0C
		internal bool WsSecuritySymmetricKeyEndpointEnabled
		{
			get
			{
				return this.wsSecuritySymmetricKeyEndpointEnabled;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00012814 File Offset: 0x00010A14
		internal bool WsSecurityX509CertEndpointEnabled
		{
			get
			{
				return this.wsSecurityX509CertEndpointEnabled;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0001281C File Offset: 0x00010A1C
		internal bool OAuthEndpointEnabled
		{
			get
			{
				return this.oAuthEndpointEnabled;
			}
		}

		// Token: 0x040002B5 RID: 693
		private static string soapAddress = string.Empty;

		// Token: 0x040002B6 RID: 694
		private readonly bool soapEndpointEnabled;

		// Token: 0x040002B7 RID: 695
		private readonly bool wsSecurityEndpointEnabled;

		// Token: 0x040002B8 RID: 696
		private readonly bool wsSecuritySymmetricKeyEndpointEnabled;

		// Token: 0x040002B9 RID: 697
		private readonly bool wsSecurityX509CertEndpointEnabled;

		// Token: 0x040002BA RID: 698
		private readonly bool oAuthEndpointEnabled;

		// Token: 0x040002BB RID: 699
		private string mySiteServiceUrlTemplate;

		// Token: 0x040002BC RID: 700
		private string mySiteLocationUrlTemplate;

		// Token: 0x040002BD RID: 701
		private string projectSiteServiceUrl;

		// Token: 0x040002BE RID: 702
		private string projectSiteLocationUrl;

		// Token: 0x040002BF RID: 703
		private string documentTypesSupportedForSharing;
	}
}
