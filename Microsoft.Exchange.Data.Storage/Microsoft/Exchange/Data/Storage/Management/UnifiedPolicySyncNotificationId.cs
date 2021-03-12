using System;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A58 RID: 2648
	[Serializable]
	public class UnifiedPolicySyncNotificationId : ObjectId
	{
		// Token: 0x17001AA3 RID: 6819
		// (get) Token: 0x060060AF RID: 24751 RVA: 0x001976C2 File Offset: 0x001958C2
		// (set) Token: 0x060060B0 RID: 24752 RVA: 0x001976CA File Offset: 0x001958CA
		public string IdValue { get; set; }

		// Token: 0x060060B1 RID: 24753 RVA: 0x001976D3 File Offset: 0x001958D3
		public UnifiedPolicySyncNotificationId(string value)
		{
			this.IdValue = value;
		}

		// Token: 0x060060B2 RID: 24754 RVA: 0x001976E2 File Offset: 0x001958E2
		public override byte[] GetBytes()
		{
			return null;
		}

		// Token: 0x060060B3 RID: 24755 RVA: 0x001976E5 File Offset: 0x001958E5
		public override string ToString()
		{
			return this.IdValue;
		}
	}
}
