using System;
using System.Collections.Generic;
using Microsoft.Exchange.Entities.DataModel.BirthdayCalendar;
using Microsoft.Exchange.Extensions;

namespace Microsoft.Exchange.Entities.BirthdayCalendar.EntitySets.BirthdayEventCommands
{
	// Token: 0x02000005 RID: 5
	internal class BirthdayEventCommandResult
	{
		// Token: 0x0600000F RID: 15 RVA: 0x0000226E File Offset: 0x0000046E
		public BirthdayEventCommandResult()
		{
			this.CreatedEvents = new List<IBirthdayEvent>();
			this.DeletedEvents = new List<IBirthdayEvent>();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000228C File Offset: 0x0000048C
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002294 File Offset: 0x00000494
		public IList<IBirthdayEvent> CreatedEvents { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000012 RID: 18 RVA: 0x0000229D File Offset: 0x0000049D
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000022A5 File Offset: 0x000004A5
		public IList<IBirthdayEvent> DeletedEvents { get; private set; }

		// Token: 0x06000014 RID: 20 RVA: 0x000022AE File Offset: 0x000004AE
		public void MergeWith(BirthdayEventCommandResult resultOf)
		{
			this.CreatedEvents.AddRange(resultOf.CreatedEvents);
			this.DeletedEvents.AddRange(resultOf.DeletedEvents);
		}
	}
}
