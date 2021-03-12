using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200017B RID: 379
	internal sealed class CcRecipientsProperty : RecipientsPropertyBase
	{
		// Token: 0x06000AD6 RID: 2774 RVA: 0x00034622 File Offset: 0x00032822
		public CcRecipientsProperty(CommandContext commandContext) : base(commandContext, RecipientItemType.Cc)
		{
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0003462C File Offset: 0x0003282C
		public static CcRecipientsProperty CreateCommand(CommandContext commandContext)
		{
			return new CcRecipientsProperty(commandContext);
		}
	}
}
