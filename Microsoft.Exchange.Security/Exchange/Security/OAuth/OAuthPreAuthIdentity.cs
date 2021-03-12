using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000E4 RID: 228
	internal class OAuthPreAuthIdentity : GenericIdentity
	{
		// Token: 0x060007AB RID: 1963 RVA: 0x00035A4B File Offset: 0x00033C4B
		public OAuthPreAuthIdentity(OAuthPreAuthType preAuthType, OrganizationId organizationId, string lookupValue) : base(string.Empty, Constants.BearerPreAuthenticationType)
		{
			this.OrganizationId = organizationId;
			this.PreAuthType = preAuthType;
			this.LookupValue = lookupValue;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x00035A72 File Offset: 0x00033C72
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x00035A7A File Offset: 0x00033C7A
		public OAuthPreAuthType PreAuthType { get; private set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00035A83 File Offset: 0x00033C83
		// (set) Token: 0x060007AF RID: 1967 RVA: 0x00035A8B File Offset: 0x00033C8B
		public OrganizationId OrganizationId { get; private set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x00035A94 File Offset: 0x00033C94
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x00035A9C File Offset: 0x00033C9C
		public string LookupValue { get; private set; }

		// Token: 0x060007B2 RID: 1970 RVA: 0x00035AA5 File Offset: 0x00033CA5
		public override string ToString()
		{
			return string.Format("{0}-{1}", this.PreAuthType, this.LookupValue);
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00035AC2 File Offset: 0x00033CC2
		public override bool IsAuthenticated
		{
			get
			{
				return true;
			}
		}
	}
}
