using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002BD RID: 701
	public struct ForwardSyncTags
	{
		// Token: 0x040012D0 RID: 4816
		public const int ForwardSyncService = 0;

		// Token: 0x040012D1 RID: 4817
		public const int FaultInjection = 1;

		// Token: 0x040012D2 RID: 4818
		public const int MainStream = 2;

		// Token: 0x040012D3 RID: 4819
		public const int FullSyncStream = 3;

		// Token: 0x040012D4 RID: 4820
		public const int MsoSyncService = 4;

		// Token: 0x040012D5 RID: 4821
		public const int PowerShell = 5;

		// Token: 0x040012D6 RID: 4822
		public const int JobProcessor = 6;

		// Token: 0x040012D7 RID: 4823
		public const int RecipientWorkflow = 7;

		// Token: 0x040012D8 RID: 4824
		public const int OrganizationWorkflow = 8;

		// Token: 0x040012D9 RID: 4825
		public const int ProvisioningLicense = 9;

		// Token: 0x040012DA RID: 4826
		public const int UnifiedGroup = 10;

		// Token: 0x040012DB RID: 4827
		public static Guid guid = new Guid("8FAC856B-D0D4-4f7d-BBE9-B713EDFCBAAD");
	}
}
