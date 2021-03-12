using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000ADB RID: 2779
	public class NewOfflineAddressBookInternal : NewMultitenancySystemConfigurationObjectTask<OfflineAddressBook>
	{
		// Token: 0x17001DF4 RID: 7668
		// (get) Token: 0x060062C0 RID: 25280 RVA: 0x0019CA6F File Offset: 0x0019AC6F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewOfflineAddressBook(base.Name.ToString(), base.FormatMultiValuedProperty(this.AddressLists));
			}
		}

		// Token: 0x17001DF5 RID: 7669
		// (get) Token: 0x060062C1 RID: 25281 RVA: 0x0019CA90 File Offset: 0x0019AC90
		private int MaxOfflineAddressBooks
		{
			get
			{
				if (!Datacenter.IsMicrosoftHostedOnly(true))
				{
					return int.MaxValue;
				}
				int? maxOfflineAddressBooks = this.ConfigurationSession.GetOrgContainer().MaxOfflineAddressBooks;
				if (maxOfflineAddressBooks == null)
				{
					return 250;
				}
				return maxOfflineAddressBooks.GetValueOrDefault();
			}
		}

		// Token: 0x060062C2 RID: 25282 RVA: 0x0019CAD2 File Offset: 0x0019ACD2
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || typeof(OrgContainerNotFoundException).IsInstanceOfType(exception) || typeof(TenantOrgContainerNotFoundException).IsInstanceOfType(exception);
		}

		// Token: 0x17001DF6 RID: 7670
		// (get) Token: 0x060062C3 RID: 25283 RVA: 0x0019CB08 File Offset: 0x0019AD08
		// (set) Token: 0x060062C4 RID: 25284 RVA: 0x0019CB1F File Offset: 0x0019AD1F
		public virtual AddressBookBaseIdParameter[] AddressLists
		{
			get
			{
				return (AddressBookBaseIdParameter[])base.Fields["AddressLists"];
			}
			set
			{
				base.Fields["AddressLists"] = value;
			}
		}

		// Token: 0x17001DF7 RID: 7671
		// (get) Token: 0x060062C5 RID: 25285 RVA: 0x0019CB32 File Offset: 0x0019AD32
		// (set) Token: 0x060062C6 RID: 25286 RVA: 0x0019CB3F File Offset: 0x0019AD3F
		[Parameter]
		public bool IsDefault
		{
			get
			{
				return this.DataObject.IsDefault;
			}
			set
			{
				this.DataObject.IsDefault = value;
			}
		}

		// Token: 0x17001DF8 RID: 7672
		// (get) Token: 0x060062C7 RID: 25287 RVA: 0x0019CB4D File Offset: 0x0019AD4D
		// (set) Token: 0x060062C8 RID: 25288 RVA: 0x0019CB64 File Offset: 0x0019AD64
		[Parameter]
		public MailboxIdParameter GeneratingMailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["GeneratingMailbox"];
			}
			set
			{
				base.Fields["GeneratingMailbox"] = value;
			}
		}

		// Token: 0x17001DF9 RID: 7673
		// (get) Token: 0x060062C9 RID: 25289 RVA: 0x0019CB77 File Offset: 0x0019AD77
		// (set) Token: 0x060062CA RID: 25290 RVA: 0x0019CB98 File Offset: 0x0019AD98
		[Parameter]
		public bool GlobalWebDistributionEnabled
		{
			get
			{
				return (bool)(base.Fields["GlobalWebDistributionEnabled"] ?? false);
			}
			set
			{
				base.Fields["GlobalWebDistributionEnabled"] = value;
			}
		}

		// Token: 0x17001DFA RID: 7674
		// (get) Token: 0x060062CB RID: 25291 RVA: 0x0019CBB0 File Offset: 0x0019ADB0
		// (set) Token: 0x060062CC RID: 25292 RVA: 0x0019CBD1 File Offset: 0x0019ADD1
		[Parameter]
		public bool ShadowMailboxDistributionEnabled
		{
			get
			{
				return (bool)(base.Fields["ShadowMailboxDistributionEnabled"] ?? false);
			}
			set
			{
				base.Fields["ShadowMailboxDistributionEnabled"] = value;
			}
		}

		// Token: 0x17001DFB RID: 7675
		// (get) Token: 0x060062CD RID: 25293 RVA: 0x0019CBE9 File Offset: 0x0019ADE9
		// (set) Token: 0x060062CE RID: 25294 RVA: 0x0019CBF6 File Offset: 0x0019ADF6
		[Parameter]
		public Unlimited<int>? DiffRetentionPeriod
		{
			get
			{
				return this.DataObject.DiffRetentionPeriod;
			}
			set
			{
				this.DataObject.DiffRetentionPeriod = value;
			}
		}

		// Token: 0x17001DFC RID: 7676
		// (get) Token: 0x060062CF RID: 25295 RVA: 0x0019CC04 File Offset: 0x0019AE04
		// (set) Token: 0x060062D0 RID: 25296 RVA: 0x0019CC1B File Offset: 0x0019AE1B
		[Parameter]
		public VirtualDirectoryIdParameter[] VirtualDirectories
		{
			get
			{
				return (VirtualDirectoryIdParameter[])base.Fields["VirtualDirectories"];
			}
			set
			{
				base.Fields["VirtualDirectories"] = value;
			}
		}

		// Token: 0x060062D1 RID: 25297 RVA: 0x0019CC30 File Offset: 0x0019AE30
		protected override IConfigurable PrepareDataObject()
		{
			this.DataObject = (OfflineAddressBook)base.PrepareDataObject();
			MultiValuedProperty<OfflineAddressBookVersion> multiValuedProperty = new MultiValuedProperty<OfflineAddressBookVersion>();
			multiValuedProperty.Add(OfflineAddressBookVersion.Version4);
			this.DataObject.Versions = multiValuedProperty;
			this.DataObject.ConfiguredAttributes = OfflineAddressBookMapiProperty.DefaultOABPropertyList;
			this.DataObject.UpdateRawMapiAttributes(false);
			if (base.Fields.IsModified("GeneratingMailbox"))
			{
				this.DataObject.GeneratingMailbox = OfflineAddressBookTaskUtility.ValidateGeneratingMailbox(base.TenantGlobalCatalogSession, this.GeneratingMailbox, new OfflineAddressBookTaskUtility.GetUniqueObject(base.GetDataObject<ADUser>), this.DataObject, new Task.TaskWarningLoggingDelegate(this.WriteWarning), new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			this.DataObject.AddressLists = OfflineAddressBookTaskUtility.ValidateAddressBook(base.DataSession, this.AddressLists, new OfflineAddressBookTaskUtility.GetUniqueObject(base.GetDataObject<AddressBookBase>), this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
			this.DataObject.PublicFolderDistributionEnabled = false;
			this.DataObject.PublicFolderDatabase = null;
			if (this.VirtualDirectories != null && this.VirtualDirectories.Length != 0)
			{
				this.DataObject.VirtualDirectories = OfflineAddressBookTaskUtility.ValidateVirtualDirectory(base.GlobalConfigSession, this.VirtualDirectories, new OfflineAddressBookTaskUtility.GetUniqueObject(base.GetDataObject<ADOabVirtualDirectory>), this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (base.Fields.IsModified("GlobalWebDistributionEnabled"))
			{
				this.DataObject.GlobalWebDistributionEnabled = this.GlobalWebDistributionEnabled;
			}
			if (base.Fields.IsModified("ShadowMailboxDistributionEnabled"))
			{
				this.DataObject.ShadowMailboxDistributionEnabled = this.ShadowMailboxDistributionEnabled;
			}
			this.DataObject.Server = ((IConfigurationSession)base.DataSession).GetOrgContainerId().GetDescendantId(this.DataObject.ParentPath);
			if (!this.DataObject.IsModified(OfflineAddressBookSchema.IsDefault) && ((IConfigurationSession)base.DataSession).Find<OfflineAddressBook>(null, QueryScope.SubTree, null, null, 1).Length == 0)
			{
				this.DataObject.IsDefault = true;
			}
			this.DataObject.SetId((IConfigurationSession)base.DataSession, base.Name);
			string parentLegacyDN = string.Format(CultureInfo.InvariantCulture, "{0}/cn=addrlists/cn=oabs", new object[]
			{
				((IConfigurationSession)base.DataSession).GetOrgContainer().LegacyExchangeDN
			});
			this.DataObject[OfflineAddressBookSchema.ExchangeLegacyDN] = LegacyDN.GenerateLegacyDN(parentLegacyDN, this.DataObject, true, new LegacyDN.LegacyDNIsUnique(this.LegacyDNIsUnique));
			OfflineAddressBookTaskUtility.WarnForNoDistribution(this.DataObject, new Task.TaskWarningLoggingDelegate(this.WriteWarning));
			return this.DataObject;
		}

		// Token: 0x060062D2 RID: 25298 RVA: 0x0019CEB8 File Offset: 0x0019B0B8
		protected override void InternalValidate()
		{
			int maxOfflineAddressBooks = this.MaxOfflineAddressBooks;
			if (maxOfflineAddressBooks < 2147483647)
			{
				IEnumerable<OfflineAddressBook> enumerable = base.DataSession.FindPaged<OfflineAddressBook>(null, ((IConfigurationSession)base.DataSession).GetOrgContainerId().GetDescendantId(this.DataObject.ParentPath), false, null, ADGenericPagedReader<OfflineAddressBook>.DefaultPageSize);
				int num = 0;
				foreach (OfflineAddressBook offlineAddressBook in enumerable)
				{
					num++;
					if (num >= maxOfflineAddressBooks)
					{
						base.WriteError(new ManagementObjectAlreadyExistsException(Strings.ErrorTooManyItems(maxOfflineAddressBooks)), ErrorCategory.LimitsExceeded, base.Name);
						break;
					}
				}
			}
			base.InternalValidate();
		}

		// Token: 0x060062D3 RID: 25299 RVA: 0x0019CF68 File Offset: 0x0019B168
		protected override void InternalProcessRecord()
		{
			OfflineAddressBook offlineAddressBook = null;
			if (this.DataObject.IsDefault)
			{
				offlineAddressBook = OfflineAddressBookTaskUtility.ResetOldDefaultOab(base.DataSession, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			bool flag = false;
			try
			{
				base.InternalProcessRecord();
				flag = true;
			}
			finally
			{
				if (!flag && offlineAddressBook != null)
				{
					offlineAddressBook.IsDefault = true;
					try
					{
						base.DataSession.Save(offlineAddressBook);
					}
					catch (DataSourceTransientException exception)
					{
						this.WriteError(exception, ErrorCategory.WriteError, null, false);
					}
				}
			}
		}

		// Token: 0x060062D4 RID: 25300 RVA: 0x0019CFF0 File Offset: 0x0019B1F0
		private bool LegacyDNIsUnique(string legacyDN)
		{
			bool result = false;
			OfflineAddressBook[] array = ((IConfigurationSession)base.DataSession).Find<OfflineAddressBook>(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, OfflineAddressBookSchema.ExchangeLegacyDN, legacyDN), null, 2);
			if (array.Length <= 0 || (array.Length == 1 && array[0].DistinguishedName.Equals(((ADObjectId)this.DataObject[ADObjectSchema.Id]).DistinguishedName)))
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060062D5 RID: 25301 RVA: 0x0019D058 File Offset: 0x0019B258
		protected override void ValidateProvisionedProperties(IConfigurable dataObject)
		{
			OfflineAddressBook offlineAddressBook = dataObject as OfflineAddressBook;
			if (offlineAddressBook != null && offlineAddressBook.IsChanged(OfflineAddressBookSchema.VirtualDirectories) && (this.GlobalWebDistributionEnabled || (base.Fields.IsModified("VirtualDirectories") && base.Fields["VirtualDirectories"] == null)))
			{
				offlineAddressBook.VirtualDirectories = null;
			}
		}
	}
}
