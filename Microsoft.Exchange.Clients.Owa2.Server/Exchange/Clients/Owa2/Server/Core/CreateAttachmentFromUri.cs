using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics.Components.Clients;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002F9 RID: 761
	internal class CreateAttachmentFromUri : ServiceCommand<string>
	{
		// Token: 0x06001995 RID: 6549 RVA: 0x00059D08 File Offset: 0x00057F08
		public CreateAttachmentFromUri(CallContext callContext, ItemId itemId, string uri, string name, string subscriptionId) : base(callContext)
		{
			if (itemId == null)
			{
				throw new ArgumentNullException("itemId");
			}
			if (string.IsNullOrWhiteSpace(uri))
			{
				throw new ArgumentNullException("uri");
			}
			this.itemId = itemId;
			this.uri = new Uri(uri);
			this.name = name;
			this.subscriptionId = subscriptionId;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x00059D60 File Offset: 0x00057F60
		protected override string InternalExecute()
		{
			UserContext userContext = UserContextManager.GetUserContext(base.CallContext.HttpContext, base.CallContext.EffectiveCaller, true);
			Guid operationId = Guid.NewGuid();
			CreateAttachmentFromUri.DownloadAndAttachFileFromUri(this.uri, this.name, this.subscriptionId, operationId, this.itemId, userContext, base.IdConverter);
			return operationId.ToString();
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0005A1A8 File Offset: 0x000583A8
		internal static void DownloadAndAttachFileFromUri(Uri uri, string name, string subscriptionId, Guid operationId, ItemId itemId, UserContext userContext, IdConverter idConverter)
		{
			try
			{
				OwaDiagnostics.SendWatsonReportsForGrayExceptions(async delegate()
				{
					try
					{
						using (HttpClient client = new HttpClient())
						{
							using (HttpResponseMessage response = await client.GetAsync(uri))
							{
								HttpStatusCode statusCode = response.StatusCode;
								AttachmentResultCode resultCode;
								if (statusCode != HttpStatusCode.OK)
								{
									switch (statusCode)
									{
									case HttpStatusCode.Forbidden:
										resultCode = AttachmentResultCode.AccessDenied;
										break;
									case HttpStatusCode.NotFound:
										resultCode = AttachmentResultCode.NotFound;
										break;
									default:
										if (statusCode != HttpStatusCode.RequestTimeout)
										{
											resultCode = AttachmentResultCode.GenericFailure;
										}
										else
										{
											resultCode = AttachmentResultCode.Timeout;
										}
										break;
									}
								}
								else
								{
									resultCode = AttachmentResultCode.Success;
								}
								if (resultCode != AttachmentResultCode.Success)
								{
									CreateAttachmentHelper.SendFailureNotification(userContext, subscriptionId, operationId.ToString(), resultCode, null, null);
								}
								byte[] buffer = await response.Content.ReadAsByteArrayAsync();
								CreateAttachmentNotificationPayload result = new CreateAttachmentNotificationPayload
								{
									SubscriptionId = subscriptionId,
									Id = operationId.ToString(),
									Bytes = buffer,
									Item = null,
									ResultCode = resultCode
								};
								CreateAttachmentHelper.CreateAttachmentAndSendPendingGetNotification(userContext, itemId.Id, buffer, name, result, idConverter);
							}
						}
					}
					catch (TaskCanceledException)
					{
					}
				});
			}
			catch (GrayException ex)
			{
				CreateAttachmentHelper.SendFailureNotification(userContext, subscriptionId, operationId.ToString(), AttachmentResultCode.GenericFailure, null, ex);
				ExTraceGlobals.AttachmentHandlingTracer.TraceError<string>(0L, "CreateAttachmentFromUri.DownloadAndAttachFileFromUri Exception while trying to download and attach file async : {0}", ex.StackTrace);
			}
		}

		// Token: 0x04000E25 RID: 3621
		private readonly string name;

		// Token: 0x04000E26 RID: 3622
		private readonly string subscriptionId;

		// Token: 0x04000E27 RID: 3623
		private ItemId itemId;

		// Token: 0x04000E28 RID: 3624
		private Uri uri;
	}
}
