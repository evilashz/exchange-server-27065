using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders
{
	// Token: 0x02000209 RID: 521
	internal class PerformanceCounterRestartResponderChecker : RestartResponderChecker
	{
		// Token: 0x06000EBC RID: 3772 RVA: 0x00061996 File Offset: 0x0005FB96
		internal PerformanceCounterRestartResponderChecker(ResponderDefinition definition, string categoryName, string counterName, string instanceName, int minThreshold, int maxThreshold, string reasonToSkip) : base(definition)
		{
			this.categoryName = categoryName;
			this.counterName = counterName;
			this.instanceName = instanceName;
			this.minThreshold = minThreshold;
			this.maxThreshold = maxThreshold;
			this.reasonToSkip = reasonToSkip;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x000619D0 File Offset: 0x0005FBD0
		protected override bool IsWithinThreshold()
		{
			this.skipReasonOrException = null;
			try
			{
				float performanceCounterValue = this.GetPerformanceCounterValue();
				if (performanceCounterValue > (float)this.maxThreshold || performanceCounterValue < (float)this.minThreshold)
				{
					this.skipReasonOrException = string.Format("{0}. Real value = {1}, Threshold = [{2}, {3}]", new object[]
					{
						this.reasonToSkip,
						performanceCounterValue,
						this.minThreshold,
						this.maxThreshold
					});
					return false;
				}
			}
			catch (Exception ex)
			{
				this.skipReasonOrException = ex.ToString();
			}
			return true;
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000EBE RID: 3774 RVA: 0x00061A70 File Offset: 0x0005FC70
		internal override string SkipReasonOrException
		{
			get
			{
				return this.skipReasonOrException;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x00061A78 File Offset: 0x0005FC78
		protected override bool Enabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x00061A7B File Offset: 0x0005FC7B
		protected override bool CheckChangedSetting()
		{
			return true;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x00061A80 File Offset: 0x0005FC80
		private float GetPerformanceCounterValue()
		{
			float num = 0f;
			using (PerformanceCounter performanceCounter = new PerformanceCounter(this.categoryName, this.counterName, this.instanceName, true))
			{
				num = performanceCounter.NextValue();
				int num2 = 0;
				while (num2 < 3 && num <= 0f)
				{
					Thread.Sleep(500);
					num = performanceCounter.NextValue();
					num2++;
				}
			}
			return num;
		}

		// Token: 0x04000AEE RID: 2798
		private const int RetryCounter = 3;

		// Token: 0x04000AEF RID: 2799
		private string reasonToSkip;

		// Token: 0x04000AF0 RID: 2800
		private int minThreshold;

		// Token: 0x04000AF1 RID: 2801
		private int maxThreshold;

		// Token: 0x04000AF2 RID: 2802
		private string categoryName;

		// Token: 0x04000AF3 RID: 2803
		private string counterName;

		// Token: 0x04000AF4 RID: 2804
		private string instanceName;

		// Token: 0x04000AF5 RID: 2805
		private string skipReasonOrException;
	}
}
