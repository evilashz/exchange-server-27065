using System;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200027E RID: 638
	internal class ADRecipientProperties : ADPropertyUnionSchema
	{
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001E13 RID: 7699 RVA: 0x000883FF File Offset: 0x000865FF
		public static ADRecipientProperties Instance
		{
			get
			{
				if (ADRecipientProperties.instance == null)
				{
					ADRecipientProperties.instance = ObjectSchema.GetInstance<ADRecipientProperties>();
				}
				return ADRecipientProperties.instance;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001E14 RID: 7700 RVA: 0x00088417 File Offset: 0x00086617
		public override ReadOnlyCollection<ADObjectSchema> ObjectSchemas
		{
			get
			{
				return ADRecipientProperties.AllRecipientSchemas;
			}
		}

		// Token: 0x040011EF RID: 4591
		private static readonly ReadOnlyCollection<ADObjectSchema> AllRecipientSchemas = new ReadOnlyCollection<ADObjectSchema>(new ADRecipientSchema[]
		{
			ObjectSchema.GetInstance<ADContactSchema>(),
			ObjectSchema.GetInstance<ADDynamicGroupSchema>(),
			ObjectSchema.GetInstance<ADGroupSchema>(),
			ObjectSchema.GetInstance<ADPublicDatabaseSchema>(),
			ObjectSchema.GetInstance<ADPublicFolderSchema>(),
			ObjectSchema.GetInstance<ADSystemAttendantMailboxSchema>(),
			ObjectSchema.GetInstance<ADSystemMailboxSchema>(),
			ObjectSchema.GetInstance<ADUserSchema>()
		});

		// Token: 0x040011F0 RID: 4592
		private static ADRecipientProperties instance;
	}
}
