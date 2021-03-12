using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000250 RID: 592
	internal abstract class CertificateDiagnostics : DisposableBase
	{
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001186 RID: 4486
		protected abstract ExEventLog.EventTuple CertificateDetailsEventTuple { get; }

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001187 RID: 4487
		protected abstract UMNotificationEvent CertificateNearExpiry { get; }

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001188 RID: 4488
		protected abstract Component UMExchangeComponent { get; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001189 RID: 4489
		protected abstract ExEventLog.EventTuple CertificateAboutToExpireEventTuple { get; }

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600118A RID: 4490
		protected abstract ExEventLog.EventTuple CertificateExpirationOkEventTuple { get; }

		// Token: 0x0600118B RID: 4491 RVA: 0x0004D488 File Offset: 0x0004B688
		public CertificateDiagnostics(X509Certificate2 certificate)
		{
			this.certificate = certificate;
			UmGlobals.ExEvent.LogEvent(this.CertificateDetailsEventTuple, null, new object[]
			{
				CommonUtil.ToEventLogString(this.certificate.Issuer),
				CommonUtil.ToEventLogString(this.certificate.GetSerialNumberString()),
				CommonUtil.ToEventLogString(this.certificate.Thumbprint),
				CommonUtil.ToEventLogString(CertificateUtils.IsSelfSignedCertificate(this.certificate)),
				CommonUtil.ToEventLogString(this.certificate.NotAfter)
			});
			this.AnalyzeCertificateExpirationDate();
			TimeSpan timeSpan = new TimeSpan(UMRecyclerConfig.SubsequentAlertIntervalAfterFirstAlert, 0, 0, 0);
			this.certTimer = new Timer(new TimerCallback(this.TimerCallback), null, timeSpan, timeSpan);
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0004D554 File Offset: 0x0004B754
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				lock (this)
				{
					if (this.certTimer != null)
					{
						this.certTimer.Dispose();
						this.certTimer = null;
					}
				}
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x0004D5A8 File Offset: 0x0004B7A8
		protected virtual TimeSpan ExpirationTimeLeft
		{
			get
			{
				return CertificateUtils.TimeToExpire(this.certificate);
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x0004D5B5 File Offset: 0x0004B7B5
		protected virtual int DaysBeforeExpirationAlert
		{
			get
			{
				return UMRecyclerConfig.DaysBeforeCertExpiryForAlert;
			}
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0004D5BC File Offset: 0x0004B7BC
		private void AnalyzeCertificateExpirationDate()
		{
			TimeSpan expirationTimeLeft = this.ExpirationTimeLeft;
			if (expirationTimeLeft.Days <= this.DaysBeforeExpirationAlert)
			{
				StatefulEventLog.Instance.LogRedEvent(base.GetType().Name, this.CertificateAboutToExpireEventTuple, null, true, new object[]
				{
					expirationTimeLeft.Days
				});
				UMEventNotificationHelper.PublishUMFailureEventNotificationItem(this.UMExchangeComponent, this.CertificateNearExpiry.ToString());
				return;
			}
			StatefulEventLog.Instance.LogGreenEvent(base.GetType().Name, this.CertificateExpirationOkEventTuple, null, false, new object[]
			{
				expirationTimeLeft.Days,
				this.DaysBeforeExpirationAlert
			});
			UMEventNotificationHelper.PublishUMSuccessEventNotificationItem(this.UMExchangeComponent, this.CertificateNearExpiry.ToString());
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0004D68C File Offset: 0x0004B88C
		private void TimerCallback(object state)
		{
			lock (this)
			{
				if (this.certTimer != null)
				{
					this.AnalyzeCertificateExpirationDate();
				}
			}
		}

		// Token: 0x04000BEC RID: 3052
		private Timer certTimer;

		// Token: 0x04000BED RID: 3053
		private X509Certificate2 certificate;
	}
}
