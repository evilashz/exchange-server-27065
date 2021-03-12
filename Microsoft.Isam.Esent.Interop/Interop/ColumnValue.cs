using System;
using System.Collections.Generic;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000093 RID: 147
	public abstract class ColumnValue
	{
		// Token: 0x060006D6 RID: 1750 RVA: 0x0000FC82 File Offset: 0x0000DE82
		protected ColumnValue()
		{
			this.ItagSequence = 1;
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0000FC91 File Offset: 0x0000DE91
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x0000FC99 File Offset: 0x0000DE99
		public JET_COLUMNID Columnid { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060006D9 RID: 1753
		public abstract object ValueAsObject { get; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0000FCA2 File Offset: 0x0000DEA2
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x0000FCAA File Offset: 0x0000DEAA
		public SetColumnGrbit SetGrbit { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0000FCB3 File Offset: 0x0000DEB3
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x0000FCBB File Offset: 0x0000DEBB
		public RetrieveColumnGrbit RetrieveGrbit { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0000FCC4 File Offset: 0x0000DEC4
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x0000FCCC File Offset: 0x0000DECC
		public int ItagSequence { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0000FCD5 File Offset: 0x0000DED5
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x0000FCDD File Offset: 0x0000DEDD
		public JET_wrn Error { get; internal set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060006E2 RID: 1762
		public abstract int Length { get; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060006E3 RID: 1763
		protected abstract int Size { get; }

		// Token: 0x060006E4 RID: 1764
		public abstract override string ToString();

		// Token: 0x060006E5 RID: 1765 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
		internal unsafe static void RetrieveColumns(JET_SESID sesid, JET_TABLEID tableid, ColumnValue[] columnValues)
		{
			if (columnValues.Length > 1024)
			{
				throw new ArgumentOutOfRangeException("columnValues", columnValues.Length, "Too many column values");
			}
			byte[] array = null;
			NATIVE_RETRIEVECOLUMN* ptr = stackalloc NATIVE_RETRIEVECOLUMN[checked(unchecked((UIntPtr)columnValues.Length) * (UIntPtr)sizeof(NATIVE_RETRIEVECOLUMN))];
			try
			{
				array = Caches.ColumnCache.Allocate();
				try
				{
					fixed (byte* ptr2 = array)
					{
						byte* ptr3 = ptr2;
						int num = columnValues.Length;
						for (int i = 0; i < columnValues.Length; i++)
						{
							if (columnValues[i].Size != 0)
							{
								columnValues[i].MakeNativeRetrieveColumn(ref ptr[i]);
								ptr[i].pvData = new IntPtr((void*)ptr3);
								ptr[i].cbData = checked((uint)columnValues[i].Size);
								ptr3 += ptr[i].cbData;
								num--;
							}
						}
						if (num > 0)
						{
							int num4;
							checked
							{
								int num2 = (int)(unchecked((long)(ptr3 - ptr2)));
								int num3 = array.Length - num2;
								num4 = num3 / num;
							}
							for (int j = 0; j < columnValues.Length; j++)
							{
								if (columnValues[j].Size == 0)
								{
									columnValues[j].MakeNativeRetrieveColumn(ref ptr[j]);
									ptr[j].pvData = new IntPtr((void*)ptr3);
									ptr[j].cbData = checked((uint)num4);
									ptr3 += ptr[j].cbData;
								}
							}
						}
						Api.Check(Api.Impl.JetRetrieveColumns(sesid, tableid, ptr, columnValues.Length));
						for (int k = 0; k < columnValues.Length; k++)
						{
							columnValues[k].Error = (JET_wrn)ptr[k].err;
							columnValues[k].ItagSequence = (int)ptr[k].itagSequence;
						}
						for (int l = 0; l < columnValues.Length; l++)
						{
							if (ptr[l].err != 1006)
							{
								byte* ptr4 = (byte*)((void*)ptr[l].pvData);
								int startIndex = checked((int)(unchecked((long)(ptr4 - ptr2))));
								columnValues[l].GetValueFromBytes(array, startIndex, checked((int)unchecked(ptr[l]).cbActual), ptr[l].err);
							}
						}
					}
				}
				finally
				{
					byte* ptr2 = null;
				}
				ColumnValue.RetrieveTruncatedBuffers(sesid, tableid, columnValues, ptr);
			}
			finally
			{
				if (array != null)
				{
					Caches.ColumnCache.Free(ref array);
				}
			}
		}

		// Token: 0x060006E6 RID: 1766
		internal unsafe abstract int SetColumns(JET_SESID sesid, JET_TABLEID tableid, ColumnValue[] columnValues, NATIVE_SETCOLUMN* nativeColumns, int i);

		// Token: 0x060006E7 RID: 1767 RVA: 0x0000FF94 File Offset: 0x0000E194
		internal unsafe int SetColumns(JET_SESID sesid, JET_TABLEID tableid, ColumnValue[] columnValues, NATIVE_SETCOLUMN* nativeColumns, int i, void* buffer, int bufferSize, bool hasValue)
		{
			this.MakeNativeSetColumn(ref nativeColumns[i]);
			if (hasValue)
			{
				nativeColumns[i].cbData = checked((uint)bufferSize);
				nativeColumns[i].pvData = new IntPtr(buffer);
				if (bufferSize == 0)
				{
					nativeColumns[i].grbit |= 32U;
				}
			}
			int result = (i == columnValues.Length - 1) ? Api.Impl.JetSetColumns(sesid, tableid, nativeColumns, columnValues.Length) : columnValues[i + 1].SetColumns(sesid, tableid, columnValues, nativeColumns, i + 1);
			this.Error = (JET_wrn)nativeColumns[i].err;
			return result;
		}

		// Token: 0x060006E8 RID: 1768
		protected abstract void GetValueFromBytes(byte[] value, int startIndex, int count, int err);

		// Token: 0x060006E9 RID: 1769 RVA: 0x00010050 File Offset: 0x0000E250
		private unsafe static void RetrieveTruncatedBuffers(JET_SESID sesid, JET_TABLEID tableid, IList<ColumnValue> columnValues, NATIVE_RETRIEVECOLUMN* nativeRetrievecolumns)
		{
			for (int i = 0; i < columnValues.Count; i++)
			{
				if (nativeRetrievecolumns[i].err == 1006)
				{
					byte[] array = new byte[nativeRetrievecolumns[i].cbActual];
					JET_RETINFO retinfo = new JET_RETINFO
					{
						itagSequence = columnValues[i].ItagSequence
					};
					int count;
					int num;
					fixed (byte* ptr = array)
					{
						num = Api.Impl.JetRetrieveColumn(sesid, tableid, columnValues[i].Columnid, new IntPtr((void*)ptr), array.Length, out count, columnValues[i].RetrieveGrbit, retinfo);
					}
					Api.Check(num);
					columnValues[i].Error = (JET_wrn)num;
					columnValues[i].GetValueFromBytes(array, 0, count, num);
				}
			}
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001013B File Offset: 0x0000E33B
		private void MakeNativeSetColumn(ref NATIVE_SETCOLUMN setcolumn)
		{
			setcolumn.columnid = this.Columnid.Value;
			setcolumn.grbit = (uint)this.SetGrbit;
			setcolumn.itagSequence = checked((uint)this.ItagSequence);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00010167 File Offset: 0x0000E367
		private void MakeNativeRetrieveColumn(ref NATIVE_RETRIEVECOLUMN retrievecolumn)
		{
			retrievecolumn.columnid = this.Columnid.Value;
			retrievecolumn.grbit = (uint)this.RetrieveGrbit;
			retrievecolumn.itagSequence = checked((uint)this.ItagSequence);
		}
	}
}
