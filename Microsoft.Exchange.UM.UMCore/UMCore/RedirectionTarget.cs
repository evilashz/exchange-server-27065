using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000251 RID: 593
	internal abstract class RedirectionTarget
	{
		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001191 RID: 4497 RVA: 0x0004D6D4 File Offset: 0x0004B8D4
		public static RedirectionTarget Instance
		{
			get
			{
				return RedirectionTarget.instance;
			}
		}

		// Token: 0x06001192 RID: 4498
		public abstract RedirectionTarget.ResultSet GetForCallAnsweringCall(UMRecipient user, IRoutingContext context);

		// Token: 0x06001193 RID: 4499
		public abstract RedirectionTarget.ResultSet GetForSubscriberAccessCall(UMRecipient user, IRoutingContext context);

		// Token: 0x06001194 RID: 4500
		public abstract RedirectionTarget.ResultSet GetForNonUserSpecificCall(OrganizationId orgId, IRoutingContext context);

		// Token: 0x04000BEE RID: 3054
		private static readonly RedirectionTarget instance = CommonConstants.UseDataCenterCallRouting ? (UMRecyclerConfig.UseDataCenterActiveManagerRouting ? new DataCenterActiveManagerRedirectionTarget() : new DataCenterLegacySupportRedirectionTarget()) : new EnterpriseRedirectionTarget();

		// Token: 0x02000252 RID: 594
		internal class ResultSet
		{
			// Token: 0x17000444 RID: 1092
			// (get) Token: 0x06001197 RID: 4503 RVA: 0x0004D70B File Offset: 0x0004B90B
			// (set) Token: 0x06001198 RID: 4504 RVA: 0x0004D713 File Offset: 0x0004B913
			public PlatformSipUri Uri { get; private set; }

			// Token: 0x17000445 RID: 1093
			// (get) Token: 0x06001199 RID: 4505 RVA: 0x0004D71C File Offset: 0x0004B91C
			// (set) Token: 0x0600119A RID: 4506 RVA: 0x0004D724 File Offset: 0x0004B924
			public string Fqdn { get; private set; }

			// Token: 0x17000446 RID: 1094
			// (get) Token: 0x0600119B RID: 4507 RVA: 0x0004D72D File Offset: 0x0004B92D
			// (set) Token: 0x0600119C RID: 4508 RVA: 0x0004D735 File Offset: 0x0004B935
			public int Port { get; private set; }

			// Token: 0x0600119D RID: 4509 RVA: 0x0004D73E File Offset: 0x0004B93E
			public ResultSet(PlatformSipUri uri, string fqdn, int port)
			{
				this.Uri = uri;
				this.Fqdn = fqdn;
				this.Port = port;
			}
		}
	}
}
