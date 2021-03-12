using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ForefrontActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.TransportDatabase
{
	// Token: 0x02000281 RID: 641
	public class TransportOldQueueDBCleanupResponder : ResponderWorkItem
	{
		// Token: 0x0600150E RID: 5390 RVA: 0x00040914 File Offset: 0x0003EB14
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceDebug(ExTraceGlobals.TransportTracer, base.TraceContext, "TransportOldQueueDatabaseCleanupResponder started.", null, "DoResponderWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Transport\\TransportOldQueueDBCleanupResponder.cs", 31);
			string queueDatabasePath;
			if (!TransportOldQueueDBCleanupHelper.TryGetQueueDatabaseFolderPath(out queueDatabasePath))
			{
				base.Result.StateAttribute1 = "Invalid path for queue database. So exiting!";
				return;
			}
			TimeSpan oldMailDatabaseRetentionPeriod;
			if (!base.Definition.Attributes.ContainsKey("OldMailDatabaseRetentionPeriod") || !TimeSpan.TryParse(base.Definition.Attributes["OldMailDatabaseRetentionPeriod"], out oldMailDatabaseRetentionPeriod))
			{
				base.Result.StateAttribute1 = "Invalid Database retention period. So exiting!";
				return;
			}
			RecoveryActionRunner recoveryActionRunner = new RecoveryActionRunner(RecoveryActionId.DeleteFile, Environment.MachineName, this, true, cancellationToken, null);
			recoveryActionRunner.Execute(delegate()
			{
				this.GetAndCleanupOldExpiredDatabases(queueDatabasePath, oldMailDatabaseRetentionPeriod);
			});
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x000409E4 File Offset: 0x0003EBE4
		private void GetAndCleanupOldExpiredDatabases(string queueDatabasePath, TimeSpan oldMailDatabaseRetentionPeriod)
		{
			List<DirectoryInfo> expiredOldQueueDatabases = TransportOldQueueDBCleanupHelper.GetExpiredOldQueueDatabases(queueDatabasePath, oldMailDatabaseRetentionPeriod);
			if (expiredOldQueueDatabases.Count < 1)
			{
				base.Result.StateAttribute1 = "No mail.que database found which was older than retention period. So exiting!";
				return;
			}
			if (this.DeleteExpiredDatabases(queueDatabasePath, expiredOldQueueDatabases))
			{
				base.Result.StateAttribute1 = string.Format("Cleanedup expired mail.que database older then retention period. DatabaseName(s): {0}.", string.Join(",", (from database in expiredOldQueueDatabases
				select database.Name).ToArray<string>()));
				return;
			}
			base.Result.StateAttribute1 = "Failure deleting one or more expired databases, detailed error can be found in Error tag for the responder.";
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x00040A78 File Offset: 0x0003EC78
		private bool DeleteExpiredDatabases(string queueDatabasePath, List<DirectoryInfo> expiredDatabases)
		{
			bool result = true;
			foreach (DirectoryInfo directoryInfo in expiredDatabases)
			{
				try
				{
					if (Directory.Exists(directoryInfo.FullName))
					{
						directoryInfo.Delete(true);
					}
				}
				catch (Exception ex)
				{
					result = false;
					ResponderResult result2 = base.Result;
					result2.Error += string.Format("ExpiredDatabaseDeletion-Error: {0}, Database Name:{1}.", ex.Message, directoryInfo.Name);
				}
			}
			return result;
		}
	}
}
