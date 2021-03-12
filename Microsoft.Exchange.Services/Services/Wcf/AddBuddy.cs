using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Wcf.Types;

namespace Microsoft.Exchange.Services.Wcf
{
	// Token: 0x02000906 RID: 2310
	internal class AddBuddy
	{
		// Token: 0x06004307 RID: 17159 RVA: 0x000DF8EE File Offset: 0x000DDAEE
		internal AddBuddy(MailboxSession session, Buddy buddy)
		{
			if (string.IsNullOrWhiteSpace(buddy.IMAddress))
			{
				throw new ArgumentException("A non-empty IM address needs to be passed in to add a one-off buddy");
			}
			this.session = session;
			this.buddy = buddy;
		}

		// Token: 0x06004308 RID: 17160 RVA: 0x000DF91C File Offset: 0x000DDB1C
		internal void Execute()
		{
			using (Folder folder = Folder.Bind(this.session, DefaultFolderType.Contacts))
			{
				StoreId storeId = BuddyListUtilities.GetSubFolderIdByClass(folder, "IPF.Contact.QuickContacts");
				bool flag = storeId != null;
				if (!flag)
				{
					storeId = this.CreateContactsSubFolder(folder, "Quick Contacts", "IPF.Contact.QuickContacts");
				}
				using (Folder folder2 = Folder.Bind(this.session, storeId))
				{
					if (flag)
					{
						StoreId buddyIdByImAddress = this.GetBuddyIdByImAddress(folder2);
						if (buddyIdByImAddress != null)
						{
							return;
						}
					}
					Participant participant = this.CreateOneOffBuddy(storeId);
					this.AddToBuddyList(participant, folder);
				}
			}
		}

		// Token: 0x06004309 RID: 17161 RVA: 0x000DF9C4 File Offset: 0x000DDBC4
		private static StoreId GetOtherContactsDLId(Folder buddyListFolder)
		{
			StoreId result = null;
			using (QueryResult queryResult = buddyListFolder.ItemQuery(ItemQueryType.None, null, new SortBy[]
			{
				new SortBy(StoreObjectSchema.ItemClass, SortOrder.Ascending)
			}, new StorePropertyDefinition[]
			{
				ItemSchema.Id
			}))
			{
				if (queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.Equal, StoreObjectSchema.ItemClass, "IPM.DistList.MOC.OtherContacts")))
				{
					IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(1);
					if (propertyBags.Length > 0)
					{
						result = (StoreId)propertyBags[0].TryGetProperty(ItemSchema.Id);
					}
				}
			}
			return result;
		}

		// Token: 0x0600430A RID: 17162 RVA: 0x000DFA5C File Offset: 0x000DDC5C
		private StoreId GetBuddyIdByImAddress(Folder quickContactsFolder)
		{
			StoreId result = null;
			using (QueryResult queryResult = quickContactsFolder.ItemQuery(ItemQueryType.None, null, new SortBy[]
			{
				new SortBy(ContactSchema.IMAddress, SortOrder.Ascending)
			}, new PropertyDefinition[]
			{
				ItemSchema.Id,
				ContactSchema.IMAddress
			}))
			{
				if (queryResult.SeekToCondition(SeekReference.OriginBeginning, new ComparisonFilter(ComparisonOperator.Equal, ContactSchema.IMAddress, this.buddy.IMAddress)))
				{
					IStorePropertyBag[] propertyBags = queryResult.GetPropertyBags(1);
					if (propertyBags.Length > 0)
					{
						result = (StoreId)propertyBags[0].TryGetProperty(ItemSchema.Id);
					}
				}
			}
			return result;
		}

		// Token: 0x0600430B RID: 17163 RVA: 0x000DFB04 File Offset: 0x000DDD04
		private Participant CreateOneOffBuddy(StoreId quickContactsFolderId)
		{
			Participant result;
			using (Contact contact = Contact.Create(this.session, quickContactsFolderId))
			{
				contact.EmailAddresses[EmailAddressIndex.Email1] = new Participant(this.buddy.DisplayName, this.buddy.IMAddress, "SMTP");
				contact.DisplayName = this.buddy.DisplayName;
				contact.ImAddress = this.buddy.IMAddress;
				contact[ContactSchema.GivenName] = this.buddy.DisplayName;
				contact.Save(SaveMode.ResolveConflicts);
				contact.Load(new PropertyDefinition[]
				{
					ContactSchema.Email1EmailAddress
				});
				result = contact.EmailAddresses[EmailAddressIndex.Email1];
			}
			return result;
		}

		// Token: 0x0600430C RID: 17164 RVA: 0x000DFBCC File Offset: 0x000DDDCC
		private void AddToBuddyList(Participant participant, Folder contactsFolder)
		{
			StoreId folderId = BuddyListUtilities.GetSubFolderIdByClass(contactsFolder, "IPF.Contact.BuddyList") ?? this.CreateContactsSubFolder(contactsFolder, "Buddy List", "IPF.Contact.BuddyList");
			using (Folder folder = Folder.Bind(this.session, folderId))
			{
				StoreId storeId = AddBuddy.GetOtherContactsDLId(folder) ?? this.CreateOtherContactsPDL(folder);
				using (DistributionList distributionList = DistributionList.Bind(this.session, storeId))
				{
					distributionList.OpenAsReadWrite();
					distributionList.Add(participant);
					distributionList.Save(SaveMode.ResolveConflicts);
				}
			}
		}

		// Token: 0x0600430D RID: 17165 RVA: 0x000DFC70 File Offset: 0x000DDE70
		private StoreId CreateContactsSubFolder(Folder folder, string displayName, string folderClassName)
		{
			StoreId id;
			using (Folder folder2 = Folder.Create(this.session, folder.StoreObjectId, StoreObjectType.ContactsFolder))
			{
				folder2.DisplayName = displayName;
				folder2.ClassName = folderClassName;
				folder2.Save();
				folder2.Load(new PropertyDefinition[]
				{
					FolderSchema.Id
				});
				id = folder2.Id;
			}
			return id;
		}

		// Token: 0x0600430E RID: 17166 RVA: 0x000DFCE0 File Offset: 0x000DDEE0
		private StoreId CreateOtherContactsPDL(Folder buddyListFolder)
		{
			StoreId id;
			using (DistributionList distributionList = DistributionList.Create(this.session, buddyListFolder.StoreObjectId))
			{
				distributionList.DisplayName = "Other Contacts";
				distributionList.ClassName = "IPM.DistList.MOC.OtherContacts";
				distributionList.Save(SaveMode.ResolveConflicts);
				distributionList.Load(new PropertyDefinition[]
				{
					ItemSchema.Id
				});
				id = distributionList.Id;
			}
			return id;
		}

		// Token: 0x04002712 RID: 10002
		private readonly MailboxSession session;

		// Token: 0x04002713 RID: 10003
		private readonly Buddy buddy;
	}
}
