using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007E9 RID: 2025
	[Cmdlet("New", "AddressBookPolicy", SupportsShouldProcess = true)]
	public sealed class NewAddressBookPolicy : NewMailboxPolicyBase<AddressBookMailboxPolicy>
	{
		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x060046C6 RID: 18118 RVA: 0x00122B7D File Offset: 0x00120D7D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewAddressBookPolicy(base.Name.ToString(), base.FormatMultiValuedProperty(this.AddressLists), this.GlobalAddressList.ToString(), this.RoomList.ToString(), this.OfflineAddressBook.ToString());
			}
		}

		// Token: 0x17001564 RID: 5476
		// (get) Token: 0x060046C7 RID: 18119 RVA: 0x00122BBC File Offset: 0x00120DBC
		private int MaxAddressBookPolicies
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

		// Token: 0x17001565 RID: 5477
		// (get) Token: 0x060046C8 RID: 18120 RVA: 0x00122C16 File Offset: 0x00120E16
		// (set) Token: 0x060046C9 RID: 18121 RVA: 0x00122C2D File Offset: 0x00120E2D
		[Parameter(Mandatory = true)]
		public AddressListIdParameter[] AddressLists
		{
			get
			{
				return (AddressListIdParameter[])base.Fields[AddressBookMailboxPolicySchema.AddressLists];
			}
			set
			{
				base.Fields[AddressBookMailboxPolicySchema.AddressLists] = value;
			}
		}

		// Token: 0x17001566 RID: 5478
		// (get) Token: 0x060046CA RID: 18122 RVA: 0x00122C40 File Offset: 0x00120E40
		// (set) Token: 0x060046CB RID: 18123 RVA: 0x00122C57 File Offset: 0x00120E57
		[Parameter(Mandatory = true)]
		public GlobalAddressListIdParameter GlobalAddressList
		{
			get
			{
				return (GlobalAddressListIdParameter)base.Fields[AddressBookMailboxPolicySchema.GlobalAddressList];
			}
			set
			{
				base.Fields[AddressBookMailboxPolicySchema.GlobalAddressList] = value;
			}
		}

		// Token: 0x17001567 RID: 5479
		// (get) Token: 0x060046CC RID: 18124 RVA: 0x00122C6A File Offset: 0x00120E6A
		// (set) Token: 0x060046CD RID: 18125 RVA: 0x00122C81 File Offset: 0x00120E81
		[Parameter(Mandatory = true)]
		public AddressListIdParameter RoomList
		{
			get
			{
				return (AddressListIdParameter)base.Fields[AddressBookMailboxPolicySchema.RoomList];
			}
			set
			{
				base.Fields[AddressBookMailboxPolicySchema.RoomList] = value;
			}
		}

		// Token: 0x17001568 RID: 5480
		// (get) Token: 0x060046CE RID: 18126 RVA: 0x00122C94 File Offset: 0x00120E94
		// (set) Token: 0x060046CF RID: 18127 RVA: 0x00122CAB File Offset: 0x00120EAB
		[Parameter(Mandatory = true)]
		public OfflineAddressBookIdParameter OfflineAddressBook
		{
			get
			{
				return (OfflineAddressBookIdParameter)base.Fields[AddressBookMailboxPolicySchema.OfflineAddressBook];
			}
			set
			{
				base.Fields[AddressBookMailboxPolicySchema.OfflineAddressBook] = value;
			}
		}

		// Token: 0x060046D0 RID: 18128 RVA: 0x00122CC0 File Offset: 0x00120EC0
		protected override void InternalValidate()
		{
			this.DataObject = (AddressBookMailboxPolicy)base.PrepareDataObject();
			this.DataObject.SetExchangeVersion(ExchangeObjectVersion.Exchange2010);
			this.DataObject.MinAdminVersion = new int?(this.DataObject.ExchangeVersion.ExchangeBuild.ToExchange2003FormatInt32());
			int maxAddressBookPolicies = this.MaxAddressBookPolicies;
			if (maxAddressBookPolicies < 2147483647)
			{
				IEnumerable<AddressBookMailboxPolicy> enumerable = base.DataSession.FindPaged<AddressBookMailboxPolicy>(null, ((IConfigurationSession)base.DataSession).GetOrgContainerId().GetDescendantId(this.DataObject.ParentPath), false, null, ADGenericPagedReader<AddressBookMailboxPolicy>.DefaultPageSize);
				int num = 0;
				foreach (AddressBookMailboxPolicy addressBookMailboxPolicy in enumerable)
				{
					num++;
					if (num >= maxAddressBookPolicies)
					{
						base.WriteError(new ManagementObjectAlreadyExistsException(Strings.ErrorTooManyItems(maxAddressBookPolicies)), ErrorCategory.LimitsExceeded, base.Name);
						break;
					}
				}
			}
			this.DataObject.AddressLists = AddressBookPolicyTaskUtility.ValidateAddressBook(base.DataSession, this.AddressLists, new AddressBookPolicyTaskUtility.GetUniqueObject(base.GetDataObject<AddressBookBase>), this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
			AddressBookBase addressBookBase = (AddressBookBase)base.GetDataObject<AddressBookBase>(this.GlobalAddressList, base.DataSession, null, new LocalizedString?(Strings.ErrorAddressListOrGlobalAddressListNotFound(this.GlobalAddressList.ToString())), new LocalizedString?(Strings.ErrorAddressListOrGlobalAddressListNotUnique(this.GlobalAddressList.ToString())));
			if (addressBookBase != null && addressBookBase.IsGlobalAddressList)
			{
				this.DataObject.GlobalAddressList = (ADObjectId)addressBookBase.Identity;
			}
			else
			{
				base.WriteError(new ArgumentException(Strings.ErrorGlobalAddressListNotFound(this.GlobalAddressList.ToString())), ErrorCategory.InvalidArgument, base.Name);
				this.DataObject.GlobalAddressList = null;
			}
			AddressBookBase addressBookBase2 = (AddressBookBase)base.GetDataObject<AddressBookBase>(this.RoomList, base.DataSession, null, new LocalizedString?(Strings.ErrorAllRoomListNotFound(this.RoomList.ToString())), new LocalizedString?(Strings.ErrorAllRoomListNotUnique(this.RoomList.ToString())));
			if (addressBookBase2 != null)
			{
				this.DataObject.RoomList = (ADObjectId)addressBookBase2.Identity;
			}
			else
			{
				base.WriteError(new ArgumentException(Strings.ErrorAllRoomListNotFound(this.RoomList.ToString())), ErrorCategory.InvalidArgument, base.Name);
				this.DataObject.RoomList = null;
			}
			OfflineAddressBook offlineAddressBook = (OfflineAddressBook)base.GetDataObject<OfflineAddressBook>(this.OfflineAddressBook, base.DataSession, null, new LocalizedString?(Strings.ErrorOfflineAddressBookNotFound(this.OfflineAddressBook.ToString())), new LocalizedString?(Strings.ErrorOfflineAddressBookNotUnique(this.OfflineAddressBook.ToString())));
			if (offlineAddressBook != null)
			{
				this.DataObject.OfflineAddressBook = (ADObjectId)offlineAddressBook.Identity;
			}
			else
			{
				base.WriteError(new ArgumentException(Strings.ErrorOfflineAddressBookNotFound(this.OfflineAddressBook.ToString())), ErrorCategory.InvalidArgument, base.Name);
				this.DataObject.OfflineAddressBook = null;
			}
			ReadOnlyCollection<PropertyDefinition> allProperties = this.DataObject.Schema.AllProperties;
			base.InternalValidate();
		}
	}
}
