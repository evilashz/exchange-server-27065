using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Directory.ValidationRules;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x020000AB RID: 171
	[Cmdlet("Get", "Recipient", DefaultParameterSetName = "Identity")]
	public sealed class GetRecipient : GetRecipientBase<RecipientIdParameter, ReducedRecipient>
	{
		// Token: 0x06000AC7 RID: 2759 RVA: 0x0002DA74 File Offset: 0x0002BC74
		static GetRecipient()
		{
			GetRecipient.OutputPropertiesToDefinitionDict = PartialPropertiesUtil.GetOutputPropertiesToDefinitionDict(ObjectSchema.GetInstance<ReducedRecipientSchema>(), typeof(ReducedRecipient), new Dictionary<string, string>
			{
				{
					"Identity",
					"Id"
				}
			});
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x0002DBC8 File Offset: 0x0002BDC8
		protected override int PageSize
		{
			get
			{
				if (base.InternalResultSize.IsUnlimited)
				{
					return 1000;
				}
				return (int)Math.Min(base.InternalResultSize.Value - base.WriteObjectCount + 1U, 1000U);
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x0002DC0C File Offset: 0x0002BE0C
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x0002DC3A File Offset: 0x0002BE3A
		[Parameter]
		[ValidateNotNullOrEmpty]
		public RecipientType[] RecipientType
		{
			get
			{
				RecipientType[] array = (RecipientType[])base.Fields["RecipientType"];
				if (array != null)
				{
					return array;
				}
				return base.RecipientTypes;
			}
			set
			{
				base.VerifyValues<RecipientType>(base.RecipientTypes, value);
				base.Fields["RecipientType"] = value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x0002DC5A File Offset: 0x0002BE5A
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x0002DC71 File Offset: 0x0002BE71
		[ValidateNotNullOrEmpty]
		[Parameter]
		public RecipientTypeDetails[] RecipientTypeDetails
		{
			get
			{
				return (RecipientTypeDetails[])base.Fields["RecipientTypeDetails"];
			}
			set
			{
				base.VerifyValues<RecipientTypeDetails>(GetRecipient.AllowedRecipientTypeDetails.Union(GetRecipient.AllowedRecipientTypeDetailsForColloborationMailbox).ToArray<RecipientTypeDetails>(), value);
				base.Fields["RecipientTypeDetails"] = value;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x0002DC9F File Offset: 0x0002BE9F
		// (set) Token: 0x06000ACE RID: 2766 RVA: 0x0002DCC0 File Offset: 0x0002BEC0
		[Parameter]
		public PropertySet PropertySet
		{
			get
			{
				return (PropertySet)(base.Fields["PropertySet"] ?? PropertySet.All);
			}
			set
			{
				base.Fields["PropertySet"] = value;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000ACF RID: 2767 RVA: 0x0002DCD8 File Offset: 0x0002BED8
		// (set) Token: 0x06000AD0 RID: 2768 RVA: 0x0002DCEF File Offset: 0x0002BEEF
		[Parameter]
		public string[] Properties
		{
			get
			{
				return (string[])base.Fields["Properties"];
			}
			set
			{
				base.Fields["Properties"] = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x0002DD02 File Offset: 0x0002BF02
		// (set) Token: 0x06000AD2 RID: 2770 RVA: 0x0002DD19 File Offset: 0x0002BF19
		[ValidateNotNullOrEmpty]
		[Parameter(ParameterSetName = "RecipientPreviewFilterSet")]
		public string RecipientPreviewFilter
		{
			get
			{
				return (string)base.Fields["RecipientPreviewFilter"];
			}
			set
			{
				base.Fields["RecipientPreviewFilter"] = value;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0002DD2C File Offset: 0x0002BF2C
		// (set) Token: 0x06000AD4 RID: 2772 RVA: 0x0002DD43 File Offset: 0x0002BF43
		[Parameter(ParameterSetName = "Identity")]
		public string BookmarkDisplayName
		{
			get
			{
				return (string)base.Fields["BookmarkDisplayName"];
			}
			set
			{
				base.Fields["BookmarkDisplayName"] = value;
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000AD5 RID: 2773 RVA: 0x0002DD56 File Offset: 0x0002BF56
		// (set) Token: 0x06000AD6 RID: 2774 RVA: 0x0002DD77 File Offset: 0x0002BF77
		[Parameter(ParameterSetName = "Identity")]
		public bool IncludeBookmarkObject
		{
			get
			{
				return (bool)(base.Fields["IncludeBookmarkObject"] ?? true);
			}
			set
			{
				base.Fields["IncludeBookmarkObject"] = value;
			}
		}

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x0002DD8F File Offset: 0x0002BF8F
		// (set) Token: 0x06000AD8 RID: 2776 RVA: 0x0002DDB0 File Offset: 0x0002BFB0
		[Parameter]
		public AuthenticationType AuthenticationType
		{
			get
			{
				return (AuthenticationType)(base.Fields["AuthenticationType"] ?? AuthenticationType.Managed);
			}
			set
			{
				base.Fields["AuthenticationType"] = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000AD9 RID: 2777 RVA: 0x0002DDC8 File Offset: 0x0002BFC8
		// (set) Token: 0x06000ADA RID: 2778 RVA: 0x0002DDDF File Offset: 0x0002BFDF
		[Parameter]
		public MultiValuedProperty<Capability> Capabilities
		{
			get
			{
				return (MultiValuedProperty<Capability>)base.Fields["Capabilities"];
			}
			set
			{
				base.Fields["Capabilities"] = value;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000ADB RID: 2779 RVA: 0x0002DDF2 File Offset: 0x0002BFF2
		// (set) Token: 0x06000ADC RID: 2780 RVA: 0x0002DE09 File Offset: 0x0002C009
		[ValidateNotNullOrEmpty]
		[Parameter(ParameterSetName = "DatabaseSet", ValueFromPipeline = true)]
		public DatabaseIdParameter Database
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["Database"];
			}
			set
			{
				base.Fields["Database"] = value;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x0002DE1C File Offset: 0x0002C01C
		internal override ObjectSchema FilterableObjectSchema
		{
			get
			{
				return ObjectSchema.GetInstance<ReducedRecipientSchema>();
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x0002DE24 File Offset: 0x0002C024
		protected override QueryFilter InternalFilter
		{
			get
			{
				QueryFilter queryFilter;
				if (this.recipientPreviewFilter != null)
				{
					queryFilter = base.ConstructQueryFilterWithCustomFilter(this.recipientPreviewFilter);
				}
				else
				{
					queryFilter = base.InternalFilter;
				}
				QueryFilter queryFilter2 = null;
				if (this.database != null)
				{
					queryFilter2 = new ComparisonFilter(ComparisonOperator.Equal, ADMailboxRecipientSchema.Database, this.database.Id);
				}
				return QueryFilter.AndTogether(new QueryFilter[]
				{
					queryFilter,
					queryFilter2,
					this.GetCapabilitiesFilter(),
					this.GetAuthenticationTypeFilter()
				});
			}
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0002DE9C File Offset: 0x0002C09C
		private QueryFilter GetCapabilitiesFilter()
		{
			QueryFilter result = null;
			if (this.Capabilities != null && this.Capabilities.Count > 0)
			{
				List<QueryFilter> list = new List<QueryFilter>(this.Capabilities.Count);
				foreach (Capability capability in this.Capabilities)
				{
					if (capability == Capability.None)
					{
						base.WriteError(new NotImplementedException(DirectoryStrings.CannotBuildCapabilityFilterUnsupportedCapability(capability.ToString())), ErrorCategory.InvalidArgument, null);
					}
					else
					{
						CapabilityIdentifierEvaluator capabilityIdentifierEvaluator = CapabilityIdentifierEvaluatorFactory.Create(capability);
						QueryFilter item;
						LocalizedString value;
						if (capabilityIdentifierEvaluator.TryGetFilter(base.CurrentOrganizationId, out item, out value))
						{
							list.Add(item);
						}
						else
						{
							base.WriteError(new ArgumentException(value), ErrorCategory.InvalidArgument, null);
						}
					}
				}
				result = QueryFilter.AndTogether(list.ToArray());
			}
			return result;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0002DF84 File Offset: 0x0002C184
		private QueryFilter GetAuthenticationTypeFilter()
		{
			QueryFilter result = null;
			LocalizedString value;
			if (base.Fields.Contains("AuthenticationType") && !ADRecipient.TryGetAuthenticationTypeFilterInternal(this.AuthenticationType, base.CurrentOrganizationId, out result, out value))
			{
				base.WriteError(new ArgumentException(value), ErrorCategory.InvalidArgument, null);
			}
			return result;
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0002DFD0 File Offset: 0x0002C1D0
		protected override PropertyDefinition[] SortProperties
		{
			get
			{
				return GetRecipient.SortPropertiesArray;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x0002DFD7 File Offset: 0x0002C1D7
		protected override RecipientType[] RecipientTypes
		{
			get
			{
				return this.RecipientType;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x0002DFDF File Offset: 0x0002C1DF
		protected override RecipientTypeDetails[] InternalRecipientTypeDetails
		{
			get
			{
				return this.RecipientTypeDetails ?? GetRecipient.AllowedRecipientTypeDetails;
			}
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x0002DFF0 File Offset: 0x0002C1F0
		protected override IEnumerable<ReducedRecipient> GetPagedData()
		{
			if (this.usingALbasedVlv)
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsInALVerboseString(base.DataSession, typeof(ReducedRecipient), this.addressList));
				return ADVlvPagedReader<ReducedRecipient>.GetADVlvPagedReader(this.addressList, (IRecipientSession)base.DataSession, this.InternalSortBy, this.IncludeBookmarkObject, true, this.PageSize, 1, this.BookmarkDisplayName, this.propertiesToRead);
			}
			base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.DataSession, typeof(ReducedRecipient), this.InternalFilter, this.RootId, this.DeepSearch));
			return ((IRecipientSession)base.DataSession).FindPaged<ReducedRecipient>((ADObjectId)this.RootId, this.DeepSearch ? QueryScope.SubTree : QueryScope.OneLevel, this.InternalFilter, this.InternalSortBy, this.PageSize, this.propertiesToRead);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0002E0CC File Offset: 0x0002C2CC
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = DirectorySessionFactory.Default.GetReducedRecipientSession((IRecipientSession)base.CreateSession(), 594, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\recipient\\GetRecipient.cs");
			this.usingALbasedVlv = this.IsUsingALbasedVlv(recipientSession);
			if (this.usingALbasedVlv)
			{
				recipientSession.UseGlobalCatalog = true;
				if (!string.IsNullOrEmpty(base.DomainController) && !recipientSession.IsReadConnectionAvailable())
				{
					IRecipientSession reducedRecipientSession = DirectorySessionFactory.Default.GetReducedRecipientSession(DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(null, true, ConsistencyMode.PartiallyConsistent, recipientSession.NetworkCredential, recipientSession.SessionSettings, 603, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\recipient\\GetRecipient.cs"), 603, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\recipient\\GetRecipient.cs");
					reducedRecipientSession.UseGlobalCatalog = true;
					recipientSession = reducedRecipientSession;
				}
			}
			else
			{
				if (base.IgnoreDefaultScope)
				{
					recipientSession.EnforceDefaultScope = false;
				}
				if (this.recipientPreviewFilter != null && this.recipientPreviewFilter is CustomLdapFilter)
				{
					recipientSession.ServerTimeout = new TimeSpan?(TimeSpan.FromSeconds(60.0));
				}
			}
			if (base.ParameterSetName == "DatabaseSet")
			{
				IRecipientSession reducedRecipientSession2 = DirectorySessionFactory.Default.GetReducedRecipientSession(DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(recipientSession.DomainController, recipientSession.SearchRoot, recipientSession.Lcid, recipientSession.ReadOnly, recipientSession.ConsistencyMode, recipientSession.NetworkCredential, recipientSession.SessionSettings, ConfigScopes.TenantSubTree, 633, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\recipient\\GetRecipient.cs"), 633, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\RecipientTasks\\recipient\\GetRecipient.cs");
				reducedRecipientSession2.EnforceDefaultScope = recipientSession.EnforceDefaultScope;
				reducedRecipientSession2.UseGlobalCatalog = recipientSession.UseGlobalCatalog;
				reducedRecipientSession2.LinkResolutionServer = recipientSession.LinkResolutionServer;
				reducedRecipientSession2.ServerTimeout = recipientSession.ServerTimeout;
				recipientSession = reducedRecipientSession2;
			}
			return recipientSession;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0002E270 File Offset: 0x0002C470
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			ReducedRecipient reducedRecipient = (ReducedRecipient)dataObject;
			if (reducedRecipient == null)
			{
				return null;
			}
			if (this.ShouldReadProperties(new ADPropertyDefinition[]
			{
				ReducedRecipientSchema.OwaMailboxPolicy,
				ReducedRecipientSchema.SharingPolicy,
				ReducedRecipientSchema.RetentionPolicy
			}) && reducedRecipient.RecipientType == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.UserMailbox)
			{
				SharedConfiguration sharedConfig = null;
				if (SharedConfiguration.IsDehydratedConfiguration(reducedRecipient.OrganizationId) || (SharedConfiguration.GetSharedConfigurationState(reducedRecipient.OrganizationId) & SharedTenantConfigurationState.Static) != SharedTenantConfigurationState.UnSupported)
				{
					sharedConfig = SharedConfiguration.GetSharedConfiguration(reducedRecipient.OrganizationId);
				}
				if (this.ShouldReadProperties(new ADPropertyDefinition[]
				{
					ReducedRecipientSchema.OwaMailboxPolicy
				}) && reducedRecipient.OwaMailboxPolicy == null)
				{
					ADObjectId defaultOwaMailboxPolicyId = this.GetDefaultOwaMailboxPolicyId(reducedRecipient);
					if (defaultOwaMailboxPolicyId != null)
					{
						reducedRecipient.OwaMailboxPolicy = defaultOwaMailboxPolicyId;
					}
				}
				if (this.ShouldReadProperties(new ADPropertyDefinition[]
				{
					ReducedRecipientSchema.SharingPolicy
				}) && reducedRecipient.SharingPolicy == null)
				{
					reducedRecipient.SharingPolicy = base.GetDefaultSharingPolicyId(reducedRecipient, sharedConfig);
				}
				if (this.ShouldReadProperties(new ADPropertyDefinition[]
				{
					ReducedRecipientSchema.RetentionPolicy
				}) && reducedRecipient.RetentionPolicy == null && reducedRecipient.ShouldUseDefaultRetentionPolicy)
				{
					reducedRecipient.RetentionPolicy = base.GetDefaultRetentionPolicyId(reducedRecipient, sharedConfig);
				}
			}
			if (this.ShouldReadProperties(new ADPropertyDefinition[]
			{
				ReducedRecipientSchema.ActiveSyncMailboxPolicy,
				ReducedRecipientSchema.ActiveSyncMailboxPolicyIsDefaulted
			}) && reducedRecipient.ActiveSyncMailboxPolicy == null && !reducedRecipient.ExchangeVersion.IsOlderThan(ReducedRecipientSchema.ActiveSyncMailboxPolicy.VersionAdded))
			{
				ADObjectId defaultPolicyId = base.GetDefaultPolicyId(reducedRecipient);
				if (defaultPolicyId != null)
				{
					reducedRecipient.ActiveSyncMailboxPolicy = defaultPolicyId;
					reducedRecipient.ActiveSyncMailboxPolicyIsDefaulted = true;
				}
			}
			if (this.ShouldReadProperties(new ADPropertyDefinition[]
			{
				ReducedRecipientSchema.AuthenticationType
			}) && reducedRecipient.OrganizationId.ConfigurationUnit != null)
			{
				SmtpAddress windowsLiveID = reducedRecipient.WindowsLiveID;
				if (reducedRecipient.WindowsLiveID.Domain != null && !reducedRecipient.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
				{
					reducedRecipient.AuthenticationType = MailboxTaskHelper.GetNamespaceAuthenticationType(reducedRecipient.OrganizationId, reducedRecipient.WindowsLiveID.Domain);
				}
			}
			if (this.ShouldReadProperties(new ADPropertyDefinition[]
			{
				ReducedRecipientSchema.Capabilities
			}))
			{
				reducedRecipient.PopulateCapabilitiesProperty();
			}
			return reducedRecipient;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0002E484 File Offset: 0x0002C684
		internal ADObjectId GetDefaultOwaMailboxPolicyId(ADObject user)
		{
			ADObjectId adobjectId = null;
			OrganizationId organizationId = user.OrganizationId;
			if (!this.owaMailboxPolicyCache.TryGetValue(organizationId, out adobjectId))
			{
				OwaMailboxPolicy defaultOwaMailboxPolicy = OwaSegmentationSettings.GetDefaultOwaMailboxPolicy(organizationId);
				if (defaultOwaMailboxPolicy != null)
				{
					adobjectId = defaultOwaMailboxPolicy.Id;
				}
				this.owaMailboxPolicyCache.Add(organizationId, adobjectId);
			}
			return adobjectId;
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0002E4CC File Offset: 0x0002C6CC
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.CheckExclusiveParameters(new object[]
			{
				"Properties",
				"PropertySet"
			});
			if (this.Identity != null && (this.PropertySet != PropertySet.All || base.Fields.IsModified("Properties")))
			{
				this.WriteWarning(base.Fields.IsModified("Properties") ? Strings.WarningPropertiesIgnored : Strings.WarningPropertySetIgnored);
				this.PropertySet = PropertySet.All;
			}
			base.InternalBeginProcessing();
			if (!string.IsNullOrEmpty(this.RecipientPreviewFilter))
			{
				this.recipientPreviewFilter = this.GetOPathFilter(this.RecipientPreviewFilter);
				if (this.recipientPreviewFilter == null)
				{
					this.recipientPreviewFilter = new CustomLdapFilter(this.RecipientPreviewFilter);
				}
			}
			if (this.Identity == null)
			{
				if (base.Fields.IsModified("Properties"))
				{
					this.InitializePropertiesToRead();
				}
				else
				{
					this.propertiesToRead = ReducedRecipient.Properties[this.PropertySet];
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0002E5C4 File Offset: 0x0002C7C4
		private void InitializePropertiesToRead()
		{
			IList<string> userSpecifiedProperties = PartialPropertiesUtil.ParseUserSpecifiedProperties(this.Properties);
			LocalizedString value;
			if (!PartialPropertiesUtil.ValidateProperties(userSpecifiedProperties, GetRecipient.OutputPropertiesToDefinitionDict, out value))
			{
				base.ThrowTerminatingError(new ArgumentException(value), ErrorCategory.InvalidArgument, null);
			}
			this.propertiesToRead = PartialPropertiesUtil.CalculatePropertiesToRead(GetRecipient.OutputPropertiesToDefinitionDict, userSpecifiedProperties, GetRecipient.MandatoryOutputProperties, GetRecipient.PropertyRelationship, GetRecipient.SpecialPropertiesLeadToAllRead, base.IsVerboseOn ? new WriteVerboseDelegate(base.WriteVerbose) : null);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0002E644 File Offset: 0x0002C844
		private bool ShouldReadProperties(params ADPropertyDefinition[] propertyDefinitions)
		{
			return this.propertiesToRead == null || propertyDefinitions.Any((ADPropertyDefinition property) => this.propertiesToRead.Contains(property));
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0002E664 File Offset: 0x0002C864
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			this.database = null;
			if (this.Database != null)
			{
				this.Database.AllowLegacy = true;
				this.database = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.Database, base.GlobalConfigSession, null, new LocalizedString?(Strings.ErrorDatabaseNotFound(this.Database.ToString())), new LocalizedString?(Strings.ErrorDatabaseNotUnique(this.Database.ToString())));
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0002E6F0 File Offset: 0x0002C8F0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalProcessRecord();
				if (this.usingALbasedVlv && base.WriteObjectCount == 0U && this.ConfigurationSession.Read<AddressBookBase>(this.addressList) == null)
				{
					this.WriteWarning(Strings.WarningSystemAddressListNotFound(this.addressList.Name));
					this.usingALbasedVlv = false;
					base.InternalProcessRecord();
				}
			}
			catch (ADOperationException ex)
			{
				if (!ADSession.IsLdapFilterError(ex) || string.IsNullOrEmpty(this.RecipientPreviewFilter))
				{
					throw;
				}
				base.WriteError(new ArgumentException(Strings.ErrorInvalidRecipientPreviewFilter(this.RecipientPreviewFilter)), ErrorCategory.InvalidArgument, null);
			}
			if (!this.usingALbasedVlv)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string text in GetRecipient.Parameters)
				{
					if (base.Fields.IsModified(text))
					{
						stringBuilder.Append(text);
						stringBuilder.Append(", ");
					}
				}
				if (stringBuilder.Length != 0)
				{
					this.WriteWarning(Strings.WarningParametersIgnored(stringBuilder.Remove(stringBuilder.Length - 2, 2).ToString()));
				}
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002E824 File Offset: 0x0002CA24
		private bool IsUsingALbasedVlv(IDirectorySession session)
		{
			ADSessionSettings sessionSettings = session.SessionSettings;
			if (this.Identity != null || !string.IsNullOrEmpty(base.Anr) || !string.IsNullOrEmpty(base.Filter) || !string.IsNullOrEmpty(this.RecipientPreviewFilter) || (this.InternalSortBy != null && this.InternalSortBy.ColumnDefinition != ADRecipientSchema.DisplayName) || (this.RecipientTypeDetails != null && this.RecipientTypeDetails.Length != 0) || base.Fields.Contains("AuthenticationType") || this.Database != null || this.Capabilities != null)
			{
				return false;
			}
			AddressListType addressListType;
			if (base.Fields["RecipientType"] == null)
			{
				addressListType = AddressListType.AllRecipients;
			}
			else
			{
				if (this.RecipientTypes.Length != 1 || this.RecipientTypes[0] != Microsoft.Exchange.Data.Directory.Recipient.RecipientType.UserMailbox)
				{
					if (this.RecipientTypes.Length == 3)
					{
						if (Array.TrueForAll<RecipientType>(this.RecipientTypes, (RecipientType item) => item == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MailNonUniversalGroup || item == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MailUniversalDistributionGroup || item == Microsoft.Exchange.Data.Directory.Recipient.RecipientType.MailUniversalSecurityGroup))
						{
							addressListType = AddressListType.AllGroups;
							goto IL_F3;
						}
					}
					return false;
				}
				addressListType = AddressListType.AllMailboxes;
			}
			IL_F3:
			if (this.RootId != null && !ADObjectId.Equals(base.CurrentOrganizationId.OrganizationalUnit, (ADObjectId)this.RootId))
			{
				return false;
			}
			if (base.CurrentOrganizationId == OrganizationId.ForestWideOrgId && !session.UseGlobalCatalog)
			{
				return false;
			}
			Organization orgContainer = this.ConfigurationSession.GetOrgContainer();
			if (!orgContainer.IsAddressListPagingEnabled)
			{
				return false;
			}
			bool flag;
			if (!this.CheckDomainReadScopeRoot(sessionSettings) || !this.CheckDomainReadScopeFilter(sessionSettings, out flag))
			{
				return false;
			}
			switch (addressListType)
			{
			case AddressListType.AllRecipients:
				if (flag)
				{
					this.addressList = base.CurrentOrgContainerId.GetDescendantId(SystemAddressList.RdnSystemAddressListContainerToOrganization).GetChildId("All Recipients(VLV)");
				}
				else
				{
					this.addressList = this.defaultGlobalAddressList;
				}
				break;
			case AddressListType.AllMailboxes:
				if (flag)
				{
					this.addressList = base.CurrentOrgContainerId.GetDescendantId(SystemAddressList.RdnSystemAddressListContainerToOrganization).GetChildId("All Mailboxes(VLV)");
				}
				else
				{
					this.addressList = base.CurrentOrgContainerId.GetDescendantId(SystemAddressList.RdnSystemAddressListContainerToOrganization).GetChildId("Mailboxes(VLV)");
				}
				break;
			case AddressListType.AllGroups:
				if (flag)
				{
					this.addressList = base.CurrentOrgContainerId.GetDescendantId(SystemAddressList.RdnSystemAddressListContainerToOrganization).GetChildId("All Groups(VLV)");
				}
				else
				{
					this.addressList = base.CurrentOrgContainerId.GetDescendantId(SystemAddressList.RdnSystemAddressListContainerToOrganization).GetChildId("Groups(VLV)");
				}
				break;
			}
			return true;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0002EA78 File Offset: 0x0002CC78
		private bool CheckDomainReadScopeRoot(ADSessionSettings settings)
		{
			return settings == null || settings.RecipientReadScope == null || (settings.RecipientReadScope.Root == null || (base.CurrentOrganizationId != OrganizationId.ForestWideOrgId && base.CurrentOrganizationId.OrganizationalUnit.IsDescendantOf(settings.RecipientReadScope.Root)));
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x0002EAD4 File Offset: 0x0002CCD4
		private bool CheckDomainReadScopeFilter(ADSessionSettings settings, out bool isRecipientsHiddenFromALIncluded)
		{
			isRecipientsHiddenFromALIncluded = true;
			if (settings == null || settings.RecipientReadScope == null || settings.RecipientReadScope.Filter == null)
			{
				return true;
			}
			AddressBookBase[] array = this.ConfigurationSession.Find<AddressBookBase>(GlobalAddressListIdParameter.GetRootContainerId(this.ConfigurationSession), QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, AddressBookBaseSchema.IsDefaultGlobalAddressList, true), null, 1);
			if (array == null || array.Length == 0)
			{
				return false;
			}
			this.defaultGlobalAddressList = array[0].Id;
			if (settings.RecipientReadScope.Filter is OrFilter)
			{
				foreach (QueryFilter queryFilter in ((OrFilter)settings.RecipientReadScope.Filter).Filters)
				{
					if (queryFilter is ComparisonFilter)
					{
						ComparisonFilter comparisonFilter = (ComparisonFilter)queryFilter;
						if (comparisonFilter.ComparisonOperator == ComparisonOperator.Equal && comparisonFilter.Property == ADRecipientSchema.AddressListMembership && ADObjectId.Equals((ADObjectId)comparisonFilter.PropertyValue, this.defaultGlobalAddressList))
						{
							isRecipientsHiddenFromALIncluded = false;
							return true;
						}
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0002EBEC File Offset: 0x0002CDEC
		private QueryFilter GetOPathFilter(string filterStr)
		{
			MonadFilter monadFilter = null;
			QueryFilter result = null;
			try
			{
				base.WriteVerbose(Strings.VerboseTryingToParseOPathFilter(filterStr));
				monadFilter = new MonadFilter(filterStr, this, ObjectSchema.GetInstance<ADRecipientProperties>());
				base.WriteVerbose(Strings.VerboseParsingOPathFilterSucceed(filterStr));
			}
			catch (InvalidCastException exception)
			{
				base.ThrowTerminatingError(exception, ErrorCategory.InvalidArgument, null);
			}
			catch (ParsingException ex)
			{
				base.WriteVerbose(Strings.VerboseParsingOPathFilterFailed(filterStr, ex.Message));
			}
			if (monadFilter != null && monadFilter.InnerFilter != null)
			{
				result = monadFilter.InnerFilter;
			}
			return result;
		}

		// Token: 0x0400024B RID: 587
		private const int LdapFilterSearchTimeoutDefaultSeconds = 60;

		// Token: 0x0400024C RID: 588
		private const string ParamBookmarkDisplayName = "BookmarkDisplayName";

		// Token: 0x0400024D RID: 589
		private const string ParamIncludeBookmarkObject = "IncludeBookmarkObject";

		// Token: 0x0400024E RID: 590
		private const string ParamAuthenticationType = "AuthenticationType";

		// Token: 0x0400024F RID: 591
		private const string ParamCapabilities = "Capabilities";

		// Token: 0x04000250 RID: 592
		private const string ParamProperties = "Properties";

		// Token: 0x04000251 RID: 593
		private const string ParamPropertySet = "PropertySet";

		// Token: 0x04000252 RID: 594
		private const string ParamDatabase = "Database";

		// Token: 0x04000253 RID: 595
		private const string ParamSetDatabaseSet = "DatabaseSet";

		// Token: 0x04000254 RID: 596
		private static readonly IDictionary<string, PropertyDefinition> OutputPropertiesToDefinitionDict;

		// Token: 0x04000255 RID: 597
		private static object syncRoot = new object();

		// Token: 0x04000256 RID: 598
		private static readonly string[] Parameters = new string[]
		{
			"BookmarkDisplayName",
			"IncludeBookmarkObject"
		};

		// Token: 0x04000257 RID: 599
		private static readonly RecipientTypeDetails[] AllowedRecipientTypeDetails = CannedSystemAddressLists.RecipientTypeDetailsForAllRecipientsAL;

		// Token: 0x04000258 RID: 600
		private static readonly RecipientTypeDetails[] AllowedRecipientTypeDetailsForColloborationMailbox = CannedSystemAddressLists.RecipientTypeDetailsForAllPublicFolderMailboxesAL.Union(CannedSystemAddressLists.RecipientTypeDetailsForGroupMailboxesAL).ToArray<RecipientTypeDetails>();

		// Token: 0x04000259 RID: 601
		private static readonly PropertyDefinition[] SortPropertiesArray = new PropertyDefinition[]
		{
			ReducedRecipientSchema.Alias,
			ReducedRecipientSchema.City,
			ReducedRecipientSchema.DisplayName,
			ReducedRecipientSchema.FirstName,
			ReducedRecipientSchema.LastName,
			ADObjectSchema.Name,
			ReducedRecipientSchema.Office,
			ReducedRecipientSchema.ServerLegacyDN
		};

		// Token: 0x0400025A RID: 602
		private static readonly PropertyDefinition[] MandatoryOutputProperties = new PropertyDefinition[]
		{
			ADObjectSchema.Id,
			ADObjectSchema.OrganizationId,
			ReducedRecipientSchema.RecipientType,
			ReducedRecipientSchema.RecipientTypeDetails,
			ADObjectSchema.ExchangeVersion
		};

		// Token: 0x0400025B RID: 603
		private static readonly PropertyDefinition[] SpecialPropertiesLeadToAllRead = new PropertyDefinition[]
		{
			ReducedRecipientSchema.Capabilities
		};

		// Token: 0x0400025C RID: 604
		private static readonly IDictionary<PropertyDefinition, IList<PropertyDefinition>> PropertyRelationship = new Dictionary<PropertyDefinition, IList<PropertyDefinition>>
		{
			{
				ReducedRecipientSchema.AuthenticationType,
				new PropertyDefinition[]
				{
					ReducedRecipientSchema.WindowsLiveID
				}
			}
		};

		// Token: 0x0400025D RID: 605
		private IList<PropertyDefinition> propertiesToRead;

		// Token: 0x0400025E RID: 606
		private bool usingALbasedVlv;

		// Token: 0x0400025F RID: 607
		private ADObjectId addressList;

		// Token: 0x04000260 RID: 608
		private ADObjectId defaultGlobalAddressList;

		// Token: 0x04000261 RID: 609
		private QueryFilter recipientPreviewFilter;

		// Token: 0x04000262 RID: 610
		private MailboxDatabase database;

		// Token: 0x04000263 RID: 611
		private readonly Dictionary<OrganizationId, ADObjectId> owaMailboxPolicyCache = new Dictionary<OrganizationId, ADObjectId>(8);
	}
}
