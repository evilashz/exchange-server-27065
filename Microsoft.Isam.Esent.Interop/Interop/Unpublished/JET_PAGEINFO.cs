using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200003F RID: 63
	public class JET_PAGEINFO : IContentEquatable<JET_PAGEINFO>, IDeepCloneable<JET_PAGEINFO>
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000AE2C File Offset: 0x0000902C
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0000AE34 File Offset: 0x00009034
		public int pgno { [DebuggerStepThrough] get; set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000AE3D File Offset: 0x0000903D
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x0000AE45 File Offset: 0x00009045
		public bool fPageIsInitialized { [DebuggerStepThrough] get; internal set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000AE4E File Offset: 0x0000904E
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x0000AE56 File Offset: 0x00009056
		public bool fCorrectableError { [DebuggerStepThrough] get; internal set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000AE5F File Offset: 0x0000905F
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x0000AE67 File Offset: 0x00009067
		public long checksumActual { [DebuggerStepThrough] get; internal set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000AE70 File Offset: 0x00009070
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x0000AE78 File Offset: 0x00009078
		public long checksumExpected { [DebuggerStepThrough] get; internal set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000AE81 File Offset: 0x00009081
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x0000AE89 File Offset: 0x00009089
		public long dbtime { [DebuggerStepThrough] get; internal set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000AE92 File Offset: 0x00009092
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x0000AE9A File Offset: 0x0000909A
		public long structureChecksum { [DebuggerStepThrough] get; internal set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000AEA3 File Offset: 0x000090A3
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x0000AEAB File Offset: 0x000090AB
		public long flags { [DebuggerStepThrough] get; internal set; }

		// Token: 0x06000446 RID: 1094 RVA: 0x0000AEB4 File Offset: 0x000090B4
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_PAGEINFO({0}:{1})", new object[]
			{
				this.pgno,
				this.dbtime
			});
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0000AEF4 File Offset: 0x000090F4
		public bool ContentEquals(JET_PAGEINFO other)
		{
			if (other == null)
			{
				return false;
			}
			this.CheckMembersAreValid();
			other.CheckMembersAreValid();
			return this.pgno == other.pgno && this.fPageIsInitialized == other.fPageIsInitialized && this.fCorrectableError == other.fCorrectableError && this.checksumActual == other.checksumActual && this.checksumExpected == other.checksumExpected && this.dbtime == other.dbtime && this.structureChecksum == other.structureChecksum && this.flags == other.flags;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0000AF84 File Offset: 0x00009184
		public JET_PAGEINFO DeepClone()
		{
			return (JET_PAGEINFO)base.MemberwiseClone();
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0000AFA0 File Offset: 0x000091A0
		internal void CheckMembersAreValid()
		{
			if (this.pgno < 0)
			{
				throw new ArgumentOutOfRangeException("pgno", this.pgno, "cannot be negative");
			}
			if (this.dbtime < 0L)
			{
				throw new ArgumentOutOfRangeException("dbtime", this.dbtime, "cannot be negative");
			}
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000AFF8 File Offset: 0x000091F8
		internal NATIVE_PAGEINFO GetNativePageinfo()
		{
			this.CheckMembersAreValid();
			NATIVE_PAGEINFO result = default(NATIVE_PAGEINFO);
			BitVector32 bitVector = new BitVector32(0);
			int num = BitVector32.CreateMask();
			int bit = BitVector32.CreateMask(num);
			bitVector[num] = this.fPageIsInitialized;
			bitVector[bit] = this.fCorrectableError;
			checked
			{
				result.bitField = (uint)bitVector.Data;
				result.pgno = (uint)this.pgno;
				result.checksumActual = (ulong)this.checksumActual;
				result.checksumExpected = (ulong)this.checksumExpected;
				result.dbtime = (ulong)this.dbtime;
				result.structureChecksum = (ulong)this.structureChecksum;
				result.flags = (ulong)this.flags;
				return result;
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0000B0A4 File Offset: 0x000092A4
		internal void SetFromNativePageInfo(ref NATIVE_PAGEINFO native)
		{
			int num = BitVector32.CreateMask();
			int bit = BitVector32.CreateMask(num);
			checked
			{
				BitVector32 bitVector = new BitVector32((int)native.bitField);
				this.fPageIsInitialized = bitVector[num];
				this.fCorrectableError = bitVector[bit];
				this.pgno = (int)native.pgno;
				this.checksumActual = (long)native.checksumActual;
				this.checksumExpected = (long)native.checksumExpected;
				this.dbtime = (long)native.dbtime;
				this.structureChecksum = (long)native.structureChecksum;
				this.flags = (long)native.flags;
			}
		}
	}
}
