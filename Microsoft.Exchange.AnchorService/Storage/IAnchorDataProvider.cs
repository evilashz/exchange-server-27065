using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService.Storage
{
	// Token: 0x0200002A RID: 42
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAnchorDataProvider : IDisposable
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001C4 RID: 452
		IAnchorADProvider ADProvider { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001C5 RID: 453
		string TenantName { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001C6 RID: 454
		string MailboxName { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001C7 RID: 455
		Guid MdbGuid { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001C8 RID: 456
		IAnchorStoreObject Folder { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001C9 RID: 457
		ADObjectId OwnerId { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001CA RID: 458
		OrganizationId OrganizationId { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001CB RID: 459
		AnchorContext AnchorContext { get; }

		// Token: 0x060001CC RID: 460
		IAnchorStoreObject GetFolderByName(string folderName, PropertyDefinition[] properties);

		// Token: 0x060001CD RID: 461
		IAnchorMessageItem CreateMessage();

		// Token: 0x060001CE RID: 462
		IAnchorEmailMessageItem CreateEmailMessage();

		// Token: 0x060001CF RID: 463
		void RemoveMessage(StoreObjectId messageId);

		// Token: 0x060001D0 RID: 464
		bool MoveMessageItems(StoreObjectId[] itemsToMove, string folderName);

		// Token: 0x060001D1 RID: 465
		IAnchorDataProvider GetProviderForFolder(AnchorContext context, string folderName);

		// Token: 0x060001D2 RID: 466
		IEnumerable<StoreObjectId> FindMessageIds(QueryFilter queryFilter, PropertyDefinition[] properties, SortBy[] sortBy, AnchorRowSelector rowSelector, int? maxCount);

		// Token: 0x060001D3 RID: 467
		IAnchorMessageItem FindMessage(StoreObjectId messageId, PropertyDefinition[] properties);
	}
}
