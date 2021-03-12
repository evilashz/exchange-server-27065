using System;
using System.Text;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000094 RID: 148
	internal sealed class AIBreadcrumbs
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x00018387 File Offset: 0x00016587
		private AIBreadcrumbs()
		{
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00018390 File Offset: 0x00016590
		public string GenerateReport()
		{
			StringBuilder stringBuilder = new StringBuilder(128 * AIBreadcrumbs.allBreadcrumbTrails.Length * 128);
			stringBuilder.AppendLine(string.Format("AI Breadcrumbs. Current Local Time: {0} Current UTC Time: {1}", DateTime.UtcNow.ToLocalTime(), DateTime.UtcNow));
			foreach (BreadcrumbsTrail breadcrumbsTrail in AIBreadcrumbs.allBreadcrumbTrails)
			{
				stringBuilder.AppendLine("Breadcrumbs for " + breadcrumbsTrail.Name);
				stringBuilder.AppendLine(breadcrumbsTrail.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000288 RID: 648
		public static readonly BreadcrumbsTrail ShutdownTrail = new BreadcrumbsTrail("ShutdownTrail", TrailLength.Long);

		// Token: 0x04000289 RID: 649
		public static readonly BreadcrumbsTrail StartupTrail = new BreadcrumbsTrail("StartupTrail", TrailLength.Long);

		// Token: 0x0400028A RID: 650
		public static readonly BreadcrumbsTrail StatusTrail = new BreadcrumbsTrail("StatusTrail", TrailLength.Short);

		// Token: 0x0400028B RID: 651
		public static readonly BreadcrumbsTrail DatabaseStatusTrail = new BreadcrumbsTrail("DatabaseStatusTrail", TrailLength.Short);

		// Token: 0x0400028C RID: 652
		public static readonly AIBreadcrumbs Instance = new AIBreadcrumbs();

		// Token: 0x0400028D RID: 653
		private static BreadcrumbsTrail[] allBreadcrumbTrails = new BreadcrumbsTrail[]
		{
			AIBreadcrumbs.StartupTrail,
			AIBreadcrumbs.DatabaseStatusTrail,
			AIBreadcrumbs.ShutdownTrail,
			AIBreadcrumbs.StatusTrail
		};
	}
}
