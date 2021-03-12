using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x0200045E RID: 1118
	public abstract class OwaFormSubPage : OwaSubPage
	{
		// Token: 0x06002984 RID: 10628 RVA: 0x000EA275 File Offset: 0x000E8475
		internal OwaFormSubPage()
		{
			this.owaFormInternal = new OwaFormInternal(base.OwaContext);
		}

		// Token: 0x06002985 RID: 10629 RVA: 0x000EA28E File Offset: 0x000E848E
		internal T Initialize<T>(params PropertyDefinition[] prefetchProperties) where T : Item
		{
			return this.Initialize<T>(true, prefetchProperties);
		}

		// Token: 0x06002986 RID: 10630 RVA: 0x000EA298 File Offset: 0x000E8498
		internal T Initialize<T>(bool itemRequired, params PropertyDefinition[] prefetchProperties) where T : Item
		{
			return this.owaFormInternal.Initialize<T>(itemRequired, false, base.GetParameter("id", false), this.GetAction(), prefetchProperties);
		}

		// Token: 0x06002987 RID: 10631 RVA: 0x000EA2BA File Offset: 0x000E84BA
		internal MessageItem InitializeAsMessageItem(params PropertyDefinition[] prefetchProperties)
		{
			return this.owaFormInternal.Initialize<MessageItem>(true, true, base.GetParameter("id", false), this.GetAction(), prefetchProperties);
		}

		// Token: 0x06002988 RID: 10632 RVA: 0x000EA2DC File Offset: 0x000E84DC
		private string GetAction()
		{
			string text = base.GetParameter("a", false);
			if (string.IsNullOrEmpty(text))
			{
				text = base.OwaContext.FormsRegistryContext.Action;
			}
			return text;
		}

		// Token: 0x06002989 RID: 10633 RVA: 0x000EA310 File Offset: 0x000E8510
		protected string GetItemType()
		{
			string text = base.GetParameter("t", false);
			if (string.IsNullOrEmpty(text))
			{
				text = base.OwaContext.FormsRegistryContext.Type;
			}
			return text;
		}

		// Token: 0x0600298A RID: 10634 RVA: 0x000EA344 File Offset: 0x000E8544
		protected void MarkPayloadAsRead()
		{
			this.owaFormInternal.MarkPayloadAsRead();
		}

		// Token: 0x0600298B RID: 10635 RVA: 0x000EA351 File Offset: 0x000E8551
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.owaFormInternal.MarkPayloadAsRead();
		}

		// Token: 0x0600298C RID: 10636 RVA: 0x000EA365 File Offset: 0x000E8565
		protected override void OnUnload(EventArgs e)
		{
			if (this.owaFormInternal != null)
			{
				this.owaFormInternal.Dispose();
				this.owaFormInternal = null;
			}
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x000EA381 File Offset: 0x000E8581
		protected string RenderEmbeddedUrl()
		{
			return this.owaFormInternal.RenderEmbeddedUrl();
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x000EA38E File Offset: 0x000E858E
		protected void RenderEmbeddedItemIds()
		{
			this.owaFormInternal.RenderEmbeddedItemIds();
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x000EA39B File Offset: 0x000E859B
		protected void RenderJavascriptEncodedItemChangeKey()
		{
			this.owaFormInternal.RenderJavascriptEncodedItemChangeKey();
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x000EA3A8 File Offset: 0x000E85A8
		protected void RenderJavascriptEncodedItemId()
		{
			this.owaFormInternal.RenderJavascriptEncodedItemId();
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x000EA3B5 File Offset: 0x000E85B5
		protected void RenderMessageInformation(TextWriter writer)
		{
			this.owaFormInternal.RenderMessageInformation(writer);
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x000EA3C3 File Offset: 0x000E85C3
		protected void RenderSubjectAttributes()
		{
			this.owaFormInternal.RenderSubjectAttributes();
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000EA3D0 File Offset: 0x000E85D0
		protected void RenderDownloadAllAttachmentsLink()
		{
			this.owaFormInternal.RenderDownloadAllAttachmentsLink();
		}

		// Token: 0x06002994 RID: 10644 RVA: 0x000EA3DD File Offset: 0x000E85DD
		protected void RenderAttachmentWellForReadItem(ArrayList attachmentWellRenderObjects)
		{
			this.owaFormInternal.RenderAttachmentWellForReadItem(attachmentWellRenderObjects);
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x06002995 RID: 10645 RVA: 0x000EA3EB File Offset: 0x000E85EB
		protected bool ShowAttachmentWell
		{
			get
			{
				return this.owaFormInternal.ShowAttachmentWell;
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06002996 RID: 10646 RVA: 0x000EA3F8 File Offset: 0x000E85F8
		// (set) Token: 0x06002997 RID: 10647 RVA: 0x000EA405 File Offset: 0x000E8605
		internal Item Item
		{
			get
			{
				return this.owaFormInternal.Item;
			}
			set
			{
				this.owaFormInternal.Item = value;
			}
		}

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06002998 RID: 10648 RVA: 0x000EA413 File Offset: 0x000E8613
		// (set) Token: 0x06002999 RID: 10649 RVA: 0x000EA420 File Offset: 0x000E8620
		internal IList<AttachmentLink> AttachmentLinks
		{
			get
			{
				return this.owaFormInternal.AttachmentLinks;
			}
			set
			{
				this.owaFormInternal.AttachmentLinks = value;
			}
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x0600299A RID: 10650 RVA: 0x000EA42E File Offset: 0x000E862E
		// (set) Token: 0x0600299B RID: 10651 RVA: 0x000EA43B File Offset: 0x000E863B
		protected bool IsPreviewForm
		{
			get
			{
				return this.owaFormInternal.IsPreviewForm;
			}
			set
			{
				this.owaFormInternal.IsPreviewForm = value;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x000EA449 File Offset: 0x000E8649
		protected bool HasCategories
		{
			get
			{
				return this.owaFormInternal.HasCategories;
			}
		}

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x0600299D RID: 10653 RVA: 0x000EA456 File Offset: 0x000E8656
		protected bool IsEmbeddedItem
		{
			get
			{
				return this.owaFormInternal.IsEmbeddedItem;
			}
		}

		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x0600299E RID: 10654 RVA: 0x000EA463 File Offset: 0x000E8663
		protected virtual bool IsPublicItem
		{
			get
			{
				return this.owaFormInternal.IsPublicItem;
			}
		}

		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x0600299F RID: 10655 RVA: 0x000EA470 File Offset: 0x000E8670
		protected virtual bool IsOtherMailboxItem
		{
			get
			{
				return this.owaFormInternal.IsOtherMailboxItem;
			}
		}

		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x060029A0 RID: 10656 RVA: 0x000EA47D File Offset: 0x000E867D
		protected bool IsItemNull
		{
			get
			{
				return this.owaFormInternal.IsItemNull;
			}
		}

		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x060029A1 RID: 10657 RVA: 0x000EA48A File Offset: 0x000E868A
		protected virtual bool IsSubjectEditable
		{
			get
			{
				return this.owaFormInternal.IsSubjectEditable;
			}
		}

		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x060029A2 RID: 10658 RVA: 0x000EA497 File Offset: 0x000E8697
		protected bool IsInDeleteItems
		{
			get
			{
				return this.owaFormInternal.IsInDeleteItems;
			}
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x060029A3 RID: 10659 RVA: 0x000EA4A4 File Offset: 0x000E86A4
		protected bool UserCanDeleteItem
		{
			get
			{
				return this.owaFormInternal.UserCanDeleteItem;
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x060029A4 RID: 10660 RVA: 0x000EA4B1 File Offset: 0x000E86B1
		protected bool UserCanEditItem
		{
			get
			{
				return this.owaFormInternal.UserCanEditItem;
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x000EA4BE File Offset: 0x000E86BE
		protected string ParentItemIdBase64String
		{
			get
			{
				return this.owaFormInternal.ParentFolderIdBase64String;
			}
		}

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x060029A6 RID: 10662 RVA: 0x000EA4CB File Offset: 0x000E86CB
		protected string ParentFolderIdBase64String
		{
			get
			{
				return this.owaFormInternal.ParentFolderIdBase64String;
			}
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x060029A7 RID: 10663 RVA: 0x000EA4D8 File Offset: 0x000E86D8
		internal OwaStoreObjectId ParentFolderId
		{
			get
			{
				return this.owaFormInternal.ParentFolderId;
			}
		}

		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x060029A8 RID: 10664 RVA: 0x000EA4E5 File Offset: 0x000E86E5
		protected string ItemClassName
		{
			get
			{
				return this.owaFormInternal.ItemClassName;
			}
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x060029A9 RID: 10665 RVA: 0x000EA4F2 File Offset: 0x000E86F2
		protected ClientSMimeControlStatus ClientSMimeControlStatus
		{
			get
			{
				return this.owaFormInternal.ClientSMimeControlStatus;
			}
		}

		// Token: 0x17000C22 RID: 3106
		// (get) Token: 0x060029AA RID: 10666 RVA: 0x000EA4FF File Offset: 0x000E86FF
		protected bool ForceAllowWebBeacon
		{
			get
			{
				return this.IsRequestCallbackForWebBeacons;
			}
		}

		// Token: 0x17000C23 RID: 3107
		// (get) Token: 0x060029AB RID: 10667 RVA: 0x000EA507 File Offset: 0x000E8707
		protected bool IsRequestCallbackForWebBeacons
		{
			get
			{
				return Utilities.IsRequestCallbackForWebBeacons(base.Request);
			}
		}

		// Token: 0x17000C24 RID: 3108
		// (get) Token: 0x060029AC RID: 10668 RVA: 0x000EA514 File Offset: 0x000E8714
		protected bool ForceEnableItemLink
		{
			get
			{
				return this.IsRequestCallbackForPhishing;
			}
		}

		// Token: 0x17000C25 RID: 3109
		// (get) Token: 0x060029AD RID: 10669 RVA: 0x000EA51C File Offset: 0x000E871C
		protected bool IsRequestCallbackForPhishing
		{
			get
			{
				return Utilities.IsRequestCallbackForPhishing(base.Request);
			}
		}

		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x060029AE RID: 10670 RVA: 0x000EA529 File Offset: 0x000E8729
		protected bool IsJunkOrPhishing
		{
			get
			{
				return this.owaFormInternal.IsJunkOrPhishing;
			}
		}

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x060029AF RID: 10671 RVA: 0x000EA536 File Offset: 0x000E8736
		protected bool IsReportItem
		{
			get
			{
				return this.owaFormInternal.IsReportItem;
			}
		}

		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x060029B0 RID: 10672 RVA: 0x000EA543 File Offset: 0x000E8743
		protected bool IsEmbeddedItemInSMimeMessage
		{
			get
			{
				return this.owaFormInternal.IsEmbeddedItemInSMimeMessage;
			}
		}

		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x060029B1 RID: 10673 RVA: 0x000EA550 File Offset: 0x000E8750
		protected bool IsEmbeddedItemInNonSMimeItem
		{
			get
			{
				return this.owaFormInternal.IsEmbeddedItemInNonSMimeItem;
			}
		}

		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x060029B2 RID: 10674 RVA: 0x000EA55D File Offset: 0x000E875D
		protected virtual bool IsIgnoredConversation
		{
			get
			{
				return this.owaFormInternal.IsIgnoredConversation;
			}
		}

		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x060029B3 RID: 10675 RVA: 0x000EA56A File Offset: 0x000E876A
		protected bool ShouldRenderDownloadAllLink
		{
			get
			{
				return this.owaFormInternal.ShouldRenderDownloadAllLink;
			}
		}

		// Token: 0x060029B4 RID: 10676 RVA: 0x000EA577 File Offset: 0x000E8777
		protected void SetShouldRenderDownloadAllLink(ArrayList attachmentWellInfos)
		{
			this.owaFormInternal.SetShouldRenderDownloadAllLink(attachmentWellInfos);
		}

		// Token: 0x060029B5 RID: 10677 RVA: 0x000EA585 File Offset: 0x000E8785
		protected void RenderActionButtons(bool isInJunkMailFolder, bool isPost)
		{
			this.owaFormInternal.RenderActionButtons(isInJunkMailFolder, isPost);
		}

		// Token: 0x04001C44 RID: 7236
		private OwaFormInternal owaFormInternal;
	}
}
