using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000048 RID: 72
	internal static class RwsPerfCounters
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x000092A0 File Offset: 0x000074A0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (RwsPerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in RwsPerfCounters.AllCounters)
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

		// Token: 0x04000150 RID: 336
		public const string CategoryName = "MSExchange ReportingWebService";

		// Token: 0x04000151 RID: 337
		public static readonly ExPerformanceCounter PID = new ExPerformanceCounter("MSExchange ReportingWebService", "PID", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000152 RID: 338
		public static readonly ExPerformanceCounter RbacPrincipals = new ExPerformanceCounter("MSExchange ReportingWebService", "RBAC Principals", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000153 RID: 339
		private static readonly ExPerformanceCounter RbacPrincipalsPerSecond = new ExPerformanceCounter("MSExchange ReportingWebService", "RBAC Principals/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000154 RID: 340
		public static readonly ExPerformanceCounter RbacPrincipalsTotal = new ExPerformanceCounter("MSExchange ReportingWebService", "RBAC Principals - Total", string.Empty, null, new ExPerformanceCounter[]
		{
			RwsPerfCounters.RbacPrincipalsPerSecond
		});

		// Token: 0x04000155 RID: 341
		public static readonly ExPerformanceCounter RbacPrincipalsPeak = new ExPerformanceCounter("MSExchange ReportingWebService", "RBAC Principals - Peak", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000156 RID: 342
		public static readonly ExPerformanceCounter AverageRbacPrincipalCreation = new ExPerformanceCounter("MSExchange ReportingWebService", "RBAC Principals - Average Creation Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000157 RID: 343
		public static readonly ExPerformanceCounter AverageRbacPrincipalCreationBase = new ExPerformanceCounter("MSExchange ReportingWebService", "RBAC Principals - Average Creation Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000158 RID: 344
		public static readonly ExPerformanceCounter PowerShellRunspace = new ExPerformanceCounter("MSExchange ReportingWebService", "PowerShell Runspaces", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000159 RID: 345
		private static readonly ExPerformanceCounter PowerShellRunspacePerSecond = new ExPerformanceCounter("MSExchange ReportingWebService", "PowerShell Runspaces/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400015A RID: 346
		public static readonly ExPerformanceCounter PowerShellRunspaceTotal = new ExPerformanceCounter("MSExchange ReportingWebService", "PowerShell Runspaces - Total", string.Empty, null, new ExPerformanceCounter[]
		{
			RwsPerfCounters.PowerShellRunspacePerSecond
		});

		// Token: 0x0400015B RID: 347
		public static readonly ExPerformanceCounter PowerShellRunspacePeak = new ExPerformanceCounter("MSExchange ReportingWebService", "PowerShell Runspaces - Peak", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400015C RID: 348
		public static readonly ExPerformanceCounter AveragePowerShellRunspaceCreation = new ExPerformanceCounter("MSExchange ReportingWebService", "PowerShell Runspaces - Average Creation Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400015D RID: 349
		public static readonly ExPerformanceCounter AveragePowerShellRunspaceCreationBase = new ExPerformanceCounter("MSExchange ReportingWebService", "PowerShell Runspaces - Average Creation Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400015E RID: 350
		public static readonly ExPerformanceCounter ActiveRunspaces = new ExPerformanceCounter("MSExchange ReportingWebService", "PowerShell Runspaces - Active", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400015F RID: 351
		private static readonly ExPerformanceCounter ActiveRunspacesPerSecond = new ExPerformanceCounter("MSExchange ReportingWebService", "PowerShell Runspaces - Activations/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000160 RID: 352
		public static readonly ExPerformanceCounter ActiveRunspacesTotal = new ExPerformanceCounter("MSExchange ReportingWebService", "PowerShell Runspaces - Activations Total", string.Empty, null, new ExPerformanceCounter[]
		{
			RwsPerfCounters.ActiveRunspacesPerSecond
		});

		// Token: 0x04000161 RID: 353
		public static readonly ExPerformanceCounter ActiveRunspacesPeak = new ExPerformanceCounter("MSExchange ReportingWebService", "PowerShell Runspaces - Peak Active", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000162 RID: 354
		public static readonly ExPerformanceCounter AverageActiveRunspace = new ExPerformanceCounter("MSExchange ReportingWebService", "PowerShell Runspaces - Average Active Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000163 RID: 355
		public static readonly ExPerformanceCounter AverageActiveRunspaceBase = new ExPerformanceCounter("MSExchange ReportingWebService", "PowerShell Runspaces - Average Active Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000164 RID: 356
		private static readonly ExPerformanceCounter RequestErrorsPerSecond = new ExPerformanceCounter("MSExchange ReportingWebService", "Request Failures/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000165 RID: 357
		public static readonly ExPerformanceCounter ReportCmdletErrors = new ExPerformanceCounter("MSExchange ReportingWebService", "Report Cmdlet Failures", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000166 RID: 358
		private static readonly ExPerformanceCounter ReportCmdletErrorsPerSecond = new ExPerformanceCounter("MSExchange ReportingWebService", "Report Cmdlet Failures/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000167 RID: 359
		public static readonly ExPerformanceCounter RequestErrors = new ExPerformanceCounter("MSExchange ReportingWebService", "Request Failures", string.Empty, null, new ExPerformanceCounter[]
		{
			RwsPerfCounters.RequestErrorsPerSecond,
			RwsPerfCounters.ReportCmdletErrorsPerSecond
		});

		// Token: 0x04000168 RID: 360
		public static readonly ExPerformanceCounter SendWatson = new ExPerformanceCounter("MSExchange ReportingWebService", "Watson Reports", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000169 RID: 361
		public static readonly ExPerformanceCounter ActiveRequests = new ExPerformanceCounter("MSExchange ReportingWebService", "Requests - Active", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400016A RID: 362
		private static readonly ExPerformanceCounter ActiveRequestsPerSecond = new ExPerformanceCounter("MSExchange ReportingWebService", "Requests - Activations/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400016B RID: 363
		public static readonly ExPerformanceCounter ActiveRequestsTotal = new ExPerformanceCounter("MSExchange ReportingWebService", "Requests - Activations Total", string.Empty, null, new ExPerformanceCounter[]
		{
			RwsPerfCounters.ActiveRequestsPerSecond
		});

		// Token: 0x0400016C RID: 364
		public static readonly ExPerformanceCounter ActiveRequestsPeak = new ExPerformanceCounter("MSExchange ReportingWebService", "Requests - Peak Active", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400016D RID: 365
		public static readonly ExPerformanceCounter AverageRequestResponseTime = new ExPerformanceCounter("MSExchange ReportingWebService", "Requests - Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400016E RID: 366
		public static readonly ExPerformanceCounter AverageRequestResponseTimeBase = new ExPerformanceCounter("MSExchange ReportingWebService", "Requests - Average Response Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400016F RID: 367
		public static readonly ExPerformanceCounter AverageReportCmdletResponseTime = new ExPerformanceCounter("MSExchange ReportingWebService", "Report Cmdlet - Average Response Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000170 RID: 368
		public static readonly ExPerformanceCounter AverageReportCmdletResponseTimeBase = new ExPerformanceCounter("MSExchange ReportingWebService", "Report Cmdlet - Average Response Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000171 RID: 369
		public static readonly ExPerformanceCounter AverageReportRow = new ExPerformanceCounter("MSExchange ReportingWebService", "Average Report Row", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000172 RID: 370
		public static readonly ExPerformanceCounter AverageReportRowBase = new ExPerformanceCounter("MSExchange ReportingWebService", "Average Report Row Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000173 RID: 371
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			RwsPerfCounters.PID,
			RwsPerfCounters.RbacPrincipals,
			RwsPerfCounters.RbacPrincipalsTotal,
			RwsPerfCounters.RbacPrincipalsPeak,
			RwsPerfCounters.AverageRbacPrincipalCreation,
			RwsPerfCounters.AverageRbacPrincipalCreationBase,
			RwsPerfCounters.PowerShellRunspace,
			RwsPerfCounters.PowerShellRunspaceTotal,
			RwsPerfCounters.PowerShellRunspacePeak,
			RwsPerfCounters.AveragePowerShellRunspaceCreation,
			RwsPerfCounters.AveragePowerShellRunspaceCreationBase,
			RwsPerfCounters.ActiveRunspaces,
			RwsPerfCounters.ActiveRunspacesTotal,
			RwsPerfCounters.ActiveRunspacesPeak,
			RwsPerfCounters.AverageActiveRunspace,
			RwsPerfCounters.AverageActiveRunspaceBase,
			RwsPerfCounters.RequestErrors,
			RwsPerfCounters.ReportCmdletErrors,
			RwsPerfCounters.SendWatson,
			RwsPerfCounters.ActiveRequests,
			RwsPerfCounters.ActiveRequestsTotal,
			RwsPerfCounters.ActiveRequestsPeak,
			RwsPerfCounters.AverageRequestResponseTime,
			RwsPerfCounters.AverageRequestResponseTimeBase,
			RwsPerfCounters.AverageReportCmdletResponseTime,
			RwsPerfCounters.AverageReportCmdletResponseTimeBase,
			RwsPerfCounters.AverageReportRow,
			RwsPerfCounters.AverageReportRowBase
		};
	}
}
