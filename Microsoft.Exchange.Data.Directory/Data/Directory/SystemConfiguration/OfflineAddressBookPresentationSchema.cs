using System;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200051F RID: 1311
	internal sealed class OfflineAddressBookPresentationSchema : ADPresentationSchema
	{
		// Token: 0x06003A06 RID: 14854 RVA: 0x000E0303 File Offset: 0x000DE503
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<OfflineAddressBookSchema>();
		}

		// Token: 0x04002780 RID: 10112
		public static readonly ADPropertyDefinition Server = OfflineAddressBookSchema.Server;

		// Token: 0x04002781 RID: 10113
		public static readonly ADPropertyDefinition AddressLists = OfflineAddressBookSchema.AddressLists;

		// Token: 0x04002782 RID: 10114
		public static readonly ADPropertyDefinition IsDefault = OfflineAddressBookSchema.IsDefault;

		// Token: 0x04002783 RID: 10115
		public static readonly ADPropertyDefinition PublicFolderDatabase = OfflineAddressBookSchema.PublicFolderDatabase;

		// Token: 0x04002784 RID: 10116
		public static readonly ADPropertyDefinition VirtualDirectories = OfflineAddressBookSchema.VirtualDirectories;

		// Token: 0x04002785 RID: 10117
		public static readonly ADPropertyDefinition MaxBinaryPropertySize = OfflineAddressBookSchema.MaxBinaryPropertySize;

		// Token: 0x04002786 RID: 10118
		public static readonly ADPropertyDefinition MaxMultivaluedBinaryPropertySize = OfflineAddressBookSchema.MaxMultivaluedBinaryPropertySize;

		// Token: 0x04002787 RID: 10119
		public static readonly ADPropertyDefinition MaxStringPropertySize = OfflineAddressBookSchema.MaxStringPropertySize;

		// Token: 0x04002788 RID: 10120
		public static readonly ADPropertyDefinition MaxMultivaluedStringPropertySize = OfflineAddressBookSchema.MaxMultivaluedStringPropertySize;

		// Token: 0x04002789 RID: 10121
		public static readonly ADPropertyDefinition LastTouchedTime = OfflineAddressBookSchema.LastTouchedTime;

		// Token: 0x0400278A RID: 10122
		public static readonly ADPropertyDefinition LastRequestedTime = OfflineAddressBookSchema.LastRequestedTime;

		// Token: 0x0400278B RID: 10123
		public static readonly ADPropertyDefinition LastFailedTime = OfflineAddressBookSchema.LastFailedTime;

		// Token: 0x0400278C RID: 10124
		public static readonly ADPropertyDefinition LastNumberOfRecords = OfflineAddressBookSchema.LastNumberOfRecords;

		// Token: 0x0400278D RID: 10125
		public static readonly ADPropertyDefinition LastGeneratingData = OfflineAddressBookSchema.LastGeneratingData;

		// Token: 0x0400278E RID: 10126
		public static readonly ADPropertyDefinition DiffRetentionPeriod = OfflineAddressBookSchema.DiffRetentionPeriod;

		// Token: 0x0400278F RID: 10127
		public static readonly ADPropertyDefinition Versions = OfflineAddressBookSchema.Versions;

		// Token: 0x04002790 RID: 10128
		public static readonly ADPropertyDefinition Schedule = OfflineAddressBookSchema.Schedule;

		// Token: 0x04002791 RID: 10129
		public static readonly ADPropertyDefinition PublicFolderDistributionEnabled = OfflineAddressBookSchema.PublicFolderDistributionEnabled;

		// Token: 0x04002792 RID: 10130
		public static readonly ADPropertyDefinition GlobalWebDistributionEnabled = OfflineAddressBookSchema.GlobalWebDistributionEnabled;

		// Token: 0x04002793 RID: 10131
		public static readonly ADPropertyDefinition WebDistributionEnabled = OfflineAddressBookSchema.WebDistributionEnabled;

		// Token: 0x04002794 RID: 10132
		public static readonly ADPropertyDefinition ShadowMailboxDistributionEnabled = OfflineAddressBookSchema.ShadowMailboxDistributionEnabled;

		// Token: 0x04002795 RID: 10133
		public static readonly ADPropertyDefinition ConfiguredAttributes = OfflineAddressBookSchema.ConfiguredAttributes;

		// Token: 0x04002796 RID: 10134
		public static readonly ADPropertyDefinition AdminDisplayName = ADConfigurationObjectSchema.AdminDisplayName;

		// Token: 0x04002797 RID: 10135
		public static readonly ADPropertyDefinition GeneratingMailbox = OfflineAddressBookSchema.GeneratingMailbox;
	}
}
