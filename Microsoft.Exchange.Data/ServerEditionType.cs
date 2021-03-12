using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200019F RID: 415
	public enum ServerEditionType
	{
		// Token: 0x04000857 RID: 2135
		[LocDescription(DataStrings.IDs.UnknownEdition)]
		Unknown,
		// Token: 0x04000858 RID: 2136
		[LocDescription(DataStrings.IDs.StandardEdition)]
		Standard,
		// Token: 0x04000859 RID: 2137
		[LocDescription(DataStrings.IDs.StandardTrialEdition)]
		StandardEvaluation,
		// Token: 0x0400085A RID: 2138
		[LocDescription(DataStrings.IDs.EnterpriseEdition)]
		Enterprise,
		// Token: 0x0400085B RID: 2139
		[LocDescription(DataStrings.IDs.EnterpriseTrialEdition)]
		EnterpriseEvaluation,
		// Token: 0x0400085C RID: 2140
		[LocDescription(DataStrings.IDs.CoexistenceEdition)]
		Coexistence,
		// Token: 0x0400085D RID: 2141
		[LocDescription(DataStrings.IDs.CoexistenceTrialEdition)]
		CoexistenceEvaluation
	}
}
