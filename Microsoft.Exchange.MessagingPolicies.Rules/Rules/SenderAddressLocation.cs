using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules
{
	// Token: 0x02000031 RID: 49
	public enum SenderAddressLocation
	{
		// Token: 0x04000158 RID: 344
		[LocDescription(TransportRulesStrings.IDs.SenderAddressLocationHeader)]
		Header,
		// Token: 0x04000159 RID: 345
		[LocDescription(TransportRulesStrings.IDs.SenderAddressLocationEnvelope)]
		Envelope,
		// Token: 0x0400015A RID: 346
		[LocDescription(TransportRulesStrings.IDs.SenderAddressLocationHeaderOrEnvelope)]
		HeaderOrEnvelope
	}
}
