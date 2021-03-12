using System;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders
{
	// Token: 0x020004E1 RID: 1249
	public class EscalateByDatabaseHealthResponder : EscalateResponder
	{
		// Token: 0x06001F07 RID: 7943 RVA: 0x000BD758 File Offset: 0x000BB958
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, string targetExtension, ServiceHealthStatus targetHealthState, string escalationTeam, string escalationSubject, string escalationMessage, NotificationServiceClass notificationServiceClass, bool enabled = true, int recurrenceIntervalSeconds = 300)
		{
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(name, serviceName, alertTypeId, alertMask, targetResource, targetHealthState, escalationTeam, escalationSubject, escalationMessage, enabled, notificationServiceClass, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.AssemblyPath = EscalateByDatabaseHealthResponder.AssemblyPath;
			responderDefinition.TypeName = EscalateByDatabaseHealthResponder.TypeName;
			responderDefinition.RecurrenceIntervalSeconds = recurrenceIntervalSeconds;
			responderDefinition.TargetExtension = targetExtension;
			return responderDefinition;
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x000BD7B0 File Offset: 0x000BB9B0
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			if (base.Definition.TargetHealthState == ServiceHealthStatus.None)
			{
				throw new InvalidOperationException(Strings.UnableToRunEscalateByDatabaseHealthResponder);
			}
			Guid guid;
			if (!Guid.TryParse(base.Definition.TargetExtension, out guid))
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.StoreTracer, base.TraceContext, "Unable to get database information based on the Guid supplied; escalating", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Store\\EscalateByDatabaseHealthResponder.cs", 191);
				base.Result.StateAttribute4 = "UnableToGetDatabaseInfoEscalate";
				base.DoResponderWork(cancellationToken);
				return;
			}
			Exception ex;
			CopyStatusClientCachedEntry[] copyStatus = CopyStatusHelper.GetCopyStatus(AmServerName.LocalComputerName, RpcGetDatabaseCopyStatusFlags2.None, new Guid[]
			{
				guid
			}, (int)EscalateByDatabaseHealthResponder.copyStatusTimeoutMSec, null, out ex);
			if (ex != null || copyStatus == null || copyStatus.Length != 1)
			{
				WTFDiagnostics.TraceError(ExTraceGlobals.StoreTracer, base.TraceContext, "Unable to get database state from AM; escalating", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Store\\EscalateByDatabaseHealthResponder.cs", 130);
				base.Result.StateAttribute4 = "UnableToGetDatabaseStateEscalate";
				base.Result.StateAttribute5 = ((ex != null) ? ex.ToString() : "InvalidCopyStatus");
				base.DoResponderWork(cancellationToken);
				return;
			}
			base.Result.StateAttribute5 = copyStatus[0].CopyStatus.CopyStatus.ToString();
			if (copyStatus[0].CopyStatus.CopyStatus == CopyStatusEnum.Healthy || copyStatus[0].CopyStatus.CopyStatus == CopyStatusEnum.Mounted)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.StoreTracer, base.TraceContext, "Database state matches the state for escalation; invoking escalate", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Store\\EscalateByDatabaseHealthResponder.cs", 149);
				base.Result.StateAttribute4 = "EscalatingBasedOnDatabaseState";
				string value;
				NotificationServiceClass notificationServiceClass;
				if (base.Definition.Attributes.TryGetValue(copyStatus[0].CopyStatus.CopyStatus.ToString(), out value) && !string.IsNullOrWhiteSpace(value) && Enum.TryParse<NotificationServiceClass>(value, true, out notificationServiceClass))
				{
					WTFDiagnostics.TraceDebug<NotificationServiceClass>(ExTraceGlobals.StoreTracer, base.TraceContext, "EscalateByDatabaseHealthResponder.DoResponderWork: Setting NotificationServiceClass to '{0}'", notificationServiceClass, null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Store\\EscalateByDatabaseHealthResponder.cs", 170);
					base.EscalationNotificationType = new NotificationServiceClass?(notificationServiceClass);
				}
				base.Result.StateAttribute5 = ((base.EscalationNotificationType != null) ? base.EscalationNotificationType.ToString() : base.Definition.NotificationServiceClass.ToString());
				base.DoResponderWork(cancellationToken);
				return;
			}
			base.Result.StateAttribute4 = "NoEscalationBasedOnDatabaseHealthState";
		}

		// Token: 0x04001672 RID: 5746
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04001673 RID: 5747
		private static readonly string TypeName = typeof(EscalateByDatabaseHealthResponder).FullName;

		// Token: 0x04001674 RID: 5748
		private static double copyStatusTimeoutMSec = TimeSpan.FromSeconds(20.0).TotalMilliseconds;
	}
}
