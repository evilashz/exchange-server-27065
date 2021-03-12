using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002ED RID: 749
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IADUserFinder
	{
		// Token: 0x0600213A RID: 8506
		IGenericADUser FindBySid(IRecipientSession recipientSession, SecurityIdentifier sid);

		// Token: 0x0600213B RID: 8507
		IGenericADUser FindByProxyAddress(IRecipientSession recipientSession, ProxyAddress proxyAddress);

		// Token: 0x0600213C RID: 8508
		IGenericADUser FindByExchangeGuid(IRecipientSession recipientSession, Guid mailboxGuid, bool includeSystemMailbox);

		// Token: 0x0600213D RID: 8509
		IGenericADUser FindByObjectId(IRecipientSession recipientSession, ADObjectId directoryEntry);

		// Token: 0x0600213E RID: 8510
		IGenericADUser FindByLegacyExchangeDn(IRecipientSession recipientSession, string legacyExchangeDn);

		// Token: 0x0600213F RID: 8511
		IGenericADUser FindMiniRecipientByProxyAddress(IRecipientSession recipientSession, ProxyAddress proxyAddress, PropertyDefinition[] miniRecipientProperties, out StorageMiniRecipient miniRecipient);
	}
}
