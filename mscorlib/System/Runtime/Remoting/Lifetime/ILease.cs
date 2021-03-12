using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Lifetime
{
	// Token: 0x020007F1 RID: 2033
	[ComVisible(true)]
	public interface ILease
	{
		// Token: 0x060057FE RID: 22526
		[SecurityCritical]
		void Register(ISponsor obj, TimeSpan renewalTime);

		// Token: 0x060057FF RID: 22527
		[SecurityCritical]
		void Register(ISponsor obj);

		// Token: 0x06005800 RID: 22528
		[SecurityCritical]
		void Unregister(ISponsor obj);

		// Token: 0x06005801 RID: 22529
		[SecurityCritical]
		TimeSpan Renew(TimeSpan renewalTime);

		// Token: 0x17000EAE RID: 3758
		// (get) Token: 0x06005802 RID: 22530
		// (set) Token: 0x06005803 RID: 22531
		TimeSpan RenewOnCallTime { [SecurityCritical] get; [SecurityCritical] set; }

		// Token: 0x17000EAF RID: 3759
		// (get) Token: 0x06005804 RID: 22532
		// (set) Token: 0x06005805 RID: 22533
		TimeSpan SponsorshipTimeout { [SecurityCritical] get; [SecurityCritical] set; }

		// Token: 0x17000EB0 RID: 3760
		// (get) Token: 0x06005806 RID: 22534
		// (set) Token: 0x06005807 RID: 22535
		TimeSpan InitialLeaseTime { [SecurityCritical] get; [SecurityCritical] set; }

		// Token: 0x17000EB1 RID: 3761
		// (get) Token: 0x06005808 RID: 22536
		TimeSpan CurrentLeaseTime { [SecurityCritical] get; }

		// Token: 0x17000EB2 RID: 3762
		// (get) Token: 0x06005809 RID: 22537
		LeaseState CurrentState { [SecurityCritical] get; }
	}
}
