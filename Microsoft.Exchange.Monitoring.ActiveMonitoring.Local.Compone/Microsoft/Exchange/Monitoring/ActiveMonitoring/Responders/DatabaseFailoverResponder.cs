using System;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.HA.ManagedAvailability;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders
{
	// Token: 0x020000F8 RID: 248
	public class DatabaseFailoverResponder : ResponderWorkItem
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0002D91F File Offset: 0x0002BB1F
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x0002D927 File Offset: 0x0002BB27
		internal string ComponentName { get; private set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0002D930 File Offset: 0x0002BB30
		// (set) Token: 0x060007A9 RID: 1961 RVA: 0x0002D938 File Offset: 0x0002BB38
		internal string DatabaseGuidString { get; private set; }

		// Token: 0x060007AA RID: 1962 RVA: 0x0002D944 File Offset: 0x0002BB44
		internal static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, string componentName, Guid databaseGuid, ServiceHealthStatus targetHealthState)
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = DatabaseFailoverResponder.assemblyPath;
			responderDefinition.TypeName = DatabaseFailoverResponder.databaseFailoverResponderTypeName;
			responderDefinition.Name = name;
			responderDefinition.ServiceName = serviceName;
			responderDefinition.AlertTypeId = alertTypeId;
			responderDefinition.AlertMask = alertMask;
			responderDefinition.TargetResource = targetResource;
			responderDefinition.TargetHealthState = targetHealthState;
			responderDefinition.TargetExtension = databaseGuid.ToString();
			responderDefinition.Attributes["ComponentName"] = componentName;
			responderDefinition.RecurrenceIntervalSeconds = 300;
			responderDefinition.WaitIntervalSeconds = 30;
			responderDefinition.TimeoutSeconds = 150;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.Enabled = true;
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, "Dag", RecoveryActionId.DatabaseFailover, databaseGuid.ToString(), null);
			return responderDefinition;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0002DA08 File Offset: 0x0002BC08
		internal virtual void InitializeAttributes()
		{
			AttributeHelper attributeHelper = new AttributeHelper(base.Definition);
			this.ComponentName = attributeHelper.GetString("ComponentName", true, null);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0002DA50 File Offset: 0x0002BC50
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			if (base.Definition.TargetHealthState == ServiceHealthStatus.None)
			{
				throw new InvalidOperationException("DatabaseFailoverResponder can only run as a chained responder with a target health state defined");
			}
			this.InitializeAttributes();
			this.DatabaseGuidString = base.Definition.TargetExtension;
			base.Result.StateAttribute1 = this.DatabaseGuidString;
			if (string.IsNullOrWhiteSpace(this.DatabaseGuidString))
			{
				base.Result.StateAttribute2 = "DatabaseGuidNotSupplied";
				WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "DatabaseFailoverResponder.DoWork: Unable to perform database failover as database Guid is not supplied", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\DatabaseFailoverResponder.cs", 145);
				throw new DatabaseGuidNotSuppliedException();
			}
			Guid databaseGuid = new Guid(this.DatabaseGuidString);
			if (DirectoryAccessor.Instance.IsDatabaseCopyActiveOnLocalServer(databaseGuid))
			{
				RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.DatabaseFailover, this.DatabaseGuidString, this, true, cancellationToken, null);
				recoveryActionRunner.Execute(delegate()
				{
					this.InitiateDatabaseFailover(databaseGuid);
				});
				return;
			}
			base.Result.StateAttribute2 = "SkippingFailoverPassiveDatabase";
			WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "DatabaseFailoverResponder.DoWork: Skipping database failover as database is already passive on local server", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\DatabaseFailoverResponder.cs", 174);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0002DB7C File Offset: 0x0002BD7C
		private void InitiateDatabaseFailover(Guid databaseGuid)
		{
			Component component = Component.FindWellKnownComponent(this.ComponentName);
			if (component != null)
			{
				WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "DatabaseFailoverResponder.DoWork: Attempting to perform database failover (componentName={0})", component.ToString(), null, "InitiateDatabaseFailover", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\DatabaseFailoverResponder.cs", 193);
				string comment = string.Format("Managed availability database failover initiated by Responder={0} Component={1}.", base.Definition.Name, component.ToString());
				MailboxDatabase mailboxDatabaseFromGuid = DirectoryAccessor.Instance.GetMailboxDatabaseFromGuid(databaseGuid);
				ManagedAvailabilityHelper.PerformDatabaseFailover(component.ToString(), comment, mailboxDatabaseFromGuid);
				return;
			}
			throw new InvalidOperationException(string.Format("{0} is not a well known exchange component", this.ComponentName));
		}

		// Token: 0x04000526 RID: 1318
		private static readonly string assemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000527 RID: 1319
		private static readonly string databaseFailoverResponderTypeName = typeof(DatabaseFailoverResponder).FullName;

		// Token: 0x020000F9 RID: 249
		internal static class AttributeNames
		{
			// Token: 0x0400052A RID: 1322
			internal const string ComponentName = "ComponentName";
		}

		// Token: 0x020000FA RID: 250
		internal class DefaultValues
		{
			// Token: 0x0400052B RID: 1323
			internal const int RecurrenceIntervalSeconds = 300;

			// Token: 0x0400052C RID: 1324
			internal const int WaitIntervalSeconds = 30;

			// Token: 0x0400052D RID: 1325
			internal const int TimeoutSeconds = 150;

			// Token: 0x0400052E RID: 1326
			internal const int MaxRetryAttempts = 3;
		}
	}
}
