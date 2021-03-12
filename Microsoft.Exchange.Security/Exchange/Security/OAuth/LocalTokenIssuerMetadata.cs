using System;
using System.Globalization;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000C2 RID: 194
	internal sealed class LocalTokenIssuerMetadata : IssuerMetadata
	{
		// Token: 0x060006A6 RID: 1702 RVA: 0x00030ADC File Offset: 0x0002ECDC
		public LocalTokenIssuerMetadata(string id, string realm) : base(IssuerKind.PartnerApp, id, realm)
		{
			this.defaultNameId = string.Format(CultureInfo.InvariantCulture, "{0}@{1}", new object[]
			{
				base.Id,
				base.Realm
			});
			this.defaultIssuer = this.GetIssuer(base.Realm);
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x00030B33 File Offset: 0x0002ED33
		public string GetIssuer()
		{
			return this.defaultIssuer;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x00030B3C File Offset: 0x0002ED3C
		public string GetIssuer(string tenantId)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}@{1}", new object[]
			{
				base.Id,
				tenantId
			});
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00030B6D File Offset: 0x0002ED6D
		public string GetNameId()
		{
			return this.defaultNameId;
		}

		// Token: 0x04000656 RID: 1622
		private readonly string defaultIssuer;

		// Token: 0x04000657 RID: 1623
		private readonly string defaultNameId;
	}
}
