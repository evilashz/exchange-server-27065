using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks.ManagementScopeExtensions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000686 RID: 1670
	[Cmdlet("New", "ManagementScope", SupportsShouldProcess = true, DefaultParameterSetName = "RecipientFilter")]
	public sealed class NewManagementScope : NewMultitenancySystemConfigurationObjectTask<ManagementScope>
	{
		// Token: 0x17001196 RID: 4502
		// (get) Token: 0x06003B14 RID: 15124 RVA: 0x000FB57C File Offset: 0x000F977C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewManagementScope(this.DataObject.ScopeRestrictionType.ToString(), this.DataObject.Filter, (this.DataObject.RecipientRoot == null) ? "<null>" : this.DataObject.RecipientRoot.ToString());
			}
		}

		// Token: 0x17001197 RID: 4503
		// (get) Token: 0x06003B15 RID: 15125 RVA: 0x000FB5D2 File Offset: 0x000F97D2
		// (set) Token: 0x06003B16 RID: 15126 RVA: 0x000FB5E9 File Offset: 0x000F97E9
		[Parameter(Mandatory = true, ParameterSetName = "RecipientFilter")]
		public string RecipientRestrictionFilter
		{
			get
			{
				return (string)base.Fields["RecipientRestrictionFilter"];
			}
			set
			{
				base.Fields["RecipientRestrictionFilter"] = value;
			}
		}

		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x06003B17 RID: 15127 RVA: 0x000FB5FC File Offset: 0x000F97FC
		// (set) Token: 0x06003B18 RID: 15128 RVA: 0x000FB613 File Offset: 0x000F9813
		[Parameter(Mandatory = false, ParameterSetName = "RecipientFilter")]
		public OrganizationalUnitIdParameter RecipientRoot
		{
			get
			{
				return (OrganizationalUnitIdParameter)base.Fields["RecipientRoot"];
			}
			set
			{
				base.Fields["RecipientRoot"] = value;
			}
		}

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x06003B19 RID: 15129 RVA: 0x000FB626 File Offset: 0x000F9826
		// (set) Token: 0x06003B1A RID: 15130 RVA: 0x000FB63D File Offset: 0x000F983D
		[Parameter(Mandatory = true, ParameterSetName = "ServerFilter")]
		public string ServerRestrictionFilter
		{
			get
			{
				return (string)base.Fields["ServerRestrictionFilter"];
			}
			set
			{
				base.Fields["ServerRestrictionFilter"] = value;
			}
		}

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x06003B1B RID: 15131 RVA: 0x000FB650 File Offset: 0x000F9850
		// (set) Token: 0x06003B1C RID: 15132 RVA: 0x000FB667 File Offset: 0x000F9867
		[Parameter(Mandatory = true, ParameterSetName = "ServerList")]
		public ServerIdParameter[] ServerList
		{
			get
			{
				return (ServerIdParameter[])base.Fields["ServerList"];
			}
			set
			{
				base.Fields["ServerList"] = value;
			}
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x06003B1D RID: 15133 RVA: 0x000FB67A File Offset: 0x000F987A
		// (set) Token: 0x06003B1E RID: 15134 RVA: 0x000FB691 File Offset: 0x000F9891
		[Parameter(Mandatory = true, ParameterSetName = "DatabaseList")]
		public DatabaseIdParameter[] DatabaseList
		{
			get
			{
				return (DatabaseIdParameter[])base.Fields["DatabaseList"];
			}
			set
			{
				base.Fields["DatabaseList"] = value;
			}
		}

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x06003B1F RID: 15135 RVA: 0x000FB6A4 File Offset: 0x000F98A4
		// (set) Token: 0x06003B20 RID: 15136 RVA: 0x000FB6BB File Offset: 0x000F98BB
		[Parameter(Mandatory = true, ParameterSetName = "DatabaseFilter")]
		public string DatabaseRestrictionFilter
		{
			get
			{
				return (string)base.Fields["DatabaseRestrictionFilter"];
			}
			set
			{
				base.Fields["DatabaseRestrictionFilter"] = value;
			}
		}

		// Token: 0x1700119D RID: 4509
		// (get) Token: 0x06003B21 RID: 15137 RVA: 0x000FB6CE File Offset: 0x000F98CE
		// (set) Token: 0x06003B22 RID: 15138 RVA: 0x000FB6E5 File Offset: 0x000F98E5
		[Parameter(Mandatory = true, ParameterSetName = "PartnerFilter")]
		public string PartnerDelegatedTenantRestrictionFilter
		{
			get
			{
				return (string)base.Fields["PartnerDelegatedTenantRestrictionFilter"];
			}
			set
			{
				base.Fields["PartnerDelegatedTenantRestrictionFilter"] = value;
			}
		}

		// Token: 0x1700119E RID: 4510
		// (get) Token: 0x06003B23 RID: 15139 RVA: 0x000FB6F8 File Offset: 0x000F98F8
		// (set) Token: 0x06003B24 RID: 15140 RVA: 0x000FB71E File Offset: 0x000F991E
		[Parameter(Mandatory = false, ParameterSetName = "DatabaseFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "RecipientFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "ServerFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "ServerList")]
		[Parameter(Mandatory = false, ParameterSetName = "DatabaseList")]
		public SwitchParameter Exclusive
		{
			get
			{
				return (SwitchParameter)(base.Fields["Exclusive"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Exclusive"] = value;
			}
		}

		// Token: 0x1700119F RID: 4511
		// (get) Token: 0x06003B25 RID: 15141 RVA: 0x000FB736 File Offset: 0x000F9936
		// (set) Token: 0x06003B26 RID: 15142 RVA: 0x000FB75C File Offset: 0x000F995C
		[Parameter(Mandatory = false, ParameterSetName = "RecipientFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "DatabaseList")]
		[Parameter(Mandatory = false, ParameterSetName = "ServerFilter")]
		[Parameter(Mandatory = false, ParameterSetName = "ServerList")]
		[Parameter(Mandatory = false, ParameterSetName = "DatabaseFilter")]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x000FB774 File Offset: 0x000F9974
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			this.DataObject = (ManagementScope)base.PrepareDataObject();
			if (this.ServerRestrictionFilter != null || this.ServerList != null)
			{
				this.DataObject.ScopeRestrictionType = ScopeRestrictionType.ServerScope;
				if (this.ServerList != null && this.ServerList.Length > 0)
				{
					this.DataObject.ServerFilter = this.BuildAndVerifyObjectListFilter<Server>(this.ServerList, "ServerList", new NewManagementScope.SingleParameterLocString(Strings.ServerNamesMustBeValid), new NewManagementScope.SingleParameterLocString(Strings.ServerNamesMustBeUnique), new NewManagementScope.SingleParameterLocString(Strings.ServerListMustBeValid));
				}
				else
				{
					this.ValidateAndSetFilterOnDataObject("ServerRestrictionFilter", this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
			}
			else if (this.PartnerDelegatedTenantRestrictionFilter != null)
			{
				this.DataObject.ScopeRestrictionType = ScopeRestrictionType.PartnerDelegatedTenantScope;
				this.ValidateAndSetFilterOnDataObject("PartnerDelegatedTenantRestrictionFilter", this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			else if (this.RecipientRestrictionFilter != null)
			{
				this.DataObject.ScopeRestrictionType = ScopeRestrictionType.RecipientScope;
				this.ValidateAndSetFilterOnDataObject("RecipientRestrictionFilter", this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			else if (this.DatabaseRestrictionFilter != null || this.DatabaseList != null)
			{
				this.DataObject.ScopeRestrictionType = ScopeRestrictionType.DatabaseScope;
				if (this.DatabaseList != null && this.DatabaseList.Length > 0)
				{
					this.DataObject.DatabaseFilter = this.BuildAndVerifyObjectListFilter<Database>(this.DatabaseList, "DatabaseList", new NewManagementScope.SingleParameterLocString(Strings.DatabaseNamesMustBeValid), new NewManagementScope.SingleParameterLocString(Strings.DatabaseNamesMustBeUnique), new NewManagementScope.SingleParameterLocString(Strings.DatabaseListMustBeValid));
				}
				else
				{
					this.ValidateAndSetFilterOnDataObject("DatabaseRestrictionFilter", this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
				this.DataObject.SetExchangeVersion(ManagementScopeSchema.ExchangeManagementScope2010_SPVersion);
			}
			if (this.Exclusive.ToBool())
			{
				this.DataObject.Exclusive = true;
			}
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			this.DataObject.RecipientRoot = null;
			if (this.RecipientRoot != null)
			{
				this.DataObject.RecipientRoot = this.GetADObjectIdFromOrganizationalUnitIdParameter(configurationSession, this.RecipientRoot);
			}
			if (!base.HasErrors)
			{
				ADObjectId rootContainerId = ManagementScopeIdParameter.GetRootContainerId(configurationSession);
				this.DataObject.SetId(rootContainerId.GetChildId(base.Name));
			}
			TaskLogger.LogExit();
			return this.DataObject;
		}

		// Token: 0x06003B28 RID: 15144 RVA: 0x000FB9C8 File Offset: 0x000F9BC8
		private ADObjectId GetADObjectIdFromOrganizationalUnitIdParameter(IConfigurationSession configurationSession, OrganizationalUnitIdParameter root)
		{
			bool useConfigNC = configurationSession.UseConfigNC;
			bool useGlobalCatalog = configurationSession.UseGlobalCatalog;
			ADObjectId id;
			try
			{
				configurationSession.UseConfigNC = false;
				configurationSession.UseGlobalCatalog = true;
				IConfigurable configurable = (ADConfigurationObject)base.GetDataObject<ExchangeOrganizationalUnit>(root, configurationSession, base.OrganizationId.OrganizationalUnit, new LocalizedString?(Strings.ErrorOrganizationalUnitNotFound(root.ToString())), new LocalizedString?(Strings.ErrorOrganizationalUnitNotUnique(root.ToString())));
				id = ((ADObject)configurable).Id;
			}
			finally
			{
				configurationSession.UseConfigNC = useConfigNC;
				configurationSession.UseGlobalCatalog = useGlobalCatalog;
			}
			return id;
		}

		// Token: 0x06003B29 RID: 15145 RVA: 0x000FBA58 File Offset: 0x000F9C58
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if ((this.ServerList != null || this.ServerRestrictionFilter != null || this.PartnerDelegatedTenantRestrictionFilter != null || this.DatabaseList != null || this.DatabaseRestrictionFilter != null) && base.CurrentOrganizationId != OrganizationId.ForestWideOrgId && base.CurrentOrganizationId.ConfigurationUnit != base.RootOrgContainerId)
			{
				base.WriteError(new ArgumentException(Strings.ServerDatabaseAndPartnerScopesAreOnlyAllowedInTopOrg(base.CurrentOrganizationId.ToString())), ErrorCategory.InvalidArgument, null);
			}
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			ManagementScope[] array = configurationSession.FindSimilarManagementScope(this.DataObject);
			if (array.Length > 0)
			{
				base.WriteError(new ArgumentException(Strings.SimilarScopeFound(array[0].Name)), ErrorCategory.InvalidArgument, null);
			}
			if (ScopeRestrictionType.DatabaseScope == this.DataObject.ScopeRestrictionType)
			{
				this.WriteWarning(Strings.WarningDatabaseScopeCreationApplicableOnlyInSP);
			}
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			TaskLogger.LogExit();
		}

		// Token: 0x06003B2A RID: 15146 RVA: 0x000FBB5C File Offset: 0x000F9D5C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			if (this.Exclusive && !this.Force && !base.ShouldContinue(Strings.ConfirmCreatingExclusiveScope(this.DataObject.Identity.ToString())))
			{
				TaskLogger.LogExit();
				return;
			}
			if (!this.Force && SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}

		// Token: 0x06003B2B RID: 15147 RVA: 0x000FBC10 File Offset: 0x000F9E10
		private string BuildAndVerifyObjectListFilter<TConfigObject>(ADIdParameter[] adParameterList, string parameterName, NewManagementScope.SingleParameterLocString misspelledObjectNamesString, NewManagementScope.SingleParameterLocString duplicateObjectNamesString, NewManagementScope.SingleParameterLocString parsingExceptionString) where TConfigObject : ADLegacyVersionableObject, new()
		{
			string[] array = new string[adParameterList.Length];
			for (int i = 0; i < adParameterList.Length; i++)
			{
				array[i] = adParameterList[i].ToString();
			}
			Result<TConfigObject>[] array2 = base.GlobalConfigSession.ReadMultipleLegacyObjects<TConfigObject>(array);
			ComparisonFilter[] array3 = new ComparisonFilter[array2.Length];
			StringBuilder stringBuilder = null;
			StringBuilder stringBuilder2 = null;
			List<TConfigObject> list = new List<TConfigObject>();
			for (int j = 0; j < array2.Length; j++)
			{
				TConfigObject data = array2[j].Data;
				ProviderError error = array2[j].Error;
				if (error == ProviderError.NotFound)
				{
					if (stringBuilder == null)
					{
						stringBuilder = new StringBuilder();
					}
					stringBuilder.Append("\r\n");
					stringBuilder.Append(array[j].ToString());
				}
				else if (error == null && data != null)
				{
					if (list.Contains(data))
					{
						if (stringBuilder2 == null)
						{
							stringBuilder2 = new StringBuilder();
						}
						stringBuilder2.Append("\r\n");
						stringBuilder2.Append(data.Name);
					}
					else
					{
						array3[j] = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.DistinguishedName, data.DistinguishedName);
						list.Add(data);
					}
				}
			}
			if (stringBuilder != null)
			{
				base.WriteError(new ArgumentException(misspelledObjectNamesString(stringBuilder.ToString()), parameterName), ErrorCategory.InvalidArgument, null);
			}
			if (stringBuilder2 != null)
			{
				base.WriteError(new ArgumentException(duplicateObjectNamesString(stringBuilder2.ToString()), parameterName), ErrorCategory.InvalidArgument, null);
			}
			string result = string.Empty;
			QueryFilter queryFilter;
			if (array3.Length == 1)
			{
				queryFilter = array3[0];
			}
			else
			{
				queryFilter = new OrFilter(array3);
			}
			try
			{
				result = queryFilter.GenerateInfixString(FilterLanguage.Monad);
			}
			catch (ParsingException ex)
			{
				base.WriteError(new ArgumentException(parsingExceptionString(ex.Message), parameterName, ex), ErrorCategory.InvalidArgument, null);
			}
			return result;
		}

		// Token: 0x02000687 RID: 1671
		// (Invoke) Token: 0x06003B2E RID: 15150
		private delegate LocalizedString SingleParameterLocString(string message);
	}
}
