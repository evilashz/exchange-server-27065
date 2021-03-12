using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002EA RID: 746
	internal sealed class TableidColumnInfoEnumerator : ColumnInfoEnumerator
	{
		// Token: 0x06000D9B RID: 3483 RVA: 0x0001B46F File Offset: 0x0001966F
		public TableidColumnInfoEnumerator(JET_SESID sesid, JET_TABLEID tableid) : base(sesid)
		{
			this.tableid = tableid;
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0001B480 File Offset: 0x00019680
		protected override void OpenTable()
		{
			JET_COLUMNLIST columnlist;
			Api.JetGetTableColumnInfo(base.Sesid, this.tableid, string.Empty, out columnlist);
			base.Columnlist = columnlist;
			base.TableidToEnumerate = base.Columnlist.tableid;
		}

		// Token: 0x04000931 RID: 2353
		private readonly JET_TABLEID tableid;
	}
}
