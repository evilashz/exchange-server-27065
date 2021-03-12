using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007EB RID: 2027
	[Cmdlet("Set", "AddressBookPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetAddressBookPolicy : SetMailboxPolicyBase<AddressBookMailboxPolicy>
	{
		// Token: 0x1700156A RID: 5482
		// (get) Token: 0x060046D5 RID: 18133 RVA: 0x00123021 File Offset: 0x00121221
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetAddressBookPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x1700156B RID: 5483
		// (get) Token: 0x060046D6 RID: 18134 RVA: 0x00123033 File Offset: 0x00121233
		// (set) Token: 0x060046D7 RID: 18135 RVA: 0x0012304A File Offset: 0x0012124A
		[Parameter]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x1700156C RID: 5484
		// (get) Token: 0x060046D8 RID: 18136 RVA: 0x0012305D File Offset: 0x0012125D
		// (set) Token: 0x060046D9 RID: 18137 RVA: 0x00123074 File Offset: 0x00121274
		[ValidateNotNullOrEmpty]
		[Parameter]
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

		// Token: 0x1700156D RID: 5485
		// (get) Token: 0x060046DA RID: 18138 RVA: 0x00123087 File Offset: 0x00121287
		// (set) Token: 0x060046DB RID: 18139 RVA: 0x0012309E File Offset: 0x0012129E
		[Parameter]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x1700156E RID: 5486
		// (get) Token: 0x060046DC RID: 18140 RVA: 0x001230B1 File Offset: 0x001212B1
		// (set) Token: 0x060046DD RID: 18141 RVA: 0x001230C8 File Offset: 0x001212C8
		[Parameter]
		[ValidateNotNullOrEmpty]
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

		// Token: 0x060046DE RID: 18142 RVA: 0x001230DC File Offset: 0x001212DC
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (base.HasErrors)
			{
				return;
			}
			AddressBookMailboxPolicy dataObject = this.DataObject;
			if (this.AddressLists != null && this.AddressLists.Length > 0)
			{
				dataObject.AddressLists = AddressBookPolicyTaskUtility.ValidateAddressBook(base.DataSession, this.AddressLists, new AddressBookPolicyTaskUtility.GetUniqueObject(base.GetDataObject<AddressBookBase>), dataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
			if (base.Fields.IsModified(AddressBookMailboxPolicySchema.GlobalAddressList) && this.GlobalAddressList != null)
			{
				AddressBookBase addressBookBase = (AddressBookBase)base.GetDataObject<AddressBookBase>(this.GlobalAddressList, base.DataSession, null, new LocalizedString?(Strings.ErrorAddressListOrGlobalAddressListNotFound(this.GlobalAddressList.ToString())), new LocalizedString?(Strings.ErrorAddressListOrGlobalAddressListNotUnique(this.GlobalAddressList.ToString())));
				if (addressBookBase.IsGlobalAddressList)
				{
					dataObject.GlobalAddressList = (ADObjectId)addressBookBase.Identity;
				}
				else
				{
					base.WriteError(new ArgumentException(Strings.ErrorGlobalAddressListNotFound(this.GlobalAddressList.ToString())), ErrorCategory.InvalidArgument, this.Identity);
					dataObject.GlobalAddressList = null;
				}
			}
			if (base.Fields.IsModified(AddressBookMailboxPolicySchema.RoomList) && this.RoomList != null)
			{
				AddressBookBase addressBookBase2 = (AddressBookBase)base.GetDataObject<AddressBookBase>(this.RoomList, base.DataSession, null, new LocalizedString?(Strings.ErrorAllRoomListNotFound(this.RoomList.ToString())), new LocalizedString?(Strings.ErrorAllRoomListNotUnique(this.RoomList.ToString())));
				if (addressBookBase2 != null)
				{
					dataObject.RoomList = (ADObjectId)addressBookBase2.Identity;
				}
				else
				{
					base.WriteError(new ArgumentException(Strings.ErrorAllRoomListNotFound(this.RoomList.ToString())), ErrorCategory.InvalidArgument, this.Identity);
					dataObject.RoomList = null;
				}
			}
			if (base.Fields.IsModified(AddressBookMailboxPolicySchema.OfflineAddressBook))
			{
				if (this.OfflineAddressBook != null)
				{
					OfflineAddressBook offlineAddressBook = (OfflineAddressBook)base.GetDataObject<OfflineAddressBook>(this.OfflineAddressBook, base.DataSession, null, new LocalizedString?(Strings.ErrorOfflineAddressBookNotFound(this.OfflineAddressBook.ToString())), new LocalizedString?(Strings.ErrorOfflineAddressBookNotUnique(this.OfflineAddressBook.ToString())));
					dataObject.OfflineAddressBook = (ADObjectId)offlineAddressBook.Identity;
				}
				else
				{
					dataObject.OfflineAddressBook = null;
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060046E0 RID: 18144 RVA: 0x00123318 File Offset: 0x00121518
		// Note: this type is marked as 'beforefieldinit'.
		static SetAddressBookPolicy()
		{
			ADPropertyDefinition[,] array = new ADPropertyDefinition[1, 2];
			array[0, 0] = ADConfigurationObjectSchema.AdminDisplayName;
			SetAddressBookPolicy.propertiesCannotBeSet = array;
		}

		// Token: 0x04002B09 RID: 11017
		private static readonly ADPropertyDefinition[,] propertiesCannotBeSet;
	}
}
