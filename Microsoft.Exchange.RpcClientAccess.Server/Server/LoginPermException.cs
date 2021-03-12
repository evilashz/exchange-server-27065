using System;

namespace Microsoft.Exchange.RpcClientAccess.Server
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	internal sealed class LoginPermException : RpcServerException
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x00004C44 File Offset: 0x00002E44
		internal LoginPermException(string message) : base(message, RpcErrorCode.LoginPerm)
		{
		}
	}
}
