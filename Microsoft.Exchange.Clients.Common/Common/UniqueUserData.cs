using System;

namespace Microsoft.Exchange.Clients.Common
{
	// Token: 0x0200002B RID: 43
	internal sealed class UniqueUserData
	{
		// Token: 0x06000119 RID: 281 RVA: 0x0000840D File Offset: 0x0000660D
		internal UniqueUserData()
		{
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00008423 File Offset: 0x00006623
		internal int CurrentLightSessionCount
		{
			get
			{
				return this.currentLightSessionCount;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000842B File Offset: 0x0000662B
		internal int CurrentPremiumSessionCount
		{
			get
			{
				return this.currentPremiumSessionCount;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00008433 File Offset: 0x00006633
		internal bool IsFirstLightSession
		{
			get
			{
				return this.isFirstLightSession;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000843B File Offset: 0x0000663B
		internal bool IsFirstPremiumSession
		{
			get
			{
				return this.isFirstPremiumSession;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00008444 File Offset: 0x00006644
		internal void IncreaseSessionCounter(bool isProxy, bool isLightExperience)
		{
			lock (this)
			{
				if (!isProxy)
				{
					if (isLightExperience)
					{
						this.currentLightSessionCount++;
						this.isFirstLightSession = false;
					}
					else
					{
						this.currentPremiumSessionCount++;
						this.isFirstPremiumSession = false;
					}
				}
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000084AC File Offset: 0x000066AC
		internal void DecreaseSessionCounter(bool isProxy, bool isLightExperience)
		{
			lock (this)
			{
				if (!isProxy)
				{
					if (isLightExperience)
					{
						this.currentLightSessionCount--;
					}
					else
					{
						this.currentPremiumSessionCount--;
					}
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00008508 File Offset: 0x00006708
		internal int CurrentSessionCount
		{
			get
			{
				return this.currentLightSessionCount + this.currentPremiumSessionCount;
			}
		}

		// Token: 0x04000282 RID: 642
		private int currentLightSessionCount;

		// Token: 0x04000283 RID: 643
		private int currentPremiumSessionCount;

		// Token: 0x04000284 RID: 644
		private bool isFirstLightSession = true;

		// Token: 0x04000285 RID: 645
		private bool isFirstPremiumSession = true;
	}
}
