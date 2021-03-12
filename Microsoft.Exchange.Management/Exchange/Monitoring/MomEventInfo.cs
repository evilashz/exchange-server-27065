using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200055B RID: 1371
	internal sealed class MomEventInfo : ReplicationEventBaseInfo
	{
		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x060030D0 RID: 12496 RVA: 0x000C5B9E File Offset: 0x000C3D9E
		public int MomEventId
		{
			get
			{
				return this.m_MomEventId;
			}
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x060030D1 RID: 12497 RVA: 0x000C5BA6 File Offset: 0x000C3DA6
		public EventTypeEnumeration MomEventType
		{
			get
			{
				return this.m_MomEventType;
			}
		}

		// Token: 0x060030D2 RID: 12498 RVA: 0x000C5BAE File Offset: 0x000C3DAE
		public MomEventInfo(int momEventId, EventTypeEnumeration momEventType, bool shouldBeRolledUp, LocalizedString? baseEventMessage) : base(ReplicationEventType.MOM, shouldBeRolledUp, baseEventMessage)
		{
			this.m_MomEventId = momEventId;
			this.m_MomEventType = momEventType;
		}

		// Token: 0x040022AA RID: 8874
		private readonly int m_MomEventId;

		// Token: 0x040022AB RID: 8875
		private readonly EventTypeEnumeration m_MomEventType;
	}
}
