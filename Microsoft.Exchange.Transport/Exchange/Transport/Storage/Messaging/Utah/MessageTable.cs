using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Storage.Messaging.Utah
{
	// Token: 0x02000112 RID: 274
	internal class MessageTable : DataTable
	{
		// Token: 0x06000C5F RID: 3167 RVA: 0x0002ADE8 File Offset: 0x00028FE8
		public MessageTable(MessagingGeneration generation)
		{
			if (generation == null)
			{
				throw new ArgumentNullException("generation");
			}
			this.generation = generation;
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0002AE46 File Offset: 0x00029046
		public int GetSafetyNetMessageCount(Destination.DestinationType destinationType)
		{
			return this.safetyNetMessageCount[(int)destinationType];
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0002AE50 File Offset: 0x00029050
		public int MessageCount
		{
			get
			{
				return this.currentMessageRowId - this.removedMessageCount;
			}
		}

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x0002AE5F File Offset: 0x0002905F
		public int ActiveMessageCount
		{
			get
			{
				return this.activeMessageCount;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0002AE67 File Offset: 0x00029067
		public int PendingMessageCount
		{
			get
			{
				return this.pendingMessageCount;
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x0002AE6F File Offset: 0x0002906F
		public MessagingGeneration Generation
		{
			get
			{
				return this.generation;
			}
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0002AE77 File Offset: 0x00029077
		public int GetNextMessageRowId()
		{
			this.perfCounters.MailItemCount.Increment();
			return Interlocked.Increment(ref this.currentMessageRowId);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0002AE95 File Offset: 0x00029095
		public int IncrementActiveMessageCount()
		{
			return Interlocked.Increment(ref this.activeMessageCount);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0002AEA2 File Offset: 0x000290A2
		public int DecrementActiveMessageCount()
		{
			return Interlocked.Decrement(ref this.activeMessageCount);
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0002AEAF File Offset: 0x000290AF
		public int IncrementPendingMessageCount()
		{
			return Interlocked.Increment(ref this.pendingMessageCount);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002AEBC File Offset: 0x000290BC
		public int DecrementPendingMessageCount()
		{
			return Interlocked.Decrement(ref this.pendingMessageCount);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002AEC9 File Offset: 0x000290C9
		public int IncrementSafetyNetMessageCount(Destination.DestinationType destinationType)
		{
			return Interlocked.Increment(ref this.safetyNetMessageCount[(int)destinationType]);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002AEDC File Offset: 0x000290DC
		public int DecrementSafetyNetMessageCount(Destination.DestinationType destinationType)
		{
			return Interlocked.Decrement(ref this.safetyNetMessageCount[(int)destinationType]);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0002AEEF File Offset: 0x000290EF
		public int DecrementMessageCount()
		{
			return Interlocked.Increment(ref this.removedMessageCount);
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002AEFC File Offset: 0x000290FC
		protected override void AttachLoadInitValues(Transaction transaction, DataTableCursor cursor)
		{
			if (base.IsNewTable)
			{
				this.currentMessageRowId = 0;
				cursor.CreateIndex("NdxMessage_DiscardState", "+DiscardState\0\0");
				return;
			}
			cursor.SetCurrentIndex(null);
			if (cursor.TryMoveLast())
			{
				this.currentMessageRowId = ((DataColumn<int>)base.Schemas[0]).ReadFromCursor(cursor);
				this.lastMessageId = new int?(this.currentMessageRowId);
				this.perfCounters.MailItemCount.IncrementBy((long)this.currentMessageRowId);
				ExTraceGlobals.StorageTracer.TraceDebug<string, int>((long)this.GetHashCode(), "Last used primary key for {0} is {1}", base.Name, this.currentMessageRowId);
			}
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002AFA0 File Offset: 0x000291A0
		public override bool TryDrop(DataConnection connection)
		{
			if (base.TryDrop(connection))
			{
				ExTraceGlobals.StorageTracer.TraceDebug(0L, "Message table {0} was dropped, Records:{1}, Pending:{2}, SN:{3}", new object[]
				{
					base.Name,
					this.MessageCount,
					this.PendingMessageCount,
					this.safetyNetMessageCount.Sum()
				});
				if (this.PendingMessageCount > 0)
				{
					ExTraceGlobals.StorageTracer.TraceError<int>(0L, "Message table {0} was dropped with {1} pending records.", this.PendingMessageCount);
				}
				this.perfCounters.MailItemCount.IncrementBy((long)(-1 * this.MessageCount));
				return true;
			}
			return false;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0002B048 File Offset: 0x00029248
		public bool TryCleanup(Transaction transaction)
		{
			ExTraceGlobals.StorageTracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "Cleanup started for table {0} estimate record count {1}, estimate pending count {2}", base.Name, this.MessageCount, this.PendingMessageCount);
			if (this.MessageCount == this.PendingMessageCount)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string>((long)this.GetHashCode(), "Cleanup can't take action to the table {0}, all messages are pending", base.Name);
				return false;
			}
			if (base.OpenCursorCount > 0)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string>((long)this.GetHashCode(), "Cleanup won't start with table in use.", base.Name);
				return false;
			}
			int num = 0;
			string text = base.Name + "temp";
			string text2 = base.Name + "new";
			bool result;
			try
			{
				MessageTable messageTable;
				using (DataTableCursor dataTableCursor = this.OpenCursor(transaction.Connection))
				{
					dataTableCursor.SetCurrentIndex("NdxMessage_DiscardState");
					if (!dataTableCursor.TryMoveFirst())
					{
						this.activeMessageCount = 0;
						this.pendingMessageCount = 0;
						return false;
					}
					messageTable = new MessageTable(this.Generation);
					messageTable.Attach(base.DataSource, transaction.Connection, text2);
					if (!messageTable.IsNewTable)
					{
						messageTable.TryDrop(transaction.Connection);
						messageTable = new MessageTable(this.Generation);
						messageTable.Attach(base.DataSource, transaction.Connection, text2);
					}
					using (DataTableCursor dataTableCursor2 = messageTable.OpenCursor(transaction.Connection))
					{
						do
						{
							dataTableCursor2.PrepareInsert(false, false);
							base.CopyRow(dataTableCursor, dataTableCursor2);
							dataTableCursor2.Update();
							num++;
							transaction.RestartIfStale(100);
						}
						while (dataTableCursor.TryMoveNext(false));
					}
					dataTableCursor.SetCurrentIndex(null);
				}
				if (!base.TryStopOpenCursor())
				{
					ExTraceGlobals.StorageTracer.TraceDebug<string>((long)this.GetHashCode(), "Cleanup couldn't get exclusive access to the table {0}", base.Name);
					result = false;
				}
				else
				{
					transaction.OnExitTransaction += base.ContinueOpenCursor;
					DataTable.Rename(transaction.Connection, base.Name, text);
					DataTable.Rename(transaction.Connection, text2, base.Name);
					DataTable.Rename(transaction.Connection, text, text2);
					messageTable.TryDrop(transaction.Connection);
					this.perfCounters.MailItemCount.IncrementBy((long)(-1 * num));
					this.removedMessageCount += this.MessageCount - num;
					result = true;
				}
			}
			finally
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string, int>((long)this.GetHashCode(), "Cleanup finished for table {0} moved messages {1}", base.Name, num);
			}
			return result;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0002B66C File Offset: 0x0002986C
		public IEnumerable<MessageTable.MailPriorityAndId> GetLeftoverPendingMessageIds()
		{
			if (this.lastMessageId != null)
			{
				using (DataConnection connection = base.DataSource.DemandNewConnection())
				{
					using (connection.BeginTransaction())
					{
						using (DataTableCursor cursor = this.OpenCursor(connection))
						{
							cursor.SetCurrentIndex("NdxMessage_DiscardState");
							cursor.MoveBeforeFirst();
							while (cursor.TryMoveNext(false))
							{
								int? messageId = base.Schemas[0].Int32FromBookmark(cursor);
								byte? pendingReason = base.Schemas[22].ByteFromIndex(cursor);
								if (messageId <= this.lastMessageId)
								{
									yield return new MessageTable.MailPriorityAndId
									{
										MessageId = this.Generation.CombineIds(messageId.Value),
										Priority = (byte)(pendingReason.Value >> 4)
									};
								}
							}
							cursor.SetCurrentIndex(null);
						}
					}
				}
			}
			yield break;
		}

		// Token: 0x0400051D RID: 1309
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp, PrimaryKey = true, Required = true)]
		public const int MessageRowId = 0;

		// Token: 0x0400051E RID: 1310
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp, Required = true)]
		public const int Flags = 1;

		// Token: 0x0400051F RID: 1311
		[DataColumnDefinition(typeof(DateTime), ColumnAccess.CachedProp, Required = true)]
		public const int DateReceived = 2;

		// Token: 0x04000520 RID: 1312
		[DataColumnDefinition(typeof(IPvxAddress), ColumnAccess.CachedProp, Required = true)]
		public const int SourceIpAddress = 3;

		// Token: 0x04000521 RID: 1313
		[DataColumnDefinition(typeof(byte), ColumnAccess.CachedProp, Required = true)]
		public const int BodyType = 4;

		// Token: 0x04000522 RID: 1314
		[DataColumnDefinition(typeof(long), ColumnAccess.CachedProp, Required = true)]
		public const int MimeSize = 5;

		// Token: 0x04000523 RID: 1315
		[DataColumnDefinition(typeof(byte), ColumnAccess.CachedProp)]
		public const int PoisonCount = 6;

		// Token: 0x04000524 RID: 1316
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp)]
		public const int ExtensionToExpiryDuration = 7;

		// Token: 0x04000525 RID: 1317
		[DataColumnDefinition(typeof(Guid), ColumnAccess.CachedProp)]
		public const int ShadowMessageId = 8;

		// Token: 0x04000526 RID: 1318
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, IntrinsicLV = true)]
		public const int ShadowServerDiscardId = 9;

		// Token: 0x04000527 RID: 1319
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, IntrinsicLV = true)]
		public const int ShadowServerContext = 10;

		// Token: 0x04000528 RID: 1320
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, IntrinsicLV = true)]
		public const int Oorg = 11;

		// Token: 0x04000529 RID: 1321
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, IntrinsicLV = true)]
		public const int EnvId = 12;

		// Token: 0x0400052A RID: 1322
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, IntrinsicLV = true)]
		public const int ReceiveConnectorName = 13;

		// Token: 0x0400052B RID: 1323
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, IntrinsicLV = true)]
		public const int HeloDomain = 14;

		// Token: 0x0400052C RID: 1324
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, IntrinsicLV = true)]
		public const int FromSmtpAddress = 15;

		// Token: 0x0400052D RID: 1325
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, IntrinsicLV = true)]
		public const int Auth = 16;

		// Token: 0x0400052E RID: 1326
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, IntrinsicLV = true)]
		public const int MimeSenderAddress = 17;

		// Token: 0x0400052F RID: 1327
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, IntrinsicLV = true)]
		public const int MimeFromAddress = 18;

		// Token: 0x04000530 RID: 1328
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, IntrinsicLV = true)]
		public const int Subject = 19;

		// Token: 0x04000531 RID: 1329
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, IntrinsicLV = true)]
		public const int InternetMessageId = 20;

		// Token: 0x04000532 RID: 1330
		[DataColumnDefinition(typeof(byte[]), ColumnAccess.Stream, IntrinsicLV = true, MultiValued = true)]
		public const int BlobCollection = 21;

		// Token: 0x04000533 RID: 1331
		[DataColumnDefinition(typeof(byte), ColumnAccess.CachedProp, Required = false)]
		public const int DiscardState = 22;

		// Token: 0x04000534 RID: 1332
		public const int PendingReason = 22;

		// Token: 0x04000535 RID: 1333
		public const string PendingReasonIndex = "NdxMessage_DiscardState";

		// Token: 0x04000536 RID: 1334
		private readonly MessagingGeneration generation;

		// Token: 0x04000537 RID: 1335
		private readonly int[] safetyNetMessageCount = new int[(int)(typeof(Destination.DestinationType).GetEnumValues().Cast<byte>().Max<byte>() + 1)];

		// Token: 0x04000538 RID: 1336
		private readonly DatabasePerfCountersInstance perfCounters = DatabasePerfCounters.GetInstance("other");

		// Token: 0x04000539 RID: 1337
		private int currentMessageRowId;

		// Token: 0x0400053A RID: 1338
		private int activeMessageCount;

		// Token: 0x0400053B RID: 1339
		private int pendingMessageCount;

		// Token: 0x0400053C RID: 1340
		private int removedMessageCount;

		// Token: 0x0400053D RID: 1341
		private int? lastMessageId;

		// Token: 0x02000113 RID: 275
		public struct MailPriorityAndId
		{
			// Token: 0x0400053E RID: 1342
			public long MessageId;

			// Token: 0x0400053F RID: 1343
			public byte Priority;
		}
	}
}
