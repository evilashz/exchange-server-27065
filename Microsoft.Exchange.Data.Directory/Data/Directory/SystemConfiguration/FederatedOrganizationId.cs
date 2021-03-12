using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000479 RID: 1145
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class FederatedOrganizationId : ADConfigurationObject
	{
		// Token: 0x06003339 RID: 13113 RVA: 0x000CE7AC File Offset: 0x000CC9AC
		internal static string AddHybridConfigurationWellKnownSubDomain(string domain)
		{
			if (!string.IsNullOrEmpty(domain) && !FederatedOrganizationId.ContainsHybridConfigurationWellKnownSubDomain(domain) && !Globals.IsDatacenter)
			{
				return FederatedOrganizationId.HybridConfigurationWellKnownSubDomain + "." + domain;
			}
			return domain;
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x000CE7D7 File Offset: 0x000CC9D7
		internal static bool ContainsHybridConfigurationWellKnownSubDomain(string domain)
		{
			return domain != null && domain.StartsWith(FederatedOrganizationId.HybridConfigurationWellKnownSubDomain + ".", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x000CE7F4 File Offset: 0x000CC9F4
		internal static string RemoveHybridConfigurationWellKnownSubDomain(string domain)
		{
			string result = domain;
			if (FederatedOrganizationId.ContainsHybridConfigurationWellKnownSubDomain(domain))
			{
				result = domain.Substring(FederatedOrganizationId.HybridConfigurationWellKnownSubDomain.Length + 1);
			}
			return result;
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x0600333C RID: 13116 RVA: 0x000CE81F File Offset: 0x000CCA1F
		internal override bool IsShareable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x0600333D RID: 13117 RVA: 0x000CE824 File Offset: 0x000CCA24
		// (set) Token: 0x0600333E RID: 13118 RVA: 0x000CE857 File Offset: 0x000CCA57
		public SmtpDomain AccountNamespace
		{
			get
			{
				SmtpDomain smtpDomain = (SmtpDomain)this[FederatedOrganizationIdSchema.AccountNamespace];
				if (smtpDomain == null)
				{
					return null;
				}
				return new SmtpDomain(FederatedOrganizationId.RemoveHybridConfigurationWellKnownSubDomain(smtpDomain.Domain));
			}
			internal set
			{
				this[FederatedOrganizationIdSchema.AccountNamespace] = value;
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x0600333F RID: 13119 RVA: 0x000CE865 File Offset: 0x000CCA65
		// (set) Token: 0x06003340 RID: 13120 RVA: 0x000CE885 File Offset: 0x000CCA85
		public SmtpDomain AccountNamespaceWithWellKnownSubDomain
		{
			get
			{
				if (!Globals.IsDatacenter)
				{
					return (SmtpDomain)this[FederatedOrganizationIdSchema.AccountNamespace];
				}
				return this.AccountNamespace;
			}
			internal set
			{
				if (!Globals.IsDatacenter)
				{
					this[FederatedOrganizationIdSchema.AccountNamespace] = value;
					return;
				}
				this[FederatedOrganizationIdSchema.AccountNamespace] = new SmtpDomain(FederatedOrganizationId.RemoveHybridConfigurationWellKnownSubDomain(value.ToString()));
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06003341 RID: 13121 RVA: 0x000CE8B6 File Offset: 0x000CCAB6
		// (set) Token: 0x06003342 RID: 13122 RVA: 0x000CE8C8 File Offset: 0x000CCAC8
		public bool Enabled
		{
			get
			{
				return (bool)this[FederatedOrganizationIdSchema.Enabled];
			}
			set
			{
				this[FederatedOrganizationIdSchema.Enabled] = value;
			}
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06003343 RID: 13123 RVA: 0x000CE8DB File Offset: 0x000CCADB
		// (set) Token: 0x06003344 RID: 13124 RVA: 0x000CE8ED File Offset: 0x000CCAED
		public SmtpAddress OrganizationContact
		{
			get
			{
				return (SmtpAddress)this[FederatedOrganizationIdSchema.OrganizationContact];
			}
			set
			{
				this[FederatedOrganizationIdSchema.OrganizationContact] = value;
			}
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06003345 RID: 13125 RVA: 0x000CE900 File Offset: 0x000CCB00
		// (set) Token: 0x06003346 RID: 13126 RVA: 0x000CE912 File Offset: 0x000CCB12
		public ADObjectId DelegationTrustLink
		{
			get
			{
				return this[FederatedOrganizationIdSchema.DelegationTrustLink] as ADObjectId;
			}
			internal set
			{
				this[FederatedOrganizationIdSchema.DelegationTrustLink] = value;
			}
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06003347 RID: 13127 RVA: 0x000CE920 File Offset: 0x000CCB20
		// (set) Token: 0x06003348 RID: 13128 RVA: 0x000CE932 File Offset: 0x000CCB32
		public ADObjectId ClientTrustLink
		{
			get
			{
				return this[FederatedOrganizationIdSchema.ClientTrustLink] as ADObjectId;
			}
			internal set
			{
				this[FederatedOrganizationIdSchema.ClientTrustLink] = value;
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06003349 RID: 13129 RVA: 0x000CE940 File Offset: 0x000CCB40
		public MultiValuedProperty<ADObjectId> AcceptedDomainsBackLink
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[FederatedOrganizationIdSchema.AcceptedDomainsBackLink];
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x0600334A RID: 13130 RVA: 0x000CE952 File Offset: 0x000CCB52
		// (set) Token: 0x0600334B RID: 13131 RVA: 0x000CE964 File Offset: 0x000CCB64
		public ADObjectId DefaultSharingPolicyLink
		{
			get
			{
				return this[FederatedOrganizationIdSchema.DefaultSharingPolicyLink] as ADObjectId;
			}
			internal set
			{
				this[FederatedOrganizationIdSchema.DefaultSharingPolicyLink] = value;
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x0600334C RID: 13132 RVA: 0x000CE972 File Offset: 0x000CCB72
		internal override ADObjectSchema Schema
		{
			get
			{
				return FederatedOrganizationId.SchemaObject;
			}
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x0600334D RID: 13133 RVA: 0x000CE979 File Offset: 0x000CCB79
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchFedOrgId";
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x0600334E RID: 13134 RVA: 0x000CE980 File Offset: 0x000CCB80
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x0600334F RID: 13135 RVA: 0x000CE987 File Offset: 0x000CCB87
		// (set) Token: 0x06003350 RID: 13136 RVA: 0x000CE98F File Offset: 0x000CCB8F
		public new string Name
		{
			get
			{
				return base.Name;
			}
			internal set
			{
				base.Name = value;
			}
		}

		// Token: 0x04002382 RID: 9090
		public const string ContainerName = "Federation";

		// Token: 0x04002383 RID: 9091
		internal const string TaskNoun = "FederatedOrganizationIdentifier";

		// Token: 0x04002384 RID: 9092
		internal const string LdapName = "msExchFedOrgId";

		// Token: 0x04002385 RID: 9093
		internal static readonly ADObjectId Container = new ADObjectId(string.Format("CN={0}", "Federation"));

		// Token: 0x04002386 RID: 9094
		internal static readonly string HybridConfigurationWellKnownSubDomain = "FYDIBOHF25SPDLT";

		// Token: 0x04002387 RID: 9095
		private static readonly FederatedOrganizationIdSchema SchemaObject = ObjectSchema.GetInstance<FederatedOrganizationIdSchema>();
	}
}
