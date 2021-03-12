using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000826 RID: 2086
	internal abstract class SyncOrgPerson : SyncRecipient
	{
		// Token: 0x06006775 RID: 26485 RVA: 0x0016D95C File Offset: 0x0016BB5C
		public SyncOrgPerson(SyncDirection syncDirection) : base(syncDirection)
		{
		}

		// Token: 0x17002493 RID: 9363
		// (get) Token: 0x06006776 RID: 26486 RVA: 0x0016D965 File Offset: 0x0016BB65
		// (set) Token: 0x06006777 RID: 26487 RVA: 0x0016D977 File Offset: 0x0016BB77
		public SyncProperty<string> AssistantName
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.AssistantName];
			}
			set
			{
				base[SyncOrgPersonSchema.AssistantName] = value;
			}
		}

		// Token: 0x17002494 RID: 9364
		// (get) Token: 0x06006778 RID: 26488 RVA: 0x0016D985 File Offset: 0x0016BB85
		// (set) Token: 0x06006779 RID: 26489 RVA: 0x0016D997 File Offset: 0x0016BB97
		public SyncProperty<string> C
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.C];
			}
			set
			{
				base[SyncOrgPersonSchema.C] = value;
			}
		}

		// Token: 0x17002495 RID: 9365
		// (get) Token: 0x0600677A RID: 26490 RVA: 0x0016D9A5 File Offset: 0x0016BBA5
		// (set) Token: 0x0600677B RID: 26491 RVA: 0x0016D9B7 File Offset: 0x0016BBB7
		public SyncProperty<string> City
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.City];
			}
			set
			{
				base[SyncOrgPersonSchema.City] = value;
			}
		}

		// Token: 0x17002496 RID: 9366
		// (get) Token: 0x0600677C RID: 26492 RVA: 0x0016D9C5 File Offset: 0x0016BBC5
		// (set) Token: 0x0600677D RID: 26493 RVA: 0x0016D9D7 File Offset: 0x0016BBD7
		public SyncProperty<string> Co
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.Co];
			}
			set
			{
				base[SyncOrgPersonSchema.Co] = value;
			}
		}

		// Token: 0x17002497 RID: 9367
		// (get) Token: 0x0600677E RID: 26494 RVA: 0x0016D9E5 File Offset: 0x0016BBE5
		// (set) Token: 0x0600677F RID: 26495 RVA: 0x0016D9F7 File Offset: 0x0016BBF7
		public SyncProperty<string> Company
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.Company];
			}
			set
			{
				base[SyncOrgPersonSchema.Company] = value;
			}
		}

		// Token: 0x17002498 RID: 9368
		// (get) Token: 0x06006780 RID: 26496 RVA: 0x0016DA05 File Offset: 0x0016BC05
		// (set) Token: 0x06006781 RID: 26497 RVA: 0x0016DA17 File Offset: 0x0016BC17
		public SyncProperty<int> CountryCode
		{
			get
			{
				return (SyncProperty<int>)base[SyncOrgPersonSchema.CountryCode];
			}
			set
			{
				base[SyncOrgPersonSchema.CountryCode] = value;
			}
		}

		// Token: 0x17002499 RID: 9369
		// (get) Token: 0x06006782 RID: 26498 RVA: 0x0016DA25 File Offset: 0x0016BC25
		// (set) Token: 0x06006783 RID: 26499 RVA: 0x0016DA37 File Offset: 0x0016BC37
		public SyncProperty<string> Department
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.Department];
			}
			set
			{
				base[SyncOrgPersonSchema.Department] = value;
			}
		}

		// Token: 0x1700249A RID: 9370
		// (get) Token: 0x06006784 RID: 26500 RVA: 0x0016DA45 File Offset: 0x0016BC45
		// (set) Token: 0x06006785 RID: 26501 RVA: 0x0016DA57 File Offset: 0x0016BC57
		public SyncProperty<string> Fax
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.Fax];
			}
			set
			{
				base[SyncOrgPersonSchema.Fax] = value;
			}
		}

		// Token: 0x1700249B RID: 9371
		// (get) Token: 0x06006786 RID: 26502 RVA: 0x0016DA65 File Offset: 0x0016BC65
		// (set) Token: 0x06006787 RID: 26503 RVA: 0x0016DA77 File Offset: 0x0016BC77
		public SyncProperty<string> FirstName
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.FirstName];
			}
			set
			{
				base[SyncOrgPersonSchema.FirstName] = value;
			}
		}

		// Token: 0x1700249C RID: 9372
		// (get) Token: 0x06006788 RID: 26504 RVA: 0x0016DA85 File Offset: 0x0016BC85
		// (set) Token: 0x06006789 RID: 26505 RVA: 0x0016DA97 File Offset: 0x0016BC97
		public SyncProperty<string> HomePhone
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.HomePhone];
			}
			set
			{
				base[SyncOrgPersonSchema.HomePhone] = value;
			}
		}

		// Token: 0x1700249D RID: 9373
		// (get) Token: 0x0600678A RID: 26506 RVA: 0x0016DAA5 File Offset: 0x0016BCA5
		// (set) Token: 0x0600678B RID: 26507 RVA: 0x0016DAB7 File Offset: 0x0016BCB7
		public SyncProperty<string> Initials
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.Initials];
			}
			set
			{
				base[SyncOrgPersonSchema.Initials] = value;
			}
		}

		// Token: 0x1700249E RID: 9374
		// (get) Token: 0x0600678C RID: 26508 RVA: 0x0016DAC5 File Offset: 0x0016BCC5
		// (set) Token: 0x0600678D RID: 26509 RVA: 0x0016DAD7 File Offset: 0x0016BCD7
		public SyncProperty<string> LastName
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.LastName];
			}
			set
			{
				base[SyncOrgPersonSchema.LastName] = value;
			}
		}

		// Token: 0x1700249F RID: 9375
		// (get) Token: 0x0600678E RID: 26510 RVA: 0x0016DAE5 File Offset: 0x0016BCE5
		// (set) Token: 0x0600678F RID: 26511 RVA: 0x0016DAF7 File Offset: 0x0016BCF7
		public SyncProperty<string> MobilePhone
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.MobilePhone];
			}
			set
			{
				base[SyncOrgPersonSchema.MobilePhone] = value;
			}
		}

		// Token: 0x170024A0 RID: 9376
		// (get) Token: 0x06006790 RID: 26512 RVA: 0x0016DB05 File Offset: 0x0016BD05
		// (set) Token: 0x06006791 RID: 26513 RVA: 0x0016DB17 File Offset: 0x0016BD17
		public SyncProperty<string> Notes
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.Notes];
			}
			set
			{
				base[SyncOrgPersonSchema.Notes] = value;
			}
		}

		// Token: 0x170024A1 RID: 9377
		// (get) Token: 0x06006792 RID: 26514 RVA: 0x0016DB25 File Offset: 0x0016BD25
		// (set) Token: 0x06006793 RID: 26515 RVA: 0x0016DB37 File Offset: 0x0016BD37
		public SyncProperty<string> Office
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.Office];
			}
			set
			{
				base[SyncOrgPersonSchema.Office] = value;
			}
		}

		// Token: 0x170024A2 RID: 9378
		// (get) Token: 0x06006794 RID: 26516 RVA: 0x0016DB45 File Offset: 0x0016BD45
		// (set) Token: 0x06006795 RID: 26517 RVA: 0x0016DB57 File Offset: 0x0016BD57
		public SyncProperty<string> OtherHomePhone
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.OtherHomePhone];
			}
			set
			{
				base[SyncOrgPersonSchema.OtherHomePhone] = value;
			}
		}

		// Token: 0x170024A3 RID: 9379
		// (get) Token: 0x06006796 RID: 26518 RVA: 0x0016DB65 File Offset: 0x0016BD65
		// (set) Token: 0x06006797 RID: 26519 RVA: 0x0016DB77 File Offset: 0x0016BD77
		public SyncProperty<MultiValuedProperty<string>> OtherTelephone
		{
			get
			{
				return (SyncProperty<MultiValuedProperty<string>>)base[SyncOrgPersonSchema.OtherTelephone];
			}
			set
			{
				base[SyncOrgPersonSchema.OtherTelephone] = value;
			}
		}

		// Token: 0x170024A4 RID: 9380
		// (get) Token: 0x06006798 RID: 26520 RVA: 0x0016DB85 File Offset: 0x0016BD85
		// (set) Token: 0x06006799 RID: 26521 RVA: 0x0016DB97 File Offset: 0x0016BD97
		public SyncProperty<string> Pager
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.Pager];
			}
			set
			{
				base[SyncOrgPersonSchema.Pager] = value;
			}
		}

		// Token: 0x170024A5 RID: 9381
		// (get) Token: 0x0600679A RID: 26522 RVA: 0x0016DBA5 File Offset: 0x0016BDA5
		// (set) Token: 0x0600679B RID: 26523 RVA: 0x0016DBB7 File Offset: 0x0016BDB7
		public SyncProperty<string> Phone
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.Phone];
			}
			set
			{
				base[SyncOrgPersonSchema.Phone] = value;
			}
		}

		// Token: 0x170024A6 RID: 9382
		// (get) Token: 0x0600679C RID: 26524 RVA: 0x0016DBC5 File Offset: 0x0016BDC5
		// (set) Token: 0x0600679D RID: 26525 RVA: 0x0016DBD7 File Offset: 0x0016BDD7
		public SyncProperty<string> PostalCode
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.PostalCode];
			}
			set
			{
				base[SyncOrgPersonSchema.PostalCode] = value;
			}
		}

		// Token: 0x170024A7 RID: 9383
		// (get) Token: 0x0600679E RID: 26526 RVA: 0x0016DBE5 File Offset: 0x0016BDE5
		// (set) Token: 0x0600679F RID: 26527 RVA: 0x0016DBF7 File Offset: 0x0016BDF7
		public SyncProperty<string> StateOrProvince
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.StateOrProvince];
			}
			set
			{
				base[SyncOrgPersonSchema.StateOrProvince] = value;
			}
		}

		// Token: 0x170024A8 RID: 9384
		// (get) Token: 0x060067A0 RID: 26528 RVA: 0x0016DC05 File Offset: 0x0016BE05
		// (set) Token: 0x060067A1 RID: 26529 RVA: 0x0016DC17 File Offset: 0x0016BE17
		public SyncProperty<string> StreetAddress
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.StreetAddress];
			}
			set
			{
				base[SyncOrgPersonSchema.StreetAddress] = value;
			}
		}

		// Token: 0x170024A9 RID: 9385
		// (get) Token: 0x060067A2 RID: 26530 RVA: 0x0016DC25 File Offset: 0x0016BE25
		// (set) Token: 0x060067A3 RID: 26531 RVA: 0x0016DC37 File Offset: 0x0016BE37
		public SyncProperty<string> TelephoneAssistant
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.TelephoneAssistant];
			}
			set
			{
				base[SyncOrgPersonSchema.TelephoneAssistant] = value;
			}
		}

		// Token: 0x170024AA RID: 9386
		// (get) Token: 0x060067A4 RID: 26532 RVA: 0x0016DC45 File Offset: 0x0016BE45
		// (set) Token: 0x060067A5 RID: 26533 RVA: 0x0016DC57 File Offset: 0x0016BE57
		public SyncProperty<string> Title
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.Title];
			}
			set
			{
				base[SyncOrgPersonSchema.Title] = value;
			}
		}

		// Token: 0x170024AB RID: 9387
		// (get) Token: 0x060067A6 RID: 26534 RVA: 0x0016DC65 File Offset: 0x0016BE65
		// (set) Token: 0x060067A7 RID: 26535 RVA: 0x0016DC77 File Offset: 0x0016BE77
		public SyncProperty<string> WebPage
		{
			get
			{
				return (SyncProperty<string>)base[SyncOrgPersonSchema.WebPage];
			}
			set
			{
				base[SyncOrgPersonSchema.WebPage] = value;
			}
		}

		// Token: 0x170024AC RID: 9388
		// (get) Token: 0x060067A8 RID: 26536 RVA: 0x0016DC88 File Offset: 0x0016BE88
		protected override SyncPropertyDefinition[] MinimumForwardSyncProperties
		{
			get
			{
				List<SyncPropertyDefinition> list = base.MinimumForwardSyncProperties.ToList<SyncPropertyDefinition>();
				list.AddRange(new SyncPropertyDefinition[]
				{
					SyncRecipientSchema.ExternalEmailAddress
				});
				return list.ToArray();
			}
		}
	}
}
