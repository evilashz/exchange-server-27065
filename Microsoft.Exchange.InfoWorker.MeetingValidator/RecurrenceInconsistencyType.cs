using System;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000033 RID: 51
	internal enum RecurrenceInconsistencyType
	{
		// Token: 0x04000127 RID: 295
		None,
		// Token: 0x04000128 RID: 296
		MissingRecurrence,
		// Token: 0x04000129 RID: 297
		ExtraRecurrence,
		// Token: 0x0400012A RID: 298
		MissingModification,
		// Token: 0x0400012B RID: 299
		ExtraModification,
		// Token: 0x0400012C RID: 300
		MissingDeletion,
		// Token: 0x0400012D RID: 301
		ExtraDeletion
	}
}
