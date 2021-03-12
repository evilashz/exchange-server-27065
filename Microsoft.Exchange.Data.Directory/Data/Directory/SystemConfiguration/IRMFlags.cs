using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020006A9 RID: 1705
	[Flags]
	internal enum IRMFlags
	{
		// Token: 0x040035BD RID: 13757
		Empty = 0,
		// Token: 0x040035BE RID: 13758
		EncryptionEnabled = 1,
		// Token: 0x040035BF RID: 13759
		PrelicensingEnabled = 2,
		// Token: 0x040035C0 RID: 13760
		JournalReportDecryptionEnabled = 4,
		// Token: 0x040035C1 RID: 13761
		UseSharedRMS = 8,
		// Token: 0x040035C2 RID: 13762
		ExternalLicensingEnabled = 16,
		// Token: 0x040035C3 RID: 13763
		SearchEnabled = 32,
		// Token: 0x040035C4 RID: 13764
		ClientAccessServerEnabled = 64,
		// Token: 0x040035C5 RID: 13765
		TransportDecryptionOptional = 128,
		// Token: 0x040035C6 RID: 13766
		TransportDecryptionMandatory = 256,
		// Token: 0x040035C7 RID: 13767
		InternalLicensingEnabled = 512,
		// Token: 0x040035C8 RID: 13768
		InternetConfidentialEnabled = 1024,
		// Token: 0x040035C9 RID: 13769
		EDiscoverySuperUserDisabled = 2048,
		// Token: 0x040035CA RID: 13770
		All = 1012,
		// Token: 0x040035CB RID: 13771
		LastFlag = 8388608
	}
}
