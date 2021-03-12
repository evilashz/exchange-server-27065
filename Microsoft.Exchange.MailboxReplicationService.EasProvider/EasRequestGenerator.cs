using System;
using System.Collections.Generic;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Eas.Commands.GetItemEstimate;
using Microsoft.Exchange.Connections.Eas.Commands.ItemOperations;
using Microsoft.Exchange.Connections.Eas.Commands.MoveItems;
using Microsoft.Exchange.Connections.Eas.Commands.Sync;
using Microsoft.Exchange.Connections.Eas.Model.Common.AirSyncBase;
using Microsoft.Exchange.Connections.Eas.Model.Common.Email;
using Microsoft.Exchange.Connections.Eas.Model.Extensions;
using Microsoft.Exchange.Connections.Eas.Model.Request.AirSync;
using Microsoft.Exchange.Connections.Eas.Model.Request.AirSyncBase;
using Microsoft.Exchange.Connections.Eas.Model.Request.GetItemEstimate;
using Microsoft.Exchange.Connections.Eas.Model.Request.ItemOperations;
using Microsoft.Exchange.Connections.Eas.Model.Request.Move;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Entities.DataModel.Calendaring;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200000B RID: 11
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class EasRequestGenerator
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00004964 File Offset: 0x00002B64
		internal static SyncRequest CreateSyncRequestForAllMessages(string syncKey, string serverId, int windowSize, bool recentOnly)
		{
			SyncFilterType filter = recentOnly ? SyncFilterType.ThreeDaysBack : SyncFilterType.NoFilter;
			Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Collection item = EasRequestGenerator.CollectionForSyncMetadata(syncKey, serverId, windowSize, filter);
			return new SyncRequest
			{
				Collections = 
				{
					item
				}
			};
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004998 File Offset: 0x00002B98
		internal static SyncRequest CreateSyncRequestForCreateCalendarEvent(string syncKey, string clientId, string folderId, Event theEvent, IList<Event> exceptionalEvents, IList<string> deletedOccurrences, UserSmtpAddress userSmtpAddress)
		{
			Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Collection collection = new Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Collection();
			AddCommand addCommand = new AddCommand();
			addCommand.Class = "Calendar";
			addCommand.ClientId = clientId;
			addCommand.ApplicationData = SyncCalendarUtils.ConvertEventToAppData(theEvent, exceptionalEvents, deletedOccurrences, userSmtpAddress);
			collection.Commands.Add(addCommand);
			collection.SyncKey = syncKey;
			collection.CollectionId = folderId;
			collection.GetChanges = new bool?(false);
			return new SyncRequest
			{
				Collections = 
				{
					collection
				}
			};
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004A10 File Offset: 0x00002C10
		internal static SyncRequest CreateSyncRequestForUpdateCalendarEvent(string syncKey, string itemId, string folderId, Event theEvent, IList<Event> exceptionalEvents, IList<string> deletedOccurrences, UserSmtpAddress userSmtpAddress)
		{
			Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Collection collection = new Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Collection();
			ChangeCommand changeCommand = new ChangeCommand();
			changeCommand.ServerId = itemId;
			changeCommand.ApplicationData = SyncCalendarUtils.ConvertEventToAppData(theEvent, exceptionalEvents, deletedOccurrences, userSmtpAddress);
			collection.Commands.Add(changeCommand);
			collection.SyncKey = syncKey;
			collection.CollectionId = folderId;
			collection.GetChanges = new bool?(false);
			return new SyncRequest
			{
				Collections = 
				{
					collection
				}
			};
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004A7C File Offset: 0x00002C7C
		internal static SyncRequest CreateSyncRequestForSelectedMessages(IReadOnlyCollection<string> messageIds, string syncKey, string serverId)
		{
			SyncRequest syncRequest = new SyncRequest();
			Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Collection collection = new Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Collection
			{
				SyncKey = syncKey,
				CollectionId = serverId,
				GetChanges = new bool?(false)
			};
			foreach (string messageId in messageIds)
			{
				Command item = EasRequestGenerator.CommandForFetchSingle(messageId);
				collection.Commands.Add(item);
			}
			syncRequest.Collections.Add(collection);
			return syncRequest;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004B10 File Offset: 0x00002D10
		internal static SyncRequest CreateSyncRequestForDeleteMessages(IReadOnlyCollection<string> messageIds, string syncKey, string serverId)
		{
			SyncRequest syncRequest = new SyncRequest();
			Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Collection collection = new Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Collection
			{
				SyncKey = syncKey,
				CollectionId = serverId,
				GetChanges = new bool?(false),
				DeletesAsMoves = new bool?(false)
			};
			foreach (string messageId in messageIds)
			{
				Command item = EasRequestGenerator.CommandForDelete(messageId);
				collection.Commands.Add(item);
			}
			syncRequest.Collections.Add(collection);
			return syncRequest;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004BB0 File Offset: 0x00002DB0
		internal static SyncRequest CreateSyncRequestForReadUnreadMessages(IReadOnlyCollection<string> messageIds, string syncKey, string serverId, bool read)
		{
			SyncRequest syncRequest = new SyncRequest();
			Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Collection collection = new Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Collection
			{
				SyncKey = syncKey,
				CollectionId = serverId,
				GetChanges = new bool?(false)
			};
			foreach (string messageId in messageIds)
			{
				Command item = EasRequestGenerator.CommandForReadUnread(messageId, read);
				collection.Commands.Add(item);
			}
			syncRequest.Collections.Add(collection);
			return syncRequest;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004C44 File Offset: 0x00002E44
		internal static SyncRequest CreateSyncRequestForFlagMessages(IReadOnlyCollection<string> messageIds, string syncKey, string serverId, FlagStatus flagStatus)
		{
			SyncRequest syncRequest = new SyncRequest();
			Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Collection collection = new Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Collection
			{
				SyncKey = syncKey,
				CollectionId = serverId,
				GetChanges = new bool?(false)
			};
			foreach (string messageId in messageIds)
			{
				Command item = EasRequestGenerator.CommandForFlag(messageId, flagStatus);
				collection.Commands.Add(item);
			}
			syncRequest.Collections.Add(collection);
			return syncRequest;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004CD8 File Offset: 0x00002ED8
		internal static GetItemEstimateRequest CreateEstimateRequest(string syncKey, string serverId, bool recentOnly)
		{
			SyncFilterType filter = recentOnly ? SyncFilterType.ThreeDaysBack : SyncFilterType.NoFilter;
			Microsoft.Exchange.Connections.Eas.Model.Request.GetItemEstimate.Collection item = EasRequestGenerator.CollectionForItemEstimate(syncKey, serverId, filter);
			return new GetItemEstimateRequest
			{
				Collections = 
				{
					item
				}
			};
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004D0C File Offset: 0x00002F0C
		internal static ItemOperationsRequest CreateItemOpsRequest(string messageId, string folderId)
		{
			BodyPreference bodyPreference = new BodyPreference
			{
				Type = new byte?(4)
			};
			Microsoft.Exchange.Connections.Eas.Model.Request.ItemOperations.Options options = new Microsoft.Exchange.Connections.Eas.Model.Request.ItemOperations.Options
			{
				MimeSupport = new byte?(2),
				BodyPreference = bodyPreference
			};
			Fetch fetch = new Fetch
			{
				Store = Store.Mailbox,
				CollectionId = folderId,
				ServerId = messageId,
				Options = options
			};
			return new ItemOperationsRequest
			{
				Fetches = new Fetch[]
				{
					fetch
				}
			};
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004D9C File Offset: 0x00002F9C
		internal static ItemOperationsRequest CreateItemOpsRequestForSelectedMessages(IReadOnlyCollection<string> messageIds, string folderId)
		{
			List<Fetch> list = new List<Fetch>(messageIds.Count);
			foreach (string serverId in messageIds)
			{
				list.Add(new Fetch
				{
					Store = Store.Mailbox,
					CollectionId = folderId,
					ServerId = serverId
				});
			}
			return new ItemOperationsRequest
			{
				Fetches = list.ToArray()
			};
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004E28 File Offset: 0x00003028
		internal static ItemOperationsRequest CreateItemOpsRequestForCalendarItem(string messageId, string folderId)
		{
			ItemOperationsRequest itemOperationsRequest = EasRequestGenerator.CreateItemOpsRequest(messageId, folderId);
			itemOperationsRequest.Fetches[0].Options.BodyPreference = new BodyPreference
			{
				Type = new byte?(1),
				AllOrNone = new bool?(true),
				TruncationSize = new uint?(10000U)
			};
			return itemOperationsRequest;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004E80 File Offset: 0x00003080
		internal static MoveItemsRequest CreateMoveRequestForMessages(IReadOnlyCollection<string> messageIds, string sourceFolderId, string destinationFolderId)
		{
			List<Move> list = new List<Move>(messageIds.Count);
			foreach (string srcMsgId in messageIds)
			{
				Move item = new Move
				{
					DstFldId = destinationFolderId,
					SrcFldId = sourceFolderId,
					SrcMsgId = srcMsgId
				};
				list.Add(item);
			}
			return new MoveItemsRequest
			{
				Moves = list.ToArray()
			};
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004F10 File Offset: 0x00003110
		private static Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Collection CollectionForSyncMetadata(string syncKey, string serverId, int windowSize, SyncFilterType filter)
		{
			Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Collection collection = new Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Collection
			{
				SyncKey = syncKey,
				CollectionId = serverId,
				DeletesAsMoves = new bool?(false),
				WindowSize = new int?(Math.Max(0, Math.Min(windowSize, 512)))
			};
			Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Options item = new Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Options
			{
				FilterType = new byte?((byte)filter)
			};
			collection.Options.Add(item);
			return collection;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004F80 File Offset: 0x00003180
		private static Microsoft.Exchange.Connections.Eas.Model.Request.GetItemEstimate.Collection CollectionForItemEstimate(string syncKey, string serverId, SyncFilterType filter)
		{
			Microsoft.Exchange.Connections.Eas.Model.Request.GetItemEstimate.Collection collection = new Microsoft.Exchange.Connections.Eas.Model.Request.GetItemEstimate.Collection
			{
				SyncKey = syncKey,
				CollectionId = serverId
			};
			Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Options item = new Microsoft.Exchange.Connections.Eas.Model.Request.AirSync.Options
			{
				FilterType = new byte?((byte)filter)
			};
			collection.Options.Add(item);
			return collection;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004FC8 File Offset: 0x000031C8
		private static Command CommandForFetchSingle(string messageId)
		{
			return new FetchCommand
			{
				ServerId = messageId
			};
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004FE8 File Offset: 0x000031E8
		private static Command CommandForDelete(string messageId)
		{
			return new DeleteCommand
			{
				ServerId = messageId
			};
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005008 File Offset: 0x00003208
		private static Command CommandForReadUnread(string messageId, bool read)
		{
			return new ChangeCommand
			{
				ServerId = messageId,
				ApplicationData = new ApplicationData
				{
					Read = new byte?(read ? 1 : 0)
				}
			};
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005044 File Offset: 0x00003244
		private static Command CommandForFlag(string messageId, FlagStatus flagStatus)
		{
			return new ChangeCommand
			{
				ServerId = messageId,
				ApplicationData = new ApplicationData
				{
					Flag = new Flag
					{
						Status = (int)flagStatus
					}
				}
			};
		}

		// Token: 0x04000045 RID: 69
		internal const int MaxSyncWindowSize = 512;

		// Token: 0x04000046 RID: 70
		internal const int MaxClientIdSize = 40;

		// Token: 0x04000047 RID: 71
		internal const int MaxFetchItemCount = 10;

		// Token: 0x04000048 RID: 72
		internal const int MaxCalendarItemBodySize = 10000;

		// Token: 0x04000049 RID: 73
		private const SyncFilterType RecentSyncFilter = SyncFilterType.ThreeDaysBack;

		// Token: 0x0400004A RID: 74
		internal static readonly TimeSpan RecentSyncTimeSpan = TimeSpan.FromDays(3.0);
	}
}
