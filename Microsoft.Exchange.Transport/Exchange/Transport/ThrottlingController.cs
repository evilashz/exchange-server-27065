using System;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200006A RID: 106
	internal sealed class ThrottlingController
	{
		// Token: 0x0600033B RID: 827 RVA: 0x0000E684 File Offset: 0x0000C884
		public ThrottlingController(Trace tracer, ResourceManagerConfiguration.ThrottlingControllerConfiguration config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("config");
			}
			this.tracer = tracer;
			this.throttleDelayBasedOnPressure = TimeSpan.Zero;
			this.config = config;
			this.maxPressureBasedThrottlingDelayInterval = this.config.MaxThrottlingDelayInterval - this.config.BaseThrottlingDelayInterval;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000E6E0 File Offset: 0x0000C8E0
		public void Increase()
		{
			TimeSpan timeSpan = this.throttleDelayBasedOnPressure;
			if (timeSpan == TimeSpan.Zero)
			{
				timeSpan = this.config.StartThrottlingDelayInterval;
			}
			else
			{
				timeSpan = timeSpan.Add(this.config.StepThrottlingDelayInterval);
			}
			if (timeSpan > this.maxPressureBasedThrottlingDelayInterval)
			{
				timeSpan = this.maxPressureBasedThrottlingDelayInterval;
			}
			this.SetCurrent(timeSpan);
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000E740 File Offset: 0x0000C940
		public void Decrease()
		{
			TimeSpan timeSpan = this.throttleDelayBasedOnPressure;
			timeSpan = timeSpan.Subtract(this.config.StepThrottlingDelayInterval);
			if (timeSpan < TimeSpan.Zero)
			{
				timeSpan = TimeSpan.Zero;
			}
			this.SetCurrent(timeSpan);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000E784 File Offset: 0x0000C984
		public TimeSpan GetCurrent()
		{
			return this.config.BaseThrottlingDelayInterval.Add(this.throttleDelayBasedOnPressure);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000E7AC File Offset: 0x0000C9AC
		public void AddDiagnosticInfo(XElement parent, bool showState)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			this.config.AddDiagnosticInfo(parent);
			if (showState)
			{
				parent.Add(new object[]
				{
					new XElement("throttleDelayBasedOnPressure", this.throttleDelayBasedOnPressure),
					new XElement("maxPressureBasedThrottlingDelayInterval", this.maxPressureBasedThrottlingDelayInterval)
				});
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000E81E File Offset: 0x0000CA1E
		private void SetCurrent(TimeSpan newThrottlingInterval)
		{
			if (this.tracer != null && newThrottlingInterval != this.throttleDelayBasedOnPressure)
			{
				this.tracer.TraceDebug<TimeSpan, TimeSpan>(0L, "throttling interval changed from '{0}' to '{1}' (not including base delay).", this.throttleDelayBasedOnPressure, newThrottlingInterval);
			}
			this.throttleDelayBasedOnPressure = newThrottlingInterval;
		}

		// Token: 0x040001CA RID: 458
		private readonly TimeSpan maxPressureBasedThrottlingDelayInterval;

		// Token: 0x040001CB RID: 459
		private ResourceManagerConfiguration.ThrottlingControllerConfiguration config;

		// Token: 0x040001CC RID: 460
		private TimeSpan throttleDelayBasedOnPressure;

		// Token: 0x040001CD RID: 461
		private Trace tracer;
	}
}
