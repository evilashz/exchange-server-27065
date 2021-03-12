using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000391 RID: 913
	internal class RequestParameters
	{
		// Token: 0x06001D27 RID: 7463 RVA: 0x00074550 File Offset: 0x00072750
		public RequestParameters(Guid requestId, string tag, MobileSpeechRecoRequestType requestType, CultureInfo culture, ExTimeZone timeZone, Guid userObjectGuid, Guid tenantGuid, OrganizationId orgId)
		{
			this.RequestId = requestId;
			this.Tag = tag;
			this.RequestType = requestType;
			this.Culture = culture;
			this.TimeZone = timeZone;
			this.UserObjectGuid = userObjectGuid;
			this.TenantGuid = tenantGuid;
			this.OrgId = orgId;
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001D28 RID: 7464 RVA: 0x000745A0 File Offset: 0x000727A0
		// (set) Token: 0x06001D29 RID: 7465 RVA: 0x000745A8 File Offset: 0x000727A8
		public Guid RequestId { get; private set; }

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001D2A RID: 7466 RVA: 0x000745B1 File Offset: 0x000727B1
		// (set) Token: 0x06001D2B RID: 7467 RVA: 0x000745B9 File Offset: 0x000727B9
		public string Tag { get; private set; }

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001D2C RID: 7468 RVA: 0x000745C2 File Offset: 0x000727C2
		// (set) Token: 0x06001D2D RID: 7469 RVA: 0x000745CA File Offset: 0x000727CA
		public MobileSpeechRecoRequestType RequestType { get; private set; }

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001D2E RID: 7470 RVA: 0x000745D3 File Offset: 0x000727D3
		// (set) Token: 0x06001D2F RID: 7471 RVA: 0x000745DB File Offset: 0x000727DB
		public CultureInfo Culture { get; private set; }

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x000745E4 File Offset: 0x000727E4
		// (set) Token: 0x06001D31 RID: 7473 RVA: 0x000745EC File Offset: 0x000727EC
		public ExTimeZone TimeZone { get; private set; }

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x000745F5 File Offset: 0x000727F5
		// (set) Token: 0x06001D33 RID: 7475 RVA: 0x000745FD File Offset: 0x000727FD
		public Guid UserObjectGuid { get; private set; }

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x00074606 File Offset: 0x00072806
		// (set) Token: 0x06001D35 RID: 7477 RVA: 0x0007460E File Offset: 0x0007280E
		public Guid TenantGuid { get; private set; }

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x00074617 File Offset: 0x00072817
		// (set) Token: 0x06001D37 RID: 7479 RVA: 0x0007461F File Offset: 0x0007281F
		public OrganizationId OrgId { get; private set; }
	}
}
