using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200076E RID: 1902
	internal class SystemMailboxPresentationObjectSchema : MailEnabledRecipientSchema
	{
		// Token: 0x06005D65 RID: 23909 RVA: 0x001423FC File Offset: 0x001405FC
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADSystemMailboxSchema>();
		}
	}
}
