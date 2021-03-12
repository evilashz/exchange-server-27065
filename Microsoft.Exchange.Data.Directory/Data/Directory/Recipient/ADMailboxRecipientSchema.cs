using System;
using System.Security.Principal;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200026E RID: 622
	internal class ADMailboxRecipientSchema : ADRecipientSchema
	{
		// Token: 0x04000FB7 RID: 4023
		public static readonly ADPropertyDefinition IsSecurityPrincipal = IADSecurityPrincipalSchema.IsSecurityPrincipal;

		// Token: 0x04000FB8 RID: 4024
		public static readonly ADPropertyDefinition SamAccountName = IADSecurityPrincipalSchema.SamAccountName;

		// Token: 0x04000FB9 RID: 4025
		public static readonly ADPropertyDefinition Sid = IADSecurityPrincipalSchema.Sid;

		// Token: 0x04000FBA RID: 4026
		public static readonly ADPropertyDefinition SidHistory = IADSecurityPrincipalSchema.SidHistory;

		// Token: 0x04000FBB RID: 4027
		public static readonly ADPropertyDefinition Database = IADMailStorageSchema.Database;

		// Token: 0x04000FBC RID: 4028
		public static readonly ADPropertyDefinition DelegateListLink = IADMailStorageSchema.DelegateListLink;

		// Token: 0x04000FBD RID: 4029
		public static readonly ADPropertyDefinition DelegateListBL = IADMailStorageSchema.DelegateListBL;

		// Token: 0x04000FBE RID: 4030
		public static readonly ADPropertyDefinition DeletedItemFlags = IADMailStorageSchema.DeletedItemFlags;

		// Token: 0x04000FBF RID: 4031
		public static readonly ADPropertyDefinition DeliverToMailboxAndForward = IADMailStorageSchema.DeliverToMailboxAndForward;

		// Token: 0x04000FC0 RID: 4032
		public static readonly ADPropertyDefinition ExchangeGuid = IADMailStorageSchema.ExchangeGuid;

		// Token: 0x04000FC1 RID: 4033
		public static readonly ADPropertyDefinition ExchangeSecurityDescriptor = IADMailStorageSchema.ExchangeSecurityDescriptor;

		// Token: 0x04000FC2 RID: 4034
		public static readonly ADPropertyDefinition ExternalOofOptions = IADMailStorageSchema.ExternalOofOptions;

		// Token: 0x04000FC3 RID: 4035
		public static readonly ADPropertyDefinition RetainDeletedItemsFor = IADMailStorageSchema.RetainDeletedItemsFor;

		// Token: 0x04000FC4 RID: 4036
		public static readonly ADPropertyDefinition IsMailboxEnabled = IADMailStorageSchema.IsMailboxEnabled;

		// Token: 0x04000FC5 RID: 4037
		public static readonly ADPropertyDefinition IssueWarningQuota = IADMailStorageSchema.IssueWarningQuota;

		// Token: 0x04000FC6 RID: 4038
		public static readonly ADPropertyDefinition OfflineAddressBook = IADMailStorageSchema.OfflineAddressBook;

		// Token: 0x04000FC7 RID: 4039
		public static readonly ADPropertyDefinition ProhibitSendQuota = IADMailStorageSchema.ProhibitSendQuota;

		// Token: 0x04000FC8 RID: 4040
		public static readonly ADPropertyDefinition ProhibitSendReceiveQuota = IADMailStorageSchema.ProhibitSendReceiveQuota;

		// Token: 0x04000FC9 RID: 4041
		public static readonly ADPropertyDefinition RulesQuota = IADMailStorageSchema.RulesQuota;

		// Token: 0x04000FCA RID: 4042
		public static readonly ADPropertyDefinition ServerLegacyDN = IADMailStorageSchema.ServerLegacyDN;

		// Token: 0x04000FCB RID: 4043
		public static readonly ADPropertyDefinition ServerName = IADMailStorageSchema.ServerName;

		// Token: 0x04000FCC RID: 4044
		public static readonly ADPropertyDefinition SharePointResources = IADMailStorageSchema.SharePointResources;

		// Token: 0x04000FCD RID: 4045
		public static readonly ADPropertyDefinition SharePointUrl = IADMailStorageSchema.SharePointUrl;

		// Token: 0x04000FCE RID: 4046
		public static readonly ADPropertyDefinition UseDatabaseQuotaDefaults = IADMailStorageSchema.UseDatabaseQuotaDefaults;

		// Token: 0x04000FCF RID: 4047
		public static readonly ADPropertyDefinition YammerGroupAddress = new ADPropertyDefinition("YammerGroupAddress", ExchangeObjectVersion.Exchange2012, typeof(string), "msExchFBURL", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x04000FD0 RID: 4048
		public static readonly ADPropertyDefinition WhenMailboxCreated = new ADPropertyDefinition("WhenMailboxCreated", ExchangeObjectVersion.Exchange2003, typeof(DateTime?), "msExchWhenMailboxCreated", ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new DelegateConstraint(new ValidationDelegate(ConstraintDelegates.ValidateWhenMailboxCreated))
		}, null, MbxRecipientSchema.WhenMailboxCreated);

		// Token: 0x04000FD1 RID: 4049
		public static readonly ADPropertyDefinition PublicToGroupSids = new ADPropertyDefinition("PublicToGroupSids", ExchangeObjectVersion.Exchange2010, typeof(SecurityIdentifier), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADMailboxRecipientSchema.DelegateListLink
		}, null, new GetterDelegate(GroupMailbox.PublicToGroupSidsGetter), null, null, null);

		// Token: 0x04000FD2 RID: 4050
		public static readonly ADPropertyDefinition GroupMailboxSharePointSiteUrl = new ADPropertyDefinition("GroupMailboxSharePointSiteUrl", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADMailboxRecipientSchema.SharePointResources
		}, null, new GetterDelegate(GroupMailbox.SharePointSiteUrlGetter), null, null, null);

		// Token: 0x04000FD3 RID: 4051
		public static readonly ADPropertyDefinition GroupMailboxSharePointDocumentsUrl = new ADPropertyDefinition("GroupMailboxSharePointDocumentsUrl", ExchangeObjectVersion.Exchange2010, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			ADMailboxRecipientSchema.SharePointResources
		}, null, new GetterDelegate(GroupMailbox.SharePointDocumentsUrlGetter), null, null, null);
	}
}
