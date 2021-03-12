using System;
using System.Collections;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000375 RID: 885
	public abstract class EditMessageOrPostBase : EditItemForm, IRegistryOnlyForm
	{
		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x000BE481 File Offset: 0x000BC681
		protected static int ImportanceLow
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x000BE484 File Offset: 0x000BC684
		protected static int ImportanceNormal
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002116 RID: 8470 RVA: 0x000BE487 File Offset: 0x000BC687
		protected static int ImportanceHigh
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002117 RID: 8471 RVA: 0x000BE48A File Offset: 0x000BC68A
		protected static int StoreObjectTypeMessage
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002118 RID: 8472 RVA: 0x000BE48E File Offset: 0x000BC68E
		protected static int StoreObjectTypeMeetingResponse
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002119 RID: 8473 RVA: 0x000BE492 File Offset: 0x000BC692
		protected static int StoreObjectTypeMeetingRequest
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x0600211A RID: 8474 RVA: 0x000BE496 File Offset: 0x000BC696
		protected static int StoreObjectTypeMeetingCancellation
		{
			get
			{
				return 13;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x000BE49A File Offset: 0x000BC69A
		protected static int StoreObjectTypeApprovalReply
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x0600211C RID: 8476 RVA: 0x000BE49E File Offset: 0x000BC69E
		protected bool AddSignatureToBody
		{
			get
			{
				return this.addSignatureToBody;
			}
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x000BE4A6 File Offset: 0x000BC6A6
		protected bool ShouldAddSignatureToBody(Markup bodymarkup, NewItemType newitemType)
		{
			return base.UserContext.IsFeatureEnabled(Feature.Signature) && base.UserContext.UserOptions.AutoAddSignature;
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x000BE4CD File Offset: 0x000BC6CD
		internal EditMessageOrPostBase()
		{
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x000BE4E0 File Offset: 0x000BC6E0
		internal EditMessageOrPostBase(bool setNoCacheNoStore) : base(setNoCacheNoStore)
		{
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x000BE4F4 File Offset: 0x000BC6F4
		protected void LoadMessageBodyIntoStream(TextWriter writer)
		{
			bool flag = BodyConversionUtilities.GenerateEditableMessageBodyAndRenderInfobarMessages(base.Item, writer, this.newItemType, base.OwaContext, ref this.shouldPromptUserForUnblockingOnFormLoad, ref this.hasInlineImages, this.infobar, base.ForceAllowWebBeacon, this.bodyMarkup);
			if (flag)
			{
				base.Item.Load();
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06002121 RID: 8481 RVA: 0x000BE546 File Offset: 0x000BC746
		protected NewItemType NewItemType
		{
			get
			{
				return this.newItemType;
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06002122 RID: 8482 RVA: 0x000BE54E File Offset: 0x000BC74E
		protected ArrayList AttachmentWellRenderObjects
		{
			get
			{
				return this.attachmentWellRenderObjects;
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06002123 RID: 8483 RVA: 0x000BE556 File Offset: 0x000BC756
		protected Infobar Infobar
		{
			get
			{
				return this.infobar;
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06002124 RID: 8484 RVA: 0x000BE55E File Offset: 0x000BC75E
		protected bool ShouldPromptUser
		{
			get
			{
				return this.shouldPromptUserForUnblockingOnFormLoad;
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06002125 RID: 8485 RVA: 0x000BE566 File Offset: 0x000BC766
		protected bool HasInlineImages
		{
			get
			{
				return this.hasInlineImages;
			}
		}

		// Token: 0x0400179F RID: 6047
		protected const string ReplyAction = "Reply";

		// Token: 0x040017A0 RID: 6048
		protected const string ReplyAllAction = "ReplyAll";

		// Token: 0x040017A1 RID: 6049
		protected const string ForwardAction = "Forward";

		// Token: 0x040017A2 RID: 6050
		protected const string DraftState = "Draft";

		// Token: 0x040017A3 RID: 6051
		protected bool addSignatureToBody;

		// Token: 0x040017A4 RID: 6052
		protected ArrayList attachmentWellRenderObjects;

		// Token: 0x040017A5 RID: 6053
		protected Markup bodyMarkup;

		// Token: 0x040017A6 RID: 6054
		protected Infobar infobar = new Infobar();

		// Token: 0x040017A7 RID: 6055
		protected bool shouldPromptUserForUnblockingOnFormLoad;

		// Token: 0x040017A8 RID: 6056
		protected bool hasInlineImages;

		// Token: 0x040017A9 RID: 6057
		protected NewItemType newItemType;
	}
}
