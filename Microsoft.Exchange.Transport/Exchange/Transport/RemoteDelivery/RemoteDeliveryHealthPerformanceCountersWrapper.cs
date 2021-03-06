using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.RemoteDelivery
{
	// Token: 0x020003B5 RID: 949
	internal class RemoteDeliveryHealthPerformanceCountersWrapper : IRemoteDeliveryHealthPerformanceCounters
	{
		// Token: 0x06002AB5 RID: 10933 RVA: 0x000AA96C File Offset: 0x000A8B6C
		public void UpdateOutboundIPPoolPerfCounter(string pool, RiskLevel riskLevel, int percentageQueueErrors)
		{
			OutboundIPPoolPerfCountersInstance instance = OutboundIPPoolPerfCounters.GetInstance(pool);
			switch (riskLevel)
			{
			case RiskLevel.Normal:
				instance.NormalRisk.RawValue = (long)percentageQueueErrors;
				return;
			case RiskLevel.Bulk:
				instance.BulkRisk.RawValue = (long)percentageQueueErrors;
				return;
			case RiskLevel.High:
				instance.HighRisk.RawValue = (long)percentageQueueErrors;
				return;
			case RiskLevel.Low:
				instance.LowRisk.RawValue = (long)percentageQueueErrors;
				return;
			default:
				return;
			}
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x000AA9D0 File Offset: 0x000A8BD0
		public void UpdateSmtpResponseSubCodePerfCounter(int code, int messageCount)
		{
			switch (code)
			{
			case 0:
				SmtpResponseSubCodePerfCounters.Zero.RawValue = (long)messageCount;
				return;
			case 1:
				SmtpResponseSubCodePerfCounters.One.RawValue = (long)messageCount;
				return;
			case 2:
				SmtpResponseSubCodePerfCounters.Two.RawValue = (long)messageCount;
				return;
			case 3:
				SmtpResponseSubCodePerfCounters.Three.RawValue = (long)messageCount;
				return;
			case 4:
				SmtpResponseSubCodePerfCounters.Four.RawValue = (long)messageCount;
				return;
			case 5:
				SmtpResponseSubCodePerfCounters.Five.RawValue = (long)messageCount;
				return;
			case 6:
				SmtpResponseSubCodePerfCounters.Six.RawValue = (long)messageCount;
				return;
			case 7:
				SmtpResponseSubCodePerfCounters.Seven.RawValue = (long)messageCount;
				return;
			default:
				return;
			}
		}
	}
}
