using System;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Basic.Controls;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Basic
{
	// Token: 0x02000095 RID: 149
	public class ContactView : ListViewPage, IRegistryOnlyForm
	{
		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00026DD4 File Offset: 0x00024FD4
		internal override StoreObjectId DefaultFolderId
		{
			get
			{
				return base.UserContext.ContactsFolderId;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x00026DE1 File Offset: 0x00024FE1
		protected override SortOrder DefaultSortOrder
		{
			get
			{
				return SortOrder.Descending;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x00026DE4 File Offset: 0x00024FE4
		protected override ColumnId DefaultSortedColumn
		{
			get
			{
				return ColumnId.FileAs;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x00026DE8 File Offset: 0x00024FE8
		public string UrlEncodedFolderId
		{
			get
			{
				return HttpUtility.UrlEncode(base.Folder.Id.ObjectId.ToBase64String());
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x00026E04 File Offset: 0x00025004
		protected override string CheckBoxId
		{
			get
			{
				return "chkRcpt";
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x00026E0B File Offset: 0x0002500B
		public string ApplicationElement
		{
			get
			{
				return Convert.ToString(base.OwaContext.FormsRegistryContext.ApplicationElement);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x00026E27 File Offset: 0x00025027
		public string Type
		{
			get
			{
				if (base.OwaContext.FormsRegistryContext.Type != null)
				{
					return base.OwaContext.FormsRegistryContext.Type;
				}
				return string.Empty;
			}
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00026E51 File Offset: 0x00025051
		public ContactView() : base(ExTraceGlobals.ContactsCallTracer, ExTraceGlobals.ContactsTracer)
		{
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00026E64 File Offset: 0x00025064
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (base.UserContext.IsWebPartRequest)
			{
				base.UserContext.LastClientViewState = new WebPartModuleViewState(base.FolderId, base.Folder.ClassName, base.PageNumber, NavigationModule.Contacts, base.SortOrder, base.SortedColumn);
				return;
			}
			if (base.FilteredView)
			{
				base.UserContext.LastClientViewState = new ContactModuleSearchViewState(base.UserContext.LastClientViewState, base.FolderId, base.Folder.ClassName, base.PageNumber, base.SearchString, base.SearchScope);
				return;
			}
			base.UserContext.LastClientViewState = new ContactModuleViewState(base.FolderId, base.Folder.ClassName, base.PageNumber);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00026F28 File Offset: 0x00025128
		protected override void CreateListView(ColumnId sortColumn, SortOrder sortOrder)
		{
			if (!base.FilteredView)
			{
				base.ListView = new ContactsListView(base.UserContext, sortColumn, sortOrder, base.Folder);
			}
			else
			{
				base.ListView = new ContactsListView(base.UserContext, sortColumn, sortOrder, base.SearchFolder, base.SearchScope);
			}
			base.InitializeListView();
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00026F80 File Offset: 0x00025180
		protected override SanitizedHtmlString BuildConcretSearchInfobarMessage(int resultsCount, SanitizedHtmlString clearSearchLink)
		{
			return SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(-1403744948), new object[]
			{
				resultsCount,
				base.SearchString,
				clearSearchLink
			});
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00026FBC File Offset: 0x000251BC
		public void RenderContactsSecondaryNavigation()
		{
			ContactSecondaryNavigation contactSecondaryNavigation = new ContactSecondaryNavigation(base.OwaContext, base.Folder.Id.ObjectId, null);
			contactSecondaryNavigation.RenderContacts(base.Response.Output);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00026FF8 File Offset: 0x000251F8
		public void RenderHeaderToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, true);
			toolbar.RenderStart();
			toolbar.RenderButton(ToolbarButtons.NewContact);
			toolbar.RenderDivider();
			toolbar.RenderButton(ToolbarButtons.Move);
			toolbar.RenderButton(ToolbarButtons.Delete);
			toolbar.RenderDivider();
			toolbar.RenderButton(ToolbarButtons.SendEmailToContact);
			if (base.UserContext.IsFeatureEnabled(Feature.Calendar))
			{
				toolbar.RenderSpace();
				toolbar.RenderButton(ToolbarButtons.SendMeetingRequestToContact);
			}
			toolbar.RenderFill();
			base.RenderPaging(false);
			toolbar.RenderEnd();
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00027088 File Offset: 0x00025288
		public void RenderFooterToolbar()
		{
			Toolbar toolbar = new Toolbar(base.Response.Output, false);
			toolbar.RenderStart();
			if (!base.UserContext.IsWebPartRequest)
			{
				toolbar.RenderButton(ToolbarButtons.Move);
			}
			toolbar.RenderButton(ToolbarButtons.Delete);
			toolbar.RenderFill();
			base.RenderPaging(true);
			toolbar.RenderEnd();
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000270E4 File Offset: 0x000252E4
		protected void RenderOptions(string helpFile)
		{
			OptionsBar optionsBar = new OptionsBar(base.UserContext, base.Response.Output, OptionsBar.SearchModule.Contacts, OptionsBar.RenderingFlags.ShowSearchContext, OptionsBar.BuildFolderSearchUrlSuffix(base.UserContext, base.FolderId));
			optionsBar.Render(helpFile);
		}

		// Token: 0x040003B7 RID: 951
		private const string RecipientCheckBox = "chkRcpt";
	}
}
