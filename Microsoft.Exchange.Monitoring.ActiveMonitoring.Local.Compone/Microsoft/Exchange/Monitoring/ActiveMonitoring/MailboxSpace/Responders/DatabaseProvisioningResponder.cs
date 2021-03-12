using System;
using System.Data.SqlTypes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.MailboxSpace.Responders
{
	// Token: 0x020001ED RID: 493
	public class DatabaseProvisioningResponder : ResponderWorkItem
	{
		// Token: 0x06000DA4 RID: 3492 RVA: 0x0005D768 File Offset: 0x0005B968
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			string databaseGuidString = base.Definition.TargetExtension;
			Guid databaseGuid = new Guid(databaseGuidString);
			base.Result.RecoveryResult = ServiceRecoveryResult.Skipped;
			base.Result.StateAttribute1 = base.Definition.TargetResource;
			base.Result.StateAttribute2 = databaseGuidString;
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.MailboxSpaceTracer, base.TraceContext, "Starting database provisioning responder for database {0}", databaseGuidString, null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\DatabaseProvisioningResponder.cs", 87);
			IDataAccessQuery<ResponderResult> lastSuccessfulResponderResult = base.Broker.GetLastSuccessfulResponderResult(base.Definition);
			Task<ResponderResult> task = lastSuccessfulResponderResult.ExecuteAsync(cancellationToken, base.TraceContext);
			task.Continue(delegate(ResponderResult lastResponderResult)
			{
				DateTime startTime = SqlDateTime.MinValue.Value;
				if (lastResponderResult != null)
				{
					startTime = lastResponderResult.ExecutionStartTime.AddMinutes(-1.0);
				}
				IDataAccessQuery<MonitorResult> lastSuccessfulMonitorResult = this.Broker.GetLastSuccessfulMonitorResult(this.Definition.AlertMask, startTime, this.Result.ExecutionStartTime);
				Task<MonitorResult> task2 = lastSuccessfulMonitorResult.ExecuteAsync(cancellationToken, this.TraceContext);
				task2.Continue(delegate(MonitorResult lastMonitorResult)
				{
					WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.MailboxSpaceTracer, this.TraceContext, "Found monitor result {0} corresponding to responder {1}", lastMonitorResult.ResultName, this.Definition.Name, null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\DatabaseProvisioningResponder.cs", 122);
					if (lastMonitorResult != null && lastMonitorResult.IsAlert)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.MailboxSpaceTracer, this.TraceContext, "Found monitor result corresponding to this responder in alert state", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\DatabaseProvisioningResponder.cs", 134);
						if (!string.IsNullOrWhiteSpace(lastMonitorResult.Error) || !string.IsNullOrWhiteSpace(lastMonitorResult.Exception))
						{
							return;
						}
						if (!DirectoryAccessor.Instance.IsDatabaseCopyActiveOnLocalServer(databaseGuid))
						{
							return;
						}
						if (string.IsNullOrWhiteSpace(lastMonitorResult.StateAttribute5))
						{
							this.Result.StateAttribute5 = "NoOp_ProvisioningActionNotProvided";
							return;
						}
						DatabaseProvisioningResponder.ProvisioningState provisioningState = (DatabaseProvisioningResponder.ProvisioningState)Enum.Parse(typeof(DatabaseProvisioningResponder.ProvisioningState), lastMonitorResult.StateAttribute5);
						if (provisioningState != DatabaseProvisioningResponder.ProvisioningState.None)
						{
							ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(false, ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 171, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\DatabaseProvisioningResponder.cs");
							MailboxDatabase mailboxDatabase = topologyConfigurationSession.FindDatabaseByGuid<MailboxDatabase>(databaseGuid);
							if (mailboxDatabase == null)
							{
								throw new DatabaseNotFoundException(databaseGuid.ToString());
							}
							WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.MailboxSpaceTracer, this.TraceContext, "Updating provisioning state for database {0}", databaseGuidString, null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\MailboxSpace\\DatabaseProvisioningResponder.cs", 183);
							this.UpdateDatabaseProvisioningState(mailboxDatabase, provisioningState);
							topologyConfigurationSession.Save(mailboxDatabase);
							this.Result.RecoveryResult = ServiceRecoveryResult.Succeeded;
						}
					}
				}, cancellationToken, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.NotOnFaulted | TaskContinuationOptions.NotOnCanceled);
			}, cancellationToken, TaskContinuationOptions.AttachedToParent);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0005D84C File Offset: 0x0005BA4C
		private void UpdateDatabaseProvisioningState(MailboxDatabase mailboxDatabase, DatabaseProvisioningResponder.ProvisioningState provisioningState)
		{
			if (mailboxDatabase == null)
			{
				throw new ArgumentNullException("mailboxDatabase", "Mailbox database on which to set provisioning state cannot be null");
			}
			switch (provisioningState)
			{
			case DatabaseProvisioningResponder.ProvisioningState.None:
				return;
			case DatabaseProvisioningResponder.ProvisioningState.EnableProvisioningBySpace:
				mailboxDatabase.IsExcludedFromProvisioning = false;
				mailboxDatabase.IsExcludedFromProvisioningBySpaceMonitoring = false;
				return;
			case DatabaseProvisioningResponder.ProvisioningState.DisableProvisioningBySpace:
				mailboxDatabase.IsExcludedFromProvisioning = true;
				mailboxDatabase.IsExcludedFromProvisioningBySpaceMonitoring = true;
				return;
			case DatabaseProvisioningResponder.ProvisioningState.EnableProvisioningBySchema:
				mailboxDatabase.IsExcludedFromProvisioning = false;
				mailboxDatabase.IsExcludedFromProvisioningBySchemaVersionMonitoring = false;
				return;
			case DatabaseProvisioningResponder.ProvisioningState.DisableProvisioningBySchema:
				mailboxDatabase.IsExcludedFromProvisioning = true;
				mailboxDatabase.IsExcludedFromProvisioningBySchemaVersionMonitoring = true;
				return;
			case DatabaseProvisioningResponder.ProvisioningState.DisableInitialProvisioning:
				mailboxDatabase.IsExcludedFromInitialProvisioning = true;
				return;
			case DatabaseProvisioningResponder.ProvisioningState.DisableIsExcludedFromProvisioningBySpaceMonitoring:
				mailboxDatabase.IsExcludedFromProvisioningBySpaceMonitoring = false;
				return;
			case DatabaseProvisioningResponder.ProvisioningState.DisableIsExcludedFromProvisioningBySchemaMonitoring:
				mailboxDatabase.IsExcludedFromProvisioningBySchemaVersionMonitoring = false;
				return;
			default:
				throw new InvalidOperationException("Trying to set invalid provisioning state");
			}
		}

		// Token: 0x020001EE RID: 494
		internal enum ProvisioningState
		{
			// Token: 0x04000A52 RID: 2642
			None,
			// Token: 0x04000A53 RID: 2643
			EnableProvisioningBySpace,
			// Token: 0x04000A54 RID: 2644
			DisableProvisioningBySpace,
			// Token: 0x04000A55 RID: 2645
			EnableProvisioningBySchema,
			// Token: 0x04000A56 RID: 2646
			DisableProvisioningBySchema,
			// Token: 0x04000A57 RID: 2647
			DisableInitialProvisioning,
			// Token: 0x04000A58 RID: 2648
			DisableIsExcludedFromProvisioningBySpaceMonitoring,
			// Token: 0x04000A59 RID: 2649
			DisableIsExcludedFromProvisioningBySchemaMonitoring
		}
	}
}
