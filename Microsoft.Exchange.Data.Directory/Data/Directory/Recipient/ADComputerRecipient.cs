using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x020001EC RID: 492
	[Serializable]
	public class ADComputerRecipient : ADUser
	{
		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060018B9 RID: 6329 RVA: 0x0006B0CE File Offset: 0x000692CE
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADComputerRecipient.schema;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060018BA RID: 6330 RVA: 0x0006B0D5 File Offset: 0x000692D5
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADComputerRecipient.MostDerivedClass;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060018BB RID: 6331 RVA: 0x0006B0DC File Offset: 0x000692DC
		internal override QueryFilter ImplicitFilter
		{
			get
			{
				return ADComputerRecipient.ImplicitFilterInternal;
			}
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0006B0E3 File Offset: 0x000692E3
		internal ADComputerRecipient(IRecipientSession session, PropertyBag propertyBag) : base(session, propertyBag)
		{
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0006B0ED File Offset: 0x000692ED
		public ADComputerRecipient()
		{
		}

		// Token: 0x04000B52 RID: 2898
		private static readonly ADComputerRecipientSchema schema = ObjectSchema.GetInstance<ADComputerRecipientSchema>();

		// Token: 0x04000B53 RID: 2899
		internal new static string MostDerivedClass = "computer";

		// Token: 0x04000B54 RID: 2900
		private static string objectCategoryName = "computer";

		// Token: 0x04000B55 RID: 2901
		internal new static QueryFilter ImplicitFilterInternal = new AndFilter(new QueryFilter[]
		{
			ADObject.ObjectClassFilter(ADComputerRecipient.MostDerivedClass),
			ADObject.ObjectCategoryFilter(ADComputerRecipient.objectCategoryName)
		});
	}
}
