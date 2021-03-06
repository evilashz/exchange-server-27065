using System;
using System.IO;
using System.Security.AccessControl;
using System.Xml;
using System.Xml.Schema;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.UM;
using Microsoft.Exchange.UM.Rpc;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000219 RID: 537
	internal sealed class UMPartnerMessageRpcServerComponent : UMRPCComponentBase
	{
		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x0004668B File Offset: 0x0004488B
		internal static UMPartnerMessageRpcServerComponent Instance
		{
			get
			{
				return UMPartnerMessageRpcServerComponent.instance;
			}
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x00046694 File Offset: 0x00044894
		internal override void RegisterServer()
		{
			uint accessMask = 1U;
			FileSecurity allowExchangeServerSecurity = Util.GetAllowExchangeServerSecurity();
			RpcServerBase.RegisterServer(typeof(UMPartnerMessageRpcServerComponent.UMPartnerMessageRpcServer), allowExchangeServerSecurity, accessMask);
		}

		// Token: 0x04000B51 RID: 2897
		private static UMPartnerMessageRpcServerComponent instance = new UMPartnerMessageRpcServerComponent();

		// Token: 0x04000B52 RID: 2898
		private static PercentageBooleanSlidingCounter partnerTranscriptions = PercentageBooleanSlidingCounter.CreateFailureCounter(1000, TimeSpan.FromHours(1.0));

		// Token: 0x0200021B RID: 539
		internal sealed class UMPartnerMessageRpcServer : UMVersionedRpcServer
		{
			// Token: 0x170003CA RID: 970
			// (get) Token: 0x06000FAB RID: 4011 RVA: 0x00046888 File Offset: 0x00044A88
			protected override UMRPCComponentBase Component
			{
				get
				{
					return UMPartnerMessageRpcServerComponent.Instance;
				}
			}

			// Token: 0x06000FAC RID: 4012 RVA: 0x0004688F File Offset: 0x00044A8F
			protected override void PrepareRequest(UMVersionedRpcRequest request)
			{
				((ProcessPartnerMessageRequest)request).ProcessPartnerMessage = new ProcessPartnerMessageDelegate(this.ProcessPartnerMessage);
			}

			// Token: 0x06000FAD RID: 4013 RVA: 0x000468A8 File Offset: 0x00044AA8
			private void ProcessPartnerMessage(ProcessPartnerMessageRequest request)
			{
				VersionedId storeId = null;
				IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromTenantGuid(request.TenantGuid);
				ADUser aduser = iadrecipientLookup.LookupByExchangeGuid(request.MailboxGuid) as ADUser;
				if (aduser == null)
				{
					throw new UserNotFoundException(request.MailboxGuid);
				}
				ExchangePrincipal exchangePrincipal = ExchangePrincipal.FromADUser(aduser, RemotingOptions.AllowCrossSite);
				try
				{
					storeId = VersionedId.Deserialize(request.ItemId);
				}
				catch (ArgumentException innerException)
				{
					throw new UMInvalidPartnerMessageException("ItemId", innerException);
				}
				using (UMSubscriber umsubscriber = UMRecipient.Factory.FromPrincipal<UMSubscriber>(exchangePrincipal))
				{
					using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = umsubscriber.CreateSessionLock())
					{
						using (MessageItem messageItem = (MessageItem)Item.Bind(mailboxSessionLock.Session, storeId, ItemBindOption.None))
						{
							messageItem.Load(UMPartnerMessageRpcServerComponent.UMPartnerMessageRpcServer.propertiesToLoad);
							if (umsubscriber == null)
							{
								throw new UserNotUmEnabledException(exchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
							}
							UMPartnerMessageRpcServerComponent.PartnerMessageProcessor partnerMessageProcessor = UMPartnerMessageRpcServerComponent.PartnerMessageProcessor.Create(messageItem);
							partnerMessageProcessor.ProcessMessage(umsubscriber, mailboxSessionLock.Session, messageItem);
						}
					}
				}
			}

			// Token: 0x04000B53 RID: 2899
			public static IntPtr RpcIntfHandle = UMVersionedRpcServerBase.UMPartnerMessageRpcIntfHandle;

			// Token: 0x04000B54 RID: 2900
			private static readonly PropertyDefinition[] propertiesToLoad = new PropertyDefinition[]
			{
				ItemSchema.Id,
				MessageItemSchema.XMsExchangeUMPartnerStatus,
				MessageItemSchema.XMsExchangeUMPartnerContent,
				MessageItemSchema.XMsExchangeUMPartnerContext,
				MessageItemSchema.FaxNumberOfPages,
				ItemSchema.SentTime
			};
		}

		// Token: 0x0200021C RID: 540
		private abstract class PartnerMessageProcessor
		{
			// Token: 0x06000FB0 RID: 4016 RVA: 0x00046A34 File Offset: 0x00044C34
			internal static UMPartnerMessageRpcServerComponent.PartnerMessageProcessor Create(MessageItem messageItem)
			{
				string input = (string)XsoUtil.SafeGetProperty(messageItem, MessageItemSchema.XMsExchangeUMPartnerContent, string.Empty);
				UMPartnerMessageRpcServerComponent.PartnerMessageProcessor result;
				if (UMPartnerMessageRpcServerComponent.PartnerMessageProcessor.IsPartnerContent(input, "fax"))
				{
					result = new UMPartnerMessageRpcServerComponent.PartnerFaxProcessor();
				}
				else
				{
					if (!UMPartnerMessageRpcServerComponent.PartnerMessageProcessor.IsPartnerContent(input, "voice+transcript"))
					{
						throw new UMInvalidPartnerMessageException(MessageItemSchema.XMsExchangeUMPartnerContent.Name);
					}
					result = new UMPartnerMessageRpcServerComponent.PartnerTranscriptionProcessor();
				}
				return result;
			}

			// Token: 0x06000FB1 RID: 4017
			internal abstract void ProcessMessage(UMSubscriber recipient, MailboxSession mailboxSession, MessageItem messageItem);

			// Token: 0x06000FB2 RID: 4018 RVA: 0x00046A94 File Offset: 0x00044C94
			protected static void ThrowAndRejectMessageIfNull(object instance, string propertyName)
			{
				if (instance == null)
				{
					throw new UMInvalidPartnerMessageException(propertyName);
				}
			}

			// Token: 0x06000FB3 RID: 4019 RVA: 0x00046AA0 File Offset: 0x00044CA0
			protected static ITempFile CreateAttachmentTempFile(MessageItem messageItem, AttachmentHandle attachmentId)
			{
				ITempFile tempFile = null;
				bool flag = false;
				try
				{
					using (Attachment attachment = messageItem.AttachmentCollection.Open(attachmentId))
					{
						tempFile = TempFileFactory.CreateTempFileFromAttachment(attachment);
						StreamAttachment streamAttachment = attachment as StreamAttachment;
						UMPartnerMessageRpcServerComponent.PartnerMessageProcessor.ThrowAndRejectMessageIfNull(streamAttachment, attachment.FileName);
						using (Stream stream = new FileStream(tempFile.FilePath, FileMode.Create))
						{
							using (Stream contentStream = streamAttachment.GetContentStream())
							{
								CommonUtil.CopyStream(contentStream, stream);
							}
						}
					}
					flag = true;
				}
				finally
				{
					if (!flag && tempFile != null)
					{
						tempFile.Dispose();
						tempFile = null;
					}
				}
				return tempFile;
			}

			// Token: 0x06000FB4 RID: 4020 RVA: 0x00046B68 File Offset: 0x00044D68
			private static bool IsPartnerContent(string input, string partnerContent)
			{
				return string.Equals(input, partnerContent, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x0200021D RID: 541
		private class PartnerFaxProcessor : UMPartnerMessageRpcServerComponent.PartnerMessageProcessor
		{
			// Token: 0x06000FB6 RID: 4022 RVA: 0x00046B7C File Offset: 0x00044D7C
			internal override void ProcessMessage(UMSubscriber recipient, MailboxSession mailboxSession, MessageItem messageItem)
			{
				string status = XsoUtil.SafeGetProperty(messageItem, MessageItemSchema.XMsExchangeUMPartnerStatus, null) as string;
				UMPartnerFaxStatus umpartnerFaxStatus;
				if (!UMPartnerFaxStatus.TryParse(status, out umpartnerFaxStatus))
				{
					throw new UMInvalidPartnerMessageException(MessageItemSchema.XMsExchangeUMPartnerStatus.Name);
				}
				this.CheckStatus(recipient, umpartnerFaxStatus);
				string messageID = Util.GenerateMessageIdFromSeed(messageItem.Id);
				object obj = XsoUtil.SafeGetProperty(messageItem, ItemSchema.SentTime, null);
				UMPartnerMessageRpcServerComponent.PartnerMessageProcessor.ThrowAndRejectMessageIfNull(obj, "sentTime");
				ExDateTime sentTime = (ExDateTime)obj;
				string text = XsoUtil.SafeGetProperty(messageItem, MessageItemSchema.XMsExchangeUMPartnerContext, null) as string;
				UMPartnerMessageRpcServerComponent.PartnerMessageProcessor.ThrowAndRejectMessageIfNull(text, MessageItemSchema.XMsExchangeUMPartnerContext.Name);
				UMPartnerFaxContext umpartnerFaxContext;
				if (!UMPartnerContext.TryParse<UMPartnerFaxContext>(text, out umpartnerFaxContext))
				{
					throw new UMInvalidPartnerMessageException(MessageItemSchema.XMsExchangeUMPartnerContext.Name);
				}
				PhoneNumber callerId;
				if (!PhoneNumber.TryParse(umpartnerFaxContext.CallerId, true, out callerId))
				{
					throw new UMInvalidPartnerMessageException("CallerId");
				}
				string callerIdDisplayName = umpartnerFaxContext.CallerIdDisplayName;
				Guid tenantGuid = recipient.TenantGuid;
				if (umpartnerFaxStatus.MissedCall)
				{
					UMMessageSubmission.SubmitMissedCall(umpartnerFaxContext.CallId, callerId, null, recipient, false, null, messageID, sentTime, callerIdDisplayName, tenantGuid);
					return;
				}
				object obj2 = XsoUtil.SafeGetProperty(messageItem, MessageItemSchema.FaxNumberOfPages, 0);
				int num = (int)obj2;
				if (num <= 0)
				{
					throw new UMInvalidPartnerMessageException(MessageItemSchema.FaxNumberOfPages.Name);
				}
				AttachmentHandle attachmentHandle = XsoUtil.FindFirstAttachmentByContentType(messageItem, "image/tiff", 1);
				UMPartnerMessageRpcServerComponent.PartnerMessageProcessor.ThrowAndRejectMessageIfNull(attachmentHandle, "image/tiff");
				using (ITempFile tempFile = UMPartnerMessageRpcServerComponent.PartnerMessageProcessor.CreateAttachmentTempFile(messageItem, attachmentHandle))
				{
					UMMessageSubmission.SubmitFax(umpartnerFaxContext.CallId, callerId, null, recipient, (uint)num, tempFile.FilePath, umpartnerFaxStatus.IsCompleteFax, messageID, sentTime, callerIdDisplayName, tenantGuid);
				}
			}

			// Token: 0x06000FB7 RID: 4023 RVA: 0x00046D18 File Offset: 0x00044F18
			private void CheckStatus(UMSubscriber subscriber, UMPartnerFaxStatus faxStatus)
			{
				if (faxStatus.Type == FaxResultType.ServerErrorFax)
				{
					UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_FaxPartnerHasServerError, null, new object[]
					{
						subscriber.UMMailboxPolicy.FaxServerURI,
						subscriber.ADRecipient.PrimarySmtpAddress,
						faxStatus.Status
					});
				}
			}

			// Token: 0x04000B55 RID: 2901
			private const int MaxAttachmentCount = 1;
		}

		// Token: 0x0200021E RID: 542
		private class PartnerTranscriptionProcessor : UMPartnerMessageRpcServerComponent.PartnerMessageProcessor
		{
			// Token: 0x06000FB9 RID: 4025 RVA: 0x00046D7C File Offset: 0x00044F7C
			internal override void ProcessMessage(UMSubscriber subscriber, MailboxSession mailboxSession, MessageItem messageItem)
			{
				string text = XsoUtil.SafeGetProperty(messageItem, MessageItemSchema.XMsExchangeUMPartnerContext, string.Empty) as string;
				UMPartnerTranscriptionContext umpartnerTranscriptionContext;
				if (string.IsNullOrEmpty(text) || !UMPartnerContext.TryParse<UMPartnerTranscriptionContext>(text, out umpartnerTranscriptionContext))
				{
					throw new UMInvalidPartnerMessageException(MessageItemSchema.XMsExchangeUMPartnerContext.Name);
				}
				AudioCodecEnum codec;
				if (!EnumValidator<AudioCodecEnum>.TryParse(umpartnerTranscriptionContext.AudioCodec, EnumParseOptions.Default, out codec))
				{
					throw new UMInvalidPartnerMessageException("AudioCodec");
				}
				PhoneNumber empty = PhoneNumber.Empty;
				if (!string.IsNullOrEmpty(umpartnerTranscriptionContext.CallingParty) && !PhoneNumber.TryParse(umpartnerTranscriptionContext.CallingParty, out empty))
				{
					throw new UMInvalidPartnerMessageException("PhoneNumber");
				}
				string callerIdDisplayName = umpartnerTranscriptionContext.CallerIdDisplayName;
				ITempFile tempFile = null;
				MessageItem messageItem2 = null;
				UMSubscriber umsubscriber = null;
				try
				{
					if (umpartnerTranscriptionContext.CallerGuid != Guid.Empty)
					{
						IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromUmUser(subscriber);
						ADRecipient adrecipient = iadrecipientLookup.LookupByObjectId(new ADObjectId(umpartnerTranscriptionContext.CallerGuid));
						umsubscriber = UMRecipient.Factory.FromADRecipient<UMSubscriber>(adrecipient);
					}
					tempFile = this.GetVoiceMessage(messageItem, umpartnerTranscriptionContext);
					messageItem2 = this.GetInterpersonalMessage(subscriber, mailboxSession, messageItem, umpartnerTranscriptionContext);
					ITranscriptionData partnerTranscription = this.GetPartnerTranscription(messageItem, umpartnerTranscriptionContext);
					Util.IncrementCounter(AvailabilityCounters.PercentagePartnerTranscriptionFailures_Base);
					if (PartnerTranscriptionData.IsPartnerError(partnerTranscription.RecognitionError))
					{
						Util.IncrementCounter(AvailabilityCounters.PercentagePartnerTranscriptionFailures, 1L);
						Util.SetCounter(AvailabilityCounters.RecentPercentagePartnerTranscriptionFailures, (long)UMPartnerMessageRpcServerComponent.partnerTranscriptions.Update(false));
						UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_TranscriptionPartnerFailure, null, new object[]
						{
							subscriber.ADRecipient.DistinguishedName,
							partnerTranscription.RecognitionError.ToString(),
							subscriber.UMMailboxPolicy.VoiceMailPreviewPartnerAddress,
							subscriber.UMMailboxPolicy.VoiceMailPreviewPartnerAssignedID
						});
					}
					else
					{
						Util.SetCounter(AvailabilityCounters.RecentPercentagePartnerTranscriptionFailures, (long)UMPartnerMessageRpcServerComponent.partnerTranscriptions.Update(true));
					}
					Guid tenantGuid = subscriber.TenantGuid;
					if (messageItem2 != null)
					{
						UMMessageSubmission.SubmitXSOVoiceMail(umpartnerTranscriptionContext.CallId, empty, subscriber, tempFile.FilePath, umpartnerTranscriptionContext.Duration, codec, umpartnerTranscriptionContext.IpmAttachmentName, umpartnerTranscriptionContext.Culture, messageItem2, partnerTranscription, callerIdDisplayName, tenantGuid);
					}
					else
					{
						UMMessageSubmission.SubmitVoiceMail(umpartnerTranscriptionContext.CallId, empty, umsubscriber, subscriber, umpartnerTranscriptionContext.Culture, codec, umpartnerTranscriptionContext.Duration, tempFile.FilePath, umpartnerTranscriptionContext.IsImportant, umpartnerTranscriptionContext.Subject, umpartnerTranscriptionContext.IsCallAnsweringMessage, false, TranscriptionEnabledSetting.Enabled, partnerTranscription, callerIdDisplayName, tenantGuid);
					}
				}
				finally
				{
					if (umsubscriber != null)
					{
						umsubscriber.Dispose();
						umsubscriber = null;
					}
					if (tempFile != null)
					{
						tempFile.Dispose();
						tempFile = null;
					}
					if (messageItem2 != null)
					{
						messageItem2.Dispose();
						messageItem2 = null;
					}
				}
			}

			// Token: 0x06000FBA RID: 4026 RVA: 0x00046FEC File Offset: 0x000451EC
			private ITranscriptionData GetPartnerTranscription(MessageItem messageItem, UMPartnerTranscriptionContext context)
			{
				if (string.IsNullOrEmpty(context.PartnerTranscriptionAttachmentName))
				{
					throw new UMInvalidPartnerMessageException("PartnerTranscriptionAttachmentName");
				}
				AttachmentHandle attachmentHandle = XsoUtil.FindAttachmentByName(messageItem, context.PartnerTranscriptionAttachmentName, 3);
				UMPartnerMessageRpcServerComponent.PartnerMessageProcessor.ThrowAndRejectMessageIfNull(attachmentHandle, "Transcription");
				ITranscriptionData result;
				using (Attachment attachment = messageItem.AttachmentCollection.Open(attachmentHandle))
				{
					StreamAttachment streamAttachment = attachment as StreamAttachment;
					UMPartnerMessageRpcServerComponent.PartnerMessageProcessor.ThrowAndRejectMessageIfNull(streamAttachment, "Transcription");
					try
					{
						using (Stream contentStream = streamAttachment.GetContentStream())
						{
							result = new PartnerTranscriptionData(contentStream);
						}
					}
					catch (XmlException innerException)
					{
						throw new UMInvalidPartnerMessageException("Transcription", innerException);
					}
					catch (XmlSchemaException innerException2)
					{
						throw new UMInvalidPartnerMessageException("Transcription", innerException2);
					}
					catch (ArgumentException innerException3)
					{
						throw new UMInvalidPartnerMessageException("Transcription", innerException3);
					}
				}
				return result;
			}

			// Token: 0x06000FBB RID: 4027 RVA: 0x000470E0 File Offset: 0x000452E0
			private ITempFile GetVoiceMessage(MessageItem messageItem, UMPartnerTranscriptionContext context)
			{
				if (string.IsNullOrEmpty(context.PcmAudioAttachmentName))
				{
					throw new UMInvalidPartnerMessageException("PcmAudioAttachmentName");
				}
				AttachmentHandle attachmentHandle = XsoUtil.FindAttachmentByName(messageItem, context.PcmAudioAttachmentName, 3);
				UMPartnerMessageRpcServerComponent.PartnerMessageProcessor.ThrowAndRejectMessageIfNull(attachmentHandle, "PcmAudioAttachmentName");
				ITempWavFile result;
				using (ITempFile tempFile = UMPartnerMessageRpcServerComponent.PartnerMessageProcessor.CreateAttachmentTempFile(messageItem, attachmentHandle))
				{
					result = MediaMethods.ToPcm(tempFile);
				}
				return result;
			}

			// Token: 0x06000FBC RID: 4028 RVA: 0x0004714C File Offset: 0x0004534C
			private MessageItem GetInterpersonalMessage(UMSubscriber subscriber, MailboxSession mailboxSession, MessageItem messageItem, UMPartnerTranscriptionContext context)
			{
				if (string.IsNullOrEmpty(context.IpmAttachmentName))
				{
					return null;
				}
				AttachmentHandle attachmentHandle = XsoUtil.FindAttachmentByName(messageItem, context.IpmAttachmentName, 3);
				UMPartnerMessageRpcServerComponent.PartnerMessageProcessor.ThrowAndRejectMessageIfNull(attachmentHandle, "IpmAttachmentName");
				bool flag = false;
				MessageItem messageItem2 = null;
				try
				{
					messageItem2 = MessageItem.Create(mailboxSession, XsoUtil.GetDraftsFolderId(mailboxSession));
					using (Attachment attachment = messageItem.AttachmentCollection.Open(attachmentHandle))
					{
						StreamAttachment streamAttachment = attachment as StreamAttachment;
						UMPartnerMessageRpcServerComponent.PartnerMessageProcessor.ThrowAndRejectMessageIfNull(streamAttachment, "IpmAttachmentName");
						using (Stream contentStream = streamAttachment.GetContentStream())
						{
							AcceptedDomain defaultAcceptedDomain = Utils.GetDefaultAcceptedDomain(subscriber.ADRecipient);
							ItemConversion.ConvertMsgStorageToItem(contentStream, messageItem2, new InboundConversionOptions(defaultAcceptedDomain.DomainName.ToString())
							{
								UserADSession = ADRecipientLookupFactory.CreateFromUmUser(subscriber).ScopedRecipientSession
							});
						}
					}
					messageItem2.Save(SaveMode.ResolveConflicts);
					messageItem2.Load();
					flag = true;
				}
				finally
				{
					if (!flag && messageItem2 != null)
					{
						messageItem2.Dispose();
						messageItem2 = null;
					}
				}
				return messageItem2;
			}

			// Token: 0x04000B56 RID: 2902
			private const int MaxAttachments = 3;
		}
	}
}
