using System;

namespace Microsoft.Exchange.EdgeSync.Datacenter
{
	// Token: 0x02000003 RID: 3
	internal class DatacenterTargetServerConfig : TargetServerConfig
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000026C8 File Offset: 0x000008C8
		protected DatacenterTargetServerConfig(string name, string provisioningUrl, string primaryLeaseLocation, string backupLeaseLocation) : base(name, provisioningUrl, 0)
		{
			this.primaryLeaseLocation = primaryLeaseLocation;
			this.backupLeaseLocation = backupLeaseLocation;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000026E2 File Offset: 0x000008E2
		public string ProvisioningUrl
		{
			get
			{
				return base.Host;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000026EA File Offset: 0x000008EA
		public string PrimaryLeaseLocation
		{
			get
			{
				return this.primaryLeaseLocation;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000026F2 File Offset: 0x000008F2
		public string BackupLeaseLocation
		{
			get
			{
				return this.backupLeaseLocation;
			}
		}

		// Token: 0x04000006 RID: 6
		private readonly string primaryLeaseLocation;

		// Token: 0x04000007 RID: 7
		private readonly string backupLeaseLocation;
	}
}
