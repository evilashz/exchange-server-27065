using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000020 RID: 32
	public class ConversationItem : TopMessage
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x000304E8 File Offset: 0x0002E6E8
		private ConversationItem(Context context, Mailbox mailbox, Folder folder, byte[] conversationId, TopMessage message) : base(context, mailbox, folder, true, null, null, ExchangeId.Null)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.fidMidList = new List<FidMid>(0);
				this.SetFidMidList(context);
				base.SetColumn(context, base.MessageTable.ConversationId, conversationId);
				base.SetColumn(context, base.MessageTable.CodePage, null);
				this.SetProperty(context, PropTag.Message.SearchKey, null);
				this.SetProperty(context, PropTag.Message.DisplayBcc, null);
				this.SetProperty(context, PropTag.Message.DisplayCc, null);
				this.SetProperty(context, PropTag.Message.DisplayTo, null);
				int num = (message != null) ? TopMessage.GetNewMessageDocumentId(context, message.GetMessageClass(context), message.ParentFolder) : TopMessage.GetNewMessageDocumentId(context, null, folder);
				base.SetColumn(context, base.MessageTable.MessageDocumentId, num);
				disposeGuard.Success();
			}
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000305E4 File Offset: 0x0002E7E4
		private ConversationItem(Context context, Mailbox mailbox, int documentId) : base(context, mailbox, null, false, documentId, null, ExchangeId.Null)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.PossiblyLoadFidMidList(context);
				disposeGuard.Success();
			}
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0003063C File Offset: 0x0002E83C
		private ConversationItem(Context context, Mailbox mailbox, Reader reader) : base(context, mailbox, null, reader)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.PossiblyLoadFidMidList(context);
				disposeGuard.Success();
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00030688 File Offset: 0x0002E888
		public override bool IsConversation
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0003068B File Offset: 0x0002E88B
		public bool IsEmpty
		{
			get
			{
				return this.fidMidList.Count == 0;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0003069B File Offset: 0x0002E89B
		public bool IsMaxSize
		{
			get
			{
				return this.fidMidList.Count >= 300;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x000306B2 File Offset: 0x0002E8B2
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x000306BA File Offset: 0x0002E8BA
		public TopMessage ModifiedMessage { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x000306C3 File Offset: 0x0002E8C3
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x000306CB File Offset: 0x0002E8CB
		public ModifiedSearchFolders ModifiedSearchFolders { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x000306D4 File Offset: 0x0002E8D4
		public IList<FidMid> FidMidList
		{
			get
			{
				return this.fidMidList;
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000306DC File Offset: 0x0002E8DC
		public static ConversationItem CreateConversationItem(Context context, Mailbox mailbox, byte[] conversationId, TopMessage message)
		{
			Folder folder = Folder.OpenFolder(context, mailbox, ConversationItem.GetConversationFolderId(context, mailbox));
			return new ConversationItem(context, mailbox, folder, conversationId, message);
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00030702 File Offset: 0x0002E902
		public static ConversationItem OpenConversationItem(Context context, Mailbox mailbox, Reader reader)
		{
			return new ConversationItem(context, mailbox, reader);
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000309A0 File Offset: 0x0002EBA0
		public static IEnumerable<ConversationItem> OpenConversations(Context context, Mailbox mailbox, IEnumerable<byte[]> conversationIds)
		{
			MessageTable messageTable = DatabaseSchema.MessageTable(mailbox.Database);
			object mailboxPartitionNumber = mailbox.MailboxPartitionNumber;
			int numConversations = conversationIds.Count<byte[]>();
			IList<KeyRange> keys = new List<KeyRange>(numConversations);
			foreach (byte[] array in conversationIds)
			{
				StartStopKey startStopKey = new StartStopKey(true, new object[]
				{
					mailboxPartitionNumber,
					array
				});
				keys.Add(new KeyRange(startStopKey, startStopKey));
			}
			SimpleQueryOperator query = ConversationItem.GetConversationsOperator(context, mailbox, messageTable.ConversationIdUnique, keys);
			using (Reader reader = query.ExecuteReader(true))
			{
				while (reader.Read())
				{
					yield return new ConversationItem(context, mailbox, reader);
				}
			}
			yield break;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x000309CC File Offset: 0x0002EBCC
		public static ConversationItem OpenConversationItem(Context context, Mailbox mailbox, byte[] conversationId)
		{
			MessageTable messageTable = DatabaseSchema.MessageTable(mailbox.Database);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				mailbox.MailboxPartitionNumber,
				conversationId
			});
			IList<KeyRange> keys = new List<KeyRange>(1)
			{
				new KeyRange(startStopKey, startStopKey)
			};
			SimpleQueryOperator conversationsOperator = ConversationItem.GetConversationsOperator(context, mailbox, messageTable.ConversationIdUnique, keys);
			using (Reader reader = conversationsOperator.ExecuteReader(true))
			{
				if (reader.Read())
				{
					return new ConversationItem(context, mailbox, reader);
				}
			}
			return null;
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00030A74 File Offset: 0x0002EC74
		public static ConversationItem OpenConversationItem(Context context, Mailbox mailbox, ExchangeId mid)
		{
			MessageTable messageTable = DatabaseSchema.MessageTable(mailbox.Database);
			ExchangeId conversationFolderId = ConversationItem.GetConversationFolderId(context, mailbox);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				mailbox.MailboxPartitionNumber,
				conversationFolderId.To26ByteArray(),
				false,
				mid.To26ByteArray()
			});
			IList<KeyRange> keys = new List<KeyRange>(1)
			{
				new KeyRange(startStopKey, startStopKey)
			};
			SimpleQueryOperator conversationsOperator = ConversationItem.GetConversationsOperator(context, mailbox, messageTable.MessageUnique, keys);
			using (Reader reader = conversationsOperator.ExecuteReader(true))
			{
				if (reader.Read())
				{
					return new ConversationItem(context, mailbox, reader);
				}
			}
			return null;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00030B44 File Offset: 0x0002ED44
		public static SimpleQueryOperator GetConversationsOperator(Context context, Mailbox mailbox, Index index, IList<KeyRange> keys)
		{
			MessageTable messageTable = DatabaseSchema.MessageTable(mailbox.Database);
			IList<Column> list = new List<Column>(messageTable.Table.CommonColumns);
			IList<Column> columns = messageTable.Table.PrimaryKeyIndex.Columns;
			for (int i = 0; i < columns.Count; i++)
			{
				Column item = columns[i];
				if (!list.Contains(item))
				{
					list.Add(item);
				}
			}
			return Factory.CreateTableOperator(context.Culture, context, messageTable.Table, index, list, null, null, 0, 0, keys, false, true);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00030BC8 File Offset: 0x0002EDC8
		public static ConversationItem OpenConversationItem(Context context, Mailbox mailbox, int documentId)
		{
			ConversationItem conversationItem = new ConversationItem(context, mailbox, documentId);
			if (conversationItem.IsDead)
			{
				conversationItem.Dispose();
				conversationItem = null;
			}
			return conversationItem;
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00030BF0 File Offset: 0x0002EDF0
		public static ExchangeId GetConversationFolderId(Context context, Mailbox mailbox)
		{
			if (mailbox.ConversationFolderId.IsNullOrZero)
			{
				mailbox.ConversationFolderId = SpecialFoldersCache.GetSpecialFolders(context, mailbox)[20];
			}
			return mailbox.ConversationFolderId;
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00030C2C File Offset: 0x0002EE2C
		public static int? GetConversationDocumentId(Context context, Mailbox mailbox, byte[] conversationId)
		{
			int? result = null;
			MessageTable messageTable = DatabaseSchema.MessageTable(mailbox.Database);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				mailbox.MailboxPartitionNumber,
				conversationId
			});
			TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, messageTable.Table, messageTable.ConversationIdUnique, new Column[]
			{
				messageTable.MessageDocumentId
			}, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true);
			using (Reader reader = tableOperator.ExecuteReader(true))
			{
				if (reader.Read())
				{
					result = new int?(reader.GetInt32(messageTable.MessageDocumentId));
				}
			}
			return result;
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00030CF4 File Offset: 0x0002EEF4
		public byte[] GetFidMidBlob()
		{
			return FidMidListSerializer.ToBytes(this.fidMidList);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00030D01 File Offset: 0x0002EF01
		public ConversationMembers GetConversationMembers(Context context, TopMessage modifiedMessage, HashSet<StorePropTag> aggregatePropertiesToCompute)
		{
			return new ConversationMembers(base.Mailbox, this.fidMidList, this.GetOriginalFidMids(context), modifiedMessage, aggregatePropertiesToCompute);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00030D20 File Offset: 0x0002EF20
		public void AddMessage(Context context, FidMid fidMid)
		{
			if (ExTraceGlobals.ConversationsDetailedTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.ConversationsDetailedTracer.TraceDebug<FidMid, int>(0L, "Adding message ({0}) to ConversationItem, which has {1} messages", fidMid, this.fidMidList.Count);
			}
			List<FidMid> list = new List<FidMid>(this.fidMidList);
			int num = list.BinarySearch(fidMid);
			if (num < 0)
			{
				list.Insert(~num, fidMid);
			}
			else
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "Conversation already contains this message");
			}
			this.fidMidList = list;
			this.SetFidMidList(context);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00030D94 File Offset: 0x0002EF94
		public void RemoveMessage(Context context, FidMid fidMid)
		{
			if (ExTraceGlobals.ConversationsDetailedTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.ConversationsDetailedTracer.TraceDebug<FidMid, int>(0L, "Removing message ({0}) from ConversationItem, which has {1} messages", fidMid, this.fidMidList.Count);
			}
			List<FidMid> list = new List<FidMid>(this.fidMidList);
			int num = list.BinarySearch(fidMid);
			if (num >= 0)
			{
				list.RemoveAt(num);
			}
			else
			{
				Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "Conversation does not contain this message");
			}
			this.fidMidList = list;
			this.SetFidMidList(context);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x00030E08 File Offset: 0x0002F008
		internal static IEnumerable<int> GetConversationDocids(Context context, Mailbox mailbox, IEnumerable<byte[]> conversationIds)
		{
			MessageTable messageTable = DatabaseSchema.MessageTable(mailbox.Database);
			int num = conversationIds.Count<byte[]>();
			IList<KeyRange> list = new List<KeyRange>(num);
			foreach (byte[] array in conversationIds)
			{
				StartStopKey startStopKey = new StartStopKey(true, new object[]
				{
					mailbox.MailboxPartitionNumber,
					array
				});
				list.Add(new KeyRange(startStopKey, startStopKey));
			}
			List<int> list2 = new List<int>(num);
			TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, messageTable.Table, messageTable.ConversationIdUnique, new PhysicalColumn[]
			{
				messageTable.MessageDocumentId
			}, null, null, 0, num, list, false, true);
			using (Reader reader = tableOperator.ExecuteReader(true))
			{
				while (reader.Read())
				{
					list2.Add(reader.GetInt32(messageTable.MessageDocumentId));
				}
			}
			list2.Sort();
			return list2;
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00030F28 File Offset: 0x0002F128
		internal static Column GetConversationIdColumn(Mailbox mailbox)
		{
			return PropertySchema.MapToColumn(mailbox.Database, ObjectType.Message, PropTag.Message.ConversationId);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00030F3B File Offset: 0x0002F13B
		protected override void PossiblyRiseNotificationEvent(Context context, NotificationEvent notificationEvent)
		{
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00030F3D File Offset: 0x0002F13D
		protected override ObjectType GetObjectType()
		{
			return ObjectType.Conversation;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00030F40 File Offset: 0x0002F140
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<ConversationItem>(this);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00030F48 File Offset: 0x0002F148
		private void PossiblyLoadFidMidList(Context context)
		{
			if (!base.IsDead)
			{
				this.fidMidList = this.LoadFidMidList(context);
			}
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00030F5F File Offset: 0x0002F15F
		private IEnumerable<FidMid> GetOriginalFidMids(Context context)
		{
			return this.LoadOriginalFidMidList(context);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00030F68 File Offset: 0x0002F168
		internal bool ConversationContainsFidMid(FidMid fidMid)
		{
			return this.fidMidList.BinarySearch(fidMid) >= 0;
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00030F7C File Offset: 0x0002F17C
		private List<FidMid> LoadFidMidList(Context context)
		{
			byte[] buffer = (byte[])base.GetColumnValue(context, base.MessageTable.ConversationMembers);
			int num = 0;
			return FidMidListSerializer.FromBytes(buffer, ref num, base.Mailbox.ReplidGuidMap);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00030FB8 File Offset: 0x0002F1B8
		private IEnumerable<FidMid> LoadOriginalFidMidList(Context context)
		{
			byte[] buffer = (byte[])base.GetOriginalColumnValue(context, DatabaseSchema.MessageTable(base.Mailbox.Database).ConversationMembers);
			int num = 0;
			return FidMidListSerializer.FromBytes(buffer, ref num, base.Mailbox.ReplidGuidMap);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00030FFC File Offset: 0x0002F1FC
		private void SetFidMidList(Context context)
		{
			byte[] value = FidMidListSerializer.ToBytes(this.fidMidList);
			base.SetColumn(context, base.MessageTable.ConversationMembers, value);
		}

		// Token: 0x04000238 RID: 568
		private const int MaxConversationMembers = 300;

		// Token: 0x04000239 RID: 569
		private List<FidMid> fidMidList;
	}
}
