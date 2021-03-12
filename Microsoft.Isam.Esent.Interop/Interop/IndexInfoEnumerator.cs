using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000261 RID: 609
	internal abstract class IndexInfoEnumerator : TableEnumerator<IndexInfo>
	{
		// Token: 0x06000A8A RID: 2698 RVA: 0x00015480 File Offset: 0x00013680
		protected IndexInfoEnumerator(JET_SESID sesid) : base(sesid)
		{
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x00015489 File Offset: 0x00013689
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x00015491 File Offset: 0x00013691
		protected JET_INDEXLIST Indexlist { get; set; }

		// Token: 0x06000A8D RID: 2701 RVA: 0x0001549A File Offset: 0x0001369A
		protected override IndexInfo GetCurrent()
		{
			return this.GetIndexInfoFromIndexlist(base.Sesid, this.Indexlist);
		}

		// Token: 0x06000A8E RID: 2702
		protected abstract void GetIndexInfo(JET_SESID sesid, string indexname, out string result, JET_IdxInfo infoLevel);

		// Token: 0x06000A8F RID: 2703 RVA: 0x000154B0 File Offset: 0x000136B0
		private static IndexSegment[] GetIndexSegmentsFromIndexlist(JET_SESID sesid, JET_INDEXLIST indexlist)
		{
			int value = Api.RetrieveColumnAsInt32(sesid, indexlist.tableid, indexlist.columnidcColumn).Value;
			Encoding encoding = EsentVersion.SupportsVistaFeatures ? Encoding.Unicode : LibraryHelpers.EncodingASCII;
			IndexSegment[] array = new IndexSegment[value];
			for (int i = 0; i < value; i++)
			{
				string text = Api.RetrieveColumnAsString(sesid, indexlist.tableid, indexlist.columnidcolumnname, encoding, RetrieveColumnGrbit.None);
				text = StringCache.TryToIntern(text);
				JET_coltyp value2 = (JET_coltyp)Api.RetrieveColumnAsInt32(sesid, indexlist.tableid, indexlist.columnidcoltyp).Value;
				IndexKeyGrbit value3 = (IndexKeyGrbit)Api.RetrieveColumnAsInt32(sesid, indexlist.tableid, indexlist.columnidgrbitColumn).Value;
				bool isAscending = IndexKeyGrbit.Ascending == value3;
				JET_CP value4 = (JET_CP)Api.RetrieveColumnAsInt16(sesid, indexlist.tableid, indexlist.columnidCp).Value;
				bool isASCII = JET_CP.ASCII == value4;
				array[i] = new IndexSegment(text, value2, isAscending, isASCII);
				if (i < value - 1)
				{
					Api.JetMove(sesid, indexlist.tableid, JET_Move.Next, MoveGrbit.None);
				}
			}
			return array;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x000155B4 File Offset: 0x000137B4
		private IndexInfo GetIndexInfoFromIndexlist(JET_SESID sesid, JET_INDEXLIST indexlist)
		{
			Encoding encoding = EsentVersion.SupportsVistaFeatures ? Encoding.Unicode : LibraryHelpers.EncodingASCII;
			string text = Api.RetrieveColumnAsString(sesid, indexlist.tableid, indexlist.columnidindexname, encoding, RetrieveColumnGrbit.None);
			text = StringCache.TryToIntern(text);
			CultureInfo cultureInfo;
			if (EsentVersion.SupportsWindows8Features)
			{
				string name;
				this.GetIndexInfo(sesid, text, out name, (JET_IdxInfo)14);
				cultureInfo = new CultureInfo(name);
			}
			else
			{
				int value = (int)Api.RetrieveColumnAsInt16(sesid, indexlist.tableid, indexlist.columnidLangid).Value;
				cultureInfo = LibraryHelpers.CreateCultureInfoByLcid(value);
			}
			uint value2 = Api.RetrieveColumnAsUInt32(sesid, indexlist.tableid, indexlist.columnidLCMapFlags).Value;
			CompareOptions compareOptions = Conversions.CompareOptionsFromLCMapFlags(value2);
			uint value3 = Api.RetrieveColumnAsUInt32(sesid, indexlist.tableid, indexlist.columnidgrbitIndex).Value;
			int value4 = Api.RetrieveColumnAsInt32(sesid, indexlist.tableid, indexlist.columnidcKey).Value;
			int value5 = Api.RetrieveColumnAsInt32(sesid, indexlist.tableid, indexlist.columnidcEntry).Value;
			int value6 = Api.RetrieveColumnAsInt32(sesid, indexlist.tableid, indexlist.columnidcPage).Value;
			IndexSegment[] indexSegmentsFromIndexlist = IndexInfoEnumerator.GetIndexSegmentsFromIndexlist(sesid, indexlist);
			return new IndexInfo(text, cultureInfo, compareOptions, indexSegmentsFromIndexlist, (CreateIndexGrbit)value3, value4, value5, value6);
		}
	}
}
