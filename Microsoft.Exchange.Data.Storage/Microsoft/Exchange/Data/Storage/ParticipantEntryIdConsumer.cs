using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000914 RID: 2324
	[Flags]
	internal enum ParticipantEntryIdConsumer
	{
		// Token: 0x04002E69 RID: 11881
		SupportsNone = 0,
		// Token: 0x04002E6A RID: 11882
		SupportsADParticipantEntryId = 1,
		// Token: 0x04002E6B RID: 11883
		SupportsStoreParticipantEntryIdForPDLs = 2,
		// Token: 0x04002E6C RID: 11884
		SupportsStoreParticipantEntryId = 6,
		// Token: 0x04002E6D RID: 11885
		SupportsWindowsAddressBookEnvelope = 8,
		// Token: 0x04002E6E RID: 11886
		CAI = 1,
		// Token: 0x04002E6F RID: 11887
		RecipientTablePrimary = 3,
		// Token: 0x04002E70 RID: 11888
		RecipientTableSecondary = 7,
		// Token: 0x04002E71 RID: 11889
		Rules = 7,
		// Token: 0x04002E72 RID: 11890
		DLMemberList = 15,
		// Token: 0x04002E73 RID: 11891
		DLOneOffList = 0,
		// Token: 0x04002E74 RID: 11892
		ContactEmailSlot = 9,
		// Token: 0x04002E75 RID: 11893
		ORAR = 0
	}
}
