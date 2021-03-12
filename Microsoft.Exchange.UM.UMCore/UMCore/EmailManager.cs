using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.MessageContent;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200012B RID: 299
	internal class EmailManager : SendMessageManager
	{
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x00022EAD File Offset: 0x000210AD
		public bool MessageListIsNull
		{
			get
			{
				return this.messageItemList == null;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x00022EB8 File Offset: 0x000210B8
		// (set) Token: 0x0600084E RID: 2126 RVA: 0x00022EC0 File Offset: 0x000210C0
		internal NameOrNumberOfCaller SpecifiedCallerDetails { get; private set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00022EC9 File Offset: 0x000210C9
		internal override bool LargeGrammarsNeeded
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00022ECC File Offset: 0x000210CC
		internal EmailManager(ActivityManager manager, EmailManager.ConfigClass config) : base(manager, config)
		{
			this.hiddenThreads = new Dictionary<EmailManager.Conversation, object>();
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000851 RID: 2129 RVA: 0x00022EE1 File Offset: 0x000210E1
		// (set) Token: 0x06000852 RID: 2130 RVA: 0x00022EE9 File Offset: 0x000210E9
		internal bool IsRecurringMeetingRequest
		{
			get
			{
				return this.isRecurringMeetingRequest;
			}
			set
			{
				this.isRecurringMeetingRequest = value;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x00022EF2 File Offset: 0x000210F2
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x00022EFA File Offset: 0x000210FA
		internal bool IsSenderRoutable
		{
			get
			{
				return this.isSenderRoutable;
			}
			set
			{
				this.isSenderRoutable = value;
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00022F03 File Offset: 0x00021103
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x00022F0B File Offset: 0x0002110B
		protected ContactInfo SenderInfo
		{
			get
			{
				return this.senderInfo;
			}
			set
			{
				this.senderInfo = value;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x00022F14 File Offset: 0x00021114
		private StoreObjectId InboxId
		{
			get
			{
				byte[] entryId = Convert.FromBase64String(this.user.ConfigFolder.TelephoneAccessFolderEmail);
				return StoreObjectId.FromProviderSpecificId(entryId);
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00022F40 File Offset: 0x00021140
		internal static bool CanReadMessageClassWithTui(string itemClass)
		{
			bool flag = true;
			flag &= (!XsoUtil.IsPureVoice(itemClass) && !XsoUtil.IsProtectedVoicemail(itemClass));
			if (!flag)
			{
				return flag;
			}
			return flag & (itemClass.StartsWith("IPM.Note", true, CultureInfo.InvariantCulture) || itemClass.StartsWith("IPM.Schedule.Meeting", true, CultureInfo.InvariantCulture));
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00022F98 File Offset: 0x00021198
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			vo.IncrementCounter(SubscriberAccessCounters.EmailMessageQueueAccessed);
			this.user = vo.CurrentCallContext.CallerInfo;
			this.folderId = this.InboxId;
			this.messageItemList = new MessageItemList(this.user, this.folderId, MessageItemListSortType.Email, EmailManager.viewProperties);
			this.SpecifiedCallerDetails = new NameOrNumberOfCaller(NameOrNumberOfCaller.TypeOfVoiceCall.MissedCall);
			base.Start(vo, refInfo);
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00023000 File Offset: 0x00021200
		internal override TransitionBase ExecuteAction(string action, BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::ExecuteAction action={0}.", new object[]
			{
				action
			});
			string input = null;
			if (string.Equals(action, "nextUnreadMessage", StringComparison.OrdinalIgnoreCase))
			{
				input = this.NextUnreadMessage();
			}
			else if (string.Equals(action, "previousMessage", StringComparison.OrdinalIgnoreCase))
			{
				input = this.PreviousMessage();
			}
			else if (string.Equals(action, "acceptMeeting", StringComparison.OrdinalIgnoreCase))
			{
				base.SendMsg = new EmailManager.MeetingResponse(vo, this.messageItemList.CurrentStoreObjectId, ResponseType.Accept, this.user, this);
				base.SendMsg.DoSubmit(Importance.Normal);
				base.SendMsg = null;
			}
			else if (string.Equals(action, "acceptMeetingTentative", StringComparison.OrdinalIgnoreCase))
			{
				base.SendMsg = new EmailManager.MeetingResponse(vo, this.messageItemList.CurrentStoreObjectId, ResponseType.Tentative, this.user, this);
				base.SendMsg.DoSubmit(Importance.Normal);
				base.SendMsg = null;
			}
			else if (string.Equals(action, "declineMeeting", StringComparison.OrdinalIgnoreCase))
			{
				base.SendMsg = new EmailManager.MeetingResponse(vo, this.messageItemList.CurrentStoreObjectId, ResponseType.Decline, this.user, this);
				base.WriteReplyIntroType(IntroType.Decline);
			}
			else if (string.Equals(action, "reply", StringComparison.OrdinalIgnoreCase))
			{
				base.SendMsg = new EmailManager.EmailReply(vo, this.messageItemList.CurrentStoreObjectId, this.user, this);
				base.WriteReplyIntroType(IntroType.Reply);
			}
			else if (string.Equals(action, "replyAll", StringComparison.OrdinalIgnoreCase))
			{
				base.SendMsg = new EmailManager.EmailReplyAll(vo, this.messageItemList.CurrentStoreObjectId, this.user, this);
				base.WriteReplyIntroType(IntroType.ReplyAll);
			}
			else if (string.Equals(action, "forward", StringComparison.OrdinalIgnoreCase))
			{
				base.SendMsg = new EmailManager.EmailForward(vo, this.messageItemList.CurrentStoreObjectId, this.user, this);
				base.WriteReplyIntroType(IntroType.Forward);
			}
			else if (string.Equals(action, "deleteMessage", StringComparison.OrdinalIgnoreCase))
			{
				this.DeleteCurrentMessage(true);
			}
			else if (string.Equals(action, "undeleteMessage", StringComparison.OrdinalIgnoreCase))
			{
				this.Undelete();
			}
			else if (string.Equals(action, "flagMessage", StringComparison.OrdinalIgnoreCase))
			{
				this.FlagMessage();
			}
			else if (string.Equals(action, "saveMessage", StringComparison.OrdinalIgnoreCase))
			{
				this.MarkRead();
			}
			else if (string.Equals(action, "markUnread", StringComparison.OrdinalIgnoreCase))
			{
				this.MarkUnread();
			}
			else if (string.Equals(action, "hideThread", StringComparison.OrdinalIgnoreCase))
			{
				this.HideThread();
			}
			else if (string.Equals(action, "deleteThread", StringComparison.OrdinalIgnoreCase))
			{
				this.DeleteThread();
			}
			else if (string.Equals(action, "findByName", StringComparison.OrdinalIgnoreCase))
			{
				this.FindByName();
			}
			else
			{
				if (string.Equals(action, "commitPendingDeletions", StringComparison.OrdinalIgnoreCase))
				{
					if (this.pendingDeletion == null)
					{
						goto IL_301;
					}
					using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
					{
						this.pendingDeletion.Commit(mailboxSessionLock.Session);
						this.pendingDeletion = null;
						goto IL_301;
					}
				}
				if (string.Equals(action, "selectLanguage", StringComparison.OrdinalIgnoreCase))
				{
					input = base.SelectLanguage();
				}
				else
				{
					if (!string.Equals(action, "nextLanguage", StringComparison.OrdinalIgnoreCase))
					{
						return base.ExecuteAction(action, vo);
					}
					input = base.NextLanguage(vo);
				}
			}
			IL_301:
			return base.CurrentActivity.GetTransition(input);
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x0002332C File Offset: 0x0002152C
		internal StoreObjectId GetCurrentItemId()
		{
			return this.messageItemList.CurrentStoreObjectId;
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x00023339 File Offset: 0x00021539
		internal string NextMessage(BaseUMCallSession vo)
		{
			return this.NextMessage(false);
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x00023344 File Offset: 0x00021544
		internal void DeleteCurrentMessage(bool allowUndelete)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "DeleteCurrentMessage with allowUndelete={0}.", new object[]
			{
				allowUndelete
			});
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
			{
				StoreObjectId currentStoreObjectId = this.messageItemList.CurrentStoreObjectId;
				string itemClass = this.messageItemList.SafeGetProperty<string>(StoreObjectSchema.ItemClass, string.Empty);
				if (ObjectClass.IsMeetingCancellation(itemClass))
				{
					XsoUtil.RemoveFromCalendar(this.messageItemList.CurrentStoreObjectId, mailboxSessionLock.Session);
				}
				if (this.pendingDeletion != null)
				{
					this.pendingDeletion.Commit(mailboxSessionLock.Session);
					this.pendingDeletion = null;
				}
				if (!allowUndelete)
				{
					mailboxSessionLock.Session.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
					{
						this.messageItemList.CurrentStoreObjectId
					});
				}
				else
				{
					this.pendingDeletion = new EmailManager.SingleItemPendingDeletion(base.CallSession, this.messageItemList);
					if (this.numDeletions < 2U && (this.numDeletions += 1U) >= 2U)
					{
						base.WriteVariable("playedUndelete", true);
					}
				}
				base.WriteVariable("canUndelete", this.pendingDeletion != null);
				this.messageItemList.Ignore(currentStoreObjectId);
			}
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x00023494 File Offset: 0x00021694
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::Dispose.", new object[0]);
					if (this.findByNameResults != null)
					{
						this.findByNameResults.Dispose();
					}
					if (this.pendingDeletion != null && this.user != null)
					{
						using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
						{
							if (mailboxSessionLock.Session != null)
							{
								this.pendingDeletion.Commit(mailboxSessionLock.Session);
								this.pendingDeletion = null;
							}
						}
					}
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x0600085F RID: 2143 RVA: 0x0002353C File Offset: 0x0002173C
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<EmailManager>(this);
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00023544 File Offset: 0x00021744
		private static void GetToAndCCFields(MessageItem emailMsg, out string addressToLine, out string addressCcLine)
		{
			XsoUtil.BuildParticipantStrings(emailMsg.Recipients, out addressToLine, out addressCcLine);
			addressToLine = ((!string.IsNullOrEmpty(addressToLine)) ? addressToLine : null);
			addressCcLine = ((!string.IsNullOrEmpty(addressCcLine)) ? addressCcLine : null);
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x00023574 File Offset: 0x00021774
		private static List<StoreObjectId> GetConversations(MailboxSession session, StoreObjectId folderId, EmailManager.Conversation targetConversation)
		{
			List<StoreObjectId> result;
			using (Folder folder = Folder.Bind(session, folderId))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, null, new PropertyDefinition[]
				{
					ItemSchema.Id,
					ItemSchema.ConversationIndex
				}))
				{
					List<StoreObjectId> list = new List<StoreObjectId>();
					object[][] rows = queryResult.GetRows(MessageItemList.PageSize);
					while (rows != null && 0 < rows.Length)
					{
						foreach (object[] array2 in rows)
						{
							StoreObjectId objectId = ((VersionedId)array2[0]).ObjectId;
							EmailManager.Conversation other = EmailManager.Conversation.FromConversationIndex(array2[1] as byte[]);
							if (targetConversation.Equals(other))
							{
								list.Add(objectId);
							}
						}
						rows = queryResult.GetRows(MessageItemList.PageSize);
					}
					result = list;
				}
			}
			return result;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x00023660 File Offset: 0x00021860
		private static ExDateTime CalculateNextOccurenceEndWindow(ExDateTime startTime, Recurrence r)
		{
			ExDateTime result;
			try
			{
				if (r.Pattern is DailyRecurrencePattern)
				{
					DailyRecurrencePattern dailyRecurrencePattern = r.Pattern as DailyRecurrencePattern;
					result = startTime.AddDays((double)(2 * dailyRecurrencePattern.RecurrenceInterval));
				}
				else if (r.Pattern is WeeklyRecurrencePattern)
				{
					WeeklyRecurrencePattern weeklyRecurrencePattern = r.Pattern as WeeklyRecurrencePattern;
					result = startTime.AddDays((double)(14 * weeklyRecurrencePattern.RecurrenceInterval));
				}
				else if (r.Pattern is MonthlyRecurrencePattern || r.Pattern is MonthlyThRecurrencePattern)
				{
					MonthlyRecurrencePattern monthlyRecurrencePattern = r.Pattern as MonthlyRecurrencePattern;
					MonthlyThRecurrencePattern monthlyThRecurrencePattern = r.Pattern as MonthlyThRecurrencePattern;
					int num = (monthlyRecurrencePattern != null) ? monthlyRecurrencePattern.RecurrenceInterval : monthlyThRecurrencePattern.RecurrenceInterval;
					result = startTime.AddMinutes((double)(2 * num));
				}
				else if (r.Pattern is YearlyRecurrencePattern || r.Pattern is YearlyThRecurrencePattern)
				{
					result = startTime.AddYears(2);
				}
				else
				{
					result = ExDateTime.MaxValue;
				}
			}
			catch (ArgumentException)
			{
				result = ExDateTime.MaxValue;
			}
			return result;
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x00023770 File Offset: 0x00021970
		private static CalendarItemBase GetCalendarItem(MeetingRequest msgReq)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, null, "EmailManager::GetCorrelatedItem.", new object[0]);
			CalendarItemBase calendarItemBase = msgReq.IsOrganizer() ? msgReq.GetCorrelatedItem() : msgReq.UpdateCalendarItem(false);
			if (calendarItemBase == null)
			{
				throw new ObjectNotFoundException(LocalizedString.Empty);
			}
			return calendarItemBase;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x000237BC File Offset: 0x000219BC
		private static bool ValidateMessage(MessageItem emailMsg)
		{
			if (null == emailMsg.From || string.IsNullOrEmpty(emailMsg.From.EmailAddress))
			{
				if (XsoUtil.IsMissedCall(emailMsg.ClassName))
				{
					return true;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, null, "Skipping message, FROM field is missing or there is no email address", new object[0]);
				return false;
			}
			else
			{
				MeetingRequest meetingRequest = emailMsg as MeetingRequest;
				if (meetingRequest != null && meetingRequest.IsOutOfDate())
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, null, "Skipping out of date meeting request.", new object[0]);
					return false;
				}
				return true;
			}
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0002383B File Offset: 0x00021A3B
		private string NextUnreadMessage()
		{
			return this.NextMessage(true);
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00023844 File Offset: 0x00021A44
		private string SeekToMessage(StoreObjectId targetObjectId)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::SeekToMessage({0}).", new object[]
			{
				targetObjectId
			});
			int currentOffset = this.messageItemList.CurrentOffset;
			this.messageItemList.Seek(targetObjectId);
			string result;
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
			{
				MessageItem messageItem = null;
				try
				{
					if (!this.FetchMessage(mailboxSessionLock.Session, out messageItem))
					{
						throw new InvalidOperationException();
					}
					result = this.InitializeMessage(true, currentOffset, messageItem);
				}
				finally
				{
					if (messageItem != null)
					{
						messageItem.Dispose();
					}
				}
			}
			return result;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000238EC File Offset: 0x00021AEC
		private string NextMessage(bool unreadOnly)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::NextMessage", new object[0]);
			int currentOffset = this.messageItemList.CurrentOffset;
			string result;
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
			{
				MessageItem messageItem = null;
				try
				{
					while (this.messageItemList.Next(unreadOnly) && !this.FetchMessage(mailboxSessionLock.Session, unreadOnly, out messageItem))
					{
					}
					result = this.InitializeMessage(true, currentOffset, messageItem);
				}
				finally
				{
					if (messageItem != null)
					{
						messageItem.Dispose();
					}
				}
			}
			return result;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00023988 File Offset: 0x00021B88
		private string PreviousMessage()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::PreviousMessage", new object[0]);
			int currentOffset = this.messageItemList.CurrentOffset;
			string result;
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
			{
				MessageItem messageItem = null;
				try
				{
					while (this.messageItemList.Previous() && !this.FetchMessage(mailboxSessionLock.Session, out messageItem))
					{
					}
					result = this.InitializeMessage(false, currentOffset, messageItem);
				}
				finally
				{
					if (messageItem != null)
					{
						messageItem.Dispose();
					}
				}
			}
			return result;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00023A24 File Offset: 0x00021C24
		private string InitializeMessage(bool movingForward, int currentOffset, MessageItem emailMsg)
		{
			string result = null;
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::InitializeMessage.", new object[0]);
			if (emailMsg == null)
			{
				this.messageItemList.Seek(currentOffset);
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "No more messages in the inbox.", new object[0]);
				result = (movingForward ? "endOfMessages" : "noPreviousMessages");
			}
			else
			{
				ContactInfo contactInfo = null;
				if (emailMsg.From != null && emailMsg.From.EmailAddress != null)
				{
					contactInfo = ContactInfo.FindByParticipant(this.user, emailMsg.From);
				}
				this.SenderInfo = ((contactInfo != null) ? contactInfo : new DefaultContactInfo());
				this.WriteMessageVariables(emailMsg);
				base.MessagePlayerContext.Reset(emailMsg.Id.ObjectId);
				base.CallSession.IncrementCounter(SubscriberAccessCounters.EmailMessagesHeard);
			}
			return result;
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00023AEE File Offset: 0x00021CEE
		private bool FetchMessage(MailboxSession session, out MessageItem emailMsg)
		{
			return this.FetchMessage(session, false, out emailMsg);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00023AFC File Offset: 0x00021CFC
		private bool FetchMessage(MailboxSession session, bool unreadOnly, out MessageItem emailMsg)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::FetchMessage.", new object[0]);
			emailMsg = null;
			string text = this.messageItemList.SafeGetProperty<string>(StoreObjectSchema.ItemClass, string.Empty);
			byte[] array = this.messageItemList.SafeGetProperty<byte[]>(ItemSchema.ConversationIndex, null);
			bool flag = this.messageItemList.SafeGetProperty<bool>(MessageItemSchema.IsRead, false);
			if (text == null || !EmailManager.CanReadMessageClassWithTui(text))
			{
				return false;
			}
			if (array != null && this.hiddenThreads.ContainsKey(EmailManager.Conversation.FromConversationIndex(array)))
			{
				return false;
			}
			if (unreadOnly && flag)
			{
				return false;
			}
			StoreObjectId currentStoreObjectId = this.messageItemList.CurrentStoreObjectId;
			try
			{
				emailMsg = MessageItem.Bind(session, currentStoreObjectId, new PropertyDefinition[]
				{
					ItemSchema.ReceivedTime,
					ItemSchema.Subject,
					MessageItemSchema.IsRead,
					ItemSchema.Importance,
					CalendarItemInstanceSchema.StartTime,
					MeetingMessageInstanceSchema.IsProcessed,
					ItemSchema.NormalizedSubject,
					ItemSchema.FlagStatus,
					ItemSchema.FlagRequest,
					MessageItemSchema.VoiceMessageSenderName,
					MessageItemSchema.SenderTelephoneNumber
				});
				if (!EmailManager.ValidateMessage(emailMsg))
				{
					emailMsg.Dispose();
					emailMsg = null;
					return false;
				}
				emailMsg.OpenAsReadWrite();
			}
			catch (ObjectNotFoundException)
			{
				CallIdTracer.TraceWarning(ExTraceGlobals.EmailTracer, this, "EmailManager caught ObjectNotFoundException.", new object[0]);
				emailMsg = null;
				this.messageItemList.Ignore(currentStoreObjectId);
				return false;
			}
			if (this.firstMessageId == null && emailMsg != null)
			{
				this.firstMessageId = emailMsg.Id.ObjectId;
			}
			return emailMsg != null;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00023C94 File Offset: 0x00021E94
		private void WriteMessageVariables(MessageItem emailMsg)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::WriteMessageVariables.", new object[0]);
			this.WriteMeetingRequestVariables(emailMsg);
			this.WriteMeetingCancellationVariables(emailMsg);
			this.WriteCommonMessageVariables(emailMsg);
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00023CC4 File Offset: 0x00021EC4
		private void WriteCommonMessageVariables(MessageItem emailMsg)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::WriteCommonMessageVariables.", new object[0]);
			base.WriteVariable("emailReceivedTime", emailMsg.ReceivedTime);
			base.WriteVariable("emailSubject", emailMsg.Subject);
			if (ObjectClass.IsOfClass(emailMsg.ClassName, "IPM.Schedule.Meeting.Resp") || ObjectClass.IsOfClass(emailMsg.ClassName, "IPM.Note.Rules.OofTemplate.Microsoft") || !string.IsNullOrEmpty(emailMsg.VotingInfo.Response))
			{
				base.WriteVariable("normalizedSubject", Utils.TrimSpaces(emailMsg.Subject));
			}
			else
			{
				base.WriteVariable("normalizedSubject", Utils.TrimSpaces((string)XsoUtil.SafeGetProperty(emailMsg, ItemSchema.NormalizedSubject, string.Empty)));
			}
			base.WriteReplyIntroType(IntroType.None);
			base.WriteVariable("read", emailMsg.IsRead);
			bool flag = emailMsg.IsRestricted || ObjectClass.IsOfClass(emailMsg.ClassName, "IPM.Note.Secure") || ObjectClass.IsOfClass(emailMsg.ClassName, "IPM.Note.SMIME");
			base.WriteVariable("drm", flag);
			base.WriteVariable("urgent", Importance.High == emailMsg.Importance);
			base.WriteVariable("attachments", 0 != emailMsg.AttachmentCollection.Count);
			base.WriteVariable("receivedDayOfWeek", (int)emailMsg.ReceivedTime.DayOfWeek);
			base.WriteVariable("receivedOffset", (int)(this.user.Now.Date - emailMsg.ReceivedTime.Date).TotalDays);
			base.WriteVariable("firstMessage", emailMsg.Id.ObjectId.Equals(this.firstMessageId));
			string varValue = null;
			string varValue2 = null;
			EmailManager.GetToAndCCFields(emailMsg, out varValue, out varValue2);
			base.WriteVariable("emailToField", varValue);
			base.WriteVariable("emailCCField", varValue2);
			base.WriteVariable("isRecorded", XsoUtil.IsMixedVoice(emailMsg.ClassName));
			PhoneNumber senderTelephoneNumber = XsoUtil.GetSenderTelephoneNumber(emailMsg);
			PhoneNumber phoneNumber = RetrieveVoicemailManager.ApplyDialingRules(this.user, senderTelephoneNumber, this.SenderInfo.DialPlan);
			base.TargetPhoneNumber = (phoneNumber ?? Util.GetNumberToDial(this.user, this.SenderInfo));
			if (XsoUtil.IsMissedCall(emailMsg.ClassName))
			{
				this.SpecifiedCallerDetails.ClearProperties();
				base.WriteVariable("isMissedCall", true);
				base.WriteVariable("senderCallerID", senderTelephoneNumber);
				this.SpecifiedCallerDetails.CallerId = senderTelephoneNumber;
				string senderName;
				if (this.SenderInfo is DefaultContactInfo)
				{
					senderName = (string)XsoUtil.SafeGetProperty(emailMsg, MessageItemSchema.VoiceMessageSenderName, string.Empty);
				}
				else
				{
					senderName = this.SenderInfo.DisplayName;
				}
				this.SetSenderName(this.SenderInfo.ADOrgPerson, senderName);
				this.SpecifiedCallerDetails.EmailSender = this.ReadVariable("emailSender");
			}
			else
			{
				base.WriteVariable("isMissedCall", false);
				base.WriteVariable("senderCallerID", null);
				this.SetSenderName(emailMsg.From);
			}
			ReplyForwardType replyForwardType = GlobCfg.SubjectToReplyForwardType(emailMsg.Subject);
			base.WriteVariable("isReply", replyForwardType == ReplyForwardType.Reply);
			base.WriteVariable("isForward", replyForwardType == ReplyForwardType.Forward);
			base.WriteVariable("messageLanguage", null);
			base.WriteVariable("languageDetected", null);
			base.WriteVariable("isHighPriority", Importance.High == emailMsg.Importance);
			this.IsSenderRoutable = (((null == emailMsg.From) ? new bool?(false) : emailMsg.From.IsRoutable(emailMsg.Session)) ?? false);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00024098 File Offset: 0x00022298
		private void WriteMeetingRequestVariables(MessageItem emailMsg)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::WriteMeetingRequestVariables.", new object[0]);
			MeetingRequest meetingRequest = emailMsg as MeetingRequest;
			bool flag = meetingRequest != null && !meetingRequest.IsDelegated();
			base.WriteVariable("meetingRequest", flag);
			if (!flag)
			{
				base.WriteVariable("outOfDate", null);
				base.WriteVariable("meetingOver", null);
				base.WriteVariable("alreadyAccepted", null);
				base.WriteVariable("emailReplyTime", null);
				base.WriteVariable("emailRequestTime", null);
				base.WriteVariable("emailRequestTimeRange", null);
				base.WriteVariable("owner", null);
				base.WriteVariable("location", null);
				base.WriteVariable("calendarStatus", string.Empty);
				base.WriteVariable("meetingDayOfWeek", 0);
				base.WriteVariable("meetingOffset", 0);
				return;
			}
			using (CalendarItemBase calendarItem = EmailManager.GetCalendarItem(meetingRequest))
			{
				using (CalendarItemBase firstFutureInstance = this.GetFirstFutureInstance(calendarItem))
				{
					this.SetConflictStatus(firstFutureInstance);
					base.WriteVariable("outOfDate", meetingRequest.IsOutOfDate());
					base.WriteVariable("meetingOver", firstFutureInstance.EndTime < this.user.Now);
					base.WriteVariable("emailRequestTime", firstFutureInstance.StartTime);
					base.WriteVariable("emailRequestTimeRange", new TimeRange(firstFutureInstance.StartTime, firstFutureInstance.EndTime));
					base.WriteVariable("owner", calendarItem.IsOrganizer());
					base.WriteVariable("location", firstFutureInstance.Location);
					base.WriteVariable("meetingDayOfWeek", (int)firstFutureInstance.StartTime.DayOfWeek);
					base.WriteVariable("meetingOffset", (int)(firstFutureInstance.StartTime.Date - this.user.Now.Date).TotalDays);
					CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "MeetingRequestType={0}.", new object[]
					{
						meetingRequest.MeetingRequestType
					});
					if (MeetingMessageType.InformationalUpdate == meetingRequest.MeetingRequestType)
					{
						object obj = (calendarItem == null) ? null : XsoUtil.SafeGetProperty(calendarItem, CalendarItemBaseSchema.AppointmentReplyTime, null);
						CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "ReplyTime={0}.", new object[]
						{
							obj
						});
						if (obj == null || !(obj is ExDateTime))
						{
							base.WriteVariable("alreadyAccepted", null);
							base.WriteVariable("emailReplyTime", null);
						}
						else
						{
							base.WriteVariable("alreadyAccepted", true);
							base.WriteVariable("emailReplyTime", obj);
						}
					}
				}
			}
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00024384 File Offset: 0x00022584
		private CalendarItemBase GetFirstFutureInstance(CalendarItemBase potentialMaster)
		{
			CalendarItemBase result = potentialMaster;
			CalendarItem calendarItem = potentialMaster as CalendarItem;
			if (calendarItem == null || calendarItem.Recurrence == null)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::GetFirstFutureInstance was passed a non-recurring meeting", new object[0]);
				this.isRecurringMeetingRequest = false;
			}
			else
			{
				this.isRecurringMeetingRequest = true;
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::GetFirstFutureInstance was passed a recurring meeting", new object[0]);
				ExDateTime now = this.user.Now;
				ExDateTime endView = EmailManager.CalculateNextOccurenceEndWindow(this.user.Now, calendarItem.Recurrence);
				IList<OccurrenceInfo> occurrenceInfoList = calendarItem.Recurrence.GetOccurrenceInfoList(now, endView);
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
				{
					foreach (OccurrenceInfo occurrenceInfo in occurrenceInfoList)
					{
						if (occurrenceInfo != null && occurrenceInfo.VersionedId != null && occurrenceInfo.StartTime >= this.user.Now)
						{
							result = CalendarItemBase.Bind(mailboxSessionLock.Session, occurrenceInfo.VersionedId);
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x000244B8 File Offset: 0x000226B8
		private void WriteMeetingCancellationVariables(MessageItem emailMsg)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::WriteMeetingCancellationVariables.", new object[0]);
			MeetingCancellation meetingCancellation = emailMsg as MeetingCancellation;
			bool flag = null != meetingCancellation;
			base.WriteVariable("meetingCancellation", flag);
			if (!flag)
			{
				return;
			}
			base.WriteVariable("emailRequestTime", emailMsg[CalendarItemInstanceSchema.StartTime]);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00024518 File Offset: 0x00022718
		private void SetConflictStatus(CalendarItemBase calendarItem)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::SetConflictStatus.", new object[0]);
			BusyType busyType = BusyType.Free;
			base.WriteVariable("calendarStatus", "free");
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
			{
				using (CalendarFolder calendarFolder = CalendarFolder.Bind(mailboxSessionLock.Session, mailboxSessionLock.Session.GetDefaultFolderId(DefaultFolderType.Calendar)))
				{
					AdjacencyOrConflictInfo[] adjacentOrConflictingItems = calendarFolder.GetAdjacentOrConflictingItems(calendarItem);
					foreach (AdjacencyOrConflictInfo adjacencyOrConflictInfo in adjacentOrConflictingItems)
					{
						if ((adjacencyOrConflictInfo.AdjacencyOrConflictType & AdjacencyOrConflictType.Conflicts) != (AdjacencyOrConflictType)0 && adjacencyOrConflictInfo.FreeBusyStatus != BusyType.Free)
						{
							if (adjacencyOrConflictInfo.FreeBusyStatus == BusyType.Tentative && busyType == BusyType.Free)
							{
								busyType = BusyType.Tentative;
								base.WriteVariable("calendarStatus", "tentative");
							}
							else if (adjacencyOrConflictInfo.FreeBusyStatus == BusyType.Busy && busyType != BusyType.OOF)
							{
								busyType = BusyType.Busy;
								base.WriteVariable("calendarStatus", "busy");
							}
							else if (adjacencyOrConflictInfo.FreeBusyStatus == BusyType.OOF)
							{
								busyType = BusyType.OOF;
								base.WriteVariable("calendarStatus", "oof");
								break;
							}
						}
					}
					CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "SetConflictStatus found mostBusyType={0}.", new object[]
					{
						busyType
					});
					base.WriteVariable("free", busyType == BusyType.Free);
				}
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x00024680 File Offset: 0x00022880
		private void SetSenderName(Participant p)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::SetSenderName.", new object[0]);
			IADRecipientLookup iadrecipientLookup = ADRecipientLookupFactory.CreateFromUmUser(this.user);
			ADRecipient recipient = iadrecipientLookup.LookupByParticipant(p);
			this.SetSenderName(recipient, p.DisplayName);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x000246C4 File Offset: 0x000228C4
		private void SetSenderName(IADRecipient recipient, string senderName)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::SetSenderName.", new object[0]);
			if (recipient == null)
			{
				base.WriteVariable("emailSender", senderName);
				return;
			}
			base.SetRecordedName("emailSender", recipient);
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x000246F8 File Offset: 0x000228F8
		private void Undelete()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::Undelete with pendingDelete={0}.", new object[]
			{
				this.pendingDeletion
			});
			if (this.pendingDeletion != null)
			{
				base.WriteVariable("undeletedAConversation", this.pendingDeletion is EmailManager.ConversationPendingDeletion);
				this.pendingDeletion.Revert();
				this.SeekToMessage(this.pendingDeletion.UndeleteRefId);
				this.pendingDeletion = null;
			}
			base.WriteVariable("canUndelete", false);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00024784 File Offset: 0x00022984
		private void FlagMessage()
		{
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
			{
				using (MessageItem messageItem = Item.BindAsMessage(mailboxSessionLock.Session, this.messageItemList.CurrentStoreObjectId))
				{
					messageItem.OpenAsReadWrite();
					messageItem.SetFlag(Strings.FollowUp.ToString(this.user.TelephonyCulture), null, null);
					ConflictResolutionResult conflictResolutionResult = messageItem.Save(SaveMode.ResolveConflicts);
					if (SaveResult.IrresolvableConflict == conflictResolutionResult.SaveStatus)
					{
						throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(this.messageItemList.CurrentStoreObjectId), conflictResolutionResult);
					}
				}
			}
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00024848 File Offset: 0x00022A48
		private void MarkRead()
		{
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
			{
				using (MessageItem messageItem = Item.BindAsMessage(mailboxSessionLock.Session, this.messageItemList.CurrentStoreObjectId))
				{
					if (!messageItem.IsRead)
					{
						messageItem.OpenAsReadWrite();
						messageItem.MarkAsRead(false, true);
						ConflictResolutionResult conflictResolutionResult = messageItem.Save(SaveMode.ResolveConflicts);
						if (SaveResult.IrresolvableConflict == conflictResolutionResult.SaveStatus)
						{
							throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(this.messageItemList.CurrentStoreObjectId), conflictResolutionResult);
						}
					}
				}
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x000248EC File Offset: 0x00022AEC
		private void MarkUnread()
		{
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
			{
				using (MessageItem messageItem = Item.BindAsMessage(mailboxSessionLock.Session, this.messageItemList.CurrentStoreObjectId))
				{
					if (messageItem.IsRead)
					{
						messageItem.OpenAsReadWrite();
						messageItem.MarkAsUnread(true);
						ConflictResolutionResult conflictResolutionResult = messageItem.Save(SaveMode.ResolveConflicts);
						if (SaveResult.IrresolvableConflict == conflictResolutionResult.SaveStatus)
						{
							throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(this.messageItemList.CurrentStoreObjectId), conflictResolutionResult);
						}
					}
				}
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00024990 File Offset: 0x00022B90
		private void HideThread()
		{
			byte[] indexBytes = this.messageItemList.SafeGetProperty<byte[]>(ItemSchema.ConversationIndex, null);
			EmailManager.Conversation conversation = EmailManager.Conversation.FromConversationIndex(indexBytes);
			this.hiddenThreads.Add(conversation, null);
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::HideThread has hidden all messages with conversation idx={0}.", new object[]
			{
				conversation.GetHashCode()
			});
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x000249EC File Offset: 0x00022BEC
		private void DeleteThread()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::DeleteThread.", new object[0]);
			if (this.pendingDeletion != null)
			{
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.user.CreateSessionLock())
				{
					this.pendingDeletion.Commit(mailboxSessionLock.Session);
					this.pendingDeletion = null;
				}
			}
			byte[] array = this.messageItemList.SafeGetProperty<byte[]>(ItemSchema.ConversationIndex, null);
			string text = this.messageItemList.SafeGetProperty<string>(ItemSchema.ConversationTopic, null);
			if (array == null || array.Length < 22 || string.IsNullOrEmpty(text))
			{
				this.pendingDeletion = new EmailManager.SingleItemPendingDeletion(base.CallSession, this.messageItemList);
			}
			else
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::DeleteThread finding all messages with topic={0}.", new object[]
				{
					text
				});
				QueryFilter filter = new TextFilter(ItemSchema.ConversationTopic, text, MatchOptions.ExactPhrase, MatchFlags.Default);
				List<StoreObjectId> conversations;
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock2 = this.user.CreateSessionLock())
				{
					using (OneTimeSearch oneTimeSearch = OneTimeSearch.Execute(this.user, mailboxSessionLock2.Session, this.InboxId, filter))
					{
						conversations = EmailManager.GetConversations(mailboxSessionLock2.Session, oneTimeSearch.FolderId, EmailManager.Conversation.FromConversationIndex(array));
					}
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::DeleteThread found c={0} such messages.", new object[]
				{
					(conversations == null) ? 0 : conversations.Count
				});
				EmailManager.Conversation conversation = EmailManager.Conversation.FromConversationIndex(array);
				this.hiddenThreads.Add(conversation, null);
				this.pendingDeletion = new EmailManager.ConversationPendingDeletion(base.CallSession, this.messageItemList.CurrentStoreObjectId, this.hiddenThreads, conversation, conversations.ToArray());
			}
			if (this.numDeletions < 2U && (this.numDeletions += 1U) >= 2U)
			{
				base.WriteVariable("playedUndelete", true);
			}
			base.WriteVariable("canUndelete", this.pendingDeletion != null);
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00024C0C File Offset: 0x00022E0C
		private void FindByName()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::FindByName.", new object[0]);
			OneTimeSearch oneTimeSearch = null;
			QueryResult queryResult = null;
			UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = null;
			Folder folder = null;
			try
			{
				ContactSearchItem contactSearchItem = (ContactSearchItem)this.ReadVariable("directorySearchResult");
				if (contactSearchItem == null)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::FindByName received a null search result!", new object[0]);
					base.WriteVariable("numMessagesFromName", 0);
					base.WriteVariable("findByName", string.Empty);
				}
				else
				{
					if (contactSearchItem.Recipient == null)
					{
						base.WriteVariable("findByName", contactSearchItem.FullName);
					}
					else if (!string.IsNullOrEmpty(contactSearchItem.Recipient.LegacyExchangeDN))
					{
						base.SetRecordedName("findByName", contactSearchItem.Recipient);
					}
					QueryFilter queryFilter = Filters.CreateFindByNameFilter(contactSearchItem);
					if (queryFilter != null)
					{
						mailboxSessionLock = this.user.CreateSessionLock();
						oneTimeSearch = OneTimeSearch.Execute(this.user, mailboxSessionLock.Session, this.InboxId, queryFilter);
						folder = Folder.Bind(mailboxSessionLock.Session, oneTimeSearch.FolderId);
						queryResult = folder.ItemQuery(ItemQueryType.None, null, null, EmailManager.viewProperties);
						int findByNameCount = this.GetFindByNameCount(queryResult);
						base.WriteVariable("numMessagesFromName", findByNameCount);
						if (findByNameCount > 0)
						{
							if (this.findByNameResults != null)
							{
								this.findByNameResults.Dispose();
								this.findByNameResults = null;
							}
							if (this.pendingDeletion != null)
							{
								this.pendingDeletion.Commit(mailboxSessionLock.Session);
								this.pendingDeletion = null;
								base.WriteVariable("canUndelete", false);
							}
							this.folderId = oneTimeSearch.FolderId;
							this.findByNameResults = oneTimeSearch;
							oneTimeSearch = null;
							this.firstMessageId = null;
							this.messageItemList = new MessageItemList(this.user, this.folderId, MessageItemListSortType.Email, EmailManager.viewProperties);
							base.WriteVariable("inFindMode", true);
						}
					}
					else
					{
						PIIMessage data = PIIMessage.Create(PIIType._User, this.user.ADRecipient.DistinguishedName);
						CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, data, "Unable to generate the search filter for Contact or Gal user with id {0} because neither displayname nor email properties are populated. Caller _User. ", new object[]
						{
							contactSearchItem.Id ?? "not available"
						});
						base.WriteVariable("numMessagesFromName", 0);
					}
				}
			}
			finally
			{
				if (queryResult != null)
				{
					queryResult.Dispose();
					queryResult = null;
				}
				if (folder != null)
				{
					folder.Dispose();
					folder = null;
				}
				if (mailboxSessionLock != null)
				{
					mailboxSessionLock.Dispose();
					mailboxSessionLock = null;
				}
				if (oneTimeSearch != null)
				{
					oneTimeSearch.Dispose();
					oneTimeSearch = null;
				}
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00024E80 File Offset: 0x00023080
		private int GetFindByNameCount(QueryResult query)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailManager::GetFindByNameCount.", new object[0]);
			int num = 0;
			int currentRow = query.CurrentRow;
			object[][] rows = query.GetRows(MessageItemList.PageSize);
			while (rows != null && 0 < rows.Length)
			{
				foreach (object[] array2 in rows)
				{
					string itemClass = (string)array2[1];
					byte[] array3 = array2[2] as byte[];
					if (EmailManager.CanReadMessageClassWithTui(itemClass) && (array3 == null || !this.hiddenThreads.ContainsKey(EmailManager.Conversation.FromConversationIndex(array3))))
					{
						num++;
					}
				}
				rows = query.GetRows(MessageItemList.PageSize);
			}
			if (query.CurrentRow != currentRow)
			{
				query.SeekToOffset(SeekReference.OriginBeginning, currentRow);
			}
			return num;
		}

		// Token: 0x04000896 RID: 2198
		private static PropertyDefinition[] viewProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			StoreObjectSchema.ItemClass,
			ItemSchema.ConversationIndex,
			ItemSchema.ConversationTopic,
			MessageItemSchema.IsRead
		};

		// Token: 0x04000897 RID: 2199
		private StoreObjectId firstMessageId;

		// Token: 0x04000898 RID: 2200
		private StoreObjectId folderId;

		// Token: 0x04000899 RID: 2201
		private UMSubscriber user;

		// Token: 0x0400089A RID: 2202
		private ContactInfo senderInfo;

		// Token: 0x0400089B RID: 2203
		private EmailManager.PendingDeletion pendingDeletion;

		// Token: 0x0400089C RID: 2204
		private uint numDeletions;

		// Token: 0x0400089D RID: 2205
		private Dictionary<EmailManager.Conversation, object> hiddenThreads;

		// Token: 0x0400089E RID: 2206
		private OneTimeSearch findByNameResults;

		// Token: 0x0400089F RID: 2207
		private bool isRecurringMeetingRequest;

		// Token: 0x040008A0 RID: 2208
		private bool isSenderRoutable;

		// Token: 0x040008A1 RID: 2209
		private MessageItemList messageItemList;

		// Token: 0x0200012C RID: 300
		internal class ConfigClass : ActivityManagerConfig
		{
			// Token: 0x0600087D RID: 2173 RVA: 0x00024F7A File Offset: 0x0002317A
			internal ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x0600087E RID: 2174 RVA: 0x00024F83 File Offset: 0x00023183
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Constructing Email activity manager.", new object[0]);
				return new EmailManager(manager, this);
			}
		}

		// Token: 0x0200012D RID: 301
		private abstract class PendingDeletion
		{
			// Token: 0x0600087F RID: 2175 RVA: 0x00024FA2 File Offset: 0x000231A2
			protected PendingDeletion(BaseUMCallSession vo)
			{
				this.vo = vo;
			}

			// Token: 0x17000219 RID: 537
			// (get) Token: 0x06000880 RID: 2176 RVA: 0x00024FB8 File Offset: 0x000231B8
			internal StoreObjectId UndeleteRefId
			{
				get
				{
					return this.Id;
				}
			}

			// Token: 0x1700021A RID: 538
			// (get) Token: 0x06000881 RID: 2177 RVA: 0x00024FC0 File Offset: 0x000231C0
			protected BaseUMCallSession CallSession
			{
				get
				{
					return this.vo;
				}
			}

			// Token: 0x1700021B RID: 539
			// (get) Token: 0x06000882 RID: 2178 RVA: 0x00024FC8 File Offset: 0x000231C8
			// (set) Token: 0x06000883 RID: 2179 RVA: 0x00024FD0 File Offset: 0x000231D0
			protected bool IsValid
			{
				get
				{
					return this.valid;
				}
				set
				{
					this.valid = value;
				}
			}

			// Token: 0x1700021C RID: 540
			// (get) Token: 0x06000884 RID: 2180 RVA: 0x00024FD9 File Offset: 0x000231D9
			// (set) Token: 0x06000885 RID: 2181 RVA: 0x00024FE1 File Offset: 0x000231E1
			protected StoreObjectId Id
			{
				get
				{
					return this.objectId;
				}
				set
				{
					this.objectId = value;
				}
			}

			// Token: 0x06000886 RID: 2182 RVA: 0x00024FEC File Offset: 0x000231EC
			internal virtual void Commit(MailboxSession session)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "Committing a pendingDeletion of type={0}.", new object[]
				{
					this
				});
				this.IsValid = false;
			}

			// Token: 0x06000887 RID: 2183 RVA: 0x0002501C File Offset: 0x0002321C
			internal virtual void Revert()
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "Reverting a pendingDeletion of type={0}.", new object[]
				{
					this
				});
				this.IsValid = false;
			}

			// Token: 0x040008A3 RID: 2211
			private BaseUMCallSession vo;

			// Token: 0x040008A4 RID: 2212
			private bool valid = true;

			// Token: 0x040008A5 RID: 2213
			private StoreObjectId objectId;
		}

		// Token: 0x0200012E RID: 302
		private class SingleItemPendingDeletion : EmailManager.PendingDeletion
		{
			// Token: 0x06000888 RID: 2184 RVA: 0x0002504C File Offset: 0x0002324C
			internal SingleItemPendingDeletion(BaseUMCallSession vo, MessageItemList storeItemList) : base(vo)
			{
				base.Id = storeItemList.CurrentStoreObjectId;
				this.storeItemList = storeItemList;
				this.storeItemList.Ignore(storeItemList.CurrentStoreObjectId);
			}

			// Token: 0x06000889 RID: 2185 RVA: 0x0002507C File Offset: 0x0002327C
			internal override void Commit(MailboxSession session)
			{
				if (base.IsValid && session != null)
				{
					base.Commit(session);
					session.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
					{
						base.Id
					});
					base.CallSession.IncrementCounter(SubscriberAccessCounters.EmailMessagesDeleted);
				}
			}

			// Token: 0x0600088A RID: 2186 RVA: 0x000250C4 File Offset: 0x000232C4
			internal override void Revert()
			{
				if (base.IsValid)
				{
					base.Revert();
					this.storeItemList.UnIgnore(base.Id);
				}
			}

			// Token: 0x040008A6 RID: 2214
			private MessageItemList storeItemList;
		}

		// Token: 0x0200012F RID: 303
		private class ConversationPendingDeletion : EmailManager.PendingDeletion
		{
			// Token: 0x0600088B RID: 2187 RVA: 0x000250E5 File Offset: 0x000232E5
			internal ConversationPendingDeletion(BaseUMCallSession vo, StoreObjectId undeleteRefId, Dictionary<EmailManager.Conversation, object> hiddenThreads, EmailManager.Conversation conversation, params StoreObjectId[] ids) : base(vo)
			{
				base.Id = undeleteRefId;
				this.hiddenThreads = hiddenThreads;
				this.conversation = conversation;
				this.objectIds = ids;
			}

			// Token: 0x0600088C RID: 2188 RVA: 0x0002510C File Offset: 0x0002330C
			internal override void Commit(MailboxSession session)
			{
				if (base.IsValid && session != null)
				{
					base.Commit(session);
					session.Delete(DeleteItemFlags.MoveToDeletedItems, this.objectIds);
					base.CallSession.IncrementCounter(SubscriberAccessCounters.EmailMessagesDeleted, (long)this.objectIds.Length);
				}
			}

			// Token: 0x0600088D RID: 2189 RVA: 0x00025147 File Offset: 0x00023347
			internal override void Revert()
			{
				if (base.IsValid)
				{
					base.Revert();
					this.hiddenThreads.Remove(this.conversation);
				}
			}

			// Token: 0x040008A7 RID: 2215
			private StoreObjectId[] objectIds;

			// Token: 0x040008A8 RID: 2216
			private Dictionary<EmailManager.Conversation, object> hiddenThreads;

			// Token: 0x040008A9 RID: 2217
			private EmailManager.Conversation conversation;
		}

		// Token: 0x02000130 RID: 304
		private class MeetingResponse : XsoRecordedMessage
		{
			// Token: 0x0600088E RID: 2190 RVA: 0x00025169 File Offset: 0x00023369
			internal MeetingResponse(BaseUMCallSession vo, StoreObjectId originalId, ResponseType responseType, UMSubscriber user, EmailManager context) : base(vo, user, context)
			{
				this.originalId = originalId;
				this.responseType = responseType;
			}

			// Token: 0x0600088F RID: 2191 RVA: 0x00025184 File Offset: 0x00023384
			public override void DoPostSubmit()
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "MeetingResponse::DoPostSubmit.", new object[0]);
				EmailManager emailManager = base.Manager as EmailManager;
				if (emailManager != null)
				{
					emailManager.DeleteCurrentMessage(false);
				}
				if (ResponseType.Decline == this.responseType && this.correlatedId != null)
				{
					using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = base.User.CreateSessionLock())
					{
						mailboxSessionLock.Session.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
						{
							this.correlatedId
						});
						CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "Successfully deleted calendar item on meeting decline.", new object[0]);
					}
				}
				base.DoPostSubmit();
			}

			// Token: 0x06000890 RID: 2192 RVA: 0x00025230 File Offset: 0x00023430
			protected override MessageItem GenerateMessage(MailboxSession session)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "MeetingResponse::GenerateResponse.", new object[0]);
				EmailManager emailManager = (EmailManager)base.Manager;
				Microsoft.Exchange.Data.Storage.MeetingResponse result = null;
				using (MeetingRequest meetingRequest = MeetingRequest.Bind(session, this.originalId))
				{
					meetingRequest.OpenAsReadWrite();
					using (CalendarItemBase calendarItemBase = meetingRequest.UpdateCalendarItem(false))
					{
						if (calendarItemBase != null)
						{
							result = XsoUtil.RespondToMeetingRequest(calendarItemBase, this.responseType);
							calendarItemBase.Load(new PropertyDefinition[]
							{
								ItemSchema.HasAttachment
							});
							base.SetAttachmentName(calendarItemBase.AttachmentCollection);
							this.correlatedId = calendarItemBase.Id;
							CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "MeetingResponse::GenerateMessage successfully built response.", new object[0]);
						}
					}
				}
				return result;
			}

			// Token: 0x040008AA RID: 2218
			private ResponseType responseType;

			// Token: 0x040008AB RID: 2219
			private StoreObjectId originalId;

			// Token: 0x040008AC RID: 2220
			private VersionedId correlatedId;
		}

		// Token: 0x02000131 RID: 305
		private abstract class EmailReplyBase : XsoRecordedMessage
		{
			// Token: 0x06000891 RID: 2193 RVA: 0x00025308 File Offset: 0x00023508
			internal EmailReplyBase(BaseUMCallSession vo, StoreObjectId originalId, UMSubscriber user, EmailManager context) : base(vo, user, context)
			{
				this.originalId = originalId;
			}

			// Token: 0x06000892 RID: 2194 RVA: 0x0002531B File Offset: 0x0002351B
			public override void DoPostSubmit()
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailReply::DoPostSubmit.", new object[0]);
				base.Session.IncrementCounter(SubscriberAccessCounters.ReplyMessagesSent);
				base.DoPostSubmit();
			}

			// Token: 0x06000893 RID: 2195 RVA: 0x0002534C File Offset: 0x0002354C
			protected override MessageItem GenerateMessage(MailboxSession session)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailReply::GenerateResponse.", new object[0]);
				MessageItem result;
				using (MessageItem messageItem = MessageItem.Bind(session, this.originalId))
				{
					messageItem.OpenAsReadWrite();
					base.SetAttachmentName(messageItem.AttachmentCollection);
					result = this.CreateReplyMessage(messageItem, base.PrepareMessageBodyPrefix(messageItem), BodyFormat.TextHtml, XsoUtil.GetDraftsFolderId(session));
				}
				return result;
			}

			// Token: 0x06000894 RID: 2196
			protected abstract MessageItem CreateReplyMessage(MessageItem originalMessage, string bodyPrefix, BodyFormat bodyFormat, StoreObjectId parentFolderId);

			// Token: 0x06000895 RID: 2197 RVA: 0x000253C4 File Offset: 0x000235C4
			protected override void AddRecordedMessageText(MessageContentBuilder content)
			{
				content.AddRecordedReplyText(base.User.DisplayName);
			}

			// Token: 0x06000896 RID: 2198 RVA: 0x000253D7 File Offset: 0x000235D7
			protected override void AddMessageHeader(Item originalMessage, MessageContentBuilder content)
			{
				content.AddEmailHeader((MessageItem)originalMessage);
			}

			// Token: 0x040008AD RID: 2221
			private StoreObjectId originalId;
		}

		// Token: 0x02000132 RID: 306
		private class EmailReply : EmailManager.EmailReplyBase
		{
			// Token: 0x06000897 RID: 2199 RVA: 0x000253E5 File Offset: 0x000235E5
			internal EmailReply(BaseUMCallSession vo, StoreObjectId originalId, UMSubscriber user, EmailManager context) : base(vo, originalId, user, context)
			{
			}

			// Token: 0x06000898 RID: 2200 RVA: 0x000253F4 File Offset: 0x000235F4
			protected override MessageItem CreateReplyMessage(MessageItem originalMessage, string bodyPrefix, BodyFormat bodyFormat, StoreObjectId parentFolderId)
			{
				ReplyForwardConfiguration replyForwardConfiguration = new ReplyForwardConfiguration(bodyFormat);
				replyForwardConfiguration.AddBodyPrefix(bodyPrefix);
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(base.User.ADUser.OrganizationId);
				AcceptedDomain defaultAcceptedDomain = iadsystemConfigurationLookup.GetDefaultAcceptedDomain();
				replyForwardConfiguration.ConversionOptionsForSmime = new InboundConversionOptions(defaultAcceptedDomain.DomainName.ToString());
				replyForwardConfiguration.ConversionOptionsForSmime.UserADSession = ADRecipientLookupFactory.CreateFromUmUser(base.User).ScopedRecipientSession;
				return originalMessage.CreateReply(parentFolderId, replyForwardConfiguration);
			}
		}

		// Token: 0x02000133 RID: 307
		private class EmailReplyAll : EmailManager.EmailReplyBase
		{
			// Token: 0x06000899 RID: 2201 RVA: 0x00025466 File Offset: 0x00023666
			internal EmailReplyAll(BaseUMCallSession vo, StoreObjectId originalId, UMSubscriber user, EmailManager context) : base(vo, originalId, user, context)
			{
			}

			// Token: 0x0600089A RID: 2202 RVA: 0x00025474 File Offset: 0x00023674
			protected override MessageItem CreateReplyMessage(MessageItem originalMessage, string bodyPrefix, BodyFormat bodyFormat, StoreObjectId parentFolderId)
			{
				ReplyForwardConfiguration replyForwardConfiguration = new ReplyForwardConfiguration(bodyFormat);
				replyForwardConfiguration.AddBodyPrefix(bodyPrefix);
				IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(base.User.ADUser.OrganizationId);
				AcceptedDomain defaultAcceptedDomain = iadsystemConfigurationLookup.GetDefaultAcceptedDomain();
				replyForwardConfiguration.ConversionOptionsForSmime = new InboundConversionOptions(defaultAcceptedDomain.DomainName.ToString());
				replyForwardConfiguration.ConversionOptionsForSmime.UserADSession = ADRecipientLookupFactory.CreateFromUmUser(base.User).ScopedRecipientSession;
				return originalMessage.CreateReplyAll(parentFolderId, replyForwardConfiguration);
			}
		}

		// Token: 0x02000134 RID: 308
		private class EmailForward : XsoRecordedMessage
		{
			// Token: 0x0600089B RID: 2203 RVA: 0x000254E6 File Offset: 0x000236E6
			internal EmailForward(BaseUMCallSession vo, StoreObjectId originalId, UMSubscriber user, EmailManager context) : base(vo, user, context)
			{
				this.originalId = originalId;
			}

			// Token: 0x0600089C RID: 2204 RVA: 0x000254F9 File Offset: 0x000236F9
			public override void DoPostSubmit()
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailForward::DoPostSubmit.", new object[0]);
				base.Session.IncrementCounter(SubscriberAccessCounters.ForwardMessagesSent);
				base.DoPostSubmit();
			}

			// Token: 0x0600089D RID: 2205 RVA: 0x00025528 File Offset: 0x00023728
			protected override MessageItem GenerateMessage(MailboxSession session)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "EmailForward::GenerateResponse.", new object[0]);
				MessageItem result;
				using (MessageItem messageItem = MessageItem.Bind(session, this.originalId))
				{
					base.SetAttachmentName(messageItem.AttachmentCollection);
					ReplyForwardConfiguration replyForwardConfiguration = new ReplyForwardConfiguration(BodyFormat.TextHtml);
					replyForwardConfiguration.AddBodyPrefix(base.PrepareMessageBodyPrefix(messageItem), BodyInjectionFormat.Html);
					IADSystemConfigurationLookup iadsystemConfigurationLookup = ADSystemConfigurationLookupFactory.CreateFromOrganizationId(base.User.ADUser.OrganizationId);
					AcceptedDomain defaultAcceptedDomain = iadsystemConfigurationLookup.GetDefaultAcceptedDomain();
					replyForwardConfiguration.ConversionOptionsForSmime = new InboundConversionOptions(defaultAcceptedDomain.DomainName.ToString());
					replyForwardConfiguration.ConversionOptionsForSmime.UserADSession = ADRecipientLookupFactory.CreateFromUmUser(base.User).ScopedRecipientSession;
					MessageItem messageItem2 = messageItem.CreateForward(XsoUtil.GetDraftsFolderId(session), replyForwardConfiguration);
					messageItem2[MessageItemSchema.VoiceMessageAttachmentOrder] = XsoUtil.GetAttachmentOrderString(messageItem);
					result = messageItem2;
				}
				return result;
			}

			// Token: 0x0600089E RID: 2206 RVA: 0x0002560C File Offset: 0x0002380C
			protected override void AddRecordedMessageText(MessageContentBuilder content)
			{
				content.AddRecordedForwardText(base.User.DisplayName);
			}

			// Token: 0x0600089F RID: 2207 RVA: 0x0002561F File Offset: 0x0002381F
			protected override void AddMessageHeader(Item originalMessage, MessageContentBuilder content)
			{
				content.AddEmailHeader((MessageItem)originalMessage);
			}

			// Token: 0x040008AE RID: 2222
			private StoreObjectId originalId;
		}

		// Token: 0x02000135 RID: 309
		private class Conversation : IEquatable<EmailManager.Conversation>
		{
			// Token: 0x060008A0 RID: 2208 RVA: 0x0002562D File Offset: 0x0002382D
			private Conversation()
			{
			}

			// Token: 0x060008A1 RID: 2209 RVA: 0x00025638 File Offset: 0x00023838
			private Conversation(byte[] indexBytes)
			{
				this.indexBytes = indexBytes;
				if (indexBytes == null || indexBytes.Length < 22)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "Conversation constructed without a valid conversation index.", new object[0]);
					this.hash = EmailManager.Conversation.r.Next();
					return;
				}
				CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, "Conversation constructor with a valid conversation index", new object[0]);
				this.hash = BitConverter.ToInt32(indexBytes, 17);
			}

			// Token: 0x060008A2 RID: 2210 RVA: 0x000256A8 File Offset: 0x000238A8
			public bool Equals(EmailManager.Conversation other)
			{
				if (this.indexBytes == null || other.indexBytes == null)
				{
					return false;
				}
				if (this.indexBytes.Length < 22 || other.indexBytes.Length < 22)
				{
					return false;
				}
				for (int i = 0; i < 22; i++)
				{
					if (this.indexBytes[i] != other.indexBytes[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060008A3 RID: 2211 RVA: 0x00025703 File Offset: 0x00023903
			public override int GetHashCode()
			{
				return this.hash;
			}

			// Token: 0x060008A4 RID: 2212 RVA: 0x0002570B File Offset: 0x0002390B
			internal static EmailManager.Conversation FromConversationIndex(byte[] indexBytes)
			{
				return new EmailManager.Conversation(indexBytes);
			}

			// Token: 0x040008AF RID: 2223
			private static Random r = new Random();

			// Token: 0x040008B0 RID: 2224
			private byte[] indexBytes;

			// Token: 0x040008B1 RID: 2225
			private int hash;
		}
	}
}
