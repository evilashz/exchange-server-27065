using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Eas.Commands.Autodiscover
{
	// Token: 0x02000019 RID: 25
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class AutodiscoverEndpoint
	{
		// Token: 0x0600009D RID: 157 RVA: 0x00003774 File Offset: 0x00001974
		internal AutodiscoverEndpoint()
		{
			this.DiscoveryDateTime = new DateTime?(DateTime.MinValue);
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600009E RID: 158 RVA: 0x0000378C File Offset: 0x0000198C
		// (set) Token: 0x0600009F RID: 159 RVA: 0x00003794 File Offset: 0x00001994
		internal string Url { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000379D File Offset: 0x0000199D
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000037A5 File Offset: 0x000019A5
		internal DateTime? DiscoveryDateTime { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000037AE File Offset: 0x000019AE
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000037B6 File Offset: 0x000019B6
		internal DateTime? ExplicitExpiration { get; set; }

		// Token: 0x060000A4 RID: 164 RVA: 0x000037BF File Offset: 0x000019BF
		internal bool IsPotentiallyReusable()
		{
			return this.ExplicitExpirationIsUsable() || this.DiscoveryExpirationIsUsable();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000037D4 File Offset: 0x000019D4
		private bool ExplicitExpirationIsUsable()
		{
			return (DateTime.UtcNow - this.ExplicitExpiration.GetValueOrDefault()).TotalHours < 24.0;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000380C File Offset: 0x00001A0C
		private bool DiscoveryExpirationIsUsable()
		{
			TimeSpan timeSpan = DateTime.UtcNow - this.DiscoveryDateTime.GetValueOrDefault();
			return !string.IsNullOrWhiteSpace(this.Url) && timeSpan.TotalHours < 24.0;
		}

		// Token: 0x040000C4 RID: 196
		private const double MaxUsableHours = 24.0;
	}
}
