using System;
using Microsoft.Exchange.EdgeSync.Datacenter;

namespace Microsoft.Exchange.EdgeSync.Mserve
{
	// Token: 0x0200003A RID: 58
	internal sealed class MserveTargetServerConfig : DatacenterTargetServerConfig
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0001175E File Offset: 0x0000F95E
		public MserveTargetServerConfig(string name, string provisioningUrl, string settingsUrl, string remoteCertSubject, string primaryLeaseLocation, string backupLeaseLocation) : base(name, provisioningUrl, primaryLeaseLocation, backupLeaseLocation)
		{
			this.settingsUrl = settingsUrl;
			this.remoteCertSubject = remoteCertSubject;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0001177B File Offset: 0x0000F97B
		public string SettingsUrl
		{
			get
			{
				return this.settingsUrl;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00011783 File Offset: 0x0000F983
		public string RemoteCertSubject
		{
			get
			{
				return this.remoteCertSubject;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0001178B File Offset: 0x0000F98B
		public override string ShortHostName
		{
			get
			{
				return "MSERV";
			}
		}

		// Token: 0x04000116 RID: 278
		private const string ShortHostNameValue = "MSERV";

		// Token: 0x04000117 RID: 279
		private readonly string settingsUrl;

		// Token: 0x04000118 RID: 280
		private readonly string remoteCertSubject;
	}
}
