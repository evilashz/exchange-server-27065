using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Directory.Probes
{
	// Token: 0x02000151 RID: 337
	public static class TenantRelocationHealthUtils
	{
		// Token: 0x060009BD RID: 2493 RVA: 0x0003D1F0 File Offset: 0x0003B3F0
		public static void CheckTenantRelocationErrors()
		{
			try
			{
				int num = 0;
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("Following tenant relocation requests have failed with Permanent errors.");
				stringBuilder.AppendLine("TenantName, RelocationStatus, RelocationError");
				IEnumerable<TenantRelocationRequest> enumerable = PartitionDataAggregator.FindPagedRelocationRequestsWithUnclassifiedPermanentError();
				foreach (TenantRelocationRequest tenantRelocationRequest in enumerable)
				{
					num++;
					stringBuilder.AppendLine(string.Format("{0}, {1}, {2}", tenantRelocationRequest.Name, tenantRelocationRequest.RelocationStatus.ToString(), tenantRelocationRequest.RelocationLastError.ToString()));
					if (num >= 30)
					{
						stringBuilder.AppendLine(string.Format("\nAbove tenant list is the top {0} tenants where relocations have failed.  Please investigate.", 30));
						break;
					}
				}
				if (num > 0)
				{
					throw new ApplicationException(stringBuilder.ToString());
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.DirectoryTracer, TenantRelocationHealthUtils.traceContext, "CheckTenantRelocationErrors::Got Exception: {0}", ex.ToString(), null, "CheckTenantRelocationErrors", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Directory\\TenantRelocationHealthUtils.cs", 73);
				throw;
			}
		}

		// Token: 0x0400070B RID: 1803
		private const int MaximumTenantErrorCount = 30;

		// Token: 0x0400070C RID: 1804
		private static TracingContext traceContext = TracingContext.Default;
	}
}
