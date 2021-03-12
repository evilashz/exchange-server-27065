using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000386 RID: 902
	internal sealed class PublicFolderTree : FolderTree
	{
		// Token: 0x06002217 RID: 8727 RVA: 0x000C2A3F File Offset: 0x000C0C3F
		private PublicFolderTree(UserContext userContext, FolderTreeNode rootNode, FolderTreeRenderType renderType) : base(userContext, rootNode, renderType)
		{
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x000C2A4A File Offset: 0x000C0C4A
		protected override void RenderAdditionalProperties(TextWriter writer)
		{
			base.RenderAdditionalProperties(writer);
			writer.Write(" _fPF=1");
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x000C2A60 File Offset: 0x000C0C60
		internal static PublicFolderTree CreatePublicFolderRootTree(UserContext userContext)
		{
			PublicFolderTree publicFolderTree = new PublicFolderTree(userContext, FolderTreeNode.CreatePublicFolderTreeRootNode(userContext), FolderTreeRenderType.None);
			publicFolderTree.RootNode.IsExpanded = true;
			publicFolderTree.RootNode.Selected = true;
			FolderTreeNode rootNode = publicFolderTree.RootNode;
			rootNode.HighlightClassName += " trNdGpHdHl";
			return publicFolderTree;
		}
	}
}
