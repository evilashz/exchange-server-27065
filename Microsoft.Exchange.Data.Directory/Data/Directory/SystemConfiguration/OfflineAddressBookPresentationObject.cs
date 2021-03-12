using System;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200051D RID: 1309
	[Serializable]
	public sealed class OfflineAddressBookPresentationObject : ADPresentationObject
	{
		// Token: 0x060039E5 RID: 14821 RVA: 0x000E00EE File Offset: 0x000DE2EE
		internal OfflineAddressBookPresentationObject(OfflineAddressBook backingOab) : base(backingOab)
		{
		}

		// Token: 0x060039E6 RID: 14822 RVA: 0x000E00F7 File Offset: 0x000DE2F7
		public OfflineAddressBookPresentationObject()
		{
		}

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x060039E7 RID: 14823 RVA: 0x000E00FF File Offset: 0x000DE2FF
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return OfflineAddressBookPresentationObject.SchemaInstance;
			}
		}

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x060039E8 RID: 14824 RVA: 0x000E0106 File Offset: 0x000DE306
		public ADObjectId Server
		{
			get
			{
				if (this.IsE15OrLater())
				{
					return null;
				}
				return (ADObjectId)this[OfflineAddressBookPresentationSchema.Server];
			}
		}

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x060039E9 RID: 14825 RVA: 0x000E0122 File Offset: 0x000DE322
		public ADObjectId GeneratingMailbox
		{
			get
			{
				return (ADObjectId)this[OfflineAddressBookPresentationSchema.GeneratingMailbox];
			}
		}

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x060039EA RID: 14826 RVA: 0x000E0134 File Offset: 0x000DE334
		public MultiValuedProperty<ADObjectId> AddressLists
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[OfflineAddressBookPresentationSchema.AddressLists];
			}
		}

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x060039EB RID: 14827 RVA: 0x000E0146 File Offset: 0x000DE346
		public MultiValuedProperty<OfflineAddressBookVersion> Versions
		{
			get
			{
				return (MultiValuedProperty<OfflineAddressBookVersion>)this[OfflineAddressBookPresentationSchema.Versions];
			}
		}

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x060039EC RID: 14828 RVA: 0x000E0158 File Offset: 0x000DE358
		public bool IsDefault
		{
			get
			{
				return (bool)this[OfflineAddressBookPresentationSchema.IsDefault];
			}
		}

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x060039ED RID: 14829 RVA: 0x000E016A File Offset: 0x000DE36A
		public ADObjectId PublicFolderDatabase
		{
			get
			{
				return (ADObjectId)this[OfflineAddressBookPresentationSchema.PublicFolderDatabase];
			}
		}

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x060039EE RID: 14830 RVA: 0x000E017C File Offset: 0x000DE37C
		public bool PublicFolderDistributionEnabled
		{
			get
			{
				return (bool)this[OfflineAddressBookPresentationSchema.PublicFolderDistributionEnabled];
			}
		}

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x060039EF RID: 14831 RVA: 0x000E018E File Offset: 0x000DE38E
		public bool GlobalWebDistributionEnabled
		{
			get
			{
				return (bool)this[OfflineAddressBookPresentationSchema.GlobalWebDistributionEnabled];
			}
		}

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x060039F0 RID: 14832 RVA: 0x000E01A0 File Offset: 0x000DE3A0
		public bool WebDistributionEnabled
		{
			get
			{
				return (bool)this[OfflineAddressBookPresentationSchema.WebDistributionEnabled];
			}
		}

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x060039F1 RID: 14833 RVA: 0x000E01B2 File Offset: 0x000DE3B2
		public bool ShadowMailboxDistributionEnabled
		{
			get
			{
				return (bool)this[OfflineAddressBookPresentationSchema.ShadowMailboxDistributionEnabled];
			}
		}

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x060039F2 RID: 14834 RVA: 0x000E01C4 File Offset: 0x000DE3C4
		public DateTime? LastTouchedTime
		{
			get
			{
				return (DateTime?)this[OfflineAddressBookPresentationSchema.LastTouchedTime];
			}
		}

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x060039F3 RID: 14835 RVA: 0x000E01D6 File Offset: 0x000DE3D6
		public DateTime? LastRequestedTime
		{
			get
			{
				return (DateTime?)this[OfflineAddressBookPresentationSchema.LastRequestedTime];
			}
		}

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x060039F4 RID: 14836 RVA: 0x000E01E8 File Offset: 0x000DE3E8
		public DateTime? LastFailedTime
		{
			get
			{
				return (DateTime?)this[OfflineAddressBookPresentationSchema.LastFailedTime];
			}
		}

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x060039F5 RID: 14837 RVA: 0x000E01FA File Offset: 0x000DE3FA
		public int? LastNumberOfRecords
		{
			get
			{
				return (int?)this[OfflineAddressBookPresentationSchema.LastNumberOfRecords];
			}
		}

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x060039F6 RID: 14838 RVA: 0x000E020C File Offset: 0x000DE40C
		public OfflineAddressBookLastGeneratingData LastGeneratingData
		{
			get
			{
				return (OfflineAddressBookLastGeneratingData)this[OfflineAddressBookPresentationSchema.LastGeneratingData];
			}
		}

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x060039F7 RID: 14839 RVA: 0x000E021E File Offset: 0x000DE41E
		public int MaxBinaryPropertySize
		{
			get
			{
				return (int)this[OfflineAddressBookPresentationSchema.MaxBinaryPropertySize];
			}
		}

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x060039F8 RID: 14840 RVA: 0x000E0230 File Offset: 0x000DE430
		public int MaxMultivaluedBinaryPropertySize
		{
			get
			{
				return (int)this[OfflineAddressBookPresentationSchema.MaxMultivaluedBinaryPropertySize];
			}
		}

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x060039F9 RID: 14841 RVA: 0x000E0242 File Offset: 0x000DE442
		public int MaxStringPropertySize
		{
			get
			{
				return (int)this[OfflineAddressBookPresentationSchema.MaxStringPropertySize];
			}
		}

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x060039FA RID: 14842 RVA: 0x000E0254 File Offset: 0x000DE454
		public int MaxMultivaluedStringPropertySize
		{
			get
			{
				return (int)this[OfflineAddressBookPresentationSchema.MaxMultivaluedStringPropertySize];
			}
		}

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x060039FB RID: 14843 RVA: 0x000E0266 File Offset: 0x000DE466
		public MultiValuedProperty<OfflineAddressBookMapiProperty> ConfiguredAttributes
		{
			get
			{
				return (MultiValuedProperty<OfflineAddressBookMapiProperty>)this[OfflineAddressBookPresentationSchema.ConfiguredAttributes];
			}
		}

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x060039FC RID: 14844 RVA: 0x000E0278 File Offset: 0x000DE478
		public Unlimited<int>? DiffRetentionPeriod
		{
			get
			{
				return (Unlimited<int>?)this[OfflineAddressBookPresentationSchema.DiffRetentionPeriod];
			}
		}

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x060039FD RID: 14845 RVA: 0x000E028A File Offset: 0x000DE48A
		public Schedule Schedule
		{
			get
			{
				return (Schedule)this[OfflineAddressBookPresentationSchema.Schedule];
			}
		}

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x060039FE RID: 14846 RVA: 0x000E029C File Offset: 0x000DE49C
		public MultiValuedProperty<ADObjectId> VirtualDirectories
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[OfflineAddressBookPresentationSchema.VirtualDirectories];
			}
		}

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x060039FF RID: 14847 RVA: 0x000E02AE File Offset: 0x000DE4AE
		public string AdminDisplayName
		{
			get
			{
				return (string)this[OfflineAddressBookPresentationSchema.AdminDisplayName];
			}
		}

		// Token: 0x06003A00 RID: 14848 RVA: 0x000E02C0 File Offset: 0x000DE4C0
		private bool IsE15OrLater()
		{
			return ((OfflineAddressBook)base.DataObject).IsE15OrLater();
		}

		// Token: 0x0400277E RID: 10110
		private static readonly OfflineAddressBookPresentationSchema SchemaInstance = ObjectSchema.GetInstance<OfflineAddressBookPresentationSchema>();
	}
}
