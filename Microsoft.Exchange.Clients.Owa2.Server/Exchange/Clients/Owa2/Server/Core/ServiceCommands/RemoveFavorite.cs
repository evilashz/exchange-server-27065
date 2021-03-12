using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x02000348 RID: 840
	internal sealed class RemoveFavorite
	{
		// Token: 0x06001B87 RID: 7047 RVA: 0x00069910 File Offset: 0x00067B10
		internal RemoveFavorite(IXSOFactory xsoFactory, MailboxSession mailboxSession, RequestDetailsLogger requestDetailsLogger, ItemId personaId)
		{
			ArgumentValidator.ThrowIfNull("xsoFactory", xsoFactory);
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("requestDetailsLogger", requestDetailsLogger);
			ArgumentValidator.ThrowIfNull("personaId", personaId);
			this.xso = xsoFactory;
			this.session = mailboxSession;
			this.logger = requestDetailsLogger;
			this.personaId = personaId;
			this.utilities = new UnifiedContactStoreUtilities(this.session, this.xso);
			this.favoritesPdlId = this.utilities.GetSystemPdlId(UnifiedContactStoreUtilities.FavoritesPdlDisplayName, "IPM.DistList.MOC.Favorites");
			this.SetLogValue(RemoveFavoriteMetadata.PersonaId, personaId);
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x000699B0 File Offset: 0x00067BB0
		internal bool Execute()
		{
			StoreObjectId defaultFolderId = this.session.GetDefaultFolderId(DefaultFolderType.QuickContacts);
			PersonId personId = IdConverter.EwsIdToPersonId(this.personaId.GetId());
			AllPersonContactsEnumerator allPersonContactsEnumerator = AllPersonContactsEnumerator.Create(this.session, personId, RemoveFavorite.ContactProperties);
			ExTraceGlobals.RemoveFavoriteTracer.TraceDebug((long)this.GetHashCode(), "Processing contacts.");
			int num = 0;
			foreach (IStorePropertyBag storePropertyBag in allPersonContactsEnumerator)
			{
				byte[] valueOrDefault = storePropertyBag.GetValueOrDefault<byte[]>(StoreObjectSchema.ParentEntryId, null);
				StoreObjectId objA = StoreObjectId.FromProviderSpecificIdOrNull(valueOrDefault);
				if (object.Equals(objA, defaultFolderId))
				{
					num++;
					VersionedId valueOrDefault2 = storePropertyBag.GetValueOrDefault<VersionedId>(ItemSchema.Id, null);
					if (valueOrDefault2 != null)
					{
						StoreId objectId = valueOrDefault2.ObjectId;
						this.utilities.RemoveContactFromGroup(objectId, this.favoritesPdlId);
						this.UnsetIsFavoriteFlagIfContactExists(objectId);
					}
				}
			}
			this.SetLogValue(RemoveFavoriteMetadata.NumberOfContacts, num);
			if (num == 0)
			{
				ExTraceGlobals.RemoveFavoriteTracer.TraceDebug<ItemId>((long)this.GetHashCode(), "No contacts found with personId: {0}", this.personaId);
			}
			return true;
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x00069AD0 File Offset: 0x00067CD0
		private void UnsetIsFavoriteFlagIfContactExists(StoreId contactId)
		{
			try
			{
				using (IContact contact = this.xso.BindToContact(this.session, contactId, null))
				{
					ExTraceGlobals.RemoveFavoriteTracer.TraceDebug((long)this.GetHashCode(), "Unsetting the IsFavorite flag on the contact.");
					contact.OpenAsReadWrite();
					contact.IsFavorite = false;
					contact.Save(SaveMode.ResolveConflicts);
				}
			}
			catch (ObjectNotFoundException)
			{
				ExTraceGlobals.RemoveFavoriteTracer.TraceDebug((long)this.GetHashCode(), "Contact doesn't exist to unset the IsFavorite flag.");
			}
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x00069B60 File Offset: 0x00067D60
		private void SetLogValue(Enum key, object value)
		{
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(this.logger, key, value);
		}

		// Token: 0x04000F89 RID: 3977
		private static readonly PropertyDefinition[] ContactProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ParentEntryId
		};

		// Token: 0x04000F8A RID: 3978
		private readonly UnifiedContactStoreUtilities utilities;

		// Token: 0x04000F8B RID: 3979
		private readonly IXSOFactory xso;

		// Token: 0x04000F8C RID: 3980
		private readonly MailboxSession session;

		// Token: 0x04000F8D RID: 3981
		private readonly RequestDetailsLogger logger;

		// Token: 0x04000F8E RID: 3982
		private readonly ItemId personaId;

		// Token: 0x04000F8F RID: 3983
		private readonly StoreObjectId favoritesPdlId;
	}
}
