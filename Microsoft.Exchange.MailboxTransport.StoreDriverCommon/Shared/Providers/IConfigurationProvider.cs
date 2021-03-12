using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Shared.Providers
{
	// Token: 0x02000021 RID: 33
	internal interface IConfigurationProvider
	{
		// Token: 0x060000DC RID: 220
		OutboundConversionOptions GetGlobalConversionOptions();

		// Token: 0x060000DD RID: 221
		string GetDefaultDomainName();

		// Token: 0x060000DE RID: 222
		bool TryGetDefaultDomainName(OrganizationId organizationId, out string domainName);

		// Token: 0x060000DF RID: 223
		void SendNDRForInvalidAddresses(IReadOnlyMailItem mailItemToSubmit, List<DsnRecipientInfo> invalidRecipients, DsnMailOutHandlerDelegate dsnMailOutHandler);

		// Token: 0x060000E0 RID: 224
		void SendNDRForFailedSubmission(IReadOnlyMailItem ndrMailItem, SmtpResponse ndrReason, DsnMailOutHandlerDelegate dsnMailOutHandler);

		// Token: 0x060000E1 RID: 225
		string GetQuarantineMailbox();

		// Token: 0x060000E2 RID: 226
		bool GetForwardingProhibitedFeatureStatus();
	}
}
