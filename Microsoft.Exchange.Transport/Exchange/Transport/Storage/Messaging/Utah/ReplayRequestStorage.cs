using System;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Transport.Storage.Messaging.Utah
{
	// Token: 0x0200011C RID: 284
	internal class ReplayRequestStorage : DataRow
	{
		// Token: 0x06000CED RID: 3309 RVA: 0x0002F43D File Offset: 0x0002D63D
		public ReplayRequestStorage(DataTableCursor cursor) : base(cursor.Table)
		{
			base.LoadFromCurrentRow(cursor);
			this.destination = new Destination(this.DestinationType, this.DestinationBlob);
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0002F46C File Offset: 0x0002D66C
		public ReplayRequestStorage(ReplayRequestTable table, Destination destination, DateTime startTime, DateTime endTime, Guid correlationId, bool isTestRequest) : base(table)
		{
			this.RequestId = table.GetNextRequestId();
			this.PrimaryRequestId = this.RequestId;
			this.StartTime = startTime;
			this.EndTime = endTime;
			this.DateCreated = DateTime.UtcNow;
			this.DestinationType = destination.Type;
			this.DestinationBlob = destination.Blob;
			this.State = ResubmitRequestState.None;
			this.DiagnosticInformation = string.Empty;
			this.ContinuationToken = MessagingGeneration.CombineIds(int.MaxValue, int.MinValue);
			this.CorrelationId = correlationId;
			this.IsTestRequest = isTestRequest;
			this.destination = destination;
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x0002F508 File Offset: 0x0002D708
		public Destination Destination
		{
			get
			{
				return this.destination;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0002F510 File Offset: 0x0002D710
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x0002F528 File Offset: 0x0002D728
		public long RequestId
		{
			get
			{
				return ((ColumnCache<long>)base.Columns[0]).Value;
			}
			set
			{
				((ColumnCache<long>)base.Columns[0]).Value = value;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0002F541 File Offset: 0x0002D741
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x0002F559 File Offset: 0x0002D759
		public long PrimaryRequestId
		{
			get
			{
				return ((ColumnCache<long>)base.Columns[1]).Value;
			}
			set
			{
				((ColumnCache<long>)base.Columns[1]).Value = value;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0002F572 File Offset: 0x0002D772
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x0002F58A File Offset: 0x0002D78A
		public DateTime StartTime
		{
			get
			{
				return ((ColumnCache<DateTime>)base.Columns[2]).Value;
			}
			set
			{
				((ColumnCache<DateTime>)base.Columns[2]).Value = value;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0002F5A3 File Offset: 0x0002D7A3
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x0002F5BB File Offset: 0x0002D7BB
		public DateTime EndTime
		{
			get
			{
				return ((ColumnCache<DateTime>)base.Columns[3]).Value;
			}
			set
			{
				((ColumnCache<DateTime>)base.Columns[3]).Value = value;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0002F5D4 File Offset: 0x0002D7D4
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x0002F5EC File Offset: 0x0002D7EC
		public DateTime DateCreated
		{
			get
			{
				return ((ColumnCache<DateTime>)base.Columns[4]).Value;
			}
			set
			{
				((ColumnCache<DateTime>)base.Columns[4]).Value = value;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0002F605 File Offset: 0x0002D805
		// (set) Token: 0x06000CFB RID: 3323 RVA: 0x0002F61D File Offset: 0x0002D81D
		private Destination.DestinationType DestinationType
		{
			get
			{
				return (Destination.DestinationType)((ColumnCache<byte>)base.Columns[5]).Value;
			}
			set
			{
				((ColumnCache<byte>)base.Columns[5]).Value = (byte)value;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x0002F636 File Offset: 0x0002D836
		// (set) Token: 0x06000CFD RID: 3325 RVA: 0x0002F64E File Offset: 0x0002D84E
		private byte[] DestinationBlob
		{
			get
			{
				return ((ColumnCache<byte[]>)base.Columns[6]).Value;
			}
			set
			{
				((ColumnCache<byte[]>)base.Columns[6]).Value = value;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x0002F667 File Offset: 0x0002D867
		// (set) Token: 0x06000CFF RID: 3327 RVA: 0x0002F67F File Offset: 0x0002D87F
		public ResubmitRequestState State
		{
			get
			{
				return (ResubmitRequestState)((ColumnCache<int>)base.Columns[7]).Value;
			}
			set
			{
				((ColumnCache<int>)base.Columns[7]).Value = (int)value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0002F698 File Offset: 0x0002D898
		// (set) Token: 0x06000D01 RID: 3329 RVA: 0x0002F6B0 File Offset: 0x0002D8B0
		public long ContinuationToken
		{
			get
			{
				return ((ColumnCache<long>)base.Columns[8]).Value;
			}
			set
			{
				((ColumnCache<long>)base.Columns[8]).Value = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0002F6C9 File Offset: 0x0002D8C9
		// (set) Token: 0x06000D03 RID: 3331 RVA: 0x0002F6E2 File Offset: 0x0002D8E2
		public string DiagnosticInformation
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[9]).Value;
			}
			set
			{
				((ColumnCache<string>)base.Columns[9]).Value = value;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0002F6FC File Offset: 0x0002D8FC
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x0002F715 File Offset: 0x0002D915
		public Guid CorrelationId
		{
			get
			{
				return ((ColumnCache<Guid>)base.Columns[10]).Value;
			}
			set
			{
				((ColumnCache<Guid>)base.Columns[10]).Value = value;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0002F72F File Offset: 0x0002D92F
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x0002F74E File Offset: 0x0002D94E
		public bool IsTestRequest
		{
			get
			{
				return ((ColumnCache<int>)base.Columns[11]).Value != 0;
			}
			set
			{
				((ColumnCache<int>)base.Columns[11]).Value = (value ? 1 : 0);
			}
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0002F76E File Offset: 0x0002D96E
		public new void Commit()
		{
			base.Commit(base.IsNew ? TransactionCommitMode.Immediate : TransactionCommitMode.MediumLatencyLazy);
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0002F782 File Offset: 0x0002D982
		public new void Materialize(Transaction transaction)
		{
			base.Materialize(transaction);
		}

		// Token: 0x04000587 RID: 1415
		private readonly Destination destination;
	}
}
