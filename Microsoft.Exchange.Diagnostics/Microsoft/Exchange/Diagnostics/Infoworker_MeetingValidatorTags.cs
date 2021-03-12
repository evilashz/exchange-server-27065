using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000255 RID: 597
	public struct Infoworker_MeetingValidatorTags
	{
		// Token: 0x04000F9F RID: 3999
		public const int Validator = 0;

		// Token: 0x04000FA0 RID: 4000
		public const int ConsistencyChecks = 1;

		// Token: 0x04000FA1 RID: 4001
		public const int CompareToAttendee = 2;

		// Token: 0x04000FA2 RID: 4002
		public const int CompareToOrganizer = 3;

		// Token: 0x04000FA3 RID: 4003
		public const int Fixup = 4;

		// Token: 0x04000FA4 RID: 4004
		public const int PFD = 5;

		// Token: 0x04000FA5 RID: 4005
		public static Guid guid = new Guid("7CCC3078-AE21-4CF6-B241-3EE7A8439681");
	}
}
