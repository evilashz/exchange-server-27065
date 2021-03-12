﻿using System;
using System.IO;
using System.Web;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002FA RID: 762
	internal static class CreateAttachmentHelper
	{
		// Token: 0x06001998 RID: 6552 RVA: 0x0005A258 File Offset: 0x00058458
		internal static CreateAttachmentRequest CreateAttachmentRequest(ItemId parentItemId, string fileName, int size, string contentType, byte[] content, bool isInline, string cancellationId)
		{
			CreateAttachmentRequest createAttachmentRequest = new CreateAttachmentRequest();
			FileAttachmentType fileAttachmentType = new FileAttachmentType();
			fileAttachmentType.Name = fileName;
			fileAttachmentType.Size = size;
			fileAttachmentType.ContentType = contentType;
			fileAttachmentType.Content = content;
			fileAttachmentType.IsInline = isInline;
			createAttachmentRequest.ParentItemId = parentItemId;
			createAttachmentRequest.Attachments = new AttachmentType[]
			{
				fileAttachmentType
			};
			createAttachmentRequest.RequireImageType = fileAttachmentType.IsInline;
			createAttachmentRequest.IncludeContentIdInResponse = fileAttachmentType.IsInline;
			createAttachmentRequest.ClientSupportsIrm = true;
			createAttachmentRequest.CancellationId = cancellationId;
			return createAttachmentRequest;
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x0005A2D8 File Offset: 0x000584D8
		internal static CreateAttachmentResponse CreateAttachment(CallContext callContext, CreateAttachmentRequest translatedRequest)
		{
			CreateAttachmentHelper.UpdateContentType(callContext);
			CreateAttachmentResponse result;
			using (CreateAttachment createAttachment = new CreateAttachment(callContext, translatedRequest))
			{
				if (!createAttachment.PreExecute())
				{
					result = null;
				}
				else
				{
					ServiceResult<AttachmentType> currentStepResult;
					try
					{
						currentStepResult = createAttachment.Execute();
					}
					catch (LocalizedException exception)
					{
						currentStepResult = ExceptionHandler<AttachmentType>.GetServiceResult<AttachmentType>(exception, null);
					}
					createAttachment.SetCurrentStepResult(currentStepResult);
					result = (CreateAttachmentResponse)createAttachment.PostExecute();
				}
			}
			return result;
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x0005A350 File Offset: 0x00058550
		internal static void UpdateContentType(CallContext callContext)
		{
			IOutgoingWebResponseContext outgoingWebResponseContext = callContext.CreateWebResponseContext();
			outgoingWebResponseContext.ContentType = "text/plain";
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x0005A370 File Offset: 0x00058570
		internal static CreateAttachmentResponse CreateAttachmentResponse(AttachmentHierarchy attachmentHierarchy, Attachment attachment, AttachmentType attachmentType, IdAndSession parentIdAndSession, ServiceError warning)
		{
			IdAndSession idAndSession = parentIdAndSession.Clone();
			attachment.Load();
			idAndSession.AttachmentIds.Add(attachment.Id);
			AttachmentType attachmentType2;
			if (attachment is StreamAttachment)
			{
				attachmentType2 = new FileAttachmentType();
			}
			else if (attachment is ReferenceAttachment)
			{
				attachmentType2 = new ReferenceAttachmentType();
				((ReferenceAttachmentType)attachmentType2).AttachLongPathName = (string)attachment.TryGetProperty(AttachmentSchema.AttachLongPathName);
				((ReferenceAttachmentType)attachmentType2).ProviderType = (string)attachment.TryGetProperty(AttachmentSchema.AttachmentProviderType);
				((ReferenceAttachmentType)attachmentType2).ProviderEndpointUrl = (string)attachment.TryGetProperty(AttachmentSchema.AttachmentProviderEndpointUrl);
				object obj = attachment.TryGetProperty(AttachmentSchema.AttachContentId);
				if (!(obj is PropertyError))
				{
					((ReferenceAttachmentType)attachmentType2).ContentId = (string)obj;
				}
				attachmentType2.Name = attachmentType.Name;
			}
			else
			{
				attachmentType2 = new ItemAttachmentType();
			}
			attachmentType2.AttachmentId = new AttachmentIdType(idAndSession.GetConcatenatedId().Id);
			if (attachmentType is ItemIdAttachmentType || attachmentType is ReferenceAttachmentType)
			{
				attachmentType2.Size = ((attachment.Size > 2147483647L) ? int.MaxValue : ((int)attachment.Size));
			}
			CreateAttachmentResponse createAttachmentResponse = new CreateAttachmentResponse();
			ServiceResult<AttachmentType> serviceResult = (warning == null) ? new ServiceResult<AttachmentType>(attachmentType2) : new ServiceResult<AttachmentType>(attachmentType2, warning);
			attachmentHierarchy.RootItem.Load();
			if (serviceResult.Value != null)
			{
				ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(attachmentHierarchy.RootItem.Id, parentIdAndSession, null);
				serviceResult.Value.AttachmentId.RootItemId = concatenatedId.Id;
				serviceResult.Value.AttachmentId.RootItemChangeKey = concatenatedId.ChangeKey;
			}
			createAttachmentResponse.AddResponses(new ServiceResult<AttachmentType>[]
			{
				serviceResult
			});
			return createAttachmentResponse;
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x0005A520 File Offset: 0x00058720
		internal static void CreateAttachmentAndSendPendingGetNotification(UserContext userContext, string itemId, byte[] bytes, string name, CreateAttachmentNotificationPayload result, IdConverter idConverter)
		{
			CreateAttachmentHelper.CreateAttachmentAndSendPendingGetNotification(userContext, itemId, bytes, name, result, idConverter, null);
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x0005A534 File Offset: 0x00058734
		internal static AttachmentIdType CreateAttachmentAndSendPendingGetNotification(UserContext userContext, string itemId, byte[] bytes, string name, CreateAttachmentNotificationPayload result, IdConverter idConverter, string channelId)
		{
			AttachmentIdType result2 = null;
			if (!userContext.IsDisposed)
			{
				AttachmentBuilder attachmentBuilder = null;
				try
				{
					userContext.LockAndReconnectMailboxSession();
					if (result.ResultCode == AttachmentResultCode.Success)
					{
						IdAndSession idAndSession = new IdAndSession(StoreId.EwsIdToStoreObjectId(itemId), userContext.MailboxSession);
						FileAttachmentType fileAttachmentType = new FileAttachmentType
						{
							Content = bytes,
							IsInline = false,
							Name = name,
							Size = bytes.Length
						};
						AttachmentHierarchy attachmentHierarchy = new AttachmentHierarchy(idAndSession, true, true);
						attachmentBuilder = new AttachmentBuilder(attachmentHierarchy, new AttachmentType[]
						{
							fileAttachmentType
						}, idConverter, true);
						ServiceError serviceError;
						using (Attachment attachment = attachmentBuilder.CreateAttachment(fileAttachmentType, out serviceError))
						{
							if (serviceError == null)
							{
								attachmentHierarchy.SaveAll();
							}
							result.Response = CreateAttachmentHelper.CreateAttachmentResponse(attachmentHierarchy, attachment, fileAttachmentType, idAndSession, serviceError);
						}
						if (result.Response.ResponseMessages.Items != null && result.Response.ResponseMessages.Items.Length > 0 && result.Response.ResponseMessages.Items[0].ResponseCode == ResponseCodeType.NoError)
						{
							((AttachmentInfoResponseMessage)result.Response.ResponseMessages.Items[0]).Attachments[0].Size = fileAttachmentType.Size;
							result2 = CreateAttachmentHelper.GetAttachmentIdFromCreateAttachmentResponse(result.Response);
						}
					}
					CreateAttachmentHelper.SendPendingGetNotification(userContext, result, channelId);
				}
				finally
				{
					userContext.UnlockAndDisconnectMailboxSession();
					if (attachmentBuilder != null)
					{
						attachmentBuilder.Dispose();
					}
				}
			}
			return result2;
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x0005A6C8 File Offset: 0x000588C8
		internal static void SendPendingGetNotification(UserContext userContext, CreateAttachmentNotificationPayload result)
		{
			CreateAttachmentHelper.SendPendingGetNotification(userContext, result, null);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x0005A6D4 File Offset: 0x000588D4
		internal static void SendPendingGetNotification(UserContext userContext, CreateAttachmentNotificationPayload result, string channelId)
		{
			if (!userContext.IsDisposed)
			{
				OwaServerTraceLogger.AppendToLog(new TraceLogEvent("AttachmentOperationCompletedNotification", userContext, "SendPendingGetNotification", "Attachment operation completed with result code: " + result.ResultCode.ToString()));
				CreateAttachmentNotifier createAttachmentNotifier = new CreateAttachmentNotifier(userContext, result.SubscriptionId);
				try
				{
					createAttachmentNotifier.RegisterWithPendingRequestNotifier();
					createAttachmentNotifier.Payload = result;
					if (userContext.IsGroupUserContext)
					{
						bool flag;
						RemoteNotificationManager.Instance.Subscribe(userContext.Key.ToString(), userContext.MailboxIdentity.PrimarySmtpAddress.ToString(), createAttachmentNotifier.SubscriptionId, channelId, userContext.LogonIdentity.GetOWAMiniRecipient().PrimarySmtpAddress.ToString(), NotificationType.CreateAttachmentNotification, out flag);
					}
					createAttachmentNotifier.PickupData();
				}
				finally
				{
					createAttachmentNotifier.UnregisterWithPendingRequestNotifier();
					if (userContext.IsGroupUserContext)
					{
						RemoteNotificationManager.Instance.UnSubscribe(userContext.Key.ToString(), createAttachmentNotifier.SubscriptionId, channelId, userContext.LogonIdentity.GetOWAMiniRecipient().PrimarySmtpAddress.ToString());
					}
				}
			}
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x0005A7F8 File Offset: 0x000589F8
		internal static void SendFailureNotification(UserContext userContext, string subscriptionId, string operationId, AttachmentResultCode code, string channelId, Exception ex)
		{
			if (ex != null)
			{
				OwaServerTraceLogger.AppendToLog(new TraceLogEvent("CreateAttachmentExceptionTrace", userContext, "SendFailureNotification", "Attachment operation failed. Stack Trace:\n" + ex.ToString()));
			}
			CreateAttachmentNotificationPayload result = new CreateAttachmentNotificationPayload
			{
				SubscriptionId = subscriptionId,
				Id = operationId,
				Bytes = null,
				Item = null,
				ResultCode = code
			};
			CreateAttachmentHelper.SendPendingGetNotification(userContext, result, channelId);
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x0005A864 File Offset: 0x00058A64
		internal static AttachmentIdType GetAttachmentIdFromCreateAttachmentResponse(CreateAttachmentResponse response)
		{
			AttachmentIdType result = null;
			if (response != null && response.ResponseMessages != null && response.ResponseMessages.Items != null && response.ResponseMessages.Items.Length > 0)
			{
				AttachmentInfoResponseMessage attachmentInfoResponseMessage = (AttachmentInfoResponseMessage)response.ResponseMessages.Items[0];
				if (attachmentInfoResponseMessage.ResponseCode == ResponseCodeType.NoError && attachmentInfoResponseMessage.Attachments != null && attachmentInfoResponseMessage.Attachments.Length > 0)
				{
					result = attachmentInfoResponseMessage.Attachments[0].AttachmentId;
				}
			}
			return result;
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0005A8D8 File Offset: 0x00058AD8
		internal static byte[] GetContentBytes(Stream stream)
		{
			byte[] array = new byte[stream.Length];
			int num = 0;
			while ((long)num < stream.Length)
			{
				num += stream.Read(array, num, (int)stream.Length - num);
			}
			return array;
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0005A918 File Offset: 0x00058B18
		internal static CreateReferenceAttachmentFromLocalFileRequest CreateReferenceAttachmentRequest(HttpRequest request)
		{
			HttpPostedFile httpPostedFile = request.Files[0];
			return new CreateReferenceAttachmentFromLocalFileRequest
			{
				FileContentToUpload = Convert.ToBase64String(CreateAttachmentHelper.GetContentBytes(httpPostedFile.InputStream)),
				FileName = httpPostedFile.FileName,
				ParentItemId = new ItemId(request.Form["parentItemId"], request.Form["parentChangeKey"]),
				SubscriptionId = request.Form["subscriptionId"]
			};
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x0005A9A0 File Offset: 0x00058BA0
		internal static CreateAttachmentResponse BuildCreateAttachmentResponseForCancelled()
		{
			CreateAttachmentResponse createAttachmentResponse = new CreateAttachmentResponse();
			createAttachmentResponse.ResponseMessages = new ArrayOfResponseMessages();
			AttachmentInfoResponseMessage attachmentInfoResponseMessage = new AttachmentInfoResponseMessage(ServiceResultCode.Success, null, null);
			attachmentInfoResponseMessage.ResponseClass = ResponseClass.Success;
			attachmentInfoResponseMessage.ResponseCode = ResponseCodeType.NoError;
			attachmentInfoResponseMessage.IsCancelled = true;
			createAttachmentResponse.ResponseMessages.AddResponse(attachmentInfoResponseMessage, ResponseType.CreateAttachmentResponseMessage);
			return createAttachmentResponse;
		}

		// Token: 0x04000E29 RID: 3625
		public const string ParentItemIdFieldName = "parentItemId";

		// Token: 0x04000E2A RID: 3626
		public const string ParentItemChangekeyFieldName = "parentChangeKey";

		// Token: 0x04000E2B RID: 3627
		public const string SubscriptionIdFieldName = "subscriptionId";

		// Token: 0x04000E2C RID: 3628
		public const string CancellationIdFieldName = "cancellationId";
	}
}
