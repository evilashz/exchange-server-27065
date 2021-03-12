using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Extensibility.Internal
{
	// Token: 0x02000061 RID: 97
	// (Invoke) Token: 0x0600033F RID: 831
	internal delegate SmtpResponse CreateAndSubmitApprovalInitiationForTransportRulesDelegate(ITransportMailItemFacade transportMailItemFacade, string originalSenderAddress, string approverAddresses, string transportRuleName);
}
