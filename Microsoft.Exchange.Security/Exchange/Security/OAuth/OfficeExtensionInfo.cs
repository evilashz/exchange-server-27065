using System;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000F4 RID: 244
	internal sealed class OfficeExtensionInfo
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x0003703C File Offset: 0x0003523C
		public OfficeExtensionInfo(string extensionId, string scope)
		{
			OAuthCommon.VerifyNonNullArgument("extensionId", extensionId);
			this.extensionId = extensionId;
			this.scope = scope;
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x0003705E File Offset: 0x0003525E
		public bool IsScopedToken
		{
			get
			{
				return !string.IsNullOrEmpty(this.scope);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x0003706E File Offset: 0x0003526E
		public string Scope
		{
			get
			{
				return this.scope;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x00037076 File Offset: 0x00035276
		public string ExtensionId
		{
			get
			{
				return this.extensionId;
			}
		}

		// Token: 0x04000789 RID: 1929
		private readonly string scope;

		// Token: 0x0400078A RID: 1930
		private readonly string extensionId;
	}
}
