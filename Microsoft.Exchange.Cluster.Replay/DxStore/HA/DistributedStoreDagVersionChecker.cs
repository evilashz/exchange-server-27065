using System;
using System.Linq;
using Microsoft.Exchange.DxStore.HA.Events;
using Microsoft.Win32;

namespace Microsoft.Exchange.DxStore.HA
{
	// Token: 0x0200016D RID: 365
	internal class DistributedStoreDagVersionChecker
	{
		// Token: 0x06000EBF RID: 3775 RVA: 0x0003F10C File Offset: 0x0003D30C
		public DistributedStoreDagVersionChecker(DistributedStoreTopologyProvider topoProvider, string featureName, int minimumRequiredVersion)
		{
			this.topoProvider = topoProvider;
			this.featureName = featureName;
			this.minimumRequiredVersion = minimumRequiredVersion;
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0003F12C File Offset: 0x0003D32C
		public Tuple<string, int> GetLowestServerVersionInDag(out Exception exception)
		{
			exception = null;
			if (this.topoProvider != null)
			{
				Tuple<string, int>[] dagServerVersionsSortedBestEffort = this.topoProvider.GetDagServerVersionsSortedBestEffort(out exception);
				if (dagServerVersionsSortedBestEffort != null)
				{
					return dagServerVersionsSortedBestEffort.FirstOrDefault<Tuple<string, int>>();
				}
			}
			return null;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0003F15C File Offset: 0x0003D35C
		public bool CheckVersionCompatibility()
		{
			if (this.isVersionRequirementSatisfied)
			{
				return true;
			}
			Tuple<string, int> tuple = null;
			bool flag = false;
			Exception ex = null;
			int? minimumVersionPersisted = this.GetMinimumVersionPersisted();
			this.isVersionRequirementSatisfied = (minimumVersionPersisted != null && minimumVersionPersisted.Value >= this.minimumRequiredVersion);
			if (!this.isVersionRequirementSatisfied)
			{
				flag = true;
				tuple = this.GetLowestServerVersionInDag(out ex);
				if (tuple != null)
				{
					this.isVersionRequirementSatisfied = (tuple.Item2 >= this.minimumRequiredVersion);
					if (this.isVersionRequirementSatisfied)
					{
						this.SetMinimumVersionPersisted(tuple.Item2);
					}
				}
			}
			string text = (minimumVersionPersisted != null) ? minimumVersionPersisted.Value.ToString() : "<unknown>";
			string text2 = "<none>";
			int num = -1;
			if (flag && tuple != null)
			{
				text2 = tuple.Item1;
				num = tuple.Item2;
			}
			if (this.isVersionRequirementSatisfied)
			{
				DxStoreHACrimsonEvents.StartupVersionCheckSatisfied.Log<string, string, int, int, string>(text, text2, num, this.minimumRequiredVersion, "<none>");
			}
			else
			{
				DxStoreHACrimsonEvents.StartupVersionCheckNotSatisfied.LogPeriodic<string, string, int, int, string>("StartupVersionCheck", TimeSpan.FromMinutes(15.0), text, text2, num, this.minimumRequiredVersion, (ex != null) ? ex.Message : "<none>");
			}
			return this.isVersionRequirementSatisfied;
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0003F28C File Offset: 0x0003D48C
		private int? GetMinimumVersionPersisted()
		{
			object value = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\DistributedStore\\VersionCache", this.featureName, null);
			if (value != null && value is int)
			{
				return new int?((int)value);
			}
			return null;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0003F2CB File Offset: 0x0003D4CB
		private void SetMinimumVersionPersisted(int version)
		{
			Registry.SetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\DistributedStore\\VersionCache", this.featureName, version);
		}

		// Token: 0x0400060E RID: 1550
		internal const string VersionCacheKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\DistributedStore\\VersionCache";

		// Token: 0x0400060F RID: 1551
		private readonly DistributedStoreTopologyProvider topoProvider;

		// Token: 0x04000610 RID: 1552
		private readonly string featureName;

		// Token: 0x04000611 RID: 1553
		private readonly int minimumRequiredVersion;

		// Token: 0x04000612 RID: 1554
		private bool isVersionRequirementSatisfied;
	}
}
