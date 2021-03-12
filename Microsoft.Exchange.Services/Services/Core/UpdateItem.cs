using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.OAuth;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Exchange.Services.OnlineMeetings;
using Microsoft.Exchange.Services.OnlineMeetings.Autodiscover;
using Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000397 RID: 919
	internal sealed class UpdateItem : CreateUpdateItemCommandBase<UpdateItemRequest, UpdateItemResponseWrapper>
	{
		// Token: 0x060019BA RID: 6586 RVA: 0x00092CB0 File Offset: 0x00090EB0
		public UpdateItem(CallContext callContext, UpdateItemRequest request) : base(callContext, request)
		{
			OwsLogRegistry.Register(UpdateItem.UpdateItemActionName, typeof(CreateAndUpdateItemMetadata), new Type[]
			{
				typeof(GetParticipantOrDLFromAddressMetadata)
			});
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00092CF0 File Offset: 0x00090EF0
		internal override void PreExecuteCommand()
		{
			this.savedItemFolderId = base.Request.SavedItemFolderId;
			this.itemChanges = base.Request.ItemChanges;
			if (base.Request.ItemShape != null || base.Request.ShapeName != null)
			{
				this.responseShape = Global.ResponseShapeResolver.GetResponseShape<ItemResponseShape>(base.Request.ShapeName, base.Request.ItemShape, base.CallContext.FeaturesManager);
			}
			else
			{
				this.responseShape = ServiceCommandBase.DefaultItemResponseShape;
			}
			this.conflictResolutionType = base.Request.ConflictResolution;
			if (!string.IsNullOrEmpty(base.Request.SendCalendarInvitationsOrCancellations))
			{
				this.sendMeetingUpdates = new CalendarItemOperationType.Update?(SendMeetingInvitationsOrCancellations.ConvertToEnum(base.Request.SendCalendarInvitationsOrCancellations));
			}
			if (!string.IsNullOrEmpty(base.Request.MessageDisposition))
			{
				this.messageDisposition = new MessageDispositionType?(MessageDisposition.ConvertToEnum(base.Request.MessageDisposition));
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, CreateAndUpdateItemMetadata.MessageDisposition, this.messageDisposition);
			}
			if (!string.IsNullOrEmpty(base.Request.ComposeOperation))
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, CreateAndUpdateItemMetadata.ComposeOperation, base.Request.ComposeOperation);
			}
			ServiceCommandBase.ThrowIfNullOrEmpty<ItemChange>(this.itemChanges, "this.itemChanges", "UpdateItem::PreExecuteCommand");
			ServiceCommandBase.ThrowIfNull(this.responseShape, "this.responseShape", "UpdateItem::PreExecuteCommand");
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060019BC RID: 6588 RVA: 0x00092E55 File Offset: 0x00091055
		internal override int StepCount
		{
			get
			{
				if (this.itemChanges != null)
				{
					return this.itemChanges.Length;
				}
				return 0;
			}
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00092E6C File Offset: 0x0009106C
		internal override IExchangeWebMethodResponse GetResponse()
		{
			UpdateItemResponse updateItemResponse = new UpdateItemResponse();
			updateItemResponse.BuildForUpdateItemResults(base.Results);
			return updateItemResponse;
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x00092E8C File Offset: 0x0009108C
		private static void ValidateSettableNonDraftMeetingOrTaskRequestProperty(PropertyPath propertyPath, bool isMeetingRequest)
		{
			PropertyUriEnum[] array = isMeetingRequest ? UpdateItem.settableNonDraftMeetingRequestProperties : UpdateItem.settableNonDraftTaskRequestProperties;
			if (propertyPath is ExtendedPropertyUri)
			{
				return;
			}
			if (!(propertyPath is PropertyUri))
			{
				if (isMeetingRequest)
				{
					throw new SentMeetingRequestUpdateException();
				}
				throw new SentTaskRequestUpdateException();
			}
			else
			{
				PropertyUriEnum uri = ((PropertyUri)propertyPath).Uri;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == uri)
					{
						return;
					}
				}
				if (isMeetingRequest)
				{
					throw new SentMeetingRequestUpdateException();
				}
				throw new SentTaskRequestUpdateException();
			}
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x00092EF8 File Offset: 0x000910F8
		private static void ValidateIdForUpdate(IdAndSession idAndSession, ConflictResolutionType conflictResolutionType)
		{
			VersionedId versionedId = idAndSession.Id as VersionedId;
			if (conflictResolutionType == ConflictResolutionType.AutoResolve && versionedId == null)
			{
				throw new ChangeKeyRequiredException();
			}
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x00092F20 File Offset: 0x00091120
		private static void PromoteInlineAttachments(Item item, PropertyUpdate[] propertyUpdates)
		{
			bool flag = false;
			int i = 0;
			while (i < propertyUpdates.Length)
			{
				PropertyUpdate propertyUpdate = propertyUpdates[i];
				PropertyUri propertyUri = propertyUpdate.PropertyPath as PropertyUri;
				if (propertyUri != null && propertyUri.Uri == PropertyUriEnum.Body)
				{
					SetPropertyUpdate setPropertyUpdate = propertyUpdate as SetPropertyUpdate;
					if (setPropertyUpdate != null)
					{
						ItemType itemType = setPropertyUpdate.ServiceObject as ItemType;
						flag = (itemType.Body.BodyType == BodyType.Text);
						break;
					}
					break;
				}
				else
				{
					i++;
				}
			}
			if (!flag)
			{
				return;
			}
			Body body = IrmUtils.GetBody(item);
			if (body.Format == BodyFormat.TextPlain)
			{
				return;
			}
			AttachmentCollection attachmentCollection = IrmUtils.GetAttachmentCollection(item);
			foreach (AttachmentHandle handle in attachmentCollection)
			{
				using (Attachment attachment = attachmentCollection.Open(handle))
				{
					if (attachment.IsInline || attachment.RenderingPosition != -1)
					{
						attachment.IsInline = false;
						attachment.RenderingPosition = -1;
						attachment.Save();
					}
				}
			}
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00093038 File Offset: 0x00091238
		private static void MarkRefAttachAsInline(Item item)
		{
			AttachmentCollection attachmentCollection = IrmUtils.GetAttachmentCollection(item);
			foreach (AttachmentHandle attachmentHandle in attachmentCollection)
			{
				if (attachmentHandle.AttachMethod == 7)
				{
					using (Attachment attachment = attachmentCollection.Open(attachmentHandle))
					{
						attachment.IsInline = true;
					}
				}
			}
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x000930B0 File Offset: 0x000912B0
		internal override ServiceResult<UpdateItemResponseWrapper> Execute()
		{
			this.ComputeAndLogStatistics();
			if (this.savedItemFolderId != null && this.savedItemFolderId.BaseFolderId != null)
			{
				try
				{
					this.saveToFolderIdAndSession = base.IdConverter.ConvertFolderIdToIdAndSessionReadOnly(this.savedItemFolderId.BaseFolderId);
				}
				catch (ObjectNotFoundException innerException)
				{
					throw new SavedItemFolderNotFoundException(innerException);
				}
			}
			ServiceError serviceError = null;
			ItemChange itemChange = this.itemChanges[base.CurrentStep];
			if (itemChange != null && itemChange.ItemId != null && base.LogItemId())
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeAppendGenericInfo(RequestDetailsLogger.Current, "UpdateItemId: ", string.Format("{0}:{1}", itemChange.ItemId.GetId(), itemChange.ItemId.GetChangeKey()));
			}
			IdAndSession idAndSession = null;
			UpdateItemResponseWrapper updateItemResponseWrapper;
			if (this.TryGetItemChangeIdAndSession(itemChange.ItemId, out idAndSession))
			{
				updateItemResponseWrapper = this.UpdateItemFromItemChange(idAndSession, itemChange, out serviceError);
			}
			else
			{
				updateItemResponseWrapper = this.CreateAndSendItem(itemChange);
			}
			this.objectsChanged++;
			string value = (updateItemResponseWrapper != null && updateItemResponseWrapper.Item != null) ? updateItemResponseWrapper.Item.GetType().Name : "NA";
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, CreateAndUpdateItemMetadata.ActionType, value);
			if (updateItemResponseWrapper.Item is PostItemType && idAndSession != null && this.saveToFolderIdAndSession != null && this.saveToFolderIdAndSession.Session.IsPublicFolderSession)
			{
				idAndSession.Session.Move(this.saveToFolderIdAndSession.Session, StoreId.GetStoreObjectId(this.saveToFolderIdAndSession.Id), idAndSession.Session == this.saveToFolderIdAndSession.Session, new StoreId[]
				{
					StoreId.GetStoreObjectId(idAndSession.Id)
				});
			}
			if (serviceError == null)
			{
				return new ServiceResult<UpdateItemResponseWrapper>(updateItemResponseWrapper);
			}
			return new ServiceResult<UpdateItemResponseWrapper>(updateItemResponseWrapper, serviceError);
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x00093264 File Offset: 0x00091464
		protected override void LogDelegateSession(string principal)
		{
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, CreateAndUpdateItemMetadata.SessionType, LogonType.Delegated);
			if (!string.IsNullOrEmpty(principal))
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, CreateAndUpdateItemMetadata.Principal, principal);
			}
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00093298 File Offset: 0x00091498
		private bool TryGetItemChangeIdAndSession(BaseItemId itemId, out IdAndSession idAndSession)
		{
			idAndSession = null;
			try
			{
				if (this.conflictResolutionType == ConflictResolutionType.AlwaysOverwrite)
				{
					idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(itemId);
				}
				else
				{
					idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadWrite(itemId);
				}
			}
			catch (InvalidStoreIdException arg)
			{
				if (!base.Request.SendOnNotFoundError || (this.messageDisposition.Value != MessageDispositionType.SendAndSaveCopy && this.messageDisposition.Value != MessageDispositionType.SendOnly))
				{
					throw;
				}
				ExTraceGlobals.UpdateItemCallTracer.TraceDebug<InvalidStoreIdException>((long)this.GetHashCode(), "UpdateItem.GetItemChangeIdAndSession: item to update's itemId is invalid; new message will be created, exception: {0}", arg);
			}
			return idAndSession != null;
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00093330 File Offset: 0x00091530
		private void ComputeAndLogStatistics()
		{
			bool flag = false;
			foreach (PropertyUpdate propertyUpdate in this.itemChanges[base.CurrentStep].PropertyUpdates)
			{
				SetItemPropertyUpdate setItemPropertyUpdate = propertyUpdate as SetItemPropertyUpdate;
				if (setItemPropertyUpdate != null)
				{
					MessageType messageType = setItemPropertyUpdate.Item as MessageType;
					if (messageType != null)
					{
						this.totalNbRecipients += ((messageType.ToRecipients != null) ? messageType.ToRecipients.Length : 0) + ((messageType.CcRecipients != null) ? messageType.CcRecipients.Length : 0) + ((messageType.BccRecipients != null) ? messageType.BccRecipients.Length : 0);
						if (messageType.Body != null && messageType.Body.Value != null)
						{
							this.totalBodySize += messageType.Body.Value.Length;
						}
						if (!flag)
						{
							this.totalNbMessages++;
							flag = true;
						}
					}
				}
			}
			if (base.CurrentStep + 1 == this.itemChanges.Length && this.totalNbMessages > 0)
			{
				RequestDetailsLogger requestDetailsLogger = RequestDetailsLogger.Current;
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(requestDetailsLogger, CreateAndUpdateItemMetadata.TotalNbMessages, this.totalNbMessages);
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(requestDetailsLogger, CreateAndUpdateItemMetadata.TotalNbRecipients, this.totalNbRecipients);
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(requestDetailsLogger, CreateAndUpdateItemMetadata.TotalBodySize, this.totalBodySize);
			}
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00093488 File Offset: 0x00091688
		private UpdateItemResponseWrapper UpdateItemFromItemChange(IdAndSession idAndSession, ItemChange itemChange, out ServiceError warning)
		{
			warning = null;
			if (itemChange.ChangesAlreadyProcessed)
			{
				return this.GenerateAlreadyProcessedResult(idAndSession);
			}
			if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
			{
				ExTimeZone exTimeZone = RecurrenceHelper.MeetingTimeZone.GetLastMeetingTimeZone(itemChange.PropertyUpdates, out warning);
				if (RecurrenceHelper.RequestTimeZone.NeedTimeZoneContextForTask(idAndSession))
				{
					exTimeZone = EWSSettings.RequestTimeZone;
				}
				else
				{
					exTimeZone = RecurrenceHelper.MeetingTimeZone.GetLastMeetingTimeZone(itemChange.PropertyUpdates, out warning);
				}
				idAndSession.Session.ExTimeZone = exTimeZone;
				if (base.CallContext.AccessingPrincipal != null && ExchangeVersionDeterminer.MatchesLocalServerVersion(base.CallContext.AccessingPrincipal.MailboxInfo.Location.ServerVersion))
				{
					base.MailboxIdentityMailboxSession.ExTimeZone = exTimeZone;
				}
			}
			UpdateItemResponseWrapper result;
			using (DelegateSessionHandleWrapper delegateSessionHandleWrapper = base.GetDelegateSessionHandleWrapper(idAndSession))
			{
				if (delegateSessionHandleWrapper != null)
				{
					idAndSession = new IdAndSession(idAndSession.Id, delegateSessionHandleWrapper.Handle.MailboxSession);
				}
				UpdateItem.ValidateIdForUpdate(idAndSession, this.conflictResolutionType);
				Item itemForUpdate;
				Item storeItem = itemForUpdate = base.GetItemForUpdate(idAndSession, base.Request.SendOnNotFoundError);
				try
				{
					result = this.ApplyUpdateAndExecuteOperation(storeItem, itemChange, idAndSession.Session);
				}
				finally
				{
					if (itemForUpdate != null)
					{
						((IDisposable)itemForUpdate).Dispose();
					}
				}
			}
			return result;
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x000935B4 File Offset: 0x000917B4
		private UpdateItemResponseWrapper CreateAndSendItem(ItemChange itemChange)
		{
			ExTraceGlobals.UpdateItemCallTracer.TraceDebug((long)this.GetHashCode(), "UpdateItem.CreateAndSendItem: itemId was invalid; Creating a new item.");
			IdAndSession defaultParentFolderIdAndSession = base.GetDefaultParentFolderIdAndSession(DefaultFolderType.Drafts);
			UpdateItemResponseWrapper result;
			using (MessageItem messageItem = MessageItem.Create(defaultParentFolderIdAndSession.Session, defaultParentFolderIdAndSession.Id))
			{
				result = this.ApplyUpdateAndExecuteOperation(messageItem, itemChange, defaultParentFolderIdAndSession.Session);
			}
			return result;
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00093620 File Offset: 0x00091820
		private UpdateItemResponseWrapper ApplyUpdateAndExecuteOperation(Item storeItem, ItemChange itemChange, StoreSession session)
		{
			this.ValidateUpdate(storeItem, itemChange.PropertyUpdates);
			if (base.CallContext.IsOwa)
			{
				base.SetBodyCharsetOptions(storeItem);
			}
			MessageItem messageItem = storeItem as MessageItem;
			if (base.Request.ClientSupportsIrm && messageItem != null)
			{
				RightsManagedMessageItem rightsManagedMessageItem = storeItem as RightsManagedMessageItem;
				if (rightsManagedMessageItem != null)
				{
					MailboxSession mailboxSession = IrmUtils.ValidateAndGetMailboxSession(session);
					OrganizationId organizationId = mailboxSession.MailboxOwner.MailboxInfo.OrganizationId;
					this.ThrowIfNotEnabledForTenant(organizationId);
					if (rightsManagedMessageItem.IsRestricted && !rightsManagedMessageItem.IsDecoded)
					{
						rightsManagedMessageItem.Decode(IrmUtils.GetOutboundConversionOptions(organizationId), true);
					}
				}
				else if (IrmUtils.IsApplyingRmsTemplate(base.Request.ComplianceId, session, out this.rmsTemplate))
				{
					OutboundConversionOptions outboundConversionOptions = IrmUtils.GetOutboundConversionOptions(((MailboxSession)session).MailboxOwner.MailboxInfo.OrganizationId);
					storeItem = RightsManagedMessageItem.Create(messageItem, outboundConversionOptions);
				}
			}
			if (!string.IsNullOrEmpty(base.Request.ComplianceId))
			{
				IrmUtils.UpdateCompliance(base.Request.ComplianceId, storeItem, this.rmsTemplate);
			}
			if (base.Request.PromoteInlineAttachments)
			{
				UpdateItem.PromoteInlineAttachments(storeItem, itemChange.PropertyUpdates);
			}
			if (!string.IsNullOrEmpty(base.Request.InternetMessageId))
			{
				storeItem[ItemSchema.InternetMessageId] = base.Request.InternetMessageId;
			}
			if (!(storeItem is CalendarItemBase) || itemChange.PropertyUpdates.Length != 0)
			{
				if (base.CallContext.FeaturesManager != null && base.CallContext.FeaturesManager.IsFeatureSupported("AttachmentsFilePicker"))
				{
					UpdateItem.MarkRefAttachAsInline(storeItem);
				}
				this.UpdateProperties(storeItem, itemChange.PropertyUpdates, base.Request.SuppressReadReceipts, base.CallContext.FeaturesManager);
			}
			this.PreventMeetingUpdateFromDeletedItem(storeItem);
			this.HandleUpdatesForOnlineMeeting(storeItem, itemChange.PropertyUpdates);
			this.HandleUpdatesForDlpPolicytips(storeItem, itemChange.PropertyUpdates);
			ConflictResolutionResult conflictResolutionResult;
			ItemType item = base.ExecuteOperation(storeItem, this.responseShape, this.conflictResolutionType, out conflictResolutionResult);
			ConflictResults conflictResults = new ConflictResults();
			if (conflictResolutionResult != null && conflictResolutionResult.SaveStatus == SaveResult.SuccessWithConflictResolution && conflictResolutionResult.PropertyConflicts != null)
			{
				conflictResults.Count = conflictResolutionResult.PropertyConflicts.Length;
			}
			return new UpdateItemResponseWrapper(item, conflictResults);
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x00093838 File Offset: 0x00091A38
		private void ValidateUpdate(Item storeItem, PropertyUpdate[] propertyUpdates)
		{
			this.ValidateOperationAttributes(storeItem);
			MessageItem messageItem;
			if (XsoDataConverter.TryGetStoreObject<MessageItem>(storeItem, out messageItem) && !messageItem.IsDraft)
			{
				if (storeItem is MeetingRequest)
				{
					foreach (PropertyUpdate propertyUpdate in propertyUpdates)
					{
						UpdateItem.ValidateSettableNonDraftMeetingOrTaskRequestProperty(propertyUpdate.PropertyPath, true);
					}
					return;
				}
				if (storeItem is TaskRequest)
				{
					foreach (PropertyUpdate propertyUpdate2 in propertyUpdates)
					{
						UpdateItem.ValidateSettableNonDraftMeetingOrTaskRequestProperty(propertyUpdate2.PropertyPath, false);
					}
				}
			}
		}

		// Token: 0x060019CA RID: 6602 RVA: 0x000938BC File Offset: 0x00091ABC
		private void ValidateOperationAttributes(StoreObject storeObject)
		{
			MessageItem messageItem = null;
			CalendarItemBase calendarItemBase = null;
			if (XsoDataConverter.TryGetStoreObject<MessageItem>(storeObject, out messageItem))
			{
				this.ValidateMessageOperationAttribute(messageItem);
				return;
			}
			if (XsoDataConverter.TryGetStoreObject<CalendarItemBase>(storeObject, out calendarItemBase))
			{
				this.ValidateCalendarOperationAttribute(calendarItemBase);
			}
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x000938F0 File Offset: 0x00091AF0
		private void ValidateMessageOperationAttribute(MessageItem messageItem)
		{
			base.CallContext.AuthZBehavior.OnUpdateMessageItem(messageItem);
			base.RequireMessageDisposition();
			if (this.messageDisposition.Value != MessageDispositionType.SaveOnly)
			{
				if (messageItem.Session is PublicFolderSession)
				{
					throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorCannotSendMessageFromPublicFolder);
				}
				if (ServiceCommandBase.IsAssociated(messageItem))
				{
					throw new ServiceInvalidOperationException((CoreResources.IDs)3281131813U);
				}
			}
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x00093958 File Offset: 0x00091B58
		private void ValidateCalendarOperationAttribute(CalendarItemBase calendarItemBase)
		{
			if (this.sendMeetingUpdates == null)
			{
				throw new SendMeetingInvitationsOrCancellationsRequiredException();
			}
			if (this.sendMeetingUpdates != CalendarItemOperationType.Update.SendToNone && calendarItemBase.Session is PublicFolderSession)
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)2990730164U);
			}
		}

		// Token: 0x060019CD RID: 6605 RVA: 0x000939B4 File Offset: 0x00091BB4
		protected override ItemType ExecuteItemOperation(Item item, ConflictResolutionType conflictResolutionType, ItemResponseShape responseShape, out ConflictResolutionResult conflictResolutionResult)
		{
			Task task = item as Task;
			if (task != null)
			{
				using (Task task2 = this.SaveTask(task, conflictResolutionType, out conflictResolutionResult))
				{
					task2.Load();
					TaskType taskType = new TaskType();
					base.LoadServiceObject(taskType, task2, IdAndSession.CreateFromItem(task2), responseShape);
					return taskType;
				}
			}
			return base.ExecuteItemOperation(item, conflictResolutionType, responseShape, out conflictResolutionResult);
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x00093A38 File Offset: 0x00091C38
		private Task SaveTask(Task task, ConflictResolutionType conflictResolutionType, out ConflictResolutionResult conflictResolutionResult)
		{
			StoreObjectId oneOffTaskId = null;
			conflictResolutionResult = base.ExecuteItemSave((SaveMode saveMode) => task.Save(saveMode, out oneOffTaskId), conflictResolutionType);
			if (oneOffTaskId == null)
			{
				return task;
			}
			return (Task)ServiceCommandBase.GetXsoItem(task.Session, oneOffTaskId, new PropertyDefinition[0]);
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x00093AA0 File Offset: 0x00091CA0
		protected override ConflictResolutionResult ExecuteCalendarOperation(CalendarItemBase calendarItemBase, ConflictResolutionType resolutionType)
		{
			CalendarItemOperationType.Update update = this.sendMeetingUpdates.Value;
			MailboxSession mailboxSession = (base.SaveToFolderIdAndSession != null) ? (base.SaveToFolderIdAndSession.Session as MailboxSession) : null;
			if (mailboxSession != null && mailboxSession.IsGroupMailbox())
			{
				Participant participant = new Participant(mailboxSession.MailboxOwner);
				calendarItemBase.AttendeeCollection.Add(participant, AttendeeType.Required, null, null, true);
				update = CalendarItemOperationType.Update.SendToAllAndSaveCopy;
			}
			ConflictResolutionResult result;
			if (ServiceCommandBase.IsOrganizerMeeting(calendarItemBase))
			{
				switch (update)
				{
				case CalendarItemOperationType.Update.SendToNone:
					calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(62623U, LastChangeAction.ExecuteEWSCalendarOperation);
					result = base.SaveXsoItem(calendarItemBase, resolutionType);
					break;
				case CalendarItemOperationType.Update.SendOnlyToAll:
					calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(52383U, LastChangeAction.ExecuteEWSCalendarOperation);
					result = base.SendMeetingMessageOnUpdate(calendarItemBase, true, false, resolutionType);
					break;
				case CalendarItemOperationType.Update.SendOnlyToChanged:
					calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(46239U, LastChangeAction.ExecuteEWSCalendarOperation);
					result = base.SendMeetingMessageOnUpdate(calendarItemBase, false, false, resolutionType);
					break;
				case CalendarItemOperationType.Update.SendToAllAndSaveCopy:
					calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(60575U, LastChangeAction.ExecuteEWSCalendarOperation);
					result = base.SendMeetingMessageOnUpdate(calendarItemBase, true, true, resolutionType);
					break;
				case CalendarItemOperationType.Update.SendToChangedAndSaveCopy:
					calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(35999U, LastChangeAction.ExecuteEWSCalendarOperation);
					result = base.SendMeetingMessageOnUpdate(calendarItemBase, false, true, resolutionType);
					break;
				default:
					throw new CalendarExceptionInvalidAttributeValue(new PropertyUri(PropertyUriEnum.CalendarItemType));
				}
			}
			else
			{
				calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(38047U, LastChangeAction.ExecuteEWSCalendarOperation);
				result = base.SaveXsoItem(calendarItemBase, resolutionType);
			}
			return result;
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x00093C0C File Offset: 0x00091E0C
		private void PreventMeetingUpdateFromDeletedItem(Item storeItem)
		{
			CalendarItemBase calendarItemBase = storeItem as CalendarItemBase;
			if (calendarItemBase == null)
			{
				return;
			}
			bool flag = false;
			if (ServiceCommandBase.IsOrganizerMeeting(calendarItemBase))
			{
				switch (this.sendMeetingUpdates.Value)
				{
				case CalendarItemOperationType.Update.SendToNone:
					break;
				case CalendarItemOperationType.Update.SendOnlyToAll:
				case CalendarItemOperationType.Update.SendOnlyToChanged:
				case CalendarItemOperationType.Update.SendToAllAndSaveCopy:
				case CalendarItemOperationType.Update.SendToChangedAndSaveCopy:
					flag = true;
					break;
				default:
					throw new CalendarExceptionInvalidAttributeValue(new PropertyUri(PropertyUriEnum.CalendarItemType));
				}
			}
			if (flag)
			{
				IdAndSession idAndSession = base.IdConverter.ConvertDefaultFolderType(DefaultFolderType.DeletedItems, ((MailboxSession)calendarItemBase.Session).MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				bool flag2 = calendarItemBase.ParentId.Equals(idAndSession.Id);
				if (flag2)
				{
					throw new CalendarExceptionCannotUpdateDeletedItem();
				}
			}
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x00093CC4 File Offset: 0x00091EC4
		private UpdateItemResponseWrapper GenerateAlreadyProcessedResult(IdAndSession idAndSession)
		{
			ConflictResults conflictResults = new ConflictResults
			{
				Count = 0
			};
			StoreObjectId asStoreObjectId = idAndSession.GetAsStoreObjectId();
			ItemType itemType = ItemType.CreateFromStoreObjectType(asStoreObjectId.ObjectType);
			ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(asStoreObjectId, idAndSession, null);
			itemType.ItemId = new ItemId
			{
				Id = concatenatedId.Id,
				ChangeKey = concatenatedId.ChangeKey
			};
			return new UpdateItemResponseWrapper(itemType, conflictResults);
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x00093D34 File Offset: 0x00091F34
		internal static void UpdateCalendarItemWithOnlineMeetingProperties(CalendarItemBase calendarItem, OnlineMeetingResult result, CultureInfo preferredCulture, RequestDetailsLogger logger)
		{
			ExTraceGlobals.OnlineMeetingTracer.TraceFunction(0L, "[UpdateItem][UpdateCalendarItemWithOnlineMeetingProperties] Entering");
			calendarItem.OpenAsReadWrite();
			calendarItem.ConferenceTelURI = OutlookAddinAdapter.GetConferenceTelUri(result);
			calendarItem.ConferenceInfo = null;
			try
			{
				calendarItem.UCCapabilities = OutlookAddinAdapter.Serialize(OutlookAddinAdapter.GetUcCapabilities(result, preferredCulture));
			}
			catch (InvalidOperationException ex)
			{
				string text = string.Format("[UpdateItem.HandleUpdatesForOnlineMeeting] Unexpected serialization error when serializing to UC Calendar Item UCCapabilities property; Error: {0}", (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
				ExTraceGlobals.OnlineMeetingTracer.TraceError(0L, text);
				logger.AppendGenericInfo("UpdateOnlineMeetingError", text);
				return;
			}
			try
			{
				calendarItem.UCInband = OutlookAddinAdapter.Serialize(OutlookAddinAdapter.GetUCInband(result));
			}
			catch (InvalidOperationException ex2)
			{
				string text2 = string.Format("[UpdateItem.HandleUpdatesForOnlineMeeting] Unexpected serialization error when serializing to UC Calendar Item UCInband property; Error: {0}", (ex2.InnerException != null) ? ex2.InnerException.Message : ex2.Message);
				ExTraceGlobals.OnlineMeetingTracer.TraceError(0L, text2);
				logger.AppendGenericInfo("UpdateOnlineMeetingError", text2);
				return;
			}
			string text3 = string.Empty;
			try
			{
				text3 = OutlookAddinAdapter.Serialize(OutlookAddinAdapter.GetUCMeetingSetting(result));
			}
			catch (InvalidOperationException ex3)
			{
				string message = string.Format("[UpdateItem.HandleUpdatesForOnlineMeeting] Unexpected serialization error when serializing to UC Calendar Item UCMeetingSetting property; Error: {0}", (ex3.InnerException != null) ? ex3.InnerException.Message : ex3.Message);
				ExTraceGlobals.OnlineMeetingTracer.TraceError(0L, message);
				return;
			}
			calendarItem.UCMeetingSetting = text3;
			calendarItem.UCMeetingSettingSent = text3;
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x00093EA4 File Offset: 0x000920A4
		internal static bool ShouldUpdateOnlineMeeting(CalendarItemBase calendarItem, PropertyUpdate[] propertyUpdates, RequestDetailsLogger logger, out string onlineMeetingId)
		{
			onlineMeetingId = string.Empty;
			if (calendarItem == null || !calendarItem.IsOrganizer() || string.IsNullOrEmpty(calendarItem.OnlineMeetingExternalLink))
			{
				return false;
			}
			bool isPublic;
			try
			{
				PropertyError propertyError = calendarItem.TryGetProperty(CalendarItemBaseSchema.UCMeetingSetting) as PropertyError;
				if (propertyError != null && (propertyError.PropertyErrorCode == PropertyErrorCode.NotEnoughMemory || propertyError.PropertyErrorCode == PropertyErrorCode.NotFound || propertyError.PropertyErrorCode == PropertyErrorCode.RequireStreamed))
				{
					string text = "[UpdateItem.ShouldUpdateOnlineMeeting] UCMeetingSetting unexpectedly does not exist";
					ExTraceGlobals.OnlineMeetingTracer.TraceError(0L, text);
					logger.AppendGenericInfo("UpdateOnlineMeetingError", text);
					return false;
				}
				object propertyValueFromStoreObject = PropertyCommand.GetPropertyValueFromStoreObject(calendarItem, CalendarItemBaseSchema.UCMeetingSetting);
				if (propertyValueFromStoreObject == null)
				{
					string text2 = "[UpdateItem.ShouldUpdateOnlineMeeting] UCMeetingSetting is unexpectedly null";
					ExTraceGlobals.OnlineMeetingTracer.TraceError(0L, text2);
					logger.AppendGenericInfo("UpdateOnlineMeetingError", text2);
					return false;
				}
				MeetingSetting meetingSetting = (MeetingSetting)OutlookAddinAdapter.Deserialize((string)propertyValueFromStoreObject, typeof(MeetingSetting));
				if (meetingSetting == null)
				{
					string text3 = "[UpdateItem.ShouldUpdateOnlineMeeting] MeetingSetting object unexpectedly null after deserialization";
					ExTraceGlobals.OnlineMeetingTracer.TraceError(0L, text3);
					logger.AppendGenericInfo("UpdateOnlineMeetingError", text3);
					return false;
				}
				onlineMeetingId = meetingSetting.ConferenceID;
				isPublic = meetingSetting.IsPublic;
			}
			catch (InvalidOperationException ex)
			{
				string text4 = string.Format("[UpdateItem.ShouldUpdateOnlineMeeting] Unexpected error occured while deserializing stored UCMeetingSetting component of calendar item: {0}", (ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
				ExTraceGlobals.OnlineMeetingTracer.TraceError(0L, text4);
				logger.AppendGenericInfo("UpdateOnlineMeetingError", text4);
				return false;
			}
			if (isPublic)
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceInformation(0, 0L, "[UpdateItem.ShouldUpdateOnlineMeeting] OnlineMeeting counterpart is public, no need to update with UCWA");
				return false;
			}
			for (int i = 0; i < propertyUpdates.Length; i++)
			{
				PropertyUri propertyUri = propertyUpdates[i].PropertyPath as PropertyUri;
				if (propertyUri != null)
				{
					PropertyUriEnum uri = propertyUri.Uri;
					if (uri == PropertyUriEnum.Subject || uri == PropertyUriEnum.End || uri == PropertyUriEnum.Recurrence)
					{
						ExTraceGlobals.OnlineMeetingTracer.TraceInformation<PropertyUriEnum>(0, 0L, "[UpdateItem.ShouldUpdateOnlineMeeting] Property '{0}' has been modified; UCWA OnlineMeeting needs to be updated", propertyUri.Uri);
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000940BC File Offset: 0x000922BC
		internal static string GetSipAddress(ProxyAddressCollection proxyAddresses, RequestDetailsLogger logger)
		{
			IEnumerable<string> source = from proxyAddress in proxyAddresses
			where string.Compare(proxyAddress.PrefixString, ProxyAddressPrefix.SIP.ToString(), StringComparison.OrdinalIgnoreCase) == 0
			select proxyAddress.ValueString;
			List<string> list = source.ToList<string>();
			if (list.Count == 0)
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceDebug<string>(0, 0L, "[UpdateItem.GetSipAddress] No sip addresses were found in proxy address collection: '{0}'", string.Join(",", proxyAddresses.ToStringArray()));
				return string.Empty;
			}
			return list[0];
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x00094150 File Offset: 0x00092350
		private void HandleUpdatesForDlpPolicytips(StoreObject storeItem, PropertyUpdate[] propertyUpdates)
		{
			if (storeItem is MessageItem || storeItem is CalendarItem)
			{
				for (int i = 0; i < propertyUpdates.Length; i++)
				{
					ExtendedPropertyUri extendedPropertyUri = propertyUpdates[i].PropertyPath as ExtendedPropertyUri;
					if (extendedPropertyUri != null && extendedPropertyUri.DistinguishedPropertySetId == DistinguishedPropertySet.InternetHeaders && !string.IsNullOrEmpty(extendedPropertyUri.PropertyName) && extendedPropertyUri.PropertyName.Equals("X-Ms-Exchange-Organization-Dlp-SenderOverrideJustification"))
					{
						string valueOrDefault = storeItem.GetValueOrDefault<string>(ItemSchema.DlpSenderOverride, null);
						if (string.IsNullOrEmpty(valueOrDefault))
						{
							break;
						}
						try
						{
							byte[] bytes = Encoding.Unicode.GetBytes(valueOrDefault);
							string propertyValue = Convert.ToBase64String(bytes);
							storeItem.SetOrDeleteProperty(ItemSchema.DlpSenderOverride, propertyValue);
							break;
						}
						catch (FormatException ex)
						{
							string value = string.Format("User: {0}; Exception: (Type: {1}; Message: {2}; StackTrace: {3}; InnerExceptionType: {4}; InnerMessage: {5}; InnerStackTrace: {6})", new object[]
							{
								(base.CallContext.AccessingADUser == null || string.IsNullOrEmpty(base.CallContext.AccessingADUser.Alias)) ? string.Empty : base.CallContext.AccessingADUser.Alias,
								ex.GetType().ToString(),
								ex.Message,
								ex.StackTrace,
								(ex.InnerException == null) ? string.Empty : ex.InnerException.GetType().ToString(),
								(ex.InnerException == null) ? string.Empty : ex.InnerException.Message,
								(ex.InnerException == null) ? string.Empty : ex.InnerException.StackTrace
							});
							base.CallContext.ProtocolLog.AppendGenericError("DlpPolicyTipsEncodeJustificationError", value);
							break;
						}
					}
				}
			}
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x000943E4 File Offset: 0x000925E4
		private void HandleUpdatesForOnlineMeeting(StoreObject storeItem, PropertyUpdate[] propertyUpdates)
		{
			CalendarItemBase calendarItem = storeItem as CalendarItemBase;
			string onlineMeetingId;
			if (!UpdateItem.ShouldUpdateOnlineMeeting(calendarItem, propertyUpdates, base.CallContext.ProtocolLog, out onlineMeetingId))
			{
				return;
			}
			string sipUri = UpdateItem.GetSipAddress(base.CallContext.AccessingADUser.EmailAddresses, base.CallContext.ProtocolLog);
			if (string.IsNullOrEmpty(sipUri))
			{
				ExTraceGlobals.OnlineMeetingTracer.TraceInformation(0, 0L, "[UpdateItem.HandleUpdatesForOnlineMeeting] Online Meetings is not supported for this user as there are no associated sip addresses");
				return;
			}
			string sipDomain = OnlineMeetingHelper.GetSipDomain(sipUri);
			if (string.IsNullOrEmpty(sipDomain))
			{
				base.CallContext.ProtocolLog.AppendGenericInfo("UpdateOnlineMeetingError", string.Format("[UpdateItem.HandleUpdatesForOnlineMeeting] Unable to parse sip domain from address: {0}", sipUri));
				return;
			}
			OAuthCredentials oauthCredentialsForAppActAsToken = OAuthCredentials.GetOAuthCredentialsForAppActAsToken(base.CallContext.AccessingADUser.OrganizationId, base.CallContext.AccessingADUser, sipDomain);
			oauthCredentialsForAppActAsToken.ClientRequestId = new Guid?(Guid.NewGuid());
			base.CallContext.ProtocolLog.AppendGenericInfo("OAuthCorrelationId", oauthCredentialsForAppActAsToken.ClientRequestId.Value.ToString());
			AutodiscoverResult ucwaDiscoveryUrl = AutodiscoverWorker.GetUcwaDiscoveryUrl(sipUri, oauthCredentialsForAppActAsToken);
			if (ucwaDiscoveryUrl.HasError)
			{
				Exception exception = ucwaDiscoveryUrl.Error.Exception;
				string text = string.Format("[UpdateItem.HandleUpdatesForOnlineMeeting] Autodiscover result failed at step '{0}' with error '{1}'", ucwaDiscoveryUrl.Error.FailureStep.ToString(), (exception.InnerException != null) ? exception.InnerException.Message : exception.Message);
				ExTraceGlobals.OnlineMeetingTracer.TraceError(0L, text);
				base.CallContext.ProtocolLog.AppendGenericInfo("UpdateOnlineMeetingError", text);
				return;
			}
			if (!ucwaDiscoveryUrl.IsUcwaSupported)
			{
				base.CallContext.ProtocolLog.AppendGenericInfo("UpdateOnlineMeetingError", string.Format("Online Meetings is not supported for this user; private online meeting not updated for user: {0}", sipUri));
				return;
			}
			CultureInfo preferredCulture = (base.CallContext.AccessingADUser.Languages == null) ? null : base.CallContext.AccessingADUser.Languages.FirstOrDefault<CultureInfo>();
			this.UpdateOnlineMeeting(calendarItem, oauthCredentialsForAppActAsToken, ucwaDiscoveryUrl.UcwaDiscoveryUrl, onlineMeetingId, preferredCulture).ContinueWith(delegate(Task<OnlineMeetingResult> t)
			{
				this.CallContext.ProtocolLog.AppendGenericInfo("UcwaCallCompleted", bool.TrueString);
				if (t.IsFaulted)
				{
					t.Exception.Flatten().Handle((Exception ex) => this.HandleUpdateOnlineMeetingTaskException(ex, sipUri));
					return;
				}
				if (t.Result == null)
				{
					return;
				}
				this.CallContext.ProtocolLog.AppendGenericInfo("UpdateOnlineMeetingError", t.Result.LogEntry.BuildFailureString());
				UpdateItem.UpdateCalendarItemWithOnlineMeetingProperties(calendarItem, t.Result, preferredCulture, this.CallContext.ProtocolLog);
			});
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x0009462C File Offset: 0x0009282C
		private bool HandleUpdateOnlineMeetingTaskException(Exception ex, string sipUri)
		{
			HttpOperationException ex2 = ex as HttpOperationException;
			if (ex2 != null)
			{
				this.LogException(ex2, "An HttpOperationException occurred while attempting to create online meeting:");
				if (ex2.ErrorInformation == null)
				{
					AutodiscoverCache.IncrementFailureCount(sipUri);
					base.CallContext.ProtocolLog.AppendGenericInfo("LyncADOperation", AutodiscoverCacheOperation.IncrementFailureCounter.ToString());
				}
				else
				{
					ErrorCode code = ex2.ErrorInformation.Code;
					if (code != ErrorCode.Forbidden)
					{
						if (code != ErrorCode.BadGateway)
						{
							AutodiscoverCache.IncrementFailureCount(sipUri);
							base.CallContext.ProtocolLog.AppendGenericInfo("LyncADOperation", AutodiscoverCacheOperation.IncrementFailureCounter.ToString());
						}
						else
						{
							AutodiscoverCache.InvalidateUser(sipUri);
							base.CallContext.ProtocolLog.AppendGenericInfo("LyncADOperation", AutodiscoverCacheOperation.InvalidateUser.ToString());
						}
					}
					else
					{
						AutodiscoverCache.InvalidateDomain(OnlineMeetingHelper.GetSipDomain(sipUri));
						base.CallContext.ProtocolLog.AppendGenericInfo("LyncADOperation", AutodiscoverCacheOperation.InvalidateDomain.ToString());
					}
				}
				return true;
			}
			if (ex is OperationFailureException)
			{
				this.LogException(ex, sipUri, "A network error occured while attempting to create online meeting:");
				return true;
			}
			if (ex is OnlineMeetingSchedulerException)
			{
				this.LogException(ex, sipUri, "Unable to create an online meeting:");
				return true;
			}
			this.LogException(ex, sipUri, "An unknown exception occurred while creating an online meeting:");
			return false;
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x000949B8 File Offset: 0x00092BB8
		private async Task<OnlineMeetingResult> UpdateOnlineMeeting(CalendarItemBase calendarItem, OAuthCredentials credentials, string ucwaUrl, string onlineMeetingId, CultureInfo culture)
		{
			base.CallContext.ProtocolLog.AppendGenericInfo("UcwaUrl", ucwaUrl);
			UcwaOnlineMeetingScheduler scheduler = new UcwaOnlineMeetingScheduler(ucwaUrl, credentials, culture);
			OnlineMeetingSettings settings = new OnlineMeetingSettings();
			settings.Subject = calendarItem.Subject;
			CalendarItem fullCalendarItem = calendarItem as CalendarItem;
			DateTime? newExpiry;
			if (fullCalendarItem != null && fullCalendarItem.Recurrence != null)
			{
				if (fullCalendarItem.Recurrence.Range is NoEndRecurrenceRange)
				{
					newExpiry = null;
				}
				else
				{
					newExpiry = new DateTime?((DateTime)fullCalendarItem.Recurrence.EndDate.AddDays(14.0));
				}
			}
			else
			{
				newExpiry = new DateTime?((DateTime)calendarItem.EndTime.AddDays(14.0));
			}
			ExTraceGlobals.OnlineMeetingTracer.TraceInformation<string>(this.GetHashCode(), 0L, "[UpdateItem.UpdateOnlineMeeting] OnlineMeeting.ExpiryDate set to {0}", (newExpiry != null) ? newExpiry.Value.ToShortDateString() : "null");
			settings.ExpiryTime = newExpiry;
			return await scheduler.UpdateMeetingAsync(onlineMeetingId, settings);
		}

		// Token: 0x060019D9 RID: 6617 RVA: 0x00094A28 File Offset: 0x00092C28
		private void LogException(Exception ex, string description)
		{
			this.LogException(ex, string.Empty, description);
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00094A38 File Offset: 0x00092C38
		private void LogException(Exception ex, string sipUri, string description)
		{
			if (!string.IsNullOrEmpty(sipUri))
			{
				AutodiscoverCache.IncrementFailureCount(sipUri);
				base.CallContext.ProtocolLog.AppendGenericInfo("LyncADOperation", AutodiscoverCacheOperation.IncrementFailureCounter.ToString());
			}
			StringBuilder stringBuilder = new StringBuilder(description);
			if (ex != null)
			{
				if (ex is HttpOperationException)
				{
					HttpOperationException ex2 = ex as HttpOperationException;
					if (ex2.HttpResponse != null)
					{
						base.CallContext.ProtocolLog.AppendGenericInfo("LyncResponseHeaders", ex2.HttpResponse.GetResponseHeadersAsString());
						base.CallContext.ProtocolLog.AppendGenericInfo("LyncResponseBody", ex2.HttpResponse.GetResponseBodyAsString());
					}
					stringBuilder.AppendLine(((HttpOperationException)ex).ToLogString());
				}
				else if (ex is AggregateException)
				{
					stringBuilder.AppendLine(((AggregateException)ex).ToLogString());
				}
				else
				{
					stringBuilder.AppendLine((ex.InnerException != null) ? ex.InnerException.Message : ex.Message);
				}
			}
			ExTraceGlobals.OnlineMeetingTracer.TraceError(0L, stringBuilder.ToString());
			base.CallContext.ProtocolLog.AppendGenericInfo("UpdateOnlineMeetingError", stringBuilder.ToString());
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00094B56 File Offset: 0x00092D56
		private void ThrowIfNotEnabledForTenant(OrganizationId organizationId)
		{
			if (!RmsClientManager.IRMConfig.IsClientAccessServerEnabledForTenant(organizationId))
			{
				throw new RightsManagementPermanentException(CoreResources.RightsManagementInternalLicensingDisabled, null);
			}
		}

		// Token: 0x0400112D RID: 4397
		private const string UpdateOnlineMeetingErrorLogKey = "UpdateOnlineMeetingError";

		// Token: 0x0400112E RID: 4398
		private const string DlpPolicyTipsEncodeJustificationErrorLogKey = "DlpPolicyTipsEncodeJustificationError";

		// Token: 0x0400112F RID: 4399
		private static readonly string UpdateItemActionName = typeof(UpdateItem).Name;

		// Token: 0x04001130 RID: 4400
		private static readonly PropertyUriEnum[] settableNonDraftMeetingRequestProperties = new PropertyUriEnum[]
		{
			PropertyUriEnum.Subject,
			PropertyUriEnum.Categories,
			PropertyUriEnum.IsRead,
			PropertyUriEnum.Importance,
			PropertyUriEnum.Flag
		};

		// Token: 0x04001131 RID: 4401
		private static readonly PropertyUriEnum[] settableNonDraftTaskRequestProperties = new PropertyUriEnum[]
		{
			PropertyUriEnum.Categories,
			PropertyUriEnum.IsRead,
			PropertyUriEnum.Importance,
			PropertyUriEnum.Flag
		};

		// Token: 0x04001132 RID: 4402
		private TargetFolderId savedItemFolderId;

		// Token: 0x04001133 RID: 4403
		private ItemChange[] itemChanges;

		// Token: 0x04001134 RID: 4404
		private ItemResponseShape responseShape;

		// Token: 0x04001135 RID: 4405
		private ConflictResolutionType conflictResolutionType;

		// Token: 0x04001136 RID: 4406
		private CalendarItemOperationType.Update? sendMeetingUpdates;

		// Token: 0x04001137 RID: 4407
		private int totalNbRecipients;

		// Token: 0x04001138 RID: 4408
		private int totalNbMessages;

		// Token: 0x04001139 RID: 4409
		private int totalBodySize;
	}
}
