using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009BF RID: 2495
	[Serializable]
	public class AdminAuditLogSearch : AuditLogSearchBase
	{
		// Token: 0x17001946 RID: 6470
		// (get) Token: 0x06005C22 RID: 23586 RVA: 0x001801BF File Offset: 0x0017E3BF
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return AdminAuditLogSearch.schema;
			}
		}

		// Token: 0x17001947 RID: 6471
		// (get) Token: 0x06005C23 RID: 23587 RVA: 0x001801C6 File Offset: 0x0017E3C6
		internal override SearchFilter ItemClassFilter
		{
			get
			{
				return new SearchFilter.ContainsSubstring(ItemSchema.ItemClass, "IPM.AuditLogSearch.Admin", 1, 0);
			}
		}

		// Token: 0x17001948 RID: 6472
		// (get) Token: 0x06005C25 RID: 23589 RVA: 0x001801E1 File Offset: 0x0017E3E1
		// (set) Token: 0x06005C26 RID: 23590 RVA: 0x001801F3 File Offset: 0x0017E3F3
		public MultiValuedProperty<string> Cmdlets
		{
			get
			{
				return (MultiValuedProperty<string>)this[AdminAuditLogSearchEwsSchema.Cmdlets];
			}
			set
			{
				this[AdminAuditLogSearchEwsSchema.Cmdlets] = value;
			}
		}

		// Token: 0x17001949 RID: 6473
		// (get) Token: 0x06005C27 RID: 23591 RVA: 0x00180201 File Offset: 0x0017E401
		// (set) Token: 0x06005C28 RID: 23592 RVA: 0x00180213 File Offset: 0x0017E413
		public MultiValuedProperty<string> Parameters
		{
			get
			{
				return (MultiValuedProperty<string>)this[AdminAuditLogSearchEwsSchema.Parameters];
			}
			set
			{
				this[AdminAuditLogSearchEwsSchema.Parameters] = value;
			}
		}

		// Token: 0x1700194A RID: 6474
		// (get) Token: 0x06005C29 RID: 23593 RVA: 0x00180221 File Offset: 0x0017E421
		// (set) Token: 0x06005C2A RID: 23594 RVA: 0x00180233 File Offset: 0x0017E433
		public MultiValuedProperty<string> ObjectIds
		{
			get
			{
				return (MultiValuedProperty<string>)this[AdminAuditLogSearchEwsSchema.ObjectIds];
			}
			set
			{
				this[AdminAuditLogSearchEwsSchema.ObjectIds] = value;
			}
		}

		// Token: 0x1700194B RID: 6475
		// (get) Token: 0x06005C2B RID: 23595 RVA: 0x00180241 File Offset: 0x0017E441
		// (set) Token: 0x06005C2C RID: 23596 RVA: 0x00180253 File Offset: 0x0017E453
		public MultiValuedProperty<string> UserIds
		{
			get
			{
				return (MultiValuedProperty<string>)this[AdminAuditLogSearchEwsSchema.UserIds];
			}
			set
			{
				this[AdminAuditLogSearchEwsSchema.UserIds] = value;
			}
		}

		// Token: 0x1700194C RID: 6476
		// (get) Token: 0x06005C2D RID: 23597 RVA: 0x00180261 File Offset: 0x0017E461
		// (set) Token: 0x06005C2E RID: 23598 RVA: 0x00180273 File Offset: 0x0017E473
		internal MultiValuedProperty<string> ResolvedUsers
		{
			get
			{
				return (MultiValuedProperty<string>)this[AdminAuditLogSearchEwsSchema.ResolvedUsers];
			}
			set
			{
				this[AdminAuditLogSearchEwsSchema.ResolvedUsers] = value;
			}
		}

		// Token: 0x1700194D RID: 6477
		// (get) Token: 0x06005C2F RID: 23599 RVA: 0x00180281 File Offset: 0x0017E481
		// (set) Token: 0x06005C30 RID: 23600 RVA: 0x00180293 File Offset: 0x0017E493
		internal bool RedactDatacenterAdmins
		{
			get
			{
				return (bool)this[AdminAuditLogSearchEwsSchema.RedactDatacenterAdmins];
			}
			set
			{
				this[AdminAuditLogSearchEwsSchema.RedactDatacenterAdmins] = value;
			}
		}

		// Token: 0x1700194E RID: 6478
		// (get) Token: 0x06005C31 RID: 23601 RVA: 0x001802A6 File Offset: 0x0017E4A6
		internal override string ItemClass
		{
			get
			{
				return "IPM.AuditLogSearch.Admin";
			}
		}

		// Token: 0x040032C4 RID: 12996
		private const string ItemClassPrefix = "IPM.AuditLogSearch.Admin";

		// Token: 0x040032C5 RID: 12997
		private static ObjectSchema schema = new AdminAuditLogSearchEwsSchema();
	}
}
