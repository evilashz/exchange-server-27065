using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.SnapIn
{
	// Token: 0x02000290 RID: 656
	internal class ConnectedForestsInfoSingleton
	{
		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001BCC RID: 7116 RVA: 0x0007954D File Offset: 0x0007774D
		// (set) Token: 0x06001BCD RID: 7117 RVA: 0x00079555 File Offset: 0x00077755
		public IExchangeExtensionSnapIn CurrentSnapIn { get; set; }

		// Token: 0x06001BCE RID: 7118 RVA: 0x00079560 File Offset: 0x00077760
		public void UpdateInfo(IList<OrganizationSetting> forestInfos)
		{
			this.forestDisplayNameToInfoMap = new Dictionary<string, OrganizationSetting>();
			this.OtherForests = new List<string>();
			this.OtherOnPremiseForests = new List<string>();
			this.currentForest = null;
			foreach (OrganizationSetting organizationSetting in forestInfos)
			{
				this.forestDisplayNameToInfoMap.Add(organizationSetting.DisplayName, organizationSetting);
				if (organizationSetting.Key == this.CurrentSnapInKey)
				{
					this.currentForest = organizationSetting.DisplayName;
				}
				else
				{
					this.OtherForests.Add(organizationSetting.DisplayName);
					if (organizationSetting.Type == OrganizationType.RemoteOnPremise || organizationSetting.Type == OrganizationType.LocalOnPremise)
					{
						this.OtherOnPremiseForests.Add(organizationSetting.DisplayName);
					}
				}
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001BCF RID: 7119 RVA: 0x00079630 File Offset: 0x00077830
		// (set) Token: 0x06001BD0 RID: 7120 RVA: 0x00079638 File Offset: 0x00077838
		public string CurrentSnapInKey { get; set; }

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001BD1 RID: 7121 RVA: 0x00079641 File Offset: 0x00077841
		public string CurrentForest
		{
			get
			{
				return this.currentForest;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001BD2 RID: 7122 RVA: 0x00079649 File Offset: 0x00077849
		// (set) Token: 0x06001BD3 RID: 7123 RVA: 0x00079651 File Offset: 0x00077851
		public IList<string> OtherForests { get; private set; }

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001BD4 RID: 7124 RVA: 0x0007965A File Offset: 0x0007785A
		// (set) Token: 0x06001BD5 RID: 7125 RVA: 0x00079662 File Offset: 0x00077862
		public IList<string> OtherOnPremiseForests { get; private set; }

		// Token: 0x06001BD6 RID: 7126 RVA: 0x0007966B File Offset: 0x0007786B
		public OrganizationSetting ForestInfoOf(string forest)
		{
			return this.forestDisplayNameToInfoMap[forest];
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001BD7 RID: 7127 RVA: 0x00079679 File Offset: 0x00077879
		public OrganizationSetting CurrentForestSetting
		{
			get
			{
				return this.ForestInfoOf(this.currentForest);
			}
		}

		// Token: 0x06001BD8 RID: 7128 RVA: 0x00079687 File Offset: 0x00077887
		public static ConnectedForestsInfoSingleton GetInstance()
		{
			return ConnectedForestsInfoSingleton.instance;
		}

		// Token: 0x04000A3C RID: 2620
		private string currentForest;

		// Token: 0x04000A3D RID: 2621
		private Dictionary<string, OrganizationSetting> forestDisplayNameToInfoMap;

		// Token: 0x04000A3E RID: 2622
		private static ConnectedForestsInfoSingleton instance = new ConnectedForestsInfoSingleton();
	}
}
