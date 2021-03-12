using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Storage.Messaging.Utah
{
	// Token: 0x0200011D RID: 285
	internal class ReplayRequestTable : DataTable
	{
		// Token: 0x06000D0A RID: 3338 RVA: 0x0002F78B File Offset: 0x0002D98B
		public long GetNextRequestId()
		{
			return Interlocked.Increment(ref this.currentRequestId);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0002F798 File Offset: 0x0002D998
		protected override void AttachLoadInitValues(Transaction transaction, DataTableCursor cursor)
		{
			base.AttachLoadInitValues(transaction, cursor);
			if (!base.IsNewTable)
			{
				cursor.SetCurrentIndex(null);
				if (cursor.TryMoveLast())
				{
					this.currentRequestId = ((DataColumn<long>)base.Schemas[0]).ReadFromCursor(cursor);
					ExTraceGlobals.StorageTracer.TraceDebug<string, long>((long)this.GetHashCode(), "Last used primary key for {0} is {1}", base.Name, this.currentRequestId);
				}
			}
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0002F9EC File Offset: 0x0002DBEC
		public IEnumerable<ReplayRequestStorage> GetAllRows()
		{
			using (DataTableCursor cursor = this.GetCursor())
			{
				using (cursor.BeginTransaction())
				{
					cursor.MoveBeforeFirst();
					while (cursor.TryMoveNext(false))
					{
						yield return new ReplayRequestStorage(cursor);
					}
				}
			}
			yield break;
		}

		// Token: 0x04000588 RID: 1416
		[DataColumnDefinition(typeof(long), ColumnAccess.CachedProp, PrimaryKey = true, Required = true)]
		public const int RequestId = 0;

		// Token: 0x04000589 RID: 1417
		[DataColumnDefinition(typeof(long), ColumnAccess.CachedProp, Required = true)]
		public const int PrimaryRequestId = 1;

		// Token: 0x0400058A RID: 1418
		[DataColumnDefinition(typeof(DateTime), ColumnAccess.CachedProp, Required = true)]
		public const int StartTime = 2;

		// Token: 0x0400058B RID: 1419
		[DataColumnDefinition(typeof(DateTime), ColumnAccess.CachedProp, Required = true)]
		public const int EndTime = 3;

		// Token: 0x0400058C RID: 1420
		[DataColumnDefinition(typeof(DateTime), ColumnAccess.CachedProp, Required = true)]
		public const int DateCreated = 4;

		// Token: 0x0400058D RID: 1421
		[DataColumnDefinition(typeof(byte), ColumnAccess.CachedProp, Required = true)]
		public const int DestinationType = 5;

		// Token: 0x0400058E RID: 1422
		[DataColumnDefinition(typeof(byte[]), ColumnAccess.CachedProp, Required = true, IntrinsicLV = true)]
		public const int Destination = 6;

		// Token: 0x0400058F RID: 1423
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp, Required = true)]
		public const int State = 7;

		// Token: 0x04000590 RID: 1424
		[DataColumnDefinition(typeof(long), ColumnAccess.CachedProp, Required = true)]
		public const int ContinuationToken = 8;

		// Token: 0x04000591 RID: 1425
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, IntrinsicLV = true)]
		public const int DiagnosticInformation = 9;

		// Token: 0x04000592 RID: 1426
		[DataColumnDefinition(typeof(Guid), ColumnAccess.CachedProp)]
		public const int CorrelationId = 10;

		// Token: 0x04000593 RID: 1427
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp)]
		public const int RequestType = 11;

		// Token: 0x04000594 RID: 1428
		private long currentRequestId;
	}
}
