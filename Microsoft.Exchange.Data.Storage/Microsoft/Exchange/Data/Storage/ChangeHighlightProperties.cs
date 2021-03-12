using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200020D RID: 525
	[Flags]
	internal enum ChangeHighlightProperties
	{
		// Token: 0x04000EFE RID: 3838
		None = 0,
		// Token: 0x04000EFF RID: 3839
		MapiStartTime = 1,
		// Token: 0x04000F00 RID: 3840
		MapiEndTime = 2,
		// Token: 0x04000F01 RID: 3841
		Duration = 3,
		// Token: 0x04000F02 RID: 3842
		RecurrenceProps = 4,
		// Token: 0x04000F03 RID: 3843
		Location = 8,
		// Token: 0x04000F04 RID: 3844
		Subject = 16,
		// Token: 0x04000F05 RID: 3845
		Recipients = 32,
		// Token: 0x04000F06 RID: 3846
		BodyProps = 128,
		// Token: 0x04000F07 RID: 3847
		BillMilesCompany = 256,
		// Token: 0x04000F08 RID: 3848
		IsSilent = 512,
		// Token: 0x04000F09 RID: 3849
		DisallowNewTimeProposal = 1024,
		// Token: 0x04000F0A RID: 3850
		NetMeetingProps = 2048,
		// Token: 0x04000F0B RID: 3851
		NetShowProps = 4096,
		// Token: 0x04000F0C RID: 3852
		OtherProps = 134217728,
		// Token: 0x04000F0D RID: 3853
		FullUpdateFlags = 7
	}
}
