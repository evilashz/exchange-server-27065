using System;
using System.Net;
using Microsoft.Exchange.Connections.Common;
using Microsoft.Exchange.Connections.Eas.Commands.Autodiscover;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas
{
	// Token: 0x02000006 RID: 6
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class EasEndpointSettings
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000025D4 File Offset: 0x000007D4
		public EasEndpointSettings(EasAuthenticationParameters authenticationParameters)
		{
			this.UserSmtpAddress = authenticationParameters.UserSmtpAddress;
			this.NetworkCredential = authenticationParameters.NetworkCredential;
			this.MostRecentEndpoint = new AutodiscoverEndpoint();
			if (!string.IsNullOrEmpty(authenticationParameters.EndpointOverride))
			{
				this.MostRecentEndpoint.DiscoveryDateTime = new DateTime?(DateTime.UtcNow);
				this.MostRecentEndpoint.Url = authenticationParameters.EndpointOverride;
				this.MostRecentDomain = authenticationParameters.EndpointOverride;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002649 File Offset: 0x00000849
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002651 File Offset: 0x00000851
		internal UserSmtpAddress UserSmtpAddress { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000265A File Offset: 0x0000085A
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002662 File Offset: 0x00000862
		internal NetworkCredential NetworkCredential { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000266B File Offset: 0x0000086B
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002673 File Offset: 0x00000873
		internal AutodiscoverEndpoint MostRecentEndpoint { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000267C File Offset: 0x0000087C
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002684 File Offset: 0x00000884
		internal string MostRecentDomain { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004F RID: 79 RVA: 0x0000268D File Offset: 0x0000088D
		internal string UserSmtpAddressString
		{
			get
			{
				return (string)this.UserSmtpAddress;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000269C File Offset: 0x0000089C
		internal string Local
		{
			get
			{
				return this.UserSmtpAddress.Local;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000026B8 File Offset: 0x000008B8
		internal string Domain
		{
			get
			{
				return this.MostRecentDomain ?? this.UserSmtpAddress.Domain;
			}
		}
	}
}
