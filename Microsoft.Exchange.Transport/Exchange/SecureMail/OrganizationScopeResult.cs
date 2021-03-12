using System;

namespace Microsoft.Exchange.SecureMail
{
	// Token: 0x0200051B RID: 1307
	internal class OrganizationScopeResult
	{
		// Token: 0x06003D20 RID: 15648 RVA: 0x000FF651 File Offset: 0x000FD851
		public OrganizationScopeResult(bool fromOrganizationScope, bool highConfidence, string reason)
		{
			this.fromOrganizationScope = fromOrganizationScope;
			this.highConfidence = highConfidence;
			this.reason = reason;
		}

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06003D21 RID: 15649 RVA: 0x000FF66E File Offset: 0x000FD86E
		public bool FromOrganizationScope
		{
			get
			{
				return this.fromOrganizationScope;
			}
		}

		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x06003D22 RID: 15650 RVA: 0x000FF676 File Offset: 0x000FD876
		public bool HighConfidence
		{
			get
			{
				return this.highConfidence;
			}
		}

		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x06003D23 RID: 15651 RVA: 0x000FF67E File Offset: 0x000FD87E
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x04001F17 RID: 7959
		private readonly bool fromOrganizationScope;

		// Token: 0x04001F18 RID: 7960
		private readonly bool highConfidence;

		// Token: 0x04001F19 RID: 7961
		private readonly string reason;
	}
}
