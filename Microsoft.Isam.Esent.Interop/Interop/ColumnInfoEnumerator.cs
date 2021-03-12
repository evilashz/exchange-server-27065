using System;
using System.Text;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200009D RID: 157
	internal abstract class ColumnInfoEnumerator : TableEnumerator<ColumnInfo>
	{
		// Token: 0x0600072F RID: 1839 RVA: 0x0001089D File Offset: 0x0000EA9D
		protected ColumnInfoEnumerator(JET_SESID sesid) : base(sesid)
		{
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x000108A6 File Offset: 0x0000EAA6
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x000108AE File Offset: 0x0000EAAE
		protected JET_COLUMNLIST Columnlist { get; set; }

		// Token: 0x06000732 RID: 1842 RVA: 0x000108B7 File Offset: 0x0000EAB7
		protected override ColumnInfo GetCurrent()
		{
			return ColumnInfoEnumerator.GetColumnInfoFromColumnlist(base.Sesid, this.Columnlist);
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x000108CC File Offset: 0x0000EACC
		private static ColumnInfo GetColumnInfoFromColumnlist(JET_SESID sesid, JET_COLUMNLIST columnlist)
		{
			Encoding encoding = EsentVersion.SupportsVistaFeatures ? Encoding.Unicode : LibraryHelpers.EncodingASCII;
			string text = Api.RetrieveColumnAsString(sesid, columnlist.tableid, columnlist.columnidcolumnname, encoding, RetrieveColumnGrbit.None);
			text = StringCache.TryToIntern(text);
			uint value = Api.RetrieveColumnAsUInt32(sesid, columnlist.tableid, columnlist.columnidcolumnid).Value;
			uint value2 = Api.RetrieveColumnAsUInt32(sesid, columnlist.tableid, columnlist.columnidcoltyp).Value;
			uint value3 = (uint)Api.RetrieveColumnAsUInt16(sesid, columnlist.tableid, columnlist.columnidCp).Value;
			uint value4 = Api.RetrieveColumnAsUInt32(sesid, columnlist.tableid, columnlist.columnidcbMax).Value;
			byte[] defaultValue = Api.RetrieveColumn(sesid, columnlist.tableid, columnlist.columnidDefault);
			uint value5 = Api.RetrieveColumnAsUInt32(sesid, columnlist.tableid, columnlist.columnidgrbit).Value;
			return new ColumnInfo(text, new JET_COLUMNID
			{
				Value = value
			}, (JET_coltyp)value2, (JET_CP)value3, (int)value4, defaultValue, (ColumndefGrbit)value5);
		}
	}
}
