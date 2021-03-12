using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000384 RID: 900
	internal class MailboxFolderTree : FolderTree
	{
		// Token: 0x0600220C RID: 8716 RVA: 0x000C261C File Offset: 0x000C081C
		private MailboxFolderTree(UserContext userContext, FolderTreeNode rootNode, FolderTreeRenderType renderType) : base(userContext, rootNode, renderType)
		{
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x000C2628 File Offset: 0x000C0828
		private MailboxFolderTree(UserContext userContext, MailboxSession mailboxSession, FolderTreeNode rootNode, FolderTreeRenderType renderType) : base(userContext, rootNode, renderType)
		{
			if (mailboxSession == null)
			{
				throw new ArgumentNullException("mailboxSession");
			}
			if (!mailboxSession.MailboxOwner.MailboxInfo.IsArchive)
			{
				this.defaultCalendarFolderId = Utilities.TryGetDefaultFolderId(mailboxSession, DefaultFolderType.Calendar);
				this.defaultContactFolderId = Utilities.TryGetDefaultFolderId(mailboxSession, DefaultFolderType.Contacts);
				this.defaultTaskFolderId = Utilities.TryGetDefaultFolderId(mailboxSession, DefaultFolderType.Tasks);
			}
			this.isRemote = mailboxSession.MailboxOwner.MailboxInfo.IsRemote;
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x000C26A0 File Offset: 0x000C08A0
		protected override void RenderAdditionalProperties(TextWriter writer)
		{
			base.RenderAdditionalProperties(writer);
			if (this.defaultCalendarFolderId != null)
			{
				writer.Write(" _DfCal=\"");
				Utilities.HtmlEncode(this.defaultCalendarFolderId.ToBase64String(), writer);
				writer.Write("\"");
			}
			if (this.defaultContactFolderId != null)
			{
				writer.Write(" _DfCnt=\"");
				Utilities.HtmlEncode(this.defaultContactFolderId.ToBase64String(), writer);
				writer.Write("\"");
			}
			if (this.defaultTaskFolderId != null)
			{
				writer.Write(" _DfTsk=\"");
				Utilities.HtmlEncode(this.defaultTaskFolderId.ToBase64String(), writer);
				writer.Write("\"");
			}
			if (this.isRemote)
			{
				writer.Write(" _IsRemote=\"1\"");
			}
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x000C2754 File Offset: 0x000C0954
		internal static MailboxFolderTree CreateOtherMailboxFolderTree(UserContext userContext, OtherMailboxConfigEntry entry, bool isExpanded)
		{
			FolderTreeNode folderTreeNode = FolderTreeNode.CreateOtherMailboxRootNode(userContext, entry, isExpanded);
			if (folderTreeNode == null)
			{
				return null;
			}
			folderTreeNode.IsExpanded = isExpanded;
			FolderTreeNode folderTreeNode2 = folderTreeNode;
			folderTreeNode2.HighlightClassName += " trNdGpHdHl";
			return new MailboxFolderTree(userContext, folderTreeNode, FolderTreeRenderType.None);
		}

		// Token: 0x06002210 RID: 8720 RVA: 0x000C2794 File Offset: 0x000C0994
		internal static MailboxFolderTree CreateMailboxFolderTree(UserContext userContext, MailboxSession mailboxSession, FolderTreeRenderType renderType, bool selectInbox)
		{
			MailboxFolderTree mailboxFolderTree = new MailboxFolderTree(userContext, mailboxSession, FolderTreeNode.CreateMailboxFolderTreeRootNode(userContext, mailboxSession, renderType), renderType);
			mailboxFolderTree.RootNode.IsExpanded = true;
			FolderTreeNode rootNode = mailboxFolderTree.RootNode;
			rootNode.HighlightClassName += " trNdGpHdHl";
			if (selectInbox)
			{
				StoreObjectId defaultFolderId = Utilities.GetDefaultFolderId(mailboxSession, DefaultFolderType.Inbox);
				OwaStoreObjectId folderId;
				if (userContext.IsMyMailbox(mailboxSession))
				{
					folderId = OwaStoreObjectId.CreateFromMailboxFolderId(defaultFolderId);
				}
				else
				{
					folderId = OwaStoreObjectId.CreateFromOtherUserMailboxFolderId(defaultFolderId, mailboxSession.MailboxOwnerLegacyDN);
				}
				mailboxFolderTree.RootNode.SelectSpecifiedFolder(folderId);
			}
			return mailboxFolderTree;
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000C2811 File Offset: 0x000C0A11
		internal static MailboxFolderTree CreateStartPageMailboxFolderTree(UserContext userContext, FolderList deepHierarchyFolderList, FolderList searchFolderList)
		{
			return new MailboxFolderTree(userContext, userContext.MailboxSession, FolderTreeNode.CreateStartPageMailboxRootNode(userContext, deepHierarchyFolderList, searchFolderList), FolderTreeRenderType.HideGeekFoldersWithSpecificOrder);
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000C2828 File Offset: 0x000C0A28
		internal static MailboxFolderTree CreateStartPageArchiveMailboxFolderTree(UserContext userContext, FolderList deepHierarchyFolderList, FolderList searchFolderList)
		{
			return new MailboxFolderTree(userContext, deepHierarchyFolderList.MailboxSession, FolderTreeNode.CreateStartPageArchiveMailboxRootNode(userContext, deepHierarchyFolderList, searchFolderList), FolderTreeRenderType.HideGeekFoldersWithSpecificOrder);
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x000C283F File Offset: 0x000C0A3F
		internal static MailboxFolderTree CreateStartPageDummyArchiveMailboxFolderTree(UserContext userContext)
		{
			return new MailboxFolderTree(userContext, FolderTreeNode.CreateStartPageDummyArchiveMailboxRootNode(userContext), FolderTreeRenderType.HideGeekFoldersWithSpecificOrder);
		}

		// Token: 0x040017FB RID: 6139
		private readonly StoreObjectId defaultCalendarFolderId;

		// Token: 0x040017FC RID: 6140
		private readonly StoreObjectId defaultContactFolderId;

		// Token: 0x040017FD RID: 6141
		private readonly StoreObjectId defaultTaskFolderId;

		// Token: 0x040017FE RID: 6142
		private readonly bool isRemote;
	}
}
