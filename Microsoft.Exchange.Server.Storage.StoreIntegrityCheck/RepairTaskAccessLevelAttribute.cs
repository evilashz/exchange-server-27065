using System;

namespace Microsoft.Exchange.Server.Storage.StoreIntegrityCheck
{
	// Token: 0x0200002F RID: 47
	public class RepairTaskAccessLevelAttribute : Attribute
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00007D03 File Offset: 0x00005F03
		public RepairTaskAccessLevelAttribute(RepairTaskAccess access)
		{
			this.Access = access;
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00007D12 File Offset: 0x00005F12
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00007D1A File Offset: 0x00005F1A
		public RepairTaskAccess Access { get; private set; }
	}
}
