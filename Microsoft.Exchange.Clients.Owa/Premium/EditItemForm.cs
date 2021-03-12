using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web.Configuration;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000368 RID: 872
	public abstract class EditItemForm : OwaForm, IRegistryOnlyForm
	{
		// Token: 0x060020D5 RID: 8405 RVA: 0x000BD33C File Offset: 0x000BB53C
		internal EditItemForm()
		{
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x000BD344 File Offset: 0x000BB544
		internal EditItemForm(bool setNoCacheNoStore) : base(setNoCacheNoStore)
		{
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x000BD350 File Offset: 0x000BB550
		protected override void OnLoad(EventArgs e)
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "exdltdrft", false);
			if (!string.IsNullOrEmpty(queryStringParameter))
			{
				this.DeleteExistingDraft = true;
			}
			string queryStringParameter2 = Utilities.GetQueryStringParameter(base.Request, "fId", false);
			if (queryStringParameter2 != null)
			{
				this.targetFolderId = OwaStoreObjectId.CreateFromString(queryStringParameter2);
			}
			if (base.Item == null)
			{
				string queryStringParameter3 = Utilities.GetQueryStringParameter(base.Request, "email", false);
				if (!string.IsNullOrEmpty(queryStringParameter3))
				{
					StoreObjectId mailboxItemStoreObjectId = null;
					if (MailToParser.TryParseMailTo(queryStringParameter3, base.UserContext, out mailboxItemStoreObjectId))
					{
						base.OwaContext.PreFormActionId = OwaStoreObjectId.CreateFromMailboxItemId(mailboxItemStoreObjectId);
						this.DeleteExistingDraft = true;
					}
				}
			}
			string queryStringParameter4 = Utilities.GetQueryStringParameter(base.Request, "fRABcc", false);
			this.isReplyAllBcc = (0 == string.CompareOrdinal("1", queryStringParameter4));
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x060020D8 RID: 8408 RVA: 0x000BD413 File Offset: 0x000BB613
		// (set) Token: 0x060020D9 RID: 8409 RVA: 0x000BD41B File Offset: 0x000BB61B
		protected bool DeleteExistingDraft
		{
			get
			{
				return this.deleteExistingDraft;
			}
			set
			{
				this.deleteExistingDraft = value;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x000BD424 File Offset: 0x000BB624
		internal OwaStoreObjectId TargetFolderId
		{
			get
			{
				return this.targetFolderId;
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x060020DB RID: 8411 RVA: 0x000BD42C File Offset: 0x000BB62C
		protected bool IsTargetFolderIdNull
		{
			get
			{
				return this.TargetFolderId == null;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x060020DC RID: 8412 RVA: 0x000BD437 File Offset: 0x000BB637
		protected override bool IsPublicItem
		{
			get
			{
				if (base.Item != null)
				{
					return base.IsPublicItem;
				}
				return !this.IsTargetFolderIdNull && this.TargetFolderId.IsPublic;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x060020DD RID: 8413 RVA: 0x000BD45D File Offset: 0x000BB65D
		protected override bool IsOtherMailboxItem
		{
			get
			{
				if (base.Item != null)
				{
					return base.IsOtherMailboxItem;
				}
				return !this.IsTargetFolderIdNull && this.TargetFolderId.IsOtherMailbox;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x060020DE RID: 8414 RVA: 0x000BD483 File Offset: 0x000BB683
		protected EditorContextMenu EditorContextMenu
		{
			get
			{
				if (this.editorContextMenu == null)
				{
					this.editorContextMenu = new EditorContextMenu(base.UserContext);
				}
				return this.editorContextMenu;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x060020DF RID: 8415 RVA: 0x000BD4A4 File Offset: 0x000BB6A4
		protected virtual ResizeImageMenu ResizeImageMenu
		{
			get
			{
				if (this.resizeImageMenu == null)
				{
					this.resizeImageMenu = new ResizeImageMenu(base.UserContext);
				}
				return this.resizeImageMenu;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x000BD4C5 File Offset: 0x000BB6C5
		protected bool IsReplyAllBcc
		{
			get
			{
				return this.isReplyAllBcc;
			}
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x000BD4CD File Offset: 0x000BB6CD
		protected virtual void RenderJavaScriptEncodedTargetFolderId()
		{
			if (this.TargetFolderId != null)
			{
				Utilities.JavascriptEncode(this.TargetFolderId.ToBase64String(), base.Response.Output);
			}
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x000BD4F2 File Offset: 0x000BB6F2
		protected void RenderDialogHelper()
		{
			base.SanitizingResponse.Write("<div class=\"offscreen\">");
			base.SanitizingResponse.Write("<object id=\"dlgHelper\" classid=\"clsid:3050f819-98b5-11cf-bb82-00aa00bdce0b\" width=\"0px\" height=\"0px\" viewastext></object>");
			base.SanitizingResponse.Write("</div>");
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x000BD524 File Offset: 0x000BB724
		protected void RenderSilverlightAttachmentManagerControl()
		{
			if (base.IsSilverlightEnabled)
			{
				int num = (base.UserContext.BrowserPlatform != BrowserPlatform.Macintosh) ? 0 : 1;
				int height = num;
				base.RenderSilverlight("AttachmentManager", "sl_attMgr", num, height, "<span></span>");
			}
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x000BD568 File Offset: 0x000BB768
		protected void RenderDataNeededBySilverlightAttachmentManager()
		{
			if (base.IsSilverlightEnabled)
			{
				RenderingUtilities.RenderSmallIconTable(base.SanitizingResponse, true);
				RenderingUtilities.RenderSmallIconTable(base.SanitizingResponse, false);
				RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "L_OpenParen", 6409762);
				RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "L_CloseParen", -1023695022);
				RenderingUtilities.RenderWebReadyPolicy(base.SanitizingResponse, base.UserContext);
				RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "a_sWRDV", AttachmentWell.GetWebReadyLink(base.UserContext));
				RenderingUtilities.RenderAttachmentBlockingPolicy(base.SanitizingResponse, base.UserContext, false);
				RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "L_WrnLevelOneReadWrite", -2118248931);
				RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "a_sMBG", base.UserContext.ExchangePrincipal.MailboxInfo.MailboxGuid.ToString());
				RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "L_ErrAttSilverlightFailure", 1330586559);
				RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "L_ErrCannotConnectToEwsRetry", -158529231);
				RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "L_ErrFileNotFound", 469568033);
				RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "L_ErrFileAlreadyInUse", -1934316340);
				RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "L_ErrAttTooLrg", -178989031);
				RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "L_ErrSingleAttTooLrg", 1582744855);
				RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "L_AttNmDiv", 440361970);
				this.RenderMaximumRequestLength(base.SanitizingResponse);
				this.RenderMaximumUserMessageSize(base.UserContext, base.SanitizingResponse);
				RenderingUtilities.RenderInteger(base.SanitizingResponse, "a_iEMOEA", 65536);
				RenderingUtilities.RenderInteger(base.SanitizingResponse, "a_iMaxAtt", 499);
				RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "L_ErrAttTooMany", SanitizedHtmlString.Format(LocalizedStrings.GetHtmlEncoded(1025276934), new object[]
				{
					499
				}));
				int input = 0;
				if (base.Item != null)
				{
					AttachmentCollection attachmentCollection = Utilities.GetAttachmentCollection(base.Item, false, base.UserContext);
					input = AttachmentUtility.GetTotalAttachmentSize(attachmentCollection);
				}
				RenderingUtilities.RenderInteger(base.SanitizingResponse, "a_sl_iTotalAttachmentSize", input);
				RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "L_ImgFileTypes", -16092782);
				RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "L_FileTypesSep", 952162300);
				RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "L_InsertingInlineImage", -242321595);
				RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "L_ErrNonImageFile", -1293887935);
				this.RenderDelegateMailboxInfo();
			}
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x000BD7E8 File Offset: 0x000BB9E8
		private void RenderDelegateMailboxInfo()
		{
			if (base.Item != null && base.UserContext != null)
			{
				MailboxSession mailboxSession = base.Item.Session as MailboxSession;
				if (mailboxSession != null)
				{
					IExchangePrincipal mailboxOwner = mailboxSession.MailboxOwner;
					if (mailboxOwner != null && mailboxOwner != base.UserContext.ExchangePrincipal)
					{
						RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "a_sDelegateMailboxGuid", mailboxOwner.MailboxInfo.MailboxGuid.ToString());
						byte[] bytes = Encoding.UTF8.GetBytes(mailboxOwner.LegacyDn);
						RenderingUtilities.RenderStringVariable(base.SanitizingResponse, "a_sDelegateMailboxLegacyDN", Convert.ToBase64String(bytes));
					}
				}
			}
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000BD884 File Offset: 0x000BBA84
		protected void RenderMaximumRequestLength(TextWriter writer)
		{
			int input = 4194304;
			System.Configuration.Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
			HttpRuntimeSection httpRuntimeSection = configuration.GetSection("system.web/httpRuntime") as HttpRuntimeSection;
			if (httpRuntimeSection != null)
			{
				input = httpRuntimeSection.MaxRequestLength * 1024;
			}
			RenderingUtilities.RenderInteger(writer, "a_iMaxRequestLength", input);
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000BD8D0 File Offset: 0x000BBAD0
		protected void RenderMaximumUserMessageSize(UserContext userContext, TextWriter writer)
		{
			int? maximumMessageSize = Utilities.GetMaximumMessageSize(userContext);
			if (maximumMessageSize == null)
			{
				return;
			}
			RenderingUtilities.RenderInteger(writer, "a_iMUMS", maximumMessageSize.Value / 1024);
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000BD906 File Offset: 0x000BBB06
		protected void RenderEditorIframe(string className)
		{
			this.RenderIframe("ifBdy", className);
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000BD914 File Offset: 0x000BBB14
		private void RenderIframe(string id, string className)
		{
			base.SanitizingResponse.Write("<iframe id=\"");
			base.SanitizingResponse.Write(id);
			base.SanitizingResponse.Write("\" ");
			base.SanitizingResponse.Write("class=");
			base.SanitizingResponse.Write("'");
			base.SanitizingResponse.Write(className);
			base.SanitizingResponse.Write("'");
			base.SanitizingResponse.Write(" frameborder=\"0\" src=\"");
			base.UserContext.RenderBlankPage(Utilities.PremiumScriptPath, base.SanitizingResponse);
			base.SanitizingResponse.Write("\"></iframe>");
		}

		// Token: 0x04001781 RID: 6017
		private const string FolderIDParameterName = "fId";

		// Token: 0x04001782 RID: 6018
		private const string ReplyAllBccParameterName = "fRABcc";

		// Token: 0x04001783 RID: 6019
		protected const string OpenAction = "Open";

		// Token: 0x04001784 RID: 6020
		private OwaStoreObjectId targetFolderId;

		// Token: 0x04001785 RID: 6021
		private bool deleteExistingDraft;

		// Token: 0x04001786 RID: 6022
		private EditorContextMenu editorContextMenu;

		// Token: 0x04001787 RID: 6023
		private ResizeImageMenu resizeImageMenu;

		// Token: 0x04001788 RID: 6024
		private bool isReplyAllBcc;
	}
}
