using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x02000086 RID: 134
	public class RoutingOverride
	{
		// Token: 0x06000313 RID: 787 RVA: 0x00007E11 File Offset: 0x00006011
		public RoutingOverride(RoutingDomain routingDomain, DeliveryQueueDomain deliveryQueueDomain) : this(routingDomain, RoutingOverride.EmptyHostList, deliveryQueueDomain)
		{
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00007E20 File Offset: 0x00006020
		public RoutingOverride(RoutingDomain routingDomain, RoutingHost alternateDeliveryRoutingHost, DeliveryQueueDomain deliveryQueueDomain)
		{
			if (routingDomain == RoutingDomain.Empty)
			{
				throw new ArgumentException(string.Format("The provided domain '{0}' is RoutingDomain.Empty which isn't valid for RoutingOverride", routingDomain));
			}
			if (alternateDeliveryRoutingHost == null && deliveryQueueDomain == DeliveryQueueDomain.UseAlternateDeliveryRoutingHosts)
			{
				throw new ArgumentException(string.Format("The provided delivery queue domain value UseAlternateDeliveryRoutingHosts is only valid when you specify alternate delivery hosts.", new object[0]));
			}
			if (alternateDeliveryRoutingHost != null && !string.Equals(routingDomain.Type, "smtp", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(string.Format("The routing domain needs to be an SMTP domain if alternate delivery hosts are specified - routingDomain.Type == {0}", routingDomain.Type));
			}
			this.routingDomain = routingDomain;
			this.alternateDeliveryRoutingHosts = alternateDeliveryRoutingHost.ToString();
			this.deliveryQueueDomain = deliveryQueueDomain;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00007EC0 File Offset: 0x000060C0
		public RoutingOverride(RoutingDomain routingDomain, List<RoutingHost> alternateDeliveryRoutingHosts, DeliveryQueueDomain deliveryQueueDomain)
		{
			if (routingDomain == RoutingDomain.Empty)
			{
				throw new ArgumentException(string.Format("The provided domain '{0}' is RoutingDomain.Empty which isn't valid for RoutingOverride", routingDomain));
			}
			if ((alternateDeliveryRoutingHosts == null || alternateDeliveryRoutingHosts.Count == 0) && deliveryQueueDomain == DeliveryQueueDomain.UseAlternateDeliveryRoutingHosts)
			{
				throw new ArgumentException(string.Format("The provided delivery queue domain value UseAlternateDeliveryRoutingHosts is only valid when you specify alternate delivery hosts.", new object[0]));
			}
			if (alternateDeliveryRoutingHosts != null && alternateDeliveryRoutingHosts.Count != 0 && !string.Equals(routingDomain.Type, "smtp", StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(string.Format("The routing domain needs to be an SMTP domain if alternate delivery hosts are specified - routingDomain.Type == {0}", routingDomain.Type));
			}
			this.routingDomain = routingDomain;
			this.alternateDeliveryRoutingHosts = RoutingHost.ConvertRoutingHostsToString<RoutingHost>(alternateDeliveryRoutingHosts, (RoutingHost host) => host);
			this.deliveryQueueDomain = deliveryQueueDomain;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00007F88 File Offset: 0x00006188
		public RoutingDomain RoutingDomain
		{
			get
			{
				return this.routingDomain;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00007F93 File Offset: 0x00006193
		public List<RoutingHost> AlternateDeliveryRoutingHosts
		{
			get
			{
				return RoutingHost.GetRoutingHostsFromString<RoutingHost>(this.AlternateDeliveryRoutingHostsString, (RoutingHost host) => host);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00007FBD File Offset: 0x000061BD
		internal string AlternateDeliveryRoutingHostsString
		{
			get
			{
				return this.alternateDeliveryRoutingHosts;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00007FC5 File Offset: 0x000061C5
		public DeliveryQueueDomain DeliveryQueueDomain
		{
			get
			{
				return this.deliveryQueueDomain;
			}
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00007FD0 File Offset: 0x000061D0
		public override string ToString()
		{
			int num = (int)this.DeliveryQueueDomain;
			return string.Format(CultureInfo.InvariantCulture, "Connector Domain:[{0}]; Alternate Delivery Routing hosts:[{1}]; Delivery Queue Domain:[{2}]", new object[]
			{
				this.RoutingDomain.ToString(),
				this.AlternateDeliveryRoutingHostsString,
				num
			});
		}

		// Token: 0x040001F2 RID: 498
		private static readonly List<RoutingHost> EmptyHostList = new List<RoutingHost>();

		// Token: 0x040001F3 RID: 499
		private readonly RoutingDomain routingDomain;

		// Token: 0x040001F4 RID: 500
		private readonly string alternateDeliveryRoutingHosts;

		// Token: 0x040001F5 RID: 501
		private readonly DeliveryQueueDomain deliveryQueueDomain;
	}
}
