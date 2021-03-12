using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003CE RID: 974
	internal class NavigationGroupHeaderTreeNode : TreeNode
	{
		// Token: 0x06002426 RID: 9254 RVA: 0x000D09A4 File Offset: 0x000CEBA4
		internal NavigationGroupHeaderTreeNode(UserContext userContext, NavigationNodeGroup group) : base(userContext)
		{
			this.group = group;
			if (group.NavigationNodeId != null)
			{
				this.id = "f" + group.NavigationNodeId.ObjectId.ToString();
			}
			else
			{
				this.id = "f" + Convert.ToBase64String(Guid.NewGuid().ToByteArray());
			}
			base.IsExpanded = group.IsExpanded;
			base.HighlightClassName += " trNdGpHdHl";
			base.NodeClassName += " trNdGpHd";
			base.IsRootNode = true;
			if (group.NavigationNodeGroupSection != NavigationNodeGroupSection.First)
			{
				base.ChildIndent = 0;
			}
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x000D0A5A File Offset: 0x000CEC5A
		protected override void RenderAdditionalProperties(TextWriter writer)
		{
			writer.Write(" _t=\"navigationGroupHeaderNode\"");
			base.RenderAdditionalProperties(writer);
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x000D0A6E File Offset: 0x000CEC6E
		protected override void RenderContent(TextWriter writer)
		{
			Utilities.HtmlEncode(this.group.Subject, writer, true);
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x06002429 RID: 9257 RVA: 0x000D0A82 File Offset: 0x000CEC82
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x0600242A RID: 9258 RVA: 0x000D0A8A File Offset: 0x000CEC8A
		internal override bool Selectable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001913 RID: 6419
		private readonly NavigationNodeGroup group;

		// Token: 0x04001914 RID: 6420
		private string id;
	}
}
