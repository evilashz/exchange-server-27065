using System;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Clients.Owa.Premium.Controls;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Clients;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x02000455 RID: 1109
	public class EditSharingMessage : EditMessage
	{
		// Token: 0x060028B9 RID: 10425 RVA: 0x000E74C3 File Offset: 0x000E56C3
		public EditSharingMessage() : base(true)
		{
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x000E74CC File Offset: 0x000E56CC
		protected override void OnLoad(EventArgs e)
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "publish", false);
			if (!string.IsNullOrEmpty(queryStringParameter))
			{
				this.isPublishing = (queryStringParameter == "1");
			}
			base.OnLoad(e);
			this.sharingMessageWriter = new SharingMessageWriter(this.sharingMessage, base.UserContext);
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x060028BB RID: 10427 RVA: 0x000E7522 File Offset: 0x000E5722
		protected SharingMessageWriter SharingMessageWriter
		{
			get
			{
				return this.sharingMessageWriter;
			}
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x000E752A File Offset: 0x000E572A
		protected void RenderSubject()
		{
			base.RenderSubject(false);
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x000E7534 File Offset: 0x000E5734
		protected void RenderTitle()
		{
			if (this.IsSharingInvitation)
			{
				string subject;
				if (this.IsDefaultFolderToBeShared)
				{
					subject = string.Format(base.UserContext.UserCulture, LocalizedStrings.GetNonEncoded(this.isPublishing ? 1155246534 : 1285974930), new object[]
					{
						base.UserContext.MailboxSession.DisplayName
					});
				}
				else
				{
					subject = string.Format(base.UserContext.UserCulture, LocalizedStrings.GetNonEncoded(this.isPublishing ? -198640513 : -2052624973), new object[]
					{
						base.UserContext.MailboxSession.DisplayName,
						this.sharingMessage.SharedFolderName
					});
				}
				RenderingUtilities.RenderSubject(base.Response.Output, subject, string.Empty);
				return;
			}
			base.RenderSubject(true);
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x060028BE RID: 10430 RVA: 0x000E760C File Offset: 0x000E580C
		protected bool IsDefaultFolderToBeShared
		{
			get
			{
				return this.sharingMessage.IsSharedFolderPrimary;
			}
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x000E761C File Offset: 0x000E581C
		protected void RenderSharingErrorEnum()
		{
			RenderingUtilities.RenderInteger(base.SanitizingResponse, "a_SHARE_ERROR_FOLDER_NOT_EXIST", 1);
			RenderingUtilities.RenderInteger(base.SanitizingResponse, "a_SHARE_ERROR_INVALID_RECIPIENTS", 2);
			RenderingUtilities.RenderInteger(base.SanitizingResponse, "a_SHARE_ERROR_PUBLISH_AND_TRY_AGAIN", 3);
			RenderingUtilities.RenderInteger(base.SanitizingResponse, "a_SHARE_ERROR_SEND_PUBLISH_LINKS", 4);
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x060028C0 RID: 10432 RVA: 0x000E766D File Offset: 0x000E586D
		protected bool IsInvitationOrRequest
		{
			get
			{
				return this.sharingMessage.SharingMessageType.IsInvitationOrRequest;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x060028C1 RID: 10433 RVA: 0x000E767F File Offset: 0x000E587F
		protected bool IsSharingInvitation
		{
			get
			{
				return this.sharingMessage.SharingMessageType == SharingMessageType.Invitation || this.sharingMessage.SharingMessageType == SharingMessageType.InvitationAndRequest;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x060028C2 RID: 10434 RVA: 0x000E76A7 File Offset: 0x000E58A7
		protected bool IsAllowOfRequest
		{
			get
			{
				return this.sharingMessage.SharingMessageType == SharingMessageType.AcceptOfRequest;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x060028C3 RID: 10435 RVA: 0x000E76BB File Offset: 0x000E58BB
		protected bool IsResponseToRequest
		{
			get
			{
				return this.sharingMessage.SharingMessageType.IsResponseToRequest;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x060028C4 RID: 10436 RVA: 0x000E76CD File Offset: 0x000E58CD
		protected override bool ShowBcc
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x060028C5 RID: 10437 RVA: 0x000E76D0 File Offset: 0x000E58D0
		protected override bool ShowFrom
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x060028C6 RID: 10438 RVA: 0x000E76D3 File Offset: 0x000E58D3
		protected override bool IsPageCacheable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x060028C7 RID: 10439 RVA: 0x000E76D6 File Offset: 0x000E58D6
		protected bool IsPublishing
		{
			get
			{
				return this.isPublishing;
			}
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000E76E0 File Offset: 0x000E58E0
		protected override void InitializeMessage()
		{
			base.Item = (base.Message = (this.sharingMessage = base.Initialize<SharingMessageItem>(false, EditMessage.PrefetchProperties)));
			if (this.sharingMessage != null)
			{
				this.isPublishing = this.sharingMessage.IsPublishing;
			}
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x000E772C File Offset: 0x000E592C
		protected override void CreateDraftMessage()
		{
			string queryStringParameter = Utilities.GetQueryStringParameter(base.Request, "fldId");
			OwaStoreObjectId owaStoreObjectId = OwaStoreObjectId.CreateFromString(queryStringParameter);
			if (owaStoreObjectId.OwaStoreObjectIdType != OwaStoreObjectIdType.MailBoxObject)
			{
				throw new OwaInvalidRequestException("Cannot share this calendar");
			}
			try
			{
				if (this.isPublishing)
				{
					try
					{
						this.sharingMessage = SharingMessageItem.CreateForPublishing(base.UserContext.MailboxSession, base.UserContext.DraftsFolderId, owaStoreObjectId.StoreId);
						goto IL_90;
					}
					catch (FolderNotPublishedException innerException)
					{
						throw new OwaInvalidRequestException("Folder is not published", innerException);
					}
				}
				this.sharingMessage = SharingMessageItem.Create(base.UserContext.MailboxSession, base.UserContext.DraftsFolderId, owaStoreObjectId.StoreId);
				IL_90:
				base.Item = (base.Message = this.sharingMessage);
			}
			catch (InvalidOperationException innerException2)
			{
				ExTraceGlobals.MailCallTracer.TraceDebug((long)this.GetHashCode(), "Failed to create sharing message: no compatible provider for the share folder was found.");
				throw new OwaInvalidRequestException("Failed to create sharing message: no compatible provider for the share folder was found.", innerException2);
			}
			catch (CannotShareFolderException innerException3)
			{
				throw new OwaInvalidRequestException("Failed to create sharing message:  This folder has been shared with you and can't be shared with any other recipients.", innerException3);
			}
			this.sharingMessage.Save(SaveMode.ResolveConflicts);
			this.sharingMessage.Load();
			this.newItemType = NewItemType.ImplicitDraft;
			base.DeleteExistingDraft = true;
			if (this.IsDefaultFolderToBeShared)
			{
				this.sharingMessage.Subject = string.Format(base.UserContext.UserCulture, LocalizedStrings.GetNonEncoded(this.isPublishing ? -1266131020 : -2015069592), new object[0]);
				return;
			}
			this.sharingMessage.Subject = string.Format(base.UserContext.UserCulture, LocalizedStrings.GetNonEncoded(this.isPublishing ? -1899057801 : 222110147), new object[]
			{
				this.sharingMessage.SharedFolderName
			});
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x060028CA RID: 10442 RVA: 0x000E78F4 File Offset: 0x000E5AF4
		protected override bool PageSupportSmime
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x060028CB RID: 10443 RVA: 0x000E78F7 File Offset: 0x000E5AF7
		protected override int CurrentStoreObjectType
		{
			get
			{
				return 28;
			}
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x000E78FB File Offset: 0x000E5AFB
		protected override EditMessageToolbar BuildToolbar()
		{
			return new EditSharingMessageToolbar(this.sharingMessage.Importance, this.bodyMarkup, this.isPublishing);
		}

		// Token: 0x04001C0C RID: 7180
		private const string SharedFolderIdParameter = "fldId";

		// Token: 0x04001C0D RID: 7181
		private const string PublishParameter = "publish";

		// Token: 0x04001C0E RID: 7182
		private SharingMessageItem sharingMessage;

		// Token: 0x04001C0F RID: 7183
		private SharingMessageWriter sharingMessageWriter;

		// Token: 0x04001C10 RID: 7184
		private bool isPublishing;
	}
}
