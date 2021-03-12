using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001D3 RID: 467
	internal class ConfigurablePropertyTable : ConfigurablePropertyBag
	{
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x0600138E RID: 5006 RVA: 0x0003AE64 File Offset: 0x00039064
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x0600138F RID: 5007 RVA: 0x0003AE89 File Offset: 0x00039089
		// (set) Token: 0x06001390 RID: 5008 RVA: 0x0003AE9B File Offset: 0x0003909B
		public string PropertyName
		{
			get
			{
				return (string)this[ConfigurablePropertyTable.NameProperty];
			}
			set
			{
				this[ConfigurablePropertyTable.NameProperty] = value;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x0003AEA9 File Offset: 0x000390A9
		// (set) Token: 0x06001392 RID: 5010 RVA: 0x0003AEBB File Offset: 0x000390BB
		public int? IntValue
		{
			get
			{
				return (int?)this[ConfigurablePropertyTable.IntValueProperty];
			}
			set
			{
				this[ConfigurablePropertyTable.IntValueProperty] = value;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x0003AECE File Offset: 0x000390CE
		// (set) Token: 0x06001394 RID: 5012 RVA: 0x0003AF08 File Offset: 0x00039108
		public string StringValue
		{
			get
			{
				if (!string.IsNullOrEmpty(this[ConfigurablePropertyTable.BlobValueProperty] as string))
				{
					return (string)this[ConfigurablePropertyTable.BlobValueProperty];
				}
				return (string)this[ConfigurablePropertyTable.StringValueProperty];
			}
			set
			{
				if (!string.IsNullOrEmpty(value) && value.Length > 255)
				{
					this[ConfigurablePropertyTable.BlobValueProperty] = value;
					return;
				}
				this[ConfigurablePropertyTable.StringValueProperty] = value;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x0003AF38 File Offset: 0x00039138
		// (set) Token: 0x06001396 RID: 5014 RVA: 0x0003AF4A File Offset: 0x0003914A
		public string BlobValue
		{
			get
			{
				return (string)this[ConfigurablePropertyTable.BlobValueProperty];
			}
			set
			{
				this[ConfigurablePropertyTable.BlobValueProperty] = value;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x0003AF58 File Offset: 0x00039158
		// (set) Token: 0x06001398 RID: 5016 RVA: 0x0003AF6A File Offset: 0x0003916A
		public Guid? GuidValue
		{
			get
			{
				return (Guid?)this[ConfigurablePropertyTable.GuidValueProperty];
			}
			set
			{
				this[ConfigurablePropertyTable.GuidValueProperty] = value;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x0003AF7D File Offset: 0x0003917D
		// (set) Token: 0x0600139A RID: 5018 RVA: 0x0003AF8F File Offset: 0x0003918F
		public bool? BoolValue
		{
			get
			{
				return (bool?)this[ConfigurablePropertyTable.BoolValueProperty];
			}
			set
			{
				this[ConfigurablePropertyTable.BoolValueProperty] = value;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x0003AFA2 File Offset: 0x000391A2
		// (set) Token: 0x0600139C RID: 5020 RVA: 0x0003AFB4 File Offset: 0x000391B4
		public DateTime? DatetimeValue
		{
			get
			{
				return (DateTime?)this[ConfigurablePropertyTable.DatetimeValueProperty];
			}
			set
			{
				this[ConfigurablePropertyTable.DatetimeValueProperty] = value;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x0003AFC7 File Offset: 0x000391C7
		// (set) Token: 0x0600139E RID: 5022 RVA: 0x0003AFD9 File Offset: 0x000391D9
		public decimal? DecimalValue
		{
			get
			{
				return (decimal?)this[ConfigurablePropertyTable.DecimalValueProperty];
			}
			set
			{
				this[ConfigurablePropertyTable.DecimalValueProperty] = value;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x0003AFEC File Offset: 0x000391EC
		// (set) Token: 0x060013A0 RID: 5024 RVA: 0x0003AFFE File Offset: 0x000391FE
		public long? LongValue
		{
			get
			{
				return (long?)this[ConfigurablePropertyTable.LongValueProperty];
			}
			set
			{
				this[ConfigurablePropertyTable.LongValueProperty] = value;
			}
		}

		// Token: 0x04000965 RID: 2405
		public static readonly HygienePropertyDefinition NameProperty = new HygienePropertyDefinition("nvc_PropertyName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000966 RID: 2406
		public static readonly HygienePropertyDefinition IndexProperty = new HygienePropertyDefinition("i_PropertyIndex", typeof(int?));

		// Token: 0x04000967 RID: 2407
		public static readonly HygienePropertyDefinition IntValueProperty = new HygienePropertyDefinition("i_PropertyValueInteger", typeof(int?));

		// Token: 0x04000968 RID: 2408
		public static readonly HygienePropertyDefinition StringValueProperty = new HygienePropertyDefinition("nvc_PropertyValueString", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000969 RID: 2409
		public static readonly HygienePropertyDefinition BoolValueProperty = new HygienePropertyDefinition("f_PropertyValueBit", typeof(bool?));

		// Token: 0x0400096A RID: 2410
		public static readonly HygienePropertyDefinition DatetimeValueProperty = new HygienePropertyDefinition("dt_PropertyValueDateTime", typeof(DateTime?));

		// Token: 0x0400096B RID: 2411
		public static readonly HygienePropertyDefinition DecimalValueProperty = new HygienePropertyDefinition("d_PropertyValueDecimal", typeof(decimal?));

		// Token: 0x0400096C RID: 2412
		public static readonly HygienePropertyDefinition LongValueProperty = new HygienePropertyDefinition("bi_PropertyValueLong", typeof(long?));

		// Token: 0x0400096D RID: 2413
		public static readonly HygienePropertyDefinition GuidValueProperty = new HygienePropertyDefinition("id_PropertyValueGuid", typeof(Guid?));

		// Token: 0x0400096E RID: 2414
		public static readonly HygienePropertyDefinition BlobValueProperty = new HygienePropertyDefinition("nvc_PropertyValueBlob", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
