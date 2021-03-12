using System;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Search.Fast;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Responders
{
	// Token: 0x0200046C RID: 1132
	public class ResumeCatalogResponder : ResponderWorkItem
	{
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001CA8 RID: 7336 RVA: 0x000A85F9 File Offset: 0x000A67F9
		// (set) Token: 0x06001CA9 RID: 7337 RVA: 0x000A8601 File Offset: 0x000A6801
		internal string DatabaseName { get; set; }

		// Token: 0x06001CAA RID: 7338 RVA: 0x000A860C File Offset: 0x000A680C
		internal static ResponderDefinition CreateDefinition(string responderName, string targetResource, string alertMask, ServiceHealthStatus responderTargetState, string throttleGroupName = null)
		{
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = ResumeCatalogResponder.AssemblyPath;
			responderDefinition.TypeName = ResumeCatalogResponder.TypeName;
			responderDefinition.Name = responderName;
			responderDefinition.TargetResource = targetResource;
			responderDefinition.ServiceName = ExchangeComponent.Search.Name;
			responderDefinition.AlertTypeId = "*";
			responderDefinition.AlertMask = alertMask;
			responderDefinition.RecurrenceIntervalSeconds = 600;
			responderDefinition.TimeoutSeconds = 300;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.TargetHealthState = responderTargetState;
			responderDefinition.WaitIntervalSeconds = 30;
			responderDefinition.Enabled = true;
			RecoveryActionRunner.SetThrottleProperties(responderDefinition, throttleGroupName, RecoveryActionId.ResumeCatalog, targetResource, null);
			return responderDefinition;
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x000A86A5 File Offset: 0x000A68A5
		protected virtual void InitializeAttributes()
		{
			new AttributeHelper(base.Definition);
			this.DatabaseName = base.Definition.TargetResource;
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x000A86EC File Offset: 0x000A68EC
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			SearchMonitoringHelper.LogRecoveryAction(this, "Invoked.", new object[0]);
			this.InitializeAttributes();
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.ResumeCatalog, this.DatabaseName, this, true, cancellationToken, null);
			try
			{
				recoveryActionRunner.Execute(delegate(RecoveryActionEntry startEntry)
				{
					this.InternalResumeCatalog(this.DatabaseName, cancellationToken);
				});
			}
			catch (ThrottlingRejectedOperationException)
			{
				SearchMonitoringHelper.LogRecoveryAction(this, "Resuming catalog is throttled.", new object[0]);
				throw;
			}
			SearchMonitoringHelper.LogRecoveryAction(this, "Completed.", new object[0]);
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x000A878C File Offset: 0x000A698C
		private void InternalResumeCatalog(string databaseName, CancellationToken cancellationToken)
		{
			MailboxDatabaseInfo databaseInfo = SearchMonitoringHelper.GetDatabaseInfo(databaseName);
			if (databaseInfo == null)
			{
				throw new ArgumentException("databaseName");
			}
			Guid mailboxDatabaseGuid = databaseInfo.MailboxDatabaseGuid;
			string indexSystemName = FastIndexVersion.GetIndexSystemName(mailboxDatabaseGuid);
			if (IndexManager.Instance.ResumeCatalog(indexSystemName))
			{
				IndexManager.Instance.UpdateConfiguration();
			}
		}

		// Token: 0x040013BF RID: 5055
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x040013C0 RID: 5056
		private static readonly string TypeName = typeof(ResumeCatalogResponder).FullName;

		// Token: 0x0200046D RID: 1133
		internal static class AttributeNames
		{
			// Token: 0x040013C2 RID: 5058
			internal const string throttleGroupName = "throttleGroupName";
		}
	}
}
