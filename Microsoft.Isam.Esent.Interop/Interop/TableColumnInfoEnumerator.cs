using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002E9 RID: 745
	internal sealed class TableColumnInfoEnumerator : ColumnInfoEnumerator
	{
		// Token: 0x06000D99 RID: 3481 RVA: 0x0001B414 File Offset: 0x00019614
		public TableColumnInfoEnumerator(JET_SESID sesid, JET_DBID dbid, string tablename) : base(sesid)
		{
			this.dbid = dbid;
			this.tablename = tablename;
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x0001B42C File Offset: 0x0001962C
		protected override void OpenTable()
		{
			JET_COLUMNLIST columnlist;
			Api.JetGetColumnInfo(base.Sesid, this.dbid, this.tablename, string.Empty, out columnlist);
			base.Columnlist = columnlist;
			base.TableidToEnumerate = base.Columnlist.tableid;
		}

		// Token: 0x0400092F RID: 2351
		private readonly JET_DBID dbid;

		// Token: 0x04000930 RID: 2352
		private readonly string tablename;
	}
}
