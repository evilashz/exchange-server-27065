using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Agent.ConnectionFiltering
{
	// Token: 0x0200000A RID: 10
	internal static class ConnectionFilteringPerfCounters
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00003804 File Offset: 0x00001A04
		public static void GetPerfCounterInfo(XElement element)
		{
			if (ConnectionFilteringPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in ConnectionFilteringPerfCounters.AllCounters)
			{
				try
				{
					element.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					element.Add(content);
				}
			}
		}

		// Token: 0x04000030 RID: 48
		public const string CategoryName = "MSExchange Connection Filtering Agent";

		// Token: 0x04000031 RID: 49
		private static readonly ExPerformanceCounter ConnectionsOnIPAllowListPerSecond = new ExPerformanceCounter("MSExchange Connection Filtering Agent", "Connections on IP Allow List/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000032 RID: 50
		public static readonly ExPerformanceCounter TotalConnectionsOnIPAllowList = new ExPerformanceCounter("MSExchange Connection Filtering Agent", "Connections on IP Allow List", string.Empty, null, new ExPerformanceCounter[]
		{
			ConnectionFilteringPerfCounters.ConnectionsOnIPAllowListPerSecond
		});

		// Token: 0x04000033 RID: 51
		private static readonly ExPerformanceCounter ConnectionsOnIPBlockListPerSecond = new ExPerformanceCounter("MSExchange Connection Filtering Agent", "Connections on IP Block List/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000034 RID: 52
		public static readonly ExPerformanceCounter TotalConnectionsOnIPBlockList = new ExPerformanceCounter("MSExchange Connection Filtering Agent", "Connections on IP Block List", string.Empty, null, new ExPerformanceCounter[]
		{
			ConnectionFilteringPerfCounters.ConnectionsOnIPBlockListPerSecond
		});

		// Token: 0x04000035 RID: 53
		private static readonly ExPerformanceCounter ConnectionsOnIPAllowProvidersPerSecond = new ExPerformanceCounter("MSExchange Connection Filtering Agent", "Connections on IP Allow List Providers/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000036 RID: 54
		public static readonly ExPerformanceCounter TotalConnectionsOnIPAllowProviders = new ExPerformanceCounter("MSExchange Connection Filtering Agent", "Connections on IP Allow List Providers", string.Empty, null, new ExPerformanceCounter[]
		{
			ConnectionFilteringPerfCounters.ConnectionsOnIPAllowProvidersPerSecond
		});

		// Token: 0x04000037 RID: 55
		private static readonly ExPerformanceCounter ConnectionsOnIPBlockProvidersPerSecond = new ExPerformanceCounter("MSExchange Connection Filtering Agent", "Connections on IP Block List Providers /sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000038 RID: 56
		public static readonly ExPerformanceCounter TotalConnectionsOnIPBlockProviders = new ExPerformanceCounter("MSExchange Connection Filtering Agent", "Connections on IP Block List Providers", string.Empty, null, new ExPerformanceCounter[]
		{
			ConnectionFilteringPerfCounters.ConnectionsOnIPBlockProvidersPerSecond
		});

		// Token: 0x04000039 RID: 57
		private static readonly ExPerformanceCounter MessagesWithOriginatingIPOnIPAllowListPerSecond = new ExPerformanceCounter("MSExchange Connection Filtering Agent", "Messages with Originating IP on IP Allow List/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400003A RID: 58
		public static readonly ExPerformanceCounter TotalMessagesWithOriginatingIPOnIPAllowList = new ExPerformanceCounter("MSExchange Connection Filtering Agent", "Messages with Originating IP on IP Allow List", string.Empty, null, new ExPerformanceCounter[]
		{
			ConnectionFilteringPerfCounters.MessagesWithOriginatingIPOnIPAllowListPerSecond
		});

		// Token: 0x0400003B RID: 59
		private static readonly ExPerformanceCounter MessagesWithOriginatingIPOnIPBlockListPerSecond = new ExPerformanceCounter("MSExchange Connection Filtering Agent", "Messages with Originating IP on IP Block List/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400003C RID: 60
		public static readonly ExPerformanceCounter TotalMessagesWithOriginatingIPOnIPBlockList = new ExPerformanceCounter("MSExchange Connection Filtering Agent", "Messages with Originating IP on IP Block List", string.Empty, null, new ExPerformanceCounter[]
		{
			ConnectionFilteringPerfCounters.MessagesWithOriginatingIPOnIPBlockListPerSecond
		});

		// Token: 0x0400003D RID: 61
		private static readonly ExPerformanceCounter MessagesWithOriginatingIPOnIPAllowProvidersPerSecond = new ExPerformanceCounter("MSExchange Connection Filtering Agent", "Messages with Originating IP on IP Allow List Providers/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400003E RID: 62
		public static readonly ExPerformanceCounter TotalMessagesWithOriginatingIPOnIPAllowProviders = new ExPerformanceCounter("MSExchange Connection Filtering Agent", "Messages with Originating IP on IP Allow List Providers", string.Empty, null, new ExPerformanceCounter[]
		{
			ConnectionFilteringPerfCounters.MessagesWithOriginatingIPOnIPAllowProvidersPerSecond
		});

		// Token: 0x0400003F RID: 63
		private static readonly ExPerformanceCounter MessagesWithOriginatingIPOnIPBlockProvidersPerSecond = new ExPerformanceCounter("MSExchange Connection Filtering Agent", "Messages with Originating IP on IP Block List Providers/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000040 RID: 64
		public static readonly ExPerformanceCounter TotalMessagesWithOriginatingIPOnIPBlockProviders = new ExPerformanceCounter("MSExchange Connection Filtering Agent", "Messages with Originating IP on IP Block List Providers", string.Empty, null, new ExPerformanceCounter[]
		{
			ConnectionFilteringPerfCounters.MessagesWithOriginatingIPOnIPBlockProvidersPerSecond
		});

		// Token: 0x04000041 RID: 65
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			ConnectionFilteringPerfCounters.TotalConnectionsOnIPAllowList,
			ConnectionFilteringPerfCounters.TotalConnectionsOnIPBlockList,
			ConnectionFilteringPerfCounters.TotalConnectionsOnIPAllowProviders,
			ConnectionFilteringPerfCounters.TotalConnectionsOnIPBlockProviders,
			ConnectionFilteringPerfCounters.TotalMessagesWithOriginatingIPOnIPAllowList,
			ConnectionFilteringPerfCounters.TotalMessagesWithOriginatingIPOnIPBlockList,
			ConnectionFilteringPerfCounters.TotalMessagesWithOriginatingIPOnIPAllowProviders,
			ConnectionFilteringPerfCounters.TotalMessagesWithOriginatingIPOnIPBlockProviders
		};
	}
}
