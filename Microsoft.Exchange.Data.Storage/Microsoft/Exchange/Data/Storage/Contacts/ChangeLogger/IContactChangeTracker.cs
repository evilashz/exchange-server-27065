using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Contacts.ChangeLogger
{
	// Token: 0x0200055A RID: 1370
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IContactChangeTracker
	{
		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x060039CA RID: 14794
		string Name { get; }

		// Token: 0x060039CB RID: 14795
		bool ShouldLoadPropertiesForFurtherCheck(COWTriggerAction operation, string itemClass, StoreObjectId itemId, CoreItem item);

		// Token: 0x060039CC RID: 14796
		StorePropertyDefinition[] GetProperties(StoreObjectId itemId, CoreItem item);

		// Token: 0x060039CD RID: 14797
		bool ShouldLogContact(StoreObjectId itemId, CoreItem item);

		// Token: 0x060039CE RID: 14798
		bool ShouldLogGroupOperation(COWTriggerAction operation, StoreSession sourceSession, StoreObjectId sourceFolderId, StoreSession destinationSession, StoreObjectId destinationFolderId, ICollection<StoreObjectId> itemIds);
	}
}
