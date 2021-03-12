using System;
using System.Xml.Linq;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000278 RID: 632
	internal sealed class UnhealthyTargetFilterConfiguration
	{
		// Token: 0x06001B51 RID: 6993 RVA: 0x0006FEDD File Offset: 0x0006E0DD
		public UnhealthyTargetFilterConfiguration(bool enabled, int glitchRetryCount, TimeSpan glitchRetryInterval, int transientFailureRetryCount, TimeSpan transientFailureRetryInterval, TimeSpan outboundConnectionFailureRetryInterval)
		{
			this.enabled = enabled;
			this.glitchRetryCount = glitchRetryCount;
			this.glitchRetryInterval = glitchRetryInterval;
			this.transientFailureRetryCount = transientFailureRetryCount;
			this.transientFailureRetryInterval = transientFailureRetryInterval;
			this.outboundConnectionFailureRetryInterval = outboundConnectionFailureRetryInterval;
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0006FF14 File Offset: 0x0006E114
		public UnhealthyTargetFilterConfiguration() : this(Components.Configuration.AppConfig.RemoteDelivery.LoadBalancingForServerFailoverEnabled, Components.Configuration.AppConfig.RemoteDelivery.QueueGlitchRetryCount, Components.Configuration.AppConfig.RemoteDelivery.QueueGlitchRetryInterval, Components.Configuration.LocalServer.TransportServer.TransientFailureRetryCount, Components.Configuration.LocalServer.TransportServer.TransientFailureRetryInterval, Components.Configuration.LocalServer.TransportServer.OutboundConnectionFailureRetryInterval)
		{
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x0006FFAC File Offset: 0x0006E1AC
		internal void AddDiagnosticInfoTo(XElement unhealthyTargetFiltererConfigElement)
		{
			unhealthyTargetFiltererConfigElement.Add(new object[]
			{
				new XElement("Enabled", this.Enabled),
				new XElement("GlitchRetryCount", this.GlitchRetryCount),
				new XElement("GlitchRetryInterval", this.GlitchRetryInterval),
				new XElement("TransientFailureRetryCount", this.TransientFailureRetryCount),
				new XElement("TransientFailureRetryInterval", this.TransientFailureRetryInterval),
				new XElement("OutboundConnectionFailureRetryInterval", this.OutboundConnectionFailureRetryInterval)
			});
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x00070075 File Offset: 0x0006E275
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x0007007D File Offset: 0x0006E27D
		public int GlitchRetryCount
		{
			get
			{
				return this.glitchRetryCount;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x00070085 File Offset: 0x0006E285
		public TimeSpan GlitchRetryInterval
		{
			get
			{
				return this.glitchRetryInterval;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x0007008D File Offset: 0x0006E28D
		public int TransientFailureRetryCount
		{
			get
			{
				return this.transientFailureRetryCount;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x00070095 File Offset: 0x0006E295
		public TimeSpan TransientFailureRetryInterval
		{
			get
			{
				return this.transientFailureRetryInterval;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x0007009D File Offset: 0x0006E29D
		public TimeSpan OutboundConnectionFailureRetryInterval
		{
			get
			{
				return this.outboundConnectionFailureRetryInterval;
			}
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x000700A5 File Offset: 0x0006E2A5
		public void ConfigChanged(bool enabled, int glitchRetryCount, TimeSpan glitchRetryInterval, int transientFailureRetryCount, TimeSpan transientFailureRetryInterval, TimeSpan outboundConnectionFailureRetryInterval)
		{
			this.glitchRetryCount = glitchRetryCount;
			this.glitchRetryInterval = glitchRetryInterval;
			this.transientFailureRetryCount = transientFailureRetryCount;
			this.transientFailureRetryInterval = transientFailureRetryInterval;
			this.outboundConnectionFailureRetryInterval = outboundConnectionFailureRetryInterval;
		}

		// Token: 0x04000CE0 RID: 3296
		private bool enabled;

		// Token: 0x04000CE1 RID: 3297
		private int glitchRetryCount;

		// Token: 0x04000CE2 RID: 3298
		private TimeSpan glitchRetryInterval;

		// Token: 0x04000CE3 RID: 3299
		private int transientFailureRetryCount;

		// Token: 0x04000CE4 RID: 3300
		private TimeSpan transientFailureRetryInterval;

		// Token: 0x04000CE5 RID: 3301
		private TimeSpan outboundConnectionFailureRetryInterval;
	}
}
