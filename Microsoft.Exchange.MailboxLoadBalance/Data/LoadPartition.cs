using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.Data
{
	// Token: 0x02000049 RID: 73
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class LoadPartition
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x00009269 File Offset: 0x00007469
		public LoadPartition(LoadContainer root, string constraintSetIdentity)
		{
			this.Root = root;
			this.ConstraintSetIdentity = constraintSetIdentity;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000927F File Offset: 0x0000747F
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x00009287 File Offset: 0x00007487
		public LoadContainer Root { get; private set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00009290 File Offset: 0x00007490
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x00009298 File Offset: 0x00007498
		public string ConstraintSetIdentity { get; private set; }
	}
}
