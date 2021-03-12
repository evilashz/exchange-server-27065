using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000204 RID: 516
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PropRow
	{
		// Token: 0x06000869 RID: 2153 RVA: 0x0002B724 File Offset: 0x00029924
		public PropRow() : this(10)
		{
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0002B72E File Offset: 0x0002992E
		public PropRow(int count)
		{
			this.properties = new List<PropValue>(count);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0002B742 File Offset: 0x00029942
		public PropRow(IList<PropValue> properties)
		{
			this.properties = properties;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0002B751 File Offset: 0x00029951
		public PropRow(SafeHandle row) : this(row, false)
		{
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0002B75B File Offset: 0x0002995B
		public PropRow(SafeHandle row, bool retainAnsiStrings)
		{
			this.properties = SRow.Unmarshal(row.DangerousGetHandle(), retainAnsiStrings);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0002B775 File Offset: 0x00029975
		public PropRow(IntPtr row)
		{
			this.properties = SRow.Unmarshal(row);
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600086F RID: 2159 RVA: 0x0002B789 File Offset: 0x00029989
		public IList<PropValue> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x0002B791 File Offset: 0x00029991
		// (set) Token: 0x06000871 RID: 2161 RVA: 0x0002B799 File Offset: 0x00029999
		public SafeHandle MarshalledPropertiesHandle
		{
			get
			{
				return this.marshalledPropertiesHandle;
			}
			set
			{
				this.marshalledPropertiesHandle = value;
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0002B7A2 File Offset: 0x000299A2
		public void Add(PropValue pv)
		{
			this.properties.Add(pv);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0002B7B0 File Offset: 0x000299B0
		public int GetBytesToMarshal()
		{
			return SRow.SizeOf;
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0002B7B8 File Offset: 0x000299B8
		public unsafe void MarshalToNative(SafeHandle row)
		{
			SRow* row2 = (SRow*)row.DangerousGetHandle().ToPointer();
			this.MarshalToNative(row2);
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0002B7DB File Offset: 0x000299DB
		internal unsafe void MarshalToNative(SRow* row)
		{
			row->cValues = this.properties.Count;
			row->lpProps = this.marshalledPropertiesHandle.DangerousGetHandle();
		}

		// Token: 0x040009FD RID: 2557
		private const int DefaultListSize = 10;

		// Token: 0x040009FE RID: 2558
		private readonly IList<PropValue> properties;

		// Token: 0x040009FF RID: 2559
		private SafeHandle marshalledPropertiesHandle;
	}
}
