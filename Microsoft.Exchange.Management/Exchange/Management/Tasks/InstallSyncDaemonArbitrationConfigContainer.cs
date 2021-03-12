using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002DB RID: 731
	[Cmdlet("install", "SyncDaemonArbitrationConfigContainer")]
	public sealed class InstallSyncDaemonArbitrationConfigContainer : InstallContainerTaskBase<SyncDaemonArbitrationConfig>
	{
		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x00071633 File Offset: 0x0006F833
		public new string[] Name
		{
			get
			{
				return InstallSyncDaemonArbitrationConfigContainer.name;
			}
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0007163A File Offset: 0x0006F83A
		public InstallSyncDaemonArbitrationConfigContainer()
		{
			base.Name = (string[])InstallSyncDaemonArbitrationConfigContainer.name.Clone();
		}

		// Token: 0x04000B09 RID: 2825
		public static readonly string GlobalSettingsContainerName = "Global Settings";

		// Token: 0x04000B0A RID: 2826
		public static readonly string SyncDaemonArbitrationConfigContainerName = "SyncDaemonArbitrationConfig";

		// Token: 0x04000B0B RID: 2827
		private static readonly string[] name = new string[]
		{
			InstallSyncDaemonArbitrationConfigContainer.GlobalSettingsContainerName,
			InstallSyncDaemonArbitrationConfigContainer.SyncDaemonArbitrationConfigContainerName
		};
	}
}
