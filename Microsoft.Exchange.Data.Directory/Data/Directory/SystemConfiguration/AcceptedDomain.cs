using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002F7 RID: 759
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class AcceptedDomain : ADConfigurationObject, IProvisioningCacheInvalidation
	{
		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x0600232A RID: 9002 RVA: 0x00098E82 File Offset: 0x00097082
		// (set) Token: 0x0600232B RID: 9003 RVA: 0x00098E94 File Offset: 0x00097094
		public SmtpDomainWithSubdomains DomainName
		{
			get
			{
				return (SmtpDomainWithSubdomains)this[AcceptedDomainSchema.DomainName];
			}
			set
			{
				this[AcceptedDomainSchema.DomainName] = value;
			}
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x00098EA2 File Offset: 0x000970A2
		// (set) Token: 0x0600232D RID: 9005 RVA: 0x00098EB4 File Offset: 0x000970B4
		public ADObjectId CatchAllRecipientID
		{
			get
			{
				return (ADObjectId)this[AcceptedDomainSchema.CatchAllRecipient];
			}
			set
			{
				this[AcceptedDomainSchema.CatchAllRecipient] = value;
			}
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600232E RID: 9006 RVA: 0x00098EC2 File Offset: 0x000970C2
		// (set) Token: 0x0600232F RID: 9007 RVA: 0x00098ED4 File Offset: 0x000970D4
		[Parameter]
		public AcceptedDomainType DomainType
		{
			get
			{
				return (AcceptedDomainType)((int)this[AcceptedDomainSchema.AcceptedDomainType]);
			}
			set
			{
				this[AcceptedDomainSchema.AcceptedDomainType] = (int)value;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x06002330 RID: 9008 RVA: 0x00098EE7 File Offset: 0x000970E7
		// (set) Token: 0x06002331 RID: 9009 RVA: 0x00098EF9 File Offset: 0x000970F9
		public bool MatchSubDomains
		{
			get
			{
				return (bool)this[AcceptedDomainSchema.MatchSubDomains];
			}
			internal set
			{
				this[AcceptedDomainSchema.MatchSubDomains] = value;
			}
		}

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x06002332 RID: 9010 RVA: 0x00098F0C File Offset: 0x0009710C
		// (set) Token: 0x06002333 RID: 9011 RVA: 0x00098F1E File Offset: 0x0009711E
		[Parameter]
		public bool AddressBookEnabled
		{
			get
			{
				return (bool)this[AcceptedDomainSchema.AddressBookEnabled];
			}
			set
			{
				this[AcceptedDomainSchema.AddressBookEnabled] = value;
			}
		}

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x06002334 RID: 9012 RVA: 0x00098F31 File Offset: 0x00097131
		// (set) Token: 0x06002335 RID: 9013 RVA: 0x00098F43 File Offset: 0x00097143
		public bool Default
		{
			get
			{
				return (bool)this[AcceptedDomainSchema.Default];
			}
			internal set
			{
				this[AcceptedDomainSchema.Default] = value;
			}
		}

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x06002336 RID: 9014 RVA: 0x00098F56 File Offset: 0x00097156
		// (set) Token: 0x06002337 RID: 9015 RVA: 0x00098F68 File Offset: 0x00097168
		internal AuthenticationType RawAuthenticationType
		{
			get
			{
				return (AuthenticationType)this[AcceptedDomainSchema.RawAuthenticationType];
			}
			set
			{
				this[AcceptedDomainSchema.RawAuthenticationType] = value;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06002338 RID: 9016 RVA: 0x00098F7B File Offset: 0x0009717B
		public AuthenticationType? AuthenticationType
		{
			get
			{
				return (AuthenticationType?)this[AcceptedDomainSchema.AuthenticationType];
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x00098F8D File Offset: 0x0009718D
		// (set) Token: 0x0600233A RID: 9018 RVA: 0x00098F9F File Offset: 0x0009719F
		internal LiveIdInstanceType RawLiveIdInstanceType
		{
			get
			{
				return (LiveIdInstanceType)this[AcceptedDomainSchema.RawLiveIdInstanceType];
			}
			set
			{
				this[AcceptedDomainSchema.RawLiveIdInstanceType] = value;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x0600233B RID: 9019 RVA: 0x00098FB2 File Offset: 0x000971B2
		public LiveIdInstanceType? LiveIdInstanceType
		{
			get
			{
				return (LiveIdInstanceType?)this[AcceptedDomainSchema.LiveIdInstanceType];
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x0600233C RID: 9020 RVA: 0x00098FC4 File Offset: 0x000971C4
		// (set) Token: 0x0600233D RID: 9021 RVA: 0x00098FD6 File Offset: 0x000971D6
		[Parameter]
		public bool PendingRemoval
		{
			get
			{
				return (bool)this[AcceptedDomainSchema.PendingRemoval];
			}
			set
			{
				this[AcceptedDomainSchema.PendingRemoval] = value;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x0600233E RID: 9022 RVA: 0x00098FE9 File Offset: 0x000971E9
		// (set) Token: 0x0600233F RID: 9023 RVA: 0x00098FFB File Offset: 0x000971FB
		[Parameter]
		public bool PendingCompletion
		{
			get
			{
				return (bool)this[AcceptedDomainSchema.PendingCompletion];
			}
			set
			{
				this[AcceptedDomainSchema.PendingCompletion] = value;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002340 RID: 9024 RVA: 0x0009900E File Offset: 0x0009720E
		// (set) Token: 0x06002341 RID: 9025 RVA: 0x00099020 File Offset: 0x00097220
		[Parameter]
		public bool DualProvisioningEnabled
		{
			get
			{
				return (bool)this[AcceptedDomainSchema.DualProvisioningEnabled];
			}
			set
			{
				this[AcceptedDomainSchema.DualProvisioningEnabled] = value;
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002342 RID: 9026 RVA: 0x00099033 File Offset: 0x00097233
		// (set) Token: 0x06002343 RID: 9027 RVA: 0x00099045 File Offset: 0x00097245
		public ADObjectId FederatedOrganizationLink
		{
			get
			{
				return (ADObjectId)this[AcceptedDomainSchema.FederatedOrganizationLink];
			}
			internal set
			{
				this[AcceptedDomainSchema.FederatedOrganizationLink] = value;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002344 RID: 9028 RVA: 0x00099053 File Offset: 0x00097253
		// (set) Token: 0x06002345 RID: 9029 RVA: 0x00099065 File Offset: 0x00097265
		public ADObjectId MailFlowPartner
		{
			get
			{
				return (ADObjectId)this[AcceptedDomainSchema.MailFlowPartner];
			}
			set
			{
				this[AcceptedDomainSchema.MailFlowPartner] = value;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002346 RID: 9030 RVA: 0x00099073 File Offset: 0x00097273
		// (set) Token: 0x06002347 RID: 9031 RVA: 0x00099085 File Offset: 0x00097285
		[Parameter]
		public bool OutboundOnly
		{
			get
			{
				return (bool)this[AcceptedDomainSchema.OutboundOnly];
			}
			set
			{
				this[AcceptedDomainSchema.OutboundOnly] = value;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06002348 RID: 9032 RVA: 0x00099098 File Offset: 0x00097298
		// (set) Token: 0x06002349 RID: 9033 RVA: 0x000990AA File Offset: 0x000972AA
		public bool PendingFederatedAccountNamespace
		{
			get
			{
				return (bool)this[AcceptedDomainSchema.PendingFederatedAccountNamespace];
			}
			internal set
			{
				this[AcceptedDomainSchema.PendingFederatedAccountNamespace] = value;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x0600234A RID: 9034 RVA: 0x000990BD File Offset: 0x000972BD
		// (set) Token: 0x0600234B RID: 9035 RVA: 0x000990CF File Offset: 0x000972CF
		public bool PendingFederatedDomain
		{
			get
			{
				return (bool)this[AcceptedDomainSchema.PendingFederatedDomain];
			}
			internal set
			{
				this[AcceptedDomainSchema.PendingFederatedDomain] = value;
			}
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x0600234C RID: 9036 RVA: 0x000990E2 File Offset: 0x000972E2
		// (set) Token: 0x0600234D RID: 9037 RVA: 0x000990F4 File Offset: 0x000972F4
		public bool IsCoexistenceDomain
		{
			get
			{
				return (bool)this[AcceptedDomainSchema.IsCoexistenceDomain];
			}
			internal set
			{
				this[AcceptedDomainSchema.IsCoexistenceDomain] = value;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x0600234E RID: 9038 RVA: 0x00099107 File Offset: 0x00097307
		public bool PerimeterDuplicateDetected
		{
			get
			{
				return (bool)this[AcceptedDomainSchema.PerimeterDuplicateDetected];
			}
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x0600234F RID: 9039 RVA: 0x00099119 File Offset: 0x00097319
		// (set) Token: 0x06002350 RID: 9040 RVA: 0x0009912B File Offset: 0x0009732B
		public bool IsDefaultFederatedDomain
		{
			get
			{
				return (bool)this[AcceptedDomainSchema.IsDefaultFederatedDomain];
			}
			set
			{
				this[AcceptedDomainSchema.IsDefaultFederatedDomain] = value;
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002351 RID: 9041 RVA: 0x0009913E File Offset: 0x0009733E
		// (set) Token: 0x06002352 RID: 9042 RVA: 0x00099150 File Offset: 0x00097350
		public bool EnableNego2Authentication
		{
			get
			{
				return (bool)this[AcceptedDomainSchema.EnableNego2Authentication];
			}
			set
			{
				this[AcceptedDomainSchema.EnableNego2Authentication] = value;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x00099163 File Offset: 0x00097363
		internal override ADObjectSchema Schema
		{
			get
			{
				return AcceptedDomain.SchemaObject;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06002354 RID: 9044 RVA: 0x0009916A File Offset: 0x0009736A
		internal override ADObjectId ParentPath
		{
			get
			{
				return AcceptedDomain.AcceptedDomainContainer;
			}
		}

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06002355 RID: 9045 RVA: 0x00099171 File Offset: 0x00097371
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchAcceptedDomain";
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002356 RID: 9046 RVA: 0x00099178 File Offset: 0x00097378
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new AndFilter(new QueryFilter[]
				{
					base.ImplicitFilter,
					X400AuthoritativeDomain.NonX400DomainsFilter
				});
			}
		}

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06002357 RID: 9047 RVA: 0x000991A3 File Offset: 0x000973A3
		// (set) Token: 0x06002358 RID: 9048 RVA: 0x000991B5 File Offset: 0x000973B5
		public bool InitialDomain
		{
			get
			{
				return (bool)this[AcceptedDomainSchema.InitialDomain];
			}
			internal set
			{
				this[AcceptedDomainSchema.InitialDomain] = value;
			}
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x000991C8 File Offset: 0x000973C8
		internal static string FormatEhfOutboundOnlyDomainName(string domainName, Guid domainGuid)
		{
			int num = 38 + "DuplicateDomain".Length;
			if (string.IsNullOrEmpty(domainName))
			{
				throw new ArgumentNullException("domainName");
			}
			int num2 = 255 - num;
			if (domainName.Length > num2)
			{
				int num3 = domainName.Length - num2;
				if (domainName[num3] == '.')
				{
					num3++;
				}
				domainName = domainName.Substring(num3);
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}-{1}.{2}", new object[]
			{
				"DuplicateDomain",
				domainGuid,
				domainName
			});
		}

		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x00099254 File Offset: 0x00097454
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x0009925C File Offset: 0x0009745C
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.DomainName != null && this.DomainName.Equals(SmtpDomainWithSubdomains.StarDomain))
			{
				if (this.DomainType == AcceptedDomainType.Authoritative)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.StarAcceptedDomainCannotBeAuthoritative, AcceptedDomainSchema.AcceptedDomainType, this.DomainType));
				}
				if (this.Default)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.StarAcceptedDomainCannotBeDefault, AcceptedDomainSchema.Default, this.Default));
				}
				if (this.InitialDomain)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.StarAcceptedDomainCannotBeInitialDomain, AcceptedDomainSchema.InitialDomain, this.InitialDomain));
				}
			}
			if (this.EnableNego2Authentication && this.DomainName.IncludeSubDomains)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorAcceptedDomainCannotContainWildcardAndNegoConfig, AcceptedDomainSchema.EnableNego2Authentication, this.EnableNego2Authentication));
			}
		}

		// Token: 0x0600235C RID: 9052 RVA: 0x0009933C File Offset: 0x0009753C
		internal bool ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			orgId = null;
			keys = null;
			bool flag = false;
			if (base.OrganizationId == null)
			{
				return flag;
			}
			if (base.ObjectState == ObjectState.New || base.ObjectState == ObjectState.Deleted)
			{
				flag = true;
			}
			if (!flag && base.ObjectState == ObjectState.Changed && (base.IsChanged(AcceptedDomainSchema.DomainName) || base.IsChanged(AcceptedDomainSchema.AcceptedDomainType) || base.IsChanged(AcceptedDomainSchema.AcceptedDomainFlags) || base.IsChanged(ADObjectSchema.OrganizationalUnitRoot) || base.IsChanged(ADObjectSchema.ConfigurationUnit)))
			{
				flag = true;
			}
			if (flag)
			{
				orgId = base.OrganizationId;
				keys = new Guid[]
				{
					CannedProvisioningCacheKeys.OrganizationAcceptedDomains,
					CannedProvisioningCacheKeys.NamespaceAuthenticationTypeCacheKey
				};
			}
			return flag;
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x000993FB File Offset: 0x000975FB
		bool IProvisioningCacheInvalidation.ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			return this.ShouldInvalidProvisioningCache(out orgId, out keys);
		}

		// Token: 0x0600235E RID: 9054 RVA: 0x00099694 File Offset: 0x00097894
		internal static IEnumerable<QueryFilter> ConflictingDomainFilters(AcceptedDomain domain, bool ignoreStars)
		{
			if (domain != null && domain.DomainName != null)
			{
				if (domain.DomainName.Equals(SmtpDomainWithSubdomains.StarDomain))
				{
					yield return new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Guid, domain.Guid);
				}
				else
				{
					if (ignoreStars || domain.DomainName.IncludeSubDomains)
					{
						yield return new AndFilter(new QueryFilter[]
						{
							new TextFilter(AcceptedDomainSchema.DomainName, '.' + domain.DomainName.Domain, MatchOptions.Suffix, MatchFlags.IgnoreCase),
							new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Guid, domain.Guid)
						});
					}
					string s = domain.DomainName.SmtpDomain.Domain;
					for (int i = s.IndexOf('.', 0); i != -1; i = s.IndexOf('.', i + 1))
					{
						yield return new TextFilter(AcceptedDomainSchema.DomainName, ignoreStars ? s.Substring(i + 1) : ("*" + s.Substring(i)), MatchOptions.FullString, MatchFlags.IgnoreCase);
					}
					yield return new TextFilter(AcceptedDomainSchema.DomainName, "*", MatchOptions.FullString, MatchFlags.IgnoreCase);
				}
			}
			yield break;
		}

		// Token: 0x040015D5 RID: 5589
		internal const string LdapName = "msExchAcceptedDomain";

		// Token: 0x040015D6 RID: 5590
		internal static readonly ADObjectId AcceptedDomainContainer = new ADObjectId("CN=Accepted Domains,CN=Transport Settings");

		// Token: 0x040015D7 RID: 5591
		private static readonly AcceptedDomainSchema SchemaObject = ObjectSchema.GetInstance<AcceptedDomainSchema>();
	}
}
