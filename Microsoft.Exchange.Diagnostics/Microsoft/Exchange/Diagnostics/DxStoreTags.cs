using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200030D RID: 781
	public struct DxStoreTags
	{
		// Token: 0x040014C4 RID: 5316
		public const int Access = 0;

		// Token: 0x040014C5 RID: 5317
		public const int Manager = 1;

		// Token: 0x040014C6 RID: 5318
		public const int Instance = 2;

		// Token: 0x040014C7 RID: 5319
		public const int PaxosMessage = 3;

		// Token: 0x040014C8 RID: 5320
		public const int HealthChecker = 4;

		// Token: 0x040014C9 RID: 5321
		public const int StateMachine = 5;

		// Token: 0x040014CA RID: 5322
		public const int Truncator = 6;

		// Token: 0x040014CB RID: 5323
		public const int Snapshot = 7;

		// Token: 0x040014CC RID: 5324
		public const int LocalStore = 8;

		// Token: 0x040014CD RID: 5325
		public const int Utils = 9;

		// Token: 0x040014CE RID: 5326
		public const int Config = 10;

		// Token: 0x040014CF RID: 5327
		public const int Mesh = 11;

		// Token: 0x040014D0 RID: 5328
		public const int AccessClient = 12;

		// Token: 0x040014D1 RID: 5329
		public const int ManagerClient = 13;

		// Token: 0x040014D2 RID: 5330
		public const int InstanceClient = 14;

		// Token: 0x040014D3 RID: 5331
		public const int StoreRead = 15;

		// Token: 0x040014D4 RID: 5332
		public const int StoreWrite = 16;

		// Token: 0x040014D5 RID: 5333
		public const int AccessEntryPoint = 18;

		// Token: 0x040014D6 RID: 5334
		public const int ManagerEntryPoint = 19;

		// Token: 0x040014D7 RID: 5335
		public const int InstanceEntryPoint = 20;

		// Token: 0x040014D8 RID: 5336
		public const int RunOperation = 21;

		// Token: 0x040014D9 RID: 5337
		public const int CommitAck = 22;

		// Token: 0x040014DA RID: 5338
		public const int EventLogger = 23;

		// Token: 0x040014DB RID: 5339
		public static Guid guid = new Guid("{3C3F940E-234C-442E-A30B-A78F146F8C10}");
	}
}
