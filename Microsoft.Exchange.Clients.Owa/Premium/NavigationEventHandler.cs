using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004C6 RID: 1222
	[OwaEventNamespace("Navigation")]
	internal sealed class NavigationEventHandler : OwaEventHandlerBase
	{
		// Token: 0x06002EA1 RID: 11937 RVA: 0x0010A779 File Offset: 0x00108979
		public static void Register()
		{
			OwaEventRegistry.RegisterHandler(typeof(NavigationEventHandler));
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x0010A78C File Offset: 0x0010898C
		[OwaEvent("GetSecondaryNavigation")]
		[OwaEventParameter("m", typeof(NavigationModule))]
		[OwaEventVerb(OwaEventVerb.Get)]
		public void GetSecondaryNavigation()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "NavigationEventHandler.GetSecondaryNavigation");
			switch ((NavigationModule)base.GetParameter("m"))
			{
			case NavigationModule.Mail:
				NavigationHost.RenderMailSecondaryNavigation(this.Writer, base.UserContext);
				return;
			case NavigationModule.Calendar:
				if (!base.UserContext.IsFeatureEnabled(Feature.Calendar))
				{
					throw new OwaSegmentationException("The calendar is disabled");
				}
				using (CalendarFolder folder = Utilities.GetFolder<CalendarFolder>(base.UserContext, base.UserContext.CalendarFolderOwaId, new PropertyDefinition[]
				{
					ViewStateProperties.CalendarViewType,
					ViewStateProperties.DailyViewDays
				}))
				{
					DailyView.RenderSecondaryNavigation(this.Writer, folder, base.UserContext);
					return;
				}
				break;
			case NavigationModule.Contacts:
				break;
			case NavigationModule.Tasks:
				if (!base.UserContext.IsFeatureEnabled(Feature.Tasks))
				{
					throw new OwaSegmentationException("Tasks are disabled");
				}
				TaskView.RenderSecondaryNavigation(this.Writer, base.UserContext);
				return;
			case NavigationModule.Options:
				return;
			case NavigationModule.AddressBook:
				DirectoryView.RenderSecondaryNavigation(this.Writer, base.UserContext);
				return;
			case NavigationModule.Documents:
				DocumentLibraryUtilities.RenderSecondaryNavigation(this.Writer, base.UserContext);
				return;
			case NavigationModule.PublicFolders:
				NavigationHost.RenderPublicFolderSecondaryNavigation(this.Writer, base.UserContext);
				return;
			default:
				return;
			}
			if (!base.UserContext.IsFeatureEnabled(Feature.Contacts))
			{
				throw new OwaSegmentationException("The Contacts feature is disabled");
			}
			ContactView.RenderSecondaryNavigation(this.Writer, base.UserContext, false);
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x0010A904 File Offset: 0x00108B04
		[OwaEvent("GetPFFilter")]
		[OwaEventParameter("t", typeof(string))]
		[OwaEventParameter("fId", typeof(OwaStoreObjectId), false, true)]
		[OwaEventVerb(OwaEventVerb.Get)]
		public void GetPublicFolderSecondaryNavigationFilter()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "NavigationEventHandler.GetSecondaryNavigationFilter");
			string containerClass = (string)base.GetParameter("t");
			if (ObjectClass.IsCalendarFolder(containerClass))
			{
				OwaStoreObjectId folderId = (OwaStoreObjectId)base.GetParameter("fId");
				using (CalendarFolder folder = Utilities.GetFolder<CalendarFolder>(base.UserContext, folderId, new PropertyDefinition[]
				{
					ViewStateProperties.CalendarViewType,
					ViewStateProperties.DailyViewDays
				}))
				{
					this.Writer.Write("<div id=divPFCalFlt style=\"display:none\">");
					RenderingUtilities.RenderSecondaryNavigationDatePicker(folder, this.Writer, "divErrPfDp", "divPfDp", base.UserContext);
					new MonthPicker(base.UserContext, "divPfMp").Render(this.Writer);
					this.Writer.Write("</div>");
					return;
				}
			}
			if (ObjectClass.IsContactsFolder(containerClass))
			{
				ContactView.RenderSecondaryNavigationFilter(this.Writer, "divPFCntFlt");
				return;
			}
			if (ObjectClass.IsTaskFolder(containerClass))
			{
				TaskView.RenderSecondaryNavigationFilter(this.Writer, "divPFTskFlt");
			}
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x0010AA20 File Offset: 0x00108C20
		[OwaEventParameter("q", typeof(bool))]
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEvent("PersistQuickLinksBar")]
		public void PersistQuickLinksBar()
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "NavigationEventHandler.PersistQuickLinksBar");
			bool isQuickLinksBarVisible = (bool)base.GetParameter("q");
			base.UserContext.UserOptions.IsQuickLinksBarVisible = isQuickLinksBarVisible;
			base.UserContext.UserOptions.CommitChanges();
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x0010AA78 File Offset: 0x00108C78
		[OwaEventVerb(OwaEventVerb.Post)]
		[OwaEvent("PersistWidth")]
		[OwaEventParameter("w", typeof(int))]
		public void PersistWidth()
		{
			ExTraceGlobals.UserOptionsCallTracer.TraceDebug((long)this.GetHashCode(), "NavigationEventHandler.PersistWidth");
			int navigationBarWidth = (int)base.GetParameter("w");
			base.UserContext.UserOptions.NavigationBarWidth = navigationBarWidth;
			base.UserContext.UserOptions.CommitChanges();
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x0010AAD0 File Offset: 0x00108CD0
		[OwaEventParameter("pTr", typeof(bool))]
		[OwaEvent("GetFolderPickerTree")]
		public void GetFolderPickerTrees()
		{
			bool requirePublicFolderTree = (bool)base.GetParameter("pTr");
			FolderPickerTree folderPickerTree = FolderPickerTree.CreateFolderPickerTree(base.UserContext, requirePublicFolderTree);
			string text = "divFPErr";
			this.Writer.Write("<div id=\"divFPTrR\">");
			Infobar infobar = new Infobar(text, "infobar");
			infobar.Render(this.Writer);
			NavigationHost.RenderTreeRegionDivStart(this.Writer, null);
			NavigationHost.RenderTreeDivStart(this.Writer, "fptree");
			folderPickerTree.ErrDiv = text;
			folderPickerTree.Render(this.Writer);
			NavigationHost.RenderTreeDivEnd(this.Writer);
			NavigationHost.RenderTreeRegionDivEnd(this.Writer);
			this.Writer.Write("</div>");
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x0010AB80 File Offset: 0x00108D80
		[OwaEventVerb(OwaEventVerb.Get)]
		[OwaEvent("ReloadCalendarNavigationTree")]
		public void ReloadCalendarNavigationTree()
		{
			NavigationHost.RenderFavoritesAndNavigationTrees(this.Writer, base.UserContext, null, new NavigationNodeGroupSection[]
			{
				NavigationNodeGroupSection.Calendar
			});
		}

		// Token: 0x0400205A RID: 8282
		public const string EventNamespace = "Navigation";

		// Token: 0x0400205B RID: 8283
		public const string MethodGetSecondaryNavigation = "GetSecondaryNavigation";

		// Token: 0x0400205C RID: 8284
		public const string MethodGetPublicFolderSecondaryNavigationFilter = "GetPFFilter";

		// Token: 0x0400205D RID: 8285
		public const string MethodPersistQuickLinksBar = "PersistQuickLinksBar";

		// Token: 0x0400205E RID: 8286
		public const string MethodPersistWidth = "PersistWidth";

		// Token: 0x0400205F RID: 8287
		public const string MethodGetFolderPickerTrees = "GetFolderPickerTree";

		// Token: 0x04002060 RID: 8288
		public const string MethodReloadCalendarNavigationTree = "ReloadCalendarNavigationTree";

		// Token: 0x04002061 RID: 8289
		public const string FolderId = "fId";

		// Token: 0x04002062 RID: 8290
		public const string FolderName = "fN";

		// Token: 0x04002063 RID: 8291
		public const string Module = "m";

		// Token: 0x04002064 RID: 8292
		public const string QuickLinksVisible = "q";

		// Token: 0x04002065 RID: 8293
		public const string Width = "w";

		// Token: 0x04002066 RID: 8294
		public const string FolderContainerClass = "t";

		// Token: 0x04002067 RID: 8295
		public const string RequirePublicFolderTree = "pTr";

		// Token: 0x04002068 RID: 8296
		public const string IsAddressBook = "fAB";

		// Token: 0x04002069 RID: 8297
		public const string Recipient = "RCP";

		// Token: 0x0400206A RID: 8298
		public const string Recipients = "Recips";
	}
}
