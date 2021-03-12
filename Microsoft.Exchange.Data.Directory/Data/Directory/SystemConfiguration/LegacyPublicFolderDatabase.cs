using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200049E RID: 1182
	[Serializable]
	public sealed class LegacyPublicFolderDatabase : LegacyDatabase
	{
		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x060035EB RID: 13803 RVA: 0x000D421C File Offset: 0x000D241C
		internal override ADObjectSchema Schema
		{
			get
			{
				return LegacyPublicFolderDatabase.schema;
			}
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x060035EC RID: 13804 RVA: 0x000D4223 File Offset: 0x000D2423
		internal override string MostDerivedObjectClass
		{
			get
			{
				return LegacyPublicFolderDatabase.MostDerivedClass;
			}
		}

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x060035ED RID: 13805 RVA: 0x000D422A File Offset: 0x000D242A
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.ObjectCategory, this.MostDerivedObjectClass);
			}
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x000D4240 File Offset: 0x000D2440
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			errors.AddRange(LegacyDatabase.ValidateAscendingQuotas(this.propertyBag, new ProviderPropertyDefinition[]
			{
				LegacyDatabaseSchema.IssueWarningQuota,
				LegacyPublicFolderDatabaseSchema.ProhibitPostQuota
			}, this.Identity));
			if (!this.UseCustomReferralServerList && this.CustomReferralServerList.Count != 0)
			{
				this.CustomReferralServerList.Clear();
			}
			foreach (ServerCostPair serverCostPair in this.CustomReferralServerList)
			{
				if (string.IsNullOrEmpty(serverCostPair.ServerName))
				{
					errors.Add(new ObjectValidationError(DirectoryStrings.PublicFolderReferralServerNotExisting(serverCostPair.ServerGuid.ToString()), this.Identity, string.Empty));
				}
			}
			if (this.CustomReferralServerList.Count > 1)
			{
				for (int i = 0; i < this.CustomReferralServerList.Count - 1; i++)
				{
					for (int j = i + 1; j < this.CustomReferralServerList.Count; j++)
					{
						if (this.CustomReferralServerList[i].ServerGuid == this.CustomReferralServerList[j].ServerGuid && this.CustomReferralServerList[i].Cost != this.CustomReferralServerList[j].Cost)
						{
							errors.Add(new ObjectValidationError(DirectoryStrings.ErrorPublicFolderReferralConflict(this.CustomReferralServerList[i].ToString(), this.CustomReferralServerList[j].ToString()), this.Identity, string.Empty));
							break;
						}
					}
				}
			}
		}

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x060035EF RID: 13807 RVA: 0x000D43FC File Offset: 0x000D25FC
		// (set) Token: 0x060035F0 RID: 13808 RVA: 0x000D440E File Offset: 0x000D260E
		public string Alias
		{
			get
			{
				return (string)this[LegacyPublicFolderDatabaseSchema.Alias];
			}
			internal set
			{
				this[LegacyPublicFolderDatabaseSchema.Alias] = value;
			}
		}

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x060035F1 RID: 13809 RVA: 0x000D441C File Offset: 0x000D261C
		// (set) Token: 0x060035F2 RID: 13810 RVA: 0x000D442E File Offset: 0x000D262E
		public bool FirstInstance
		{
			get
			{
				return (bool)this[LegacyPublicFolderDatabaseSchema.FirstInstance];
			}
			internal set
			{
				this[LegacyPublicFolderDatabaseSchema.FirstInstance] = value;
			}
		}

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x060035F3 RID: 13811 RVA: 0x000D4441 File Offset: 0x000D2641
		// (set) Token: 0x060035F4 RID: 13812 RVA: 0x000D4453 File Offset: 0x000D2653
		internal ADObjectId HomeMta
		{
			get
			{
				return (ADObjectId)this[LegacyPublicFolderDatabaseSchema.HomeMta];
			}
			set
			{
				this[LegacyPublicFolderDatabaseSchema.HomeMta] = value;
			}
		}

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x060035F5 RID: 13813 RVA: 0x000D4461 File Offset: 0x000D2661
		// (set) Token: 0x060035F6 RID: 13814 RVA: 0x000D4473 File Offset: 0x000D2673
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> MaxItemSize
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[LegacyPublicFolderDatabaseSchema.MaxItemSize];
			}
			set
			{
				this[LegacyPublicFolderDatabaseSchema.MaxItemSize] = value;
			}
		}

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x060035F7 RID: 13815 RVA: 0x000D4486 File Offset: 0x000D2686
		// (set) Token: 0x060035F8 RID: 13816 RVA: 0x000D4498 File Offset: 0x000D2698
		[Parameter(Mandatory = false)]
		public Unlimited<EnhancedTimeSpan> ItemRetentionPeriod
		{
			get
			{
				return (Unlimited<EnhancedTimeSpan>)this[LegacyPublicFolderDatabaseSchema.ItemRetentionPeriod];
			}
			set
			{
				this[LegacyPublicFolderDatabaseSchema.ItemRetentionPeriod] = value;
			}
		}

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x060035F9 RID: 13817 RVA: 0x000D44AB File Offset: 0x000D26AB
		// (set) Token: 0x060035FA RID: 13818 RVA: 0x000D44BD File Offset: 0x000D26BD
		[Parameter(Mandatory = false)]
		public uint ReplicationPeriod
		{
			get
			{
				return (uint)this[LegacyPublicFolderDatabaseSchema.ReplicationPeriod];
			}
			set
			{
				this[LegacyPublicFolderDatabaseSchema.ReplicationPeriod] = value;
			}
		}

		// Token: 0x17001057 RID: 4183
		// (get) Token: 0x060035FB RID: 13819 RVA: 0x000D44D0 File Offset: 0x000D26D0
		// (set) Token: 0x060035FC RID: 13820 RVA: 0x000D44E2 File Offset: 0x000D26E2
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> ProhibitPostQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[LegacyPublicFolderDatabaseSchema.ProhibitPostQuota];
			}
			set
			{
				this[LegacyPublicFolderDatabaseSchema.ProhibitPostQuota] = value;
			}
		}

		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x060035FD RID: 13821 RVA: 0x000D44F5 File Offset: 0x000D26F5
		// (set) Token: 0x060035FE RID: 13822 RVA: 0x000D4507 File Offset: 0x000D2707
		public ADObjectId PublicFolderHierarchy
		{
			get
			{
				return (ADObjectId)this[LegacyPublicFolderDatabaseSchema.PublicFolderHierarchy];
			}
			internal set
			{
				this[LegacyPublicFolderDatabaseSchema.PublicFolderHierarchy] = value;
			}
		}

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x060035FF RID: 13823 RVA: 0x000D4515 File Offset: 0x000D2715
		// (set) Token: 0x06003600 RID: 13824 RVA: 0x000D4527 File Offset: 0x000D2727
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize ReplicationMessageSize
		{
			get
			{
				return (ByteQuantifiedSize)this[LegacyPublicFolderDatabaseSchema.ReplicationMessageSize];
			}
			set
			{
				this[LegacyPublicFolderDatabaseSchema.ReplicationMessageSize] = value;
			}
		}

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x06003601 RID: 13825 RVA: 0x000D453A File Offset: 0x000D273A
		internal ScheduleMode ReplicationMode
		{
			get
			{
				return (ScheduleMode)this[LegacyPublicFolderDatabaseSchema.ReplicationMode];
			}
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x06003602 RID: 13826 RVA: 0x000D454C File Offset: 0x000D274C
		// (set) Token: 0x06003603 RID: 13827 RVA: 0x000D455E File Offset: 0x000D275E
		[Parameter(Mandatory = false)]
		public Schedule ReplicationSchedule
		{
			get
			{
				return (Schedule)this[LegacyPublicFolderDatabaseSchema.ReplicationSchedule];
			}
			set
			{
				this[LegacyPublicFolderDatabaseSchema.ReplicationSchedule] = value;
			}
		}

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x06003604 RID: 13828 RVA: 0x000D456C File Offset: 0x000D276C
		// (set) Token: 0x06003605 RID: 13829 RVA: 0x000D457E File Offset: 0x000D277E
		public bool UseCustomReferralServerList
		{
			get
			{
				return (bool)this[LegacyPublicFolderDatabaseSchema.UseCustomReferralServerList];
			}
			set
			{
				this[LegacyPublicFolderDatabaseSchema.UseCustomReferralServerList] = value;
			}
		}

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x06003606 RID: 13830 RVA: 0x000D4591 File Offset: 0x000D2791
		// (set) Token: 0x06003607 RID: 13831 RVA: 0x000D45A3 File Offset: 0x000D27A3
		public MultiValuedProperty<ServerCostPair> CustomReferralServerList
		{
			get
			{
				return (MultiValuedProperty<ServerCostPair>)this[LegacyPublicFolderDatabaseSchema.CustomReferralServerList];
			}
			set
			{
				this[LegacyPublicFolderDatabaseSchema.CustomReferralServerList] = value;
			}
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000D45B4 File Offset: 0x000D27B4
		internal override void StampPersistableDefaultValues()
		{
			if (!base.IsModified(LegacyPublicFolderDatabaseSchema.MaxItemSize))
			{
				this.MaxItemSize = new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromKB(10240UL));
			}
			if (!base.IsModified(LegacyPublicFolderDatabaseSchema.ProhibitPostQuota))
			{
				this.ProhibitPostQuota = new Unlimited<ByteQuantifiedSize>(ByteQuantifiedSize.FromGB(2UL));
			}
			base.StampPersistableDefaultValues();
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x000D460C File Offset: 0x000D280C
		internal static object ReplicationScheduleGetter(IPropertyBag propertyBag)
		{
			switch ((ScheduleMode)propertyBag[LegacyPublicFolderDatabaseSchema.ReplicationMode])
			{
			case ScheduleMode.Never:
				return Schedule.Never;
			case ScheduleMode.Always:
				return Schedule.Always;
			}
			return propertyBag[LegacyPublicFolderDatabaseSchema.ReplicationScheduleBitmaps];
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x000D4655 File Offset: 0x000D2855
		internal static void ReplicationScheduleSetter(object value, IPropertyBag propertyBag)
		{
			if (value == null)
			{
				value = Schedule.Never;
			}
			propertyBag[LegacyPublicFolderDatabaseSchema.ReplicationMode] = ((Schedule)value).Mode;
			propertyBag[LegacyPublicFolderDatabaseSchema.ReplicationScheduleBitmaps] = value;
		}

		// Token: 0x0400246D RID: 9325
		private static LegacyPublicFolderDatabaseSchema schema = ObjectSchema.GetInstance<LegacyPublicFolderDatabaseSchema>();

		// Token: 0x0400246E RID: 9326
		internal static readonly string MostDerivedClass = "msExchPublicMDB";
	}
}
