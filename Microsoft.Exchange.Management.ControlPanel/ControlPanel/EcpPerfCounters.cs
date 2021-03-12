using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200001C RID: 28
	internal static class EcpPerfCounters
	{
		// Token: 0x06001893 RID: 6291 RVA: 0x0004C4EC File Offset: 0x0004A6EC
		public static void GetPerfCounterInfo(XElement element)
		{
			if (EcpPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in EcpPerfCounters.AllCounters)
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

		// Token: 0x0400195F RID: 6495
		public const string CategoryName = "MSExchange Control Panel";

		// Token: 0x04001960 RID: 6496
		public static readonly ExPerformanceCounter PID = new ExPerformanceCounter("MSExchange Control Panel", "PID", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001961 RID: 6497
		public static readonly ExPerformanceCounter RbacSessions = new ExPerformanceCounter("MSExchange Control Panel", "RBAC Sessions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001962 RID: 6498
		private static readonly ExPerformanceCounter RbacSessionsPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "RBAC Sessions/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001963 RID: 6499
		public static readonly ExPerformanceCounter RbacSessionsTotal = new ExPerformanceCounter("MSExchange Control Panel", "RBAC Sessions - Total", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.RbacSessionsPerSecond
		});

		// Token: 0x04001964 RID: 6500
		public static readonly ExPerformanceCounter RbacSessionsPeak = new ExPerformanceCounter("MSExchange Control Panel", "RBAC Sessions - Peak", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001965 RID: 6501
		public static readonly ExPerformanceCounter AverageRbacSessionCreation = new ExPerformanceCounter("MSExchange Control Panel", "RBAC Sessions - Average Creation Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001966 RID: 6502
		public static readonly ExPerformanceCounter AverageRbacSessionCreationBase = new ExPerformanceCounter("MSExchange Control Panel", "RBAC Sessions - Average Creation Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001967 RID: 6503
		public static readonly ExPerformanceCounter PowerShellRunspace = new ExPerformanceCounter("MSExchange Control Panel", "PowerShell Runspaces", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001968 RID: 6504
		private static readonly ExPerformanceCounter PowerShellRunspacePerSecond = new ExPerformanceCounter("MSExchange Control Panel", "PowerShell Runspaces/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001969 RID: 6505
		public static readonly ExPerformanceCounter PowerShellRunspaceTotal = new ExPerformanceCounter("MSExchange Control Panel", "PowerShell Runspaces - Total", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.PowerShellRunspacePerSecond
		});

		// Token: 0x0400196A RID: 6506
		public static readonly ExPerformanceCounter PowerShellRunspacePeak = new ExPerformanceCounter("MSExchange Control Panel", "PowerShell Runspaces - Peak", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400196B RID: 6507
		public static readonly ExPerformanceCounter AveragePowerShellRunspaceCreation = new ExPerformanceCounter("MSExchange Control Panel", "PowerShell Runspaces - Average Creation Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400196C RID: 6508
		public static readonly ExPerformanceCounter AveragePowerShellRunspaceCreationBase = new ExPerformanceCounter("MSExchange Control Panel", "PowerShell Runspaces - Average Creation Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400196D RID: 6509
		public static readonly ExPerformanceCounter ActiveRunspaces = new ExPerformanceCounter("MSExchange Control Panel", "PowerShell Runspaces - Active", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400196E RID: 6510
		private static readonly ExPerformanceCounter ActiveRunspacesPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "PowerShell Runspaces - Activations/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400196F RID: 6511
		public static readonly ExPerformanceCounter ActiveRunspacesTotal = new ExPerformanceCounter("MSExchange Control Panel", "PowerShell Runspaces - Activations Total", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.ActiveRunspacesPerSecond
		});

		// Token: 0x04001970 RID: 6512
		public static readonly ExPerformanceCounter ActiveRunspacesPeak = new ExPerformanceCounter("MSExchange Control Panel", "PowerShell Runspaces - Peak Active", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001971 RID: 6513
		public static readonly ExPerformanceCounter AverageActiveRunspace = new ExPerformanceCounter("MSExchange Control Panel", "PowerShell Runspaces - Average Active Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001972 RID: 6514
		public static readonly ExPerformanceCounter AverageActiveRunspaceBase = new ExPerformanceCounter("MSExchange Control Panel", "PowerShell Runspaces - Average Active Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001973 RID: 6515
		private static readonly ExPerformanceCounter AspNetErrorsPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "ASP.Net Request Failures/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001974 RID: 6516
		public static readonly ExPerformanceCounter AspNetErrors = new ExPerformanceCounter("MSExchange Control Panel", "ASP.Net Request Failures", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.AspNetErrorsPerSecond
		});

		// Token: 0x04001975 RID: 6517
		private static readonly ExPerformanceCounter WebServiceErrorsPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Web Service Request Failures/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001976 RID: 6518
		public static readonly ExPerformanceCounter WebServiceErrors = new ExPerformanceCounter("MSExchange Control Panel", "Web Service Request Failures", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.WebServiceErrorsPerSecond
		});

		// Token: 0x04001977 RID: 6519
		public static readonly ExPerformanceCounter SendWatson = new ExPerformanceCounter("MSExchange Control Panel", "Watson Reports", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001978 RID: 6520
		private static readonly ExPerformanceCounter RedirectToErrorPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Requests Redirected To Error Page/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001979 RID: 6521
		public static readonly ExPerformanceCounter RedirectToError = new ExPerformanceCounter("MSExchange Control Panel", "Requests Redirected To Error Page", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.RedirectToErrorPerSecond
		});

		// Token: 0x0400197A RID: 6522
		private static readonly ExPerformanceCounter WebServiceGetListPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Web Service - GetList Calls/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400197B RID: 6523
		public static readonly ExPerformanceCounter WebServiceGetList = new ExPerformanceCounter("MSExchange Control Panel", "Web Service - GetList Calls", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.WebServiceGetListPerSecond
		});

		// Token: 0x0400197C RID: 6524
		private static readonly ExPerformanceCounter WebServiceGetObjectPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Web Service - GetObject Calls/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400197D RID: 6525
		public static readonly ExPerformanceCounter WebServiceGetObject = new ExPerformanceCounter("MSExchange Control Panel", "Web Service - GetObject Calls", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.WebServiceGetObjectPerSecond
		});

		// Token: 0x0400197E RID: 6526
		private static readonly ExPerformanceCounter WebServiceNewObjectPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Web Service - NewObject Calls/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400197F RID: 6527
		public static readonly ExPerformanceCounter WebServiceNewObject = new ExPerformanceCounter("MSExchange Control Panel", "Web Service - NewObject Calls", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.WebServiceNewObjectPerSecond
		});

		// Token: 0x04001980 RID: 6528
		private static readonly ExPerformanceCounter WebServiceSetObjectPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Web Service - SetObject Calls/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001981 RID: 6529
		public static readonly ExPerformanceCounter WebServiceSetObject = new ExPerformanceCounter("MSExchange Control Panel", "Web Service - SetObject Calls", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.WebServiceSetObjectPerSecond
		});

		// Token: 0x04001982 RID: 6530
		private static readonly ExPerformanceCounter WebServiceRemoveObjectPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Web Service - RemoveObject Calls/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001983 RID: 6531
		public static readonly ExPerformanceCounter WebServiceRemoveObject = new ExPerformanceCounter("MSExchange Control Panel", "Web Service - RemoveObject Calls", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.WebServiceRemoveObjectPerSecond
		});

		// Token: 0x04001984 RID: 6532
		public static readonly ExPerformanceCounter ActiveRequests = new ExPerformanceCounter("MSExchange Control Panel", "Requests - Active", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001985 RID: 6533
		private static readonly ExPerformanceCounter ActiveRequestsPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Requests - Activations/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001986 RID: 6534
		public static readonly ExPerformanceCounter ActiveRequestsTotal = new ExPerformanceCounter("MSExchange Control Panel", "Requests - Activations Total", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.ActiveRequestsPerSecond
		});

		// Token: 0x04001987 RID: 6535
		public static readonly ExPerformanceCounter ActiveRequestsPeak = new ExPerformanceCounter("MSExchange Control Panel", "Requests - Peak Active", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001988 RID: 6536
		public static readonly ExPerformanceCounter AverageResponseTime = new ExPerformanceCounter("MSExchange Control Panel", "Requests - Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001989 RID: 6537
		public static readonly ExPerformanceCounter AverageResponseTimeBase = new ExPerformanceCounter("MSExchange Control Panel", "Requests - Average Response Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400198A RID: 6538
		public static readonly ExPerformanceCounter OutboundProxySessions = new ExPerformanceCounter("MSExchange Control Panel", "Outbound Proxy Sessions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400198B RID: 6539
		private static readonly ExPerformanceCounter OutboundProxySessionsPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Outbound Proxy Sessions/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400198C RID: 6540
		public static readonly ExPerformanceCounter OutboundProxySessionsTotal = new ExPerformanceCounter("MSExchange Control Panel", "Outbound Proxy Sessions - Total", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.OutboundProxySessionsPerSecond
		});

		// Token: 0x0400198D RID: 6541
		public static readonly ExPerformanceCounter OutboundProxySessionsPeak = new ExPerformanceCounter("MSExchange Control Panel", "Outbound Proxy Sessions - Peak", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400198E RID: 6542
		public static readonly ExPerformanceCounter OutboundProxyRequests = new ExPerformanceCounter("MSExchange Control Panel", "Outbound Proxy Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400198F RID: 6543
		private static readonly ExPerformanceCounter OutboundProxyRequestsPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Outbound Proxy Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001990 RID: 6544
		public static readonly ExPerformanceCounter OutboundProxyRequestsTotal = new ExPerformanceCounter("MSExchange Control Panel", "Outbound Proxy Requests - Total", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.OutboundProxyRequestsPerSecond
		});

		// Token: 0x04001991 RID: 6545
		public static readonly ExPerformanceCounter OutboundProxyRequestsPeak = new ExPerformanceCounter("MSExchange Control Panel", "Outbound Proxy Requests - Peak", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001992 RID: 6546
		public static readonly ExPerformanceCounter AverageOutboundProxyRequestsResponseTime = new ExPerformanceCounter("MSExchange Control Panel", "Outbound Proxy Requests - Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001993 RID: 6547
		public static readonly ExPerformanceCounter AverageOutboundProxyRequestsResponseTimeBase = new ExPerformanceCounter("MSExchange Control Panel", "Outbound Proxy Requests - Average Response Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001994 RID: 6548
		public static readonly ExPerformanceCounter OutboundProxyRequestBytes = new ExPerformanceCounter("MSExchange Control Panel", "Outbound Proxy Request Bytes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001995 RID: 6549
		public static readonly ExPerformanceCounter OutboundProxyResponseBytes = new ExPerformanceCounter("MSExchange Control Panel", "Outbound Proxy Response Bytes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001996 RID: 6550
		public static readonly ExPerformanceCounter EsoOutboundProxySessions = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Outbound Proxy Sessions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001997 RID: 6551
		private static readonly ExPerformanceCounter EsoOutboundProxySessionsPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Outbound Proxy Sessions/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001998 RID: 6552
		public static readonly ExPerformanceCounter EsoOutboundProxySessionsTotal = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Outbound Proxy Sessions - Total", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.EsoOutboundProxySessionsPerSecond
		});

		// Token: 0x04001999 RID: 6553
		public static readonly ExPerformanceCounter EsoOutboundProxySessionsPeak = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Outbound Proxy Sessions - Peak", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400199A RID: 6554
		public static readonly ExPerformanceCounter EsoOutboundProxyRequests = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Outbound Proxy Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400199B RID: 6555
		private static readonly ExPerformanceCounter EsoOutboundProxyRequestsPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Outbound Proxy Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400199C RID: 6556
		public static readonly ExPerformanceCounter EsoOutboundProxyRequestsTotal = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Outbound Proxy Requests - Total", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.EsoOutboundProxyRequestsPerSecond
		});

		// Token: 0x0400199D RID: 6557
		public static readonly ExPerformanceCounter EsoOutboundProxyRequestsPeak = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Outbound Proxy Requests - Peak", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400199E RID: 6558
		public static readonly ExPerformanceCounter AverageEsoOutboundProxyRequestsResponseTime = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Outbound Proxy Requests - Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400199F RID: 6559
		public static readonly ExPerformanceCounter AverageEsoOutboundProxyRequestsResponseTimeBase = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Outbound Proxy Requests - Average Response Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019A0 RID: 6560
		public static readonly ExPerformanceCounter EsoOutboundProxyRequestBytes = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Outbound Proxy Request Bytes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019A1 RID: 6561
		public static readonly ExPerformanceCounter EsoOutboundProxyResponseBytes = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Outbound Proxy Response Bytes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019A2 RID: 6562
		public static readonly ExPerformanceCounter InboundProxySessions = new ExPerformanceCounter("MSExchange Control Panel", "Inbound Proxy Sessions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019A3 RID: 6563
		private static readonly ExPerformanceCounter InboundProxySessionsPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Inbound Proxy Sessions/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019A4 RID: 6564
		public static readonly ExPerformanceCounter InboundProxySessionsTotal = new ExPerformanceCounter("MSExchange Control Panel", "Inbound Proxy Sessions - Total", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.InboundProxySessionsPerSecond
		});

		// Token: 0x040019A5 RID: 6565
		public static readonly ExPerformanceCounter InboundProxySessionsPeak = new ExPerformanceCounter("MSExchange Control Panel", "Inbound Proxy Sessions - Peak", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019A6 RID: 6566
		public static readonly ExPerformanceCounter InboundProxyRequests = new ExPerformanceCounter("MSExchange Control Panel", "Inbound Proxy Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019A7 RID: 6567
		private static readonly ExPerformanceCounter InboundProxyRequestsPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Inbound Proxy Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019A8 RID: 6568
		public static readonly ExPerformanceCounter InboundProxyRequestsTotal = new ExPerformanceCounter("MSExchange Control Panel", "Inbound Proxy Requests - Total", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.InboundProxyRequestsPerSecond
		});

		// Token: 0x040019A9 RID: 6569
		public static readonly ExPerformanceCounter InboundProxyRequestsPeak = new ExPerformanceCounter("MSExchange Control Panel", "Inbound Proxy Requests - Peak", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019AA RID: 6570
		public static readonly ExPerformanceCounter EsoInboundProxySessions = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Inbound Proxy Sessions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019AB RID: 6571
		private static readonly ExPerformanceCounter EsoInboundProxySessionsPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Inbound Proxy Sessions/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019AC RID: 6572
		public static readonly ExPerformanceCounter EsoInboundProxySessionsTotal = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Inbound Proxy Sessions - Total", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.EsoInboundProxySessionsPerSecond
		});

		// Token: 0x040019AD RID: 6573
		public static readonly ExPerformanceCounter EsoInboundProxySessionsPeak = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Inbound Proxy Sessions - Peak", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019AE RID: 6574
		public static readonly ExPerformanceCounter EsoInboundProxyRequests = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Inbound Proxy Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019AF RID: 6575
		private static readonly ExPerformanceCounter EsoInboundProxyRequestsPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Inbound Proxy Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019B0 RID: 6576
		public static readonly ExPerformanceCounter EsoInboundProxyRequestsTotal = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Inbound Proxy Requests - Total", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.EsoInboundProxyRequestsPerSecond
		});

		// Token: 0x040019B1 RID: 6577
		public static readonly ExPerformanceCounter EsoInboundProxyRequestsPeak = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Inbound Proxy Requests - Peak", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019B2 RID: 6578
		public static readonly ExPerformanceCounter StandardSessions = new ExPerformanceCounter("MSExchange Control Panel", "Standard RBAC Sessions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019B3 RID: 6579
		private static readonly ExPerformanceCounter StandardSessionsPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Standard RBAC Sessions/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019B4 RID: 6580
		public static readonly ExPerformanceCounter StandardSessionsTotal = new ExPerformanceCounter("MSExchange Control Panel", "Standard RBAC Sessions - Total", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.StandardSessionsPerSecond
		});

		// Token: 0x040019B5 RID: 6581
		public static readonly ExPerformanceCounter StandardSessionsPeak = new ExPerformanceCounter("MSExchange Control Panel", "Standard RBAC Sessions - Peak", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019B6 RID: 6582
		public static readonly ExPerformanceCounter StandardRequests = new ExPerformanceCounter("MSExchange Control Panel", "Standard RBAC Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019B7 RID: 6583
		private static readonly ExPerformanceCounter StandardRequestsPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Standard RBAC Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019B8 RID: 6584
		public static readonly ExPerformanceCounter StandardRequestsTotal = new ExPerformanceCounter("MSExchange Control Panel", "Standard RBAC Requests - Total", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.StandardRequestsPerSecond
		});

		// Token: 0x040019B9 RID: 6585
		public static readonly ExPerformanceCounter StandardRequestsPeak = new ExPerformanceCounter("MSExchange Control Panel", "Standard RBAC Requests - Peak", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019BA RID: 6586
		public static readonly ExPerformanceCounter EsoStandardSessions = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Standard RBAC Sessions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019BB RID: 6587
		private static readonly ExPerformanceCounter EsoStandardSessionsPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Standard RBAC Sessions/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019BC RID: 6588
		public static readonly ExPerformanceCounter EsoStandardSessionsTotal = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Standard RBAC Sessions - Total", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.EsoStandardSessionsPerSecond
		});

		// Token: 0x040019BD RID: 6589
		public static readonly ExPerformanceCounter EsoStandardSessionsPeak = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Standard RBAC Sessions - Peak", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019BE RID: 6590
		public static readonly ExPerformanceCounter EsoStandardRequests = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Standard RBAC Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019BF RID: 6591
		private static readonly ExPerformanceCounter EsoStandardRequestsPerSecond = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Standard RBAC Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019C0 RID: 6592
		public static readonly ExPerformanceCounter EsoStandardRequestsTotal = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Standard RBAC Requests - Total", string.Empty, null, new ExPerformanceCounter[]
		{
			EcpPerfCounters.EsoStandardRequestsPerSecond
		});

		// Token: 0x040019C1 RID: 6593
		public static readonly ExPerformanceCounter EsoStandardRequestsPeak = new ExPerformanceCounter("MSExchange Control Panel", "Explicit Sign-On Standard RBAC Requests - Peak", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040019C2 RID: 6594
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			EcpPerfCounters.PID,
			EcpPerfCounters.RbacSessions,
			EcpPerfCounters.RbacSessionsTotal,
			EcpPerfCounters.RbacSessionsPeak,
			EcpPerfCounters.AverageRbacSessionCreation,
			EcpPerfCounters.AverageRbacSessionCreationBase,
			EcpPerfCounters.PowerShellRunspace,
			EcpPerfCounters.PowerShellRunspaceTotal,
			EcpPerfCounters.PowerShellRunspacePeak,
			EcpPerfCounters.AveragePowerShellRunspaceCreation,
			EcpPerfCounters.AveragePowerShellRunspaceCreationBase,
			EcpPerfCounters.ActiveRunspaces,
			EcpPerfCounters.ActiveRunspacesTotal,
			EcpPerfCounters.ActiveRunspacesPeak,
			EcpPerfCounters.AverageActiveRunspace,
			EcpPerfCounters.AverageActiveRunspaceBase,
			EcpPerfCounters.AspNetErrors,
			EcpPerfCounters.WebServiceErrors,
			EcpPerfCounters.SendWatson,
			EcpPerfCounters.RedirectToError,
			EcpPerfCounters.WebServiceGetList,
			EcpPerfCounters.WebServiceGetObject,
			EcpPerfCounters.WebServiceNewObject,
			EcpPerfCounters.WebServiceSetObject,
			EcpPerfCounters.WebServiceRemoveObject,
			EcpPerfCounters.ActiveRequests,
			EcpPerfCounters.ActiveRequestsTotal,
			EcpPerfCounters.ActiveRequestsPeak,
			EcpPerfCounters.AverageResponseTime,
			EcpPerfCounters.AverageResponseTimeBase,
			EcpPerfCounters.OutboundProxySessions,
			EcpPerfCounters.OutboundProxySessionsTotal,
			EcpPerfCounters.OutboundProxySessionsPeak,
			EcpPerfCounters.OutboundProxyRequests,
			EcpPerfCounters.OutboundProxyRequestsTotal,
			EcpPerfCounters.OutboundProxyRequestsPeak,
			EcpPerfCounters.AverageOutboundProxyRequestsResponseTime,
			EcpPerfCounters.AverageOutboundProxyRequestsResponseTimeBase,
			EcpPerfCounters.OutboundProxyRequestBytes,
			EcpPerfCounters.OutboundProxyResponseBytes,
			EcpPerfCounters.EsoOutboundProxySessions,
			EcpPerfCounters.EsoOutboundProxySessionsTotal,
			EcpPerfCounters.EsoOutboundProxySessionsPeak,
			EcpPerfCounters.EsoOutboundProxyRequests,
			EcpPerfCounters.EsoOutboundProxyRequestsTotal,
			EcpPerfCounters.EsoOutboundProxyRequestsPeak,
			EcpPerfCounters.AverageEsoOutboundProxyRequestsResponseTime,
			EcpPerfCounters.AverageEsoOutboundProxyRequestsResponseTimeBase,
			EcpPerfCounters.EsoOutboundProxyRequestBytes,
			EcpPerfCounters.EsoOutboundProxyResponseBytes,
			EcpPerfCounters.InboundProxySessions,
			EcpPerfCounters.InboundProxySessionsTotal,
			EcpPerfCounters.InboundProxySessionsPeak,
			EcpPerfCounters.InboundProxyRequests,
			EcpPerfCounters.InboundProxyRequestsTotal,
			EcpPerfCounters.InboundProxyRequestsPeak,
			EcpPerfCounters.EsoInboundProxySessions,
			EcpPerfCounters.EsoInboundProxySessionsTotal,
			EcpPerfCounters.EsoInboundProxySessionsPeak,
			EcpPerfCounters.EsoInboundProxyRequests,
			EcpPerfCounters.EsoInboundProxyRequestsTotal,
			EcpPerfCounters.EsoInboundProxyRequestsPeak,
			EcpPerfCounters.StandardSessions,
			EcpPerfCounters.StandardSessionsTotal,
			EcpPerfCounters.StandardSessionsPeak,
			EcpPerfCounters.StandardRequests,
			EcpPerfCounters.StandardRequestsTotal,
			EcpPerfCounters.StandardRequestsPeak,
			EcpPerfCounters.EsoStandardSessions,
			EcpPerfCounters.EsoStandardSessionsTotal,
			EcpPerfCounters.EsoStandardSessionsPeak,
			EcpPerfCounters.EsoStandardRequests,
			EcpPerfCounters.EsoStandardRequestsTotal,
			EcpPerfCounters.EsoStandardRequestsPeak
		};
	}
}
