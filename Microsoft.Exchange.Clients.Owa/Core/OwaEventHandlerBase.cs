using System;
using System.Collections;
using System.IO;
using System.Web;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000187 RID: 391
	internal abstract class OwaEventHandlerBase : DisposeTrackableBase
	{
		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0005BA1A File Offset: 0x00059C1A
		public static bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x0005BA1D File Offset: 0x00059C1D
		internal static bool ShouldIgnoreRequest(OwaContext owaContext, UserContext userContext)
		{
			return PendingRequestEventHandler.IsObsoleteRequest(owaContext, userContext);
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0005BA26 File Offset: 0x00059C26
		// (set) Token: 0x06000E4A RID: 3658 RVA: 0x0005BA2E File Offset: 0x00059C2E
		public OwaEventAttribute EventInfo
		{
			get
			{
				return this.eventInfo;
			}
			internal set
			{
				this.eventInfo = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0005BA37 File Offset: 0x00059C37
		// (set) Token: 0x06000E4C RID: 3660 RVA: 0x0005BA3F File Offset: 0x00059C3F
		public OwaContext OwaContext
		{
			get
			{
				return this.owaContext;
			}
			internal set
			{
				this.owaContext = value;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000E4D RID: 3661 RVA: 0x0005BA48 File Offset: 0x00059C48
		// (set) Token: 0x06000E4E RID: 3662 RVA: 0x0005BA55 File Offset: 0x00059C55
		public UserContext UserContext
		{
			get
			{
				return this.OwaContext.UserContext;
			}
			set
			{
				this.OwaContext.UserContext = value;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000E4F RID: 3663 RVA: 0x0005BA63 File Offset: 0x00059C63
		public ISessionContext SessionContext
		{
			get
			{
				return this.OwaContext.SessionContext;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x0005BA70 File Offset: 0x00059C70
		public virtual HttpContext HttpContext
		{
			get
			{
				return this.OwaContext.HttpContext;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000E51 RID: 3665 RVA: 0x0005BA7D File Offset: 0x00059C7D
		// (set) Token: 0x06000E52 RID: 3666 RVA: 0x0005BA85 File Offset: 0x00059C85
		public OwaEventVerb Verb
		{
			get
			{
				return this.verb;
			}
			internal set
			{
				this.verb = value;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000E53 RID: 3667 RVA: 0x0005BA8E File Offset: 0x00059C8E
		// (set) Token: 0x06000E54 RID: 3668 RVA: 0x0005BA96 File Offset: 0x00059C96
		public OwaEventContentType ResponseContentType
		{
			get
			{
				return this.responseContentType;
			}
			set
			{
				this.responseContentType = value;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x0005BA9F File Offset: 0x00059C9F
		public virtual TextWriter Writer
		{
			get
			{
				return this.owaContext.HttpContext.Response.Output;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x0005BAB6 File Offset: 0x00059CB6
		public virtual TextWriter SanitizingWriter
		{
			get
			{
				return this.owaContext.SanitizingResponseWriter;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x0005BAC3 File Offset: 0x00059CC3
		// (set) Token: 0x06000E58 RID: 3672 RVA: 0x0005BACB File Offset: 0x00059CCB
		public bool DontWriteHeaders
		{
			get
			{
				return this.dontWriteHeaders;
			}
			set
			{
				this.dontWriteHeaders = value;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0005BAD4 File Offset: 0x00059CD4
		// (set) Token: 0x06000E5A RID: 3674 RVA: 0x0005BADC File Offset: 0x00059CDC
		public bool ShowErrorInPage
		{
			get
			{
				return this.showErrorInPage;
			}
			set
			{
				this.showErrorInPage = value;
			}
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x0005BAE5 File Offset: 0x00059CE5
		internal void SetParameterTable(Hashtable parameterTable)
		{
			this.parameterTable = parameterTable;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0005BAEE File Offset: 0x00059CEE
		internal object GetParameter(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.parameterTable[name];
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0005BB0A File Offset: 0x00059D0A
		protected bool IsParameterSet(string name)
		{
			return null != this.GetParameter(name);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0005BB19 File Offset: 0x00059D19
		protected void RenderPartialFailure(Strings.IDs messageString)
		{
			this.RenderPartialFailure(messageString, OwaEventHandlerErrorCode.NotSet);
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x0005BB24 File Offset: 0x00059D24
		protected void RenderPartialFailure(Strings.IDs messageString, OwaEventHandlerErrorCode errorCode)
		{
			this.RenderPartialFailure(messageString, null, ButtonDialogIcon.NotSet, errorCode);
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0005BB43 File Offset: 0x00059D43
		protected void RenderPartialFailure(Strings.IDs messageString, Strings.IDs? titleString, ButtonDialogIcon icon)
		{
			this.RenderPartialFailure(messageString, titleString, icon, OwaEventHandlerErrorCode.NotSet);
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x0005BB50 File Offset: 0x00059D50
		protected void RenderPartialFailure(Strings.IDs messageString, Strings.IDs? titleString, ButtonDialogIcon icon, OwaEventHandlerErrorCode errorCode)
		{
			this.RenderPartialFailure(LocalizedStrings.GetHtmlEncoded(messageString), (titleString != null) ? LocalizedStrings.GetHtmlEncoded(titleString.Value) : null, icon, errorCode);
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0005BB7C File Offset: 0x00059D7C
		protected void RenderPartialFailure(string messageHtml, string titleHtml, ButtonDialogIcon icon, OwaEventHandlerErrorCode errorCode)
		{
			if (messageHtml == null)
			{
				throw new ArgumentNullException("message");
			}
			this.Writer.Write("<div id=err _msg=\"");
			this.Writer.Write(messageHtml);
			this.Writer.Write("\"");
			if (errorCode != OwaEventHandlerErrorCode.NotSet)
			{
				this.Writer.Write(" _cd=");
				this.Writer.Write((int)errorCode);
			}
			if (titleHtml != null)
			{
				this.Writer.Write(" _ttl=\"");
				this.Writer.Write(titleHtml);
				this.Writer.Write("\"");
			}
			if (icon != ButtonDialogIcon.NotSet)
			{
				this.Writer.Write(" _icn=\"");
				this.Writer.Write((int)icon);
				this.Writer.Write("\"");
			}
			this.Writer.Write("></div>");
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0005BC55 File Offset: 0x00059E55
		protected override void InternalDispose(bool isDisposing)
		{
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0005BC57 File Offset: 0x00059E57
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaEventHandlerBase>(this);
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0005BC5F File Offset: 0x00059E5F
		protected void ThrowIfCannotActAsOwner()
		{
			if (!this.UserContext.CanActAsOwner)
			{
				throw new OwaAccessDeniedException(LocalizedStrings.GetNonEncoded(1622692336), true);
			}
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0005BC80 File Offset: 0x00059E80
		protected void SaveHideMailTipsByDefault()
		{
			if (this.IsParameterSet("HideMailTipsByDefault"))
			{
				bool flag = (bool)this.GetParameter("HideMailTipsByDefault");
				if (flag != this.UserContext.UserOptions.HideMailTipsByDefault)
				{
					this.UserContext.UserOptions.HideMailTipsByDefault = flag;
					this.UserContext.UserOptions.CommitChanges();
				}
			}
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0005BCE0 File Offset: 0x00059EE0
		protected void WriteNewItemId(Item item)
		{
			this.SanitizingWriter.Write("<div id=");
			this.SanitizingWriter.Write("itemId");
			this.SanitizingWriter.Write(">");
			this.SanitizingWriter.Write(item.Id.ObjectId.ToBase64String());
			this.SanitizingWriter.Write("</div>");
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0005BD48 File Offset: 0x00059F48
		protected void WriteChangeKey(Item item)
		{
			this.SanitizingWriter.Write("<div id=");
			this.SanitizingWriter.Write("ck");
			this.SanitizingWriter.Write(">");
			this.SanitizingWriter.Write(item.Id.ChangeKeyAsBase64String());
			this.SanitizingWriter.Write("</div>");
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x0005BDAB File Offset: 0x00059FAB
		protected void WriteIdAndChangeKey(Item item, bool existingItem)
		{
			item.Load();
			if (!existingItem)
			{
				this.WriteNewItemId(item);
			}
			this.WriteChangeKey(item);
		}

		// Token: 0x040009B4 RID: 2484
		private const int InitialTableCapacity = 4;

		// Token: 0x040009B5 RID: 2485
		private const string JavascriptContentType = "application/x-javascript";

		// Token: 0x040009B6 RID: 2486
		private const string HtmlContentType = "text/html";

		// Token: 0x040009B7 RID: 2487
		public const string HideMailTipsByDefault = "HideMailTipsByDefault";

		// Token: 0x040009B8 RID: 2488
		protected const string ItemIdKey = "itemId";

		// Token: 0x040009B9 RID: 2489
		protected const string ChangeKeyKey = "ck";

		// Token: 0x040009BA RID: 2490
		private OwaContext owaContext;

		// Token: 0x040009BB RID: 2491
		private OwaEventAttribute eventInfo;

		// Token: 0x040009BC RID: 2492
		private Hashtable parameterTable;

		// Token: 0x040009BD RID: 2493
		private OwaEventContentType responseContentType = OwaEventContentType.Html;

		// Token: 0x040009BE RID: 2494
		private OwaEventVerb verb;

		// Token: 0x040009BF RID: 2495
		private bool dontWriteHeaders;

		// Token: 0x040009C0 RID: 2496
		private bool showErrorInPage;
	}
}
