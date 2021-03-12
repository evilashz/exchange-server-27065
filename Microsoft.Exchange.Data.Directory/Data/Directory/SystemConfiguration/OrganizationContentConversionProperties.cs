using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000645 RID: 1605
	[Serializable]
	public class OrganizationContentConversionProperties
	{
		// Token: 0x170018F2 RID: 6386
		// (get) Token: 0x06004B87 RID: 19335 RVA: 0x0011676B File Offset: 0x0011496B
		// (set) Token: 0x06004B88 RID: 19336 RVA: 0x00116773 File Offset: 0x00114973
		public int PreferredInternetCodePageForShiftJis
		{
			get
			{
				return this.preferredInternetCodePageForShiftJis;
			}
			internal set
			{
				this.preferredInternetCodePageForShiftJis = value;
			}
		}

		// Token: 0x170018F3 RID: 6387
		// (get) Token: 0x06004B89 RID: 19337 RVA: 0x0011677C File Offset: 0x0011497C
		// (set) Token: 0x06004B8A RID: 19338 RVA: 0x00116784 File Offset: 0x00114984
		public int RequiredCharsetCoverage
		{
			get
			{
				return this.requiredCharsetCoverage;
			}
			internal set
			{
				this.requiredCharsetCoverage = value;
			}
		}

		// Token: 0x170018F4 RID: 6388
		// (get) Token: 0x06004B8B RID: 19339 RVA: 0x0011678D File Offset: 0x0011498D
		// (set) Token: 0x06004B8C RID: 19340 RVA: 0x00116795 File Offset: 0x00114995
		public int ByteEncoderTypeFor7BitCharsets
		{
			get
			{
				return this.byteEncoderTypeFor7BitCharsets;
			}
			internal set
			{
				this.byteEncoderTypeFor7BitCharsets = value;
			}
		}

		// Token: 0x170018F5 RID: 6389
		// (get) Token: 0x06004B8D RID: 19341 RVA: 0x0011679E File Offset: 0x0011499E
		// (set) Token: 0x06004B8E RID: 19342 RVA: 0x001167A6 File Offset: 0x001149A6
		public bool ValidOrganization
		{
			get
			{
				return this.validOrganization;
			}
			internal set
			{
				this.validOrganization = value;
			}
		}

		// Token: 0x040033E2 RID: 13282
		private int preferredInternetCodePageForShiftJis;

		// Token: 0x040033E3 RID: 13283
		private int requiredCharsetCoverage;

		// Token: 0x040033E4 RID: 13284
		private int byteEncoderTypeFor7BitCharsets;

		// Token: 0x040033E5 RID: 13285
		private bool validOrganization = true;
	}
}
