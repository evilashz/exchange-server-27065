using System;
using System.Diagnostics;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000062 RID: 98
	[DebuggerDisplay("Property Name = {PropertyName, nq}; Value = {GetValue()};")]
	internal class AuditHistoryResult : ConfigurablePropertyBag
	{
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0000A968 File Offset: 0x00008B68
		public override ObjectId Identity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000A96F File Offset: 0x00008B6F
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0000A981 File Offset: 0x00008B81
		public string PropertyName
		{
			get
			{
				return this[AuditHistoryResult.PropertyNameDefinition] as string;
			}
			set
			{
				this[AuditHistoryResult.PropertyNameDefinition] = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000A98F File Offset: 0x00008B8F
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000A9A6 File Offset: 0x00008BA6
		public int? PropertyId
		{
			get
			{
				return this[AuditHistoryResult.PropertyIdDefinition] as int?;
			}
			set
			{
				this[AuditHistoryResult.PropertyIdDefinition] = value;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000A9B9 File Offset: 0x00008BB9
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		public Guid? PropertyValueGuid
		{
			get
			{
				return this[AuditHistoryResult.PropertyValueGuidDefinition] as Guid?;
			}
			set
			{
				this[AuditHistoryResult.PropertyValueGuidDefinition] = value;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000A9E3 File Offset: 0x00008BE3
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x0000A9FA File Offset: 0x00008BFA
		public int? PropertyValueInteger
		{
			get
			{
				return this[AuditHistoryResult.PropertyValueIntegerDefinition] as int?;
			}
			set
			{
				this[AuditHistoryResult.PropertyValueIntegerDefinition] = value;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000AA0D File Offset: 0x00008C0D
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000AA24 File Offset: 0x00008C24
		public long? PropertyValueLong
		{
			get
			{
				return this[AuditHistoryResult.PropertyValueLongDefinition] as long?;
			}
			set
			{
				this[AuditHistoryResult.PropertyValueLongDefinition] = value;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000AA37 File Offset: 0x00008C37
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x0000AA49 File Offset: 0x00008C49
		public string PropertyValueString
		{
			get
			{
				return this[AuditHistoryResult.PropertyValueStringDefinition] as string;
			}
			set
			{
				this[AuditHistoryResult.PropertyValueLongDefinition] = value;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000AA57 File Offset: 0x00008C57
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x0000AA6E File Offset: 0x00008C6E
		public DateTime? PropertyValueDateTime
		{
			get
			{
				return this[AuditHistoryResult.PropertyValueDateTimeDefinition] as DateTime?;
			}
			set
			{
				this[AuditHistoryResult.PropertyValueDateTimeDefinition] = value;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000AA81 File Offset: 0x00008C81
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0000AA98 File Offset: 0x00008C98
		public bool? PropertyValueBit
		{
			get
			{
				return this[AuditHistoryResult.PropertyValueBitDefinition] as bool?;
			}
			set
			{
				this[AuditHistoryResult.PropertyValueBitDefinition] = value;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000AAAB File Offset: 0x00008CAB
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0000AAC2 File Offset: 0x00008CC2
		public decimal? PropertyValueDecimal
		{
			get
			{
				return this[AuditHistoryResult.PropertyValueDecimalDefinition] as decimal?;
			}
			set
			{
				this[AuditHistoryResult.PropertyValueDecimalDefinition] = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000AAD5 File Offset: 0x00008CD5
		// (set) Token: 0x060003CE RID: 974 RVA: 0x0000AAE7 File Offset: 0x00008CE7
		public string PropertyValueBlob
		{
			get
			{
				return this[AuditHistoryResult.PropertyValueBlobDefinition] as string;
			}
			set
			{
				this[AuditHistoryResult.PropertyValueBlobDefinition] = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000AAF5 File Offset: 0x00008CF5
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x0000AB0C File Offset: 0x00008D0C
		public DateTime? ChangedDateTime
		{
			get
			{
				return this[AuditHistoryResult.ChangedDateTimeDefinition] as DateTime?;
			}
			set
			{
				this[AuditHistoryResult.ChangedDateTimeDefinition] = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000AB1F File Offset: 0x00008D1F
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x0000AB36 File Offset: 0x00008D36
		public DateTime? DeletedDateTime
		{
			get
			{
				return this[AuditHistoryResult.DeletedDateTimeDefinition] as DateTime?;
			}
			set
			{
				this[AuditHistoryResult.DeletedDateTimeDefinition] = value;
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000AB4C File Offset: 0x00008D4C
		public object GetValue()
		{
			object result = null;
			if (this.PropertyValueGuid != null)
			{
				result = this.PropertyValueGuid;
			}
			else if (this.PropertyValueInteger != null)
			{
				result = this.PropertyValueInteger;
			}
			else if (this.PropertyValueLong != null)
			{
				result = this.PropertyValueLong;
			}
			else if (!string.IsNullOrEmpty(this.PropertyValueString))
			{
				result = this.PropertyValueString;
			}
			else if (this.PropertyValueDateTime != null)
			{
				result = this.PropertyValueDateTime;
			}
			else if (this.PropertyValueBit != null)
			{
				result = this.PropertyValueBit;
			}
			else if (this.PropertyValueDecimal != null)
			{
				result = this.PropertyValueDecimal;
			}
			else if (!string.IsNullOrEmpty(this.PropertyValueBlob))
			{
				result = this.PropertyValueBlob;
			}
			return result;
		}

		// Token: 0x04000244 RID: 580
		public static readonly HygienePropertyDefinition EntityInstanceIdParameterDefinition = new HygienePropertyDefinition("id_EntityInstanceId", typeof(Guid?));

		// Token: 0x04000245 RID: 581
		public static readonly HygienePropertyDefinition EntityNameParameterDefinition = new HygienePropertyDefinition("nvc_EntityName", typeof(string));

		// Token: 0x04000246 RID: 582
		public static readonly HygienePropertyDefinition PartitionIdParameterDefinition = new HygienePropertyDefinition("id_PartitionId", typeof(Guid));

		// Token: 0x04000247 RID: 583
		public static readonly HygienePropertyDefinition StartTimeParameterDefinition = new HygienePropertyDefinition("dt_StartTime", typeof(DateTime?));

		// Token: 0x04000248 RID: 584
		public static readonly HygienePropertyDefinition EndTimeParameterDefinition = new HygienePropertyDefinition("dt_EndTime", typeof(DateTime?));

		// Token: 0x04000249 RID: 585
		public static readonly HygienePropertyDefinition PropertyIdDefinition = new HygienePropertyDefinition("i_PropertyId", typeof(int?));

		// Token: 0x0400024A RID: 586
		public static readonly HygienePropertyDefinition PropertyNameDefinition = new HygienePropertyDefinition("nvc_PropertyName", typeof(string));

		// Token: 0x0400024B RID: 587
		public static readonly HygienePropertyDefinition ChangedDateTimeDefinition = new HygienePropertyDefinition("dt_ChangedDatetime", typeof(DateTime?));

		// Token: 0x0400024C RID: 588
		public static readonly HygienePropertyDefinition DeletedDateTimeDefinition = new HygienePropertyDefinition("dt_DeletedDatetime", typeof(DateTime?));

		// Token: 0x0400024D RID: 589
		public static readonly HygienePropertyDefinition PropertyValueGuidDefinition = new HygienePropertyDefinition("id_PropertyValueGuid", typeof(Guid?));

		// Token: 0x0400024E RID: 590
		public static readonly HygienePropertyDefinition PropertyValueIntegerDefinition = new HygienePropertyDefinition("i_PropertyValueInteger", typeof(int?));

		// Token: 0x0400024F RID: 591
		public static readonly HygienePropertyDefinition PropertyValueLongDefinition = new HygienePropertyDefinition("bi_PropertyValueLong", typeof(long?));

		// Token: 0x04000250 RID: 592
		public static readonly HygienePropertyDefinition PropertyValueStringDefinition = new HygienePropertyDefinition("nvc_PropertyValueString", typeof(string));

		// Token: 0x04000251 RID: 593
		public static readonly HygienePropertyDefinition PropertyValueDateTimeDefinition = new HygienePropertyDefinition("dt_PropertyValueDateTime", typeof(DateTime?));

		// Token: 0x04000252 RID: 594
		public static readonly HygienePropertyDefinition PropertyValueBitDefinition = new HygienePropertyDefinition("f_PropertyValueBit", typeof(bool?));

		// Token: 0x04000253 RID: 595
		public static readonly HygienePropertyDefinition PropertyValueDecimalDefinition = new HygienePropertyDefinition("d_PropertyValueDecimal", typeof(decimal?));

		// Token: 0x04000254 RID: 596
		public static readonly HygienePropertyDefinition PropertyValueBlobDefinition = new HygienePropertyDefinition("nvc_PropertyValueBlob", typeof(string));
	}
}
