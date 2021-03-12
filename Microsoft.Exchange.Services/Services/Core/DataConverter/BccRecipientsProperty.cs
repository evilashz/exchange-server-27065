using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x0200017A RID: 378
	internal sealed class BccRecipientsProperty : RecipientsPropertyBase
	{
		// Token: 0x06000AD4 RID: 2772 RVA: 0x00034610 File Offset: 0x00032810
		public BccRecipientsProperty(CommandContext commandContext) : base(commandContext, RecipientItemType.Bcc)
		{
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0003461A File Offset: 0x0003281A
		public static BccRecipientsProperty CreateCommand(CommandContext commandContext)
		{
			return new BccRecipientsProperty(commandContext);
		}
	}
}
