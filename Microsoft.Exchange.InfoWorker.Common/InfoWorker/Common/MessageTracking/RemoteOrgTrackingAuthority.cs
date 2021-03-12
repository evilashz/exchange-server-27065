using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002B9 RID: 697
	internal class RemoteOrgTrackingAuthority : TrackingAuthority
	{
		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x0005A99E File Offset: 0x00058B9E
		public override SearchScope AssociatedScope
		{
			get
			{
				return SearchScope.Organization;
			}
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0005A9A1 File Offset: 0x00058BA1
		public override bool IsAllowedScope(SearchScope scope)
		{
			return false;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0005A9A4 File Offset: 0x00058BA4
		public override string ToString()
		{
			return "Type=RemoteOrgTrackingAuthority";
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x0005A9AB File Offset: 0x00058BAB
		private RemoteOrgTrackingAuthority() : base(TrackingAuthorityKind.RemoteOrg)
		{
		}

		// Token: 0x04000CFD RID: 3325
		public static RemoteOrgTrackingAuthority Instance = new RemoteOrgTrackingAuthority();
	}
}
