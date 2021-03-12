using System;
using System.Globalization;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x020002BF RID: 703
	[Serializable]
	public class JET_RSTINFO : IContentEquatable<JET_RSTINFO>, IDeepCloneable<JET_RSTINFO>
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x000197D5 File Offset: 0x000179D5
		// (set) Token: 0x06000CB5 RID: 3253 RVA: 0x000197DD File Offset: 0x000179DD
		public JET_RSTMAP[] rgrstmap { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x000197E6 File Offset: 0x000179E6
		// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x000197EE File Offset: 0x000179EE
		public int crstmap { get; set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x000197F7 File Offset: 0x000179F7
		// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x000197FF File Offset: 0x000179FF
		public JET_LGPOS lgposStop { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00019808 File Offset: 0x00017A08
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x00019810 File Offset: 0x00017A10
		public JET_LOGTIME logtimeStop { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00019819 File Offset: 0x00017A19
		// (set) Token: 0x06000CBD RID: 3261 RVA: 0x00019821 File Offset: 0x00017A21
		public JET_PFNSTATUS pfnStatus { get; set; }

		// Token: 0x06000CBE RID: 3262 RVA: 0x0001982C File Offset: 0x00017A2C
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "JET_RSTINFO(crstmap={0})", new object[]
			{
				this.crstmap
			});
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00019860 File Offset: 0x00017A60
		public bool ContentEquals(JET_RSTINFO other)
		{
			if (other == null)
			{
				return false;
			}
			this.CheckMembersAreValid();
			other.CheckMembersAreValid();
			return this.crstmap == other.crstmap && this.lgposStop == other.lgposStop && this.logtimeStop == other.logtimeStop && this.pfnStatus == other.pfnStatus && Util.ArrayObjectContentEquals<JET_RSTMAP>(this.rgrstmap, other.rgrstmap, this.crstmap);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x000198E0 File Offset: 0x00017AE0
		public JET_RSTINFO DeepClone()
		{
			JET_RSTINFO jet_RSTINFO = (JET_RSTINFO)base.MemberwiseClone();
			jet_RSTINFO.rgrstmap = Util.DeepCloneArray<JET_RSTMAP>(this.rgrstmap);
			return jet_RSTINFO;
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x0001990C File Offset: 0x00017B0C
		internal void CheckMembersAreValid()
		{
			if (this.crstmap < 0)
			{
				throw new ArgumentOutOfRangeException("crstmap", this.crstmap, "cannot be negative");
			}
			if (this.rgrstmap == null && this.crstmap > 0)
			{
				throw new ArgumentOutOfRangeException("crstmap", this.crstmap, "must be zero");
			}
			if (this.rgrstmap != null && this.crstmap > this.rgrstmap.Length)
			{
				throw new ArgumentOutOfRangeException("crstmap", this.crstmap, "cannot be greater than the length of rgrstmap");
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x0001999C File Offset: 0x00017B9C
		internal NATIVE_RSTINFO GetNativeRstinfo()
		{
			this.CheckMembersAreValid();
			return new NATIVE_RSTINFO
			{
				cbStruct = (uint)NATIVE_RSTINFO.SizeOfRstinfo,
				crstmap = checked((uint)this.crstmap),
				lgposStop = this.lgposStop,
				logtimeStop = this.logtimeStop
			};
		}
	}
}
