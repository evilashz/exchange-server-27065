using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.MessageContent;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001C9 RID: 457
	internal class RetrieveVoicemailManager : SendMessageManager
	{
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x0003AB56 File Offset: 0x00038D56
		public bool MessageListIsNull
		{
			get
			{
				return this.voiceMessageList.MessageListIsNull;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x0003AB63 File Offset: 0x00038D63
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x0003AB6B File Offset: 0x00038D6B
		internal NameOrNumberOfCaller SpecifiedCallerDetails { get; private set; }

		// Token: 0x06000D4E RID: 3406 RVA: 0x0003AB74 File Offset: 0x00038D74
		internal RetrieveVoicemailManager(ActivityManager manager, RetrieveVoicemailManager.ConfigClass config) : base(manager, config)
		{
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x0003AB89 File Offset: 0x00038D89
		internal bool IsForwardEnabled
		{
			get
			{
				return !this.GlobalManager.LimitedOVAAccess && this.GlobalManager.AddressBookEnabled;
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x0003ABA5 File Offset: 0x00038DA5
		internal bool DrmIsEnabled
		{
			get
			{
				return this.drmIsEnabled;
			}
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x0003ABAD File Offset: 0x00038DAD
		internal override bool LargeGrammarsNeeded
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000D52 RID: 3410 RVA: 0x0003ABB0 File Offset: 0x00038DB0
		internal bool IsForwardToContactEnabled
		{
			get
			{
				return !this.GlobalManager.LimitedOVAAccess && this.GlobalManager.ContactsAccessEnabled;
			}
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x0003ABCC File Offset: 0x00038DCC
		internal bool IsFindEnabled
		{
			get
			{
				return !this.GlobalManager.LimitedOVAAccess && this.GlobalManager.AddressBookEnabled;
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000D54 RID: 3412 RVA: 0x0003ABE8 File Offset: 0x00038DE8
		internal bool IsFindByContactEnabled
		{
			get
			{
				return !this.GlobalManager.LimitedOVAAccess && this.GlobalManager.ContactsAccessEnabled;
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0003AC04 File Offset: 0x00038E04
		internal static PhoneNumber ApplyDialingRules(UMSubscriber caller, PhoneNumber senderPhone, UMDialPlan targetDialPlan)
		{
			PIIMessage data = PIIMessage.Create(PIIType._PhoneNumber, senderPhone);
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, null, data, "ApplyDialingRules(phone: _PhoneNumber targetDialplan: {0}.", new object[]
			{
				(targetDialPlan == null) ? "<null>" : targetDialPlan.Name
			});
			if (PhoneNumber.IsNullOrEmpty(senderPhone))
			{
				return null;
			}
			PhoneNumber phoneNumber = null;
			PhoneNumber phoneNumber2 = DialPermissions.Canonicalize(senderPhone, caller.DialPlan, null, targetDialPlan);
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, null, data, "DialPermissions.Canonicalize(InputPhone:_PhoneNumber, OrigDP:{0}, TargetDP:{1}, CanonicalPhone:{2}.", new object[]
			{
				caller.DialPlan.Name,
				(targetDialPlan == null) ? "<null>" : targetDialPlan.Name,
				phoneNumber2
			});
			if (phoneNumber2 == null)
			{
				return null;
			}
			bool flag = DialPermissions.Check(phoneNumber2, (ADUser)caller.ADRecipient, caller.DialPlan, targetDialPlan, out phoneNumber);
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, null, "CheckDialPermissions(Phone:{0}, OrigDP:{1}, TargetDP:{2}, Allowed:{3}, Dial:{4}.", new object[]
			{
				phoneNumber2,
				caller.DialPlan.Name,
				(targetDialPlan == null) ? "<null>" : targetDialPlan.Name,
				flag,
				phoneNumber
			});
			if (!flag)
			{
				return null;
			}
			return phoneNumber;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0003AD20 File Offset: 0x00038F20
		internal override void Start(BaseUMCallSession vo, string refInfo)
		{
			vo.IncrementCounter(SubscriberAccessCounters.VoiceMessageQueueAccessed);
			this.caller = vo.CurrentCallContext.CallerInfo;
			this.SpecifiedCallerDetails = new NameOrNumberOfCaller(NameOrNumberOfCaller.TypeOfVoiceCall.VoicemailCall);
			this.drmIsEnabled = (this.caller != null && this.caller.DRMPolicyForInterpersonal != DRMProtectionOptions.None);
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.caller.CreateSessionLock())
			{
				using (UMSearchFolder umsearchFolder = UMSearchFolder.Get(mailboxSessionLock.Session, UMSearchFolder.Type.VoiceMail))
				{
					this.voicemailSearchFolderId = umsearchFolder.SearchFolder.Id.ObjectId;
				}
			}
			base.Start(vo, refInfo);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0003ADE4 File Offset: 0x00038FE4
		internal override void CheckAuthorization(UMSubscriber u)
		{
			if (!u.IsAuthenticated)
			{
				base.CheckAuthorization(u);
			}
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0003ADF8 File Offset: 0x00038FF8
		internal override TransitionBase ExecuteAction(string action, BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "RetrieveVoicemailManager asked to do action {0}.", new object[]
			{
				action
			});
			string input = null;
			if (string.Equals(action, "getNewMessages", StringComparison.OrdinalIgnoreCase))
			{
				this.voiceMessageList.InitializeCurrentPagerList(new MessageItemList(this.caller, this.voicemailSearchFolderId, this.GetSortTypeForUnreadVoicemessages(this.caller), RetrieveVoicemailManager.viewProperties));
				this.readingSavedMessages = false;
				this.readAsFirstMessage = true;
				input = this.GetNextMessage(vo);
			}
			else if (string.Equals(action, "getSavedMessages", StringComparison.OrdinalIgnoreCase))
			{
				this.voiceMessageList.InitializeCurrentPagerList(new MessageItemList(this.caller, this.voicemailSearchFolderId, MessageItemListSortType.LifoVoicemail, RetrieveVoicemailManager.viewProperties));
				this.readingSavedMessages = true;
				this.readAsFirstMessage = !this.haveReadSavedMessages;
				this.haveReadSavedMessages = true;
				input = this.GetNextMessage(vo);
			}
			else if (string.Equals(action, "getNextMessage", StringComparison.OrdinalIgnoreCase))
			{
				input = this.GetNextMessage(vo);
			}
			else if (string.Equals(action, "getPreviousMessage", StringComparison.OrdinalIgnoreCase))
			{
				input = this.GetPreviousMessage(vo);
			}
			else if (string.Equals(action, "getPriorityOfMessage", StringComparison.OrdinalIgnoreCase))
			{
				input = this.GetPriorityOfMessage();
			}
			else if (string.Equals(action, "deleteVoiceMail", StringComparison.OrdinalIgnoreCase))
			{
				this.DeleteCurrentMessage();
			}
			else if (string.Equals(action, "replyVoiceMail", StringComparison.OrdinalIgnoreCase))
			{
				base.SendMsg = new VoicemailReply(vo, this.voiceMessageList.CurrentMessageBeingRead, this.caller, this.currentSender, this, (bool)this.ReadVariable("protected"));
				base.WriteReplyIntroType(IntroType.Reply);
			}
			else if (string.Equals(action, "forwardVoiceMail", StringComparison.OrdinalIgnoreCase))
			{
				base.SendMsg = new VoicemailForward(vo, this.voiceMessageList.CurrentMessageBeingRead, this.caller, this);
				base.WriteReplyIntroType(IntroType.Forward);
			}
			else if (string.Equals(action, "undeleteVoiceMail", StringComparison.OrdinalIgnoreCase))
			{
				this.UndeleteCurrentMessage();
			}
			else if (string.Equals(action, "saveVoiceMail", StringComparison.OrdinalIgnoreCase))
			{
				this.SaveMessage();
			}
			else if (string.Equals(action, "markUnreadVoiceMail", StringComparison.OrdinalIgnoreCase))
			{
				this.MarkUnread();
			}
			else if (string.Equals(action, "flagVoiceMail", StringComparison.OrdinalIgnoreCase))
			{
				this.FlagMessage();
			}
			else if (string.Equals(action, "getEnvelopInfo", StringComparison.OrdinalIgnoreCase))
			{
				this.GetEnvelopInformation();
			}
			else if (string.Equals(action, "findByName", StringComparison.OrdinalIgnoreCase))
			{
				this.FindByName();
			}
			else
			{
				if (!string.Equals(action, "getMessageReadProperty", StringComparison.OrdinalIgnoreCase))
				{
					return base.ExecuteAction(action, vo);
				}
				input = (this.readingSavedMessages ? "currentSavedMessage" : "currentNewMessage");
			}
			return base.CurrentActivity.GetTransition(input);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0003B086 File Offset: 0x00039286
		internal string ReplyAll(BaseUMCallSession vo)
		{
			base.SendMsg = new VoicemailReplyAll(vo, this.voiceMessageList.CurrentMessageBeingRead, this.caller, this.currentSender, this, (bool)this.ReadVariable("protected"));
			base.WriteReplyIntroType(IntroType.ReplyAll);
			return null;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0003B0C4 File Offset: 0x000392C4
		protected override void InternalDispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "RetrieveVoicemailManager::Dispose.", new object[0]);
					if (this.findByNameResults != null)
					{
						this.findByNameResults.Dispose();
					}
					if (this.pendingDeletionObjectId != null)
					{
						this.CommitPendingDeletion();
					}
				}
			}
			finally
			{
				base.InternalDispose(disposing);
			}
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0003B128 File Offset: 0x00039328
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RetrieveVoicemailManager>(this);
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0003B130 File Offset: 0x00039330
		private MessageItemListSortType GetSortTypeForUnreadVoicemessages(UMSubscriber user)
		{
			if (user.ConfigFolder.ReadUnreadVoicemailInFIFOOrder)
			{
				return MessageItemListSortType.FifoVoicemail;
			}
			return MessageItemListSortType.LifoVoicemail;
		}

		// Token: 0x06000D5D RID: 3421 RVA: 0x0003B144 File Offset: 0x00039344
		private string GetNextMessage(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "Fetching the next Message.", new object[0]);
			string result = null;
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.caller.CreateSessionLock())
			{
				if (!this.voiceMessageList.GetNextMessageToRead(this.readingSavedMessages))
				{
					return this.readingSavedMessages ? "noSavedMessages" : "noNewMessages";
				}
				using (MessageItem messageItem = Item.BindAsMessage(mailboxSessionLock.Session, this.voiceMessageList.CurrentMessageBeingRead, RetrieveVoicemailManager.voiceMessageProperties))
				{
					messageItem.OpenAsReadWrite();
					this.LoadCurrentMessageProperties(messageItem);
					this.readAsFirstMessage = false;
				}
			}
			return result;
		}

		// Token: 0x06000D5E RID: 3422 RVA: 0x0003B208 File Offset: 0x00039408
		private string GetPreviousMessage(BaseUMCallSession vo)
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "Fetching the previous Message.", new object[0]);
			string result = null;
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.caller.CreateSessionLock())
			{
				if (!this.voiceMessageList.GetPreviousMessageToRead())
				{
					result = "noPreviousNewMessages";
				}
				else
				{
					using (MessageItem messageItem = Item.BindAsMessage(mailboxSessionLock.Session, this.voiceMessageList.CurrentMessageBeingRead, RetrieveVoicemailManager.voiceMessageProperties))
					{
						this.LoadCurrentMessageProperties(messageItem);
						this.readAsFirstMessage = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06000D5F RID: 3423 RVA: 0x0003B2B0 File Offset: 0x000394B0
		private void LoadCurrentMessageProperties(MessageItem currentMessage)
		{
			this.SpecifiedCallerDetails.ClearProperties();
			base.MessagePlayerContext.Reset(currentMessage.Id.ObjectId);
			PhoneNumber senderTelephoneNumber = XsoUtil.GetSenderTelephoneNumber(currentMessage);
			base.WriteVariable("senderCallerID", senderTelephoneNumber);
			this.SpecifiedCallerDetails.CallerId = senderTelephoneNumber;
			ContactInfo contactInfo = null;
			if (null != currentMessage.From && currentMessage.From.EmailAddress != null)
			{
				contactInfo = ContactInfo.FindByParticipant(this.caller, currentMessage.From);
			}
			this.currentSender = ((contactInfo != null) ? contactInfo : new DefaultContactInfo());
			bool flag = this.currentSender is DefaultContactInfo;
			PhoneNumber phoneNumber = RetrieveVoicemailManager.ApplyDialingRules(this.caller, senderTelephoneNumber, this.currentSender.DialPlan);
			base.TargetPhoneNumber = phoneNumber;
			base.WriteVariable("knowSenderPhoneNumber", null != phoneNumber);
			string text = flag ? null : this.currentSender.EMailAddress;
			base.WriteVariable("knowVoicemailSender", !string.IsNullOrEmpty(text) && SmtpAddress.IsValidSmtpAddress(text));
			base.WriteVariable("messageReceivedTime", currentMessage.ReceivedTime);
			this.SpecifiedCallerDetails.MessageReceivedTime = currentMessage.ReceivedTime;
			base.WriteVariable("receivedDayOfWeek", (int)currentMessage.ReceivedTime.DayOfWeek);
			base.WriteVariable("receivedOffset", (int)(this.caller.Now.Date - currentMessage.ReceivedTime.Date).TotalDays);
			if (flag)
			{
				string varValue = (string)XsoUtil.SafeGetProperty(currentMessage, MessageItemSchema.VoiceMessageSenderName, string.Empty);
				base.WriteVariable("emailSender", varValue);
			}
			else if (this.currentSender.ADOrgPerson == null)
			{
				base.WriteVariable("emailSender", this.currentSender.DisplayName);
			}
			else
			{
				base.SetRecordedName("emailSender", this.currentSender.ADOrgPerson);
			}
			base.WriteVariable("firstMessage", this.readAsFirstMessage);
			base.WriteVariable("read", this.readingSavedMessages);
			ReplyForwardType replyForwardType = GlobCfg.SubjectToReplyForwardType(currentMessage.Subject);
			base.WriteVariable("isReply", replyForwardType == ReplyForwardType.Reply);
			base.WriteVariable("isForward", replyForwardType == ReplyForwardType.Forward);
			base.WriteVariable("urgent", Importance.High == currentMessage.Importance);
			this.currentMessageImportance = currentMessage.Importance;
			base.WriteVariable("protected", currentMessage.IsRestricted);
			base.WriteReplyIntroType(IntroType.None);
			base.CallSession.IncrementCounter(SubscriberAccessCounters.VoiceMessagesHeard);
			if (currentMessage.IsRestricted)
			{
				base.CallSession.IncrementCounter(SubscriberAccessCounters.ProtectedVoiceMessagesHeard);
			}
		}

		// Token: 0x06000D60 RID: 3424 RVA: 0x0003B571 File Offset: 0x00039771
		private string GetPriorityOfMessage()
		{
			if (Importance.High != this.currentMessageImportance)
			{
				return null;
			}
			return "isHighPriority";
		}

		// Token: 0x06000D61 RID: 3425 RVA: 0x0003B584 File Offset: 0x00039784
		private void SaveMessage()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "Attempting to save message.", new object[0]);
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.caller.CreateSessionLock())
			{
				using (MessageItem messageItem = MessageItem.Bind(mailboxSessionLock.Session, this.voiceMessageList.CurrentMessageBeingRead))
				{
					if (!messageItem.IsRead)
					{
						messageItem.OpenAsReadWrite();
						messageItem.MarkAsRead(false, true);
						ConflictResolutionResult conflictResolutionResult = messageItem.Save(SaveMode.ResolveConflicts);
						if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
						{
							throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(this.voiceMessageList.CurrentMessageBeingRead), conflictResolutionResult);
						}
					}
				}
			}
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x0003B63C File Offset: 0x0003983C
		private void MarkUnread()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "Attempting to mark message as unread.", new object[0]);
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.caller.CreateSessionLock())
			{
				using (MessageItem messageItem = MessageItem.Bind(mailboxSessionLock.Session, this.voiceMessageList.CurrentMessageBeingRead))
				{
					if (messageItem.IsRead)
					{
						messageItem.OpenAsReadWrite();
						messageItem.MarkAsUnread(true);
						ConflictResolutionResult conflictResolutionResult = messageItem.Save(SaveMode.ResolveConflicts);
						if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
						{
							throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(this.voiceMessageList.CurrentMessageBeingRead), conflictResolutionResult);
						}
					}
				}
			}
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0003B6F4 File Offset: 0x000398F4
		private void FlagMessage()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "Attempting to flag message.", new object[0]);
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.caller.CreateSessionLock())
			{
				using (MessageItem messageItem = MessageItem.Bind(mailboxSessionLock.Session, this.voiceMessageList.CurrentMessageBeingRead))
				{
					messageItem.OpenAsReadWrite();
					messageItem.SetFlag(Strings.FollowUp.ToString(this.caller.TelephonyCulture), null, null);
					ConflictResolutionResult conflictResolutionResult = messageItem.Save(SaveMode.ResolveConflicts);
					if (conflictResolutionResult.SaveStatus == SaveResult.IrresolvableConflict)
					{
						throw new SaveConflictException(ServerStrings.ExSaveFailedBecauseOfConflicts(this.voiceMessageList.CurrentMessageBeingRead), conflictResolutionResult);
					}
				}
			}
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0003B7D0 File Offset: 0x000399D0
		private void GetEnvelopInformation()
		{
			using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.caller.CreateSessionLock())
			{
				using (MessageItem messageItem = MessageItem.Bind(mailboxSessionLock.Session, this.voiceMessageList.CurrentMessageBeingRead, RetrieveVoicemailManager.voiceMessageProperties))
				{
					int num = (int)XsoUtil.SafeGetProperty(messageItem, MessageItemSchema.VoiceMessageDuration, 0);
					base.WriteVariable("durationMinutes", num / 60);
					base.WriteVariable("durationSeconds", num % 60);
					base.WriteVariable("isHighPriority", Importance.High == messageItem.Importance);
					base.WriteVariable("isProtected", messageItem.IsRestricted);
					string text = (string)XsoUtil.SafeGetProperty(messageItem, MessageItemSchema.VoiceMessageSenderName, null);
					if (string.IsNullOrEmpty(text))
					{
						PhoneNumber senderTelephoneNumber = XsoUtil.GetSenderTelephoneNumber(messageItem);
						base.WriteVariable("senderCallerID", senderTelephoneNumber);
						this.SpecifiedCallerDetails.CallerId = senderTelephoneNumber;
					}
					base.WriteVariable("senderInfo", text);
					this.SpecifiedCallerDetails.CallerName = text;
				}
			}
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0003B8FC File Offset: 0x00039AFC
		private void DeleteCurrentMessage()
		{
			this.CommitPendingDeletion();
			if (this.numDeletions < 2U && (this.numDeletions += 1U) >= 2U)
			{
				base.WriteVariable("playedUndelete", true);
			}
			this.pendingDeletionObjectId = this.voiceMessageList.CurrentMessageBeingRead;
			this.voiceMessageList.DeleteMessage(this.pendingDeletionObjectId);
			base.WriteVariable("canUndelete", true);
			base.CallSession.IncrementCounter(SubscriberAccessCounters.VoiceMessagesDeleted);
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x0003B980 File Offset: 0x00039B80
		private void UndeleteCurrentMessage()
		{
			this.voiceMessageList.UnDeleteMessage(this.pendingDeletionObjectId);
			this.pendingDeletionObjectId = null;
			base.WriteVariable("canUndelete", false);
			base.CallSession.DecrementCounter(SubscriberAccessCounters.VoiceMessagesDeleted);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0003B9BC File Offset: 0x00039BBC
		private void FindByName()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "VmailManager::FindByName.", new object[0]);
			UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = null;
			OneTimeSearch oneTimeSearch = null;
			int num = 0;
			try
			{
				ContactSearchItem contactSearchItem = (ContactSearchItem)this.ReadVariable("directorySearchResult");
				if (contactSearchItem == null)
				{
					CallIdTracer.TraceDebug(ExTraceGlobals.VoiceMailTracer, this, "VmailManager::FindByName received a null search result!", new object[0]);
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
					QueryFilter queryFilter = Filters.CreateVoicemailFindByNameFilter(contactSearchItem, this.caller.MessageSubmissionCulture);
					if (queryFilter != null)
					{
						StoreId fid = this.voicemailSearchFolderId;
						mailboxSessionLock = this.caller.CreateSessionLock();
						oneTimeSearch = OneTimeSearch.Execute(this.caller, mailboxSessionLock.Session, fid, queryFilter);
						num = oneTimeSearch.ItemCount;
						base.WriteVariable("numMessagesFromName", num);
						if (num > 0)
						{
							base.WriteVariable("inFindMode", true);
							this.readAsFirstMessage = true;
							this.haveReadSavedMessages = false;
							if (this.pendingDeletionObjectId != null)
							{
								this.CommitPendingDeletion();
							}
							base.WriteVariable("canUndelete", false);
							if (this.findByNameResults != null)
							{
								this.findByNameResults.Dispose();
							}
							this.findByNameResults = oneTimeSearch;
							oneTimeSearch = null;
							this.voiceMessageList.InitializeCurrentPagerList(new MessageItemList(this.caller, this.findByNameResults.FolderId, MessageItemListSortType.LifoVoicemail, RetrieveVoicemailManager.viewProperties));
						}
					}
					else
					{
						PIIMessage data = PIIMessage.Create(PIIType._PII, this.caller.ADRecipient.DistinguishedName);
						CallIdTracer.TraceDebug(ExTraceGlobals.EmailTracer, this, data, "Unable to generate the search filter for Contact or Gal user with id {0} because neither displayname nor email properties are populated. Caller _PII. ", new object[]
						{
							contactSearchItem.Id ?? "not available"
						});
						base.WriteVariable("numMessagesFromName", 0);
					}
				}
			}
			finally
			{
				if (mailboxSessionLock != null)
				{
					mailboxSessionLock.Dispose();
					mailboxSessionLock = null;
				}
				if (num == 0)
				{
					base.WriteVariable("inFindMode", false);
				}
				if (oneTimeSearch != null)
				{
					oneTimeSearch.Dispose();
					oneTimeSearch = null;
				}
			}
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0003BBFC File Offset: 0x00039DFC
		private void CommitPendingDeletion()
		{
			if (this.caller != null && this.pendingDeletionObjectId != null)
			{
				using (UMMailboxRecipient.MailboxSessionLock mailboxSessionLock = this.caller.CreateSessionLock())
				{
					mailboxSessionLock.Session.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
					{
						this.pendingDeletionObjectId
					});
				}
			}
		}

		// Token: 0x04000A80 RID: 2688
		private static PropertyDefinition[] viewProperties = new PropertyDefinition[]
		{
			MessageItemSchema.IsRead
		};

		// Token: 0x04000A81 RID: 2689
		private static PropertyDefinition[] voiceMessageProperties = new PropertyDefinition[]
		{
			ItemSchema.ReceivedTime,
			ItemSchema.Importance,
			MessageItemSchema.SenderTelephoneNumber,
			MessageItemSchema.VoiceMessageSenderName,
			MessageItemSchema.VoiceMessageDuration
		};

		// Token: 0x04000A82 RID: 2690
		private VoiceMessageList voiceMessageList = new VoiceMessageList();

		// Token: 0x04000A83 RID: 2691
		private ContactInfo currentSender;

		// Token: 0x04000A84 RID: 2692
		private uint numDeletions;

		// Token: 0x04000A85 RID: 2693
		private UMSubscriber caller;

		// Token: 0x04000A86 RID: 2694
		private OneTimeSearch findByNameResults;

		// Token: 0x04000A87 RID: 2695
		private StoreObjectId voicemailSearchFolderId;

		// Token: 0x04000A88 RID: 2696
		private Importance currentMessageImportance;

		// Token: 0x04000A89 RID: 2697
		private bool readAsFirstMessage;

		// Token: 0x04000A8A RID: 2698
		private bool readingSavedMessages;

		// Token: 0x04000A8B RID: 2699
		private bool haveReadSavedMessages;

		// Token: 0x04000A8C RID: 2700
		private StoreObjectId pendingDeletionObjectId;

		// Token: 0x04000A8D RID: 2701
		private bool drmIsEnabled;

		// Token: 0x020001CA RID: 458
		internal class ConfigClass : ActivityManagerConfig
		{
			// Token: 0x06000D6A RID: 3434 RVA: 0x0003BCB7 File Offset: 0x00039EB7
			public ConfigClass(ActivityManagerConfig manager) : base(manager)
			{
			}

			// Token: 0x06000D6B RID: 3435 RVA: 0x0003BCC0 File Offset: 0x00039EC0
			internal override ActivityManager CreateActivityManager(ActivityManager manager)
			{
				CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Constructing Retrieve Voicemail activity manager.", new object[0]);
				return new RetrieveVoicemailManager(manager, this);
			}
		}
	}
}
