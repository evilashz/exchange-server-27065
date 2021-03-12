using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Spam
{
	// Token: 0x020001D6 RID: 470
	internal class DeleteAll : ConfigurablePropertyBag
	{
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x0003B2F0 File Offset: 0x000394F0
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(Guid.NewGuid().ToString());
			}
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x0003B315 File Offset: 0x00039515
		// (set) Token: 0x060013B4 RID: 5044 RVA: 0x0003B327 File Offset: 0x00039527
		public string EntityName
		{
			get
			{
				return (string)this[DeleteAll.EntityNameProperty];
			}
			set
			{
				this[DeleteAll.EntityNameProperty] = value;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x0003B335 File Offset: 0x00039535
		// (set) Token: 0x060013B6 RID: 5046 RVA: 0x0003B347 File Offset: 0x00039547
		public string PropertyName
		{
			get
			{
				return (string)this[DeleteAll.PropertyNameProperty];
			}
			set
			{
				this[DeleteAll.PropertyNameProperty] = value;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x0003B355 File Offset: 0x00039555
		// (set) Token: 0x060013B8 RID: 5048 RVA: 0x0003B367 File Offset: 0x00039567
		public int? IntValue
		{
			get
			{
				return (int?)this[DeleteAll.IntValueProperty];
			}
			set
			{
				this[DeleteAll.IntValueProperty] = value;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x0003B37A File Offset: 0x0003957A
		// (set) Token: 0x060013BA RID: 5050 RVA: 0x0003B38C File Offset: 0x0003958C
		public Guid? GuidValue
		{
			get
			{
				return (Guid?)this[DeleteAll.GuidValueProperty];
			}
			set
			{
				this[DeleteAll.GuidValueProperty] = value;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060013BB RID: 5051 RVA: 0x0003B39F File Offset: 0x0003959F
		// (set) Token: 0x060013BC RID: 5052 RVA: 0x0003B3B1 File Offset: 0x000395B1
		public string StringValue
		{
			get
			{
				return (string)this[DeleteAll.StringValueProperty];
			}
			set
			{
				this[DeleteAll.StringValueProperty] = value;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060013BD RID: 5053 RVA: 0x0003B3BF File Offset: 0x000395BF
		// (set) Token: 0x060013BE RID: 5054 RVA: 0x0003B3D1 File Offset: 0x000395D1
		public bool? BoolValue
		{
			get
			{
				return (bool?)this[DeleteAll.BoolValueProperty];
			}
			set
			{
				this[DeleteAll.BoolValueProperty] = value;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060013BF RID: 5055 RVA: 0x0003B3E4 File Offset: 0x000395E4
		// (set) Token: 0x060013C0 RID: 5056 RVA: 0x0003B3F6 File Offset: 0x000395F6
		public DateTime? DatetimeValue
		{
			get
			{
				return (DateTime?)this[DeleteAll.DatetimeValueProperty];
			}
			set
			{
				this[DeleteAll.DatetimeValueProperty] = value;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060013C1 RID: 5057 RVA: 0x0003B409 File Offset: 0x00039609
		// (set) Token: 0x060013C2 RID: 5058 RVA: 0x0003B41B File Offset: 0x0003961B
		public decimal? DecimalValue
		{
			get
			{
				return (decimal?)this[DeleteAll.DecimalValueProperty];
			}
			set
			{
				this[DeleteAll.DecimalValueProperty] = value;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060013C3 RID: 5059 RVA: 0x0003B42E File Offset: 0x0003962E
		// (set) Token: 0x060013C4 RID: 5060 RVA: 0x0003B440 File Offset: 0x00039640
		public long? LongValue
		{
			get
			{
				return (long?)this[DeleteAll.LongValueProperty];
			}
			set
			{
				this[DeleteAll.LongValueProperty] = value;
			}
		}

		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x060013C5 RID: 5061 RVA: 0x0003B453 File Offset: 0x00039653
		// (set) Token: 0x060013C6 RID: 5062 RVA: 0x0003B465 File Offset: 0x00039665
		public bool? HardDelete
		{
			get
			{
				return (bool?)this[DeleteAll.HardDeleteProperty];
			}
			set
			{
				this[DeleteAll.HardDeleteProperty] = value;
			}
		}

		// Token: 0x0400097B RID: 2427
		public static readonly HygienePropertyDefinition EntityNameProperty = new HygienePropertyDefinition("nvc_EntityName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400097C RID: 2428
		public static readonly HygienePropertyDefinition PropertyNameProperty = new HygienePropertyDefinition("nvc_PropertyName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400097D RID: 2429
		public static readonly HygienePropertyDefinition IntValueProperty = new HygienePropertyDefinition("i_PropertyValueInteger", typeof(int?));

		// Token: 0x0400097E RID: 2430
		public static readonly HygienePropertyDefinition GuidValueProperty = new HygienePropertyDefinition("id_PropertyValueGuid", typeof(Guid?));

		// Token: 0x0400097F RID: 2431
		public static readonly HygienePropertyDefinition StringValueProperty = new HygienePropertyDefinition("nvc_PropertyValueString", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000980 RID: 2432
		public static readonly HygienePropertyDefinition BoolValueProperty = new HygienePropertyDefinition("f_PropertyValueBit", typeof(bool?));

		// Token: 0x04000981 RID: 2433
		public static readonly HygienePropertyDefinition DatetimeValueProperty = new HygienePropertyDefinition("dt_PropertyValueDateTime", typeof(DateTime?));

		// Token: 0x04000982 RID: 2434
		public static readonly HygienePropertyDefinition DecimalValueProperty = new HygienePropertyDefinition("d_PropertyValueDecimal", typeof(decimal?));

		// Token: 0x04000983 RID: 2435
		public static readonly HygienePropertyDefinition LongValueProperty = new HygienePropertyDefinition("bi_PropertyValueLong", typeof(long?));

		// Token: 0x04000984 RID: 2436
		public static readonly HygienePropertyDefinition HardDeleteProperty = new HygienePropertyDefinition("f_HardDelete", typeof(bool?));

		// Token: 0x020001D7 RID: 471
		public static class Entity
		{
			// Token: 0x04000985 RID: 2437
			public const string PredicateExtendedProperties = "PredicateExtendedProperties";

			// Token: 0x04000986 RID: 2438
			public const string Predicates = "Predicates";

			// Token: 0x04000987 RID: 2439
			public const string Processors = "Processors";

			// Token: 0x04000988 RID: 2440
			public const string RuleExtendedProperties = "RuleExtendedProperties";

			// Token: 0x04000989 RID: 2441
			public const string Rules = "Rules";

			// Token: 0x0400098A RID: 2442
			public const string SpamRules = "SpamRules";

			// Token: 0x0400098B RID: 2443
			public const string SpamRuleProcessors = "SpamRuleProcessors";

			// Token: 0x0400098C RID: 2444
			public const string SyncWatermark = "SyncWatermark";

			// Token: 0x0400098D RID: 2445
			public const string SpamDataBlobs = "SpamDataBlobs";

			// Token: 0x0400098E RID: 2446
			public const string SpamExclusionData = "SpamExclusionData";
		}

		// Token: 0x020001D8 RID: 472
		public static class Property
		{
			// Token: 0x0400098F RID: 2447
			public const string IdPredicateId = "id_PredicateId";

			// Token: 0x04000990 RID: 2448
			public const string IdRuleId = "id_RuleId";

			// Token: 0x04000991 RID: 2449
			public const string BiRuleId = "bi_RuleId";

			// Token: 0x04000992 RID: 2450
			public const string BiProcessorId = "bi_ProcessorId";

			// Token: 0x04000993 RID: 2451
			public const string IdIdentity = "id_Identity";

			// Token: 0x04000994 RID: 2452
			public const string NvcSyncContext = "nvc_SyncContext";

			// Token: 0x04000995 RID: 2453
			public const string IdSpamExclusionDataId = "id_SpamExclusionDataId";

			// Token: 0x04000996 RID: 2454
			public const string IdSpamDataBlobId = "IdSpamDataBlobId";

			// Token: 0x04000997 RID: 2455
			public const string TiDataId = "ti_DataId";
		}
	}
}
