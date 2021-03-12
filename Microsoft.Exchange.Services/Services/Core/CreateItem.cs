using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002C0 RID: 704
	internal sealed class CreateItem : CreateUpdateItemCommandBase<CreateItemRequest, ItemType[]>, IDisposeTrackable, IDisposable
	{
		// Token: 0x0600132F RID: 4911 RVA: 0x0005DD3C File Offset: 0x0005BF3C
		public CreateItem(CallContext callContext, CreateItemRequest request) : base(callContext, request)
		{
			this.disposeTracker = this.GetDisposeTracker();
			OwsLogRegistry.Register(CreateItem.CreateItemActionName, typeof(EnhancedLocationMetadata), new Type[]
			{
				typeof(CreateAndUpdateItemMetadata),
				typeof(GetParticipantOrDLFromAddressMetadata)
			});
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0005DD9F File Offset: 0x0005BF9F
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<CreateItem>(this);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0005DDA7 File Offset: 0x0005BFA7
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x0005DDBC File Offset: 0x0005BFBC
		public void Dispose()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
			}
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x0005DDDE File Offset: 0x0005BFDE
		internal override int StepCount
		{
			get
			{
				return this.items.Length;
			}
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0005DDE8 File Offset: 0x0005BFE8
		internal override void PreExecuteCommand()
		{
			this.saveToFolderId = base.Request.SavedItemFolderId;
			this.items = base.Request.Items.Items;
			if (!string.IsNullOrEmpty(base.Request.SendMeetingInvitations))
			{
				this.sendMeetingInvitations = new CalendarItemOperationType.CreateOrDelete?(SendMeetingInvitations.ConvertToEnum(base.Request.SendMeetingInvitations));
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
			ServiceCommandBase.ThrowIfNullOrEmpty<ItemType>(this.items, "this.items", "CreateItem::Execute");
			if (this.saveToFolderId != null)
			{
				try
				{
					this.saveToFolderIdAndSession = base.IdConverter.ConvertTargetFolderIdToIdAndContentSession(this.saveToFolderId.BaseFolderId, true);
				}
				catch (ObjectNotFoundException innerException)
				{
					throw new SavedItemFolderNotFoundException(innerException);
				}
			}
			this.dsHandle = base.GetDelegateSessionHandleWrapper(this.saveToFolderIdAndSession, this.messageDisposition == MessageDispositionType.SendAndSaveCopy);
			if (this.dsHandle != null)
			{
				this.saveToFolderIdAndSession = new IdAndSession(this.saveToFolderIdAndSession.Id, this.dsHandle.Handle.MailboxSession);
			}
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0005DF70 File Offset: 0x0005C170
		internal override IExchangeWebMethodResponse GetResponse()
		{
			CreateItemResponse createItemResponse = new CreateItemResponse();
			createItemResponse.BuildForResults<ItemType[]>(base.Results);
			return createItemResponse;
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0005DF90 File Offset: 0x0005C190
		protected override void LogDelegateSession(string principal)
		{
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, CreateAndUpdateItemMetadata.SessionType, LogonType.Delegated);
			if (!string.IsNullOrEmpty(principal))
			{
				RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, CreateAndUpdateItemMetadata.Principal, principal);
			}
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x0005DFC4 File Offset: 0x0005C1C4
		private static void ValidateFolderType(StoreObjectId storeObjectId, StoreObjectType storeObjectType)
		{
			if (storeObjectId != null && storeObjectId.ObjectType == storeObjectType)
			{
				return;
			}
			if (storeObjectType == StoreObjectType.CalendarFolder)
			{
				throw new InvalidFolderIdException((CoreResources.IDs)3564002022U);
			}
			if (storeObjectType == StoreObjectType.ContactsFolder)
			{
				throw new InvalidFolderIdException(CoreResources.IDs.ErrorCannotCreateContactInNonContactFolder);
			}
			throw new InvalidFolderIdException(CoreResources.IDs.ErrorCannotCreateTaskInNonTaskFolder);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0005E015 File Offset: 0x0005C215
		private void Dispose(bool fromDispose)
		{
			if (this.dsHandle != null)
			{
				this.dsHandle.Dispose();
				this.dsHandle = null;
			}
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0005E034 File Offset: 0x0005C234
		internal override ServiceResult<ItemType[]> Execute()
		{
			this.ComputeAndLogStatistics();
			ServiceError serviceError;
			ItemType[] value = this.CreateItemFromServiceObject(this.items[base.CurrentStep], out serviceError);
			this.objectsChanged++;
			if (serviceError == null)
			{
				return new ServiceResult<ItemType[]>(value);
			}
			return new ServiceResult<ItemType[]>(value, serviceError);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0005E07C File Offset: 0x0005C27C
		private void ComputeAndLogStatistics()
		{
			MessageType messageType = this.items[base.CurrentStep] as MessageType;
			if (messageType != null)
			{
				this.totalNbRecipients += ((messageType.ToRecipients != null) ? messageType.ToRecipients.Length : 0) + ((messageType.CcRecipients != null) ? messageType.CcRecipients.Length : 0) + ((messageType.BccRecipients != null) ? messageType.BccRecipients.Length : 0);
				if (messageType.Body != null && messageType.Body.Value != null)
				{
					this.totalBodySize += messageType.Body.Value.Length;
				}
				this.totalNbMessages++;
			}
			EwsCalendarItemType ewsCalendarItemType = this.items[base.CurrentStep] as EwsCalendarItemType;
			if (ewsCalendarItemType != null)
			{
				int num = ((ewsCalendarItemType.RequiredAttendees != null) ? ewsCalendarItemType.RequiredAttendees.Length : 0) + ((ewsCalendarItemType.OptionalAttendees != null) ? ewsCalendarItemType.OptionalAttendees.Length : 0);
				if (num > 0)
				{
					this.totalMeetings++;
				}
			}
			if (base.CurrentStep + 1 == this.items.Length)
			{
				RequestDetailsLogger requestDetailsLogger = RequestDetailsLogger.Current;
				if (this.totalNbMessages > 0)
				{
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(requestDetailsLogger, CreateAndUpdateItemMetadata.TotalNbMessages, this.totalNbMessages);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(requestDetailsLogger, CreateAndUpdateItemMetadata.TotalNbRecipients, this.totalNbRecipients);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(requestDetailsLogger, CreateAndUpdateItemMetadata.TotalBodySize, this.totalBodySize);
				}
				if (this.totalMeetings > 0)
				{
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(requestDetailsLogger, CreateAndUpdateItemMetadata.TotalMeetings, this.totalMeetings);
				}
			}
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0005E204 File Offset: 0x0005C404
		private ItemType[] CreateItemFromServiceObject(ItemType item, out ServiceError warning)
		{
			warning = null;
			if (base.Request.ItemShape != null || base.Request.ShapeName != null)
			{
				this.responseShape = Global.ResponseShapeResolver.GetResponseShape<ItemResponseShape>(base.Request.ShapeName, base.Request.ItemShape, base.CallContext.FeaturesManager);
			}
			else if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010SP2) && item.Attachments != null && item.Attachments.Length > 0)
			{
				this.responseShape = ServiceCommandBase.DefaultItemResponseShapeWithAttachments;
			}
			else
			{
				this.responseShape = ServiceCommandBase.DefaultItemResponseShape;
			}
			RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, CreateAndUpdateItemMetadata.ActionType, item.GetType().Name);
			Func<CreateItem, ItemType, ItemType[]> func = null;
			ItemType[] result;
			if (CreateItem.responseActionMap.Member.TryGetValue(item.GetType(), out func))
			{
				result = func(this, item);
			}
			else
			{
				result = new ItemType[]
				{
					this.CreateStoreItem(item, out warning)
				};
			}
			return result;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0005E2F8 File Offset: 0x0005C4F8
		internal bool IsItemAttachment(SmartResponseType item)
		{
			bool result;
			try
			{
				ServiceIdConverter.ConvertFromConcatenatedId(item.ReferenceItemId.GetId(), BasicTypes.Attachment, new List<AttachmentId>());
				result = true;
			}
			catch (InvalidIdNotAnItemAttachmentIdException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0005E338 File Offset: 0x0005C538
		internal ItemType[] ExecuteForwardReply(ItemType item)
		{
			ItemType itemType = null;
			SmartResponseType smartResponseType = (SmartResponseType)item;
			Item item2 = null;
			Item item3 = null;
			AttachmentHierarchy attachmentHierarchy = null;
			try
			{
				CreateItem.ForwardReplyInformation forwardReplyInformation;
				if (this.IsItemAttachment(smartResponseType))
				{
					smartResponseType.ReferenceItemId = new AttachmentIdType(smartResponseType.ReferenceItemId.GetId());
					attachmentHierarchy = this.GetXsoItemAttachment((AttachmentIdType)smartResponseType.ReferenceItemId);
					item3 = attachmentHierarchy.RootItem;
					item2 = attachmentHierarchy.LastAsXsoItem;
					forwardReplyInformation = new CreateItem.ForwardReplyInformation(smartResponseType, base.IdConverter, true);
				}
				else
				{
					forwardReplyInformation = new CreateItem.ForwardReplyInformation(smartResponseType, base.IdConverter, false);
					item2 = this.GetReferenceItem(forwardReplyInformation, base.Request.SendOnNotFoundError);
				}
				if (item2 != null)
				{
					bool requireUpToDateOriginalMessage = this.ShouldRequireUpdateToDateOriginalMessage(base.Request.SendOnNotFoundError);
					using (MessageItem forwardReplyMessage = this.GetForwardReplyMessage(ref item2, forwardReplyInformation, base.CallContext.ClientCulture, item3, requireUpToDateOriginalMessage))
					{
						itemType = this.UpdateNewItem(forwardReplyMessage, forwardReplyInformation.UpdateItem);
						goto IL_10E;
					}
				}
				using (MessageItem messageToUpdate = this.GetMessageToUpdate(forwardReplyInformation))
				{
					forwardReplyInformation.UpdateItem.Body = smartResponseType.NewBodyContent;
					itemType = this.UpdateNewItem(messageToUpdate, forwardReplyInformation.UpdateItem);
				}
				IL_10E:;
			}
			finally
			{
				if (item2 != null)
				{
					item2.Dispose();
				}
				if (attachmentHierarchy != null)
				{
					attachmentHierarchy.Dispose();
				}
				if (item3 != null)
				{
					item3.Dispose();
				}
			}
			return new ItemType[]
			{
				itemType
			};
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0005E4D0 File Offset: 0x0005C6D0
		private bool ShouldRequireUpdateToDateOriginalMessage(bool sendOnNotFoundError)
		{
			return !sendOnNotFoundError || (this.messageDisposition.Value != MessageDispositionType.SendAndSaveCopy && this.messageDisposition.Value != MessageDispositionType.SendOnly);
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0005E4F8 File Offset: 0x0005C6F8
		private MessageItem GetMessageToUpdate(CreateItem.ForwardReplyInformation forwardReplyInformation)
		{
			MessageItem messageItem;
			if (forwardReplyInformation.UpdateResponseItemId != null)
			{
				ExTraceGlobals.CreateItemCallTracer.TraceDebug<StoreId, string>((long)this.GetHashCode(), "CreateItem.GetMessageToUpdate: Reference item {0} not found; Get message to update {1}.", forwardReplyInformation.ReferenceIdAndSession.Id, forwardReplyInformation.UpdateResponseItemId.Id);
				messageItem = this.GetUpdateResponseItem(forwardReplyInformation.UpdateResponseItemId);
				if (messageItem == null)
				{
					ExTraceGlobals.CreateItemCallTracer.TraceDebug<ItemId>((long)this.GetHashCode(), "CreateItem.GetMessageToUpdate: UpdateResponseItem not found; Create new response message.", forwardReplyInformation.UpdateResponseItemId);
					messageItem = this.CreateNewUpdateResponseMessage(forwardReplyInformation);
				}
			}
			else
			{
				ExTraceGlobals.CreateItemCallTracer.TraceDebug<StoreId>((long)this.GetHashCode(), "CreateItem.GetMessageToUpdate: Reference item {0} not found and no UpdateResponseItemId; Create new response message.", forwardReplyInformation.ReferenceIdAndSession.Id);
				messageItem = this.CreateNewUpdateResponseMessage(forwardReplyInformation);
			}
			return messageItem;
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0005E599 File Offset: 0x0005C799
		private MessageItem CreateNewUpdateResponseMessage(CreateItem.ForwardReplyInformation forwardReplyInformation)
		{
			return MessageItem.Create(forwardReplyInformation.ReferenceIdAndSession.Session, forwardReplyInformation.ReferenceIdAndSession.Session.GetDefaultFolderId(DefaultFolderType.Drafts));
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0005E5BC File Offset: 0x0005C7BC
		private Item GetReferenceItem(CreateItem.ForwardReplyInformation forwardReplyInformation, bool sendOnNotFoundError)
		{
			Item result;
			try
			{
				result = forwardReplyInformation.ReferenceIdAndSession.GetRootXsoItem(CreateItem.forwardReplyPropertyDefinitionArray);
			}
			catch (ObjectNotFoundException)
			{
				if (!sendOnNotFoundError)
				{
					throw;
				}
				if (forwardReplyInformation.ReferenceItemDocumentId == 0)
				{
					ExTraceGlobals.CreateItemCallTracer.TraceError<StoreId>((long)this.GetHashCode(), "CreateItem.GetOriginalItem: No DocumentId for Reference item {0}", forwardReplyInformation.ReferenceIdAndSession.Id);
					result = null;
				}
				else
				{
					ExTraceGlobals.CreateItemCallTracer.TraceDebug<StoreId>((long)this.GetHashCode(), "CreateItem.GetReferenceItem: Reference item {0} not found; Find moved reference item.", forwardReplyInformation.ReferenceIdAndSession.Id);
					result = this.FindMovedItem(forwardReplyInformation);
				}
			}
			return result;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0005E64C File Offset: 0x0005C84C
		private Item FindMovedItem(CreateItem.ForwardReplyInformation forwardReplyInformation)
		{
			Item result;
			using (Folder folder = Folder.Bind(forwardReplyInformation.ReferenceIdAndSession.Session, DefaultFolderType.Configuration))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.DocumentIdView, null, null, new PropertyDefinition[]
				{
					ItemSchema.Id
				}))
				{
					ComparisonFilter seekFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.DocumentId, forwardReplyInformation.ReferenceItemDocumentId);
					queryResult.SeekToCondition(SeekReference.OriginCurrent, seekFilter, SeekToConditionFlags.None);
					object[][] rows = queryResult.GetRows(1);
					if (rows == null || rows.Length == 0)
					{
						ExTraceGlobals.CreateItemCallTracer.TraceDebug<StoreId>((long)this.GetHashCode(), "CreateItem.FindMovedItem: Moved reference item {0} not found.", forwardReplyInformation.ReferenceIdAndSession.Id);
						result = null;
					}
					else
					{
						VersionedId versionedId = rows[0][0] as VersionedId;
						if (versionedId == null)
						{
							ExTraceGlobals.CreateItemCallTracer.TraceDebug<StoreId>((long)this.GetHashCode(), "CreateItem.FindMovedItem: Moved reference item {0} not found.", forwardReplyInformation.ReferenceIdAndSession.Id);
							result = null;
						}
						else
						{
							IdAndSession idAndSession = new IdAndSession(versionedId, forwardReplyInformation.ReferenceIdAndSession.Session);
							try
							{
								ExTraceGlobals.CreateItemCallTracer.TraceDebug<StoreId>((long)this.GetHashCode(), "CreateItem.FindMovedItem: Binding with moved ReferenceItemId: {0}", idAndSession.Id);
								Item rootXsoItem = idAndSession.GetRootXsoItem(CreateItem.forwardReplyPropertyDefinitionArray);
								forwardReplyInformation.ReplaceReferenceId(idAndSession);
								result = rootXsoItem;
							}
							catch (ObjectNotFoundException)
							{
								ExTraceGlobals.CreateItemCallTracer.TraceDebug<StoreId>((long)this.GetHashCode(), "CreateItem.FindMovedItem: Moved reference item {0} not found", idAndSession.Id);
								result = null;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0005E7F0 File Offset: 0x0005C9F0
		private AttachmentHierarchy GetXsoItemAttachment(AttachmentIdType attachmentIdType)
		{
			string id = attachmentIdType.GetId();
			List<AttachmentId> attachmentIds = new List<AttachmentId>();
			IdHeaderInformation headerInformation = ServiceIdConverter.ConvertFromConcatenatedId(id, BasicTypes.Attachment, attachmentIds);
			IdAndSession idAndSession = IdConverter.ConvertId(CallContext.Current, headerInformation, IdConverter.ConvertOption.IgnoreChangeKey, BasicTypes.Attachment, attachmentIds, null, this.GetHashCode());
			return new AttachmentHierarchy(idAndSession, false, true);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0005E834 File Offset: 0x0005CA34
		private MessageItem GetForwardReplyMessage(ref Item originalItem, CreateItem.ForwardReplyInformation forwardReplyInformation, CultureInfo culture, Item rootItem, bool requireUpToDateOriginalMessage)
		{
			MessageItem messageItem = null;
			if (forwardReplyInformation.UpdateResponseItemId != null)
			{
				messageItem = this.GetUpdateResponseItem(forwardReplyInformation.UpdateResponseItemId);
			}
			MessageItem messageItem2 = this.ValidateAndCreateForwardReply(ref originalItem, forwardReplyInformation, base.CallContext.ClientCulture, rootItem, requireUpToDateOriginalMessage, messageItem);
			if (forwardReplyInformation.UpdateResponseItemId == null)
			{
				messageItem = messageItem2;
			}
			else if (messageItem == null)
			{
				messageItem = messageItem2;
			}
			else
			{
				bool flag = false;
				try
				{
					StoreSession session = messageItem.Session;
					RightsManagedMessageItem rightsManagedMessageItem = messageItem as RightsManagedMessageItem;
					Body body;
					if (base.Request.ClientSupportsIrm && rightsManagedMessageItem != null && rightsManagedMessageItem.IsRestricted)
					{
						MailboxSession mailboxSession = IrmUtils.ValidateAndGetMailboxSession(session);
						IrmUtils.TryDecrypt(rightsManagedMessageItem, mailboxSession.MailboxOwner.MailboxInfo.OrganizationId);
						body = IrmUtils.GetBodyFromDecryptedIrmItem(rightsManagedMessageItem);
					}
					else if (base.Request.ClientSupportsIrm && !string.IsNullOrEmpty(base.Request.ComplianceId) && IrmUtils.IsApplyingRmsTemplate(base.Request.ComplianceId, session, out this.rmsTemplate))
					{
						MailboxSession mailboxSession2 = IrmUtils.ValidateAndGetMailboxSession(session);
						OutboundConversionOptions outboundConversionOptions = IrmUtils.GetOutboundConversionOptions(mailboxSession2.MailboxOwner.MailboxInfo.OrganizationId);
						rightsManagedMessageItem = RightsManagedMessageItem.Create(messageItem, outboundConversionOptions);
						body = rightsManagedMessageItem.ProtectedBody;
						messageItem = rightsManagedMessageItem;
					}
					else
					{
						body = messageItem.Body;
					}
					RightsManagedMessageItem rightsManagedMessageItem2 = messageItem2 as RightsManagedMessageItem;
					Body body2;
					if (rightsManagedMessageItem2 != null && base.Request.ClientSupportsIrm)
					{
						body2 = IrmUtils.GetBodyFromDecryptedIrmItem(rightsManagedMessageItem2);
					}
					else
					{
						body2 = messageItem2.Body;
					}
					BodyReadConfiguration configuration = new BodyReadConfiguration(body2.Format, body2.Charset);
					BodyWriteConfiguration bodyWriteConfiguration = new BodyWriteConfiguration(body2.Format, body2.Charset);
					bodyWriteConfiguration.SetTargetFormat(body2.Format, body2.Charset, messageItem2.CharsetDetector.CharsetFlags);
					HtmlUpdateBodyCallback htmlUpdateBodyCallback = null;
					if (body2.Format == Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml)
					{
						htmlUpdateBodyCallback = new HtmlUpdateBodyCallback(messageItem2);
						bodyWriteConfiguration.SetHtmlOptions(HtmlStreamingFlags.None, htmlUpdateBodyCallback);
					}
					using (Stream stream = body2.OpenReadStream(configuration))
					{
						using (Stream stream2 = body.OpenWriteStream(bodyWriteConfiguration))
						{
							Util.StreamHandler.CopyStreamData(stream, stream2);
						}
					}
					if (htmlUpdateBodyCallback != null)
					{
						htmlUpdateBodyCallback.SaveChanges();
					}
					messageItem.From = null;
					flag = true;
				}
				finally
				{
					messageItem2.Dispose();
					if (!flag && messageItem != null)
					{
						messageItem.Dispose();
					}
				}
			}
			return messageItem;
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x0005EAA4 File Offset: 0x0005CCA4
		private MessageItem GetUpdateResponseItem(ItemId itemId)
		{
			MessageItem result = null;
			IdAndSession updateResponseItemId = this.GetUpdateResponseItemId(itemId, base.Request.SendOnNotFoundError);
			if (updateResponseItemId != null)
			{
				result = (MessageItem)base.GetItemForUpdate(updateResponseItemId, base.Request.SendOnNotFoundError);
			}
			return result;
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x0005EAE4 File Offset: 0x0005CCE4
		private IdAndSession GetUpdateResponseItemId(ItemId itemId, bool sendOnNotFoundError)
		{
			IdAndSession result = null;
			try
			{
				result = base.IdConverter.ConvertItemIdToIdAndSessionReadOnly(itemId);
			}
			catch (InvalidStoreIdException arg)
			{
				if (this.ShouldRequireUpdateToDateOriginalMessage(sendOnNotFoundError))
				{
					throw;
				}
				ExTraceGlobals.CreateItemCallTracer.TraceDebug<InvalidStoreIdException>((long)this.GetHashCode(), "CreateItem.GetUpdateResponseItemId:  UpdateResponseItemId is invalid; returning null idandsession. exception {0}", arg);
			}
			return result;
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0005EB38 File Offset: 0x0005CD38
		private MessageItem ValidateAndCreateForwardReply(ref Item originalItem, CreateItem.ForwardReplyInformation forwardReplyInformation, CultureInfo culture, Item rootItem, bool requireUpToDateOriginalMessage, MessageItem messageToUpdate)
		{
			IdAndSession messageParentFolderIdAndSession = base.GetMessageParentFolderIdAndSession();
			MailboxSession mailboxSession = (MailboxSession)messageParentFolderIdAndSession.Session;
			bool flag = false;
			bool flag2 = true;
			Microsoft.Exchange.Data.Storage.BodyFormat format = Util.GetEffectiveBody(originalItem).Format;
			RightsManagedMessageItem rightsManagedMessageItem = originalItem as RightsManagedMessageItem;
			if (base.Request.ClientSupportsIrm && rightsManagedMessageItem != null && rightsManagedMessageItem.IsRestricted)
			{
				OrganizationId organizationId = mailboxSession.MailboxOwner.MailboxInfo.OrganizationId;
				RightsManagedMessageDecryptionStatus rightsManagedMessageDecryptionStatus = IrmUtils.TryDecrypt(rightsManagedMessageItem, organizationId);
				if (!RightsManagedMessageDecryptionStatus.Success.Equals(rightsManagedMessageDecryptionStatus))
				{
					IrmUtils.ThrowIfInternalLicensingDisabled(organizationId);
					if (!RightsManagedMessageDecryptionStatus.FeatureDisabled.Equals(rightsManagedMessageDecryptionStatus))
					{
						throw new RightsManagementPermanentException(CoreResources.IrmRmsErrorMessage(rightsManagedMessageDecryptionStatus.ToString()), null);
					}
					flag = true;
					flag2 = false;
				}
				else if (!this.CanEditItem(rightsManagedMessageItem))
				{
					format = rightsManagedMessageItem.ProtectedBody.Format;
					rightsManagedMessageItem.Dispose();
					originalItem = forwardReplyInformation.ReferenceIdAndSession.GetRootXsoItem(CreateItem.forwardReplyPropertyDefinitionArray);
					flag = true;
				}
				else
				{
					format = rightsManagedMessageItem.ProtectedBody.Format;
				}
			}
			MessageItem messageItem = null;
			MessageItem messageItem2;
			CalendarItemBase calendarItemBase;
			PostItem originalPostItem;
			this.ValidateForwardReply(originalItem, forwardReplyInformation, requireUpToDateOriginalMessage, out messageItem2, out calendarItemBase, out originalPostItem);
			string text = forwardReplyInformation.BodyPrefix;
			Microsoft.Exchange.Data.Storage.BodyFormat bodyFormat = Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain;
			if (forwardReplyInformation.WasBodyPrefixSpecified)
			{
				bodyFormat = forwardReplyInformation.BodyPrefixType;
			}
			else
			{
				switch (format)
				{
				case Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain:
					bodyFormat = Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain;
					break;
				case Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml:
				case Microsoft.Exchange.Data.Storage.BodyFormat.ApplicationRtf:
					bodyFormat = Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml;
					break;
				}
			}
			ForwardReplyHeaderOptions headerOptions = new ForwardReplyHeaderOptions();
			bool isMeetingItem = originalItem is MeetingMessage || calendarItemBase != null;
			string text2 = ForwardReplyUtilities.CreateForwardReplyHeader(bodyFormat, originalItem, headerOptions, isMeetingItem, culture, base.Request.TimeFormat, null);
			if (flag)
			{
				text = string.Empty;
			}
			else
			{
				text = (string.IsNullOrEmpty(text) ? text2 : (text + text2));
			}
			ReplyForwardConfiguration replyForwardConfiguration = new ReplyForwardConfiguration(bodyFormat);
			CallContext callContext = CallContext.Current;
			if (!string.IsNullOrEmpty(base.Request.SubjectPrefix))
			{
				replyForwardConfiguration.SubjectPrefix = base.Request.SubjectPrefix;
			}
			replyForwardConfiguration.ConversionOptionsForSmime = new InboundConversionOptions(callContext.DefaultDomain.DomainName.Domain);
			replyForwardConfiguration.ConversionOptionsForSmime.IsSenderTrusted = true;
			if (forwardReplyInformation.IsAttachment)
			{
				replyForwardConfiguration.ConversionOptionsForSmime.UserADSession = rootItem.Session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid);
			}
			else
			{
				replyForwardConfiguration.ConversionOptionsForSmime.UserADSession = originalItem.Session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid);
			}
			replyForwardConfiguration.AddBodyPrefix(text);
			replyForwardConfiguration.ShouldSuppressReadReceipt = base.Request.ShouldSuppressReadReceipt;
			if (!forwardReplyInformation.IsAttachment)
			{
				originalItem.Load(StoreObjectSchema.ContentConversionProperties);
				if (!string.IsNullOrEmpty(this.responseShape.InlineImageUrlTemplate) || this.responseShape.AddBlankTargetToLinks || this.responseShape.BlockExternalImages)
				{
					replyForwardConfiguration.HtmlCallbacks = new HtmlBodyCallback(originalItem, null, false);
				}
			}
			if (messageToUpdate != null)
			{
				replyForwardConfiguration.ShouldSkipFilterHtmlOnBodyWrite = true;
			}
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				messageItem = this.CreateForwardReplyMessage(forwardReplyInformation, messageItem2, calendarItemBase, originalPostItem, messageParentFolderIdAndSession, replyForwardConfiguration);
				disposeGuard.Add<MessageItem>(messageItem);
				if (messageItem2 != null && !forwardReplyInformation.IsAttachment && (!(forwardReplyInformation.UpdateItem is ForwardItemType) || !(messageItem is MeetingMessage)))
				{
					IrmUtils.CopyMessageClassificationProperties(messageItem2, messageItem);
				}
				if (flag)
				{
					messageItem.AttachmentCollection.RemoveAll();
					if (flag2)
					{
						using (ItemAttachment itemAttachment = messageItem.AttachmentCollection.AddExistingItem(originalItem))
						{
							string text3 = originalItem.TryGetProperty(ItemSchema.NormalizedSubject) as string;
							if (text3 == null)
							{
								text3 = (originalItem.TryGetProperty(ItemSchema.Subject) as string);
							}
							if (!string.IsNullOrEmpty(text3))
							{
								itemAttachment[AttachmentSchema.DisplayName] = text3;
							}
							itemAttachment.Save();
						}
					}
					using (TextWriter textWriter = messageItem.Body.OpenTextWriter(bodyFormat))
					{
						textWriter.Write(string.Empty);
					}
				}
				if (base.Request.ClientSupportsIrm && !(messageItem is RightsManagedMessageItem) && IrmUtils.IsApplyingRmsTemplate(base.Request.ComplianceId, mailboxSession, out this.rmsTemplate))
				{
					OutboundConversionOptions outboundConversionOptions = IrmUtils.GetOutboundConversionOptions(mailboxSession.MailboxOwner.MailboxInfo.OrganizationId);
					messageItem = RightsManagedMessageItem.Create(messageItem, outboundConversionOptions);
				}
				disposeGuard.Success();
			}
			if (base.CallContext.IsOwa)
			{
				base.SetBodyCharsetOptions(messageItem);
			}
			return messageItem;
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x0005EFDC File Offset: 0x0005D1DC
		private MessageItem CreateForwardReplyMessage(CreateItem.ForwardReplyInformation forwardReplyInformation, MessageItem originalMessageItem, CalendarItemBase originalCalendarItem, PostItem originalPostItem, IdAndSession parentStoreIdAndSession, ReplyForwardConfiguration configuration)
		{
			MessageItem result = null;
			MailboxSession session = (MailboxSession)parentStoreIdAndSession.Session;
			StoreObjectId asStoreObjectId = parentStoreIdAndSession.GetAsStoreObjectId();
			if (forwardReplyInformation.UpdateItem is ForwardItemType)
			{
				if (originalMessageItem != null)
				{
					result = originalMessageItem.CreateForward(session, asStoreObjectId, configuration);
				}
				else if (originalCalendarItem != null)
				{
					result = originalCalendarItem.CreateForward(session, asStoreObjectId, configuration, null, null);
				}
				else
				{
					result = originalPostItem.CreateForward(session, asStoreObjectId, configuration);
				}
			}
			else if (forwardReplyInformation.UpdateItem is ReplyAllToItemType)
			{
				if (originalMessageItem != null)
				{
					result = originalMessageItem.CreateReplyAll(session, asStoreObjectId, configuration);
				}
				else if (originalCalendarItem != null)
				{
					result = originalCalendarItem.CreateReplyAll(session, asStoreObjectId, configuration);
				}
				else
				{
					result = originalPostItem.CreateReplyAll(session, asStoreObjectId, configuration);
				}
			}
			else if (forwardReplyInformation.UpdateItem is ReplyToItemType)
			{
				if (originalMessageItem != null)
				{
					result = originalMessageItem.CreateReply(session, asStoreObjectId, configuration);
				}
				else if (originalCalendarItem != null)
				{
					result = originalCalendarItem.CreateReply(session, asStoreObjectId, configuration);
				}
				else
				{
					result = originalPostItem.CreateReply(session, asStoreObjectId, configuration);
				}
			}
			return result;
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0005F0C0 File Offset: 0x0005D2C0
		private PostItem GetPostReplyItem(Item originalItem, CreateItem.PostReplyInformation postReplyInformation, CultureInfo culture)
		{
			this.ValidatePostReply(originalItem, postReplyInformation);
			StoreObjectId parentFolderId = (this.saveToFolderIdAndSession == null) ? originalItem.ParentId : this.saveToFolderIdAndSession.GetAsStoreObjectId();
			PostItem postItem = originalItem as PostItem;
			Microsoft.Exchange.Data.Storage.BodyFormat bodyFormat = Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain;
			string text = postReplyInformation.BodyPrefix;
			if (postReplyInformation.WasBodyPrefixSpecified)
			{
				bodyFormat = postReplyInformation.BodyPrefixType;
			}
			else
			{
				switch (Util.GetEffectiveBody(originalItem).Format)
				{
				case Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain:
					bodyFormat = Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain;
					break;
				case Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml:
				case Microsoft.Exchange.Data.Storage.BodyFormat.ApplicationRtf:
					bodyFormat = Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml;
					break;
				}
			}
			ForwardReplyHeaderOptions headerOptions = new ForwardReplyHeaderOptions();
			string text2 = ForwardReplyUtilities.CreateForwardReplyHeader(bodyFormat, originalItem, headerOptions, false, culture, base.Request.TimeFormat, null);
			text = (string.IsNullOrEmpty(text) ? text2 : (text + text2));
			ReplyForwardConfiguration replyForwardConfiguration = new ReplyForwardConfiguration(bodyFormat);
			CallContext callContext = CallContext.Current;
			replyForwardConfiguration.ConversionOptionsForSmime = new InboundConversionOptions(callContext.DefaultDomain.DomainName.Domain);
			replyForwardConfiguration.ConversionOptionsForSmime.IsSenderTrusted = true;
			replyForwardConfiguration.ConversionOptionsForSmime.UserADSession = originalItem.Session.GetADRecipientSession(true, ConsistencyMode.IgnoreInvalid);
			replyForwardConfiguration.AddBodyPrefix(text);
			PostItem postItem2 = postItem.ReplyToFolder(parentFolderId, replyForwardConfiguration);
			if (postItem2 != null && originalItem.Session.IsPublicFolderSession)
			{
				postItem2.From = new Participant(base.CallContext.AccessingPrincipal);
				postItem2.Sender = postItem2.From;
			}
			return postItem2;
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0005F210 File Offset: 0x0005D410
		private void ValidatePostReply(Item originalItem, CreateItem.PostReplyInformation postReplyInformation)
		{
			ServiceCommandBase.RequireUpToDateItem(postReplyInformation.ReferenceIdAndSession.Id, originalItem);
			if (!(originalItem is PostItem))
			{
				throw new InvalidReferenceItemException();
			}
			if (this.saveToFolderIdAndSession != null)
			{
				MailboxSession mailboxSession = originalItem.Session as MailboxSession;
				MailboxSession mailboxSession2 = this.saveToFolderIdAndSession.Session as MailboxSession;
				if (mailboxSession != null && mailboxSession2 != null)
				{
					string a = mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
					string b = mailboxSession2.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString();
					if (!string.Equals(a, b, StringComparison.OrdinalIgnoreCase))
					{
						throw new ServiceInvalidOperationException((CoreResources.IDs)2999374145U);
					}
				}
				else if (!(originalItem.Session is PublicFolderSession) || !(this.saveToFolderIdAndSession.Session is PublicFolderSession))
				{
					throw new ServiceInvalidOperationException((CoreResources.IDs)2999374145U);
				}
				StoreObjectType objectType = StoreId.GetStoreObjectId(this.saveToFolderIdAndSession.Id).ObjectType;
				if (!this.IsMailFolder(objectType))
				{
					throw new CannotCreatePostItemInNonMailFolderException();
				}
			}
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0005F31C File Offset: 0x0005D51C
		private void ValidateForwardReply(Item originalItem, CreateItem.ForwardReplyInformation forwardReplyInformation, bool requireUpToDateOriginalMessage, out MessageItem originalMessageItem, out CalendarItemBase originalCalendarItem, out PostItem originalPostItem)
		{
			originalMessageItem = null;
			originalCalendarItem = null;
			originalPostItem = null;
			this.ValidateForwardReplyMessageDisposition();
			if (!forwardReplyInformation.IsAttachment && requireUpToDateOriginalMessage)
			{
				ServiceCommandBase.RequireUpToDateItem(forwardReplyInformation.ReferenceIdAndSession.Id, originalItem);
			}
			if (!XsoDataConverter.TryGetStoreObject<MessageItem>(originalItem, out originalMessageItem) && !XsoDataConverter.TryGetStoreObject<CalendarItemBase>(originalItem, out originalCalendarItem))
			{
				XsoDataConverter.TryGetStoreObject<PostItem>(originalItem, out originalPostItem);
			}
			if (originalCalendarItem != null)
			{
				if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2012) && originalCalendarItem.IsOrganizer() && !(forwardReplyInformation.UpdateItem is ForwardItemType))
				{
					throw new InvalidReferenceItemException();
				}
				if (!originalCalendarItem.IsForwardAllowed)
				{
					throw new ServiceInvalidOperationException((CoreResources.IDs)4004906780U);
				}
			}
			else if (originalMessageItem != null)
			{
				if (!(originalMessageItem is MeetingMessage) && Shape.IsGenericMessageOnly(originalMessageItem) && !ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1))
				{
					throw new InvalidReferenceItemException();
				}
				if (originalMessageItem.IsDraft && !forwardReplyInformation.IsAttachment && (forwardReplyInformation.UpdateItem is ReplyAllToItemType || forwardReplyInformation.UpdateItem is ReplyToItemType))
				{
					throw new InvalidReferenceItemException();
				}
				if (!originalMessageItem.IsReplyAllowed)
				{
					if (forwardReplyInformation.UpdateItem is ReplyAllToItemType)
					{
						throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorInvalidItemForReplyAll);
					}
					if (forwardReplyInformation.UpdateItem is ReplyToItemType)
					{
						throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorInvalidItemForReply);
					}
				}
			}
			else
			{
				if (originalPostItem != null)
				{
					return;
				}
				throw new InvalidReferenceItemException();
			}
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0005F47C File Offset: 0x0005D67C
		private void ValidateForwardReplyMessageDisposition()
		{
			base.RequireMessageDisposition();
			if (!base.IsSaveToFolderIdSessionPublicFolderSession())
			{
				return;
			}
			switch (this.messageDisposition.Value)
			{
			case MessageDispositionType.SaveOnly:
				throw new ServiceInvalidOperationException((CoreResources.IDs)4104292452U);
			case MessageDispositionType.SendAndSaveCopy:
				throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorInvalidOperationSendAndSaveCopyToPublicFolder);
			default:
				return;
			}
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x0005F4D4 File Offset: 0x0005D6D4
		private ItemType[] PerformSuppressReadReceipt(ItemType responseObject)
		{
			ItemType itemType = null;
			CreateItem.ResponseObjectInformation mailboxItemResponseObjectInformation = this.GetMailboxItemResponseObjectInformation(responseObject, base.IdConverter, true);
			IdAndSession referenceIdAndSession = mailboxItemResponseObjectInformation.ReferenceIdAndSession;
			if (!(referenceIdAndSession.Session is MailboxSession))
			{
				throw new InvalidReferenceItemException();
			}
			this.ValidateOperationForGroupMailbox(responseObject, referenceIdAndSession);
			using (Item rootXsoItem = referenceIdAndSession.GetRootXsoItem(new PropertyDefinition[]
			{
				MessageItemSchema.IsReadReceiptPending
			}))
			{
				MessageItem messageItem = rootXsoItem as MessageItem;
				if (messageItem == null)
				{
					throw new InvalidReferenceItemException();
				}
				MailboxSession mailboxSession = referenceIdAndSession.Session as MailboxSession;
				if (!(bool)messageItem[MessageItemSchema.IsReadReceiptPending] || mailboxSession.IsDefaultFolderType(messageItem.ParentId) == DefaultFolderType.JunkEmail)
				{
					throw new ReadReceiptNotPendingException();
				}
				if (messageItem.IsRead)
				{
					mailboxSession.MarkAsRead(true, new StoreId[]
					{
						referenceIdAndSession.Id
					});
				}
				else
				{
					mailboxSession.MarkAsUnread(true, new StoreId[]
					{
						referenceIdAndSession.Id
					});
				}
				messageItem.Load();
				itemType = new MessageType();
				base.LoadServiceObject(itemType, messageItem, referenceIdAndSession, this.responseShape);
			}
			return new ItemType[]
			{
				itemType
			};
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0005F604 File Offset: 0x0005D804
		private CreateItem.ResponseObjectInformation GetMailboxItemResponseObjectInformation(ItemType responseObject, IdConverter idConverter, bool isReadWriteReferenceIdAndSession)
		{
			CreateItem.ResponseObjectInformation responseObjectInformation = new CreateItem.ResponseObjectInformation((ResponseObjectType)responseObject, idConverter, isReadWriteReferenceIdAndSession);
			if (responseObjectInformation.ReferenceIdAndSession.Session is PublicFolderSession)
			{
				throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorInvalidOperationForPublicFolderItems);
			}
			return responseObjectInformation;
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x0005F644 File Offset: 0x0005D844
		internal ItemType[] PostReplyItem(ItemType serviceItem)
		{
			ItemType itemType = null;
			CreateItem.PostReplyInformation postReplyInformation = new CreateItem.PostReplyInformation((PostReplyItemType)serviceItem, base.IdConverter);
			using (Item rootXsoItem = postReplyInformation.ReferenceIdAndSession.GetRootXsoItem(CreateItem.forwardReplyPropertyDefinitionArray))
			{
				using (PostItem postReplyItem = this.GetPostReplyItem(rootXsoItem, postReplyInformation, base.CallContext.ClientCulture))
				{
					itemType = this.UpdateNewItem(postReplyItem, postReplyInformation.UpdateItem);
				}
			}
			return new ItemType[]
			{
				itemType
			};
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0005F6DC File Offset: 0x0005D8DC
		private ItemType CreateStoreItem(ItemType item, out ServiceError warning)
		{
			warning = null;
			ItemType result = null;
			base.CallContext.ADRecipientSessionContext.ExcludeInactiveMailboxInADRecipientSession();
			using (Item item2 = this.CreateStoreItemFromServiceObject(item, out warning))
			{
				result = this.UpdateNewItem(item2, item);
			}
			return result;
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0005F730 File Offset: 0x0005D930
		private Item CreateStoreItemFromServiceObject(ItemType serviceItem, out ServiceError warning)
		{
			warning = null;
			Item result;
			if (serviceItem is MessageType)
			{
				bool flag = serviceItem.IsAssociated != null && serviceItem.IsAssociated.Value;
				serviceItem.IsAssociated = null;
				this.ValidateMessageOperationAttribute(flag);
				IdAndSession idAndSession = base.GetMessageParentFolderIdAndSession();
				MessageItem messageItem;
				if (flag)
				{
					if (!(idAndSession.Session is PublicFolderSession) && idAndSession.Session.LogonType == LogonType.Delegated)
					{
						throw new ServiceInvalidOperationException((CoreResources.IDs)3721795127U);
					}
					messageItem = MessageItem.CreateAssociated(idAndSession.Session, idAndSession.Id);
				}
				else if (base.Request.ClientSupportsIrm && IrmUtils.IsApplyingRmsTemplate(base.Request.ComplianceId, idAndSession.Session, out this.rmsTemplate))
				{
					MailboxSession mailboxSession = (MailboxSession)idAndSession.Session;
					OutboundConversionOptions outboundConversionOptions = IrmUtils.GetOutboundConversionOptions(mailboxSession.MailboxOwner.MailboxInfo.OrganizationId);
					messageItem = RightsManagedMessageItem.Create(mailboxSession, idAndSession.Id, outboundConversionOptions);
				}
				else
				{
					messageItem = MessageItem.Create(idAndSession.Session, idAndSession.Id);
				}
				if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1))
				{
					string itemClass = serviceItem.ItemClass;
					if (itemClass != null)
					{
						messageItem.ClassName = itemClass;
					}
				}
				messageItem.IsDeliveryReceiptRequested = false;
				messageItem.IsReadReceiptRequested = false;
				result = messageItem;
			}
			else if (serviceItem is EwsCalendarItemType)
			{
				this.ValidateCalendarOperationAttribute();
				IdAndSession idAndSession = this.GetParentFolderIdAndSession(DefaultFolderType.Calendar, StoreObjectType.CalendarFolder);
				if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
				{
					idAndSession.Session.ExTimeZone = RecurrenceHelper.MeetingTimeZone.GetMeetingTimeZone(((EwsCalendarItemType)serviceItem).MeetingTimeZone, out warning);
				}
				if (serviceItem.MimeContent != null)
				{
					result = MimeContentProperty.CreateCalendarItemFromICAL(serviceItem.MimeContent, idAndSession.Session, idAndSession.GetAsStoreObjectId());
				}
				else
				{
					result = CalendarItem.Create(idAndSession.Session, idAndSession.Id);
				}
			}
			else if (serviceItem is TaskType)
			{
				IdAndSession idAndSession = this.GetParentFolderIdAndSession(DefaultFolderType.Tasks, StoreObjectType.TasksFolder);
				if (ExchangeVersion.Current.Equals(ExchangeVersion.Exchange2007SP1))
				{
					if (RecurrenceHelper.RequestTimeZone.TimeZoneContextIsAvailable())
					{
						idAndSession.Session.ExTimeZone = EWSSettings.RequestTimeZone;
					}
				}
				else if (ExchangeVersion.Current.Equals(ExchangeVersion.Exchange2007))
				{
					idAndSession.Session.ExTimeZone = RecurrenceHelper.MeetingTimeZone.DefaultMeetingTimeZone;
				}
				result = Task.Create(idAndSession.Session, idAndSession.Id);
			}
			else if (serviceItem is ContactItemType)
			{
				IdAndSession idAndSession = this.GetParentFolderIdAndSession(DefaultFolderType.Contacts, StoreObjectType.ContactsFolder);
				result = Contact.Create(idAndSession.Session, idAndSession.Id);
			}
			else if (serviceItem is DistributionListType)
			{
				if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
				{
					throw new InvalidItemForOperationException("CreateItem");
				}
				IdAndSession idAndSession = this.GetParentFolderIdAndSession(DefaultFolderType.Contacts, StoreObjectType.ContactsFolder);
				result = DistributionList.Create(idAndSession.Session, idAndSession.Id);
			}
			else if (serviceItem is PostItemType)
			{
				IdAndSession idAndSession = this.GetPostItemParentFolderIdAndSession();
				result = PostItem.Create(idAndSession.Session, idAndSession.Id);
			}
			else
			{
				if (serviceItem == null)
				{
					throw new InvalidItemForOperationException("CreateItem");
				}
				IdAndSession idAndSession = this.GetParentFolderIdAndSession(DefaultFolderType.Drafts);
				result = Item.Create(idAndSession.Session, string.Empty, idAndSession.Id);
			}
			return result;
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0005FA34 File Offset: 0x0005DC34
		private IdAndSession GetParentFolderIdAndSession(DefaultFolderType defaultFolderType)
		{
			IdAndSession result;
			if (this.saveToFolderIdAndSession == null)
			{
				result = base.GetDefaultParentFolderIdAndSession(defaultFolderType);
			}
			else
			{
				result = this.saveToFolderIdAndSession;
			}
			return result;
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0005FA5B File Offset: 0x0005DC5B
		private IdAndSession GetParentFolderIdAndSession(DefaultFolderType defaultFolderType, StoreObjectType storeObjectType)
		{
			if (this.saveToFolderIdAndSession != null)
			{
				CreateItem.ValidateFolderType(this.saveToFolderIdAndSession.GetAsStoreObjectId(), storeObjectType);
			}
			return this.GetParentFolderIdAndSession(defaultFolderType);
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0005FA80 File Offset: 0x0005DC80
		private IdAndSession GetPostItemParentFolderIdAndSession()
		{
			IdAndSession idAndSession;
			if (this.saveToFolderIdAndSession != null)
			{
				idAndSession = this.saveToFolderIdAndSession;
				StoreObjectType objectType = StoreId.GetStoreObjectId(idAndSession.Id).ObjectType;
				if (!this.IsMailFolder(objectType))
				{
					throw new CannotCreatePostItemInNonMailFolderException();
				}
			}
			else
			{
				idAndSession = base.GetDefaultParentFolderIdAndSession(DefaultFolderType.Drafts);
			}
			return idAndSession;
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0005FAC8 File Offset: 0x0005DCC8
		private ItemType UpdateNewItem(Item xsoItem, ItemType serviceObject)
		{
			if (base.Request.ClientSupportsIrm && !string.IsNullOrEmpty(base.Request.ComplianceId))
			{
				IrmUtils.UpdateCompliance(base.Request.ComplianceId, xsoItem, this.rmsTemplate);
			}
			if (base.CallContext.IsOwa && xsoItem.IsNew)
			{
				base.SetBodyCharsetOptions(xsoItem);
			}
			base.SetProperties(xsoItem, serviceObject);
			this.charBuffer = new char[32768];
			ConflictResolutionResult conflictResolutionResult;
			return base.ExecuteOperation(xsoItem, this.responseShape, ConflictResolutionType.AlwaysOverwrite, out conflictResolutionResult);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x0005FB50 File Offset: 0x0005DD50
		private void ValidateCalendarOperationAttribute()
		{
			if (this.sendMeetingInvitations == null)
			{
				throw new SendMeetingInvitationsRequiredException();
			}
			if (this.sendMeetingInvitations != CalendarItemOperationType.CreateOrDelete.SendToNone && base.IsSaveToFolderIdSessionPublicFolderSession())
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)2990730164U);
			}
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0005FBA8 File Offset: 0x0005DDA8
		private void ValidateMessageOperationAttribute(bool isAssociated)
		{
			base.CallContext.AuthZBehavior.OnCreateMessageItem(isAssociated);
			base.RequireMessageDisposition();
			if (isAssociated && this.messageDisposition.Value != MessageDispositionType.SaveOnly)
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)3281131813U);
			}
			if (this.messageDisposition.Value == MessageDispositionType.SendAndSaveCopy && base.IsSaveToFolderIdSessionPublicFolderSession())
			{
				throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorInvalidOperationSendAndSaveCopyToPublicFolder);
			}
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0005FC13 File Offset: 0x0005DE13
		private bool IsMailFolder(StoreObjectType folderType)
		{
			return folderType != StoreObjectType.CalendarFolder && folderType != StoreObjectType.ContactsFolder && folderType != StoreObjectType.TasksFolder && folderType != StoreObjectType.NotesFolder && folderType != StoreObjectType.JournalFolder && folderType != StoreObjectType.SearchFolder && folderType != StoreObjectType.OutlookSearchFolder;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x0005FC38 File Offset: 0x0005DE38
		internal ItemType[] PerformAcceptSharingInvitation(ItemType responseObject)
		{
			CreateItem.ResponseObjectInformation mailboxItemResponseObjectInformation = this.GetMailboxItemResponseObjectInformation(responseObject, base.IdConverter, false);
			IdAndSession referenceIdAndSession = mailboxItemResponseObjectInformation.ReferenceIdAndSession;
			if (!(referenceIdAndSession.Session is MailboxSession))
			{
				throw new InvalidReferenceItemException();
			}
			this.SubscribeToSharingItem(referenceIdAndSession);
			return new ItemType[1];
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0005FC80 File Offset: 0x0005DE80
		protected override void DeleteDraftCreatedOnSendFailure(LocalizedException localizedException, MailboxSession mailboxSession, StoreId storeId)
		{
			SmartResponseType smartResponseType = this.items[base.CurrentStep] as SmartResponseType;
			if (smartResponseType != null && smartResponseType.UpdateResponseItemId != null)
			{
				return;
			}
			if (mailboxSession == null || storeId == null)
			{
				return;
			}
			try
			{
				mailboxSession.Delete(DeleteItemFlags.HardDelete, new StoreId[]
				{
					storeId
				});
			}
			catch (LocalizedException arg)
			{
				ExTraceGlobals.CreateItemCallTracer.TraceError<LocalizedException>((long)this.GetHashCode(), "CreateItem.DeleteDraftCreatedOnSendFailure: Exception thrown on delete of draft: {0}", arg);
			}
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0005FCF4 File Offset: 0x0005DEF4
		protected override ConflictResolutionResult ExecuteCalendarOperation(CalendarItemBase calendarItemBase, ConflictResolutionType resolutionType)
		{
			CalendarItemOperationType.CreateOrDelete createOrDelete = this.sendMeetingInvitations.Value;
			MailboxSession mailboxSession = (base.SaveToFolderIdAndSession != null) ? (base.SaveToFolderIdAndSession.Session as MailboxSession) : null;
			if (mailboxSession != null && mailboxSession.IsGroupMailbox())
			{
				IAttendeeCollection attendeeCollection = calendarItemBase.AttendeeCollection;
				Participant participant = new Participant(mailboxSession.MailboxOwner);
				attendeeCollection.Add(participant, AttendeeType.Required, null, null, true);
				Participant participant2 = new Participant(base.CallContext.AccessingPrincipal);
				attendeeCollection.Add(participant2, AttendeeType.Required, null, null, true);
				calendarItemBase.IsMeeting = true;
				if (base.Request.IsDraftEvent)
				{
					createOrDelete = CalendarItemOperationType.CreateOrDelete.SendToNone;
				}
				else
				{
					createOrDelete = CalendarItemOperationType.CreateOrDelete.SendToAllAndSaveCopy;
				}
			}
			ConflictResolutionResult result;
			if (ServiceCommandBase.IsOrganizerMeeting(calendarItemBase))
			{
				switch (createOrDelete)
				{
				case CalendarItemOperationType.CreateOrDelete.SendToNone:
					calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(47263U, LastChangeAction.ExecuteEWSCalendarOperation);
					result = base.SaveXsoItem(calendarItemBase, resolutionType);
					break;
				case CalendarItemOperationType.CreateOrDelete.SendOnlyToAll:
					calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(50335U, LastChangeAction.ExecuteEWSCalendarOperation);
					result = this.SendMeetingMessages(calendarItemBase, true, false);
					break;
				case CalendarItemOperationType.CreateOrDelete.SendToAllAndSaveCopy:
					calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(33951U, LastChangeAction.ExecuteEWSCalendarOperation);
					result = this.SendMeetingMessages(calendarItemBase, true, true);
					break;
				default:
					throw new CalendarExceptionInvalidAttributeValue(new PropertyUri(PropertyUriEnum.CalendarItemType));
				}
			}
			else
			{
				calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(63647U, LastChangeAction.ExecuteEWSCalendarOperation);
				result = base.SaveXsoItem(calendarItemBase, resolutionType);
			}
			return result;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0005FE64 File Offset: 0x0005E064
		private ConflictResolutionResult SendMeetingMessages(CalendarItemBase calendarItem, bool isToAllAttendees, bool copyToSentItems)
		{
			try
			{
				calendarItem.SendMeetingMessages(isToAllAttendees, null, true, copyToSentItems, null, null);
			}
			catch (SaveConflictException ex)
			{
				return ex.ConflictResolutionResult;
			}
			catch (DumpsterOperationException innerException)
			{
				throw new ObjectSaveException(innerException, true);
			}
			catch (PropertyErrorException ex2)
			{
				throw new ObjectSavePropertyErrorException(SearchSchemaMap.GetPropertyPaths(ex2.PropertyErrors), ex2, true);
			}
			catch (PropertyValidationException ex3)
			{
				throw new ObjectSavePropertyErrorException(SearchSchemaMap.GetPropertyPaths(ex3.PropertyValidationErrors), ex3, true);
			}
			catch (ObjectValidationException ex4)
			{
				throw new ObjectSavePropertyErrorException(SearchSchemaMap.GetPropertyPaths(ex4.Errors), ex4, true);
			}
			catch (StoragePermanentException ex5)
			{
				bool flag = ex5.InnerException != null;
				if (!flag)
				{
					throw;
				}
				if (ex5.InnerException is MapiExceptionCrossPostDenied)
				{
					throw new ObjectSaveException(ex5, true);
				}
				if (ex5.InnerException is MapiExceptionInvalidParameter)
				{
					throw new ObjectSaveException(ex5, true);
				}
				if (ex5.InnerException is MapiExceptionJetErrorColumnTooBig)
				{
					throw new ObjectSaveException(ex5, true);
				}
				if (ex5.InnerException is MapiExceptionJetWarningColumnMaxTruncated)
				{
					throw new ObjectSaveException(ex5, true);
				}
				if (ex5.InnerException is MapiExceptionUnexpectedType)
				{
					throw new ObjectCorruptException(ex5, true);
				}
				if (ex5.InnerException is MapiExceptionFailCallback)
				{
					throw new ObjectSaveException(ex5, true);
				}
				throw;
			}
			return ConflictResolutionResult.Success;
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0005FFDC File Offset: 0x0005E1DC
		private static ResponseType ItemToResponseType(ItemType responseObject)
		{
			ResponseType result = ResponseType.None;
			if (CreateItem.responseTypeMap.Member.TryGetValue(responseObject.GetType(), out result))
			{
				return result;
			}
			return ResponseType.None;
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00060008 File Offset: 0x0005E208
		private static void SetNewBodyContent(Item item, BodyContentType newBodyContent)
		{
			if (newBodyContent != null && !string.IsNullOrEmpty(newBodyContent.Value))
			{
				BodyType bodyType = newBodyContent.BodyType;
				Microsoft.Exchange.Data.Storage.BodyFormat bodyFormat = (bodyType == BodyType.HTML) ? Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml : Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain;
				string value = newBodyContent.Value;
				using (TextWriter textWriter = item.Body.OpenTextWriter(bodyFormat))
				{
					textWriter.Write(value);
				}
			}
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0006006C File Offset: 0x0005E26C
		private static void SetMailResponseBodyContent(MessageItem messageItem, BodyContentType newBodyContent, string bodyPrefix)
		{
			if (newBodyContent != null && !string.IsNullOrEmpty(newBodyContent.Value))
			{
				BodyType bodyType = newBodyContent.BodyType;
				Microsoft.Exchange.Data.Storage.BodyFormat sourceFormat = (bodyType == BodyType.HTML) ? Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml : Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain;
				BodyWriteConfiguration bodyWriteConfiguration = new BodyWriteConfiguration(sourceFormat);
				bodyWriteConfiguration.AddInjectedText(bodyPrefix, null, BodyInjectionFormat.Text);
				string value = newBodyContent.Value;
				using (TextWriter textWriter = messageItem.Body.OpenTextWriter(bodyWriteConfiguration))
				{
					textWriter.Write(value);
				}
			}
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x000600E4 File Offset: 0x0005E2E4
		private static Recurrence GetClientRecurrencePatternUsingCalendarItemBaseTimeZone(ItemInformationType itemInfo, CalendarItemBase calendarItemBase)
		{
			Recurrence result = null;
			if (calendarItemBase != null && calendarItemBase.CalendarItemType == CalendarItemType.RecurringMaster)
			{
				CalendarItem calendarItem = calendarItemBase as CalendarItem;
				if (itemInfo.Recurrence != null)
				{
					RecurrenceHelper.Recurrence.Parse(calendarItem.Recurrence.CreatedExTimeZone, itemInfo.Recurrence, out result);
				}
			}
			return result;
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x00060128 File Offset: 0x0005E328
		private CalendarItemBase GetOrCreateCorrelatedItem(MeetingRequest meetingRequest, PropertyDefinition[] properties)
		{
			CalendarItemBase calendarItemBase = null;
			bool flag = false;
			try
			{
				ExTraceGlobals.CalendarDataTracer.TraceDebug<string>((long)this.GetHashCode(), "Retrieving calendar item assosiated with meeting message. Id='{0}'", meetingRequest.InternetMessageId);
				calendarItemBase = meetingRequest.GetCorrelatedItemForEWS();
				if (!meetingRequest.IsOutOfDate(calendarItemBase))
				{
					ExTraceGlobals.CalendarDataTracer.TraceDebug<string>((long)this.GetHashCode(), "Calling MeetingMessage.UpdateCalendarItem() to process existing CalendarItem. Id='{0}'", meetingRequest.InternetMessageId);
					meetingRequest.OpenAsReadWrite();
					if (meetingRequest.TryUpdateCalendarItem(ref calendarItemBase, meetingRequest.IsDelegated()))
					{
						base.SaveXsoItem(calendarItemBase, ConflictResolutionType.AlwaysOverwrite);
						base.SaveXsoItem(meetingRequest, ConflictResolutionType.AlwaysOverwrite);
						meetingRequest.Load(properties);
						flag = true;
						return calendarItemBase;
					}
				}
				throw new CalendarExceptionMeetingRequestIsOutOfDate();
			}
			catch (ObjectNotFoundException ex)
			{
				if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1))
				{
					throw new ServiceAccessDeniedException(CoreResources.IDs.MessageCalendarInsufficientPermissionsToDefaultCalendarFolder, ex);
				}
				ExTraceGlobals.CalendarDataTracer.TraceError<ObjectNotFoundException>((long)this.GetHashCode(), "CalendarItem associated with MeetingMessage could not be found. Exception (Permanent) '{0}'.", ex);
				throw;
			}
			catch (StoragePermanentException arg)
			{
				ExTraceGlobals.CalendarDataTracer.TraceError<StoragePermanentException>((long)this.GetHashCode(), "CalendarItem associated with MeetingMessage could not be found. Exception (Permanent) '{0}'.", arg);
				throw;
			}
			catch (StorageTransientException arg2)
			{
				ExTraceGlobals.CalendarDataTracer.TraceError<StorageTransientException>((long)this.GetHashCode(), "CalendarItem associated with MeetingMessage could not be found. Exception (Transient) '{0}'.", arg2);
				throw;
			}
			finally
			{
				if (!flag && calendarItemBase != null)
				{
					calendarItemBase.Dispose();
				}
			}
			CalendarItemBase result;
			return result;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x00060278 File Offset: 0x0005E478
		private ItemType MoveItem(Item srcItem, IdAndSession dstFolderIdAndSession, DeleteItemFlags? deleteFlags)
		{
			StoreSession session = srcItem.Session;
			ItemType itemType = null;
			Folder folder;
			try
			{
				folder = Folder.Bind(dstFolderIdAndSession.Session, dstFolderIdAndSession.Id, null);
			}
			catch (ObjectNotFoundException innerException)
			{
				if (!ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1))
				{
					throw;
				}
				ServiceAccessDeniedException ex = new ServiceAccessDeniedException((CoreResources.IDs)3694049238U, innerException);
				this.UpdateExceptionInfo(ex, "AccessingDestinationFolder", srcItem, dstFolderIdAndSession);
				throw ex;
			}
			bool flag = dstFolderIdAndSession.Session == session;
			using (folder)
			{
				srcItem.Load(new PropertyDefinition[]
				{
					ItemSchema.Id
				});
				AggregateOperationResult aggregateOperationResult = session.Move(dstFolderIdAndSession.Session, dstFolderIdAndSession.Id, flag, deleteFlags, new StoreId[]
				{
					srcItem.Id
				});
				OperationResult operationResult = aggregateOperationResult.OperationResult;
				if (operationResult == OperationResult.Failed)
				{
					MoveCopyException ex2 = new MoveCopyException();
					this.UpdateExceptionInfo(ex2, "MoveToDestinationFolder", srcItem, dstFolderIdAndSession);
					throw ex2;
				}
				if (flag && aggregateOperationResult.GroupOperationResults != null && aggregateOperationResult.GroupOperationResults.Length > 0 && aggregateOperationResult.GroupOperationResults[0].ObjectIds.Count > 0 && aggregateOperationResult.GroupOperationResults[0].ResultObjectIds != null && aggregateOperationResult.GroupOperationResults[0].ResultObjectIds.Count > 0)
				{
					StoreObjectId storeObjectId = aggregateOperationResult.GroupOperationResults[0].ResultObjectIds[0];
					StoreId storeId = IdConverter.CombineStoreObjectIdWithChangeKey(aggregateOperationResult.GroupOperationResults[0].ResultObjectIds[0], aggregateOperationResult.GroupOperationResults[0].ResultChangeKeys[0]);
					itemType = ItemType.CreateFromStoreObjectType(storeObjectId.ObjectType);
					ConcatenatedIdAndChangeKey concatenatedId = IdConverter.GetConcatenatedId(storeId, dstFolderIdAndSession, null);
					itemType.ItemId = new ItemId
					{
						Id = concatenatedId.Id,
						ChangeKey = concatenatedId.ChangeKey
					};
				}
			}
			return itemType;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00060488 File Offset: 0x0005E688
		private void UpdateExceptionInfo(ServicePermanentException servicePermanentException, string failedOperation, Item srcItem, IdAndSession dstFolderId)
		{
			if (!string.IsNullOrEmpty(failedOperation))
			{
				servicePermanentException.ConstantValues.Add("FailedOperation", failedOperation);
			}
			if (srcItem != null)
			{
				string value;
				if (srcItem is CalendarItemBase)
				{
					value = "CalendarItem";
				}
				else if (srcItem is MeetingRequest)
				{
					value = "MeetingRequest";
				}
				else if (srcItem is MeetingCancellation)
				{
					value = "MeetingCancellation";
				}
				else
				{
					value = "Item";
				}
				servicePermanentException.ConstantValues.Add("SourceItemClassName", srcItem.ClassName);
				servicePermanentException.ConstantValues.Add("SourceItemType", value);
			}
			if (dstFolderId != null && dstFolderId.Session != null)
			{
				MailboxSession mailboxSession = dstFolderId.Session as MailboxSession;
				if (mailboxSession != null)
				{
					if (mailboxSession.MailboxOwner != null && !string.IsNullOrEmpty(mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()))
					{
						servicePermanentException.ConstantValues.Add("TargetMailboxOwner", mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
					}
					if (mailboxSession.DelegateUser != null)
					{
						SmtpAddress primarySmtpAddress = mailboxSession.DelegateUser.PrimarySmtpAddress;
						servicePermanentException.ConstantValues.Add("DelegateUser", mailboxSession.DelegateUser.PrimarySmtpAddress.ToString());
					}
				}
			}
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x000605D0 File Offset: 0x0005E7D0
		internal ItemType[] PerformAcceptDeclineTentativeItem(MeetingRegistrationResponseObjectType meetingRegistration)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			this.ValidateMeetingRegistration(meetingRegistration);
			base.RequireMessageDisposition();
			IdAndSession idAndSession = null;
			ResponseType responseType = CreateItem.ItemToResponseType(meetingRegistration);
			CreateItem.ResponseObjectInformation mailboxItemResponseObjectInformation;
			try
			{
				mailboxItemResponseObjectInformation = this.GetMailboxItemResponseObjectInformation(meetingRegistration, base.IdConverter, false);
			}
			catch (ObjectNotFoundException)
			{
				if (this.ProcessResponseFailureAndCreateMessageIfNeeded(meetingRegistration))
				{
					throw new CalendarExceptionMeetingIsOutOfDateResponseNotProcessedMessageSent();
				}
				throw;
			}
			stopwatch.Stop();
			base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.GetMailboxItemResponseObjectInformation, stopwatch.ElapsedMilliseconds);
			stopwatch.Restart();
			string emailAddress;
			ItemType[] result;
			using (base.GetDelegateSessionHandleWrapperAndWorkflowContext(mailboxItemResponseObjectInformation.ReferenceIdAndSession, out idAndSession, out emailAddress))
			{
				this.ValidateOperationForGroupMailbox(meetingRegistration, idAndSession);
				using (Item xsoItemForUpdate = ServiceCommandBase.GetXsoItemForUpdate(idAndSession, ToXmlPropertyList.Empty))
				{
					IdAndSession idAndSession2 = base.IdConverter.ConvertDefaultFolderType(DefaultFolderType.DeletedItems, emailAddress);
					bool flag = xsoItemForUpdate.ParentId.Equals(idAndSession2.Id);
					bool intendToSendResponse = this.messageDisposition != null && (this.messageDisposition.Value == MessageDispositionType.SendAndSaveCopy || this.messageDisposition.Value == MessageDispositionType.SendOnly);
					stopwatch.Stop();
					base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.PrepareItem, stopwatch.ElapsedMilliseconds);
					MeetingRequest meetingRequest = xsoItemForUpdate as MeetingRequest;
					if (meetingRequest != null)
					{
						result = this.PerformResponseToMeetingRequest(meetingRequest, responseType, intendToSendResponse, mailboxItemResponseObjectInformation, flag, idAndSession2, meetingRegistration.GetType().Name);
					}
					else
					{
						CalendarItemBase calendarItemBase = xsoItemForUpdate as CalendarItemBase;
						if (calendarItemBase == null)
						{
							throw new InvalidItemForOperationException(meetingRegistration.GetType().Name);
						}
						if (calendarItemBase.IsOrganizer())
						{
							throw new CalendarExceptionIsOrganizer(meetingRegistration.GetType().Name);
						}
						if (flag && calendarItemBase is CalendarItemOccurrence)
						{
							throw new InvalidItemForOperationException(meetingRegistration.GetType().Name);
						}
						if (calendarItemBase.IsCancelled)
						{
							if (this.ProcessResponseFailureAndCreateMessageIfNeeded(meetingRegistration, calendarItemBase))
							{
								throw new CalendarExceptionIsCancelledMessageSent();
							}
							throw new CalendarExceptionIsCancelled(meetingRegistration.GetType().Name);
						}
						else if (this.ShouldResponseFail(mailboxItemResponseObjectInformation.ReferenceItemId, meetingRegistration.AdditionalInfo, calendarItemBase))
						{
							if (this.ProcessResponseFailureAndCreateMessageIfNeeded(meetingRegistration, calendarItemBase))
							{
								throw new CalendarExceptionMeetingIsOutOfDateResponseNotProcessedMessageSent();
							}
							throw new CalendarExceptionMeetingIsOutOfDateResponseNotProcessed();
						}
						else
						{
							result = this.PerformResponseToCalendarItemBase(calendarItemBase, responseType, intendToSendResponse, mailboxItemResponseObjectInformation, flag, idAndSession2);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x00060850 File Offset: 0x0005EA50
		private void ValidateMeetingRegistration(MeetingRegistrationResponseObjectType meetingRegistration)
		{
			if (!meetingRegistration.HasStartAndEndProposedTimeOrNone())
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)2326085984U);
			}
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0006086C File Offset: 0x0005EA6C
		private bool HasLocationChanged(ItemInformationType itemInfo, string calendarItemLocation, string calendarItemLocationUri)
		{
			if (itemInfo.Location == null)
			{
				return !string.IsNullOrEmpty(calendarItemLocation);
			}
			if (itemInfo.Location.PostalAddress == null || itemInfo.Location.PostalAddress.LocationSource == LocationSourceType.None)
			{
				return (!string.IsNullOrEmpty(itemInfo.Location.DisplayName) || !string.IsNullOrEmpty(calendarItemLocation)) && itemInfo.Location.DisplayName != calendarItemLocation;
			}
			return itemInfo.Location.PostalAddress.LocationUri != calendarItemLocationUri;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x000608F0 File Offset: 0x0005EAF0
		private bool ShouldResponseFail(BaseItemId referencedItemId, ItemInformationType itemInfo, CalendarItemBase calendarItem)
		{
			if (!base.Request.FailResponseOnImportantUpdate)
			{
				return false;
			}
			if (!this.HasAdditionalInformationForConflictResolution(itemInfo))
			{
				IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadWrite(referencedItemId);
				return idAndSession.Id is VersionedId && !((VersionedId)idAndSession.Id).Equals(calendarItem.Id);
			}
			ExDateTime exDateTime = ExDateTime.ParseISO(EWSSettings.RequestTimeZone, itemInfo.Start);
			ExDateTime exDateTime2 = ExDateTime.ParseISO(EWSSettings.RequestTimeZone, itemInfo.End);
			if (!exDateTime.Equals(calendarItem.StartTime) || !exDateTime2.Equals(calendarItem.EndTime))
			{
				return true;
			}
			if (calendarItem.CalendarItemType == CalendarItemType.RecurringMaster)
			{
				CalendarItem calendarItem2 = calendarItem as CalendarItem;
				Recurrence clientRecurrencePatternUsingCalendarItemBaseTimeZone = CreateItem.GetClientRecurrencePatternUsingCalendarItemBaseTimeZone(itemInfo, calendarItem2);
				return clientRecurrencePatternUsingCalendarItemBaseTimeZone == null || !calendarItem2.Recurrence.Equals(clientRecurrencePatternUsingCalendarItemBaseTimeZone);
			}
			return false;
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x000609BE File Offset: 0x0005EBBE
		private bool HasAdditionalInformationForConflictResolution(ItemInformationType itemInfo)
		{
			return itemInfo.Start != null && itemInfo.End != null && itemInfo.Organizer != null && itemInfo.Subject != null && itemInfo.Location != null && itemInfo.ConversationId != null;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x000609F6 File Offset: 0x0005EBF6
		private bool ProcessResponseFailureAndCreateMessageIfNeeded(WellKnownResponseObjectType responseItem)
		{
			return this.ProcessResponseFailureAndCreateMessageIfNeeded(responseItem, null);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00060A00 File Offset: 0x0005EC00
		private bool ProcessResponseFailureAndCreateMessageIfNeeded(WellKnownResponseObjectType responseItem, CalendarItemBase calendarItem)
		{
			if (!base.Request.GenerateResponseMessageOnFailure || responseItem.AdditionalInfo == null || !responseItem.AdditionalInfo.IsResponseRequested || responseItem.Body == null || string.IsNullOrEmpty(responseItem.Body.Value))
			{
				this.SetClientIntentOnCalendarItem(calendarItem, ClientIntentFlags.MeetingResponseFailedNoContentToSend);
				return false;
			}
			if (!this.HasAdditionalInformationForConflictResolution(responseItem.AdditionalInfo))
			{
				this.SetClientIntentOnCalendarItem(calendarItem, ClientIntentFlags.MeetingResponseFailedNoContentToSend);
				return false;
			}
			IdAndSession messageParentFolderIdAndSession = base.GetMessageParentFolderIdAndSession();
			MessageItem messageItem = MessageItem.Create(messageParentFolderIdAndSession.Session, messageParentFolderIdAndSession.Id);
			using (messageItem)
			{
				CreateItem.SetNewBodyContent(messageItem, responseItem.Body);
				if (messageItem.Body.PreviewText.Trim() == string.Empty)
				{
					this.SetClientIntentOnCalendarItem(calendarItem, ClientIntentFlags.MeetingResponseFailedNoContentToSend);
					return false;
				}
				CreateItem.SetMailResponseBodyContent(messageItem, responseItem.Body, string.Empty);
				messageItem.IsDeliveryReceiptRequested = false;
				messageItem.IsReadReceiptRequested = false;
				messageItem.Subject = responseItem.AdditionalInfo.Subject;
				messageItem.Recipients.Add(MailboxHelper.GetParticipantFromAddress(responseItem.AdditionalInfo.Organizer.Mailbox));
				IdAndSession idAndSession = base.IdConverter.ConvertConversationIdToIdAndSession(responseItem.AdditionalInfo.ConversationId);
				messageItem[ItemSchema.ConversationId] = idAndSession.Id;
				messageItem.Send();
			}
			this.SetClientIntentOnCalendarItem(calendarItem, ClientIntentFlags.MeetingResponseFailedButMessageContentSent);
			return true;
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00060B78 File Offset: 0x0005ED78
		private void SetClientIntentOnCalendarItem(CalendarItemBase calendarItem, ClientIntentFlags clientIntent)
		{
			if (calendarItem != null)
			{
				calendarItem.ClientIntent |= clientIntent;
				calendarItem.Save(SaveMode.ResolveConflicts);
			}
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00060B94 File Offset: 0x0005ED94
		private ItemType[] PerformResponseToCalendarItemBase(CalendarItemBase calendarItemBase, ResponseType responseType, bool intendToSendResponse, CreateItem.ResponseObjectInformation responseObjectInformation, bool itemIsInDeletedItemsFolder, IdAndSession deletedItemsFolder)
		{
			calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(39071U, LastChangeAction.RespondToMeetingRequest);
			ItemType itemType = this.RespondToCalendarItemBase(calendarItemBase, responseType, responseObjectInformation, intendToSendResponse);
			StorePerformanceCountersCapture storePerformanceCountersCapture = StorePerformanceCountersCapture.Start((MailboxSession)calendarItemBase.Session);
			ItemType itemType2;
			if (responseType == ResponseType.Decline)
			{
				DeleteItemFlags deleteItemFlagsForDecline = this.GetDeleteItemFlagsForDecline(itemIsInDeletedItemsFolder, intendToSendResponse);
				if (itemIsInDeletedItemsFolder)
				{
					calendarItemBase.Load();
					calendarItemBase.Session.Delete(deleteItemFlagsForDecline, new StoreId[]
					{
						calendarItemBase.Id
					});
					return new ItemType[]
					{
						itemType
					};
				}
				if (!(calendarItemBase is CalendarItemOccurrence))
				{
					itemType2 = this.MoveItem(calendarItemBase, deletedItemsFolder, new DeleteItemFlags?(deleteItemFlagsForDecline));
					return new ItemType[]
					{
						itemType2,
						itemType
					};
				}
				calendarItemBase.Load();
				calendarItemBase.Session.Delete(deleteItemFlagsForDecline, new StoreId[]
				{
					calendarItemBase.Id
				});
			}
			else if (itemIsInDeletedItemsFolder && (responseType == ResponseType.Accept || responseType == ResponseType.Tentative))
			{
				IdAndSession dstFolderIdAndSession = base.IdConverter.ConvertDefaultFolderType(DefaultFolderType.Calendar, ((MailboxSession)deletedItemsFolder.Session).MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
				itemType2 = this.MoveItem(calendarItemBase, dstFolderIdAndSession, null);
				return new ItemType[]
				{
					itemType2,
					itemType
				};
			}
			calendarItemBase.Load();
			itemType2 = new EwsCalendarItemType();
			base.LoadServiceObject(itemType2, calendarItemBase, IdAndSession.CreateFromItem(calendarItemBase), this.responseShape);
			StorePerformanceCounters storePerformanceCounters = storePerformanceCountersCapture.Stop();
			base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.RespondToCalendarItemBaseRpcCount, storePerformanceCounters.RpcCount);
			base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.RespondToCalendarItemBaseRpcLatency, storePerformanceCounters.RpcLatency);
			base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.RespondToCalendarItemBaseTime, storePerformanceCounters.ElapsedMilliseconds);
			return new ItemType[]
			{
				itemType2,
				itemType
			};
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00060D8C File Offset: 0x0005EF8C
		private DeleteItemFlags GetDeleteItemFlagsForDecline(bool itemIsInDeletedItemsFolder, bool intendToSendResponse)
		{
			DeleteItemFlags deleteItemFlags;
			if (itemIsInDeletedItemsFolder)
			{
				deleteItemFlags = DeleteItemFlags.SoftDelete;
			}
			else
			{
				deleteItemFlags = DeleteItemFlags.MoveToDeletedItems;
			}
			return deleteItemFlags | (intendToSendResponse ? DeleteItemFlags.DeclineCalendarItemWithResponse : DeleteItemFlags.DeclineCalendarItemWithoutResponse);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00060DB8 File Offset: 0x0005EFB8
		private ItemType RespondToCalendarItemBase(CalendarItemBase calendarItemBase, ResponseType responseType, CreateItem.ResponseObjectInformation responseObjectInformation, bool intendToSendResponse)
		{
			StorePerformanceCountersCapture storePerformanceCountersCapture = StorePerformanceCountersCapture.Start((MailboxSession)calendarItemBase.Session);
			string location = calendarItemBase.Location;
			string locationUri = calendarItemBase.LocationUri;
			MeetingRegistrationResponseObjectType meetingRegistrationResponseObjectType = (MeetingRegistrationResponseObjectType)responseObjectInformation.UpdateItem;
			MeetingResponse meetingResponse;
			try
			{
				if (meetingRegistrationResponseObjectType.HasTimeProposal)
				{
					meetingResponse = calendarItemBase.RespondToMeetingRequest(responseType, true, intendToSendResponse, new ExDateTime?(ExDateTimeConverter.ParseTimeZoneRelated(meetingRegistrationResponseObjectType.ProposedStart, EWSSettings.RequestTimeZone)), new ExDateTime?(ExDateTimeConverter.ParseTimeZoneRelated(meetingRegistrationResponseObjectType.ProposedEnd, EWSSettings.RequestTimeZone)));
				}
				else
				{
					meetingResponse = calendarItemBase.RespondToMeetingRequest(responseType, true, intendToSendResponse, null, null);
				}
			}
			catch (InvalidTimeProposalException ex)
			{
				throw new ServiceInvalidOperationException(CoreResources.ErrorTimeProposal(ex.Message), ex);
			}
			catch (ObjectNotFoundException innerException)
			{
				if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1))
				{
					ServiceAccessDeniedException ex2 = new ServiceAccessDeniedException((CoreResources.IDs)3694049238U, innerException);
					this.UpdateExceptionInfo(ex2, "CreateResponseMessageInDrafts", calendarItemBase, null);
					throw ex2;
				}
				throw;
			}
			StorePerformanceCounters storePerformanceCounters = storePerformanceCountersCapture.Stop();
			base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.RespondToMeetingRequestRpcCount, storePerformanceCounters.RpcCount);
			base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.RespondToMeetingRequestRpcLatency, storePerformanceCounters.RpcLatency);
			base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.RespondToMeetingRequestTime, storePerformanceCounters.ElapsedMilliseconds);
			storePerformanceCountersCapture = StorePerformanceCountersCapture.Start((MailboxSession)calendarItemBase.Session);
			ItemType result;
			using (meetingResponse)
			{
				if (meetingRegistrationResponseObjectType.From != null)
				{
					throw new InvalidPropertySetException(CoreResources.IDs.ErrorCannotSetFromOnMeetingResponse, new PropertyUri(PropertyUriEnum.From));
				}
				base.SetProperties(meetingResponse, meetingRegistrationResponseObjectType);
				if (base.Request.GenerateMeetingResponseWithOldLocationIfChanged && this.HasLocationChanged(meetingRegistrationResponseObjectType.AdditionalInfo, location, locationUri))
				{
					string location2 = (meetingRegistrationResponseObjectType.AdditionalInfo.Location != null && meetingRegistrationResponseObjectType.AdditionalInfo.Location.DisplayName != null) ? meetingRegistrationResponseObjectType.AdditionalInfo.Location.DisplayName : string.Empty;
					meetingResponse.Location = location2;
				}
				MailboxSession mailboxSession = (MailboxSession)calendarItemBase.Session;
				if (mailboxSession.DelegateUser != null)
				{
					meetingResponse.From = new Participant(mailboxSession.MailboxOwner);
				}
				ItemType itemType = this.ExecuteMeetingMessageOperation(meetingResponse);
				storePerformanceCounters = storePerformanceCountersCapture.Stop();
				base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.UpdateMeetingTime, storePerformanceCounters.ElapsedMilliseconds);
				base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.UpdateMeetingRpcCount, storePerformanceCounters.RpcCount);
				base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.UpdateMeetingRpcLatency, storePerformanceCounters.RpcLatency);
				result = itemType;
			}
			return result;
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x000610B8 File Offset: 0x0005F2B8
		private ItemType[] PerformResponseToMeetingRequest(MeetingRequest meetingRequest, ResponseType responseType, bool intendToSendResponse, CreateItem.ResponseObjectInformation responseObjectInformation, bool itemIsInDeletedItemsFolder, IdAndSession deletedItemsFolder, string responseObjectName)
		{
			ItemType itemType = null;
			ItemType itemType2 = null;
			ItemType itemType3 = null;
			StorePerformanceCountersCapture storePerformanceCountersCapture = StorePerformanceCountersCapture.Start((MailboxSession)meetingRequest.Session);
			MeetingRegistrationResponseObjectType meetingRegistrationResponseObjectType = (MeetingRegistrationResponseObjectType)responseObjectInformation.UpdateItem;
			CalendarItemBase calendarItemBase = null;
			try
			{
				if (meetingRequest.IsDelegated())
				{
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, CreateAndUpdateItemMetadata.Principal, meetingRequest.ReceivedRepresenting.EmailAddress);
					RequestDetailsLoggerBase<RequestDetailsLogger>.SafeSetLogger(RequestDetailsLogger.Current, CreateAndUpdateItemMetadata.SessionType, LogonType.Delegated);
				}
				calendarItemBase = this.GetOrCreateCorrelatedItem(meetingRequest, CreateItem.calendarProcessedPropertyDefinition);
			}
			catch (CalendarExceptionMeetingRequestIsOutOfDate)
			{
				if (this.ProcessResponseFailureAndCreateMessageIfNeeded(meetingRegistrationResponseObjectType))
				{
					throw new CalendarExceptionMeetingIsOutOfDateResponseNotProcessedMessageSent();
				}
				throw;
			}
			StorePerformanceCounters storePerformanceCounters = storePerformanceCountersCapture.Stop();
			base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.GetCalendarItemBaseRpcCount, storePerformanceCounters.RpcCount);
			base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.GetCalendarItemBaseRpcLatency, storePerformanceCounters.RpcLatency);
			base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.GetCalendarItemBaseTime, storePerformanceCounters.ElapsedMilliseconds);
			using (calendarItemBase)
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				if (calendarItemBase.IsOrganizer())
				{
					throw new CalendarExceptionIsOrganizer(responseObjectName);
				}
				if (calendarItemBase.IsCancelled)
				{
					if (this.ProcessResponseFailureAndCreateMessageIfNeeded(meetingRegistrationResponseObjectType, calendarItemBase))
					{
						throw new CalendarExceptionIsCancelledMessageSent();
					}
					throw new CalendarExceptionIsCancelled(responseObjectName);
				}
				else if (this.ShouldResponseFail(responseObjectInformation.ReferenceItemId, meetingRegistrationResponseObjectType.AdditionalInfo, calendarItemBase))
				{
					if (this.ProcessResponseFailureAndCreateMessageIfNeeded(meetingRegistrationResponseObjectType, calendarItemBase))
					{
						throw new CalendarExceptionMeetingIsOutOfDateResponseNotProcessedMessageSent();
					}
					throw new CalendarExceptionMeetingIsOutOfDateResponseNotProcessed();
				}
				else
				{
					meetingRequest.IsRead = true;
					base.SaveXsoItem(meetingRequest, ConflictResolutionType.AutoResolve);
					calendarItemBase.LocationIdentifierHelperInstance.SetLocationIdentifier(55455U, LastChangeAction.RespondToMeetingRequest);
					stopwatch.Stop();
					base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.Save, stopwatch.ElapsedMilliseconds);
					itemType = this.RespondToCalendarItemBase(calendarItemBase, responseType, responseObjectInformation, intendToSendResponse);
					storePerformanceCountersCapture = StorePerformanceCountersCapture.Start((MailboxSession)calendarItemBase.Session);
					if (responseType == ResponseType.Decline)
					{
						calendarItemBase.Load();
						calendarItemBase.Session.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
						{
							calendarItemBase.Id
						});
					}
					calendarItemBase.Load();
					itemType3 = new EwsCalendarItemType();
					base.LoadServiceObject(itemType3, calendarItemBase, IdAndSession.CreateFromItem(calendarItemBase), this.responseShape);
					storePerformanceCounters = storePerformanceCountersCapture.Stop();
					base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.LoadAndDeleteItemRpcCount, storePerformanceCounters.RpcCount);
					base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.LoadAndDeleteItemRpcLatency, storePerformanceCounters.RpcLatency);
					base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.LoadAndDeleteItemTime, storePerformanceCounters.ElapsedMilliseconds);
				}
			}
			storePerformanceCountersCapture = StorePerformanceCountersCapture.Start((MailboxSession)meetingRequest.Session);
			if (!itemIsInDeletedItemsFolder)
			{
				itemType2 = this.MoveItem(meetingRequest, deletedItemsFolder, new DeleteItemFlags?(DeleteItemFlags.MoveToDeletedItems));
			}
			else
			{
				meetingRequest.Load();
				meetingRequest.Session.Delete(DeleteItemFlags.SoftDelete, new StoreId[]
				{
					meetingRequest.Id
				});
			}
			storePerformanceCounters = storePerformanceCountersCapture.Stop();
			base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.MoveItemRpcCount, storePerformanceCounters.RpcCount);
			base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.MoveItemRpcLatency, storePerformanceCounters.RpcLatency);
			base.CallContext.ProtocolLog.Set(CreateAndUpdateItemMetadata.MoveItemTime, storePerformanceCounters.ElapsedMilliseconds);
			return new ItemType[]
			{
				itemType,
				itemType3,
				itemType2
			};
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0006147C File Offset: 0x0005F67C
		internal ItemType[] PerformCancelCalendarItem(ItemType serviceItem)
		{
			ItemType itemType = null;
			ItemType itemType2 = null;
			IdAndSession idAndSession = null;
			base.RequireMessageDisposition();
			CreateItem.ResponseObjectInformation mailboxItemResponseObjectInformation = this.GetMailboxItemResponseObjectInformation(serviceItem, base.IdConverter, true);
			string emailAddress;
			using (base.GetDelegateSessionHandleWrapperAndWorkflowContext(mailboxItemResponseObjectInformation.ReferenceIdAndSession, out idAndSession, out emailAddress))
			{
				ToXmlPropertyList propertyList = XsoDataConverter.GetPropertyList(idAndSession.Id, idAndSession.Session, this.responseShape);
				using (Item xsoItemForUpdate = ServiceCommandBase.GetXsoItemForUpdate(idAndSession, propertyList))
				{
					CalendarItemBase calendarItemBase = xsoItemForUpdate as CalendarItemBase;
					if (calendarItemBase == null)
					{
						throw new InvalidItemForOperationException(serviceItem.GetType().Name);
					}
					if (!calendarItemBase.IsOrganizer())
					{
						throw new CalendarExceptionIsNotOrganizer();
					}
					using (MeetingCancellation meetingCancellation = calendarItemBase.CancelMeeting(null, null))
					{
						SmartResponseType smartResponseType = serviceItem as SmartResponseType;
						meetingCancellation.IsReadReceiptRequested = (smartResponseType.IsReadReceiptRequestedSpecified && smartResponseType.IsReadReceiptRequested.Value);
						meetingCancellation.IsDeliveryReceiptRequested = (smartResponseType.IsDeliveryReceiptRequestedSpecified && smartResponseType.IsDeliveryReceiptRequested.Value);
						CreateItem.SetNewBodyContent(meetingCancellation, smartResponseType.NewBodyContent);
						itemType2 = this.ExecuteMeetingMessageOperation(meetingCancellation);
						IdAndSession idAndSession2 = base.IdConverter.ConvertDefaultFolderType(DefaultFolderType.DeletedItems, emailAddress);
						if (calendarItemBase is CalendarItemOccurrence)
						{
							calendarItemBase.Session.Delete(DeleteItemFlags.MoveToDeletedItems | DeleteItemFlags.CancelCalendarItem, new StoreId[]
							{
								calendarItemBase.Id
							});
							itemType = new EwsCalendarItemType();
							base.LoadServiceObject(itemType, calendarItemBase, IdAndSession.CreateFromItem(calendarItemBase), this.responseShape);
						}
						else
						{
							if (!xsoItemForUpdate.ParentId.Equals(idAndSession2.Id))
							{
								itemType = this.MoveItem(calendarItemBase, idAndSession2, new DeleteItemFlags?(DeleteItemFlags.MoveToDeletedItems | DeleteItemFlags.CancelCalendarItem));
								return new ItemType[]
								{
									itemType,
									itemType2
								};
							}
							calendarItemBase.Session.Delete(DeleteItemFlags.SoftDelete | DeleteItemFlags.CancelCalendarItem, new StoreId[]
							{
								calendarItemBase.Id
							});
							return new ItemType[]
							{
								itemType2
							};
						}
					}
				}
			}
			return new ItemType[]
			{
				itemType,
				itemType2
			};
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x000616EC File Offset: 0x0005F8EC
		private ItemType ExecuteMeetingMessageOperation(MeetingMessage meetingMessage)
		{
			ConflictResolutionResult conflictResolutionResult;
			return base.ExecuteMessageOperation(meetingMessage, this.responseShape, ConflictResolutionType.AlwaysOverwrite, out conflictResolutionResult);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0006170C File Offset: 0x0005F90C
		internal ItemType[] PerformRemoveItem(ItemType serviceItem)
		{
			IdAndSession idAndSession = null;
			CreateItem.ResponseObjectInformation mailboxItemResponseObjectInformation = this.GetMailboxItemResponseObjectInformation(serviceItem, base.IdConverter, false);
			string emailAddress;
			ItemType[] result;
			using (base.GetDelegateSessionHandleWrapperAndWorkflowContext(mailboxItemResponseObjectInformation.ReferenceIdAndSession, out idAndSession, out emailAddress))
			{
				using (Item xsoItemForUpdate = ServiceCommandBase.GetXsoItemForUpdate(idAndSession, ToXmlPropertyList.Empty))
				{
					IdAndSession idAndSession2 = base.IdConverter.ConvertDefaultFolderType(DefaultFolderType.DeletedItems, emailAddress);
					ItemType itemType = null;
					MeetingCancellation meetingCancellation = xsoItemForUpdate as MeetingCancellation;
					if (meetingCancellation == null)
					{
						throw new InvalidItemForOperationException(serviceItem.GetType().Name);
					}
					if (meetingCancellation.IsOrganizer())
					{
						throw new CalendarExceptionIsOrganizer(serviceItem.GetType().Name);
					}
					using (CalendarItemBase correlatedItemForEWS = meetingCancellation.GetCorrelatedItemForEWS())
					{
						if (correlatedItemForEWS != null)
						{
							CalendarItemOccurrence calendarItemOccurrence = correlatedItemForEWS as CalendarItemOccurrence;
							if (calendarItemOccurrence != null)
							{
								calendarItemOccurrence.Session.Delete(DeleteItemFlags.MoveToDeletedItems, new StoreId[]
								{
									calendarItemOccurrence.Id
								});
							}
							else
							{
								itemType = this.MoveItem(correlatedItemForEWS, idAndSession2, new DeleteItemFlags?(DeleteItemFlags.MoveToDeletedItems));
							}
						}
						ItemType itemType2;
						if (!xsoItemForUpdate.ParentId.Equals(idAndSession2.Id))
						{
							itemType2 = this.MoveItem(meetingCancellation, idAndSession2, new DeleteItemFlags?(DeleteItemFlags.MoveToDeletedItems));
						}
						else
						{
							itemType2 = ItemType.CreateFromStoreObjectType(meetingCancellation.Id.ObjectId.ObjectType);
							base.LoadServiceObject(itemType2, meetingCancellation, IdAndSession.CreateFromItem(meetingCancellation), this.responseShape);
						}
						result = new ItemType[]
						{
							itemType2,
							itemType
						};
					}
				}
			}
			return result;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x000618CC File Offset: 0x0005FACC
		internal void ValidateOperationForGroupMailbox(ItemType serviceItem, IdAndSession storeIdAndSession)
		{
			if (storeIdAndSession != null && storeIdAndSession.Session is MailboxSession && (storeIdAndSession.Session as MailboxSession).IsGroupMailbox())
			{
				throw new CalendarExceptionIsGroupMailbox(serviceItem.GetType().Name);
			}
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x00061901 File Offset: 0x0005FB01
		internal ItemType[] InvalidateAddItemToMyCalendar(ItemType serviceItem)
		{
			throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorInvalidOperationAddItemToMyCalendar);
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00061912 File Offset: 0x0005FB12
		internal ItemType[] InvalidateProposeNewTime(ItemType serviceItem)
		{
			throw new ServiceInvalidOperationException((CoreResources.IDs)3997746891U);
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00061924 File Offset: 0x0005FB24
		internal ItemType[] CreateApprovalRequestResponseItem(ItemType item)
		{
			base.RequireMessageDisposition();
			int num = (item is ApproveRequestItemType) ? 0 : 1;
			CreateItem.ResponseObjectInformation mailboxItemResponseObjectInformation = this.GetMailboxItemResponseObjectInformation(item, base.IdConverter, true);
			ItemType itemType = null;
			using (Item rootXsoItem = mailboxItemResponseObjectInformation.ReferenceIdAndSession.GetRootXsoItem(CreateItem.approvalPropertyDefinitionArray))
			{
				MessageItem messageItem = rootXsoItem as MessageItem;
				if (messageItem == null || !messageItem.IsValidApprovalRequest())
				{
					throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorInvalidApprovalRequest);
				}
				if (!messageItem.IsValidUndecidedApprovalRequest())
				{
					throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorApprovalRequestAlreadyDecided);
				}
				string[] array = (string[])messageItem.VotingInfo.GetOptionsList();
				if (array != null && num < array.Length && !string.IsNullOrEmpty(array[num]))
				{
					MessageItem messageItem2;
					MessageItem xsoItem = messageItem2 = messageItem.CreateVotingResponse(string.Empty, messageItem.Body.Format, base.GetDefaultParentFolderIdAndSession(DefaultFolderType.Drafts).Id, array[num]);
					try
					{
						itemType = this.UpdateNewItem(xsoItem, mailboxItemResponseObjectInformation.UpdateItem);
					}
					finally
					{
						if (messageItem2 != null)
						{
							((IDisposable)messageItem2).Dispose();
						}
					}
				}
			}
			return new ItemType[]
			{
				itemType
			};
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00061A50 File Offset: 0x0005FC50
		internal ItemType[] CreateVotingResponseItem(ItemType item)
		{
			base.RequireMessageDisposition();
			VotingResponseItemType votingResponseItemType = item as VotingResponseItemType;
			if (votingResponseItemType == null || string.IsNullOrEmpty(votingResponseItemType.Response))
			{
				throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorInvalidVotingResponse);
			}
			CreateItem.ResponseObjectInformation mailboxItemResponseObjectInformation = this.GetMailboxItemResponseObjectInformation(item, base.IdConverter, true);
			ItemType itemType = null;
			using (Item rootXsoItem = mailboxItemResponseObjectInformation.ReferenceIdAndSession.GetRootXsoItem(CreateItem.approvalPropertyDefinitionArray))
			{
				MessageItem messageItem = rootXsoItem as MessageItem;
				if (messageItem == null || messageItem.VotingInfo == null || messageItem.IsValidApprovalRequest())
				{
					throw new ServiceInvalidOperationException((CoreResources.IDs)2253496121U);
				}
				IList<string> optionsList = messageItem.VotingInfo.GetOptionsList();
				if (optionsList == null || optionsList.Count == 0)
				{
					throw new ServiceInvalidOperationException((CoreResources.IDs)2253496121U);
				}
				if (!optionsList.Contains(votingResponseItemType.Response))
				{
					throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorInvalidVotingResponse);
				}
				MessageItem messageItem2;
				MessageItem xsoItem = messageItem2 = messageItem.CreateVotingResponse(string.Empty, messageItem.Body.Format, base.GetDefaultParentFolderIdAndSession(DefaultFolderType.Drafts).Id, votingResponseItemType.Response);
				try
				{
					itemType = this.UpdateNewItem(xsoItem, mailboxItemResponseObjectInformation.UpdateItem);
				}
				finally
				{
					if (messageItem2 != null)
					{
						((IDisposable)messageItem2).Dispose();
					}
				}
			}
			return new ItemType[]
			{
				itemType
			};
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x00061BA8 File Offset: 0x0005FDA8
		private bool CanEditItem(RightsManagedMessageItem irmItem)
		{
			return irmItem.UsageRights.IsUsageRightGranted(ContentRight.Edit);
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x00061BB8 File Offset: 0x0005FDB8
		private void SubscribeToSharingItem(IdAndSession idAndSession)
		{
			using (SharingMessageItem sharingMessageItem = SharingMessageItem.Bind(idAndSession.Session, idAndSession.Id))
			{
				SubscribeResults arg = sharingMessageItem.Subscribe();
				ExTraceGlobals.CreateItemCallTracer.TraceDebug<SubscribeResults>((long)this.GetHashCode(), "Subscribe results: {0}", arg);
			}
		}

		// Token: 0x04000D30 RID: 3376
		internal const int BufferSize = 32768;

		// Token: 0x04000D31 RID: 3377
		private static readonly string CreateItemActionName = typeof(CreateItem).Name;

		// Token: 0x04000D32 RID: 3378
		private static readonly PropertyDefinition[] forwardReplyPropertyDefinitionArray = new PropertyDefinition[]
		{
			ItemSchema.SentTime,
			ItemSchema.IsClassified,
			ItemSchema.Classification,
			ItemSchema.ClassificationGuid,
			ItemSchema.ClassificationDescription,
			ItemSchema.ClassificationKeep
		};

		// Token: 0x04000D33 RID: 3379
		private readonly DisposeTracker disposeTracker;

		// Token: 0x04000D34 RID: 3380
		private static LazyMember<Dictionary<Type, Func<CreateItem, ItemType, ItemType[]>>> responseActionMap = new LazyMember<Dictionary<Type, Func<CreateItem, ItemType, ItemType[]>>>(delegate()
		{
			Dictionary<Type, Func<CreateItem, ItemType, ItemType[]>> dictionary = new Dictionary<Type, Func<CreateItem, ItemType, ItemType[]>>();
			dictionary.Add(typeof(ForwardItemType), (CreateItem cmd, ItemType item) => cmd.ExecuteForwardReply(item));
			dictionary.Add(typeof(ReplyToItemType), (CreateItem cmd, ItemType item) => cmd.ExecuteForwardReply(item));
			dictionary.Add(typeof(ReplyAllToItemType), (CreateItem cmd, ItemType item) => cmd.ExecuteForwardReply(item));
			dictionary.Add(typeof(AcceptItemType), (CreateItem cmd, ItemType item) => cmd.PerformAcceptDeclineTentativeItem(item as MeetingRegistrationResponseObjectType));
			dictionary.Add(typeof(DeclineItemType), (CreateItem cmd, ItemType item) => cmd.PerformAcceptDeclineTentativeItem(item as MeetingRegistrationResponseObjectType));
			dictionary.Add(typeof(TentativelyAcceptItemType), (CreateItem cmd, ItemType item) => cmd.PerformAcceptDeclineTentativeItem(item as MeetingRegistrationResponseObjectType));
			dictionary.Add(typeof(CancelCalendarItemType), (CreateItem cmd, ItemType item) => cmd.PerformCancelCalendarItem(item));
			dictionary.Add(typeof(RemoveItemType), (CreateItem cmd, ItemType item) => cmd.PerformRemoveItem(item));
			dictionary.Add(typeof(SuppressReadReceiptType), (CreateItem cmd, ItemType item) => cmd.PerformSuppressReadReceipt(item));
			dictionary.Add(typeof(PostReplyItemType), (CreateItem cmd, ItemType item) => cmd.PostReplyItem(item));
			dictionary.Add(typeof(AcceptSharingInvitationType), (CreateItem cmd, ItemType item) => cmd.PerformAcceptSharingInvitation(item));
			dictionary.Add(typeof(ApproveRequestItemType), (CreateItem cmd, ItemType item) => cmd.CreateApprovalRequestResponseItem(item));
			dictionary.Add(typeof(RejectRequestItemType), (CreateItem cmd, ItemType item) => cmd.CreateApprovalRequestResponseItem(item));
			dictionary.Add(typeof(VotingResponseItemType), (CreateItem cmd, ItemType item) => cmd.CreateVotingResponseItem(item));
			dictionary.Add(typeof(AddItemToMyCalendarType), (CreateItem cmd, ItemType item) => cmd.InvalidateAddItemToMyCalendar(item));
			dictionary.Add(typeof(ProposeNewTimeType), (CreateItem cmd, ItemType item) => cmd.InvalidateProposeNewTime(item));
			return dictionary;
		});

		// Token: 0x04000D35 RID: 3381
		private TargetFolderId saveToFolderId;

		// Token: 0x04000D36 RID: 3382
		private ItemType[] items;

		// Token: 0x04000D37 RID: 3383
		private ItemResponseShape responseShape;

		// Token: 0x04000D38 RID: 3384
		private DelegateSessionHandleWrapper dsHandle;

		// Token: 0x04000D39 RID: 3385
		private CalendarItemOperationType.CreateOrDelete? sendMeetingInvitations = null;

		// Token: 0x04000D3A RID: 3386
		private int totalNbRecipients;

		// Token: 0x04000D3B RID: 3387
		private int totalNbMessages;

		// Token: 0x04000D3C RID: 3388
		private int totalBodySize;

		// Token: 0x04000D3D RID: 3389
		private int totalMeetings;

		// Token: 0x04000D3E RID: 3390
		private static readonly PropertyDefinition[] calendarProcessedPropertyDefinition = new PropertyDefinition[]
		{
			MeetingMessageSchema.CalendarProcessed
		};

		// Token: 0x04000D3F RID: 3391
		private static LazyMember<Dictionary<Type, ResponseType>> responseTypeMap = new LazyMember<Dictionary<Type, ResponseType>>(() => new Dictionary<Type, ResponseType>
		{
			{
				typeof(AcceptItemType),
				ResponseType.Accept
			},
			{
				typeof(DeclineItemType),
				ResponseType.Decline
			},
			{
				typeof(TentativelyAcceptItemType),
				ResponseType.Tentative
			}
		});

		// Token: 0x04000D40 RID: 3392
		private static readonly PropertyDefinition[] approvalPropertyDefinitionArray = new PropertyDefinition[]
		{
			MessageItemSchema.ApprovalDecisionTime,
			MessageItemSchema.ApprovalDecision,
			MessageItemSchema.ApprovalDecisionMaker
		};

		// Token: 0x020002C1 RID: 705
		private class ResponseObjectInformation
		{
			// Token: 0x0600138D RID: 5005 RVA: 0x000620C8 File Offset: 0x000602C8
			public ResponseObjectInformation(ResponseObjectType responseObject, IdConverter idConverter, bool isReadWriteReferenceIdAndSession)
			{
				this.updateItem = responseObject;
				this.referenceItemId = this.updateItem.ReferenceItemId;
				if (this.referenceItemId is AttachmentIdType)
				{
					this.referenceIdAndSession = idConverter.ConvertAttachmentIdToIdAndSessionReadOnly((AttachmentIdType)this.referenceItemId);
				}
				else if (isReadWriteReferenceIdAndSession)
				{
					this.referenceIdAndSession = idConverter.ConvertItemIdToIdAndSessionReadWrite(this.referenceItemId);
				}
				else
				{
					this.referenceIdAndSession = idConverter.ConvertItemIdToIdAndSessionReadOnly(this.referenceItemId);
				}
				this.updateItem.ReferenceItemId = null;
			}

			// Token: 0x0600138E RID: 5006 RVA: 0x0006214E File Offset: 0x0006034E
			public ResponseObjectInformation(ResponseObjectType responseObject, IdConverter idConverter) : this(responseObject, idConverter, false)
			{
			}

			// Token: 0x1700025D RID: 605
			// (get) Token: 0x0600138F RID: 5007 RVA: 0x00062159 File Offset: 0x00060359
			public ItemType UpdateItem
			{
				get
				{
					return this.updateItem;
				}
			}

			// Token: 0x1700025E RID: 606
			// (get) Token: 0x06001390 RID: 5008 RVA: 0x00062161 File Offset: 0x00060361
			public IdAndSession ReferenceIdAndSession
			{
				get
				{
					return this.referenceIdAndSession;
				}
			}

			// Token: 0x1700025F RID: 607
			// (get) Token: 0x06001391 RID: 5009 RVA: 0x00062169 File Offset: 0x00060369
			public BaseItemId ReferenceItemId
			{
				get
				{
					return this.referenceItemId;
				}
			}

			// Token: 0x06001392 RID: 5010 RVA: 0x00062171 File Offset: 0x00060371
			public void ReplaceReferenceId(IdAndSession idAndSession)
			{
				this.referenceIdAndSession = idAndSession;
				this.referenceItemId = IdConverter.ConvertStoreItemIdToItemId(idAndSession.Id, idAndSession.Session);
			}

			// Token: 0x04000D53 RID: 3411
			private IdAndSession referenceIdAndSession;

			// Token: 0x04000D54 RID: 3412
			protected ResponseObjectType updateItem;

			// Token: 0x04000D55 RID: 3413
			private BaseItemId referenceItemId;
		}

		// Token: 0x020002C2 RID: 706
		private class ForwardReplyInformation : CreateItem.ResponseObjectInformation
		{
			// Token: 0x06001393 RID: 5011 RVA: 0x00062191 File Offset: 0x00060391
			public ForwardReplyInformation(SmartResponseType responseObject, IdConverter idConverter, bool isAttachment) : this(responseObject, idConverter)
			{
				this.isAttachment = isAttachment;
				this.updateResponseItemId = responseObject.UpdateResponseItemId;
				this.referenceItemDocumentId = responseObject.ReferenceItemDocumentId;
			}

			// Token: 0x06001394 RID: 5012 RVA: 0x000621BA File Offset: 0x000603BA
			public ForwardReplyInformation(SmartResponseType responseObject, IdConverter idConverter) : base(responseObject, idConverter, true)
			{
				this.ProcessBodyPrefix(responseObject.NewBodyContent);
			}

			// Token: 0x06001395 RID: 5013 RVA: 0x000621D1 File Offset: 0x000603D1
			public ForwardReplyInformation(PostReplyItemType responseObject, IdConverter idConverter) : base(responseObject, idConverter, true)
			{
				this.ProcessBodyPrefix(responseObject.NewBodyContent);
			}

			// Token: 0x06001396 RID: 5014 RVA: 0x000621E8 File Offset: 0x000603E8
			private void ProcessBodyPrefix(BodyContentType bodyPrefix)
			{
				if (bodyPrefix != null)
				{
					this.wasBodyPrefixSpecified = true;
					switch (bodyPrefix.BodyType)
					{
					case BodyType.Text:
						this.bodyPrefixType = Microsoft.Exchange.Data.Storage.BodyFormat.TextPlain;
						break;
					case BodyType.HTML:
						this.bodyPrefixType = Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml;
						break;
					}
					this.bodyPrefix = bodyPrefix.Value;
					this.updateItem.Body = null;
					return;
				}
				this.wasBodyPrefixSpecified = false;
				this.bodyPrefix = string.Empty;
				this.bodyPrefixType = Microsoft.Exchange.Data.Storage.BodyFormat.TextHtml;
			}

			// Token: 0x17000260 RID: 608
			// (get) Token: 0x06001397 RID: 5015 RVA: 0x00062258 File Offset: 0x00060458
			public bool WasBodyPrefixSpecified
			{
				get
				{
					return this.wasBodyPrefixSpecified;
				}
			}

			// Token: 0x17000261 RID: 609
			// (get) Token: 0x06001398 RID: 5016 RVA: 0x00062260 File Offset: 0x00060460
			public string BodyPrefix
			{
				get
				{
					return this.bodyPrefix;
				}
			}

			// Token: 0x17000262 RID: 610
			// (get) Token: 0x06001399 RID: 5017 RVA: 0x00062268 File Offset: 0x00060468
			public Microsoft.Exchange.Data.Storage.BodyFormat BodyPrefixType
			{
				get
				{
					return this.bodyPrefixType;
				}
			}

			// Token: 0x17000263 RID: 611
			// (get) Token: 0x0600139A RID: 5018 RVA: 0x00062270 File Offset: 0x00060470
			public bool IsAttachment
			{
				get
				{
					return this.isAttachment;
				}
			}

			// Token: 0x17000264 RID: 612
			// (get) Token: 0x0600139B RID: 5019 RVA: 0x00062278 File Offset: 0x00060478
			public ItemId UpdateResponseItemId
			{
				get
				{
					return this.updateResponseItemId;
				}
			}

			// Token: 0x17000265 RID: 613
			// (get) Token: 0x0600139C RID: 5020 RVA: 0x00062280 File Offset: 0x00060480
			public int ReferenceItemDocumentId
			{
				get
				{
					return this.referenceItemDocumentId;
				}
			}

			// Token: 0x04000D56 RID: 3414
			private bool wasBodyPrefixSpecified;

			// Token: 0x04000D57 RID: 3415
			private string bodyPrefix;

			// Token: 0x04000D58 RID: 3416
			private Microsoft.Exchange.Data.Storage.BodyFormat bodyPrefixType;

			// Token: 0x04000D59 RID: 3417
			private readonly bool isAttachment;

			// Token: 0x04000D5A RID: 3418
			private ItemId updateResponseItemId;

			// Token: 0x04000D5B RID: 3419
			private readonly int referenceItemDocumentId;
		}

		// Token: 0x020002C3 RID: 707
		private class PostReplyInformation : CreateItem.ForwardReplyInformation
		{
			// Token: 0x0600139D RID: 5021 RVA: 0x00062288 File Offset: 0x00060488
			public PostReplyInformation(PostReplyItemType postReplyItem, IdConverter idConverter) : base(postReplyItem, idConverter)
			{
			}
		}
	}
}
