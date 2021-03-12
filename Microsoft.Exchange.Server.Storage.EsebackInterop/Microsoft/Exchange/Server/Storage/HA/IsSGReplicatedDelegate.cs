using System;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x02000006 RID: 6
	// (Invoke) Token: 0x06000074 RID: 116
	internal unsafe delegate int IsSGReplicatedDelegate(_ESEBACK_CONTEXT* pContext, ulong jetinst, int* pfReplicated, uint cbSGGuid, ushort* wszSGGuid, uint* pcInfo, _LOGSHIP_INFO** prgInfo);
}
