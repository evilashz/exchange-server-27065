using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x0200076C RID: 1900
	internal class SystemAttendantMailboxPresentationObjectSchema : MailEnabledRecipientSchema
	{
		// Token: 0x06005D5F RID: 23903 RVA: 0x001423C9 File Offset: 0x001405C9
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<ADSystemAttendantMailboxSchema>();
		}
	}
}
