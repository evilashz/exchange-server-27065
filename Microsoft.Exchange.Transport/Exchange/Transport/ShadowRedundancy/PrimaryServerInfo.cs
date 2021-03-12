using System;
using System.Collections.Generic;
using Microsoft.Exchange.Transport.Storage;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x0200036B RID: 875
	internal sealed class PrimaryServerInfo : DataRow
	{
		// Token: 0x06002604 RID: 9732 RVA: 0x00093EE6 File Offset: 0x000920E6
		public PrimaryServerInfo(DataTable dataTable) : base(dataTable)
		{
			this.EndTime = DateTime.MaxValue;
		}

		// Token: 0x06002605 RID: 9733 RVA: 0x00093EFC File Offset: 0x000920FC
		public PrimaryServerInfo(PrimaryServerInfo primaryServerInfo, DataTable dataTable) : base(dataTable)
		{
			this.ServerFqdn = primaryServerInfo.ServerFqdn;
			this.DatabaseState = primaryServerInfo.DatabaseState;
			this.StartTime = primaryServerInfo.StartTime;
			this.EndTime = primaryServerInfo.EndTime;
			this.Version = primaryServerInfo.Version;
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06002606 RID: 9734 RVA: 0x00093F4C File Offset: 0x0009214C
		// (set) Token: 0x06002607 RID: 9735 RVA: 0x00093F64 File Offset: 0x00092164
		public string ServerFqdn
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[2]).Value;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("serverFqdn");
				}
				((ColumnCache<string>)base.Columns[2]).Value = value;
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x00093F90 File Offset: 0x00092190
		// (set) Token: 0x06002609 RID: 9737 RVA: 0x00093FA8 File Offset: 0x000921A8
		public string DatabaseState
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[1]).Value;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentNullException("databaseState");
				}
				((ColumnCache<string>)base.Columns[1]).Value = value;
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x00093FD4 File Offset: 0x000921D4
		// (set) Token: 0x0600260B RID: 9739 RVA: 0x00093FEC File Offset: 0x000921EC
		public DateTime StartTime
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

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x0600260C RID: 9740 RVA: 0x00094005 File Offset: 0x00092205
		// (set) Token: 0x0600260D RID: 9741 RVA: 0x0009401D File Offset: 0x0009221D
		public DateTime EndTime
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

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x00094036 File Offset: 0x00092236
		public int Id
		{
			get
			{
				return ((ColumnCache<int>)base.Columns[0]).Value;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x0600260F RID: 9743 RVA: 0x0009404E File Offset: 0x0009224E
		// (set) Token: 0x06002610 RID: 9744 RVA: 0x00094066 File Offset: 0x00092266
		public ShadowRedundancyCompatibilityVersion Version
		{
			get
			{
				return (ShadowRedundancyCompatibilityVersion)((ColumnCache<int>)base.Columns[5]).Value;
			}
			set
			{
				((ColumnCache<int>)base.Columns[5]).Value = (int)value;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06002611 RID: 9745 RVA: 0x00094080 File Offset: 0x00092280
		public bool IsActive
		{
			get
			{
				return this.EndTime.Year == DateTime.MaxValue.Year;
			}
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x000940AC File Offset: 0x000922AC
		public static PrimaryServerInfo Load(DataTableCursor cursor, DataTable dataTable)
		{
			PrimaryServerInfo primaryServerInfo = new PrimaryServerInfo(dataTable);
			primaryServerInfo.LoadFromCurrentRow(cursor);
			return primaryServerInfo;
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x000940C8 File Offset: 0x000922C8
		public static void CommitLazy(IEnumerable<PrimaryServerInfo> serversToCommit, DataTable dataTable)
		{
			using (DataTableCursor cursor = dataTable.GetCursor())
			{
				using (Transaction transaction = cursor.BeginTransaction())
				{
					foreach (PrimaryServerInfo primaryServerInfo in serversToCommit)
					{
						primaryServerInfo.MaterializeToCursor(transaction, cursor);
					}
					transaction.Commit(TransactionCommitMode.Lazy);
				}
			}
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x00094158 File Offset: 0x00092358
		public void Delete()
		{
			this.MarkToDelete();
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x00094160 File Offset: 0x00092360
		public new void Commit(TransactionCommitMode transactionCommitMode)
		{
			base.Commit(transactionCommitMode);
		}
	}
}
