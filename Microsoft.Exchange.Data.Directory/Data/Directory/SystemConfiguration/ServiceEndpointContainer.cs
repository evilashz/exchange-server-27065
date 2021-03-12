using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020005C3 RID: 1475
	[ObjectScope(ConfigScopes.Global)]
	[Serializable]
	internal class ServiceEndpointContainer : Container
	{
		// Token: 0x060043E4 RID: 17380 RVA: 0x000FEC34 File Offset: 0x000FCE34
		public ServiceEndpoint GetEndpoint(string commonName)
		{
			if (string.IsNullOrEmpty(commonName))
			{
				throw new ArgumentNullException("commonName");
			}
			ADServiceConnectionPoint scp = this.GetSCP(commonName);
			if (scp == null)
			{
				throw new ServiceEndpointNotFoundException(commonName);
			}
			Uri uri = null;
			if (scp.ServiceBindingInformation.Count > 0)
			{
				uri = new Uri(scp.ServiceBindingInformation[0]);
			}
			string uriTemplate = null;
			string certificateSubject = null;
			string token = null;
			foreach (string text in scp.Keywords)
			{
				if (text.StartsWith(ServiceEndpointContainer.UriTemplateKey, StringComparison.OrdinalIgnoreCase))
				{
					uriTemplate = text.Substring(ServiceEndpointContainer.UriTemplateKey.Length);
				}
				else if (text.StartsWith(ServiceEndpointContainer.CertSubjectKey, StringComparison.OrdinalIgnoreCase))
				{
					certificateSubject = text.Substring(ServiceEndpointContainer.CertSubjectKey.Length);
				}
				else if (text.StartsWith(ServiceEndpointContainer.TokenKey, StringComparison.OrdinalIgnoreCase))
				{
					token = text.Substring(ServiceEndpointContainer.TokenKey.Length);
				}
			}
			return new ServiceEndpoint(uri, uriTemplate, certificateSubject, token);
		}

		// Token: 0x17001641 RID: 5697
		// (get) Token: 0x060043E5 RID: 17381 RVA: 0x000FED44 File Offset: 0x000FCF44
		internal override ADObjectSchema Schema
		{
			get
			{
				return ServiceEndpointContainer.schema;
			}
		}

		// Token: 0x17001642 RID: 5698
		// (get) Token: 0x060043E6 RID: 17382 RVA: 0x000FED4B File Offset: 0x000FCF4B
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ServiceEndpointContainer.mostDerivedClass;
			}
		}

		// Token: 0x060043E7 RID: 17383 RVA: 0x000FED52 File Offset: 0x000FCF52
		private ADServiceConnectionPoint GetSCP(string commonName)
		{
			return base.Session.Read<ADServiceConnectionPoint>(base.Id.GetChildId(commonName));
		}

		// Token: 0x04002E17 RID: 11799
		private static ServiceEndpointContainerSchema schema = ObjectSchema.GetInstance<ServiceEndpointContainerSchema>();

		// Token: 0x04002E18 RID: 11800
		private static string mostDerivedClass = "msExchContainer";

		// Token: 0x04002E19 RID: 11801
		public static readonly string DefaultName = "ServiceEndpoints";

		// Token: 0x04002E1A RID: 11802
		public static readonly string UriTemplateKey = "urltemplate:";

		// Token: 0x04002E1B RID: 11803
		public static readonly string CertSubjectKey = "subject:";

		// Token: 0x04002E1C RID: 11804
		public static readonly string TokenKey = "token:";
	}
}
