using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002F7 RID: 759
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct JET_INDEXID : IEquatable<JET_INDEXID>
	{
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0001C194 File Offset: 0x0001A394
		internal static uint SizeOfIndexId
		{
			[DebuggerStepThrough]
			get
			{
				return JET_INDEXID.TheSizeOfIndexId;
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0001C19B File Offset: 0x0001A39B
		public static bool operator ==(JET_INDEXID lhs, JET_INDEXID rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0001C1A5 File Offset: 0x0001A3A5
		public static bool operator !=(JET_INDEXID lhs, JET_INDEXID rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0001C1B1 File Offset: 0x0001A3B1
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_INDEXID)obj);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0001C1E4 File Offset: 0x0001A3E4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_INDEXID(0x{0:x}:0x{1:x}:0x{2:x})", new object[]
			{
				this.IndexId1,
				this.IndexId2,
				this.IndexId3
			});
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0001C232 File Offset: 0x0001A432
		public override int GetHashCode()
		{
			return this.CbStruct.GetHashCode() ^ this.IndexId1.GetHashCode() ^ this.IndexId2.GetHashCode() ^ this.IndexId3.GetHashCode();
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x0001C26C File Offset: 0x0001A46C
		public bool Equals(JET_INDEXID other)
		{
			return this.CbStruct == other.CbStruct && this.IndexId1 == other.IndexId1 && this.IndexId2 == other.IndexId2 && this.IndexId3 == other.IndexId3;
		}

		// Token: 0x04000941 RID: 2369
		internal uint CbStruct;

		// Token: 0x04000942 RID: 2370
		internal IntPtr IndexId1;

		// Token: 0x04000943 RID: 2371
		internal uint IndexId2;

		// Token: 0x04000944 RID: 2372
		internal uint IndexId3;

		// Token: 0x04000945 RID: 2373
		private static readonly uint TheSizeOfIndexId = (uint)Marshal.SizeOf(typeof(JET_INDEXID));
	}
}
