using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000555 RID: 1365
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RecursiveContactsEnumerator : IEnumerable<IStorePropertyBag>, IEnumerable
	{
		// Token: 0x060039AF RID: 14767 RVA: 0x000EC34C File Offset: 0x000EA54C
		public RecursiveContactsEnumerator(IMailboxSession session, IXSOFactory xsoFactory, DefaultFolderType folderType, params PropertyDefinition[] properties)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(properties, "properties");
			this.session = session;
			this.xsoFactory = xsoFactory;
			this.folderType = folderType;
			this.properties = PropertyDefinitionCollection.Merge<PropertyDefinition>(RecursiveContactsEnumerator.RequiredProperties, properties);
		}

		// Token: 0x060039B0 RID: 14768 RVA: 0x000EC780 File Offset: 0x000EA980
		public IEnumerator<IStorePropertyBag> GetEnumerator()
		{
			ContactFoldersEnumeratorOptions foldersEnumeratorOptions = ContactFoldersEnumeratorOptions.SkipHiddenFolders | ContactFoldersEnumeratorOptions.SkipDeletedFolders | ContactFoldersEnumeratorOptions.IncludeParentFolder;
			ContactFoldersEnumerator foldersEnumerator = new ContactFoldersEnumerator(this.session, new XSOFactory(), this.folderType, foldersEnumeratorOptions, new PropertyDefinition[0]);
			foreach (IStorePropertyBag folderPropertyBag in foldersEnumerator)
			{
				VersionedId folderId = folderPropertyBag.GetValueOrDefault<VersionedId>(FolderSchema.Id, null);
				IFolder folder;
				try
				{
					folder = this.xsoFactory.BindToFolder(this.session, folderId.ObjectId);
				}
				catch (ObjectNotFoundException)
				{
					RecursiveContactsEnumerator.Tracer.TraceError<VersionedId, Guid>((long)this.GetHashCode(), "Failed to bind to folder. FolderId: {0}. Mailbox: {1}.", folderId, this.session.MailboxOwner.MailboxInfo.MailboxGuid);
					continue;
				}
				try
				{
					using (IQueryResult contactsQuery = folder.IItemQuery(ItemQueryType.None, null, null, this.properties))
					{
						IStorePropertyBag[] contacts = contactsQuery.GetPropertyBags(100);
						while (contacts.Length > 0)
						{
							foreach (IStorePropertyBag contactPropertyBag in contacts)
							{
								if (contactPropertyBag != null && !(contactPropertyBag.TryGetProperty(ItemSchema.Id) is PropertyError) && ObjectClass.IsContact(contactPropertyBag.GetValueOrDefault<string>(StoreObjectSchema.ItemClass, null)))
								{
									yield return contactPropertyBag;
								}
							}
							contacts = contactsQuery.GetPropertyBags(100);
						}
					}
				}
				finally
				{
					folder.Dispose();
				}
			}
			yield break;
		}

		// Token: 0x060039B1 RID: 14769 RVA: 0x000EC79C File Offset: 0x000EA99C
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generic version of GetEnumerator.");
		}

		// Token: 0x04001EC5 RID: 7877
		private const int ChunkSize = 100;

		// Token: 0x04001EC6 RID: 7878
		protected static readonly Trace Tracer = ExTraceGlobals.ContactsEnumeratorTracer;

		// Token: 0x04001EC7 RID: 7879
		private static readonly PropertyDefinition[] RequiredProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass
		};

		// Token: 0x04001EC8 RID: 7880
		private readonly IMailboxSession session;

		// Token: 0x04001EC9 RID: 7881
		private readonly IXSOFactory xsoFactory;

		// Token: 0x04001ECA RID: 7882
		private readonly PropertyDefinition[] properties;

		// Token: 0x04001ECB RID: 7883
		private readonly DefaultFolderType folderType;
	}
}
