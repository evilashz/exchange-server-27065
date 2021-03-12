using System;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	public sealed class JET_CHECKPOINTINFO : IEquatable<JET_CHECKPOINTINFO>
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000347 RID: 839 RVA: 0x00008CA8 File Offset: 0x00006EA8
		// (set) Token: 0x06000348 RID: 840 RVA: 0x00008CB0 File Offset: 0x00006EB0
		public int genMin
		{
			[DebuggerStepThrough]
			get
			{
				return this._genMin;
			}
			internal set
			{
				this._genMin = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00008CB9 File Offset: 0x00006EB9
		// (set) Token: 0x0600034A RID: 842 RVA: 0x00008CC1 File Offset: 0x00006EC1
		public int genMax
		{
			[DebuggerStepThrough]
			get
			{
				return this._genMax;
			}
			internal set
			{
				this._genMax = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00008CCA File Offset: 0x00006ECA
		// (set) Token: 0x0600034C RID: 844 RVA: 0x00008CD2 File Offset: 0x00006ED2
		public JET_LOGTIME logtimeGenMaxCreate
		{
			[DebuggerStepThrough]
			get
			{
				return this._logtimeGenMaxCreate;
			}
			internal set
			{
				this._logtimeGenMaxCreate = value;
			}
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00008CDC File Offset: 0x00006EDC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_CHECKPOINTINFO({0},{1},{2})", new object[]
			{
				this._genMin,
				this._genMax,
				this._logtimeGenMaxCreate
			});
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00008D2C File Offset: 0x00006F2C
		public override int GetHashCode()
		{
			int[] hashes = new int[]
			{
				this._genMin,
				this._genMax,
				this._logtimeGenMaxCreate.GetHashCode()
			};
			return Util.CalculateHashCode(hashes);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00008D6E File Offset: 0x00006F6E
		public override bool Equals(object obj)
		{
			return obj != null && !(base.GetType() != obj.GetType()) && this.Equals((JET_CHECKPOINTINFO)obj);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00008D94 File Offset: 0x00006F94
		public bool Equals(JET_CHECKPOINTINFO other)
		{
			return other != null && (this._genMin == other._genMin && this._genMax == other._genMax) && this._logtimeGenMaxCreate == other._logtimeGenMaxCreate;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00008DCA File Offset: 0x00006FCA
		internal void SetFromNativeCheckpointInfo(ref NATIVE_CHECKPOINTINFO native)
		{
			this._genMin = (int)native.genMin;
			this._genMax = (int)native.genMax;
			this._logtimeGenMaxCreate = native.logtimeGenMaxCreate;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00008DF0 File Offset: 0x00006FF0
		internal NATIVE_CHECKPOINTINFO GetNativeCheckpointinfo()
		{
			return new NATIVE_CHECKPOINTINFO
			{
				genMin = (uint)this._genMin,
				genMax = (uint)this._genMax,
				logtimeGenMaxCreate = this._logtimeGenMaxCreate
			};
		}

		// Token: 0x04000082 RID: 130
		private int _genMin;

		// Token: 0x04000083 RID: 131
		private int _genMax;

		// Token: 0x04000084 RID: 132
		private JET_LOGTIME _logtimeGenMaxCreate;
	}
}
