using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000463 RID: 1123
	public class PrintMessage : OwaForm, IRegistryOnlyForm
	{
		// Token: 0x06002A13 RID: 10771 RVA: 0x000EBF14 File Offset: 0x000EA114
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			string type = base.OwaContext.FormsRegistryContext.Type;
			StorePropertyDefinition[] array = new StorePropertyDefinition[]
			{
				ItemSchema.BlockStatus,
				BodySchema.Codepage,
				BodySchema.InternetCpid,
				MessageItemSchema.SenderTelephoneNumber,
				ItemSchema.FlagStatus,
				ItemSchema.FlagCompleteTime
			};
			if (ObjectClass.IsMessage(type, false))
			{
				this.message = base.Initialize<MessageItem>(array);
			}
			else
			{
				this.message = base.InitializeAsMessageItem(array);
			}
			this.IrmDecryptIfRestricted();
			this.recipientWell = new PrintRecipientWell(new MessageRecipientWell(this.message));
			this.attachmentWellRenderObjects = AttachmentWell.GetAttachmentInformation(this.message, base.AttachmentLinks, base.UserContext.IsPublicLogon, base.IsEmbeddedItem);
			this.shouldRenderAttachmentWell = (!this.IsRestrictedButIrmFeatureDisabledOrDecryptionFailed && PrintAttachmentWell.ShouldRenderAttachments(this.attachmentWellRenderObjects));
			if (this.message.Importance == Importance.High)
			{
				this.importanceString = LocalizedStrings.GetHtmlEncoded(-77932258);
			}
			else if (this.message.Importance == Importance.Low)
			{
				this.importanceString = LocalizedStrings.GetHtmlEncoded(1502599728);
			}
			switch (this.message.Sensitivity)
			{
			case Sensitivity.Personal:
				this.sensitivityString = LocalizedStrings.GetHtmlEncoded(567923294);
				break;
			case Sensitivity.Private:
				this.sensitivityString = LocalizedStrings.GetHtmlEncoded(-1268489823);
				break;
			case Sensitivity.CompanyConfidential:
				this.sensitivityString = LocalizedStrings.GetHtmlEncoded(-819101664);
				break;
			}
			this.categoriesString = ItemUtility.GetCategoriesAsString(this.message);
			if (this.message.Id != null && !this.message.IsRead && !base.IsPreviewForm)
			{
				this.message.MarkAsRead(Utilities.ShouldSuppressReadReceipt(base.UserContext, this.message), false);
			}
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x000EC0DC File Offset: 0x000EA2DC
		protected void LoadMessageBodyIntoStream(TextWriter writer)
		{
			if (this.IsRestrictedButIrmFeatureDisabledOrDecryptionFailed)
			{
				this.RenderAlternateBodyForIrm(writer);
				return;
			}
			BodyConversionUtilities.GeneratePrintMessageBody(this.message, writer, base.OwaContext, base.IsEmbeddedItem, base.IsEmbeddedItem ? base.RenderEmbeddedUrl() : null, base.ForceAllowWebBeacon, base.ForceEnableItemLink);
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x000EC130 File Offset: 0x000EA330
		protected void RenderSender(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (Utilities.IsOnBehalfOf(this.message.Sender, this.message.From))
			{
				writer.Write(LocalizedStrings.GetHtmlEncoded(-165544498), RenderingUtilities.GetDisplaySenderName(this.message.Sender), RenderingUtilities.GetDisplaySenderName(this.message.From));
				return;
			}
			writer.Write(RenderingUtilities.GetDisplaySenderName(this.message.Sender));
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x000EC1AF File Offset: 0x000EA3AF
		protected void RenderSubject(bool isTitle)
		{
			if (isTitle)
			{
				RenderingUtilities.RenderSubject(base.Response.Output, this.message, LocalizedStrings.GetNonEncoded(730745110));
				return;
			}
			RenderingUtilities.RenderSubject(base.Response.Output, this.message);
		}

		// Token: 0x06002A17 RID: 10775 RVA: 0x000EC1EB File Offset: 0x000EA3EB
		protected void RenderOwaPlainTextStyle()
		{
			OwaPlainTextStyle.WriteLocalizedStyleIntoHeadForPlainTextBody(this.message, base.Response.Output, "DIV#divBdy");
		}

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06002A18 RID: 10776 RVA: 0x000EC208 File Offset: 0x000EA408
		protected RecipientWell RecipientWell
		{
			get
			{
				return this.recipientWell;
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06002A19 RID: 10777 RVA: 0x000EC210 File Offset: 0x000EA410
		protected bool ShouldRenderAttachmentWell
		{
			get
			{
				return this.shouldRenderAttachmentWell;
			}
		}

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06002A1A RID: 10778 RVA: 0x000EC218 File Offset: 0x000EA418
		protected ArrayList AttachmentWellRenderObjects
		{
			get
			{
				return this.attachmentWellRenderObjects;
			}
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06002A1B RID: 10779 RVA: 0x000EC220 File Offset: 0x000EA420
		protected string ImportanceString
		{
			get
			{
				return this.importanceString;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06002A1C RID: 10780 RVA: 0x000EC228 File Offset: 0x000EA428
		protected string SensitivityString
		{
			get
			{
				return this.sensitivityString;
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06002A1D RID: 10781 RVA: 0x000EC230 File Offset: 0x000EA430
		protected string CategoriesString
		{
			get
			{
				return this.categoriesString;
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06002A1E RID: 10782 RVA: 0x000EC238 File Offset: 0x000EA438
		protected ExDateTime MessageSentTime
		{
			get
			{
				return this.message.SentTime;
			}
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x000EC245 File Offset: 0x000EA445
		private void RenderAlternateBodyForIrm(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.Write(Utilities.GetAlternateBodyForIrm(base.UserContext, Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml, this.irmDecryptionStatus, ObjectClass.IsVoiceMessage(this.message.ClassName)));
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06002A20 RID: 10784 RVA: 0x000EC27D File Offset: 0x000EA47D
		private bool IsRestrictedButIrmFeatureDisabledOrDecryptionFailed
		{
			get
			{
				return this.message.IsRestricted && (!base.UserContext.IsIrmEnabled || this.irmDecryptionStatus.Failed);
			}
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x000EC2A8 File Offset: 0x000EA4A8
		private void IrmDecryptIfRestricted()
		{
			if (!Utilities.IsIrmRestricted(this.message))
			{
				return;
			}
			RightsManagedMessageItem rightsManagedMessageItem = (RightsManagedMessageItem)this.message;
			if (!base.UserContext.IsIrmEnabled)
			{
				this.irmDecryptionStatus = RightsManagedMessageDecryptionStatus.FeatureDisabled;
				return;
			}
			if (!rightsManagedMessageItem.CanDecode)
			{
				this.irmDecryptionStatus = RightsManagedMessageDecryptionStatus.NotSupported;
				return;
			}
			try
			{
				if (Utilities.IrmDecryptIfRestricted(this.message, base.UserContext, true))
				{
					RightsManagedMessageItem rightsManagedMessageItem2 = (RightsManagedMessageItem)this.message;
					if (!rightsManagedMessageItem2.UsageRights.IsUsageRightGranted(ContentRight.Print))
					{
						this.irmDecryptionStatus = new RightsManagedMessageDecryptionStatus(RightsManagementFailureCode.UserRightNotGranted, null);
					}
				}
			}
			catch (RightsManagementPermanentException exception)
			{
				this.irmDecryptionStatus = RightsManagedMessageDecryptionStatus.CreateFromException(exception);
			}
			catch (RightsManagementTransientException exception2)
			{
				this.irmDecryptionStatus = RightsManagedMessageDecryptionStatus.CreateFromException(exception2);
			}
		}

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06002A22 RID: 10786 RVA: 0x000EC37C File Offset: 0x000EA57C
		protected bool IsCopyRestricted
		{
			get
			{
				return this.IsUsageRightRestricted(ContentRight.Extract);
			}
		}

		// Token: 0x06002A23 RID: 10787 RVA: 0x000EC388 File Offset: 0x000EA588
		private bool IsUsageRightRestricted(ContentRight right)
		{
			RightsManagedMessageItem rightsManagedMessageItem = this.message as RightsManagedMessageItem;
			if (rightsManagedMessageItem == null || !rightsManagedMessageItem.IsRestricted)
			{
				return false;
			}
			if (!base.OwaContext.UserContext.IsIrmEnabled)
			{
				return false;
			}
			if (this.irmDecryptionStatus.Failed)
			{
				return !right.IsUsageRightGranted(ContentRight.Extract) && !right.IsUsageRightGranted(ContentRight.Print);
			}
			return !rightsManagedMessageItem.UsageRights.IsUsageRightGranted(right);
		}

		// Token: 0x04001C7B RID: 7291
		private static readonly PropertyDefinition[] prefetchProperties = new PropertyDefinition[]
		{
			ItemSchema.BlockStatus,
			BodySchema.Codepage,
			BodySchema.InternetCpid,
			MessageItemSchema.IsDraft,
			MessageItemSchema.IsRead
		};

		// Token: 0x04001C7C RID: 7292
		private MessageItem message;

		// Token: 0x04001C7D RID: 7293
		private RecipientWell recipientWell;

		// Token: 0x04001C7E RID: 7294
		private bool shouldRenderAttachmentWell;

		// Token: 0x04001C7F RID: 7295
		private ArrayList attachmentWellRenderObjects;

		// Token: 0x04001C80 RID: 7296
		private string sensitivityString;

		// Token: 0x04001C81 RID: 7297
		private string importanceString;

		// Token: 0x04001C82 RID: 7298
		private string categoriesString;

		// Token: 0x04001C83 RID: 7299
		private RightsManagedMessageDecryptionStatus irmDecryptionStatus;
	}
}
