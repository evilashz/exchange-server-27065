using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001DE RID: 478
	internal class OwaItemBinder : StoreSession.IItemBinder
	{
		// Token: 0x06000F5B RID: 3931 RVA: 0x0005F50A File Offset: 0x0005D70A
		public OwaItemBinder(UserContext userContext)
		{
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			this.userContext = userContext;
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0005F528 File Offset: 0x0005D728
		public Item BindItem(StoreObjectId itemId, bool isPublic, StoreObjectId folderId)
		{
			OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromItemId(itemId, OwaStoreObjectId.CreateFromFolderId(folderId, isPublic ? OwaStoreObjectIdType.PublicStoreFolder : OwaStoreObjectIdType.MailBoxObject));
			try
			{
				return Utilities.GetItem<Item>(this.userContext, owaStoreObjectId, new PropertyDefinition[0]);
			}
			catch (StoragePermanentException)
			{
			}
			catch (StorageTransientException)
			{
			}
			catch (OwaPermanentException)
			{
			}
			catch (OwaTransientException)
			{
			}
			return null;
		}

		// Token: 0x04000A59 RID: 2649
		private UserContext userContext;
	}
}
