using System;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000759 RID: 1881
	internal class SyncDeletedObjectSchema : ADPresentationSchema
	{
		// Token: 0x06005B51 RID: 23377 RVA: 0x0013F997 File Offset: 0x0013DB97
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<DeletedObjectSchema>();
		}

		// Token: 0x04003E29 RID: 15913
		public static readonly ADPropertyDefinition EndOfList = DeletedObjectSchema.EndOfList;

		// Token: 0x04003E2A RID: 15914
		public static readonly ADPropertyDefinition Cookie = DeletedObjectSchema.Cookie;
	}
}
