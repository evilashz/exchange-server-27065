using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004FF RID: 1279
	internal sealed class MRSRequestSchema : ADConfigurationObjectSchema
	{
		// Token: 0x040026B5 RID: 9909
		public static readonly ADPropertyDefinition MailboxMoveStatus = SharedPropertyDefinitions.MailboxMoveStatus;

		// Token: 0x040026B6 RID: 9910
		public static readonly ADPropertyDefinition MailboxMoveFlags = SharedPropertyDefinitions.MailboxMoveFlags;

		// Token: 0x040026B7 RID: 9911
		public static readonly ADPropertyDefinition MailboxMoveBatchName = SharedPropertyDefinitions.MailboxMoveBatchName;

		// Token: 0x040026B8 RID: 9912
		public static readonly ADPropertyDefinition MailboxMoveRemoteHostName = SharedPropertyDefinitions.MailboxMoveRemoteHostName;

		// Token: 0x040026B9 RID: 9913
		public static readonly ADPropertyDefinition MailboxMoveSourceMDB = SharedPropertyDefinitions.MailboxMoveSourceMDB;

		// Token: 0x040026BA RID: 9914
		public static readonly ADPropertyDefinition MailboxMoveTargetMDB = SharedPropertyDefinitions.MailboxMoveTargetMDB;

		// Token: 0x040026BB RID: 9915
		public static readonly ADPropertyDefinition MailboxMoveFilePath = new ADPropertyDefinition("MailboxMoveFilePath", ExchangeObjectVersion.Exchange2003, typeof(string), "msExchMailboxMoveFilePath", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040026BC RID: 9916
		public static readonly ADPropertyDefinition MailboxMoveRequestGuid = new ADPropertyDefinition("MailboxMoveRequestGuid", ExchangeObjectVersion.Exchange2003, typeof(Guid), "msExchMailboxMoveRequestGuid", ADPropertyDefinitionFlags.Binary | ADPropertyDefinitionFlags.DoNotProvisionalClone, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040026BD RID: 9917
		public static readonly ADPropertyDefinition MailboxMoveStorageMDB = new ADPropertyDefinition("MailboxMoveStorageMDB", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), null, "msExchMailboxMoveStorageMDBLink", null, "msExchMailboxMoveStorageMDBLinkSL", ADPropertyDefinitionFlags.ValidateInFirstOrganization, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, SimpleProviderPropertyDefinition.None, null, null, null, null, null);

		// Token: 0x040026BE RID: 9918
		public static readonly ADPropertyDefinition MailboxMoveSourceUser = new ADPropertyDefinition("MailboxMoveSourceUser", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchMailboxMoveSourceUserLink", ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040026BF RID: 9919
		public static readonly ADPropertyDefinition MailboxMoveTargetUser = new ADPropertyDefinition("MailboxMoveTargetUser", ExchangeObjectVersion.Exchange2003, typeof(ADObjectId), "msExchMailboxMoveTargetUserLink", ADPropertyDefinitionFlags.DoNotProvisionalClone, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040026C0 RID: 9920
		public static readonly ADPropertyDefinition MRSRequestType = new ADPropertyDefinition("MRSRequestType", ExchangeObjectVersion.Exchange2003, typeof(MRSRequestType), "msExchMRSRequestType", ADPropertyDefinitionFlags.None, Microsoft.Exchange.Data.Directory.SystemConfiguration.MRSRequestType.Move, new PropertyDefinitionConstraint[]
		{
			new EnumValueDefinedConstraint(typeof(MRSRequestType))
		}, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040026C1 RID: 9921
		public static readonly ADPropertyDefinition DisplayName = SharedPropertyDefinitions.MandatoryDisplayName;
	}
}
