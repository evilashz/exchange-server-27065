using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop.Unpublished
{
	// Token: 0x0200003C RID: 60
	[Serializable]
	public sealed class JET_LOGINFOMISC : IContentEquatable<JET_LOGINFOMISC>, IDeepCloneable<JET_LOGINFOMISC>
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000AA6B File Offset: 0x00008C6B
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x0000AA73 File Offset: 0x00008C73
		public int ulGeneration
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulGeneration;
			}
			internal set
			{
				this._ulGeneration = value;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000AA7C File Offset: 0x00008C7C
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x0000AA84 File Offset: 0x00008C84
		public JET_SIGNATURE signLog
		{
			[DebuggerStepThrough]
			get
			{
				return this._signLog;
			}
			internal set
			{
				this._signLog = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000AA8D File Offset: 0x00008C8D
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x0000AA95 File Offset: 0x00008C95
		public JET_LOGTIME logtimeCreate
		{
			[DebuggerStepThrough]
			get
			{
				return this._logtimeCreate;
			}
			internal set
			{
				this._logtimeCreate = value;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000AA9E File Offset: 0x00008C9E
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x0000AAA6 File Offset: 0x00008CA6
		public JET_LOGTIME logtimePreviousGeneration
		{
			[DebuggerStepThrough]
			get
			{
				return this._logtimePreviousGeneration;
			}
			internal set
			{
				this._logtimePreviousGeneration = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000AAAF File Offset: 0x00008CAF
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x0000AAB7 File Offset: 0x00008CB7
		public JET_LogInfoFlag ulFlags
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulFlags;
			}
			internal set
			{
				this._ulFlags = value;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000AAC0 File Offset: 0x00008CC0
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x0000AAC8 File Offset: 0x00008CC8
		public int ulVersionMajor
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulVersionMajor;
			}
			internal set
			{
				this._ulVersionMajor = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000AAD1 File Offset: 0x00008CD1
		// (set) Token: 0x06000421 RID: 1057 RVA: 0x0000AAD9 File Offset: 0x00008CD9
		public int ulVersionMinor
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulVersionMinor;
			}
			internal set
			{
				this._ulVersionMinor = value;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000AAE2 File Offset: 0x00008CE2
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x0000AAEA File Offset: 0x00008CEA
		public int ulVersionUpdate
		{
			[DebuggerStepThrough]
			get
			{
				return this._ulVersionUpdate;
			}
			internal set
			{
				this._ulVersionUpdate = value;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000AAF3 File Offset: 0x00008CF3
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x0000AAFB File Offset: 0x00008CFB
		public int cbSectorSize
		{
			[DebuggerStepThrough]
			get
			{
				return this._cbSectorSize;
			}
			internal set
			{
				this._cbSectorSize = value;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000AB04 File Offset: 0x00008D04
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x0000AB0C File Offset: 0x00008D0C
		public int cbHeader
		{
			[DebuggerStepThrough]
			get
			{
				return this._cbHeader;
			}
			internal set
			{
				this._cbHeader = value;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000AB15 File Offset: 0x00008D15
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000AB1D File Offset: 0x00008D1D
		public int cbFile
		{
			[DebuggerStepThrough]
			get
			{
				return this._cbFile;
			}
			internal set
			{
				this._cbFile = value;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000AB26 File Offset: 0x00008D26
		// (set) Token: 0x0600042B RID: 1067 RVA: 0x0000AB2E File Offset: 0x00008D2E
		public int cbDatabasePageSize
		{
			[DebuggerStepThrough]
			get
			{
				return this._cbDatabasePageSize;
			}
			internal set
			{
				this._cbDatabasePageSize = value;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000AB37 File Offset: 0x00008D37
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0000AB3F File Offset: 0x00008D3F
		public JET_LGPOS lgposCheckpoint
		{
			[DebuggerStepThrough]
			get
			{
				return this._lgposCheckpoint;
			}
			internal set
			{
				this._lgposCheckpoint = value;
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000AB48 File Offset: 0x00008D48
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_LOGINFOMISC({0})", new object[]
			{
				this._signLog
			});
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000AB7C File Offset: 0x00008D7C
		public bool ContentEquals(JET_LOGINFOMISC other)
		{
			return other != null && (this._ulGeneration == other._ulGeneration && this._signLog == other._signLog && this._logtimeCreate == other._logtimeCreate && this._logtimePreviousGeneration == other._logtimePreviousGeneration && this._ulFlags == other._ulFlags && this._ulVersionMajor == other._ulVersionMajor && this._ulVersionMinor == other._ulVersionMinor && this._ulVersionUpdate == other._ulVersionUpdate && this._cbSectorSize == other._cbSectorSize && this._cbHeader == other._cbHeader && this._cbFile == other._cbFile && this._cbDatabasePageSize == other._cbDatabasePageSize) && this._lgposCheckpoint == other._lgposCheckpoint;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000AC64 File Offset: 0x00008E64
		public JET_LOGINFOMISC DeepClone()
		{
			return (JET_LOGINFOMISC)base.MemberwiseClone();
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000AC80 File Offset: 0x00008E80
		internal void SetFromNativeLoginfoMisc(ref NATIVE_LOGINFOMISC native)
		{
			this._ulGeneration = (int)native.ulGeneration;
			this._signLog = new JET_SIGNATURE(native.signLog);
			this._logtimeCreate = native.logtimeCreate;
			this._logtimePreviousGeneration = native.logtimePreviousGeneration;
			this._ulFlags = native.ulFlags;
			this._ulVersionMajor = (int)native.ulVersionMajor;
			this._ulVersionMinor = (int)native.ulVersionMinor;
			this._ulVersionUpdate = (int)native.ulVersionUpdate;
			this._cbSectorSize = (int)native.cbSectorSize;
			this._cbHeader = (int)native.cbHeader;
			this._cbFile = (int)native.cbFile;
			this._cbDatabasePageSize = (int)native.cbDatabasePageSize;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000AD22 File Offset: 0x00008F22
		internal void SetFromNativeLoginfoMisc(ref NATIVE_LOGINFOMISC2 native)
		{
			this.SetFromNativeLoginfoMisc(ref native.loginfo);
			this._lgposCheckpoint = native.lgposCheckpoint;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000AD3C File Offset: 0x00008F3C
		internal NATIVE_LOGINFOMISC GetNativeLoginfomisc()
		{
			return new NATIVE_LOGINFOMISC
			{
				ulGeneration = (uint)this._ulGeneration,
				signLog = this._signLog.GetNativeSignature(),
				logtimeCreate = this._logtimeCreate,
				logtimePreviousGeneration = this._logtimePreviousGeneration,
				ulFlags = this._ulFlags,
				ulVersionMajor = (uint)this._ulVersionMajor,
				ulVersionMinor = (uint)this._ulVersionMinor,
				ulVersionUpdate = (uint)this._ulVersionUpdate,
				cbSectorSize = (uint)this._cbSectorSize,
				cbHeader = (uint)this._cbHeader,
				cbFile = (uint)this._cbFile,
				cbDatabasePageSize = (uint)this._cbDatabasePageSize
			};
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000ADF4 File Offset: 0x00008FF4
		internal NATIVE_LOGINFOMISC2 GetNativeLoginfomisc2()
		{
			return new NATIVE_LOGINFOMISC2
			{
				loginfo = this.GetNativeLoginfomisc(),
				lgposCheckpoint = this._lgposCheckpoint
			};
		}

		// Token: 0x04000132 RID: 306
		private int _ulGeneration;

		// Token: 0x04000133 RID: 307
		private JET_SIGNATURE _signLog;

		// Token: 0x04000134 RID: 308
		private JET_LOGTIME _logtimeCreate;

		// Token: 0x04000135 RID: 309
		private JET_LOGTIME _logtimePreviousGeneration;

		// Token: 0x04000136 RID: 310
		private JET_LogInfoFlag _ulFlags;

		// Token: 0x04000137 RID: 311
		private int _ulVersionMajor;

		// Token: 0x04000138 RID: 312
		private int _ulVersionMinor;

		// Token: 0x04000139 RID: 313
		private int _ulVersionUpdate;

		// Token: 0x0400013A RID: 314
		private int _cbSectorSize;

		// Token: 0x0400013B RID: 315
		private int _cbHeader;

		// Token: 0x0400013C RID: 316
		private int _cbFile;

		// Token: 0x0400013D RID: 317
		private int _cbDatabasePageSize;

		// Token: 0x0400013E RID: 318
		private JET_LGPOS _lgposCheckpoint;
	}
}
