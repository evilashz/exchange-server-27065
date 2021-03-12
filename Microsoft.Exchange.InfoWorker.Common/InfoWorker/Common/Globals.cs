using System;
using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x02000002 RID: 2
	internal static class Globals
	{
		// Token: 0x04000001 RID: 1
		public const string ComponentGuidString = "3A8BB7C6-6298-45eb-BE95-1A3AF02F7FFA";

		// Token: 0x04000002 RID: 2
		public const string AvailabilityEventSource = "MSExchange Availability";

		// Token: 0x04000003 RID: 3
		public const string OOFEventSource = "MSExchangeMailboxAssistants";

		// Token: 0x04000004 RID: 4
		public const string ELCEventSource = "MSExchangeMailboxAssistants";

		// Token: 0x04000005 RID: 5
		public const string ServicesNamespaceBase = "http://schemas.microsoft.com/exchange/services/2006";

		// Token: 0x04000006 RID: 6
		public const string ServicesTypeNamespace = "http://schemas.microsoft.com/exchange/services/2006/types";

		// Token: 0x04000007 RID: 7
		public const string ServicesErrorNamespace = "http://schemas.microsoft.com/exchange/services/2006/errors";

		// Token: 0x04000008 RID: 8
		public static readonly Delayed<string> ProcessId = new Delayed<string>(delegate()
		{
			string text = "unknown";
			try
			{
				text = AppDomain.CurrentDomain.FriendlyName;
			}
			catch (SystemException)
			{
			}
			string text2 = "unknown";
			string text3 = "unknown";
			try
			{
				using (Process currentProcess = Process.GetCurrentProcess())
				{
					text2 = currentProcess.Id.ToString();
					text3 = currentProcess.MainModule.ModuleName;
				}
			}
			catch (Win32Exception)
			{
			}
			return string.Concat(new string[]
			{
				text2,
				"[",
				text3,
				":",
				text,
				"]"
			});
		});

		// Token: 0x04000009 RID: 9
		public static readonly Guid ComponentGuid = new Guid("3A8BB7C6-6298-45eb-BE95-1A3AF02F7FFA");

		// Token: 0x0400000A RID: 10
		public static ExEventLog AvailabilityLogger = new ExEventLog(ExTraceGlobals.SingleInstanceItemTracer.Category, "MSExchange Availability");

		// Token: 0x0400000B RID: 11
		public static ExEventLog OOFLogger = new ExEventLog(ExTraceGlobals.SingleInstanceItemTracer.Category, "MSExchangeMailboxAssistants");

		// Token: 0x0400000C RID: 12
		public static ExEventLog ELCLogger = new ExEventLog(ExTraceGlobals.ELCTracer.Category, "MSExchangeMailboxAssistants");

		// Token: 0x0400000D RID: 13
		public static readonly string CertificateValidationComponentId = "AvailabilityService";

		// Token: 0x0400000E RID: 14
		public static readonly int E14Version = new ServerVersion(14, 0, 0, 0).ToInt();

		// Token: 0x0400000F RID: 15
		public static readonly int E14SP1Version = new ServerVersion(14, 1, 0, 0).ToInt();

		// Token: 0x04000010 RID: 16
		public static readonly int E14SP2Version = new ServerVersion(14, 2, 0, 0).ToInt();

		// Token: 0x04000011 RID: 17
		public static readonly int E14SP3Version = new ServerVersion(14, 3, 0, 0).ToInt();

		// Token: 0x04000012 RID: 18
		public static readonly int E15Version = new ServerVersion(15, 0, 0, 0).ToInt();
	}
}
