using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x020000A6 RID: 166
	public class GenericServiceProbe : ProbeWorkItem
	{
		// Token: 0x060007E1 RID: 2017 RVA: 0x00020C93 File Offset: 0x0001EE93
		public override void PopulateDefinition<ProbeDefinition>(ProbeDefinition definition, Dictionary<string, string> propertyBag)
		{
			definition.TargetResource = propertyBag["TargetResource"];
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00020CB0 File Offset: 0x0001EEB0
		internal override IEnumerable<PropertyInformation> GetSubstitutePropertyInformation()
		{
			return new List<PropertyInformation>
			{
				new PropertyInformation(GenericServiceProbe.TargetResourcePropertyName, StringsLocal.GenericServiceProbeTargetResource, true)
			};
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00020CDF File Offset: 0x0001EEDF
		protected virtual bool ShouldRun()
		{
			return true;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00020CFC File Offset: 0x0001EEFC
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!this.ShouldRun())
			{
				return;
			}
			string windowsServiceName = this.GetWindowsServiceName();
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.ServiceTracer, base.TraceContext, "Starting Service check against {0}", windowsServiceName, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\WorkItems\\Probes\\GenericServiceProbe.cs", 81);
			try
			{
				using (ServiceController serviceController = new ServiceController(windowsServiceName))
				{
					if (serviceController.Status != ServiceControllerStatus.Running)
					{
						base.Result.StateAttribute1 = serviceController.Status.ToString();
						string message = string.Format("{0} service is not running", windowsServiceName);
						WTFDiagnostics.TraceError(ExTraceGlobals.ServiceTracer, base.TraceContext, message, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\common\\src\\WorkerTaskFramework\\LocalDataAccess\\WorkItems\\Probes\\GenericServiceProbe.cs", 102);
						throw new Exception(message);
					}
					if (base.Broker != null)
					{
						IDataAccessQuery<ProbeResult> probeResults = base.Broker.GetProbeResults(base.Definition, GenericServiceProbe.systemBootTime);
						Task<ProbeResult> task = probeResults.ExecuteAsync(cancellationToken, base.TraceContext);
						task.Continue(delegate(ProbeResult lastProbeResult)
						{
							if (lastProbeResult == null)
							{
								StartupNotification.InsertStartupNotification(windowsServiceName);
							}
						}, cancellationToken, TaskContinuationOptions.AttachedToParent);
					}
				}
			}
			finally
			{
				stopwatch.Stop();
				base.Result.SampleValue = (double)((int)stopwatch.ElapsedMilliseconds);
			}
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00020E58 File Offset: 0x0001F058
		protected string GetWindowsServiceName()
		{
			if (base.Definition.Attributes.ContainsKey("WindowsServiceName"))
			{
				return base.Definition.Attributes["WindowsServiceName"];
			}
			return base.Definition.TargetResource;
		}

		// Token: 0x04000608 RID: 1544
		public static readonly string TargetResourcePropertyName = "TargetResource";

		// Token: 0x04000609 RID: 1545
		private static DateTime systemBootTime = StartupNotification.GetSystemBootTime(true);

		// Token: 0x020000A7 RID: 167
		internal static class AttributeNames
		{
			// Token: 0x0400060A RID: 1546
			internal const string WindowsServiceName = "WindowsServiceName";
		}
	}
}
