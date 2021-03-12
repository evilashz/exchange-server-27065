using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.ManagementEndpoint
{
	// Token: 0x02000479 RID: 1145
	public sealed class ManagementEndpoint
	{
		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06002843 RID: 10307 RVA: 0x0009E9EE File Offset: 0x0009CBEE
		// (set) Token: 0x06002844 RID: 10308 RVA: 0x0009E9F6 File Offset: 0x0009CBF6
		public SmtpDomain DomainName { get; internal set; }

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06002845 RID: 10309 RVA: 0x0009E9FF File Offset: 0x0009CBFF
		// (set) Token: 0x06002846 RID: 10310 RVA: 0x0009EA07 File Offset: 0x0009CC07
		public Guid ExternalDirectoryOrganizationId { get; internal set; }

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06002847 RID: 10311 RVA: 0x0009EA10 File Offset: 0x0009CC10
		// (set) Token: 0x06002848 RID: 10312 RVA: 0x0009EA18 File Offset: 0x0009CC18
		public string RemotePowershellUrl { get; internal set; }

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06002849 RID: 10313 RVA: 0x0009EA21 File Offset: 0x0009CC21
		// (set) Token: 0x0600284A RID: 10314 RVA: 0x0009EA29 File Offset: 0x0009CC29
		public string ResourceForest { get; internal set; }

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x0600284B RID: 10315 RVA: 0x0009EA32 File Offset: 0x0009CC32
		// (set) Token: 0x0600284C RID: 10316 RVA: 0x0009EA3A File Offset: 0x0009CC3A
		public string AccountPartition { get; internal set; }

		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x0600284D RID: 10317 RVA: 0x0009EA43 File Offset: 0x0009CC43
		// (set) Token: 0x0600284E RID: 10318 RVA: 0x0009EA4B File Offset: 0x0009CC4B
		public string SmtpNextHopDomain { get; internal set; }

		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x0009EA54 File Offset: 0x0009CC54
		// (set) Token: 0x06002850 RID: 10320 RVA: 0x0009EA5C File Offset: 0x0009CC5C
		public ManagementEndpointVersion Version { get; private set; }

		// Token: 0x06002851 RID: 10321 RVA: 0x0009EA65 File Offset: 0x0009CC65
		public ManagementEndpoint(string remotePowershellUrl, ManagementEndpointVersion version)
		{
			this.Version = version;
			this.RemotePowershellUrl = remotePowershellUrl;
		}
	}
}
