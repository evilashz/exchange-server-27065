using System;
using System.Globalization;
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
	// Token: 0x02000029 RID: 41
	[Cmdlet("New", "DynamicDistributionGroup", SupportsShouldProcess = true, DefaultParameterSetName = "PrecannedFilter")]
	public sealed class NewDynamicDistributionGroup : NewMailEnabledRecipientObjectTask<ADDynamicGroup>
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00009FB4 File Offset: 0x000081B4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if ("CustomFilter" == base.ParameterSetName)
				{
					return Strings.ConfirmationMessageNewDynamicDistributionGroupCustomFilter(base.Name.ToString(), this.RecipientFilter.ToString(), base.RecipientContainerId.ToString());
				}
				return Strings.ConfirmationMessageNewDynamicDistributionGroupPrecannedFilter(base.Name.ToString(), this.IncludedRecipients.ToString(), base.RecipientContainerId.ToString());
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000A029 File Offset: 0x00008229
		// (set) Token: 0x060001D8 RID: 472 RVA: 0x0000A040 File Offset: 0x00008240
		[Parameter(Mandatory = true, ParameterSetName = "CustomFilter")]
		public string RecipientFilter
		{
			get
			{
				return (string)base.Fields[DynamicDistributionGroupSchema.RecipientFilter];
			}
			set
			{
				base.Fields[DynamicDistributionGroupSchema.RecipientFilter] = (value ?? string.Empty);
				MonadFilter monadFilter = new MonadFilter(value ?? string.Empty, this, ObjectSchema.GetInstance<ADRecipientProperties>());
				this.innerFilter = monadFilter.InnerFilter;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000A089 File Offset: 0x00008289
		// (set) Token: 0x060001DA RID: 474 RVA: 0x0000A0A0 File Offset: 0x000082A0
		[Parameter]
		public OrganizationalUnitIdParameter RecipientContainer
		{
			get
			{
				return (OrganizationalUnitIdParameter)base.Fields[ADDynamicGroupSchema.RecipientContainer];
			}
			set
			{
				base.Fields[ADDynamicGroupSchema.RecipientContainer] = value;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000A0B3 File Offset: 0x000082B3
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000A0C0 File Offset: 0x000082C0
		[Parameter(Mandatory = true, ParameterSetName = "PrecannedFilter")]
		public WellKnownRecipientType? IncludedRecipients
		{
			get
			{
				return this.DataObject.IncludedRecipients;
			}
			set
			{
				this.DataObject.IncludedRecipients = value;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000A0CE File Offset: 0x000082CE
		// (set) Token: 0x060001DE RID: 478 RVA: 0x0000A0DB File Offset: 0x000082DB
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
		public MultiValuedProperty<string> ConditionalDepartment
		{
			get
			{
				return this.DataObject.ConditionalDepartment;
			}
			set
			{
				this.DataObject.ConditionalDepartment = value;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001DF RID: 479 RVA: 0x0000A0E9 File Offset: 0x000082E9
		// (set) Token: 0x060001E0 RID: 480 RVA: 0x0000A0F6 File Offset: 0x000082F6
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCompany
		{
			get
			{
				return this.DataObject.ConditionalCompany;
			}
			set
			{
				this.DataObject.ConditionalCompany = value;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060001E1 RID: 481 RVA: 0x0000A104 File Offset: 0x00008304
		// (set) Token: 0x060001E2 RID: 482 RVA: 0x0000A111 File Offset: 0x00008311
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
		public MultiValuedProperty<string> ConditionalStateOrProvince
		{
			get
			{
				return this.DataObject.ConditionalStateOrProvince;
			}
			set
			{
				this.DataObject.ConditionalStateOrProvince = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060001E3 RID: 483 RVA: 0x0000A11F File Offset: 0x0000831F
		// (set) Token: 0x060001E4 RID: 484 RVA: 0x0000A12C File Offset: 0x0000832C
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute1
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute1;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute1 = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x0000A13A File Offset: 0x0000833A
		// (set) Token: 0x060001E6 RID: 486 RVA: 0x0000A147 File Offset: 0x00008347
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute2
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute2;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute2 = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x0000A155 File Offset: 0x00008355
		// (set) Token: 0x060001E8 RID: 488 RVA: 0x0000A162 File Offset: 0x00008362
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute3
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute3;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute3 = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x0000A170 File Offset: 0x00008370
		// (set) Token: 0x060001EA RID: 490 RVA: 0x0000A17D File Offset: 0x0000837D
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute4
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute4;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute4 = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001EB RID: 491 RVA: 0x0000A18B File Offset: 0x0000838B
		// (set) Token: 0x060001EC RID: 492 RVA: 0x0000A198 File Offset: 0x00008398
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute5
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute5;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute5 = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001ED RID: 493 RVA: 0x0000A1A6 File Offset: 0x000083A6
		// (set) Token: 0x060001EE RID: 494 RVA: 0x0000A1B3 File Offset: 0x000083B3
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute6
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute6;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute6 = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001EF RID: 495 RVA: 0x0000A1C1 File Offset: 0x000083C1
		// (set) Token: 0x060001F0 RID: 496 RVA: 0x0000A1CE File Offset: 0x000083CE
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute7
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute7;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute7 = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000A1DC File Offset: 0x000083DC
		// (set) Token: 0x060001F2 RID: 498 RVA: 0x0000A1E9 File Offset: 0x000083E9
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute8
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute8;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute8 = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000A1F7 File Offset: 0x000083F7
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000A204 File Offset: 0x00008404
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute9
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute9;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute9 = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000A212 File Offset: 0x00008412
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000A21F File Offset: 0x0000841F
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute10
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute10;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute10 = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x0000A22D File Offset: 0x0000842D
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x0000A23A File Offset: 0x0000843A
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute11
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute11;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute11 = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0000A248 File Offset: 0x00008448
		// (set) Token: 0x060001FA RID: 506 RVA: 0x0000A255 File Offset: 0x00008455
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute12
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute12;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute12 = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0000A263 File Offset: 0x00008463
		// (set) Token: 0x060001FC RID: 508 RVA: 0x0000A270 File Offset: 0x00008470
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute13
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute13;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute13 = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000A27E File Offset: 0x0000847E
		// (set) Token: 0x060001FE RID: 510 RVA: 0x0000A28B File Offset: 0x0000848B
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute14
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute14;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute14 = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000A299 File Offset: 0x00008499
		// (set) Token: 0x06000200 RID: 512 RVA: 0x0000A2A6 File Offset: 0x000084A6
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
		public MultiValuedProperty<string> ConditionalCustomAttribute15
		{
			get
			{
				return this.DataObject.ConditionalCustomAttribute15;
			}
			set
			{
				this.DataObject.ConditionalCustomAttribute15 = value;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000A2B4 File Offset: 0x000084B4
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			if (base.Fields.IsModified(DynamicDistributionGroupSchema.RecipientFilter))
			{
				LocalizedString? localizedString;
				if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.SupportOptimizedFilterOnlyInDDG.Enabled && DynamicDistributionGroupFilterValidation.ContainsNonOptimizedCondition(this.innerFilter, out localizedString))
				{
					base.ThrowTerminatingError(new TaskArgumentException(localizedString.Value, null), ExchangeErrorCategory.Client, null);
				}
				this.DataObject.SetRecipientFilter(this.innerFilter);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000A33C File Offset: 0x0000853C
		protected override void StampDefaultValues(ADDynamicGroup dataObject)
		{
			base.StampDefaultValues(dataObject);
			dataObject.StampDefaultValues(RecipientType.DynamicDistributionGroup);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000A350 File Offset: 0x00008550
		protected override void PrepareRecipientObject(ADDynamicGroup group)
		{
			TaskLogger.LogEnter();
			base.PrepareRecipientObject(group);
			if (base.OrganizationalUnit == null && group[ADRecipientSchema.DefaultDistributionListOU] != null)
			{
				ADObjectId adobjectId = (ADObjectId)group[ADRecipientSchema.DefaultDistributionListOU];
				RecipientTaskHelper.ResolveOrganizationalUnitInOrganization(new OrganizationalUnitIdParameter(adobjectId), this.ConfigurationSession, base.CurrentOrganizationId, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ExchangeOrganizationalUnit>), ExchangeErrorCategory.ServerOperation, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
				group.SetId(adobjectId.GetChildId(base.Name));
			}
			if (this.RecipientContainer == null)
			{
				if (!base.Fields.IsModified(ADDynamicGroupSchema.RecipientContainer))
				{
					group.RecipientContainer = group.Id.Parent;
				}
			}
			else
			{
				bool useConfigNC = this.ConfigurationSession.UseConfigNC;
				bool useGlobalCatalog = this.ConfigurationSession.UseGlobalCatalog;
				this.ConfigurationSession.UseConfigNC = false;
				if (string.IsNullOrEmpty(this.ConfigurationSession.DomainController))
				{
					this.ConfigurationSession.UseGlobalCatalog = true;
				}
				ExchangeOrganizationalUnit exchangeOrganizationalUnit = (ExchangeOrganizationalUnit)base.GetDataObject<ExchangeOrganizationalUnit>(this.RecipientContainer, this.ConfigurationSession, (base.CurrentOrganizationId != null) ? base.CurrentOrganizationId.OrganizationalUnit : null, null, new LocalizedString?(Strings.ErrorOrganizationalUnitNotFound(this.RecipientContainer.ToString())), new LocalizedString?(Strings.ErrorOrganizationalUnitNotUnique(this.RecipientContainer.ToString())), ExchangeErrorCategory.Client);
				RecipientTaskHelper.IsOrgnizationalUnitInOrganization(this.ConfigurationSession, group.OrganizationId, exchangeOrganizationalUnit, new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError));
				this.ConfigurationSession.UseConfigNC = useConfigNC;
				this.ConfigurationSession.UseGlobalCatalog = useGlobalCatalog;
				group.RecipientContainer = (ADObjectId)exchangeOrganizationalUnit.Identity;
			}
			group.RecipientDisplayType = new RecipientDisplayType?(RecipientDisplayType.DynamicDistributionGroup);
			TaskLogger.LogExit();
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000A520 File Offset: 0x00008720
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (string.IsNullOrEmpty(this.DataObject.LegacyExchangeDN))
			{
				AdministrativeGroup administrativeGroup = base.GlobalConfigSession.GetAdministrativeGroup();
				string parentLegacyDN = string.Format(CultureInfo.InvariantCulture, "{0}/cn=Recipients", new object[]
				{
					administrativeGroup.LegacyExchangeDN
				});
				this.DataObject.LegacyExchangeDN = LegacyDN.GenerateLegacyDN(parentLegacyDN, this.DataObject, true, new LegacyDN.LegacyDNIsUnique(this.LegacyDNIsUnique));
			}
			if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.EmailAddressPolicy.Enabled)
			{
				this.DataObject.EmailAddressPolicyEnabled = false;
			}
			DistributionGroupTaskHelper.CheckModerationInMixedEnvironment(this.DataObject, new Task.TaskWarningLoggingDelegate(this.WriteWarning), Strings.WarningLegacyExchangeServer);
			TaskLogger.LogExit();
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000A5EC File Offset: 0x000087EC
		protected override void WriteResult(ADObject result)
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Identity
			});
			DynamicDistributionGroup result2 = new DynamicDistributionGroup((ADDynamicGroup)result);
			base.WriteResult(result2);
			TaskLogger.LogExit();
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000206 RID: 518 RVA: 0x0000A62C File Offset: 0x0000882C
		protected override string ClonableTypeName
		{
			get
			{
				return typeof(DynamicDistributionGroup).FullName;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000A640 File Offset: 0x00008840
		private bool LegacyDNIsUnique(string legacyDN)
		{
			QueryFilter filter = new AndFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.LegacyExchangeDN, legacyDN),
				new ComparisonFilter(ComparisonOperator.NotEqual, ADObjectSchema.Id, this.DataObject.Id)
			});
			IRecipientSession recipientSession = RecipientTaskHelper.CreatePartitionOrRootOrgScopedGcSession(base.DomainController, this.DataObject.Id);
			base.WriteVerbose(TaskVerboseStringHelper.GetFindDataObjectsVerboseString(recipientSession, typeof(ADRecipient), filter, null, true));
			ADRecipient[] array = null;
			try
			{
				array = recipientSession.Find(null, QueryScope.SubTree, filter, null, 1);
			}
			finally
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(recipientSession));
			}
			return 0 == array.Length;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000A6F4 File Offset: 0x000088F4
		protected override IConfigurable ConvertDataObjectToPresentationObject(IConfigurable dataObject)
		{
			return DynamicDistributionGroup.FromDataObject((ADDynamicGroup)dataObject);
		}

		// Token: 0x04000048 RID: 72
		private QueryFilter innerFilter;
	}
}
