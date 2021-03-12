using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000383 RID: 899
	internal abstract class FolderTree : Tree
	{
		// Token: 0x06002209 RID: 8713 RVA: 0x000C25DE File Offset: 0x000C07DE
		internal FolderTree(UserContext userContext, FolderTreeNode rootNode, FolderTreeRenderType renderType) : base(userContext, rootNode)
		{
			this.renderType = renderType;
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x000C25EF File Offset: 0x000C07EF
		protected override void RenderAdditionalProperties(TextWriter writer)
		{
			base.RenderAdditionalProperties(writer);
			writer.Write(" _frt=");
			writer.Write((int)this.renderType);
		}

		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x000C260F File Offset: 0x000C080F
		internal new FolderTreeNode RootNode
		{
			get
			{
				return (FolderTreeNode)base.RootNode;
			}
		}

		// Token: 0x040017FA RID: 6138
		private readonly FolderTreeRenderType renderType;
	}
}
