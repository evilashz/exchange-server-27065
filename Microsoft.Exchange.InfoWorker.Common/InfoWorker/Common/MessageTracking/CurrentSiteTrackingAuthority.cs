using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002BB RID: 699
	internal class CurrentSiteTrackingAuthority : TrackingAuthority
	{
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x0005A9E2 File Offset: 0x00058BE2
		public override SearchScope AssociatedScope
		{
			get
			{
				return SearchScope.Site;
			}
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0005A9E5 File Offset: 0x00058BE5
		public override bool IsAllowedScope(SearchScope scope)
		{
			return true;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x0005A9E8 File Offset: 0x00058BE8
		public override string ToString()
		{
			return "Type=CurrentSiteTrackingAuthority";
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0005A9EF File Offset: 0x00058BEF
		private CurrentSiteTrackingAuthority() : base(TrackingAuthorityKind.CurrentSite)
		{
		}

		// Token: 0x04000CFF RID: 3327
		public static CurrentSiteTrackingAuthority Instance = new CurrentSiteTrackingAuthority();
	}
}
