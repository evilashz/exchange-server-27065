using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200055A RID: 1370
	internal abstract class ReplicationEventBaseInfo
	{
		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x060030CC RID: 12492 RVA: 0x000C5B69 File Offset: 0x000C3D69
		public ReplicationEventType EventType
		{
			get
			{
				return this.m_eventType;
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x060030CD RID: 12493 RVA: 0x000C5B71 File Offset: 0x000C3D71
		public LocalizedString? BaseEventMessage
		{
			get
			{
				return this.m_BaseEventMessage;
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x060030CE RID: 12494 RVA: 0x000C5B79 File Offset: 0x000C3D79
		public bool ShouldBeRolledUp
		{
			get
			{
				return this.m_ShouldBeRolledUp;
			}
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x000C5B81 File Offset: 0x000C3D81
		public ReplicationEventBaseInfo(ReplicationEventType eventType, bool shouldBeRolledUp, LocalizedString? baseEventMessage)
		{
			this.m_eventType = eventType;
			this.m_ShouldBeRolledUp = shouldBeRolledUp;
			this.m_BaseEventMessage = baseEventMessage;
		}

		// Token: 0x040022A7 RID: 8871
		private readonly ReplicationEventType m_eventType;

		// Token: 0x040022A8 RID: 8872
		private readonly LocalizedString? m_BaseEventMessage;

		// Token: 0x040022A9 RID: 8873
		private readonly bool m_ShouldBeRolledUp;
	}
}
