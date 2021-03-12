using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Core.Controls;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200045F RID: 1119
	public class OwaFormInternal : DisposeTrackableBase
	{
		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x060029B6 RID: 10678 RVA: 0x000EA594 File Offset: 0x000E8794
		// (set) Token: 0x060029B7 RID: 10679 RVA: 0x000EA59C File Offset: 0x000E879C
		private OwaContext OwaContext { get; set; }

		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x060029B8 RID: 10680 RVA: 0x000EA5A5 File Offset: 0x000E87A5
		private UserContext UserContext
		{
			get
			{
				return this.OwaContext.UserContext;
			}
		}

		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x060029B9 RID: 10681 RVA: 0x000EA5B2 File Offset: 0x000E87B2
		// (set) Token: 0x060029BA RID: 10682 RVA: 0x000EA5BA File Offset: 0x000E87BA
		private HttpRequest Request { get; set; }

		// Token: 0x060029BB RID: 10683 RVA: 0x000EA5C4 File Offset: 0x000E87C4
		internal OwaFormInternal(OwaContext owaContext)
		{
			this.responseWriter = owaContext.HttpContext.Response.Output;
			this.sanitizingResponseWriter = owaContext.SanitizingResponseWriter;
			this.OwaContext = owaContext;
			this.Request = this.OwaContext.HttpContext.Request;
		}

		// Token: 0x060029BC RID: 10684 RVA: 0x000EA616 File Offset: 0x000E8816
		internal T Initialize<T>(bool itemRequired, bool forceAsMessageItem, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			return this.Initialize<T>(itemRequired, forceAsMessageItem, Utilities.GetQueryStringParameter(this.Request, "id", false), this.OwaContext.FormsRegistryContext.Action, prefetchProperties);
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x000EA644 File Offset: 0x000E8844
		internal T Initialize<T>(bool itemRequired, bool forceAsMessageItem, string id, string action, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			object obj = null;
			object preFormActionId = this.OwaContext.PreFormActionId;
			if (preFormActionId != null)
			{
				OwaStoreObjectId owaStoreObjectId = (OwaStoreObjectId)preFormActionId;
				obj = Utilities.GetItem<T>(this.UserContext, owaStoreObjectId, forceAsMessageItem, prefetchProperties);
			}
			else if (!string.IsNullOrEmpty(id))
			{
				obj = Utilities.GetItemById<T>(this.OwaContext, out this.parentItem, id, forceAsMessageItem, prefetchProperties);
			}
			else if (itemRequired)
			{
				throw new OwaInvalidRequestException("Missing 'id' URL parameter");
			}
			this.item = (obj as Item);
			this.isEmbeddedItemInNonSMimeItem = (Utilities.GetQueryStringParameter(this.Request, "attcnt", false) != null);
			this.isEmbeddedItemInSMimeMessage = (Utilities.GetQueryStringParameter(this.Request, "smemb", false) != null);
			this.isPreviewForm = (action != null && action.Equals("Preview"));
			if (this.item != null && !this.IsEmbeddedItem && !this.IsPublicItem)
			{
				if (this.IsRequestCallbackForPhishing)
				{
					JunkEmailUtilities.SetLinkEnabled(this.item, prefetchProperties);
				}
				Utilities.SetWebBeaconPolicy(this.IsRequestCallbackForWebBeacons, this.item, prefetchProperties);
				if (this.IsRequestCallbackForRemoveIRM)
				{
					Utilities.IrmRemoveRestriction(this.item, this.UserContext);
				}
			}
			if (obj == null)
			{
				return default(T);
			}
			return (T)((object)obj);
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x000EA794 File Offset: 0x000E8994
		internal void MarkPayloadAsRead()
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(this.Request, "mrd", false);
			if (queryStringParameter != null)
			{
				JunkEmailStatus junkEmailStatus = JunkEmailStatus.Unknown;
				string queryStringParameter2 = Utilities.GetQueryStringParameter(this.Request, "JS", false);
				int num;
				if (queryStringParameter2 != null && int.TryParse(queryStringParameter2, out num) && (num == 1 || num == 0))
				{
					junkEmailStatus = (JunkEmailStatus)num;
				}
				OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromString(queryStringParameter);
				OwaStoreObjectId[] localItemIds = ConversationUtilities.GetLocalItemIds(this.UserContext, new OwaStoreObjectId[]
				{
					owaStoreObjectId
				}, null, new PropertyDefinition[]
				{
					MessageItemSchema.IsRead
				}, (IStorePropertyBag propertyBag) => !ItemUtility.GetProperty<bool>(propertyBag, MessageItemSchema.IsRead, true));
				if (localItemIds.Length > 0)
				{
					Utilities.MarkItemsAsRead(this.UserContext, localItemIds, junkEmailStatus, false);
				}
			}
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x000EA84F File Offset: 0x000E8A4F
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				if (this.parentItem != null)
				{
					this.parentItem.Dispose();
					this.parentItem = null;
				}
				if (this.item != null)
				{
					this.item.Dispose();
					this.item = null;
				}
			}
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x000EA888 File Offset: 0x000E8A88
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OwaFormInternal>(this);
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x000EA890 File Offset: 0x000E8A90
		internal string RenderEmbeddedUrl()
		{
			StringBuilder stringBuilder = new StringBuilder(100);
			stringBuilder.Append("attachment.ashx?attcnt=");
			stringBuilder.Append(this.EmbeddedItemNestingLevel + 1);
			stringBuilder.Append("&id=");
			stringBuilder.Append(Utilities.UrlEncode(this.ParentItemIdBase64String));
			for (int i = 0; i < this.EmbeddedItemNestingLevel; i++)
			{
				string text = "attid" + i.ToString(CultureInfo.InvariantCulture);
				string queryStringParameter = Utilities.GetQueryStringParameter(this.Request, text);
				stringBuilder.Append("&");
				stringBuilder.Append(text);
				stringBuilder.Append("=");
				stringBuilder.Append(Utilities.UrlEncode(queryStringParameter));
			}
			stringBuilder.Append("&attid" + this.EmbeddedItemNestingLevel.ToString(CultureInfo.InvariantCulture) + "=");
			return stringBuilder.ToString();
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x000EA970 File Offset: 0x000E8B70
		internal void RenderEmbeddedItemIds()
		{
			this.responseWriter.Write("new Array(\"");
			Utilities.JavascriptEncode(this.ParentItemIdBase64String, this.responseWriter);
			this.responseWriter.Write("\"");
			for (int i = 0; i < this.EmbeddedItemNestingLevel; i++)
			{
				string name = "attid" + i.ToString(CultureInfo.InvariantCulture);
				string queryStringParameter = Utilities.GetQueryStringParameter(this.Request, name);
				this.responseWriter.Write(",\"");
				Utilities.JavascriptEncode(queryStringParameter, this.responseWriter);
				this.responseWriter.Write("\"");
			}
			this.responseWriter.Write(")");
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x000EAA1F File Offset: 0x000E8C1F
		internal void RenderJavascriptEncodedItemChangeKey()
		{
			Utilities.JavascriptEncode(this.Item.Id.ChangeKeyAsBase64String(), this.sanitizingResponseWriter);
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x000EAA3C File Offset: 0x000E8C3C
		internal void RenderJavascriptEncodedItemId()
		{
			if (this.Item != null)
			{
				Utilities.JavascriptEncode(Utilities.GetIdAsString(this.Item), this.sanitizingResponseWriter);
			}
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x000EAA5C File Offset: 0x000E8C5C
		internal void RenderMessageInformation(TextWriter writer)
		{
			writer.WriteLine("var a_fEnIL = {0};", this.IsRequestCallbackForPhishing ? 1 : 0);
			writer.WriteLine("var a_fJoP = {0};", (this.Item == null) ? 0 : (this.IsJunkOrPhishing ? 1 : 0));
			writer.WriteLine("var a_fEmb = {0};", this.IsEmbeddedItem ? 1 : 0);
			writer.WriteLine("var a_fRp = {0};", (this.Item == null) ? 0 : (this.IsReportItem ? 1 : 0));
			writer.WriteLine("var a_iEmbD = {0};", Utilities.GetEmbeddedDepth(HttpContext.Current.Request));
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x000EAB10 File Offset: 0x000E8D10
		internal void RenderSubjectAttributes()
		{
			this.sanitizingResponseWriter.Write(" _fAllwCM=\"1\"");
			if (this.IsSubjectEditable)
			{
				this.sanitizingResponseWriter.Write(" TABINDEX=0 _editable=1");
			}
			if (this.UserContext.IsSmsEnabled && ObjectClass.IsSmsMessage(this.ItemClassName))
			{
				this.sanitizingResponseWriter.Write(" style=\"display:none\"");
			}
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x000EAB70 File Offset: 0x000E8D70
		internal void RenderDownloadAllAttachmentsLink()
		{
			if (this.shouldRenderDownloadAllLink)
			{
				string urlEncodedItemId;
				if (this.IsEmbeddedItemInNonSMimeItem)
				{
					urlEncodedItemId = Utilities.UrlEncode(this.ParentItemIdBase64String);
				}
				else
				{
					urlEncodedItemId = Utilities.UrlEncode(Utilities.GetIdAsString(this.item));
				}
				AttachmentUtility.RenderDownloadAllAttachmentsLink((SanitizingTextWriter<OwaHtml>)this.sanitizingResponseWriter, this.Request, urlEncodedItemId, this.IsEmbeddedItemInNonSMimeItem, this.UserContext, this.downloadAllCount);
			}
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x000EABD7 File Offset: 0x000E8DD7
		internal void RenderAttachmentWellForReadItem(ArrayList attachmentWellRenderObjects)
		{
			AttachmentWell.RenderAttachmentWell(this.responseWriter, AttachmentWellType.ReadOnly, attachmentWellRenderObjects, this.UserContext);
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x060029C9 RID: 10697 RVA: 0x000EABEC File Offset: 0x000E8DEC
		internal int EmbeddedItemNestingLevel
		{
			get
			{
				if (this.IsEmbeddedItem && this.embeddedItemNestingLevel == null)
				{
					this.embeddedItemNestingLevel = new int?(AttachmentUtility.GetEmbeddedItemNestingLevel(this.Request));
				}
				if (this.embeddedItemNestingLevel == null)
				{
					return 0;
				}
				return this.embeddedItemNestingLevel.Value;
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x060029CA RID: 10698 RVA: 0x000EAC3E File Offset: 0x000E8E3E
		internal bool ShowAttachmentWell
		{
			get
			{
				return this.Item != null && this.Item.AttachmentCollection != null && this.Item.AttachmentCollection.Count > 0;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x060029CB RID: 10699 RVA: 0x000EAC6A File Offset: 0x000E8E6A
		// (set) Token: 0x060029CC RID: 10700 RVA: 0x000EAC72 File Offset: 0x000E8E72
		internal Item Item
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x060029CD RID: 10701 RVA: 0x000EAC7B File Offset: 0x000E8E7B
		// (set) Token: 0x060029CE RID: 10702 RVA: 0x000EAC83 File Offset: 0x000E8E83
		internal IList<AttachmentLink> AttachmentLinks
		{
			get
			{
				return this.attachmentLinks;
			}
			set
			{
				this.attachmentLinks = value;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x060029CF RID: 10703 RVA: 0x000EAC8C File Offset: 0x000E8E8C
		// (set) Token: 0x060029D0 RID: 10704 RVA: 0x000EAC94 File Offset: 0x000E8E94
		internal bool IsPreviewForm
		{
			get
			{
				return this.isPreviewForm;
			}
			set
			{
				this.isPreviewForm = value;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x060029D1 RID: 10705 RVA: 0x000EAC9D File Offset: 0x000E8E9D
		internal bool HasCategories
		{
			get
			{
				return this.item != null && ItemUtility.HasCategories(this.item);
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x060029D2 RID: 10706 RVA: 0x000EACB4 File Offset: 0x000E8EB4
		internal bool IsEmbeddedItem
		{
			get
			{
				return this.IsEmbeddedItemInNonSMimeItem || this.IsEmbeddedItemInSMimeMessage;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x060029D3 RID: 10707 RVA: 0x000EACC6 File Offset: 0x000E8EC6
		internal bool IsPublicItem
		{
			get
			{
				return Utilities.IsPublic(this.item);
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x060029D4 RID: 10708 RVA: 0x000EACD3 File Offset: 0x000E8ED3
		internal bool IsOtherMailboxItem
		{
			get
			{
				return !this.IsItemNull && this.UserContext.IsInOtherMailbox(this.item);
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x060029D5 RID: 10709 RVA: 0x000EACF0 File Offset: 0x000E8EF0
		internal bool IsItemNull
		{
			get
			{
				return this.item == null;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x060029D6 RID: 10710 RVA: 0x000EACFB File Offset: 0x000E8EFB
		internal bool IsSubjectEditable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x060029D7 RID: 10711 RVA: 0x000EACFE File Offset: 0x000E8EFE
		internal bool IsItemEditable
		{
			get
			{
				return !this.IsEmbeddedItem && ItemUtility.UserCanEditItem(this.Item);
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x060029D8 RID: 10712 RVA: 0x000EAD15 File Offset: 0x000E8F15
		internal bool IsInDeleteItems
		{
			get
			{
				return this.item != null && !this.IsEmbeddedItemInNonSMimeItem && Utilities.IsItemInDefaultFolder(this.item, DefaultFolderType.DeletedItems);
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x060029D9 RID: 10713 RVA: 0x000EAD35 File Offset: 0x000E8F35
		internal bool UserCanDeleteItem
		{
			get
			{
				return this.IsItemNull || (!this.IsPublicItem && !this.IsOtherMailboxItem) || ItemUtility.UserCanDeleteItem(this.Item);
			}
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x060029DA RID: 10714 RVA: 0x000EAD5C File Offset: 0x000E8F5C
		internal bool UserCanEditItem
		{
			get
			{
				return this.IsItemNull || (!this.IsPublicItem && !this.IsOtherMailboxItem) || ItemUtility.UserCanEditItem(this.Item);
			}
		}

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x060029DB RID: 10715 RVA: 0x000EAD83 File Offset: 0x000E8F83
		internal string ParentItemIdBase64String
		{
			get
			{
				return OwaStoreObjectId.CreateFromStoreObject(this.parentItem).ToString();
			}
		}

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x060029DC RID: 10716 RVA: 0x000EAD95 File Offset: 0x000E8F95
		internal string ParentFolderIdBase64String
		{
			get
			{
				return this.ParentFolderId.ToBase64String();
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x060029DD RID: 10717 RVA: 0x000EADA4 File Offset: 0x000E8FA4
		internal OwaStoreObjectId ParentFolderId
		{
			get
			{
				Item item = this.IsEmbeddedItemInNonSMimeItem ? this.parentItem : this.item;
				OwaStoreObjectIdType objectIdType = OwaStoreObjectIdType.MailBoxObject;
				string legacyDN = null;
				if (Utilities.IsPublic(item))
				{
					objectIdType = OwaStoreObjectIdType.PublicStoreFolder;
				}
				else if (this.UserContext.IsInOtherMailbox(item))
				{
					objectIdType = OwaStoreObjectIdType.OtherUserMailboxObject;
					legacyDN = Utilities.GetMailboxSessionLegacyDN(item);
				}
				else if (Utilities.IsInArchiveMailbox(item))
				{
					objectIdType = OwaStoreObjectIdType.ArchiveMailboxObject;
					legacyDN = Utilities.GetMailboxSessionLegacyDN(item);
				}
				return OwaStoreObjectId.CreateFromFolderId(item.ParentId, objectIdType, legacyDN);
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x060029DE RID: 10718 RVA: 0x000EAE0F File Offset: 0x000E900F
		internal string ItemClassName
		{
			get
			{
				return this.Item.ClassName;
			}
		}

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x000EAE1C File Offset: 0x000E901C
		internal ClientSMimeControlStatus ClientSMimeControlStatus
		{
			get
			{
				if (this.clientSMimeControlStatus == ClientSMimeControlStatus.None)
				{
					this.clientSMimeControlStatus = Utilities.CheckClientSMimeControlStatus(Utilities.GetQueryStringParameter(this.Request, "smime", false), this.OwaContext);
				}
				return this.clientSMimeControlStatus;
			}
		}

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x060029E0 RID: 10720 RVA: 0x000EAE4E File Offset: 0x000E904E
		internal bool IsJunkOrPhishing
		{
			get
			{
				return JunkEmailUtilities.IsJunkOrPhishing(this.Item, this.IsEmbeddedItem, this.IsRequestCallbackForPhishing, this.UserContext);
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x060029E1 RID: 10721 RVA: 0x000EAE6D File Offset: 0x000E906D
		internal bool IsReportItem
		{
			get
			{
				return this.Item is ReportMessage;
			}
		}

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x060029E2 RID: 10722 RVA: 0x000EAE7D File Offset: 0x000E907D
		internal bool IsEmbeddedItemInSMimeMessage
		{
			get
			{
				return this.isEmbeddedItemInSMimeMessage;
			}
		}

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x060029E3 RID: 10723 RVA: 0x000EAE85 File Offset: 0x000E9085
		internal bool IsEmbeddedItemInNonSMimeItem
		{
			get
			{
				return this.isEmbeddedItemInNonSMimeItem;
			}
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x060029E4 RID: 10724 RVA: 0x000EAE8D File Offset: 0x000E908D
		internal bool IsRequestCallbackForPhishing
		{
			get
			{
				return Utilities.IsRequestCallbackForPhishing(this.Request);
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x060029E5 RID: 10725 RVA: 0x000EAE9A File Offset: 0x000E909A
		internal bool IsRequestCallbackForWebBeacons
		{
			get
			{
				return Utilities.IsRequestCallbackForWebBeacons(this.Request);
			}
		}

		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x060029E6 RID: 10726 RVA: 0x000EAEA7 File Offset: 0x000E90A7
		internal bool IsIgnoredConversation
		{
			get
			{
				return ConversationUtilities.IsConversationIgnored(this.item);
			}
		}

		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x060029E7 RID: 10727 RVA: 0x000EAEB4 File Offset: 0x000E90B4
		protected bool IsRequestCallbackForRemoveIRM
		{
			get
			{
				return !string.IsNullOrEmpty(Utilities.GetQueryStringParameter(this.Request, "rr", false));
			}
		}

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x060029E8 RID: 10728 RVA: 0x000EAECF File Offset: 0x000E90CF
		internal bool ShouldRenderDownloadAllLink
		{
			get
			{
				return this.shouldRenderDownloadAllLink;
			}
		}

		// Token: 0x060029E9 RID: 10729 RVA: 0x000EAED7 File Offset: 0x000E90D7
		internal void SetShouldRenderDownloadAllLink(ArrayList attachmentWellInfos)
		{
			if (!this.IsJunkOrPhishing && attachmentWellInfos != null && attachmentWellInfos.Count > 0)
			{
				this.downloadAllCount = AttachmentUtility.GetCountForDownloadAttachments(attachmentWellInfos);
				this.shouldRenderDownloadAllLink = (this.downloadAllCount > 1);
			}
		}

		// Token: 0x060029EA RID: 10730 RVA: 0x000EAF08 File Offset: 0x000E9108
		internal void RenderActionButtons(bool isInJunkMailFolder, bool isPost)
		{
			if (this.IsPreviewForm)
			{
				if (isInJunkMailFolder)
				{
					RenderingUtilities.RenderJunkMailActionIcons(this.responseWriter, this.UserContext);
					return;
				}
				RenderingUtilities.RenderActiveActionIcons(this.responseWriter, this.UserContext, isPost);
			}
		}

		// Token: 0x04001C45 RID: 7237
		internal const string PreviewFormString = "Preview";

		// Token: 0x04001C46 RID: 7238
		public const string MarkAsReadPiggyBack = "mrd";

		// Token: 0x04001C47 RID: 7239
		private Item item;

		// Token: 0x04001C48 RID: 7240
		private Item parentItem;

		// Token: 0x04001C49 RID: 7241
		private bool isPreviewForm;

		// Token: 0x04001C4A RID: 7242
		private bool isEmbeddedItemInNonSMimeItem;

		// Token: 0x04001C4B RID: 7243
		private bool isEmbeddedItemInSMimeMessage;

		// Token: 0x04001C4C RID: 7244
		private bool shouldRenderDownloadAllLink;

		// Token: 0x04001C4D RID: 7245
		private int downloadAllCount;

		// Token: 0x04001C4E RID: 7246
		private int? embeddedItemNestingLevel;

		// Token: 0x04001C4F RID: 7247
		private ClientSMimeControlStatus clientSMimeControlStatus;

		// Token: 0x04001C50 RID: 7248
		private IList<AttachmentLink> attachmentLinks;

		// Token: 0x04001C51 RID: 7249
		private TextWriter responseWriter;

		// Token: 0x04001C52 RID: 7250
		private TextWriter sanitizingResponseWriter;
	}
}
