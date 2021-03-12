using System;
using System.Collections.Generic;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000472 RID: 1138
	public class ReadSharingMessage : ReadMessage
	{
		// Token: 0x06002B34 RID: 11060 RVA: 0x000F2D7C File Offset: 0x000F0F7C
		protected override void OnLoad(EventArgs e)
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "id", true);
			if (OwaStoreObjectId.CreateFromString(queryStringParameter).IsPublic)
			{
				throw new OwaInvalidRequestException("Cannot open item in public folder with this form");
			}
			base.Message = (this.sharingMessageItem = base.Initialize<SharingMessageItem>(ReadSharingMessage.PrefetchProperties));
			base.InitializeReadMessageFormElements();
			this.sharingMessageWriter = new SharingMessageWriter(this.sharingMessageItem, base.UserContext);
			if (!this.sharingMessageItem.IsDraft)
			{
				this.AddMessagesToInfobar();
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06002B35 RID: 11061 RVA: 0x000F2DFD File Offset: 0x000F0FFD
		protected SharingMessageWriter SharingMessageWriter
		{
			get
			{
				return this.sharingMessageWriter;
			}
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x000F2E05 File Offset: 0x000F1005
		protected override void AddMessagesToInfobar()
		{
			base.AddMessagesToInfobar();
			this.sharingMessageWriter.AddSharingInfoToInfobar(base.Infobar);
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06002B37 RID: 11063 RVA: 0x000F2E1E File Offset: 0x000F101E
		protected bool ShouldRenderToolbar
		{
			get
			{
				return !base.IsPreviewForm;
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06002B38 RID: 11064 RVA: 0x000F2E29 File Offset: 0x000F1029
		protected bool IsDraft
		{
			get
			{
				return this.sharingMessageItem.IsDraft;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06002B39 RID: 11065 RVA: 0x000F2E36 File Offset: 0x000F1036
		protected string BrowseUrl
		{
			get
			{
				return this.sharingMessageItem.BrowseUrl;
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x000F2E43 File Offset: 0x000F1043
		protected string RedirectBrowseUrl
		{
			get
			{
				return Redir.BuildRedirUrl(base.UserContext, this.sharingMessageItem.BrowseUrl);
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x000F2E5B File Offset: 0x000F105B
		protected bool IsPublishing
		{
			get
			{
				return this.sharingMessageItem.IsPublishing;
			}
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x000F2E68 File Offset: 0x000F1068
		protected void RenderSharingToolbar()
		{
			this.sharingMessageWriter.SharingToolbar.Render(base.Response.Output);
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x000F2E85 File Offset: 0x000F1085
		protected override void RenderSubject()
		{
			RenderingUtilities.RenderSubject(base.Response.Output, this.sharingMessageItem);
		}

		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06002B3E RID: 11070 RVA: 0x000F2E9D File Offset: 0x000F109D
		protected bool IsInvitationOrAcceptOfRequest
		{
			get
			{
				return this.sharingMessageItem.SharingMessageType.IsInvitationOrAcceptOfRequest;
			}
		}

		// Token: 0x17000CBB RID: 3259
		// (get) Token: 0x06002B3F RID: 11071 RVA: 0x000F2EAF File Offset: 0x000F10AF
		public override IEnumerable<string> ExternalScriptFiles
		{
			get
			{
				return this.externalScriptFiles;
			}
		}

		// Token: 0x17000CBC RID: 3260
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x000F2EB8 File Offset: 0x000F10B8
		public override SanitizedHtmlString Title
		{
			get
			{
				string subject = this.sharingMessageItem.Subject;
				if (string.IsNullOrEmpty(subject))
				{
					return SanitizedHtmlString.FromStringId(730745110);
				}
				return new SanitizedHtmlString(subject);
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (get) Token: 0x06002B41 RID: 11073 RVA: 0x000F2EEA File Offset: 0x000F10EA
		public override string PageType
		{
			get
			{
				return "ReadSharingMessagePage";
			}
		}

		// Token: 0x04001CDD RID: 7389
		private const string IdQueryParameter = "id";

		// Token: 0x04001CDE RID: 7390
		private SharingMessageItem sharingMessageItem;

		// Token: 0x04001CDF RID: 7391
		private SharingMessageWriter sharingMessageWriter;

		// Token: 0x04001CE0 RID: 7392
		internal static StorePropertyDefinition[] PrefetchProperties = new StorePropertyDefinition[]
		{
			ItemSchema.IsClassified,
			ItemSchema.Classification,
			ItemSchema.ClassificationDescription,
			ItemSchema.ClassificationGuid,
			StoreObjectSchema.EffectiveRights,
			ItemSchema.FlagStatus,
			ItemSchema.FlagCompleteTime,
			ItemSchema.Categories,
			MessageItemSchema.IsReadReceiptPending,
			StoreObjectSchema.PolicyTag,
			ItemSchema.RetentionDate
		};

		// Token: 0x04001CE1 RID: 7393
		private string[] externalScriptFiles = new string[]
		{
			"freadsharingmsg.js"
		};
	}
}
