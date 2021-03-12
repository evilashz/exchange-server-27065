using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.CrossServerMailboxAccess;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x02000D1F RID: 3359
	[Cmdlet("Get", "UMCallSummaryReport")]
	public sealed class GetUMCallSummaryReport : UMReportsTaskBase<MailboxIdParameter>
	{
		// Token: 0x170027FB RID: 10235
		// (get) Token: 0x060080ED RID: 33005 RVA: 0x0020FA69 File Offset: 0x0020DC69
		// (set) Token: 0x060080EE RID: 33006 RVA: 0x0020FA71 File Offset: 0x0020DC71
		private new MailboxIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x170027FC RID: 10236
		// (get) Token: 0x060080EF RID: 33007 RVA: 0x0020FA7A File Offset: 0x0020DC7A
		// (set) Token: 0x060080F0 RID: 33008 RVA: 0x0020FA91 File Offset: 0x0020DC91
		[Parameter(Mandatory = false)]
		[ValidateNotNullOrEmpty]
		public UMDialPlanIdParameter UMDialPlan
		{
			get
			{
				return (UMDialPlanIdParameter)base.Fields["UMDialPlan"];
			}
			set
			{
				base.Fields["UMDialPlan"] = value;
			}
		}

		// Token: 0x170027FD RID: 10237
		// (get) Token: 0x060080F1 RID: 33009 RVA: 0x0020FAA4 File Offset: 0x0020DCA4
		// (set) Token: 0x060080F2 RID: 33010 RVA: 0x0020FABB File Offset: 0x0020DCBB
		[ValidateNotNullOrEmpty]
		[Parameter(Mandatory = false)]
		public UMIPGatewayIdParameter UMIPGateway
		{
			get
			{
				return (UMIPGatewayIdParameter)base.Fields["UMIPGateway"];
			}
			set
			{
				base.Fields["UMIPGateway"] = value;
			}
		}

		// Token: 0x170027FE RID: 10238
		// (get) Token: 0x060080F3 RID: 33011 RVA: 0x0020FACE File Offset: 0x0020DCCE
		// (set) Token: 0x060080F4 RID: 33012 RVA: 0x0020FAE5 File Offset: 0x0020DCE5
		[Parameter(Mandatory = true)]
		public GroupBy GroupBy
		{
			get
			{
				return (GroupBy)base.Fields["GroupBy"];
			}
			set
			{
				base.Fields["GroupBy"] = value;
			}
		}

		// Token: 0x060080F5 RID: 33013 RVA: 0x0020FAFD File Offset: 0x0020DCFD
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			base.ValidateCommonParamsAndSetOrg(this.UMDialPlan, this.UMIPGateway, out this.dialPlanGuid, out this.gatewayGuid, out this.dialPlanName, out this.gatewayName);
		}

		// Token: 0x060080F6 RID: 33014 RVA: 0x0020FB30 File Offset: 0x0020DD30
		protected override void ProcessMailbox()
		{
			try
			{
				using (IUMCallDataRecordStorage umcallDataRecordsAcessor = InterServerMailboxAccessor.GetUMCallDataRecordsAcessor(this.DataObject))
				{
					UMReportRawCounters[] umcallSummary = umcallDataRecordsAcessor.GetUMCallSummary(this.dialPlanGuid, this.gatewayGuid, this.GroupBy);
					if (umcallSummary != null)
					{
						this.WriteAsConfigObjects(umcallSummary);
					}
				}
			}
			catch (StorageTransientException exception)
			{
				base.WriteError(exception, ErrorCategory.ReadError, null);
			}
			catch (StoragePermanentException exception2)
			{
				base.WriteError(exception2, ErrorCategory.ReadError, null);
			}
			catch (CDROperationException exception3)
			{
				base.WriteError(exception3, ErrorCategory.ReadError, null);
			}
			catch (EWSUMMailboxAccessException exception4)
			{
				base.WriteError(exception4, ErrorCategory.ReadError, null);
			}
		}

		// Token: 0x060080F7 RID: 33015 RVA: 0x0020FBF4 File Offset: 0x0020DDF4
		private void WriteAsConfigObjects(UMReportRawCounters[] counters)
		{
			foreach (UMReportRawCounters umreportRawCounters in counters)
			{
				UMCallSummaryReport umcallSummaryReport = new UMCallSummaryReport(this.DataObject.Identity);
				switch (this.GroupBy)
				{
				case GroupBy.Day:
					umcallSummaryReport.Date = umreportRawCounters.Date.ToShortDateString();
					break;
				case GroupBy.Month:
					umcallSummaryReport.Date = umreportRawCounters.Date.ToString("MMM/yyyy");
					break;
				case GroupBy.Total:
					umcallSummaryReport.Date = "---";
					break;
				default:
					throw new NotImplementedException("Value of GroupBy is unknown.");
				}
				umcallSummaryReport.AutoAttendant = umreportRawCounters.AutoAttendantCalls;
				umcallSummaryReport.FailedOrRejectedCalls = umreportRawCounters.FailedCalls;
				umcallSummaryReport.Fax = umreportRawCounters.FaxCalls;
				umcallSummaryReport.MissedCalls = umreportRawCounters.MissedCalls;
				umcallSummaryReport.OtherCalls = umreportRawCounters.OtherCalls;
				umcallSummaryReport.Outbound = umreportRawCounters.OutboundCalls;
				umcallSummaryReport.SubscriberAccess = umreportRawCounters.SubscriberAccessCalls;
				umcallSummaryReport.VoiceMessages = umreportRawCounters.VoiceMailCalls;
				umcallSummaryReport.TotalCalls = umreportRawCounters.TotalCalls;
				umcallSummaryReport.UMDialPlanName = this.dialPlanName;
				umcallSummaryReport.UMIPGatewayName = this.gatewayName;
				umcallSummaryReport.NMOS = Utils.GetNullableAudioQualityMetric((float)umreportRawCounters.AudioMetricsAverages.NMOS.Average);
				umcallSummaryReport.NMOSDegradation = Utils.GetNullableAudioQualityMetric((float)umreportRawCounters.AudioMetricsAverages.NMOSDegradation.Average);
				umcallSummaryReport.PercentPacketLoss = Utils.GetNullableAudioQualityMetric((float)umreportRawCounters.AudioMetricsAverages.PercentPacketLoss.Average);
				umcallSummaryReport.Jitter = Utils.GetNullableAudioQualityMetric((float)umreportRawCounters.AudioMetricsAverages.Jitter.Average);
				umcallSummaryReport.RoundTripMilliseconds = Utils.GetNullableAudioQualityMetric((float)umreportRawCounters.AudioMetricsAverages.RoundTrip.Average);
				umcallSummaryReport.BurstLossDurationMilliseconds = Utils.GetNullableAudioQualityMetric((float)umreportRawCounters.AudioMetricsAverages.BurstLossDuration.Average);
				umcallSummaryReport.TotalAudioQualityCallsSampled = umreportRawCounters.AudioMetricsAverages.TotalAudioQualityCallsSampled;
				base.WriteObject(umcallSummaryReport);
			}
		}

		// Token: 0x04003F12 RID: 16146
		private const string TotalReportDateString = "---";

		// Token: 0x04003F13 RID: 16147
		private const string DateFormat = "MMM/yyyy";

		// Token: 0x04003F14 RID: 16148
		private const string FixedFormatString = "F1";

		// Token: 0x04003F15 RID: 16149
		private Guid dialPlanGuid;

		// Token: 0x04003F16 RID: 16150
		private Guid gatewayGuid;

		// Token: 0x04003F17 RID: 16151
		private string dialPlanName;

		// Token: 0x04003F18 RID: 16152
		private string gatewayName;
	}
}
