using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000035 RID: 53
	internal interface IAutdStatusData
	{
		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000384 RID: 900
		// (set) Token: 0x06000385 RID: 901
		int? LastPingHeartbeat { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000386 RID: 902
		// (set) Token: 0x06000387 RID: 903
		Dictionary<string, PingCommand.DPFolderInfo> DPFolderList { get; set; }

		// Token: 0x06000388 RID: 904
		void SaveAndDispose();
	}
}
