using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Directory.Probes
{
	// Token: 0x02000148 RID: 328
	public class SharedConfigurationTenantProbe : ProbeWorkItem
	{
		// Token: 0x0600098D RID: 2445 RVA: 0x0003A5FB File Offset: 0x000387FB
		public override void PopulateDefinition<ProbeDefinition>(ProbeDefinition pDef, Dictionary<string, string> propertyBag)
		{
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0003A687 File Offset: 0x00038887
		protected override void DoWork(CancellationToken cancellationToken)
		{
			DirectoryUtils.Logger(this, StxLogType.SharedConfigurationTenantMonitor, delegate
			{
				string name = DirectoryAccessor.Instance.Server.Name;
				base.Result.StateAttribute1 = name;
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.DirectoryTracer, base.TraceContext, "Starting verification of Shared configuration tenants in server: {0}", name, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Directory\\SharedConfigurationTenantProbe.cs", 52);
				DirectoryUtils.CheckSharedConfigurationTenants();
				string text = string.Format("SCTs are created for all supported versions: {0}.", name);
				WTFDiagnostics.TraceInformation(ExTraceGlobals.DirectoryTracer, base.TraceContext, text, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Directory\\SharedConfigurationTenantProbe.cs", 64);
				base.Result.StateAttribute2 = text;
			});
		}
	}
}
