using System;
using System.Collections.Specialized;
using Microsoft.Exchange.Transport.Common;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020002EE RID: 750
	internal class SystemCheckConfig : TransportAppConfig
	{
		// Token: 0x0600214C RID: 8524 RVA: 0x0007E3B8 File Offset: 0x0007C5B8
		public SystemCheckConfig(NameValueCollection appSettings = null) : base(appSettings)
		{
			this.isSystemCheckEnabled = base.GetConfigBool("SystemCheckEnabled", true);
			this.isDiskSystemCheckEnabled = base.GetConfigBool("DiskSystemCheckEnabled", true);
			this.lockedVolumeCheckRetryInterval = base.GetConfigTimeSpan("LockedVolumeCheckRetryInterval", SystemCheckConfig.MinLockedVolumeCheckRetryInterval, SystemCheckConfig.MaxLockedVolumeCheckRetryInterval, SystemCheckConfig.DefaultLockedVolumeCheckRetryInterval);
			this.lockedVolumeCheckRetryCount = base.GetConfigInt("LockedVolumeCheckRetryCount", 0, 20, 10);
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x0600214D RID: 8525 RVA: 0x0007E426 File Offset: 0x0007C626
		public bool IsSystemCheckEnabled
		{
			get
			{
				return this.isSystemCheckEnabled;
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x0600214E RID: 8526 RVA: 0x0007E42E File Offset: 0x0007C62E
		public bool IsDiskSystemCheckEnabled
		{
			get
			{
				return this.isDiskSystemCheckEnabled;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x0600214F RID: 8527 RVA: 0x0007E436 File Offset: 0x0007C636
		public TimeSpan LockedVolumeCheckRetryInterval
		{
			get
			{
				return this.lockedVolumeCheckRetryInterval;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06002150 RID: 8528 RVA: 0x0007E43E File Offset: 0x0007C63E
		public int LockedVolumeCheckRetryCount
		{
			get
			{
				return this.lockedVolumeCheckRetryCount;
			}
		}

		// Token: 0x04001173 RID: 4467
		public const string SystemCheckEnabledLabel = "SystemCheckEnabled";

		// Token: 0x04001174 RID: 4468
		public const string DiskSystemCheckEnabledLabel = "DiskSystemCheckEnabled";

		// Token: 0x04001175 RID: 4469
		public const string LockedVolumeCheckRetryIntervalLabel = "LockedVolumeCheckRetryInterval";

		// Token: 0x04001176 RID: 4470
		public const string LockedVolumeCheckRetryCountLabel = "LockedVolumeCheckRetryCount";

		// Token: 0x04001177 RID: 4471
		public const bool DefaultSystemCheckEnabled = true;

		// Token: 0x04001178 RID: 4472
		public const bool DefaultDiskSystemCheckEnabled = true;

		// Token: 0x04001179 RID: 4473
		public const int MinLockedVolumeCheckRetryCount = 0;

		// Token: 0x0400117A RID: 4474
		public const int DefaultLockedVolumeCheckRetryCount = 10;

		// Token: 0x0400117B RID: 4475
		public const int MaxLockedVolumeCheckRetryCount = 20;

		// Token: 0x0400117C RID: 4476
		public static readonly TimeSpan MinLockedVolumeCheckRetryInterval = TimeSpan.Zero;

		// Token: 0x0400117D RID: 4477
		public static readonly TimeSpan DefaultLockedVolumeCheckRetryInterval = TimeSpan.FromSeconds(3.0);

		// Token: 0x0400117E RID: 4478
		public static readonly TimeSpan MaxLockedVolumeCheckRetryInterval = TimeSpan.FromSeconds(10.0);

		// Token: 0x0400117F RID: 4479
		private readonly bool isSystemCheckEnabled;

		// Token: 0x04001180 RID: 4480
		private readonly bool isDiskSystemCheckEnabled;

		// Token: 0x04001181 RID: 4481
		private readonly TimeSpan lockedVolumeCheckRetryInterval;

		// Token: 0x04001182 RID: 4482
		private readonly int lockedVolumeCheckRetryCount;
	}
}
