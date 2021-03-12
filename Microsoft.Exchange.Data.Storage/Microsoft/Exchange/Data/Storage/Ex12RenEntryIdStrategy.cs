using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000043 RID: 67
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class Ex12RenEntryIdStrategy : LocationEntryIdStrategy
	{
		// Token: 0x0600058E RID: 1422 RVA: 0x0002D73A File Offset: 0x0002B93A
		internal Ex12RenEntryIdStrategy(StorePropertyDefinition property, LocationEntryIdStrategy.GetLocationPropertyBagDelegate getLocationPropertyBag, Ex12RenEntryIdStrategy.PersistenceId persistenceId) : base(property, getLocationPropertyBag)
		{
			this.persistenceId = persistenceId;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0002D74C File Offset: 0x0002B94C
		internal override byte[] GetEntryId(DefaultFolderContext context)
		{
			Ex12ExRenEntryParser ex12ExRenEntryParser = Ex12ExRenEntryParser.FromBytes(this.GetLocationPropertyBag(context).TryGetProperty(this.Property) as byte[]);
			return ex12ExRenEntryParser.GetEntryId(this.persistenceId);
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0002D788 File Offset: 0x0002B988
		internal override void SetEntryId(DefaultFolderContext context, byte[] entryId)
		{
			Ex12ExRenEntryParser ex12ExRenEntryParser = Ex12ExRenEntryParser.FromBytes(this.GetLocationPropertyBag(context).TryGetProperty(this.Property) as byte[]);
			ex12ExRenEntryParser.Insert(this.persistenceId, entryId);
			base.SetEntryId(context, ex12ExRenEntryParser.ToBytes());
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0002D7D4 File Offset: 0x0002B9D4
		internal override FolderSaveResult UnsetEntryId(DefaultFolderContext context)
		{
			Ex12ExRenEntryParser ex12ExRenEntryParser = Ex12ExRenEntryParser.FromBytes(this.GetLocationPropertyBag(context).TryGetProperty(this.Property) as byte[]);
			ex12ExRenEntryParser.Remove(this.persistenceId);
			base.SetEntryId(context, ex12ExRenEntryParser.ToBytes());
			return FolderPropertyBag.SuccessfulSave;
		}

		// Token: 0x04000175 RID: 373
		private Ex12RenEntryIdStrategy.PersistenceId persistenceId;

		// Token: 0x02000044 RID: 68
		internal enum PersistenceId : ushort
		{
			// Token: 0x04000177 RID: 375
			Mask = 32768,
			// Token: 0x04000178 RID: 376
			RssSubscriptions,
			// Token: 0x04000179 RID: 377
			SendAndTrack,
			// Token: 0x0400017A RID: 378
			InfoMail,
			// Token: 0x0400017B RID: 379
			ToDoSearch,
			// Token: 0x0400017C RID: 380
			ToDoSearchOffline,
			// Token: 0x0400017D RID: 381
			ConversationActions,
			// Token: 0x0400017E RID: 382
			ImContactList = 32778,
			// Token: 0x0400017F RID: 383
			QuickContacts
		}
	}
}
