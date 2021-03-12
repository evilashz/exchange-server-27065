using System;
using System.Collections.Generic;
using System.Security.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.LogicalDataModel;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.Common.ExtensionMethods.Linq;
using Microsoft.Exchange.Server.Storage.LazyIndexing;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Exchange.Server.Storage.StoreCommonServices.DatabaseUpgraders;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200001F RID: 31
	public class TopMessage : Message, IStateObject
	{
		// Token: 0x060004CC RID: 1228 RVA: 0x0002D548 File Offset: 0x0002B748
		private TopMessage(Context context, MessageTable messageTable, Mailbox mailbox, bool newMessage, object documentId) : base(context, messageTable.Table, messageTable.Size, mailbox, true, newMessage, documentId != null, false, (documentId == null) ? new ColumnValue[]
		{
			new ColumnValue(messageTable.MailboxPartitionNumber, mailbox.MailboxPartitionNumber)
		} : new ColumnValue[]
		{
			new ColumnValue(messageTable.MailboxPartitionNumber, mailbox.MailboxPartitionNumber),
			new ColumnValue(messageTable.MessageDocumentId, documentId)
		})
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.messageTable = messageTable;
				disposeGuard.Success();
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0002D620 File Offset: 0x0002B820
		protected TopMessage(Context context, Mailbox mailbox, Folder folder, bool newMessage, object documentId, Folder copySourceFolder, ExchangeId copySourceMessageId) : this(context, Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(mailbox.Database), mailbox, newMessage, documentId)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.parentFolder = folder;
				if (!newMessage)
				{
					this.InitExistingMessage(context, (int)documentId);
				}
				else
				{
					if (documentId == null)
					{
						base.SetColumn(context, this.messageTable.IsHidden, false);
						base.SetColumn(context, this.messageTable.IsRead, false);
						base.SetColumn(context, this.messageTable.Status, 0);
						base.SetColumn(context, this.messageTable.MessageFlagsActual, 8);
						this.SetProperty(context, PropTag.Message.CreationTime, mailbox.UtcNow);
						this.SetProperty(context, PropTag.Message.SearchKey, Guid.NewGuid().ToByteArray());
						base.SetColumn(context, this.messageTable.CodePage, 1252);
						PropertySchemaPopulation.InitializeMessage(context, this);
						if (mailbox.SharedState.UnifiedState != null)
						{
							int num = (int)this.parentFolder.GetPropertyValue(context, PropTag.Folder.MailboxNum);
							Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(num == -1 || num == mailbox.MailboxNumber, "Mailbox object used to create message is wrong");
							if (context.PrimaryMailboxContext != null && context.PrimaryMailboxContext.MailboxNumber != mailbox.MailboxNumber)
							{
								throw new StoreException((LID)47836U, ErrorCodeValue.NotSupported);
							}
							if (mailbox.MailboxNumber != mailbox.MailboxPartitionNumber)
							{
								base.SetColumn(context, this.messageTable.MailboxNumber, mailbox.MailboxNumber);
							}
						}
					}
					else
					{
						if (context.PerfInstance != null)
						{
							context.PerfInstance.MessagesOpenedRate.Increment();
						}
						StorePerClientTypePerformanceCountersInstance perClientPerfInstance = context.Diagnostics.PerClientPerfInstance;
						if (perClientPerfInstance != null)
						{
							perClientPerfInstance.MessagesOpenedRate.Increment();
						}
						context.RegisterStateObject(this);
					}
					this.originalFolder = copySourceFolder;
					this.originalMessageID = copySourceMessageId;
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0002D844 File Offset: 0x0002BA44
		protected TopMessage(Context context, Mailbox mailbox, Folder folder, Reader reader) : base(context, Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(mailbox.Database).Table, Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(mailbox.Database).Size, mailbox, true, false, reader)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				this.messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(mailbox.Database);
				this.parentFolder = folder;
				if (!base.IsDead)
				{
					this.InitExistingMessage(context, this.GetDocumentId(context));
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0002D8DC File Offset: 0x0002BADC
		public ExchangeId DeleteChangeNumber
		{
			get
			{
				return this.deleteChangeNumber;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0002D8E4 File Offset: 0x0002BAE4
		public MessageTable MessageTable
		{
			get
			{
				return this.messageTable;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0002D8EC File Offset: 0x0002BAEC
		public Folder ParentFolder
		{
			get
			{
				return this.parentFolder;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0002D8F4 File Offset: 0x0002BAF4
		public virtual bool IsConversation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0002D8F7 File Offset: 0x0002BAF7
		public override bool IsEmbedded
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0002D8FA File Offset: 0x0002BAFA
		public int OriginalAttachCount
		{
			get
			{
				return this.originalAttachCount;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0002D902 File Offset: 0x0002BB02
		internal Folder OriginalFolder
		{
			get
			{
				return this.originalFolder;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004D6 RID: 1238 RVA: 0x0002D90A File Offset: 0x0002BB0A
		internal ExchangeId OriginalMessageID
		{
			get
			{
				return this.originalMessageID;
			}
		}

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0002D912 File Offset: 0x0002BB12
		internal OpenMessageInstance OpenMessageInstanceState
		{
			get
			{
				return this.openMessageInstanceState;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x0002D91A File Offset: 0x0002BB1A
		internal ulong ChangedGroups
		{
			get
			{
				return this.changedGroups;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0002D922 File Offset: 0x0002BB22
		internal ConversationItem ConversationItem
		{
			get
			{
				return this.conversationItem;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x0002D92A File Offset: 0x0002BB2A
		internal bool TimerEventFired
		{
			get
			{
				return this.timerEventFired;
			}
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0002D932 File Offset: 0x0002BB32
		internal static IDisposable SetMessagePrereadTestHook(Action<PreReadOperator> action)
		{
			return TopMessage.messagePrereadTestHook.SetTestHook(action);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0002D958 File Offset: 0x0002BB58
		public static TopMessage CopyMessage(Context context, Folder source, ExchangeId sourceMessageId, Folder destination)
		{
			if (destination is SearchFolder)
			{
				throw new StoreException((LID)52856U, ErrorCodeValue.SearchFolder);
			}
			SearchFolder searchFolder = source as SearchFolder;
			int? num;
			if (searchFolder != null)
			{
				num = searchFolder.LookupMessageByMid(context, sourceMessageId, null);
			}
			else
			{
				num = TopMessage.GetDocumentIdFromId(context, source.Mailbox, source.GetId(context), sourceMessageId);
			}
			if (num == null)
			{
				return null;
			}
			string messageClassByDocumentId = TopMessage.GetMessageClassByDocumentId(context, source.Mailbox, num.Value);
			int newMessageDocumentId = TopMessage.GetNewMessageDocumentId(context, messageClassByDocumentId, destination);
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(context.Database);
			HashSet<Column> excludeColumns = new HashSet<Column>();
			excludeColumns.Add(messageTable.MessageDocumentId);
			excludeColumns.Add(messageTable.MessageId);
			excludeColumns.Add(messageTable.FolderId);
			excludeColumns.Add(messageTable.ArticleNumber);
			excludeColumns.Add(messageTable.IMAPId);
			excludeColumns.Add(messageTable.LcnReadUnread);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				source.Mailbox.MailboxPartitionNumber,
				num
			});
			List<Column> list = new List<Column>(from col in messageTable.Table.Columns
			where !excludeColumns.Contains(col)
			select col);
			List<Column> list2 = new List<Column>(list);
			list.Add(Factory.CreateConstantColumn(newMessageDocumentId, messageTable.MessageDocumentId));
			list2.Add(messageTable.MessageDocumentId);
			using (InsertOperator insertOperator = Factory.CreateInsertOperator(context.Culture, context, messageTable.Table, Factory.CreateTableOperator(context.Culture, context, messageTable.Table, messageTable.Table.PrimaryKeyIndex, list, null, null, 0, 1, new KeyRange(startStopKey, startStopKey), false, true), list2, null, null, true))
			{
				int num2 = (int)insertOperator.ExecuteScalar();
			}
			if (context.PerfInstance != null)
			{
				context.PerfInstance.MessagesCreatedRate.Increment();
			}
			StorePerClientTypePerformanceCountersInstance perClientPerfInstance = context.Diagnostics.PerClientPerfInstance;
			if (perClientPerfInstance != null)
			{
				perClientPerfInstance.MessagesCreatedRate.Increment();
			}
			TopMessage topMessage = new TopMessage(context, source.Mailbox, destination, true, newMessageDocumentId, source, sourceMessageId);
			foreach (Column column in excludeColumns)
			{
				if (column != messageTable.MessageDocumentId)
				{
					topMessage.SizeChange((long)(-(long)column.Size));
				}
			}
			topMessage.DeepCopySubobjects(context);
			if (topMessage.Subobjects != null)
			{
				topMessage.Subobjects.ClearDeleted(context);
			}
			topMessage.SetColumn(context, topMessage.messageTable.VersionHistory, null);
			topMessage.SetColumn(context, topMessage.messageTable.LcnCurrent, null);
			topMessage.SetColumn(context, topMessage.messageTable.GroupCns, null);
			topMessage.SetColumn(context, topMessage.messageTable.SourceKey, null);
			topMessage.SetColumn(context, topMessage.messageTable.ChangeKey, null);
			topMessage.SetColumn(context, topMessage.messageTable.LastModificationTime, null);
			return topMessage;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0002DCBC File Offset: 0x0002BEBC
		public static TopMessage CreateMessage(Context context, Folder folder)
		{
			if (folder is SearchFolder)
			{
				throw new StoreException((LID)46712U, ErrorCodeValue.SearchFolder);
			}
			return new TopMessage(context, folder.Mailbox, folder, true, null, null, ExchangeId.Null);
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0002DCF0 File Offset: 0x0002BEF0
		public static TopMessage OpenMessage(Context context, Mailbox mailbox, ExchangeId folderId, ExchangeId messageId)
		{
			Folder folder = Folder.OpenFolder(context, mailbox, folderId);
			if (folder == null)
			{
				DiagnosticContext.TraceLocation((LID)34044U);
				return null;
			}
			SearchFolder searchFolder = folder as SearchFolder;
			int? num;
			if (searchFolder != null)
			{
				num = searchFolder.LookupMessageByMid(context, messageId, null);
				folder = null;
			}
			else
			{
				num = TopMessage.GetDocumentIdFromId(context, folder.Mailbox, folderId, messageId);
			}
			if (num == null)
			{
				DiagnosticContext.TraceLocation((LID)50428U);
				return null;
			}
			return TopMessage.OpenMessage(context, (folder != null) ? folder.Mailbox : mailbox, num.Value);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0002DD80 File Offset: 0x0002BF80
		public static TopMessage OpenMessage(Context context, Mailbox mailbox, int documentId)
		{
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(context.Database);
			Mailbox mailbox2 = mailbox;
			if (mailbox.SharedState.UnifiedState != null)
			{
				StartStopKey startStopKey = new StartStopKey(true, new object[]
				{
					mailbox.MailboxPartitionNumber,
					documentId
				});
				Column column = PropertySchema.MapToColumn(context.Database, ObjectType.Message, PropTag.Message.MailboxNum);
				Column[] columnsToFetch = new Column[]
				{
					column
				};
				TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, messageTable.Table, messageTable.MessagePK, columnsToFetch, null, null, null, 0, 0, new KeyRange(startStopKey, startStopKey), false, true, true);
				using (Reader reader = tableOperator.ExecuteReader(true))
				{
					if (!reader.Read())
					{
						return null;
					}
					int @int = reader.GetInt32(column);
					if (mailbox.MailboxNumber != @int)
					{
						mailbox2 = (Mailbox)context.GetMailboxContext(@int);
					}
				}
			}
			TopMessage topMessage = new TopMessage(context, mailbox2, null, false, documentId, null, ExchangeId.Null);
			if (topMessage.IsDead)
			{
				topMessage.Dispose();
				topMessage = null;
			}
			return topMessage;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0002DEAC File Offset: 0x0002C0AC
		public static void PreReadMessages(Context context, Mailbox mailbox, IList<FidMid> fidmidList)
		{
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(mailbox.Database);
			List<KeyRange> list = new List<KeyRange>(fidmidList.Count);
			for (int i = 0; i < fidmidList.Count; i++)
			{
				StartStopKey startStopKey = new StartStopKey(true, new object[]
				{
					mailbox.MailboxPartitionNumber,
					fidmidList[i].FolderId.To26ByteArray(),
					false,
					fidmidList[i].MessageId.To26ByteArray()
				});
				list.Add(new KeyRange(startStopKey, startStopKey));
			}
			using (PreReadOperator preReadOperator = Factory.CreatePreReadOperator(context.Culture, context, messageTable.Table, messageTable.MessageUnique, list, null, true))
			{
				preReadOperator.ExecuteScalar();
				if (TopMessage.messagePrereadTestHook.Value != null)
				{
					TopMessage.messagePrereadTestHook.Value(preReadOperator);
				}
			}
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0002DFB4 File Offset: 0x0002C1B4
		public static bool GetPerUserReadUnreadColumnFunction(Context context, Folder folder, bool baseValue, bool preSavedIsReadValue, ExchangeId changeNumber)
		{
			if (!folder.IsPerUserReadUnreadTrackingEnabled)
			{
				return baseValue;
			}
			if (context.UserIdentity == Guid.Empty)
			{
				return baseValue;
			}
			if (changeNumber.IsNullOrZero)
			{
				return preSavedIsReadValue;
			}
			return TopMessage.GetPerUserRead(context, folder, changeNumber);
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0002DFE8 File Offset: 0x0002C1E8
		public static bool GetPerUserRead(Context context, Folder folder, ExchangeId changeNumber)
		{
			PerUser perUser = PerUser.LoadResident(context, folder.Mailbox, context.UserIdentity, folder.GetId(context));
			return perUser != null && perUser.Contains(folder.Mailbox, changeNumber);
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0002E024 File Offset: 0x0002C224
		public static FunctionColumn CreateVirtualParentDisplayFunctionColumn(MessageTable messageTable, Func<object[], object> function)
		{
			return Factory.CreateFunctionColumn("ParentDisplayName", typeof(string), 0, 255, messageTable.Table, function, "ComputeParentDisplayName", new Column[]
			{
				messageTable.FolderId
			});
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0002E068 File Offset: 0x0002C268
		public SecurityIdentifier GetCreatorSecurityIdentifierInternal(Context context, byte[] sidBuffer)
		{
			return SecurityHelper.CreateSecurityIdentifier(context, sidBuffer);
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0002E071 File Offset: 0x0002C271
		private bool AddToPerUser(Context context, Folder parentFolder, ExchangeId changeNumber)
		{
			return PerUser.InsertInResident(context, base.Mailbox, context.UserIdentity, parentFolder.GetId(context), changeNumber);
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0002E08D File Offset: 0x0002C28D
		private bool RemoveFromPerUser(Context context, Folder parentFolder, ExchangeId changeNumber)
		{
			return PerUser.RemoveFromResident(context, base.Mailbox, context.UserIdentity, parentFolder.GetId(context), changeNumber);
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x0002E0AC File Offset: 0x0002C2AC
		protected static int GetNewMessageDocumentId(Context context, string messageClass, Folder parentFolder)
		{
			bool flag = MessageClassHelper.MatchingMessageClass(messageClass, "IPM.Rule.Message") || MessageClassHelper.MatchingMessageClass(messageClass, "IPM.Rule.Version2.Message") || MessageClassHelper.MatchingMessageClass(messageClass, "IPM.ExtendedRule.Message") || parentFolder.RequiresPerFolderDocumentId(context);
			int result;
			do
			{
				result = (flag ? parentFolder.GetNextMessageDocumentId(context) : parentFolder.Mailbox.GetNextMessageDocumentId(context));
			}
			while (OpenMessageStates.DoesOpenMessageStateExist(context, parentFolder.Mailbox, result));
			return result;
		}

		// Token: 0x060004E8 RID: 1256 RVA: 0x0002E116 File Offset: 0x0002C316
		public int GetDocumentId(Context context)
		{
			return (int)base.GetColumnValue(context, this.messageTable.MessageDocumentId);
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0002E12F File Offset: 0x0002C32F
		public int? GetConversationDocumentId(Context context)
		{
			return (int?)base.GetColumnValue(context, this.messageTable.ConversationDocumentId);
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0002E148 File Offset: 0x0002C348
		public int? GetOriginalConversationDocumentId(Context context)
		{
			return (int?)base.GetOriginalColumnValue(context, this.messageTable.ConversationDocumentId);
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0002E161 File Offset: 0x0002C361
		public ExchangeId GetId(Context context)
		{
			return ExchangeId.CreateFrom26ByteArray(context, base.Mailbox.ReplidGuidMap, (byte[])base.GetColumnValue(context, this.messageTable.MessageId));
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0002E18B File Offset: 0x0002C38B
		public void SetId(Context context, ExchangeId newId)
		{
			base.SetColumn(context, this.messageTable.MessageId, newId.To26ByteArray());
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0002E1A8 File Offset: 0x0002C3A8
		public ExchangeId GetFolderId(Context context)
		{
			if (!this.parentFolder.IsDead)
			{
				return this.parentFolder.GetId(context);
			}
			byte[] bytes = (byte[])base.GetColumnValue(context, this.messageTable.FolderId);
			return ExchangeId.CreateFrom26ByteArray(context, base.Mailbox.ReplidGuidMap, bytes);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0002E1FC File Offset: 0x0002C3FC
		public ExchangeId GetOriginalFolderId(Context context)
		{
			byte[] bytes = (byte[])base.GetOriginalColumnValue(context, this.messageTable.FolderId);
			return ExchangeId.CreateFrom26ByteArray(context, base.Mailbox.ReplidGuidMap, bytes);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0002E235 File Offset: 0x0002C435
		public bool GetOriginalRead(Context context)
		{
			return (bool)base.GetOriginalColumnValue(context, this.messageTable.IsRead);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0002E24E File Offset: 0x0002C44E
		public override HashSet<ushort> GetDefaultPromotedPropertyIds(Context context)
		{
			return base.Mailbox.GetDefaultPromotedMessagePropertyIds(context);
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0002E25C File Offset: 0x0002C45C
		public override HashSet<ushort> GetAlwaysPromotedPropertyIds(Context context)
		{
			return base.Mailbox.GetAlwaysPromotedMessagePropertyIds(context);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0002E26C File Offset: 0x0002C46C
		public override HashSet<ushort> GetAdditionalPromotedPropertyIds(Context context)
		{
			return this.ParentFolder.GetPromotedUniquePropertyIdsForMessages(context, this.GetIsHidden(context));
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0002E28E File Offset: 0x0002C48E
		public bool GetIsHidden(Context context)
		{
			return (bool)this.GetPropertyValue(context, PropTag.Message.Associated);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0002E2A1 File Offset: 0x0002C4A1
		public void SetIsHidden(Context context, bool value)
		{
			this.SetProperty(context, PropTag.Message.Associated, value);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0002E2B8 File Offset: 0x0002C4B8
		public void ResetEverRead(Context context)
		{
			MessageFlags messageFlags = (MessageFlags)((int)this.GetPropertyValue(context, PropTag.Message.MessageFlagsActual));
			this.SetProperty(context, PropTag.Message.MessageFlagsActual, (int)(messageFlags & ~MessageFlags.EverRead));
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0002E2F0 File Offset: 0x0002C4F0
		public ExchangeId GetLcnCurrent(Context context)
		{
			byte[] bytes = (byte[])base.GetColumnValue(context, this.messageTable.LcnCurrent);
			return ExchangeId.CreateFrom26ByteArray(context, base.Mailbox.ReplidGuidMap, bytes);
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0002E328 File Offset: 0x0002C528
		public ExchangeId GetLcnOriginal(Context context)
		{
			byte[] bytes = (byte[])base.GetOriginalColumnValue(context, this.messageTable.LcnCurrent);
			return ExchangeId.CreateFrom26ByteArray(context, base.Mailbox.ReplidGuidMap, bytes);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0002E35F File Offset: 0x0002C55F
		internal void SetLcnCurrent(Context context, ExchangeId value)
		{
			base.SetColumn(context, this.messageTable.LcnCurrent, value.To26ByteArray());
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0002E37C File Offset: 0x0002C57C
		public PropGroupChangeInfo GetPropGroupChangeInfo(Context context)
		{
			byte[] value = (byte[])base.GetColumnValue(context, this.messageTable.GroupCns);
			return PropGroupChangeInfo.Deserialize(context, base.Mailbox.ReplidGuidMap, value);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0002E3B3 File Offset: 0x0002C5B3
		internal void SetPCL(Context context, byte[] value, bool forceDirty)
		{
			if (forceDirty)
			{
				base.SetColumn(context, this.messageTable.VersionHistory, null);
			}
			base.SetColumn(context, this.messageTable.VersionHistory, value);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x0002E3E0 File Offset: 0x0002C5E0
		public ExchangeId GetLcnReadUnread(Context context)
		{
			byte[] bytes = (byte[])base.GetColumnValue(context, this.messageTable.LcnReadUnread);
			return ExchangeId.CreateFrom26ByteArray(context, base.Mailbox.ReplidGuidMap, bytes);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0002E417 File Offset: 0x0002C617
		public void SetLcnReadUnread(Context context, ExchangeId value)
		{
			base.SetColumn(context, this.messageTable.LcnReadUnread, value.To26ByteArray());
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0002E432 File Offset: 0x0002C632
		public DateTime GetLastModificationTime(Context context)
		{
			return (DateTime)this.GetPropertyValue(context, PropTag.Message.LastModificationTime);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0002E445 File Offset: 0x0002C645
		public void SetLastModificationTime(Context context, DateTime value)
		{
			this.SetProperty(context, PropTag.Message.LastModificationTime, value);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0002E45A File Offset: 0x0002C65A
		public int GetStatus(Context context)
		{
			return (int)this.GetPropertyValue(context, PropTag.Message.MsgStatus);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0002E46D File Offset: 0x0002C66D
		public string GetInternetMessageId(Context context)
		{
			return (string)this.GetPropertyValue(context, PropTag.Message.InternetMessageId);
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0002E480 File Offset: 0x0002C680
		public DateTime? GetDateReceived(Context context)
		{
			return (DateTime?)this.GetPropertyValue(context, PropTag.Message.MessageDeliveryTime);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0002E493 File Offset: 0x0002C693
		public void SetDateReceived(Context context, DateTime? value)
		{
			this.SetProperty(context, PropTag.Message.MessageDeliveryTime, value);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0002E4A8 File Offset: 0x0002C6A8
		public void SetDateSent(Context context, DateTime? value)
		{
			this.SetProperty(context, PropTag.Message.ClientSubmitTime, value);
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0002E4BD File Offset: 0x0002C6BD
		public int? GetArticleNumber(Context context)
		{
			return (int?)this.GetPropertyValue(context, PropTag.Message.InternetArticleNumber);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0002E4D0 File Offset: 0x0002C6D0
		internal void SetArticleNumber(Context context, int? value)
		{
			this.SetProperty(context, PropTag.Message.InternetArticleNumber, value);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0002E4E5 File Offset: 0x0002C6E5
		public Sensitivity GetSensitivity(Context context)
		{
			return (Sensitivity)((int)this.GetPropertyValue(context, PropTag.Message.Sensitivity));
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0002E4F8 File Offset: 0x0002C6F8
		public DateTime GetCreationTime(Context context)
		{
			return (DateTime)this.GetPropertyValue(context, PropTag.Message.CreationTime);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0002E50B File Offset: 0x0002C70B
		public Importance GetImportance(Context context)
		{
			return (Importance)((int)this.GetPropertyValue(context, PropTag.Message.Importance));
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0002E51E File Offset: 0x0002C71E
		public Priority GetPriority(Context context)
		{
			return (Priority)((int)this.GetPropertyValue(context, PropTag.Message.Priority));
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0002E534 File Offset: 0x0002C734
		public SecurityIdentifier GetConversationCreatorSecurityIdentifier(Context context)
		{
			byte[] array = (byte[])this.GetPropertyValue(context, PropTag.Message.ConversationCreatorSID);
			if (array == null)
			{
				return null;
			}
			return SecurityHelper.CreateSecurityIdentifier(context, array);
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x0002E560 File Offset: 0x0002C760
		public void SetConversationCreatorSecurityIdentifier(Context context, SecurityIdentifier conversationCreatorSID)
		{
			byte[] array = new byte[conversationCreatorSID.BinaryLength];
			conversationCreatorSID.GetBinaryForm(array, 0);
			this.SetProperty(context, PropTag.Message.ConversationCreatorSID, array);
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0002E590 File Offset: 0x0002C790
		public SecurityIdentifier GetCreatorSecurityIdentifier(Context context)
		{
			byte[] array = (byte[])this.GetPropertyValue(context, PropTag.Message.CreatorSID);
			if (array == null)
			{
				return null;
			}
			return this.GetCreatorSecurityIdentifierInternal(context, array);
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x0002E5BC File Offset: 0x0002C7BC
		public void SetCreatorSecurityIdentifier(Context context, SecurityIdentifier creatorSID)
		{
			byte[] array = new byte[creatorSID.BinaryLength];
			creatorSID.GetBinaryForm(array, 0);
			this.SetProperty(context, PropTag.Message.CreatorSID, array);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0002E5EC File Offset: 0x0002C7EC
		public void Move(Context context, Folder destination)
		{
			if (!destination.CheckAlive(context))
			{
				throw new StoreException((LID)38520U, ErrorCodeValue.ObjectDeleted);
			}
			if (destination is SearchFolder)
			{
				throw new StoreException((LID)55928U, ErrorCodeValue.SearchFolder);
			}
			base.SetColumn(context, this.messageTable.FolderId, destination.GetId(context).To26ByteArray());
			this.parentFolder = destination;
			base.SetColumn(context, this.messageTable.IMAPId, null);
			this.SetArticleNumber(context, null);
			base.SetColumn(context, this.messageTable.LcnReadUnread, null);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x0002E692 File Offset: 0x0002C892
		public override IChunked PrepareToSaveChanges(Context context)
		{
			return null;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0002E695 File Offset: 0x0002C895
		public override bool SaveChanges(Context context)
		{
			return this.SaveChanges(context, SaveMessageChangesFlags.None);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0002E6A0 File Offset: 0x0002C8A0
		public bool SaveChanges(Context context, SaveMessageChangesFlags flags)
		{
			if (base.IsDead)
			{
				throw new StoreException((LID)34424U, ErrorCodeValue.ObjectDeleted);
			}
			if (!this.parentFolder.CheckAlive(context))
			{
				throw new StoreException((LID)42616U, ErrorCodeValue.ObjectDeleted);
			}
			if (this.originalFolder != null && this.originalFolder != this.parentFolder && !this.originalFolder.CheckAlive(context))
			{
				throw new StoreException((LID)32700U, ErrorCodeValue.ObjectDeleted);
			}
			if (context.PrimaryMailboxContext != null && context.PrimaryMailboxContext.MailboxNumber != (int)this.GetPropertyValue(context, PropTag.Message.MailboxNum))
			{
				throw new StoreException((LID)35356U, ErrorCodeValue.NotSupported);
			}
			bool flag = this.openMessageInstanceState != null && !this.openMessageInstanceState.Current;
			if (flag)
			{
				if (this.openMessageInstanceState.Deleted || this.openMessageInstanceState.Moved)
				{
					throw new StoreException((LID)47736U, ErrorCodeValue.NotFound);
				}
				return false;
			}
			else
			{
				if ((byte)(flags & SaveMessageChangesFlags.SkipQuotaCheck) != 12)
				{
					this.EnforceQuota(context, (byte)(flags & SaveMessageChangesFlags.SkipFolderQuotaCheck) == 8, (byte)(flags & SaveMessageChangesFlags.SkipMailboxQuotaCheck) == 4);
				}
				else if (ExTraceGlobals.QuotaTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.QuotaTracer.TraceDebug(29968L, "Quota check specifically skipped for this save");
				}
				bool flag2 = (byte)(flags & SaveMessageChangesFlags.ForceSave) == 2;
				if (!this.IsDirty && !flag2)
				{
					return true;
				}
				base.UpdateRecipients(context);
				base.UpdateSubobjects(context);
				using (context.CriticalBlock((LID)47072U, CriticalBlockScope.MailboxShared))
				{
					this.forceCreatedEventForCopy = (0 != (byte)(flags & SaveMessageChangesFlags.ForceCreatedEventForCopy));
					this.timerEventFired = (0 != (byte)(flags & SaveMessageChangesFlags.TimerEventFired));
					bool flag3 = flag2 || base.IsNew || !this.IsNonConflictingChange();
					ExchangeId exchangeId = ExchangeId.Null;
					bool flag4 = false;
					ExchangeId lcnCurrent = this.GetLcnCurrent(context);
					bool isPerUserReadUnreadTrackingEnabled = this.parentFolder.IsPerUserReadUnreadTrackingEnabled;
					if (!this.IsConversation && ((this.DataRow.ColumnDirty(this.messageTable.IsRead) && !isPerUserReadUnreadTrackingEnabled && !this.DataRow.ColumnDirty(this.messageTable.LcnReadUnread)) || base.GetColumnValue(context, this.messageTable.LcnReadUnread) == null))
					{
						exchangeId = this.parentFolder.GetNextMessageCn(context);
						this.SetLcnReadUnread(context, exchangeId);
					}
					if (flag3)
					{
						flag4 = this.DataRow.ColumnDirty(this.messageTable.LcnCurrent);
						if (!flag4 || base.GetColumnValue(context, this.messageTable.LcnCurrent) == null)
						{
							if (exchangeId.IsNull)
							{
								exchangeId = this.parentFolder.GetNextMessageCn(context);
							}
							this.SetLcnCurrent(context, exchangeId);
						}
						if (!this.DataRow.ColumnDirty(this.messageTable.ChangeKey))
						{
							base.SetColumn(context, this.messageTable.ChangeKey, null);
						}
						if (!this.DataRow.ColumnDirty(this.messageTable.VersionHistory) || base.GetColumnValue(context, this.messageTable.VersionHistory) == null)
						{
							PCL pcl = default(PCL);
							byte[] array = (byte[])base.GetColumnValue(context, this.messageTable.VersionHistory);
							if (array != null)
							{
								pcl.LoadBinaryLXCN(array);
							}
							pcl.Add(this.GetLcnCurrent(context));
							base.SetColumn(context, this.messageTable.VersionHistory, pcl.DumpBinaryLXCN());
						}
						if (base.IsNew)
						{
							if (base.GetColumnValue(context, this.messageTable.MessageId) == null)
							{
								ExchangeId nextMessageId = this.parentFolder.GetNextMessageId(context);
								base.SetColumn(context, this.messageTable.MessageId, nextMessageId.To26ByteArray());
							}
							ExchangeId id = this.parentFolder.GetId(context);
							base.SetColumn(context, this.messageTable.FolderId, id.To26ByteArray());
						}
						else if (this.parentFolder != this.originalFolder)
						{
							if (!this.DataRow.ColumnDirty(this.messageTable.MessageId) || base.GetColumnValue(context, this.messageTable.MessageId) == null)
							{
								ExchangeId nextMessageId2 = this.parentFolder.GetNextMessageId(context);
								base.SetColumn(context, this.messageTable.MessageId, nextMessageId2.To26ByteArray());
							}
							if (!this.DataRow.ColumnDirty(this.messageTable.SourceKey))
							{
								base.SetColumn(context, this.messageTable.SourceKey, null);
							}
						}
						if ((byte)(flags & SaveMessageChangesFlags.IMAPIDChange) != 0 || !this.DataRow.ColumnDirty(this.messageTable.ArticleNumber) || base.GetColumnValue(context, this.messageTable.ArticleNumber) == null)
						{
							base.SetColumn(context, this.messageTable.ArticleNumber, this.parentFolder.GetNextArticleNumber(context));
						}
						if ((byte)(flags & SaveMessageChangesFlags.IMAPIDChange) != 0 || base.GetColumnValue(context, this.messageTable.IMAPId) == null)
						{
							base.SetColumn(context, this.messageTable.IMAPId, this.GetArticleNumber(context));
						}
						DateTime utcNow = base.Mailbox.UtcNow;
						if (!this.DataRow.ColumnDirty(this.messageTable.LastModificationTime) || base.GetColumnValue(context, this.messageTable.LastModificationTime) == null)
						{
							base.SetColumn(context, this.messageTable.LastModificationTime, utcNow);
						}
					}
					if (base.IsNew && base.GetColumnValue(context, this.messageTable.MessageDocumentId) == null)
					{
						int newMessageDocumentId = TopMessage.GetNewMessageDocumentId(context, base.GetMessageClass(context), this.ParentFolder);
						base.SetColumn(context, this.messageTable.MessageDocumentId, newMessageDocumentId);
					}
					using (ConversationItem conversationItem = Conversations.OpenOrCreateConversation(context, base.Mailbox, this))
					{
						base.SetColumn(context, this.messageTable.ConversationDocumentId, (conversationItem == null) ? null : conversationItem.GetDocumentId(context));
						this.conversationItem = conversationItem;
						try
						{
							if (!base.IsNew && this.Schema.ColumnGroups != null && !this.DataRow.ColumnDirty(this.messageTable.GroupCns))
							{
								if (flag4)
								{
									this.changedGroups = ulong.MaxValue;
									exchangeId = this.GetLcnCurrent(context);
								}
								else
								{
									this.changedGroups = this.GetChangedPropertyGroups(context);
								}
								if (0UL != (this.changedGroups & 9223372036871553023UL))
								{
									if (exchangeId.IsNull)
									{
										exchangeId = this.parentFolder.GetNextMessageCn(context);
									}
									if (isPerUserReadUnreadTrackingEnabled)
									{
										PropertyMapping propertyMapping = this.Schema.FindMapping(PropTag.Message.MessageFlags);
										if (propertyMapping != null)
										{
											this.changedGroups |= propertyMapping.PropertyTag.GroupMask;
										}
									}
									byte[] value;
									if ((this.changedGroups & 9223372036871553023UL) == 9223372036871553023UL)
									{
										value = null;
									}
									else
									{
										byte[] value2 = (byte[])base.GetColumnValue(context, this.messageTable.GroupCns);
										PropGroupChangeInfo propGroupChangeInfo = PropGroupChangeInfo.Deserialize(context, base.Mailbox.ReplidGuidMap, value2);
										if (!propGroupChangeInfo.IsValid)
										{
											propGroupChangeInfo = new PropGroupChangeInfo(lcnCurrent);
										}
										if (0UL != (this.changedGroups & 9223372036854775808UL))
										{
											propGroupChangeInfo.Other = exchangeId;
										}
										for (int i = 0; i < 24; i++)
										{
											if (0UL != (this.changedGroups & MessagePropGroups.NumberedGroupMasks[i]))
											{
												propGroupChangeInfo[i] = exchangeId;
											}
										}
										value = propGroupChangeInfo.Serialize();
									}
									base.SetColumn(context, this.messageTable.GroupCns, value);
								}
							}
							bool flag5 = base.IsNew && (this.originalFolder == null || this.originalMessageID.IsNull);
							bool nonFatalDuplicateKey = context.GetConnection().NonFatalDuplicateKey;
							if (this.originalFolder == null)
							{
								this.originalFolder = this.parentFolder;
							}
							TimerEventHandler.CheckAndRaiseDeferredEventTimer(context, this);
							try
							{
								if (base.IsNew && (byte)(flags & SaveMessageChangesFlags.NonFatalDuplicateKey) != 0)
								{
									context.GetConnection().NonFatalDuplicateKey = true;
								}
								base.SaveChanges(context);
							}
							finally
							{
								context.GetConnection().NonFatalDuplicateKey = nonFatalDuplicateKey;
							}
							if (base.Subobjects != null)
							{
								base.Subobjects.TombstoneDeleted(context);
								base.Subobjects.ClearDeleted(context);
								base.Subobjects.CommitAll(context);
							}
							this.originalAttachCount = base.AttachCount;
							this.originalFolder = this.parentFolder;
							this.originalMessageID = this.GetId(context);
							if (flag5)
							{
								if (context.PerfInstance != null)
								{
									context.PerfInstance.MessagesCreatedRate.Increment();
								}
								StorePerClientTypePerformanceCountersInstance perClientPerfInstance = context.Diagnostics.PerClientPerfInstance;
								if (perClientPerfInstance != null)
								{
									perClientPerfInstance.MessagesCreatedRate.Increment();
								}
							}
							else
							{
								if (context.PerfInstance != null)
								{
									context.PerfInstance.MessagesUpdatedRate.Increment();
								}
								StorePerClientTypePerformanceCountersInstance perClientPerfInstance2 = context.Diagnostics.PerClientPerfInstance;
								if (perClientPerfInstance2 != null)
								{
									perClientPerfInstance2.MessagesUpdatedRate.Increment();
								}
							}
						}
						finally
						{
							this.conversationItem = null;
						}
					}
					context.EndCriticalBlock();
				}
				return true;
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x0002EFA4 File Offset: 0x0002D1A4
		public override void Delete(Context context)
		{
			if (base.IsDead)
			{
				return;
			}
			if (context.PrimaryMailboxContext != null && context.PrimaryMailboxContext.MailboxNumber != (int)this.GetPropertyValue(context, PropTag.Message.MailboxNum))
			{
				throw new StoreException((LID)45596U, ErrorCodeValue.NotSupported);
			}
			if (base.IsNew)
			{
				if (this.originalFolder == null || !this.originalMessageID.IsValid)
				{
					base.MarkAsDeleted(context);
					return;
				}
				this.Delete(context, false);
				base.ResetIsNew();
				if (base.Subobjects != null)
				{
					base.Subobjects.TombstoneAll(context);
					base.Subobjects.TombstoneDeleted(context);
				}
				if (context.PerfInstance != null)
				{
					context.PerfInstance.MessagesDeletedRate.Increment();
				}
				StorePerClientTypePerformanceCountersInstance perClientPerfInstance = context.Diagnostics.PerClientPerfInstance;
				if (perClientPerfInstance != null)
				{
					perClientPerfInstance.MessagesDeletedRate.Increment();
				}
				base.ReleaseDescendants(context, false);
				return;
			}
			else
			{
				if (!this.originalFolder.CheckAlive(context))
				{
					throw new StoreException((LID)64120U, ErrorCodeValue.ObjectDeleted);
				}
				bool flag = this.openMessageInstanceState != null && !this.openMessageInstanceState.Current;
				if (flag)
				{
					if (this.openMessageInstanceState.Moved)
					{
						throw new StoreException((LID)39544U, ErrorCodeValue.NotFound);
					}
					bool deleted = this.openMessageInstanceState.Deleted;
					base.MarkAsDeleted(context);
					OpenMessageStates.RemoveInstance(context, base.Mailbox, this.openMessageInstanceState);
					this.openMessageInstanceState = null;
					if (deleted)
					{
						goto IL_27C;
					}
					using (TopMessage topMessage = TopMessage.OpenMessage(context, base.Mailbox, this.originalFolder.GetId(context), this.originalMessageID))
					{
						topMessage.Delete(context);
						goto IL_27C;
					}
				}
				if (this.parentFolder.IsPerUserReadUnreadTrackingEnabled)
				{
					base.SaveColumnPreImage(this.messageTable.IsRead, base.GetIsRead(context));
				}
				this.deleteChangeNumber = this.parentFolder.GetNextMessageCn(context);
				base.SetColumn(context, this.messageTable.LcnCurrent, this.deleteChangeNumber.To26ByteArray());
				base.SetColumn(context, this.messageTable.LastModificationTime, base.Mailbox.UtcNow);
				base.Delete(context);
				if (base.Subobjects != null)
				{
					base.Subobjects.TombstoneAll(context);
					base.Subobjects.TombstoneDeleted(context);
				}
				if (context.PerfInstance != null)
				{
					context.PerfInstance.MessagesDeletedRate.Increment();
				}
				StorePerClientTypePerformanceCountersInstance perClientPerfInstance2 = context.Diagnostics.PerClientPerfInstance;
				if (perClientPerfInstance2 != null)
				{
					perClientPerfInstance2.MessagesDeletedRate.Increment();
				}
				IL_27C:
				base.ReleaseDescendants(context, false);
				return;
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x0002F248 File Offset: 0x0002D448
		protected override bool RecoverFromDuplicateKey(Context context)
		{
			return false;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x0002F24C File Offset: 0x0002D44C
		protected override bool IsValidDataRow(Context context, DataRow dataRow)
		{
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(context.Database);
			return dataRow.GetValue(context, messageTable.FolderId) != null;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0002F278 File Offset: 0x0002D478
		private void EnforceQuota(Context context, bool skipFolderQuotaCheck, bool skipMailboxQuotaCheck)
		{
			bool flag = base.CurrentSize > base.OriginalSize;
			bool flag2 = this.originalFolder != null && this.originalFolder != this.parentFolder;
			bool flag3 = flag2 && this.originalFolder.IsDumpsterMarkedFolder(context) != this.parentFolder.IsDumpsterMarkedFolder(context);
			if (ExTraceGlobals.QuotaTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.QuotaTracer.TraceDebug<bool, bool, bool>(29940L, "Quota check: isGrowing={0}, isMoving={1}, isDumpsterTransition={2}", flag, flag2, flag3);
			}
			if (!skipFolderQuotaCheck)
			{
				if (flag || flag2)
				{
					if (ExTraceGlobals.QuotaTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.QuotaTracer.TraceDebug(29944L, "folder-scoped quota enforcement...");
					}
					Quota.Enforce((LID)38156U, context, this.parentFolder, QuotaType.StorageOverQuotaLimit, false);
				}
				else if (ExTraceGlobals.QuotaTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.QuotaTracer.TraceDebug(29984L, "item isn't growing or moving, so no folder-scoped quota check for this save");
				}
			}
			else if (ExTraceGlobals.QuotaTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.QuotaTracer.TraceDebug(29952L, "folder-scoped quota check specifically skipped for this save");
			}
			if (!skipMailboxQuotaCheck)
			{
				if (flag || flag3)
				{
					QuotaType quotaType = this.parentFolder.IsDumpsterMarkedFolder(context) ? QuotaType.DumpsterShutoff : QuotaType.StorageShutoff;
					if (ExTraceGlobals.QuotaTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.QuotaTracer.TraceDebug(29956L, "mailbox-scoped quota enforcement...");
					}
					Quota.Enforce((LID)54540U, context, base.Mailbox, quotaType, false);
					return;
				}
				if (ExTraceGlobals.QuotaTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.QuotaTracer.TraceDebug(29988L, "item isn't growing or transitioning dumpster status, so no mailbox-scoped quota check for this save");
					return;
				}
			}
			else if (ExTraceGlobals.QuotaTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.QuotaTracer.TraceDebug(29960L, "mailbox-scoped quota check specifically skipped for this save");
			}
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0002F424 File Offset: 0x0002D624
		private object GetParentDisplayColumnFunction(object[] columnValues)
		{
			Context currentOperationContext = base.Mailbox.CurrentOperationContext;
			ExchangeId id = ExchangeId.CreateFrom26ByteArray(currentOperationContext, base.Mailbox.ReplidGuidMap, (byte[])columnValues[0]);
			if (id.IsValid)
			{
				Folder folder = Folder.OpenFolder(currentOperationContext, base.Mailbox, id);
				if (folder != null)
				{
					return folder.GetName(currentOperationContext);
				}
			}
			else if (id.IsNull)
			{
				return this.ParentFolder.GetName(currentOperationContext);
			}
			return null;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0002F494 File Offset: 0x0002D694
		protected override IReadOnlyDictionary<Column, Column> GetColumnRenames(Context context)
		{
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(context.Database);
			Dictionary<Column, Column> dictionary = new Dictionary<Column, Column>(3);
			dictionary[messageTable.VirtualIsRead] = Factory.CreateFunctionColumn("PerUserIsRead", typeof(bool), PropertyTypeHelper.SizeFromPropType(PropertyType.Boolean), PropertyTypeHelper.MaxLengthFromPropType(PropertyType.Boolean), messageTable.Table, new Func<object[], object>(this.GetPerUserReadUnreadColumnFunction), "ComputePerUserIsRead", new Column[]
			{
				messageTable.IsRead,
				messageTable.LcnCurrent
			});
			dictionary[messageTable.VirtualUnreadMessageCount] = PropertySchema.MapToColumn(context.Database, ObjectType.Message, PropTag.Message.UnreadCountInt64);
			dictionary[messageTable.VirtualParentDisplay] = TopMessage.CreateVirtualParentDisplayFunctionColumn(messageTable, new Func<object[], object>(this.GetParentDisplayColumnFunction));
			return dictionary;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0002F550 File Offset: 0x0002D750
		private object GetPerUserReadUnreadColumnFunction(object[] columnValues)
		{
			Context currentOperationContext = base.Mailbox.CurrentOperationContext;
			Folder folder = this.ParentFolder;
			ExchangeId changeNumber = ExchangeId.CreateFrom26ByteArray(currentOperationContext, base.Mailbox.ReplidGuidMap, (byte[])columnValues[1]);
			return TopMessage.GetPerUserReadUnreadColumnFunction(currentOperationContext, folder, (bool)columnValues[0], this.isReadTemp, changeNumber);
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0002F5A8 File Offset: 0x0002D7A8
		protected internal static int? GetDocumentIdFromId(Context context, Mailbox mailbox, ExchangeId folderId, ExchangeId messageId)
		{
			int? result = null;
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(mailbox.Database);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				mailbox.MailboxPartitionNumber,
				folderId.To26ByteArray(),
				false,
				messageId.To26ByteArray()
			});
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, messageTable.Table, messageTable.MessageUnique, new Column[]
			{
				messageTable.MessageDocumentId
			}, null, null, null, 0, 1, new KeyRange(startStopKey, startStopKey), false, false, true))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					if (reader.Read())
					{
						result = new int?(reader.GetInt32(messageTable.MessageDocumentId));
					}
				}
			}
			if (result == null)
			{
				startStopKey = new StartStopKey(true, new object[]
				{
					mailbox.MailboxPartitionNumber,
					folderId.To26ByteArray(),
					true,
					messageId.To26ByteArray()
				});
				using (TableOperator tableOperator2 = Factory.CreateTableOperator(context.Culture, context, messageTable.Table, messageTable.MessageUnique, new Column[]
				{
					messageTable.MessageDocumentId
				}, null, null, null, 0, 1, new KeyRange(startStopKey, startStopKey), false, false, true))
				{
					using (Reader reader2 = tableOperator2.ExecuteReader(false))
					{
						if (reader2.Read())
						{
							result = new int?(reader2.GetInt32(messageTable.MessageDocumentId));
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0002F788 File Offset: 0x0002D988
		protected internal static string GetMessageClassByDocumentId(Context context, Mailbox mailbox, int documentId)
		{
			MessageTable messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(mailbox.Database);
			StartStopKey startStopKey = new StartStopKey(true, new object[]
			{
				mailbox.MailboxPartitionNumber,
				documentId
			});
			using (TableOperator tableOperator = Factory.CreateTableOperator(context.Culture, context, messageTable.Table, messageTable.MessagePK, new Column[]
			{
				messageTable.MessageClass
			}, null, null, 0, 1, new KeyRange(startStopKey, startStopKey), false, true))
			{
				using (Reader reader = tableOperator.ExecuteReader(false))
				{
					if (reader.Read())
					{
						return reader.GetString(messageTable.MessageClass);
					}
				}
			}
			return null;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0002F860 File Offset: 0x0002DA60
		public override void Scrub(Context context)
		{
			base.Scrub(context);
			base.SetColumn(context, this.messageTable.SubjectPrefix, null);
			base.SetColumn(context, this.messageTable.ConversationIndex, null);
			base.SetColumn(context, this.messageTable.DateReceived, null);
			base.SetColumn(context, this.messageTable.DateSent, null);
			base.SetColumn(context, this.messageTable.BodyType, null);
			base.SetColumn(context, this.messageTable.NativeBody, null);
			base.SetColumn(context, this.messageTable.ArticleNumber, null);
			base.SetColumn(context, this.messageTable.IMAPId, null);
			base.SetColumn(context, this.messageTable.LcnCurrent, null);
			base.SetColumn(context, this.messageTable.VersionHistory, null);
			base.SetColumn(context, this.messageTable.LastModificationTime, null);
			base.SetColumn(context, this.messageTable.MessageClass, null);
			PropertySchemaPopulation.InitializeMessage(context, this);
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0002F960 File Offset: 0x0002DB60
		public void DirtyId(Context context)
		{
			base.DirtyColumn(context, this.messageTable.MessageId);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0002F974 File Offset: 0x0002DB74
		protected override ObjectType GetObjectType()
		{
			return ObjectType.Message;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0002F978 File Offset: 0x0002DB78
		public PerUser GetPerUserInstance(Context context, Folder parentFolder)
		{
			if (context.UserIdentity == Guid.Empty)
			{
				return null;
			}
			ExchangeId id = parentFolder.GetId(context);
			PerUser perUser = PerUser.LoadResident(context, base.Mailbox, context.UserIdentity, id);
			if (perUser == null)
			{
				perUser = PerUser.CreateResident(context, base.Mailbox, context.UserIdentity, id, new IdSet());
			}
			return perUser;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0002F9D4 File Offset: 0x0002DBD4
		public bool AddToPerUser(Context context, bool postUpdateNotification)
		{
			if (context.UserIdentity == Guid.Empty)
			{
				return false;
			}
			ExchangeId lcnCurrent = this.GetLcnCurrent(context);
			bool flag;
			if (lcnCurrent.IsNullOrZero)
			{
				flag = !this.isReadTemp;
				this.isReadTemp = true;
			}
			else
			{
				flag = this.AddToPerUser(context, this.parentFolder, lcnCurrent);
				if (flag)
				{
					base.SaveColumnPreImage(this.messageTable.IsRead, false);
					if (postUpdateNotification)
					{
						this.TrackUpdate(context, LogicalIndex.LogicalOperation.Update, new Guid?(context.UserIdentity));
					}
				}
			}
			return flag;
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0002FA5C File Offset: 0x0002DC5C
		public bool RemoveFromPerUser(Context context, bool postUpdateNotification)
		{
			if (context.UserIdentity == Guid.Empty)
			{
				return false;
			}
			ExchangeId lcnCurrent = this.GetLcnCurrent(context);
			bool flag;
			if (lcnCurrent.IsNullOrZero)
			{
				flag = this.isReadTemp;
				this.isReadTemp = false;
			}
			else
			{
				flag = this.RemoveFromPerUser(context, this.parentFolder, lcnCurrent);
				if (flag)
				{
					base.SaveColumnPreImage(this.messageTable.IsRead, true);
					if (postUpdateNotification)
					{
						this.TrackUpdate(context, LogicalIndex.LogicalOperation.Update, new Guid?(context.UserIdentity));
					}
				}
			}
			return flag;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0002FAE0 File Offset: 0x0002DCE0
		private void InitExistingMessage(Context context, int docid)
		{
			if (!base.IsDead)
			{
				if (base.Mailbox.SharedState.UnifiedState != null && !this.IsConversation)
				{
					int num = (int)this.GetPropertyValue(context, PropTag.Message.MailboxNum);
					Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(num == base.Mailbox.MailboxNumber, "Wrong mailbox object is used to open message");
				}
				if (this.parentFolder == null)
				{
					byte[] bytes = (byte[])base.GetColumnValue(context, this.messageTable.FolderId);
					ExchangeId id = ExchangeId.CreateFrom26ByteArray(context, base.Mailbox.ReplidGuidMap, bytes);
					if (id.IsNullOrZero)
					{
						base.Dispose();
						throw new StoreException((LID)61048U, ErrorCodeValue.NotFound);
					}
					this.parentFolder = Folder.OpenFolder(context, base.Mailbox, id);
					if (this.parentFolder == null)
					{
						base.Dispose();
						throw new StoreException((LID)36472U, ErrorCodeValue.NotFound);
					}
				}
				this.originalFolder = this.parentFolder;
				this.originalMessageID = this.GetId(context);
				this.originalAttachCount = base.AttachCount;
				this.documentId = docid;
				if (!this.IsConversation)
				{
					this.openMessageInstanceState = OpenMessageStates.AddInstance(context, base.Mailbox, this.documentId, this.DataRow);
				}
				if (context.PerfInstance != null)
				{
					context.PerfInstance.MessagesOpenedRate.Increment();
				}
				StorePerClientTypePerformanceCountersInstance perClientPerfInstance = context.Diagnostics.PerClientPerfInstance;
				if (perClientPerfInstance != null)
				{
					perClientPerfInstance.MessagesOpenedRate.Increment();
				}
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0002FC54 File Offset: 0x0002DE54
		private void TrackUpdate(Context context, LogicalIndex.LogicalOperation operation, Guid? userIdentityContext)
		{
			ObjectNotificationEvent notificationEvent = null;
			Folder folder = null;
			bool flag = false;
			switch (operation)
			{
			case LogicalIndex.LogicalOperation.Insert:
				folder = this.parentFolder;
				folder.ItemInserted(context, this);
				if (!this.forceCreatedEventForCopy && this.originalFolder != null && this.originalMessageID.IsValid)
				{
					notificationEvent = NotificationEvents.CreateMessageCopiedEvent(context, this, this.originalFolder, this.originalMessageID);
				}
				else
				{
					notificationEvent = NotificationEvents.CreateMessageCreatedEvent(context, this);
				}
				break;
			case LogicalIndex.LogicalOperation.Update:
				folder = this.parentFolder;
				flag = (this.parentFolder != this.originalFolder);
				if (flag || this.GetId(context) != this.originalMessageID)
				{
					this.originalFolder.ItemDeleted(context, this);
					this.parentFolder.ItemInserted(context, this);
					notificationEvent = NotificationEvents.CreateMessageMovedEvent(context, this, this.originalFolder);
				}
				else
				{
					this.parentFolder.ItemUpdated(context, this);
					notificationEvent = NotificationEvents.CreateMessageModifiedEvent(context, this, userIdentityContext);
				}
				break;
			case LogicalIndex.LogicalOperation.Delete:
				folder = this.originalFolder;
				folder.ItemDeleted(context, this);
				notificationEvent = NotificationEvents.CreateMessageDeletedEvent(context, this);
				break;
			}
			this.PossiblyRiseNotificationEvent(context, notificationEvent);
			ModifiedSearchFolders modifiedSearchFolders = new ModifiedSearchFolders();
			if (flag)
			{
				SearchFolder.TrackSearchFolderUpdate(context, this.originalFolder, this, LogicalIndex.LogicalOperation.Delete, userIdentityContext, modifiedSearchFolders);
				SearchFolder.TrackSearchFolderUpdate(context, this.parentFolder, this, LogicalIndex.LogicalOperation.Insert, userIdentityContext, modifiedSearchFolders);
				LogicalIndexCache.TrackIndexUpdate(context, base.Mailbox, this.originalFolder.GetId(context), LogicalIndexType.Messages, LogicalIndex.LogicalOperation.Delete, this);
				LogicalIndexCache.TrackIndexUpdate(context, base.Mailbox, this.parentFolder.GetId(context), LogicalIndexType.Messages, LogicalIndex.LogicalOperation.Insert, this);
			}
			else
			{
				SearchFolder.TrackSearchFolderUpdate(context, folder, this, operation, userIdentityContext, modifiedSearchFolders);
				using (new Context.UserLockCheckFrame(context, Context.UserLockCheckFrame.Scope.LogicalIndex, userIdentityContext, base.Mailbox.SharedState))
				{
					if (userIdentityContext != null)
					{
						base.ForgetColumnPreImage(this.messageTable.IsRead);
					}
					else
					{
						LogicalIndexCache.TrackIndexUpdate(context, base.Mailbox, folder.GetId(context), LogicalIndexType.Messages, operation, this);
					}
				}
			}
			if (userIdentityContext == null)
			{
				Conversations.TrackConversationUpdate(context, base.Mailbox, this, operation, modifiedSearchFolders);
			}
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0002FE54 File Offset: 0x0002E054
		private bool IsNonConflictingChange()
		{
			if (this.IsDirtyExceptDataRow)
			{
				return false;
			}
			for (int i = 0; i < this.messageTable.Table.Columns.Count; i++)
			{
				PhysicalColumn physicalColumn = this.messageTable.Table.Columns[i];
				if (this.DataRow.ColumnDirty(physicalColumn) && physicalColumn != this.messageTable.IsRead && physicalColumn != this.messageTable.MessageFlagsActual && physicalColumn != this.messageTable.MailFlags && physicalColumn != this.messageTable.Status && physicalColumn != this.messageTable.LcnReadUnread && physicalColumn != this.messageTable.VersionHistory && physicalColumn != this.messageTable.GroupCns)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0002FF44 File Offset: 0x0002E144
		protected override void OnBeforeDataRowFlushOrDelete(Context context, bool delete)
		{
			bool move = false;
			if (!base.IsNew)
			{
				if (delete)
				{
					this.TrackUpdate(context, LogicalIndex.LogicalOperation.Delete, null);
				}
				else
				{
					move = (this.parentFolder != this.originalFolder || this.GetId(context) != this.originalMessageID);
				}
			}
			if (!this.IsConversation)
			{
				if (this.originalFolder.IsPerUserReadUnreadTrackingEnabled && context.UserIdentity != Guid.Empty)
				{
					PerUser perUserInstance = this.GetPerUserInstance(context, this.originalFolder);
					ExchangeId lcnOriginal = this.GetLcnOriginal(context);
					ExchangeId lcnCurrent = this.GetLcnCurrent(context);
					if (!base.IsNew)
					{
						this.isReadTemp = (perUserInstance.Contains(base.Mailbox, lcnCurrent) || perUserInstance.Contains(base.Mailbox, lcnOriginal));
					}
					this.SetProperty(context, PropTag.Message.IsReadColumn, this.isReadTemp);
					if (delete)
					{
						this.RemoveFromPerUser(context, this.parentFolder, lcnOriginal);
						this.RemoveFromPerUser(context, this.parentFolder, lcnCurrent);
					}
				}
				else
				{
					this.isReadTemp = (bool)this.GetPropertyValue(context, PropTag.Message.IsReadColumn);
				}
			}
			if (!base.IsNew && this.openMessageInstanceState != null)
			{
				OpenMessageStates.OnBeforeFlushOrDelete(context, this.openMessageInstanceState, delete, move, this.IsNonConflictingChange());
			}
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00030088 File Offset: 0x0002E288
		protected override void OnAfterDataRowFlushOrDelete(Context context, bool delete)
		{
			if (delete)
			{
				if (this.openMessageInstanceState != null)
				{
					OpenMessageStates.RemoveInstance(context, base.Mailbox, this.openMessageInstanceState);
					this.openMessageInstanceState = null;
					return;
				}
			}
			else
			{
				if (!this.IsConversation)
				{
					if (this.originalFolder != this.parentFolder && this.originalFolder.IsPerUserReadUnreadTrackingEnabled && context.UserIdentity != Guid.Empty)
					{
						this.RemoveFromPerUser(context, this.originalFolder, this.GetLcnOriginal(context));
						this.RemoveFromPerUser(context, this.originalFolder, this.GetLcnCurrent(context));
					}
					if (this.parentFolder.IsPerUserReadUnreadTrackingEnabled && context.UserIdentity != Guid.Empty)
					{
						if (this.isReadTemp)
						{
							this.AddToPerUser(context, this.parentFolder, this.GetLcnCurrent(context));
						}
						else
						{
							this.RemoveFromPerUser(context, this.parentFolder, this.GetLcnCurrent(context));
						}
					}
				}
				if (base.IsNew)
				{
					this.documentId = this.GetDocumentId(context);
					if (!this.IsConversation)
					{
						this.openMessageInstanceState = OpenMessageStates.AddInstance(context, base.Mailbox, this.documentId, this.DataRow);
					}
					this.TrackUpdate(context, LogicalIndex.LogicalOperation.Insert, null);
					return;
				}
				this.TrackUpdate(context, LogicalIndex.LogicalOperation.Update, null);
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000301D0 File Offset: 0x0002E3D0
		protected override void DataRowDeletionImplementation(Context context)
		{
			if (!AsyncMessageCleanup.IsReady(context, context.Database))
			{
				base.DataRowDeletionImplementation(context);
				return;
			}
			if (this.DataRow.IsNew)
			{
				return;
			}
			this.DataRow.SetValue(context, this.messageTable.FolderId, null);
			this.DataRow.SetValue(context, this.messageTable.ConversationId, null);
			this.DataRow.SetValue(context, this.messageTable.ConversationDocumentId, null);
			this.DataRow.SetValue(context, this.messageTable.MessageId, this.parentFolder.GetNextMessageId(context).To26ByteArray());
			if (this.DataRow.GetValue(context, this.messageTable.LcnCurrent) == null)
			{
				this.DataRow.SetValue(context, this.messageTable.LcnCurrent, ExchangeId.Zero.To26ByteArray());
			}
			if (this.DataRow.GetValue(context, this.messageTable.VersionHistory) == null)
			{
				this.DataRow.SetValue(context, this.messageTable.VersionHistory, Array<byte>.Empty);
			}
			if (this.DataRow.GetValue(context, this.messageTable.LastModificationTime) == null)
			{
				this.DataRow.SetValue(context, this.messageTable.LastModificationTime, base.Mailbox.UtcNow);
			}
			this.DataRow.SetValue(context, this.messageTable.IMAPId, null);
			this.DataRow.SetValue(context, this.messageTable.ArticleNumber, null);
			this.DataRow.Flush(context);
			SubobjectCleanup.AddMessageToTombstone(context, this);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0003036C File Offset: 0x0002E56C
		protected virtual void PossiblyRiseNotificationEvent(Context context, NotificationEvent notificationEvent)
		{
			context.RiseNotificationEvent(notificationEvent);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00030378 File Offset: 0x0002E578
		protected override void OnColumnChanged(Column column, long deltaSize)
		{
			MessageTable messageTable = this.messageTable;
			if (messageTable == null)
			{
				messageTable = Microsoft.Exchange.Server.Storage.LogicalDataModel.DatabaseSchema.MessageTable(base.Mailbox.Database);
			}
			if (column != messageTable.GroupCns)
			{
				base.OnColumnChanged(column, deltaSize);
			}
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x000303B6 File Offset: 0x0002E5B6
		void IStateObject.OnBeforeCommit(Context context)
		{
			if (base.IsNew && this.originalFolder != null && this.originalMessageID.IsValid)
			{
				throw new StoreException((LID)55893U, ErrorCodeValue.CallFailed);
			}
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000303EA File Offset: 0x0002E5EA
		void IStateObject.OnCommit(Context context)
		{
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x000303EC File Offset: 0x0002E5EC
		void IStateObject.OnAbort(Context context)
		{
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x000303EE File Offset: 0x0002E5EE
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<TopMessage>(this);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x000303F8 File Offset: 0x0002E5F8
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.openMessageInstanceState != null)
				{
					OpenMessageStates.RemoveInstance(base.Mailbox.CurrentOperationContext, base.Mailbox, this.openMessageInstanceState);
					this.openMessageInstanceState = null;
				}
			}
			else if (this.openMessageInstanceState != null)
			{
				this.openMessageInstanceState.MarkUnreferenced();
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x00030450 File Offset: 0x0002E650
		internal bool NeedsGroupExpansion(Context context)
		{
			if (context.DatabaseType != DatabaseType.Sql)
			{
				StorePropTag namedPropStorePropTag = base.Mailbox.GetNamedPropStorePropTag(context, NamedPropInfo.Compliance.NeedGroupExpansion.PropName, PropertyType.Boolean, ObjectType.Message);
				object propertyValue = this.GetPropertyValue(context, namedPropStorePropTag);
				if (propertyValue != null)
				{
					return (bool)propertyValue;
				}
			}
			return false;
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00030494 File Offset: 0x0002E694
		internal bool IsInferenceProcessingNeeded(Context context)
		{
			if (context.DatabaseType != DatabaseType.Sql)
			{
				StorePropTag namedPropStorePropTag = base.Mailbox.GetNamedPropStorePropTag(context, NamedPropInfo.Inference.InferenceProcessingNeeded.PropName, PropertyType.Boolean, ObjectType.Message);
				object propertyValue = this.GetPropertyValue(context, namedPropStorePropTag);
				if (propertyValue != null)
				{
					return (bool)propertyValue;
				}
			}
			return false;
		}

		// Token: 0x0400022A RID: 554
		private static Hookable<Action<PreReadOperator>> messagePrereadTestHook = Hookable<Action<PreReadOperator>>.Create(true, null);

		// Token: 0x0400022B RID: 555
		private Folder parentFolder;

		// Token: 0x0400022C RID: 556
		private int documentId;

		// Token: 0x0400022D RID: 557
		private Folder originalFolder;

		// Token: 0x0400022E RID: 558
		private ExchangeId originalMessageID;

		// Token: 0x0400022F RID: 559
		private ExchangeId deleteChangeNumber;

		// Token: 0x04000230 RID: 560
		private int originalAttachCount;

		// Token: 0x04000231 RID: 561
		private ulong changedGroups;

		// Token: 0x04000232 RID: 562
		private MessageTable messageTable;

		// Token: 0x04000233 RID: 563
		private OpenMessageInstance openMessageInstanceState;

		// Token: 0x04000234 RID: 564
		private bool forceCreatedEventForCopy;

		// Token: 0x04000235 RID: 565
		private bool timerEventFired;

		// Token: 0x04000236 RID: 566
		private bool isReadTemp;

		// Token: 0x04000237 RID: 567
		private ConversationItem conversationItem;
	}
}
