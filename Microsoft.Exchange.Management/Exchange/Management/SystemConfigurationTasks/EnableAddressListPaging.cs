using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AEA RID: 2794
	[Cmdlet("enable", "AddressListPaging", SupportsShouldProcess = true)]
	public sealed class EnableAddressListPaging : AddressListPagingTaskBase
	{
		// Token: 0x17001E15 RID: 7701
		// (get) Token: 0x06006330 RID: 25392 RVA: 0x0019E8D4 File Offset: 0x0019CAD4
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageEnableAddressListPaging((this.Identity != null) ? this.Identity.ToString() : "Current Oganization");
			}
		}

		// Token: 0x17001E16 RID: 7702
		// (get) Token: 0x06006331 RID: 25393 RVA: 0x0019E8F5 File Offset: 0x0019CAF5
		// (set) Token: 0x06006332 RID: 25394 RVA: 0x0019E91B File Offset: 0x0019CB1B
		[Parameter]
		public SwitchParameter DoNotUpdateRecipients
		{
			get
			{
				return (SwitchParameter)(base.Fields["DoNotUpdateRecipients"] ?? false);
			}
			set
			{
				base.Fields["DoNotUpdateRecipients"] = value;
			}
		}

		// Token: 0x17001E17 RID: 7703
		// (get) Token: 0x06006333 RID: 25395 RVA: 0x0019E933 File Offset: 0x0019CB33
		// (set) Token: 0x06006334 RID: 25396 RVA: 0x0019E959 File Offset: 0x0019CB59
		[Parameter]
		public SwitchParameter ForceUpdateOfRecipients
		{
			get
			{
				return (SwitchParameter)(base.Fields["ForceUpdateOfRecipients"] ?? false);
			}
			set
			{
				base.Fields["ForceUpdateOfRecipients"] = value;
			}
		}

		// Token: 0x06006335 RID: 25397 RVA: 0x0019E974 File Offset: 0x0019CB74
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.DoNotUpdateRecipients.IsPresent && this.ForceUpdateOfRecipients.IsPresent)
			{
				base.WriteError(new ArgumentException(Strings.CannotSpecifyBothDoNotUpdateAndForceUpdate), ErrorCategory.InvalidArgument, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06006336 RID: 25398 RVA: 0x0019E9C8 File Offset: 0x0019CBC8
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			IConfigurationSession configurationSession = (IConfigurationSession)base.DataSession;
			OrganizationId organizationId = (this.Identity != null) ? this.DataObject.OrganizationId : base.CurrentOrganizationId;
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, organizationId, base.ExecutingUserOrganizationId, false);
			configurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(configurationSession.DomainController, false, ConsistencyMode.PartiallyConsistent, configurationSession.NetworkCredential, sessionSettings, 102, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\organization\\EnableAddressListPaging.cs");
			ADObjectId descendantId;
			if (organizationId != OrganizationId.ForestWideOrgId)
			{
				descendantId = organizationId.ConfigurationUnit.GetDescendantId(SystemAddressList.RdnSystemAddressListContainerToOrganization);
			}
			else
			{
				descendantId = base.RootOrgContainerId.GetDescendantId(SystemAddressList.RdnSystemAddressListContainerToOrganization);
			}
			AddressBookBase addressBookBase = configurationSession.Read<AddressBookBase>(descendantId);
			if (null == addressBookBase)
			{
				addressBookBase = new AddressBookBase();
				addressBookBase.SetId(descendantId);
				addressBookBase.DisplayName = SystemAddressList.RdnSystemAddressListContainerToOrganization.Name;
				addressBookBase.OrganizationId = organizationId;
				configurationSession.Save(addressBookBase);
			}
			Dictionary<string, bool> dictionary;
			this.CreateCannedSystemAddressLists(configurationSession, addressBookBase, out dictionary);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.TenantGlobalCatalogSession.DomainController, true, ConsistencyMode.PartiallyConsistent, null, sessionSettings, 146, "InternalProcessRecord", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\SystemConfigurationTasks\\organization\\EnableAddressListPaging.cs");
			AddressBookBase[] array = configurationSession.Find<AddressBookBase>(descendantId, QueryScope.SubTree, null, null, 0);
			foreach (AddressBookBase addressBookBase2 in array)
			{
				if (base.Stopping)
				{
					return;
				}
				bool flag = false;
				dictionary.TryGetValue(addressBookBase2.Name, out flag);
				if (this.DoNotUpdateRecipients.IsPresent)
				{
					if (flag)
					{
						this.WriteWarning(Strings.WarningMustRunEnableAddressListPagingWithForceSwitch(addressBookBase2.Name));
					}
				}
				else if (this.ForceUpdateOfRecipients.IsPresent || flag)
				{
					UpdateAddressBookBase<AddressListIdParameter>.UpdateRecipients(addressBookBase2, base.DomainController, tenantOrRootOrgRecipientSession, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new WriteProgress(base.WriteProgress), this);
				}
			}
			Organization orgContainer = configurationSession.GetOrgContainer();
			if (!orgContainer.IsAddressListPagingEnabled)
			{
				orgContainer.IsAddressListPagingEnabled = true;
				base.WriteVerbose(TaskVerboseStringHelper.GetSaveObjectVerboseString(orgContainer, configurationSession, typeof(Organization)));
				configurationSession.Save(orgContainer);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06006337 RID: 25399 RVA: 0x0019EBE4 File Offset: 0x0019CDE4
		private void CreateCannedSystemAddressLists(IConfigurationSession session, AddressBookBase parent, out Dictionary<string, bool> addressListsCreated)
		{
			addressListsCreated = new Dictionary<string, bool>();
			foreach (string text in CannedSystemAddressLists.RecipientFilters.Keys)
			{
				AddressBookBase addressBookBase = session.Read<AddressBookBase>(parent.Id.GetChildId(text));
				bool flag = false;
				bool flag2 = false;
				if (addressBookBase == null)
				{
					flag = true;
					addressBookBase = new AddressBookBase();
					addressBookBase.SetId(parent.Id.GetChildId(text));
					addressBookBase.DisplayName = text;
					addressBookBase.OrganizationId = parent.OrganizationId;
					addressBookBase.IsSystemAddressList = CannedSystemAddressLists.SystemFlags[text];
				}
				if (flag || !string.Equals(LdapFilterBuilder.LdapFilterFromQueryFilter(CannedSystemAddressLists.RecipientFilters[text]), addressBookBase.LdapRecipientFilter, StringComparison.OrdinalIgnoreCase))
				{
					flag2 = true;
					addressBookBase.SetRecipientFilter(CannedSystemAddressLists.RecipientFilters[text]);
					addressBookBase[AddressBookBaseSchema.RecipientFilterFlags] = RecipientFilterableObjectFlags.FilterApplied;
					base.WriteVerbose(TaskVerboseStringHelper.GetSaveObjectVerboseString(addressBookBase, session, typeof(AddressBookBase)));
					try
					{
						session.Save(addressBookBase);
					}
					finally
					{
						base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(session));
					}
				}
				addressListsCreated.Add(text, flag || flag2);
			}
		}
	}
}
