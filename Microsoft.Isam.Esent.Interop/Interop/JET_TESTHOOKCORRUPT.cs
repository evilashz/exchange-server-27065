using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.Isam.Esent.Interop.Implementation;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000060 RID: 96
	[Serializable]
	public class JET_TESTHOOKCORRUPT : IContentEquatable<JET_TESTHOOKCORRUPT>, IDeepCloneable<JET_TESTHOOKCORRUPT>
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000B976 File Offset: 0x00009B76
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x0000B97E File Offset: 0x00009B7E
		public int grbit
		{
			[DebuggerStepThrough]
			get
			{
				return this.grbitOptions;
			}
			set
			{
				this.grbitOptions = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000B987 File Offset: 0x00009B87
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x0000B98F File Offset: 0x00009B8F
		public long pgnoTarget
		{
			[DebuggerStepThrough]
			get
			{
				return this.pgnoTargetPage;
			}
			set
			{
				this.pgnoTargetPage = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000B998 File Offset: 0x00009B98
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0000B9A0 File Offset: 0x00009BA0
		public long iSubTarget
		{
			[DebuggerStepThrough]
			get
			{
				return this.iSubTargetTarget;
			}
			set
			{
				this.iSubTargetTarget = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000B9A9 File Offset: 0x00009BA9
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0000B9B1 File Offset: 0x00009BB1
		public string szDatabaseFilePath
		{
			[DebuggerStepThrough]
			get
			{
				return this.databaseFilePath;
			}
			set
			{
				this.databaseFilePath = value;
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000B9BC File Offset: 0x00009BBC
		public bool ContentEquals(JET_TESTHOOKCORRUPT other)
		{
			return other != null && (this.grbitOptions == other.grbitOptions && this.pgnoTargetPage == other.pgnoTargetPage && this.iSubTargetTarget == other.iSubTargetTarget) && string.Equals(this.databaseFilePath, other.databaseFilePath);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000BA10 File Offset: 0x00009C10
		public JET_TESTHOOKCORRUPT DeepClone()
		{
			return (JET_TESTHOOKCORRUPT)base.MemberwiseClone();
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000BA2C File Offset: 0x00009C2C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_TESTHOOKCORRUPT({0}:{1})", new object[]
			{
				this.grbit,
				this.databaseFilePath,
				this.pgnoTargetPage,
				this.iSubTargetTarget
			});
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000BA84 File Offset: 0x00009C84
		internal NATIVE_TESTHOOKCORRUPT_DATABASEFILE GetNativeCorruptDatabaseFile(ref GCHandleCollection handles)
		{
			NATIVE_TESTHOOKCORRUPT_DATABASEFILE native_TESTHOOKCORRUPT_DATABASEFILE = default(NATIVE_TESTHOOKCORRUPT_DATABASEFILE);
			native_TESTHOOKCORRUPT_DATABASEFILE.cbStruct = checked((uint)Marshal.SizeOf(native_TESTHOOKCORRUPT_DATABASEFILE));
			native_TESTHOOKCORRUPT_DATABASEFILE.grbit = (uint)this.grbitOptions;
			native_TESTHOOKCORRUPT_DATABASEFILE.szDatabaseFilePath = handles.Add(Util.ConvertToNullTerminatedUnicodeByteArray(this.szDatabaseFilePath));
			native_TESTHOOKCORRUPT_DATABASEFILE.pgnoTarget = this.pgnoTarget;
			native_TESTHOOKCORRUPT_DATABASEFILE.iSubTarget = this.iSubTarget;
			return native_TESTHOOKCORRUPT_DATABASEFILE;
		}

		// Token: 0x040001E0 RID: 480
		private int grbitOptions;

		// Token: 0x040001E1 RID: 481
		private string databaseFilePath;

		// Token: 0x040001E2 RID: 482
		private long pgnoTargetPage;

		// Token: 0x040001E3 RID: 483
		private long iSubTargetTarget;
	}
}
