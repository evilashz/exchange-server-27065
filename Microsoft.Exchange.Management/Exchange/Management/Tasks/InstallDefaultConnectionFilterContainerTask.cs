using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002D2 RID: 722
	[Cmdlet("install", "DefaultConnectionFilterContainer")]
	public sealed class InstallDefaultConnectionFilterContainerTask : InstallContainerTaskBase<DefaultConnectionFilterGlobalSettings>
	{
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x0007103C File Offset: 0x0006F23C
		public new string[] Name
		{
			get
			{
				return InstallDefaultConnectionFilterContainerTask.name;
			}
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x00071043 File Offset: 0x0006F243
		public InstallDefaultConnectionFilterContainerTask()
		{
			base.Name = (string[])InstallDefaultConnectionFilterContainerTask.name.Clone();
		}

		// Token: 0x04000B03 RID: 2819
		private static readonly string[] name = new string[]
		{
			InstallMessageDeliveryContainerTask.GlobalSettingsContainerName,
			MessageDeliveryGlobalSettings.DefaultName,
			DefaultMessageFilterGlobalSettings.DefaultName,
			DefaultConnectionFilterGlobalSettings.DefaultName
		};
	}
}
