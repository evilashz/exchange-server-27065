using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Fips
{
	// Token: 0x02000510 RID: 1296
	public class CheckAVEngineEnabled : ProbeWorkItem
	{
		// Token: 0x06001FE7 RID: 8167 RVA: 0x000C2CA0 File Offset: 0x000C0EA0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			this.TraceDebug("CheckAVEngineEnabledProbe started.");
			string[] enabledEngines = this.GetEnabledEngines();
			if (enabledEngines == null || enabledEngines.Length != 3)
			{
				string text = (enabledEngines != null) ? string.Format("Only {0} engines enabled. Currently enabled engines are {1}", enabledEngines.Length, string.Join(",", enabledEngines)) : "Unable to get the enabled engines";
				base.Result.Error = text;
				this.TraceError(text);
				throw new ApplicationException(text);
			}
			this.TraceDebug("CheckAVEngineEnabledProbe finished with success.");
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x000C2D18 File Offset: 0x000C0F18
		private string[] GetEnabledEngines()
		{
			try
			{
				Collection<PSObject> collection = FipsUtils.RunFipsCmdlet<object>("Get-AntivirusScanSettings", null);
				using (IEnumerator<PSObject> enumerator = collection.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						PSObject psobject = enumerator.Current;
						return psobject.Properties["Engines"].Value as string[];
					}
				}
			}
			catch (Exception ex)
			{
				base.Result.Error = ex.Message;
				this.TraceError(ex.Message);
				throw;
			}
			return null;
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x000C2DB8 File Offset: 0x000C0FB8
		protected virtual void TraceDebug(string message)
		{
			ProbeResult result = base.Result;
			result.ExecutionContext = result.ExecutionContext + message + " ";
			WTFDiagnostics.TraceDebug(ExTraceGlobals.FIPSTracer, base.TraceContext, message, null, "TraceDebug", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\FIPS\\CheckAVEngineEnabled.cs", 95);
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x000C2DF4 File Offset: 0x000C0FF4
		protected virtual void TraceError(string message)
		{
			ProbeResult result = base.Result;
			result.ExecutionContext = result.ExecutionContext + message + " ";
			WTFDiagnostics.TraceError(ExTraceGlobals.FIPSTracer, base.TraceContext, message, null, "TraceError", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\FIPS\\CheckAVEngineEnabled.cs", 105);
		}

		// Token: 0x04001772 RID: 6002
		private const string CmdletGetAntivirusScanSettings = "Get-AntivirusScanSettings";
	}
}
