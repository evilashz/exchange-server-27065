using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020002C4 RID: 708
	public struct WorkloadManagementTags
	{
		// Token: 0x040012F3 RID: 4851
		public const int Common = 0;

		// Token: 0x040012F4 RID: 4852
		public const int Execution = 1;

		// Token: 0x040012F5 RID: 4853
		public const int Scheduler = 2;

		// Token: 0x040012F6 RID: 4854
		public const int Policies = 3;

		// Token: 0x040012F7 RID: 4855
		public const int ActivityContext = 4;

		// Token: 0x040012F8 RID: 4856
		public const int UserWorkloadManager = 5;

		// Token: 0x040012F9 RID: 4857
		public const int FaultInjection = 6;

		// Token: 0x040012FA RID: 4858
		public const int AdmissionControl = 7;

		// Token: 0x040012FB RID: 4859
		public static Guid guid = new Guid("488b469c-d752-4650-8655-28590e044606");
	}
}
