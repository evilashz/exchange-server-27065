using System;
using Microsoft.Exchange.Server.Storage.Common;

namespace Microsoft.Exchange.Server.Storage.RpcProxy
{
	// Token: 0x02000016 RID: 22
	// (Invoke) Token: 0x060000AC RID: 172
	internal delegate void OnPoolNotificationsReceivedCallback(Guid instanceId, int generation, ErrorCode errorCode, uint[] sessionHandles);
}
