using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000534 RID: 1332
	internal interface IReplicationCheck
	{
		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06002FBC RID: 12220
		string Title { get; }

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x06002FBD RID: 12221
		LocalizedString Description { get; }

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x06002FBE RID: 12222
		CheckCategory Category { get; }

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06002FBF RID: 12223
		IEventManager EventManager { get; }

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06002FC0 RID: 12224
		bool HasRun { get; }

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06002FC1 RID: 12225
		bool HasError { get; }

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06002FC2 RID: 12226
		bool HasPassed { get; }

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06002FC3 RID: 12227
		ReplicationCheckOutcome Outcome { get; }

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06002FC4 RID: 12228
		List<ReplicationCheckOutputObject> OutputObjects { get; }

		// Token: 0x06002FC5 RID: 12229
		void Run();

		// Token: 0x06002FC6 RID: 12230
		void Skip();

		// Token: 0x06002FC7 RID: 12231
		void LogEvents();
	}
}
