using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000290 RID: 656
	internal class EventDescriptionInformation : Attribute
	{
		// Token: 0x06001285 RID: 4741 RVA: 0x00055BB9 File Offset: 0x00053DB9
		public EventDescriptionInformation()
		{
			this.EventPriority = int.MaxValue;
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x00055BCC File Offset: 0x00053DCC
		// (set) Token: 0x06001287 RID: 4743 RVA: 0x00055BD4 File Offset: 0x00053DD4
		public bool IsTerminal { get; set; }

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x00055BDD File Offset: 0x00053DDD
		// (set) Token: 0x06001289 RID: 4745 RVA: 0x00055BE5 File Offset: 0x00053DE5
		public int EventPriority { get; set; }
	}
}
