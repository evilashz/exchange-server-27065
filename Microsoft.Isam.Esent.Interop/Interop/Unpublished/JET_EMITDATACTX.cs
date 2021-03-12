using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000035 RID: 53
	public class JET_EMITDATACTX : IContentEquatable<JET_EMITDATACTX>, IDeepCloneable<JET_EMITDATACTX>
	{
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000A807 File Offset: 0x00008A07
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x0000A80F File Offset: 0x00008A0F
		public int dwVersion { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000A818 File Offset: 0x00008A18
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x0000A820 File Offset: 0x00008A20
		public ulong qwSequenceNum { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000A829 File Offset: 0x00008A29
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x0000A831 File Offset: 0x00008A31
		public ShadowLogEmitGrbit grbitOperationalFlags { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000A83A File Offset: 0x00008A3A
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x0000A842 File Offset: 0x00008A42
		public DateTime logtimeEmit { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000A84B File Offset: 0x00008A4B
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x0000A853 File Offset: 0x00008A53
		public JET_LGPOS lgposLogData { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000A85C File Offset: 0x00008A5C
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x0000A864 File Offset: 0x00008A64
		public long cbLogData { get; set; }

		// Token: 0x0600040E RID: 1038 RVA: 0x0000A870 File Offset: 0x00008A70
		public JET_EMITDATACTX DeepClone()
		{
			return (JET_EMITDATACTX)base.MemberwiseClone();
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000A88C File Offset: 0x00008A8C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_EMITDATACTX({0}:{1}:{2}:{3}:{4})", new object[]
			{
				this.dwVersion,
				this.qwSequenceNum,
				this.grbitOperationalFlags,
				this.lgposLogData,
				this.logtimeEmit
			});
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000A8F8 File Offset: 0x00008AF8
		public bool ContentEquals(JET_EMITDATACTX other)
		{
			return other != null && (this.dwVersion == other.dwVersion && this.qwSequenceNum == other.qwSequenceNum && this.grbitOperationalFlags == other.grbitOperationalFlags && this.logtimeEmit == other.logtimeEmit && this.lgposLogData == other.lgposLogData) && this.cbLogData == other.cbLogData;
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000A96C File Offset: 0x00008B6C
		internal NATIVE_EMITDATACTX GetNativeEmitdatactx()
		{
			return checked(new NATIVE_EMITDATACTX
			{
				cbStruct = (uint)Marshal.SizeOf(typeof(NATIVE_EMITDATACTX)),
				dwVersion = (uint)this.dwVersion,
				qwSequenceNum = this.qwSequenceNum,
				grbitOperationalFlags = (uint)this.grbitOperationalFlags,
				logtimeEmit = new JET_LOGTIME(this.logtimeEmit),
				lgposLogData = this.lgposLogData,
				cbLogData = (uint)this.cbLogData
			});
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000A9F0 File Offset: 0x00008BF0
		internal void SetFromNative(ref NATIVE_EMITDATACTX native)
		{
			this.dwVersion = (int)native.dwVersion;
			this.qwSequenceNum = native.qwSequenceNum;
			this.grbitOperationalFlags = (ShadowLogEmitGrbit)native.grbitOperationalFlags;
			this.logtimeEmit = (native.logtimeEmit.ToDateTime() ?? DateTime.MinValue);
			this.lgposLogData = native.lgposLogData;
			this.cbLogData = (long)((ulong)native.cbLogData);
		}
	}
}
