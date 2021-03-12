using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas
{
	// Token: 0x02000073 RID: 115
	[ClassAccessLevel(AccessLevel.Implementation)]
	public sealed class EasDeviceParameters
	{
		// Token: 0x0600021C RID: 540 RVA: 0x00005CC0 File Offset: 0x00003EC0
		internal EasDeviceParameters(string deviceId, string deviceType, string userAgent = "MRS-EASConnection-UserAgent", string deviceIdPrefix = "")
		{
			this.deviceIdPrefix = deviceIdPrefix;
			this.deviceType = deviceType;
			this.userAgent = userAgent;
			this.DeviceId = this.deviceIdPrefix + deviceId;
			if (this.DeviceId.Length > 32)
			{
				throw new ArgumentOutOfRangeException("DeviceId");
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00005D15 File Offset: 0x00003F15
		internal EasDeviceParameters(string deviceId, EasDeviceParameters other) : this(deviceId, other.deviceType, other.userAgent, other.deviceIdPrefix)
		{
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00005D30 File Offset: 0x00003F30
		// (set) Token: 0x0600021F RID: 543 RVA: 0x00005D38 File Offset: 0x00003F38
		internal string DeviceId { get; private set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00005D41 File Offset: 0x00003F41
		internal string DeviceType
		{
			get
			{
				return this.deviceType;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00005D49 File Offset: 0x00003F49
		internal string UserAgent
		{
			get
			{
				return this.userAgent;
			}
		}

		// Token: 0x040001D3 RID: 467
		internal const int MaxDeviceIdLength = 32;

		// Token: 0x040001D4 RID: 468
		private readonly string deviceIdPrefix;

		// Token: 0x040001D5 RID: 469
		private readonly string deviceType;

		// Token: 0x040001D6 RID: 470
		private readonly string userAgent;
	}
}
