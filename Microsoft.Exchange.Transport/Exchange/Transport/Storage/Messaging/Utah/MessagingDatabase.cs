using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.MessageResubmission;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Transport.Storage.Messaging.Utah
{
	// Token: 0x02000114 RID: 276
	internal class MessagingDatabase : IMessagingDatabase
	{
		// Token: 0x06000C71 RID: 3185 RVA: 0x0002B68C File Offset: 0x0002988C
		public MessagingDatabase()
		{
			this.generationTable = new DataGenerationTable(this);
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0002B6DC File Offset: 0x000298DC
		public void Start()
		{
			this.useAllGenerations = true;
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0002B6E5 File Offset: 0x000298E5
		public DataSource DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0002B6ED File Offset: 0x000298ED
		public QueueTable QueueTable
		{
			get
			{
				return this.queueTable;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0002B6F5 File Offset: 0x000298F5
		public ServerInfoTable ServerInfoTable
		{
			get
			{
				return this.serverInfoTable;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0002B6FD File Offset: 0x000298FD
		internal IMessagingDatabaseConfig Config
		{
			get
			{
				return this.config;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x0002B705 File Offset: 0x00029905
		internal DatabaseAutoRecovery DatabaseAutoRecovery
		{
			get
			{
				return this.databaseAutoRecovery;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0002B70D File Offset: 0x0002990D
		public DataGenerationManager<MessagingGeneration> GenerationManager
		{
			get
			{
				return this.generationManager;
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x0002B715 File Offset: 0x00029915
		public string CurrentState
		{
			get
			{
				if (this.databaseOpenTime != TimeSpan.Zero)
				{
					return string.Format("Messaging Database open time {0}", this.databaseOpenTime);
				}
				return "Messaging Database is not open";
			}
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0002B744 File Offset: 0x00029944
		public void Attach(IMessagingDatabaseConfig config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			this.config = config;
			this.useAllGenerations = false;
			MailItemStorage.DefaultAsyncCommitTimeout = config.DefaultAsyncCommitTimeout;
			int i = 10;
			while (i > 0)
			{
				i--;
				try
				{
					this.AttachInternal();
					break;
				}
				catch (EsentFileAccessDeniedException ex)
				{
					if (i <= 0)
					{
						Components.EventLogger.LogEvent(TransportEventLogConstants.Tuple_DatabaseInUse, null, new object[]
						{
							Strings.MessagingDatabaseInstanceName,
							ex
						});
						string notificationReason = string.Format("Database {0} is already in use. The service will be stopped. Exception details: {1}", Strings.MessagingDatabaseInstanceName, ex);
						EventNotificationItem.Publish(ExchangeComponent.Transport.Name, "TransportServiceStartError", null, notificationReason, ResultSeverityLevel.Error, false);
						throw new TransportComponentLoadFailedException(Strings.DataBaseInUse("Messaging Database"), ex);
					}
					Thread.Sleep(1000);
				}
				catch (SchemaException inner)
				{
					throw new TransportComponentLoadFailedException(Strings.DatabaseAttachFailed("Messaging Database"), inner);
				}
			}
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0002B850 File Offset: 0x00029A50
		public void Detach()
		{
			this.QueueTable.Detach();
			this.ServerInfoTable.Detach();
			this.GenerationManager.Unload();
			this.generationTable.Detach();
			this.replayRequestTable.Detach();
			this.dataSource.CloseDatabase(false);
			this.dataSource = null;
			if (this.delayedBootloaderComplete != null)
			{
				this.delayedBootloaderComplete.Dispose();
				this.delayedBootloaderComplete = null;
			}
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0002B8D0 File Offset: 0x00029AD0
		private void AttachInternal()
		{
			ExTraceGlobals.StorageTracer.TraceDebug<string>(0L, " db path {0}", this.config.DatabasePath);
			ExTraceGlobals.StorageTracer.TraceDebug<string>(0L, " logfile path {0}", this.config.LogFilePath);
			ExTraceGlobals.StorageTracer.TraceDebug<int>(0L, " max connections {0}", this.config.MaxConnections);
			this.databaseAutoRecovery = new DatabaseAutoRecovery(this.config.DatabaseRecoveryAction, "MessagingDatabase", this.config.DatabasePath, this.config.LogFilePath, Strings.MessagingDatabaseInstanceName, "Messaging", 3, null);
			this.databaseAutoRecovery.PerformDatabaseAutoRecoveryIfNeccessary();
			this.dataSource = new DataSource(Strings.MessagingDatabaseInstanceName, this.config.DatabasePath, "mail.que", this.config.MaxConnections, "mail", this.config.LogFilePath, this.databaseAutoRecovery);
			this.dataSource.LogBuffers = this.config.LogBufferSize;
			this.dataSource.LogFileSize = this.config.LogFileSize;
			this.dataSource.MaxBackgroundCleanupTasks = this.config.MaxBackgroundCleanupTasks;
			this.dataSource.ExtensionSize = this.config.ExtensionSize;
			Stopwatch stopwatch = Stopwatch.StartNew();
			this.dataSource.OpenDatabase();
			this.databaseOpenTime = stopwatch.Elapsed;
			stopwatch.Stop();
			if (this.dataSource.NewDatabase)
			{
				ExTraceGlobals.StorageTracer.TraceDebug(0L, "New database created");
				Components.EventLogger.LogEvent(TransportEventLogConstants.Tuple_NewDatabaseCreated, null, new object[]
				{
					"mail.que"
				});
			}
			VersionTable versionTable = new VersionTable(this.DatabaseAutoRecovery, 15L, 1L);
			ExTraceGlobals.StorageTracer.TraceDebug(0L, "attaching tables");
			using (DataConnection dataConnection = this.dataSource.DemandNewConnection())
			{
				ExTraceGlobals.StorageTracer.TraceDebug(0L, " mail version");
				versionTable.Attach(this.dataSource, dataConnection);
				ExTraceGlobals.StorageTracer.TraceDebug(0L, " generation table");
				this.generationTable.Attach(this.dataSource, dataConnection);
				ExTraceGlobals.StorageTracer.TraceDebug(0L, " replay request table");
				this.replayRequestTable.Attach(this.dataSource, dataConnection);
				ExTraceGlobals.StorageTracer.TraceDebug(0L, " server info");
				this.ServerInfoTable.Attach(this.dataSource, dataConnection);
				ExTraceGlobals.StorageTracer.TraceDebug(0L, " mail queue");
				this.QueueTable.Attach(this.dataSource, dataConnection);
				ExTraceGlobals.StorageTracer.TraceDebug(0L, " done.");
			}
			TimeSpan messagingGenerationLength = this.config.MessagingGenerationLength;
			Func<TimeSpan> ageToCleanup = () => this.config.MessagingGenerationCleanupAge;
			DataGenerationCategory category = DataGenerationCategory.Messaging;
			DataGenerationTable table = this.generationTable;
			int recentGenerationDepth = this.config.RecentGenerationDepth;
			bool autoCreateEnabled = true;
			this.generationManager = new DataGenerationManager<MessagingGeneration>(messagingGenerationLength, ageToCleanup, category, table, recentGenerationDepth, true, autoCreateEnabled, true);
			this.generationManager.GetRecentGenerations();
			this.UpgradeRevision(versionTable);
			versionTable.Detach();
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0002BBD8 File Offset: 0x00029DD8
		private void UpgradeRevision(VersionTable version)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			Stopwatch stopwatch2 = new Stopwatch();
			if (version.CurrentRevision < 1L)
			{
				using (DataConnection dataConnection = this.DataSource.DemandNewConnection())
				{
					using (Transaction transaction = dataConnection.BeginTransaction())
					{
						foreach (MessagingGeneration messagingGeneration in this.GenerationManager.GetGenerations(DateTime.MinValue, DateTime.MaxValue))
						{
							using (DataTableCursor dataTableCursor = messagingGeneration.RecipientTable.OpenCursor(dataConnection))
							{
								using (DataTableCursor dataTableCursor2 = messagingGeneration.MessageTable.OpenCursor(dataConnection))
								{
									int num = 0;
									stopwatch2.Restart();
									dataTableCursor2.TryCreateIndex("NdxMessage_DiscardState", "+DiscardState\0\0");
									dataTableCursor.SetCurrentIndex("NdxRecipient_UndeliveredMessageRowId");
									dataTableCursor.MoveBeforeFirst();
									dataTableCursor2.SetCurrentIndex(null);
									DataColumn<byte> dataColumn = (DataColumn<byte>)messagingGeneration.MessageTable.Schemas[22];
									while (dataTableCursor.TryMoveNext(true))
									{
										if (dataTableCursor2.TrySeek(new byte[][]
										{
											BitConverter.GetBytes(messagingGeneration.RecipientTable.Schemas[9].Int32FromIndex(dataTableCursor).Value)
										}))
										{
											dataTableCursor2.PrepareUpdate(true);
											dataColumn.WriteToCursor(dataTableCursor2, 2);
											dataTableCursor2.Update();
											num++;
										}
										transaction.RestartIfStale(100);
									}
									transaction.Checkpoint(TransactionCommitMode.Lazy, 100);
									ExTraceGlobals.StorageTracer.TracePerformance<string, int, TimeSpan>((long)this.GetHashCode(), "Generation {0} upgraded {1} active messages to revision: 1 in {2}", messagingGeneration.Name, num, stopwatch2.Elapsed);
								}
							}
						}
						version.UpgradeRevision(transaction, 1L);
						transaction.Commit(TransactionCommitMode.Lazy);
						ExTraceGlobals.StorageTracer.TracePerformance<TimeSpan>((long)this.GetHashCode(), "Database upgraded to revision: 1 in {0}", stopwatch.Elapsed);
					}
				}
			}
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0002BE1C File Offset: 0x0002A01C
		public IMailRecipientStorage NewRecipientStorage(long messageId)
		{
			MessagingGeneration messagingGeneration = (messageId != 0L) ? this.GetGenerationFromId(messageId) : this.GenerationManager.GetCurrentGeneration();
			if (messagingGeneration == null)
			{
				throw new ArgumentException("The generation of this message no longer exists.", "messageId");
			}
			return new MailRecipientStorage(messagingGeneration.RecipientTable, messageId);
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0002BE62 File Offset: 0x0002A062
		public IMailItemStorage NewMailItemStorage(bool loadDefaults)
		{
			return new MailItemStorage(this.GenerationManager.GetCurrentGeneration().MessageTable, loadDefaults);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0002BE7C File Offset: 0x0002A07C
		public IMailItemStorage LoadMailItemFromId(long messageId)
		{
			MessagingGeneration generationFromId = this.GetGenerationFromId(messageId);
			if (generationFromId == null)
			{
				return null;
			}
			IMailItemStorage result;
			using (DataTableCursor cursor = generationFromId.MessageTable.GetCursor())
			{
				using (cursor.BeginTransaction())
				{
					cursor.SetCurrentIndex(null);
					result = (cursor.TrySeek(new byte[][]
					{
						BitConverter.GetBytes((int)messageId)
					}) ? new MailItemStorage(cursor) : null);
				}
			}
			return result;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0002BF0C File Offset: 0x0002A10C
		public IMailRecipientStorage LoadMailRecipientFromId(long recipientId)
		{
			MessagingGeneration generationFromId = this.GetGenerationFromId(recipientId);
			if (generationFromId == null)
			{
				return null;
			}
			IMailRecipientStorage result;
			using (DataTableCursor cursor = generationFromId.RecipientTable.GetCursor())
			{
				using (cursor.BeginTransaction())
				{
					cursor.SetCurrentIndex(null);
					result = this.LoadMailRecipientFromRowId(MessagingGeneration.GetRowId(recipientId), cursor);
				}
			}
			return result;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0002C214 File Offset: 0x0002A414
		public IEnumerable<IMailRecipientStorage> LoadMailRecipientsFromMessageId(long messageId)
		{
			MessagingGeneration generation = this.GetGenerationFromId(messageId);
			if (generation != null)
			{
				using (DataTableCursor cursor = generation.RecipientTable.GetCursor())
				{
					using (cursor.BeginTransaction())
					{
						int messageRowId = MessagingGeneration.GetRowId(messageId);
						cursor.SetCurrentIndex("NdxRecipient_UndeliveredMessageRowId");
						if (cursor.TrySeek(new byte[][]
						{
							BitConverter.GetBytes(messageRowId)
						}) && cursor.TrySetIndexUpperRange(new byte[][]
						{
							BitConverter.GetBytes(messageRowId)
						}))
						{
							do
							{
								yield return new MailRecipientStorage(cursor);
							}
							while (cursor.TryMoveNext(false));
							cursor.TryRemoveIndexRange();
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0002C340 File Offset: 0x0002A540
		public IEnumerable<IMailRecipientStorage> LoadMailRecipientsFromCursor(DataTableCursor cursor)
		{
			if (cursor == null)
			{
				throw new ArgumentNullException("cursor");
			}
			if (cursor.HasData())
			{
				do
				{
					yield return new MailRecipientStorage(cursor);
				}
				while (cursor.TryMoveNext(false));
			}
			yield break;
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0002C364 File Offset: 0x0002A564
		public IMailRecipientStorage LoadMailRecipientFromRowId(int recipientRowId, DataTableCursor cursor)
		{
			if (cursor == null)
			{
				throw new ArgumentNullException("cursor");
			}
			if (!cursor.TrySeek(new byte[][]
			{
				BitConverter.GetBytes(recipientRowId)
			}))
			{
				return null;
			}
			return new MailRecipientStorage(cursor);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0002C3A0 File Offset: 0x0002A5A0
		public Transaction BeginNewTransaction()
		{
			DataConnection dataConnection = this.DataSource.DemandNewConnection();
			Transaction result = dataConnection.BeginTransaction();
			dataConnection.Release();
			return result;
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0002C3D0 File Offset: 0x0002A5D0
		public MessagingDatabaseResultStatus ReadUnprocessedMessageIds(out Dictionary<byte, List<long>> unprocessedMessageIds)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			MessagingDatabaseResultStatus result;
			if (this.useAllGenerations)
			{
				unprocessedMessageIds = this.GetUnprocessedMessageIdsInGenerations(this.generationManager.GetGenerations(DateTime.MinValue, DateTime.MaxValue));
				result = MessagingDatabaseResultStatus.Complete;
			}
			else
			{
				unprocessedMessageIds = this.GetUnprocessedMessageIdsInGenerations(this.generationManager.GetRecentGenerations());
				result = MessagingDatabaseResultStatus.Partial;
			}
			stopwatch.Stop();
			ExTraceGlobals.StorageTracer.TracePerformance<int, TimeSpan>(0L, "ReadUnprocessedMessageIds retrieved {0} bookmarks in {1}", unprocessedMessageIds.Values.Sum((List<long> i) => i.Count<long>()), stopwatch.Elapsed);
			return result;
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0002C900 File Offset: 0x0002AB00
		public IEnumerable<MailItemAndRecipients> GetMessages(List<long> messageIds)
		{
			Transaction transaction = null;
			DataTableCursor messageCursor = null;
			DataTableCursor recipientCursor = null;
			Stopwatch stopwatch = Stopwatch.StartNew();
			int messageCount = 0;
			int generationCount = 0;
			try
			{
				transaction = this.BeginNewTransaction();
				MessagingGeneration currentGeneration = null;
				for (int i = 0; i < messageIds.Count; i++)
				{
					if (messageIds[i] != 0L)
					{
						int generationId = MessagingGeneration.GetGenerationId(messageIds[i]);
						int messageId = MessagingGeneration.GetRowId(messageIds[i]);
						if (currentGeneration == null || currentGeneration.GenerationId != generationId)
						{
							if (messageCursor != null)
							{
								messageCursor.Close();
							}
							if (recipientCursor != null)
							{
								recipientCursor.Close();
							}
							currentGeneration = this.GenerationManager.GetGeneration(generationId);
							messageCursor = currentGeneration.MessageTable.OpenCursor(transaction.Connection);
							messageCursor.SetCurrentIndex(null);
							recipientCursor = currentGeneration.RecipientTable.OpenCursor(transaction.Connection);
							recipientCursor.SetCurrentIndex("NdxRecipient_UndeliveredMessageRowId");
							generationCount++;
						}
						if (messageCursor.TrySeek(new byte[][]
						{
							BitConverter.GetBytes(messageId)
						}))
						{
							bool foundRecipients = recipientCursor.TrySeek(new byte[][]
							{
								BitConverter.GetBytes(messageId)
							}) && recipientCursor.TrySetIndexUpperRange(new byte[][]
							{
								BitConverter.GetBytes(messageId)
							});
							stopwatch.Stop();
							yield return new MailItemAndRecipients(new MailItemStorage(messageCursor), foundRecipients ? this.LoadMailRecipientsFromCursor(recipientCursor) : Enumerable.Empty<IMailRecipientStorage>());
							stopwatch.Start();
							if (foundRecipients)
							{
								recipientCursor.TryRemoveIndexRange();
							}
							messageIds[i] = 0L;
							messageCount++;
							transaction.RestartIfStale(this.Config.MaxMessageLoadTimePercentage);
						}
					}
				}
			}
			finally
			{
				if (recipientCursor != null)
				{
					recipientCursor.Dispose();
				}
				if (messageCursor != null)
				{
					messageCursor.Dispose();
				}
				if (transaction != null)
				{
					transaction.Dispose();
				}
				stopwatch.Stop();
				ExTraceGlobals.StorageTracer.TraceDebug<int, int, TimeSpan>(0L, "GetMessages returned {0} messages across {1} generations in {2}", messageCount, generationCount, stopwatch.Elapsed);
				ExTraceGlobals.StorageTracer.TracePerformance<int, int, TimeSpan>(0L, "GetMessages returned {0} messages across {1} generations in {2}", messageCount, generationCount, stopwatch.Elapsed);
			}
			yield break;
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0002C924 File Offset: 0x0002AB24
		public IReplayRequest NewReplayRequest(Guid correlationId, Destination destination, DateTime startTime, DateTime endTime, bool isTestRequest = false)
		{
			return new ReplayRequest(this, new ReplayRequestStorage(this.replayRequestTable, destination, startTime, endTime, correlationId, isTestRequest));
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0002C947 File Offset: 0x0002AB47
		public IEnumerable<IReplayRequest> GetAllReplayRequests()
		{
			return (from storage in this.replayRequestTable.GetAllRows()
			select new ReplayRequest(this, storage)).ToArray<ReplayRequest>();
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0002C97C File Offset: 0x0002AB7C
		public List<MailItemAndRecipients> GetDeliveredMessages(IEnumerable<Tuple<MessagingGeneration, IGrouping<int, int>>> bookmarks, ref long continuationKey, string conditions = null)
		{
			List<MailItemAndRecipients> list = new List<MailItemAndRecipients>();
			ResubmitFilter resubmitFilter = null;
			if (!string.IsNullOrEmpty(conditions) && !ResubmitFilter.TryBuild(conditions, out resubmitFilter))
			{
				return new List<MailItemAndRecipients>();
			}
			using (Transaction transaction = this.BeginNewTransaction())
			{
				foreach (IGrouping<MessagingGeneration, IGrouping<int, int>> grouping in from i in bookmarks
				group i.Item2 by i.Item1)
				{
					if (grouping.Key.TryEnterReplay())
					{
						try
						{
							using (DataTableCursor dataTableCursor = grouping.Key.MessageTable.OpenCursor(transaction.Connection))
							{
								using (DataTableCursor dataTableCursor2 = grouping.Key.RecipientTable.OpenCursor(transaction.Connection))
								{
									foreach (IGrouping<int, int> grouping2 in grouping)
									{
										dataTableCursor.Seek(new byte[][]
										{
											BitConverter.GetBytes(grouping2.Key)
										});
										if (resubmitFilter == null || !resubmitFilter.FromAddressChecking || resubmitFilter.ValidateStringParam(ResubmitFilter.FilterParameterType.FromAddress, dataTableCursor.Table.Schemas[15].StringFromCursor(dataTableCursor)))
										{
											List<IMailRecipientStorage> list2 = new List<IMailRecipientStorage>();
											foreach (int recipientRowId in grouping2)
											{
												list2.Add(this.LoadMailRecipientFromRowId(recipientRowId, dataTableCursor2));
											}
											MailItemAndRecipients item = new MailItemAndRecipients(new MailItemStorage(dataTableCursor), list2);
											list.Add(item);
										}
										int? num = dataTableCursor.Table.Schemas[0].Int32FromCursor(dataTableCursor);
										if (num != null)
										{
											continuationKey = grouping.Key.MessageTable.Generation.CombineIds(num.Value);
										}
										transaction.RestartIfStale(100);
									}
								}
							}
						}
						finally
						{
							grouping.Key.TryExitReplay();
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0002D1E0 File Offset: 0x0002B3E0
		public IEnumerable<Tuple<MessagingGeneration, IGrouping<int, int>>> GetDeliveredBookmarks(Destination destination, DateTime startDate, DateTime endDate, long continuationKey)
		{
			foreach (MessagingGeneration gen2 in (from gen in this.GenerationManager.GetGenerations(this.AdjustStartDateForMaxDeliveryLag(startDate), endDate)
			where gen.GenerationId <= MessagingGeneration.GetGenerationId(continuationKey)
			select gen).Reverse<MessagingGeneration>())
			{
				SortedSet<Tuple<int, int>> deliveryBookmarks = new SortedSet<Tuple<int, int>>();
				using (Transaction transaction = this.BeginNewTransaction())
				{
					using (DataTableCursor dataTableCursor = gen2.RecipientTable.OpenCursor(transaction.Connection))
					{
						Stopwatch stopwatch = Stopwatch.StartNew();
						int num = 0;
						dataTableCursor.SetCurrentIndex("NdxRecipient_DestinationHash_DeliveryTimeOffset");
						if (gen2.TryEnterReplay())
						{
							if (dataTableCursor.TrySeekGE(new byte[][]
							{
								BitConverter.GetBytes(destination.GetHashCode()),
								BitConverter.GetBytes(MailRecipientStorage.GetTimeOffset(startDate)),
								BitConverter.GetBytes(-2147483648)
							}) && dataTableCursor.TrySetIndexUpperRange(new byte[][]
							{
								BitConverter.GetBytes(destination.GetHashCode()),
								BitConverter.GetBytes(MailRecipientStorage.GetTimeOffset(endDate)),
								BitConverter.GetBytes(2147483647)
							}))
							{
								do
								{
									int value = gen2.RecipientTable.Schemas[1].Int32FromIndex(dataTableCursor).Value;
									int value2 = gen2.RecipientTable.Schemas[0].Int32FromBookmark(dataTableCursor).Value;
									if (gen2.GenerationId != MessagingGeneration.GetGenerationId(continuationKey) || value > MessagingGeneration.GetRowId(continuationKey))
									{
										deliveryBookmarks.Add(Tuple.Create<int, int>(value, value2));
										num++;
									}
								}
								while (dataTableCursor.TryMoveNext(false));
							}
							gen2.TryExitReplay();
							stopwatch.Stop();
							ExTraceGlobals.StorageTracer.TracePerformance<int, int, TimeSpan>((long)gen2.GenerationId, "GetDelivered bookmarks for this generation returned {0} messages, {1} recipients and took {2}", deliveryBookmarks.Count, num, stopwatch.Elapsed);
							this.perfCounters.ReplayBookmarkAverageLatency.IncrementBy(stopwatch.ElapsedTicks);
							this.perfCounters.ReplayBookmarkAverageLatencyBase.IncrementBy((long)num);
						}
					}
				}
				foreach (IGrouping<int, int> messageRecipGroup in from i in deliveryBookmarks
				group i.Item2 by i.Item1)
				{
					yield return Tuple.Create<MessagingGeneration, IGrouping<int, int>>(gen2, messageRecipGroup);
				}
			}
			yield break;
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0002D794 File Offset: 0x0002B994
		public IEnumerable<Tuple<MessagingGeneration, IGrouping<int, int>>> GetConditionalDeliveredBookmarks(DateTime startDate, DateTime endDate, Destination destination, long continuationKey, string conditions)
		{
			ResubmitFilter filter = null;
			if (!string.IsNullOrEmpty(conditions) && ResubmitFilter.TryBuild(conditions, out filter))
			{
				foreach (MessagingGeneration gen2 in (from gen in this.GenerationManager.GetGenerations(this.AdjustStartDateForMaxDeliveryLag(startDate), endDate)
				where gen.GenerationId <= MessagingGeneration.GetGenerationId(continuationKey)
				select gen).Reverse<MessagingGeneration>())
				{
					SortedSet<Tuple<int, int>> deliveryBookmarks = new SortedSet<Tuple<int, int>>();
					using (Transaction transaction = this.BeginNewTransaction())
					{
						using (DataTableCursor dataTableCursor = gen2.RecipientTable.OpenCursor(transaction.Connection))
						{
							Stopwatch stopwatch = Stopwatch.StartNew();
							int num = 0;
							dataTableCursor.SetCurrentIndex(null);
							if (gen2.TryEnterReplay())
							{
								if (dataTableCursor.TryMoveFirst())
								{
									do
									{
										int value = gen2.RecipientTable.Schemas[1].Int32FromCursor(dataTableCursor).Value;
										int value2 = gen2.RecipientTable.Schemas[0].Int32FromIndex(dataTableCursor).Value;
										if (gen2.GenerationId != MessagingGeneration.GetGenerationId(continuationKey) || value > MessagingGeneration.GetRowId(continuationKey))
										{
											byte[] array = gen2.RecipientTable.Schemas[10].BytesFromCursor(dataTableCursor, false, 1);
											if (array != null && array[0] == 1 && (!filter.ToAddressChecking || filter.ValidateStringParam(ResubmitFilter.FilterParameterType.ToAddress, gen2.RecipientTable.Schemas[12].StringFromCursor(dataTableCursor))))
											{
												deliveryBookmarks.Add(Tuple.Create<int, int>(value, value2));
												num++;
											}
										}
									}
									while (dataTableCursor.TryMoveNext(false));
								}
								gen2.TryExitReplay();
								stopwatch.Stop();
								ExTraceGlobals.StorageTracer.TracePerformance<int, int, TimeSpan>((long)gen2.GenerationId, "GetDelivered bookmarks for this generation returned {0} messages, {1} recipients and took {2}", deliveryBookmarks.Count, num, stopwatch.Elapsed);
								this.perfCounters.ReplayBookmarkAverageLatency.IncrementBy(stopwatch.ElapsedTicks);
								this.perfCounters.ReplayBookmarkAverageLatencyBase.IncrementBy((long)num);
							}
						}
					}
					foreach (IGrouping<int, int> messageRecipGroup in from i in deliveryBookmarks
					group i.Item2 by i.Item1)
					{
						yield return Tuple.Create<MessagingGeneration, IGrouping<int, int>>(gen2, messageRecipGroup);
					}
				}
			}
			yield break;
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0002D7D0 File Offset: 0x0002B9D0
		public XElement GetDiagnosticInfo(string argument)
		{
			if (argument.Equals("Database", StringComparison.InvariantCultureIgnoreCase))
			{
				return new XElement("DatabaseOpenTime", this.databaseOpenTime);
			}
			if (argument.Equals("Generations", StringComparison.InvariantCultureIgnoreCase))
			{
				return this.GenerationManager.GetDiagnosticInfo(argument);
			}
			if (argument.StartsWith("Transaction", StringComparison.InvariantCultureIgnoreCase))
			{
				return Transaction.GetDiagnosticInfo(argument);
			}
			return new XElement("UtahMessagingDatabase", new XElement("help", "Supported arguments: config, Database, Generations, TransactionsOpen, TransactionStartTrace, TransactionTrace, TransactionPercentile"));
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0002D859 File Offset: 0x0002BA59
		public XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return this.GetDiagnosticInfo(parameters.Argument);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0002D868 File Offset: 0x0002BA68
		internal MessagingGeneration GetGenerationFromId(long id)
		{
			return this.GenerationManager.GetGeneration(MessagingGeneration.GetGenerationId(id));
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0002D87B File Offset: 0x0002BA7B
		public void SuspendDataCleanup()
		{
			this.generationManager.SuspendDataCleanup();
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0002D888 File Offset: 0x0002BA88
		public void SuspendDataCleanup(DateTime startDate, DateTime endDate)
		{
			this.generationManager.SuspendDataCleanup(this.AdjustStartDateForMaxDeliveryLag(startDate), endDate);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0002D89D File Offset: 0x0002BA9D
		public void ResumeDataCleanup()
		{
			this.generationManager.ResumeDataCleanup();
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0002D8AA File Offset: 0x0002BAAA
		public void ResumeDataCleanup(DateTime startDate, DateTime endDate)
		{
			this.generationManager.ResumeDataCleanup(this.AdjustStartDateForMaxDeliveryLag(startDate), endDate);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0002D8D0 File Offset: 0x0002BAD0
		public void BootLoadCompleted()
		{
			this.ResumeDataCleanup();
			if (this.databaseAutoRecovery.GetDatabaseCorruptionCount() > 0)
			{
				this.delayedBootloaderComplete = new Timer(delegate(object o)
				{
					this.databaseAutoRecovery.ResetDatabaseCorruptionFlag();
				}, null, TimeSpan.FromMinutes(5.0), TimeSpan.FromMilliseconds(-1.0));
			}
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0002D92C File Offset: 0x0002BB2C
		internal DateTime AdjustStartDateForMaxDeliveryLag(DateTime originalStartDate)
		{
			TimeSpan t = this.config.MessagingGenerationExpirationAge - this.config.MessagingGenerationCleanupAge;
			if (!(originalStartDate <= DateTime.MinValue + t))
			{
				return originalStartDate - t;
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0002D984 File Offset: 0x0002BB84
		private Dictionary<byte, List<long>> GetUnprocessedMessageIdsInGenerations(IEnumerable<MessagingGeneration> generations)
		{
			Dictionary<byte, List<long>> dictionary = new Dictionary<byte, List<long>>();
			foreach (MessageTable.MailPriorityAndId mailPriorityAndId in generations.SelectMany((MessagingGeneration gen) => gen.MessageTable.GetLeftoverPendingMessageIds()))
			{
				List<long> list;
				if (!dictionary.TryGetValue(mailPriorityAndId.Priority, out list))
				{
					list = (dictionary[mailPriorityAndId.Priority] = new List<long>());
				}
				list.Add(mailPriorityAndId.MessageId);
			}
			return dictionary;
		}

		// Token: 0x04000540 RID: 1344
		public const string PerfCounterInstanceName = "mail";

		// Token: 0x04000541 RID: 1345
		private const string DefaultDatabaseFileName = "mail.que";

		// Token: 0x04000542 RID: 1346
		private readonly QueueTable queueTable = new QueueTable();

		// Token: 0x04000543 RID: 1347
		private readonly ServerInfoTable serverInfoTable = new ServerInfoTable();

		// Token: 0x04000544 RID: 1348
		private readonly DataGenerationTable generationTable;

		// Token: 0x04000545 RID: 1349
		private readonly ReplayRequestTable replayRequestTable = new ReplayRequestTable();

		// Token: 0x04000546 RID: 1350
		private readonly DatabasePerfCountersInstance perfCounters = DatabasePerfCounters.GetInstance("other");

		// Token: 0x04000547 RID: 1351
		private Timer delayedBootloaderComplete;

		// Token: 0x04000548 RID: 1352
		private DataGenerationManager<MessagingGeneration> generationManager;

		// Token: 0x04000549 RID: 1353
		private IMessagingDatabaseConfig config;

		// Token: 0x0400054A RID: 1354
		private DataSource dataSource;

		// Token: 0x0400054B RID: 1355
		private DatabaseAutoRecovery databaseAutoRecovery;

		// Token: 0x0400054C RID: 1356
		private TimeSpan databaseOpenTime;

		// Token: 0x0400054D RID: 1357
		private bool useAllGenerations;
	}
}
