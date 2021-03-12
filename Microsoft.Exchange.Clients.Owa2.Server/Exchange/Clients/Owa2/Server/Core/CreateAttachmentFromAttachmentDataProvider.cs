using System;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002F7 RID: 759
	internal class CreateAttachmentFromAttachmentDataProvider : ServiceCommand<string>
	{
		// Token: 0x0600198F RID: 6543 RVA: 0x00059668 File Offset: 0x00057868
		public CreateAttachmentFromAttachmentDataProvider(CallContext callContext, ItemId draftEmailId, string attachmentDataProviderId, string location, string dataProviderItemId, string subscriptionId, string dataProviderParentItemId, string providerEndpointUrl, string channelId = null, string cancellationId = null) : base(callContext)
		{
			if (draftEmailId == null)
			{
				throw new ArgumentNullException("draftEmailId");
			}
			if (string.IsNullOrEmpty(attachmentDataProviderId))
			{
				throw new ArgumentException("The parameter cannot be null or empty.", "attachmentDataProviderId");
			}
			if (string.IsNullOrEmpty(dataProviderItemId))
			{
				throw new ArgumentException("The parameter cannot be null or empty.", "dataProviderItemId");
			}
			if (string.IsNullOrEmpty(subscriptionId))
			{
				throw new ArgumentException("The parameter cannot be null or empty.", "subscriptionId");
			}
			if (string.IsNullOrEmpty(location) && (string.IsNullOrEmpty(dataProviderItemId) || string.IsNullOrEmpty(dataProviderParentItemId)))
			{
				throw new ArgumentException("When the location is not specified we need valid itemId and dataProviderItemId.");
			}
			this.draftEmailId = draftEmailId;
			this.attachmentDataProviderId = attachmentDataProviderId;
			this.location = location;
			this.subscriptionId = subscriptionId;
			this.cancellationId = cancellationId;
			this.channelId = channelId;
			this.providerEndpointUrl = providerEndpointUrl;
			this.dataProviderItemId = dataProviderItemId;
			this.dataProviderParentItemId = dataProviderParentItemId;
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x00059740 File Offset: 0x00057940
		protected override string InternalExecute()
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			CancellationToken token = cancellationTokenSource.Token;
			UserContext userContext = UserContextManager.GetUserContext(base.CallContext.HttpContext, base.CallContext.EffectiveCaller, true);
			if (this.cancellationId != null && userContext.CancelAttachmentManager.OnCreateAttachment(this.cancellationId, cancellationTokenSource))
			{
				return null;
			}
			AttachmentDataProvider provider = userContext.AttachmentDataProviderManager.GetProvider(base.CallContext, this.attachmentDataProviderId);
			Guid operationId = Guid.NewGuid();
			CreateAttachmentFromAttachmentDataProvider.DownloadAndAttachFile(operationId, provider, userContext, this.location, this.dataProviderItemId, this.draftEmailId.Id, this.subscriptionId, base.IdConverter, this.channelId, this.dataProviderParentItemId, this.providerEndpointUrl, token, this.cancellationId);
			return operationId.ToString();
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00059ADC File Offset: 0x00057CDC
		internal static void DownloadAndAttachFile(Guid operationId, AttachmentDataProvider attachmentDataProvider, UserContext userContext, string location, string dataProviderItemId, string parentItemId, string subscriptionId, IdConverter idConverter, string channelId, string dataProviderParentItemId, string providerEndpointUrl, CancellationToken cancellationToken, string cancellationId)
		{
			AttachmentResultCode errorCode = AttachmentResultCode.GenericFailure;
			Exception exception = null;
			AttachmentIdType attachmentId = null;
			try
			{
				OwaDiagnostics.SendWatsonReportsForGrayExceptions(async delegate()
				{
					try
					{
						DownloadItemAsyncResult downloadItemResult = await attachmentDataProvider.DownloadItemAsync(location, dataProviderItemId, dataProviderParentItemId, providerEndpointUrl, cancellationToken).ConfigureAwait(false);
						CreateAttachmentNotificationPayload result = new CreateAttachmentNotificationPayload
						{
							SubscriptionId = subscriptionId,
							Id = operationId.ToString(),
							Bytes = downloadItemResult.Bytes,
							Item = downloadItemResult.Item,
							ResultCode = downloadItemResult.ResultCode
						};
						attachmentId = CreateAttachmentHelper.CreateAttachmentAndSendPendingGetNotification(userContext, parentItemId, result.Bytes, result.Item.Name, result, idConverter, channelId);
					}
					catch (OperationCanceledException exception)
					{
						errorCode = AttachmentResultCode.Cancelled;
						exception = exception;
						if (cancellationId != null)
						{
							userContext.CancelAttachmentManager.CreateAttachmentCancelled(cancellationId);
						}
					}
				});
			}
			catch (GrayException ex)
			{
				ExTraceGlobals.AttachmentHandlingTracer.TraceError<string>(0L, "CreateAttachmentFromAttachmentDataProvider.DownloadAndAttachFile Exception while trying to download and attach file async : {0}", ex.StackTrace);
				exception = ex;
			}
			finally
			{
				if (cancellationId != null)
				{
					userContext.CancelAttachmentManager.CreateAttachmentCompleted(cancellationId, attachmentId);
				}
				if (exception != null)
				{
					CreateAttachmentHelper.SendFailureNotification(userContext, subscriptionId, operationId.ToString(), errorCode, channelId, exception);
				}
			}
		}

		// Token: 0x04000E1B RID: 3611
		private readonly ItemId draftEmailId;

		// Token: 0x04000E1C RID: 3612
		private readonly string attachmentDataProviderId;

		// Token: 0x04000E1D RID: 3613
		private readonly string location;

		// Token: 0x04000E1E RID: 3614
		private readonly string subscriptionId;

		// Token: 0x04000E1F RID: 3615
		private readonly string channelId;

		// Token: 0x04000E20 RID: 3616
		private readonly string cancellationId;

		// Token: 0x04000E21 RID: 3617
		private readonly string providerEndpointUrl;

		// Token: 0x04000E22 RID: 3618
		private readonly string dataProviderItemId;

		// Token: 0x04000E23 RID: 3619
		private readonly string dataProviderParentItemId;
	}
}
