using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000022 RID: 34
	internal enum MRSProxyCapabilities
	{
		// Token: 0x0400010E RID: 270
		PagedEnumeration,
		// Token: 0x0400010F RID: 271
		UpdateMovedMailboxReporting,
		// Token: 0x04000110 RID: 272
		Archives,
		// Token: 0x04000111 RID: 273
		ADUserXML,
		// Token: 0x04000112 RID: 274
		Push,
		// Token: 0x04000113 RID: 275
		InvokePreMoveAction,
		// Token: 0x04000114 RID: 276
		ProxyThrottling,
		// Token: 0x04000115 RID: 277
		CachedMailboxSyncState,
		// Token: 0x04000116 RID: 278
		MergeMailbox,
		// Token: 0x04000117 RID: 279
		ArchiveSeparation,
		// Token: 0x04000118 RID: 280
		DataGuarantee,
		// Token: 0x04000119 RID: 281
		PostMoveCleanup,
		// Token: 0x0400011A RID: 282
		AdvancedServerHealthCheck,
		// Token: 0x0400011B RID: 283
		MailboxOptions,
		// Token: 0x0400011C RID: 284
		ForceLogRoll,
		// Token: 0x0400011D RID: 285
		GetFolderRecFlags,
		// Token: 0x0400011E RID: 286
		SimpleExport,
		// Token: 0x0400011F RID: 287
		R5DC,
		// Token: 0x04000120 RID: 288
		NonMrsLogon,
		// Token: 0x04000121 RID: 289
		TenantHint = 24,
		// Token: 0x04000122 RID: 290
		PublicFolderMigration,
		// Token: 0x04000123 RID: 291
		ReleaseInfo,
		// Token: 0x04000124 RID: 292
		PublicFolderMove,
		// Token: 0x04000125 RID: 293
		LegacyResourceReservation,
		// Token: 0x04000126 RID: 294
		CopyToWithFlags = 30,
		// Token: 0x04000127 RID: 295
		SessionStatistics,
		// Token: 0x04000128 RID: 296
		InMailboxExternalRules,
		// Token: 0x04000129 RID: 297
		MailboxTableInfoFlags,
		// Token: 0x0400012A RID: 298
		ConfigRestore,
		// Token: 0x0400012B RID: 299
		DiscoverWellKnownFolders,
		// Token: 0x0400012C RID: 300
		MailboxReleaseCheck,
		// Token: 0x0400012D RID: 301
		ResourceReservation,
		// Token: 0x0400012E RID: 302
		IcsMidSetCheck,
		// Token: 0x0400012F RID: 303
		Pst,
		// Token: 0x04000130 RID: 304
		SetMessageProps,
		// Token: 0x04000131 RID: 305
		ConfigPst,
		// Token: 0x04000132 RID: 306
		ProxyConfiguration,
		// Token: 0x04000133 RID: 307
		MailboxCapabilities,
		// Token: 0x04000134 RID: 308
		Eas,
		// Token: 0x04000135 RID: 309
		FindServerByDatabaseOrMailbox,
		// Token: 0x04000136 RID: 310
		ContainerOperations,
		// Token: 0x04000137 RID: 311
		MailboxCapabilities2,
		// Token: 0x04000138 RID: 312
		ConfigPreferredADConnection,
		// Token: 0x04000139 RID: 313
		CanStoreCreatePFDumpster,
		// Token: 0x0400013A RID: 314
		CreateFolder3,
		// Token: 0x0400013B RID: 315
		ExtendedAclInformation,
		// Token: 0x0400013C RID: 316
		GetMailboxContainerMailboxes,
		// Token: 0x0400013D RID: 317
		ConfigEas2,
		// Token: 0x0400013E RID: 318
		ExportFolders,
		// Token: 0x0400013F RID: 319
		ConfigOlc,
		// Token: 0x04000140 RID: 320
		SetItemProperties,
		// Token: 0x04000141 RID: 321
		RemotePstExport,
		// Token: 0x04000142 RID: 322
		InternalAccessFolderCreation,
		// Token: 0x04000143 RID: 323
		SetMailboxSettings,
		// Token: 0x04000144 RID: 324
		MailboxRulesAclsCapabilities,
		// Token: 0x04000145 RID: 325
		PublicFolderMailboxesMigrationSupport = 82,
		// Token: 0x04000146 RID: 326
		MaxElement
	}
}
