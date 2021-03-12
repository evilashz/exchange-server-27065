using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000388 RID: 904
	internal abstract class TreeNode
	{
		// Token: 0x0600221C RID: 8732 RVA: 0x000C2D18 File Offset: 0x000C0F18
		protected TreeNode(UserContext userContext)
		{
			this.UserContext = userContext;
			this.ChildIndent = 8;
		}

		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x000C2D65 File Offset: 0x000C0F65
		// (set) Token: 0x0600221E RID: 8734 RVA: 0x000C2D6D File Offset: 0x000C0F6D
		private protected UserContext UserContext { protected get; private set; }

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x0600221F RID: 8735
		public abstract string Id { get; }

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x000C2D76 File Offset: 0x000C0F76
		internal List<TreeNode> Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06002221 RID: 8737 RVA: 0x000C2D7E File Offset: 0x000C0F7E
		// (set) Token: 0x06002222 RID: 8738 RVA: 0x000C2D86 File Offset: 0x000C0F86
		internal int ChildIndent { get; set; }

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06002223 RID: 8739 RVA: 0x000C2D8F File Offset: 0x000C0F8F
		protected virtual bool ContentVisible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002224 RID: 8740 RVA: 0x000C2D92 File Offset: 0x000C0F92
		internal virtual bool Selectable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06002225 RID: 8741 RVA: 0x000C2D95 File Offset: 0x000C0F95
		protected virtual bool Visible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x06002226 RID: 8742 RVA: 0x000C2D98 File Offset: 0x000C0F98
		internal virtual bool HasChildren
		{
			get
			{
				return this.Children.Count > 0;
			}
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06002227 RID: 8743 RVA: 0x000C2DA8 File Offset: 0x000C0FA8
		// (set) Token: 0x06002228 RID: 8744 RVA: 0x000C2DB0 File Offset: 0x000C0FB0
		internal TreeNode Parent
		{
			get
			{
				return this.parent;
			}
			private set
			{
				this.parent = value;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002229 RID: 8745 RVA: 0x000C2DB9 File Offset: 0x000C0FB9
		// (set) Token: 0x0600222A RID: 8746 RVA: 0x000C2DC1 File Offset: 0x000C0FC1
		internal bool Selected
		{
			get
			{
				return this.selected;
			}
			set
			{
				this.selected = value;
			}
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x0600222B RID: 8747 RVA: 0x000C2DCA File Offset: 0x000C0FCA
		// (set) Token: 0x0600222C RID: 8748 RVA: 0x000C2DD2 File Offset: 0x000C0FD2
		internal string HighlightClassName
		{
			get
			{
				return this.highlightClassName;
			}
			set
			{
				this.highlightClassName = value;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x000C2DDB File Offset: 0x000C0FDB
		// (set) Token: 0x0600222E RID: 8750 RVA: 0x000C2DE3 File Offset: 0x000C0FE3
		internal string NodeClassName
		{
			get
			{
				return this.nodeClassName;
			}
			set
			{
				this.nodeClassName = value;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x0600222F RID: 8751 RVA: 0x000C2DEC File Offset: 0x000C0FEC
		// (set) Token: 0x06002230 RID: 8752 RVA: 0x000C2DF4 File Offset: 0x000C0FF4
		internal bool IsDummy
		{
			get
			{
				return this.isDummy;
			}
			set
			{
				this.isDummy = value;
			}
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06002231 RID: 8753 RVA: 0x000C2E00 File Offset: 0x000C1000
		protected ThemeFileId GetECIcon
		{
			get
			{
				ThemeFileId result = ThemeFileId.Clear;
				if (this.HasChildren)
				{
					if (this.IsExpanded)
					{
						if (this.UserContext.IsRtl)
						{
							result = ThemeFileId.MinusRTL;
						}
						else
						{
							result = ThemeFileId.Minus;
						}
					}
					else if (this.UserContext.IsRtl)
					{
						result = ThemeFileId.PlusRTL;
					}
					else
					{
						result = ThemeFileId.Plus;
					}
				}
				return result;
			}
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002232 RID: 8754 RVA: 0x000C2E59 File Offset: 0x000C1059
		// (set) Token: 0x06002233 RID: 8755 RVA: 0x000C2E61 File Offset: 0x000C1061
		protected virtual bool HasIcon
		{
			get
			{
				return this.hasIcon;
			}
			set
			{
				this.hasIcon = value;
			}
		}

		// Token: 0x17000927 RID: 2343
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x000C2E6C File Offset: 0x000C106C
		// (set) Token: 0x06002235 RID: 8757 RVA: 0x000C2EA9 File Offset: 0x000C10A9
		internal bool NeedSync
		{
			get
			{
				bool? flag = this.needSync;
				if (flag == null)
				{
					return this.HasChildren && this.Children.Count == 0;
				}
				return flag.GetValueOrDefault();
			}
			set
			{
				this.needSync = new bool?(value);
			}
		}

		// Token: 0x17000928 RID: 2344
		// (get) Token: 0x06002236 RID: 8758 RVA: 0x000C2EB7 File Offset: 0x000C10B7
		// (set) Token: 0x06002237 RID: 8759 RVA: 0x000C2EBF File Offset: 0x000C10BF
		internal bool IsExpanded
		{
			get
			{
				return this.isExpanded;
			}
			set
			{
				this.isExpanded = value;
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06002238 RID: 8760 RVA: 0x000C2EC8 File Offset: 0x000C10C8
		// (set) Token: 0x06002239 RID: 8761 RVA: 0x000C2ED0 File Offset: 0x000C10D0
		internal bool IsRootNode
		{
			get
			{
				return this.isRootNode;
			}
			set
			{
				this.isRootNode = value;
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x000C2ED9 File Offset: 0x000C10D9
		// (set) Token: 0x0600223B RID: 8763 RVA: 0x000C2EE1 File Offset: 0x000C10E1
		internal string CustomAttributes
		{
			get
			{
				return this.customAttributes;
			}
			set
			{
				this.customAttributes = value;
			}
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x000C2EEC File Offset: 0x000C10EC
		internal static void WrapTreeNodeStart(TextWriter writer, UserContext userContext, string nodeID, string highlightClassName, bool contentVisibility, bool hasChildren, bool selected, bool isRootNode, int indent, ThemeFileId expandCollapseIcon, bool isRtl)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (userContext == null)
			{
				throw new ArgumentNullException("userContext");
			}
			writer.Write("<div id=\"divTrNdO\"");
			if (isRootNode)
			{
				writer.Write(" root=\"1\"");
			}
			writer.Write(">");
			writer.Write("<div id=\"divTrNdHl\" class=\"");
			writer.Write(highlightClassName);
			writer.Write("\"");
			if (!contentVisibility)
			{
				writer.Write("style=\"display:none\"");
			}
			writer.Write(">");
			if (hasChildren && contentVisibility)
			{
				userContext.RenderThemeImage(writer, expandCollapseIcon, null, new object[]
				{
					"id=ec",
					string.Format("style=\"{0}:{1}px\"", isRtl ? "right" : "left", indent)
				});
			}
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x000C2FB8 File Offset: 0x000C11B8
		internal static void WrapTreeNodeEndToChild(TextWriter writer, string nodeID, bool hasChildren, bool displayChildren, bool isRootNode)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write("</div>");
			if (hasChildren)
			{
				writer.Write("<div id=\"divTrNdCC\"");
				if (!displayChildren)
				{
					writer.Write(" style=\"display:none;\"");
				}
				else if (isRootNode)
				{
					writer.Write("style=\"padding-bottom:9px;\"");
				}
				writer.Write(">");
			}
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x000C3016 File Offset: 0x000C1216
		internal static void WrapTreeNodeEnd(TextWriter writer, bool hasChildren)
		{
			if (hasChildren)
			{
				writer.Write("</div>");
			}
			writer.Write("</div>");
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x000C3031 File Offset: 0x000C1231
		internal void AddChild(TreeNode child)
		{
			this.Children.Add(child);
			child.Parent = this;
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x000C3048 File Offset: 0x000C1248
		internal bool SelectSpecifiedFolder(OwaStoreObjectId folderId)
		{
			foreach (TreeNode treeNode in this.Children)
			{
				FolderTreeNode folderTreeNode = treeNode as FolderTreeNode;
				if (folderTreeNode != null && folderId.Equals(folderTreeNode.FolderId))
				{
					folderTreeNode.Selected = true;
					return true;
				}
				if (treeNode.SelectSpecifiedFolder(folderId))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x000C30C8 File Offset: 0x000C12C8
		protected virtual void RenderAdditionalProperties(TextWriter writer)
		{
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x000C30CA File Offset: 0x000C12CA
		protected virtual void RenderIcon(TextWriter writer, params string[] extraAttributes)
		{
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x000C30CC File Offset: 0x000C12CC
		protected virtual void RenderContent(TextWriter writer)
		{
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x000C30D0 File Offset: 0x000C12D0
		internal void RenderUndecoratedChildrenNode(TextWriter writer)
		{
			foreach (TreeNode treeNode in this.Children)
			{
				treeNode.RenderUndecoratedNode(writer);
			}
		}

		// Token: 0x06002245 RID: 8773 RVA: 0x000C3124 File Offset: 0x000C1324
		internal void RenderUndecoratedNode(TextWriter writer)
		{
			this.RenderUndecoratedNode(writer, 0);
		}

		// Token: 0x06002246 RID: 8774 RVA: 0x000C3130 File Offset: 0x000C1330
		internal void RenderUndecoratedNode(TextWriter writer, int indent)
		{
			writer.Write("<a class=\"");
			writer.Write(this.NodeClassName);
			writer.Write("\" hideFocus=1 href=\"#\"");
			writer.Write(" style=\"");
			if (!this.ContentVisible)
			{
				writer.Write("display:none;");
			}
			else
			{
				writer.Write(this.UserContext.IsRtl ? "right:" : "left:");
				writer.Write(indent + 21);
				writer.Write("px;");
			}
			writer.Write("\" id=\"");
			Utilities.HtmlEncode(this.Id, writer);
			writer.Write("\"");
			writer.Write(" _indnt=");
			writer.Write(indent);
			if (!this.Selectable)
			{
				writer.Write(" _nosel=1");
			}
			if (!string.IsNullOrEmpty(this.HighlightClassName))
			{
				writer.Write(" _hlCls=\"");
				writer.Write(this.HighlightClassName);
				writer.Write("\"");
			}
			if (this.Selected)
			{
				writer.Write(" _sel=1");
			}
			if (this.IsExpanded)
			{
				writer.Write(" _exp=1");
			}
			if (this.NeedSync)
			{
				writer.Write(" _sync=1");
			}
			if (this.HasChildren)
			{
				writer.Write(" _hsChld=1");
			}
			if (this.IsDummy)
			{
				writer.Write(" _dummy=1");
			}
			writer.Write(" _chldIndnt=");
			writer.Write(this.ChildIndent);
			if (!string.IsNullOrEmpty(this.CustomAttributes))
			{
				writer.Write(" ");
				writer.Write(this.CustomAttributes);
			}
			this.RenderAdditionalProperties(writer);
			writer.Write(">");
			this.RenderNodeBody(writer);
			writer.Write("</a>");
		}

		// Token: 0x06002247 RID: 8775 RVA: 0x000C32E8 File Offset: 0x000C14E8
		internal virtual void RenderNodeBody(TextWriter writer)
		{
			if (this.HasIcon)
			{
				this.RenderIcon(writer, new string[]
				{
					"id=\"imgTrNd\""
				});
			}
			writer.Write("<span id=\"spnTrNdCnt\">");
			this.RenderContent(writer);
			writer.Write("</span>");
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x000C3334 File Offset: 0x000C1534
		internal void Render(TextWriter writer, int indent)
		{
			if (!this.Visible)
			{
				return;
			}
			TreeNode.WrapTreeNodeStart(writer, this.UserContext, this.Id, this.highlightClassName, this.ContentVisible, this.HasChildren, this.Selected, this.isRootNode, indent, this.GetECIcon, this.UserContext.IsRtl);
			this.RenderUndecoratedNode(writer, indent);
			TreeNode.WrapTreeNodeEndToChild(writer, this.Id, this.HasChildren, this.IsExpanded, this.IsRootNode);
			foreach (TreeNode treeNode in this.Children)
			{
				treeNode.Render(writer, indent + this.ChildIndent);
			}
			TreeNode.WrapTreeNodeEnd(writer, this.HasChildren);
		}

		// Token: 0x04001800 RID: 6144
		public const string TreeNodeGroupHeaderClass = " trNdGpHd";

		// Token: 0x04001801 RID: 6145
		public const string DeletedItemsFoldersClass = " trNdDelFol";

		// Token: 0x04001802 RID: 6146
		protected const int DefaultChildIndent = 8;

		// Token: 0x04001803 RID: 6147
		protected const int ContentOffset = 21;

		// Token: 0x04001804 RID: 6148
		protected const string ImageIconId = "imgTrNd";

		// Token: 0x04001805 RID: 6149
		protected const string IdPrefix = "f";

		// Token: 0x04001806 RID: 6150
		private readonly List<TreeNode> children = new List<TreeNode>();

		// Token: 0x04001807 RID: 6151
		private TreeNode parent;

		// Token: 0x04001808 RID: 6152
		private bool selected;

		// Token: 0x04001809 RID: 6153
		private bool isExpanded;

		// Token: 0x0400180A RID: 6154
		private string customAttributes = string.Empty;

		// Token: 0x0400180B RID: 6155
		private string highlightClassName = "trNdHl";

		// Token: 0x0400180C RID: 6156
		private string nodeClassName = "trNd";

		// Token: 0x0400180D RID: 6157
		private bool? needSync;

		// Token: 0x0400180E RID: 6158
		private bool hasIcon;

		// Token: 0x0400180F RID: 6159
		private bool isRootNode;

		// Token: 0x04001810 RID: 6160
		private bool isDummy;
	}
}
