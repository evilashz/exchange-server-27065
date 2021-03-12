using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.WorkloadManagement;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000144 RID: 324
	internal sealed class LegacyResourceHealthMonitorKey : ResourceKey
	{
		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001535C File Offset: 0x0001355C
		public LegacyResourceHealthMonitorKey(Guid databaseGuid) : base(ResourceMetricType.Remote, null)
		{
			if (databaseGuid == Guid.Empty)
			{
				throw new ArgumentException("Guid.Empty is not a valid MdbGuid value", "mdbGuid");
			}
			this.cachedHashCode = ("LegacyMdblCi({0})".GetHashCode() ^ databaseGuid.GetHashCode());
			this.DatabaseGuid = databaseGuid;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x000153B8 File Offset: 0x000135B8
		private bool InitializeIdentity()
		{
			DatabaseInformation databaseInformation = MapiUtils.FindServerForMdb(this.DatabaseGuid, null, null, FindServerFlags.AllowMissing);
			if (databaseInformation.IsMissing)
			{
				this.databaseName = string.Format("LegacyMdblCiUnresolved({0}{1})", this.DatabaseGuid.ToString(), databaseInformation.ForestFqdn);
			}
			else
			{
				this.databaseName = string.Format("LegacyMdblCi({0})", databaseInformation.DatabaseName);
			}
			return !databaseInformation.IsMissing;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0001542B File Offset: 0x0001362B
		protected internal override CacheableResourceHealthMonitor CreateMonitor()
		{
			return new LegacyResourceHealthMonitor(this);
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000AD7 RID: 2775 RVA: 0x00015433 File Offset: 0x00013633
		// (set) Token: 0x06000AD8 RID: 2776 RVA: 0x0001543B File Offset: 0x0001363B
		public Guid DatabaseGuid { get; private set; }

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00015444 File Offset: 0x00013644
		public override string ToString()
		{
			if (this.databaseName == null)
			{
				this.InitializeIdentity();
			}
			return this.databaseName;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x0001545C File Offset: 0x0001365C
		public override bool Equals(object obj)
		{
			LegacyResourceHealthMonitorKey legacyResourceHealthMonitorKey = obj as LegacyResourceHealthMonitorKey;
			return legacyResourceHealthMonitorKey != null && legacyResourceHealthMonitorKey.DatabaseGuid == this.DatabaseGuid;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00015486 File Offset: 0x00013686
		public override int GetHashCode()
		{
			return this.cachedHashCode;
		}

		// Token: 0x04000651 RID: 1617
		private const string FormatString = "LegacyMdblCi({0})";

		// Token: 0x04000652 RID: 1618
		private const string FormatStringUnresolved = "LegacyMdblCiUnresolved({0}{1})";

		// Token: 0x04000653 RID: 1619
		private readonly int cachedHashCode;

		// Token: 0x04000654 RID: 1620
		private string databaseName;
	}
}
