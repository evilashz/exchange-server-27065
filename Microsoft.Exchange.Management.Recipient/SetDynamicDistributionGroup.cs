using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000030 RID: 48
	[Cmdlet("Set", "DynamicDistributionGroup", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetDynamicDistributionGroup : SetMailEnabledRecipientObjectTask<DynamicGroupIdParameter, DynamicDistributionGroup, ADDynamicGroup>
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000BC5A File Offset: 0x00009E5A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetDynamicDistributionGroup(this.Identity.ToString());
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000BC6C File Offset: 0x00009E6C
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000BC83 File Offset: 0x00009E83
		[Parameter]
		public string RecipientFilter
		{
			get
			{
				return (string)base.Fields[DynamicDistributionGroupSchema.RecipientFilter];
			}
			set
			{
				base.Fields[DynamicDistributionGroupSchema.RecipientFilter] = (value ?? string.Empty);
				this.innerFilter = this.ConvertToQueryFilter(value);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000BCAC File Offset: 0x00009EAC
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000BCC3 File Offset: 0x00009EC3
		[Parameter]
		public OrganizationalUnitIdParameter RecipientContainer
		{
			get
			{
				return (OrganizationalUnitIdParameter)base.Fields[DynamicDistributionGroupSchema.RecipientContainer];
			}
			set
			{
				base.Fields[DynamicDistributionGroupSchema.RecipientContainer] = value;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000BCD6 File Offset: 0x00009ED6
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000BCED File Offset: 0x00009EED
		[Parameter(Mandatory = false)]
		public string ExpansionServer
		{
			get
			{
				return (string)base.Fields["ExpansionServer"];
			}
			set
			{
				base.Fields["ExpansionServer"] = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000BD00 File Offset: 0x00009F00
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000BD17 File Offset: 0x00009F17
		[Parameter(Mandatory = false)]
		public GeneralRecipientIdParameter ManagedBy
		{
			get
			{
				return (GeneralRecipientIdParameter)base.Fields["ManagedBy"];
			}
			set
			{
				base.Fields["ManagedBy"] = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000BD2A File Offset: 0x00009F2A
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000BD50 File Offset: 0x00009F50
		[Parameter]
		public SwitchParameter ForceUpgrade
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForceUpgrade"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["ForceUpgrade"] = value;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000BD68 File Offset: 0x00009F68
		protected override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000BD6B File Offset: 0x00009F6B
		protected override bool ShouldUpgradeExchangeVersion(ADObject adObject)
		{
			return base.ShouldUpgradeExchangeVersion(adObject) || base.Fields.IsModified(DynamicDistributionGroupSchema.RecipientFilter) || RecipientFilterHelper.IsRecipientFilterPropertiesModified(adObject, false);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000BD94 File Offset: 0x00009F94
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (base.Fields.IsModified(DynamicDistributionGroupSchema.RecipientFilter))
			{
				DynamicDistributionGroup adObject = (DynamicDistributionGroup)this.GetDynamicParameters();
				if (RecipientFilterHelper.IsRecipientFilterPropertiesModified(adObject, false))
				{
					base.ThrowTerminatingError(new TaskArgumentException(Strings.ErrorBothCustomAndPrecannedFilterSpecified, null), ExchangeErrorCategory.Client, null);
				}
			}
			if (base.Fields.IsModified("ExpansionServer"))
			{
				if (string.IsNullOrEmpty(this.ExpansionServer))
				{
					this.ExpansionServer = string.Empty;
					this.homeMTA = null;
				}
				else
				{
					Server server = SetDynamicDistributionGroup.ResolveExpansionServer(this.ExpansionServer, base.GlobalConfigSession, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<Server>), new Task.ErrorLoggerDelegate(base.ThrowTerminatingError));
					base.ValidateExpansionServer(server, true);
					this.ExpansionServer = server.ExchangeLegacyDN;
					this.homeMTA = server.ResponsibleMTA;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000BE6C File Offset: 0x0000A06C
		protected override void ResolveLocalSecondaryIdentities()
		{
			base.ResolveLocalSecondaryIdentities();
			if (this.ManagedBy != null)
			{
				this.managedBy = (ADRecipient)base.GetDataObject<ADRecipient>(this.ManagedBy, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(this.ManagedBy.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(this.ManagedBy.ToString())), ExchangeErrorCategory.Client);
			}
			if (this.RecipientContainer != null)
			{
				this.recipientContainerId = this.ValidateRecipientContainer(this.RecipientContainer);
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			DistributionGroupTaskHelper.CheckModerationInMixedEnvironment(this.DataObject, new Task.TaskWarningLoggingDelegate(this.WriteWarning), Strings.WarningLegacyExchangeServer);
			if (base.Fields.IsModified(DynamicDistributionGroupSchema.RecipientFilter) && VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.SupportOptimizedFilterOnlyInDDG.Enabled)
			{
				QueryFilter oldFilter = this.ConvertToQueryFilter(this.originalFilter);
				LocalizedString? localizedString;
				if (!DynamicDistributionGroupFilterValidation.IsFullOptimizedOrImproved(oldFilter, this.innerFilter, out localizedString))
				{
					base.WriteError(new RecipientTaskException(localizedString.Value, null), ExchangeErrorCategory.Client, this.DataObject.Identity);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0000BFA8 File Offset: 0x0000A1A8
		internal static Server ResolveExpansionServer(string expansionServer, ITopologyConfigurationSession scSession, DataAccessHelper.CategorizedGetDataObjectDelegate getUniqueDataObjectDelegate, Task.ErrorLoggerDelegate errorHandler)
		{
			if (string.IsNullOrEmpty(expansionServer))
			{
				throw new ArgumentNullException("expansionServer");
			}
			if (scSession == null)
			{
				throw new ArgumentNullException("scSession");
			}
			if (getUniqueDataObjectDelegate == null)
			{
				throw new ArgumentNullException("getUniqueDataObjectDelegate");
			}
			if (errorHandler == null)
			{
				throw new ArgumentNullException("errorHandler");
			}
			ServerIdParameter id = null;
			try
			{
				id = ServerIdParameter.Parse(expansionServer);
			}
			catch (ArgumentException)
			{
				errorHandler(new TaskArgumentException(Strings.ErrorInvalidExpansionServer(expansionServer)), ExchangeErrorCategory.Client, null);
			}
			return (Server)getUniqueDataObjectDelegate(id, scSession, null, null, new LocalizedString?(Strings.ErrorServerNotFound(expansionServer)), new LocalizedString?(Strings.ErrorServerNotUnique(expansionServer)), ExchangeErrorCategory.Client);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000C054 File Offset: 0x0000A254
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			ADDynamicGroup addynamicGroup = (ADDynamicGroup)base.PrepareDataObject();
			this.originalFilter = addynamicGroup.RecipientFilter;
			if (base.Fields.IsModified(DynamicDistributionGroupSchema.RecipientContainer))
			{
				addynamicGroup.RecipientContainer = this.recipientContainerId;
			}
			else if (addynamicGroup.IsChanged(DynamicDistributionGroupSchema.RecipientContainer) && addynamicGroup.RecipientContainer != null)
			{
				addynamicGroup.RecipientContainer = this.ValidateRecipientContainer(new OrganizationalUnitIdParameter(addynamicGroup.RecipientContainer));
			}
			if (base.Fields.IsModified("ManagedBy"))
			{
				addynamicGroup.ManagedBy = ((this.managedBy == null) ? null : this.managedBy.Id);
			}
			if (addynamicGroup.ManagedBy != null)
			{
				if (this.managedBy == null)
				{
					this.managedBy = (ADRecipient)base.GetDataObject<ADRecipient>(new GeneralRecipientIdParameter(addynamicGroup.ManagedBy), base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorRecipientNotFound(addynamicGroup.ManagedBy.ToString())), new LocalizedString?(Strings.ErrorRecipientNotUnique(addynamicGroup.ManagedBy.ToString())), ExchangeErrorCategory.Client);
				}
				if (!addynamicGroup.OrganizationId.Equals(this.managedBy.OrganizationId))
				{
					base.WriteError(new RecipientTaskException(Strings.ErrorManagedByCrossTenant(this.managedBy.Identity.ToString())), ExchangeErrorCategory.Client, addynamicGroup.Identity);
				}
			}
			if (base.Fields.IsModified("ExpansionServer"))
			{
				addynamicGroup.ExpansionServer = this.ExpansionServer;
				addynamicGroup.HomeMTA = this.homeMTA;
			}
			else if (addynamicGroup.IsChanged(DistributionGroupBaseSchema.ExpansionServer))
			{
				if (!string.IsNullOrEmpty(addynamicGroup.ExpansionServer))
				{
					QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ServerSchema.ExchangeLegacyDN, addynamicGroup.ExpansionServer);
					base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(base.GlobalConfigSession, typeof(Server), filter, null, true));
					Server[] array = null;
					try
					{
						array = base.GlobalConfigSession.Find<Server>(null, QueryScope.SubTree, filter, null, 2);
					}
					finally
					{
						base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.GlobalConfigSession));
					}
					switch (array.Length)
					{
					case 0:
						base.WriteError(new ManagementObjectNotFoundException(Strings.ErrorServerNotFound(addynamicGroup.ExpansionServer)), ExchangeErrorCategory.Client, this.Identity);
						return null;
					case 1:
						base.ValidateExpansionServer(array[0], false);
						addynamicGroup.ExpansionServer = array[0].ExchangeLegacyDN;
						break;
					case 2:
						base.WriteError(new ManagementObjectAmbiguousException(Strings.ErrorServerNotUnique(addynamicGroup.ExpansionServer)), ExchangeErrorCategory.Client, this.Identity);
						return null;
					}
					addynamicGroup.HomeMTA = array[0].ResponsibleMTA;
				}
				else
				{
					addynamicGroup.HomeMTA = null;
				}
			}
			if (base.Fields.IsModified(DynamicDistributionGroupSchema.RecipientFilter))
			{
				addynamicGroup.SetRecipientFilter(this.innerFilter);
			}
			addynamicGroup.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.DynamicDistributionGroup);
			TaskLogger.LogExit();
			return addynamicGroup;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000C318 File Offset: 0x0000A518
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (RecipientFilterHelper.FixExchange12RecipientFilterMetadata(this.DataObject, ADObjectSchema.ExchangeVersion, ADDynamicGroupSchema.PurportedSearchUI, ADDynamicGroupSchema.RecipientFilterMetadata, string.Empty))
			{
				base.WriteVerbose(Strings.WarningFixTheInvalidRecipientFilterMetadata(this.Identity.ToString()));
			}
			if (!base.IsUpgrading || this.ForceUpgrade || base.ShouldContinue(Strings.ContinueUpgradeObjectVersion(this.DataObject.Name)))
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000C39C File Offset: 0x0000A59C
		private ADObjectId ValidateRecipientContainer(OrganizationalUnitIdParameter recipientContainer)
		{
			bool useConfigNC = this.ConfigurationSession.UseConfigNC;
			bool useGlobalCatalog = this.ConfigurationSession.UseGlobalCatalog;
			ADObjectId id;
			try
			{
				this.ConfigurationSession.UseConfigNC = false;
				if (string.IsNullOrEmpty(this.ConfigurationSession.DomainController))
				{
					this.ConfigurationSession.UseGlobalCatalog = true;
				}
				ADConfigurationObject adconfigurationObject = (ADConfigurationObject)base.GetDataObject<ExchangeOrganizationalUnit>(recipientContainer, this.ConfigurationSession, null, new LocalizedString?(Strings.ErrorRecipientContainerInvalidOrNotExists), new LocalizedString?(Strings.ErrorOrganizationalUnitNotUnique(recipientContainer.ToString())), ExchangeErrorCategory.Client);
				id = adconfigurationObject.Id;
			}
			finally
			{
				this.ConfigurationSession.UseConfigNC = useConfigNC;
				this.ConfigurationSession.UseGlobalCatalog = useGlobalCatalog;
			}
			return id;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000C454 File Offset: 0x0000A654
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return DynamicDistributionGroup.FromDataObject((ADDynamicGroup)dataObject);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000C464 File Offset: 0x0000A664
		private QueryFilter ConvertToQueryFilter(string filter)
		{
			MonadFilter monadFilter = new MonadFilter(filter ?? string.Empty, this, ObjectSchema.GetInstance<ADRecipientProperties>());
			return monadFilter.InnerFilter;
		}

		// Token: 0x0400004C RID: 76
		private ADObjectId homeMTA;

		// Token: 0x0400004D RID: 77
		private ADObjectId recipientContainerId;

		// Token: 0x0400004E RID: 78
		private QueryFilter innerFilter;

		// Token: 0x0400004F RID: 79
		private string originalFilter;

		// Token: 0x04000050 RID: 80
		private ADRecipient managedBy;
	}
}
