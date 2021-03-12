using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x02000033 RID: 51
	public class JET_DBUTIL : IContentEquatable<JET_DBUTIL>, IDeepCloneable<JET_DBUTIL>
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00009EF9 File Offset: 0x000080F9
		// (set) Token: 0x060003CA RID: 970 RVA: 0x00009F01 File Offset: 0x00008101
		public JET_SESID sesid { [DebuggerStepThrough] get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00009F0A File Offset: 0x0000810A
		// (set) Token: 0x060003CC RID: 972 RVA: 0x00009F12 File Offset: 0x00008112
		public JET_DBID dbid { [DebuggerStepThrough] get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00009F1B File Offset: 0x0000811B
		// (set) Token: 0x060003CE RID: 974 RVA: 0x00009F23 File Offset: 0x00008123
		public JET_TABLEID tableid { [DebuggerStepThrough] get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00009F2C File Offset: 0x0000812C
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x00009F34 File Offset: 0x00008134
		public DBUTIL_OP op { [DebuggerStepThrough] get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x00009F3D File Offset: 0x0000813D
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x00009F45 File Offset: 0x00008145
		public EDBDUMP_OP edbdump { [DebuggerStepThrough] get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x00009F4E File Offset: 0x0000814E
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x00009F56 File Offset: 0x00008156
		public DbutilGrbit grbitOptions { [DebuggerStepThrough] get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00009F5F File Offset: 0x0000815F
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x00009F67 File Offset: 0x00008167
		public string szDatabase { [DebuggerStepThrough] get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x00009F70 File Offset: 0x00008170
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x00009F78 File Offset: 0x00008178
		public string szSLV { [DebuggerStepThrough] get; internal set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x00009F81 File Offset: 0x00008181
		// (set) Token: 0x060003DA RID: 986 RVA: 0x00009F89 File Offset: 0x00008189
		public string szBackup { [DebuggerStepThrough] get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060003DB RID: 987 RVA: 0x00009F92 File Offset: 0x00008192
		// (set) Token: 0x060003DC RID: 988 RVA: 0x00009F9A File Offset: 0x0000819A
		public string szTable { [DebuggerStepThrough] get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060003DD RID: 989 RVA: 0x00009FA3 File Offset: 0x000081A3
		// (set) Token: 0x060003DE RID: 990 RVA: 0x00009FAB File Offset: 0x000081AB
		public string szIndex { [DebuggerStepThrough] get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00009FB4 File Offset: 0x000081B4
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x00009FBC File Offset: 0x000081BC
		public string szIntegPrefix { [DebuggerStepThrough] get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x00009FC5 File Offset: 0x000081C5
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x00009FCD File Offset: 0x000081CD
		public int pgno { [DebuggerStepThrough] get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x00009FD6 File Offset: 0x000081D6
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x00009FDE File Offset: 0x000081DE
		public int iline { [DebuggerStepThrough] get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x00009FE7 File Offset: 0x000081E7
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x00009FEF File Offset: 0x000081EF
		public int lGeneration { [DebuggerStepThrough] get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x00009FF8 File Offset: 0x000081F8
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000A000 File Offset: 0x00008200
		public int isec { [DebuggerStepThrough] get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000A009 File Offset: 0x00008209
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000A011 File Offset: 0x00008211
		public int ib { [DebuggerStepThrough] get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000A01A File Offset: 0x0000821A
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000A022 File Offset: 0x00008222
		public int cRetry { [DebuggerStepThrough] get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000A02B File Offset: 0x0000822B
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000A033 File Offset: 0x00008233
		public IntPtr pfnCallback { [DebuggerStepThrough] get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000A03C File Offset: 0x0000823C
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000A044 File Offset: 0x00008244
		public IntPtr pvCallback { [DebuggerStepThrough] get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000A04D File Offset: 0x0000824D
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000A055 File Offset: 0x00008255
		public string szLog { [DebuggerStepThrough] get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000A05E File Offset: 0x0000825E
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0000A066 File Offset: 0x00008266
		public string szBase { [DebuggerStepThrough] get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000A06F File Offset: 0x0000826F
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0000A077 File Offset: 0x00008277
		public byte[] pvBuffer { [DebuggerStepThrough] get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000A080 File Offset: 0x00008280
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0000A088 File Offset: 0x00008288
		public int cbBuffer { [DebuggerStepThrough] get; set; }

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000A094 File Offset: 0x00008294
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_DBUTIL({0})", new object[]
			{
				this.op
			});
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000A0C8 File Offset: 0x000082C8
		public bool ContentEquals(JET_DBUTIL other)
		{
			if (other == null)
			{
				return false;
			}
			this.CheckMembersAreValid();
			other.CheckMembersAreValid();
			return this.sesid == other.sesid && this.dbid == other.dbid && this.tableid == other.tableid && this.op == other.op && this.edbdump == other.edbdump && this.grbitOptions == other.grbitOptions && this.szDatabase == other.szDatabase && this.szSLV == other.szSLV && this.szBackup == other.szBackup && this.szTable == other.szTable && this.szIndex == other.szIndex && this.szIntegPrefix == other.szIntegPrefix && this.pgno == other.pgno && this.iline == other.iline && this.lGeneration == other.lGeneration && this.isec == other.isec && this.ib == other.ib && this.cRetry == other.cRetry && this.pfnCallback == other.pfnCallback && this.pvCallback == other.pvCallback && this.szLog == other.szLog && this.szBase == other.szBase && this.cbBuffer == other.cbBuffer && Util.ArrayEqual(this.pvBuffer, other.pvBuffer, 0, this.cbBuffer);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000A2B8 File Offset: 0x000084B8
		public JET_DBUTIL DeepClone()
		{
			JET_DBUTIL jet_DBUTIL = (JET_DBUTIL)base.MemberwiseClone();
			if (this.pvBuffer != null)
			{
				jet_DBUTIL.pvBuffer = new byte[this.pvBuffer.Length];
				Array.Copy(this.pvBuffer, jet_DBUTIL.pvBuffer, this.pvBuffer.Length);
			}
			return jet_DBUTIL;
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000A308 File Offset: 0x00008508
		internal void SetFromNative(ref NATIVE_DBUTIL_LEGACY native)
		{
			this.sesid = new JET_SESID
			{
				Value = native.sesid
			};
			this.dbid = new JET_DBID
			{
				Value = native.dbid
			};
			this.tableid = new JET_TABLEID
			{
				Value = native.tableid
			};
			this.op = native.op;
			this.edbdump = native.edbdump;
			this.grbitOptions = native.grbitOptions;
			this.szDatabase = native.szDatabase;
			this.szSLV = native.szSLV;
			this.szBackup = native.szBackup;
			this.szTable = native.szTable;
			this.szIndex = native.szIndex;
			this.szIntegPrefix = native.szIntegPrefix;
			this.pgno = native.pgno;
			this.iline = native.iline;
			this.lGeneration = native.lGeneration;
			this.isec = native.isec;
			this.ib = native.ib;
			this.cRetry = native.cRetry;
			this.pfnCallback = native.pfnCallback;
			this.pvCallback = native.pvCallback;
			this.szLog = null;
			this.szBase = null;
			this.pvBuffer = null;
			this.cbBuffer = 0;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000A454 File Offset: 0x00008654
		internal void SetFromNative(ref NATIVE_DBUTIL_CHECKSUMLOG native)
		{
			this.sesid = new JET_SESID
			{
				Value = native.sesid
			};
			this.dbid = new JET_DBID
			{
				Value = native.dbid
			};
			this.tableid = new JET_TABLEID
			{
				Value = native.tableid
			};
			this.op = native.op;
			this.edbdump = native.edbdump;
			this.grbitOptions = native.grbitOptions;
			this.szDatabase = null;
			this.szSLV = null;
			this.szBackup = null;
			this.szTable = null;
			this.szIndex = null;
			this.szIntegPrefix = null;
			this.pgno = 0;
			this.iline = 0;
			this.lGeneration = 0;
			this.isec = 0;
			this.ib = 0;
			this.cRetry = 0;
			this.pfnCallback = IntPtr.Zero;
			this.pvCallback = IntPtr.Zero;
			this.szLog = native.szLog;
			this.szBase = native.szBase;
			this.pvBuffer = null;
			this.cbBuffer = native.cbBuffer;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000A570 File Offset: 0x00008770
		internal NATIVE_DBUTIL_LEGACY GetNativeDbutilLegacy()
		{
			return new NATIVE_DBUTIL_LEGACY
			{
				cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_DBUTIL_LEGACY))),
				sesid = this.sesid.Value,
				dbid = this.dbid.Value,
				tableid = this.tableid.Value,
				op = this.op,
				edbdump = this.edbdump,
				grbitOptions = this.grbitOptions,
				szDatabase = this.szDatabase,
				szSLV = this.szSLV,
				szBackup = this.szBackup,
				szTable = this.szTable,
				szIndex = this.szIndex,
				szIntegPrefix = this.szIntegPrefix,
				pgno = this.pgno,
				iline = this.iline,
				lGeneration = this.lGeneration,
				isec = this.isec,
				ib = this.ib,
				cRetry = this.cRetry,
				pfnCallback = this.pfnCallback,
				pvCallback = this.pvCallback
			};
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000A6B0 File Offset: 0x000088B0
		internal NATIVE_DBUTIL_CHECKSUMLOG GetNativeDbutilChecksumLog()
		{
			return new NATIVE_DBUTIL_CHECKSUMLOG
			{
				cbStruct = checked((uint)Marshal.SizeOf(typeof(NATIVE_DBUTIL_CHECKSUMLOG))),
				sesid = this.sesid.Value,
				dbid = this.dbid.Value,
				tableid = this.tableid.Value,
				op = this.op,
				edbdump = this.edbdump,
				grbitOptions = this.grbitOptions,
				szLog = this.szLog,
				szBase = this.szBase,
				pvBuffer = IntPtr.Zero,
				cbBuffer = this.cbBuffer
			};
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000A770 File Offset: 0x00008970
		internal void CheckMembersAreValid()
		{
			if (this.cbBuffer < 0)
			{
				throw new ArgumentOutOfRangeException("cbBuffer", this.cbBuffer, "cannot be negative");
			}
			if (this.pvBuffer == null && this.cbBuffer != 0)
			{
				throw new ArgumentOutOfRangeException("cbBuffer", this.cbBuffer, "must be 0");
			}
			if (this.pvBuffer != null && this.cbBuffer > this.pvBuffer.Length)
			{
				throw new ArgumentOutOfRangeException("cbBuffer", this.cbBuffer, "can't be greater than pvBuffer.Length");
			}
		}
	}
}
