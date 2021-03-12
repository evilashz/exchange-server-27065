using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.DagManagement;

namespace Microsoft.Exchange.Cluster.Replay.Monitoring
{
	// Token: 0x020001D2 RID: 466
	internal class PrimaryRoleTracker
	{
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x0004BAD2 File Offset: 0x00049CD2
		private static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.DatabaseHealthTrackerTracer;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x0004BAD9 File Offset: 0x00049CD9
		// (set) Token: 0x060012A1 RID: 4769 RVA: 0x0004BAE1 File Offset: 0x00049CE1
		public bool HasBecomePrimary { get; private set; }

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x0004BAEA File Offset: 0x00049CEA
		// (set) Token: 0x060012A3 RID: 4771 RVA: 0x0004BAF2 File Offset: 0x00049CF2
		public bool WasPAM { get; private set; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x0004BAFB File Offset: 0x00049CFB
		// (set) Token: 0x060012A5 RID: 4773 RVA: 0x0004BB03 File Offset: 0x00049D03
		public bool IsPAM { get; private set; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x0004BB0C File Offset: 0x00049D0C
		public bool IsAMRoleChanged
		{
			get
			{
				return this.IsPAM != this.WasPAM;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x0004BB1F File Offset: 0x00049D1F
		private bool ShouldBecomePrimary
		{
			get
			{
				return this.IsPAM && !this.HasBecomePrimary;
			}
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0004BB4C File Offset: 0x00049D4C
		public void ReportPAMStatus(IEnumerable<CopyStatusClientCachedEntry> localStatuses)
		{
			this.WasPAM = this.IsPAM;
			if (localStatuses.Any((CopyStatusClientCachedEntry status) => status.Result == CopyStatusRpcResult.Success && status.CopyStatus.IsPrimaryActiveManager))
			{
				this.IsPAM = true;
			}
			else
			{
				this.IsPAM = false;
				this.HasBecomePrimary = false;
			}
			PrimaryRoleTracker.Tracer.TraceDebug<bool, bool, bool>((long)this.GetHashCode(), "ReportPAMStatus() found: IsPAM = '{0}', IsAMRoleChanged = '{1}', HasBecomePrimary = '{2}'", this.IsPAM, this.IsAMRoleChanged, this.HasBecomePrimary);
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0004BBCC File Offset: 0x00049DCC
		public bool ShouldTryToBecomePrimary()
		{
			bool result = false;
			TransientErrorInfo.ErrorType errorType;
			if (!this.IsPAM)
			{
				PrimaryRoleTracker.Tracer.TraceDebug((long)this.GetHashCode(), "ShouldTryToBecomePrimary(): The node is *NOT* the PAM! Returning 'false'.");
				this.m_primaryTransitionSuppression.ReportFailurePeriodic(out errorType);
				return false;
			}
			if (!this.ShouldBecomePrimary)
			{
				PrimaryRoleTracker.Tracer.TraceDebug((long)this.GetHashCode(), "ShouldTryToBecomePrimary(): The node has already become the primary. Returning 'false'.");
				this.m_primaryTransitionSuppression.ReportFailurePeriodic(out errorType);
				return false;
			}
			PrimaryRoleTracker.Tracer.TraceDebug((long)this.GetHashCode(), "ShouldTryToBecomePrimary(): The node is PAM but hasn't become the primary yet.");
			if (this.m_primaryTransitionSuppression.ReportSuccessPeriodic(out errorType) && errorType == TransientErrorInfo.ErrorType.Success)
			{
				result = true;
				PrimaryRoleTracker.Tracer.TraceDebug((long)this.GetHashCode(), "ShouldTryToBecomePrimary(): Returning 'true' after periodic suppression.");
			}
			else
			{
				PrimaryRoleTracker.Tracer.TraceDebug((long)this.GetHashCode(), "ShouldTryToBecomePrimary(): Returning 'false' because of periodic suppression.");
			}
			return result;
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x0004BC94 File Offset: 0x00049E94
		public void BecomePrimary()
		{
			this.HasBecomePrimary = true;
			TransientErrorInfo.ErrorType errorType;
			this.m_primaryTransitionSuppression.ReportFailurePeriodic(out errorType);
		}

		// Token: 0x04000726 RID: 1830
		private static readonly TimeSpan PrimaryTransitionSuppressionWindow = TimeSpan.FromSeconds((double)RegistryParameters.MonitoringDHTPrimaryTransitionSuppressionInSec);

		// Token: 0x04000727 RID: 1831
		private static readonly TimeSpan PrimaryPeriodicSuppressionWindow = TimeSpan.FromSeconds((double)RegistryParameters.MonitoringDHTPrimaryPeriodicSuppressionInSec);

		// Token: 0x04000728 RID: 1832
		private TransientErrorInfoPeriodic m_primaryTransitionSuppression = new TransientErrorInfoPeriodic(PrimaryRoleTracker.PrimaryTransitionSuppressionWindow, PrimaryRoleTracker.PrimaryPeriodicSuppressionWindow, TimeSpan.Zero, TransientErrorInfoPeriodic.InfiniteTimeSpan, TransientErrorInfo.ErrorType.Failure);
	}
}
