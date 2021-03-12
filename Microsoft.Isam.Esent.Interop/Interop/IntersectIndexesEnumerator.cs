using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000267 RID: 615
	internal sealed class IntersectIndexesEnumerator : TableEnumerator<byte[]>
	{
		// Token: 0x06000AB6 RID: 2742 RVA: 0x00015C4B File Offset: 0x00013E4B
		public IntersectIndexesEnumerator(JET_SESID sesid, JET_INDEXRANGE[] ranges) : base(sesid)
		{
			this.ranges = ranges;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00015C5B File Offset: 0x00013E5B
		protected override void OpenTable()
		{
			Api.JetIntersectIndexes(base.Sesid, this.ranges, this.ranges.Length, out this.recordlist, IntersectIndexesGrbit.None);
			base.TableidToEnumerate = this.recordlist.tableid;
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x00015C8E File Offset: 0x00013E8E
		protected override byte[] GetCurrent()
		{
			return Api.RetrieveColumn(base.Sesid, base.TableidToEnumerate, this.recordlist.columnidBookmark);
		}

		// Token: 0x04000454 RID: 1108
		private readonly JET_INDEXRANGE[] ranges;

		// Token: 0x04000455 RID: 1109
		private JET_RECORDLIST recordlist;
	}
}
