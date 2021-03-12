using System;
using System.Web;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C12 RID: 3090
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class EventLogConstants
	{
		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x060043AD RID: 17325 RVA: 0x000B5D25 File Offset: 0x000B3F25
		internal static bool IsPowerShellWebService
		{
			get
			{
				if (EventLogConstants.isPowerShellWebService == null)
				{
					EventLogConstants.isPowerShellWebService = new bool?(EventLogConstants.CalculateIsPswsFlag());
				}
				return EventLogConstants.isPowerShellWebService.Value;
			}
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x060043AE RID: 17326 RVA: 0x000B5D4C File Offset: 0x000B3F4C
		internal static ExEventLog NetEventLogger
		{
			get
			{
				return EventLogConstants.netEventLogger.Value;
			}
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x000B5D58 File Offset: 0x000B3F58
		private static bool CalculateIsPswsFlag()
		{
			try
			{
				if (HttpContext.Current == null || HttpContext.Current.Request == null || HttpContext.Current.Request.Url == null)
				{
					return false;
				}
			}
			catch (HttpException)
			{
				return false;
			}
			bool result;
			try
			{
				Uri url = HttpContext.Current.Request.Url;
				if (url.AbsolutePath == null)
				{
					result = false;
				}
				else
				{
					result = url.AbsolutePath.StartsWith("/psws", StringComparison.OrdinalIgnoreCase);
				}
			}
			catch (InvalidOperationException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x040039A0 RID: 14752
		private static readonly Lazy<ExEventLog> netEventLogger = new Lazy<ExEventLog>(() => new ExEventLog(ExTraceGlobals.AppSettingsTracer.Category, "MSExchange Net"));

		// Token: 0x040039A1 RID: 14753
		private static bool? isPowerShellWebService;
	}
}
