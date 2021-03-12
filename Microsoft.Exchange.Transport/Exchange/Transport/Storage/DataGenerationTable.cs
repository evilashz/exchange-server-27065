using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Transport.Storage.Messaging;

namespace Microsoft.Exchange.Transport.Storage
{
	// Token: 0x020000E3 RID: 227
	internal class DataGenerationTable : DataTable
	{
		// Token: 0x06000826 RID: 2086 RVA: 0x00020D76 File Offset: 0x0001EF76
		public DataGenerationTable(IMessagingDatabase messagingDatabase)
		{
			this.messagingDatabase = messagingDatabase;
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x00020D85 File Offset: 0x0001EF85
		public IMessagingDatabase MessagingDatabase
		{
			get
			{
				return this.messagingDatabase;
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00020D8D File Offset: 0x0001EF8D
		public int GetNextGenerationId()
		{
			return Interlocked.Increment(ref this.currentGenerationId);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00020D9C File Offset: 0x0001EF9C
		protected override void AttachLoadInitValues(Transaction transaction, DataTableCursor cursor)
		{
			base.AttachLoadInitValues(transaction, cursor);
			if (!base.IsNewTable)
			{
				cursor.SetCurrentIndex(null);
				if (cursor.TryMoveLast())
				{
					this.currentGenerationId = ((DataColumn<int>)base.Schemas[0]).ReadFromCursor(cursor);
					ExTraceGlobals.StorageTracer.TraceDebug<string, int>((long)this.GetHashCode(), "Last used primary key for {0} is {1}", base.Name, this.currentGenerationId);
				}
			}
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00020FF0 File Offset: 0x0001F1F0
		public IEnumerable<DataGenerationRow> GetAllRows()
		{
			using (DataTableCursor cursor = this.GetCursor())
			{
				using (cursor.BeginTransaction())
				{
					cursor.MoveBeforeFirst();
					while (cursor.TryMoveNext(false))
					{
						yield return DataGenerationRow.LoadFromRow(cursor);
					}
				}
			}
			yield break;
		}

		// Token: 0x04000418 RID: 1048
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp, PrimaryKey = true, Required = true)]
		public const int GenerationId = 0;

		// Token: 0x04000419 RID: 1049
		[DataColumnDefinition(typeof(DateTime), ColumnAccess.CachedProp, Required = true)]
		public const int StartTime = 1;

		// Token: 0x0400041A RID: 1050
		[DataColumnDefinition(typeof(DateTime), ColumnAccess.CachedProp, Required = true)]
		public const int EndTime = 2;

		// Token: 0x0400041B RID: 1051
		[DataColumnDefinition(typeof(int), ColumnAccess.CachedProp, Required = true)]
		public const int Category = 3;

		// Token: 0x0400041C RID: 1052
		[DataColumnDefinition(typeof(string), ColumnAccess.CachedProp, Required = true, IntrinsicLV = true)]
		public const int GenerationName = 4;

		// Token: 0x0400041D RID: 1053
		private readonly IMessagingDatabase messagingDatabase;

		// Token: 0x0400041E RID: 1054
		private int currentGenerationId;
	}
}
