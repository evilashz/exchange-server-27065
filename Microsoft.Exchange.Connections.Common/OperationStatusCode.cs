using System;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000019 RID: 25
	public enum OperationStatusCode
	{
		// Token: 0x04000056 RID: 86
		None,
		// Token: 0x04000057 RID: 87
		Success,
		// Token: 0x04000058 RID: 88
		ErrorInvalidCredentials,
		// Token: 0x04000059 RID: 89
		ErrorCannotCommunicateWithRemoteServer,
		// Token: 0x0400005A RID: 90
		ErrorInvalidRemoteServer,
		// Token: 0x0400005B RID: 91
		ErrorUnsupportedProtocolVersion
	}
}
