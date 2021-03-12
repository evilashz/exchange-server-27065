using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.Storage.ResourceHealth;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000242 RID: 578
	internal abstract class BaseServerIdInfo
	{
		// Token: 0x06000F43 RID: 3907 RVA: 0x0004B49F File Offset: 0x0004969F
		protected BaseServerIdInfo(string serverFQDN, Guid mdbGuid, int serverVersion, string cafeFQDN, bool isFromDifferentResourceForest)
		{
			this.CafeFQDN = cafeFQDN;
			this.ServerFQDN = serverFQDN;
			this.MdbGuid = mdbGuid;
			this.serverVersion = serverVersion;
			this.IsFromDifferentResourceForest = isFromDifferentResourceForest;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x0004B4D7 File Offset: 0x000496D7
		public bool IsLocalServer
		{
			get
			{
				return !this.IsFromDifferentResourceForest && string.Equals(BaseServerIdInfo.LocalServerFQDN, this.ServerFQDN, StringComparison.OrdinalIgnoreCase);
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x0004B4F4 File Offset: 0x000496F4
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x0004B4FC File Offset: 0x000496FC
		public bool IsFromDifferentResourceForest { get; private set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x0004B505 File Offset: 0x00049705
		public int ServerVersion
		{
			get
			{
				return this.serverVersion;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x0004B50D File Offset: 0x0004970D
		// (set) Token: 0x06000F49 RID: 3913 RVA: 0x0004B515 File Offset: 0x00049715
		public string ServerFQDN { get; private set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000F4A RID: 3914 RVA: 0x0004B51E File Offset: 0x0004971E
		// (set) Token: 0x06000F4B RID: 3915 RVA: 0x0004B526 File Offset: 0x00049726
		public string CafeFQDN { get; private set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x0004B52F File Offset: 0x0004972F
		// (set) Token: 0x06000F4D RID: 3917 RVA: 0x0004B537 File Offset: 0x00049737
		public Guid MdbGuid { get; private set; }

		// Token: 0x06000F4E RID: 3918 RVA: 0x0004B540 File Offset: 0x00049740
		public virtual ResourceKey[] ToResourceKey(bool writeOperation)
		{
			if (!writeOperation)
			{
				return new ResourceKey[]
				{
					new MdbResourceHealthMonitorKey(this.MdbGuid)
				};
			}
			if (CallContext.Current.IsLongRunningScenario)
			{
				return new ResourceKey[]
				{
					new MdbResourceHealthMonitorKey(this.MdbGuid),
					new MdbReplicationResourceHealthMonitorKey(this.MdbGuid),
					new CiAgeOfLastNotificationResourceKey(this.MdbGuid)
				};
			}
			return new ResourceKey[]
			{
				new MdbResourceHealthMonitorKey(this.MdbGuid),
				new MdbReplicationResourceHealthMonitorKey(this.MdbGuid)
			};
		}

		// Token: 0x04000BA7 RID: 2983
		private int serverVersion = BaseServerIdInfo.InvalidServerVersion;

		// Token: 0x04000BA8 RID: 2984
		internal static readonly int InvalidServerVersion = 0;

		// Token: 0x04000BA9 RID: 2985
		internal static readonly string LocalServerFQDN = NativeHelpers.GetLocalComputerFqdn(true);
	}
}
