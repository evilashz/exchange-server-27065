using System;

namespace Microsoft.Exchange.Data.Transport.Routing
{
	// Token: 0x0200008A RID: 138
	public abstract class ResolvedMessageEventSource : QueuedMessageEventSource
	{
		// Token: 0x06000334 RID: 820 RVA: 0x000080FA File Offset: 0x000062FA
		internal ResolvedMessageEventSource()
		{
		}

		// Token: 0x06000335 RID: 821
		public abstract RoutingOverride GetRoutingOverride(EnvelopeRecipient recipient);

		// Token: 0x06000336 RID: 822
		public abstract void SetRoutingOverride(EnvelopeRecipient recipient, RoutingOverride routingOverride);

		// Token: 0x06000337 RID: 823
		public abstract string GetTlsDomain(EnvelopeRecipient recipient);

		// Token: 0x06000338 RID: 824
		public abstract void SetTlsDomain(EnvelopeRecipient recipient, string domain);

		// Token: 0x06000339 RID: 825
		internal abstract void SetRoutingOverride(EnvelopeRecipient recipient, RoutingOverride routingOverride, string overrideSource);

		// Token: 0x0600033A RID: 826
		internal abstract RequiredTlsAuthLevel? GetTlsAuthLevel(EnvelopeRecipient recipient);

		// Token: 0x0600033B RID: 827
		internal abstract void SetTlsAuthLevel(EnvelopeRecipient recipient, RequiredTlsAuthLevel? tlsAuthLevel);
	}
}
