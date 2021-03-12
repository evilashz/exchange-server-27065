using System;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Directory
{
	// Token: 0x02000157 RID: 343
	public class PutMultipleDCInMMResponder : ResponderWorkItem
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x0003DFCB File Offset: 0x0003C1CB
		// (set) Token: 0x060009DE RID: 2526 RVA: 0x0003DFD3 File Offset: 0x0003C1D3
		internal string TargetServerFQDN { get; set; }

		// Token: 0x060009DF RID: 2527 RVA: 0x0003DFDC File Offset: 0x0003C1DC
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetServer, ServiceHealthStatus targetHealthState, string throttleGroupName = "DomainController")
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = PutMultipleDCInMMResponder.AssemblyPath;
			responderDefinition.TypeName = PutMultipleDCInMMResponder.TypeName;
			responderDefinition.Name = name;
			responderDefinition.ServiceName = serviceName;
			responderDefinition.AlertTypeId = alertTypeId;
			responderDefinition.AlertMask = alertMask;
			responderDefinition.TargetResource = targetServer;
			responderDefinition.TargetHealthState = targetHealthState;
			responderDefinition.RecurrenceIntervalSeconds = 1200;
			responderDefinition.WaitIntervalSeconds = 60;
			responderDefinition.TimeoutSeconds = 600;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.Enabled = true;
			responderDefinition.StartTime = DateTime.UtcNow;
			responderDefinition.Attributes["TargetServerFQDN"] = targetServer;
			string resourceName = string.Empty;
			if (string.IsNullOrEmpty(targetServer))
			{
				resourceName = DirectoryGeneralUtils.GetLocalFQDN();
			}
			else
			{
				resourceName = targetServer;
			}
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, throttleGroupName, RecoveryActionId.PutMultipleDCInMM, resourceName, null);
			return responderDefinition;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0003E0A1 File Offset: 0x0003C2A1
		protected void InitializeAttributes(AttributeHelper attributeHelper)
		{
			this.TargetServerFQDN = attributeHelper.GetString("TargetServerFQDN", true, DirectoryGeneralUtils.GetLocalFQDN());
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x0003E0BC File Offset: 0x0003C2BC
		protected virtual void InitializeAttributes()
		{
			AttributeHelper attributeHelper = new AttributeHelper(base.Definition);
			this.InitializeAttributes(attributeHelper);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0003E0F8 File Offset: 0x0003C2F8
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			this.InitializeAttributes();
			string resourceName = string.Empty;
			if (string.IsNullOrEmpty(this.TargetServerFQDN))
			{
				resourceName = DirectoryGeneralUtils.GetLocalFQDN();
			}
			else
			{
				resourceName = this.TargetServerFQDN;
			}
			string dcsToRecover = string.Empty;
			MonitorResult lastFailedMonitorResult = WorkItemResultHelper.GetLastFailedMonitorResult(this, base.Broker, cancellationToken);
			if (lastFailedMonitorResult == null)
			{
				base.Result.StateAttribute1 = "Unable to get Target DCs for recovery from last failed probe result.";
				return;
			}
			dcsToRecover = lastFailedMonitorResult.StateAttribute2;
			if (string.IsNullOrEmpty(dcsToRecover))
			{
				base.Result.StateAttribute1 = "DCs to recover (ProbeResult.StateAttribute2) was empty in the last failed probe result.";
				return;
			}
			base.Result.StateAttribute1 = string.Format("DCs for recover:  {0}", dcsToRecover);
			new RecoveryActionRunner(RecoveryActionId.PutMultipleDCInMM, resourceName, this, true, cancellationToken, null)
			{
				IsIgnoreResourceName = true
			}.Execute(delegate()
			{
				this.DirectoryPutDCInMM(dcsToRecover);
			});
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DirectoryTracer, base.TraceContext, "Putting DC into MM is Completed", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Directory\\PutMultipleDCInMMResponder.cs", 189);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0003E1FC File Offset: 0x0003C3FC
		private void DirectoryPutDCInMM(string dcsToRecover)
		{
			if (string.IsNullOrEmpty(dcsToRecover))
			{
				base.Result.StateAttribute1 = "DCs to recover (ProbeResult.StateAttribute2) was empty in the last failed probe result.";
				return;
			}
			string[] array = dcsToRecover.Split(new char[]
			{
				','
			});
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string targetFqdn in array)
			{
				string value = DirectoryGeneralUtils.InternalPutDCInMM(targetFqdn, base.TraceContext);
				stringBuilder.Append(value);
				stringBuilder.AppendLine();
			}
			base.Result.StateAttribute1 = stringBuilder.ToString();
		}

		// Token: 0x04000715 RID: 1813
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000716 RID: 1814
		private static readonly string TypeName = typeof(PutMultipleDCInMMResponder).FullName;

		// Token: 0x02000158 RID: 344
		internal static class AttributeNames
		{
			// Token: 0x04000718 RID: 1816
			internal const string TargetServerFQDN = "TargetServerFQDN";

			// Token: 0x04000719 RID: 1817
			internal const string throttleGroupName = "throttleGroupName";
		}
	}
}
