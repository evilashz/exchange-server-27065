using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Vista
{
	// Token: 0x020002B7 RID: 695
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public struct JET_RECSIZE : IEquatable<JET_RECSIZE>
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x00018D1F File Offset: 0x00016F1F
		// (set) Token: 0x06000C6B RID: 3179 RVA: 0x00018D27 File Offset: 0x00016F27
		public long cbData
		{
			[DebuggerStepThrough]
			get
			{
				return this.userData;
			}
			internal set
			{
				this.userData = value;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x00018D30 File Offset: 0x00016F30
		// (set) Token: 0x06000C6D RID: 3181 RVA: 0x00018D38 File Offset: 0x00016F38
		public long cbLongValueData
		{
			[DebuggerStepThrough]
			get
			{
				return this.userLongValueData;
			}
			internal set
			{
				this.userLongValueData = value;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x00018D41 File Offset: 0x00016F41
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x00018D49 File Offset: 0x00016F49
		public long cbOverhead
		{
			[DebuggerStepThrough]
			get
			{
				return this.overhead;
			}
			internal set
			{
				this.overhead = value;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00018D52 File Offset: 0x00016F52
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x00018D5A File Offset: 0x00016F5A
		public long cbLongValueOverhead
		{
			[DebuggerStepThrough]
			get
			{
				return this.longValueOverhead;
			}
			internal set
			{
				this.longValueOverhead = value;
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x00018D63 File Offset: 0x00016F63
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x00018D6B File Offset: 0x00016F6B
		public long cNonTaggedColumns
		{
			[DebuggerStepThrough]
			get
			{
				return this.numNonTaggedColumns;
			}
			internal set
			{
				this.numNonTaggedColumns = value;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x00018D74 File Offset: 0x00016F74
		// (set) Token: 0x06000C75 RID: 3189 RVA: 0x00018D7C File Offset: 0x00016F7C
		public long cTaggedColumns
		{
			[DebuggerStepThrough]
			get
			{
				return this.numTaggedColumns;
			}
			internal set
			{
				this.numTaggedColumns = value;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x00018D85 File Offset: 0x00016F85
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x00018D8D File Offset: 0x00016F8D
		public long cLongValues
		{
			[DebuggerStepThrough]
			get
			{
				return this.numLongValues;
			}
			internal set
			{
				this.numLongValues = value;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00018D96 File Offset: 0x00016F96
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x00018D9E File Offset: 0x00016F9E
		public long cMultiValues
		{
			[DebuggerStepThrough]
			get
			{
				return this.numMultiValues;
			}
			internal set
			{
				this.numMultiValues = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00018DA7 File Offset: 0x00016FA7
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x00018DAF File Offset: 0x00016FAF
		public long cCompressedColumns
		{
			[DebuggerStepThrough]
			get
			{
				return this.numCompressedColumns;
			}
			internal set
			{
				this.numCompressedColumns = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00018DB8 File Offset: 0x00016FB8
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x00018DC0 File Offset: 0x00016FC0
		public long cbDataCompressed
		{
			[DebuggerStepThrough]
			get
			{
				return this.userDataAfterCompression;
			}
			internal set
			{
				this.userDataAfterCompression = value;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00018DC9 File Offset: 0x00016FC9
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x00018DD1 File Offset: 0x00016FD1
		public long cbLongValueDataCompressed
		{
			[DebuggerStepThrough]
			get
			{
				return this.userLongValueDataCompressed;
			}
			internal set
			{
				this.userLongValueDataCompressed = value;
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00018DDC File Offset: 0x00016FDC
		public static JET_RECSIZE Add(JET_RECSIZE s1, JET_RECSIZE s2)
		{
			return checked(new JET_RECSIZE
			{
				cbData = s1.cbData + s2.cbData,
				cbDataCompressed = s1.cbDataCompressed + s2.cbDataCompressed,
				cbLongValueData = s1.cbLongValueData + s2.cbLongValueData,
				cbLongValueDataCompressed = s1.cbLongValueDataCompressed + s2.cbLongValueDataCompressed,
				cbLongValueOverhead = s1.cbLongValueOverhead + s2.cbLongValueOverhead,
				cbOverhead = s1.cbOverhead + s2.cbOverhead,
				cCompressedColumns = s1.cCompressedColumns + s2.cCompressedColumns,
				cLongValues = s1.cLongValues + s2.cLongValues,
				cMultiValues = s1.cMultiValues + s2.cMultiValues,
				cNonTaggedColumns = s1.cNonTaggedColumns + s2.cNonTaggedColumns,
				cTaggedColumns = s1.cTaggedColumns + s2.cTaggedColumns
			});
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00018EE4 File Offset: 0x000170E4
		public static JET_RECSIZE operator +(JET_RECSIZE left, JET_RECSIZE right)
		{
			return JET_RECSIZE.Add(left, right);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00018EF0 File Offset: 0x000170F0
		public static JET_RECSIZE Subtract(JET_RECSIZE s1, JET_RECSIZE s2)
		{
			return checked(new JET_RECSIZE
			{
				cbData = s1.cbData - s2.cbData,
				cbDataCompressed = s1.cbDataCompressed - s2.cbDataCompressed,
				cbLongValueData = s1.cbLongValueData - s2.cbLongValueData,
				cbLongValueDataCompressed = s1.cbLongValueDataCompressed - s2.cbLongValueDataCompressed,
				cbLongValueOverhead = s1.cbLongValueOverhead - s2.cbLongValueOverhead,
				cbOverhead = s1.cbOverhead - s2.cbOverhead,
				cCompressedColumns = s1.cCompressedColumns - s2.cCompressedColumns,
				cLongValues = s1.cLongValues - s2.cLongValues,
				cMultiValues = s1.cMultiValues - s2.cMultiValues,
				cNonTaggedColumns = s1.cNonTaggedColumns - s2.cNonTaggedColumns,
				cTaggedColumns = s1.cTaggedColumns - s2.cTaggedColumns
			});
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00018FF8 File Offset: 0x000171F8
		public static JET_RECSIZE operator -(JET_RECSIZE left, JET_RECSIZE right)
		{
			return JET_RECSIZE.Subtract(left, right);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x00019001 File Offset: 0x00017201
		public static bool operator ==(JET_RECSIZE lhs, JET_RECSIZE rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0001900B File Offset: 0x0001720B
		public static bool operator !=(JET_RECSIZE lhs, JET_RECSIZE rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x00019017 File Offset: 0x00017217
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_RECSIZE)obj);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00019048 File Offset: 0x00017248
		public override int GetHashCode()
		{
			long num = this.cbData ^ this.cbDataCompressed << 1 ^ this.cbLongValueData << 2 ^ this.cbDataCompressed << 3 ^ this.cbLongValueDataCompressed << 4 ^ this.cbOverhead << 5 ^ this.cbLongValueOverhead << 6 ^ this.cNonTaggedColumns << 7 ^ this.cTaggedColumns << 8 ^ this.cLongValues << 9 ^ this.cMultiValues << 10 ^ this.cCompressedColumns << 11;
			return (int)(num & (long)((ulong)-1)) ^ (int)(num >> 32);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x000190D0 File Offset: 0x000172D0
		public bool Equals(JET_RECSIZE other)
		{
			return this.cbData == other.cbData && this.cbLongValueData == other.cbLongValueData && this.cbOverhead == other.cbOverhead && this.cbLongValueOverhead == other.cbLongValueOverhead && this.cNonTaggedColumns == other.cNonTaggedColumns && this.cTaggedColumns == other.cTaggedColumns && this.cLongValues == other.cLongValues && this.cMultiValues == other.cMultiValues && this.cCompressedColumns == other.cCompressedColumns && this.cbDataCompressed == other.cbDataCompressed && this.cbLongValueDataCompressed == other.cbLongValueDataCompressed;
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0001918C File Offset: 0x0001738C
		internal void SetFromNativeRecsize(NATIVE_RECSIZE value)
		{
			checked
			{
				this.cbData = (long)value.cbData;
				this.cbDataCompressed = (long)value.cbData;
				this.cbLongValueData = (long)value.cbLongValueData;
				this.cbLongValueDataCompressed = (long)value.cbLongValueData;
				this.cbLongValueOverhead = (long)value.cbLongValueOverhead;
				this.cbOverhead = (long)value.cbOverhead;
				this.cCompressedColumns = 0L;
				this.cLongValues = (long)value.cLongValues;
				this.cMultiValues = (long)value.cMultiValues;
				this.cNonTaggedColumns = (long)value.cNonTaggedColumns;
				this.cTaggedColumns = (long)value.cTaggedColumns;
			}
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x00019230 File Offset: 0x00017430
		internal void SetFromNativeRecsize(NATIVE_RECSIZE2 value)
		{
			checked
			{
				this.cbData = (long)value.cbData;
				this.cbDataCompressed = (long)value.cbDataCompressed;
				this.cbLongValueData = (long)value.cbLongValueData;
				this.cbLongValueDataCompressed = (long)value.cbLongValueDataCompressed;
				this.cbLongValueOverhead = (long)value.cbLongValueOverhead;
				this.cbOverhead = (long)value.cbOverhead;
				this.cCompressedColumns = (long)value.cCompressedColumns;
				this.cLongValues = (long)value.cLongValues;
				this.cMultiValues = (long)value.cMultiValues;
				this.cNonTaggedColumns = (long)value.cNonTaggedColumns;
				this.cTaggedColumns = (long)value.cTaggedColumns;
			}
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x000192D8 File Offset: 0x000174D8
		internal NATIVE_RECSIZE GetNativeRecsize()
		{
			return new NATIVE_RECSIZE
			{
				cbData = (ulong)this.cbData,
				cbLongValueData = (ulong)this.cbLongValueData,
				cbLongValueOverhead = (ulong)this.cbLongValueOverhead,
				cbOverhead = (ulong)this.cbOverhead,
				cLongValues = (ulong)this.cLongValues,
				cMultiValues = (ulong)this.cMultiValues,
				cNonTaggedColumns = (ulong)this.cNonTaggedColumns,
				cTaggedColumns = (ulong)this.cTaggedColumns
			};
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x00019358 File Offset: 0x00017558
		internal NATIVE_RECSIZE2 GetNativeRecsize2()
		{
			return new NATIVE_RECSIZE2
			{
				cbData = (ulong)this.cbData,
				cbDataCompressed = (ulong)this.cbDataCompressed,
				cbLongValueData = (ulong)this.cbLongValueData,
				cbLongValueDataCompressed = (ulong)this.cbLongValueDataCompressed,
				cbLongValueOverhead = (ulong)this.cbLongValueOverhead,
				cbOverhead = (ulong)this.cbOverhead,
				cCompressedColumns = (ulong)this.cCompressedColumns,
				cLongValues = (ulong)this.cLongValues,
				cMultiValues = (ulong)this.cMultiValues,
				cNonTaggedColumns = (ulong)this.cNonTaggedColumns,
				cTaggedColumns = (ulong)this.cTaggedColumns
			};
		}

		// Token: 0x040007EE RID: 2030
		private long userData;

		// Token: 0x040007EF RID: 2031
		private long userLongValueData;

		// Token: 0x040007F0 RID: 2032
		private long overhead;

		// Token: 0x040007F1 RID: 2033
		private long longValueOverhead;

		// Token: 0x040007F2 RID: 2034
		private long numNonTaggedColumns;

		// Token: 0x040007F3 RID: 2035
		private long numTaggedColumns;

		// Token: 0x040007F4 RID: 2036
		private long numLongValues;

		// Token: 0x040007F5 RID: 2037
		private long numMultiValues;

		// Token: 0x040007F6 RID: 2038
		private long numCompressedColumns;

		// Token: 0x040007F7 RID: 2039
		private long userDataAfterCompression;

		// Token: 0x040007F8 RID: 2040
		private long userLongValueDataCompressed;
	}
}
