using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000A9 RID: 169
	internal class TableTraceRecord : ConfigurablePropertyBag
	{
		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x00012C27 File Offset: 0x00010E27
		public override ObjectId Identity
		{
			get
			{
				return new ConfigObjectId(string.Format("T:{0}-S:{1}-D:{2}", this.TableName, this.SourceDatabase, this.DestinationDatabase));
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00012C4A File Offset: 0x00010E4A
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x00012C5C File Offset: 0x00010E5C
		public string TableName
		{
			get
			{
				return this[TableTraceRecord.TableNameProp] as string;
			}
			set
			{
				this[TableTraceRecord.TableNameProp] = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00012C6A File Offset: 0x00010E6A
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x00012C7C File Offset: 0x00010E7C
		public string SourceDatabase
		{
			get
			{
				return this[TableTraceRecord.SourceDatabaseProp] as string;
			}
			set
			{
				this[TableTraceRecord.SourceDatabaseProp] = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00012C8A File Offset: 0x00010E8A
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x00012C9C File Offset: 0x00010E9C
		public string DestinationDatabase
		{
			get
			{
				return this[TableTraceRecord.DestinationDatabaseProp] as string;
			}
			set
			{
				this[TableTraceRecord.DestinationDatabaseProp] = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x00012CAA File Offset: 0x00010EAA
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x00012CBC File Offset: 0x00010EBC
		public DateTime SourceDatetime
		{
			get
			{
				return (DateTime)this[TableTraceRecord.SourceDatetimeProp];
			}
			set
			{
				this[TableTraceRecord.SourceDatetimeProp] = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x00012CCF File Offset: 0x00010ECF
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x00012CE1 File Offset: 0x00010EE1
		public DateTime DestinationDatetime
		{
			get
			{
				return (DateTime)this[TableTraceRecord.DestinationDatetimeProp];
			}
			set
			{
				this[TableTraceRecord.DestinationDatetimeProp] = value;
			}
		}

		// Token: 0x04000379 RID: 889
		public static readonly HygienePropertyDefinition TableNameProp = new HygienePropertyDefinition("TableName", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400037A RID: 890
		public static readonly HygienePropertyDefinition SourceDatabaseProp = new HygienePropertyDefinition("SourceDatabase", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400037B RID: 891
		public static readonly HygienePropertyDefinition DestinationDatabaseProp = new HygienePropertyDefinition("DestinationDatabase", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400037C RID: 892
		public static readonly HygienePropertyDefinition SourceDatetimeProp = new HygienePropertyDefinition("SourceDatetime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400037D RID: 893
		public static readonly HygienePropertyDefinition DestinationDatetimeProp = new HygienePropertyDefinition("DestinationDatetime", typeof(DateTime), DateTime.MinValue, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
