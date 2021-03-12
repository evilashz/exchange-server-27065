using System;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Directory
{
	// Token: 0x0200015B RID: 347
	public class RenameNTDSPowerOffResponder : ResponderWorkItem
	{
		// Token: 0x060009EF RID: 2543 RVA: 0x0003E6A4 File Offset: 0x0003C8A4
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, ServiceHealthStatus targetHealthState, string throttleGroupName = "DomainController")
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = RenameNTDSPowerOffResponder.AssemblyPath;
			responderDefinition.TypeName = RenameNTDSPowerOffResponder.TypeName;
			responderDefinition.Name = name;
			responderDefinition.ServiceName = serviceName;
			responderDefinition.AlertTypeId = alertTypeId;
			responderDefinition.AlertMask = alertMask;
			responderDefinition.TargetResource = DirectoryGeneralUtils.GetLocalFQDN();
			responderDefinition.TargetHealthState = targetHealthState;
			responderDefinition.RecurrenceIntervalSeconds = 1200;
			responderDefinition.WaitIntervalSeconds = 60;
			responderDefinition.TimeoutSeconds = 600;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.Enabled = true;
			responderDefinition.StartTime = DateTime.UtcNow;
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, throttleGroupName, RecoveryActionId.RenameNTDSPowerOff, DirectoryGeneralUtils.GetLocalFQDN(), null);
			return responderDefinition;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0003E74C File Offset: 0x0003C94C
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			string localFQDN = DirectoryGeneralUtils.GetLocalFQDN();
			new RecoveryActionRunner(RecoveryActionId.RenameNTDSPowerOff, localFQDN, this, true, cancellationToken, null)
			{
				IsIgnoreResourceName = true
			}.Execute(delegate()
			{
				this.RenameNTDSPowerOff();
			});
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DirectoryTracer, base.TraceContext, "Rename NTDS and Power Off is Completed", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Directory\\RenameNTDSPowerOffResponder.cs", 114);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0003E7B0 File Offset: 0x0003C9B0
		private void RenameNTDSPowerOff()
		{
			string localFQDN = DirectoryGeneralUtils.GetLocalFQDN();
			string stateAttribute = DirectoryGeneralUtils.InternalPutDCInMM(localFQDN, base.TraceContext);
			base.Result.StateAttribute1 = stateAttribute;
			DirectoryGeneralUtils.MarkDCAsDemote(localFQDN);
			DirectoryGeneralUtils.RetryWhileException(delegate
			{
				this.RenameNTDSPowerOffCore();
			});
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0003E7F4 File Offset: 0x0003C9F4
		private void RenameNTDSPowerOffCore()
		{
			if (Directory.Exists("D:\\NTDS"))
			{
				string text = string.Format("D:\\NTDS_{0}", DateTime.UtcNow.ToString("yyMMddhhmmss"));
				Directory.Move("D:\\NTDS", text);
				WTFDiagnostics.TraceInformation(ExTraceGlobals.DirectoryTracer, base.TraceContext, string.Format("Renamed NTDS folder to {}", text), null, "RenameNTDSPowerOffCore", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Directory\\RenameNTDSPowerOffResponder.cs", 148);
			}
			DirectoryGeneralUtils.InvokeCommand("Shutdown", "-s -t 0");
		}

		// Token: 0x0400071D RID: 1821
		private const string NDTSDirectory = "D:\\NTDS";

		// Token: 0x0400071E RID: 1822
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400071F RID: 1823
		private static readonly string TypeName = typeof(RenameNTDSPowerOffResponder).FullName;

		// Token: 0x0200015C RID: 348
		internal static class AttributeNames
		{
			// Token: 0x04000720 RID: 1824
			internal const string throttleGroupName = "throttleGroupName";
		}
	}
}
