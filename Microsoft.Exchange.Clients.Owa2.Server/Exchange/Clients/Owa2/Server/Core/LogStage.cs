using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000235 RID: 565
	internal enum LogStage
	{
		// Token: 0x04000BB6 RID: 2998
		ReceiveRequest,
		// Token: 0x04000BB7 RID: 2999
		LoadItem,
		// Token: 0x04000BB8 RID: 3000
		RefreshClassifications,
		// Token: 0x04000BB9 RID: 3001
		LoadRules,
		// Token: 0x04000BBA RID: 3002
		EvaluateRules,
		// Token: 0x04000BBB RID: 3003
		LoadCustomStrings,
		// Token: 0x04000BBC RID: 3004
		SendResponse
	}
}
