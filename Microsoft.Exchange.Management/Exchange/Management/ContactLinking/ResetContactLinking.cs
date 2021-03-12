using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.ContactLinking
{
	// Token: 0x0200014F RID: 335
	[Cmdlet("Reset", "ContactLinking")]
	public sealed class ResetContactLinking : ContactLinkingBaseCmdLet
	{
		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0003772F File Offset: 0x0003592F
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x00037737 File Offset: 0x00035937
		[Parameter(Mandatory = false, Position = 1)]
		public SwitchParameter IncludeUserApproved { get; set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00037740 File Offset: 0x00035940
		protected override string UserAgent
		{
			get
			{
				return "Client=Management;Action=Reset-ContactLinking";
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00037747 File Offset: 0x00035947
		internal override void ContactLinkingOperation(MailboxSession mailboxSession)
		{
			this.UpdateAllItems(mailboxSession, new Predicate<Item>(this.ShouldProcessContact), new Action<Item>(ResetContactLinking.CleanUpContactLinkingInformation));
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00037768 File Offset: 0x00035968
		private static void CleanUpContactLinkingInformation(Item contactItem)
		{
			contactItem.OpenAsReadWrite();
			contactItem.SetOrDeleteProperty(ContactSchema.PersonId, PersonId.CreateNew());
			contactItem.SetOrDeleteProperty(ContactSchema.Linked, false);
			contactItem.SetOrDeleteProperty(ContactSchema.LinkRejectHistory, null);
			contactItem.SetOrDeleteProperty(ContactSchema.GALLinkState, GALLinkState.NotLinked);
			contactItem.SetOrDeleteProperty(ContactSchema.GALLinkID, null);
			contactItem.SetOrDeleteProperty(ContactSchema.SmtpAddressCache, null);
			contactItem.SetOrDeleteProperty(ContactSchema.AddressBookEntryId, null);
			contactItem.SetOrDeleteProperty(ContactSchema.UserApprovedLink, false);
			contactItem.Save(SaveMode.NoConflictResolution);
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x000377F8 File Offset: 0x000359F8
		private bool ShouldProcessContact(Item contactItem)
		{
			return this.IncludeUserApproved.IsPresent || !contactItem.GetValueOrDefault<bool>(ContactSchema.UserApprovedLink, false);
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0003782C File Offset: 0x00035A2C
		private void UpdateAllItems(MailboxSession mailboxSession, Predicate<Item> filterCriteria, Action<Item> updateAction)
		{
			ContactsEnumerator<IStorePropertyBag> contactsEnumerator = ContactsEnumerator<IStorePropertyBag>.CreateContactsOnlyEnumerator(mailboxSession, DefaultFolderType.AllContacts, ResetContactLinking.ContactPropertiesToEnumerate, (IStorePropertyBag propertyBag) => propertyBag, new XSOFactory());
			foreach (IStorePropertyBag storePropertyBag in contactsEnumerator)
			{
				VersionedId valueOrDefault = storePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
				base.PerformanceTracker.IncrementContactsRead();
				if (valueOrDefault != null)
				{
					try
					{
						using (Item item = Item.Bind(mailboxSession, valueOrDefault.ObjectId, ResetContactLinking.ContactPropertiesToBind))
						{
							if (filterCriteria(item))
							{
								updateAction(item);
								base.PerformanceTracker.IncrementContactsUpdated();
							}
						}
					}
					catch (ObjectNotFoundException)
					{
						this.WriteWarning(base.GetErrorMessageObjectNotFound(valueOrDefault.ObjectId.ToBase64String(), typeof(Item).ToString(), mailboxSession.ToString()));
					}
				}
			}
		}

		// Token: 0x040005BF RID: 1471
		private static readonly PropertyDefinition[] ContactPropertiesToBind = new PropertyDefinition[]
		{
			ContactSchema.GALLinkState,
			ContactSchema.GALLinkID,
			ContactSchema.SmtpAddressCache,
			ContactSchema.AddressBookEntryId,
			ContactSchema.UserApprovedLink,
			ContactSchema.PersonId,
			ContactSchema.Linked,
			ContactSchema.LinkRejectHistory
		};

		// Token: 0x040005C0 RID: 1472
		private static readonly PropertyDefinition[] ContactPropertiesToEnumerate = new PropertyDefinition[]
		{
			ItemSchema.Id
		};
	}
}
