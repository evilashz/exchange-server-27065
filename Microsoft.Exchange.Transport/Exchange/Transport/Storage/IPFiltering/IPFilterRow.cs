using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Storage.IPFiltering
{
	// Token: 0x0200012B RID: 299
	internal class IPFilterRow : DataRow
	{
		// Token: 0x06000D53 RID: 3411 RVA: 0x00030CCD File Offset: 0x0002EECD
		public IPFilterRow() : base(Database.Table)
		{
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00030CDA File Offset: 0x0002EEDA
		public IPFilterRow(int identity) : base(Database.Table)
		{
			this.Identity = identity;
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x00030CEE File Offset: 0x0002EEEE
		// (set) Token: 0x06000D56 RID: 3414 RVA: 0x00030D06 File Offset: 0x0002EF06
		public int Identity
		{
			get
			{
				return ((ColumnCache<int>)base.Columns[0]).Value;
			}
			set
			{
				((ColumnCache<int>)base.Columns[0]).Value = value;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x00030D1F File Offset: 0x0002EF1F
		// (set) Token: 0x06000D58 RID: 3416 RVA: 0x00030D2A File Offset: 0x0002EF2A
		public IPRange.Format Format
		{
			get
			{
				return (IPRange.Format)(this.TypeFlags & 15);
			}
			set
			{
				this.TypeFlags = (this.TypeFlags & -16 & (int)(value & (IPRange.Format)15));
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x00030D40 File Offset: 0x0002EF40
		// (set) Token: 0x06000D5A RID: 3418 RVA: 0x00030D50 File Offset: 0x0002EF50
		public PolicyType Policy
		{
			get
			{
				return (PolicyType)((this.TypeFlags & 240) >> 4);
			}
			set
			{
				this.TypeFlags = (this.TypeFlags & -241 & (int)((int)(value & (PolicyType)15) << 4));
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x00030D6B File Offset: 0x0002EF6B
		// (set) Token: 0x06000D5C RID: 3420 RVA: 0x00030D83 File Offset: 0x0002EF83
		public IPvxAddress LowerBound
		{
			get
			{
				return ((ColumnCache<IPvxAddress>)base.Columns[2]).Value;
			}
			set
			{
				((ColumnCache<IPvxAddress>)base.Columns[2]).Value = value;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x00030D9C File Offset: 0x0002EF9C
		// (set) Token: 0x06000D5E RID: 3422 RVA: 0x00030DB4 File Offset: 0x0002EFB4
		public IPvxAddress UpperBound
		{
			get
			{
				return ((ColumnCache<IPvxAddress>)base.Columns[3]).Value;
			}
			set
			{
				((ColumnCache<IPvxAddress>)base.Columns[3]).Value = value;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x00030DCD File Offset: 0x0002EFCD
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x00030DE5 File Offset: 0x0002EFE5
		public DateTime ExpiresOn
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

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000D61 RID: 3425 RVA: 0x00030DFE File Offset: 0x0002EFFE
		// (set) Token: 0x06000D62 RID: 3426 RVA: 0x00030E16 File Offset: 0x0002F016
		public string Comment
		{
			get
			{
				return ((ColumnCache<string>)base.Columns[5]).Value;
			}
			set
			{
				((ColumnCache<string>)base.Columns[5]).Value = value;
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000D63 RID: 3427 RVA: 0x00030E2F File Offset: 0x0002F02F
		// (set) Token: 0x06000D64 RID: 3428 RVA: 0x00030E47 File Offset: 0x0002F047
		internal int TypeFlags
		{
			get
			{
				return ((ColumnCache<int>)base.Columns[1]).Value;
			}
			set
			{
				((ColumnCache<int>)base.Columns[1]).Value = value;
			}
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x00030E60 File Offset: 0x0002F060
		public static IPFilterRow LoadFromRow(DataTableCursor cursor)
		{
			IPFilterRow ipfilterRow = new IPFilterRow();
			ipfilterRow.LoadFromCurrentRow(cursor);
			return ipfilterRow;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00030E7B File Offset: 0x0002F07B
		public void Commit(Transaction transaction, DataTableCursor cursor)
		{
			base.MaterializeToCursor(transaction, cursor);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x00030E85 File Offset: 0x0002F085
		public new void Commit()
		{
			base.Commit();
		}
	}
}
