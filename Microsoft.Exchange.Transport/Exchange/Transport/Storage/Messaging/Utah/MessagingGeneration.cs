using System;
using System.Diagnostics;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Storage.Messaging.Utah
{
	// Token: 0x02000115 RID: 277
	internal class MessagingGeneration : DataGeneration
	{
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0002DA20 File Offset: 0x0002BC20
		public MessageTable MessageTable
		{
			get
			{
				return this.messageTable;
			}
		}

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000CA3 RID: 3235 RVA: 0x0002DA28 File Offset: 0x0002BC28
		public RecipientTable RecipientTable
		{
			get
			{
				return this.recipientTable;
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0002DA30 File Offset: 0x0002BC30
		public new MessagingDatabase MessagingDatabase
		{
			get
			{
				return (MessagingDatabase)base.MessagingDatabase;
			}
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0002DA3D File Offset: 0x0002BC3D
		public static int GetGenerationId(long combinedId)
		{
			return (int)((ulong)combinedId >> 32);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0002DA44 File Offset: 0x0002BC44
		public static int GetRowId(long combinedId)
		{
			return (int)combinedId;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0002DA48 File Offset: 0x0002BC48
		public static long CombineIds(int generationId, int rowId)
		{
			ulong num = (ulong)((ulong)((long)generationId) << 32);
			return (long)(num | (ulong)rowId);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0002DA61 File Offset: 0x0002BC61
		public bool TryEnterReplay()
		{
			return this.TryEnterState(MessagingGeneration.State.Replaying);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0002DA6A File Offset: 0x0002BC6A
		public bool TryExitReplay()
		{
			return this.TryExitState(MessagingGeneration.State.Replaying);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0002DA74 File Offset: 0x0002BC74
		public override XElement GetDiagnosticInfo(string argument)
		{
			XElement xelement = base.GetDiagnosticInfo(argument);
			xelement.Add(new XElement("State", (MessagingGeneration.State)this.state));
			xelement.Add(new XElement("AttachStatus", base.IsAttached));
			if (base.IsAttached)
			{
				xelement.Add(new XElement("AttachTime", this.attachTime));
				xelement.Add(new XElement("MessageTableName", this.MessageTable.Name));
				xelement.Add(new XElement("MessageCount", this.MessageTable.MessageCount));
				xelement.Add(new XElement("ActiveMessageCount", this.MessageTable.ActiveMessageCount));
				xelement.Add(new XElement("PendingMessageCount", this.MessageTable.PendingMessageCount));
				xelement.Add(new XElement("RecipientTableName", this.RecipientTable.Name));
				xelement.Add(new XElement("RecipientCount", this.RecipientTable.RecipientCount));
				xelement.Add(new XElement("ActiveRecipientCount", this.RecipientTable.ActiveRecipientCount));
				foreach (object obj in typeof(Destination.DestinationType).GetEnumValues())
				{
					Destination.DestinationType destinationType = (Destination.DestinationType)obj;
					XElement xelement2 = new XElement("SafetyNetRecipientCount", this.RecipientTable.GetSafetyNetRecipientCount(destinationType));
					xelement2.SetAttributeValue("DestinationType", destinationType);
					xelement.Add(xelement2);
				}
			}
			xelement.Add(new XElement("DiagnosticInfo", this.diagnosticInfo));
			return xelement;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0002DC98 File Offset: 0x0002BE98
		private bool TryEnterState(MessagingGeneration.State newState)
		{
			return Interlocked.CompareExchange(ref this.state, (int)newState, 0) == 0;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0002DCAA File Offset: 0x0002BEAA
		private bool TryExitState(MessagingGeneration.State oldState)
		{
			return Interlocked.CompareExchange(ref this.state, 0, (int)oldState) == (int)oldState;
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0002DCBC File Offset: 0x0002BEBC
		protected override void Attach(Transaction transaction)
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			this.messageTable = new MessageTable(this);
			this.MessageTable.Attach(transaction.Connection.Source, transaction.Connection, "MailItemTable-" + base.Name);
			this.recipientTable = new RecipientTable(this);
			this.RecipientTable.Attach(transaction.Connection.Source, transaction.Connection, "RecipientTable-" + base.Name);
			this.attachTime = stopwatch.Elapsed;
			stopwatch.Stop();
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0002DD51 File Offset: 0x0002BF51
		protected override void Detach()
		{
			this.MessageTable.Detach();
			this.RecipientTable.Detach();
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0002DD6C File Offset: 0x0002BF6C
		protected override GenerationCleanupMode CleanupInternal()
		{
			if (!this.TryEnterState(MessagingGeneration.State.Expiring))
			{
				string message = string.Format("Cannot clean up generation {0}, it is being used. State={1}", base.Name, (MessagingGeneration.State)this.state);
				this.diagnosticInfo = message;
				ExTraceGlobals.StorageTracer.TraceDebug((long)base.GenerationId, message);
				return GenerationCleanupMode.None;
			}
			DateTime dateTime = this.MessagingDatabase.GenerationManager.ReferenceClock();
			bool flag = dateTime - base.EndTime > this.MessagingDatabase.Config.MessagingGenerationExpirationAge;
			bool flag2 = this.MessageTable.PendingMessageCount > 0;
			if (!flag)
			{
				if (flag2)
				{
					string message2 = string.Format("Cannot clean up generation {0}, it has {1} pending messages", base.Name, this.MessageTable.PendingMessageCount);
					this.diagnosticInfo = message2;
					ExTraceGlobals.StorageTracer.TraceDebug((long)base.GenerationId, message2);
					this.TryExitState(MessagingGeneration.State.Expiring);
					return GenerationCleanupMode.None;
				}
				DateTime dateTime2 = dateTime - this.MessagingDatabase.Config.MessagingGenerationCleanupAge;
				int timeOffset = MailRecipientStorage.GetTimeOffset(dateTime2);
				if (timeOffset < this.lastKnownDelivery)
				{
					string message3 = string.Format("Cannot cleanup generation {0} for cutoff {1}, it has deliveries at least till {2}", base.Name, dateTime2, MailRecipientStorage.GetTimeFromOffset(this.lastKnownDelivery));
					this.diagnosticInfo = message3;
					ExTraceGlobals.StorageTracer.TraceDebug((long)base.GenerationId, message3);
					this.TryExitState(MessagingGeneration.State.Expiring);
					return GenerationCleanupMode.None;
				}
				using (DataConnection dataConnection = this.MessageTable.DataSource.DemandNewConnection())
				{
					using (dataConnection.BeginTransaction())
					{
						using (DataTableCursor dataTableCursor = this.RecipientTable.OpenCursor(dataConnection))
						{
							dataTableCursor.SetCurrentIndex("NdxRecipient_DestinationHash_DeliveryTimeOffset");
							dataTableCursor.MoveBeforeFirst();
							while (dataTableCursor.TryMoveNext(false))
							{
								int? num = this.RecipientTable.Schemas[7].Int32FromIndex(dataTableCursor);
								if (num != null && num.Value > timeOffset)
								{
									this.lastKnownDelivery = num.Value;
									string message4 = string.Format("Cannot clean up generation {0} for cutoff {1}, it has deliveries at least till {2}", base.Name, dateTime2, MailRecipientStorage.GetTimeFromOffset(this.lastKnownDelivery));
									this.diagnosticInfo = message4;
									ExTraceGlobals.StorageTracer.TraceDebug((long)base.GenerationId, message4);
									this.TryExitState(MessagingGeneration.State.Expiring);
									return GenerationCleanupMode.None;
								}
							}
						}
					}
				}
			}
			GenerationCleanupMode result;
			using (DataConnection dataConnection2 = this.MessageTable.DataSource.DemandNewConnection())
			{
				using (Transaction transaction2 = dataConnection2.BeginTransaction())
				{
					if (flag2)
					{
						this.RecipientTable.TryCleanup(transaction2);
						this.MessageTable.TryCleanup(transaction2);
						transaction2.Commit(TransactionCommitMode.Lazy);
						this.TryExitState(MessagingGeneration.State.Expiring);
						result = GenerationCleanupMode.Cleanup;
					}
					else
					{
						transaction2.Checkpoint(TransactionCommitMode.Lazy, 100);
						this.RecipientTable.TryDrop(dataConnection2);
						this.MessageTable.TryDrop(dataConnection2);
						transaction2.Commit(TransactionCommitMode.Lazy);
						this.TryExitState(MessagingGeneration.State.Expiring);
						result = GenerationCleanupMode.Drop;
					}
				}
			}
			return result;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0002E0B8 File Offset: 0x0002C2B8
		protected override bool DropInternal()
		{
			if (!this.TryEnterState(MessagingGeneration.State.Expiring))
			{
				string message = string.Format("Cannot drop generation {0}, it is being used. State={1}", base.Name, (MessagingGeneration.State)this.state);
				this.diagnosticInfo = message;
				ExTraceGlobals.StorageTracer.TraceDebug((long)base.GenerationId, message);
				return false;
			}
			using (DataConnection dataConnection = this.MessageTable.DataSource.DemandNewConnection())
			{
				using (Transaction transaction = dataConnection.BeginTransaction())
				{
					this.RecipientTable.TryDrop(dataConnection);
					this.MessageTable.TryDrop(dataConnection);
					transaction.Commit(TransactionCommitMode.Lazy);
				}
			}
			return true;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0002E174 File Offset: 0x0002C374
		public long CombineIds(int id)
		{
			return MessagingGeneration.CombineIds(base.GenerationId, id);
		}

		// Token: 0x04000556 RID: 1366
		private MessageTable messageTable;

		// Token: 0x04000557 RID: 1367
		private RecipientTable recipientTable;

		// Token: 0x04000558 RID: 1368
		private int lastKnownDelivery = int.MinValue;

		// Token: 0x04000559 RID: 1369
		private int state;

		// Token: 0x0400055A RID: 1370
		private string diagnosticInfo = "newvalue";

		// Token: 0x0400055B RID: 1371
		private TimeSpan attachTime;

		// Token: 0x02000116 RID: 278
		private enum State
		{
			// Token: 0x0400055D RID: 1373
			Active,
			// Token: 0x0400055E RID: 1374
			Expiring,
			// Token: 0x0400055F RID: 1375
			Replaying
		}
	}
}
