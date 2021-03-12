using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Search;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.OOF;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200045D RID: 1117
	public class MessageView2 : FolderListViewSubPage, IRegistryOnlyForm
	{
		// Token: 0x0600295E RID: 10590 RVA: 0x000E9790 File Offset: 0x000E7990
		public MessageView2() : base(ExTraceGlobals.MailCallTracer, ExTraceGlobals.MailTracer)
		{
		}

		// Token: 0x0600295F RID: 10591 RVA: 0x000E97E1 File Offset: 0x000E79E1
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.arrangeByMenu = new MessageViewArrangeByMenu(base.Folder, base.UserContext, this.SortedColumn);
		}

		// Token: 0x06002960 RID: 10592 RVA: 0x000E9807 File Offset: 0x000E7A07
		protected override void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
			base.UserContext.MessageViewFirstRender = false;
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06002961 RID: 10593 RVA: 0x000E981C File Offset: 0x000E7A1C
		internal override StoreObjectId DefaultFolderId
		{
			get
			{
				return base.UserContext.InboxFolderId;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06002962 RID: 10594 RVA: 0x000E9829 File Offset: 0x000E7A29
		protected override SortOrder DefaultSortOrder
		{
			get
			{
				return SortOrder.Descending;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06002963 RID: 10595 RVA: 0x000E982C File Offset: 0x000E7A2C
		protected override bool FindBarOn
		{
			get
			{
				return !base.IsPublicFolder && base.UserContext.UserOptions.MailFindBarOn;
			}
		}

		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06002964 RID: 10596 RVA: 0x000E9848 File Offset: 0x000E7A48
		protected override int LVRPContainerTop
		{
			get
			{
				return this.lvRPContainerTop;
			}
		}

		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06002965 RID: 10597 RVA: 0x000E9850 File Offset: 0x000E7A50
		protected override ColumnId DefaultSortedColumn
		{
			get
			{
				if (base.FolderType == DefaultFolderType.DeletedItems || base.FolderType == DefaultFolderType.JunkEmail)
				{
					return ColumnId.DeliveryTime;
				}
				if (base.FolderType == DefaultFolderType.Outbox)
				{
					return ColumnId.SentTime;
				}
				return ColumnId.ConversationLastDeliveryTime;
			}
		}

		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06002966 RID: 10598 RVA: 0x000E9874 File Offset: 0x000E7A74
		protected bool ShowToInFilter
		{
			get
			{
				return base.FolderType == DefaultFolderType.SentItems || base.FolderType == DefaultFolderType.Drafts || base.FolderType == DefaultFolderType.Outbox;
			}
		}

		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06002967 RID: 10599 RVA: 0x000E9895 File Offset: 0x000E7A95
		protected override ReadingPanePosition DefaultReadingPanePosition
		{
			get
			{
				if (base.FolderType == DefaultFolderType.JunkEmail || base.FolderType == DefaultFolderType.SentItems || base.FolderType == DefaultFolderType.Outbox || base.FolderType == DefaultFolderType.DeletedItems)
				{
					return ReadingPanePosition.Off;
				}
				return ReadingPanePosition.Right;
			}
		}

		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06002968 RID: 10600 RVA: 0x000E98C0 File Offset: 0x000E7AC0
		protected override bool DefaultMultiLineSetting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06002969 RID: 10601 RVA: 0x000E98C3 File Offset: 0x000E7AC3
		protected MessageViewContextMenu ContextMenu
		{
			get
			{
				if (this.contextMenu == null)
				{
					this.contextMenu = new MessageViewContextMenu(base.UserContext, "divVwm", base.IsPublicFolder, ConversationUtilities.ShouldAllowConversationView(base.UserContext, base.Folder));
				}
				return this.contextMenu;
			}
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x0600296A RID: 10602 RVA: 0x000E9900 File Offset: 0x000E7B00
		protected MessageViewArrangeByMenu ArrangeByMenu
		{
			get
			{
				return this.arrangeByMenu;
			}
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x0600296B RID: 10603 RVA: 0x000E9908 File Offset: 0x000E7B08
		protected bool IsConversationView
		{
			get
			{
				return ConversationUtilities.IsConversationSortColumn(this.SortedColumn);
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x0600296C RID: 10604 RVA: 0x000E9915 File Offset: 0x000E7B15
		protected bool IsJunkMailFolder
		{
			get
			{
				return Utilities.IsDefaultFolder(base.Folder, DefaultFolderType.JunkEmail);
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x0600296D RID: 10605 RVA: 0x000E9923 File Offset: 0x000E7B23
		protected bool IsSearchFolder
		{
			get
			{
				return base.Folder is SearchFolder;
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x0600296E RID: 10606 RVA: 0x000E9933 File Offset: 0x000E7B33
		protected bool DontAllowAddFilterToFavorites
		{
			get
			{
				return base.IsFilteredViewInFavorites || base.IsInDeleteItems || base.IsDeletedItemsSubFolder;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x0600296F RID: 10607 RVA: 0x000E994D File Offset: 0x000E7B4D
		protected static int StoreObjectTypeFolder
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06002970 RID: 10608 RVA: 0x000E9950 File Offset: 0x000E7B50
		protected override IListView CreateListView(ColumnId sortedColumn, SortOrder sortOrder)
		{
			if (Globals.ArePerfCountersEnabled)
			{
				OwaSingleCounters.MailViewsLoaded.Increment();
			}
			MessageVirtualListView2 messageVirtualListView = new MessageVirtualListView2(base.UserContext, "divVLV", this.IsMultiLine, sortedColumn, sortOrder, base.Folder, base.Folder, null, (base.Folder is SearchFolder) ? SearchScope.AllFoldersAndItems : SearchScope.SelectedFolder);
			if (Utilities.IsDefaultFolder(base.Folder, DefaultFolderType.JunkEmail))
			{
				messageVirtualListView.AddAttribute("fJnk", "1");
			}
			messageVirtualListView.LoadData(0, 50);
			return messageVirtualListView;
		}

		// Token: 0x06002971 RID: 10609 RVA: 0x000E99D0 File Offset: 0x000E7BD0
		protected override Toolbar CreateListToolbar()
		{
			MessageItemManageToolbar toolbar = new MessageItemManageToolbar(base.IsPublicFolder, ConversationUtilities.ShouldAllowConversationView(base.UserContext, base.Folder), base.IsInDeleteItems || this.IsJunkMailFolder, this.IsMultiLine, base.IsOtherMailboxFolder, base.UserContext.IsWebPartRequest, base.Folder.ClassName, this.ReadingPanePosition, this.IsConversationView, base.UserContext.UserOptions.ConversationSortOrder == ConversationSortOrder.ChronologicalNewestOnTop, base.UserContext.UserOptions.ShowTreeInListView, base.IsInDeleteItems || base.IsDeletedItemsSubFolder, this.IsJunkMailFolder);
			MessageViewManageToolbar toolbar2 = new MessageViewManageToolbar();
			MessageViewActionToolbar toolbar3 = new MessageViewActionToolbar(this.IsJunkMailFolder);
			MessageViewActionsButtonToolbar toolbar4 = new MessageViewActionsButtonToolbar();
			return new MultipartToolbar(new MultipartToolbar.ToolbarInfo[]
			{
				new MultipartToolbar.ToolbarInfo(toolbar, "divItemToolbar"),
				new MultipartToolbar.ToolbarInfo(toolbar2, "divViewToolbar"),
				new MultipartToolbar.ToolbarInfo(toolbar3, "divReplyForwardToolbar"),
				new MultipartToolbar.ToolbarInfo(toolbar4, "divActionsButtonToolbar")
			});
		}

		// Token: 0x06002972 RID: 10610 RVA: 0x000E9ADA File Offset: 0x000E7CDA
		protected override Toolbar CreateActionToolbar()
		{
			return null;
		}

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06002973 RID: 10611 RVA: 0x000E9AE0 File Offset: 0x000E7CE0
		private bool IsUserOofEnabled
		{
			get
			{
				return (base.UserContext.MailboxSession.Mailbox.TryGetProperty(MailboxSchema.MailboxOofState) as bool?) ?? false;
			}
		}

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06002974 RID: 10612 RVA: 0x000E9B24 File Offset: 0x000E7D24
		private UserOofSettings UserOofSettings
		{
			get
			{
				if (!base.UserContext.IsWebPartRequest && this.userOofSettings == null)
				{
					try
					{
						this.userOofSettings = UserOofSettings.GetUserOofSettings(base.UserContext.MailboxSession);
					}
					catch (QuotaExceededException ex)
					{
						ExTraceGlobals.CoreTracer.TraceDebug<string>(0L, "MessageView.UserOofSettings: Failed. Exception: {0}", ex.Message);
					}
				}
				return this.userOofSettings;
			}
		}

		// Token: 0x06002975 RID: 10613 RVA: 0x000E9B90 File Offset: 0x000E7D90
		protected bool ShouldShowScheduledOofInfobar()
		{
			if (!this.IsUserOofEnabled || base.UserContext.IsWebPartRequest || this.UserOofSettings == null)
			{
				return false;
			}
			if (!base.UserContext.MessageViewFirstRender || this.UserOofSettings.OofState != OofState.Scheduled)
			{
				return false;
			}
			DateTime utcNow = DateTime.UtcNow;
			DateTime t = DateTime.MinValue;
			DateTime t2 = DateTime.MinValue;
			if (this.UserOofSettings.Duration != null)
			{
				t = this.UserOofSettings.Duration.StartTime;
				t2 = this.UserOofSettings.Duration.EndTime;
			}
			return utcNow > t && t2 > utcNow;
		}

		// Token: 0x06002976 RID: 10614 RVA: 0x000E9C2C File Offset: 0x000E7E2C
		protected bool ShouldShowOofDialog()
		{
			return !base.UserContext.IsWebPartRequest && base.UserContext.MessageViewFirstRender && this.IsUserOofEnabled && this.UserOofSettings != null && this.UserOofSettings.OofState == OofState.Enabled;
		}

		// Token: 0x06002977 RID: 10615 RVA: 0x000E9C6C File Offset: 0x000E7E6C
		protected void RenderOofNotificationInfobar(Infobar infobar)
		{
			ExDateTime exDateTime = new ExDateTime(base.UserContext.TimeZone, this.UserOofSettings.Duration.EndTime);
			SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
			sanitizingStringBuilder.Append("<div class=\"divIBTxt\">");
			sanitizingStringBuilder.AppendFormat(LocalizedStrings.GetHtmlEncoded(-1261886615), new object[]
			{
				exDateTime.ToLongDateString() + " " + exDateTime.ToString(base.UserContext.UserOptions.TimeFormat)
			});
			sanitizingStringBuilder.Append("</div>");
			sanitizingStringBuilder.Append("<div class=\"divIBTxt\"><a href=# id=\"lnkRmvOofIB\" _sRmvId=\"divOofIB\">");
			sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(1303059585));
			sanitizingStringBuilder.Append("</a></div>");
			infobar.AddMessage(sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>(), InfobarMessageType.Informational, "divOofIB");
		}

		// Token: 0x06002978 RID: 10616 RVA: 0x000E9D34 File Offset: 0x000E7F34
		protected void RenderExpiringPasswordNotificationInfobar(Infobar infobar, int daysUntilExpiration)
		{
			SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
			sanitizingStringBuilder.Append("<div class=\"divIBTxt\">");
			if (daysUntilExpiration == 0)
			{
				sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(352263686));
			}
			else
			{
				sanitizingStringBuilder.AppendFormat(LocalizedStrings.GetHtmlEncoded(-2025544575), new object[]
				{
					daysUntilExpiration
				});
			}
			sanitizingStringBuilder.Append("</div>");
			sanitizingStringBuilder.Append("<div class=\"divIBTxt\"><a href=# id=\"lnkChgPwd\">");
			sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(-1273337393));
			sanitizingStringBuilder.Append("</a></div>");
			sanitizingStringBuilder.Append("<div class=\"divIBTxt\"><a href=# id=\"lnkRmvPwdIB\" _sRmvId=\"divPwdIB\">");
			sanitizingStringBuilder.Append(LocalizedStrings.GetNonEncoded(1496915101));
			sanitizingStringBuilder.Append("</a></div>");
			infobar.AddMessage(sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>(), InfobarMessageType.Informational, "divPwdIB");
		}

		// Token: 0x06002979 RID: 10617 RVA: 0x000E9DF1 File Offset: 0x000E7FF1
		protected void RenderHtmlEncodedFolderName()
		{
			Utilities.HtmlEncode(this.ContainerName, base.Response.Output);
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x000E9E09 File Offset: 0x000E8009
		protected void RenderJavascriptEncodedInboxFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.InboxFolderId.ToBase64String(), base.Response.Output);
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x000E9E2B File Offset: 0x000E802B
		protected void RenderJavascriptEncodedJunkEmailFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.JunkEmailFolderId.ToBase64String(), base.Response.Output);
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x000E9E4D File Offset: 0x000E804D
		protected void RenderJavascriptEncodedSentItemsFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.SentItemsFolderId.ToBase64String(), base.Response.Output);
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x000E9E70 File Offset: 0x000E8070
		protected void RenderSMimeControlUpdateInfobar(Infobar infobar)
		{
			SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
			sanitizingStringBuilder.Append("<div class=\"divIBTxt\">");
			sanitizingStringBuilder.AppendFormat(LocalizedStrings.GetHtmlEncoded(-172046453), new object[]
			{
				"<a href=\"/ecp/?p=Security/SMIME.aspx\" target=\"_parent\" class=\"lnk\">",
				"</a>"
			});
			sanitizingStringBuilder.Append("</div>");
			infobar.AddMessage(sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>(), InfobarMessageType.Informational, "divSMimeIB", true);
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x000E9ED4 File Offset: 0x000E80D4
		protected void RenderELCCommentAndQuota(Infobar infobar)
		{
			SanitizingStringBuilder<OwaHtml> sanitizingStringBuilder = new SanitizingStringBuilder<OwaHtml>();
			SanitizedHtmlString sanitizedHtmlString = new SanitizedHtmlString(base.RenderELCComment());
			sanitizedHtmlString.DecreeToBeTrusted();
			SanitizedHtmlString sanitizedHtmlString2 = new SanitizedHtmlString(base.RenderELCQuota());
			sanitizedHtmlString2.DecreeToBeTrusted();
			sanitizingStringBuilder.Append<SanitizedHtmlString>(sanitizedHtmlString);
			sanitizingStringBuilder.Append<SanitizedHtmlString>(sanitizedHtmlString2);
			infobar.AddMessage(sanitizingStringBuilder.ToSanitizedString<SanitizedHtmlString>(), InfobarMessageType.Informational, "divElcIB", !base.IsELCInfobarVisible);
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x000E9F34 File Offset: 0x000E8134
		protected override void RenderViewInfobars()
		{
			Infobar infobar = new Infobar("divErr", "infobarMessageView");
			int num = 0;
			if (base.ShouldRenderELCInfobar)
			{
				this.RenderELCCommentAndQuota(infobar);
				if (base.IsELCInfobarVisible)
				{
					num += 34;
				}
			}
			if (this.ShouldShowScheduledOofInfobar())
			{
				this.RenderOofNotificationInfobar(infobar);
				num += 20;
			}
			int daysUntilExpiration;
			if (Utilities.ShouldRenderExpiringPasswordInfobar(base.UserContext, out daysUntilExpiration))
			{
				this.RenderExpiringPasswordNotificationInfobar(infobar, daysUntilExpiration);
				num += 20;
			}
			if (Utilities.IsSMimeFeatureUsable(base.OwaContext) && !base.IsPublicFolder)
			{
				this.RenderSMimeControlUpdateInfobar(infobar);
			}
			if (0 < num)
			{
				int num2 = 60;
				if (num2 < num)
				{
					num = num2;
				}
				this.lvRPContainerTop = num + 3 + 1;
			}
			infobar.Render(base.SanitizingResponse);
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x000E9FDF File Offset: 0x000E81DF
		protected bool IsMessagePrefetchEnabled()
		{
			return MessagePrefetchConfiguration.IsMessagePrefetchEnabledForSession(base.UserContext, base.Folder.Session);
		}

		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06002981 RID: 10625 RVA: 0x000EA244 File Offset: 0x000E8444
		public override IEnumerable<string> ExternalScriptFiles
		{
			get
			{
				foreach (string script in this.externalScriptFiles)
				{
					yield return script;
				}
				if (base.UserContext.ExchangePrincipal.RecipientTypeDetails == RecipientTypeDetails.DiscoveryMailbox)
				{
					foreach (string script2 in this.externalScriptFilesForAnnotation)
					{
						yield return script2;
					}
				}
				yield break;
			}
		}

		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06002982 RID: 10626 RVA: 0x000EA261 File Offset: 0x000E8461
		public override SanitizedHtmlString Title
		{
			get
			{
				return new SanitizedHtmlString(this.ContainerName);
			}
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06002983 RID: 10627 RVA: 0x000EA26E File Offset: 0x000E846E
		public override string PageType
		{
			get
			{
				return "MessageViewSubPage";
			}
		}

		// Token: 0x04001C39 RID: 7225
		private const string OofEndTime = "oof";

		// Token: 0x04001C3A RID: 7226
		private const int InfobarColorbarHeight = 3;

		// Token: 0x04001C3B RID: 7227
		private const int InfobarMessageHeight = 20;

		// Token: 0x04001C3C RID: 7228
		private const int InfobarElcMessageHeight = 34;

		// Token: 0x04001C3D RID: 7229
		private const int InfobarBottomMargin = 1;

		// Token: 0x04001C3E RID: 7230
		private MessageViewContextMenu contextMenu;

		// Token: 0x04001C3F RID: 7231
		private MessageViewArrangeByMenu arrangeByMenu;

		// Token: 0x04001C40 RID: 7232
		private UserOofSettings userOofSettings;

		// Token: 0x04001C41 RID: 7233
		private int lvRPContainerTop;

		// Token: 0x04001C42 RID: 7234
		private string[] externalScriptFiles = new string[]
		{
			"uview.js",
			"vlv.js"
		};

		// Token: 0x04001C43 RID: 7235
		private string[] externalScriptFilesForAnnotation = new string[]
		{
			"MessageAnnotationDialog.js"
		};
	}
}
