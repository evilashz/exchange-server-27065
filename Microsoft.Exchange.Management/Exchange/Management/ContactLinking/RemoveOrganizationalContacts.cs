using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.ContactLinking
{
	// Token: 0x0200014D RID: 333
	[Cmdlet("Remove", "OrganizationalContacts")]
	public sealed class RemoveOrganizationalContacts : ContactLinkingBaseCmdLet
	{
		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x00037311 File Offset: 0x00035511
		protected override string UserAgent
		{
			get
			{
				return "Client=Management;Action=Remove-OrganizationalContacts";
			}
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0003731C File Offset: 0x0003551C
		internal override void ContactLinkingOperation(MailboxSession mailboxSession)
		{
			ContactsEnumerator<IStorePropertyBag> contactsEnumerator = ContactsEnumerator<IStorePropertyBag>.CreateContactsOnlyEnumerator(mailboxSession, DefaultFolderType.AllContacts, RemoveOrganizationalContacts.ContactPropertiesToEnumerate, (IStorePropertyBag propertyBag) => propertyBag, new XSOFactory());
			foreach (IStorePropertyBag storePropertyBag in contactsEnumerator)
			{
				VersionedId valueOrDefault = storePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
				base.PerformanceTracker.IncrementContactsRead();
				if (valueOrDefault != null && RemoveOrganizationalContacts.ShouldDeleteContact(storePropertyBag))
				{
					try
					{
						mailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
						{
							valueOrDefault.ObjectId
						});
						base.PerformanceTracker.IncrementContactsUpdated();
					}
					catch (ObjectNotFoundException)
					{
						this.WriteWarning(base.GetErrorMessageObjectNotFound(valueOrDefault.ObjectId.ToBase64String(), typeof(Item).ToString(), mailboxSession.ToString()));
					}
				}
			}
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0003741C File Offset: 0x0003561C
		private static bool ShouldDeleteContact(IStorePropertyBag contactItem)
		{
			string valueOrDefault = contactItem.GetValueOrDefault<string>(ContactSchema.PartnerNetworkId, string.Empty);
			return string.Equals(WellKnownNetworkNames.GAL, valueOrDefault, StringComparison.InvariantCulture);
		}

		// Token: 0x040005B5 RID: 1461
		private static readonly PropertyDefinition[] ContactPropertiesToEnumerate = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ContactSchema.PartnerNetworkId
		};
	}
}
