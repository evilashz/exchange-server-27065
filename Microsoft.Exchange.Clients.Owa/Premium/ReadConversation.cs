using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200046E RID: 1134
	public class ReadConversation : OwaFormSubPage, IRegistryOnlyForm
	{
		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06002AC4 RID: 10948 RVA: 0x000F08D6 File Offset: 0x000EEAD6
		protected override bool IsIgnoredConversation
		{
			get
			{
				return ConversationUtilities.IsConversationIgnored(base.UserContext, this.conversationId, this.conversation);
			}
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x000F08F0 File Offset: 0x000EEAF0
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string parameter = base.GetParameter("id", true);
			this.searchWords = base.GetParameter("sw", false);
			this.conversationId = OwaStoreObjectId.CreateFromString(parameter);
			this.conversation = ConversationUtilities.LoadConversation(base.UserContext, this.conversationId, ItemPartWriter.GetRequestedProperties());
			this.conversation.TrimToNewest(Globals.MaxItemsInConversationReadingPane);
			MailboxSession mailboxSession = (MailboxSession)this.conversationId.GetSession(base.UserContext);
			this.sentItemsFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.SentItems);
			this.localItemIds = ConversationUtilities.GetLocalItemIds(mailboxSession, this.conversation, this.conversationId.ParentFolderId);
			base.OwaContext.ShouldDeferInlineImages = !base.IsInOEHResponse;
			this.conversation.OnBeforeItemLoad += ItemPartWriter.OnBeforeItemLoad;
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x000F09C8 File Offset: 0x000EEBC8
		protected void RenderDataExpandos()
		{
			MailboxSession mailboxSession = (MailboxSession)this.conversationId.GetSession(base.UserContext);
			OwaStoreObjectId deletedItemsFolderId = base.UserContext.GetDeletedItemsFolderId(mailboxSession);
			RenderingUtilities.RenderExpando(base.SanitizingResponse, "sCnvId", this.conversationId.ToString());
			RenderingUtilities.RenderExpando(base.SanitizingResponse, "iMR", (int)base.UserContext.UserOptions.PreviewMarkAsRead);
			RenderingUtilities.RenderExpando(base.SanitizingResponse, "iMRDly", base.UserContext.UserOptions.MarkAsReadDelaytime);
			RenderingUtilities.RenderExpando(base.SanitizingResponse, "fHideDelItems", ConversationUtilities.HideDeletedItems ? 1 : 0);
			RenderingUtilities.RenderExpando(base.SanitizingResponse, "sDeletedItemsId", deletedItemsFolderId.ToString());
			RenderingUtilities.RenderExpando(base.SanitizingResponse, "iGC", ConversationUtilities.GetGlobalCount(this.conversation));
			RenderingUtilities.RenderExpando(base.SanitizingResponse, "iC", this.localItemIds.Count);
			RenderingUtilities.RenderExpando(base.SanitizingResponse, "iSort", (int)base.UserContext.UserOptions.ConversationSortOrder);
			RenderingUtilities.RenderExpando(base.SanitizingResponse, "iMaxItemPrts", Globals.MaxItemsInConversationReadingPane);
			RenderingUtilities.RenderExpando(base.SanitizingResponse, "fIsConversationIgnored", this.IsIgnoredConversation ? 1 : 0);
			string text = string.Format(CultureInfo.InvariantCulture, "<a id=\"aIbBlk\" href=\"#\">{0}</a>", new object[]
			{
				LocalizedStrings.GetHtmlEncoded(469213884)
			});
			string value = string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetNonEncoded(2063285740), new object[]
			{
				text
			});
			RenderingUtilities.RenderExpando(base.SanitizingResponse, "L_ImgFltBlock", value);
			RenderingUtilities.RenderExpando(base.SanitizingResponse, "L_ImgFltCompBlock", SanitizedHtmlString.FromStringId(-1196115124));
			text = string.Format(CultureInfo.InvariantCulture, "<a id=\"aIbNotSup\" href=\"#\">{0}</a>", new object[]
			{
				LocalizedStrings.GetHtmlEncoded(1099573627)
			});
			value = string.Format(CultureInfo.InvariantCulture, LocalizedStrings.GetNonEncoded(-1170788421), new object[]
			{
				text
			});
			RenderingUtilities.RenderExpando(base.SanitizingResponse, "L_TypeNotSup", value);
			RenderingUtilities.RenderExpando(base.SanitizingResponse, "L_OpnInOlk", SanitizedHtmlString.FromStringId(1305715400));
			text = string.Format(CultureInfo.InvariantCulture, "<a id=\"aIbReadRcp\" href=\"#\">{0}</a>", new object[]
			{
				LocalizedStrings.GetHtmlEncoded(1190033799)
			});
			RenderingUtilities.RenderExpando(base.SanitizingResponse, "L_ReadRcp", SanitizedHtmlString.Format("{0} {1}", new object[]
			{
				SanitizedHtmlString.FromStringId(115261126),
				text
			}));
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x000F0C5C File Offset: 0x000EEE5C
		protected void RenderConversationTopic()
		{
			string text = string.Empty;
			if (ConversationUtilities.IsSmsConversation(this.conversation))
			{
				text = ConversationUtilities.GenerateSmsConversationTitle(this.sentItemsFolderId, this.conversation);
			}
			if (string.IsNullOrEmpty(text))
			{
				text = this.conversation.Topic;
			}
			Utilities.SanitizeHtmlEncode(ConversationUtilities.MaskConversationSubject(text), base.Response.Output);
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x000F0CB8 File Offset: 0x000EEEB8
		protected void RenderItemParts()
		{
			ConversationUtilities.RenderItemParts(base.SanitizingResponse, base.UserContext, this.conversationId, this.conversation, null, null, this.localItemIds, this.searchWords);
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06002AC9 RID: 10953 RVA: 0x000F0CE5 File Offset: 0x000EEEE5
		public override IEnumerable<string> ExternalScriptFiles
		{
			get
			{
				return this.externalScriptFiles;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06002ACA RID: 10954 RVA: 0x000F0CED File Offset: 0x000EEEED
		public override SanitizedHtmlString Title
		{
			get
			{
				return new SanitizedHtmlString(this.conversation.Topic);
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06002ACB RID: 10955 RVA: 0x000F0CFF File Offset: 0x000EEEFF
		public override string PageType
		{
			get
			{
				return "ReadConversationPage";
			}
		}

		// Token: 0x04001CB9 RID: 7353
		private const string SearchKey = "sw";

		// Token: 0x04001CBA RID: 7354
		private OwaStoreObjectId conversationId;

		// Token: 0x04001CBB RID: 7355
		private Conversation conversation;

		// Token: 0x04001CBC RID: 7356
		private List<StoreObjectId> localItemIds;

		// Token: 0x04001CBD RID: 7357
		private string searchWords;

		// Token: 0x04001CBE RID: 7358
		private StoreObjectId sentItemsFolderId;

		// Token: 0x04001CBF RID: 7359
		private string[] externalScriptFiles = new string[]
		{
			"freadcnv.js"
		};
	}
}
