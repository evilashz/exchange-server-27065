using System;
using System.Globalization;
using Microsoft.Isam.Esent.Interop.Implementation;

namespace Microsoft.Isam.Esent.Interop.Windows8
{
	// Token: 0x02000294 RID: 660
	public class JET_INDEX_RANGE
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x000174EC File Offset: 0x000156EC
		// (set) Token: 0x06000B87 RID: 2951 RVA: 0x000174F4 File Offset: 0x000156F4
		public JET_INDEX_COLUMN[] startColumns { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x000174FD File Offset: 0x000156FD
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x00017505 File Offset: 0x00015705
		public JET_INDEX_COLUMN[] endColumns { get; set; }

		// Token: 0x06000B8A RID: 2954 RVA: 0x0001750E File Offset: 0x0001570E
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_INDEX_RANGE", new object[0]);
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00017528 File Offset: 0x00015728
		internal NATIVE_INDEX_RANGE GetNativeIndexRange(ref GCHandleCollection handles)
		{
			NATIVE_INDEX_RANGE result = default(NATIVE_INDEX_RANGE);
			if (this.startColumns != null)
			{
				NATIVE_INDEX_COLUMN[] array = new NATIVE_INDEX_COLUMN[this.startColumns.Length];
				for (int i = 0; i < this.startColumns.Length; i++)
				{
					array[i] = this.startColumns[i].GetNativeIndexColumn(ref handles);
				}
				result.rgStartColumns = handles.Add(array);
				result.cStartColumns = (uint)this.startColumns.Length;
			}
			if (this.endColumns != null)
			{
				NATIVE_INDEX_COLUMN[] array = new NATIVE_INDEX_COLUMN[this.endColumns.Length];
				for (int j = 0; j < this.endColumns.Length; j++)
				{
					array[j] = this.endColumns[j].GetNativeIndexColumn(ref handles);
				}
				result.rgEndColumns = handles.Add(array);
				result.cEndColumns = (uint)this.endColumns.Length;
			}
			return result;
		}
	}
}
