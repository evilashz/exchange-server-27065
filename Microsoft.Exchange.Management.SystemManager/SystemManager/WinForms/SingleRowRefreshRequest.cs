using System;
using Microsoft.ManagementGUI;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x02000159 RID: 345
	public class SingleRowRefreshRequest : PartialRefreshRequest
	{
		// Token: 0x06000E09 RID: 3593 RVA: 0x00035455 File Offset: 0x00033655
		public SingleRowRefreshRequest(object refreshCategory, object identity) : base(refreshCategory)
		{
			this.identity = identity;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x00035468 File Offset: 0x00033668
		public override void DoRefresh(IRefreshable refreshableDataSource, IProgress progress)
		{
			ISupportFastRefresh supportFastRefresh = refreshableDataSource as ISupportFastRefresh;
			if (supportFastRefresh == null)
			{
				throw new InvalidOperationException();
			}
			supportFastRefresh.Refresh(progress, this.identity);
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x00035494 File Offset: 0x00033694
		public override bool Equals(object right)
		{
			if (right == null)
			{
				return false;
			}
			if (object.ReferenceEquals(this, right))
			{
				return true;
			}
			SingleRowRefreshRequest singleRowRefreshRequest = right as SingleRowRefreshRequest;
			return singleRowRefreshRequest != null && base.RefreshCategory == singleRowRefreshRequest.RefreshCategory && this.identity == singleRowRefreshRequest.identity;
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x000354D9 File Offset: 0x000336D9
		public override int GetHashCode()
		{
			return this.identity.GetHashCode() ^ base.RefreshCategory.GetHashCode();
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x000354F2 File Offset: 0x000336F2
		protected object ObjectIdentity
		{
			get
			{
				return this.identity;
			}
		}

		// Token: 0x04000596 RID: 1430
		private readonly object identity;
	}
}
