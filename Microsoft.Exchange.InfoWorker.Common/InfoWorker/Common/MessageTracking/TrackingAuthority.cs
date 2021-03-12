using System;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002B7 RID: 695
	internal abstract class TrackingAuthority
	{
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001369 RID: 4969
		public abstract SearchScope AssociatedScope { get; }

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x0005A965 File Offset: 0x00058B65
		public TrackingAuthorityKind TrackingAuthorityKind
		{
			get
			{
				return this.trackingAuthorityKind;
			}
		}

		// Token: 0x0600136B RID: 4971
		public abstract bool IsAllowedScope(SearchScope scope);

		// Token: 0x0600136C RID: 4972 RVA: 0x0005A96D File Offset: 0x00058B6D
		protected TrackingAuthority(TrackingAuthorityKind responsibleTracker)
		{
			this.trackingAuthorityKind = responsibleTracker;
		}

		// Token: 0x04000CFB RID: 3323
		private TrackingAuthorityKind trackingAuthorityKind;
	}
}
