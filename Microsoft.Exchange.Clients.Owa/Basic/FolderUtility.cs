using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x020000AF RID: 175
	internal sealed class FolderUtility
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x00034054 File Offset: 0x00032254
		public static ContentCountDisplay GetContentCountDisplay(object extendedFolderFlagValue, StoreObjectId folderId)
		{
			if (folderId == null)
			{
				throw new ArgumentNullException("FolderID");
			}
			if (extendedFolderFlagValue != null && extendedFolderFlagValue is ExtendedFolderFlags)
			{
				ExtendedFolderFlags valueToTest = (ExtendedFolderFlags)extendedFolderFlagValue;
				if (Utilities.IsFlagSet((int)valueToTest, 2))
				{
					return ContentCountDisplay.ItemCount;
				}
				if (Utilities.IsFlagSet((int)valueToTest, 1))
				{
					return ContentCountDisplay.UnreadCount;
				}
			}
			return FolderUtility.GetDefaultContentCountDisplay(folderId);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000340A0 File Offset: 0x000322A0
		public static ContentCountDisplay GetDefaultContentCountDisplay(StoreObjectId folderId)
		{
			DefaultFolderType defaultFolderType = Utilities.GetDefaultFolderType(UserContextManager.GetUserContext().MailboxSession, folderId);
			if (defaultFolderType == DefaultFolderType.Root)
			{
				return ContentCountDisplay.None;
			}
			if (defaultFolderType == DefaultFolderType.Outbox || defaultFolderType == DefaultFolderType.Drafts || defaultFolderType == DefaultFolderType.JunkEmail)
			{
				return ContentCountDisplay.ItemCount;
			}
			return ContentCountDisplay.UnreadCount;
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x000340D8 File Offset: 0x000322D8
		public static bool IsPrimaryMailFolder(StoreObjectId id, UserContext userContext)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			return id.Equals(userContext.GetDeletedItemsFolderId(userContext.MailboxSession).StoreObjectId) || id.Equals(userContext.DraftsFolderId) || id.Equals(userContext.InboxFolderId) || id.Equals(userContext.JunkEmailFolderId) || id.Equals(userContext.SentItemsFolderId);
		}
	}
}
