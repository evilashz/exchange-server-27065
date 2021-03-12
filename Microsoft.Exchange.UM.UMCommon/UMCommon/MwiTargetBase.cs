using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000D1 RID: 209
	internal abstract class MwiTargetBase : IMwiTarget, IRpcTarget
	{
		// Token: 0x060006E0 RID: 1760 RVA: 0x0001ABAC File Offset: 0x00018DAC
		protected MwiTargetBase(ADConfigurationObject configObject, string instanceNameSuffix)
		{
			this.configObject = configObject;
			if (VariantConfiguration.InvariantNoFlightingSnapshot.UM.UMDataCenterLogging.Enabled)
			{
				this.perfCounters = null;
				return;
			}
			this.InitializePerfCounters(instanceNameSuffix);
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0001ABFB File Offset: 0x00018DFB
		public string Name
		{
			get
			{
				return this.ConfigObject.Name;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0001AC08 File Offset: 0x00018E08
		public ADConfigurationObject ConfigObject
		{
			get
			{
				return this.configObject;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0001AC10 File Offset: 0x00018E10
		protected long AverageProcessingTimeMsec
		{
			get
			{
				return this.averageProcessingTime.Value;
			}
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001AC1D File Offset: 0x00018E1D
		public virtual void SendMessageAsync(MwiMessage message)
		{
			if (this.perfCounters != null)
			{
				MwiDiagnostics.IncrementCounter(this.perfCounters.TotalMwiMessages);
			}
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001AC38 File Offset: 0x00018E38
		public override bool Equals(object target)
		{
			MwiTargetBase mwiTargetBase = target as MwiTargetBase;
			return mwiTargetBase != null && this.ConfigObject.Guid.Equals(mwiTargetBase.ConfigObject.Guid);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001AC6F File Offset: 0x00018E6F
		public override int GetHashCode()
		{
			if (this.ConfigObject.Id == null)
			{
				return base.GetHashCode();
			}
			return this.ConfigObject.Id.GetHashCode();
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001AC98 File Offset: 0x00018E98
		protected void UpdatePerformanceCounters(MwiMessage message, MwiDeliveryException error)
		{
			if (this.perfCounters != null)
			{
				TimeSpan timeSpan = ExDateTime.UtcNow.Subtract(message.SentTimeUtc);
				MwiDiagnostics.SetCounterValue(this.perfCounters.AverageMwiProcessingTime, this.averageProcessingTime.Update(timeSpan.TotalMilliseconds));
				if (error != null)
				{
					MwiDiagnostics.IncrementCounter(this.perfCounters.TotalFailedMwiMessages);
				}
			}
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001ACF8 File Offset: 0x00018EF8
		private void InitializePerfCounters(string instanceNameSuffix)
		{
			string instanceName = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				this.Name,
				instanceNameSuffix
			});
			this.perfCounters = MwiDiagnostics.GetInstance(instanceName);
			this.perfCounters.Reset();
		}

		// Token: 0x04000402 RID: 1026
		private MwiLoadBalancerPerformanceCountersInstance perfCounters;

		// Token: 0x04000403 RID: 1027
		private MovingAverage averageProcessingTime = new MovingAverage(50);

		// Token: 0x04000404 RID: 1028
		private ADConfigurationObject configObject;
	}
}
