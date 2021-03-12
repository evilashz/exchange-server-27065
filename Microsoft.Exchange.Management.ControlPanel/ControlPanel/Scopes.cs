using System;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200069F RID: 1695
	internal static class Scopes
	{
		// Token: 0x0400310E RID: 12558
		public const string ReadSelf = "@R:Self";

		// Token: 0x0400310F RID: 12559
		public const string WriteSelf = "@W:Self";

		// Token: 0x04003110 RID: 12560
		public const string ReadMyGAL = "@R:MyGAL";

		// Token: 0x04003111 RID: 12561
		public const string WriteMyGAL = "@W:MyGAL";

		// Token: 0x04003112 RID: 12562
		public const string ReadOrg = "@R:Organization";

		// Token: 0x04003113 RID: 12563
		public const string WriteOrg = "@W:Organization";

		// Token: 0x04003114 RID: 12564
		public const string WriteSelfOrOrg = "@W:Self|Organization";

		// Token: 0x04003115 RID: 12565
		public const string ReadMyDistributionGroups = "@R:MyDistributionGroups";

		// Token: 0x04003116 RID: 12566
		public const string WriteMyDistributionGroups = "@W:MyDistributionGroups";

		// Token: 0x04003117 RID: 12567
		public const string OrgConfig = "@C:OrganizationConfig";
	}
}
