using System;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000233 RID: 563
	internal struct ConfigurationStatus
	{
		// Token: 0x060012F2 RID: 4850 RVA: 0x00053164 File Offset: 0x00051364
		public ConfigurationStatus(string role)
		{
			this.Role = role;
			this.Action = InstallationModes.Unknown;
			this.Watermark = null;
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0005317B File Offset: 0x0005137B
		public ConfigurationStatus(string role, InstallationModes action)
		{
			this.Role = role;
			this.Action = action;
			this.Watermark = null;
		}

		// Token: 0x04000805 RID: 2053
		public string Role;

		// Token: 0x04000806 RID: 2054
		public InstallationModes Action;

		// Token: 0x04000807 RID: 2055
		public string Watermark;
	}
}
