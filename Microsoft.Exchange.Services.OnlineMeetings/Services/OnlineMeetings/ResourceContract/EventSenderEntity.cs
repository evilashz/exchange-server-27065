using System;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200008F RID: 143
	internal class EventSenderEntity : HyperReference
	{
		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000A565 File Offset: 0x00008765
		// (set) Token: 0x060003BA RID: 954 RVA: 0x0000A56D File Offset: 0x0000876D
		public Collection<EventEntity> Events
		{
			get
			{
				return this.allEvents;
			}
			set
			{
				this.allEvents = value;
			}
		}

		// Token: 0x04000292 RID: 658
		private Collection<EventEntity> allEvents = new Collection<EventEntity>();
	}
}
