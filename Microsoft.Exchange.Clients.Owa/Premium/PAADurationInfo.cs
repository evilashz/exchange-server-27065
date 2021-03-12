using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004AA RID: 1194
	[OwaEventStruct("pdi")]
	internal sealed class PAADurationInfo
	{
		// Token: 0x04001EF7 RID: 7927
		public const string StructNamespace = "pdi";

		// Token: 0x04001EF8 RID: 7928
		public const string StartTime = "st";

		// Token: 0x04001EF9 RID: 7929
		public const string EndTime = "end";

		// Token: 0x04001EFA RID: 7930
		public const string Days = "dys";

		// Token: 0x04001EFB RID: 7931
		public const string IsWorking = "fw";

		// Token: 0x04001EFC RID: 7932
		public const string IsNonWorking = "fnw";

		// Token: 0x04001EFD RID: 7933
		public const string IsCustom = "fc";

		// Token: 0x04001EFE RID: 7934
		[OwaEventField("st", true, 0)]
		public int StartTimeMinutes;

		// Token: 0x04001EFF RID: 7935
		[OwaEventField("end", true, 0)]
		public int EndTimeMinutes;

		// Token: 0x04001F00 RID: 7936
		[OwaEventField("dys", true, 0)]
		public int DaysOfWeek;

		// Token: 0x04001F01 RID: 7937
		[OwaEventField("fw", false, true)]
		public bool IsWorkingHours;

		// Token: 0x04001F02 RID: 7938
		[OwaEventField("fnw", false, false)]
		public bool IsNonWorkingHours;

		// Token: 0x04001F03 RID: 7939
		[OwaEventField("fc", false, false)]
		public bool IsCustomDuration;
	}
}
