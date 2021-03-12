using System;
using System.IO;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Fips
{
	// Token: 0x0200050A RID: 1290
	internal class OldFileDeletionPolicy : IFileDeletionPolicy
	{
		// Token: 0x06001FD2 RID: 8146 RVA: 0x000C2524 File Offset: 0x000C0724
		public OldFileDeletionPolicy(TimeSpan lifeTime)
		{
			this.lifeTime = lifeTime;
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x000C2540 File Offset: 0x000C0740
		public bool ShouldDelete(string filePath)
		{
			bool result = false;
			try
			{
				if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
				{
					FileInfo fileInfo = new FileInfo(filePath);
					if (fileInfo.CreationTime.ToUniversalTime() <= DateTime.UtcNow.AddMilliseconds(-1.0 * this.lifeTime.TotalMilliseconds))
					{
						result = true;
					}
				}
			}
			catch (Exception ex)
			{
				WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.FIPSTracer, this.traceContext, "The file {0} could not be deleted. Exception: {1}", filePath, ex.Message, null, "ShouldDelete", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\FIPS\\OldFileDeletionPolicy.cs", 74);
			}
			return result;
		}

		// Token: 0x04001744 RID: 5956
		private readonly TimeSpan lifeTime;

		// Token: 0x04001745 RID: 5957
		private TracingContext traceContext = TracingContext.Default;
	}
}
