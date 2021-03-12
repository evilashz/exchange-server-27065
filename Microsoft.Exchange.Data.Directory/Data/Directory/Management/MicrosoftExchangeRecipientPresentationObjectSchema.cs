using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200073E RID: 1854
	internal class MicrosoftExchangeRecipientPresentationObjectSchema : MailEnabledRecipientSchema
	{
		// Token: 0x06005A19 RID: 23065 RVA: 0x0013CE5E File Offset: 0x0013B05E
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADMicrosoftExchangeRecipientSchema>();
		}
	}
}
