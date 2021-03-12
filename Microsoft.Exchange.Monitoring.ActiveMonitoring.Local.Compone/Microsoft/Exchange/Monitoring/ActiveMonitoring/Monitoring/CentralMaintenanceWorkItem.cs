using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Monitoring
{
	// Token: 0x02000218 RID: 536
	public abstract class CentralMaintenanceWorkItem : MaintenanceWorkItem
	{
		// Token: 0x06000F1C RID: 3868 RVA: 0x00064318 File Offset: 0x00062518
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.MonitoringTracer, base.TraceContext, "Starting CentralMaintenanceWorkItem::DoWork()", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Monitoring\\CentralMaintenanceWorkitem.cs", 45);
			if (!this.IsInScope())
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.MonitoringTracer, base.TraceContext, "This workitem does not run in this machine.  Stopping with no maintenance operation", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Monitoring\\CentralMaintenanceWorkitem.cs", 49);
				return;
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.MonitoringTracer, base.TraceContext, "Validated that this maintenance workitem runs on this machine", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Monitoring\\CentralMaintenanceWorkitem.cs", 53);
			this.GenerateWorkItems(cancellationToken);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.MonitoringTracer, base.TraceContext, "Completed CentralMaintenanceWorkItem::DoWork()", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Monitoring\\CentralMaintenanceWorkitem.cs", 57);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x000643C0 File Offset: 0x000625C0
		public bool IsInScope()
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.MonitoringTracer, base.TraceContext, "CentralMaintenanceWorkItem: Determining whether this server is in scope to run this maintenance workitem", null, "IsInScope", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Monitoring\\CentralMaintenanceWorkitem.cs", 68);
			if (!this.IsOspServer())
			{
				return false;
			}
			string text = base.Definition.Attributes["TargetMachineNames"];
			if (string.IsNullOrEmpty(text))
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.MonitoringTracer, base.TraceContext, "CentralMaintenanceWorkItem: This maintenance workitem will run on all machines in the specified machine definition", null, "IsInScope", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Monitoring\\CentralMaintenanceWorkitem.cs", 84);
				return true;
			}
			WTFDiagnostics.TraceInformation(ExTraceGlobals.MonitoringTracer, base.TraceContext, string.Format("CentralMaintenanceWorkItem: Candidate hostnames for this maintenance workitem are: {0}", text), null, "IsInScope", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Monitoring\\CentralMaintenanceWorkitem.cs", 88);
			List<string> source = text.Split(new char[]
			{
				','
			}).ToList<string>();
			string hostName = Dns.GetHostName();
			if (!string.IsNullOrEmpty(hostName) && source.Contains(hostName, StringComparer.InvariantCultureIgnoreCase))
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.MonitoringTracer, base.TraceContext, "CentralMaintenanceWorkItem: Found this server in the machine list.  Will be executing the full maintenance workitem", null, "IsInScope", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Monitoring\\CentralMaintenanceWorkitem.cs", 98);
				return true;
			}
			return false;
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x000644C0 File Offset: 0x000626C0
		internal bool IsOspServer()
		{
			bool result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\ExchangeServer\\v15\\OspServerRole"))
			{
				result = (registryKey != null);
			}
			return result;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00064504 File Offset: 0x00062704
		public List<Tuple<string, string>> GetAzureInstanceList()
		{
			List<Tuple<string, string>> list = new List<Tuple<string, string>>();
			Command item = new Command("Get-AzureGroup");
			using (Runspace runspace = this.CreateCentralAdminRunspace())
			{
				Pipeline pipeline = runspace.CreatePipeline();
				pipeline.Commands.Add(item);
				Collection<PSObject> collection = pipeline.Invoke();
				if (collection != null && collection.Count > 0)
				{
					foreach (PSObject psobject in collection)
					{
						string text = psobject.Properties["Name"].Value as string;
						string item2 = psobject.Properties["AzureWorkload"].Value as string;
						if (!string.IsNullOrEmpty(text))
						{
							list.Add(new Tuple<string, string>(text, item2));
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x000645FC File Offset: 0x000627FC
		public Runspace CreateCentralAdminRunspace()
		{
			RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();
			PSSnapInException ex = null;
			runspaceConfiguration.AddPSSnapIn("Microsoft.Exchange.Management.Powershell.CentralAdmin", out ex);
			if (ex != null)
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.MonitoringTracer, base.TraceContext, string.Format("CentralMaintenanceWorkItem: Loading the Central Admin powershell snapin failed: {0}.  Exception Message: {1}.  Exception detail: {2}", "Microsoft.Exchange.Management.Powershell.CentralAdmin", ex.Message, ex.StackTrace), null, "CreateCentralAdminRunspace", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Monitoring\\CentralMaintenanceWorkitem.cs", 167);
				throw ex;
			}
			Runspace runspace = RunspaceFactory.CreateRunspace(runspaceConfiguration);
			runspace.Open();
			return runspace;
		}

		// Token: 0x06000F21 RID: 3873
		public abstract Task GenerateWorkItems(CancellationToken cancellationToken);

		// Token: 0x04000B45 RID: 2885
		private const string TargetMachineFilterKey = "TargetMachineNames";

		// Token: 0x04000B46 RID: 2886
		private const string CentralAdminPowershellSnapinName = "Microsoft.Exchange.Management.Powershell.CentralAdmin";
	}
}
