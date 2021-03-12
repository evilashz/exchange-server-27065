using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200036F RID: 879
	internal sealed class SendItem : MultiStepServiceCommand<SendItemRequest, ServiceResultNone>
	{
		// Token: 0x0600189E RID: 6302 RVA: 0x00086950 File Offset: 0x00084B50
		public SendItem(CallContext callContext, SendItemRequest request) : base(callContext, request)
		{
			this.itemIds = base.Request.Ids;
			this.targetFolderToSaveIn = base.Request.SavedItemFolderId;
			this.saveInFolder = base.Request.SaveItemToFolder;
			ServiceCommandBase.ThrowIfNullOrEmpty<BaseItemId>(this.itemIds, "itemIds", "SendItem::ctor");
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600189F RID: 6303 RVA: 0x000869AD File Offset: 0x00084BAD
		// (remove) Token: 0x060018A0 RID: 6304 RVA: 0x000869C6 File Offset: 0x00084BC6
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

		// Token: 0x060018A1 RID: 6305 RVA: 0x000869DF File Offset: 0x00084BDF
		internal override void PreExecuteCommand()
		{
			this.saveFolderIdAndSession = this.GetSaveToFolderIdFromRequest();
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x000869F0 File Offset: 0x00084BF0
		internal override IExchangeWebMethodResponse GetResponse()
		{
			SendItemResponse sendItemResponse = new SendItemResponse();
			sendItemResponse.BuildForNoReturnValue(base.Results);
			return sendItemResponse;
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x00086A10 File Offset: 0x00084C10
		internal override int StepCount
		{
			get
			{
				return this.itemIds.Length;
			}
		}

		// Token: 0x060018A4 RID: 6308 RVA: 0x00086A1A File Offset: 0x00084C1A
		private void OnBeforeMessageDisposition(MessageItem messageItem)
		{
			if (this.beforeMessageDispositionEventHandler != null)
			{
				this.beforeMessageDispositionEventHandler(messageItem);
			}
		}

		// Token: 0x060018A5 RID: 6309 RVA: 0x00086A30 File Offset: 0x00084C30
		private IdAndSession GetSaveToFolderIdFromRequest()
		{
			IdAndSession idAndSession = null;
			if (this.targetFolderToSaveIn != null)
			{
				if (!this.saveInFolder)
				{
					throw new InvalidSendItemSaveSettingsException();
				}
				try
				{
					idAndSession = base.IdConverter.ConvertTargetFolderIdToIdAndContentSession(this.targetFolderToSaveIn.BaseFolderId, true);
				}
				catch (ObjectNotFoundException innerException)
				{
					throw new SavedItemFolderNotFoundException(innerException);
				}
				if (idAndSession.Session is PublicFolderSession)
				{
					throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorCannotSaveSentItemInPublicFolder);
				}
				if (idAndSession.Session is MailboxSession && ((MailboxSession)idAndSession.Session).MailboxOwner.MailboxInfo.IsArchive)
				{
					throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorCannotSaveSentItemInArchiveFolder);
				}
			}
			return idAndSession;
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00086AE4 File Offset: 0x00084CE4
		internal override ServiceResult<ServiceResultNone> Execute()
		{
			IdAndSession idAndSession = base.IdConverter.ConvertItemIdToIdAndSessionReadWrite(this.itemIds[base.CurrentStep]);
			if (idAndSession.Session is PublicFolderSession)
			{
				throw new ServiceInvalidOperationException(CoreResources.IDs.ErrorCannotSendMessageFromPublicFolder);
			}
			IdAndSession sentItemsFolderId = this.GetSentItemsFolderId(idAndSession);
			return this.SendItemById(idAndSession, sentItemsFolderId);
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x00086B38 File Offset: 0x00084D38
		private IdAndSession GetSentItemsFolderId(IdAndSession itemIdToSend)
		{
			IdAndSession result = null;
			if (this.saveInFolder)
			{
				if (this.saveFolderIdAndSession != null)
				{
					result = this.saveFolderIdAndSession;
				}
				else
				{
					try
					{
						result = base.IdConverter.ConvertDefaultFolderType(DefaultFolderType.SentItems, ((MailboxSession)itemIdToSend.Session).MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString());
					}
					catch (ObjectNotFoundException innerException)
					{
						throw new SavedItemFolderNotFoundException(innerException);
					}
				}
			}
			return result;
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x00086BB4 File Offset: 0x00084DB4
		private ServiceResult<ServiceResultNone> SendItemById(IdAndSession sendIdAndSession, IdAndSession folderIdAndSessionToSaveIn)
		{
			using (Item xsoItemForUpdate = ServiceCommandBase.GetXsoItemForUpdate(sendIdAndSession, new PropertyDefinition[]
			{
				MessageItemSchema.Flags
			}))
			{
				MessageItem messageItem = xsoItemForUpdate as MessageItem;
				this.ValidateMessageToSend(messageItem);
				this.OnBeforeMessageDisposition(messageItem);
				try
				{
					ServiceCommandBase.RequireUpToDateItem(sendIdAndSession.Id, xsoItemForUpdate);
					if (folderIdAndSessionToSaveIn == null)
					{
						messageItem.SendWithoutSavingMessage();
					}
					else if (folderIdAndSessionToSaveIn.Id.Equals(messageItem.ParentId))
					{
						messageItem.SendWithoutMovingMessage(folderIdAndSessionToSaveIn.GetAsStoreObjectId(), SaveMode.ResolveConflicts);
					}
					else
					{
						messageItem.Send(folderIdAndSessionToSaveIn.GetAsStoreObjectId(), SaveMode.ResolveConflicts);
					}
				}
				catch (InvalidRecipientsException ex)
				{
					if (messageItem.Recipients.Count == 0)
					{
						throw new MissingRecipientsException(ex.InnerException);
					}
					throw;
				}
			}
			this.objectsChanged++;
			return new ServiceResult<ServiceResultNone>(new ServiceResultNone());
		}

		// Token: 0x060018A9 RID: 6313 RVA: 0x00086C94 File Offset: 0x00084E94
		private void ValidateMessageToSend(MessageItem messageToSend)
		{
			if (messageToSend == null || (!(messageToSend is MeetingResponse) && Shape.IsGenericMessageOnly(messageToSend) && !ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2007SP1)))
			{
				throw new InvalidItemForOperationException("SendItem");
			}
			if (ServiceCommandBase.IsAssociated(messageToSend))
			{
				throw new ServiceInvalidOperationException((CoreResources.IDs)3859804741U);
			}
		}

		// Token: 0x04001082 RID: 4226
		private BaseItemId[] itemIds;

		// Token: 0x04001083 RID: 4227
		private TargetFolderId targetFolderToSaveIn;

		// Token: 0x04001084 RID: 4228
		private bool saveInFolder;

		// Token: 0x04001085 RID: 4229
		private IdAndSession saveFolderIdAndSession;

		// Token: 0x04001086 RID: 4230
		private Action<MessageItem> beforeMessageDispositionEventHandler;
	}
}
