using System;
using System.IO;
using System.Management.Automation;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Security;
using System.Threading;
using System.Xml;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000374 RID: 884
	[Cmdlet("Request", "SyncDaemonLease")]
	public sealed class RequestSyncDaemonLease : Task
	{
		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x0008605C File Offset: 0x0008425C
		// (set) Token: 0x06001F09 RID: 7945 RVA: 0x00086064 File Offset: 0x00084264
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
		public ServiceInstanceId ServiceInstance { get; set; }

		// Token: 0x06001F0A RID: 7946 RVA: 0x00086070 File Offset: 0x00084270
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || TaskHelper.IsTaskKnownException(exception) || exception is RidMasterConfigException || exception is SyncDaemonArbitrationConfigException || exception is ReadTopologyTimeoutException || exception is XmlException || exception is SerializationException || exception is SecurityException || exception is DirectoryNotFoundException || exception is IOException || exception is UnauthorizedAccessException;
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x000860DC File Offset: 0x000842DC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			string instanceId = this.ServiceInstance.InstanceId;
			ArbitrationConfigFromAD arbitrationConfigFromAD = SyncDaemonArbitrationConfigHelper.GetArbitrationConfigFromAD(instanceId);
			string path = string.Format("\\\\{0}", arbitrationConfigFromAD.RidMasterInfo.RidMasterServer);
			string path2 = Path.Combine(path, SyncDaemonArbitrationConfigHelper.SyncDaemonLeaseShare);
			string leaseFilePath = Path.Combine(path2, SyncDaemonArbitrationConfigHelper.GetLeaseFileName(instanceId));
			Guid guid = LocalSiteCache.LocalSite.Guid;
			LeaseFileEntry leaseFileEntry = new LeaseFileEntry(SyncDaemonArbitrationConfigHelper.ServerNameForFakeLock, Guid.NewGuid().ToString("N"), arbitrationConfigFromAD.RidMasterInfo.RidMasterVersionFromAD, new DateTime(DateTime.UtcNow.Ticks + new TimeSpan(0, 0, arbitrationConfigFromAD.SyncDaemonArbitrationConfig.PassiveInstanceSleepInterval).Ticks, DateTimeKind.Utc), guid);
			try
			{
				this.WriteLeaseFileEntry(leaseFilePath, leaseFileEntry);
			}
			catch (IOException)
			{
				Thread.Sleep(1000);
				this.WriteLeaseFileEntry(leaseFilePath, leaseFileEntry);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x000861D8 File Offset: 0x000843D8
		private void WriteLeaseFileEntry(string leaseFilePath, LeaseFileEntry leaseFileEntry)
		{
			using (FileStream fileStream = new FileStream(leaseFilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
			{
				SoapFormatter soapFormatter = new SoapFormatter();
				soapFormatter.Serialize(fileStream, leaseFileEntry);
			}
		}
	}
}
