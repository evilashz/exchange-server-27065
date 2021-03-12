using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000142 RID: 322
	internal class LegacyResourceHealthMonitor : CacheableResourceHealthMonitor, ILegacyResourceHealthProvider
	{
		// Token: 0x06000AC7 RID: 2759 RVA: 0x000151B8 File Offset: 0x000133B8
		internal LegacyResourceHealthMonitor(ResourceKey key) : base(key)
		{
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x000151C1 File Offset: 0x000133C1
		protected override int InternalMetricValue
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x000151C4 File Offset: 0x000133C4
		public override ResourceHealthMonitorWrapper CreateWrapper()
		{
			return new LegacyResourceHealthMonitorWrapper(this);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000151CC File Offset: 0x000133CC
		public override ResourceLoad GetResourceLoad(WorkloadClassification classification, bool raw = false, object optionalData = null)
		{
			this.LastAccessUtc = DateTime.UtcNow;
			ResourceLoad resourceLoad;
			switch (this.constraintResult)
			{
			case ConstraintCheckResultType.Retry:
				resourceLoad = ResourceLoad.Unknown;
				goto IL_48;
			case ConstraintCheckResultType.Satisfied:
				resourceLoad = ResourceLoad.Zero;
				goto IL_48;
			case ConstraintCheckResultType.NotSatisfied:
				resourceLoad = ResourceLoad.Full;
				goto IL_48;
			}
			resourceLoad = ResourceLoad.Critical;
			IL_48:
			Guid databaseGuid = (base.Key as LegacyResourceHealthMonitorKey).DatabaseGuid;
			MrsTracer.ResourceHealth.Debug("LegacyResourceHealthMonitor.GetResourceLoad(): Mdb: '{0}', Constraint result: '{1}', Load: '{2}'", new object[]
			{
				databaseGuid,
				this.constraintResult,
				resourceLoad
			});
			return resourceLoad;
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0001526C File Offset: 0x0001346C
		public void Update(ConstraintCheckResultType constraintResult, ConstraintCheckAgent agent, LocalizedString failureReason)
		{
			this.LastUpdateUtc = DateTime.UtcNow;
			this.constraintResult = constraintResult;
			this.agent = agent;
			this.failureReason = failureReason;
			Guid databaseGuid = (base.Key as LegacyResourceHealthMonitorKey).DatabaseGuid;
			MrsTracer.ResourceHealth.Debug("ILegacyResourceHealthProvider.Update(): Updating health for Mdb '{0}': Result: '{1}', Agent: '{2}', Reason: '{3}'", new object[]
			{
				databaseGuid,
				constraintResult,
				this.agent,
				this.failureReason
			});
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x000152EF File Offset: 0x000134EF
		public ConstraintCheckResultType ConstraintResult
		{
			get
			{
				return this.constraintResult;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x000152F7 File Offset: 0x000134F7
		public ConstraintCheckAgent Agent
		{
			get
			{
				return this.agent;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000ACE RID: 2766 RVA: 0x000152FF File Offset: 0x000134FF
		public LocalizedString FailureReason
		{
			get
			{
				return this.failureReason;
			}
		}

		// Token: 0x0400064E RID: 1614
		private ConstraintCheckResultType constraintResult;

		// Token: 0x0400064F RID: 1615
		private ConstraintCheckAgent agent;

		// Token: 0x04000650 RID: 1616
		private LocalizedString failureReason;
	}
}
