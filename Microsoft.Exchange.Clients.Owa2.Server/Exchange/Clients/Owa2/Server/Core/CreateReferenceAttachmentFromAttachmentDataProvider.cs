using System;
using System.IO;
using System.Threading;
using System.Web;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002FF RID: 767
	internal class CreateReferenceAttachmentFromAttachmentDataProvider : ServiceCommand<CreateAttachmentResponse>
	{
		// Token: 0x060019CB RID: 6603 RVA: 0x0005C190 File Offset: 0x0005A390
		public CreateReferenceAttachmentFromAttachmentDataProvider(CallContext callContext, ItemId itemId, string attachmentDataProviderId, string location, string dataProviderItemId, string dataProviderParentItemId = null, string providerEndpointUrl = null, string cancellationId = null) : base(callContext)
		{
			if (itemId == null)
			{
				throw new ArgumentNullException("itemId");
			}
			if (string.IsNullOrEmpty(attachmentDataProviderId))
			{
				throw new ArgumentException("The parameter cannot be null or empty.", "attachmentDataProviderId");
			}
			this.itemId = itemId;
			this.attachmentDataProviderId = attachmentDataProviderId;
			this.location = location;
			this.providerEndpointUrl = providerEndpointUrl;
			this.cancellationId = cancellationId;
			this.dataProviderItemId = dataProviderItemId;
			this.dataProviderParentItemId = dataProviderParentItemId;
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0005C200 File Offset: 0x0005A400
		protected override CreateAttachmentResponse InternalExecute()
		{
			UserContext userContext = UserContextManager.GetUserContext(base.CallContext.HttpContext, base.CallContext.EffectiveCaller, true);
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			CancellationToken token = cancellationTokenSource.Token;
			if (this.cancellationId != null && userContext.CancelAttachmentManager.OnCreateAttachment(this.cancellationId, cancellationTokenSource))
			{
				return null;
			}
			CreateAttachmentResponse createAttachmentResponse = null;
			try
			{
				AttachmentDataProvider provider = userContext.AttachmentDataProviderManager.GetProvider(base.CallContext, this.attachmentDataProviderId);
				createAttachmentResponse = CreateReferenceAttachmentFromAttachmentDataProvider.AttachReferenceAttachment(provider, userContext, this.location, this.dataProviderItemId, this.itemId.Id, base.IdConverter, this.dataProviderParentItemId, this.providerEndpointUrl);
			}
			finally
			{
				if (this.cancellationId != null)
				{
					AttachmentIdType attachmentIdFromCreateAttachmentResponse = CreateAttachmentHelper.GetAttachmentIdFromCreateAttachmentResponse(createAttachmentResponse);
					if (attachmentIdFromCreateAttachmentResponse != null)
					{
						userContext.CancelAttachmentManager.CreateAttachmentCompleted(this.cancellationId, attachmentIdFromCreateAttachmentResponse);
					}
					else
					{
						userContext.CancelAttachmentManager.CreateAttachmentCancelled(this.cancellationId);
					}
				}
			}
			return createAttachmentResponse;
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x0005C2F0 File Offset: 0x0005A4F0
		internal static CreateAttachmentResponse AttachReferenceAttachment(AttachmentDataProvider attachmentDataProvider, UserContext userContext, string location, string dataProviderItemId, string parentItemId, IdConverter idConverter, string dataProviderParentItemId = null, string providerEndpointUrl = null)
		{
			CreateAttachmentResponse result = null;
			if (!userContext.IsDisposed)
			{
				if (string.IsNullOrEmpty(providerEndpointUrl))
				{
					providerEndpointUrl = attachmentDataProvider.GetEndpointUrlFromItemLocation(location);
				}
				string linkingUrl = attachmentDataProvider.GetLinkingUrl(userContext, location, providerEndpointUrl, dataProviderItemId, dataProviderParentItemId);
				string text = Path.GetFileName(HttpUtility.UrlDecode(linkingUrl));
				if (OneDriveProUtilities.IsDurableUrlFormat(text))
				{
					text = text.Substring(0, text.LastIndexOf("?", StringComparison.InvariantCulture));
				}
				try
				{
					userContext.LockAndReconnectMailboxSession();
					IdAndSession idAndSession = new IdAndSession(StoreId.EwsIdToStoreObjectId(parentItemId), userContext.MailboxSession);
					ReferenceAttachmentType referenceAttachmentType = new ReferenceAttachmentType
					{
						Name = text,
						AttachLongPathName = linkingUrl,
						ProviderEndpointUrl = providerEndpointUrl,
						ProviderType = attachmentDataProvider.Type.ToString()
					};
					if (!userContext.IsGroupUserContext)
					{
						referenceAttachmentType.ContentId = Guid.NewGuid().ToString();
						referenceAttachmentType.ContentType = "image/png";
					}
					AttachmentHierarchy attachmentHierarchy = new AttachmentHierarchy(idAndSession, true, true);
					using (AttachmentBuilder attachmentBuilder = new AttachmentBuilder(attachmentHierarchy, new AttachmentType[]
					{
						referenceAttachmentType
					}, idConverter, true))
					{
						ServiceError serviceError;
						Attachment attachment = attachmentBuilder.CreateAttachment(referenceAttachmentType, out serviceError);
						if (serviceError == null)
						{
							attachmentHierarchy.SaveAll();
						}
						result = CreateAttachmentHelper.CreateAttachmentResponse(attachmentHierarchy, attachment, referenceAttachmentType, idAndSession, serviceError);
					}
				}
				finally
				{
					userContext.UnlockAndDisconnectMailboxSession();
				}
			}
			return result;
		}

		// Token: 0x04000E38 RID: 3640
		private readonly ItemId itemId;

		// Token: 0x04000E39 RID: 3641
		private readonly string attachmentDataProviderId;

		// Token: 0x04000E3A RID: 3642
		private readonly string location;

		// Token: 0x04000E3B RID: 3643
		private readonly string providerEndpointUrl;

		// Token: 0x04000E3C RID: 3644
		private readonly string cancellationId;

		// Token: 0x04000E3D RID: 3645
		private readonly string dataProviderItemId;

		// Token: 0x04000E3E RID: 3646
		private readonly string dataProviderParentItemId;
	}
}
