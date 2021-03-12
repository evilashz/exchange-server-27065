using System;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x02000782 RID: 1922
	internal static class ReducedRecipientProperties
	{
		// Token: 0x17002279 RID: 8825
		// (get) Token: 0x06006054 RID: 24660 RVA: 0x001475EA File Offset: 0x001457EA
		public static ReadOnlyCollection<PropertyDefinition> AllProperties
		{
			get
			{
				return ReducedRecipientProperties.recipientSchema.AllProperties;
			}
		}

		// Token: 0x04004061 RID: 16481
		private static ReducedRecipientSchema recipientSchema = ObjectSchema.GetInstance<ReducedRecipientSchema>();
	}
}
