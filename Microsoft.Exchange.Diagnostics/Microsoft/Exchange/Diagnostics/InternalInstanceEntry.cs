using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200013B RID: 315
	public struct InternalInstanceEntry
	{
		// Token: 0x04000620 RID: 1568
		public int SpinLock;

		// Token: 0x04000621 RID: 1569
		public int InstanceNameHashCode;

		// Token: 0x04000622 RID: 1570
		public int InstanceNameOffset;

		// Token: 0x04000623 RID: 1571
		public int RefCount;

		// Token: 0x04000624 RID: 1572
		public int FirstCounterOffset;

		// Token: 0x04000625 RID: 1573
		public int NextInstanceOffset;
	}
}
