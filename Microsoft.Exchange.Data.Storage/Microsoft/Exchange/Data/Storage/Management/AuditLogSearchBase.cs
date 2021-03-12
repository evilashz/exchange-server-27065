using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009BD RID: 2493
	[Serializable]
	public class AuditLogSearchBase : EwsStoreObject
	{
		// Token: 0x17001931 RID: 6449
		// (get) Token: 0x06005BFA RID: 23546 RVA: 0x0017FF80 File Offset: 0x0017E180
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return AuditLogSearchBase.schema;
			}
		}

		// Token: 0x17001932 RID: 6450
		// (get) Token: 0x06005BFB RID: 23547 RVA: 0x0017FF87 File Offset: 0x0017E187
		internal override SearchFilter ItemClassFilter
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001933 RID: 6451
		// (get) Token: 0x06005BFC RID: 23548 RVA: 0x0017FF8A File Offset: 0x0017E18A
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17001934 RID: 6452
		// (get) Token: 0x06005BFD RID: 23549 RVA: 0x0017FF91 File Offset: 0x0017E191
		// (set) Token: 0x06005BFE RID: 23550 RVA: 0x0017FF99 File Offset: 0x0017E199
		protected new string AlternativeId
		{
			get
			{
				return base.AlternativeId;
			}
			set
			{
				base.AlternativeId = value;
			}
		}

		// Token: 0x17001935 RID: 6453
		// (get) Token: 0x06005BFF RID: 23551 RVA: 0x0017FFA2 File Offset: 0x0017E1A2
		// (set) Token: 0x06005C00 RID: 23552 RVA: 0x0017FFB4 File Offset: 0x0017E1B4
		public new AuditLogSearchId Identity
		{
			get
			{
				return (AuditLogSearchId)this[AuditLogSearchBaseEwsSchema.Identity];
			}
			set
			{
				this[AuditLogSearchBaseEwsSchema.Identity] = value;
			}
		}

		// Token: 0x17001936 RID: 6454
		// (get) Token: 0x06005C01 RID: 23553 RVA: 0x0017FFC2 File Offset: 0x0017E1C2
		// (set) Token: 0x06005C02 RID: 23554 RVA: 0x0017FFD4 File Offset: 0x0017E1D4
		public string Name
		{
			get
			{
				return (string)this[AuditLogSearchBaseEwsSchema.Name];
			}
			set
			{
				this[AuditLogSearchBaseEwsSchema.Name] = value;
			}
		}

		// Token: 0x17001937 RID: 6455
		// (get) Token: 0x06005C03 RID: 23555 RVA: 0x0017FFE2 File Offset: 0x0017E1E2
		// (set) Token: 0x06005C04 RID: 23556 RVA: 0x0017FFF4 File Offset: 0x0017E1F4
		public DateTime CreationTime
		{
			get
			{
				return (DateTime)this[AuditLogSearchBaseEwsSchema.CreationTime];
			}
			set
			{
				this[AuditLogSearchBaseEwsSchema.CreationTime] = value;
			}
		}

		// Token: 0x17001938 RID: 6456
		// (get) Token: 0x06005C05 RID: 23557 RVA: 0x00180007 File Offset: 0x0017E207
		// (set) Token: 0x06005C06 RID: 23558 RVA: 0x00180019 File Offset: 0x0017E219
		public DateTime? StartDateUtc
		{
			get
			{
				return (DateTime?)this[AuditLogSearchBaseEwsSchema.StartDateUtc];
			}
			set
			{
				this[AuditLogSearchBaseEwsSchema.StartDateUtc] = value;
			}
		}

		// Token: 0x17001939 RID: 6457
		// (get) Token: 0x06005C07 RID: 23559 RVA: 0x0018002C File Offset: 0x0017E22C
		// (set) Token: 0x06005C08 RID: 23560 RVA: 0x0018003E File Offset: 0x0017E23E
		public DateTime? EndDateUtc
		{
			get
			{
				return (DateTime?)this[AuditLogSearchBaseEwsSchema.EndDateUtc];
			}
			set
			{
				this[AuditLogSearchBaseEwsSchema.EndDateUtc] = value;
			}
		}

		// Token: 0x1700193A RID: 6458
		// (get) Token: 0x06005C09 RID: 23561 RVA: 0x00180051 File Offset: 0x0017E251
		// (set) Token: 0x06005C0A RID: 23562 RVA: 0x00180063 File Offset: 0x0017E263
		public MultiValuedProperty<string> StatusMailRecipients
		{
			get
			{
				return (MultiValuedProperty<string>)this[AuditLogSearchBaseEwsSchema.StatusMailRecipients];
			}
			set
			{
				this[AuditLogSearchBaseEwsSchema.StatusMailRecipients] = value;
			}
		}

		// Token: 0x1700193B RID: 6459
		// (get) Token: 0x06005C0B RID: 23563 RVA: 0x00180071 File Offset: 0x0017E271
		// (set) Token: 0x06005C0C RID: 23564 RVA: 0x00180083 File Offset: 0x0017E283
		public string CreatedBy
		{
			get
			{
				return (string)this[AuditLogSearchBaseEwsSchema.CreatedBy];
			}
			set
			{
				this[AuditLogSearchBaseEwsSchema.CreatedBy] = value;
			}
		}

		// Token: 0x1700193C RID: 6460
		// (get) Token: 0x06005C0D RID: 23565 RVA: 0x00180091 File Offset: 0x0017E291
		// (set) Token: 0x06005C0E RID: 23566 RVA: 0x001800A3 File Offset: 0x0017E2A3
		public string ExternalAccess
		{
			get
			{
				return (string)this[AuditLogSearchBaseEwsSchema.ExternalAccess];
			}
			set
			{
				this[AuditLogSearchBaseEwsSchema.ExternalAccess] = value;
			}
		}

		// Token: 0x1700193D RID: 6461
		// (get) Token: 0x06005C0F RID: 23567 RVA: 0x001800B1 File Offset: 0x0017E2B1
		// (set) Token: 0x06005C10 RID: 23568 RVA: 0x001800C3 File Offset: 0x0017E2C3
		public ADObjectId CreatedByEx
		{
			get
			{
				return (ADObjectId)this[AuditLogSearchBaseEwsSchema.CreatedByEx];
			}
			set
			{
				this[AuditLogSearchBaseEwsSchema.CreatedByEx] = value;
			}
		}

		// Token: 0x1700193E RID: 6462
		// (get) Token: 0x06005C11 RID: 23569 RVA: 0x001800D1 File Offset: 0x0017E2D1
		// (set) Token: 0x06005C12 RID: 23570 RVA: 0x001800E3 File Offset: 0x0017E2E3
		public string Type
		{
			get
			{
				return (string)this[AuditLogSearchBaseEwsSchema.Type];
			}
			set
			{
				this[AuditLogSearchBaseEwsSchema.Type] = value;
			}
		}

		// Token: 0x040032C1 RID: 12993
		private static ObjectSchema schema = new AuditLogSearchBaseEwsSchema();
	}
}
