using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Principal;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000007 RID: 7
	internal interface IADRecipientLookup
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000069 RID: 105
		IRecipientSession ScopedRecipientSession { get; }

		// Token: 0x0600006A RID: 106
		ADUser GetUMDataStorageMailbox();

		// Token: 0x0600006B RID: 107
		ADRecipient LookupByExchangeGuid(Guid exchangeGuid);

		// Token: 0x0600006C RID: 108
		ADRecipient LookupByObjectId(ADObjectId objectId);

		// Token: 0x0600006D RID: 109
		ADRecipient LookupByExtensionAndDialPlan(string extension, UMDialPlan dialPlan);

		// Token: 0x0600006E RID: 110
		ADRecipient LookupByExtensionAndEquivalentDialPlan(string extension, UMDialPlan dialPlan);

		// Token: 0x0600006F RID: 111
		ADRecipient LookupByExchangePrincipal(IExchangePrincipal exchangePrincipal);

		// Token: 0x06000070 RID: 112
		ADRecipient LookupByLegacyExchangeDN(string legacyExchangeDN);

		// Token: 0x06000071 RID: 113
		ADRecipient LookupBySmtpAddress(string emailAddress);

		// Token: 0x06000072 RID: 114
		ADRecipient[] LookupBySmtpAddresses(List<string> smtpAddresses);

		// Token: 0x06000073 RID: 115
		ADRecipient LookupByUmAddress(string proxyAddressStr);

		// Token: 0x06000074 RID: 116
		ADRecipient LookupByParticipant(Participant p);

		// Token: 0x06000075 RID: 117
		ADRecipient LookupBySipExtension(string extension);

		// Token: 0x06000076 RID: 118
		ADRecipient LookupByEumSipResourceIdentifierPrefix(string sipUri);

		// Token: 0x06000077 RID: 119
		ADRecipient[] LookupByDtmfMap(string mode, string dtmf, bool removeHiddenUsers, bool anonymousCaller, UMDialPlan targetDialPlan, int numberOfResults);

		// Token: 0x06000078 RID: 120
		ADRecipient[] LookupByQueryFilter(QueryFilter filter);

		// Token: 0x06000079 RID: 121
		void ProcessRecipients(QueryFilter recipientFilter, PropertyDefinition[] properties, ADConfigurationProcessor<ADRawEntry> configurationProcessor, int retryCount);

		// Token: 0x0600007A RID: 122
		bool TenantSizeExceedsThreshold(QueryFilter filter, int threshold);
	}
}
