using System;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001EA RID: 490
	internal interface IADUser : IADMailboxRecipient, IADMailStorage, IADSecurityPrincipal, IADOrgPerson, IADRecipient, IADObject, IADRawEntry, IConfigurable, IPropertyBag, IReadOnlyPropertyBag, IOriginatingChangeTimestamp, IFederatedIdentityParameters, IProvisioningCacheInvalidation
	{
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060016F7 RID: 5879
		// (set) Token: 0x060016F8 RID: 5880
		ADObjectId ActiveSyncMailboxPolicy { get; set; }

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060016F9 RID: 5881
		MultiValuedProperty<Guid> AggregatedMailboxGuids { get; }

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060016FA RID: 5882
		// (set) Token: 0x060016FB RID: 5883
		ADObjectId ArchiveDatabase { get; set; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060016FC RID: 5884
		SmtpDomain ArchiveDomain { get; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x060016FD RID: 5885
		Guid ArchiveGuid { get; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x060016FE RID: 5886
		// (set) Token: 0x060016FF RID: 5887
		MultiValuedProperty<string> ArchiveName { get; set; }

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001700 RID: 5888
		ArchiveState ArchiveState { get; }

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001701 RID: 5889
		ArchiveStatusFlags ArchiveStatus { get; }

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001702 RID: 5890
		IMailboxLocationCollection MailboxLocations { get; }

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001703 RID: 5891
		NetID NetID { get; }

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001704 RID: 5892
		// (set) Token: 0x06001705 RID: 5893
		ADObjectId OwaMailboxPolicy { get; set; }

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001706 RID: 5894
		// (set) Token: 0x06001707 RID: 5895
		ADObjectId QueryBaseDN { get; set; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001708 RID: 5896
		// (set) Token: 0x06001709 RID: 5897
		Capability? SKUCapability { get; set; }

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600170A RID: 5898
		// (set) Token: 0x0600170B RID: 5899
		bool? SKUAssigned { get; set; }

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600170C RID: 5900
		// (set) Token: 0x0600170D RID: 5901
		string UserPrincipalName { get; set; }

		// Token: 0x0600170E RID: 5902
		SmtpAddress GetFederatedSmtpAddress();
	}
}
