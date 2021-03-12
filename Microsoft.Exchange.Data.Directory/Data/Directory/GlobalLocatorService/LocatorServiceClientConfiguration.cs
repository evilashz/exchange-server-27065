using System;
using System.Configuration;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000122 RID: 290
	internal class LocatorServiceClientConfiguration : ConfigurationSection
	{
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000C0B RID: 3083 RVA: 0x00036D12 File Offset: 0x00034F12
		public static string SectionName
		{
			get
			{
				return "globalLocatorService";
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x00036D19 File Offset: 0x00034F19
		public static LocatorServiceClientConfiguration Instance
		{
			get
			{
				return LocatorServiceClientConfiguration.instance;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x00036D20 File Offset: 0x00034F20
		public ServiceEndpoint Endpoint
		{
			get
			{
				if (!string.IsNullOrEmpty(this.EndpointUri))
				{
					return new ServiceEndpoint(new Uri(this.EndpointUri), this.EndpointUriTemplate ?? string.Empty, this.EndpointCertSubject, this.EndpointToken);
				}
				return null;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00036D5C File Offset: 0x00034F5C
		[ConfigurationProperty("endpointUri", IsRequired = false)]
		public string EndpointUri
		{
			get
			{
				return base["endpointUri"] as string;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000C0F RID: 3087 RVA: 0x00036D6E File Offset: 0x00034F6E
		[ConfigurationProperty("endpointUriTemplate", IsRequired = false)]
		public string EndpointUriTemplate
		{
			get
			{
				return base["endpointUriTemplate"] as string;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x00036D80 File Offset: 0x00034F80
		[ConfigurationProperty("endpointCertSubject", IsRequired = false)]
		public string EndpointCertSubject
		{
			get
			{
				return base["endpointCertSubject"] as string;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x00036D92 File Offset: 0x00034F92
		[ConfigurationProperty("endpointToken", IsRequired = false)]
		public string EndpointToken
		{
			get
			{
				return base["endpointToken"] as string;
			}
		}

		// Token: 0x0400063E RID: 1598
		private const string EndpointUriKey = "endpointUri";

		// Token: 0x0400063F RID: 1599
		private const string EndpointUriTemplateKey = "endpointUriTemplate";

		// Token: 0x04000640 RID: 1600
		private const string EndpointCertSubjectKey = "endpointCertSubject";

		// Token: 0x04000641 RID: 1601
		private const string EndpointTokenKey = "endpointToken";

		// Token: 0x04000642 RID: 1602
		private static LocatorServiceClientConfiguration instance = ((LocatorServiceClientConfiguration)ConfigurationManager.GetSection(LocatorServiceClientConfiguration.SectionName)) ?? new LocatorServiceClientConfiguration();
	}
}
