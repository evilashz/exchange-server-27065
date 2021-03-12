using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using Microsoft.Exchange.Clients.Common;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200047A RID: 1146
	public class StartPage : NavigationHost, IRegistryOnlyForm
	{
		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06002BE4 RID: 11236 RVA: 0x000F52BE File Offset: 0x000F34BE
		protected string QuotaWarningMessage
		{
			get
			{
				return this.quotaWarningMessage;
			}
		}

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06002BE5 RID: 11237 RVA: 0x000F52C6 File Offset: 0x000F34C6
		protected string QuotaExceededMessage
		{
			get
			{
				return this.quotaExceededMessage;
			}
		}

		// Token: 0x17000CFC RID: 3324
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x000F52CE File Offset: 0x000F34CE
		protected string MySiteUrl
		{
			get
			{
				return Utilities.GetHomePageForMailboxUser(base.OwaContext);
			}
		}

		// Token: 0x17000CFD RID: 3325
		// (get) Token: 0x06002BE7 RID: 11239 RVA: 0x000F52DC File Offset: 0x000F34DC
		protected bool IsReInitializationRequest
		{
			get
			{
				if (this.isReInitializationRequest == null)
				{
					this.isReInitializationRequest = new bool?(!string.IsNullOrEmpty(base.OwaContext.HttpContext.Request.QueryString["reInit"]));
				}
				return this.isReInitializationRequest == true;
			}
		}

		// Token: 0x06002BE8 RID: 11240 RVA: 0x000F5344 File Offset: 0x000F3544
		protected override NavigationModule SelectNavagationModule()
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "rru", false);
			if (string.Equals(queryStringParameter, "contacts", StringComparison.Ordinal))
			{
				return NavigationModule.Contacts;
			}
			string queryStringParameter2 = Utilities.GetQueryStringParameter(base.Request, "modurl", false);
			if (queryStringParameter2 != null)
			{
				int num;
				if (!int.TryParse(queryStringParameter2, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
				{
					throw new OwaInvalidRequestException("Invalid modurl querystring parameter");
				}
				if (num >= 0 && num <= 7)
				{
					return (NavigationModule)num;
				}
			}
			return NavigationModule.Mail;
		}

		// Token: 0x06002BE9 RID: 11241 RVA: 0x000F53B0 File Offset: 0x000F35B0
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.navigationBar = new NavigationBar(base.UserContext);
			InstantMessageManager instantMessageManager = base.UserContext.InstantMessageManager;
			if (!this.IsReInitializationRequest)
			{
				base.UserContext.PendingRequestManager.HandleFinishRequestFromClient(true);
			}
			if (instantMessageManager != null && !this.IsReInitializationRequest)
			{
				instantMessageManager.StartProvider();
			}
			if (base.UserContext.IsFeatureEnabled(Feature.Notifications))
			{
				if (base.UserContext.IsPushNotificationsEnabled)
				{
					base.UserContext.MapiNotificationManager.SubscribeForFolderCounts(null, base.UserContext.MailboxSession);
					if (base.UserContext.UserOptions.EnableReminders)
					{
						base.UserContext.MapiNotificationManager.SubscribeForReminders();
					}
					if (base.UserContext.UserOptions.NewItemNotify != NewNotification.None)
					{
						base.UserContext.MapiNotificationManager.SubscribeForNewMail();
					}
					base.UserContext.MapiNotificationManager.SubscribeForSubscriptionChanges();
				}
				if (base.UserContext.IsPullNotificationsEnabled)
				{
					if (base.UserContext.NotificationManager.FolderCountAdvisor == null)
					{
						base.UserContext.NotificationManager.CreateOwaFolderCountAdvisor(base.UserContext.MailboxSession);
					}
					Dictionary<OwaStoreObjectId, OwaConditionAdvisor> conditionAdvisorTable = base.UserContext.NotificationManager.ConditionAdvisorTable;
					if (base.UserContext.UserOptions.EnableReminders && (conditionAdvisorTable == null || !conditionAdvisorTable.ContainsKey(base.UserContext.RemindersSearchFolderOwaId)))
					{
						base.UserContext.NotificationManager.CreateOwaConditionAdvisor(base.UserContext, base.UserContext.MailboxSession, base.UserContext.RemindersSearchFolderOwaId, EventObjectType.None, EventType.None);
					}
					if (base.UserContext.UserOptions.NewItemNotify != NewNotification.None && base.UserContext.NotificationManager.LastEventAdvisor == null)
					{
						base.UserContext.NotificationManager.CreateOwaLastEventAdvisor(base.UserContext, base.UserContext.InboxFolderId, EventObjectType.Item, EventType.NewMail);
					}
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			using (StringWriter stringWriter = new StringWriter(stringBuilder))
			{
				Utilities.RenderSizeWithUnits(stringWriter, base.UserContext.QuotaSend, false);
			}
			this.quotaWarningMessage = string.Format(LocalizedStrings.GetHtmlEncoded(406437542), stringBuilder.ToString());
			this.quotaExceededMessage = string.Format(LocalizedStrings.GetHtmlEncoded(611216529), stringBuilder.ToString());
			if (this.IsReInitializationRequest)
			{
				PerformanceCounterManager.ProcessUserContextReInitializationRequest();
			}
			if (base.UserContext.IsClientSideDataCollectingEnabled)
			{
				this.UpdateClientCookie("owacsdc", "1");
				return;
			}
			Utilities.DeleteCookie(base.OwaContext.HttpContext.Response, "owacsdc");
		}

		// Token: 0x06002BEA RID: 11242 RVA: 0x000F564C File Offset: 0x000F384C
		protected void UpdateClientCookie(string cookieName, string cookieValue)
		{
			HttpCookie httpCookie = new HttpCookie(cookieName);
			httpCookie.Value = cookieValue;
			base.OwaContext.HttpContext.Response.Cookies.Add(httpCookie);
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06002BEB RID: 11243 RVA: 0x000F5682 File Offset: 0x000F3882
		protected RecipientWell RecipientWell
		{
			get
			{
				return this.recipientWell;
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x000F568A File Offset: 0x000F388A
		protected int InstantMessagingType
		{
			get
			{
				return (int)base.UserContext.InstantMessagingType;
			}
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x000F5698 File Offset: 0x000F3898
		protected void RenderNoScriptInfobar()
		{
			SanitizedHtmlString noScriptHtml = Utilities.GetNoScriptHtml();
			RenderingUtilities.RenderError(base.UserContext, base.Response.Output, noScriptHtml);
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x000F56C2 File Offset: 0x000F38C2
		protected void RenderJavascriptEncodedInboxFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.InboxFolderId.ToBase64String(), base.Response.Output);
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x000F56E4 File Offset: 0x000F38E4
		protected void RenderJavascriptEncodedCalendarFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.CalendarFolderId.ToBase64String(), base.Response.Output);
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000F5706 File Offset: 0x000F3906
		protected void RenderJavascriptEncodedContactsFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.ContactsFolderId.ToBase64String(), base.Response.Output);
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x000F5728 File Offset: 0x000F3928
		protected void RenderJavascriptEncodedFlaggedItemsAndTasksFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.FlaggedItemsAndTasksFolderId.ToBase64String(), base.Response.Output);
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x000F574A File Offset: 0x000F394A
		protected void RenderJavascriptEncodedJunkEmailFolderId()
		{
			Utilities.JavascriptEncode(base.UserContext.JunkEmailFolderId.ToBase64String(), base.Response.Output);
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x000F576C File Offset: 0x000F396C
		protected void RenderJavascriptEncodedPublicFolderRootId()
		{
			string text = null;
			if (base.NavigationModule == NavigationModule.PublicFolders)
			{
				text = base.UserContext.TryGetPublicFolderRootIdString();
			}
			if (text == null)
			{
				text = string.Empty;
			}
			Utilities.JavascriptEncode(text, base.Response.Output);
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x000F57AC File Offset: 0x000F39AC
		protected void RenderUserTile()
		{
			base.Response.Write("<div id=\"divUserTile\">");
			bool flag = base.UserContext.IsSenderPhotosFeatureEnabled(Feature.SetPhoto);
			bool flag2 = base.UserContext.IsSenderPhotosFeatureEnabled(Feature.DisplayPhotos) || flag;
			bool flag3 = base.UserContext.IsFeatureEnabled(Feature.ExplicitLogon);
			bool flag4 = base.UserContext.IsInstantMessageEnabled();
			if (flag4 || flag3 || flag2)
			{
				base.Response.Write("<a id=\"aUserTile\"  hidefocus href=\"#\" ");
				if (flag4 || flag)
				{
					base.RenderOnClick("opnMeCardMenu();");
					base.RenderScriptHandler("oncontextmenu", "opnMeCardMenu();");
				}
				else if (flag3)
				{
					base.RenderOnClick("showOpenMailboxDialog();");
				}
				base.Response.Write("><span id=\"spnUserTileTxt\" class=\"userTileTxt\">");
				Utilities.RenderDirectionEnhancedValue(base.Response.Output, Utilities.HtmlEncode(base.ExchangePrincipalDisplayName), base.UserContext.IsRtl);
				base.Response.Write("</span>");
				if (flag4)
				{
					ThemeFileId themeFileId;
					if (flag2)
					{
						themeFileId = (this.IsInstantMessagingTypeOcs(base.UserContext) ? ThemeFileId.PresenceUnknownVbarSmall : ThemeFileId.PresenceOfflineVbarSmall);
					}
					else
					{
						themeFileId = (this.IsInstantMessagingTypeOcs(base.UserContext) ? ThemeFileId.PresenceUnknown : ThemeFileId.PresenceOffline);
					}
					base.UserContext.RenderThemeImage(base.Response.Output, themeFileId, flag2 ? "VbarSm" : "noPic", new object[]
					{
						"id=\"imgUserTileJB\" unselectable=\"on\""
					});
				}
				if (flag2)
				{
					string adpictureUrl = RenderingUtilities.GetADPictureUrl(base.UserContext.ExchangePrincipal.LegacyDn, "EX", base.UserContext, true);
					RenderingUtilities.RenderDisplayPicture(base.Response.Output, base.UserContext, adpictureUrl, 32, true, ThemeFileId.DoughboyPersonSmall);
				}
				if (flag4 || flag || flag3)
				{
					base.UserContext.RenderThemeImage(base.Response.Output, ThemeFileId.UserTileDropDownArrow, null, new object[]
					{
						"id=\"imgUserTileDD\""
					});
				}
				base.Response.Write("</a>");
			}
			else
			{
				base.Response.Write("<span id=\"spnUserTileTxt\" class=\"userTileTxt\">");
				Utilities.RenderDirectionEnhancedValue(base.Response.Output, Utilities.HtmlEncode(base.ExchangePrincipalDisplayName), base.UserContext.IsRtl);
				base.Response.Write("</span>");
			}
			base.Response.Write("</div>");
		}

		// Token: 0x06002BF5 RID: 11253 RVA: 0x000F5A0D File Offset: 0x000F3C0D
		protected void RenderHelpContextMenu()
		{
			new HelpContextMenu(base.UserContext).Render(base.Response.Output);
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x000F5A2C File Offset: 0x000F3C2C
		protected void RenderNonMailModuleContextMenu()
		{
			NonMailModuleContextMenu nonMailModuleContextMenu = new NonMailModuleContextMenu(base.UserContext);
			nonMailModuleContextMenu.Render(base.Response.Output);
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x000F5A56 File Offset: 0x000F3C56
		protected void RenderNavigationBar(NavigationBarType type)
		{
			this.navigationBar.Render(base.Response.Output, base.NavigationModule, type);
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x000F5A75 File Offset: 0x000F3C75
		protected int GetNavigationBarHeight(NavigationBarType type)
		{
			return this.navigationBar.GetNavigationBarHeight(type);
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x000F5A84 File Offset: 0x000F3C84
		protected void RenderMeCardContextMenu()
		{
			InstantMessagePresenceContextMenu instantMessagePresenceContextMenu = new InstantMessagePresenceContextMenu(base.UserContext);
			instantMessagePresenceContextMenu.Render(base.Response.Output);
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x000F5AB0 File Offset: 0x000F3CB0
		protected void RenderJavascriptEncodedDisplayName()
		{
			string mailboxOwnerDisplayName = Utilities.GetMailboxOwnerDisplayName(base.UserContext.MailboxSession);
			Utilities.JavascriptEncode(mailboxOwnerDisplayName ?? string.Empty, base.Response.Output);
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000F5AE8 File Offset: 0x000F3CE8
		protected void RenderJavascriptEncodedLegacyDN()
		{
			Utilities.JavascriptEncode(base.UserContext.ExchangePrincipal.LegacyDn, base.Response.Output);
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000F5B0A File Offset: 0x000F3D0A
		protected void RenderJavascriptEncodedSipUri()
		{
			if (this.IsInstantMessagingTypeOcs(base.UserContext))
			{
				Utilities.JavascriptEncode(base.UserContext.SipUri ?? string.Empty, base.Response.Output);
			}
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x000F5B40 File Offset: 0x000F3D40
		protected void RenderJavascriptEncodedSetPhotoUrl()
		{
			if (base.UserContext.IsSenderPhotosFeatureEnabled(Feature.SetPhoto) && !string.IsNullOrEmpty(base.UserContext.SetPhotoURL))
			{
				Utilities.JavascriptEncode(base.UserContext.SetPhotoURL, base.Response.Output);
			}
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x000F5B90 File Offset: 0x000F3D90
		protected bool IsInstantMessagingTypeOcs(UserContext userContext)
		{
			return base.UserContext.InstantMessagingType == InstantMessagingTypeOptions.Ocs;
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x000F5BA0 File Offset: 0x000F3DA0
		protected void RenderBrandBarLogo()
		{
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000F5BA2 File Offset: 0x000F3DA2
		protected void RenderBrandBarHeaderLinks()
		{
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x000F5BA4 File Offset: 0x000F3DA4
		protected void RenderBrandThemeMask(TextWriter output, UserContext userContext)
		{
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x000F5BA6 File Offset: 0x000F3DA6
		protected void RenderBrandHeaderMask(TextWriter output, UserContext userContext)
		{
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x000F5BA8 File Offset: 0x000F3DA8
		protected void RenderLiveHeaderContextMenus()
		{
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x000F5BAA File Offset: 0x000F3DAA
		protected void RenderLiveHeaderMenuLinkIds(TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x000F5BBC File Offset: 0x000F3DBC
		protected void RenderBreadcrumbs()
		{
			Breadcrumbs breadcrumbs = new Breadcrumbs(base.UserContext, base.NavigationModule);
			breadcrumbs.Render(base.Response.Output);
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x000F5BEC File Offset: 0x000F3DEC
		protected void EndOfStartpage()
		{
			base.UserContext.SetFullyInitialized();
		}

		// Token: 0x04001D05 RID: 7429
		public const string ClientSideDataCollectionCookieName = "owacsdc";

		// Token: 0x04001D06 RID: 7430
		private MessageRecipientWell recipientWell = new MessageRecipientWell();

		// Token: 0x04001D07 RID: 7431
		private string quotaWarningMessage;

		// Token: 0x04001D08 RID: 7432
		private string quotaExceededMessage;

		// Token: 0x04001D09 RID: 7433
		private NavigationBar navigationBar;

		// Token: 0x04001D0A RID: 7434
		private bool? isReInitializationRequest = null;
	}
}
