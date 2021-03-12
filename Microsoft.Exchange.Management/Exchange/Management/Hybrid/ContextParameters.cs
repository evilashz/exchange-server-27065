using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.Hybrid
{
	// Token: 0x020008FA RID: 2298
	internal class ContextParameters : IContextParameters
	{
		// Token: 0x06005184 RID: 20868 RVA: 0x00152579 File Offset: 0x00150779
		public ContextParameters()
		{
			this.dictionary = new Dictionary<string, object>();
		}

		// Token: 0x06005185 RID: 20869 RVA: 0x0015258C File Offset: 0x0015078C
		public T Get<T>(string name)
		{
			object obj;
			if (!this.dictionary.TryGetValue(name, out obj))
			{
				obj = default(T);
			}
			return (T)((object)obj);
		}

		// Token: 0x06005186 RID: 20870 RVA: 0x001525BE File Offset: 0x001507BE
		public void Set<T>(string name, T value)
		{
			this.dictionary[name] = value;
		}

		// Token: 0x04002FBC RID: 12220
		private Dictionary<string, object> dictionary;

		// Token: 0x020008FB RID: 2299
		internal static class DefaultNames
		{
			// Token: 0x04002FBD RID: 12221
			public const string TenantCoexistenceDomain = "_hybridDomain";

			// Token: 0x04002FBE RID: 12222
			public const string HybridDomainList = "_hybridDomainList";

			// Token: 0x04002FBF RID: 12223
			public const string OnPremOrgRel = "_onPremOrgRel";

			// Token: 0x04002FC0 RID: 12224
			public const string TenantOrgRel = "_tenantOrgRel";

			// Token: 0x04002FC1 RID: 12225
			public const string OnPremAcceptedDomains = "_onPremAcceptedDomains";

			// Token: 0x04002FC2 RID: 12226
			public const string TenantAcceptedDomains = "_tenantAcceptedDomains";

			// Token: 0x04002FC3 RID: 12227
			public const string OnPremAcceptedTokenIssuerUris = "_onPremAcceptedTokenIssuerUris";

			// Token: 0x04002FC4 RID: 12228
			public const string TenantAcceptedTokenIssuerUris = "_tenantAcceptedTokenIssuerUris";

			// Token: 0x04002FC5 RID: 12229
			public const string ForceUpgrade = "_forceUpgrade";

			// Token: 0x04002FC6 RID: 12230
			public const string SuppressOAuthWarning = "_suppressOAuthWarning";
		}
	}
}
