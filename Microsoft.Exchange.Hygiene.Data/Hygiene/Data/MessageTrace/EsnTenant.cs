using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Hygiene.Data.MessageTrace
{
	// Token: 0x02000141 RID: 321
	internal class EsnTenant : ConfigurablePropertyBag
	{
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000C77 RID: 3191 RVA: 0x00026F71 File Offset: 0x00025171
		public Guid OrganizationalUnitRoot
		{
			get
			{
				return (Guid)this[EsnTenantSchema.OrganizationalUnitRootProperty];
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00026F83 File Offset: 0x00025183
		public int RecipientCount
		{
			get
			{
				return (int)this[EsnTenantSchema.RecipientCountProperty];
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00026F95 File Offset: 0x00025195
		public int MessageCount
		{
			get
			{
				return (int)this[EsnTenantSchema.MessageCountProperty];
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00026FA7 File Offset: 0x000251A7
		public override ObjectId Identity
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x00026FAE File Offset: 0x000251AE
		public override Type GetSchemaType()
		{
			return typeof(EsnTenantSchema);
		}
	}
}
