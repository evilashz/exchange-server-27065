using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000077 RID: 119
	[Cmdlet("Install", "ActiveSyncDeviceClassContainer", SupportsShouldProcess = true)]
	public sealed class InstallActiveSyncDeviceClassContainer : InstallContainerTaskBase<ActiveSyncDeviceClasses>
	{
		// Token: 0x060003AB RID: 939 RVA: 0x0000F8B4 File Offset: 0x0000DAB4
		public InstallActiveSyncDeviceClassContainer()
		{
			base.Name = InstallActiveSyncDeviceClassContainer.name;
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000F8C7 File Offset: 0x0000DAC7
		public new string[] Name
		{
			get
			{
				return InstallActiveSyncDeviceClassContainer.name;
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000F8D0 File Offset: 0x0000DAD0
		protected override ADObjectId GetBaseContainer()
		{
			ADObjectId baseContainer = base.GetBaseContainer();
			return baseContainer.GetChildId("Mobile Mailbox Settings");
		}

		// Token: 0x0400020B RID: 523
		private static readonly string[] name = new string[]
		{
			"ExchangeDeviceClasses"
		};
	}
}
