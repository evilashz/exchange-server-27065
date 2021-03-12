using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Storage.Messaging.Utah
{
	// Token: 0x02000119 RID: 281
	internal class RecipientTable : DataTable
	{
		// Token: 0x06000CBE RID: 3262 RVA: 0x0002E2B4 File Offset: 0x0002C4B4
		public RecipientTable(MessagingGeneration generation)
		{
			if (generation == null)
			{
				throw new ArgumentNullException("generation");
			}
			this.generation = generation;
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x0002E312 File Offset: 0x0002C512
		public int GetSafetyNetRecipientCount(Destination.DestinationType destinationType)
		{
			return this.safetyNetRecipientCount[(int)destinationType];
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0002E31C File Offset: 0x0002C51C
		public int RecipientCount
		{
			get
			{
				return this.currentRecipientRowId - this.removedRecipientCount;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0002E32B File Offset: 0x0002C52B
		public int ActiveRecipientCount
		{
			get
			{
				return this.activeRecipientCount;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0002E333 File Offset: 0x0002C533
		public MessagingGeneration Generation
		{
			get
			{
				return this.generation;
			}
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x0002E33B File Offset: 0x0002C53B
		public int GetNextRecipientRowId()
		{
			this.perfCounters.MailRecipientCount.Increment();
			return Interlocked.Increment(ref this.currentRecipientRowId);
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x0002E359 File Offset: 0x0002C559
		public int IncrementActiveRecipientCount()
		{
			this.perfCounters.MailRecipientActiveCount.Increment();
			return Interlocked.Increment(ref this.activeRecipientCount);
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x0002E377 File Offset: 0x0002C577
		public int DecrementActiveRecipientCount()
		{
			this.perfCounters.MailRecipientActiveCount.Decrement();
			return Interlocked.Decrement(ref this.activeRecipientCount);
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0002E395 File Offset: 0x0002C595
		public int DecrementRecipientCount()
		{
			this.perfCounters.MailRecipientCount.Decrement();
			return Interlocked.Increment(ref this.removedRecipientCount);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x0002E3B4 File Offset: 0x0002C5B4
		public int IncrementSafetyNetRecipientCount(Destination.DestinationType destinationType)
		{
			this.perfCounters.MailRecipientSafetyNetCount.Increment();
			switch (destinationType)
			{
			case Destination.DestinationType.Mdb:
				this.perfCounters.MailRecipientSafetyNetMdbCount.Increment();
				break;
			case Destination.DestinationType.Shadow:
				this.perfCounters.MailRecipientShadowSafetyNetCount.Increment();
				break;
			}
			return Interlocked.Increment(ref this.safetyNetRecipientCount[(int)destinationType]);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x0002E41C File Offset: 0x0002C61C
		public int DecrementSafetyNetRecipientCount(Destination.DestinationType destinationType)
		{
			this.perfCounters.MailRecipientSafetyNetCount.Decrement();
			switch (destinationType)
			{
			case Destination.DestinationType.Mdb:
				this.perfCounters.MailRecipientSafetyNetMdbCount.Decrement();
				break;
			case Destination.DestinationType.Shadow:
				this.perfCounters.MailRecipientShadowSafetyNetCount.Decrement();
				break;
			}
			return Interlocked.Decrement(ref this.safetyNetRecipientCount[(int)destinationType]);
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x0002E484 File Offset: 0x0002C684
		private void UpdateSafetyNetStats()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			using (DataConnection dataConnection = base.DataSource.DemandNewConnection())
			{
				using (dataConnection.BeginTransaction())
				{
					using (DataTableCursor dataTableCursor = this.OpenCursor(dataConnection))
					{
						dataTableCursor.SetCurrentIndex("NdxRecipient_DestinationHash_DeliveryTimeOffset");
						dataTableCursor.MoveBeforeFirst();
						while (dataTableCursor.TryMoveNext(false))
						{
							int value = base.Schemas[6].Int32FromIndex(dataTableCursor).Value;
							Destination.DestinationType destinationType;
							if (!Destination.DestinationTypeDictionary.TryGetValue(value, out destinationType))
							{
								destinationType = (Destination.DestinationType)base.Schemas[10].BytesFromCursor(dataTableCursor, false, 1)[0];
								Destination.DestinationTypeDictionary.Add(value, destinationType);
							}
							this.IncrementSafetyNetRecipientCount(destinationType);
						}
						dataTableCursor.SetCurrentIndex(null);
					}
				}
			}
			ExTraceGlobals.StorageTracer.TracePerformance<TimeSpan>(0L, "UpdateSafetyNetStats completed in {0}", stopwatch.Elapsed);
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x0002E598 File Offset: 0x0002C798
		protected override void AttachLoadInitValues(Transaction transaction, DataTableCursor cursor)
		{
			if (base.IsNewTable)
			{
				this.currentRecipientRowId = 0;
				cursor.CreateIndex("NdxRecipient_MessageRowId", "+MessageRowId\0\0");
				cursor.CreateIndex("NdxRecipient_DestinationHash_DeliveryTimeOffset", "+DestinationHash\0+DeliveryTimeOffset\0+MessageRowId\0\0");
				cursor.CreateIndex("NdxRecipient_UndeliveredMessageRowId", "+UndeliveredMessageRowId\0\0");
				return;
			}
			cursor.SetCurrentIndex(null);
			if (cursor.TryMoveLast())
			{
				this.currentRecipientRowId = ((DataColumn<int>)base.Schemas[0]).ReadFromCursor(cursor);
				this.perfCounters.MailRecipientCount.IncrementBy((long)this.currentRecipientRowId);
				ExTraceGlobals.StorageTracer.TraceDebug<string, int>((long)this.GetHashCode(), "Last used primary key for {0} is {1}", base.Name, this.currentRecipientRowId);
			}
			this.UpdateSafetyNetStats();
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x0002E654 File Offset: 0x0002C854
		public override bool TryDrop(DataConnection connection)
		{
			if (base.TryDrop(connection))
			{
				ExTraceGlobals.StorageTracer.TraceDebug(0L, "Recipient table {0} was dropped, Records:{1}, Active:{2}, SN:{3}", new object[]
				{
					base.Name,
					this.RecipientCount,
					this.ActiveRecipientCount,
					this.safetyNetRecipientCount.Sum()
				});
				if (this.ActiveRecipientCount > 0)
				{
					ExTraceGlobals.StorageTracer.TraceError<int>(0L, "Recipient table {0} was dropped with {1} active records.", this.ActiveRecipientCount);
				}
				this.perfCounters.MailRecipientCount.IncrementBy((long)(-1 * this.RecipientCount));
				this.perfCounters.MailRecipientActiveCount.IncrementBy((long)(-1 * this.ActiveRecipientCount));
				this.perfCounters.MailRecipientSafetyNetCount.IncrementBy((long)(-1 * this.safetyNetRecipientCount.Sum()));
				this.perfCounters.MailRecipientSafetyNetMdbCount.IncrementBy((long)(-1 * this.safetyNetRecipientCount[1]));
				this.perfCounters.MailRecipientShadowSafetyNetCount.IncrementBy((long)(-1 * this.safetyNetRecipientCount[2]));
				return true;
			}
			return false;
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x0002E76C File Offset: 0x0002C96C
		public bool TryCleanup(Transaction transaction)
		{
			ExTraceGlobals.StorageTracer.TraceDebug<string, int, int>((long)this.GetHashCode(), "Cleanup started for table {0} estimate record count {1}, estimate active count {2}", base.Name, this.RecipientCount, this.ActiveRecipientCount);
			if (this.RecipientCount == this.ActiveRecipientCount)
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string>((long)this.GetHashCode(), "Cleanup can't take action to the table {0}, all recipients are active", base.Name);
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
				RecipientTable recipientTable;
				using (DataTableCursor dataTableCursor = this.OpenCursor(transaction.Connection))
				{
					dataTableCursor.SetCurrentIndex("NdxRecipient_UndeliveredMessageRowId");
					if (!dataTableCursor.TryMoveFirst())
					{
						this.activeRecipientCount = 0;
						return false;
					}
					recipientTable = new RecipientTable(this.Generation);
					recipientTable.Attach(base.DataSource, transaction.Connection, text2);
					if (!recipientTable.IsNewTable)
					{
						recipientTable.TryDrop(transaction.Connection);
						recipientTable = new RecipientTable(this.Generation);
						recipientTable.Attach(base.DataSource, transaction.Connection, text2);
					}
					using (DataTableCursor dataTableCursor2 = recipientTable.OpenCursor(transaction.Connection))
					{
						do
						{
							dataTableCursor2.PrepareInsert(false, false);
							base.CopyRow(dataTableCursor, dataTableCursor2);
							dataTableCursor2.Update();
							num++;
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
					recipientTable.TryDrop(transaction.Connection);
					this.removedRecipientCount += this.RecipientCount - num;
					this.perfCounters.MailRecipientCount.IncrementBy((long)(-1 * num));
					this.perfCounters.MailRecipientSafetyNetCount.IncrementBy((long)(-1 * this.safetyNetRecipientCount.Sum()));
					this.perfCounters.MailRecipientSafetyNetMdbCount.IncrementBy((long)(-1 * this.safetyNetRecipientCount[1]));
					this.perfCounters.MailRecipientShadowSafetyNetCount.IncrementBy((long)(-1 * this.safetyNetRecipientCount[2]));
					for (int i = 0; i < this.safetyNetRecipientCount.Length; i++)
					{
						this.safetyNetRecipientCount[i] = 0;
					}
					result = true;
				}
			}
			finally
			{
				ExTraceGlobals.StorageTracer.TraceDebug<string, int>((long)this.GetHashCode(), "Cleanup finished for table {0} moved messages {1}", base.Name, num);
			}
			return result;
		}

		// Token: 0x04000563 RID: 1379
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp, PrimaryKey = true, Required = true)]
		public const int RecipientRowId = 0;

		// Token: 0x04000564 RID: 1380
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp, Required = true)]
		public const int MessageRowId = 1;

		// Token: 0x04000565 RID: 1381
		[DataColumnDefinition(typeof(byte), ColumnAccess.CachedProp, Required = true)]
		public const int AdminActionStatus = 2;

		// Token: 0x04000566 RID: 1382
		[DataColumnDefinition(typeof(byte), ColumnAccess.CachedProp, Required = true)]
		public const int Status = 3;

		// Token: 0x04000567 RID: 1383
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp, Required = true)]
		public const int Dsn = 4;

		// Token: 0x04000568 RID: 1384
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp)]
		public const int RetryCount = 5;

		// Token: 0x04000569 RID: 1385
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp)]
		public const int DestinationHash = 6;

		// Token: 0x0400056A RID: 1386
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp)]
		public const int DeliveryTimeOffset = 7;

		// Token: 0x0400056B RID: 1387
		[DataColumnDefinition(typeof(DateTime), ColumnAccess.CachedProp)]
		public const int DeliveryTime = 8;

		// Token: 0x0400056C RID: 1388
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp)]
		public const int UndeliveredMessageRowId = 9;

		// Token: 0x0400056D RID: 1389
		[DataColumnDefinition(typeof(byte), ColumnAccess.CachedProp)]
		public const int DeliveredDestinationType = 10;

		// Token: 0x0400056E RID: 1390
		[DataColumnDefinition(typeof(byte[]), ColumnAccess.CachedProp, IntrinsicLV = true)]
		public const int DeliveredDestination = 11;

		// Token: 0x0400056F RID: 1391
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, IntrinsicLV = true)]
		public const int ToSmtpAddress = 12;

		// Token: 0x04000570 RID: 1392
		[Obsolete("The property is no longer being used. Should be cleaned up in the next DB upgrade")]
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, IntrinsicLV = true)]
		public const int TlsDomain = 13;

		// Token: 0x04000571 RID: 1393
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, IntrinsicLV = true)]
		public const int ORcpt = 14;

		// Token: 0x04000572 RID: 1394
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, IntrinsicLV = true)]
		public const int PrimaryServerFqdnGuid = 15;

		// Token: 0x04000573 RID: 1395
		[DataColumnDefinition(typeof(byte[]), ColumnAccess.Stream, IntrinsicLV = true, MultiValued = true)]
		public const int BlobCollection = 16;

		// Token: 0x04000574 RID: 1396
		public const string MessageIndexName = "NdxRecipient_MessageRowId";

		// Token: 0x04000575 RID: 1397
		public const string DestinationIndexName = "NdxRecipient_DestinationHash_DeliveryTimeOffset";

		// Token: 0x04000576 RID: 1398
		public const string BootstrapIndexName = "NdxRecipient_UndeliveredMessageRowId";

		// Token: 0x04000577 RID: 1399
		private readonly MessagingGeneration generation;

		// Token: 0x04000578 RID: 1400
		private readonly int[] safetyNetRecipientCount = new int[(int)(typeof(Destination.DestinationType).GetEnumValues().Cast<byte>().Max<byte>() + 1)];

		// Token: 0x04000579 RID: 1401
		private readonly DatabasePerfCountersInstance perfCounters = DatabasePerfCounters.GetInstance("other");

		// Token: 0x0400057A RID: 1402
		private int currentRecipientRowId;

		// Token: 0x0400057B RID: 1403
		private int activeRecipientCount;

		// Token: 0x0400057C RID: 1404
		private int removedRecipientCount;
	}
}
