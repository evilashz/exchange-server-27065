using System;
using System.Text;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002ED RID: 749
	internal sealed class TableNameEnumerator : TableEnumerator<string>
	{
		// Token: 0x06000DA3 RID: 3491 RVA: 0x0001B594 File Offset: 0x00019794
		public TableNameEnumerator(JET_SESID sesid, JET_DBID dbid) : base(sesid)
		{
			this.dbid = dbid;
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0001B5A4 File Offset: 0x000197A4
		protected override void OpenTable()
		{
			Api.JetGetObjectInfo(base.Sesid, this.dbid, out this.objectlist);
			base.TableidToEnumerate = this.objectlist.tableid;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0001B5D0 File Offset: 0x000197D0
		protected override bool SkipCurrent()
		{
			int value = Api.RetrieveColumnAsInt32(base.Sesid, base.TableidToEnumerate, this.objectlist.columnidflags).Value;
			return int.MinValue == (value & int.MinValue);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0001B610 File Offset: 0x00019810
		protected override string GetCurrent()
		{
			Encoding encoding = EsentVersion.SupportsVistaFeatures ? Encoding.Unicode : LibraryHelpers.EncodingASCII;
			string s = Api.RetrieveColumnAsString(base.Sesid, base.TableidToEnumerate, this.objectlist.columnidobjectname, encoding, RetrieveColumnGrbit.None);
			return StringCache.TryToIntern(s);
		}

		// Token: 0x04000935 RID: 2357
		private readonly JET_DBID dbid;

		// Token: 0x04000936 RID: 2358
		private JET_OBJECTLIST objectlist;
	}
}
