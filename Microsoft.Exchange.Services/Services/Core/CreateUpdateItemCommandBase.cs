using System;
using System.Globalization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002BF RID: 703
	internal abstract class CreateUpdateItemCommandBase<RequestType, SingleItemType> : MultiStepServiceCommand<RequestType, SingleItemType> where RequestType : BaseRequest
	{
		// Token: 0x06001311 RID: 4881 RVA: 0x0005D12F File Offset: 0x0005B32F
		public CreateUpdateItemCommandBase(CallContext callContext, RequestType request) : base(callContext, request)
		{
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06001312 RID: 4882 RVA: 0x0005D145 File Offset: 0x0005B345
		// (remove) Token: 0x06001313 RID: 4883 RVA: 0x0005D15E File Offset: 0x0005B35E
		public event Action<MessageItem> BeforeMessageDisposition
		{
			add
			{
				this.beforeMessageDispositionEventHandler = (Action<MessageItem>)Delegate.Combine(this.beforeMessageDispositionEventHandler, value);
			}
			remove
			{
				this.beforeMessageDispositionEventHandler = (Action<MessageItem>)Delegate.Remove(this.beforeMessageDispositionEventHandler, value);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06001314 RID: 4884 RVA: 0x0005D177 File Offset: 0x0005B377
		// (remove) Token: 0x06001315 RID: 4885 RVA: 0x0005D190 File Offset: 0x0005B390
		public event Action<MessageItem> AfterMessageDisposition
		{
			add
			{
				this.afterMessageDispositionEventHandler = (Action<MessageItem>)Delegate.Combine(this.afterMessageDispositionEventHandler, value);
			}
			remove
			{
				this.afterMessageDispositionEventHandler = (Action<MessageItem>)Delegate.Remove(this.afterMessageDispositionEventHandler, value);
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06001316 RID: 4886 RVA: 0x0005D1A9 File Offset: 0x0005B3A9
		public IdAndSession SaveToFolderIdAndSession
		{
			get
			{
				return this.saveToFolderIdAndSession;
			}
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0005D1B4 File Offset: 0x0005B3B4
		protected ItemType ExecuteOperation(Item item, ItemResponseShape responseShape, ConflictResolutionType conflictResolutionType, out ConflictResolutionResult conflictResolutionResult)
		{
			MessageItem messageItem = null;
			CalendarItemBase calendarItem = null;
			ItemType itemType;
			if (XsoDataConverter.TryGetStoreObject<MessageItem>(item, out messageItem))
			{
				itemType = this.ExecuteMessageOperation(messageItem, responseShape, conflictResolutionType, out conflictResolutionResult);
			}
			else if (XsoDataConverter.TryGetStoreObject<CalendarItemBase>(item, out calendarItem))
			{
				conflictResolutionResult = this.ExecuteCalendarOperation(calendarItem, conflictResolutionType);
				item.Load();
				itemType = new EwsCalendarItemType();
				base.LoadServiceObject(itemType, item, IdAndSession.CreateFromItem(item), responseShape);
			}
			else
			{
				itemType = this.ExecuteItemOperation(item, conflictResolutionType, responseShape, out conflictResolutionResult);
			}
			return itemType;
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0005D220 File Offset: 0x0005B420
		protected ConflictResolutionResult SaveThenSendMeetingMessages(CalendarItemBase calendarItem, bool isToAllAttendees, bool copyToSentItems, ConflictResolutionType conflictResolutionType)
		{
			calendarItem.SetClientIntentBasedOnModifiedProperties(null);
			ConflictResolutionResult result = base.SaveXsoItem(calendarItem, conflictResolutionType);
			calendarItem.SendMeetingMessages(isToAllAttendees, null, false, copyToSentItems, null, null);
			return result;
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0005D31C File Offset: 0x0005B51C
		protected ConflictResolutionResult SaveWhileSendingMeetingMessages(CalendarItem calendarItem, bool isToAllAttendees, bool copyToSentItems, ConflictResolutionType conflictResolutionType)
		{
			calendarItem.SetClientIntentBasedOnModifiedProperties(null);
			return base.SaveXsoItem(calendarItem, delegate(SaveMode saveModeDelegate)
			{
				calendarItem.SaveModeOnSendMeetingMessages = saveModeDelegate;
				calendarItem.SendMeetingMessages(isToAllAttendees, null, false, copyToSentItems, null, null);
				if (conflictResolutionType == ConflictResolutionType.AutoResolve)
				{
					return new ConflictResolutionResult(SaveResult.SuccessWithConflictResolution, new PropertyConflict[]
					{
						new PropertyConflict(StoreObjectSchema.LastModifiedTime, DateTime.Now.AddSeconds(-2.0), DateTime.Now.AddSeconds(-1.0), DateTime.Now, DateTime.Now, true)
					});
				}
				return new ConflictResolutionResult(SaveResult.Success, null);
			}, conflictResolutionType, null);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0005D380 File Offset: 0x0005B580
		protected ConflictResolutionResult SendMeetingMessageOnUpdate(CalendarItemBase calendarItemBase, bool isToAllAttendees, bool copyToSentItems, ConflictResolutionType conflictResolutionType)
		{
			CalendarItem calendarItem = calendarItemBase as CalendarItem;
			if (calendarItem == null)
			{
				return this.SaveThenSendMeetingMessages(calendarItemBase, isToAllAttendees, copyToSentItems, conflictResolutionType);
			}
			return this.SaveWhileSendingMeetingMessages(calendarItem, isToAllAttendees, copyToSentItems, conflictResolutionType);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0005D3B0 File Offset: 0x0005B5B0
		protected virtual ItemType ExecuteItemOperation(Item item, ConflictResolutionType conflictResolutionType, ItemResponseShape responseShape, out ConflictResolutionResult conflictResolutionResult)
		{
			RightsManagedMessageItem rightsManagedMessageItem = item as RightsManagedMessageItem;
			bool flag = rightsManagedMessageItem != null && rightsManagedMessageItem.IsDecoded;
			conflictResolutionResult = base.SaveXsoItem(item, conflictResolutionType);
			if (flag)
			{
				IrmUtils.DecodeIrmMessage(item.Session, item, true);
			}
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(item.Id);
			ItemType itemType = ItemType.CreateFromStoreObjectType(storeObjectId.ObjectType);
			ToServiceObjectPropertyList toServiceObjectPropertyList = XsoDataConverter.GetToServiceObjectPropertyList(item, responseShape);
			toServiceObjectPropertyList.CharBuffer = this.charBuffer;
			ServiceCommandBase.LoadServiceObject(itemType, item, IdAndSession.CreateFromItem(item), responseShape, toServiceObjectPropertyList);
			return itemType;
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0005D490 File Offset: 0x0005B690
		protected ItemType ExecuteMessageOperation(MessageItem messageItem, ItemResponseShape responseShape, ConflictResolutionType conflictResolutionType, out ConflictResolutionResult conflictResolutionResult)
		{
			CreateUpdateItemCommandBase<RequestType, SingleItemType>.<>c__DisplayClass4 CS$<>8__locals1 = new CreateUpdateItemCommandBase<RequestType, SingleItemType>.<>c__DisplayClass4();
			CS$<>8__locals1.messageItem = messageItem;
			CS$<>8__locals1.conflictResolutionType = conflictResolutionType;
			CS$<>8__locals1.<>4__this = this;
			ItemType result = null;
			conflictResolutionResult = new ConflictResolutionResult(SaveResult.Success, null);
			CS$<>8__locals1.mailboxSession = (CS$<>8__locals1.messageItem.Session as MailboxSession);
			this.OnBeforeMessageDisposition(CS$<>8__locals1.messageItem);
			switch (this.messageDisposition.Value)
			{
			case MessageDispositionType.SendOnly:
				this.SendMessageItemWithoutSaving(CS$<>8__locals1.messageItem, CS$<>8__locals1.mailboxSession);
				break;
			case MessageDispositionType.SaveOnly:
				result = this.SaveMessageItem(CS$<>8__locals1.messageItem, responseShape, CS$<>8__locals1.conflictResolutionType, out conflictResolutionResult);
				break;
			case MessageDispositionType.SendAndSaveCopy:
			{
				StoreObjectId sentItemsFolderId = this.GetSentItemsFolderId(CS$<>8__locals1.messageItem);
				try
				{
					CreateItemRequest createItemRequest = base.Request as CreateItemRequest;
					UpdateItemRequest updateItemRequest = base.Request as UpdateItemRequest;
					if ((createItemRequest != null && createItemRequest.ItemShape != null) || (updateItemRequest != null && updateItemRequest.ItemShape != null))
					{
						result = this.LoadItemAfterOperation(CS$<>8__locals1.messageItem, responseShape, delegate
						{
							CS$<>8__locals1.<>4__this.SendMessage(CS$<>8__locals1.messageItem, CS$<>8__locals1.mailboxSession, sentItemsFolderId, CS$<>8__locals1.conflictResolutionType);
							CS$<>8__locals1.messageItem.Load();
						});
					}
					else
					{
						this.SendMessage(CS$<>8__locals1.messageItem, CS$<>8__locals1.mailboxSession, sentItemsFolderId, CS$<>8__locals1.conflictResolutionType);
					}
				}
				catch (AccessDeniedException innerException)
				{
					if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1))
					{
						throw new ServiceAccessDeniedException(CoreResources.IDs.MessageInsufficientPermissionsToSend, innerException);
					}
					throw;
				}
				catch (LocalizedException localizedException)
				{
					this.DeleteDraftCreatedOnSendFailure(localizedException, CS$<>8__locals1.mailboxSession, CS$<>8__locals1.messageItem.Id);
					throw;
				}
				break;
			}
			}
			this.OnAfterMessageDisposition(CS$<>8__locals1.messageItem);
			return result;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x0005D660 File Offset: 0x0005B860
		protected IdAndSession GetMessageParentFolderIdAndSession()
		{
			if (base.CallContext.AccessingPrincipal == null && this.messageDisposition != null && this.messageDisposition.Value == MessageDispositionType.SendOnly)
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)2271901695U);
			}
			return this.saveToFolderIdAndSession ?? this.GetDefaultParentFolderIdAndSession(DefaultFolderType.Drafts);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x0005D6B8 File Offset: 0x0005B8B8
		protected void SetBodyCharsetOptions(Item item)
		{
			StoreSession session = this.GetMessageParentFolderIdAndSession().Session;
			if (session != null)
			{
				Charset preferredCharset = null;
				string name;
				BodyCharsetFlags bodyCharsetOptions = this.GetBodyCharsetOptions(out name);
				Charset.TryGetCharset(name, out preferredCharset);
				OutboundConversionOptions outboundConversionOptions = new OutboundConversionOptions(base.CallContext.DefaultDomain.DomainName.Domain);
				outboundConversionOptions.LoadPerOrganizationCharsetDetectionOptions(session.OrganizationId);
				outboundConversionOptions.DetectionOptions.PreferredCharset = preferredCharset;
				item.CharsetDetector.DetectionOptions = outboundConversionOptions.DetectionOptions;
				item.CharsetDetector.CharsetFlags = bodyCharsetOptions;
			}
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0005D73C File Offset: 0x0005B93C
		private BodyCharsetFlags GetBodyCharsetOptions(out string charsetName)
		{
			BodyCharsetFlags bodyCharsetFlags = BodyCharsetFlags.None;
			bool flag = false;
			bool flag2 = false;
			OutboundCharsetOptions outboundCharsetOptions = OutboundCharsetOptions.AutoDetect;
			if (base.Request is CreateItemRequest)
			{
				CreateItemRequest createItemRequest = base.Request as CreateItemRequest;
				flag = createItemRequest.UseGB18030;
				flag2 = createItemRequest.UseISO885915;
				outboundCharsetOptions = createItemRequest.OutboundCharsetOptions;
			}
			else if (base.Request is UpdateItemRequest)
			{
				UpdateItemRequest updateItemRequest = base.Request as UpdateItemRequest;
				flag = updateItemRequest.UseGB18030;
				flag2 = updateItemRequest.UseISO885915;
				outboundCharsetOptions = updateItemRequest.OutboundCharsetOptions;
			}
			if (flag)
			{
				bodyCharsetFlags |= BodyCharsetFlags.PreferGB18030;
			}
			if (flag2)
			{
				bodyCharsetFlags |= BodyCharsetFlags.PreferIso885915;
			}
			if (outboundCharsetOptions == OutboundCharsetOptions.AlwaysUTF8)
			{
				bodyCharsetFlags |= BodyCharsetFlags.DisableCharsetDetection;
				charsetName = "utf-8";
			}
			else
			{
				if (outboundCharsetOptions == OutboundCharsetOptions.UserLanguageChoice)
				{
					bodyCharsetFlags |= BodyCharsetFlags.DisableCharsetDetection;
				}
				else
				{
					bodyCharsetFlags = bodyCharsetFlags;
				}
				CultureInfo clientCulture = base.CallContext.ClientCulture;
				Culture culture = null;
				if (Culture.TryGetCulture(clientCulture.Name, out culture))
				{
					Charset mimeCharset = culture.MimeCharset;
					if (mimeCharset.IsAvailable)
					{
						charsetName = mimeCharset.Name;
						return bodyCharsetFlags;
					}
				}
				charsetName = Culture.Default.MimeCharset.Name;
			}
			return bodyCharsetFlags;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0005D84C File Offset: 0x0005BA4C
		private void OnBeforeMessageDisposition(MessageItem messageItem)
		{
			if (this.beforeMessageDispositionEventHandler != null)
			{
				this.beforeMessageDispositionEventHandler(messageItem);
			}
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x0005D862 File Offset: 0x0005BA62
		private void OnAfterMessageDisposition(MessageItem messageItem)
		{
			if (this.afterMessageDispositionEventHandler != null)
			{
				this.afterMessageDispositionEventHandler(messageItem);
			}
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0005D878 File Offset: 0x0005BA78
		private void SendMessage(MessageItem messageItem, MailboxSession mailboxSession, StoreObjectId sentItemsFolderId, ConflictResolutionType conflictResolutionType)
		{
			CreateItemRequest createItemRequest = base.Request as CreateItemRequest;
			if (createItemRequest != null && createItemRequest.IsNonDraft)
			{
				messageItem.MarkAsNonDraft();
			}
			SaveMode saveMode = base.GetSaveMode(conflictResolutionType);
			if (sentItemsFolderId.Equals(messageItem.ParentId))
			{
				messageItem.SendWithoutMovingMessage(sentItemsFolderId, saveMode);
				return;
			}
			messageItem.Send(sentItemsFolderId, saveMode);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x0005D8D0 File Offset: 0x0005BAD0
		private void SendMessageItemWithoutSaving(MessageItem messageItem, MailboxSession mailboxSession)
		{
			try
			{
				messageItem.SendWithoutSavingMessage();
			}
			catch (LocalizedException localizedException)
			{
				this.DeleteDraftCreatedOnSendFailure(localizedException, mailboxSession, messageItem.Id);
				throw;
			}
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x0005D930 File Offset: 0x0005BB30
		private ItemType SaveMessageItem(MessageItem messageItem, ItemResponseShape responseShape, ConflictResolutionType conflictResolutionType, out ConflictResolutionResult conflictResolutionResult)
		{
			ConflictResolutionResult localResult = null;
			CreateItemRequest createItemRequest = base.Request as CreateItemRequest;
			if (createItemRequest != null && createItemRequest.IsNonDraft)
			{
				messageItem.MarkAsNonDraft();
				if (base.CallContext.IsOwa)
				{
					messageItem[MessageItemSchema.NeedSpecialRecipientProcessing] = true;
				}
			}
			ItemType result = this.LoadItemAfterOperation(messageItem, responseShape, delegate
			{
				localResult = this.SaveXsoItem(messageItem, conflictResolutionType);
			});
			conflictResolutionResult = localResult;
			return result;
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x0005D9D0 File Offset: 0x0005BBD0
		protected Item GetItemForUpdate(IdAndSession idAndSession, bool sendOnNotFoundError)
		{
			Item result;
			try
			{
				result = ServiceCommandBase.GetXsoItemForUpdate(idAndSession, new PropertyDefinition[]
				{
					MessageItemSchema.Flags
				});
			}
			catch (ObjectNotFoundException)
			{
				if (!sendOnNotFoundError || (this.messageDisposition.Value != MessageDispositionType.SendAndSaveCopy && this.messageDisposition.Value != MessageDispositionType.SendOnly))
				{
					throw;
				}
				ExTraceGlobals.CreateItemCallTracer.TraceDebug<StoreId>((long)this.GetHashCode(), "CreateUpdateItemCommandBase.GetItemForUpdate: item to update {0} not found; Creating a new item.", idAndSession.Id);
				result = MessageItem.Create(idAndSession.Session, idAndSession.Session.GetDefaultFolderId(DefaultFolderType.Drafts));
			}
			return result;
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x0005DA60 File Offset: 0x0005BC60
		protected IdAndSession GetDefaultParentFolderIdAndSession(DefaultFolderType defaultFolderType)
		{
			return new IdAndSession(base.MailboxIdentityMailboxSession.GetRefreshedDefaultFolderId(defaultFolderType), base.MailboxIdentityMailboxSession);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x0005DA88 File Offset: 0x0005BC88
		private StoreObjectId GetSentItemsFolderId(MessageItem messageItem)
		{
			IdAndSession idAndSession;
			if (this.saveToFolderIdAndSession != null)
			{
				idAndSession = this.saveToFolderIdAndSession;
			}
			else
			{
				MailboxSession mailboxSession = messageItem.Session as MailboxSession;
				idAndSession = base.IdConverter.ConvertDefaultFolderType(DefaultFolderType.SentItems, mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
			}
			return idAndSession.GetAsStoreObjectId();
		}

		// Token: 0x06001328 RID: 4904
		protected abstract ConflictResolutionResult ExecuteCalendarOperation(CalendarItemBase calendarItem, ConflictResolutionType resolutionType);

		// Token: 0x06001329 RID: 4905 RVA: 0x0005DAE6 File Offset: 0x0005BCE6
		protected virtual void DeleteDraftCreatedOnSendFailure(LocalizedException localizedException, MailboxSession mailboxSession, StoreId storeId)
		{
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0005DAE8 File Offset: 0x0005BCE8
		protected void RequireMessageDisposition()
		{
			if (this.messageDisposition == null)
			{
				throw new MessageDispositionRequiredException();
			}
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0005DAFD File Offset: 0x0005BCFD
		protected bool IsSaveToFolderIdSessionPublicFolderSession()
		{
			return this.saveToFolderIdAndSession != null && this.saveToFolderIdAndSession.Session is PublicFolderSession;
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0005DB1C File Offset: 0x0005BD1C
		protected DelegateSessionHandleWrapper GetDelegateSessionHandleWrapperAndWorkflowContext(IdAndSession referenceIdAndSession, out IdAndSession adjustedIdAndSession, out string workflowMailboxSmtpAddress)
		{
			return this.GetDelegateSessionHandleWrapperAndWorkflowContext(referenceIdAndSession, false, out adjustedIdAndSession, out workflowMailboxSmtpAddress);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0005DB28 File Offset: 0x0005BD28
		protected DelegateSessionHandleWrapper GetDelegateSessionHandleWrapperAndWorkflowContext(IdAndSession referenceIdAndSession, bool checkSameAccountForOwnerLogon, out IdAndSession adjustedIdAndSession, out string workflowMailboxSmtpAddress)
		{
			MailboxSession mailboxSession = referenceIdAndSession.Session as MailboxSession;
			if (mailboxSession == null || base.CallContext.AccessingPrincipal == null || (mailboxSession.LogonType == LogonType.Owner && (!checkSameAccountForOwnerLogon || string.Equals(mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), base.CallContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString(), StringComparison.OrdinalIgnoreCase))) || mailboxSession.LogonType == LogonType.Admin || mailboxSession.LogonType == LogonType.SystemService || !ExchangeVersionDeterminer.MatchesLocalServerVersion(base.CallContext.AccessingPrincipal.MailboxInfo.Location.ServerVersion))
			{
				adjustedIdAndSession = referenceIdAndSession;
				workflowMailboxSmtpAddress = ((MailboxSession)adjustedIdAndSession.Session).MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
				return null;
			}
			this.LogDelegateSession(mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
			DelegateSessionHandleWrapper delegateSessionHandleWrapper = new DelegateSessionHandleWrapper(base.MailboxIdentityMailboxSession.GetDelegateSessionHandleForEWS(mailboxSession.MailboxOwner));
			adjustedIdAndSession = new IdAndSession(referenceIdAndSession.Id, delegateSessionHandleWrapper.Handle.MailboxSession);
			workflowMailboxSmtpAddress = base.MailboxIdentityMailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
			return delegateSessionHandleWrapper;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0005DC8C File Offset: 0x0005BE8C
		private ItemType LoadItemAfterOperation(MessageItem messageItem, ItemResponseShape responseShape, Action operation)
		{
			RightsManagedMessageItem rightsManagedMessageItem = messageItem as RightsManagedMessageItem;
			bool flag = rightsManagedMessageItem != null && rightsManagedMessageItem.IsDecoded;
			operation();
			if (flag)
			{
				IrmUtils.DecodeIrmMessage(messageItem.Session, messageItem, false);
			}
			ItemType itemType = ItemType.CreateFromStoreObjectType(messageItem.Id.ObjectId.ObjectType);
			ToServiceObjectPropertyList toServiceObjectPropertyList = XsoDataConverter.GetToServiceObjectPropertyList(messageItem, responseShape);
			toServiceObjectPropertyList.CharBuffer = this.charBuffer;
			ServiceCommandBase.LoadServiceObject(itemType, messageItem, IdAndSession.CreateFromItem(messageItem), responseShape, toServiceObjectPropertyList);
			itemType.Conversation = Util.LoadConversationUsingConversationId((ConversationId)PropertyCommand.GetPropertyValueFromStoreObject(messageItem, ItemSchema.ConversationId), responseShape.ConversationShapeName, responseShape.ConversationFolderId, base.IdConverter, this.GetHashCode(), base.CallContext.ProtocolLog);
			return itemType;
		}

		// Token: 0x04000D2A RID: 3370
		protected MessageDispositionType? messageDisposition = null;

		// Token: 0x04000D2B RID: 3371
		protected IdAndSession saveToFolderIdAndSession;

		// Token: 0x04000D2C RID: 3372
		protected char[] charBuffer;

		// Token: 0x04000D2D RID: 3373
		protected RmsTemplate rmsTemplate;

		// Token: 0x04000D2E RID: 3374
		private Action<MessageItem> beforeMessageDispositionEventHandler;

		// Token: 0x04000D2F RID: 3375
		private Action<MessageItem> afterMessageDispositionEventHandler;
	}
}
