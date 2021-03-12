using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000183 RID: 387
	internal sealed class ToRecipientsProperty : RecipientsPropertyBase
	{
		// Token: 0x06000B0C RID: 2828 RVA: 0x00034E68 File Offset: 0x00033068
		public ToRecipientsProperty(CommandContext commandContext) : base(commandContext, RecipientItemType.To)
		{
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x00034E72 File Offset: 0x00033072
		public static ToRecipientsProperty CreateCommand(CommandContext commandContext)
		{
			return new ToRecipientsProperty(commandContext);
		}
	}
}
