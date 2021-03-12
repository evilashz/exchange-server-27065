using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020004B4 RID: 1204
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class ContactLinkingLogSchema
	{
		// Token: 0x020004B5 RID: 1205
		internal enum ContactUpdate
		{
			// Token: 0x04001C6A RID: 7274
			ItemId,
			// Token: 0x04001C6B RID: 7275
			PersonId,
			// Token: 0x04001C6C RID: 7276
			Linked,
			// Token: 0x04001C6D RID: 7277
			LinkRejectHistory,
			// Token: 0x04001C6E RID: 7278
			GALLinkState,
			// Token: 0x04001C6F RID: 7279
			GALLinkID,
			// Token: 0x04001C70 RID: 7280
			AddressBookEntryId,
			// Token: 0x04001C71 RID: 7281
			SmtpAddressCache,
			// Token: 0x04001C72 RID: 7282
			UserApprovedLink
		}

		// Token: 0x020004B6 RID: 1206
		internal enum ContactLinking
		{
			// Token: 0x04001C74 RID: 7284
			LinkOperation,
			// Token: 0x04001C75 RID: 7285
			LinkingPersonId,
			// Token: 0x04001C76 RID: 7286
			LinkingItemId,
			// Token: 0x04001C77 RID: 7287
			LinkToPersonId,
			// Token: 0x04001C78 RID: 7288
			LinkToItemId
		}

		// Token: 0x020004B7 RID: 1207
		internal enum ContactUnlinking
		{
			// Token: 0x04001C7A RID: 7290
			PersonId,
			// Token: 0x04001C7B RID: 7291
			ItemId,
			// Token: 0x04001C7C RID: 7292
			ADObjectIdGuid
		}

		// Token: 0x020004B8 RID: 1208
		internal enum GALLinkFixup
		{
			// Token: 0x04001C7E RID: 7294
			PersonId,
			// Token: 0x04001C7F RID: 7295
			ADObjectIdGuid
		}

		// Token: 0x020004B9 RID: 1209
		internal enum RejectSuggestion
		{
			// Token: 0x04001C81 RID: 7297
			PersonId,
			// Token: 0x04001C82 RID: 7298
			SuggestionPersonId
		}

		// Token: 0x020004BA RID: 1210
		internal enum Error
		{
			// Token: 0x04001C84 RID: 7300
			Exception,
			// Token: 0x04001C85 RID: 7301
			Context
		}

		// Token: 0x020004BB RID: 1211
		internal enum Warning
		{
			// Token: 0x04001C87 RID: 7303
			Context
		}

		// Token: 0x020004BC RID: 1212
		internal enum SkippedContactLink
		{
			// Token: 0x04001C89 RID: 7305
			LinkingPersonId,
			// Token: 0x04001C8A RID: 7306
			LinkingItemId,
			// Token: 0x04001C8B RID: 7307
			LinkToPersonId,
			// Token: 0x04001C8C RID: 7308
			LinkToItemId,
			// Token: 0x04001C8D RID: 7309
			LinkToPersonContactCount,
			// Token: 0x04001C8E RID: 7310
			CurrentCountOfContactsAdded,
			// Token: 0x04001C8F RID: 7311
			MaximumContactsAllowedToAdd
		}

		// Token: 0x020004BD RID: 1213
		internal enum PerformanceData
		{
			// Token: 0x04001C91 RID: 7313
			Elapsed,
			// Token: 0x04001C92 RID: 7314
			CPU,
			// Token: 0x04001C93 RID: 7315
			RPCCount,
			// Token: 0x04001C94 RID: 7316
			RPCLatency,
			// Token: 0x04001C95 RID: 7317
			DirectoryCount,
			// Token: 0x04001C96 RID: 7318
			DirectoryLatency,
			// Token: 0x04001C97 RID: 7319
			StoreTimeInServer,
			// Token: 0x04001C98 RID: 7320
			StoreTimeInCPU,
			// Token: 0x04001C99 RID: 7321
			StorePagesRead,
			// Token: 0x04001C9A RID: 7322
			StorePagesPreRead,
			// Token: 0x04001C9B RID: 7323
			StoreLogRecords,
			// Token: 0x04001C9C RID: 7324
			StoreLogBytes,
			// Token: 0x04001C9D RID: 7325
			ContactsCreated,
			// Token: 0x04001C9E RID: 7326
			ContactsUpdated,
			// Token: 0x04001C9F RID: 7327
			ContactsRead,
			// Token: 0x04001CA0 RID: 7328
			ContactsProcessed
		}

		// Token: 0x020004BE RID: 1214
		internal enum MigrationStart
		{
			// Token: 0x04001CA2 RID: 7330
			DueTime
		}

		// Token: 0x020004BF RID: 1215
		internal enum MigrationEnd
		{
			// Token: 0x04001CA4 RID: 7332
			Success
		}
	}
}
