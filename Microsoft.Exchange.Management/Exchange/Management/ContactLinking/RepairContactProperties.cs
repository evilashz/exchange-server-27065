using System;
using System.Globalization;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Management.ContactLinking
{
	// Token: 0x0200014E RID: 334
	[Cmdlet("Repair", "ContactProperties")]
	public sealed class RepairContactProperties : ContactLinkingBaseCmdLet
	{
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000BEB RID: 3051 RVA: 0x0003747A File Offset: 0x0003567A
		// (set) Token: 0x06000BEC RID: 3052 RVA: 0x00037482 File Offset: 0x00035682
		[Parameter(Mandatory = true, Position = 1, ParameterSetName = "DisplayNameParameterSet")]
		public SwitchParameter FixDisplayName { get; set; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000BED RID: 3053 RVA: 0x0003748B File Offset: 0x0003568B
		// (set) Token: 0x06000BEE RID: 3054 RVA: 0x00037493 File Offset: 0x00035693
		[Parameter(Mandatory = false, Position = 2, ParameterSetName = "DisplayNameParameterSet")]
		[Parameter(Mandatory = true, Position = 1, ParameterSetName = "ConversationIndexParameterSet")]
		public SwitchParameter FixConversationIndexTracking { get; set; }

		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0003749C File Offset: 0x0003569C
		protected override string UserAgent
		{
			get
			{
				return "Client=Management;Action=Repair-ContactProperties";
			}
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x000374A8 File Offset: 0x000356A8
		internal override void ContactLinkingOperation(MailboxSession mailboxSession)
		{
			ContactsEnumerator<IStorePropertyBag> contactsEnumerator = ContactsEnumerator<IStorePropertyBag>.CreateContactsAndPdlsEnumerator(mailboxSession, DefaultFolderType.AllContacts, RepairContactProperties.ContactPropertiesToEnumerate, (IStorePropertyBag propertyBag) => propertyBag, new XSOFactory());
			foreach (IStorePropertyBag storePropertyBag in contactsEnumerator)
			{
				VersionedId valueOrDefault = storePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
				base.PerformanceTracker.IncrementContactsRead();
				try
				{
					using (Item item = Item.Bind(mailboxSession, valueOrDefault.ObjectId, RepairContactProperties.ContactPropertiesToBind))
					{
						item.OpenAsReadWrite();
						if (this.FixDisplayName.IsPresent)
						{
							RepairContactProperties.TriggerContactDisplayNamePropertyRule(item);
						}
						if (this.FixConversationIndexTracking.IsPresent)
						{
							RepairContactProperties.TurnOnConversationIndexTracking(item);
						}
						if (item.IsDirty)
						{
							item.Save(SaveMode.NoConflictResolution);
							base.PerformanceTracker.IncrementContactsUpdated();
							if (base.IsVerboseOn)
							{
								base.WriteVerbose(RepairContactProperties.FormatContactForVerboseOutput(this.Identity, item));
							}
						}
					}
				}
				catch (ObjectNotFoundException)
				{
					this.WriteWarning(base.GetErrorMessageObjectNotFound(valueOrDefault.ObjectId.ToBase64String(), typeof(Item).ToString(), mailboxSession.ToString()));
				}
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0003760C File Offset: 0x0003580C
		private static void TriggerContactDisplayNamePropertyRule(Item contactItem)
		{
			string valueOrDefault = contactItem.GetValueOrDefault<string>(StoreObjectSchema.DisplayName, null);
			string propertyValue = Guid.NewGuid().ToString();
			contactItem.SetOrDeleteProperty(StoreObjectSchema.DisplayName, propertyValue);
			contactItem.SetOrDeleteProperty(StoreObjectSchema.DisplayName, valueOrDefault);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00037652 File Offset: 0x00035852
		private static void TurnOnConversationIndexTracking(Item contactItem)
		{
			contactItem.SetOrDeleteProperty(ItemSchema.ConversationIndexTracking, true);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00037668 File Offset: 0x00035868
		private static LocalizedString FormatContactForVerboseOutput(MailboxIdParameter mailbox, Item contactItem)
		{
			contactItem.Load(new PropertyDefinition[]
			{
				ItemSchema.Id,
				ContactSchema.PersonId,
				StoreObjectSchema.DisplayName
			});
			return new LocalizedString(string.Format(CultureInfo.InvariantCulture, "Updated contact in Mailbox: {0}. EntryID: {1}, PersonId: {2}, DisplayNameFirstLast: '{3}'.", new object[]
			{
				mailbox,
				contactItem.Id.ObjectId,
				contactItem.GetValueOrDefault<PersonId>(ContactSchema.PersonId),
				contactItem.GetValueOrDefault<string>(ContactBaseSchema.DisplayNameFirstLast)
			}));
		}

		// Token: 0x040005B7 RID: 1463
		private const string DisplayNameParameterSet = "DisplayNameParameterSet";

		// Token: 0x040005B8 RID: 1464
		private const string ConversationIndexParameterSet = "ConversationIndexParameterSet";

		// Token: 0x040005B9 RID: 1465
		private const string RepairContactPropertiesUserAgent = "Client=Management;Action=Repair-ContactProperties";

		// Token: 0x040005BA RID: 1466
		private static readonly PropertyDefinition[] ContactPropertiesToEnumerate = new PropertyDefinition[]
		{
			ItemSchema.Id
		};

		// Token: 0x040005BB RID: 1467
		private static readonly PropertyDefinition[] ContactPropertiesToBind = new PropertyDefinition[]
		{
			StoreObjectSchema.DisplayName,
			ItemSchema.ConversationIndexTracking
		};
	}
}
