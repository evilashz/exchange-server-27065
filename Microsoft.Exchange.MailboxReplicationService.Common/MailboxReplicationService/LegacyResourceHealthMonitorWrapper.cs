using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000143 RID: 323
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LegacyResourceHealthMonitorWrapper : ResourceHealthMonitorWrapper, ILegacyResourceHealthProvider
	{
		// Token: 0x06000ACF RID: 2767 RVA: 0x00015307 File Offset: 0x00013507
		public LegacyResourceHealthMonitorWrapper(LegacyResourceHealthMonitor provider) : base(provider)
		{
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00015310 File Offset: 0x00013510
		public void Update(ConstraintCheckResultType constraintResult, ConstraintCheckAgent agent, LocalizedString failureReason)
		{
			base.CheckExpired();
			LegacyResourceHealthMonitor wrappedMonitor = base.GetWrappedMonitor<LegacyResourceHealthMonitor>();
			wrappedMonitor.Update(constraintResult, agent, failureReason);
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000AD1 RID: 2769 RVA: 0x00015333 File Offset: 0x00013533
		public ConstraintCheckResultType ConstraintResult
		{
			get
			{
				return base.GetWrappedMonitor<LegacyResourceHealthMonitor>().ConstraintResult;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x00015340 File Offset: 0x00013540
		public ConstraintCheckAgent Agent
		{
			get
			{
				return base.GetWrappedMonitor<LegacyResourceHealthMonitor>().Agent;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000AD3 RID: 2771 RVA: 0x0001534D File Offset: 0x0001354D
		public LocalizedString FailureReason
		{
			get
			{
				return base.GetWrappedMonitor<LegacyResourceHealthMonitor>().FailureReason;
			}
		}
	}
}
