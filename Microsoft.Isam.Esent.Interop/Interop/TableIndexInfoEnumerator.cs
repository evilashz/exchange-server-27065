using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002EC RID: 748
	internal sealed class TableIndexInfoEnumerator : IndexInfoEnumerator
	{
		// Token: 0x06000DA0 RID: 3488 RVA: 0x0001B520 File Offset: 0x00019720
		public TableIndexInfoEnumerator(JET_SESID sesid, JET_DBID dbid, string tablename) : base(sesid)
		{
			this.dbid = dbid;
			this.tablename = tablename;
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0001B538 File Offset: 0x00019738
		protected override void OpenTable()
		{
			JET_INDEXLIST indexlist;
			Api.JetGetIndexInfo(base.Sesid, this.dbid, this.tablename, string.Empty, out indexlist, JET_IdxInfo.List);
			base.Indexlist = indexlist;
			base.TableidToEnumerate = base.Indexlist.tableid;
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0001B57C File Offset: 0x0001977C
		protected override void GetIndexInfo(JET_SESID sesid, string indexname, out string result, JET_IdxInfo infoLevel)
		{
			Api.JetGetIndexInfo(sesid, this.dbid, this.tablename, indexname, out result, infoLevel);
		}

		// Token: 0x04000933 RID: 2355
		private readonly JET_DBID dbid;

		// Token: 0x04000934 RID: 2356
		private readonly string tablename;
	}
}
