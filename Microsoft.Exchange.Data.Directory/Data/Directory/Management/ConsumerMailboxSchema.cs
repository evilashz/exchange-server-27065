using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006EA RID: 1770
	internal class ConsumerMailboxSchema : ADPresentationSchema
	{
		// Token: 0x060052D1 RID: 21201 RVA: 0x0012FCB5 File Offset: 0x0012DEB5
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADUserSchema>();
		}

		// Token: 0x040037E5 RID: 14309
		public static readonly ADPropertyDefinition Alias = ADRecipientSchema.Alias;

		// Token: 0x040037E6 RID: 14310
		public static readonly ADPropertyDefinition Database = ADMailboxRecipientSchema.Database;

		// Token: 0x040037E7 RID: 14311
		public static readonly ADPropertyDefinition Description = ADRecipientSchema.Description;

		// Token: 0x040037E8 RID: 14312
		public static readonly ADPropertyDefinition DisplayName = ADRecipientSchema.DisplayName;

		// Token: 0x040037E9 RID: 14313
		public static readonly ADPropertyDefinition EmailAddresses = ADRecipientSchema.EmailAddresses;

		// Token: 0x040037EA RID: 14314
		public static readonly ADPropertyDefinition ExchangeGuid = ADMailboxRecipientSchema.ExchangeGuid;

		// Token: 0x040037EB RID: 14315
		public static readonly ADPropertyDefinition LegacyExchangeDN = ADRecipientSchema.LegacyExchangeDN;

		// Token: 0x040037EC RID: 14316
		public static readonly ADPropertyDefinition NetID = ADUserSchema.NetID;

		// Token: 0x040037ED RID: 14317
		public static readonly ADPropertyDefinition PrimarySmtpAddress = ADRecipientSchema.PrimarySmtpAddress;

		// Token: 0x040037EE RID: 14318
		public static readonly ADPropertyDefinition ServerName = ADMailboxRecipientSchema.ServerName;

		// Token: 0x040037EF RID: 14319
		public static readonly ADPropertyDefinition WindowsLiveID = ADRecipientSchema.WindowsLiveID;

		// Token: 0x040037F0 RID: 14320
		public static readonly ADPropertyDefinition PrimaryMailboxSource = ADUserSchema.PrimaryMailboxSource;

		// Token: 0x040037F1 RID: 14321
		public static readonly ADPropertyDefinition SatchmoClusterIp = ADUserSchema.SatchmoClusterIp;

		// Token: 0x040037F2 RID: 14322
		public static readonly ADPropertyDefinition SatchmoDGroup = ADUserSchema.SatchmoDGroup;

		// Token: 0x040037F3 RID: 14323
		public static readonly ADPropertyDefinition FblEnabled = ADUserSchema.FblEnabled;

		// Token: 0x040037F4 RID: 14324
		public static readonly ADPropertyDefinition Gender = ADUserSchema.Gender;

		// Token: 0x040037F5 RID: 14325
		public static readonly ADPropertyDefinition Occupation = ADUserSchema.Occupation;

		// Token: 0x040037F6 RID: 14326
		public static readonly ADPropertyDefinition Region = ADUserSchema.Region;

		// Token: 0x040037F7 RID: 14327
		public static readonly ADPropertyDefinition Timezone = ADUserSchema.Timezone;

		// Token: 0x040037F8 RID: 14328
		public static readonly ADPropertyDefinition Birthdate = ADUserSchema.Birthdate;

		// Token: 0x040037F9 RID: 14329
		public static readonly ADPropertyDefinition BirthdayPrecision = ADUserSchema.BirthdayPrecision;

		// Token: 0x040037FA RID: 14330
		public static readonly ADPropertyDefinition NameVersion = ADUserSchema.NameVersion;

		// Token: 0x040037FB RID: 14331
		public static readonly ADPropertyDefinition AlternateSupportEmailAddresses = ADUserSchema.AlternateSupportEmailAddresses;

		// Token: 0x040037FC RID: 14332
		public static readonly ADPropertyDefinition PostalCode = ADUserSchema.PostalCode;

		// Token: 0x040037FD RID: 14333
		public static readonly ADPropertyDefinition OptInUser = ADUserSchema.OptInUser;

		// Token: 0x040037FE RID: 14334
		public static readonly ADPropertyDefinition MigrationDryRun = ADUserSchema.MigrationDryRun;

		// Token: 0x040037FF RID: 14335
		public static readonly ADPropertyDefinition FirstName = ADUserSchema.FirstName;

		// Token: 0x04003800 RID: 14336
		public static readonly ADPropertyDefinition LastName = ADUserSchema.LastName;

		// Token: 0x04003801 RID: 14337
		public static readonly ADPropertyDefinition UsageLocation = ADRecipientSchema.UsageLocation;

		// Token: 0x04003802 RID: 14338
		public static readonly ADPropertyDefinition LocaleID = ADUserSchema.LocaleID;

		// Token: 0x04003803 RID: 14339
		public static readonly ADPropertyDefinition IsPremiumConsumerMailbox = ADUserSchema.IsPremiumConsumerMailbox;

		// Token: 0x04003804 RID: 14340
		public static readonly ADPropertyDefinition IsMigratedConsumerMailbox = ADUserSchema.IsMigratedConsumerMailbox;
	}
}
