using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003CD RID: 973
	internal class NavigationFolderTreeNode : FolderTreeNode
	{
		// Token: 0x0600241B RID: 9243 RVA: 0x000D06B4 File Offset: 0x000CE8B4
		internal NavigationFolderTreeNode(UserContext userContext, NavigationNodeFolder nodeFolder) : base(userContext, OwaStoreObjectId.CreateFromNavigationNodeFolder(userContext, nodeFolder), nodeFolder.Subject, nodeFolder.GetFolderClass(), nodeFolder.IsFlagSet(NavigationNodeFlags.TodoFolder) ? DefaultFolderType.ToDoSearch : DefaultFolderType.None)
		{
			if (nodeFolder.IsNew)
			{
				throw new ArgumentException("Should not use newly created node");
			}
			this.navigationNodeFolder = nodeFolder;
			this.elcPolicyFolderId = null;
		}

		// Token: 0x0600241C RID: 9244 RVA: 0x000D070E File Offset: 0x000CE90E
		internal NavigationFolderTreeNode(UserContext userContext, NavigationNodeFolder nodeFolder, StoreObjectId elcPolicyFolderId, object[] values, Dictionary<PropertyDefinition, int> propertyMap) : base(userContext, OwaStoreObjectId.CreateFromNavigationNodeFolder(userContext, nodeFolder).GetSession(userContext), values, propertyMap)
		{
			if (nodeFolder.IsNew)
			{
				throw new ArgumentException("Should not use newly created node");
			}
			this.navigationNodeFolder = nodeFolder;
			this.elcPolicyFolderId = elcPolicyFolderId;
		}

		// Token: 0x0600241D RID: 9245 RVA: 0x000D0749 File Offset: 0x000CE949
		internal NavigationFolderTreeNode(UserContext userContext, NavigationNodeFolder nodeFolder, Folder folder) : base(userContext, folder)
		{
			if (nodeFolder.IsNew)
			{
				throw new ArgumentException("Should not use newly created node");
			}
			this.navigationNodeFolder = nodeFolder;
			this.elcPolicyFolderId = null;
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x000D0774 File Offset: 0x000CE974
		public override string Id
		{
			get
			{
				return "f" + this.navigationNodeFolder.NavigationNodeId.ObjectId.ToString();
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x0600241F RID: 9247 RVA: 0x000D0798 File Offset: 0x000CE998
		internal override string DisplayName
		{
			get
			{
				if (base.FolderType == DefaultFolderType.ToDoSearch)
				{
					return LocalizedStrings.GetNonEncoded(-1954334922);
				}
				string text = (this.navigationNodeFolder.NavigationNodeGroupSection == NavigationNodeGroupSection.First) ? base.DisplayName : this.FolderName;
				if (this.navigationNodeFolder.NavigationNodeType == NavigationNodeType.NormalFolder && !StringComparer.OrdinalIgnoreCase.Equals(this.navigationNodeFolder.MailboxLegacyDN, base.UserContext.MailboxOwnerLegacyDN))
				{
					return string.Format(LocalizedStrings.GetNonEncoded(-83764036), text, Utilities.GetMailboxOwnerDisplayName((MailboxSession)base.FolderId.GetSession(base.UserContext)));
				}
				return text;
			}
		}

		// Token: 0x06002420 RID: 9248 RVA: 0x000D0834 File Offset: 0x000CEA34
		protected override void RenderAdditionalProperties(TextWriter writer)
		{
			writer.Write(" _fid=\"");
			Utilities.HtmlEncode(base.FolderId.ToBase64String(), writer);
			writer.Write("\"");
			if (this.navigationNodeFolder.NavigationNodeGroupSection == NavigationNodeGroupSection.Calendar && CalendarColorManager.IsColorIndexValid(this.navigationNodeFolder.NavigationNodeCalendarColor))
			{
				writer.Write(" _iSavedColor=");
				writer.Write(CalendarColorManager.GetClientColorIndex(this.navigationNodeFolder.NavigationNodeCalendarColor));
			}
			if (this.navigationNodeFolder.IsFilteredView)
			{
				writer.Write(" _fltr=1");
			}
			if (this.navigationNodeFolder.NavigationNodeGroupSection == NavigationNodeGroupSection.First && this.elcPolicyFolderId != null)
			{
				writer.Write(" _sPlcyFId=\"");
				Utilities.HtmlEncode(this.elcPolicyFolderId.ToBase64String(), writer);
				writer.Write("\"");
			}
			base.RenderAdditionalProperties(writer);
		}

		// Token: 0x06002421 RID: 9249 RVA: 0x000D0908 File Offset: 0x000CEB08
		internal override void RenderNodeBody(TextWriter writer)
		{
			if (this.navigationNodeFolder.NavigationNodeGroupSection == NavigationNodeGroupSection.Calendar)
			{
				base.UserContext.RenderThemeImage(writer, base.Selected ? ThemeFileId.CheckChecked : ThemeFileId.CheckUnchecked, null, new object[]
				{
					"id=\"imgCk\""
				});
			}
			base.RenderNodeBody(writer);
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x000D095B File Offset: 0x000CEB5B
		internal override bool HasChildren
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06002423 RID: 9251 RVA: 0x000D095E File Offset: 0x000CEB5E
		internal override bool Selectable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06002424 RID: 9252 RVA: 0x000D0961 File Offset: 0x000CEB61
		protected override FolderTreeNode.ContentCountDisplayType ContentCountDisplay
		{
			get
			{
				if (this.navigationNodeFolder.NavigationNodeGroupSection != NavigationNodeGroupSection.First)
				{
					return FolderTreeNode.ContentCountDisplayType.None;
				}
				return base.ContentCountDisplay;
			}
		}

		// Token: 0x06002425 RID: 9253 RVA: 0x000D0979 File Offset: 0x000CEB79
		protected override void RenderIcon(TextWriter writer, params string[] extraAttributes)
		{
			if (this.navigationNodeFolder.IsFilteredView)
			{
				base.UserContext.RenderThemeImage(writer, ThemeFileId.FavoritesFilter, null, extraAttributes);
				return;
			}
			base.RenderIcon(writer, extraAttributes);
		}

		// Token: 0x04001911 RID: 6417
		private readonly StoreObjectId elcPolicyFolderId;

		// Token: 0x04001912 RID: 6418
		private readonly NavigationNodeFolder navigationNodeFolder;
	}
}
