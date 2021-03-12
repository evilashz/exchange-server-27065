using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002D6 RID: 726
	[Cmdlet("install", "MessageDeliveryContainer")]
	public sealed class InstallMessageDeliveryContainerTask : InstallContainerTaskBase<MessageDeliveryGlobalSettings>
	{
		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x0600194E RID: 6478 RVA: 0x0007125E File Offset: 0x0006F45E
		public new string[] Name
		{
			get
			{
				return InstallMessageDeliveryContainerTask.name;
			}
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00071265 File Offset: 0x0006F465
		public InstallMessageDeliveryContainerTask()
		{
			base.Name = (string[])InstallMessageDeliveryContainerTask.name.Clone();
		}

		// Token: 0x04000B06 RID: 2822
		public static readonly string GlobalSettingsContainerName = "Global Settings";

		// Token: 0x04000B07 RID: 2823
		private static readonly string[] name = new string[]
		{
			InstallMessageDeliveryContainerTask.GlobalSettingsContainerName,
			MessageDeliveryGlobalSettings.DefaultName
		};
	}
}
