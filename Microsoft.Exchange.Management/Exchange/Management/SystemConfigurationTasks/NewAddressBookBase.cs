using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007D8 RID: 2008
	public abstract class NewAddressBookBase : NewMultitenancySystemConfigurationObjectTask<AddressBookBase>
	{
		// Token: 0x17001538 RID: 5432
		// (get) Token: 0x06004638 RID: 17976 RVA: 0x00120518 File Offset: 0x0011E718
		protected virtual int MaxAddressLists
		{
			get
			{
				if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).CmdletInfra.MaxAddressBookPolicies.Enabled)
				{
					return int.MaxValue;
				}
				int? maxAddressBookPolicies = this.ConfigurationSession.GetOrgContainer().MaxAddressBookPolicies;
				if (maxAddressBookPolicies == null)
				{
					return 250;
				}
				return maxAddressBookPolicies.GetValueOrDefault();
			}
		}

		// Token: 0x17001539 RID: 5433
		// (get) Token: 0x06004639 RID: 17977 RVA: 0x00120572 File Offset: 0x0011E772
		// (set) Token: 0x0600463A RID: 17978 RVA: 0x0012058C File Offset: 0x0011E78C
		[Parameter(Mandatory = false, ParameterSetName = "CustomFilter")]
		public string RecipientFilter
		{
			get
			{
				return (string)base.Fields["RecipientFilter"];
			}
			set
			{
				base.Fields["RecipientFilter"] = (value ?? string.Empty);
				MonadFilter monadFilter = new MonadFilter(value ?? string.Empty, this, ObjectSchema.GetInstance<ADRecipientProperties>());
				this.DataObject.SetRecipientFilter(monadFilter.InnerFilter);
			}
		}

		// Token: 0x1700153A RID: 5434
		// (get) Token: 0x0600463B RID: 17979 RVA: 0x001205DA File Offset: 0x0011E7DA
		// (set) Token: 0x0600463C RID: 17980 RVA: 0x001205E7 File Offset: 0x0011E7E7
		[Parameter(Mandatory = false, ParameterSetName = "PrecannedFilter")]
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

		// Token: 0x1700153B RID: 5435
		// (get) Token: 0x0600463D RID: 17981 RVA: 0x001205F5 File Offset: 0x0011E7F5
		// (set) Token: 0x0600463E RID: 17982 RVA: 0x00120602 File Offset: 0x0011E802
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

		// Token: 0x1700153C RID: 5436
		// (get) Token: 0x0600463F RID: 17983 RVA: 0x00120610 File Offset: 0x0011E810
		// (set) Token: 0x06004640 RID: 17984 RVA: 0x0012061D File Offset: 0x0011E81D
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

		// Token: 0x1700153D RID: 5437
		// (get) Token: 0x06004641 RID: 17985 RVA: 0x0012062B File Offset: 0x0011E82B
		// (set) Token: 0x06004642 RID: 17986 RVA: 0x00120638 File Offset: 0x0011E838
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

		// Token: 0x1700153E RID: 5438
		// (get) Token: 0x06004643 RID: 17987 RVA: 0x00120646 File Offset: 0x0011E846
		// (set) Token: 0x06004644 RID: 17988 RVA: 0x0012065D File Offset: 0x0011E85D
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

		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x06004645 RID: 17989 RVA: 0x00120670 File Offset: 0x0011E870
		// (set) Token: 0x06004646 RID: 17990 RVA: 0x0012067D File Offset: 0x0011E87D
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

		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x06004647 RID: 17991 RVA: 0x0012068B File Offset: 0x0011E88B
		// (set) Token: 0x06004648 RID: 17992 RVA: 0x00120698 File Offset: 0x0011E898
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

		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x06004649 RID: 17993 RVA: 0x001206A6 File Offset: 0x0011E8A6
		// (set) Token: 0x0600464A RID: 17994 RVA: 0x001206B3 File Offset: 0x0011E8B3
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

		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x0600464B RID: 17995 RVA: 0x001206C1 File Offset: 0x0011E8C1
		// (set) Token: 0x0600464C RID: 17996 RVA: 0x001206CE File Offset: 0x0011E8CE
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

		// Token: 0x17001543 RID: 5443
		// (get) Token: 0x0600464D RID: 17997 RVA: 0x001206DC File Offset: 0x0011E8DC
		// (set) Token: 0x0600464E RID: 17998 RVA: 0x001206E9 File Offset: 0x0011E8E9
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

		// Token: 0x17001544 RID: 5444
		// (get) Token: 0x0600464F RID: 17999 RVA: 0x001206F7 File Offset: 0x0011E8F7
		// (set) Token: 0x06004650 RID: 18000 RVA: 0x00120704 File Offset: 0x0011E904
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

		// Token: 0x17001545 RID: 5445
		// (get) Token: 0x06004651 RID: 18001 RVA: 0x00120712 File Offset: 0x0011E912
		// (set) Token: 0x06004652 RID: 18002 RVA: 0x0012071F File Offset: 0x0011E91F
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

		// Token: 0x17001546 RID: 5446
		// (get) Token: 0x06004653 RID: 18003 RVA: 0x0012072D File Offset: 0x0011E92D
		// (set) Token: 0x06004654 RID: 18004 RVA: 0x0012073A File Offset: 0x0011E93A
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

		// Token: 0x17001547 RID: 5447
		// (get) Token: 0x06004655 RID: 18005 RVA: 0x00120748 File Offset: 0x0011E948
		// (set) Token: 0x06004656 RID: 18006 RVA: 0x00120755 File Offset: 0x0011E955
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

		// Token: 0x17001548 RID: 5448
		// (get) Token: 0x06004657 RID: 18007 RVA: 0x00120763 File Offset: 0x0011E963
		// (set) Token: 0x06004658 RID: 18008 RVA: 0x00120770 File Offset: 0x0011E970
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

		// Token: 0x17001549 RID: 5449
		// (get) Token: 0x06004659 RID: 18009 RVA: 0x0012077E File Offset: 0x0011E97E
		// (set) Token: 0x0600465A RID: 18010 RVA: 0x0012078B File Offset: 0x0011E98B
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

		// Token: 0x1700154A RID: 5450
		// (get) Token: 0x0600465B RID: 18011 RVA: 0x00120799 File Offset: 0x0011E999
		// (set) Token: 0x0600465C RID: 18012 RVA: 0x001207A6 File Offset: 0x0011E9A6
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

		// Token: 0x1700154B RID: 5451
		// (get) Token: 0x0600465D RID: 18013 RVA: 0x001207B4 File Offset: 0x0011E9B4
		// (set) Token: 0x0600465E RID: 18014 RVA: 0x001207C1 File Offset: 0x0011E9C1
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

		// Token: 0x1700154C RID: 5452
		// (get) Token: 0x0600465F RID: 18015 RVA: 0x001207CF File Offset: 0x0011E9CF
		// (set) Token: 0x06004660 RID: 18016 RVA: 0x001207DC File Offset: 0x0011E9DC
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

		// Token: 0x1700154D RID: 5453
		// (get) Token: 0x06004661 RID: 18017 RVA: 0x001207EA File Offset: 0x0011E9EA
		// (set) Token: 0x06004662 RID: 18018 RVA: 0x001207F7 File Offset: 0x0011E9F7
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

		// Token: 0x06004663 RID: 18019
		protected abstract ADObjectId GetContainerId();

		// Token: 0x06004664 RID: 18020 RVA: 0x00120808 File Offset: 0x0011EA08
		internal static ADObjectId GetRecipientContainer(OrganizationalUnitIdParameter recipientContainer, IConfigurationSession cfgSession, OrganizationId organizationId, NewAddressBookBase.GetUniqueObject getDataObject, Task.ErrorLoggerDelegate writeError, Task.TaskVerboseLoggingDelegate writeVerbose)
		{
			bool useConfigNC = cfgSession.UseConfigNC;
			bool useGlobalCatalog = cfgSession.UseGlobalCatalog;
			cfgSession.UseConfigNC = false;
			cfgSession.UseGlobalCatalog = true;
			ExchangeOrganizationalUnit exchangeOrganizationalUnit;
			try
			{
				exchangeOrganizationalUnit = (ExchangeOrganizationalUnit)getDataObject(recipientContainer, cfgSession, organizationId.OrganizationalUnit, new LocalizedString?(Strings.ErrorOrganizationalUnitNotFound(recipientContainer.ToString())), new LocalizedString?(Strings.ErrorOrganizationalUnitNotUnique(recipientContainer.ToString())), ExchangeErrorCategory.Client);
				RecipientTaskHelper.IsOrgnizationalUnitInOrganization(cfgSession, organizationId, exchangeOrganizationalUnit, writeVerbose, writeError);
			}
			finally
			{
				cfgSession.UseConfigNC = useConfigNC;
				cfgSession.UseGlobalCatalog = useGlobalCatalog;
			}
			return exchangeOrganizationalUnit.Id;
		}

		// Token: 0x06004665 RID: 18021 RVA: 0x001208A0 File Offset: 0x0011EAA0
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			this.DataObject = (AddressBookBase)base.PrepareDataObject();
			if (!base.HasErrors)
			{
				if (!this.DataObject.IsModified(AddressBookBaseSchema.DisplayName))
				{
					this.DataObject.DisplayName = base.Name;
				}
				ADObjectId containerId = this.GetContainerId();
				if (!base.HasErrors)
				{
					this.DataObject.SetId(containerId.GetChildId(base.Name));
				}
			}
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
			else if (this.DataObject.RecipientContainer != null)
			{
				organizationalUnitIdParameter = new OrganizationalUnitIdParameter(this.DataObject.RecipientContainer);
			}
			if (organizationalUnitIdParameter != null)
			{
				if (base.GlobalConfigSession.IsInPreE14InteropMode())
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorCannotSetRecipientContainer), ErrorCategory.InvalidArgument, this.DataObject.Identity);
				}
				this.DataObject.RecipientContainer = NewAddressBookBase.GetRecipientContainer(organizationalUnitIdParameter, (IConfigurationSession)base.DataSession, base.OrganizationId ?? OrganizationId.ForestWideOrgId, new NewAddressBookBase.GetUniqueObject(base.GetDataObject<ExchangeOrganizationalUnit>), new Task.ErrorLoggerDelegate(base.WriteError), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose));
			}
			TaskLogger.LogExit();
			return this.DataObject;
		}

		// Token: 0x06004666 RID: 18022 RVA: 0x001209F0 File Offset: 0x0011EBF0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			this.CheckLimit();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			if (this.DataObject.IsModified(AddressBookBaseSchema.LdapRecipientFilter))
			{
				RecipientFilterHelper.StampE2003FilterMetadata(this.DataObject, this.DataObject.LdapRecipientFilter, AddressBookBaseSchema.PurportedSearchUI);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004667 RID: 18023 RVA: 0x00120A4C File Offset: 0x0011EC4C
		protected void CheckLimit()
		{
			int maxAddressLists = this.MaxAddressLists;
			if (maxAddressLists < 2147483647)
			{
				IEnumerable<AddressBookBase> allAddressLists = AddressBookBase.GetAllAddressLists(this.GetContainerId(), null, this.ConfigurationSession, null);
				int num = 0;
				foreach (AddressBookBase addressBookBase in allAddressLists)
				{
					if (!addressBookBase.IsTopContainer)
					{
						num++;
						if (num >= maxAddressLists)
						{
							base.WriteError(new ManagementObjectAlreadyExistsException(Strings.ErrorTooManyItems(maxAddressLists)), ErrorCategory.LimitsExceeded, base.Name);
							break;
						}
					}
				}
			}
		}

		// Token: 0x020007D9 RID: 2009
		// (Invoke) Token: 0x0600466A RID: 18026
		internal delegate IConfigurable GetUniqueObject(IIdentityParameter id, IConfigDataProvider session, ObjectId rootID, LocalizedString? notFoundError, LocalizedString? multipleFoundError, ExchangeErrorCategory errorCategory);
	}
}
