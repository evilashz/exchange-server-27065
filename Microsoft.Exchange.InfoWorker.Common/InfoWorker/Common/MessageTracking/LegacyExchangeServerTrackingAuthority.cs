using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002BA RID: 698
	internal class LegacyExchangeServerTrackingAuthority : TrackingAuthority
	{
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001377 RID: 4983 RVA: 0x0005A9C0 File Offset: 0x00058BC0
		public override SearchScope AssociatedScope
		{
			get
			{
				return SearchScope.Site;
			}
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0005A9C3 File Offset: 0x00058BC3
		public override bool IsAllowedScope(SearchScope scope)
		{
			return false;
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0005A9C6 File Offset: 0x00058BC6
		public override string ToString()
		{
			return "Type=LegacyExchangeServerTrackingAuthority";
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0005A9CD File Offset: 0x00058BCD
		private LegacyExchangeServerTrackingAuthority() : base(TrackingAuthorityKind.LegacyExchangeServer)
		{
		}

		// Token: 0x04000CFE RID: 3326
		public static LegacyExchangeServerTrackingAuthority Instance = new LegacyExchangeServerTrackingAuthority();
	}
}
