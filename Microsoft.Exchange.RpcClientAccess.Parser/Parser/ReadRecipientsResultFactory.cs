using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Parser
{
	// Token: 0x02000112 RID: 274
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ReadRecipientsResultFactory : StandardResultFactory
	{
		// Token: 0x06000584 RID: 1412 RVA: 0x0001048B File Offset: 0x0000E68B
		internal ReadRecipientsResultFactory(int maxSize) : base(RopId.ReadRecipients)
		{
			this.maxSize = maxSize;
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x0001049C File Offset: 0x0000E69C
		public RecipientCollector CreateRecipientCollector(PropertyTag[] extraPropertyTags)
		{
			return new RecipientCollector(this.maxSize - 6 - 1, extraPropertyTags, RecipientSerializationFlags.RecipientRowId | RecipientSerializationFlags.ExtraUnicodeProperties | RecipientSerializationFlags.CodePageId);
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x000104AF File Offset: 0x0000E6AF
		public RopResult CreateSuccessfulResult(RecipientCollector recipientCollector)
		{
			return new SuccessfulReadRecipientsResult(recipientCollector);
		}

		// Token: 0x04000313 RID: 787
		private readonly int maxSize;
	}
}
