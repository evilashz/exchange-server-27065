using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Directory.Probes
{
	// Token: 0x02000150 RID: 336
	public class TenantRelocationErrorProbe : ProbeWorkItem
	{
		// Token: 0x060009B9 RID: 2489 RVA: 0x0003D143 File Offset: 0x0003B343
		public override void PopulateDefinition<ProbeDefinition>(ProbeDefinition pDef, Dictionary<string, string> propertyBag)
		{
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0003D1CF File Offset: 0x0003B3CF
		protected override void DoWork(CancellationToken cancellationToken)
		{
			DirectoryUtils.Logger(this, StxLogType.TenantRelocationErrorMonitor, delegate
			{
				string name = DirectoryAccessor.Instance.Server.Name;
				base.Result.StateAttribute1 = name;
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.DirectoryTracer, base.TraceContext, "Starting Tenant Relocation requests processing on server {0}", name, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Directory\\TenantRelocationErrorProbe.cs", 52);
				TenantRelocationHealthUtils.CheckTenantRelocationErrors();
				string text = string.Format("There were no errors found in Tenant Relocation processing on server: {0}.", name);
				WTFDiagnostics.TraceInformation(ExTraceGlobals.DirectoryTracer, base.TraceContext, text, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Directory\\TenantRelocationErrorProbe.cs", 65);
				base.Result.StateAttribute2 = text;
			});
		}
	}
}
