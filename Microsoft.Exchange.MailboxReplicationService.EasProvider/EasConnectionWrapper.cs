using System;
using System.Collections.Generic;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Eas;
using Microsoft.Exchange.Connections.Eas.Commands;
using Microsoft.Exchange.Connections.Eas.Commands.Connect;
using Microsoft.Exchange.Connections.Eas.Commands.Disconnect;
using Microsoft.Exchange.Connections.Eas.Commands.FolderCreate;
using Microsoft.Exchange.Connections.Eas.Commands.FolderDelete;
using Microsoft.Exchange.Connections.Eas.Commands.FolderSync;
using Microsoft.Exchange.Connections.Eas.Commands.FolderUpdate;
using Microsoft.Exchange.Connections.Eas.Commands.GetItemEstimate;
using Microsoft.Exchange.Connections.Eas.Commands.ItemOperations;
using Microsoft.Exchange.Connections.Eas.Commands.MoveItems;
using Microsoft.Exchange.Connections.Eas.Commands.SendMail;
using Microsoft.Exchange.Connections.Eas.Commands.Sync;
using Microsoft.Exchange.Connections.Eas.Model.Extensions;
using Microsoft.Exchange.Connections.Eas.Model.Response.ItemOperations;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000002 RID: 2
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class EasConnectionWrapper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal EasConnectionWrapper(IEasConnection easConnection)
		{
			ArgumentValidator.ThrowIfNull("easConnection", easConnection);
			this.wrappedObject = easConnection;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x000020EA File Offset: 0x000002EA
		internal string ServerName
		{
			get
			{
				return this.wrappedObject.ServerName;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020F7 File Offset: 0x000002F7
		internal UserSmtpAddress UserSmtpAddress
		{
			get
			{
				return this.wrappedObject.UserSmtpAddress;
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002104 File Offset: 0x00000304
		internal void Connect()
		{
			ConnectResponse connectResponse = this.wrappedObject.Connect(ConnectRequest.Default, null);
			if (connectResponse.ConnectStatus != ConnectStatus.Success)
			{
				throw new EasConnectFailedException(connectResponse.ConnectStatusString, connectResponse.HttpStatusString, connectResponse.UserSmtpAddressString);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002144 File Offset: 0x00000344
		internal void Disconnect()
		{
			this.wrappedObject.Disconnect(DisconnectRequest.Default);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002158 File Offset: 0x00000358
		internal FolderSyncResponse FolderSync()
		{
			FolderSyncRequest initialSyncRequest = FolderSyncRequest.InitialSyncRequest;
			return this.FolderSync(initialSyncRequest);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002174 File Offset: 0x00000374
		internal FolderSyncResponse FolderSync(string syncKey)
		{
			FolderSyncRequest folderSyncRequest = new FolderSyncRequest
			{
				SyncKey = syncKey
			};
			return this.FolderSync(folderSyncRequest);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002198 File Offset: 0x00000398
		internal int GetCountOfItemsToSync(string folderId, EasSyncOptions options)
		{
			GetItemEstimateRequest getItemEstimateRequest = EasRequestGenerator.CreateEstimateRequest(options.SyncKey, folderId, options.RecentOnly);
			GetItemEstimateResponse itemEstimate = this.GetItemEstimate(getItemEstimateRequest);
			if (itemEstimate.Estimate == null)
			{
				return 0;
			}
			return itemEstimate.Estimate.Value;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021E4 File Offset: 0x000003E4
		internal ItemOperationsResponse LookupItems(IReadOnlyCollection<string> messageIds, string folderId)
		{
			ItemOperationsRequest itemOperationsRequest = EasRequestGenerator.CreateItemOpsRequestForSelectedMessages(messageIds, folderId);
			return this.ItemOperations(itemOperationsRequest);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002238 File Offset: 0x00000438
		internal Properties FetchMessageItem(string messageId, string folderId)
		{
			ItemOperationsRequest itemOperationsRequest = EasRequestGenerator.CreateItemOpsRequest(messageId, folderId);
			ItemOperationsResponse response = this.ItemOperations(itemOperationsRequest);
			ItemOperationsStatus status;
			Properties messageProperties = response.GetMessageProperties(0, out status);
			EasConnectionWrapper.WrapException(delegate()
			{
				response.ThrowIfStatusIsFailed(status);
			}, (ConnectionsTransientException e) => new EasFetchFailedTransientException(e.Message, e), (ConnectionsPermanentException e) => new EasFetchFailedPermanentException(e.Message, e));
			return messageProperties;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022C0 File Offset: 0x000004C0
		internal Properties FetchCalendarItem(string itemId, string folderId)
		{
			ItemOperationsRequest itemOperationsRequest = EasRequestGenerator.CreateItemOpsRequestForCalendarItem(itemId, folderId);
			ItemOperationsResponse itemOperationsResponse = this.ItemOperations(itemOperationsRequest);
			return itemOperationsResponse.GetCalendarItemProperties();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000022E4 File Offset: 0x000004E4
		internal string MoveItem(string messageId, string sourceFolderId, string destinationFolderId)
		{
			MoveItemsRequest moveItemsRequest = EasRequestGenerator.CreateMoveRequestForMessages(new string[]
			{
				messageId
			}, sourceFolderId, destinationFolderId);
			MoveItemsResponse moveItemsResponse = this.MoveItems(moveItemsRequest);
			return moveItemsResponse.Responses[0].DstMsgId;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000231C File Offset: 0x0000051C
		internal SendMailResponse SendMail(string clientId, string mimeString)
		{
			SendMailRequest sendMailRequest = new SendMailRequest
			{
				ClientId = clientId,
				SaveInSentItems = true,
				Mime = mimeString
			};
			return this.SendMail(sendMailRequest);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002350 File Offset: 0x00000550
		internal SyncResponse Sync(string folderId, EasSyncOptions options, bool recentOnly)
		{
			SyncRequest syncRequest = EasRequestGenerator.CreateSyncRequestForAllMessages(options.SyncKey, folderId, options.MaxNumberOfMessage, recentOnly);
			return this.Sync(syncRequest);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000237C File Offset: 0x0000057C
		internal void SyncRead(string messageId, string syncKey, string folderId, bool isRead)
		{
			SyncRequest syncRequest = EasRequestGenerator.CreateSyncRequestForReadUnreadMessages(new string[]
			{
				messageId
			}, syncKey, folderId, isRead);
			this.SyncUpdate(messageId, syncRequest);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023A8 File Offset: 0x000005A8
		internal void SyncFlag(string messageId, string syncKey, string folderId, FlagStatus flagStatus)
		{
			SyncRequest syncRequest = EasRequestGenerator.CreateSyncRequestForFlagMessages(new string[]
			{
				messageId
			}, syncKey, folderId, flagStatus);
			this.SyncUpdate(messageId, syncRequest);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023D4 File Offset: 0x000005D4
		internal void DeleteItem(string messageId, string syncKey, string folderId)
		{
			SyncRequest syncRequest = EasRequestGenerator.CreateSyncRequestForDeleteMessages(new string[]
			{
				messageId
			}, syncKey, folderId);
			this.Sync(syncRequest);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002400 File Offset: 0x00000600
		internal void UpdateCalendarEvent(string calendarEventId, string syncKey, string folderId, Event theEvent, IList<Event> exceptionalEvents, IList<string> deletedOccurrences, UserSmtpAddress userSmtpAddress)
		{
			SyncRequest syncRequest = EasRequestGenerator.CreateSyncRequestForUpdateCalendarEvent(syncKey, calendarEventId, folderId, theEvent, exceptionalEvents, deletedOccurrences, userSmtpAddress);
			this.SyncUpdate(calendarEventId, syncRequest);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002428 File Offset: 0x00000628
		internal byte[] CreateCalendarEvent(string clientId, string syncKey, out string newSyncKey, string folderId, Event theEvent, IList<Event> exceptionalEvents, IList<string> deletedOccurrences, UserSmtpAddress userSmtpAddress)
		{
			SyncRequest syncRequest = EasRequestGenerator.CreateSyncRequestForCreateCalendarEvent(syncKey, clientId, folderId, theEvent, exceptionalEvents, deletedOccurrences, userSmtpAddress);
			SyncResponse syncResponse = this.SyncCreation(clientId, syncRequest);
			newSyncKey = syncResponse.Collections[0].SyncKey;
			return EasMailbox.GetEntryId(syncResponse.AddResponses[0].ServerId);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000247C File Offset: 0x0000067C
		internal FolderCreateResponse CreateFolder(string syncKey, string displayName, string parentId, EasFolderType folderType)
		{
			FolderCreateRequest folderCreateRequest = new FolderCreateRequest
			{
				SyncKey = syncKey,
				DisplayName = displayName,
				ParentId = parentId,
				Type = (int)folderType
			};
			return this.FolderCreate(folderCreateRequest);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000024B8 File Offset: 0x000006B8
		internal FolderDeleteResponse DeleteFolder(string syncKey, string folderId)
		{
			FolderDeleteRequest folderDeleteRequest = new FolderDeleteRequest
			{
				SyncKey = syncKey,
				ServerId = folderId
			};
			return this.FolderDelete(folderDeleteRequest);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024E4 File Offset: 0x000006E4
		internal FolderUpdateResponse MoveOrRenameFolder(string syncKey, string folderId, string destParentId, string displayName)
		{
			FolderUpdateRequest folderUpdateRequest = new FolderUpdateRequest
			{
				SyncKey = syncKey,
				ServerId = folderId,
				ParentId = destParentId,
				DisplayName = displayName
			};
			return this.FolderUpdate(folderUpdateRequest);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002538 File Offset: 0x00000738
		private static T WrapException<T>(Func<T> function, Func<ConnectionsTransientException, MailboxReplicationTransientException> wrapTransientException, Func<ConnectionsPermanentException, MailboxReplicationPermanentException> wrapPermanentException) where T : class
		{
			T result = default(T);
			EasConnectionWrapper.WrapException(delegate()
			{
				result = function();
			}, wrapTransientException, wrapPermanentException);
			return result;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002578 File Offset: 0x00000778
		private static void WrapException(Action action, Func<ConnectionsTransientException, MailboxReplicationTransientException> wrapTransientException, Func<ConnectionsPermanentException, MailboxReplicationPermanentException> wrapPermanentException)
		{
			try
			{
				action();
			}
			catch (EasRequiresSyncKeyResetException)
			{
				throw;
			}
			catch (EasRetryAfterException ex)
			{
				throw new RelinquishJobServerBusyTransientException(ex.LocalizedString, ex.Delay, ex);
			}
			catch (ConnectionsTransientException arg)
			{
				throw wrapTransientException(arg);
			}
			catch (ConnectionsPermanentException arg2)
			{
				throw wrapPermanentException(arg2);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002624 File Offset: 0x00000824
		private void SyncUpdate(string messageId, SyncRequest syncRequest)
		{
			SyncResponse response = this.Sync(syncRequest);
			SyncStatus status = response.GetChangeResponseStatus(0);
			if (status == SyncStatus.SyncItemNotFound)
			{
				MrsTracer.Provider.Warning("Source message {0} doesn't exist", new object[]
				{
					messageId
				});
				throw new EasObjectNotFoundException(messageId);
			}
			EasConnectionWrapper.WrapException(delegate()
			{
				response.ThrowIfStatusIsFailed(status);
			}, (ConnectionsTransientException e) => new EasSyncFailedTransientException(e.Message, e), (ConnectionsPermanentException e) => new EasSyncFailedPermanentException(e.Message, e));
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002710 File Offset: 0x00000910
		private SyncResponse SyncCreation(string itemId, SyncRequest syncRequest)
		{
			SyncResponse response = this.Sync(syncRequest);
			SyncStatus status = response.GetAddResponseStatus(0);
			EasConnectionWrapper.WrapException(delegate()
			{
				response.ThrowIfStatusIsFailed(status);
			}, (ConnectionsTransientException e) => new EasSyncFailedTransientException(e.Message, e), (ConnectionsPermanentException e) => new EasSyncFailedPermanentException(e.Message, e));
			return response;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000027D0 File Offset: 0x000009D0
		private FolderCreateResponse FolderCreate(FolderCreateRequest folderCreateRequest)
		{
			return EasConnectionWrapper.WrapException<FolderCreateResponse>(() => this.wrappedObject.FolderCreate(folderCreateRequest), (ConnectionsTransientException e) => new EasFolderCreateFailedTransientException(e.Message, e), (ConnectionsPermanentException e) => new EasFolderCreateFailedPermanentException(e.Message, e));
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002878 File Offset: 0x00000A78
		private FolderDeleteResponse FolderDelete(FolderDeleteRequest folderDeleteRequest)
		{
			return EasConnectionWrapper.WrapException<FolderDeleteResponse>(() => this.wrappedObject.FolderDelete(folderDeleteRequest), (ConnectionsTransientException e) => new EasFolderDeleteFailedTransientException(e.Message, e), (ConnectionsPermanentException e) => new EasFolderDeleteFailedPermanentException(e.Message, e));
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002920 File Offset: 0x00000B20
		private FolderSyncResponse FolderSync(FolderSyncRequest folderSyncRequest)
		{
			return EasConnectionWrapper.WrapException<FolderSyncResponse>(() => this.wrappedObject.FolderSync(folderSyncRequest), (ConnectionsTransientException e) => new EasFolderSyncFailedTransientException(e.Message, e), (ConnectionsPermanentException e) => new EasFolderSyncFailedPermanentException(e.Message, e));
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000029C8 File Offset: 0x00000BC8
		private FolderUpdateResponse FolderUpdate(FolderUpdateRequest folderUpdateRequest)
		{
			return EasConnectionWrapper.WrapException<FolderUpdateResponse>(() => this.wrappedObject.FolderUpdate(folderUpdateRequest), (ConnectionsTransientException e) => new EasFolderUpdateFailedTransientException(e.Message, e), (ConnectionsPermanentException e) => new EasFolderUpdateFailedPermanentException(e.Message, e));
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002A70 File Offset: 0x00000C70
		private GetItemEstimateResponse GetItemEstimate(GetItemEstimateRequest getItemEstimateRequest)
		{
			return EasConnectionWrapper.WrapException<GetItemEstimateResponse>(() => this.wrappedObject.GetItemEstimate(getItemEstimateRequest), (ConnectionsTransientException e) => new EasCountFailedTransientException(e.Message, e), (ConnectionsPermanentException e) => new EasCountFailedPermanentException(e.Message, e));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002B18 File Offset: 0x00000D18
		private ItemOperationsResponse ItemOperations(ItemOperationsRequest itemOperationsRequest)
		{
			return EasConnectionWrapper.WrapException<ItemOperationsResponse>(() => this.wrappedObject.ItemOperations(itemOperationsRequest), (ConnectionsTransientException e) => new EasFetchFailedTransientException(e.Message, e), (ConnectionsPermanentException e) => new EasFetchFailedPermanentException(e.Message, e));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002BC0 File Offset: 0x00000DC0
		private MoveItemsResponse MoveItems(MoveItemsRequest moveItemsRequest)
		{
			return EasConnectionWrapper.WrapException<MoveItemsResponse>(() => this.wrappedObject.MoveItems(moveItemsRequest), (ConnectionsTransientException e) => new EasMoveFailedTransientException(e.Message, e), (ConnectionsPermanentException e) => new EasMoveFailedPermanentException(e.Message, e));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002C68 File Offset: 0x00000E68
		private SendMailResponse SendMail(SendMailRequest sendMailRequest)
		{
			return EasConnectionWrapper.WrapException<SendMailResponse>(() => this.wrappedObject.SendMail(sendMailRequest), (ConnectionsTransientException e) => new EasSendFailedTransientException(e.Message, e), (ConnectionsPermanentException e) => new EasSendFailedPermanentException(e.Message, e));
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002D10 File Offset: 0x00000F10
		private SyncResponse Sync(SyncRequest syncRequest)
		{
			return EasConnectionWrapper.WrapException<SyncResponse>(() => this.wrappedObject.Sync(syncRequest), (ConnectionsTransientException e) => new EasSyncFailedTransientException(e.Message, e), (ConnectionsPermanentException e) => new EasSyncFailedPermanentException(e.Message, e));
		}

		// Token: 0x04000001 RID: 1
		private readonly IEasConnection wrappedObject;
	}
}
