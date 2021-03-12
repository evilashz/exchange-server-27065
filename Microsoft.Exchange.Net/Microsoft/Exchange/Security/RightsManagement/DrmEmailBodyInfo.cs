using System;

namespace Microsoft.Exchange.Security.RightsManagement
{
	// Token: 0x02000985 RID: 2437
	public struct DrmEmailBodyInfo
	{
		// Token: 0x060034DE RID: 13534 RVA: 0x00085F78 File Offset: 0x00084178
		public DrmEmailBodyInfo(BodyFormat bodyFormat, int cpId)
		{
			this.bodyFormat = bodyFormat;
			this.cpId = cpId;
		}

		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x00085F88 File Offset: 0x00084188
		public BodyFormat BodyFormat
		{
			get
			{
				return this.bodyFormat;
			}
		}

		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x060034E0 RID: 13536 RVA: 0x00085F90 File Offset: 0x00084190
		public int CodePage
		{
			get
			{
				return this.cpId;
			}
		}

		// Token: 0x060034E1 RID: 13537 RVA: 0x00085F98 File Offset: 0x00084198
		public static bool operator ==(DrmEmailBodyInfo drmEmailBodyInfo1, DrmEmailBodyInfo drmEmailBodyInfo2)
		{
			return drmEmailBodyInfo1.Equals(drmEmailBodyInfo2);
		}

		// Token: 0x060034E2 RID: 13538 RVA: 0x00085FA2 File Offset: 0x000841A2
		public static bool operator !=(DrmEmailBodyInfo drmEmailBodyInfo1, DrmEmailBodyInfo drmEmailBodyInfo2)
		{
			return !drmEmailBodyInfo1.Equals(drmEmailBodyInfo2);
		}

		// Token: 0x060034E3 RID: 13539 RVA: 0x00085FAF File Offset: 0x000841AF
		public override int GetHashCode()
		{
			return this.cpId.GetHashCode() ^ this.bodyFormat.GetHashCode();
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x00085FCD File Offset: 0x000841CD
		public override bool Equals(object obj)
		{
			return obj is DrmEmailBodyInfo && this.Equals((DrmEmailBodyInfo)obj);
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x00085FE5 File Offset: 0x000841E5
		public bool Equals(DrmEmailBodyInfo other)
		{
			return this.bodyFormat == other.bodyFormat && this.cpId == other.cpId;
		}

		// Token: 0x04002CF8 RID: 11512
		private BodyFormat bodyFormat;

		// Token: 0x04002CF9 RID: 11513
		private int cpId;
	}
}
