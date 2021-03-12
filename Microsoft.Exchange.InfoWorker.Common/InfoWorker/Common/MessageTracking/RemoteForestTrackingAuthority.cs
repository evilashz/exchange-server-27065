using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x020002BF RID: 703
	internal class RemoteForestTrackingAuthority : ADAuthenticationTrackingAuthority
	{
		// Token: 0x06001398 RID: 5016 RVA: 0x0005AF23 File Offset: 0x00059123
		public static RemoteForestTrackingAuthority Create(string domain, SmtpAddress proxyRecipient)
		{
			return new RemoteForestTrackingAuthority(domain, proxyRecipient);
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0005AF2C File Offset: 0x0005912C
		public override bool IsAllowedScope(SearchScope scope)
		{
			return scope == SearchScope.Organization || scope == SearchScope.World;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0005AF38 File Offset: 0x00059138
		public override string ToString()
		{
			return string.Format("Type=RemoteForestTrackingAuthority,Domain={0}", this.domain);
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x0005AF4A File Offset: 0x0005914A
		public override SearchScope AssociatedScope
		{
			get
			{
				return SearchScope.Forest;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x0005AF4D File Offset: 0x0005914D
		public override string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x0005AF55 File Offset: 0x00059155
		// (set) Token: 0x0600139E RID: 5022 RVA: 0x0005AF5D File Offset: 0x0005915D
		public override SmtpAddress ProxyRecipient { get; protected set; }

		// Token: 0x0600139F RID: 5023 RVA: 0x0005AF68 File Offset: 0x00059168
		private RemoteForestTrackingAuthority(string domain, SmtpAddress proxyRecipient) : base(TrackingAuthorityKind.RemoteForest, null)
		{
			if (string.IsNullOrEmpty(domain) && SmtpAddress.Empty.Equals(proxyRecipient))
			{
				throw new ArgumentException("Either domain or proxyRecipient must be supplied, otherwise we cannot autodiscover the remote forest");
			}
			this.ProxyRecipient = proxyRecipient;
			this.domain = domain;
		}

		// Token: 0x04000D09 RID: 3337
		private string domain;
	}
}
