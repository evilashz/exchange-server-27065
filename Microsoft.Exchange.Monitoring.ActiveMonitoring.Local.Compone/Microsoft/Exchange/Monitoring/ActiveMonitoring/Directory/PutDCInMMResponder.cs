using System;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Directory
{
	// Token: 0x02000155 RID: 341
	public class PutDCInMMResponder : ResponderWorkItem
	{
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x0003DD38 File Offset: 0x0003BF38
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x0003DD40 File Offset: 0x0003BF40
		internal string TargetServerFQDN { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0003DD49 File Offset: 0x0003BF49
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x0003DD51 File Offset: 0x0003BF51
		internal bool IgnoreGroupThrottlingWhenMajorityNotSucceeded { get; set; }

		// Token: 0x060009D5 RID: 2517 RVA: 0x0003DD5C File Offset: 0x0003BF5C
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetServer, ServiceHealthStatus targetHealthState, bool isIgnoreGroupThrottlingWhenMajorityNotSucceeded = false, string throttleGroupName = "DomainController")
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = PutDCInMMResponder.AssemblyPath;
			responderDefinition.TypeName = PutDCInMMResponder.TypeName;
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
			responderDefinition.Attributes["IgnoreGroupThrottling"] = isIgnoreGroupThrottlingWhenMajorityNotSucceeded.ToString();
			string resourceName = string.Empty;
			if (string.IsNullOrEmpty(targetServer))
			{
				resourceName = DirectoryGeneralUtils.GetLocalFQDN();
			}
			else
			{
				resourceName = targetServer;
			}
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, throttleGroupName, RecoveryActionId.PutDCInMM, resourceName, null);
			return responderDefinition;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0003DE38 File Offset: 0x0003C038
		protected void InitializeAttributes(AttributeHelper attributeHelper)
		{
			this.TargetServerFQDN = attributeHelper.GetString("TargetServerFQDN", true, DirectoryGeneralUtils.GetLocalFQDN());
			this.IgnoreGroupThrottlingWhenMajorityNotSucceeded = attributeHelper.GetBool("IgnoreGroupThrottling", false, false);
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0003DE64 File Offset: 0x0003C064
		protected virtual void InitializeAttributes()
		{
			AttributeHelper attributeHelper = new AttributeHelper(base.Definition);
			this.InitializeAttributes(attributeHelper);
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0003DE94 File Offset: 0x0003C094
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DirectoryTracer, base.TraceContext, "Inside PuttingDCInMMResponder::DoResponderWork.", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Directory\\PuttingDCInMMResponder.cs", 154);
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
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.PutDCInMM, resourceName, this, true, cancellationToken, null);
			recoveryActionRunner.IsIgnoreResourceName = true;
			recoveryActionRunner.IgnoreGroupThrottlingWhenMajorityNotSucceeded = this.IgnoreGroupThrottlingWhenMajorityNotSucceeded;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DirectoryTracer, base.TraceContext, string.Format("Putting DC {0} in MM.", this.TargetServerFQDN), null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Directory\\PuttingDCInMMResponder.cs", 184);
			recoveryActionRunner.Execute(delegate()
			{
				this.DirectoryPutDCInMM(this.TargetServerFQDN);
			});
			WTFDiagnostics.TraceInformation(ExTraceGlobals.DirectoryTracer, base.TraceContext, "Putting DC into MM is Completed", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Directory\\PuttingDCInMMResponder.cs", 191);
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0003DF78 File Offset: 0x0003C178
		private void DirectoryPutDCInMM(string targetFqdn)
		{
			string stateAttribute = DirectoryGeneralUtils.InternalPutDCInMM(targetFqdn, base.TraceContext);
			base.Result.StateAttribute1 = stateAttribute;
		}

		// Token: 0x0400070E RID: 1806
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400070F RID: 1807
		private static readonly string TypeName = typeof(PutDCInMMResponder).FullName;

		// Token: 0x02000156 RID: 342
		internal static class AttributeNames
		{
			// Token: 0x04000712 RID: 1810
			internal const string TargetServerFQDN = "TargetServerFQDN";

			// Token: 0x04000713 RID: 1811
			internal const string throttleGroupName = "throttleGroupName";

			// Token: 0x04000714 RID: 1812
			internal const string IgnoreGroupThrottlingWhenMajorityNotSucceeded = "IgnoreGroupThrottling";
		}
	}
}
