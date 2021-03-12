using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002B8 RID: 696
	internal class UndefinedTrackingAuthority : TrackingAuthority
	{
		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x0005A97C File Offset: 0x00058B7C
		public override SearchScope AssociatedScope
		{
			get
			{
				return SearchScope.Organization;
			}
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0005A97F File Offset: 0x00058B7F
		public override bool IsAllowedScope(SearchScope scope)
		{
			return false;
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x0005A982 File Offset: 0x00058B82
		public override string ToString()
		{
			return "Type=UndefinedTrackingAuthority";
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x0005A989 File Offset: 0x00058B89
		private UndefinedTrackingAuthority() : base(TrackingAuthorityKind.Undefined)
		{
		}

		// Token: 0x04000CFC RID: 3324
		public static UndefinedTrackingAuthority Instance = new UndefinedTrackingAuthority();
	}
}
