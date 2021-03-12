using System;

namespace Microsoft.Exchange.EdgeSync.Validation
{
	// Token: 0x0200003D RID: 61
	[Serializable]
	public class CredentialRecord
	{
		// Token: 0x04000105 RID: 261
		public string TargetEdgeServerFQDN;

		// Token: 0x04000106 RID: 262
		public string ESRAUsername;

		// Token: 0x04000107 RID: 263
		public DateTime EffectiveDate;

		// Token: 0x04000108 RID: 264
		public TimeSpan Duration;

		// Token: 0x04000109 RID: 265
		public bool IsBootStrapAccount;
	}
}
