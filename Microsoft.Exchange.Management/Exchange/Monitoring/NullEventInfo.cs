using System;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200055C RID: 1372
	internal sealed class NullEventInfo : ReplicationEventBaseInfo
	{
		// Token: 0x060030D3 RID: 12499 RVA: 0x000C5BC8 File Offset: 0x000C3DC8
		public NullEventInfo() : base(ReplicationEventType.Null, false, null)
		{
		}
	}
}
