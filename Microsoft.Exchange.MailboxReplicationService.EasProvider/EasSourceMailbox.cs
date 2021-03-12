using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Eas;
using Microsoft.Exchange.Connections.Eas.Commands;
using Microsoft.Exchange.Connections.Eas.Commands.FolderSync;
using Microsoft.Exchange.Connections.Eas.Model.Extensions;
using Microsoft.Exchange.Connections.Eas.Model.Response.FolderHierarchy;
using Microsoft.Exchange.Connections.Eas.Model.Response.ItemOperations;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.Calendaring;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class EasSourceMailbox : EasMailbox, ISourceMailbox, IMailbox, IDisposable, ISupportMime, IReplayProvider
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00005580 File Offset: 0x00003780
		public EasSourceMailbox()
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005588 File Offset: 0x00003788
		internal EasSourceMailbox(EasConnectionParameters connectionParameters, EasAuthenticationParameters authenticationParameters, EasDeviceParameters deviceParameters) : base(connectionParameters, authenticationParameters, deviceParameters)
		{
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00005593 File Offset: 0x00003793
		internal override bool SupportsSavingSyncState
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005598 File Offset: 0x00003798
		Stream ISupportMime.GetMimeStream(MessageRec message, out PropValueData[] extraPropValues)
		{
			extraPropValues = null;
			Properties properties = this.FetchMessageItem(message);
			if (properties == null || properties.Body == null || string.IsNullOrEmpty(properties.Body.Data))
			{
				throw new UnableToFetchMimeStreamException(EasMailbox.GetStringId(message.EntryId));
			}
			if (properties.Flag != null)
			{
				extraPropValues = new PropValueData[]
				{
					new PropValueData((PropTag)277872643U, properties.Flag.Status)
				};
			}
			string data = properties.Body.Data;
			MemoryStream memoryStream = new MemoryStream(data.Length);
			Stream result;
			using (DisposeGuard disposeGuard = memoryStream.Guard())
			{
				using (StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.UTF8, 1024, true))
				{
					streamWriter.Write(data);
				}
				memoryStream.Seek(0L, SeekOrigin.Begin);
				disposeGuard.Success();
				result = memoryStream;
			}
			return result;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000569C File Offset: 0x0000389C
		byte[] ISourceMailbox.GetMailboxBasicInfo(MailboxSignatureFlags flags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000056A4 File Offset: 0x000038A4
		ISourceFolder ISourceMailbox.GetFolder(byte[] entryId)
		{
			MrsTracer.Provider.Function("EasSourceMailbox.GetFolder({0})", new object[]
			{
				TraceUtils.DumpEntryId(entryId)
			});
			Add add;
			EasFolderBase easFolderBase;
			if (base.EasFolderCache.TryGetValue(entryId, out add))
			{
				easFolderBase = new EasSourceFolder(add, base.EasConnectionWrapper.UserSmtpAddress);
			}
			else if (EasMailbox.GetStringId(entryId) == EasSyntheticFolder.RootFolder.ServerId)
			{
				easFolderBase = EasSyntheticFolder.RootFolder;
			}
			else
			{
				if (!(EasMailbox.GetStringId(entryId) == EasSyntheticFolder.IpmSubtreeFolder.ServerId))
				{
					MrsTracer.Provider.Debug("Folder with folderId '{0}' does not exist.", new object[]
					{
						entryId
					});
					return null;
				}
				easFolderBase = EasSyntheticFolder.IpmSubtreeFolder;
			}
			return (ISourceFolder)easFolderBase.Configure(this);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000575C File Offset: 0x0000395C
		void ISourceMailbox.CopyTo(IFxProxy destMailboxProxy, PropTag[] excludeTags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00005763 File Offset: 0x00003963
		void ISourceMailbox.SetMailboxSyncState(string syncStateString)
		{
			base.SetMailboxSyncState(syncStateString);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000576C File Offset: 0x0000396C
		string ISourceMailbox.GetMailboxSyncState()
		{
			return base.GetMailboxSyncState();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000057E8 File Offset: 0x000039E8
		MailboxChangesManifest ISourceMailbox.EnumerateHierarchyChanges(EnumerateHierarchyChangesFlags flags, int maxChanges)
		{
			MrsTracer.Provider.Function("EasSourceMailbox.EnumerateHierarchyChanges(flags:{0}, maxChanges:{1})", new object[]
			{
				flags,
				maxChanges
			});
			bool catchup = flags.HasFlag(EnumerateHierarchyChangesFlags.Catchup);
			if (catchup)
			{
				if (string.IsNullOrEmpty(this.SyncState.HierarchyData.ProviderSyncState) || this.SyncState.HierarchyData.ManualSyncData != null)
				{
					base.RefreshFolderCache();
				}
				return null;
			}
			return this.EnumerateHierarchyChanges(catchup, delegate(SyncHierarchyManifestState hierState)
			{
				MailboxChangesManifest result;
				try
				{
					result = this.DoManifestSync(flags, maxChanges, hierState, null);
				}
				catch (EasRequiresSyncKeyResetException ex)
				{
					MrsTracer.Provider.Error("Encountered RequiresSyncKeyReset error: {0}", new object[]
					{
						ex
					});
					result = this.RunManualHierarchySync(catchup, hierState);
				}
				return result;
			});
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000058B4 File Offset: 0x00003AB4
		void ISourceMailbox.ExportMessages(List<MessageRec> messages, IFxProxyPool proxyPool, ExportMessagesFlags flags, PropTag[] propsToCopyExplicitly, PropTag[] excludeProps)
		{
			MrsTracer.Provider.Function("EasSourceMailbox.ExportMessages({0} messages)", new object[]
			{
				messages.Count
			});
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			this.CopyMessagesOneByOne(messages, proxyPool, propsToCopyExplicitly, excludeProps, null);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000058FA File Offset: 0x00003AFA
		void ISourceMailbox.ExportFolders(List<byte[]> folderIds, IFxProxyPool proxyPool, ExportFoldersDataToCopyFlags exportFoldersDataToCopyFlags, GetFolderRecFlags folderRecFlags, PropTag[] additionalFolderRecProps, CopyPropertiesFlags copyPropertiesFlags, PropTag[] excludeProps, AclFlags extendedAclFlags)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00005904 File Offset: 0x00003B04
		List<ReplayActionResult> ISourceMailbox.ReplayActions(List<ReplayAction> actions)
		{
			MrsTracer.Provider.Function("EasSourceMailbox.ReplayActions({0} actions)", new object[]
			{
				actions.Count
			});
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			return this.Replay(actions);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005944 File Offset: 0x00003B44
		void IReplayProvider.MarkAsRead(IReadOnlyCollection<MarkAsReadAction> actions)
		{
			MrsTracer.Provider.Function("EasSourceMailbox.MarkAsRead({0} actions)", new object[]
			{
				actions.Count
			});
			foreach (MarkAsReadAction markAsReadAction in actions)
			{
				this.MarkMessageAsReadUnread(markAsReadAction.ItemId, markAsReadAction.FolderId, true);
			}
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000059C0 File Offset: 0x00003BC0
		void IReplayProvider.MarkAsUnRead(IReadOnlyCollection<MarkAsUnReadAction> actions)
		{
			MrsTracer.Provider.Function("EasSourceMailbox.MarkAsUnRead({0} actions)", new object[]
			{
				actions.Count
			});
			foreach (MarkAsUnReadAction markAsUnReadAction in actions)
			{
				this.MarkMessageAsReadUnread(markAsUnReadAction.ItemId, markAsUnReadAction.FolderId, false);
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005A3C File Offset: 0x00003C3C
		IReadOnlyCollection<MoveActionResult> IReplayProvider.Move(IReadOnlyCollection<MoveAction> actions)
		{
			MrsTracer.Provider.Function("EasSourceMailbox.Move({0} actions)", new object[]
			{
				actions.Count
			});
			List<MoveActionResult> list = new List<MoveActionResult>(actions.Count);
			foreach (MoveAction moveAction in actions)
			{
				byte[] itemId = moveAction.ItemId;
				bool moveAsDelete;
				byte[] itemId2 = this.MoveItem(itemId, moveAction.PreviousFolderId, moveAction.FolderId, out moveAsDelete);
				list.Add(new MoveActionResult(itemId2, itemId, moveAsDelete));
			}
			return list;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00005AE4 File Offset: 0x00003CE4
		void IReplayProvider.Send(SendAction action)
		{
			MrsTracer.Provider.Function("EasSourceMailbox.Send({0})", new object[]
			{
				action
			});
			string mimeString = null;
			using (MemoryStream memoryStream = new MemoryStream(action.Data))
			{
				using (StreamReader streamReader = new StreamReader(memoryStream, Encoding.UTF8))
				{
					mimeString = streamReader.ReadToEnd();
				}
			}
			string clientId = EasSourceMailbox.ClientIdFromItemId(action.ItemId);
			base.EasConnectionWrapper.SendMail(clientId, mimeString);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005B80 File Offset: 0x00003D80
		void IReplayProvider.Delete(IReadOnlyCollection<DeleteAction> actions)
		{
			MrsTracer.Provider.Function("EasSourceMailbox.Delete({0} actions)", new object[]
			{
				actions.Count
			});
			foreach (MoveAction moveAction in actions)
			{
				this.DeleteItem(moveAction.ItemId, moveAction.PreviousFolderId);
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005BF8 File Offset: 0x00003DF8
		void IReplayProvider.Flag(IReadOnlyCollection<FlagAction> actions)
		{
			MrsTracer.Provider.Function("EasSourceMailbox.Flag({0} actions)", new object[]
			{
				actions.Count
			});
			foreach (FlagAction flagAction in actions)
			{
				this.FlagMessage(flagAction.ItemId, flagAction.FolderId, FlagStatus.Flagged);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005C74 File Offset: 0x00003E74
		void IReplayProvider.FlagClear(IReadOnlyCollection<FlagClearAction> actions)
		{
			MrsTracer.Provider.Function("EasSourceMailbox.FlagClear({0} actions)", new object[]
			{
				actions.Count
			});
			foreach (FlagClearAction flagClearAction in actions)
			{
				this.FlagMessage(flagClearAction.ItemId, flagClearAction.FolderId, FlagStatus.NotFlagged);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005CF0 File Offset: 0x00003EF0
		void IReplayProvider.FlagComplete(IReadOnlyCollection<FlagCompleteAction> actions)
		{
			MrsTracer.Provider.Function("EasSourceMailbox.FlagComplete({0} actions)", new object[]
			{
				actions.Count
			});
			foreach (FlagCompleteAction flagCompleteAction in actions)
			{
				this.FlagMessage(flagCompleteAction.ItemId, flagCompleteAction.FolderId, FlagStatus.Complete);
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005D6C File Offset: 0x00003F6C
		IReadOnlyCollection<CreateCalendarEventActionResult> IReplayProvider.CreateCalendarEvent(IReadOnlyCollection<CreateCalendarEventAction> actions)
		{
			MrsTracer.Provider.Function("EasSourceMailbox.CreateCalendarEvent({0} actions)", new object[]
			{
				actions.Count
			});
			List<CreateCalendarEventActionResult> list = new List<CreateCalendarEventActionResult>(actions.Count);
			foreach (CreateCalendarEventAction createCalendarEventAction in actions)
			{
				byte[] sourceItemId = this.CreateCalendarEvent(createCalendarEventAction.ItemId, createCalendarEventAction.FolderId, createCalendarEventAction.Event, createCalendarEventAction.ExceptionalEvents, createCalendarEventAction.DeletedOccurrences);
				list.Add(new CreateCalendarEventActionResult(sourceItemId));
			}
			return list;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005E18 File Offset: 0x00004018
		void IReplayProvider.UpdateCalendarEvent(IReadOnlyCollection<UpdateCalendarEventAction> actions)
		{
			MrsTracer.Provider.Function("EasSourceMailbox.UpdateCalendarEvent({0} actions)", new object[]
			{
				actions.Count
			});
			foreach (UpdateCalendarEventAction updateCalendarEventAction in actions)
			{
				this.UpdateCalendarEvent(updateCalendarEventAction.ItemId, updateCalendarEventAction.FolderId, updateCalendarEventAction.Event, updateCalendarEventAction.ExceptionalEvents, updateCalendarEventAction.DeletedOccurrences);
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005F40 File Offset: 0x00004140
		protected override void CopySingleMessage(MessageRec messageRec, IFolderProxy folderProxy, PropTag[] propTagsToExclude, PropTag[] excludeProps)
		{
			ExecutionContext.Create(new DataContext[]
			{
				new OperationDataContext("EasSourceMailbox.CopySingleMessage", OperationType.None),
				new EntryIDsDataContext(messageRec.EntryId)
			}).Execute(delegate
			{
				Add add;
				if (this.EasFolderCache.TryGetValue(messageRec.FolderId, out add))
				{
					EasFolderType easFolderType = add.GetEasFolderType();
					if (EasFolder.IsCalendarFolder(easFolderType))
					{
						Properties calendarItemProperties = this.ReadCalendarItem(messageRec);
						EasSourceMailbox.CopyCalendarItem(messageRec, calendarItemProperties, folderProxy);
						return;
					}
					if (EasFolder.IsContactFolder(easFolderType))
					{
						EasSourceMailbox.CopyContactItem(messageRec, folderProxy);
						return;
					}
					SyncEmailUtils.CopyMimeStream(this, messageRec, folderProxy);
				}
			});
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005FA8 File Offset: 0x000041A8
		protected override MailboxChangesManifest DoManifestSync(EnumerateHierarchyChangesFlags flags, int maxChanges, SyncHierarchyManifestState hierState, MapiStore mapiStore)
		{
			MrsTracer.Provider.Function("EasSourceMailbox.DoManifestSync", new object[0]);
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			flags.HasFlag(EnumerateHierarchyChangesFlags.Catchup);
			EasHierarchySyncState easHierarchySyncState = EasHierarchySyncState.Deserialize(hierState.ProviderSyncState);
			string syncKey = easHierarchySyncState.SyncKey;
			string syncKey2;
			IReadOnlyCollection<Add> readOnlyCollection;
			MailboxChangesManifest folderChangesOnServer = this.GetFolderChangesOnServer(syncKey, out syncKey2, out readOnlyCollection);
			bool flag = false;
			easHierarchySyncState.SyncKey = syncKey2;
			if (folderChangesOnServer.DeletedFolders != null)
			{
				List<Add> list = new List<Add>(folderChangesOnServer.DeletedFolders.Count);
				foreach (Add add in easHierarchySyncState.Folders)
				{
					foreach (byte[] entryId in folderChangesOnServer.DeletedFolders)
					{
						if (StringComparer.Ordinal.Equals(add.ServerId, EasMailbox.GetStringId(entryId)))
						{
							list.Add(add);
							break;
						}
					}
				}
				foreach (Add item in list)
				{
					easHierarchySyncState.Folders.Remove(item);
					flag = true;
				}
			}
			if (readOnlyCollection != null)
			{
				foreach (Add item2 in readOnlyCollection)
				{
					easHierarchySyncState.Folders.Add(item2);
					flag = true;
				}
			}
			hierState.ProviderSyncState = easHierarchySyncState.Serialize(false);
			if (flag)
			{
				base.RefreshFolderCache(easHierarchySyncState);
			}
			return folderChangesOnServer;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006180 File Offset: 0x00004380
		protected override MailboxChangesManifest RunManualHierarchySync(bool catchup, SyncHierarchyManifestState hierState)
		{
			MrsTracer.Provider.Function("EasSourceMailbox.RunManualHierarchySync", new object[0]);
			base.VerifyMailboxConnection(VerifyMailboxConnectionFlags.None);
			EasHierarchySyncState easHierarchySyncState = EasHierarchySyncState.Deserialize(hierState.ProviderSyncState);
			hierState.ProviderSyncState = null;
			EasHierarchySyncState easHierarchySyncState2 = base.RefreshFolderCache();
			EntryIdMap<EasHierarchySyncState.EasFolderData> entryIdMap = new EntryIdMap<EasHierarchySyncState.EasFolderData>();
			foreach (EasHierarchySyncState.EasFolderData easFolderData in easHierarchySyncState.FolderData)
			{
				entryIdMap[EasMailbox.GetEntryId(easFolderData.ServerId)] = easFolderData;
			}
			EntryIdMap<EasHierarchySyncState.EasFolderData> entryIdMap2 = new EntryIdMap<EasHierarchySyncState.EasFolderData>();
			foreach (EasHierarchySyncState.EasFolderData easFolderData2 in easHierarchySyncState2.FolderData)
			{
				entryIdMap2[EasMailbox.GetEntryId(easFolderData2.ServerId)] = easFolderData2;
			}
			MailboxChangesManifest mailboxChangesManifest = new MailboxChangesManifest();
			mailboxChangesManifest.DeletedFolders = new List<byte[]>();
			foreach (byte[] array in entryIdMap.Keys)
			{
				if (!entryIdMap2.ContainsKey(array))
				{
					mailboxChangesManifest.DeletedFolders.Add(array);
				}
			}
			mailboxChangesManifest.ChangedFolders = new List<byte[]>();
			foreach (KeyValuePair<byte[], EasHierarchySyncState.EasFolderData> keyValuePair in entryIdMap2)
			{
				byte[] key = keyValuePair.Key;
				EasHierarchySyncState.EasFolderData value = keyValuePair.Value;
				EasHierarchySyncState.EasFolderData easFolderData3;
				if (entryIdMap.TryGetValue(key, out easFolderData3))
				{
					if (easFolderData3.ParentId != value.ParentId || easFolderData3.DisplayName != value.DisplayName)
					{
						mailboxChangesManifest.ChangedFolders.Add(key);
					}
				}
				else
				{
					mailboxChangesManifest.ChangedFolders.Add(key);
				}
			}
			return mailboxChangesManifest;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00006354 File Offset: 0x00004554
		private static string ClientIdFromItemId(byte[] itemId)
		{
			string text = TraceUtils.DumpBytes(itemId);
			if (text.Length > 40)
			{
				text = text.Substring(text.Length - 40);
			}
			return text;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00006384 File Offset: 0x00004584
		private static void CopyContactItem(MessageRec messageRec, IFolderProxy folderProxy)
		{
			ArgumentValidator.ThrowIfNull("messageRec", messageRec);
			ArgumentValidator.ThrowIfNull("folderProxy", folderProxy);
			EasFxContactMessage message = new EasFxContactMessage(messageRec);
			FxUtils.CopyItem(messageRec, message, folderProxy, EasSourceMailbox.EmptyPropTagArray);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000063BC File Offset: 0x000045BC
		private static void CopyCalendarItem(MessageRec messageRec, Properties calendarItemProperties, IFolderProxy folderProxy)
		{
			ArgumentValidator.ThrowIfNull("messageRec", messageRec);
			ArgumentValidator.ThrowIfNull("folderProxy", folderProxy);
			if (calendarItemProperties == null)
			{
				return;
			}
			EasFxCalendarMessage message = new EasFxCalendarMessage(calendarItemProperties);
			FxUtils.CopyItem(messageRec, message, folderProxy, EasSourceMailbox.EmptyPropTagArray);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000063F8 File Offset: 0x000045F8
		private Properties ReadCalendarItem(MessageRec messageRec)
		{
			Add add = base.EasFolderCache[messageRec.FolderId];
			string stringId = EasMailbox.GetStringId(messageRec.EntryId);
			return base.EasConnectionWrapper.FetchCalendarItem(stringId, add.ServerId);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00006438 File Offset: 0x00004638
		private MailboxChangesManifest GetFolderChangesOnServer(string syncKey, out string newSyncKey, out IReadOnlyCollection<Add> newFolders)
		{
			newFolders = null;
			FolderSyncResponse folderSyncResponse = base.EasConnectionWrapper.FolderSync(syncKey);
			newSyncKey = folderSyncResponse.SyncKey;
			MailboxChangesManifest mailboxChangesManifest = new MailboxChangesManifest();
			mailboxChangesManifest.ChangedFolders = new List<byte[]>(0);
			mailboxChangesManifest.DeletedFolders = new List<byte[]>(0);
			if (folderSyncResponse.Changes != null)
			{
				List<Add> additions = folderSyncResponse.Changes.Additions;
				if (additions != null && additions.Count > 0)
				{
					mailboxChangesManifest.ChangedFolders.Capacity += additions.Count;
					foreach (Add add in additions)
					{
						mailboxChangesManifest.ChangedFolders.Add(EasMailbox.GetEntryId(add.ServerId));
					}
					newFolders = additions;
				}
				List<Update> updates = folderSyncResponse.Changes.Updates;
				if (updates != null && updates.Count > 0)
				{
					mailboxChangesManifest.ChangedFolders.Capacity += updates.Count;
					foreach (Update update in updates)
					{
						mailboxChangesManifest.ChangedFolders.Add(EasMailbox.GetEntryId(update.ServerId));
					}
				}
				List<Delete> deletions = folderSyncResponse.Changes.Deletions;
				if (deletions != null && deletions.Count > 0)
				{
					mailboxChangesManifest.DeletedFolders.Capacity = deletions.Count;
					foreach (Delete delete in deletions)
					{
						mailboxChangesManifest.DeletedFolders.Add(EasMailbox.GetEntryId(delete.ServerId));
					}
				}
			}
			return mailboxChangesManifest;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000660C File Offset: 0x0000480C
		private Properties FetchMessageItem(MessageRec messageRec)
		{
			base.CheckDisposed();
			Add add = base.EasFolderCache[messageRec.FolderId];
			string stringId = EasMailbox.GetStringId(messageRec.EntryId);
			return base.EasConnectionWrapper.FetchMessageItem(stringId, add.ServerId);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00006674 File Offset: 0x00004874
		private void MarkMessageAsReadUnread(byte[] messageEntryId, byte[] folderEntryId, bool isRead)
		{
			this.UpdateItem(messageEntryId, folderEntryId, delegate(string messageId, string syncKey, string serverId)
			{
				this.EasConnectionWrapper.SyncRead(messageId, syncKey, serverId, isRead);
			});
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000066AC File Offset: 0x000048AC
		private byte[] MoveItem(byte[] messageEntryId, byte[] sourceFolderEntryId, byte[] destFolderEntryId, out bool isPermanentDeletionMove)
		{
			isPermanentDeletionMove = false;
			base.CheckDisposed();
			Add add;
			if (!base.EasFolderCache.TryGetValue(sourceFolderEntryId, out add))
			{
				MrsTracer.Provider.Warning("Source folder {0} doesn't exist", new object[]
				{
					TraceUtils.DumpBytes(sourceFolderEntryId)
				});
				throw new EasObjectNotFoundException(EasMailbox.GetStringId(sourceFolderEntryId));
			}
			Add add2;
			if (!base.EasFolderCache.TryGetValue(destFolderEntryId, out add2))
			{
				MrsTracer.Provider.Warning("Destination folder {0} doesn't exist", new object[]
				{
					TraceUtils.DumpBytes(destFolderEntryId)
				});
				throw new EasObjectNotFoundException(EasMailbox.GetStringId(destFolderEntryId));
			}
			string stringId = EasMailbox.GetStringId(messageEntryId);
			if (add2.Type == 4 && EasFolder.IsCalendarFolder((EasFolderType)add.Type))
			{
				this.DeleteItem(messageEntryId, sourceFolderEntryId);
				isPermanentDeletionMove = true;
				return null;
			}
			string stringId2 = base.EasConnectionWrapper.MoveItem(stringId, add.ServerId, add2.ServerId);
			return EasMailbox.GetEntryId(stringId2);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000678C File Offset: 0x0000498C
		private void DeleteItem(byte[] messageEntryId, byte[] folderEntryId)
		{
			base.CheckDisposed();
			Add add;
			if (!base.EasFolderCache.TryGetValue(folderEntryId, out add))
			{
				MrsTracer.Provider.Warning("Source folder {0} doesn't exist", new object[]
				{
					TraceUtils.DumpBytes(folderEntryId)
				});
				throw new EasObjectNotFoundException(EasMailbox.GetStringId(folderEntryId));
			}
			string stringId = EasMailbox.GetStringId(messageEntryId);
			string syncKey = base.GetPersistedSyncState(folderEntryId).SyncKey;
			base.EasConnectionWrapper.DeleteItem(stringId, syncKey, add.ServerId);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006828 File Offset: 0x00004A28
		private void FlagMessage(byte[] messageEntryId, byte[] folderEntryId, FlagStatus flagStatus)
		{
			this.UpdateItem(messageEntryId, folderEntryId, delegate(string messageId, string syncKey, string serverId)
			{
				this.EasConnectionWrapper.SyncFlag(messageId, syncKey, serverId, flagStatus);
			});
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006860 File Offset: 0x00004A60
		private void UpdateItem(byte[] itemEntryId, byte[] folderEntryId, Action<string, string, string> executeSync)
		{
			base.CheckDisposed();
			Add add;
			if (!base.EasFolderCache.TryGetValue(folderEntryId, out add))
			{
				MrsTracer.Provider.Warning("Source folder {0} doesn't exist", new object[]
				{
					TraceUtils.DumpBytes(folderEntryId)
				});
				throw new EasObjectNotFoundException(EasMailbox.GetStringId(folderEntryId));
			}
			string stringId = EasMailbox.GetStringId(itemEntryId);
			string syncKey = base.GetPersistedSyncState(folderEntryId).SyncKey;
			executeSync(stringId, syncKey, add.ServerId);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000068D4 File Offset: 0x00004AD4
		private byte[] CreateItem(byte[] itemEntryId, byte[] folderEntryId, Func<string, string, string, byte[]> executeSync)
		{
			base.CheckDisposed();
			Add add;
			if (!base.EasFolderCache.TryGetValue(folderEntryId, out add))
			{
				MrsTracer.Provider.Warning("Source folder {0} doesn't exist", new object[]
				{
					TraceUtils.DumpBytes(folderEntryId)
				});
				throw new EasObjectNotFoundException(EasMailbox.GetStringId(folderEntryId));
			}
			string arg = EasSourceMailbox.ClientIdFromItemId(itemEntryId);
			string syncKey = base.GetPersistedSyncState(folderEntryId).SyncKey;
			return executeSync(arg, syncKey, add.ServerId);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00006984 File Offset: 0x00004B84
		private void UpdateCalendarEvent(byte[] calendarEventId, byte[] folderEntryId, Event theEvent, IList<Event> exceptionalEvents, IList<string> deletedOccurrences)
		{
			this.UpdateItem(calendarEventId, folderEntryId, delegate(string messageId, string syncKey, string serverId)
			{
				this.EasConnectionWrapper.UpdateCalendarEvent(messageId, syncKey, serverId, theEvent, exceptionalEvents, deletedOccurrences, this.EasAuthenticationParameters.UserSmtpAddress);
			});
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00006A1C File Offset: 0x00004C1C
		private byte[] CreateCalendarEvent(byte[] calendarEventId, byte[] folderEntryId, Event theEvent, IList<Event> exceptionalEvents, IList<string> deletedOccurrences)
		{
			SyncContentsManifestState syncContentsManifestState = this.SyncState[folderEntryId];
			EasFolderSyncState persistedSyncState = base.GetPersistedSyncState(syncContentsManifestState);
			string newSyncKey = null;
			byte[] result = this.CreateItem(calendarEventId, folderEntryId, (string itemClientId, string syncKey, string serverId) => this.EasConnectionWrapper.CreateCalendarEvent(itemClientId, syncKey, out newSyncKey, serverId, theEvent, exceptionalEvents, deletedOccurrences, this.EasAuthenticationParameters.UserSmtpAddress));
			if (newSyncKey != null)
			{
				persistedSyncState.SyncKey = newSyncKey;
				syncContentsManifestState.Data = persistedSyncState.Serialize();
			}
			return result;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00006AAC File Offset: 0x00004CAC
		ResourceHealthTracker ISupportMime.get_RHTracker()
		{
			return base.RHTracker;
		}

		// Token: 0x0400004F RID: 79
		private static readonly PropTag[] EmptyPropTagArray = new PropTag[0];
	}
}
