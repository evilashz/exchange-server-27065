using System;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000076 RID: 118
	internal class AmEvtBase
	{
		// Token: 0x060004F8 RID: 1272 RVA: 0x0001A924 File Offset: 0x00018B24
		internal AmEvtBase()
		{
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001A92C File Offset: 0x00018B2C
		internal bool Notify(bool isHighPriority)
		{
			AmTrace.Debug("Notifying system manager about event arrival: {0}", new object[]
			{
				this.ToString()
			});
			return AmSystemManager.Instance.EnqueueEvent(this, isHighPriority);
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001A962 File Offset: 0x00018B62
		internal bool Notify()
		{
			return this.Notify(false);
		}
	}
}
