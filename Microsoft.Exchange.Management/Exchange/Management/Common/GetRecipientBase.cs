using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000105 RID: 261
	public abstract class GetRecipientBase<TIdentity, TDataObject> : GetRecipientObjectTask<TIdentity, TDataObject> where TIdentity : RecipientIdParameter, new() where TDataObject : ADObject, new()
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00021963 File Offset: 0x0001FB63
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x0002197C File Offset: 0x0001FB7C
		[ValidateNotNullOrEmpty]
		[Parameter]
		public string Filter
		{
			get
			{
				return (string)base.Fields["Filter"];
			}
			set
			{
				MonadFilter monadFilter = new MonadFilter(value, this, this.FilterableObjectSchema);
				this.inputFilter = monadFilter.InnerFilter;
				base.OptionalIdentityData.AdditionalFilter = monadFilter.InnerFilter;
				base.Fields["Filter"] = value;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x000219C5 File Offset: 0x0001FBC5
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x000219DC File Offset: 0x0001FBDC
		[ValidateLength(3, 5120)]
		[Parameter(ParameterSetName = "AnrSet")]
		public string Anr
		{
			get
			{
				return (string)base.Fields["Anr"];
			}
			set
			{
				if (char.IsWhiteSpace(value[0]))
				{
					throw new ArgumentException(Strings.ErrorStringContainsLeadingSpace(value, "Anr"));
				}
				if (char.IsWhiteSpace(value[value.Length - 1]))
				{
					throw new ArgumentException(Strings.ErrorStringContainsTrailingSpace(value, "Anr"));
				}
				base.Fields["Anr"] = value;
				this.inputFilter = new AmbiguousNameResolutionFilter(value);
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00021A55 File Offset: 0x0001FC55
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x00021A6C File Offset: 0x0001FC6C
		[Parameter]
		public OrganizationIdParameter Organization
		{
			get
			{
				return (OrganizationIdParameter)base.Fields["Organization"];
			}
			set
			{
				base.Fields["Organization"] = value;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00021A7F File Offset: 0x0001FC7F
		// (set) Token: 0x060007CF RID: 1999 RVA: 0x00021A87 File Offset: 0x0001FC87
		[Parameter(Mandatory = false)]
		public new AccountPartitionIdParameter AccountPartition
		{
			get
			{
				return base.AccountPartition;
			}
			set
			{
				base.AccountPartition = value;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x00021A90 File Offset: 0x0001FC90
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x00021AA7 File Offset: 0x0001FCA7
		[Parameter]
		public string SortBy
		{
			get
			{
				return (string)base.Fields["SortBy"];
			}
			set
			{
				base.Fields["SortBy"] = (string.IsNullOrEmpty(value) ? null : value);
				this.internalSortBy = QueryHelper.GetSortBy(this.SortBy, this.SortProperties);
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x00021ADC File Offset: 0x0001FCDC
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x00021AF3 File Offset: 0x0001FCF3
		[Parameter]
		public OrganizationalUnitIdParameter OrganizationalUnit
		{
			get
			{
				return (OrganizationalUnitIdParameter)base.Fields["OrganizationalUnit"];
			}
			set
			{
				base.Fields["OrganizationalUnit"] = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x00021B06 File Offset: 0x0001FD06
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x00021B0E File Offset: 0x0001FD0E
		[Parameter]
		public SwitchParameter IgnoreDefaultScope
		{
			get
			{
				return base.InternalIgnoreDefaultScope;
			}
			set
			{
				base.InternalIgnoreDefaultScope = value;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060007D6 RID: 2006
		internal abstract ObjectSchema FilterableObjectSchema { get; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060007D7 RID: 2007
		protected abstract PropertyDefinition[] SortProperties { get; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x00021B18 File Offset: 0x0001FD18
		protected virtual RecipientType[] RecipientTypes
		{
			get
			{
				RecipientIdParameter recipientIdParameter = Activator.CreateInstance<TIdentity>();
				return recipientIdParameter.RecipientTypes;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x00021B36 File Offset: 0x0001FD36
		protected virtual RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x00021B39 File Offset: 0x0001FD39
		protected override SortBy InternalSortBy
		{
			get
			{
				return this.internalSortBy;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x00021B44 File Offset: 0x0001FD44
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter = this.ConstructQueryFilterWithCustomFilter(this.inputFilter);
				if (this.UsnForReconciliationSearch >= 0L)
				{
					queryFilter = new AndFilter(new QueryFilter[]
					{
						queryFilter,
						new ComparisonFilter(ComparisonOperator.GreaterThan, ADRecipientSchema.UsnCreated, this.UsnForReconciliationSearch)
					});
				}
				return queryFilter;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x00021B94 File Offset: 0x0001FD94
		protected override ObjectId RootId
		{
			get
			{
				return this.rootId;
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00021B9C File Offset: 0x0001FD9C
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x00021BA4 File Offset: 0x0001FDA4
		protected long UsnForReconciliationSearch
		{
			get
			{
				return this.usnForReconciliationSearch;
			}
			set
			{
				this.usnForReconciliationSearch = value;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x00021BAD File Offset: 0x0001FDAD
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x00021BD3 File Offset: 0x0001FDD3
		public SwitchParameter SoftDeletedObject
		{
			get
			{
				return (SwitchParameter)(base.Fields["SoftDeletedObject"] ?? false);
			}
			set
			{
				base.Fields["SoftDeletedObject"] = value;
			}
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00021BEC File Offset: 0x0001FDEC
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (this.Identity != null && this.OrganizationalUnit != null && this.IgnoreDefaultScope)
			{
				base.ThrowTerminatingError(new ArgumentException(Strings.ErrorIdAndOUSetTogetherUnderIgnoreDefaultScope), ErrorCategory.InvalidArgument, null);
			}
			ExchangeOrganizationalUnit exchangeOrganizationalUnit = null;
			if (this.OrganizationalUnit != null)
			{
				bool useConfigNC = this.ConfigurationSession.UseConfigNC;
				bool useGlobalCatalog = this.ConfigurationSession.UseGlobalCatalog;
				this.ConfigurationSession.UseConfigNC = false;
				if (string.IsNullOrEmpty(this.ConfigurationSession.DomainController))
				{
					this.ConfigurationSession.UseGlobalCatalog = true;
				}
				try
				{
					exchangeOrganizationalUnit = (ExchangeOrganizationalUnit)base.GetDataObject<ExchangeOrganizationalUnit>(this.OrganizationalUnit, this.ConfigurationSession, (base.CurrentOrganizationId != null) ? base.CurrentOrganizationId.OrganizationalUnit : null, new LocalizedString?(Strings.ErrorOrganizationalUnitNotFound(this.OrganizationalUnit.ToString())), new LocalizedString?(Strings.ErrorOrganizationalUnitNotUnique(this.OrganizationalUnit.ToString())));
					RecipientTaskHelper.IsOrgnizationalUnitInOrganization(this.ConfigurationSession, base.CurrentOrganizationId, exchangeOrganizationalUnit, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
				}
				finally
				{
					this.ConfigurationSession.UseConfigNC = useConfigNC;
					this.ConfigurationSession.UseGlobalCatalog = useGlobalCatalog;
				}
			}
			if (exchangeOrganizationalUnit != null)
			{
				this.rootId = exchangeOrganizationalUnit.Id;
			}
			else
			{
				this.rootId = ((base.CurrentOrganizationId != null) ? base.CurrentOrganizationId.OrganizationalUnit : null);
			}
			if (this.UsnForReconciliationSearch >= 0L)
			{
				if (base.DomainController == null)
				{
					base.ThrowTerminatingError(new ArgumentException(Strings.ErrorDomainControllerNotSpecifiedWithUsnForReconciliationSearch), ErrorCategory.InvalidArgument, null);
				}
				base.InternalResultSize = Unlimited<uint>.UnlimitedValue;
				base.OptionalIdentityData.AdditionalFilter = new ComparisonFilter(ComparisonOperator.GreaterThan, ADRecipientSchema.UsnCreated, this.UsnForReconciliationSearch);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00021DD0 File Offset: 0x0001FFD0
		protected override void InternalStateReset()
		{
			base.CheckExclusiveParameters(new object[]
			{
				"AccountPartition",
				"Organization"
			});
			base.InternalStateReset();
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00021E04 File Offset: 0x00020004
		protected override OrganizationId ResolveCurrentOrganization()
		{
			if (this.Organization != null)
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(true, ConsistencyMode.PartiallyConsistent, sessionSettings, 410, "ResolveCurrentOrganization", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\GetRecipientBase.cs");
				tenantOrTopologyConfigurationSession.UseConfigNC = false;
				ADOrganizationalUnit adorganizationalUnit = (ADOrganizationalUnit)base.GetDataObject<ADOrganizationalUnit>(this.Organization, tenantOrTopologyConfigurationSession, null, new LocalizedString?(Strings.ErrorOrganizationNotFound(this.Organization.ToString())), new LocalizedString?(Strings.ErrorOrganizationNotUnique(this.Organization.ToString())));
				return adorganizationalUnit.OrganizationId;
			}
			return base.ResolveCurrentOrganization();
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00021EA4 File Offset: 0x000200A4
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = (IRecipientSession)base.CreateSession();
			if (this.IgnoreDefaultScope)
			{
				recipientSession.UseGlobalCatalog = (this.Identity == null && this.OrganizationalUnit == null);
			}
			return recipientSession;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00021EEC File Offset: 0x000200EC
		protected override bool ShouldSkipObject(IConfigurable dataObject)
		{
			if (dataObject is PagedPositionInfo)
			{
				return false;
			}
			RecipientType recipientType;
			RecipientTypeDetails recipientTypeDetails;
			if (dataObject is ReducedRecipient)
			{
				ReducedRecipient reducedRecipient = dataObject as ReducedRecipient;
				recipientType = reducedRecipient.RecipientType;
				recipientTypeDetails = reducedRecipient.RecipientTypeDetails;
			}
			else
			{
				ADRecipient adrecipient = dataObject as ADRecipient;
				recipientType = adrecipient.RecipientType;
				recipientTypeDetails = adrecipient.RecipientTypeDetails;
			}
			return Array.IndexOf<RecipientType>(this.RecipientTypes, recipientType) == -1 || (this.InternalRecipientTypeDetails != null && this.InternalRecipientTypeDetails.Length > 0 && Array.IndexOf<RecipientTypeDetails>(this.InternalRecipientTypeDetails, recipientTypeDetails) == -1);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00021F70 File Offset: 0x00020170
		protected QueryFilter ConstructQueryFilterWithCustomFilter(QueryFilter customFilter)
		{
			List<QueryFilter> list = new List<QueryFilter>();
			RecipientType[] recipientTypes = this.RecipientTypes;
			List<RecipientTypeDetails> list2 = new List<RecipientTypeDetails>();
			if (this.InternalRecipientTypeDetails != null && this.InternalRecipientTypeDetails.Length > 0)
			{
				foreach (RecipientTypeDetails recipientTypeDetails in this.InternalRecipientTypeDetails)
				{
					RecipientType recipientType = RecipientTaskHelper.RecipientTypeDetailsToRecipientType(recipientTypeDetails);
					if (recipientType != RecipientType.Invalid && Array.IndexOf<RecipientType>(this.RecipientTypes, recipientType) != -1)
					{
						list2.Add(recipientTypeDetails);
					}
					else if (base.IsVerboseOn)
					{
						base.WriteVerbose(Strings.VerboseRecipientTypeDetailsIgnored(recipientTypeDetails.ToString()));
					}
				}
				if (list2.Count == 0)
				{
					base.WriteError(new ArgumentException(Strings.ErrorRecipientTypeDetailsConflictWithRecipientType), ErrorCategory.InvalidArgument, null);
				}
			}
			QueryFilter internalFilter = base.InternalFilter;
			if (internalFilter != null)
			{
				list.Add(internalFilter);
			}
			QueryFilter recipientTypeDetailsFilter = RecipientIdParameter.GetRecipientTypeDetailsFilter(list2.ToArray());
			if (recipientTypeDetailsFilter != null)
			{
				list.Add(recipientTypeDetailsFilter);
			}
			else
			{
				list.Add(RecipientIdParameter.GetRecipientTypeFilter(recipientTypes));
			}
			if (this.Organization != null)
			{
				QueryFilter item = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, base.CurrentOrganizationId.OrganizationalUnit);
				list.Add(item);
			}
			if (customFilter != null)
			{
				list.Add(customFilter);
			}
			if (list.Count != 1)
			{
				return new AndFilter(list.ToArray());
			}
			return list[0];
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000220B8 File Offset: 0x000202B8
		protected ADObjectId GetDefaultPolicyId(ADObject user)
		{
			ADObjectId result;
			try
			{
				if (!this.defaultPolicyIds.ContainsKey(user.OrganizationId))
				{
					IConfigurationSession session = this.GenerateIConfigurationSessionForShareableObjects(user.OrganizationId);
					IList<MobileMailboxPolicy> defaultPolicies = DefaultMobileMailboxPolicyUtility<MobileMailboxPolicy>.GetDefaultPolicies(session);
					if (defaultPolicies.Count > 1)
					{
						this.WriteWarning(Strings.MultipleDefaultPoliciesExist(user.OrganizationId.ToString()));
					}
					this.defaultPolicyIds[user.OrganizationId] = ((defaultPolicies.Count > 0) ? defaultPolicies[0].Id : null);
				}
				result = this.defaultPolicyIds[user.OrganizationId];
			}
			catch (DataSourceTransientException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, null);
				result = null;
			}
			return result;
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00022168 File Offset: 0x00020368
		internal ADObjectId GetDefaultRetentionPolicyId(ADObject user, SharedConfiguration sharedConfig)
		{
			if (OrganizationId.ForestWideOrgId.Equals(user.OrganizationId))
			{
				return null;
			}
			bool flag = false;
			if (user[OrgPersonPresentationObjectSchema.RecipientTypeDetails] != null && (RecipientTypeDetails)user[OrgPersonPresentationObjectSchema.RecipientTypeDetails] == RecipientTypeDetails.ArbitrationMailbox)
			{
				flag = true;
			}
			OrganizationId organizationId = user.OrganizationId;
			ADObjectId adobjectId = null;
			if (!flag)
			{
				if (this.defaultRetentionPolicyIds.TryGetValue(organizationId, out adobjectId))
				{
					return adobjectId;
				}
			}
			try
			{
				IConfigurationSession scopedSession = this.GenerateIConfigurationSessionForShareableObjects(organizationId, sharedConfig);
				IList<RetentionPolicy> defaultRetentionPolicy = SharedConfiguration.GetDefaultRetentionPolicy(scopedSession, flag, null, 1);
				if (defaultRetentionPolicy != null && defaultRetentionPolicy.Count > 0)
				{
					adobjectId = defaultRetentionPolicy[0].Id;
				}
				else
				{
					adobjectId = null;
				}
				if (!flag)
				{
					this.defaultRetentionPolicyIds.Add(organizationId, adobjectId);
				}
			}
			catch (DataSourceTransientException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, null);
				return null;
			}
			return adobjectId;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0002223C File Offset: 0x0002043C
		internal ADObjectId GetDefaultSharingPolicyId(ADObject user, SharedConfiguration sharedConfig)
		{
			ADObjectId result;
			try
			{
				if (!this.defaultSharingPolicyIds.ContainsKey(user.OrganizationId))
				{
					IConfigurationSession configurationSession = this.GenerateIConfigurationSessionForShareableObjects(user.OrganizationId, sharedConfig);
					FederatedOrganizationId federatedOrganizationId = configurationSession.GetFederatedOrganizationId(configurationSession.SessionSettings.CurrentOrganizationId);
					this.defaultSharingPolicyIds[user.OrganizationId] = ((federatedOrganizationId != null) ? federatedOrganizationId.DefaultSharingPolicyLink : null);
				}
				result = this.defaultSharingPolicyIds[user.OrganizationId];
			}
			catch (DataSourceTransientException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, null);
				result = null;
			}
			return result;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x000222D0 File Offset: 0x000204D0
		private IConfigurationSession GenerateIConfigurationSessionForShareableObjects(OrganizationId organizationId)
		{
			return this.GenerateIConfigurationSessionForShareableObjects(organizationId, null);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x000222DC File Offset: 0x000204DC
		private IConfigurationSession GenerateIConfigurationSessionForShareableObjects(OrganizationId organizationId, SharedConfiguration sharedConfig)
		{
			ADSessionSettings adsessionSettings = null;
			if (sharedConfig != null)
			{
				adsessionSettings = sharedConfig.GetSharedConfigurationSessionSettings();
			}
			if (adsessionSettings == null)
			{
				adsessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, organizationId, base.ExecutingUserOrganizationId, false);
				adsessionSettings.IsSharedConfigChecked = true;
			}
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.NetCredential, adsessionSettings, 720, "GenerateIConfigurationSessionForShareableObjects", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Common\\recipient\\GetRecipientBase.cs");
		}

		// Token: 0x040003A5 RID: 933
		private Dictionary<OrganizationId, ADObjectId> defaultPolicyIds = new Dictionary<OrganizationId, ADObjectId>(8);

		// Token: 0x040003A6 RID: 934
		private Dictionary<OrganizationId, ADObjectId> defaultSharingPolicyIds = new Dictionary<OrganizationId, ADObjectId>(8);

		// Token: 0x040003A7 RID: 935
		private readonly Dictionary<OrganizationId, ADObjectId> defaultRetentionPolicyIds = new Dictionary<OrganizationId, ADObjectId>(8);

		// Token: 0x040003A8 RID: 936
		private QueryFilter inputFilter;

		// Token: 0x040003A9 RID: 937
		private SortBy internalSortBy;

		// Token: 0x040003AA RID: 938
		private ADObjectId rootId;

		// Token: 0x040003AB RID: 939
		private long usnForReconciliationSearch = -1L;
	}
}
