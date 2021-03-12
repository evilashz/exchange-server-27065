using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200020F RID: 527
	[Serializable]
	public enum RequestPriority
	{
		// Token: 0x04000B11 RID: 2833
		[LocDescription(MrsStrings.IDs.RequestPriorityLowest)]
		Lowest = 20,
		// Token: 0x04000B12 RID: 2834
		[LocDescription(MrsStrings.IDs.RequestPriorityLower)]
		Lower = 30,
		// Token: 0x04000B13 RID: 2835
		[LocDescription(MrsStrings.IDs.RequestPriorityLow)]
		Low = 40,
		// Token: 0x04000B14 RID: 2836
		[LocDescription(MrsStrings.IDs.RequestPriorityNormal)]
		Normal = 50,
		// Token: 0x04000B15 RID: 2837
		[LocDescription(MrsStrings.IDs.RequestPriorityHigh)]
		High = 60,
		// Token: 0x04000B16 RID: 2838
		[LocDescription(MrsStrings.IDs.RequestPriorityHigher)]
		Higher = 70,
		// Token: 0x04000B17 RID: 2839
		[LocDescription(MrsStrings.IDs.RequestPriorityHighest)]
		Highest = 80,
		// Token: 0x04000B18 RID: 2840
		[LocDescription(MrsStrings.IDs.RequestPriorityEmergency)]
		Emergency = 100
	}
}
