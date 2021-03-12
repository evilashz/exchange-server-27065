using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x02000063 RID: 99
	internal class AuditProperty : ConfigurablePropertyBag
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000AE08 File Offset: 0x00009008
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(this.InstanceId.ToString() + this.Sequence.ToString());
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000AE41 File Offset: 0x00009041
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x0000AE53 File Offset: 0x00009053
		public string UserId
		{
			get
			{
				return this[AuditProperty.UserIdProp] as string;
			}
			set
			{
				this[AuditProperty.UserIdProp] = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000AE61 File Offset: 0x00009061
		// (set) Token: 0x060003DA RID: 986 RVA: 0x0000AE73 File Offset: 0x00009073
		public Guid AuditId
		{
			get
			{
				return (Guid)this[AuditProperty.AuditIdProp];
			}
			set
			{
				this[AuditProperty.AuditIdProp] = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000AE86 File Offset: 0x00009086
		// (set) Token: 0x060003DC RID: 988 RVA: 0x0000AE98 File Offset: 0x00009098
		public int Sequence
		{
			get
			{
				return (int)this[AuditProperty.SequenceProp];
			}
			set
			{
				this[AuditProperty.SequenceProp] = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000AEAB File Offset: 0x000090AB
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0000AEC2 File Offset: 0x000090C2
		public Guid? InstanceId
		{
			get
			{
				return this[AuditProperty.InstanceIdProp] as Guid?;
			}
			set
			{
				this[AuditProperty.InstanceIdProp] = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000AED5 File Offset: 0x000090D5
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0000AEE7 File Offset: 0x000090E7
		public string EntityName
		{
			get
			{
				return this[AuditProperty.EntityNameProp] as string;
			}
			set
			{
				this[AuditProperty.EntityNameProp] = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000AEF5 File Offset: 0x000090F5
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000AF07 File Offset: 0x00009107
		public string PropertyName
		{
			get
			{
				return this[AuditProperty.PropertyNameProp] as string;
			}
			set
			{
				this[AuditProperty.PropertyNameProp] = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000AF15 File Offset: 0x00009115
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000AF27 File Offset: 0x00009127
		public int PropertyIndex
		{
			get
			{
				return (int)this[AuditProperty.PropertyIndexProp];
			}
			set
			{
				this[AuditProperty.PropertyIndexProp] = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000AF3A File Offset: 0x0000913A
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000AF51 File Offset: 0x00009151
		public int? IntegerChange
		{
			get
			{
				return this[AuditProperty.IntegerChangeProp] as int?;
			}
			set
			{
				this[AuditProperty.IntegerChangeProp] = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000AF64 File Offset: 0x00009164
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000AF76 File Offset: 0x00009176
		public string StringChange
		{
			get
			{
				return this[AuditProperty.StringChangeProp] as string;
			}
			set
			{
				this[AuditProperty.StringChangeProp] = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000AF84 File Offset: 0x00009184
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000AF9B File Offset: 0x0000919B
		public DateTime? DateTimeChange
		{
			get
			{
				return this[AuditProperty.DateTimeChangeProp] as DateTime?;
			}
			set
			{
				this[AuditProperty.DateTimeChangeProp] = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000AFAE File Offset: 0x000091AE
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000AFC5 File Offset: 0x000091C5
		public double? DecimalChange
		{
			get
			{
				return this[AuditProperty.DecimalChangeProp] as double?;
			}
			set
			{
				this[AuditProperty.DecimalChangeProp] = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000AFD8 File Offset: 0x000091D8
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000AFEF File Offset: 0x000091EF
		public Guid? IdChange
		{
			get
			{
				return this[AuditProperty.IdChangeProp] as Guid?;
			}
			set
			{
				this[AuditProperty.EntityNameProp] = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000B002 File Offset: 0x00009202
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000B014 File Offset: 0x00009214
		public string BlobChange
		{
			get
			{
				return this[AuditProperty.BlobChangeProp] as string;
			}
			set
			{
				this[AuditProperty.BlobChangeProp] = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000B022 File Offset: 0x00009222
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000B039 File Offset: 0x00009239
		public bool? BoolChange
		{
			get
			{
				return this[AuditProperty.BoolChangeProp] as bool?;
			}
			set
			{
				this[AuditProperty.BoolChangeProp] = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000B04C File Offset: 0x0000924C
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0000B05E File Offset: 0x0000925E
		public bool Deleted
		{
			get
			{
				return (bool)this[AuditProperty.RecordSoftDeleted];
			}
			set
			{
				this[AuditProperty.RecordSoftDeleted] = value;
			}
		}

		// Token: 0x04000255 RID: 597
		public static readonly HygienePropertyDefinition PartitionIdProp = new HygienePropertyDefinition("id_PartitionId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000256 RID: 598
		public static readonly HygienePropertyDefinition UserIdProp = new HygienePropertyDefinition("nvc_UserId", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000257 RID: 599
		public static readonly HygienePropertyDefinition AuditIdProp = new HygienePropertyDefinition("id_AuditId", typeof(Guid), Guid.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000258 RID: 600
		public static readonly HygienePropertyDefinition SequenceProp = new HygienePropertyDefinition("i_Sequence", typeof(int), -1, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000259 RID: 601
		public static readonly HygienePropertyDefinition InstanceIdProp = new HygienePropertyDefinition("id_InstanceId", typeof(Guid));

		// Token: 0x0400025A RID: 602
		public static readonly HygienePropertyDefinition EntityNameProp = new HygienePropertyDefinition("nvc_EntityName", typeof(string));

		// Token: 0x0400025B RID: 603
		public static readonly HygienePropertyDefinition PropertyNameProp = new HygienePropertyDefinition("nvc_PropertyName", typeof(string));

		// Token: 0x0400025C RID: 604
		public static readonly HygienePropertyDefinition PropertyIndexProp = new HygienePropertyDefinition("nvc_PropertyIndex", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400025D RID: 605
		public static readonly HygienePropertyDefinition IntegerChangeProp = new HygienePropertyDefinition("i_PropertyValueInteger", typeof(int?));

		// Token: 0x0400025E RID: 606
		public static readonly HygienePropertyDefinition StringChangeProp = new HygienePropertyDefinition("nvc_PropertyValueString", typeof(string));

		// Token: 0x0400025F RID: 607
		public static readonly HygienePropertyDefinition DateTimeChangeProp = new HygienePropertyDefinition("dt_PropertyValueDateTime", typeof(DateTime?));

		// Token: 0x04000260 RID: 608
		public static readonly HygienePropertyDefinition DecimalChangeProp = new HygienePropertyDefinition("d_PropertyValueDecimal", typeof(double?));

		// Token: 0x04000261 RID: 609
		public static readonly HygienePropertyDefinition IdChangeProp = new HygienePropertyDefinition("id_PropertyValueGuid", typeof(Guid));

		// Token: 0x04000262 RID: 610
		public static readonly HygienePropertyDefinition BlobChangeProp = new HygienePropertyDefinition("nvc_PropertyValueBlob", typeof(string));

		// Token: 0x04000263 RID: 611
		public static readonly HygienePropertyDefinition BoolChangeProp = new HygienePropertyDefinition("f_PropertyValueBit", typeof(bool?));

		// Token: 0x04000264 RID: 612
		public static readonly HygienePropertyDefinition RecordSoftDeleted = new HygienePropertyDefinition("f_deleted", typeof(bool));

		// Token: 0x04000265 RID: 613
		public static readonly Dictionary<Type, HygienePropertyDefinition> TypeToProertyMap = new Dictionary<Type, HygienePropertyDefinition>
		{
			{
				AuditProperty.IdChangeProp.Type,
				AuditProperty.IdChangeProp
			},
			{
				AuditProperty.IntegerChangeProp.Type,
				AuditProperty.IntegerChangeProp
			},
			{
				AuditProperty.StringChangeProp.Type,
				AuditProperty.StringChangeProp
			},
			{
				AuditProperty.DateTimeChangeProp.Type,
				AuditProperty.DateTimeChangeProp
			},
			{
				AuditProperty.BoolChangeProp.Type,
				AuditProperty.BoolChangeProp
			},
			{
				AuditProperty.DecimalChangeProp.Type,
				AuditProperty.DecimalChangeProp
			}
		};
	}
}
