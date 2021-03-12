using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.GroupMailbox.Escalation;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Data.Transport.StoreDriver;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;
using Microsoft.Exchange.MailboxTransport.StoreDriver.Configuration;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000073 RID: 115
	internal class GroupEscalationAgent : StoreDriverDeliveryAgent
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x00014854 File Offset: 0x00012A54
		public GroupEscalationAgent(ProcessedMessageTracker processedMessages)
		{
			this.processedMessages = processedMessages;
			base.OnCreatedMessage += this.OnCreatedMessageHandler;
			base.OnDeliveredMessage += this.OnDeliveredMessageHandler;
			base.OnPromotedMessage += this.OnPromotedMessageHandler;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000148A4 File Offset: 0x00012AA4
		public void OnDeliveredMessageHandler(StoreDriverEventSource source, StoreDriverDeliveryEventArgs args)
		{
			StoreDriverDeliveryEventArgsImpl storeDriverDeliveryEventArgsImpl = (StoreDriverDeliveryEventArgsImpl)args;
			MailboxSession mailboxSession = storeDriverDeliveryEventArgsImpl.MailboxSession;
			if (mailboxSession == null || !mailboxSession.IsGroupMailbox())
			{
				GroupEscalationAgent.Tracer.TraceDebug((long)this.GetHashCode(), "GroupEscalationAgent.OnDeliveredMessageHandler: Session null or not a group mailbox.");
				return;
			}
			MessageItem messageItem = storeDriverDeliveryEventArgsImpl.MessageItem;
			messageItem.Load(new PropertyDefinition[]
			{
				ItemSchema.InternetMessageId,
				ItemSchema.SentTime
			});
			this.processedMessages.ClearMessageFromProcessedList(messageItem.InternetMessageId, messageItem.SentTime, mailboxSession.MailboxGuid);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00014924 File Offset: 0x00012B24
		public void OnCreatedMessageHandler(StoreDriverEventSource source, StoreDriverDeliveryEventArgs args)
		{
			StoreDriverDeliveryEventArgsImpl storeDriverDeliveryEventArgsImpl = (StoreDriverDeliveryEventArgsImpl)args;
			MessageItem messageItem = storeDriverDeliveryEventArgsImpl.MessageItem;
			MbxTransportMailItem mbxTransportMailItem = storeDriverDeliveryEventArgsImpl.MailItemDeliver.MbxTransportMailItem;
			if (messageItem == null || mbxTransportMailItem == null)
			{
				GroupEscalationAgent.Tracer.TraceError((long)this.GetHashCode(), "No message to process");
				return;
			}
			MailboxSession mailboxSession = messageItem.Session as MailboxSession;
			if (GroupEscalationAgent.ShouldGenerateIrmNdr(mailboxSession, messageItem, storeDriverDeliveryEventArgsImpl.MailboxOwner))
			{
				GroupEscalationAgent.Tracer.TraceDebug((long)this.GetHashCode(), "GroupEscalationAgent.ShouldProcessMessage: NDR the message since it's irm message sent to group");
				throw new SmtpResponseException(GroupEscalationAgent.IrmMessageDeliveryToGroupMailBoxError);
			}
			if (!this.ShouldProcessMessage(mailboxSession, storeDriverDeliveryEventArgsImpl))
			{
				return;
			}
			IGroupEscalationFlightInfo groupEscalationFlightInfo = new GroupEscalationFlightInfo(storeDriverDeliveryEventArgsImpl.MailboxOwner.GetContext(null));
			TransportGroupEscalation transportGroupEscalation = new TransportGroupEscalation(mbxTransportMailItem, XSOFactory.Default, groupEscalationFlightInfo, new MailboxUrls(mailboxSession.MailboxOwner, false));
			bool flag;
			if (transportGroupEscalation.EscalateItem(messageItem, mailboxSession, out flag, false))
			{
				GroupEscalationAgent.Tracer.TraceDebug<string>((long)this.GetHashCode(), "GroupEscalationAgent.EscalateItem: Escalation of message {0} from group mailbox successful", messageItem.InternetMessageId);
				this.processedMessages.AddMessageToProcessedList(messageItem.InternetMessageId, messageItem.SentTime, mailboxSession.MailboxGuid, DeliveryStage.CreatedMessageEventHandled);
				return;
			}
			GroupEscalationAgent.Tracer.TraceError<string, bool>((long)this.GetHashCode(), "GroupEscalationAgent.EscalateItem: Escalation of message {0} from group mailbox failed. IsTransientError: {1}", messageItem.InternetMessageId, flag);
			this.processedMessages.ClearMessageFromProcessedList(messageItem.InternetMessageId, messageItem.SentTime, mailboxSession.MailboxGuid);
			if (flag)
			{
				throw new SmtpResponseException(GroupEscalationAgent.EscalationFailedTransientError);
			}
			throw new SmtpResponseException(GroupEscalationAgent.EscalationFailedPermanentError);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00014A7C File Offset: 0x00012C7C
		public void OnPromotedMessageHandler(StoreDriverEventSource source, StoreDriverDeliveryEventArgs args)
		{
			if (args == null)
			{
				return;
			}
			StoreDriverDeliveryEventArgsImpl storeDriverDeliveryEventArgsImpl = (StoreDriverDeliveryEventArgsImpl)args;
			MailboxSession mailboxSession = storeDriverDeliveryEventArgsImpl.MailboxSession;
			string messageClass = storeDriverDeliveryEventArgsImpl.MessageClass;
			if (GroupEscalationAgent.ShouldBlockMessageForGroup(mailboxSession, messageClass))
			{
				storeDriverDeliveryEventArgsImpl.DeliverToFolder = mailboxSession.GetDefaultFolderId(DefaultFolderType.DeletedItems);
				GroupEscalationAgent.Tracer.TraceDebug<string>((long)this.GetHashCode(), "GroupEscalationAgent.OnPromotedMessageHandler: message is blocked. Its class is {0}", messageClass);
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00014ACF File Offset: 0x00012CCF
		private static bool ShouldGenerateIrmNdr(MailboxSession session, MessageItem messageItem, MiniRecipient mailboxOwner)
		{
			return session != null && session.IsGroupMailbox() && messageItem.IsRestricted && !GroupEscalationAgent.IsIrmEnabledGroup(mailboxOwner);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00014AEF File Offset: 0x00012CEF
		private static bool IsIrmEnabledGroup(MiniRecipient mailboxOwner)
		{
			return true;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00014AF4 File Offset: 0x00012CF4
		private static bool IsRepairUpdateMessage(MessageItem messageItem)
		{
			messageItem.Load(new PropertyDefinition[]
			{
				MeetingMessageSchema.AppointmentAuxiliaryFlags
			});
			AppointmentAuxiliaryFlags valueOrDefault = messageItem.GetValueOrDefault<AppointmentAuxiliaryFlags>(MeetingMessageSchema.AppointmentAuxiliaryFlags);
			return (valueOrDefault & AppointmentAuxiliaryFlags.RepairUpdateMessage) != (AppointmentAuxiliaryFlags)0;
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00014B30 File Offset: 0x00012D30
		private static bool IsEHAMigrationMeetingMessage(DeliverableMailItem messageItem)
		{
			return messageItem.Message.MimeDocument.RootPart.Headers.FindFirst(UnJournalAgent.UnjournalHeaders.MessageOriginalDate) != null;
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00014B63 File Offset: 0x00012D63
		private static bool ShouldBlockMessageForGroup(MailboxSession session, string messageClass)
		{
			return session != null && session.IsGroupMailbox() && GroupEscalationAgent.IsOofOrDsnMessage(messageClass);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x00014B78 File Offset: 0x00012D78
		private static bool IsOofOrDsnMessage(string messageClass)
		{
			return ObjectClass.IsOfClass(messageClass, "IPM.Note.Rules.OofTemplate.Microsoft") || ObjectClass.IsDsn(messageClass);
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00014B90 File Offset: 0x00012D90
		private bool ShouldProcessMessage(MailboxSession session, StoreDriverDeliveryEventArgsImpl argsImpl)
		{
			MessageItem messageItem = argsImpl.MessageItem;
			if (!StoreDriverConfig.Instance.IsGroupEscalationAgentEnabled)
			{
				GroupEscalationAgent.Tracer.TraceDebug((long)this.GetHashCode(), "GroupEscalationAgent.ShouldProcessMessage: skipping group message escalation as the feature is disabled via app config.");
				return false;
			}
			if (!GroupEscalation.IsEscalationEnabled())
			{
				GroupEscalationAgent.Tracer.TraceDebug((long)this.GetHashCode(), "GroupEscalationAgent.ShouldProcessMessage: skipping group message escalation as the feature is disabled.");
				return false;
			}
			if (session == null || !session.IsGroupMailbox())
			{
				GroupEscalationAgent.Tracer.TraceDebug((long)this.GetHashCode(), "GroupEscalationAgent.ShouldProcessMessage: skipping group message escalation as the session is not for a group mailbox.");
				return false;
			}
			if (this.processedMessages.IsAlreadyProcessedForStage(messageItem.InternetMessageId, messageItem.SentTime, session.MailboxGuid, DeliveryStage.CreatedMessageEventHandled))
			{
				GroupEscalationAgent.Tracer.TraceDebug((long)this.GetHashCode(), "GroupEscalationAgent.ShouldProcessMessage: skipping group message escalation as it was already processed earlier.");
				return false;
			}
			if (!ObjectClass.IsMessage(argsImpl.MessageClass, false) && !ObjectClass.IsMeetingMessage(argsImpl.MessageClass) && !ObjectClass.IsMeetingMessageSeries(argsImpl.MessageClass))
			{
				GroupEscalationAgent.Tracer.TraceDebug<string>((long)this.GetHashCode(), "GroupEscalationAgent.ShouldProcessMessage: ignoring messages that are not messages nor meeting messages. Message class {0}", argsImpl.MessageClass);
				return false;
			}
			if (ObjectClass.IsMeetingForwardNotification(argsImpl.MessageClass) || ObjectClass.IsMeetingForwardNotificationSeries(argsImpl.MessageClass))
			{
				GroupEscalationAgent.Tracer.TraceDebug((long)this.GetHashCode(), "GroupEscalationAgent.ShouldProcessMessage: item class is meeting forward notification. Do not process.");
				return false;
			}
			if (ObjectClass.IsMeetingResponse(argsImpl.MessageClass) || ObjectClass.IsMeetingResponseSeries(argsImpl.MessageClass))
			{
				GroupEscalationAgent.Tracer.TraceDebug((long)this.GetHashCode(), "GroupEscalationAgent.ShouldProcessMessage: item class is meeting response. Do not process.");
				return false;
			}
			if (GroupEscalationAgent.IsEHAMigrationMeetingMessage(argsImpl.MailItem))
			{
				GroupEscalationAgent.Tracer.TraceDebug((long)this.GetHashCode(), "GroupEscalationAgent.ShouldProcessMessage: ignoring EHA migration messages.");
				return false;
			}
			if (GroupEscalationAgent.IsRepairUpdateMessage(argsImpl.MessageItem))
			{
				GroupEscalationAgent.Tracer.TraceDebug((long)this.GetHashCode(), "GroupEscalationAgent.ShouldProcessMessage: ignoring RUM messages.");
				return false;
			}
			if (GroupEscalationAgent.IsOofOrDsnMessage(argsImpl.MessageClass))
			{
				GroupEscalationAgent.Tracer.TraceDebug((long)this.GetHashCode(), "GroupEscalationAgent.ShouldProcessMessage: ignoring OOF or DSN messages.");
				return false;
			}
			return true;
		}

		// Token: 0x04000255 RID: 597
		private static readonly Trace Tracer = ExTraceGlobals.GroupEscalationAgentTracer;

		// Token: 0x04000256 RID: 598
		private static readonly SmtpResponse IrmMessageDeliveryToGroupMailBoxError = new SmtpResponse("550", "5.7.1", new string[]
		{
			"GroupEscalationAgent; Message delivery failed due to IRM message sent to group mailbox"
		});

		// Token: 0x04000257 RID: 599
		private static readonly SmtpResponse EscalationFailedTransientError = new SmtpResponse("432", "4.3.2", new string[]
		{
			"GroupEscalationAgent; Escalation failed due to a transient error"
		});

		// Token: 0x04000258 RID: 600
		private static readonly SmtpResponse EscalationFailedPermanentError = new SmtpResponse("550", "5.7.1", new string[]
		{
			"GroupEscalationAgent; Escalation failed due to a permanent error"
		});

		// Token: 0x04000259 RID: 601
		private readonly ProcessedMessageTracker processedMessages;
	}
}
