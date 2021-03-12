using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020001F4 RID: 500
	internal abstract class MonitoringAlert
	{
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x000509A2 File Offset: 0x0004EBA2
		protected static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.MonitoringTracer;
			}
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x000509A9 File Offset: 0x0004EBA9
		protected MonitoringAlert(string identity, Guid identityGuid)
		{
			this.m_identity = identity;
			this.m_identityGuid = identityGuid;
			this.Init();
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x000509CC File Offset: 0x0004EBCC
		protected virtual TimeSpan DatabaseHealthCheckGreenTransitionSuppression
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckGreenTransitionSuppressionInSec);
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x060013DA RID: 5082 RVA: 0x000509D9 File Offset: 0x0004EBD9
		protected virtual TimeSpan DatabaseHealthCheckGreenPeriodicInterval
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckGreenPeriodicIntervalInSec);
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x000509E6 File Offset: 0x0004EBE6
		protected virtual TimeSpan DatabaseHealthCheckRedTransitionSuppression
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckRedTransitionSuppressionInSec);
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x000509F3 File Offset: 0x0004EBF3
		protected virtual TimeSpan DatabaseHealthCheckRedPeriodicInterval
		{
			get
			{
				return TimeSpan.FromSeconds((double)RegistryParameters.DatabaseHealthCheckRedPeriodicIntervalInSec);
			}
		}

		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x00050A00 File Offset: 0x0004EC00
		protected virtual bool IsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060013DE RID: 5086 RVA: 0x00050A03 File Offset: 0x0004EC03
		public string Identity
		{
			get
			{
				return this.m_identity;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x00050A0B File Offset: 0x0004EC0B
		public Guid IdentityGuid
		{
			get
			{
				return this.m_identityGuid;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060013E0 RID: 5088 RVA: 0x00050A13 File Offset: 0x0004EC13
		// (set) Token: 0x060013E1 RID: 5089 RVA: 0x00050A1B File Offset: 0x0004EC1B
		public TransientErrorInfo.ErrorType CurrentAlertState { get; private set; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x00050A24 File Offset: 0x0004EC24
		// (set) Token: 0x060013E3 RID: 5091 RVA: 0x00050A2C File Offset: 0x0004EC2C
		public string ErrorMessage { get; private set; }

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x00050A35 File Offset: 0x0004EC35
		// (set) Token: 0x060013E5 RID: 5093 RVA: 0x00050A3D File Offset: 0x0004EC3D
		public string ErrorMessageWithoutFullStatus { get; private set; }

		// Token: 0x060013E6 RID: 5094 RVA: 0x00050A48 File Offset: 0x0004EC48
		public void RaiseAppropriateAlertIfNecessary(IHealthValidationResultMinimal result)
		{
			if (!this.IsEnabled)
			{
				MonitoringAlert.Tracer.TraceDebug<string>((long)this.GetHashCode(), "MonitoringAlert: RaiseAppropriateAlertIfNecessary() for '{0}' is skipped because the alert is disabled!", this.Identity);
				this.m_resetEligible = false;
				return;
			}
			this.m_resetEligible = true;
			TransientErrorInfo.ErrorType errorType;
			if (this.IsValidationSuccessful(result))
			{
				if (this.m_alertSuppression.ReportSuccessPeriodic(out errorType))
				{
					this.RaiseAppropriateEvent(errorType, result);
				}
			}
			else if (this.m_alertSuppression.ReportFailurePeriodic(out errorType))
			{
				this.RaiseAppropriateEvent(errorType, result);
			}
			this.CurrentAlertState = errorType;
			this.ErrorMessage = result.ErrorMessage;
			this.ErrorMessageWithoutFullStatus = result.ErrorMessageWithoutFullStatus;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x00050AE0 File Offset: 0x0004ECE0
		public virtual void ResetState()
		{
			if (this.m_resetEligible)
			{
				MonitoringAlert.Tracer.TraceDebug<string>((long)this.GetHashCode(), "MonitoringAlert: ResetState() called! Alert state for '{0}' is being overwritten to 'Unknown'.", this.Identity);
				this.Init();
				this.m_resetEligible = false;
			}
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x00050B13 File Offset: 0x0004ED13
		protected virtual bool IsValidationSuccessful(IHealthValidationResultMinimal result)
		{
			return result.IsValidationSuccessful;
		}

		// Token: 0x060013E9 RID: 5097
		protected abstract void RaiseGreenEvent(IHealthValidationResultMinimal result);

		// Token: 0x060013EA RID: 5098
		protected abstract void RaiseRedEvent(IHealthValidationResultMinimal result);

		// Token: 0x060013EB RID: 5099 RVA: 0x00050B1B File Offset: 0x0004ED1B
		private void Init()
		{
			this.m_alertSuppression = new TransientErrorInfoPeriodic(this.DatabaseHealthCheckGreenTransitionSuppression, this.DatabaseHealthCheckGreenPeriodicInterval, this.DatabaseHealthCheckRedTransitionSuppression, this.DatabaseHealthCheckRedPeriodicInterval, TransientErrorInfo.ErrorType.Unknown);
			this.CurrentAlertState = TransientErrorInfo.ErrorType.Unknown;
			this.ErrorMessage = null;
			this.ErrorMessageWithoutFullStatus = null;
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00050B56 File Offset: 0x0004ED56
		private void RaiseAppropriateEvent(TransientErrorInfo.ErrorType currentState, IHealthValidationResultMinimal result)
		{
			if (currentState == TransientErrorInfo.ErrorType.Success)
			{
				this.RaiseGreenEvent(result);
				return;
			}
			if (currentState == TransientErrorInfo.ErrorType.Failure)
			{
				this.RaiseRedEvent(result);
			}
		}

		// Token: 0x040007B0 RID: 1968
		protected const string MonitorStateRegkey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\HighAvailability\\States";

		// Token: 0x040007B1 RID: 1969
		private readonly string m_identity;

		// Token: 0x040007B2 RID: 1970
		private readonly Guid m_identityGuid;

		// Token: 0x040007B3 RID: 1971
		private bool m_resetEligible = true;

		// Token: 0x040007B4 RID: 1972
		private TransientErrorInfoPeriodic m_alertSuppression;
	}
}
