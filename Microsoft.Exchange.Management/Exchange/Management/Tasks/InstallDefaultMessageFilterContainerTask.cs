using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002D3 RID: 723
	[Cmdlet("install", "DefaultMessageFilterContainer")]
	public sealed class InstallDefaultMessageFilterContainerTask : InstallContainerTaskBase<DefaultMessageFilterGlobalSettings>
	{
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x0007109A File Offset: 0x0006F29A
		public new string[] Name
		{
			get
			{
				return InstallDefaultMessageFilterContainerTask.name;
			}
		}

		// Token: 0x06001946 RID: 6470 RVA: 0x000710A1 File Offset: 0x0006F2A1
		public InstallDefaultMessageFilterContainerTask()
		{
			base.Name = (string[])InstallDefaultMessageFilterContainerTask.name.Clone();
		}

		// Token: 0x04000B04 RID: 2820
		private static readonly string[] name = new string[]
		{
			InstallMessageDeliveryContainerTask.GlobalSettingsContainerName,
			MessageDeliveryGlobalSettings.DefaultName,
			DefaultMessageFilterGlobalSettings.DefaultName
		};
	}
}
