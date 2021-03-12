using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Configuration.MonadDataProvider
{
	// Token: 0x020001AC RID: 428
	internal class CASMailboxCreator : ConfigurableObjectCreator
	{
		// Token: 0x06000F8F RID: 3983 RVA: 0x0002E7E4 File Offset: 0x0002C9E4
		internal override IList<string> GetProperties(string fullName)
		{
			return new string[]
			{
				"Identity",
				"EmailAddresses",
				"LegacyExchangeDN",
				"LinkedMasterAccount",
				"PrimarySmtpAddress",
				"SamAccountName",
				"ServerLegacyDN",
				"DisplayName",
				"Name",
				"DistinguishedName",
				"ObjectCategory",
				"WhenChanged",
				"ActiveSyncAllowedDeviceIDs",
				"ActiveSyncBlockedDeviceIDs",
				"OWAEnabled",
				"ActiveSyncEnabled",
				"MAPIEnabled",
				"MapiHttpEnabled",
				"PopEnabled",
				"ImapEnabled",
				"OwaMailboxPolicy",
				"ActiveSyncMailboxPolicy",
				"ActiveSyncMailboxPolicyIsDefaulted",
				"ActiveSyncDebugLogging",
				"HasActiveSyncDevicePartnership",
				"ExternalImapSettings",
				"InternalImapSettings",
				"ExternalPopSettings",
				"InternalPopSettings",
				"ExternalSmtpSettings",
				"InternalSmtpSettings",
				"ECPEnabled",
				"EmwsEnabled",
				"ImapUseProtocolDefaults",
				"ImapUseProtocolDefaults",
				"ImapMessagesRetrievalMimeFormat",
				"PopUseProtocolDefaults",
				"PopMessagesRetrievalMimeFormat",
				"PopEnableExactRFC822Size",
				"PopSuppressReadReceipt",
				"ImapEnableExactRFC822Size",
				"ImapSuppressReadReceipt",
				"MAPIBlockOutlookNonCachedMode",
				"MAPIBlockOutlookVersions",
				"MAPIBlockOutlookRpcHttp",
				"MAPIBlockOutlookExternalConnectivity",
				"EwsEnabled",
				"EwsAllowOutlook",
				"EwsAllowMacOutlook",
				"EwsAllowEntourage",
				"EwsApplicationAccessPolicy",
				"EwsAllowList",
				"EwsBlockList",
				"ShowGalAsDefaultView"
			};
		}
	}
}
