using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002DB RID: 731
	[Serializable]
	public sealed class ExchangeUpgradeBucket : ADConfigurationObject
	{
		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x0600224C RID: 8780 RVA: 0x0009692B File Offset: 0x00094B2B
		internal override ADObjectId ParentPath
		{
			get
			{
				return ExchangeUpgradeBucket.ContainerPath;
			}
		}

		// Token: 0x17000896 RID: 2198
		// (get) Token: 0x0600224D RID: 8781 RVA: 0x00096932 File Offset: 0x00094B32
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ExchangeUpgradeBucket.MostDerivedClass;
			}
		}

		// Token: 0x17000897 RID: 2199
		// (get) Token: 0x0600224E RID: 8782 RVA: 0x00096939 File Offset: 0x00094B39
		internal override ADObjectSchema Schema
		{
			get
			{
				return ExchangeUpgradeBucket.SchemaInstance;
			}
		}

		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x0600224F RID: 8783 RVA: 0x00096940 File Offset: 0x00094B40
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x00096947 File Offset: 0x00094B47
		internal static object TargetVersionGetterDelegate(IPropertyBag bag)
		{
			return ExchangeUpgradeBucket.GetVersionStringFromInteger((long)bag[ExchangeUpgradeBucketSchema.RawTargetVersion]);
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x0009695E File Offset: 0x00094B5E
		internal static void TargetVersionSetterDelegate(object value, IPropertyBag bag)
		{
			bag[ExchangeUpgradeBucketSchema.RawTargetVersion] = ExchangeUpgradeBucket.GetVersionIntegerFromString((string)value);
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x0009697B File Offset: 0x00094B7B
		internal static object SourceVersionGetterDelegate(IPropertyBag bag)
		{
			return ExchangeUpgradeBucket.GetVersionStringFromInteger((long)bag[ExchangeUpgradeBucketSchema.RawSourceVersion]);
		}

		// Token: 0x06002253 RID: 8787 RVA: 0x00096992 File Offset: 0x00094B92
		internal static void SourceVersionSetterDelegate(object value, IPropertyBag bag)
		{
			bag[ExchangeUpgradeBucketSchema.RawSourceVersion] = ExchangeUpgradeBucket.GetVersionIntegerFromString((string)value);
		}

		// Token: 0x06002254 RID: 8788 RVA: 0x000969B0 File Offset: 0x00094BB0
		private static string GetVersionStringFromInteger(long versionInteger)
		{
			ExchangeBuild exchangeBuild = new ExchangeBuild(versionInteger);
			string text = exchangeBuild.Major.ToString() + ".";
			text += ((exchangeBuild.Minor == byte.MaxValue) ? "*" : (exchangeBuild.Minor.ToString() + "."));
			if (exchangeBuild.Minor != 255)
			{
				text += ((exchangeBuild.Build == ushort.MaxValue) ? "*" : (exchangeBuild.Build.ToString() + "."));
			}
			if (exchangeBuild.Build != 65535)
			{
				text += ((exchangeBuild.BuildRevision == 1023) ? "*" : exchangeBuild.BuildRevision.ToString());
			}
			return text;
		}

		// Token: 0x06002255 RID: 8789 RVA: 0x00096A94 File Offset: 0x00094C94
		private static long GetVersionIntegerFromString(string versionString)
		{
			string[] array = versionString.Split(new char[]
			{
				'.'
			});
			byte b;
			if (!byte.TryParse(array[0], out b) || b == 255)
			{
				throw new ArgumentException(DirectoryStrings.ExchangeUpgradeBucketInvalidVersionFormat(versionString));
			}
			byte maxValue = byte.MaxValue;
			if (array[1] != "*" && (!byte.TryParse(array[1], out maxValue) || maxValue == 255))
			{
				throw new ArgumentException(DirectoryStrings.ExchangeUpgradeBucketInvalidVersionFormat(versionString));
			}
			ushort maxValue2 = ushort.MaxValue;
			ushort num = 1023;
			if (array.Length > 2 && array[2] != "*" && (!ushort.TryParse(array[2], out maxValue2) || maxValue2 == 65535))
			{
				throw new ArgumentException(DirectoryStrings.ExchangeUpgradeBucketInvalidVersionFormat(versionString));
			}
			if (array.Length > 3 && array[3] != "*" && (!ushort.TryParse(array[3], out num) || num >= 1023))
			{
				throw new ArgumentException(DirectoryStrings.ExchangeUpgradeBucketInvalidVersionFormat(versionString));
			}
			ExchangeBuild exchangeBuild = new ExchangeBuild(b, maxValue, maxValue2, num);
			return exchangeBuild.ToInt64();
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002256 RID: 8790 RVA: 0x00096BAC File Offset: 0x00094DAC
		// (set) Token: 0x06002257 RID: 8791 RVA: 0x00096BBE File Offset: 0x00094DBE
		[Parameter(Mandatory = false)]
		public ExchangeUpgradeBucketStatus Status
		{
			get
			{
				return (ExchangeUpgradeBucketStatus)this[ExchangeUpgradeBucketSchema.Status];
			}
			set
			{
				this[ExchangeUpgradeBucketSchema.Status] = value;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06002258 RID: 8792 RVA: 0x00096BD1 File Offset: 0x00094DD1
		// (set) Token: 0x06002259 RID: 8793 RVA: 0x00096BE3 File Offset: 0x00094DE3
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return (bool)this[ExchangeUpgradeBucketSchema.Enabled];
			}
			set
			{
				this[ExchangeUpgradeBucketSchema.Enabled] = value;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x0600225A RID: 8794 RVA: 0x00096BF6 File Offset: 0x00094DF6
		// (set) Token: 0x0600225B RID: 8795 RVA: 0x00096C08 File Offset: 0x00094E08
		[Parameter(Mandatory = false)]
		public DateTime? StartDate
		{
			get
			{
				return (DateTime?)this[ExchangeUpgradeBucketSchema.StartDate];
			}
			set
			{
				this[ExchangeUpgradeBucketSchema.StartDate] = value;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x0600225C RID: 8796 RVA: 0x00096C1B File Offset: 0x00094E1B
		// (set) Token: 0x0600225D RID: 8797 RVA: 0x00096C45 File Offset: 0x00094E45
		[Parameter(Mandatory = false)]
		public Unlimited<int> MaxMailboxes
		{
			get
			{
				if (this[ExchangeUpgradeBucketSchema.MaxMailboxes] != null)
				{
					return (int)this[ExchangeUpgradeBucketSchema.MaxMailboxes];
				}
				return Unlimited<int>.UnlimitedValue;
			}
			set
			{
				this[ExchangeUpgradeBucketSchema.MaxMailboxes] = (value.IsUnlimited ? null : value.Value);
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x0600225E RID: 8798 RVA: 0x00096C6A File Offset: 0x00094E6A
		// (set) Token: 0x0600225F RID: 8799 RVA: 0x00096C7C File Offset: 0x00094E7C
		[ValidateRange(1, 999)]
		[Parameter(Mandatory = false)]
		public int Priority
		{
			get
			{
				return (int)this[ExchangeUpgradeBucketSchema.Priority];
			}
			set
			{
				this[ExchangeUpgradeBucketSchema.Priority] = value;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x00096C8F File Offset: 0x00094E8F
		// (set) Token: 0x06002261 RID: 8801 RVA: 0x00096CA1 File Offset: 0x00094EA1
		[Parameter(Mandatory = false)]
		public string Description
		{
			get
			{
				return (string)this[ExchangeUpgradeBucketSchema.Description];
			}
			set
			{
				this[ExchangeUpgradeBucketSchema.Description] = value;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x00096CAF File Offset: 0x00094EAF
		// (set) Token: 0x06002263 RID: 8803 RVA: 0x00096CC1 File Offset: 0x00094EC1
		public string SourceVersion
		{
			get
			{
				return (string)this[ExchangeUpgradeBucketSchema.SourceVersion];
			}
			set
			{
				this[ExchangeUpgradeBucketSchema.SourceVersion] = value;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06002264 RID: 8804 RVA: 0x00096CCF File Offset: 0x00094ECF
		// (set) Token: 0x06002265 RID: 8805 RVA: 0x00096CE1 File Offset: 0x00094EE1
		public string TargetVersion
		{
			get
			{
				return (string)this[ExchangeUpgradeBucketSchema.TargetVersion];
			}
			set
			{
				this[ExchangeUpgradeBucketSchema.TargetVersion] = value;
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002266 RID: 8806 RVA: 0x00096CEF File Offset: 0x00094EEF
		public MultiValuedProperty<ADObjectId> Organizations
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ExchangeUpgradeBucketSchema.Organizations];
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002267 RID: 8807 RVA: 0x00096D01 File Offset: 0x00094F01
		public int OrganizationCount
		{
			get
			{
				return this.Organizations.Count;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x00096D0E File Offset: 0x00094F0E
		// (set) Token: 0x06002269 RID: 8809 RVA: 0x00096D20 File Offset: 0x00094F20
		public int MailboxCount
		{
			get
			{
				return (int)this[ExchangeUpgradeBucketSchema.MailboxCount];
			}
			internal set
			{
				this[ExchangeUpgradeBucketSchema.MailboxCount] = value;
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x00096D34 File Offset: 0x00094F34
		// (set) Token: 0x0600226B RID: 8811 RVA: 0x00096D80 File Offset: 0x00094F80
		[ValidateCount(0, 4)]
		[Parameter(Mandatory = false)]
		public OrganizationUpgradeStage[] DisabledUpgradeStages
		{
			get
			{
				int num = (int)this[ExchangeUpgradeBucketSchema.DisabledUpgradeStages];
				List<OrganizationUpgradeStage> list = new List<OrganizationUpgradeStage>();
				for (int i = 0; i <= 3; i++)
				{
					int num2 = 1 << i;
					if ((num & num2) > 0)
					{
						list.Add((OrganizationUpgradeStage)i);
					}
				}
				return list.ToArray();
			}
			set
			{
				int num = 0;
				if (value != null)
				{
					for (int i = 0; i < value.Length; i++)
					{
						OrganizationUpgradeStage organizationUpgradeStage = value[i];
						num |= 1 << (int)organizationUpgradeStage;
					}
				}
				this[ExchangeUpgradeBucketSchema.DisabledUpgradeStages] = num;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x00096DC0 File Offset: 0x00094FC0
		// (set) Token: 0x0600226D RID: 8813 RVA: 0x00096DD2 File Offset: 0x00094FD2
		[Parameter(Mandatory = false)]
		public OrganizationUpgradeStageStatus StartUpgradeStatus
		{
			get
			{
				return (OrganizationUpgradeStageStatus)this[ExchangeUpgradeBucketSchema.StartUpgradeStatus];
			}
			set
			{
				this[ExchangeUpgradeBucketSchema.StartUpgradeStatus] = (int)value;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x00096DE5 File Offset: 0x00094FE5
		// (set) Token: 0x0600226F RID: 8815 RVA: 0x00096DF7 File Offset: 0x00094FF7
		[Parameter(Mandatory = false)]
		public OrganizationUpgradeStageStatus UpgradeOrganizationMailboxesStatus
		{
			get
			{
				return (OrganizationUpgradeStageStatus)this[ExchangeUpgradeBucketSchema.UpgradeOrganizationMailboxesStatus];
			}
			set
			{
				this[ExchangeUpgradeBucketSchema.UpgradeOrganizationMailboxesStatus] = (int)value;
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x00096E0A File Offset: 0x0009500A
		// (set) Token: 0x06002271 RID: 8817 RVA: 0x00096E1C File Offset: 0x0009501C
		[Parameter(Mandatory = false)]
		public OrganizationUpgradeStageStatus UpgradeUserMailboxesStatus
		{
			get
			{
				return (OrganizationUpgradeStageStatus)this[ExchangeUpgradeBucketSchema.UpgradeUserMailboxesStatus];
			}
			set
			{
				this[ExchangeUpgradeBucketSchema.UpgradeUserMailboxesStatus] = (int)value;
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x00096E2F File Offset: 0x0009502F
		// (set) Token: 0x06002273 RID: 8819 RVA: 0x00096E41 File Offset: 0x00095041
		[Parameter(Mandatory = false)]
		public OrganizationUpgradeStageStatus CompleteUpgradeStatus
		{
			get
			{
				return (OrganizationUpgradeStageStatus)this[ExchangeUpgradeBucketSchema.CompleteUpgradeStatus];
			}
			set
			{
				this[ExchangeUpgradeBucketSchema.CompleteUpgradeStatus] = (int)value;
			}
		}

		// Token: 0x04001572 RID: 5490
		private static readonly ADObjectId ContainerPath = new ADObjectId("CN=UpgradeOrchestration,CN=Global Settings");

		// Token: 0x04001573 RID: 5491
		private static readonly string MostDerivedClass = "msExchOrganizationUpgradePolicy";

		// Token: 0x04001574 RID: 5492
		private static readonly ExchangeUpgradeBucketSchema SchemaInstance = ObjectSchema.GetInstance<ExchangeUpgradeBucketSchema>();
	}
}
