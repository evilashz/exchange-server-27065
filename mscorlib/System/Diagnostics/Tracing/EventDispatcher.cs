using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000402 RID: 1026
	internal class EventDispatcher
	{
		// Token: 0x0600344E RID: 13390 RVA: 0x000CA66E File Offset: 0x000C886E
		internal EventDispatcher(EventDispatcher next, bool[] eventEnabled, EventListener listener)
		{
			this.m_Next = next;
			this.m_EventEnabled = eventEnabled;
			this.m_Listener = listener;
		}

		// Token: 0x04001707 RID: 5895
		internal readonly EventListener m_Listener;

		// Token: 0x04001708 RID: 5896
		internal bool[] m_EventEnabled;

		// Token: 0x04001709 RID: 5897
		internal bool m_activityFilteringEnabled;

		// Token: 0x0400170A RID: 5898
		internal EventDispatcher m_Next;
	}
}
