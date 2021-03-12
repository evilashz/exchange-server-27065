using System;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x0200009E RID: 158
	internal abstract class ChunkingScanner
	{
		// Token: 0x06000591 RID: 1425 RVA: 0x000171A3 File Offset: 0x000153A3
		public virtual void Scan(Transaction transaction, DataTableCursor cursor, bool forward)
		{
			if (ChunkingScanner.SeekStartRow(cursor, forward))
			{
				this.ScanFromCurrentPosition(transaction, cursor, forward);
			}
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x000171B8 File Offset: 0x000153B8
		public void ScanFromCurrentPosition(Transaction transaction, DataTableCursor cursor, bool forward)
		{
			if (transaction == null)
			{
				throw new ArgumentNullException("transaction");
			}
			if (cursor == null)
			{
				throw new ArgumentNullException("cursor");
			}
			if (forward)
			{
				while (this.HandleRecord(cursor) == ChunkingScanner.ScanControl.Continue)
				{
					transaction.RestartIfStale(100);
					if (!cursor.TryMoveNext(false))
					{
						return;
					}
				}
				return;
			}
			while (this.HandleRecord(cursor) == ChunkingScanner.ScanControl.Continue)
			{
				transaction.RestartIfStale(100);
				if (!cursor.TryMovePrevious(false))
				{
					return;
				}
			}
		}

		// Token: 0x06000593 RID: 1427
		protected abstract ChunkingScanner.ScanControl HandleRecord(DataTableCursor cursor);

		// Token: 0x06000594 RID: 1428 RVA: 0x0001721D File Offset: 0x0001541D
		private static bool SeekStartRow(DataTableCursor cursor, bool forward)
		{
			if (forward)
			{
				cursor.PrereadForward();
				return cursor.TryMoveFirst();
			}
			return cursor.TryMoveLast();
		}

		// Token: 0x0200009F RID: 159
		protected enum ScanControl
		{
			// Token: 0x040002D6 RID: 726
			Continue,
			// Token: 0x040002D7 RID: 727
			Stop
		}
	}
}
