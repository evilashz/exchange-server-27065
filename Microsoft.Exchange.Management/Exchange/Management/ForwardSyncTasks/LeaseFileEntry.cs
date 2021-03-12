using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000354 RID: 852
	[Serializable]
	internal sealed class LeaseFileEntry : ISerializable
	{
		// Token: 0x06001D60 RID: 7520 RVA: 0x00081BD5 File Offset: 0x0007FDD5
		public LeaseFileEntry(string serverName, string serviceInstanceId, int ridMasterVersion, DateTime lockExpirationTime, Guid siteGuid)
		{
			this.ServerName = serverName;
			this.ServiceInstanceId = serviceInstanceId;
			this.RidMasterVersion = ridMasterVersion;
			this.LockExpirationTimeUtc = lockExpirationTime;
			this.SiteGuid = siteGuid;
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x00081C04 File Offset: 0x0007FE04
		public LeaseFileEntry(SerializationInfo info, StreamingContext context)
		{
			this.ServerName = info.GetString("ServerName");
			this.ServiceInstanceId = info.GetString("ServiceInstanceId");
			this.RidMasterVersion = info.GetInt32("RidMasterVersion");
			this.LockExpirationTimeUtc = DateTime.FromFileTimeUtc(long.Parse(info.GetString("LockExpirationTimeUtc")));
			this.SiteGuid = new Guid(info.GetString("SiteGuid"));
		}

		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x00081C7B File Offset: 0x0007FE7B
		// (set) Token: 0x06001D63 RID: 7523 RVA: 0x00081C83 File Offset: 0x0007FE83
		public string ServerName { get; private set; }

		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x00081C8C File Offset: 0x0007FE8C
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x00081C94 File Offset: 0x0007FE94
		public string ServiceInstanceId { get; private set; }

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x00081C9D File Offset: 0x0007FE9D
		// (set) Token: 0x06001D67 RID: 7527 RVA: 0x00081CA5 File Offset: 0x0007FEA5
		public Guid SiteGuid { get; private set; }

		// Token: 0x17000860 RID: 2144
		// (get) Token: 0x06001D68 RID: 7528 RVA: 0x00081CAE File Offset: 0x0007FEAE
		// (set) Token: 0x06001D69 RID: 7529 RVA: 0x00081CB6 File Offset: 0x0007FEB6
		public int RidMasterVersion { get; private set; }

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06001D6A RID: 7530 RVA: 0x00081CBF File Offset: 0x0007FEBF
		// (set) Token: 0x06001D6B RID: 7531 RVA: 0x00081CC7 File Offset: 0x0007FEC7
		public DateTime LockExpirationTimeUtc { get; private set; }

		// Token: 0x06001D6C RID: 7532 RVA: 0x00081CD0 File Offset: 0x0007FED0
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("ServerName", this.ServerName);
			info.AddValue("ServiceInstanceId", this.ServiceInstanceId);
			info.AddValue("RidMasterVersion", this.RidMasterVersion);
			info.AddValue("LockExpirationTimeUtc", this.LockExpirationTimeUtc.ToFileTimeUtc());
			info.AddValue("SiteGuid", this.SiteGuid.ToString());
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x00081D48 File Offset: 0x0007FF48
		public override string ToString()
		{
			return string.Format("ServerName: {0}, ServiceInstanceId: {1}, RidMasterVersion: {2}, LockExpirationTimeUtc: {3}, SiteGuid: {4}", new object[]
			{
				this.ServerName,
				this.ServiceInstanceId,
				this.RidMasterVersion,
				this.LockExpirationTimeUtc,
				this.SiteGuid.ToString()
			});
		}

		// Token: 0x04001897 RID: 6295
		private const string ServerNameElement = "ServerName";

		// Token: 0x04001898 RID: 6296
		private const string ServiceInstanceIdElement = "ServiceInstanceId";

		// Token: 0x04001899 RID: 6297
		private const string SiteGuidElement = "SiteGuid";

		// Token: 0x0400189A RID: 6298
		private const string RidMasterVersionElement = "RidMasterVersion";

		// Token: 0x0400189B RID: 6299
		private const string LockExpirationTimeUtcElement = "LockExpirationTimeUtc";
	}
}
