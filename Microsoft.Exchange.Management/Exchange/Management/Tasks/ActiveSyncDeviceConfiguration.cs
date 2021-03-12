using System;
using Microsoft.Exchange.AirSync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200005E RID: 94
	[Serializable]
	public class ActiveSyncDeviceConfiguration : MobileDeviceConfiguration
	{
		// Token: 0x060002B8 RID: 696 RVA: 0x0000B29E File Offset: 0x0000949E
		public ActiveSyncDeviceConfiguration(DeviceInfo deviceInfo) : base(deviceInfo)
		{
			if (base.ClientType != MobileClientType.EAS)
			{
				throw new ArgumentException("ClientType is not EAS.");
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000B2BB File Offset: 0x000094BB
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0000B2C3 File Offset: 0x000094C3
		public string DeviceActiveSyncVersion
		{
			get
			{
				return base.ClientVersion;
			}
			internal set
			{
				base.ClientVersion = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000B2CC File Offset: 0x000094CC
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0000B2D4 File Offset: 0x000094D4
		private new string ClientVersion { get; set; }
	}
}
