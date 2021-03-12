using System;
using System.Collections.Generic;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Eas.Commands;
using Microsoft.Exchange.Connections.Eas.Commands.ItemOperations;
using Microsoft.Exchange.Connections.Eas.Commands.Sync;
using Microsoft.Exchange.Connections.Eas.Model.Extensions;
using Microsoft.Exchange.Connections.Eas.Model.Response.AirSync;
using Microsoft.Exchange.Connections.Eas.Model.Response.FolderHierarchy;
using Microsoft.Exchange.Connections.Eas.Model.Response.ItemOperations;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000005 RID: 5
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class EasFolder : EasFolderBase
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00002FE3 File Offset: 0x000011E3
		protected EasFolder()
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002FEB File Offset: 0x000011EB
		protected EasFolder(Add add, UserSmtpAddress userSmtpAddress) : base(add.ServerId, add.ParentId, add.DisplayName, add.GetEasFolderType())
		{
			this.UserSmtpAddressString = (string)userSmtpAddress;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003017 File Offset: 0x00001217
		// (set) Token: 0x0600006F RID: 111 RVA: 0x0000301F File Offset: 0x0000121F
		internal string UserSmtpAddressString { get; private set; }

		// Token: 0x06000070 RID: 112 RVA: 0x00003028 File Offset: 0x00001228
		internal static bool IsCalendarFolder(EasFolderType easFolderType)
		{
			return easFolderType == EasFolderType.Calendar || easFolderType == EasFolderType.UserCalendar;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003035 File Offset: 0x00001235
		internal static bool IsContactFolder(EasFolderType easFolderType)
		{
			return easFolderType == EasFolderType.Contacts || easFolderType == EasFolderType.UserContacts;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000030A0 File Offset: 0x000012A0
		protected override List<MessageRec> InternalLookupMessages(PropTag ptagToLookup, List<byte[]> keysToLookup, PropTag[] additionalPtagsToLoad)
		{
			ArgumentValidator.ThrowIfInvalidValue<PropTag>("ptagToLookup", ptagToLookup, (PropTag ptag) => ptag == PropTag.EntryId);
			List<string> list = new List<string>(keysToLookup.Count);
			foreach (byte[] entryId in keysToLookup)
			{
				list.Add(EasMailbox.GetStringId(entryId));
			}
			List<Fetch> fetchList = new List<Fetch>(keysToLookup.Count);
			CommonUtils.ProcessInBatches<string>(list.ToArray(), 10, delegate(string[] messageBatch)
			{
				ItemOperationsResponse itemOperationsResponse = this.Mailbox.EasConnectionWrapper.LookupItems(messageBatch, this.ServerId);
				fetchList.AddRange(itemOperationsResponse.Response.Fetches);
			});
			return this.CreateMessageRecsForFetches(fetchList);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000316C File Offset: 0x0000136C
		protected int GetItemEstimate(EasConnectionWrapper easConnectionWrapper, EasSyncOptions options)
		{
			MrsTracer.Provider.Function("EasFolder.GetItemEstimate: SyncKey={0}", new object[]
			{
				options.SyncKey
			});
			return easConnectionWrapper.GetCountOfItemsToSync(base.ServerId, options);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000031A8 File Offset: 0x000013A8
		protected EasSyncResult SyncMessages(EasConnectionWrapper easConnectionWrapper, EasSyncOptions options)
		{
			MrsTracer.Provider.Function("EasFolder.SyncMessages: SyncKey={0}", new object[]
			{
				options.SyncKey
			});
			bool recentOnly = !EasFolder.IsCalendarFolder(base.EasFolderType) && options.RecentOnly && !EasFolder.IsContactFolder(base.EasFolderType);
			SyncResponse syncResponse;
			try
			{
				syncResponse = easConnectionWrapper.Sync(base.ServerId, options, recentOnly);
			}
			catch (EasRequiresSyncKeyResetException ex)
			{
				MrsTracer.Provider.Error("Encountered RequiresSyncKeyReset error: {0}", new object[]
				{
					ex
				});
				options.SyncKey = "0";
				syncResponse = easConnectionWrapper.Sync(base.ServerId, options, recentOnly);
			}
			if (!(options.SyncKey == "0"))
			{
				return this.GetMessageRecsAndNewSyncKey(syncResponse, options);
			}
			return this.ProcessPrimingSync(easConnectionWrapper, options, syncResponse);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003440 File Offset: 0x00001640
		protected IEnumerable<MessageRec> CreateMessageRecsForDeletions(List<DeleteCommand> deletions)
		{
			foreach (DeleteCommand delete in deletions)
			{
				yield return new MessageRec(EasMailbox.GetEntryId(delete.ServerId), base.EntryId, DateTime.MinValue, 0, MsgRecFlags.Deleted, this.GetAdditionalProps(delete));
			}
			yield break;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003464 File Offset: 0x00001664
		private static DateTime GetCreationTimestamp(string dateReceived)
		{
			DateTime minValue;
			if (!DateTime.TryParse(dateReceived, out minValue))
			{
				minValue = DateTime.MinValue;
			}
			return minValue;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003484 File Offset: 0x00001684
		private List<MessageRec> CreateMessageRecsForFetches(IReadOnlyCollection<Fetch> fetches)
		{
			List<MessageRec> list = new List<MessageRec>(fetches.Count);
			foreach (Fetch fetch in fetches)
			{
				if (fetch.Status == 1)
				{
					list.Add(new MessageRec(EasMailbox.GetEntryId(fetch.ServerId), base.EntryId, EasFolder.GetCreationTimestamp(fetch.Properties.DateReceived), (int)fetch.Properties.Body.EstimatedDataSize, MsgRecFlags.None, this.GetAdditionalProps(fetch)));
				}
			}
			return list;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003720 File Offset: 0x00001920
		private IEnumerable<MessageRec> CreateMessageRecsForAdditions(List<AddCommand> additions)
		{
			foreach (AddCommand addition in additions)
			{
				yield return new MessageRec(EasMailbox.GetEntryId(addition.ServerId), base.EntryId, EasFolder.GetCreationTimestamp(addition.ApplicationData.DateReceived), (int)((addition.ApplicationData.Body == null) ? 0U : addition.ApplicationData.Body.EstimatedDataSize), MsgRecFlags.None, this.GetAdditionalProps(addition));
			}
			yield break;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003940 File Offset: 0x00001B40
		private IEnumerable<MessageRec> CreateMessageRecsForChanges(List<ChangeCommand> changes)
		{
			foreach (ChangeCommand change in changes)
			{
				int estimatedDataSize = (int)((change.ApplicationData.Body == null) ? 0U : change.ApplicationData.Body.EstimatedDataSize);
				yield return new MessageRec(EasMailbox.GetEntryId(change.ServerId), base.EntryId, DateTime.MinValue, estimatedDataSize, MsgRecFlags.None, this.GetAdditionalProps(change));
			}
			yield break;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003978 File Offset: 0x00001B78
		private string GetSyncKeyForFolder(SyncResponse syncResponse, out bool hasMoreAvailable)
		{
			Collection collection2 = syncResponse.Collections.Find((Collection collection) => collection.CollectionId == base.ServerId);
			hasMoreAvailable = (collection2 != null && collection2.HasMoreAvailable());
			if (collection2 == null)
			{
				return null;
			}
			return collection2.SyncKey;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000039B8 File Offset: 0x00001BB8
		private EasSyncResult ProcessPrimingSync(EasConnectionWrapper easConnection, EasSyncOptions options, SyncResponse syncResponse)
		{
			bool flag;
			string syncKeyForFolder = this.GetSyncKeyForFolder(syncResponse, out flag);
			if (string.IsNullOrEmpty(syncKeyForFolder))
			{
				throw new EasSyncCouldNotFindFolderException(base.ServerId);
			}
			options.SyncKey = syncKeyForFolder;
			return this.SyncMessages(easConnection, options);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000039F4 File Offset: 0x00001BF4
		private EasSyncResult GetMessageRecsAndNewSyncKey(SyncResponse syncResponse, EasSyncOptions options)
		{
			List<MessageRec> list = new List<MessageRec>(options.MaxNumberOfMessage);
			bool hasMoreAvailable;
			string text = this.GetSyncKeyForFolder(syncResponse, out hasMoreAvailable);
			if (string.IsNullOrEmpty(text))
			{
				text = options.SyncKey;
			}
			else
			{
				list.AddRange(this.CreateMessageRecsForAdditions(syncResponse.Additions));
				list.AddRange(this.CreateMessageRecsForDeletions(syncResponse.Deletions));
				list.AddRange(this.CreateMessageRecsForChanges(syncResponse.Changes));
			}
			return new EasSyncResult
			{
				MessageRecs = list,
				SyncKeyRequested = options.SyncKey,
				NewSyncKey = text,
				HasMoreAvailable = hasMoreAvailable
			};
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003A90 File Offset: 0x00001C90
		private PropValueData[] GetAdditionalProps(Fetch fetch)
		{
			return SyncEmailUtils.GetMessageProps(new SyncEmailContext
			{
				IsRead = new bool?(fetch.IsRead()),
				IsDraft = new bool?(base.EasFolderType == EasFolderType.Drafts),
				SyncMessageId = fetch.ServerId
			}, this.UserSmtpAddressString, base.EntryId, new PropValueData[]
			{
				new PropValueData(PropTag.LastModificationTime, DateTime.UtcNow)
			});
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003B0C File Offset: 0x00001D0C
		private PropValueData[] GetAdditionalProps(AddCommand add)
		{
			List<PropValueData> list = new List<PropValueData>();
			list.Add(new PropValueData(PropTag.LastModificationTime, CommonUtils.DefaultLastModificationTime));
			list.Add(new PropValueData(PropTag.ObjectType, 0));
			if (EasFolder.IsContactFolder(base.EasFolderType))
			{
				List<PropValueData> contactProperties = EasFxContactMessage.GetContactProperties(add.ApplicationData);
				if (contactProperties.Count > 0)
				{
					list.AddRange(contactProperties);
				}
			}
			return SyncEmailUtils.GetMessageProps(new SyncEmailContext
			{
				IsRead = new bool?(add.IsRead()),
				IsDraft = new bool?(base.EasFolderType == EasFolderType.Drafts),
				SyncMessageId = add.ServerId
			}, this.UserSmtpAddressString, base.EntryId, list.ToArray());
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003BCC File Offset: 0x00001DCC
		private PropValueData[] GetAdditionalProps(ChangeCommand change)
		{
			bool? isRead = change.IsRead();
			EasMessageCategory easMessageCategory = (isRead == null) ? EasMessageCategory.AddOrUpdate : (isRead.Value ? EasMessageCategory.ChangeToRead : EasMessageCategory.ChangeToUnread);
			List<PropValueData> list = new List<PropValueData>();
			list.Add(new PropValueData(PropTag.LastModificationTime, DateTime.UtcNow));
			list.Add(new PropValueData(PropTag.ObjectType, (int)easMessageCategory));
			if (EasFolder.IsContactFolder(base.EasFolderType))
			{
				List<PropValueData> contactProperties = EasFxContactMessage.GetContactProperties(change.ApplicationData);
				if (contactProperties.Count > 0)
				{
					list.AddRange(contactProperties);
				}
			}
			return SyncEmailUtils.GetMessageProps(new SyncEmailContext
			{
				IsRead = isRead,
				IsDraft = new bool?(base.EasFolderType == EasFolderType.Drafts),
				SyncMessageId = change.ServerId
			}, this.UserSmtpAddressString, base.EntryId, list.ToArray());
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003CA4 File Offset: 0x00001EA4
		private PropValueData[] GetAdditionalProps(DeleteCommand delete)
		{
			return SyncEmailUtils.GetMessageProps(new SyncEmailContext
			{
				SyncMessageId = delete.ServerId
			}, this.UserSmtpAddressString, base.EntryId, new PropValueData[]
			{
				new PropValueData(PropTag.ObjectType, 1)
			});
		}
	}
}
