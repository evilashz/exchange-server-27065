using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006E3 RID: 1763
	internal class CASMailboxSchema : ADPresentationSchema
	{
		// Token: 0x06005178 RID: 20856 RVA: 0x0012D82D File Offset: 0x0012BA2D
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADUserSchema>();
		}

		// Token: 0x04003731 RID: 14129
		public static readonly ADPropertyDefinition EmailAddresses = ADRecipientSchema.EmailAddresses;

		// Token: 0x04003732 RID: 14130
		public static readonly ADPropertyDefinition LegacyExchangeDN = ADRecipientSchema.LegacyExchangeDN;

		// Token: 0x04003733 RID: 14131
		public static readonly ADPropertyDefinition LinkedMasterAccount = ADRecipientSchema.LinkedMasterAccount;

		// Token: 0x04003734 RID: 14132
		public static readonly ADPropertyDefinition PrimarySmtpAddress = ADRecipientSchema.PrimarySmtpAddress;

		// Token: 0x04003735 RID: 14133
		public static readonly ADPropertyDefinition ProtocolSettings = ADRecipientSchema.ProtocolSettings;

		// Token: 0x04003736 RID: 14134
		public static readonly ADPropertyDefinition SamAccountName = ADMailboxRecipientSchema.SamAccountName;

		// Token: 0x04003737 RID: 14135
		public static readonly ADPropertyDefinition ServerLegacyDN = ADMailboxRecipientSchema.ServerLegacyDN;

		// Token: 0x04003738 RID: 14136
		public static readonly ADPropertyDefinition ServerName = ADMailboxRecipientSchema.ServerName;

		// Token: 0x04003739 RID: 14137
		public static readonly ADPropertyDefinition ActiveSyncAllowedDeviceIDs = ADUserSchema.ActiveSyncAllowedDeviceIDs;

		// Token: 0x0400373A RID: 14138
		public static readonly ADPropertyDefinition ActiveSyncBlockedDeviceIDs = ADUserSchema.ActiveSyncBlockedDeviceIDs;

		// Token: 0x0400373B RID: 14139
		public static readonly ADPropertyDefinition ActiveSyncMailboxPolicy = ADUserSchema.ActiveSyncMailboxPolicy;

		// Token: 0x0400373C RID: 14140
		public static readonly ADPropertyDefinition ActiveSyncMailboxPolicyIsDefaulted = ADUserSchema.ActiveSyncMailboxPolicyIsDefaulted;

		// Token: 0x0400373D RID: 14141
		public static readonly ADPropertyDefinition ActiveSyncDebugLogging = ADUserSchema.ActiveSyncDebugLogging;

		// Token: 0x0400373E RID: 14142
		public static readonly ADPropertyDefinition ActiveSyncEnabled = ADUserSchema.ActiveSyncEnabled;

		// Token: 0x0400373F RID: 14143
		public static readonly ADPropertyDefinition HasActiveSyncDevicePartnership = ADUserSchema.HasActiveSyncDevicePartnership;

		// Token: 0x04003740 RID: 14144
		public static readonly ADPropertyDefinition DisplayName = ADRecipientSchema.DisplayName;

		// Token: 0x04003741 RID: 14145
		public static readonly ADPropertyDefinition OwaMailboxPolicy = ADUserSchema.OwaMailboxPolicy;

		// Token: 0x04003742 RID: 14146
		public static readonly ADPropertyDefinition OWAEnabled = ADRecipientSchema.OWAEnabled;

		// Token: 0x04003743 RID: 14147
		public static readonly ADPropertyDefinition OWAforDevicesEnabled = ADUserSchema.OWAforDevicesEnabled;

		// Token: 0x04003744 RID: 14148
		public static readonly ADPropertyDefinition ECPEnabled = ADRecipientSchema.ECPEnabled;

		// Token: 0x04003745 RID: 14149
		public static readonly ADPropertyDefinition PopEnabled = ADRecipientSchema.PopEnabled;

		// Token: 0x04003746 RID: 14150
		public static readonly ADPropertyDefinition PopUseProtocolDefaults = ADRecipientSchema.PopUseProtocolDefaults;

		// Token: 0x04003747 RID: 14151
		public static readonly ADPropertyDefinition PopMessagesRetrievalMimeFormat = ADRecipientSchema.PopMessagesRetrievalMimeFormat;

		// Token: 0x04003748 RID: 14152
		public static readonly ADPropertyDefinition PopEnableExactRFC822Size = ADRecipientSchema.PopEnableExactRFC822Size;

		// Token: 0x04003749 RID: 14153
		public static readonly ADPropertyDefinition PopProtocolLoggingEnabled = ADRecipientSchema.PopProtocolLoggingEnabled;

		// Token: 0x0400374A RID: 14154
		public static readonly ADPropertyDefinition PopSuppressReadReceipt = ADRecipientSchema.PopSuppressReadReceipt;

		// Token: 0x0400374B RID: 14155
		public static readonly ADPropertyDefinition PopForceICalForCalendarRetrievalOption = ADRecipientSchema.PopForceICalForCalendarRetrievalOption;

		// Token: 0x0400374C RID: 14156
		public static readonly ADPropertyDefinition ImapEnabled = ADRecipientSchema.ImapEnabled;

		// Token: 0x0400374D RID: 14157
		public static readonly ADPropertyDefinition ImapUseProtocolDefaults = ADRecipientSchema.ImapUseProtocolDefaults;

		// Token: 0x0400374E RID: 14158
		public static readonly ADPropertyDefinition ImapMessagesRetrievalMimeFormat = ADRecipientSchema.ImapMessagesRetrievalMimeFormat;

		// Token: 0x0400374F RID: 14159
		public static readonly ADPropertyDefinition ImapEnableExactRFC822Size = ADRecipientSchema.ImapEnableExactRFC822Size;

		// Token: 0x04003750 RID: 14160
		public static readonly ADPropertyDefinition ImapProtocolLoggingEnabled = ADRecipientSchema.ImapProtocolLoggingEnabled;

		// Token: 0x04003751 RID: 14161
		public static readonly ADPropertyDefinition ImapSuppressReadReceipt = ADRecipientSchema.ImapSuppressReadReceipt;

		// Token: 0x04003752 RID: 14162
		public static readonly ADPropertyDefinition ImapForceICalForCalendarRetrievalOption = ADRecipientSchema.ImapForceICalForCalendarRetrievalOption;

		// Token: 0x04003753 RID: 14163
		public static readonly ADPropertyDefinition MAPIEnabled = ADRecipientSchema.MAPIEnabled;

		// Token: 0x04003754 RID: 14164
		public static readonly ADPropertyDefinition MapiHttpEnabled = ADRecipientSchema.MapiHttpEnabled;

		// Token: 0x04003755 RID: 14165
		public static readonly ADPropertyDefinition MAPIBlockOutlookNonCachedMode = ADRecipientSchema.MAPIBlockOutlookNonCachedMode;

		// Token: 0x04003756 RID: 14166
		public static readonly ADPropertyDefinition MAPIBlockOutlookVersions = ADRecipientSchema.MAPIBlockOutlookVersions;

		// Token: 0x04003757 RID: 14167
		public static readonly ADPropertyDefinition MAPIBlockOutlookRpcHttp = ADRecipientSchema.MAPIBlockOutlookRpcHttp;

		// Token: 0x04003758 RID: 14168
		public static readonly ADPropertyDefinition MAPIBlockOutlookExternalConnectivity = ADRecipientSchema.MAPIBlockOutlookExternalConnectivity;

		// Token: 0x04003759 RID: 14169
		public static readonly ADPropertyDefinition MemberOfGroup = ADRecipientSchema.MemberOfGroup;

		// Token: 0x0400375A RID: 14170
		public static readonly ADPropertyDefinition EwsEnabled = ADRecipientSchema.EwsEnabled;

		// Token: 0x0400375B RID: 14171
		public static readonly ADPropertyDefinition EwsAllowOutlook = ADRecipientSchema.EwsAllowOutlook;

		// Token: 0x0400375C RID: 14172
		public static readonly ADPropertyDefinition EwsAllowMacOutlook = ADRecipientSchema.EwsAllowMacOutlook;

		// Token: 0x0400375D RID: 14173
		public static readonly ADPropertyDefinition EwsAllowEntourage = ADRecipientSchema.EwsAllowEntourage;

		// Token: 0x0400375E RID: 14174
		public static readonly ADPropertyDefinition EwsWellKnownApplicationAccessPolicies = ADRecipientSchema.EwsWellKnownApplicationAccessPolicies;

		// Token: 0x0400375F RID: 14175
		public static readonly ADPropertyDefinition EwsApplicationAccessPolicy = ADRecipientSchema.EwsApplicationAccessPolicy;

		// Token: 0x04003760 RID: 14176
		public static readonly ADPropertyDefinition EwsExceptions = ADRecipientSchema.EwsExceptions;

		// Token: 0x04003761 RID: 14177
		public static readonly ADPropertyDefinition AddressBookFlags = ADRecipientSchema.AddressBookFlags;
	}
}
