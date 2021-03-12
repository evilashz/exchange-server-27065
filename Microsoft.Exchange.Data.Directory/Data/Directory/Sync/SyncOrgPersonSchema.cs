using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000823 RID: 2083
	internal abstract class SyncOrgPersonSchema : SyncRecipientSchema
	{
		// Token: 0x04004428 RID: 17448
		public static SyncPropertyDefinition AssistantName = new SyncPropertyDefinition(ADRecipientSchema.AssistantName, "MSExchAssistantName", typeof(DirectoryPropertyStringSingleLength1To256), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004429 RID: 17449
		public static SyncPropertyDefinition C = new SyncPropertyDefinition(ADOrgPersonSchema.C, "C", typeof(DirectoryPropertyStringSingleLength1To3), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400442A RID: 17450
		public static SyncPropertyDefinition City = new SyncPropertyDefinition(ADOrgPersonSchema.City, "L", typeof(DirectoryPropertyStringSingleLength1To128), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400442B RID: 17451
		public static SyncPropertyDefinition Co = new SyncPropertyDefinition(ADOrgPersonSchema.Co, "Co", typeof(DirectoryPropertyStringSingleLength1To128), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400442C RID: 17452
		public static SyncPropertyDefinition Company = new SyncPropertyDefinition(ADOrgPersonSchema.Company, "Company", typeof(DirectoryPropertyStringSingleLength1To64), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400442D RID: 17453
		public static SyncPropertyDefinition CountryCode = new SyncPropertyDefinition(ADOrgPersonSchema.CountryCode, "CountryCode", typeof(DirectoryPropertyInt32SingleMin0Max65535), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400442E RID: 17454
		public static SyncPropertyDefinition Department = new SyncPropertyDefinition(ADOrgPersonSchema.Department, "Department", typeof(DirectoryPropertyStringSingleLength1To64), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400442F RID: 17455
		public static SyncPropertyDefinition Fax = new SyncPropertyDefinition(ADOrgPersonSchema.Fax, "FacsimileTelephoneNumber", typeof(DirectoryPropertyStringSingleLength1To64), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004430 RID: 17456
		public static SyncPropertyDefinition FirstName = new SyncPropertyDefinition(ADOrgPersonSchema.FirstName, "GivenName", typeof(DirectoryPropertyStringSingleLength1To64), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004431 RID: 17457
		public static SyncPropertyDefinition HomePhone = new SyncPropertyDefinition(ADOrgPersonSchema.HomePhone, "HomePhone", typeof(DirectoryPropertyStringSingleLength1To64), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004432 RID: 17458
		public static SyncPropertyDefinition Initials = new SyncPropertyDefinition(ADOrgPersonSchema.Initials, "Initials", typeof(DirectoryPropertyStringSingleLength1To6), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004433 RID: 17459
		public static SyncPropertyDefinition LastName = new SyncPropertyDefinition(ADOrgPersonSchema.LastName, "Sn", typeof(DirectoryPropertyStringSingleLength1To64), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004434 RID: 17460
		public static SyncPropertyDefinition MobilePhone = new SyncPropertyDefinition(ADOrgPersonSchema.MobilePhone, "Mobile", typeof(DirectoryPropertyStringSingleLength1To64), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004435 RID: 17461
		public static SyncPropertyDefinition Notes = new SyncPropertyDefinition(ADRecipientSchema.Notes, "Info", typeof(DirectoryPropertyStringSingleLength1To1024), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004436 RID: 17462
		public static SyncPropertyDefinition Office = new SyncPropertyDefinition(ADOrgPersonSchema.Office, "PhysicalDeliveryOfficeName", typeof(DirectoryPropertyStringSingleLength1To128), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004437 RID: 17463
		public static SyncPropertyDefinition OtherFax = new SyncPropertyDefinition(ADOrgPersonSchema.OtherFax, "OtherFacsimileTelephoneNumber", typeof(DirectoryPropertyStringLength1To64), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004438 RID: 17464
		public static SyncPropertyDefinition OtherHomePhone = new SyncPropertyDefinition(ADOrgPersonSchema.OtherHomePhone, "OtherHomePhone", typeof(DirectoryPropertyStringLength1To64), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004439 RID: 17465
		public static SyncPropertyDefinition OtherTelephone = new SyncPropertyDefinition(ADOrgPersonSchema.OtherTelephone, "OtherTelephone", typeof(DirectoryPropertyStringLength1To64), SyncPropertyDefinitionFlags.ForwardSync | SyncPropertyDefinitionFlags.BackSync | SyncPropertyDefinitionFlags.MultiValued, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400443A RID: 17466
		public static SyncPropertyDefinition Pager = new SyncPropertyDefinition(ADOrgPersonSchema.Pager, "Pager", typeof(DirectoryPropertyStringSingleLength1To64), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400443B RID: 17467
		public static SyncPropertyDefinition Phone = new SyncPropertyDefinition(ADOrgPersonSchema.Phone, "TelephoneNumber", typeof(DirectoryPropertyStringSingleLength1To64), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400443C RID: 17468
		public static SyncPropertyDefinition PostalCode = new SyncPropertyDefinition(ADOrgPersonSchema.PostalCode, "PostalCode", typeof(DirectoryPropertyStringSingleLength1To40), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400443D RID: 17469
		public static SyncPropertyDefinition StateOrProvince = new SyncPropertyDefinition(ADOrgPersonSchema.StateOrProvince, "St", typeof(DirectoryPropertyStringSingleLength1To128), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400443E RID: 17470
		public static SyncPropertyDefinition StreetAddress = new SyncPropertyDefinition(ADOrgPersonSchema.StreetAddress, "StreetAddress", typeof(DirectoryPropertyStringSingleLength1To1024), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x0400443F RID: 17471
		public static SyncPropertyDefinition TelephoneAssistant = new SyncPropertyDefinition(ADOrgPersonSchema.TelephoneAssistant, "TelephoneAssistant", typeof(DirectoryPropertyStringSingleLength1To64), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004440 RID: 17472
		public static SyncPropertyDefinition Title = new SyncPropertyDefinition(ADOrgPersonSchema.Title, "Title", typeof(DirectoryPropertyStringSingleLength1To128), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);

		// Token: 0x04004441 RID: 17473
		public static SyncPropertyDefinition WebPage = new SyncPropertyDefinition(ADRecipientSchema.WebPage, "WwwHomepage", typeof(DirectoryPropertyStringSingleLength1To2048), SyncPropertyDefinitionFlags.TwoWay, SyncPropertyDefinition.InitialSyncPropertySetVersion);
	}
}
