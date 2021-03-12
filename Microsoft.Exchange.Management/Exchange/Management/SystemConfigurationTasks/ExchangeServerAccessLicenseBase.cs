using System;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020009A5 RID: 2469
	[Serializable]
	public abstract class ExchangeServerAccessLicenseBase
	{
		// Token: 0x06005849 RID: 22601 RVA: 0x00170549 File Offset: 0x0016E749
		protected ExchangeServerAccessLicenseBase() : this(string.Empty)
		{
		}

		// Token: 0x0600584A RID: 22602 RVA: 0x00170556 File Offset: 0x0016E756
		protected ExchangeServerAccessLicenseBase(string licenseName)
		{
			this.LicenseName = licenseName;
		}

		// Token: 0x17001A55 RID: 6741
		// (get) Token: 0x0600584B RID: 22603 RVA: 0x00170565 File Offset: 0x0016E765
		// (set) Token: 0x0600584C RID: 22604 RVA: 0x0017056D File Offset: 0x0016E76D
		public string LicenseName { get; protected set; }
	}
}
