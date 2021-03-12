using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000454 RID: 1108
	public class EditPost : EditMessageOrPostBase
	{
		// Token: 0x060028AD RID: 10413 RVA: 0x000E724C File Offset: 0x000E544C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.post = base.Initialize<PostItem>(false, new PropertyDefinition[0]);
			Importance importance;
			if (this.post != null)
			{
				base.DeleteExistingDraft = true;
				this.newItemType = NewItemType.PostReply;
				importance = this.post.Importance;
			}
			else
			{
				base.DeleteExistingDraft = false;
				this.newItemType = NewItemType.New;
				importance = Importance.Normal;
			}
			this.bodyMarkup = BodyConversionUtilities.GetBodyFormatOfEditItem(base.Item, this.newItemType, base.UserContext.UserOptions);
			this.addSignatureToBody = base.ShouldAddSignatureToBody(this.bodyMarkup, this.newItemType);
			this.toolbar = new EditPostToolbar(importance, this.bodyMarkup);
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x060028AE RID: 10414 RVA: 0x000E72F5 File Offset: 0x000E54F5
		protected static int StoreObjectTypePost
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x000E72FC File Offset: 0x000E54FC
		protected void RenderFolderDisplayName()
		{
			string text = null;
			if (string.IsNullOrEmpty(text))
			{
				using (Folder folder = Utilities.GetFolder<Folder>(base.UserContext, base.TargetFolderId, new PropertyDefinition[0]))
				{
					text = folder.DisplayName;
				}
			}
			Utilities.HtmlEncode(text, base.Response.Output);
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x000E7360 File Offset: 0x000E5560
		protected void RenderTitle()
		{
			if (this.post == null)
			{
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(-439597685));
				return;
			}
			string subject = this.post.Subject;
			if (string.IsNullOrEmpty(subject))
			{
				base.Response.Write(LocalizedStrings.GetHtmlEncoded(-439597685));
				return;
			}
			Utilities.HtmlEncode(subject, base.Response.Output);
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x000E73C8 File Offset: 0x000E55C8
		protected void CreateAttachmentHelpers()
		{
			if (this.post == null)
			{
				return;
			}
			this.attachmentWellRenderObjects = AttachmentWell.GetAttachmentInformation(this.post, base.AttachmentLinks, base.UserContext.IsPublicLogon, base.IsEmbeddedItem);
			InfobarRenderingHelper infobarRenderingHelper = new InfobarRenderingHelper(this.attachmentWellRenderObjects);
			if (infobarRenderingHelper.HasLevelOne)
			{
				this.infobar.AddMessage(SanitizedHtmlString.FromStringId(-2118248931), InfobarMessageType.Informational, AttachmentWell.AttachmentInfobarHtmlTag);
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x000E7435 File Offset: 0x000E5635
		protected Toolbar Toolbar
		{
			get
			{
				return this.toolbar;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x060028B3 RID: 10419 RVA: 0x000E743D File Offset: 0x000E563D
		protected Markup BodyMarkup
		{
			get
			{
				return this.bodyMarkup;
			}
		}

		// Token: 0x060028B4 RID: 10420 RVA: 0x000E7445 File Offset: 0x000E5645
		protected void RenderCategories()
		{
			if (base.Item != null)
			{
				CategorySwatch.RenderCategories(base.OwaContext, base.SanitizingResponse, base.Item);
			}
		}

		// Token: 0x060028B5 RID: 10421 RVA: 0x000E7466 File Offset: 0x000E5666
		protected void RenderCategoriesJavascriptArray()
		{
			CategorySwatch.RenderCategoriesJavascriptArray(base.SanitizingResponse, base.Item);
		}

		// Token: 0x060028B6 RID: 10422 RVA: 0x000E7479 File Offset: 0x000E5679
		protected void RenderConversation()
		{
			if (this.post != null)
			{
				Utilities.CropAndRenderText(base.Response.Output, this.post.ConversationTopic, 255);
			}
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x000E74A3 File Offset: 0x000E56A3
		protected void RenderSubject()
		{
			RenderingUtilities.RenderSubject(base.Response.Output, this.post);
		}

		// Token: 0x04001C0A RID: 7178
		private PostItem post;

		// Token: 0x04001C0B RID: 7179
		private EditPostToolbar toolbar;
	}
}
