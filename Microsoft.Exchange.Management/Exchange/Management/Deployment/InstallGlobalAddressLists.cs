using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001F2 RID: 498
	[Cmdlet("Install", "GlobalAddressLists")]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class InstallGlobalAddressLists : InstallAddressBookContainer
	{
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x0004A9D5 File Offset: 0x00048BD5
		protected override ADObjectId RdnContainerToOrganization
		{
			get
			{
				return GlobalAddressList.RdnGalContainerToOrganization;
			}
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0004A9DC File Offset: 0x00048BDC
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			ADObjectId id = this.DataObject.Id;
			ADObjectId childId = id.GetChildId(InstallGlobalAddressLists.NameOfDefaultGlobalAddressList);
			AddressBookBase[] array = ((IConfigurationSession)base.DataSession).Find<AddressBookBase>(id, QueryScope.SubTree, new OrFilter(new QueryFilter[]
			{
				new ComparisonFilter(ComparisonOperator.Equal, AddressBookBaseSchema.IsDefaultGlobalAddressList, true),
				new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Id, childId)
			}), null, 1);
			if (array == null || array.Length == 0)
			{
				this.CreateDefaultGal(childId);
				this.PostExchange(childId);
			}
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x0004AA64 File Offset: 0x00048C64
		private void CreateDefaultGal(ADObjectId defaultGal)
		{
			AddressBookBase addressBookBase = new AddressBookBase();
			addressBookBase.SetId(defaultGal);
			addressBookBase.DisplayName = defaultGal.Name;
			addressBookBase.SetRecipientFilter(GlobalAddressList.RecipientFilterForDefaultGal);
			addressBookBase[AddressBookBaseSchema.RecipientFilterFlags] = (RecipientFilterableObjectFlags.FilterApplied | RecipientFilterableObjectFlags.IsDefault);
			RecipientFilterHelper.StampE2003FilterMetadata(addressBookBase, addressBookBase.LdapRecipientFilter, AddressBookBaseSchema.PurportedSearchUI);
			addressBookBase.OrganizationId = (base.CurrentOrganizationId ?? OrganizationId.ForestWideOrgId);
			base.WriteVerbose(TaskVerboseStringHelper.GetSaveObjectVerboseString(addressBookBase, base.DataSession, typeof(AddressBookBase)));
			try
			{
				base.DataSession.Save(addressBookBase);
			}
			finally
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
			}
			base.WriteObject(addressBookBase);
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x0004AB20 File Offset: 0x00048D20
		private void PostExchange(ADObjectId defaultGal)
		{
			IConfigurationSession configurationSession = base.CreateGlobalWritableConfigSession();
			bool skipRangedAttributes = configurationSession.SkipRangedAttributes;
			configurationSession.SkipRangedAttributes = true;
			try
			{
				ExchangeConfigurationContainerWithAddressLists exchangeConfigurationContainerWithAddressLists = configurationSession.GetExchangeConfigurationContainerWithAddressLists();
				if (exchangeConfigurationContainerWithAddressLists.LinkedAddressBookRootAttributesPresent())
				{
					exchangeConfigurationContainerWithAddressLists.DefaultGlobalAddressList2.Add(defaultGal);
				}
				base.WriteVerbose(TaskVerboseStringHelper.GetSaveObjectVerboseString(exchangeConfigurationContainerWithAddressLists, configurationSession, typeof(ExchangeConfigurationContainer)));
				configurationSession.Save(exchangeConfigurationContainerWithAddressLists);
				exchangeConfigurationContainerWithAddressLists.ResetChangeTracking();
				if (!AddressBookUtilities.IsTenantAddressList(configurationSession, defaultGal))
				{
					try
					{
						exchangeConfigurationContainerWithAddressLists.DefaultGlobalAddressList.Add(defaultGal);
						base.WriteVerbose(TaskVerboseStringHelper.GetSaveObjectVerboseString(exchangeConfigurationContainerWithAddressLists, configurationSession, typeof(ExchangeConfigurationContainer)));
						configurationSession.Save(exchangeConfigurationContainerWithAddressLists);
					}
					catch (AdminLimitExceededException innerException)
					{
						throw new ADOperationException(Strings.ErrorTooManyGALsCreated, innerException);
					}
				}
			}
			finally
			{
				configurationSession.SkipRangedAttributes = skipRangedAttributes;
				base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(configurationSession));
			}
		}

		// Token: 0x0400078C RID: 1932
		internal static string NameOfDefaultGlobalAddressList = Strings.DefaultGlobalAddressList;
	}
}
