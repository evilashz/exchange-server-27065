using System;

namespace Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence
{
	// Token: 0x02000070 RID: 112
	public class PatternedRecurrence
	{
		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000330 RID: 816 RVA: 0x000065D5 File Offset: 0x000047D5
		// (set) Token: 0x06000331 RID: 817 RVA: 0x000065DD File Offset: 0x000047DD
		public RecurrencePattern Pattern { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000332 RID: 818 RVA: 0x000065E6 File Offset: 0x000047E6
		// (set) Token: 0x06000333 RID: 819 RVA: 0x000065EE File Offset: 0x000047EE
		public RecurrenceRange Range { get; set; }
	}
}
