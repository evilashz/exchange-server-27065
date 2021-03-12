using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Configuration.Core;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Configuration.Core
{
	// Token: 0x02000009 RID: 9
	internal static class Constants
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002C95 File Offset: 0x00000E95
		internal static ExEventLog CoreEventLogger
		{
			get
			{
				return Constants.coreEventLogger.Value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002CA4 File Offset: 0x00000EA4
		internal static int ProcessId
		{
			get
			{
				if (Constants.processId == null)
				{
					using (Process currentProcess = Process.GetCurrentProcess())
					{
						Constants.processId = new int?(currentProcess.Id);
					}
				}
				return Constants.processId.Value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002CFC File Offset: 0x00000EFC
		internal static string ProcessName
		{
			get
			{
				return ExWatson.RealAppName;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002D03 File Offset: 0x00000F03
		internal static bool IsPowerShellWebService
		{
			get
			{
				return EventLogConstants.IsPowerShellWebService;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002D0A File Offset: 0x00000F0A
		internal static bool IsRemotePS
		{
			get
			{
				return !Constants.IsPowerShellWebService;
			}
		}

		// Token: 0x04000017 RID: 23
		public const string HttpContextUserSidItemKey = "X-EX-UserSid";

		// Token: 0x04000018 RID: 24
		private static readonly Lazy<ExEventLog> coreEventLogger = new Lazy<ExEventLog>(() => new ExEventLog(ExTraceGlobals.InstrumentationTracer.Category, "MSExchange Configuration Core"));

		// Token: 0x04000019 RID: 25
		private static int? processId;
	}
}
