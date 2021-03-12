using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000869 RID: 2153
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ShortcutMessage : MessageItem
	{
		// Token: 0x170016B1 RID: 5809
		// (get) Token: 0x0600510A RID: 20746 RVA: 0x00151A0C File Offset: 0x0014FC0C
		// (set) Token: 0x0600510B RID: 20747 RVA: 0x00151A24 File Offset: 0x0014FC24
		public string FavoriteDisplayAlias
		{
			get
			{
				this.CheckDisposed("FavoriteDisplayAlias::get");
				return base.GetValueOrDefault<string>(ShortcutMessageSchema.FavoriteDisplayAlias);
			}
			set
			{
				this.CheckDisposed("FavoriteDisplayAlias::set");
				this[ShortcutMessageSchema.FavoriteDisplayAlias] = value;
			}
		}

		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x0600510C RID: 20748 RVA: 0x00151A3D File Offset: 0x0014FC3D
		// (set) Token: 0x0600510D RID: 20749 RVA: 0x00151A55 File Offset: 0x0014FC55
		public string FavoriteDisplayName
		{
			get
			{
				this.CheckDisposed("FavoriteDisplayName::get");
				return base.GetValueOrDefault<string>(ShortcutMessageSchema.FavoriteDisplayName);
			}
			set
			{
				this.CheckDisposed("FavoriteDisplayName::set");
				this[ShortcutMessageSchema.FavoriteDisplayName] = value;
			}
		}

		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x0600510E RID: 20750 RVA: 0x00151A6E File Offset: 0x0014FC6E
		// (set) Token: 0x0600510F RID: 20751 RVA: 0x00151A86 File Offset: 0x0014FC86
		public byte[] FavPublicSourceKey
		{
			get
			{
				this.CheckDisposed("FavPublicSourceKey::get");
				return base.GetValueOrDefault<byte[]>(ShortcutMessageSchema.FavPublicSourceKey);
			}
			set
			{
				this.CheckDisposed("FavPublicSourceKey::set");
				this[ShortcutMessageSchema.FavPublicSourceKey] = value;
			}
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x00151A9F File Offset: 0x0014FC9F
		internal ShortcutMessage(ICoreItem coreItem) : base(coreItem, false)
		{
			if (base.IsNew)
			{
				this.InitializeNewShortcutMessage();
			}
		}

		// Token: 0x06005111 RID: 20753 RVA: 0x00151AB8 File Offset: 0x0014FCB8
		public static ShortcutMessage Create(MailboxSession session, byte[] longTermFolderId, string folderName)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(longTermFolderId, "longTermFolderId");
			ShortcutMessage shortcutMessage = ItemBuilder.CreateNewItem<ShortcutMessage>(session, session.GetDefaultFolderId(DefaultFolderType.LegacyShortcuts), ItemCreateInfo.ShortcutMessageInfo, CreateMessageType.Normal);
			shortcutMessage.FavoriteDisplayName = folderName;
			shortcutMessage.FavPublicSourceKey = longTermFolderId;
			return shortcutMessage;
		}

		// Token: 0x06005112 RID: 20754 RVA: 0x00151AFF File Offset: 0x0014FCFF
		public static ShortcutMessage Bind(MailboxSession session, StoreId storeId)
		{
			return ShortcutMessage.Bind(session, storeId, null);
		}

		// Token: 0x06005113 RID: 20755 RVA: 0x00151B0C File Offset: 0x0014FD0C
		public static ShortcutMessage Bind(MailboxSession session, StoreId storeId, PropertyDefinition[] propsToReturn)
		{
			Util.ThrowOnNullArgument(session, "session");
			Util.ThrowOnNullArgument(storeId, "storeId");
			return ItemBuilder.ItemBind<ShortcutMessage>(session, storeId, ShortcutMessageSchema.Instance, propsToReturn);
		}

		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x06005114 RID: 20756 RVA: 0x00151B3E File Offset: 0x0014FD3E
		public override Schema Schema
		{
			get
			{
				this.CheckDisposed("Schema::get");
				return ShortcutMessageSchema.Instance;
			}
		}

		// Token: 0x06005115 RID: 20757 RVA: 0x00151B50 File Offset: 0x0014FD50
		private void InitializeNewShortcutMessage()
		{
			this[ItemSchema.FavLevelMask] = 1;
			this[InternalSchema.ItemClass] = "IPM.Note";
		}
	}
}
