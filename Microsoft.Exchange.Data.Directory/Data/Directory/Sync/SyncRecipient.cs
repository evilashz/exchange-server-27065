using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000825 RID: 2085
	internal abstract class SyncRecipient : SyncObject
	{
		// Token: 0x06006704 RID: 26372 RVA: 0x0016CFE8 File Offset: 0x0016B1E8
		public SyncRecipient(SyncDirection syncDirection) : base(syncDirection)
		{
		}

		// Token: 0x1700245D RID: 9309
		// (get) Token: 0x06006705 RID: 26373 RVA: 0x0016CFF1 File Offset: 0x0016B1F1
		// (set) Token: 0x06006706 RID: 26374 RVA: 0x0016D003 File Offset: 0x0016B203
		public SyncProperty<MultiValuedProperty<SyncLink>> AcceptMessagesOnlyFrom
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<SyncLink>>)base[SyncRecipientSchema.AcceptMessagesOnlyFrom];
			}
			set
			{
				base[SyncRecipientSchema.AcceptMessagesOnlyFrom] = value;
			}
		}

		// Token: 0x1700245E RID: 9310
		// (get) Token: 0x06006707 RID: 26375 RVA: 0x0016D011 File Offset: 0x0016B211
		// (set) Token: 0x06006708 RID: 26376 RVA: 0x0016D023 File Offset: 0x0016B223
		public SyncProperty<MultiValuedProperty<SyncLink>> AcceptMessagesOnlyFromDLMembers
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<SyncLink>>)base[SyncRecipientSchema.AcceptMessagesOnlyFromDLMembers];
			}
			set
			{
				base[SyncRecipientSchema.AcceptMessagesOnlyFromDLMembers] = value;
			}
		}

		// Token: 0x1700245F RID: 9311
		// (get) Token: 0x06006709 RID: 26377 RVA: 0x0016D031 File Offset: 0x0016B231
		// (set) Token: 0x0600670A RID: 26378 RVA: 0x0016D043 File Offset: 0x0016B243
		public SyncProperty<string> Alias
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.Alias];
			}
			set
			{
				base[SyncRecipientSchema.Alias] = value;
			}
		}

		// Token: 0x17002460 RID: 9312
		// (get) Token: 0x0600670B RID: 26379 RVA: 0x0016D051 File Offset: 0x0016B251
		// (set) Token: 0x0600670C RID: 26380 RVA: 0x0016D063 File Offset: 0x0016B263
		public SyncProperty<MultiValuedProperty<SyncLink>> BypassModerationFrom
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<SyncLink>>)base[SyncRecipientSchema.BypassModerationFrom];
			}
			set
			{
				base[SyncRecipientSchema.BypassModerationFrom] = value;
			}
		}

		// Token: 0x17002461 RID: 9313
		// (get) Token: 0x0600670D RID: 26381 RVA: 0x0016D071 File Offset: 0x0016B271
		// (set) Token: 0x0600670E RID: 26382 RVA: 0x0016D083 File Offset: 0x0016B283
		public SyncProperty<MultiValuedProperty<SyncLink>> BypassModerationFromDLMembers
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<SyncLink>>)base[SyncRecipientSchema.BypassModerationFromDLMembers];
			}
			set
			{
				base[SyncRecipientSchema.BypassModerationFromDLMembers] = value;
			}
		}

		// Token: 0x17002462 RID: 9314
		// (get) Token: 0x0600670F RID: 26383 RVA: 0x0016D091 File Offset: 0x0016B291
		// (set) Token: 0x06006710 RID: 26384 RVA: 0x0016D0A3 File Offset: 0x0016B2A3
		public SyncProperty<string> Cn
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.Cn];
			}
			set
			{
				base[SyncRecipientSchema.Cn] = value;
			}
		}

		// Token: 0x17002463 RID: 9315
		// (get) Token: 0x06006711 RID: 26385 RVA: 0x0016D0B1 File Offset: 0x0016B2B1
		// (set) Token: 0x06006712 RID: 26386 RVA: 0x0016D0B9 File Offset: 0x0016B2B9
		public string UpdatedCnFromMSO { get; set; }

		// Token: 0x17002464 RID: 9316
		// (get) Token: 0x06006713 RID: 26387 RVA: 0x0016D0C2 File Offset: 0x0016B2C2
		// (set) Token: 0x06006714 RID: 26388 RVA: 0x0016D0D4 File Offset: 0x0016B2D4
		public string CloudLegacyExchangeDN
		{
			get
			{
				return (string)base[SyncRecipientSchema.CloudLegacyExchangeDN];
			}
			set
			{
				base[SyncRecipientSchema.CloudLegacyExchangeDN] = value;
			}
		}

		// Token: 0x17002465 RID: 9317
		// (get) Token: 0x06006715 RID: 26389 RVA: 0x0016D0E2 File Offset: 0x0016B2E2
		// (set) Token: 0x06006716 RID: 26390 RVA: 0x0016D0F4 File Offset: 0x0016B2F4
		public SyncProperty<int?> RecipientDisplayType
		{
			get
			{
				return (SyncProperty<int?>)base[SyncRecipientSchema.RecipientDisplayType];
			}
			set
			{
				base[SyncRecipientSchema.RecipientDisplayType] = value;
			}
		}

		// Token: 0x17002466 RID: 9318
		// (get) Token: 0x06006717 RID: 26391 RVA: 0x0016D102 File Offset: 0x0016B302
		// (set) Token: 0x06006718 RID: 26392 RVA: 0x0016D114 File Offset: 0x0016B314
		public SyncProperty<string> CustomAttribute1
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.CustomAttribute1];
			}
			set
			{
				base[SyncRecipientSchema.CustomAttribute1] = value;
			}
		}

		// Token: 0x17002467 RID: 9319
		// (get) Token: 0x06006719 RID: 26393 RVA: 0x0016D122 File Offset: 0x0016B322
		// (set) Token: 0x0600671A RID: 26394 RVA: 0x0016D134 File Offset: 0x0016B334
		public SyncProperty<string> CustomAttribute10
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.CustomAttribute10];
			}
			set
			{
				base[SyncRecipientSchema.CustomAttribute10] = value;
			}
		}

		// Token: 0x17002468 RID: 9320
		// (get) Token: 0x0600671B RID: 26395 RVA: 0x0016D142 File Offset: 0x0016B342
		// (set) Token: 0x0600671C RID: 26396 RVA: 0x0016D154 File Offset: 0x0016B354
		public SyncProperty<string> CustomAttribute11
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.CustomAttribute11];
			}
			set
			{
				base[SyncRecipientSchema.CustomAttribute11] = value;
			}
		}

		// Token: 0x17002469 RID: 9321
		// (get) Token: 0x0600671D RID: 26397 RVA: 0x0016D162 File Offset: 0x0016B362
		// (set) Token: 0x0600671E RID: 26398 RVA: 0x0016D174 File Offset: 0x0016B374
		public SyncProperty<string> CustomAttribute12
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.CustomAttribute12];
			}
			set
			{
				base[SyncRecipientSchema.CustomAttribute12] = value;
			}
		}

		// Token: 0x1700246A RID: 9322
		// (get) Token: 0x0600671F RID: 26399 RVA: 0x0016D182 File Offset: 0x0016B382
		// (set) Token: 0x06006720 RID: 26400 RVA: 0x0016D194 File Offset: 0x0016B394
		public SyncProperty<string> CustomAttribute13
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.CustomAttribute13];
			}
			set
			{
				base[SyncRecipientSchema.CustomAttribute13] = value;
			}
		}

		// Token: 0x1700246B RID: 9323
		// (get) Token: 0x06006721 RID: 26401 RVA: 0x0016D1A2 File Offset: 0x0016B3A2
		// (set) Token: 0x06006722 RID: 26402 RVA: 0x0016D1B4 File Offset: 0x0016B3B4
		public SyncProperty<string> CustomAttribute14
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.CustomAttribute14];
			}
			set
			{
				base[SyncRecipientSchema.CustomAttribute14] = value;
			}
		}

		// Token: 0x1700246C RID: 9324
		// (get) Token: 0x06006723 RID: 26403 RVA: 0x0016D1C2 File Offset: 0x0016B3C2
		// (set) Token: 0x06006724 RID: 26404 RVA: 0x0016D1D4 File Offset: 0x0016B3D4
		public SyncProperty<string> CustomAttribute15
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.CustomAttribute15];
			}
			set
			{
				base[SyncRecipientSchema.CustomAttribute15] = value;
			}
		}

		// Token: 0x1700246D RID: 9325
		// (get) Token: 0x06006725 RID: 26405 RVA: 0x0016D1E2 File Offset: 0x0016B3E2
		// (set) Token: 0x06006726 RID: 26406 RVA: 0x0016D1F4 File Offset: 0x0016B3F4
		public SyncProperty<string> CustomAttribute2
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.CustomAttribute2];
			}
			set
			{
				base[SyncRecipientSchema.CustomAttribute2] = value;
			}
		}

		// Token: 0x1700246E RID: 9326
		// (get) Token: 0x06006727 RID: 26407 RVA: 0x0016D202 File Offset: 0x0016B402
		// (set) Token: 0x06006728 RID: 26408 RVA: 0x0016D214 File Offset: 0x0016B414
		public SyncProperty<string> CustomAttribute3
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.CustomAttribute3];
			}
			set
			{
				base[SyncRecipientSchema.CustomAttribute3] = value;
			}
		}

		// Token: 0x1700246F RID: 9327
		// (get) Token: 0x06006729 RID: 26409 RVA: 0x0016D222 File Offset: 0x0016B422
		// (set) Token: 0x0600672A RID: 26410 RVA: 0x0016D234 File Offset: 0x0016B434
		public SyncProperty<string> CustomAttribute4
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.CustomAttribute4];
			}
			set
			{
				base[SyncRecipientSchema.CustomAttribute4] = value;
			}
		}

		// Token: 0x17002470 RID: 9328
		// (get) Token: 0x0600672B RID: 26411 RVA: 0x0016D242 File Offset: 0x0016B442
		// (set) Token: 0x0600672C RID: 26412 RVA: 0x0016D254 File Offset: 0x0016B454
		public SyncProperty<string> CustomAttribute5
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.CustomAttribute5];
			}
			set
			{
				base[SyncRecipientSchema.CustomAttribute5] = value;
			}
		}

		// Token: 0x17002471 RID: 9329
		// (get) Token: 0x0600672D RID: 26413 RVA: 0x0016D262 File Offset: 0x0016B462
		// (set) Token: 0x0600672E RID: 26414 RVA: 0x0016D274 File Offset: 0x0016B474
		public SyncProperty<string> CustomAttribute6
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.CustomAttribute6];
			}
			set
			{
				base[SyncRecipientSchema.CustomAttribute6] = value;
			}
		}

		// Token: 0x17002472 RID: 9330
		// (get) Token: 0x0600672F RID: 26415 RVA: 0x0016D282 File Offset: 0x0016B482
		// (set) Token: 0x06006730 RID: 26416 RVA: 0x0016D294 File Offset: 0x0016B494
		public SyncProperty<string> CustomAttribute7
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.CustomAttribute7];
			}
			set
			{
				base[SyncRecipientSchema.CustomAttribute7] = value;
			}
		}

		// Token: 0x17002473 RID: 9331
		// (get) Token: 0x06006731 RID: 26417 RVA: 0x0016D2A2 File Offset: 0x0016B4A2
		// (set) Token: 0x06006732 RID: 26418 RVA: 0x0016D2B4 File Offset: 0x0016B4B4
		public SyncProperty<string> CustomAttribute8
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.CustomAttribute8];
			}
			set
			{
				base[SyncRecipientSchema.CustomAttribute8] = value;
			}
		}

		// Token: 0x17002474 RID: 9332
		// (get) Token: 0x06006733 RID: 26419 RVA: 0x0016D2C2 File Offset: 0x0016B4C2
		// (set) Token: 0x06006734 RID: 26420 RVA: 0x0016D2D4 File Offset: 0x0016B4D4
		public SyncProperty<string> CustomAttribute9
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.CustomAttribute9];
			}
			set
			{
				base[SyncRecipientSchema.CustomAttribute9] = value;
			}
		}

		// Token: 0x17002475 RID: 9333
		// (get) Token: 0x06006735 RID: 26421 RVA: 0x0016D2E2 File Offset: 0x0016B4E2
		// (set) Token: 0x06006736 RID: 26422 RVA: 0x0016D2F4 File Offset: 0x0016B4F4
		public SyncProperty<MultiValuedProperty<string>> ExtensionCustomAttribute1
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<string>>)base[SyncRecipientSchema.ExtensionCustomAttribute1];
			}
			set
			{
				base[SyncRecipientSchema.ExtensionCustomAttribute1] = value;
			}
		}

		// Token: 0x17002476 RID: 9334
		// (get) Token: 0x06006737 RID: 26423 RVA: 0x0016D302 File Offset: 0x0016B502
		// (set) Token: 0x06006738 RID: 26424 RVA: 0x0016D314 File Offset: 0x0016B514
		public SyncProperty<MultiValuedProperty<string>> ExtensionCustomAttribute2
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<string>>)base[SyncRecipientSchema.ExtensionCustomAttribute2];
			}
			set
			{
				base[SyncRecipientSchema.ExtensionCustomAttribute2] = value;
			}
		}

		// Token: 0x17002477 RID: 9335
		// (get) Token: 0x06006739 RID: 26425 RVA: 0x0016D322 File Offset: 0x0016B522
		// (set) Token: 0x0600673A RID: 26426 RVA: 0x0016D334 File Offset: 0x0016B534
		public SyncProperty<MultiValuedProperty<string>> ExtensionCustomAttribute3
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<string>>)base[SyncRecipientSchema.ExtensionCustomAttribute3];
			}
			set
			{
				base[SyncRecipientSchema.ExtensionCustomAttribute3] = value;
			}
		}

		// Token: 0x17002478 RID: 9336
		// (get) Token: 0x0600673B RID: 26427 RVA: 0x0016D342 File Offset: 0x0016B542
		// (set) Token: 0x0600673C RID: 26428 RVA: 0x0016D354 File Offset: 0x0016B554
		public SyncProperty<MultiValuedProperty<string>> ExtensionCustomAttribute4
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<string>>)base[SyncRecipientSchema.ExtensionCustomAttribute4];
			}
			set
			{
				base[SyncRecipientSchema.ExtensionCustomAttribute4] = value;
			}
		}

		// Token: 0x17002479 RID: 9337
		// (get) Token: 0x0600673D RID: 26429 RVA: 0x0016D362 File Offset: 0x0016B562
		// (set) Token: 0x0600673E RID: 26430 RVA: 0x0016D374 File Offset: 0x0016B574
		public SyncProperty<MultiValuedProperty<string>> ExtensionCustomAttribute5
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<string>>)base[SyncRecipientSchema.ExtensionCustomAttribute5];
			}
			set
			{
				base[SyncRecipientSchema.ExtensionCustomAttribute5] = value;
			}
		}

		// Token: 0x1700247A RID: 9338
		// (get) Token: 0x0600673F RID: 26431 RVA: 0x0016D382 File Offset: 0x0016B582
		// (set) Token: 0x06006740 RID: 26432 RVA: 0x0016D394 File Offset: 0x0016B594
		public SyncProperty<string> DisplayName
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.DisplayName];
			}
			set
			{
				base[SyncRecipientSchema.DisplayName] = value;
			}
		}

		// Token: 0x1700247B RID: 9339
		// (get) Token: 0x06006741 RID: 26433 RVA: 0x0016D3A2 File Offset: 0x0016B5A2
		// (set) Token: 0x06006742 RID: 26434 RVA: 0x0016D3B4 File Offset: 0x0016B5B4
		public SyncProperty<ProxyAddressCollection> EmailAddresses
		{
			get
			{
				return (SyncProperty<ProxyAddressCollection>)base[SyncRecipientSchema.EmailAddresses];
			}
			set
			{
				base[SyncRecipientSchema.EmailAddresses] = value;
			}
		}

		// Token: 0x1700247C RID: 9340
		// (get) Token: 0x06006743 RID: 26435 RVA: 0x0016D3C2 File Offset: 0x0016B5C2
		// (set) Token: 0x06006744 RID: 26436 RVA: 0x0016D3D4 File Offset: 0x0016B5D4
		public SyncProperty<ProxyAddress> ExternalEmailAddress
		{
			get
			{
				return (SyncProperty<ProxyAddress>)base[SyncRecipientSchema.ExternalEmailAddress];
			}
			set
			{
				base[SyncRecipientSchema.ExternalEmailAddress] = value;
			}
		}

		// Token: 0x1700247D RID: 9341
		// (get) Token: 0x06006745 RID: 26437 RVA: 0x0016D3E2 File Offset: 0x0016B5E2
		// (set) Token: 0x06006746 RID: 26438 RVA: 0x0016D3F4 File Offset: 0x0016B5F4
		public SyncProperty<MultiValuedProperty<SyncLink>> GrantSendOnBehalfTo
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<SyncLink>>)base[SyncRecipientSchema.GrantSendOnBehalfTo];
			}
			set
			{
				base[SyncRecipientSchema.GrantSendOnBehalfTo] = value;
			}
		}

		// Token: 0x1700247E RID: 9342
		// (get) Token: 0x06006747 RID: 26439 RVA: 0x0016D402 File Offset: 0x0016B602
		// (set) Token: 0x06006748 RID: 26440 RVA: 0x0016D414 File Offset: 0x0016B614
		public SyncProperty<bool> HiddenFromAddressListsEnabled
		{
			get
			{
				return (SyncProperty<bool>)base[SyncRecipientSchema.HiddenFromAddressListsEnabled];
			}
			set
			{
				base[SyncRecipientSchema.HiddenFromAddressListsEnabled] = value;
			}
		}

		// Token: 0x1700247F RID: 9343
		// (get) Token: 0x06006749 RID: 26441 RVA: 0x0016D422 File Offset: 0x0016B622
		// (set) Token: 0x0600674A RID: 26442 RVA: 0x0016D434 File Offset: 0x0016B634
		public SyncProperty<bool> IsDirSynced
		{
			get
			{
				return (SyncProperty<bool>)base[SyncRecipientSchema.IsDirSynced];
			}
			set
			{
				base[SyncRecipientSchema.IsDirSynced] = value;
			}
		}

		// Token: 0x17002480 RID: 9344
		// (get) Token: 0x0600674B RID: 26443 RVA: 0x0016D442 File Offset: 0x0016B642
		// (set) Token: 0x0600674C RID: 26444 RVA: 0x0016D454 File Offset: 0x0016B654
		public SyncProperty<MultiValuedProperty<string>> DirSyncAuthorityMetadata
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<string>>)base[SyncRecipientSchema.DirSyncAuthorityMetadata];
			}
			set
			{
				base[SyncRecipientSchema.DirSyncAuthorityMetadata] = value;
			}
		}

		// Token: 0x17002481 RID: 9345
		// (get) Token: 0x0600674D RID: 26445 RVA: 0x0016D462 File Offset: 0x0016B662
		// (set) Token: 0x0600674E RID: 26446 RVA: 0x0016D474 File Offset: 0x0016B674
		public SyncProperty<MultiValuedProperty<string>> MailTipTranslations
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<string>>)base[SyncRecipientSchema.MailTipTranslations];
			}
			set
			{
				base[SyncRecipientSchema.MailTipTranslations] = value;
			}
		}

		// Token: 0x17002482 RID: 9346
		// (get) Token: 0x0600674F RID: 26447 RVA: 0x0016D482 File Offset: 0x0016B682
		// (set) Token: 0x06006750 RID: 26448 RVA: 0x0016D494 File Offset: 0x0016B694
		public SyncProperty<MultiValuedProperty<SyncLink>> ModeratedBy
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<SyncLink>>)base[SyncRecipientSchema.ModeratedBy];
			}
			set
			{
				base[SyncRecipientSchema.ModeratedBy] = value;
			}
		}

		// Token: 0x17002483 RID: 9347
		// (get) Token: 0x06006751 RID: 26449 RVA: 0x0016D4A2 File Offset: 0x0016B6A2
		// (set) Token: 0x06006752 RID: 26450 RVA: 0x0016D4B4 File Offset: 0x0016B6B4
		public SyncProperty<bool> ModerationEnabled
		{
			get
			{
				return (SyncProperty<bool>)base[SyncRecipientSchema.ModerationEnabled];
			}
			set
			{
				base[SyncRecipientSchema.ModerationEnabled] = value;
			}
		}

		// Token: 0x17002484 RID: 9348
		// (get) Token: 0x06006753 RID: 26451 RVA: 0x0016D4C2 File Offset: 0x0016B6C2
		// (set) Token: 0x06006754 RID: 26452 RVA: 0x0016D4D4 File Offset: 0x0016B6D4
		public SyncProperty<int> ModerationFlags
		{
			get
			{
				return (SyncProperty<int>)base[SyncRecipientSchema.ModerationFlags];
			}
			set
			{
				base[SyncRecipientSchema.ModerationFlags] = value;
			}
		}

		// Token: 0x17002485 RID: 9349
		// (get) Token: 0x06006755 RID: 26453 RVA: 0x0016D4E2 File Offset: 0x0016B6E2
		// (set) Token: 0x06006756 RID: 26454 RVA: 0x0016D4F4 File Offset: 0x0016B6F4
		public SyncProperty<string> OnPremisesObjectId
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.OnPremisesObjectId];
			}
			set
			{
				base[SyncRecipientSchema.OnPremisesObjectId] = value;
			}
		}

		// Token: 0x17002486 RID: 9350
		// (get) Token: 0x06006757 RID: 26455 RVA: 0x0016D502 File Offset: 0x0016B702
		// (set) Token: 0x06006758 RID: 26456 RVA: 0x0016D514 File Offset: 0x0016B714
		public SyncProperty<string> PhoneticDisplayName
		{
			get
			{
				return (SyncProperty<string>)base[SyncRecipientSchema.PhoneticDisplayName];
			}
			set
			{
				base[SyncRecipientSchema.PhoneticDisplayName] = value;
			}
		}

		// Token: 0x17002487 RID: 9351
		// (get) Token: 0x06006759 RID: 26457 RVA: 0x0016D522 File Offset: 0x0016B722
		// (set) Token: 0x0600675A RID: 26458 RVA: 0x0016D534 File Offset: 0x0016B734
		public virtual SyncProperty<RecipientTypeDetails> RecipientTypeDetailsValue
		{
			get
			{
				return (SyncProperty<RecipientTypeDetails>)base[SyncRecipientSchema.RecipientTypeDetailsValue];
			}
			set
			{
				base[SyncRecipientSchema.RecipientTypeDetailsValue] = value;
			}
		}

		// Token: 0x17002488 RID: 9352
		// (get) Token: 0x0600675B RID: 26459 RVA: 0x0016D542 File Offset: 0x0016B742
		// (set) Token: 0x0600675C RID: 26460 RVA: 0x0016D554 File Offset: 0x0016B754
		public SyncProperty<MultiValuedProperty<SyncLink>> RejectMessagesFrom
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<SyncLink>>)base[SyncRecipientSchema.RejectMessagesFrom];
			}
			set
			{
				base[SyncRecipientSchema.RejectMessagesFrom] = value;
			}
		}

		// Token: 0x17002489 RID: 9353
		// (get) Token: 0x0600675D RID: 26461 RVA: 0x0016D562 File Offset: 0x0016B762
		// (set) Token: 0x0600675E RID: 26462 RVA: 0x0016D574 File Offset: 0x0016B774
		public SyncProperty<MultiValuedProperty<SyncLink>> RejectMessagesFromDLMembers
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<SyncLink>>)base[SyncRecipientSchema.RejectMessagesFromDLMembers];
			}
			set
			{
				base[SyncRecipientSchema.RejectMessagesFromDLMembers] = value;
			}
		}

		// Token: 0x1700248A RID: 9354
		// (get) Token: 0x0600675F RID: 26463 RVA: 0x0016D582 File Offset: 0x0016B782
		public SyncProperty<bool> BypassNestedModerationEnabled
		{
			get
			{
				return (SyncProperty<bool>)base[SyncRecipientSchema.BypassNestedModerationEnabled];
			}
		}

		// Token: 0x1700248B RID: 9355
		// (get) Token: 0x06006760 RID: 26464 RVA: 0x0016D594 File Offset: 0x0016B794
		public SyncProperty<TransportModerationNotificationFlags> SendModerationNotifications
		{
			get
			{
				return (SyncProperty<TransportModerationNotificationFlags>)base[SyncRecipientSchema.SendModerationNotifications];
			}
		}

		// Token: 0x1700248C RID: 9356
		// (get) Token: 0x06006761 RID: 26465 RVA: 0x0016D5A6 File Offset: 0x0016B7A6
		// (set) Token: 0x06006762 RID: 26466 RVA: 0x0016D5B8 File Offset: 0x0016B7B8
		public SyncProperty<bool> RequireAllSendersAreAuthenticated
		{
			get
			{
				return (SyncProperty<bool>)base[SyncRecipientSchema.RequireAllSendersAreAuthenticated];
			}
			set
			{
				base[SyncRecipientSchema.RequireAllSendersAreAuthenticated] = value;
			}
		}

		// Token: 0x1700248D RID: 9357
		// (get) Token: 0x06006763 RID: 26467 RVA: 0x0016D5C6 File Offset: 0x0016B7C6
		// (set) Token: 0x06006764 RID: 26468 RVA: 0x0016D5D8 File Offset: 0x0016B7D8
		public SyncProperty<int?> SeniorityIndex
		{
			get
			{
				return (SyncProperty<int?>)base[SyncRecipientSchema.SeniorityIndex];
			}
			set
			{
				base[SyncRecipientSchema.SeniorityIndex] = value;
			}
		}

		// Token: 0x1700248E RID: 9358
		// (get) Token: 0x06006765 RID: 26469 RVA: 0x0016D5E6 File Offset: 0x0016B7E6
		// (set) Token: 0x06006766 RID: 26470 RVA: 0x0016D5F8 File Offset: 0x0016B7F8
		public SyncProperty<MultiValuedProperty<byte[]>> UserCertificate
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<byte[]>>)base[SyncRecipientSchema.UserCertificate];
			}
			set
			{
				base[SyncRecipientSchema.UserCertificate] = value;
			}
		}

		// Token: 0x1700248F RID: 9359
		// (get) Token: 0x06006767 RID: 26471 RVA: 0x0016D606 File Offset: 0x0016B806
		// (set) Token: 0x06006768 RID: 26472 RVA: 0x0016D618 File Offset: 0x0016B818
		public SyncProperty<MultiValuedProperty<byte[]>> UserSMimeCertificate
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<byte[]>>)base[SyncRecipientSchema.UserSMimeCertificate];
			}
			set
			{
				base[SyncRecipientSchema.UserSMimeCertificate] = value;
			}
		}

		// Token: 0x17002490 RID: 9360
		// (get) Token: 0x06006769 RID: 26473 RVA: 0x0016D626 File Offset: 0x0016B826
		// (set) Token: 0x0600676A RID: 26474 RVA: 0x0016D638 File Offset: 0x0016B838
		public SyncProperty<ProxyAddressCollection> SipAddresses
		{
			get
			{
				return (SyncProperty<ProxyAddressCollection>)base[SyncRecipientSchema.SipAddresses];
			}
			set
			{
				base[SyncRecipientSchema.SipAddresses] = value;
			}
		}

		// Token: 0x17002491 RID: 9361
		// (get) Token: 0x0600676B RID: 26475 RVA: 0x0016D646 File Offset: 0x0016B846
		// (set) Token: 0x0600676C RID: 26476 RVA: 0x0016D658 File Offset: 0x0016B858
		public SyncProperty<MultiValuedProperty<ValidationErrorValue>> ValidationError
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<ValidationErrorValue>>)base[SyncRecipientSchema.ValidationError];
			}
			set
			{
				base[SyncRecipientSchema.ValidationError] = value;
			}
		}

		// Token: 0x0600676D RID: 26477 RVA: 0x0016D668 File Offset: 0x0016B868
		public SyncRecipient CreateObjectWithMinimumForwardSyncPropertySet()
		{
			SyncRecipient syncRecipient = (SyncRecipient)SyncObject.CreateBlankObjectByClass(this.ObjectClass, SyncDirection.Forward);
			syncRecipient[SyncObjectSchema.ObjectId] = base.ObjectId;
			syncRecipient[SyncObjectSchema.ContextId] = base.ContextId;
			IDictionary<SyncPropertyDefinition, object> changedProperties = base.GetChangedProperties();
			foreach (SyncPropertyDefinition syncPropertyDefinition in this.MinimumForwardSyncProperties)
			{
				object value = null;
				if (changedProperties.TryGetValue(syncPropertyDefinition, out value))
				{
					syncRecipient[syncPropertyDefinition] = value;
				}
			}
			return syncRecipient;
		}

		// Token: 0x0600676E RID: 26478 RVA: 0x0016D6E8 File Offset: 0x0016B8E8
		public virtual SyncRecipient CreatePlaceHolder()
		{
			SyncRecipient syncRecipient = (SyncRecipient)SyncObject.CreateBlankObjectByClass(this.ObjectClass, SyncDirection.Forward);
			syncRecipient[SyncObjectSchema.ObjectId] = base.ObjectId;
			syncRecipient[SyncObjectSchema.ContextId] = base.ContextId;
			if (this.IsDirSynced.HasValue)
			{
				syncRecipient[SyncRecipientSchema.IsDirSynced] = this.IsDirSynced;
			}
			if (this.OnPremisesObjectId.HasValue)
			{
				syncRecipient[SyncRecipientSchema.OnPremisesObjectId] = this.OnPremisesObjectId;
			}
			return syncRecipient;
		}

		// Token: 0x17002492 RID: 9362
		// (get) Token: 0x0600676F RID: 26479 RVA: 0x0016D768 File Offset: 0x0016B968
		protected virtual SyncPropertyDefinition[] MinimumForwardSyncProperties
		{
			get
			{
				return new SyncPropertyDefinition[]
				{
					SyncRecipientSchema.IsDirSynced,
					SyncRecipientSchema.OnPremisesObjectId,
					SyncRecipientSchema.Alias,
					SyncRecipientSchema.EmailAddresses
				};
			}
		}

		// Token: 0x06006770 RID: 26480 RVA: 0x0016D79D File Offset: 0x0016B99D
		internal static void ProxyAddressesSetter(object value, IPropertyBag propertyBag)
		{
			propertyBag[SyncRecipientSchema.EmailAddresses] = value;
		}

		// Token: 0x06006771 RID: 26481 RVA: 0x0016D7AC File Offset: 0x0016B9AC
		internal static ProxyAddressCollection GetEmailAddressesByPrefix(IPropertyBag propertyBag, ProxyAddressPrefix proxyAddressPrefix, SyncPropertyDefinition propertyDefinition)
		{
			List<ProxyAddress> list = new List<ProxyAddress>();
			ADPropertyDefinition propertyDefinition2 = SyncRecipientSchema.EmailAddresses;
			if ((bool)propertyBag[SyncRecipientSchema.UseShadow])
			{
				propertyDefinition2 = SyncRecipientSchema.EmailAddresses.ShadowProperty;
			}
			ProxyAddressCollection proxyAddressCollection = (ProxyAddressCollection)propertyBag[propertyDefinition2];
			foreach (ProxyAddress proxyAddress in proxyAddressCollection)
			{
				if (proxyAddress.Prefix == proxyAddressPrefix)
				{
					list.Add(proxyAddress);
				}
			}
			return new ProxyAddressCollection(false, propertyDefinition, list);
		}

		// Token: 0x06006772 RID: 26482 RVA: 0x0016D848 File Offset: 0x0016BA48
		internal static object SendModerationNotificationsGetter(IPropertyBag propertyBag)
		{
			int moderationFlags = (int)propertyBag[SyncRecipientSchema.ModerationFlags];
			return ADRecipient.GetSendModerationNotificationsFromModerationFlags(moderationFlags);
		}

		// Token: 0x06006773 RID: 26483 RVA: 0x0016D874 File Offset: 0x0016BA74
		internal static DirectoryObjectClass GetRecipientType(PropertyBag propertyBag)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)propertyBag[ADObjectSchema.ObjectClass];
			if (multiValuedProperty.Contains(ADUser.MostDerivedClass))
			{
				return DirectoryObjectClass.User;
			}
			if (multiValuedProperty.Contains(ADGroup.MostDerivedClass))
			{
				return DirectoryObjectClass.Group;
			}
			if (multiValuedProperty.Contains(ADContact.MostDerivedClass))
			{
				return DirectoryObjectClass.Contact;
			}
			throw new InvalidOperationException("Unexpected recipient type");
		}

		// Token: 0x04004443 RID: 17475
		internal static readonly QueryFilter SyncRecipientObjectTypeFilter = new OrFilter(new QueryFilter[]
		{
			ADObject.ObjectClassFilter(ADUser.MostDerivedClass),
			ADObject.ObjectClassFilter(ADGroup.MostDerivedClass),
			ADObject.ObjectClassFilter(ADContact.MostDerivedClass)
		});

		// Token: 0x04004444 RID: 17476
		internal static readonly QueryFilter SyncRecipientObjectTypeFilterOptDisabled = new OrFilter(false, true, new QueryFilter[]
		{
			ADObject.ObjectClassFilter(ADUser.MostDerivedClass),
			ADObject.ObjectClassFilter(ADGroup.MostDerivedClass),
			ADObject.ObjectClassFilter(ADContact.MostDerivedClass)
		});

		// Token: 0x04004445 RID: 17477
		internal static ProxyAddressPrefix SipPrefix = ProxyAddressPrefix.GetPrefix("sip");
	}
}
