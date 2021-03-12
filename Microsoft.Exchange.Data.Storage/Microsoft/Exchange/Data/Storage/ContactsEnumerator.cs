using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004D5 RID: 1237
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ContactsEnumerator<T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06003609 RID: 13833 RVA: 0x000D9BF4 File Offset: 0x000D7DF4
		private ContactsEnumerator(IMailboxSession session, DefaultFolderType folderType, SortBy[] sortColumns, PropertyDefinition[] properties, Func<IStorePropertyBag, T> converter, ContactsEnumerator<T>.SupportedContactItemClasses includedItemClasses, IXSOFactory xsoFactory)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(properties, "properties");
			Util.ThrowOnNullArgument(converter, "converter");
			Util.ThrowOnNullArgument(xsoFactory, "xsoFactory");
			this.session = session;
			this.folderType = folderType;
			this.sortColumns = sortColumns;
			this.properties = PropertyDefinitionCollection.Merge<PropertyDefinition>(ContactsEnumerator<T>.requiredProperties, properties);
			this.converter = converter;
			this.includedItemClasses = includedItemClasses;
			this.xsoFactory = xsoFactory;
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x000D9C75 File Offset: 0x000D7E75
		public static ContactsEnumerator<T> CreateContactsOnlyEnumerator(IMailboxSession session, DefaultFolderType folderType, PropertyDefinition[] properties, Func<IStorePropertyBag, T> converter, IXSOFactory xsoFactory)
		{
			return new ContactsEnumerator<T>(session, folderType, null, properties, converter, ContactsEnumerator<T>.SupportedContactItemClasses.Contacts, xsoFactory);
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x000D9C84 File Offset: 0x000D7E84
		public static ContactsEnumerator<T> CreateContactsOnlyEnumerator(IMailboxSession session, DefaultFolderType folderType, SortBy[] sortColumns, PropertyDefinition[] properties, Func<IStorePropertyBag, T> converter, IXSOFactory xsoFactory)
		{
			return new ContactsEnumerator<T>(session, folderType, sortColumns, properties, converter, ContactsEnumerator<T>.SupportedContactItemClasses.Contacts, xsoFactory);
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x000D9C94 File Offset: 0x000D7E94
		public static ContactsEnumerator<T> CreateContactsAndPdlsEnumerator(IMailboxSession session, DefaultFolderType folderType, PropertyDefinition[] properties, Func<IStorePropertyBag, T> converter, IXSOFactory xsoFactory)
		{
			return new ContactsEnumerator<T>(session, folderType, null, properties, converter, ContactsEnumerator<T>.SupportedContactItemClasses.Contacts | ContactsEnumerator<T>.SupportedContactItemClasses.Pdls, xsoFactory);
		}

		// Token: 0x0600360D RID: 13837 RVA: 0x000D9FA8 File Offset: 0x000D81A8
		public IEnumerator<T> GetEnumerator()
		{
			if (this.session.GetDefaultFolderId(this.folderType) == null)
			{
				this.session.CreateDefaultFolder(this.folderType);
			}
			using (IFolder allContactsFolder = this.xsoFactory.BindToFolder(this.session, this.folderType))
			{
				using (IQueryResult contactsQuery = allContactsFolder.IItemQuery(ItemQueryType.None, null, this.sortColumns, this.properties))
				{
					IStorePropertyBag[] contacts = contactsQuery.GetPropertyBags(100);
					while (contacts.Length > 0)
					{
						foreach (IStorePropertyBag contact in contacts)
						{
							if (contact != null && !(contact.TryGetProperty(ItemSchema.Id) is PropertyError) && this.ShouldEnumerateItemClass(contact))
							{
								yield return this.converter(contact);
							}
						}
						contacts = contactsQuery.GetPropertyBags(100);
					}
				}
			}
			yield break;
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x000D9FC4 File Offset: 0x000D81C4
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotSupportedException("Must use the generic version of GetEnumerator.");
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x000D9FD0 File Offset: 0x000D81D0
		private bool ShouldEnumerateItemClass(IStorePropertyBag contactPropertyBag)
		{
			string text = contactPropertyBag.TryGetProperty(StoreObjectSchema.ItemClass) as string;
			if (!string.IsNullOrEmpty(text))
			{
				if (ObjectClass.IsContact(text))
				{
					return (this.includedItemClasses & ContactsEnumerator<T>.SupportedContactItemClasses.Contacts) == ContactsEnumerator<T>.SupportedContactItemClasses.Contacts;
				}
				if (ObjectClass.IsDistributionList(text))
				{
					return (this.includedItemClasses & ContactsEnumerator<T>.SupportedContactItemClasses.Pdls) == ContactsEnumerator<T>.SupportedContactItemClasses.Pdls;
				}
			}
			return false;
		}

		// Token: 0x04001CF7 RID: 7415
		private const int ChunkSize = 100;

		// Token: 0x04001CF8 RID: 7416
		private static readonly PropertyDefinition[] requiredProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass
		};

		// Token: 0x04001CF9 RID: 7417
		private readonly IXSOFactory xsoFactory;

		// Token: 0x04001CFA RID: 7418
		private readonly IMailboxSession session;

		// Token: 0x04001CFB RID: 7419
		private readonly PropertyDefinition[] properties;

		// Token: 0x04001CFC RID: 7420
		private readonly DefaultFolderType folderType;

		// Token: 0x04001CFD RID: 7421
		private readonly Func<IStorePropertyBag, T> converter;

		// Token: 0x04001CFE RID: 7422
		private readonly ContactsEnumerator<T>.SupportedContactItemClasses includedItemClasses;

		// Token: 0x04001CFF RID: 7423
		private readonly SortBy[] sortColumns;

		// Token: 0x020004D6 RID: 1238
		[Flags]
		private enum SupportedContactItemClasses
		{
			// Token: 0x04001D01 RID: 7425
			None = 0,
			// Token: 0x04001D02 RID: 7426
			Contacts = 1,
			// Token: 0x04001D03 RID: 7427
			Pdls = 2
		}
	}
}
