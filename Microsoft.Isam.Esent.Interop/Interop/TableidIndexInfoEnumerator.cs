using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002EB RID: 747
	internal sealed class TableidIndexInfoEnumerator : IndexInfoEnumerator
	{
		// Token: 0x06000D9D RID: 3485 RVA: 0x0001B4BD File Offset: 0x000196BD
		public TableidIndexInfoEnumerator(JET_SESID sesid, JET_TABLEID tableid) : base(sesid)
		{
			this.tableid = tableid;
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0001B4D0 File Offset: 0x000196D0
		protected override void OpenTable()
		{
			JET_INDEXLIST indexlist;
			Api.JetGetTableIndexInfo(base.Sesid, this.tableid, string.Empty, out indexlist, JET_IdxInfo.List);
			base.Indexlist = indexlist;
			base.TableidToEnumerate = base.Indexlist.tableid;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0001B50E File Offset: 0x0001970E
		protected override void GetIndexInfo(JET_SESID sesid, string indexname, out string result, JET_IdxInfo infoLevel)
		{
			Api.JetGetTableIndexInfo(sesid, this.tableid, indexname, out result, infoLevel);
		}

		// Token: 0x04000932 RID: 2354
		private readonly JET_TABLEID tableid;
	}
}
