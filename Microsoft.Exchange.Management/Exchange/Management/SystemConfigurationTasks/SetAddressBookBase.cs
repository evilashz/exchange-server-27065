using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007DF RID: 2015
	public class SetAddressBookBase<TIdParameter, TRepresentationObject> : SetSystemConfigurationObjectTask<TIdParameter, TRepresentationObject, AddressBookBase> where TIdParameter : ADIdParameter, new() where TRepresentationObject : AddressListBase, new()
	{
		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x06004695 RID: 18069 RVA: 0x001216F5 File Offset: 0x0011F8F5
		// (set) Token: 0x06004696 RID: 18070 RVA: 0x0012170C File Offset: 0x0011F90C
		[Parameter]
		public string RecipientFilter
		{
			get
			{
				return (string)base.Fields[AddressListBaseSchema.RecipientFilter];
			}
			set
			{
				base.Fields[AddressListBaseSchema.RecipientFilter] = (value ?? string.Empty);
				MonadFilter monadFilter = new MonadFilter(value ?? string.Empty, this, ObjectSchema.GetInstance<ADRecipientProperties>());
				this.innerFilter = monadFilter.InnerFilter;
			}
		}

		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x06004697 RID: 18071 RVA: 0x00121755 File Offset: 0x0011F955
		// (set) Token: 0x06004698 RID: 18072 RVA: 0x0012176C File Offset: 0x0011F96C
		[Parameter]
		public OrganizationalUnitIdParameter RecipientContainer
		{
			get
			{
				return (OrganizationalUnitIdParameter)base.Fields["RecipientContainer"];
			}
			set
			{
				base.Fields["RecipientContainer"] = value;
			}
		}

		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x06004699 RID: 18073 RVA: 0x0012177F File Offset: 0x0011F97F
		// (set) Token: 0x0600469A RID: 18074 RVA: 0x001217A5 File Offset: 0x0011F9A5
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

		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x0600469B RID: 18075 RVA: 0x001217BD File Offset: 0x0011F9BD
		protected override bool ExchangeVersionUpgradeSupported
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600469C RID: 18076 RVA: 0x001217C0 File Offset: 0x0011F9C0
		protected override bool ShouldUpgradeExchangeVersion(ADObject adObject)
		{
			return base.Fields.IsModified(AddressListBaseSchema.RecipientFilter) || RecipientFilterHelper.IsRecipientFilterPropertiesModified(adObject, false);
		}

		// Token: 0x0600469D RID: 18077 RVA: 0x001217E0 File Offset: 0x0011F9E0
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			AddressListBase adObject = (TRepresentationObject)((object)this.GetDynamicParameters());
			if (base.Fields.IsModified(AddressListBaseSchema.RecipientFilter) && RecipientFilterHelper.IsRecipientFilterPropertiesModified(adObject, false))
			{
				base.ThrowTerminatingError(new ArgumentException(Strings.ErrorBothCustomAndPrecannedFilterSpecified, null), ErrorCategory.InvalidArgument, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0600469E RID: 18078 RVA: 0x00121844 File Offset: 0x0011FA44
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			AddressBookBase addressBookBase = (AddressBookBase)dataObject;
			this.originalVersion = addressBookBase.ExchangeVersion;
			base.StampChangesOn(dataObject);
			if (base.Fields.IsModified(AddressBookBaseSchema.RecipientFilter))
			{
				addressBookBase.SetRecipientFilter(this.innerFilter);
			}
		}

		// Token: 0x0600469F RID: 18079 RVA: 0x0012188C File Offset: 0x0011FA8C
		protected void ValidateBrokenRecipientFilterChange(QueryFilter expectedRecipientFilter)
		{
			if (!this.DataObject.IsModified(AddressBookBaseSchema.RecipientFilter) || this.originalVersion.IsOlderThan(AddressBookBaseSchema.RecipientFilter.VersionAdded))
			{
				if (this.DataObject.IsChanged(AddressBookBaseSchema.LdapRecipientFilter))
				{
					string b = (expectedRecipientFilter == null) ? string.Empty : LdapFilterBuilder.LdapFilterFromQueryFilter(expectedRecipientFilter);
					if (!string.Equals(this.DataObject.LdapRecipientFilter, b, StringComparison.OrdinalIgnoreCase))
					{
						string expected = (expectedRecipientFilter == null) ? string.Empty : expectedRecipientFilter.GenerateInfixString(FilterLanguage.Monad);
						if (this.DataObject.IsTopContainer)
						{
							TIdParameter identity = this.Identity;
							base.WriteError(new InvalidOperationException(Strings.ErrorInvalidFilterForAddressBook(identity.ToString(), this.DataObject.RecipientFilter, expected)), ErrorCategory.InvalidOperation, this.DataObject.Identity);
							return;
						}
						TIdParameter identity2 = this.Identity;
						base.WriteError(new InvalidOperationException(Strings.ErrorInvalidFilterForDefaultGlobalAddressList(identity2.ToString(), this.DataObject.RecipientFilter, GlobalAddressList.RecipientFilterForDefaultGal.GenerateInfixString(FilterLanguage.Monad))), ErrorCategory.InvalidOperation, this.DataObject.Identity);
					}
				}
				return;
			}
			if (this.DataObject.IsTopContainer)
			{
				TIdParameter identity3 = this.Identity;
				base.WriteError(new InvalidOperationException(Strings.ErrorInvalidOperationOnContainer(identity3.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
				return;
			}
			TIdParameter identity4 = this.Identity;
			base.WriteError(new InvalidOperationException(Strings.ErrorInvalidOperationOnDefaultGAL(identity4.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
		}

		// Token: 0x060046A0 RID: 18080 RVA: 0x00121A2C File Offset: 0x0011FC2C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			OrganizationalUnitIdParameter organizationalUnitIdParameter = null;
			if (base.Fields.IsModified("RecipientContainer"))
			{
				if (this.RecipientContainer == null)
				{
					this.DataObject.RecipientContainer = null;
				}
				else
				{
					organizationalUnitIdParameter = this.RecipientContainer;
				}
			}
			else if (this.DataObject.IsModified(AddressBookBaseSchema.RecipientContainer) && this.DataObject.RecipientContainer != null)
			{
				organizationalUnitIdParameter = new OrganizationalUnitIdParameter(this.DataObject.RecipientContainer);
			}
			if (organizationalUnitIdParameter != null)
			{
				if (base.GlobalConfigSession.IsInPreE14InteropMode())
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorCannotSetRecipientContainer), ErrorCategory.InvalidArgument, this.DataObject.Identity);
				}
				OrganizationId organizationId = this.DataObject.OrganizationId;
				this.DataObject.RecipientContainer = NewAddressBookBase.GetRecipientContainer(organizationalUnitIdParameter, this.ConfigurationSession, organizationId, new NewAddressBookBase.GetUniqueObject(base.GetDataObject<ExchangeOrganizationalUnit>), new Task.ErrorLoggerDelegate(base.WriteError), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
			if (this.IsObjectStateChanged() && this.DataObject.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2007))
			{
				TIdParameter identity = this.Identity;
				base.WriteError(new InvalidOperationException(Strings.ErrorObjectNotManagableFromCurrentConsole(identity.ToString(), this.DataObject.ExchangeVersion.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
			}
			if (!base.HasErrors)
			{
				if (this.DataObject.IsTopContainer)
				{
					if (this.DataObject.IsModified(ADObjectSchema.Name) || this.DataObject.IsModified(AddressBookBaseSchema.DisplayName))
					{
						TIdParameter identity2 = this.Identity;
						base.WriteError(new InvalidOperationException(Strings.ErrorInvalidOperationOnContainer(identity2.ToString())), ErrorCategory.InvalidOperation, this.DataObject.Identity);
					}
					this.ValidateBrokenRecipientFilterChange(null);
				}
				if (!base.HasErrors && (this.DataObject.IsChanged(AddressBookBaseSchema.RecipientFilter) || this.DataObject.IsChanged(AddressBookBaseSchema.RecipientContainer)))
				{
					this.DataObject[AddressBookBaseSchema.RecipientFilterApplied] = false;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060046A1 RID: 18081 RVA: 0x00121C44 File Offset: 0x0011FE44
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (RecipientFilterHelper.FixExchange12RecipientFilterMetadata(this.DataObject, ADObjectSchema.ExchangeVersion, AddressBookBaseSchema.PurportedSearchUI, AddressBookBaseSchema.RecipientFilterMetadata, this.DataObject.LdapRecipientFilter))
			{
				TIdParameter identity = this.Identity;
				base.WriteVerbose(Strings.WarningFixTheInvalidRecipientFilterMetadata(identity.ToString()));
			}
			if (!base.IsUpgrading || this.ForceUpgrade || base.ShouldContinue(Strings.ContinueUpgradeObjectVersion(this.DataObject.Name)))
			{
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04002B04 RID: 11012
		private QueryFilter innerFilter;

		// Token: 0x04002B05 RID: 11013
		private ExchangeObjectVersion originalVersion;
	}
}
