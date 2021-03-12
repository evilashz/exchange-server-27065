using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000205 RID: 517
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PropRowSet
	{
		// Token: 0x06000876 RID: 2166 RVA: 0x0002B7FF File Offset: 0x000299FF
		public PropRowSet() : this(10)
		{
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0002B809 File Offset: 0x00029A09
		public PropRowSet(int count)
		{
			this.rows = new List<PropRow>(count);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0002B81D File Offset: 0x00029A1D
		public PropRowSet(SafeHandle rowset) : this(rowset, false)
		{
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0002B828 File Offset: 0x00029A28
		public PropRowSet(SafeHandle rowset, bool retainAnsiStrings)
		{
			PropValue[][] array = SRowSet.Unmarshal(rowset, retainAnsiStrings);
			this.rows = new List<PropRow>(array.Length);
			foreach (PropValue[] properties in array)
			{
				this.rows.Add(new PropRow(properties));
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x0002B876 File Offset: 0x00029A76
		public IList<PropRow> Rows
		{
			get
			{
				return this.rows;
			}
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0002B87E File Offset: 0x00029A7E
		public void Add(PropRow row)
		{
			this.rows.Add(row);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0002B88C File Offset: 0x00029A8C
		public int GetBytesToMarshal()
		{
			return SRowSet.SizeOf + this.rows.Count * SRow.SizeOf;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0002B8A8 File Offset: 0x00029AA8
		public unsafe void MarshalToNative(SafeHandle rowset)
		{
			SRowSet* ptr = (SRowSet*)rowset.DangerousGetHandle().ToPointer();
			ptr->cRows = this.rows.Count;
			SRow* ptr2 = (SRow*)(ptr + SRowSet.DataOffset / sizeof(SRowSet));
			foreach (PropRow propRow in this.rows)
			{
				propRow.MarshalToNative(ptr2);
				ptr2++;
			}
		}

		// Token: 0x04000A00 RID: 2560
		private const int DefaultListSize = 10;

		// Token: 0x04000A01 RID: 2561
		private List<PropRow> rows;
	}
}
