using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.StoreDriver;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.MailboxTransport.StoreDriver.Configuration;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x0200009B RID: 155
	internal class MeetingMessageProcessingAgent : StoreDriverDeliveryAgent
	{
		// Token: 0x0600053F RID: 1343 RVA: 0x0001C48E File Offset: 0x0001A68E
		public MeetingMessageProcessingAgent()
		{
			base.OnCreatedMessage += this.OnCreatedMessageHandler;
			base.OnPromotedMessage += this.OnPromotedMessageHandler;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001C4BC File Offset: 0x0001A6BC
		private void OnPromotedMessageHandler(StoreDriverEventSource source, StoreDriverDeliveryEventArgs args)
		{
			if (StoreDriverConfig.Instance.IsAutoAcceptForGroupAndSelfForwardedEventEnabled)
			{
				StoreDriverDeliveryEventArgsImpl storeDriverDeliveryEventArgsImpl = (StoreDriverDeliveryEventArgsImpl)args;
				if ((ObjectClass.IsMeetingRequest(storeDriverDeliveryEventArgsImpl.MessageClass) || ObjectClass.IsMeetingResponse(storeDriverDeliveryEventArgsImpl.MessageClass)) && !this.IsEHAMigrationMeetingMessage(storeDriverDeliveryEventArgsImpl.MailItem))
				{
					MailboxSession mailboxSession = storeDriverDeliveryEventArgsImpl.MailboxSession;
					if (mailboxSession == null || storeDriverDeliveryEventArgsImpl.ReplayItem == null)
					{
						MeetingMessageProcessingAgent.tracer.TraceError((long)this.GetHashCode(), "MeetingMessageProcessingAgent::OnPromotedMessageHandler() MailboxSession or StoreDriverDeliveryEventArgsImpl.ReplayItem is null");
						return;
					}
					if (ObjectClass.IsMeetingRequest(storeDriverDeliveryEventArgsImpl.MessageClass) && !mailboxSession.IsGroupMailbox())
					{
						MeetingRequest meetingRequest = null;
						try
						{
							meetingRequest = (Item.ConvertFrom(storeDriverDeliveryEventArgsImpl.ReplayItem, mailboxSession) as MeetingRequest);
							if (meetingRequest != null && MeetingMessageProcessing.IsSentToSelf(meetingRequest, mailboxSession))
							{
								MeetingMessageProcessingAgent.tracer.TraceDebug<string, IExchangePrincipal>((long)this.GetHashCode(), "Attempting to deliver self forwarded message {0} to mailbox {1} to Deleted Items", meetingRequest.InternetMessageId, mailboxSession.MailboxOwner);
								this.DeliverToDeletedItems(mailboxSession, meetingRequest, storeDriverDeliveryEventArgsImpl);
							}
						}
						finally
						{
							if (meetingRequest != null)
							{
								Item.SafeDisposeConvertedItem(storeDriverDeliveryEventArgsImpl.ReplayItem, meetingRequest);
							}
						}
					}
					if (ObjectClass.IsMeetingResponse(storeDriverDeliveryEventArgsImpl.MessageClass) && mailboxSession.IsGroupMailbox())
					{
						MeetingResponse meetingResponse = Item.ConvertFrom(storeDriverDeliveryEventArgsImpl.ReplayItem, mailboxSession) as MeetingResponse;
						if (meetingResponse != null && meetingResponse.IsSilent)
						{
							ADRecipient adrecipient = null;
							if (meetingResponse.From.TryGetADRecipient(mailboxSession.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid), out adrecipient) && adrecipient != null && adrecipient.RecipientDisplayType != RecipientDisplayType.ConferenceRoomMailbox && adrecipient.RecipientDisplayType != RecipientDisplayType.SyncedConferenceRoomMailbox && adrecipient.RecipientDisplayType != RecipientDisplayType.EquipmentMailbox && adrecipient.RecipientDisplayType != RecipientDisplayType.SyncedEquipmentMailbox && !meetingResponse.IsCounterProposal)
							{
								MeetingMessageProcessingAgent.tracer.TraceDebug<string, IExchangePrincipal>((long)this.GetHashCode(), "Attempting to deliver empty response {0} to mailbox {1} to Deleted Items", meetingResponse.InternetMessageId, mailboxSession.MailboxOwner);
								this.DeliverToDeletedItems(mailboxSession, meetingResponse, storeDriverDeliveryEventArgsImpl);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001C6E0 File Offset: 0x0001A8E0
		public void OnCreatedMessageHandler(StoreDriverEventSource source, StoreDriverDeliveryEventArgs args)
		{
			StoreDriverDeliveryEventArgsImpl storeDriverDeliveryEventArgsImpl = (StoreDriverDeliveryEventArgsImpl)args;
			if (this.ShouldProcessMessage(storeDriverDeliveryEventArgsImpl))
			{
				MeetingMessageProcessing.ProcessMessage(storeDriverDeliveryEventArgsImpl.MessageItem, storeDriverDeliveryEventArgsImpl.MailRecipient, this.GetCachedCalendarItemIdFromHeaders(storeDriverDeliveryEventArgsImpl), storeDriverDeliveryEventArgsImpl.DeliverToFolder);
				this.SaveChangedPropertiesForDelegateForward(storeDriverDeliveryEventArgsImpl);
			}
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001C724 File Offset: 0x0001A924
		private StoreObjectId GetCachedCalendarItemIdFromHeaders(StoreDriverDeliveryEventArgsImpl args)
		{
			if (MeetingSeriesMessageOrderingAgent.SeriesMessageOrderingEnabled(args.MailboxOwner))
			{
				TextHeader textHeader = args.MailItem.Message.MimeDocument.RootPart.Headers.FindFirst("X-MS-Exchange-Calendar-Series-Instance-Calendar-Item-Id") as TextHeader;
				if (textHeader != null && !string.IsNullOrEmpty(textHeader.Value))
				{
					try
					{
						return StoreObjectId.Deserialize(textHeader.Value);
					}
					catch (FormatException arg)
					{
						MeetingMessageProcessingAgent.tracer.TraceWarning<string, string, FormatException>((long)this.GetHashCode(), "Error deserializing cached calender item id from headers. Message {0}, mailbox {1}, error: {2}", args.MessageItem.InternetMessageId, args.MailboxSession.MailboxOwnerLegacyDN, arg);
					}
					catch (CorruptDataException arg2)
					{
						MeetingMessageProcessingAgent.tracer.TraceWarning<string, string, CorruptDataException>((long)this.GetHashCode(), "Error deserializing cached calender item id from headers. Message {0}, mailbox {1}, error: {2}", args.MessageItem.InternetMessageId, args.MailboxSession.MailboxOwnerLegacyDN, arg2);
					}
				}
			}
			return null;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0001C808 File Offset: 0x0001AA08
		private void SaveChangedPropertiesForDelegateForward(StoreDriverDeliveryEventArgsImpl argsImpl)
		{
			if (argsImpl.PropertiesForDelegateForward == null)
			{
				argsImpl.PropertiesForDelegateForward = new Dictionary<PropertyDefinition, object>(MeetingMessageProcessingAgent.propertyChangingByProcessing.Length);
			}
			foreach (PropertyDefinition propertyDefinition in MeetingMessageProcessingAgent.propertyChangingByProcessing)
			{
				try
				{
					object obj = argsImpl.MessageItem.TryGetProperty(propertyDefinition);
					if (obj != null && !(obj is PropertyError))
					{
						if (propertyDefinition == CalendarItemBaseSchema.MeetingRequestType && obj is int && (MeetingMessageType)obj == MeetingMessageType.PrincipalWantsCopy)
						{
							MeetingMessageType valueOrDefault = argsImpl.MessageItem.GetValueOrDefault<MeetingMessageType>(MeetingMessageInstanceSchema.OriginalMeetingType, MeetingMessageType.FullUpdate);
							argsImpl.PropertiesForDelegateForward[propertyDefinition] = valueOrDefault;
						}
						else
						{
							argsImpl.PropertiesForDelegateForward[propertyDefinition] = obj;
						}
					}
				}
				catch (NotInBagPropertyErrorException)
				{
				}
			}
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001C8D0 File Offset: 0x0001AAD0
		private bool ShouldProcessMessage(StoreDriverDeliveryEventArgsImpl argsImpl)
		{
			return (ObjectClass.IsMeetingMessage(argsImpl.MessageClass) || ObjectClass.IsMeetingMessageSeries(argsImpl.MessageClass)) && !this.IsEHAMigrationMeetingMessage(argsImpl.MailItem) && (!ObjectClass.IsMeetingMessageSeries(argsImpl.MessageClass) || this.SeriesMessageProcessingEnabled(argsImpl.MailboxOwner));
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001C924 File Offset: 0x0001AB24
		private bool SeriesMessageProcessingEnabled(MiniRecipient mailOwner)
		{
			return mailOwner != null && VariantConfiguration.GetSnapshot(mailOwner.GetContext(null), null, null).MailboxTransport.ProcessSeriesMeetingMessages.Enabled;
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0001C958 File Offset: 0x0001AB58
		private bool IsEHAMigrationMeetingMessage(DeliverableMailItem messageItem)
		{
			return messageItem.Message.MimeDocument.RootPart.Headers.FindFirst(UnJournalAgent.UnjournalHeaders.MessageOriginalDate) != null;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001C98C File Offset: 0x0001AB8C
		private void DeliverToDeletedItems(MailboxSession mailboxSession, MeetingMessage meetingMessage, StoreDriverDeliveryEventArgsImpl deliveryArgument)
		{
			StoreObjectId defaultFolderId = mailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems);
			if (defaultFolderId == null)
			{
				MeetingMessageProcessingAgent.tracer.TraceError<string, IExchangePrincipal>((long)this.GetHashCode(), "Meeting message {0} to mailbox {1} could not deliver to Deleted Items as this folder does not exist", meetingMessage.InternetMessageId, mailboxSession.MailboxOwner);
				return;
			}
			deliveryArgument.DeliverToFolder = defaultFolderId;
			deliveryArgument.ShouldSkipMoveRule = true;
			MeetingMessageProcessingAgent.tracer.TraceDebug<string, IExchangePrincipal>((long)this.GetHashCode(), "Meeting message {0} to mailbox {1} is assigned to deliver to Deleted Items", meetingMessage.InternetMessageId, mailboxSession.MailboxOwner);
		}

		// Token: 0x040002D9 RID: 729
		private static readonly PropertyDefinition[] propertyChangingByProcessing = new PropertyDefinition[]
		{
			MeetingMessageInstanceSchema.CalendarProcessingSteps,
			MeetingMessageInstanceSchema.OriginalMeetingType,
			CalendarItemBaseSchema.MeetingRequestType
		};

		// Token: 0x040002DA RID: 730
		private static readonly Trace tracer = ExTraceGlobals.MeetingMessageProcessingAgentTracer;
	}
}
