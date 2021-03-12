using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000385 RID: 901
	internal sealed class FolderPickerTree : Tree
	{
		// Token: 0x06002214 RID: 8724 RVA: 0x000C284E File Offset: 0x000C0A4E
		private FolderPickerTree(UserContext userContext, InvisibleRootTreeNode rootNode, FolderTreeRenderType renderType) : base(userContext, rootNode)
		{
			this.renderType = renderType;
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x000C285F File Offset: 0x000C0A5F
		protected override void RenderAdditionalProperties(TextWriter writer)
		{
			base.RenderAdditionalProperties(writer);
			writer.Write(" _frt=");
			writer.Write((int)this.renderType);
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000C28DC File Offset: 0x000C0ADC
		internal static FolderPickerTree CreateFolderPickerTree(UserContext userContext, bool requirePublicFolderTree)
		{
			FolderPickerTree folderPickerTree = new FolderPickerTree(userContext, new InvisibleRootTreeNode(userContext), FolderTreeRenderType.HideSearchFolders);
			FolderTreeNode folderTreeNode = FolderTreeNode.CreateMailboxFolderTreeRootNode(userContext, userContext.MailboxSession, FolderTreeRenderType.HideSearchFolders);
			folderTreeNode.IsExpanded = true;
			folderTreeNode.Selected = true;
			FolderTreeNode folderTreeNode2 = folderTreeNode;
			folderTreeNode2.HighlightClassName += " trNdGpHdHl";
			folderPickerTree.RootNode.AddChild(folderTreeNode);
			if (!userContext.IsExplicitLogon && userContext.HasArchive)
			{
				FolderTreeNode archiveRootNode = null;
				if (userContext.ArchiveAccessed)
				{
					userContext.TryLoopArchiveMailboxes(delegate(MailboxSession archiveSession)
					{
						FolderTreeNode archiveRootNode = FolderTreeNode.CreateMailboxFolderTreeRootNode(userContext, archiveSession, FolderTreeRenderType.HideSearchFolders);
						archiveRootNode.IsExpanded = false;
						archiveRootNode = archiveRootNode;
						archiveRootNode.HighlightClassName += " trNdGpHdHl";
					});
				}
				else
				{
					archiveRootNode = FolderTreeNode.CreateFolderPickerDummyArchiveMailboxRootNode(userContext);
				}
				if (archiveRootNode != null)
				{
					folderPickerTree.RootNode.AddChild(archiveRootNode);
				}
			}
			if (requirePublicFolderTree && userContext.IsPublicFoldersAvailable())
			{
				FolderTreeNode folderTreeNode3 = FolderTreeNode.CreatePublicFolderTreeRootNode(userContext);
				folderTreeNode3.IsExpanded = true;
				FolderTreeNode folderTreeNode4 = folderTreeNode3;
				folderTreeNode4.HighlightClassName += " trNdGpHdHl";
				folderPickerTree.RootNode.AddChild(folderTreeNode3);
			}
			folderPickerTree.RootNode.IsExpanded = true;
			return folderPickerTree;
		}

		// Token: 0x040017FF RID: 6143
		private readonly FolderTreeRenderType renderType;
	}
}
