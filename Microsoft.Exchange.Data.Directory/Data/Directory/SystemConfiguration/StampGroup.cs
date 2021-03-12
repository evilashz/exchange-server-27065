using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020003EB RID: 1003
	[Serializable]
	public class StampGroup : ADConfigurationObject
	{
		// Token: 0x17000CF3 RID: 3315
		// (get) Token: 0x06002E16 RID: 11798 RVA: 0x000BBC45 File Offset: 0x000B9E45
		internal override ADObjectSchema Schema
		{
			get
			{
				if (this.schema == null)
				{
					this.schema = ObjectSchema.GetInstance<StampGroupSchema>();
				}
				return this.schema;
			}
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x000BBC60 File Offset: 0x000B9E60
		internal override string MostDerivedObjectClass
		{
			get
			{
				return StampGroup.mostDerivedClass;
			}
		}

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x06002E18 RID: 11800 RVA: 0x000BBC67 File Offset: 0x000B9E67
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x000BBC6E File Offset: 0x000B9E6E
		// (set) Token: 0x06002E1A RID: 11802 RVA: 0x000BBC80 File Offset: 0x000B9E80
		public new string Name
		{
			get
			{
				return (string)this[StampGroupSchema.Name];
			}
			internal set
			{
				this[StampGroupSchema.Name] = value;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06002E1B RID: 11803 RVA: 0x000BBC8E File Offset: 0x000B9E8E
		public MultiValuedProperty<ADObjectId> Servers
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[StampGroupSchema.Servers];
			}
		}

		// Token: 0x06002E1C RID: 11804 RVA: 0x000BBCA0 File Offset: 0x000B9EA0
		internal bool IsStampGroupEmpty()
		{
			using (MultiValuedProperty<ADObjectId>.Enumerator enumerator = this.Servers.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ADObjectId adobjectId = enumerator.Current;
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001EF9 RID: 7929
		private static string mostDerivedClass = "msExchMDBAvailabilityGroup";

		// Token: 0x04001EFA RID: 7930
		[NonSerialized]
		private StampGroupSchema schema;
	}
}
