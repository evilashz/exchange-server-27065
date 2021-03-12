﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Basic.Controls
{
	// Token: 0x0200005D RID: 93
	internal class MailSecondaryNavigation : SecondaryNavigation
	{
		// Token: 0x06000295 RID: 661 RVA: 0x00016FE4 File Offset: 0x000151E4
		public MailSecondaryNavigation(OwaContext owaContext, StoreObjectId folderId, FolderList folderList, MruFolderList mruFolderList, SecondaryNavigationArea? selectedUsing) : base(owaContext, folderId)
		{
			UserContext userContext = owaContext.UserContext;
			if (mruFolderList == null)
			{
				mruFolderList = new MruFolderList(userContext);
			}
			this.mruFolderList = mruFolderList;
			if (selectedUsing != null)
			{
				this.selectedUsing = selectedUsing.Value;
			}
			else
			{
				MessageModuleViewState messageModuleViewState = userContext.LastClientViewState as MessageModuleViewState;
				if (messageModuleViewState != null)
				{
					this.selectedUsing = messageModuleViewState.SelectedUsing;
				}
			}
			this.SetAllFolderNavigationEnabled();
			if (RenderingFlags.EnableAllFolderNavigation(userContext) && folderList == null)
			{
				folderList = new FolderList(userContext, userContext.MailboxSession, null, 1024, true, null, FolderList.FolderPropertiesInBasic);
			}
			this.folderList = folderList;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0001707C File Offset: 0x0001527C
		private void SetAllFolderNavigationEnabled()
		{
			HttpRequest request = this.owaContext.HttpContext.Request;
			if (Utilities.IsPostRequest(request))
			{
				string formParameter = Utilities.GetFormParameter(request, "hidactbrfld", false);
				if (formParameter == "1")
				{
					RenderingFlags.EnableAllFolderNavigation(this.owaContext.UserContext, true);
				}
			}
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000170CD File Offset: 0x000152CD
		public void Render(TextWriter writer)
		{
			this.Render(writer, true, true);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000170D8 File Offset: 0x000152D8
		private void Render(TextWriter writer, bool renderMru, bool renderAllFolder)
		{
			ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "MailSecondaryNavigation.Render()");
			writer.Write("<table cellpadding=0 cellspacing=0 class=\"wh100\"><caption>");
			writer.Write(LocalizedStrings.GetHtmlEncoded(-1286941817));
			writer.Write("</caption><tr><td>");
			this.RenderSpecialFolderNavigation(writer);
			if (renderAllFolder)
			{
				this.RenderAllFolderNavigation(writer);
			}
			if (renderMru)
			{
				this.RenderMru(writer);
			}
			base.RenderManageFolderButton(-628437851, writer);
			writer.Write("</td></tr></table>");
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00017153 File Offset: 0x00015353
		public void RenderWithoutMruAndAllFolder(TextWriter writer)
		{
			this.Render(writer, false, false);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00017160 File Offset: 0x00015360
		private void RenderSpecialFolderNavigation(TextWriter writer)
		{
			UserContext userContext = this.owaContext.UserContext;
			writer.Write("<table cellspacing=0 cellpadding=0 class=\"snt\">");
			if (this.folderList != null)
			{
				for (int i = 0; i < this.folderList.Count; i++)
				{
					StoreObjectId objectId = ((VersionedId)this.folderList.GetPropertyValue(i, FolderSchema.Id)).ObjectId;
					if (FolderUtility.IsPrimaryMailFolder(objectId, userContext))
					{
						ContentCountDisplay contentCountDisplay = FolderUtility.GetContentCountDisplay(this.folderList.GetPropertyValue(i, FolderSchema.ExtendedFolderFlags), objectId);
						int count = 0;
						if (contentCountDisplay == ContentCountDisplay.ItemCount)
						{
							count = (int)this.folderList.GetPropertyValue(i, FolderSchema.ItemCount);
						}
						else if (contentCountDisplay == ContentCountDisplay.UnreadCount)
						{
							count = (int)this.folderList.GetPropertyValue(i, FolderSchema.UnreadCount);
						}
						this.RenderFolder(objectId, (string)this.folderList.GetPropertyValue(i, FolderSchema.DisplayName), count, contentCountDisplay, SecondaryNavigationArea.Special, writer);
					}
				}
			}
			else
			{
				Dictionary<PropertyDefinition, int> folderPropertyToIndexInBasic = FolderList.FolderPropertyToIndexInBasic;
				using (Folder folder = Folder.Bind(userContext.MailboxSession, userContext.GetRootFolderId(userContext.MailboxSession)))
				{
					using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, null, FolderList.FolderPropertiesInBasic))
					{
						for (;;)
						{
							object[][] rows = queryResult.GetRows(10000);
							if (rows.Length <= 0)
							{
								break;
							}
							foreach (object[] array in rows)
							{
								StoreObjectId objectId2 = ((VersionedId)array[folderPropertyToIndexInBasic[FolderSchema.Id]]).ObjectId;
								if (FolderUtility.IsPrimaryMailFolder(objectId2, userContext))
								{
									object extendedFolderFlagValue = array[folderPropertyToIndexInBasic[FolderSchema.ExtendedFolderFlags]];
									ContentCountDisplay contentCountDisplay2 = FolderUtility.GetContentCountDisplay(extendedFolderFlagValue, objectId2);
									int count2 = 0;
									if (contentCountDisplay2 == ContentCountDisplay.ItemCount)
									{
										count2 = (int)array[folderPropertyToIndexInBasic[FolderSchema.ItemCount]];
									}
									else if (contentCountDisplay2 == ContentCountDisplay.UnreadCount)
									{
										count2 = (int)array[folderPropertyToIndexInBasic[FolderSchema.UnreadCount]];
									}
									this.RenderFolder(objectId2, (string)array[folderPropertyToIndexInBasic[FolderSchema.DisplayName]], count2, contentCountDisplay2, SecondaryNavigationArea.Special, writer);
								}
							}
						}
					}
				}
			}
			writer.Write("</table>");
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0001739C File Offset: 0x0001559C
		private void RenderAllFolderNavigation(TextWriter writer)
		{
			UserContext userContext = this.owaContext.UserContext;
			writer.Write("<table cellspacing=0 cellpadding=0 class=\"brwst\">");
			writer.Write("<tr><td align=\"center\" nowrap>");
			if (RenderingFlags.EnableAllFolderNavigation(userContext))
			{
				writer.Write("<span class=\"brws ");
				if (this.selectedUsing == SecondaryNavigationArea.BrowseAll)
				{
					writer.Write("sl");
				}
				writer.Write("\">");
				FolderDropdown folderDropdown = new FolderDropdown(userContext);
				StoreObjectId selectedFolderId = null;
				if (this.selectedUsing == SecondaryNavigationArea.BrowseAll)
				{
					selectedFolderId = this.selectedFolderId;
				}
				folderDropdown.RenderAllFolderSelectInMailSecondaryNavigation(this.folderList, selectedFolderId, writer);
				writer.Write("<a href=\"#\" id=\"lnkGotoFldr\" onClick=\"return onClkBrwsFldNv();\"><img src=\"");
				userContext.RenderThemeFileUrl(writer, ThemeFileId.Go2);
				writer.Write("\" alt=\"");
				writer.Write(LocalizedStrings.GetHtmlEncoded(1053153637));
				writer.Write("\"></a></span>");
			}
			else
			{
				writer.Write("<a class=\"lnk\" id=\"lnkBrwsAllFldrs\" href=\"#\" onClick=\"return onClkBrwsFld();\">");
				writer.Write(LocalizedStrings.GetHtmlEncoded(-2125794143));
				writer.Write("<img src=\"");
				userContext.RenderThemeFileUrl(writer, ThemeFileId.Expand);
				writer.Write("\" alt=\"\"></a>");
			}
			writer.Write("</td></tr></table>");
			writer.Write("<input type=\"hidden\" name=\"hidactbrfld\" value=\"\">");
		}

		// Token: 0x0600029C RID: 668 RVA: 0x000174B4 File Offset: 0x000156B4
		private void RenderMru(TextWriter writer)
		{
			if (this.mruFolderList.Count > 0)
			{
				writer.Write("<table cellspacing=0 cellpadding=0 class=\"snt\">");
				for (int i = 0; i < this.mruFolderList.Count; i++)
				{
					MruFolderItem mruFolderItem = this.mruFolderList[i];
					int count = 0;
					ContentCountDisplay contentCountDisplay = FolderUtility.GetContentCountDisplay(mruFolderItem.ExtendedFolderFlags, mruFolderItem.Id);
					if (contentCountDisplay == ContentCountDisplay.ItemCount)
					{
						count = mruFolderItem.ItemCount;
					}
					else if (contentCountDisplay == ContentCountDisplay.UnreadCount)
					{
						count = mruFolderItem.UnreadCount;
					}
					this.RenderFolder(mruFolderItem.Id, mruFolderItem.DisplayName, count, contentCountDisplay, SecondaryNavigationArea.Mru, writer);
				}
				writer.Write("</table>");
				base.RenderHorizontalDivider(writer);
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00017554 File Offset: 0x00015754
		private void RenderFolder(StoreObjectId folderId, string folderDisplayName, int count, ContentCountDisplay contentCountDisplay, SecondaryNavigationArea secondaryNavigationArea, TextWriter writer)
		{
			UserContext userContext = this.owaContext.UserContext;
			writer.Write("<tr><td nowrap class=\"fld");
			if (this.selectedUsing == secondaryNavigationArea && folderId.Equals(this.selectedFolderId))
			{
				writer.Write(" sl");
			}
			if (count > 0)
			{
				writer.Write(" bld");
			}
			writer.Write("\"><a name=\"lnkFldr\" href=\"?ae=Folder&t=IPF.Note&id=");
			writer.Write(Utilities.UrlEncode(folderId.ToBase64String()));
			writer.Write("&slUsng=");
			writer.Write((int)secondaryNavigationArea);
			if (secondaryNavigationArea == SecondaryNavigationArea.Mru)
			{
				writer.Write("&mru=1");
			}
			writer.Write("\" title=\"");
			Utilities.HtmlEncode(folderDisplayName, writer);
			writer.Write("\">");
			writer.Write("<img src=\"");
			RenderingUtilities.RenderSpecialFolderIcon(writer, userContext, "IPF.Note", folderId);
			writer.Write("\" alt=\"\">");
			Utilities.CropAndRenderText(writer, folderDisplayName, 24);
			writer.Write(" </a>");
			if (count > 0)
			{
				if (contentCountDisplay == ContentCountDisplay.ItemCount)
				{
					writer.Write("<span class=\"itm\">[");
					writer.Write(count);
					writer.Write("]</span>");
				}
				else if (contentCountDisplay == ContentCountDisplay.UnreadCount)
				{
					writer.Write("<span class=\"unrd\">(");
					writer.Write(count);
					writer.Write(")</span>");
				}
			}
			writer.Write("</td></tr>");
		}

		// Token: 0x040001E1 RID: 481
		private const string ActivateBrowseAllFoldersFormParameter = "hidactbrfld";

		// Token: 0x040001E2 RID: 482
		private SecondaryNavigationArea selectedUsing;

		// Token: 0x040001E3 RID: 483
		private FolderList folderList;

		// Token: 0x040001E4 RID: 484
		private MruFolderList mruFolderList;
	}
}
