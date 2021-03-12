using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200022E RID: 558
	internal enum OWAEnabledFlags
	{
		// Token: 0x04000CCC RID: 3276
		GlobalAddressListEnabledMask = 1,
		// Token: 0x04000CCD RID: 3277
		CalendarEnabledMask,
		// Token: 0x04000CCE RID: 3278
		ContactsEnabledMask = 4,
		// Token: 0x04000CCF RID: 3279
		TasksEnabledMask = 8,
		// Token: 0x04000CD0 RID: 3280
		JournalEnabledMask = 16,
		// Token: 0x04000CD1 RID: 3281
		NotesEnabledMask = 32,
		// Token: 0x04000CD2 RID: 3282
		PublicFoldersEnabledMask = 64,
		// Token: 0x04000CD3 RID: 3283
		OrganizationEnabledMask = 128,
		// Token: 0x04000CD4 RID: 3284
		RemindersAndNotificationsEnabledMask = 256,
		// Token: 0x04000CD5 RID: 3285
		PremiumClientEnabledMask = 512,
		// Token: 0x04000CD6 RID: 3286
		SpellCheckerEnabledMask = 1024,
		// Token: 0x04000CD7 RID: 3287
		SMimeEnabledMask = 2048,
		// Token: 0x04000CD8 RID: 3288
		SearchFoldersEnabledMask = 4096,
		// Token: 0x04000CD9 RID: 3289
		SignaturesEnabledMask = 8192,
		// Token: 0x04000CDA RID: 3290
		RulesEnabledMask = 16384,
		// Token: 0x04000CDB RID: 3291
		ThemeSelectionEnabledMask = 32768,
		// Token: 0x04000CDC RID: 3292
		JunkEmailEnabledMask = 65536,
		// Token: 0x04000CDD RID: 3293
		UMIntegrationEnabledMask = 131072,
		// Token: 0x04000CDE RID: 3294
		WSSAccessOnPublicComputersEnabledMask = 262144,
		// Token: 0x04000CDF RID: 3295
		WSSAccessOnPrivateComputersEnabledMask = 524288,
		// Token: 0x04000CE0 RID: 3296
		UNCAccessOnPublicComputersEnabledMask = 1048576,
		// Token: 0x04000CE1 RID: 3297
		UNCAccessOnPrivateComputersEnabledMask = 2097152,
		// Token: 0x04000CE2 RID: 3298
		ActiveSyncIntegrationEnabledMask = 4194304,
		// Token: 0x04000CE3 RID: 3299
		ExplicitLogonEnabledMask = 8388608,
		// Token: 0x04000CE4 RID: 3300
		AllAddressListsEnabledMask = 16777216,
		// Token: 0x04000CE5 RID: 3301
		RecoverDeletedItemsEnabledMask = 33554432,
		// Token: 0x04000CE6 RID: 3302
		ChangePasswordEnabledMask = 67108864,
		// Token: 0x04000CE7 RID: 3303
		InstantMessagingEnabledMask = 134217728,
		// Token: 0x04000CE8 RID: 3304
		TextMessagingEnabledMask = 268435456,
		// Token: 0x04000CE9 RID: 3305
		OWALightEnabledMask = 536870912,
		// Token: 0x04000CEA RID: 3306
		DelegateAccessEnabledMask = 1073741824,
		// Token: 0x04000CEB RID: 3307
		IRMEnabledMask = -2147483648
	}
}
